

using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using clean.application.Contract.Infrastructure.Security;
using Newtonsoft.Json;
using System.Globalization;

namespace api.Controllers;

[ApiController]
[Route("api/test-uxm")]
public class UxmTestController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly IAsymmetricCryptoService _asymmetricCryptoService;

    public UxmTestController(IHttpClientFactory httpClientFactory, IAsymmetricCryptoService asymmetricCryptoService)
    {
        _httpClient = httpClientFactory.CreateClient();
        _asymmetricCryptoService = asymmetricCryptoService;
    }

    [HttpPost("order-v1")]
    public async Task<IActionResult> SendPaymentDiV1()
    {
        var merchantNumber = "564955";
        var merchantOrderId = Guid.NewGuid().ToString();

        // ✅ Tạo dữ liệu input sẵn bên trong controller
        var input = new UxmShortInputV1
        {
            MemberId = "9e2d26f4-908e-49a3-818b-de7fff69858f",
            PayerBankAccountName = "string",
            FiatAmount = 10.0m,
            FiatType = "CNY"
        };

        var data = new UxmV1PayloadData
        {
            MerchantNumber = merchantNumber,
            MerchantOrderId = merchantOrderId,
            MemberId = input.MemberId,
            PayerBankAccountName = input.PayerBankAccountName,
            FiatAmount = input.FiatAmount,
            FiatType = input.FiatType
        };

        // ✅ Tạo chữ ký
        var signature = _asymmetricCryptoService.Sign(TestKeyPair.PrivateKey, data);

        // ✅ Payload cuối cùng
        var payload = new
        {
            data = data,
            signature = signature
        };

        var payloadJson = JsonConvert.SerializeObject(payload, Formatting.None);

        // ✅ HTTP POST request
        var content = new StringContent(payloadJson, Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Post, "https://developapi.uxm-pay.uk/v1/order/payment-di")
        {
            Content = content
        };

        request.Headers.Add("Signature", signature);

        var response = await _httpClient.SendAsync(request);
        var responseText = await response.Content.ReadAsStringAsync();

        return Ok(new
        {
            status = (int)response.StatusCode,
            sent = payloadJson,
            signature = signature,
            response = responseText
        });
    }
}

// ✅ Payload chuẩn
public class UxmV1PayloadData
{
    public string MerchantNumber { get; set; } = string.Empty;
    public string MerchantOrderId { get; set; } = string.Empty;
    public string MemberId { get; set; } = string.Empty;
    public string PayerBankAccountName { get; set; } = string.Empty;
    public decimal FiatAmount { get; set; }
    public string FiatType { get; set; } = string.Empty;
}

// ✅ Input mẫu
public class UxmShortInputV1
{
    public string MemberId { get; set; } = string.Empty;
    public string PayerBankAccountName { get; set; } = string.Empty;
    public decimal FiatAmount { get; set; }
    public string FiatType { get; set; } = string.Empty;
}
public class UxmTestResponse
{
    public int Status { get; set; }
    public string Sent { get; set; } = string.Empty;
    public string Signature { get; set; } = string.Empty;
    public string Response { get; set; } = string.Empty;
}

// ✅ Cặp khoá test
public static class TestKeyPair
{
    public const string PublicKey =
        "-----BEGIN PUBLIC KEY-----\r\nMFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEjVFAIFlSHE+8Mwrkj9HO8JBOLOa/\r\nKs51ifUnskrdOUriPBFwefvwgKLvjriO9qTQ2Of2xxrfmMYfWjskhFfRRg==\r\n-----END PUBLIC KEY-----";

    public const string PrivateKey =
        "-----BEGIN PRIVATE KEY-----\r\nMIGHAgEAMBMGByqGSM49AgEGCCqGSM49AwEHBG0wawIBAQQgeHfCvUl2//eyxqGp\r\nCBpSzxNXl24mW1bOAN7VQktPMNahRANCAATfBvopoTBT3MUaqfr8GYftP5Z6RywQ\r\nyLZGcxYvO2ud8XcKTQ7wbmzzyObRmGrXm31btUDRC7eyv/pYWhmNLZzu\r\n-----END PRIVATE KEY-----";
}

