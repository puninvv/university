using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RabbitMQWinFormsClient
{
    public partial class UCEnd : UserControl
    {
        public UCEnd()
        {
            InitializeComponent();
            Cursor = Cursors.Arrow;
        }

        public void SetupImage(Image _img)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = _img;
            Cursor = Cursors.Hand;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog())
            {
                dlg.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image.Save(dlg.FileName);

                    this.BackColor = Color.Green;
                    Cursor = Cursors.Arrow;
                }
            }

            pictureBox1.Enabled = false;
        }
    }
}
