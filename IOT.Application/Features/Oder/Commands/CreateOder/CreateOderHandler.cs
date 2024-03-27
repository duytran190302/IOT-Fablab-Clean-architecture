using AutoMapper;
using IOT.Application.Contract.Persistence;
using IOT.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Oder.Commands.CreateOder
{
	public class CreateOderHandler : IRequestHandler<CreateOder, string>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		public CreateOderHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}
        public async Task<string> Handle(CreateOder request, CancellationToken cancellationToken)
		{
			//validate
			var validator = new CreateOderValidation();
			var validatorResult = await validator.ValidateAsync(request);
			if (validatorResult.Errors.Any())
			{
				throw new BadRequestException("Invalid Location", validatorResult);
			}

			var oderToCreate = _mapper.Map<Domain.Oder>(request);

			//add to db
			 _unitOfWork.oderRepository.Add(oderToCreate);
			 await _unitOfWork.CompleteAsync();
			//return 
			return oderToCreate.OderId;

		}
	}
}
