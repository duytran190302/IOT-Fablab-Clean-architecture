using AutoMapper;
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
	public class OderProfile : Profile
	{
		public OderProfile()
		{
			CreateMap<Oder, OderDTO>().ReverseMap();
			CreateMap<CreateOder, Oder>();
		}

	}
}
