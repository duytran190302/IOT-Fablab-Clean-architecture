using IOT.Domain;

namespace IOT.Application.Contract.Persistence
{
	public interface IDetailPictureRepository : IRepository<DetailPicture, (Guid,string)>
	{
		void PostDetailPicture(DetailPicture picture);
	}
}
