using AutoMapper;
using IOT.Application.Contract.Persistence;
using IOT.Application.Exceptions;
using IOT.Application.Features.Oder.Commands.CreateOder;
using IOT.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Worker.Commands.CreateWorker
{
	public class CreateWorkerHandler : IRequestHandler<CreateWorker, string>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		public CreateWorkerHandler(IMapper mapper, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}
		public async Task<string> Handle(CreateWorker request, CancellationToken cancellationToken)
		{
			//validate
			var validator = new CreateWorkerValidation();
			var validatorResult = await validator.ValidateAsync(request);
			if (validatorResult.Errors.Any())
			{
				throw new BadRequestException("Invalid Location", validatorResult);
			}

			var workerToCreate = _mapper.Map<Domain.Worker>(request);
			
			//add to db
			_unitOfWork.workerRepository.Add(workerToCreate);
			if(request.FileData != null)
			{
				var picture = new WorkerPicture
				{
					FileData = Convert.FromBase64String(request.FileData),
					WorkerId = request.WorkerId,
					WorkerPictureId = Guid.NewGuid(),
				};
				_unitOfWork.workerPictureRepository.Add(picture);

			}

			await _unitOfWork.CompleteAsync();
			//return 
			return workerToCreate.WorkerId;

		}
	}
}
