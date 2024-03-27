using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Application.Contract.Persistence
{
	public interface IUnitOfWork
	{
		IDetailPictureRepository detailPictureRepository { get; }
		IDetailRepository detailRepository { get; }
		IMachineRepository machineRepository { get; }
		IOderRepository oderRepository { get; }
		IOEERepository oeeRepository { get; }
		IProjectRepository projectRepository { get; }
		IWorkerPictureRepository workerPictureRepository { get; }
		IWorkerRepository workerRepository { get; }
		Task<int> CompleteAsync();
		void Complete();

	}
}
