namespace Dota2ParserWinforms
{
    partial class UCDota2Match
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanelRadiant = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelMatchId = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // flowLayoutPanelRadiant
            // 
            this.flowLayoutPanelRadiant.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.flowLayoutPanelRadiant.Location = new System.Drawing.Point(3, 141);
            this.flowLayoutPanelRadiant.Name = "flowLayoutPanelRadiant";
            this.flowLayoutPanelRadiant.Size = new System.Drawing.Size(530, 185);
            this.flowLayoutPanelRadiant.TabIndex = 10;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 332);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(530, 185);
            this.flowLayoutPanel1.TabIndex = 11;
            // 
            // labelMatchId
            // 
            this.labelMatchId.AutoSize = true;
            this.labelMatchId.Location = new System.Drawing.Point(3, 13);
            this.labelMatchId.Name = "labelMatchId";
            this.labelMatchId.Size = new System.Drawing.Size(52, 13);
            this.labelMatchId.TabIndex = 12;
            this.labelMatchId.Text = "Match Id:";
            // 
            // UCDota2Match
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelMatchId);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.flowLayoutPanelRadiant);
            this.Name = "UCDota2Match";
            this.Size = new System.Drawing.Size(538, 520);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelRadiant;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label labelMatchId;
    }
}
