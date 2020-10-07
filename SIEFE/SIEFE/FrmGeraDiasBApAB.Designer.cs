namespace SIEFE
{
    partial class FrmGeraDiasBApAB
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnAlteraDiaBase = new System.Windows.Forms.Button();
            this.btnGeraS = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btnGeraS);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(269, 215);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.comboBox2);
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Controls.Add(this.btnAlteraDiaBase);
            this.panel2.Location = new System.Drawing.Point(16, 80);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(234, 109);
            this.panel2.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(141, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "Dia destino:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Dia origem:";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(144, 64);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(61, 24);
            this.comboBox2.TabIndex = 10;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(16, 64);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(61, 24);
            this.comboBox1.TabIndex = 9;
            // 
            // btnAlteraDiaBase
            // 
            this.btnAlteraDiaBase.Location = new System.Drawing.Point(52, 18);
            this.btnAlteraDiaBase.Name = "btnAlteraDiaBase";
            this.btnAlteraDiaBase.Size = new System.Drawing.Size(123, 23);
            this.btnAlteraDiaBase.TabIndex = 8;
            this.btnAlteraDiaBase.Text = "AlteraDiaBase";
            this.btnAlteraDiaBase.UseVisualStyleBackColor = true;
            // 
            // btnGeraS
            // 
            this.btnGeraS.Location = new System.Drawing.Point(75, 12);
            this.btnGeraS.Name = "btnGeraS";
            this.btnGeraS.Size = new System.Drawing.Size(116, 47);
            this.btnGeraS.TabIndex = 4;
            this.btnGeraS.Text = "Gerar semana";
            this.btnGeraS.UseVisualStyleBackColor = true;
            this.btnGeraS.Click += new System.EventHandler(this.btnGeraS_Click);
            // 
            // FrmGeraDiasBApAB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 239);
            this.Controls.Add(this.panel1);
            this.Name = "FrmGeraDiasBApAB";
            this.Text = "FrmGeraDiasBApAB";
            this.Load += new System.EventHandler(this.FrmGeraDiasBApAB_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnAlteraDiaBase;
        private System.Windows.Forms.Button btnGeraS;
    }
}