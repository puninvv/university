using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZeroMQ;
using ZeroMQCommonLib;

namespace ZeroMQClient
{
    class ClientMain
    {
        static void Main(string[] args)
        {
            var messageCreator = new StringMessageCreator();

            using (var context = ZContext.Create())
            {
                using (var socket = ZSocket.Create(context, ZSocketType.REQ))
                {
                    socket.Connect(Properties.Settings.Default.ConnectionURI);

                    while (true)
                    {
                        Thread.Sleep(Properties.Settings.Default.Delay);

                        var msg = "от клиента";
                        var reply = messageCreator.CreateMessage(msg);

                        Console.WriteLine("Sending : " + msg + Environment.NewLine);
                        socket.SendMessage(reply);

                        var received = socket.ReceiveMessage();
                        Console.WriteLine("Received: " + received.PopString());
                    }
                }
            }
        }
    }
}
