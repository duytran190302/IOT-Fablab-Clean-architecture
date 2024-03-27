using IOT.Application.Features.Project.Queries.GetAllPrj;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Worker.Queries.GetAllWorker
{
	public record GetAllWorker : IRequest<List<GetAllWorkerDTO>>;

}
