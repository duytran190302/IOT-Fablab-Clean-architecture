using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Features.Oder.Commands.DeleteOder
{
	public class DeleteOder : IRequest<string>
	{
		public string OderId { get; set; } = string.Empty;
	}
}
