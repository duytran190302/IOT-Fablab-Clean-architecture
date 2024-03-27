using FluentValidation;
using IOT.Application.Features.Oder.Commands.CreateOder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Machine.Commands.CreateMachine
{
	public class CreateMachineValidation : AbstractValidator<CreateMachine>
	{
		public CreateMachineValidation()
		{
			RuleFor(p => p.MachineId)
				.NotEmpty().WithMessage("{property} is required")
				.NotNull()
				.MaximumLength(100);

			RuleFor(p => p.MachineName)
			  .NotEmpty().WithMessage("{property} is required")
			  .NotNull()
			  .MaximumLength(100);
		}

	}
}
