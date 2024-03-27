namespace IOT.Domain
{
	public class Project
	{
		public string ProjectId { get; set; } = string.Empty;
		public string ProjectName { get; set; } = string.Empty;
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public DateTime RealEndDate { get; set; }
		public string Note { get; set; } = string.Empty;



		public string OderId { get; set; } = string.Empty;
		public Oder Oder { get; set; }
		public List<Detail> Details { get; set; }

	}
}
