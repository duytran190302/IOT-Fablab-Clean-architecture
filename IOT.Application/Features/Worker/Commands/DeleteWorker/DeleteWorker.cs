using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Worker.Commands.DeleteWorker
{
	public class DeleteWorker : IRequest<string>
	{
		public string WorkerId { get; set; } = string.Empty;
	}
}
