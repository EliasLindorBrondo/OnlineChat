using OnlineChat.Mobile.Model;
using OnlineChat.Mobile.Servicios.Interfaces;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Unity;

namespace OnlineChat.Mobile.ViewModels
{
    public class ChatRoomPageViewModel : ViewModelBase
    {
        private readonly IChatServicio chatServicio;

        private string userName;
        public string UserName { get => userName; set => SetProperty(ref userName, value); }

        private string message;
        public string Message { get => message; set => SetProperty(ref message, value); }

        private IEnumerable<MessageModel> messageList;

        public IEnumerable<MessageModel> MessageList { get => messageList; set => SetProperty(ref messageList,value); }

        public ICommand SendMsgCommand { get; private set; }
        public ChatRoomPageViewModel(INavigationService navigationService, IChatServicio chatServicio) : base(navigationService)
        {
            this.chatServicio = chatServicio;
            SendMsgCommand = new DelegateCommand(SendMsg);
        }
       
        public override async void Initialize(INavigationParameters parameters)
        {
            UserName = parameters.GetValue<string>("UserNameId");
            MessageList = new List<MessageModel>();
            try
            {
                chatServicio.ReceivedMessage(GetMessage);
                await chatServicio.Connect();
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public void SendMsg()
        {
            chatServicio.SendMessage(UserName, message);
            AddMessage(userName, Message, true);
        }
        private void GetMessage(string userName,string message )
        {
            AddMessage(userName,message,false);
        }

        private void AddMessage(string userName,string message,bool IsOwner)
        {
            var tempList = MessageList.ToList<MessageModel>();
            tempList.Add(new MessageModel {IsOwnerMessage = IsOwner, Message = message, UserName = userName });
            MessageList = new List<MessageModel>(tempList);
            Message = string.Empty;
        }
    }
}
