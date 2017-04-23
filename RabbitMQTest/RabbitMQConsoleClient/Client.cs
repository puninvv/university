using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQCommonLib.Client;
using System.Drawing;
using RabbitMQCommonLib;
using System.IO;

namespace RabbitMQConsoleClient
{
    class Client
    {
        static void Main(string[] args)
        {
            var rnd = new Random();

            using (var client = new RabbitMQClient())
            {
                var imgPath = Console.ReadLine();

                var bmp = new Bitmap(imgPath);
                var serializer = new BytesSerializer<Bitmap>();

                var task = new RabbitMQTask() { TaskType = RabbitMQTaskType.DetectEdges, Data = serializer.ObjectToByteArray(bmp) };

                Console.WriteLine(" [x] Requesting {0}", imgPath);

                var response = client.GetResponce(task);

                var result = serializer.ByteArrayToObject(response.Data);
                var newFileName = Path.GetFileNameWithoutExtension(imgPath) + "_result" + Path.GetExtension(imgPath);
                var resultPath = Path.Combine(Path.GetDirectoryName(imgPath), newFileName);

                result.Save(resultPath);

                bmp.Dispose();
                result.Dispose();

                Console.WriteLine(" [.] Got {0}", resultPath);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
