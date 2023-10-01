using LiteDB;

namespace WorkflowService.Managers
{
	public class SectionsManager
    {
		private LiteDatabase _db;
		private ILiteCollection<Section> _col;

		public SectionsManager(LiteDB.LiteDatabase db)
		{
			_db = db;
			_col = _db.GetCollection<Section>("sections");
		}
		
		public Section GetSection(Int64 sectionId) 
		{
			Section section = new Section();
			section.Id = sectionId;
			section.Creationdate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow);
			section.Lastmodified = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow);;
			section.Title = "Section " + sectionId;
			section.Html = "<h1>Section " + sectionId + "</h1><h3>Helping our customers address their healthcare challenges</h3><p>In the consumer domain, we develop innovative solutions that support healthier lifestyles, prevent disease, and help people to live well with chronic illness, also in the home and community settings.</p>";
			return section;
		}
    }
}