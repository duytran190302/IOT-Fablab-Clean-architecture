using IOT.Infastructure.Communication;
using IOT.Infastructure;
using IOT.Application;
using IOT.Persistence;
using IOT.Api.Middleware;
using IOT.Api.Hubs;
using Microsoft.Extensions.Hosting;
using Buffer = IOT.Api.Worker.Buffer;
using IOT.Api.Worker;
namespace IOT.Api
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddApplicationServices();
			builder.Services.AddInfrastructureServices(builder.Configuration);
			builder.Services.AddPersistenceServices(builder.Configuration);

			builder.Services.AddControllers();

			builder.Services.AddCors(options =>
			{
				options.AddPolicy("all", builder => builder.AllowAnyOrigin()
				.AllowAnyHeader()
				.AllowAnyMethod());
			});
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.Configure<MqttOptions>(builder.Configuration.GetSection("MqttOptions"));

			builder.Services.AddSignalR();
			builder.Services.AddSingleton<ManagedMqttClient>();
			builder.Services.AddSingleton<Buffer>();
			builder.Services.AddHostedService<ScadaHost>();
			var app = builder.Build();
			app.UseMiddleware<ExceptionMiddleware>();
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();
			app.MapHub<NotificationHub>("/notificationHub");
			app.Run();
		}
	}
}
