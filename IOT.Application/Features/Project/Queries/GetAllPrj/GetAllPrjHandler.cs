using AutoMapper;
using IOT.Application.Contract.Persistence;
using IOT.Application.Features.Oder.Queries.GetAllOder;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Project.Queries.GetAllPrj
{
	public class GetAllPrjHandler : IRequestHandler<GetAllPrj, List<GetAllPrjDTO>>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		public GetAllPrjHandler(IMapper mapper, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public async Task<List<GetAllPrjDTO>> Handle(GetAllPrj request, CancellationToken cancellationToken)
		{
			//query
			var prjs = await _unitOfWork.projectRepository.GetAllAsync();
			//await _unitOfWork.CompleteAsync();
			//logging
			//_logger.LogInformation("get location successfully");


			// convert
			var data = _mapper.Map<List<GetAllPrjDTO>>(prjs);
			//return
			return data;
		}
	}
}
