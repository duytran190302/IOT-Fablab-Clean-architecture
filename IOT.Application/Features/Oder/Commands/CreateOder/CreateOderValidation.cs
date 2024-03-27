using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Oder.Commands.CreateOder;
public class CreateOderValidation : AbstractValidator<CreateOder>
{
	public CreateOderValidation()
	{
		RuleFor(p => p.OderId)
			.NotEmpty().WithMessage("{property} is required")
			.NotNull()
			.MaximumLength(100);

		RuleFor(p => p.Custummer)
	      .NotEmpty().WithMessage("{property} is required")
	      .NotNull()
	      .MaximumLength(100);

		RuleFor(p => p.Note)
			.MaximumLength(500).WithMessage("max length = 200");
	}

}