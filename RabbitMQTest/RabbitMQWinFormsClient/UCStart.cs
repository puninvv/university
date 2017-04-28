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
    public partial class UCStart : UserControl
    {
        public class UCStartEventArgs : EventArgs
        {
            public string ImageFullPath
            {
                get;
                private set;
            }

            public UCStartEventArgs(string _imgPath)
            {
                ImageFullPath = _imgPath;
            }
        }

        public event EventHandler<UCStartEventArgs> OnCorrectlyInitialized;

        public UCStart()
        {
            InitializeComponent();
        }

        public void ChangeBackGround(Color _color)
        {
            this.BackColor = _color;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (!File.Exists(dlg.FileName))
                    {
                        MessageBox.Show("Incorrect file name");
                        this.BackColor = Color.DarkRed;
                        return;
                    }

                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.Image = Image.FromFile(dlg.FileName);

                    if (OnCorrectlyInitialized != null)
                        OnCorrectlyInitialized(this, new UCStartEventArgs(dlg.FileName));

                    this.BackColor = Color.Green;
                }
            }

            pictureBox1.Enabled = false;
        }
    }
}
