using IOT.Application.Features.Project.Queries.GetAllPrj;
using IOT.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Detail.Queries.GetAllDetail
{
	public class GetAllDetails : IRequest<List<GetAllDetailsDTO>>
	{
		public string? DetailId { get; set; }	
		public DetailStatus? DetailStatusFromSearch { get; set; }
		public string? WorkerId { get; set; }
		public string? MachineId { get; set; }
		public string? ProjectId { get; set; }

	}
	
}
