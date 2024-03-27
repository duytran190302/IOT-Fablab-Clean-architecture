using AutoMapper;
using IOT.Application.Contract.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Oder.Queries.GetAllOder
{
	public class GetAllOderHandler : IRequestHandler<GetAllOder, List<OderDTO>>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
        public GetAllOderHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
			_unitOfWork = unitOfWork;
        }

		public async Task<List<OderDTO>> Handle(GetAllOder request, CancellationToken cancellationToken)
		{
			//query
			var oders = await _unitOfWork.oderRepository.GetAllAsync();
			await _unitOfWork.CompleteAsync();
			//logging
			//_logger.LogInformation("get location successfully");


			// convert
			var data = _mapper.Map<List<OderDTO>>(oders);
			//return
			return data;
		}
	}
}
