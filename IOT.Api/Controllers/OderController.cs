using IOT.Application.Features.Oder.Commands.CreateOder;
using IOT.Application.Features.Oder.Commands.DeleteOder;
using IOT.Application.Features.Oder.Queries.GetAllOder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IOT.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OderController : Controller
	{
		private readonly IMediator _mediator;

	public OderController(IMediator mediator)
	{
		_mediator = mediator;
	}
	[HttpGet]
	public async Task<IActionResult> GetOders([FromQuery] string? search)
	{
		var oder = await _mediator.Send(new GetAllOder());
			if (search != null)
			{
				oder = oder.Where(x=>x.OderId.Contains(search)).ToList();
			}
		return Ok(oder);
	}

	[HttpPost]
	public async Task<IActionResult> PostOder([FromBody] CreateOder oder)
	{
		CreateOder command = oder;
		var oderId = await _mediator.Send(command);
		return Ok(oderId);

	}
	[HttpDelete]
	public async Task<IActionResult> DeleteOder([FromQuery] string oderId)
		{
			var command = new DeleteOder { OderId = oderId };

			var IdReturn = await _mediator.Send(command);
		    return Ok(IdReturn);

	}
    }
}
