using IOT.Application.Features.Detail.Commands.DeleteDetail;
using IOT.Application.Features.Detail.Queries.GetAllDetail;
using IOT.Application.Features.Oder.Commands.DeleteOder;
using IOT.Application.Features.Project.Queries.GetAllPrj;
using IOT.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IOT.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DetailController : Controller
	{
		private readonly IMediator _mediator;
		public DetailController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpGet]
		public async Task<IActionResult> GetDetails([FromQuery]  string? prjId,string? detailId, string? workerId, string? machineId, DetailStatus? status, int pageSize = 10, int pageNumber = 1)
		{
			var details = await _mediator.Send(new GetAllDetails { DetailId= detailId , WorkerId= workerId, DetailStatusFromSearch= status, MachineId = machineId, ProjectId= prjId});
			details = details.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
			return Ok(details);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteDetail([FromQuery] string detailId)
		{
			var command = new DeleteDetail { DetailId = detailId };

			var IdReturn = await _mediator.Send(command);
			return Ok(IdReturn);

		}
	}
}
