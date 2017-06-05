using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dota2CommonLib.Heroes;

namespace Dota2ParserWinforms
{
    public partial class UCDota2Element : UserControl
    {
        public Image Icon
        {
            get
            {
                return pictureBox1.Image;
            }
            set
            {
                pictureBox1.Image = value;
            }
        }
        
        public string Caption
        {
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value;
            }
        }

        public UCDota2Element()
        {
            InitializeComponent();
        }
    }
}
