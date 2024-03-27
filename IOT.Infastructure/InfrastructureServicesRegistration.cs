using IOT.Application.Contract.Email;
using IOT.Application.Contract.Gmail;
using IOT.Application.Contract.Logging;
using IOT.Application.Models.Email;
using IOT.Application.Models.Gmail;
using IOT.Infastructure.EmailService;
using IOT.Infastructure.GmailService;
using IOT.Infastructure.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IOT.Infastructure
{
	public static class InfrastructureServicesRegistration
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
			services.AddTransient<IEmailSender, EmailSender>();

			services.Configure<GmailSettings>(configuration.GetSection("GmailSettings"));
			services.AddTransient<IGmailSender, GmailSender>();
			services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
			return services;
		}
	}
}
