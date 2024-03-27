using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Project.Queries.GetAllPrj
{
	public class GetAllPrjDTO
	{
		public string ProjectId { get; set; } = string.Empty;
		public string ProjectName { get; set; } = string.Empty;
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public DateTime RealEndDate { get; set; }
		public string Note { get; set; } = string.Empty;



		public string OderId { get; set; } = string.Empty;
	}
}
