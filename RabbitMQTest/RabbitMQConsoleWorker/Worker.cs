using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RabbitMQConsoleWorker
{
    class Worker
    {
        static void Main(string[] args)
        {
            using (var worker = new RabbitMQCommonLib.Workers.UserRabbitMQWorker.UserRabbitMQWorker())
            {
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
