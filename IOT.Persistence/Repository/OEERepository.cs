using IOT.Application.Contract.Persistence;
using IOT.Domain;
using IOT.Persistence.DatabaseContext;

namespace IOT.Persistence.Repository
{
	public class OEERepository : Repository<OEE, (DateTime, string)>, IOEERepository
	{
		public OEERepository(IOTDbContext context) : base(context)
		{

		}
	}

}
