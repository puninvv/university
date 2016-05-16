namespace FormClient
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TextTo = new System.Windows.Forms.TextBox();
            this.TextFrom = new System.Windows.Forms.TextBox();
            this.TextFunction = new System.Windows.Forms.TextBox();
            this.TextVariable = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TextError = new System.Windows.Forms.TextBox();
            this.RadioRectangle = new System.Windows.Forms.RadioButton();
            this.RadioTrapeze = new System.Windows.Forms.RadioButton();
            this.RadioSimpsons = new System.Windows.Forms.RadioButton();
            this.Integrate = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.AllMethods = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "1) Введите функцию:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(303, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "2) Введите допустимую погрешность:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 231);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(290, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "3) Выберите метод интегрирования:";
            // 
            // TextTo
            // 
            this.TextTo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextTo.Location = new System.Drawing.Point(12, 38);
            this.TextTo.Name = "TextTo";
            this.TextTo.Size = new System.Drawing.Size(107, 26);
            this.TextTo.TabIndex = 3;
            this.TextTo.Text = "1,57079632";
            this.TextTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TextFrom
            // 
            this.TextFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextFrom.Location = new System.Drawing.Point(12, 148);
            this.TextFrom.Name = "TextFrom";
            this.TextFrom.Size = new System.Drawing.Size(107, 26);
            this.TextFrom.TabIndex = 4;
            this.TextFrom.Text = "0";
            this.TextFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TextFunction
            // 
            this.TextFunction.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextFunction.Location = new System.Drawing.Point(82, 94);
            this.TextFunction.Name = "TextFunction";
            this.TextFunction.Size = new System.Drawing.Size(335, 26);
            this.TextFunction.TabIndex = 5;
            this.TextFunction.Text = "ln((5^2)*(sin(x))^2+(6^2)*(cos(x))^2)";
            this.TextFunction.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TextVariable
            // 
            this.TextVariable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextVariable.Location = new System.Drawing.Point(450, 94);
            this.TextVariable.Name = "TextVariable";
            this.TextVariable.Size = new System.Drawing.Size(39, 26);
            this.TextVariable.TabIndex = 6;
            this.TextVariable.Text = "x";
            this.TextVariable.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(426, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 19);
            this.label4.TabIndex = 7;
            this.label4.Text = "d";
            // 
            // TextError
            // 
            this.TextError.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextError.Location = new System.Drawing.Point(372, 185);
            this.TextError.Name = "TextError";
            this.TextError.Size = new System.Drawing.Size(107, 26);
            this.TextError.TabIndex = 8;
            this.TextError.Text = "0,001";
            this.TextError.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // RadioRectangle
            // 
            this.RadioRectangle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RadioRectangle.AutoSize = true;
            this.RadioRectangle.Checked = true;
            this.RadioRectangle.Location = new System.Drawing.Point(32, 254);
            this.RadioRectangle.Name = "RadioRectangle";
            this.RadioRectangle.Size = new System.Drawing.Size(156, 23);
            this.RadioRectangle.TabIndex = 9;
            this.RadioRectangle.TabStop = true;
            this.RadioRectangle.Text = "Прямоугольники";
            this.RadioRectangle.UseVisualStyleBackColor = true;
            // 
            // RadioTrapeze
            // 
            this.RadioTrapeze.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RadioTrapeze.AutoSize = true;
            this.RadioTrapeze.Location = new System.Drawing.Point(32, 283);
            this.RadioTrapeze.Name = "RadioTrapeze";
            this.RadioTrapeze.Size = new System.Drawing.Size(100, 23);
            this.RadioTrapeze.TabIndex = 10;
            this.RadioTrapeze.TabStop = true;
            this.RadioTrapeze.Text = "Трапеции";
            this.RadioTrapeze.UseVisualStyleBackColor = true;
            // 
            // RadioSimpsons
            // 
            this.RadioSimpsons.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RadioSimpsons.AutoSize = true;
            this.RadioSimpsons.Location = new System.Drawing.Point(32, 312);
            this.RadioSimpsons.Name = "RadioSimpsons";
            this.RadioSimpsons.Size = new System.Drawing.Size(93, 23);
            this.RadioSimpsons.TabIndex = 11;
            this.RadioSimpsons.TabStop = true;
            this.RadioSimpsons.Text = "Симпсон";
            this.RadioSimpsons.UseVisualStyleBackColor = true;
            // 
            // Integrate
            // 
            this.Integrate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Integrate.Location = new System.Drawing.Point(137, 375);
            this.Integrate.Name = "Integrate";
            this.Integrate.Size = new System.Drawing.Size(216, 79);
            this.Integrate.TabIndex = 12;
            this.Integrate.Text = "Интегрировать";
            this.Integrate.UseVisualStyleBackColor = true;
            this.Integrate.Click += new System.EventHandler(this.Integrate_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FormClient.Properties.Resources.int_pic;
            this.pictureBox1.Location = new System.Drawing.Point(32, 70);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(44, 72);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // AllMethods
            // 
            this.AllMethods.AutoSize = true;
            this.AllMethods.Location = new System.Drawing.Point(32, 341);
            this.AllMethods.Name = "AllMethods";
            this.AllMethods.Size = new System.Drawing.Size(164, 23);
            this.AllMethods.TabIndex = 14;
            this.AllMethods.TabStop = true;
            this.AllMethods.Text = "Все методы сразу";
            this.AllMethods.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(501, 466);
            this.Controls.Add(this.AllMethods);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Integrate);
            this.Controls.Add(this.RadioSimpsons);
            this.Controls.Add(this.RadioTrapeze);
            this.Controls.Add(this.RadioRectangle);
            this.Controls.Add(this.TextError);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TextVariable);
            this.Controls.Add(this.TextFunction);
            this.Controls.Add(this.TextFrom);
            this.Controls.Add(this.TextTo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Интегрирование";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TextTo;
        private System.Windows.Forms.TextBox TextFrom;
        private System.Windows.Forms.TextBox TextFunction;
        private System.Windows.Forms.TextBox TextVariable;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TextError;
        private System.Windows.Forms.RadioButton RadioRectangle;
        private System.Windows.Forms.RadioButton RadioTrapeze;
        private System.Windows.Forms.RadioButton RadioSimpsons;
        private System.Windows.Forms.Button Integrate;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RadioButton AllMethods;
    }
}

