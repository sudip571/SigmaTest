namespace CandidateHub.Events;


public static class CandidateHubLog
{
    #region Notification

    public record CandidateHubLogNotification(string logMessage) : INotification;

    #endregion Notification

    #region Handlers
  
    internal sealed class CandidateHubLogHandler : INotificationHandler<CandidateHubLogNotification>
    {
        private readonly ILogger logger;

        public CandidateHubLogHandler(ILogger logger)
        {
            this.logger = logger;          
        }

        public async Task Handle(CandidateHubLogNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                // you can write the log on database or perform other logics here.
                logger.Information($"{notification.logMessage}");
                await Task.CompletedTask;
                
            }
            catch (Exception ex)
            {
                logger.Error("Got an exception {error}", ex.Message);
                throw;
            }
        }
    }

    #endregion Handlers
}
