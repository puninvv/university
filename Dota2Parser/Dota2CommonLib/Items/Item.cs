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
    public class Item : PictureContainerBase
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

        protected override string ItemName
        {
            get
            {
                return Name.Replace("item_", "").Replace("recipe_", "");
            }
            set
            {
            }
        }

        protected override string CacheFolder
        {
            get
            {
                return Properties.Settings.Default.ImagesFolderItems;
            }
            set
            {
            }
        }

        protected override string BaseURI
        {
            get
            {
                return @"http://cdn.dota2.com/apps/dota2/images/items/";
            }
            set
            {
            }
        }

        protected override ImageType PictureType
        {
            get
            {
                return ImageType.Large;
            }
            set
            {
            }
        }
    }
}
