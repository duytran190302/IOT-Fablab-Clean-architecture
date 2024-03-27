namespace IOT.Domain
{
	public class OEE
	{
		public DateTime  TimeStamp { get; set;}
		public float IdleTime { get; set;}
		public float ShiftTime { get; set;}
		public float OperationTime { get; set;}
		public float Oee { get; set;}


		public string MachineId { get; set; }
		public Machine machine { get; set; }
	}
}
