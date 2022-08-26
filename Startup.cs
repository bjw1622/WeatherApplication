using Microsoft.AspNet.SignalR;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherApplication
{
    public class Startup : Hub
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}