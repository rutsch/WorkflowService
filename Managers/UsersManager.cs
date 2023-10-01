using LiteDB;

namespace WorkflowService.Managers
{
	public class UsersManager
    {
		private LiteDatabase _db;
		private ILiteCollection<User> _col;

		public UsersManager(LiteDB.LiteDatabase db)
		{
			_db = db;
			_col = _db.GetCollection<User>("users");

		}

		public List<User> GetUser(Int64 id) 
		{
			
			// User user = new User();
			// user.Email = "rutger@taxxor.com";
			// user.Firstname = "Rutger";
			// user.Lastname = "Scheepens";
			
			// _col.Insert(user);
			var results = _col.Query()
				.Where(x => x.Id == id)
				.ToList();
			return results;
		}
		
		public List<User> GetUsers() 
		{
			var results = _col.Query()
				.ToList();
			return results;
		}
    }
}