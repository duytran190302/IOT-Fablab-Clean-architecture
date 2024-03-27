using IOT.Application.Contract.Persistence;
using IOT.Application.Exceptions;
using IOT.Application.Features.Detail.Commands.DeleteDetail;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Worker.Commands.DeleteWorker
{
	public class DeleteWorkerHandler : IRequestHandler<DeleteWorker, string>
	{
		private readonly IUnitOfWork _unitOfWork;

		public DeleteWorkerHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<string> Handle(DeleteWorker request, CancellationToken cancellationToken)
		{

			var workerToDelete = _unitOfWork.workerRepository.Find(x => x.WorkerId == request.WorkerId).FirstOrDefault();
			if (workerToDelete == null)
			{
				throw new NotFoundException(nameof(Worker), request.WorkerId);
			}
			var workerPictureToDelete = _unitOfWork.workerPictureRepository.Find(x => x.WorkerId == request.WorkerId).ToList();
			if (workerPictureToDelete != null)
			{
				_unitOfWork.workerPictureRepository.RemoveRange(workerPictureToDelete);
			}

			_unitOfWork.workerRepository.Remove(workerToDelete);
			await _unitOfWork.CompleteAsync();
			return workerToDelete.WorkerId;
		}
	}
}
