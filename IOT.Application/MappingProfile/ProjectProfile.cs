using AutoMapper;
using IOT.Application.Features.Project.Commands.CreatePrjDetail;
using IOT.Application.Features.Project.Queries.GetAllPrj;
using IOT.Domain;

namespace IOT.Application.MappingProfile
{
	public class ProjectProfile : Profile
	{
		public ProjectProfile()
		{
			CreateMap<Project, GetAllPrjDTO>().ReverseMap();
			CreateMap<CreatePrjDetail, Project>();
		}

	}

}
