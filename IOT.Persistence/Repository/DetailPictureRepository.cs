using AutoMapper;
using IOT.Application.Contract.Persistence;
using IOT.Domain;
using IOT.Persistence.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Persistence.Repository
{
	public class DetailPictureRepository : Repository<DetailPicture, (Guid, string)>, IDetailPictureRepository
	{
		private readonly IOTDbContext iOTDbContext;
		public DetailPictureRepository(IOTDbContext context) : base(context)
		{
		   iOTDbContext = context;
		}

		public void PostDetailPicture(DetailPicture picture)
		{
			 iOTDbContext.Add(picture);
			 iOTDbContext.SaveChanges();
		}

	}

}
