// Application/Contracts/INotificationService.cs
namespace galaxy_pay.application.Contracts;

public interface INotificationService
{
    Task NotifyAll(string message);
}
