using AutoMapper;
using IOT.Application.Contract.Persistence;
using IOT.Application.Exceptions;
using IOT.Application.Features.Oder.Commands.CreateOder;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Machine.Commands.CreateMachine
{
	public class CreateMachineHandler : IRequestHandler<CreateMachine, string>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		public CreateMachineHandler(IMapper mapper, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}
		public async Task<string> Handle(CreateMachine request, CancellationToken cancellationToken)
		{
			//validate
			var validator = new CreateMachineValidation();
			var validatorResult = await validator.ValidateAsync(request);
			if (validatorResult.Errors.Any())
			{
				throw new BadRequestException("Invalid Location", validatorResult);
			}

			var machineToCreate = _mapper.Map<Domain.Machine>(request);

			//add to db
			_unitOfWork.machineRepository.Add(machineToCreate);
			await _unitOfWork.CompleteAsync();
			//return 
			return machineToCreate.MachineId;

		}
	}
}
