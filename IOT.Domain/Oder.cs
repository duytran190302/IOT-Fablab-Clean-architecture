using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Domain
{
	public class Oder
	{
		public string OderId { get; set; } = string.Empty;
		public string Custummer { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Product {  get; set; } = string.Empty;
		public string Note { get ; set; } = string.Empty;

		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		public List<Project> Projects { get; set; }
	}
}
