using OnlineChat.Mobile.Servicios.Core;
using OnlineChat.Mobile.Servicios.Interfaces;
using OnlineChat.Mobile.ViewModels;
using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace OnlineChat.Mobile.Views
{
    public partial class MainPage 
    {
        private readonly IChatServicio chatServicio;
        public MainPage()
        {
            InitializeComponent();
            chatServicio = DependencyService.Get<IChatServicio>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            chatServicio.MessageReceived += chatServicio_RecibirMensaje;
            chatServicio.Connected += chatServicio_Conectado;
        }

        protected override void OnDisappearing()
        {
            chatServicio.MessageReceived -= chatServicio_RecibirMensaje;
            chatServicio.Connected -= chatServicio_Conectado;
            base.OnDisappearing();
        }

        private void chatServicio_Conectado(object sender, EventArgs e)
        {
            Estado.Text = "Conectado.";
            Estado.FontAttributes = FontAttributes.Bold;
            OrgId.IsEnabled = false;
            btConexion.IsEnabled = false;
        }

        private async void chatServicio_RecibirMensaje(object sender, MessageT e)
        {
            await DisplayAlert("Nuevo Mensaje", e.Message, "Listo");
        }

        private void btEnviar_Clicked(object sender, EventArgs e)
        {
            chatServicio.SendMessageToDevice(new MessageT
            {
                Message = eMessage.Text,
                OrigenId = Convert.ToInt32(OrgId.Text),
                DestinoId = Convert.ToInt32(DesId.Text)
            });
        }

        private void btConexion_Clicked(object sender, EventArgs e)
        {
            ChatServicio.DeviceId = Convert.ToInt32(OrgId.Text);
            chatServicio.Connect();
        }
    }
}
