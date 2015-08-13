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
    }
}
