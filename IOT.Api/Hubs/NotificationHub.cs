using IOT.Infastructure.Communication;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.ComponentModel.Design;
using Buffer = IOT.Api.Worker.Buffer;
namespace IOT.Api.Hubs;

public class NotificationHub : Hub
{
	private readonly Buffer _buffer;
	private readonly ManagedMqttClient _mqttClient;

	public NotificationHub(Buffer buffer, ManagedMqttClient mqttClient)
	{
		_buffer = buffer;
		_mqttClient = mqttClient;
	}

	public string SendAll()
	{
		string allTags = _buffer.GetAllTag();
		return allTags;
	}

	public async Task SendAllTag()
	{
		string allTags = _buffer.GetAllTag();

		await Clients.All.SendAsync("GetAll", allTags);
	}

	public async Task SendCommand(string deviceId, string command)
	{
		string topic = $"IOT/Detail/NewDetailToWork/{deviceId}";

		await _mqttClient.Publish(topic, command, true);
	}
}
