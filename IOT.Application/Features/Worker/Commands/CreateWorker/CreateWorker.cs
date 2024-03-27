using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Worker.Commands.CreateWorker
{
	public class CreateWorker : IRequest<string>
	{
		public string WorkerId { get; set; } = string.Empty;
		public string WorkerName { get; set; } = string.Empty;
		public string Position { get; set; } = string.Empty;
		public string FileData { get; set; } = string.Empty;
	}
}
