using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dota2CommonLib.Common
{
    [DataContract]
    public abstract class PictureContainerBase
    {
        [IgnoreDataMember]
        public Image Picture
        {
            get
            {
                return GetPicture(PictureType);
            }
            set
            {
            }
        }

        private Image GetPicture(ImageType _type)
        {
            if (!Directory.Exists(CacheFolder))
                Directory.CreateDirectory(CacheFolder);

            var itemNameWithSuffix = ItemName + ImageTypeToString.ToString(_type);

            string localFilename = CacheFolder + itemNameWithSuffix;

            if (File.Exists(localFilename))
                return Image.FromFile(localFilename);

            var url = BaseURI + itemNameWithSuffix;

            try
            {
                using (WebClient client = new WebClient())
                    client.DownloadFile(url, localFilename);

                return Image.FromFile(localFilename);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(url);
                System.Diagnostics.Debug.WriteLine(ex);
                return null;
            }
        }

        [IgnoreDataMember]
        protected abstract string ItemName { get; set; }

        [IgnoreDataMember]
        protected abstract string CacheFolder{ get; set; }

        [IgnoreDataMember]
        protected abstract string BaseURI { get; set; }

        [IgnoreDataMember]
        protected abstract ImageType PictureType { get; set; }
    }
}
