using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Net;
using Dota2CommonLib.Common;

namespace Dota2CommonLib.Heroes
{
    [DataContract]
    public class Hero
    {
        [DataMember(Name = "name")]
        public string Name
        {
            get; set;
        }

        [DataMember(Name = "localized_name")]
        public string LocalizedName
        {
            get; set;
        }

        [DataMember(Name = "id")]
        public int Id
        {
            get; set;
        }

        [DataMember(Name = "picture", IsRequired = false)]
        public Image Picture
        {
            get { return GetImage(ImageType.FullVertical); }
            set { }
        }

        private Image GetImage(ImageType _imageType)
        {
            var heronameWithSuffix = Name.Replace("npc_dota_hero_", "");

            heronameWithSuffix += ImageTypeToString.ToString(_imageType);

            if (!Directory.Exists(Properties.Settings.Default.ImagesFolderHeroes))
                Directory.CreateDirectory(Properties.Settings.Default.ImagesFolderHeroes);

            string localFilename = Properties.Settings.Default.ImagesFolderHeroes + heronameWithSuffix;

            if (File.Exists(localFilename))
                return Image.FromFile(localFilename);

            var url = @"http://cdn.dota2.com/apps/dota2/images/heroes/" + heronameWithSuffix;

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
    }
}
