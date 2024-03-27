using IOT.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Detail.Queries.GetAllDetail
{
	public class GetAllDetailsDTO
	{
		public string DetailId { get; set; } = string.Empty;
		public string DetailName { get; set; } = string.Empty;
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public DetailStatus DetailStatus { get; set; }

		public string WorkerId { get; set; } = string.Empty;
		public string ProjectId { get; set; } = string.Empty;
		public string MachineId { get; set; } = string.Empty;
		public byte[] FileData { get; set; }

	}
}
