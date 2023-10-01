using Grpc.Core;
using WorkflowService.Managers;
using Google.Protobuf.WellKnownTypes;

namespace WorkflowService.Services
{
	public class HierarchyService : Hierarchies.HierarchiesBase
	{
		private readonly ILogger<HierarchyService> _logger;
		private readonly HierarchiesManager _hierarchiesManager;
		public HierarchyService(ILogger<HierarchyService> logger, HierarchiesManager hierarchiesManager) => (_logger, _hierarchiesManager) = (logger, hierarchiesManager);

		public override Task<HierarchyReply> GetHierarchy(HierarchyRequest request, ServerCallContext context)
		{
			_logger.LogInformation("Getting Hierarchy");
			HierarchyReply hierarchyReply = new HierarchyReply();
			hierarchyReply.Hierarchy = _hierarchiesManager.GetHierarchy(request.Id);

			return Task.FromResult(hierarchyReply);
		}
	}

}

