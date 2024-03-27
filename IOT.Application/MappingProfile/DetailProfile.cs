using AutoMapper;
using IOT.Application.Features.Detail.Queries.GetAllDetail;
using IOT.Application.Features.Oder.Commands.CreateOder;
using IOT.Application.Features.Oder.Queries.GetAllOder;
using IOT.Application.Features.Project.Commands.CreatePrjDetail;
using IOT.Domain;

namespace IOT.Application.MappingProfile
{
	public class DetailProfile : Profile
	{
		public DetailProfile()
		{
			CreateMap<Detail, GetAllDetailsDTO>().ReverseMap();
			CreateMap<DetailToCreate, Detail>();
		}
	}
}
