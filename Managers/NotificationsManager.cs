using LiteDB;

namespace WorkflowService.Managers
{
	public class NotificationsManager
    {
		private LiteDatabase _db;
		private ILiteCollection<Notification> _col;

		public NotificationsManager(LiteDB.LiteDatabase db)
		{
			_db = db;
			_col = _db.GetCollection<Notification>("notifications");

			// Notification notification = new Notification();
			// notification.Author = "rutger@taxxor.com";
			// notification.Type = Notification.Types.NotificationType.Info;
			// notification.Timestamp = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.Now.ToUniversalTime());
			// notification.Showuntil = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.Now.AddDays(5).ToUniversalTime());
			// notification.Text = "A new version of TaxxorDM will be released on 11-11-2021. Please make sure to refresh your browser window to see the latest changes.";
			// notification.Projectid = "all";

			// _col.Insert(notification);

			// Notification notification2 = new Notification();
			// notification2.Author = "magdalena.sosnowska@philips.com";
			// notification2.Type = Notification.Types.NotificationType.Warn;
			// notification2.Timestamp = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.Now.ToUniversalTime());
			// notification2.Showuntil = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.Now.AddDays(1).ToUniversalTime());
			// notification2.Text = "We will close draft 4 at 17:00hrs today. Please make sure to apply your changes before that time!";
			// notification2.Projectid = "ar21";

			// _col.Insert(notification2);
			// _col.DeleteAll();
		}
		
		public List<Notification> GetNotifications() 
		{
			Google.Protobuf.WellKnownTypes.Timestamp timestampnow = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow);
			// var results = _col.Query()
			// 	.Where(x => x.Showuntil >= timestampnow)
			// 	.OrderByDescending(x => x.Timestamp)
			// 	.ToList();
			List<Notification> result = new List<Notification>();
			
			Notification notification = new Notification();
			notification.Author = "rutger@taxxor.com";
			notification.Type = Notification.Types.NotificationType.Info;
			notification.Timestamp = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.Now.ToUniversalTime());
			notification.Showuntil = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.Now.AddDays(5).ToUniversalTime());
			notification.Text = "A new version of TaxxorDM will be released on 11-11-2021. Please make sure to refresh your browser window to see the latest changes.";
			notification.Projectid = "all";

			Notification notification2 = new Notification();
			notification2.Author = "magdalena.sosnowska@philips.com";
			notification2.Type = Notification.Types.NotificationType.Warn;
			notification2.Timestamp = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.Now.ToUniversalTime());
			notification2.Showuntil = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.Now.AddDays(1).ToUniversalTime());
			notification2.Text = "We will close draft 4 at 17:00hrs today. Please make sure to apply your changes before that time!";
			notification2.Projectid = "ar21";

			result.Add(notification);
			result.Add(notification2);

			return result;
		}
    }
}