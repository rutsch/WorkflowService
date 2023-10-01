using LiteDB;

namespace WorkflowService.Managers
{
	public class ProjectsManager
    {
		private LiteDatabase _db;
		private ILiteCollection<Project> _col;

		public ProjectsManager(LiteDB.LiteDatabase db)
		{
			_db = db;
			_col = _db.GetCollection<Project>("projects");
		}
		
		public List<Project> GetProjects() 
		{
			List<Project> results = new List<Project>();

			List<OutputChannel> outputChannels = new List<OutputChannel>();
			OutputChannel o1 = new OutputChannel();
			o1.Id = 1;
			o1.Name = "English PDF";
			
			OutputChannel o2 = new OutputChannel();
			o2.Id = 2;
			o2.Name = "Mandarin PDF";
			outputChannels.Add(o1);
			outputChannels.Add(o2);

			ReportingRequirement r1 = new ReportingRequirement();
			r1.Id = 1;
			r1.Name = "ESMA XBRL";

			ReportType rt1 = new ReportType();
			rt1.Id = 1;
			rt1.Name = "Annual report";

			Status s1 = new Status();
			s1.Id = 1;
			s1.Name = "open";

			Status s2 = new Status();
			s2.Id = 2;
			s2.Name = "closed";

			Project p1 = new Project();
			p1.Id = 1;
			p1.Name = "Annual Report 2021";
			p1.Creationdate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow);
			p1.Outputchannels.Add(outputChannels);
			p1.Publicationdate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow.AddDays(50));
			p1.Reportingrequirements.Add(r1);
			p1.Reporttype = rt1;
			p1.Status = s1;
			p1.Version = "3.1";

			Project p2 = new Project();
			p2.Id = 2;
			p2.Name = "Remuneration report 2021 with a very long title and even more text";
			p2.Creationdate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow);
			p2.Outputchannels.Add(outputChannels);
			p2.Publicationdate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow.AddDays(-50));
			p2.Reportingrequirements.Add(r1);
			p2.Reporttype = rt1;
			p2.Status = s2;
			p2.Version = "4.0";

			results.Add(p1);
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);			
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);
			results.Add(p2);


			return results;			
		}
    }
}