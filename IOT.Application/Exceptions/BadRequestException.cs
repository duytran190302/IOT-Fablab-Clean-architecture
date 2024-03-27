using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations; note: trung annotations
using FluentValidation.Results;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Exceptions
{
	public class BadRequestException : Exception
	{
		public BadRequestException(string message) : base(message)
		{

		}
		public BadRequestException(string message, ValidationResult validationResult) : base(message)
		{
			ValidationErrors = validationResult.ToDictionary();
		}

		public IDictionary<string, string[]> ValidationErrors { get; set; }
	}
}
