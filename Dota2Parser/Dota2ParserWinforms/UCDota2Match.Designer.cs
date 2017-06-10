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
            this.flowLayoutPanelDire = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMatchId = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSequenceNumber = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblLobbyType = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // flowLayoutPanelRadiant
            // 
            this.flowLayoutPanelRadiant.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.flowLayoutPanelRadiant.Location = new System.Drawing.Point(0, 98);
            this.flowLayoutPanelRadiant.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flowLayoutPanelRadiant.Name = "flowLayoutPanelRadiant";
            this.flowLayoutPanelRadiant.Size = new System.Drawing.Size(654, 214);
            this.flowLayoutPanelRadiant.TabIndex = 10;
            // 
            // flowLayoutPanelDire
            // 
            this.flowLayoutPanelDire.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.flowLayoutPanelDire.Location = new System.Drawing.Point(0, 321);
            this.flowLayoutPanelDire.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flowLayoutPanelDire.Name = "flowLayoutPanelDire";
            this.flowLayoutPanelDire.Size = new System.Drawing.Size(654, 214);
            this.flowLayoutPanelDire.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "Match Id:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMatchId
            // 
            this.lblMatchId.AutoSize = true;
            this.lblMatchId.Location = new System.Drawing.Point(132, 8);
            this.lblMatchId.Name = "lblMatchId";
            this.lblMatchId.Size = new System.Drawing.Size(58, 15);
            this.lblMatchId.TabIndex = 13;
            this.lblMatchId.Text = "Match Id:";
            this.lblMatchId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 15);
            this.label2.TabIndex = 15;
            this.label2.Text = "Sequence number:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSequenceNumber
            // 
            this.lblSequenceNumber.AutoSize = true;
            this.lblSequenceNumber.Location = new System.Drawing.Point(132, 38);
            this.lblSequenceNumber.Name = "lblSequenceNumber";
            this.lblSequenceNumber.Size = new System.Drawing.Size(58, 15);
            this.lblSequenceNumber.TabIndex = 14;
            this.lblSequenceNumber.Text = "Match Id:";
            this.lblSequenceNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(61, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 15);
            this.label3.TabIndex = 17;
            this.label3.Text = "StartTime:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStartTime
            // 
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.Location = new System.Drawing.Point(132, 69);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(58, 15);
            this.lblStartTime.TabIndex = 16;
            this.lblStartTime.Text = "Match Id:";
            this.lblStartTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(348, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 15);
            this.label4.TabIndex = 18;
            this.label4.Text = "Lobby type:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLobbyType
            // 
            this.lblLobbyType.AutoSize = true;
            this.lblLobbyType.Location = new System.Drawing.Point(428, 8);
            this.lblLobbyType.Name = "lblLobbyType";
            this.lblLobbyType.Size = new System.Drawing.Size(58, 15);
            this.lblLobbyType.TabIndex = 19;
            this.lblLobbyType.Text = "Match Id:";
            this.lblLobbyType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UCDota2Match
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblLobbyType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblStartTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblSequenceNumber);
            this.Controls.Add(this.lblMatchId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowLayoutPanelDire);
            this.Controls.Add(this.flowLayoutPanelRadiant);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UCDota2Match";
            this.Size = new System.Drawing.Size(654, 534);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelRadiant;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDire;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMatchId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSequenceNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblLobbyType;
    }
}
