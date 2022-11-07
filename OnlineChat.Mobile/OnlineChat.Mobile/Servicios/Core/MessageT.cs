using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineChat.Mobile.Servicios.Core
{
    public class MessageT
    {
        public string Message { get; set; }
        public int OrigenId { get; set; }
        public int DestinoId { get; set; }
    }
}
