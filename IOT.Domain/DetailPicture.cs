using System.ComponentModel.DataAnnotations.Schema;

namespace IOT.Domain
{
	public class DetailPicture
	{

		[Column(TypeName = "VARBINARY(MAX)")]
		public byte[] FileData { get; set; }
		public Guid DetailPictureId { get; set; }

		public string DetailId { get; set; }
		public Detail Detail { get; set; }

	}
}
