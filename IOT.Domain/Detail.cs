namespace IOT.Domain
{
	public class Detail
	{
		public string DetailId { get; set; } = string.Empty;
		public string DetailName { get; set; } = string.Empty;
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }




		public DetailStatus DetailStatus { get; set; }
		public List<DetailPicture> DetailPictures { get; set; }

		//public string WorkerId { get; set; } = string.Empty;
		public Worker Worker { get; set; }

		public Project Project { get; set; }
		public string ProjectId { get; set; } = string.Empty;

		//public string MachineId { get; set; } = string.Empty;
		public Machine Machine { get; set; }

	}
}
