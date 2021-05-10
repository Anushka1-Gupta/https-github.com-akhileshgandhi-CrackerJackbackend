using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace AngularJSAuthentication.API.SignalR
{

    public class ChatHub : Hub
    {
        public void Send(string dboyCurrentLocation)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(dboyCurrentLocation);
        }
    }
}