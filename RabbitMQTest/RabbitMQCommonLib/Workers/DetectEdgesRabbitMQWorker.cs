using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQCommonLib.Workers
{
    internal class DetectEdgesRabbitMQWorker : IRabbitMQWorker
    {
        public byte[] ProcessTask(RabbitMQTask _task)
        {
            var serializer = new BytesSerializer<Bitmap>();

            var imgFactory = new ImageProcessor.ImageFactory();
            var img = imgFactory.Load(_task.Data);

            var filter = new ImageProcessor.Imaging.Filters.EdgeDetection.PrewittEdgeFilter();
            var detectedEdges = img.DetectEdges(filter, true);

            var resultBitmap = new Bitmap(detectedEdges.Image);
            var resultBytes = serializer.ObjectToByteArray(resultBitmap);

            imgFactory.Dispose();
            img.Dispose();
            detectedEdges.Dispose();
            resultBitmap.Dispose();

            return resultBytes;
        }
    }
}
