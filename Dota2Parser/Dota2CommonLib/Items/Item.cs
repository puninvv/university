using Dota2CommonLib.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dota2CommonLib.Items
{
    [DataContract]
    public class Item
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "cost")]
        public int Cost { get; set; }

        [DataMember(Name = "secret_shop")]
        public int IsAvailibleInSecretShop { get; set; }

        [DataMember(Name = "side_shop")]
        public int IsAvailibleInSideShop { get; set; }

        [DataMember(Name = "recipe")]
        public int IsRecipe { get; set; }

        [DataMember(Name = "localized_name")]
        public string LocalizedName { get; set; }

        [DataMember(Name = "picture", IsRequired = false)]
        public Image Picture
        {
            get { return GetImage(ImageType.Large); }
            set { }
        }

        private Image GetImage(ImageType _imageType)
        {
            var itemNameWithSuffix = Name.Replace("item_", "").Replace("recipe_", "");

            itemNameWithSuffix += ImageTypeToString.ToString(_imageType);

            if (!Directory.Exists(Properties.Settings.Default.ImagesFolderItems))
                Directory.CreateDirectory(Properties.Settings.Default.ImagesFolderItems);

            string localFilename = Properties.Settings.Default.ImagesFolderItems + itemNameWithSuffix;

            if (File.Exists(localFilename))
                return Image.FromFile(localFilename);

            var url = @"http://cdn.dota2.com/apps/dota2/images/items/" + itemNameWithSuffix;

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
