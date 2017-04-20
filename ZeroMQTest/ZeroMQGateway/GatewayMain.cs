using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZeroMQ;
using ZeroMQCommonLib;

namespace ZeroMQGateway
{
    class GatewayMain
    {
        static void Main(string[] args)
        {
            var messageCreator = new StringMessageCreator();

            using (var context = ZContext.Create())
            {
                using (var socket = ZSocket.Create(context, ZSocketType.REP))
                {
                    socket.Bind(Properties.Settings.Default.BindingURI);

                    while (true)
                    {
                        Thread.Sleep(Properties.Settings.Default.Delay);

                        var received = socket.ReceiveMessage();
                        Console.WriteLine("Received: " + received.PopString());

                        var msg = "от гейтвэя";
                        var reply = messageCreator.CreateMessage(msg);
                        Console.WriteLine("Sending : " + msg + Environment.NewLine);

                        socket.SendMessage(reply);
                    }
                }
            }
        }
    }
}
