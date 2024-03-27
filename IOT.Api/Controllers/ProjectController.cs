using IOT.Application.Features.Oder.Commands.CreateOder;
using IOT.Application.Features.Oder.Commands.DeleteOder;
using IOT.Application.Features.Oder.Queries.GetAllOder;
using IOT.Application.Features.Project.Commands.CreatePrjDetail;
using IOT.Application.Features.Project.Commands.DeletePrj;
using IOT.Application.Features.Project.Queries.GetAllPrj;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IOT.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProjectController : Controller
	{ private readonly IMediator _mediator;

	public ProjectController(IMediator mediator)
	{
		_mediator = mediator;
	}
	[HttpGet]
	public async Task<IActionResult> GetPrjs([FromQuery] string? oderId, string? prjId)
	{
		var prjs = await _mediator.Send(new GetAllPrj());
		if (oderId != null)
		{
			prjs = prjs.Where(x => x.OderId == oderId).ToList();
		}

		if (prjId != null)
		{
			prjs = prjs.Where(x => x.ProjectId == prjId).ToList();
		}
			return Ok(prjs);
	}

	[HttpPost]
	public async Task<IActionResult> PostPrj([FromBody] CreatePrjDetail prj)
	{
		var oderId = await _mediator.Send(prj);
		return Ok(oderId);

	}
	[HttpDelete]
	public async Task<IActionResult> DeletePrj([FromQuery] string prjId)
	{
		var command = new DeletePrj { ProjectId = prjId };

		var IdReturn = await _mediator.Send(command);
		return Ok(IdReturn);

	}
}
}
