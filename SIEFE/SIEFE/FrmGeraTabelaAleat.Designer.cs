namespace SIEFE
{
    partial class FrmGeraTabelaAleat
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbRod = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtkm = new System.Windows.Forms.TextBox();
            this.btnGerar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Rodovia:";
            // 
            // cmbRod
            // 
            this.cmbRod.FormattingEnabled = true;
            this.cmbRod.Location = new System.Drawing.Point(78, 12);
            this.cmbRod.Name = "cmbRod";
            this.cmbRod.Size = new System.Drawing.Size(86, 24);
            this.cmbRod.TabIndex = 1;
            this.cmbRod.SelectedIndexChanged += new System.EventHandler(this.cmbRod_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(204, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Km:";
            // 
            // txtkm
            // 
            this.txtkm.Location = new System.Drawing.Point(274, 9);
            this.txtkm.Name = "txtkm";
            this.txtkm.Size = new System.Drawing.Size(67, 22);
            this.txtkm.TabIndex = 3;
            this.txtkm.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btnGerar
            // 
            this.btnGerar.Enabled = false;
            this.btnGerar.Location = new System.Drawing.Point(131, 54);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(84, 26);
            this.btnGerar.TabIndex = 4;
            this.btnGerar.Text = "Gerar";
            this.btnGerar.UseVisualStyleBackColor = true;
            this.btnGerar.Click += new System.EventHandler(this.btnGerar_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Location = new System.Drawing.Point(-3, 86);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(344, 75);
            this.panel1.TabIndex = 5;
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(3, 9);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(329, 57);
            this.textBox2.TabIndex = 6;
            this.textBox2.Text = "Gera indices randomicos para a tabela tperct1. Essa tabela auxilia na geração de " +
    "dados randomicos de contagens";
            // 
            // FrmGeraTabelaAleat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 168);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnGerar);
            this.Controls.Add(this.txtkm);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbRod);
            this.Controls.Add(this.label1);
            this.Name = "FrmGeraTabelaAleat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmGeraTabelaAleat";
            this.Load += new System.EventHandler(this.FrmGeraTabelaAleat_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbRod;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtkm;
        private System.Windows.Forms.Button btnGerar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox2;
    }
}