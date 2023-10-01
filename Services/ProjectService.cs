using Grpc.Core;
using WorkflowService.Managers;
using Google.Protobuf.WellKnownTypes;

namespace WorkflowService.Services
{
	public class ProjectService : Projects.ProjectsBase
	{
		private readonly ILogger<ProjectService> _logger;
		private readonly ProjectsManager _projectsManager;
		public ProjectService(ILogger<ProjectService> logger, ProjectsManager projectsManager) => (_logger, _projectsManager) = (logger, projectsManager);

		public override Task<AllProjectsReply> GetProjects(AllProjectsRequest request, ServerCallContext context)
		{
			_logger.LogInformation("Getting Projects");
			AllProjectsReply projectsReply = new AllProjectsReply();
			projectsReply.Projects.Add(_projectsManager.GetProjects());
			return Task.FromResult(projectsReply);
		}
	}

}

