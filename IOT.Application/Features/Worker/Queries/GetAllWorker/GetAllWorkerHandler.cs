using AutoMapper;
using IOT.Application.Contract.Persistence;
using IOT.Application.Features.Project.Queries.GetAllPrj;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Worker.Queries.GetAllWorker
{
	public class GetAllWorkerHandler : IRequestHandler<GetAllWorker, List<GetAllWorkerDTO>>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		public GetAllWorkerHandler(IMapper mapper, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public async Task<List<GetAllWorkerDTO>> Handle(GetAllWorker request, CancellationToken cancellationToken)
		{
			//query
			var workers = await _unitOfWork.workerRepository.GetAllAsync();

			//logging
			//_logger.LogInformation("get location successfully");


			// convert
			var data = _mapper.Map<List<GetAllWorkerDTO>>(workers);
			foreach (var item in data)
			{
				var dataPicture = _unitOfWork.workerPictureRepository.Find(x => x.WorkerId == item.WorkerId).First();
				item.FileData = dataPicture.FileData;
			}
			await _unitOfWork.CompleteAsync();
			//return
			return data;
		}
	}
}
