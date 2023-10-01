using LiteDB;

namespace WorkflowService.Managers
{
	public class HierarchiesManager
    {
		private LiteDatabase _db;
		private ILiteCollection<Hierarchy> _col;

		public HierarchiesManager(LiteDB.LiteDatabase db)
		{
			_db = db;
			_col = _db.GetCollection<Hierarchy>("hierarchies");
		}
		
		public Hierarchy GetHierarchy(Int64 hierarchyId) 
		{
			Hierarchy hierarchy = new Hierarchy();
			hierarchy.Id = hierarchyId;
			hierarchy.Creationdate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow);
			hierarchy.Lastmodified = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow);
			hierarchy.Title = "Hierarchy " + hierarchyId;

			HierarchyItem hi1 = new HierarchyItem();
			hi1.Id = 1;
			hi1.Title = "Cover";

			HierarchyItem hi2 = new HierarchyItem();
			hi2.Id = 2;
			hi2.Title = "CEO message";

			HierarchyItem hi3 = new HierarchyItem();
			hi3.Id = 3;
			hi3.Title = "Back cover";			

			HierarchyItem hi4 = new HierarchyItem();
			hi4.Id = 4;
			hi4.Title = "Sample child 1";

			HierarchyItem hi5 = new HierarchyItem();
			hi5.Id = 5;
			hi5.Title = "Sample child 2";

			HierarchyItem hi6 = new HierarchyItem();
			hi6.Id = 6;
			hi6.Title = "Sample child 3";

			hi4.Children.Add(hi6);

			hi2.Children.Add(hi4);

			if(hierarchyId == 1)
			{
				hi2.Children.Add(hi5);
			} 

			hierarchy.Children.Add(hi1);
			hierarchy.Children.Add(hi2);
			hierarchy.Children.Add(hi3);
			return hierarchy;
		}
    }
}