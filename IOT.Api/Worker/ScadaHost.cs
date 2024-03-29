using IOT.Api.Hubs;
using IOT.Api.MQTTModels;
using IOT.Application.Contract.Email;
using IOT.Application.Contract.Gmail;
using IOT.Application.Contract.Persistence;
using IOT.Application.Models.Email;
using IOT.Application.Models.Gmail;
using IOT.Domain;
using IOT.Infastructure.Communication;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Text.Json.Nodes;

namespace IOT.Api.Worker;

public class ScadaHost : BackgroundService
{
    private readonly ManagedMqttClient _mqttClient;
    private readonly Buffer _buffer;
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly IGmailSender _gmailSender;
    //private readonly IEmailSender _emailSender;
    //private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceScopeFactory _scopeFactory;



    public ScadaHost(ManagedMqttClient mqttClient, Buffer buffer,
        IHubContext<NotificationHub> hubContext,
        IServiceScopeFactory scopeFactory,
        IGmailSender gmailSender
        //IEmailSender emailSender,
        //IUnitOfWork unitOfWork
        )
    {
        _mqttClient = mqttClient;
        _buffer = buffer;
        _hubContext = hubContext;
        _gmailSender = gmailSender;
        //_emailSender = emailSender;
        //_unitOfWork = unitOfWork;
        _scopeFactory = scopeFactory;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await ConnectToMqttBrokerAsync();
    }

    private async Task ConnectToMqttBrokerAsync()
    {
        _mqttClient.MessageReceived += OnMqttClientMessageReceivedAsync;
        await _mqttClient.ConnectAsync();
        await _mqttClient.Subscribe("FABLAB/+/+/+/+");
		//FABLAB/MACHANICAL_MACHINES/KB30/Metric/OEE
		await _mqttClient.Subscribe("FABLAB/+/+/+");
		//FABLAB/Environment/Metric/Humidity
	}

	private async Task OnMqttClientMessageReceivedAsync(MqttMessage e)
    {
        var topic = e.Topic;
        var payloadMessage = e.Payload;
        if (topic is null || payloadMessage is null)
        {
            return;
        }
        var topicSegments = topic.Split('/');
        var topic1 = topicSegments[1];


        payloadMessage = payloadMessage.Replace("\\", "");
        payloadMessage = payloadMessage.Replace("\r", "");
        payloadMessage = payloadMessage.Replace("\n", "");
        payloadMessage = payloadMessage.Replace(" ", "");
        payloadMessage = payloadMessage.Replace("false", "\"FALSE\"");
        payloadMessage = payloadMessage.Replace("true", "\"TRUE\"");
        payloadMessage = payloadMessage.Replace("[", "");
        payloadMessage = payloadMessage.Replace("]", "");


        switch (topic1)
        {
            // gửi chỉ số oee, xử lí lưu database, gửi lên web
            case "MACHANICAL_MACHINES":


                switch (topicSegments[4]) 
                {
					case "OEE":
						var machineId = topicSegments[2];
						var oee = JsonConvert.DeserializeObject<OeeRecieve>(payloadMessage);
						var A = oee.idleTime / oee.shiftTime;
						var P = oee.operationTime / oee.idleTime;
						var oeeDb = new OEE
						{
							MachineId = machineId,
							IdleTime = oee.idleTime,
							ShiftTime = oee.shiftTime,
							OperationTime = oee.operationTime,
							TimeStamp = oee.timestamp,
							Oee = A * P
						};
						try
						{
							using (var scope = _scopeFactory.CreateScope())
							{
								var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

								_unitOfWork.oeeRepository.Add(oeeDb);
								await _unitOfWork.CompleteAsync();

							}
						}
						catch (Exception ex)
						{
							throw ex;
						}

						string jsonDb = JsonConvert.SerializeObject(oeeDb);
                        // ten chu de, ten may, value
						var notification = new TagChangedNotification(topicSegments[4], machineId, jsonDb);
						_buffer.Update(notification);

						var oeeSend = new OeeSend
						{
							DeviceId = machineId,
							IdleTime = oee.idleTime,
							OperationTime = oee.shiftTime,
							Oee = A * P,
							ShiftTime = oee.shiftTime,
							Timestamp = oee.timestamp,
							Topic = topic1,
						};
						string jsonNoti = JsonConvert.SerializeObject(oeeSend);
						jsonNoti = jsonNoti.Replace("\\", "");
						jsonNoti = jsonNoti.Replace("\r", "");
						jsonNoti = jsonNoti.Replace("\n", "");
						jsonNoti = jsonNoti.Replace(" ", "");
						await _hubContext.Clients.All.SendAsync("OeeChanged", jsonNoti);

						break;
                    case "Vibration":
                    case "Speed":
                    case "Power":
					case "MachineStatus":
					case "Operator":
                        var data = JsonConvert.DeserializeObject<TempleteObject>(payloadMessage);
                        var dataSend = new DataMachineSend
                        {
                            machineId = topicSegments[2],
                            name= data.name,
                            timestamp = data.timestamp,
                            value = data.value
                        };
                        string dataMachine = JsonConvert.SerializeObject(dataSend);
						await _hubContext.Clients.All.SendAsync("DataMachineChanged", dataMachine);
						var dataMachineBuffer = new TagChangedNotification(topicSegments[4], topicSegments[2], dataMachine);
						_buffer.Update(dataMachineBuffer);
						break;
                    case "MaterialCodeProducting":
						var detailOperating = JsonConvert.DeserializeObject<DetailUpdate>(payloadMessage);
						try
						{
							using (var scope = _scopeFactory.CreateScope())
							{
								var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

								var detail = await _unitOfWork.detailRepository.GetByIdAsync(detailOperating.value);
								detail.StartTime = detailOperating.timestamp;
                                detail.EndTime = null;
								detail.Worker = await _unitOfWork.workerRepository.GetByIdAsync(detailOperating.operatorid);
								detail.DetailStatus = DetailStatus.working;

								_unitOfWork.detailRepository.Update(detail);
								await _unitOfWork.CompleteAsync();

							}
						}
						catch (Exception ex)
						{
							throw ex;
						}
						break;


                    case "MaterialCodeDone":
						var detailOperated = JsonConvert.DeserializeObject<DetailUpdate>(payloadMessage);
						try
						{
							using (var scope = _scopeFactory.CreateScope())
							{
								var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

								var detail = await _unitOfWork.detailRepository.GetByIdAsync(detailOperated.value);
								detail.EndTime = detailOperated.timestamp;
								detail.DetailStatus = DetailStatus.completed;

								_unitOfWork.detailRepository.Update(detail);
								await _unitOfWork.CompleteAsync();

							}
						}
						catch (Exception ex)
						{
							throw ex;
						}

						break;



				}
                


                break;
            case "Environment":
				var environment = JsonConvert.DeserializeObject<TempleteObject>(payloadMessage);
                var environmentSend = new EnvironmentSend
                {
                    name = environment.name,
                    timestamp = environment.timestamp,
                    value = environment.value,
                    sensorId = topicSegments[3]
                };

				string jsonEnvironment = JsonConvert.SerializeObject(environmentSend);
                // moi truong/ ten cam bien/ value
                var envirBuffer = new TagChangedNotification(topicSegments[1], topicSegments[3], jsonEnvironment);
                _buffer.Update(envirBuffer);
				await _hubContext.Clients.All.SendAsync("EnvironmentChanged", jsonEnvironment);



				break;





            // gửi email warning
            case "GmailWarning":
                var machineIdWarning = topicSegments[3];
                var notificationWarning = new TagChangedNotification(topic1, machineIdWarning, payloadMessage);
                _buffer.Update(notificationWarning);



                string jsonWarning = JsonConvert.SerializeObject(notificationWarning);
                await _hubContext.Clients.All.SendAsync("TagChanged", jsonWarning);
                var gmail = new GmailMessage
                {
                    To = "duy.tran190302@gmail.com",
                    Subject = machineIdWarning,
                    Body = jsonWarning

                };
                try
                {
                    await _gmailSender.SendGmail(gmail);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                break;

        }







    }
}
