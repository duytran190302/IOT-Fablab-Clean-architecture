using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Machine.Queries.GetMachineOEE
{
	public class GetMachineOeeDTO
	{
		public DateTime TimeStamp { get; set; }
		public float IdleTime { get; set; }
		public float ShiftTime { get; set; }
		public float OperationTime { get; set; }
		public float Oee { get; set; }


		public string MachineId { get; set; }

	}
}
