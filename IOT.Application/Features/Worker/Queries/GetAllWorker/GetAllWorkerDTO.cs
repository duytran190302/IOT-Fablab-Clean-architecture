using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Worker.Queries.GetAllWorker
{
	public class GetAllWorkerDTO
	{
		public string WorkerId { get; set; } = string.Empty;
		public string WorkerName { get; set; } = string.Empty;
		public string Position { get; set; } = string.Empty;
		public byte[] FileData { get; set; }
	}
}
