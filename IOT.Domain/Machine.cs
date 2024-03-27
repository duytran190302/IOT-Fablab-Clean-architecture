namespace IOT.Domain
{
	public class Machine
	{
		public string MachineId { get; set; }
		public string MachineName { get; set; }
		public string Description { get; set; }


		public List<Detail> Details { get; set; }
		public List <OEE> OEEs { get; set; }
	}
}
