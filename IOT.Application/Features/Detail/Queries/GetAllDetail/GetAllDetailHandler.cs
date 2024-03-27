using AutoMapper;
using IOT.Application.Contract.Persistence;
using IOT.Application.Features.Project.Queries.GetAllPrj;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Detail.Queries.GetAllDetail
{
	public class GetAllDetailHandler : IRequestHandler<GetAllDetails, List<GetAllDetailsDTO>>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		public GetAllDetailHandler(IMapper mapper, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public async Task<List<GetAllDetailsDTO>> Handle(GetAllDetails request, CancellationToken cancellationToken)
		{
			//query
			var details = await _unitOfWork.detailRepository.GetAllDetailAsync();
			if (request.ProjectId != null)
			{
				details= details.Where(x=>x.ProjectId == request.ProjectId);
			}
			if (request.DetailStatusFromSearch != null)
			{
				details= details.Where(x=>x.DetailStatus == request.DetailStatusFromSearch);
			}
			if (request.WorkerId != null)
			{
				details= details.Where(x=>x.Worker.WorkerId==request.WorkerId);
			}
			if (request.MachineId != null)
			{
				details = details.Where(x => x.Machine.MachineId == request.MachineId);
			}
			if (request.DetailId != null)
			{
				details = details.Where(x => x.DetailId == request.DetailId);
			}

			// convert
			var data = _mapper.Map<List<GetAllDetailsDTO>>(details);
			foreach (var item in data)
			{


			var dataPicture = _unitOfWork.detailPictureRepository.Find(x => x.DetailId == item.DetailId).FirstOrDefault();
				if(dataPicture != null)
				{
					item.FileData = dataPicture.FileData;
				}

			//	item.WorkerId = workerForDetail.WorkerId;
			}
			
			await _unitOfWork.CompleteAsync();
			//return
			return data;
		}
	}
}
