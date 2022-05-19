using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElMarket.Hubs
{
    public class Main:Hub
    {
        public void SendMessage(string MessageFrom , string MessageContent)
        {
            Clients.All.recieveMessage(MessageFrom, MessageContent);
        }
    }
}