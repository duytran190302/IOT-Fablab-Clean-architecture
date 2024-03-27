using IOT.Application.Contract.Persistence;
using IOT.Application.Exceptions;
using MediatR;

namespace IOT.Application.Features.Project.Commands.DeletePrj
{
	public class DeletePrjHandler : IRequestHandler<DeletePrj, string>
	{
		private readonly IUnitOfWork _unitOfWork;

		public DeletePrjHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<string> Handle(DeletePrj request, CancellationToken cancellationToken)
		{

			var prjToDelete = _unitOfWork.projectRepository.Find(x => x.ProjectId == request.ProjectId).FirstOrDefault();
			if (prjToDelete == null)
			{
				throw new NotFoundException(nameof(Oder), request.ProjectId);
			}

			_unitOfWork.projectRepository.Remove(prjToDelete);
			await _unitOfWork.CompleteAsync();
			return prjToDelete.OderId;
		}
	}
}
