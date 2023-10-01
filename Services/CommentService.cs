using Grpc.Core;
using WorkflowService.Managers;
using Google.Protobuf.WellKnownTypes;

namespace WorkflowService.Services
{
	public class CommentService : Comments.CommentsBase
	{
		private readonly ILogger<CommentService> _logger;
		private readonly CommentsManager _commentsManager;
		public CommentService(ILogger<CommentService> logger, CommentsManager commentsManager) => (_logger, _commentsManager) = (logger, commentsManager);

		public override Task<CommentReply> GetComment(CommentRequest request, ServerCallContext context)
		{
			return Task.FromResult(new CommentReply
			{
				Comment = _commentsManager.getComment(request.Id)[0]
			});
		}

		public override Task<SaveCommentReply> SaveComment(SaveCommentRequest request, ServerCallContext context)
		{
			Comment comment = request.Comment;
			comment.Timestamp = DateTimeOffset.UtcNow.ToTimestamp();
			_commentsManager.AddComment(comment);
			return Task.FromResult(new SaveCommentReply
			{
				Result = "Succes"
			});
		}

		public override Task<UpdateCommentReply> UpdateComment(UpdateCommentRequest request, ServerCallContext context)
		{
			_commentsManager.UpdateComment(request.Comment);
			return Task.FromResult(new UpdateCommentReply
			{
				Result = "Succes"
			});
		}

		public override Task<DeleteCommentReply> DeleteComment(DeleteCommentRequest request, ServerCallContext context)
		{
			_commentsManager.DeleteComment(request.Id);
			return Task.FromResult(new DeleteCommentReply
			{
				Result = "Succes"
			});
		}

		public override Task<ResolveCommentsReply> ResolveComments(ResolveCommentsRequest request, ServerCallContext context)
		{
			_commentsManager.ResolveComments(request.ThreadId);
			return Task.FromResult(new ResolveCommentsReply
			{
				Result = "Succes"
			});
		}

		public override Task<CommentThreadReply> GetCommentThread(CommentThreadRequest request, ServerCallContext context)
		{
			var commentsReply = new CommentThreadReply();
			commentsReply.Comments.Add(_commentsManager.getCommentThread(request.ThreadId));
			return Task.FromResult(commentsReply);
		}

		public override Task<DeleteCommentThreadReply> DeleteCommentThread(DeleteCommentThreadRequest request, ServerCallContext context)
		{
			_commentsManager.DeleteCommentThread(request.ThreadId);
			return Task.FromResult(new DeleteCommentThreadReply
			{
				Result = "Succes"
			});
		}

		public override Task<AllCommentsReply> GetAllComments(AllCommentsRequest request, ServerCallContext context)
		{
			_logger.LogInformation(request.User);
			var commentsReply = new AllCommentsReply();
			commentsReply.Commentthreads.Add(_commentsManager.getComments(request.User));
			return Task.FromResult(commentsReply);
		}

		public override Task<AllSectionCommentsReply> GetAllCommentsForSection(AllSectionCommentsRequest request, ServerCallContext context)
		{
			var commentsReply = new AllSectionCommentsReply();
			commentsReply.Commentthreads.Add(_commentsManager.getCommentsForSection(request.Ids.Select(commentId => commentId.ToString()).ToList()));
			return Task.FromResult(commentsReply);
		}

		public override Task<AllUserCommentsReply> GetAllCommentsForUser(AllUserCommentsRequest request, ServerCallContext context)
		{
			var commentsReply = new AllUserCommentsReply();
			var sectionIdList = request.Sectionids.Select(sectionId => sectionId.ToString()).ToList();
			var seenIdList = request.Seenids.Select(sectionId => sectionId.ToString()).ToList();
			commentsReply.Commentthreads.Add(_commentsManager.getCommentsForUser(sectionIdList, seenIdList, request.Useremail));
			return Task.FromResult(commentsReply);
		}

		public override Task<UserCommentsCountReply> GetCommentsCountForUser(UserCommentsCountRequest request, ServerCallContext context)
		{
			var commentsReply = new UserCommentsCountReply();
			var sectionIdList = request.Sectionids.Select(sectionId => sectionId.ToString()).ToList();
			var seenIdList = request.Seenids.Select(sectionId => sectionId.ToString()).ToList();
			commentsReply.Commentcount = _commentsManager.getCommentsCountForUser(sectionIdList, seenIdList, request.Useremail);
			return Task.FromResult(commentsReply);
		}

		public override Task<UniqueCommentProjectsReply> GetUniqueCommentProjects(UniqueCommentProjectsRequest request, ServerCallContext context)
		{
			var commentsReply = new UniqueCommentProjectsReply();
			commentsReply.Sectionids.Add(_commentsManager.GetUniqueCommentProjects());
			return Task.FromResult(commentsReply);
		}

		public override Task CommentsStream(CommentsStreamRequest request, IServerStreamWriter<CommentsStreamReply> responseStream, ServerCallContext context)
		{
			using CommentListener listener = new CommentListener(_commentsManager, responseStream, request.User);		// could add some filtering options to listener
			context.CancellationToken.WaitHandle.WaitOne();
			return Task.CompletedTask;			
		}

	}
	public class CommentListener : IDisposable
	{
		private readonly CommentsManager _commentsManager;
		private readonly IServerStreamWriter<CommentsStreamReply> _responseStream;
		private readonly string _user;

		public CommentListener(CommentsManager commentsManager, IServerStreamWriter<CommentsStreamReply> responseStream, string user)
		{
			(_commentsManager, _responseStream, _user) = (commentsManager, responseStream, user);
			commentsManager.NewCommentHandler += NewCommentHandler;
		}

		public async void NewCommentHandler(Comment comment)
		{
			// can filter here
			if (_commentsManager.CommentForUserFilter(comment, _user)) 
			{
				var reply = new CommentsStreamReply()
				{
					Comment = comment
				};
				await _responseStream.WriteAsync(reply);
			}
		}

		public void Dispose() => _commentsManager.NewCommentHandler -= NewCommentHandler;
	}
}

