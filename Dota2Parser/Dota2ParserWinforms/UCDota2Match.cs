using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dota2CommonLib.Matches;
using Dota2CommonLib.Heroes;

namespace Dota2ParserWinforms
{
    public partial class UCDota2Match : UserControl
    {
        public UCDota2Match()
        {
            InitializeComponent();
        }

        public string LobbyType
        {
            get
            {
                return lblLobbyType.Text;
            }
            set
            {
                lblLobbyType.Text = value;
            }

        }

        public string StartTime
        {
            get
            {
                return lblStartTime.Text;
            }
            set
            {
                lblStartTime.Text = value;
            }
        }

        public string SequnceNumber
        {
            get
            {
                return lblSequenceNumber.Text;
            }
            set
            {
                lblSequenceNumber.Text = value;
            }
        }

        public string MatchId
        {
            get
            {
                return lblMatchId.Text;
            }

            set
            {
                lblMatchId.Text = value;
            }
        }

        public Dictionary<int, Hero> Heroes
        {
            get;
            set;
        }

        public Player[] Players
        {
            set
            {
                foreach (var player in value)
                {
                    var element = new UCDota2Element();
                    element.Icon = Heroes[player.HeroId].Picture;
                    element.Caption = player.AccountId.ToString();

                    if (player.IsRadiant)
                        flowLayoutPanelRadiant.Controls.Add(element);
                    else
                        flowLayoutPanelDire.Controls.Add(element);
                }
            }
        }
    }
}
