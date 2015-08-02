using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kursach
{
    public partial class MainForm : Form
    {
        private Graphics graphics;
        private myPolygon first;
        private myPolygon second;
        private Boolean first_ended;
        private Boolean second_ended;
        private Boolean painted;
        public MainForm()
        {
            InitializeComponent();
            graphics = this.CreateGraphics();
            first_ended = second_ended = painted = false;
            first = null; second = null;
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            button1.Enabled = true;
            if (painted)
            {
                graphics.Clear(Color.White);
                first_ended = second_ended = painted = false;
                first = null;
                second = null;

                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox4.Checked = false;
            }
            if (!first_ended && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (first == null)
                    first = new myPolygon(new myVertex(e.X, e.Y), graphics, new Pen(Color.Red, 2));
                else
                {
                    mySection attention = new mySection(first.GetLastVertex(), new myVertex(e.X, e.Y));
                    if (first.Intersect(attention))
                        MessageBox.Show("Самопересечения не допускаются. Будьте внимательнее!");
                    else
                        if (!first.AddVertex(new myVertex(e.X, e.Y), graphics, new Pen(Color.Red, 2)))
                        {
                            first_ended = true;
                            checkBox1.Checked = true;
                        }
                }
            }
            else
                if (!second_ended && e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    
                    if (second == null)
                        second = new myPolygon(new myVertex(e.X, e.Y), graphics, new Pen(Color.Green, 2));
                    else
                    {
                        mySection attention = new mySection(second.GetLastVertex(), new myVertex(e.X, e.Y));
                        if (second.Intersect(attention))
                            MessageBox.Show("Самопересечения не допускаются. Будьте внимательнее!");
                        else
                            if (!second.AddVertex(new myVertex(e.X, e.Y), graphics, new Pen(Color.Green, 2)))
                            {
                                second_ended = true;
                                checkBox2.Checked = true;
                            }
                    }
                }
                else
                {
                    graphics.Clear(Color.White);
                    if (first_ended && second_ended)
                        first.GetUnionWith(second, graphics, new Pen(Color.Brown,3), checkBox4.Checked);
                    painted = true;
                    MessageBox.Show("Для того, чтобы выполнить процедуру с новыми многоугольниками\n кликните мышкой по введенным многоугольникам.");
                }
        }

        private void оПрограммеToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            String message0 = "Данная программа позволяет построить объединение двух полигонов, ";
            String message1 = "реализуя модифицированный алгоритм Вейлера-Азертона.\n\n";
            String message2 = "Для того чтобы задать первый многоугольник - щёлкайте левой\n";
            String message3 = "кнопкой мыши по свободной области...\n";
            String message4 = "Для того чтобы задать второй многоугольник - щёлкайте правой\n";
            String message5 = "кнопкой мыши по свободной области...\n";
            String message6 = "Затем нажмите любую кнопку мыши для того чтобы увидеть результат!\n";
            String message65 = "(Объединение имеет коричневый цвет, а пустоты останутся белыми!\n\n\n";
            String message7 = "Автор - Пунин Виктор 2 курс ПМ-ПУ СПбГУ";

            String message = message0 + message1 + message2 + message3 + message4 + message5 + message6 + message65 + message7;
            MessageBox.Show(message, "О программе");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            String message0 = "Данная программа позволяет построить объединение двух полигонов, ";
            String message1 = "реализуя модифицированный алгоритм Вейлера-Азертона.\n\n";
            String message2 = "Для того чтобы задать первый многоугольник - щёлкайте левой\n";
            String message3 = "кнопкой мыши по свободной области...(он будет отображен красным цветом)\n";
            String message4 = "Для того чтобы задать второй многоугольник - щёлкайте правой\n";
            String message5 = "кнопкой мыши по свободной области...(он будет отображен зелёным цветом)\n";
            String message6 = "Затем нажмите любую кнопку мыши для того чтобы увидеть результат!\n";
            String message65 = "(Объединение имеет коричневый цвет, а пустоты останутся белыми!\n\n\n";
            String message7 = "Автор - Пунин Виктор 2 курс ПМ-ПУ СПбГУ";

            String message = message0 + message1 + message2 + message3 + message4 + message5 + message6 + message65 + message7;
            MessageBox.Show(message, "О программе");
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            graphics = this.CreateGraphics();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true && checkBox2.Checked == true)
            {
                first.GetUnionWith(second, graphics, new Pen(Color.Brown, 3), checkBox4.Checked);
                painted = true;
                MessageBox.Show("Для того, чтобы выполнить процедуру с новыми многоугольниками\n кликните мышкой по введенным многоугольникам.");
                button1.Enabled = false;
            }
            else
                MessageBox.Show("Один из многоугольников введен не до конца! Продолжите ввод.");
        }
    }
}
