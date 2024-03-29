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
        await _mqttClient.Subscribe("Fablab/IOT/+");
        await _mqttClient.Subscribe("Fablab/IOT/+/+");
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
        var topicId = topicSegments[2];


        payloadMessage = payloadMessage.Replace("\\", "");
        payloadMessage = payloadMessage.Replace("\r", "");
        payloadMessage = payloadMessage.Replace("\n", "");
        payloadMessage = payloadMessage.Replace(" ", "");
        payloadMessage = payloadMessage.Replace("false", "\"FALSE\"");
        payloadMessage = payloadMessage.Replace("true", "\"TRUE\"");
        payloadMessage = payloadMessage.Replace("[", "");
        payloadMessage = payloadMessage.Replace("]", "");


        switch (topicId)
        {
            // gửi chỉ số oee, xử lí lưu database, gửi lên web
            case "MachineOEE":
                var machineId = topicSegments[3];
                var oee = JsonConvert.DeserializeObject<OeeRecieve>(payloadMessage);

                var A = oee.it / oee.st;
                var P = oee.ot / oee.it;
                var oeeDb = new OEE
                {

                    MachineId = machineId,
                    IdleTime = oee.it,
                    ShiftTime = oee.st,
                    OperationTime = oee.ot,
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
                var notification = new TagChangedNotification(topicId, machineId, jsonDb);
                _buffer.Update(notification);

                var oeeSend = new OeeSend
                {
                    DeviceId= machineId,
                    IdleTime= oee.it,
                    OperationTime= oee.st,
                    Oee = A * P,
                    ShiftTime= oee.st,
                    Timestamp = oee.timestamp,
                    Topic = topicId,
				};
                string jsonNoti = JsonConvert.SerializeObject(oeeSend);
                jsonNoti = jsonNoti.Replace("\\", "");
                jsonNoti = jsonNoti.Replace("\r", "");
                jsonNoti = jsonNoti.Replace("\n", "");
                jsonNoti = jsonNoti.Replace(" ", "");
                await _hubContext.Clients.All.SendAsync("OeeChanged", jsonNoti);

                break;
















            case "DetailWorking":
                var detailIdWorking = topicSegments[3]; // ma chi tiet
                var detailWorking = JsonConvert.DeserializeObject<DetailUpdate>(payloadMessage);

                try 
                {
					using (var scope = _scopeFactory.CreateScope())
					{
						var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

						var detail = await _unitOfWork.detailRepository.GetByIdAsync(detailIdWorking);
						detail.StartTime = detailWorking.timestamp;
						detail.Worker = await _unitOfWork.workerRepository.GetByIdAsync(detailWorking.workerId);
						detail.Machine = await _unitOfWork.machineRepository.GetByIdAsync(detailWorking.machineId);
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
            case "DetailCompleted":
                var detailIdCompleted = topicSegments[3]; // ma chi tiet
                var detailCompleted = JsonConvert.DeserializeObject<DetailUpdate>(payloadMessage);
                using (var scope = _scopeFactory.CreateScope())
                {
                    var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                    var detail2 = await _unitOfWork.detailRepository.GetByIdAsync(detailIdCompleted);


                    detail2.EndTime = detailCompleted.timestamp;
                    detail2.Worker = await _unitOfWork.workerRepository.GetByIdAsync(detailCompleted.workerId);
                    detail2.Machine = await _unitOfWork.machineRepository.GetByIdAsync(detailCompleted.machineId);
                    detail2.DetailStatus = DetailStatus.completed;

                    _unitOfWork.detailRepository.Update(detail2);
                    await _unitOfWork.CompleteAsync();


                }


                break;




            // gửi trạng thái máy lên web

            case "MachineStatus":
                var machineIdStatus = topicSegments[3];
                var notificationStatus = new TagChangedNotification(topicId, machineIdStatus, payloadMessage);
                _buffer.Update(notificationStatus);

                string jsonStatus = JsonConvert.SerializeObject(notificationStatus);
                jsonStatus = jsonStatus.Replace("\\", "");
                jsonStatus = jsonStatus.Replace("\r", "");
                jsonStatus = jsonStatus.Replace("\n", "");
                jsonStatus = jsonStatus.Replace(" ", "");
                await _hubContext.Clients.All.SendAsync("TagChanged", jsonStatus);
                break;
            // gửi email warning
            case "GmailWarning":
                var machineIdWarning = topicSegments[3];
                var notificationWarning = new TagChangedNotification(topicId, machineIdWarning, payloadMessage);
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


        //// xử lí 
        //if (!string.IsNullOrEmpty(payloadMessage))
        //{
        //	var notification = new TagChangedNotification(topicId,"aa", payloadMessage);
        //	_buffer.Update(notification);
        //	string json = JsonConvert.SerializeObject(notification);
        //	await _hubContext.Clients.All.SendAsync("TagChanged", json);






    }
}
