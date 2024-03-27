using IOT.Application.Contract.Persistence;
using IOT.Application.Exceptions;
using IOT.Application.Features.Oder.Commands.DeleteOder;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Machine.Commands.DeleteMachine
{
	public class DeleteMachineHandler : IRequestHandler<DeleteMachine, string>
	{
		private readonly IUnitOfWork _unitOfWork;

		public DeleteMachineHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<string> Handle(DeleteMachine request, CancellationToken cancellationToken)
		{


			var machineToDelete = _unitOfWork.machineRepository.Find(x => x.MachineId == request.MachineId).FirstOrDefault();
			if (machineToDelete == null)
			{
				throw new NotFoundException(nameof(Machine), request.MachineId);
			}

			var oeeToDelete = _unitOfWork.oeeRepository.Find(x => x.MachineId == request.MachineId).ToList();
			_unitOfWork.oeeRepository.RemoveRange(oeeToDelete);


			_unitOfWork.machineRepository.Remove(machineToDelete);

			await _unitOfWork.CompleteAsync();
			return machineToDelete.MachineId;
		}
	}
}
