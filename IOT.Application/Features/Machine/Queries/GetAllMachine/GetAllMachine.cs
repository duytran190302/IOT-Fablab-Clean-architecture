using IOT.Application.Features.Oder.Queries.GetAllOder;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Machine.Queries.GetAllMachine
{
	public record GetAllMachine : IRequest<List<MachineDTO>>;
}
