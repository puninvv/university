﻿using RabbitMQ.Client;
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
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
