using Grpc.Core;
using WorkflowService.Managers;
using Google.Protobuf.WellKnownTypes;

namespace WorkflowService.Services
{
	public class ActivityService : Activities.ActivitiesBase
	{
		private readonly ILogger<ActivityService> _logger;
		private readonly ActivitiesManager _activitiesManager;
		public ActivityService(ILogger<ActivityService> logger, ActivitiesManager activitiesManager) => (_logger, _activitiesManager) = (logger, activitiesManager);

		public override Task<AllActivitiesReply> GetAllActivities(Empty request, ServerCallContext context)
		{
			_logger.LogInformation("Getting Users");
			AllActivitiesReply activitiesReply = new AllActivitiesReply();
			activitiesReply.Activities.Add(_activitiesManager.GetActivities());
			return Task.FromResult(activitiesReply);
		}
	}

}

