using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Detail.Commands.DeleteDetail
{
	public class DeleteDetail : IRequest<string>
	{
		public string DetailId { get; set; } = string.Empty;
	}
}
