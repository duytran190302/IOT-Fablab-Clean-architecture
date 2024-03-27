using IOT.Application.Contract.Persistence;
using IOT.Domain;
using IOT.Persistence.DatabaseContext;

namespace IOT.Persistence.Repository
{
	public class OderRepository : Repository<Oder, string>, IOderRepository
	{
		public OderRepository(IOTDbContext context) : base(context)
		{

		}
	}

}
