using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2CommonLib.Common
{
    public enum ImageType
    {
        Small,
        Large,
        FullHorizontal,
        FullVertical
    }

    internal static class ImageTypeToString
    {
        public static string ToString(ImageType _type)
        {
            switch (_type)
            {
                case ImageType.Small:
                    return "_sb.png";
                case ImageType.Large:
                    return "_lg.png";
                case ImageType.FullHorizontal:
                    return "_full.png";
                case ImageType.FullVertical:
                    return "_vert.jpg";
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}
