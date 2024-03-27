using AutoMapper;
using IOT.Application.Features.Machine.Commands.CreateMachine;
using IOT.Application.Features.Machine.Queries.GetAllMachine;
using IOT.Application.Features.Machine.Queries.GetMachineOEE;
using IOT.Application.Features.Oder.Commands.CreateOder;
using IOT.Application.Features.Oder.Queries.GetAllOder;
using IOT.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.MappingProfile
{
	public class MachineProfile : Profile
	{
		public MachineProfile()
		{
			CreateMap<Machine, MachineDTO>().ReverseMap();
			CreateMap<OEE, GetMachineOeeDTO>().ReverseMap();
			CreateMap<CreateMachine, Machine>();
		}

	}
}
