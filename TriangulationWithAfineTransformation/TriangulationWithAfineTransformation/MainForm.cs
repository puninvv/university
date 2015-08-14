using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TriangulationWithAfineTransformation
{
    public partial class MainForm : Form
    {
        private static Pen defPointPen = new Pen(Color.Red, 2);
        private static Pen defLinePen = new Pen(Color.LimeGreen, 2);

        private static Graphics graphic;
        private static int formHeight;

        public MainForm()
        {
            InitializeComponent();

            graphic = this.CreateGraphics();
            formHeight = this.Size.Height;
        }

        private void OnMainFormResize(object sender, EventArgs e)
        {
            formHeight = this.Size.Height;
            graphic = this.CreateGraphics();
            graphic.Clear(this.BackColor);
        }

        
        private void LoadImage(PictureBox pictureBox) 
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";
                dlg.Filter = "bmp files (*.bmp)|*.bmp";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // Create a new Bitmap object from the picture file on disk,
                    // and assign that to the PictureBox.Image property
                    pictureBox.Image = new Bitmap(dlg.FileName);
                }
            }
        }

        private void ImageToOnClick(object sender, EventArgs e)
        {
            LoadImage(ImageTo);
        }

        private void ImageFromOnClick(object sender, EventArgs e)
        {
            LoadImage(ImageFrom);
        }

        private void GetMinutias (){
            var image = ImageFrom.Image;
            TriangulationWithAfineTransformation.Classes.ImageHelper.LoadImage<int>((Bitmap)ImageFrom.Image);
            var bytes = TriangulationWithAfineTransformation.Classes.ImageHelper.LoadImage<int>((Bitmap)ImageFrom.Image);
            TriangulationWithAfineTransformation.Classes.PixelwiseOrientationField field = new TriangulationWithAfineTransformation.Classes.PixelwiseOrientationField(bytes, 16);

            List<TriangulationWithAfineTransformation.Classes.Minutia> minutias = TriangulationWithAfineTransformation.Classes.MinutiaDetector.GetMinutias(bytes, field);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetMinutias();
        }
    }
}
