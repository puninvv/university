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
            var setup = Properties.Settings.Default;

            using (var worker = new RabbitMQCommonLib.Workers.RabbitMQWorker(setup.Host, setup.User, setup.Pass, setup.Port))
            {
                Console.WriteLine(" Press [exist] to exit or\ndrop [queue-name]");

                while (true)
                {
                    var cmd = Console.ReadLine();

                    if (cmd.Equals("exit"))
                        return;

                    var cmds = cmd.Split(' ');
                    if (cmd[0].Equals("drop"))
                    {
                        worker.DropQueue(cmds[1]);
                    }
                }
            }
        }
    }
}
