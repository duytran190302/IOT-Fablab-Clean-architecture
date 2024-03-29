using IOT.Application.Contract.Persistence;
using IOT.Application.Exceptions;
using IOT.Application.Features.Project.Commands.DeletePrj;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Detail.Commands.DeleteDetail
{
	public class DeleteDetailHandler : IRequestHandler<DeleteDetail, string>
	{
		private readonly IUnitOfWork _unitOfWork;

		public DeleteDetailHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;

		}
		public async Task<string> Handle(DeleteDetail request, CancellationToken cancellationToken)
		{

			var detailToDelete = _unitOfWork.detailRepository.Find(x => x.DetailId == request.DetailId).FirstOrDefault();
			if (detailToDelete == null)
			{
				throw new NotFoundException(nameof(Detail), request.DetailId);
			}
			var detailPictureToDelete=  _unitOfWork.detailPictureRepository.Find(x => x.DetailId==request.DetailId).ToList();
			if (detailPictureToDelete != null)
			{
				_unitOfWork.detailPictureRepository.RemoveRange(detailPictureToDelete);
			}
			
			_unitOfWork.detailRepository.Remove(detailToDelete);
			await _unitOfWork.CompleteAsync();
			return detailToDelete.DetailId;
		}
	}
}
