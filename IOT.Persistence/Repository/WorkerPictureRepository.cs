using IOT.Application.Contract.Persistence;
using IOT.Domain;
using IOT.Persistence.DatabaseContext;

namespace IOT.Persistence.Repository
{
	public class WorkerPictureRepository : Repository<WorkerPicture, (Guid, string)>, IWorkerPictureRepository
	{
		//private readonly IMapper _mapper;
		public WorkerPictureRepository(IOTDbContext context) : base(context)
		{
			//_mapper = mapper;
		}
	}

}
