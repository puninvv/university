using RabbitMQCommonLib.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessor;

namespace RabbitMQCommonLib.Workers
{
    internal class GaussianBlurRabbitMQWorker : ImageProcessingHelper
    {
        protected override ImageFactory ProcessImage(ImageFactory _img)
        {
            return _img.GaussianBlur(20);
        }
    }
}
