namespace IOT.Domain
{
	public class Worker
	{
		public string WorkerId { get; set; } = string.Empty;
		public string WorkerName { get; set; } = string.Empty;
		public string Position {  get; set; }= string.Empty;

		public List<WorkerPicture> WorkerPictures { get; set; }
		public List<Detail> Details { get; set; }
		
	}
}
