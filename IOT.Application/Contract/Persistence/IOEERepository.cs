using IOT.Domain;

namespace IOT.Application.Contract.Persistence
{
	public interface IOEERepository : IRepository<OEE, (DateTime, string)>
	{

	}
}
