using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQCommonLib.Workers;
using System.Drawing;
using System.Drawing.Imaging;

namespace RabbitMQCommonLib.Workers.ImageRabbitMQWorker
{
    internal class ToGrayScaleRabbitMQWorker : IRabbitMQWorker
    {
        public byte[] ProcessTask(byte[] _data)
        {
            var serializer = new BytesSerializer<Bitmap>();

            var bitmap = serializer.ByteArrayToObject(_data);

            var result = ToGrayScale(bitmap);

            return serializer.ObjectToByteArray(result);
        }

        private static Bitmap ToGrayScale(Bitmap original)
        {
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            Graphics g = Graphics.FromImage(newBitmap);

            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][]
               {
                 new float[] {.3f, .3f, .3f, 0, 0},
                 new float[] {.59f, .59f, .59f, 0, 0},
                 new float[] {.11f, .11f, .11f, 0, 0},
                 new float[] {0, 0, 0, 1, 0},
                 new float[] {0, 0, 0, 0, 1}
               });

            ImageAttributes attributes = new ImageAttributes();

            attributes.SetColorMatrix(colorMatrix);

            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            g.Dispose();
            return newBitmap;
        }
    }
}
