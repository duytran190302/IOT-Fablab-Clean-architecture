using IOT.Application.Features.Machine.Queries.GetAllMachine;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Machine.Queries.GetMachineOEE
{
	public class GetMachineOee : IRequest<List<GetMachineOeeDTO>>
	{
		public string MachineId { get; set; }
		public DateTime Start {  get; set; }
		public DateTime End { get; set; }
	}	
}
