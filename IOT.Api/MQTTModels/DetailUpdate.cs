namespace IOT.Api.MQTTModels
{
	public class DetailUpdate
	{
		public DateTime timestamp { get; set; }
		public string workerId { get; set; } = string.Empty;
		public string machineId { get; set; } = string.Empty;
	}
}
