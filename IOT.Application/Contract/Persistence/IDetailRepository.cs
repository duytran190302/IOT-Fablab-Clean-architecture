using IOT.Domain;

namespace IOT.Application.Contract.Persistence
{
	public interface IDetailRepository : IRepository<Detail, string>
	{
		void PostListDetails(List<Detail> entity);
		void PostDetail(Detail entity);
		Task<IEnumerable<Detail>> GetAllDetailAsync();
	}
}
