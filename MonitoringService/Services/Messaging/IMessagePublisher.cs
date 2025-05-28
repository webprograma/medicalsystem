namespace MonitoringService.Services.Messaging
{
    public interface IMessagePublisher
    {
        void PublishAlert(string message);
    }
}
