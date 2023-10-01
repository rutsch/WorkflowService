using Grpc.Core;
using WorkflowService.Managers;
using Google.Protobuf.WellKnownTypes;

namespace WorkflowService.Services
{
	public class SectionService : Sections.SectionsBase
	{
		private readonly ILogger<SectionService> _logger;
		private readonly SectionsManager _sectionsManager;
		public SectionService(ILogger<SectionService> logger, SectionsManager sectionsManager) => (_logger, _sectionsManager) = (logger, sectionsManager);

		public override Task<SectionReply> GetSection(SectionRequest request, ServerCallContext context)
		{
			_logger.LogInformation("Getting Section");
			SectionReply sectionReply = new SectionReply();
			sectionReply.Section = _sectionsManager.GetSection(request.Id);

			return Task.FromResult(sectionReply);
		}
	}

}

