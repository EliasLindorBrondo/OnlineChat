using OnlineChat.Mobile.Servicios.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Mobile.Servicios.Interfaces
{
    public interface IChatServicio
    {
        event EventHandler<MessageT> MessageReceived;
        event EventHandler Connected;
        Task Connect();
        Task Disconnect();
        Task SendMessage(string userId, string message);
        Task SendMessageToDevice(MessageT item);
        void ReceivedMessage(Action<string, string> GetMessageAndUser);
    }
}
