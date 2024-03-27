using IOT.Application.Contract.Persistence;
using IOT.Domain;
using IOT.Persistence.DatabaseContext;

namespace IOT.Persistence.Repository
{
	public class WorkerRepository : Repository<Worker, string>, IWorkerRepository
	{
		public WorkerRepository(IOTDbContext context) : base(context)
		{

		}
	}

}
