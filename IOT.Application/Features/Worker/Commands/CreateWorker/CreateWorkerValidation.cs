using FluentValidation;
using IOT.Application.Features.Oder.Commands.CreateOder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Worker.Commands.CreateWorker
{
	public class CreateWorkerValidation : AbstractValidator<CreateWorker>
	{
		public CreateWorkerValidation()
		{
			RuleFor(p => p.WorkerId)
				.NotEmpty().WithMessage("{property} is required")
				.NotNull()
				.MaximumLength(100);

			RuleFor(p => p.WorkerName)
			  .NotEmpty().WithMessage("{property} is required")
			  .NotNull()
			  .MaximumLength(100);

			RuleFor(p => p.Position)
				.NotEmpty().WithMessage("{property} is required")
			    .NotNull()
				.MaximumLength(500).WithMessage("max length = 200");
		}

	}
}
