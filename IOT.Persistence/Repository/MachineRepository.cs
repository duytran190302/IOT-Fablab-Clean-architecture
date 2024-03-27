using IOT.Application.Contract.Persistence;
using IOT.Domain;
using IOT.Persistence.DatabaseContext;

namespace IOT.Persistence.Repository
{
	public class MachineRepository : Repository<Machine, string>, IMachineRepository
	{
		public MachineRepository(IOTDbContext context) : base(context)
		{

		}
	}

}
