// Presentation/Controllers/NotificationController.cs
using galaxy_pay.application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace galaxy_pay.api.Controllers;

[ApiController]
[Route("api/signal")]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpPost("broadcast")]
    public async Task<IActionResult> Broadcast([FromBody] string message)
    {
        await _notificationService.NotifyAll(message);
        return Ok(new { status = "sent", message });
    }
}
