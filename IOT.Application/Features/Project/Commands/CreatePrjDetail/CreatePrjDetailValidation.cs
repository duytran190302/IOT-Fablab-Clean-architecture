using FluentValidation;
using IOT.Application.Features.Oder.Commands.CreateOder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Project.Commands.CreatePrjDetail;
public class CreatePrjDetailValidation : AbstractValidator<CreatePrjDetail>
{
	public CreatePrjDetailValidation()
	{
		RuleFor(p => p.ProjectId)
			.NotEmpty().WithMessage("{property} is required")
			.NotNull()
			.MaximumLength(100);

		RuleFor(p => p.ProjectName)
		  .NotEmpty().WithMessage("{property} is required")
		  .NotNull()
		  .MaximumLength(100);

		RuleFor(p => p.OderId)
			.NotEmpty()
			.WithMessage("{property} is required")
			.MaximumLength(500).WithMessage("max length = 200");
	}

}
