using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Machine.Commands.DeleteMachine
{
	public class DeleteMachine : IRequest<string>
	{
		public string MachineId { get; set; } = string.Empty;
	}
}
