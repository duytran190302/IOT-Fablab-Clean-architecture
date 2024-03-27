using IOT.Application.Contract.Persistence;
using IOT.Domain;
using IOT.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace IOT.Persistence.Repository
{
	public class DetailRepository : Repository<Detail, string>, IDetailRepository
	{
		private readonly IOTDbContext iOTDbContext;
		public DetailRepository(IOTDbContext context) : base(context)
		{
			iOTDbContext = context;
		}

		public async Task<IEnumerable<Detail>> GetAllDetailAsync()
		{
		    return await iOTDbContext.Detail.Include(x=>x.Project).Include(x=>x.Worker).Include(x=>x.Machine).ToListAsync();
		}
		public void PostListDetails(List<Detail> entity)
		{
			 iOTDbContext.AddRange(entity);
			 iOTDbContext.SaveChanges();
		}

		public  void PostDetail(Detail entity)
		{
			 iOTDbContext.Add(entity);
			 iOTDbContext.SaveChanges();
		}
	}

}
