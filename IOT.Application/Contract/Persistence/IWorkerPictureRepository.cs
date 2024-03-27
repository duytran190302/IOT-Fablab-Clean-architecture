using IOT.Domain;

namespace IOT.Application.Contract.Persistence
{
	public interface IWorkerPictureRepository : IRepository<WorkerPicture, (Guid, string)>
	{

	}
}
