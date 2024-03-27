using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Project.Commands.CreatePrjDetail
{
	public class CreatePrjDetail : IRequest<string>
	{
		public string ProjectId { get; set; } = string.Empty;
		public string ProjectName { get; set; } = string.Empty;
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		//public DateTime RealEndDate { get; set; }
		public string Note { get; set; } = string.Empty;
		public string OderId { get; set; } = string.Empty;
		public List<DetailToCreate> Details { get; set; }

	}
	public class DetailToCreate
	{
		public string DetailId { get; set; } = string.Empty;
		public string DetailName { get; set; } = string.Empty;
		public string FileData { get; set; } = string.Empty;

	}
}
