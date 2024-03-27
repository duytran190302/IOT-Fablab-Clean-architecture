namespace IOT.Api.MQTTModels
{
	public class OeeSend
	{
		public string machineId { get; set; }
		public DateTime timestamp { get; set; }
		public float oee { get; set; }
		public float st { get; set; }
		public float it { get; set; }
		public float ot { get; set; }
	}
}
