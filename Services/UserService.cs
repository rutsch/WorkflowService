using Grpc.Core;
using WorkflowService.Managers;
using Google.Protobuf.WellKnownTypes;

namespace WorkflowService.Services
{
	public class UserService : Users.UsersBase
	{
		private readonly ILogger<UserService> _logger;
		private readonly UsersManager _usersManager;
		public UserService(ILogger<UserService> logger, UsersManager usersManager) => (_logger, _usersManager) = (logger, usersManager);

		public override Task<UserReply> GetUser(UserRequest request, ServerCallContext context)
		{
			return Task.FromResult(new UserReply
			{
				User = _usersManager.GetUser(request.Id)[0]
			});
		}
		public override Task<AllUsersReply> GetUsers(Empty request, ServerCallContext context)
		{
			_logger.LogInformation("Getting Users");
			AllUsersReply usersReply = new AllUsersReply();
			usersReply.Users.Add(_usersManager.GetUsers());
			return Task.FromResult(usersReply);
		}
	}

}

