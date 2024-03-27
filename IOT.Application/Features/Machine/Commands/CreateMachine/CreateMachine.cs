using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Machine.Commands.CreateMachine
{
	public class CreateMachine: IRequest<string>
	{
		public string MachineId { get; set; }
		public string MachineName { get; set; }
		public string Description { get; set; }

	}
}
