using AutoMapper;
using IOT.Application.Contract.Persistence;
using IOT.Application.Exceptions;
using MediatR;

namespace IOT.Application.Features.Oder.Commands.DeleteOder
{
	public class DeleteOderHandler : IRequestHandler<DeleteOder, string>
	{
		private readonly IUnitOfWork _unitOfWork;

		public DeleteOderHandler( IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<string> Handle(DeleteOder request, CancellationToken cancellationToken)
		{


			var oderToDelete =  _unitOfWork.oderRepository.Find(x => x.OderId == request.OderId).FirstOrDefault();
			if (oderToDelete == null)
			{
				throw new NotFoundException(nameof(Oder), request.OderId);
			}

			 _unitOfWork.oderRepository.Remove(oderToDelete);
			await _unitOfWork.CompleteAsync();
			return oderToDelete.OderId;
		}
	}
}
