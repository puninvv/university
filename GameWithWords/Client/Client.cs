using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Client
{
    public partial class Client : Form
    {
        private String[] BaseOfWords;
        private TcpClient Server;
        private NetworkStream ns;
        private int TimeOfGame;
        private Boolean EndOfGame;
        private Boolean WordIsEntered;
        private String words_;
        private String WordOfGame;


        private void UpdateBases() {
            try
            {
                BaseOfWords = System.IO.File.ReadAllLines("words.txt");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Файлwords не найден. Используются слова по-умолчанию");
                BaseOfWords = new string[] { "хлебушек", "синхрофазатрон", "класснаяиграомг" };
            }
        }
        private Boolean IsInBase(String word)
        {
            for (int i = 0; i < BaseOfWords.Length; i++)
                if (BaseOfWords[i] == word) return true;
            return false;
        }
        private void ConnectToServer()
        {
            try
            {
                String[] input = System.IO.File.ReadAllText("Config.txt").Split(':');
                Server = new TcpClient(input[0], Int32.Parse(input[1]));
            }
            catch
            {
                Server = new TcpClient("localhost", 1488);
            }
            ns = Server.GetStream();
        }
        private void InitializeButtons(Boolean enabled, Boolean ini, String word)
        {
            b1.Enabled = enabled; b2.Enabled = enabled; b3.Enabled = enabled;
            b4.Enabled = enabled; b5.Enabled = enabled; b6.Enabled = enabled; 
            b7.Enabled = enabled; b8.Enabled = enabled; b9.Enabled = enabled;
            b10.Enabled = enabled; b11.Enabled = enabled; b12.Enabled = enabled;
            b13.Enabled = enabled; b14.Enabled = enabled;

            if (ini)
            {
                b1.Text = word[0].ToString(); b2.Text = word[1].ToString();
                b3.Text = word[2].ToString(); b4.Text = word[3].ToString();
                b5.Text = word[4].ToString(); b6.Text = word[5].ToString();
                b7.Text = word[6].ToString(); b8.Text = word[7].ToString();
                b9.Text = word[8].ToString(); b10.Text = word[9].ToString();
                b11.Text = word[10].ToString(); b12.Text = word[11].ToString();
                b13.Text = word[12].ToString(); b14.Text = word[13].ToString();

                WordOfGame = word;
            }
        }
        private void PreparePanel(Boolean flag) 
        {
            BackPanel.Visible = flag;
            Words.Text = "";
            words_ = "";
            StartButton.Enabled = !flag;       
        }
        private void PrepareToTurn(int turn)
        {
            TimeProgress.Value = 0;
            if (turn == 1)
            {
                AddButton.Enabled = true;
                RemoveButton.Enabled = true;
                Turn.Text = "Ваш ход!";
                Turn.ForeColor = System.Drawing.Color.Red;
                MyTimer.Enabled = true;
                OpponentTimer.Enabled = false;
                TimeProgress.RightToLeft = System.Windows.Forms.RightToLeft.No;
            }
            else
            {
                AddButton.Enabled = false;
                RemoveButton.Enabled = false;
                Turn.Text = "Ход соперника!";
                Turn.ForeColor = System.Drawing.Color.Green;
                MyTimer.Enabled = false;
                OpponentTimer.Enabled = true;
                TimeProgress.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            }
            TimerSecond.Enabled = true;
            CurrentWord.Text = "";
        }


        void AppendText(RichTextBox box, Color color, string text)
        {
            int start = box.TextLength;
            box.AppendText(text);
            int end = box.TextLength;

            // Textbox may transform chars, so (end-start) != text.Length
            box.Select(start, end - start);
            {
                box.SelectionColor = color;
                // could set box.SelectionBackColor, box.SelectionFont too.
            }
            box.SelectionLength = 0; // clear
        }


        public Client()
        {
            InitializeComponent();
            PreparePanel(false);
            UpdateBases();
        }

        private String GetMessage() 
        {
            try
            {
                byte[] buffer = new byte[Server.ReceiveBufferSize];
                int data = ns.Read(buffer, 0, Server.ReceiveBufferSize);
                return Encoding.UTF8.GetString(buffer, 0, data);
            }
            catch 
            {
                return "DC";
            }
        }
        private void SendMessage(String message) 
        {
            ns.Write(Encoding.UTF8.GetBytes(message), 0, Encoding.UTF8.GetBytes(message).Length);
        }
        private void StartButton_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectToServer();
                String settings_input = GetMessage();
                String[] settings = settings_input.Split(':');

                int turn = Int32.Parse(settings[0]);
                TimeOfGame = Int32.Parse(settings[1]);
                String word = settings[2];
                InitializeButtons(true, true, word);

                MyTimer.Interval = TimeOfGame*1000;
                OpponentTimer.Interval = TimeOfGame*1000;
                TimeProgress.Maximum = TimeOfGame;

                EndOfGame = false;

                PreparePanel(true);
                if (turn == 1)
                {
                    WordIsEntered = false;
                    PrepareToTurn(1);
                }
                else
                {
                    PrepareToTurn(2);
                }

            }
            catch
            {
                MessageBox.Show("В данный момент сервер недоступен...");
            }
        }

        private void TimerSecond_Tick(object sender, EventArgs e)
        {
            if (TimeProgress.Value < TimeProgress.Maximum)
            {
                TimeProgress.Value++;
            }
            else
                TimerSecond.Enabled = false;
        }
        private void MyTimer_Tick(object sender, EventArgs e)
        {
            MyTimer.Enabled = false;
            if (WordIsEntered)
            {
                SendMessage(CurrentWord.Text);
                AppendText(this.Words, Color.Green, CurrentWord.Text + '\n');
                words_ += CurrentWord.Text + ':';
                PrepareToTurn(2);
                CurrentWord.Text = "";
            }
            else
            {
                SendMessage("DC");
                MessageBox.Show("Вы проиграли!");
                PreparePanel(false);
                MyTimer.Enabled = false;
                OpponentTimer.Enabled = false;
                TimerSecond.Enabled = false;
            }
            WordIsEntered = false;
        }
        private void OpponentTimer_Tick(object sender, EventArgs e)
        {
            OpponentTimer.Enabled = false;
            String input = GetMessage();
            if (input != "WIN")
            {
                AppendText(this.Words, Color.Red, input + '\n');
                words_ += input + ':';
                PrepareToTurn(1);
                InitializeButtons(true, false, "");
            }
            else
            {
                PreparePanel(false);
                MyTimer.Enabled = false;
                OpponentTimer.Enabled = false;
                TimerSecond.Enabled = false;
                MessageBox.Show("Вы победитель!");
            }
        }

        private void b1_Click(object sender, EventArgs e)
        {
            CurrentWord.Text += b1.Text;
            b1.Enabled = false;
        }
        private void b2_Click(object sender, EventArgs e)
        {
            CurrentWord.Text += b2.Text;
            b2.Enabled = false;
        }
        private void b3_Click(object sender, EventArgs e)
        {
            CurrentWord.Text += b3.Text;
            b3.Enabled = false;
        }
        private void b4_Click(object sender, EventArgs e)
        {
            CurrentWord.Text += b4.Text;
            b4.Enabled = false;
        }
        private void b5_Click(object sender, EventArgs e)
        {
            CurrentWord.Text += b5.Text;
            b5.Enabled = false;
        }
        private void b6_Click(object sender, EventArgs e)
        {
            CurrentWord.Text += b6.Text;
            b6.Enabled = false;
        }
        private void b7_Click(object sender, EventArgs e)
        {
            CurrentWord.Text += b7.Text;
            b7.Enabled = false;
        }
        private void b8_Click(object sender, EventArgs e)
        {
            CurrentWord.Text += b8.Text;
            b8.Enabled = false;
        }
        private void b9_Click(object sender, EventArgs e)
        {
            CurrentWord.Text += b9.Text;
            b9.Enabled = false;
        }
        private void b10_Click(object sender, EventArgs e)
        {
            CurrentWord.Text += b10.Text;
            b10.Enabled = false;
        }
        private void b11_Click(object sender, EventArgs e)
        {
            CurrentWord.Text += b11.Text;
            b11.Enabled = false;
        }
        private void b12_Click(object sender, EventArgs e)
        {
            CurrentWord.Text += b12.Text;
            b12.Enabled = false;
        }
        private void b13_Click(object sender, EventArgs e)
        {
            CurrentWord.Text += b13.Text;
            b13.Enabled = false;
        }
        private void b14_Click(object sender, EventArgs e)
        {
            CurrentWord.Text += b14.Text;
            b14.Enabled = false;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (!IsInBase(CurrentWord.Text))
            {
                InitializeButtons(true, false, "");
                WordIsEntered = false;
                CurrentWord.Text = "";
                MessageBox.Show("Такого слова не существует!");

            }
            else
                if (words_.Split(':').Contains(CurrentWord.Text) && CurrentWord.Text != WordOfGame) 
                {
                    InitializeButtons(true, false, "");
                    WordIsEntered = false;
                    CurrentWord.Text = "";
                    MessageBox.Show("Данное слово уже использовалось!");
                }
                else
                {
                    AddButton.Enabled = false;
                    RemoveButton.Enabled = false;
                    WordIsEntered = true;
                }
              
        }
        private void RemoveButton_Click(object sender, EventArgs e)
        {
            InitializeButtons(true, false, "");
            CurrentWord.Text = "";
            WordIsEntered = false;
        }

        void KeyInput(KeyPressEventArgs e)
        {
            if (MyTimer.Enabled && !WordIsEntered)
            {
                if (e.KeyChar.ToString() == "+")
                {
                    if (!IsInBase(CurrentWord.Text))
                    {
                        InitializeButtons(true, false, "");
                        WordIsEntered = false;
                        CurrentWord.Text = "";
                        MessageBox.Show("Такого слова не существует!");

                    }
                    else
                        if (words_.Split(':').Contains(CurrentWord.Text))
                        {
                            InitializeButtons(true, false, "");
                            WordIsEntered = false;
                            CurrentWord.Text = "";
                            MessageBox.Show("Данное слово уже использовалось!");
                        }
                        else
                        {
                            AddButton.Enabled = false;
                            RemoveButton.Enabled = false;
                            WordIsEntered = true;
                        }
                }
                else
                if (e.KeyChar == Convert.ToChar(Keys.Back))
                {
                    InitializeButtons(true, false, "");
                    CurrentWord.Text = "";
                    WordIsEntered = false;
                }
                else
                if (e.KeyChar.ToString() == b1.Text && b1.Enabled == true) 
                { CurrentWord.Text += b1.Text; b1.Enabled = false;}
                else
                if (e.KeyChar.ToString() == b2.Text && b2.Enabled == true) 
                { CurrentWord.Text += b2.Text; b2.Enabled = false;}
                else
                if (e.KeyChar.ToString() == b3.Text && b3.Enabled == true) 
                { CurrentWord.Text += b3.Text; b3.Enabled = false;}
                else
                if (e.KeyChar.ToString() == b4.Text && b4.Enabled == true) 
                { CurrentWord.Text += b4.Text; b4.Enabled = false;}
                else
                if (e.KeyChar.ToString() == b5.Text && b5.Enabled == true) 
                { CurrentWord.Text += b5.Text; b5.Enabled = false;}
                else
                if (e.KeyChar.ToString() == b6.Text && b6.Enabled == true) 
                { CurrentWord.Text += b6.Text; b6.Enabled = false;}
                else
                if (e.KeyChar.ToString() == b7.Text && b7.Enabled == true) 
                { CurrentWord.Text += b7.Text; b7.Enabled = false;}
                else
                if (e.KeyChar.ToString() == b8.Text && b8.Enabled == true) 
                { CurrentWord.Text += b8.Text; b8.Enabled = false;}
                else
                if (e.KeyChar.ToString() == b9.Text && b9.Enabled == true) 
                { CurrentWord.Text += b9.Text; b9.Enabled = false;}
                else
                if (e.KeyChar.ToString() == b10.Text && b10.Enabled == true) 
                { CurrentWord.Text += b10.Text; b10.Enabled = false;}
                else
                if (e.KeyChar.ToString() == b11.Text && b11.Enabled == true) 
                { CurrentWord.Text += b11.Text; b11.Enabled = false;}
                else
                if (e.KeyChar.ToString() == b12.Text && b12.Enabled == true)
                { CurrentWord.Text += b12.Text; b12.Enabled = false; }
                else
                if (e.KeyChar.ToString() == b13.Text && b13.Enabled == true)
                { CurrentWord.Text += b13.Text; b13.Enabled = false; }
                else
                if (e.KeyChar.ToString() == b14.Text && b14.Enabled == true) 
                { CurrentWord.Text += b14.Text; b14.Enabled = false;}     
            }
        }
        private void b1_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyInput(e);
        }
        private void b2_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyInput(e);
        }
        private void b3_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyInput(e);
        }
        private void b4_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyInput(e);
        }
        private void b5_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyInput(e);
        }
        private void b6_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyInput(e);
        }
        private void b7_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyInput(e);
        }
        private void b8_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyInput(e);
        }
        private void b9_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyInput(e);
        }
        private void b10_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyInput(e);
        }
        private void b11_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyInput(e);
        }
        private void b12_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyInput(e);
        }
        private void b13_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyInput(e);
        }
        private void b14_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyInput(e);
        }
        private void Words_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyInput(e);
        }
        private void RemoveButton_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyInput(e);
        }
        private void AddButton_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyInput(e);
        }
        private void StartButton_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyInput(e);
        }
        private void Client_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyInput(e);
        }

    }
}
