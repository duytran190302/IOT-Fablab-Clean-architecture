using AutoMapper;
using IOT.Application.Contract.Persistence;
using IOT.Application.Features.Machine.Queries.GetAllMachine;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Machine.Queries.GetMachineOEE
{
	public class GetMachineOeeHandler : IRequestHandler<GetMachineOee, List<GetMachineOeeDTO>>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		public GetMachineOeeHandler(IMapper mapper, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public async Task<List<GetMachineOeeDTO>> Handle(GetMachineOee request, CancellationToken cancellationToken)
		{
			var machineOee =  _unitOfWork.oeeRepository
				.Find(x=> x.MachineId==request.MachineId)
				.Where(x=>x.TimeStamp >= request.Start)
				.Where(x=> x.TimeStamp <= request.End)
				.OrderByDescending(x=>x.TimeStamp);

			await _unitOfWork.CompleteAsync();
			//logging
			//_logger.LogInformation("get location successfully");


			// convert
			var data = _mapper.Map<List<GetMachineOeeDTO>>(machineOee);
			//return
			return data;
		}
	}
}
