namespace IOT.Api.MQTTModels
{
	public class DetailUpdate
	{
		public string name { get; set; }
		public string value { get; set; }
		public DateTime timestamp { get; set; }
		public string operatorid { get; set; } = string.Empty;
	}
}
