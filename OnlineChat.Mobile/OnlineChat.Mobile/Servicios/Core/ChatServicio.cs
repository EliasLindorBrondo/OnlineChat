using Microsoft.AspNetCore.SignalR.Client;
using OnlineChat.Mobile.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(OnlineChat.Mobile.Servicios.Core.ChatServicio))]
namespace OnlineChat.Mobile.Servicios.Core
{
    public class ChatServicio : IChatServicio
    {
        private readonly HubConnection hubconexion;
        public event EventHandler<MessageT> MessageReceived;
        public event EventHandler Connected;
        public static int DeviceId { get; set; }
        public ChatServicio()
        {
            hubconexion = new HubConnectionBuilder().WithUrl("http://192.168.0.108/APIOnlineChat/Conexion").Build();
        }

        public async Task Connect()
        {
            await hubconexion.StartAsync();
            await ConexionId();
            //Activo el evento connect
            Connected?.Invoke(this, null);
            //Llamadas del servidor al cliente.
            hubconexion.On<MessageT>("NewMessage", NewMessage);
        }

        public async Task Disconnect()
        {
            await hubconexion.StopAsync();
        }

        private async Task ConexionId()
        {
            await hubconexion.InvokeAsync("ConexionId", new Dispositivo { Id = DeviceId });
        }
        public async Task SendMessage(string userId,string message)
        {
            await hubconexion.InvokeAsync("SendMessage",userId,message);
        }

        public void ReceivedMessage(Action<string,string> GetMessageAndUser)
        {
            hubconexion.On("ReceivedMessage",GetMessageAndUser);
        }
        public async Task SendMessageToDevice(MessageT item)
        {
            await hubconexion.InvokeAsync("SendMessageToDevice", item); 
        }
        private void NewMessage(MessageT obj)
        {
            //Activo el evento
            MessageReceived?.Invoke(this, obj);
        }
    }
}
