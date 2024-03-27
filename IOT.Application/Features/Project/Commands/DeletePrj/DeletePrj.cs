using IOT.Application.Features.Oder.Commands.DeleteOder;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Project.Commands.DeletePrj
{
	public class DeletePrj : IRequest<string>
	{
		public string ProjectId { get; set; } = string.Empty;
	}
}
