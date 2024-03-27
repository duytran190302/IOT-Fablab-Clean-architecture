using Microsoft.AspNetCore.Mvc;

namespace IOT.Api.Model
{
	public class CustomProblemDetails : ProblemDetails
	{
		public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
	}
}
