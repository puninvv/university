namespace Client
{
    partial class Client
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.BackPanel = new System.Windows.Forms.Panel();
            this.Turn = new System.Windows.Forms.Label();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.CurrentWord = new System.Windows.Forms.Label();
            this.Words = new System.Windows.Forms.RichTextBox();
            this.TimeProgress = new System.Windows.Forms.ProgressBar();
            this.b14 = new System.Windows.Forms.Button();
            this.b13 = new System.Windows.Forms.Button();
            this.b12 = new System.Windows.Forms.Button();
            this.b11 = new System.Windows.Forms.Button();
            this.b10 = new System.Windows.Forms.Button();
            this.b9 = new System.Windows.Forms.Button();
            this.b8 = new System.Windows.Forms.Button();
            this.b7 = new System.Windows.Forms.Button();
            this.b6 = new System.Windows.Forms.Button();
            this.b5 = new System.Windows.Forms.Button();
            this.b4 = new System.Windows.Forms.Button();
            this.b3 = new System.Windows.Forms.Button();
            this.b2 = new System.Windows.Forms.Button();
            this.b1 = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.MyTimer = new System.Windows.Forms.Timer(this.components);
            this.OpponentTimer = new System.Windows.Forms.Timer(this.components);
            this.TimerSecond = new System.Windows.Forms.Timer(this.components);
            this.BackPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // BackPanel
            // 
            this.BackPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.BackPanel.Controls.Add(this.Turn);
            this.BackPanel.Controls.Add(this.RemoveButton);
            this.BackPanel.Controls.Add(this.AddButton);
            this.BackPanel.Controls.Add(this.CurrentWord);
            this.BackPanel.Controls.Add(this.Words);
            this.BackPanel.Controls.Add(this.TimeProgress);
            this.BackPanel.Controls.Add(this.b14);
            this.BackPanel.Controls.Add(this.b13);
            this.BackPanel.Controls.Add(this.b12);
            this.BackPanel.Controls.Add(this.b11);
            this.BackPanel.Controls.Add(this.b10);
            this.BackPanel.Controls.Add(this.b9);
            this.BackPanel.Controls.Add(this.b8);
            this.BackPanel.Controls.Add(this.b7);
            this.BackPanel.Controls.Add(this.b6);
            this.BackPanel.Controls.Add(this.b5);
            this.BackPanel.Controls.Add(this.b4);
            this.BackPanel.Controls.Add(this.b3);
            this.BackPanel.Controls.Add(this.b2);
            this.BackPanel.Controls.Add(this.b1);
            this.BackPanel.Location = new System.Drawing.Point(12, 12);
            this.BackPanel.Name = "BackPanel";
            this.BackPanel.Size = new System.Drawing.Size(409, 258);
            this.BackPanel.TabIndex = 0;
            // 
            // Turn
            // 
            this.Turn.AutoSize = true;
            this.Turn.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Turn.ForeColor = System.Drawing.Color.Red;
            this.Turn.Location = new System.Drawing.Point(243, 206);
            this.Turn.Name = "Turn";
            this.Turn.Size = new System.Drawing.Size(132, 23);
            this.Turn.TabIndex = 20;
            this.Turn.Text = "Ход соперника!";
            // 
            // RemoveButton
            // 
            this.RemoveButton.Location = new System.Drawing.Point(323, 34);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(23, 23);
            this.RemoveButton.TabIndex = 19;
            this.RemoveButton.Text = "-";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            this.RemoveButton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RemoveButton_KeyPress);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(352, 34);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(52, 23);
            this.AddButton.TabIndex = 18;
            this.AddButton.Text = "+";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            this.AddButton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AddButton_KeyPress);
            // 
            // CurrentWord
            // 
            this.CurrentWord.AutoSize = true;
            this.CurrentWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CurrentWord.Location = new System.Drawing.Point(212, 37);
            this.CurrentWord.Name = "CurrentWord";
            this.CurrentWord.Size = new System.Drawing.Size(0, 15);
            this.CurrentWord.TabIndex = 17;
            // 
            // Words
            // 
            this.Words.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Words.Location = new System.Drawing.Point(4, 34);
            this.Words.Name = "Words";
            this.Words.ReadOnly = true;
            this.Words.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.Words.Size = new System.Drawing.Size(197, 221);
            this.Words.TabIndex = 15;
            this.Words.Text = "";
            this.Words.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Words_KeyPress);
            // 
            // TimeProgress
            // 
            this.TimeProgress.Location = new System.Drawing.Point(207, 232);
            this.TimeProgress.Name = "TimeProgress";
            this.TimeProgress.Size = new System.Drawing.Size(197, 23);
            this.TimeProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.TimeProgress.TabIndex = 14;
            // 
            // b14
            // 
            this.b14.Location = new System.Drawing.Point(381, 4);
            this.b14.Name = "b14";
            this.b14.Size = new System.Drawing.Size(23, 23);
            this.b14.TabIndex = 13;
            this.b14.UseVisualStyleBackColor = true;
            this.b14.Click += new System.EventHandler(this.b14_Click);
            this.b14.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.b14_KeyPress);
            // 
            // b13
            // 
            this.b13.Location = new System.Drawing.Point(352, 4);
            this.b13.Name = "b13";
            this.b13.Size = new System.Drawing.Size(23, 23);
            this.b13.TabIndex = 12;
            this.b13.UseVisualStyleBackColor = true;
            this.b13.Click += new System.EventHandler(this.b13_Click);
            this.b13.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.b13_KeyPress);
            // 
            // b12
            // 
            this.b12.Location = new System.Drawing.Point(323, 4);
            this.b12.Name = "b12";
            this.b12.Size = new System.Drawing.Size(23, 23);
            this.b12.TabIndex = 11;
            this.b12.UseVisualStyleBackColor = true;
            this.b12.Click += new System.EventHandler(this.b12_Click);
            this.b12.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.b12_KeyPress);
            // 
            // b11
            // 
            this.b11.Location = new System.Drawing.Point(294, 4);
            this.b11.Name = "b11";
            this.b11.Size = new System.Drawing.Size(23, 23);
            this.b11.TabIndex = 10;
            this.b11.UseVisualStyleBackColor = true;
            this.b11.Click += new System.EventHandler(this.b11_Click);
            this.b11.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.b11_KeyPress);
            // 
            // b10
            // 
            this.b10.Location = new System.Drawing.Point(265, 4);
            this.b10.Name = "b10";
            this.b10.Size = new System.Drawing.Size(23, 23);
            this.b10.TabIndex = 9;
            this.b10.UseVisualStyleBackColor = true;
            this.b10.Click += new System.EventHandler(this.b10_Click);
            this.b10.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.b10_KeyPress);
            // 
            // b9
            // 
            this.b9.Location = new System.Drawing.Point(236, 4);
            this.b9.Name = "b9";
            this.b9.Size = new System.Drawing.Size(23, 23);
            this.b9.TabIndex = 8;
            this.b9.UseVisualStyleBackColor = true;
            this.b9.Click += new System.EventHandler(this.b9_Click);
            this.b9.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.b9_KeyPress);
            // 
            // b8
            // 
            this.b8.Location = new System.Drawing.Point(207, 4);
            this.b8.Name = "b8";
            this.b8.Size = new System.Drawing.Size(23, 23);
            this.b8.TabIndex = 7;
            this.b8.UseVisualStyleBackColor = true;
            this.b8.Click += new System.EventHandler(this.b8_Click);
            this.b8.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.b8_KeyPress);
            // 
            // b7
            // 
            this.b7.Location = new System.Drawing.Point(178, 4);
            this.b7.Name = "b7";
            this.b7.Size = new System.Drawing.Size(23, 23);
            this.b7.TabIndex = 6;
            this.b7.UseVisualStyleBackColor = true;
            this.b7.Click += new System.EventHandler(this.b7_Click);
            this.b7.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.b7_KeyPress);
            // 
            // b6
            // 
            this.b6.Location = new System.Drawing.Point(149, 4);
            this.b6.Name = "b6";
            this.b6.Size = new System.Drawing.Size(23, 23);
            this.b6.TabIndex = 5;
            this.b6.UseVisualStyleBackColor = true;
            this.b6.Click += new System.EventHandler(this.b6_Click);
            this.b6.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.b6_KeyPress);
            // 
            // b5
            // 
            this.b5.Location = new System.Drawing.Point(120, 4);
            this.b5.Name = "b5";
            this.b5.Size = new System.Drawing.Size(23, 23);
            this.b5.TabIndex = 4;
            this.b5.UseVisualStyleBackColor = true;
            this.b5.Click += new System.EventHandler(this.b5_Click);
            this.b5.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.b5_KeyPress);
            // 
            // b4
            // 
            this.b4.Location = new System.Drawing.Point(91, 4);
            this.b4.Name = "b4";
            this.b4.Size = new System.Drawing.Size(23, 23);
            this.b4.TabIndex = 3;
            this.b4.UseVisualStyleBackColor = true;
            this.b4.Click += new System.EventHandler(this.b4_Click);
            this.b4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.b4_KeyPress);
            // 
            // b3
            // 
            this.b3.Location = new System.Drawing.Point(62, 4);
            this.b3.Name = "b3";
            this.b3.Size = new System.Drawing.Size(23, 23);
            this.b3.TabIndex = 2;
            this.b3.UseVisualStyleBackColor = true;
            this.b3.Click += new System.EventHandler(this.b3_Click);
            this.b3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.b3_KeyPress);
            // 
            // b2
            // 
            this.b2.Location = new System.Drawing.Point(33, 4);
            this.b2.Name = "b2";
            this.b2.Size = new System.Drawing.Size(23, 23);
            this.b2.TabIndex = 1;
            this.b2.UseVisualStyleBackColor = true;
            this.b2.Click += new System.EventHandler(this.b2_Click);
            this.b2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.b2_KeyPress);
            // 
            // b1
            // 
            this.b1.Location = new System.Drawing.Point(4, 4);
            this.b1.Name = "b1";
            this.b1.Size = new System.Drawing.Size(23, 23);
            this.b1.TabIndex = 0;
            this.b1.UseVisualStyleBackColor = true;
            this.b1.Click += new System.EventHandler(this.b1_Click);
            this.b1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.b1_KeyPress);
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(12, 276);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 1;
            this.StartButton.Text = "Начать";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            this.StartButton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StartButton_KeyPress);
            // 
            // MyTimer
            // 
            this.MyTimer.Tick += new System.EventHandler(this.MyTimer_Tick);
            // 
            // OpponentTimer
            // 
            this.OpponentTimer.Tick += new System.EventHandler(this.OpponentTimer_Tick);
            // 
            // TimerSecond
            // 
            this.TimerSecond.Interval = 1000;
            this.TimerSecond.Tick += new System.EventHandler(this.TimerSecond_Tick);
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(434, 305);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.BackPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Client";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Client_KeyPress);
            this.BackPanel.ResumeLayout(false);
            this.BackPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel BackPanel;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button b14;
        private System.Windows.Forms.Button b13;
        private System.Windows.Forms.Button b12;
        private System.Windows.Forms.Button b11;
        private System.Windows.Forms.Button b10;
        private System.Windows.Forms.Button b9;
        private System.Windows.Forms.Button b8;
        private System.Windows.Forms.Button b7;
        private System.Windows.Forms.Button b6;
        private System.Windows.Forms.Button b5;
        private System.Windows.Forms.Button b4;
        private System.Windows.Forms.Button b3;
        private System.Windows.Forms.Button b2;
        private System.Windows.Forms.Button b1;
        private System.Windows.Forms.Label CurrentWord;
        private System.Windows.Forms.RichTextBox Words;
        private System.Windows.Forms.ProgressBar TimeProgress;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Timer MyTimer;
        private System.Windows.Forms.Timer OpponentTimer;
        private System.Windows.Forms.Timer TimerSecond;
        private System.Windows.Forms.Label Turn;
    }
}

