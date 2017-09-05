using Code2PngBot.Responders;
using ColorCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Code2PngBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var bot = new BotWrapper();

            Console.WriteLine("Enter \"Stop\" to stop bot");

            while (!Console.ReadLine().Equals("stop", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.Clear();
                Console.WriteLine("Enter \"Stop\" to stop bot");
            }

            bot.Dispose();
        }
    }
}
