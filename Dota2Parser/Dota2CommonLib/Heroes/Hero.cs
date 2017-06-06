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
    public class Hero : PictureContainerBase
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

        protected override string ItemName
        {
            get
            {
                return Name.Replace("npc_dota_hero_", "");
            }
            set
            {
            }
        }

        protected override string CacheFolder
        {
            get
            {
                return Properties.Settings.Default.ImagesFolderHeroes;
            }
            set
            {
            }
        }

        protected override string BaseURI
        {
            get
            {
                return @"http://cdn.dota2.com/apps/dota2/images/heroes/";
            }
            set
            {
            }
        }

        protected override ImageType PictureType
        {
            get
            {
                return ImageType.FullVertical;
            }

            set
            {
                
            }
        }
    }
}
