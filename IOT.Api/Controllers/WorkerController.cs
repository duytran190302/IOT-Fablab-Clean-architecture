using IOT.Application.Features.Oder.Commands.CreateOder;
using IOT.Application.Features.Oder.Commands.DeleteOder;
using IOT.Application.Features.Oder.Queries.GetAllOder;
using IOT.Application.Features.Worker.Commands.CreateWorker;
using IOT.Application.Features.Worker.Commands.DeleteWorker;
using IOT.Application.Features.Worker.Queries.GetAllWorker;
using IOT.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IOT.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WorkerController : Controller
	{
		private readonly IMediator _mediator;

		public WorkerController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpGet]
		public async Task<IActionResult> GetWorkers([FromQuery] string? workerId, int pageSize = 10, int pageNumber = 1)
		{
			var workers = await _mediator.Send(new GetAllWorker());
			if (workerId != null)
			{
				workers = workers.Where(x => x.WorkerId== workerId).ToList();
			}
			workers = workers.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
			return Ok(workers);
		}

		[HttpPost]
		public async Task<IActionResult> PostWorker([FromBody] CreateWorker worker)
		{
			var oderId = await _mediator.Send(worker);
			return Ok(oderId);

		}
		[HttpDelete]
		public async Task<IActionResult> DeleteWorker([FromQuery] string workerId)
		{
			var command = new DeleteWorker { WorkerId = workerId };

			var IdReturn = await _mediator.Send(command);
			return Ok(IdReturn);

		}
	}
}
