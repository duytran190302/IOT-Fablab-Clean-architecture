using AutoMapper;
using FluentValidation;
using IOT.Application.Contract.Logging;
using IOT.Application.Contract.Persistence;
using IOT.Application.Exceptions;
using IOT.Application.Features.Oder.Commands.CreateOder;
using IOT.Application.Features.Oder.Queries.GetAllOder;
using IOT.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Project.Commands.CreatePrjDetail
{
	public class CreatePrjDetailHander : IRequestHandler<CreatePrjDetail, string>
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IAppLogger<CreatePrjDetailHander> _logger;
		private readonly IProjectRepository _projectRepository;
		private readonly IDetailPictureRepository _pictureRepository;
		private readonly IDetailRepository _detailRepository;

		public CreatePrjDetailHander(IMapper mapper, IUnitOfWork unitOfWork, IAppLogger<CreatePrjDetailHander> logger, 
			IDetailRepository detailRepository,
			IDetailPictureRepository detailPictureRepository,
			IProjectRepository projectRepository
			)
		{ 
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			_logger = logger;
			_pictureRepository = detailPictureRepository;
			_detailRepository = detailRepository;
			_projectRepository = projectRepository;

		}



        public async Task<string> Handle(CreatePrjDetail request, CancellationToken cancellationToken)
		{
			var validator = new CreatePrjDetailValidation();
			var validatorResult = await validator.ValidateAsync(request);
			if (validatorResult.Errors.Any())
			{
				throw new BadRequestException("Invalid Location", validatorResult);
			}

			var prjToCreate = new Domain.Project
			{
				ProjectId = request.ProjectId,
				ProjectName = request.ProjectName,
				StartDate = request.StartDate,
				EndDate = request.EndDate,
				Note = request.Note,
				OderId = request.OderId

			};

			 _projectRepository.PostProject(prjToCreate);
			// _unitOfWork.projectRepository.AddSyn(prjToCreate);


			foreach (var detail in request.Details)
			{
				var detailToCreate = new Domain.Detail
				{
					DetailId = detail.DetailId,
					DetailName = detail.DetailName,
					ProjectId = request.ProjectId,
					DetailStatus = 0,

				};


				  _detailRepository.PostDetail(detailToCreate);
				//_unitOfWork.Complete();

				_logger.LogWarning("post Detail {0} - {1}", nameof(Detail), detail.DetailId);
				if(!string.IsNullOrEmpty(detail.FileData))
				{
					var ab = new DetailPicture
					{
						DetailPictureId = Guid.NewGuid(),
						DetailId = detail.DetailId,
						FileData = Convert.FromBase64String(detail.FileData)
					};
				 	 _pictureRepository.PostDetailPicture(ab);
					_logger.LogWarning("post DetailPicture {0} - {1}", nameof(DetailPicture), detail.DetailId);

				}

			}
			await _unitOfWork.CompleteAsync();
			//return 
			return prjToCreate.ProjectId;
		}
	}
}
