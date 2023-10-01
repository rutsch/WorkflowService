using LiteDB;

namespace WorkflowService.Managers
{
	public class CommentsManager
    {
		public delegate void NewCommentDelegate(Comment comment);
		public event NewCommentDelegate? NewCommentHandler;
		private LiteDatabase _db;
		private ILiteCollection<Comment> _col;

		public bool CommentForUserFilter(Comment comment, string user) 
		{
			return comment.Text.Contains(user);
		}

		public CommentsManager(LiteDB.LiteDatabase db)
		{
			_db = db;
			_col = _db.GetCollection<Comment>("comments");
		}

		public void AddComment(Comment comment) 
		{
			_col.Insert(comment);
			_col.EnsureIndex(x => x.Text);
			NewCommentHandler?.Invoke(comment);
		}
		public List<Comment> getComment(Int64 id)
		{
			var results = _col.Query()
				.Where(x => x.Id == id)
				.ToList();
			return results;
		}
		public List<CommentThread> getComments(string user)
		{
			var results = _col.FindAll()
				.Where(x => x.Text.Contains(user))
				.GroupBy(x => x.ThreadId)
				.Select(x => {
					var newThread = new CommentThread() {
						ThreadId = x.Key
					};
					newThread.Comments.AddRange(x.OrderByDescending(y => y.Timestamp.Seconds));
					return newThread;
				})
				.ToList();
			return results;
		}
		
		public List<CommentThread> getCommentsForUser(List<string> idList, List<string> seenIdList, string useremail)
		{
			var results = _col.FindAll()
				.Where(x => 
					idList.Contains(x.Sectionid) && 
					x.Resolved == false && 
					seenIdList.Contains(x.Id.ToString()) == false &&
					x.Author != useremail
				)
				.GroupBy(x => x.ThreadId)
				.Select(x => {
					var newThread = new CommentThread() {
						ThreadId = x.Key
					};
					newThread.Comments.AddRange(x.OrderBy(y => y.Timestamp.Seconds));
					return newThread;
				})
				.ToList();
			return results;
		}
		
		public int getCommentsCountForUser(List<string> idList, List<string> seenIdList, string useremail)
		{
			var results = _col.FindAll()
				.Where(x => 
					idList.Contains(x.Sectionid) && 
					x.Resolved == false && 
					seenIdList.Contains(x.Id.ToString()) == false &&
					x.Author != useremail
				)
				.GroupBy(x => x.ThreadId)
				.Count();
			return results;
		}

		public List<CommentThread> getCommentsForSection(List<string> idList)
		{
			var results = _col.FindAll()
				.Where(x => idList.Contains(x.ThreadId))
				.GroupBy(x => x.ThreadId)
				.Select(x => {
					var newThread = new CommentThread() {
						ThreadId = x.Key
					};
					newThread.Comments.AddRange(x.OrderBy(y => y.Timestamp.Seconds));
					return newThread;
				})
				.ToList();
			return results;
		}

		public List<string> GetUniqueCommentProjects()
		{
			var results = _col.FindAll()
				.Select(x => x.Projectid)
				.Distinct()
				.ToList();
			return results;
		}		

		public List<Comment> getCommentThread(string threadId)
		{
			var results = _col.Query()
				.Where(x => x.ThreadId == threadId)
				.OrderByDescending(x => x.Timestamp.Seconds)
				.ToList();
			return results;
		}
		public void UpdateComment(Comment comment)
		{
			_col.Update(comment);
		}

		public void DeleteComment(Int64 id)
		{
			_col.DeleteMany(x => x.Id == id);
		}
		public void ResolveComments(string threadId)
		{
			var results = _col
				.Find(Query.EQ("ThreadId", threadId))
				.ToList();

			foreach (var result in results)
			{
				result.Resolved = true;
				_col.Update(result);
			}
		}

		public void DeleteCommentThread(string threadId)
		{
			_col.DeleteMany(x => x.ThreadId == threadId);
		}
    }
}