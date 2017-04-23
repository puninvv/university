using ImageProcessor;
using RabbitMQCommonLib.Workers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQCommonLib.Helpers
{
    internal abstract class ImageProcessingHelper : IRabbitMQWorker
    {
        public byte[] ProcessTask(RabbitMQTask _task)
        {
            var serializer = new BytesSerializer<Bitmap>();

            var inputBitmap = serializer.ByteArrayToObject(_task.Data);

            var imgFactory = new ImageFactory();
            var img = imgFactory.Load(inputBitmap);

            var resultImg = ProcessImage(img);

            var resultBitmap = new Bitmap(resultImg.Image);
            var resultBytes = serializer.ObjectToByteArray(resultBitmap);

            imgFactory.Dispose();
            img.Dispose();
            resultImg.Dispose();
            resultBitmap.Dispose();

            return resultBytes;
        }

        protected abstract ImageFactory ProcessImage(ImageFactory _img);
    }
}
