using LiteDB;

namespace WorkflowService.Managers
{
	public class ActivitiesManager
    {
		private LiteDatabase _db;
		private ILiteCollection<Activity> _col;

		public ActivitiesManager(LiteDB.LiteDatabase db)
		{
			_db = db;
			_col = _db.GetCollection<Activity>("activities");
		}
		
		public List<Activity> GetActivities() 
		{
			Google.Protobuf.WellKnownTypes.Timestamp timestampnow = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow);

			List<Activity> results = new List<Activity>();

			Activity activity = new Activity();
			activity.Projectid = "ar21";
			activity.Details = "Message from the CEO";
			activity.User = "rutger@taxxor.com";
			activity.Type = Activity.Types.ActivityType.Sectionopened;
			activity.Timestamp = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow);

			results.Add(activity);

			Activity activity2 = new Activity();
			activity2.Projectid = "ar21";
			activity2.Details = "Message from the CEO";
			activity2.User = "rutger@taxxor.com";
			activity2.Type = Activity.Types.ActivityType.Sectionsaved;
			activity2.Timestamp = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow);

			results.Add(activity2);

			Activity activity3 = new Activity();
			activity3.Projectid = "ar21";
			activity3.Details = "Johan has just entered TaxxorDM";
			activity3.User = "johan@taxxor.com";
			activity3.Type = Activity.Types.ActivityType.Loggedin;
			activity3.Timestamp = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow);

			results.Add(activity3);

			Activity activity4 = new Activity();
			activity4.Projectid = "q122";
			activity4.Details = "Version 3.2.1";
			activity4.User = "rutger@taxxor.com";
			activity4.Type = Activity.Types.ActivityType.Versioncreated;
			activity4.Timestamp = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow);

			results.Add(activity4);

			Activity activity5 = new Activity();
			activity5.Projectid = "ar21";
			activity5.Details = "iXBRL package for ESMA was created";
			activity5.User = "bas@taxxor.com";
			activity5.Type = Activity.Types.ActivityType.Xbrlgenerated;
			activity5.Timestamp = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow);

			results.Add(activity5);

			return results;
		}
    }
}