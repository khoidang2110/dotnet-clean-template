// Infrastructure/Services/NotificationService.cs
using galaxy_pay.application.Contracts;
using galaxy_pay.infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace galaxy_pay.infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationService(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task NotifyAll(string message)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
    }
}
