using AutoMapper;
using IOT.Application.Features.Detail.Queries.GetAllDetail;
using IOT.Application.Features.Project.Commands.CreatePrjDetail;
using IOT.Application.Features.Worker.Commands.CreateWorker;
using IOT.Application.Features.Worker.Queries.GetAllWorker;
using IOT.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.MappingProfile
{
	public class WorkerProfile : Profile
	{
		public WorkerProfile()
		{
			CreateMap<Worker, GetAllWorkerDTO>().ReverseMap();
			CreateMap<CreateWorker, Worker>();
		}
	}
}
