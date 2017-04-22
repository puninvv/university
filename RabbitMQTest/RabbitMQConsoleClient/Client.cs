using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQCommonLib.Workers.UserRabbitMQWorker;

namespace RabbitMQConsoleClient
{
    class Client
    {
        static void Main(string[] args)
        {
            var rnd = new Random();

            Console.ReadKey();

            using (var client = new RabbitMQCommonLib.Client.UserRabbitMQClient.UserRabbitMQClient())
            {
                var user = new User() { Name = Guid.NewGuid().ToString(), Age = rnd.Next(10, 100) };
                Console.WriteLine(" [x] Requesting pair for user {0}", user);

                var response = client.GetResponce(user);
                Console.WriteLine(" [.] Got '{0}'", response);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
