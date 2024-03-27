using MediatR;

namespace IOT.Application.Features.Oder.Queries.GetAllOder
{
	public record GetAllOder : IRequest<List<OderDTO>>;
}
