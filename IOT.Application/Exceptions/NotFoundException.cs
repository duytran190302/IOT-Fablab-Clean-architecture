//using System.ComponentModel.DataAnnotations; note: trung annotations
namespace IOT.Application.Exceptions
{
	public class NotFoundException : Exception
	{
		public NotFoundException(string name, object key) : base($"{name} ({key}) was not found")
		{

		}
	}
}
