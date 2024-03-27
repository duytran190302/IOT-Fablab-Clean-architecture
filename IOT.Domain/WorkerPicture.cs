using System.ComponentModel.DataAnnotations.Schema;

namespace IOT.Domain
{
	public class WorkerPicture
	{

		[Column(TypeName = "VARBINARY(MAX)")]
		public byte[] FileData { get; set; }
		public Guid WorkerPictureId { get; set; }
		public string WorkerId { get; set; }
		public Worker Worker { get; set; }

	}
}
