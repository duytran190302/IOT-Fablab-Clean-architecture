using IOT.Application.Contract.Persistence;
using IOT.Persistence.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Persistence.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly IOTDbContext _db;
		public UnitOfWork(IOTDbContext db)
        {
            _db = db;
			detailPictureRepository = new DetailPictureRepository(db);
			detailRepository=new DetailRepository(db);
			machineRepository = new MachineRepository(db);
			oderRepository = new OderRepository(db);
			oeeRepository = new OEERepository(db);
			projectRepository = new ProjectRepository(db);
			workerPictureRepository=new WorkerPictureRepository(db);
			workerRepository =new WorkerRepository(db);
		}
        public IDetailPictureRepository detailPictureRepository { get; private set; }

		public IDetailRepository detailRepository { get; private set; }

		public IMachineRepository machineRepository { get; private set; }

		public IOderRepository oderRepository { get; private set; }

		public IOEERepository oeeRepository { get; private set; }

		public IProjectRepository projectRepository { get; private set; }

		public IWorkerPictureRepository workerPictureRepository { get; private set; }

		public IWorkerRepository workerRepository { get; private set; }

		public void Complete()
		{
			_db.SaveChanges();
		}

		public async Task<int> CompleteAsync()
		{
			return await _db.SaveChangesAsync();
		}

		public void Dispose()
		{
			_db.Dispose();
		}
	}
}
