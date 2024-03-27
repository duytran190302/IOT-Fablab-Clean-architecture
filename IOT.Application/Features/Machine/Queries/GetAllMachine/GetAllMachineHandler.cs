using AutoMapper;
using IOT.Application.Contract.Persistence;
using IOT.Application.Features.Oder.Queries.GetAllOder;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Machine.Queries.GetAllMachine
{
	public class GetAllMachineHandler : IRequestHandler<GetAllMachine, List<MachineDTO>>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		public GetAllMachineHandler(IMapper mapper, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public async Task<List<MachineDTO>> Handle(GetAllMachine request, CancellationToken cancellationToken)
		{
			var machines = await _unitOfWork.machineRepository.GetAllAsync();
			await _unitOfWork.CompleteAsync();
			//logging
			//_logger.LogInformation("get location successfully");


			// convert
			var data = _mapper.Map<List<MachineDTO>>(machines);
			//return
			return data;
		}
	}
}
