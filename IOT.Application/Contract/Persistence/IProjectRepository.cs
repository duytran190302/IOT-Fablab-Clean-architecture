using IOT.Domain;

namespace IOT.Application.Contract.Persistence
{
	public interface IProjectRepository : IRepository<Project, string>
	{
		void PostProject(Project entity);

	}
}
