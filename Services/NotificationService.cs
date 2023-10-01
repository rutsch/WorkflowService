using Grpc.Core;
using WorkflowService.Managers;
using Google.Protobuf.WellKnownTypes;

namespace WorkflowService.Services
{
	public class NotificationService : Notifications.NotificationsBase
	{
		private readonly ILogger<NotificationService> _logger;
		private readonly NotificationsManager _notificationsManager;
		public NotificationService(ILogger<NotificationService> logger, NotificationsManager notificationsManager) => (_logger, _notificationsManager) = (logger, notificationsManager);

		public override Task<AllNotificationsReply> GetAllNotifications(Empty request, ServerCallContext context)
		{
			_logger.LogInformation("Getting Users");
			AllNotificationsReply notificationsReply = new AllNotificationsReply();
			notificationsReply.Notifications.Add(_notificationsManager.GetNotifications());
			return Task.FromResult(notificationsReply);
		}
	}

}

