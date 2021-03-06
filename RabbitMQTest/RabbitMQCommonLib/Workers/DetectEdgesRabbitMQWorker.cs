﻿using RabbitMQCommonLib.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessor;

namespace RabbitMQCommonLib.Workers
{
    internal class DetectEdgesRabbitMQWorker : ImageProcessingHelper
    {
        protected override ImageFactory ProcessImage(ImageFactory _img)
        {
            var filter = new ImageProcessor.Imaging.Filters.EdgeDetection.PrewittEdgeFilter();
            return _img.DetectEdges(filter, true);
        }
    }
}
