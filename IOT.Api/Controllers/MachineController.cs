using IOT.Application.Features.Machine.Commands.CreateMachine;
using IOT.Application.Features.Machine.Commands.DeleteMachine;
using IOT.Application.Features.Machine.Queries.GetAllMachine;
using IOT.Application.Features.Machine.Queries.GetMachineOEE;
using IOT.Application.Features.Oder.Commands.CreateOder;
using IOT.Application.Features.Oder.Commands.DeleteOder;
using IOT.Application.Features.Oder.Queries.GetAllOder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IOT.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MachineController : Controller
	{
		private readonly IMediator _mediator;

		public MachineController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpGet]
		public async Task<IActionResult> GetAllMachines([FromQuery] string? machineId)
		{
			var machines = await _mediator.Send(new GetAllMachine());
			if (machineId != null)
			{
				machines = machines.Where(x => x.MachineId== machineId).ToList();
			}
			return Ok(machines);
		}
		[HttpGet("OEE")]
		public async Task<IActionResult> GetMachineOEEs([FromQuery] string machineId, DateTime startDate, DateTime endDate)
		{
			var machines = await _mediator.Send(new GetMachineOee { MachineId = machineId, Start = startDate, End = endDate });

			return Ok(machines);
		}

		[HttpPost]
		public async Task<IActionResult> PostMachine([FromBody] CreateMachine machine)
		{
			var oderId = await _mediator.Send(machine);
			return Ok(oderId);

		}
		[HttpDelete]
		public async Task<IActionResult> DeleteOder([FromQuery] string machineId)
		{
			var command = new DeleteMachine { MachineId = machineId };

			var IdReturn = await _mediator.Send(command);
			return Ok(IdReturn);

		}
	}
}
