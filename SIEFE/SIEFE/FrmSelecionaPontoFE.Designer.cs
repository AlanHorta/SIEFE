namespace SIEFE
{
    partial class FrmSelecionaPontoFE
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
            this.lblFeAt = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.cmbRod = new System.Windows.Forms.ComboBox();
            this.btSelecionar = new System.Windows.Forms.Button();
            this.btSair = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblFeAt);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.listBox1);
            this.panel1.Controls.Add(this.cmbRod);
            this.panel1.Location = new System.Drawing.Point(32, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(265, 426);
            this.panel1.TabIndex = 0;
            // 
            // lblFeAt
            // 
            this.lblFeAt.AutoSize = true;
            this.lblFeAt.Location = new System.Drawing.Point(140, 53);
            this.lblFeAt.Name = "lblFeAt";
            this.lblFeAt.Size = new System.Drawing.Size(102, 17);
            this.lblFeAt.TabIndex = 4;
            this.lblFeAt.Text = "Ponto FE Atual";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Ponto Selecionado:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Rodovia:";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(68, 71);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(118, 340);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // cmbRod
            // 
            this.cmbRod.FormattingEnabled = true;
            this.cmbRod.Location = new System.Drawing.Point(100, 18);
            this.cmbRod.Name = "cmbRod";
            this.cmbRod.Size = new System.Drawing.Size(97, 24);
            this.cmbRod.TabIndex = 0;
            this.cmbRod.SelectedIndexChanged += new System.EventHandler(this.cmbRod_SelectedIndexChanged);
            // 
            // btSelecionar
            // 
            this.btSelecionar.Location = new System.Drawing.Point(209, 448);
            this.btSelecionar.Name = "btSelecionar";
            this.btSelecionar.Size = new System.Drawing.Size(88, 23);
            this.btSelecionar.TabIndex = 1;
            this.btSelecionar.Text = "Selecionar";
            this.btSelecionar.UseVisualStyleBackColor = true;
            this.btSelecionar.Click += new System.EventHandler(this.btSelecionar_Click);
            // 
            // btSair
            // 
            this.btSair.Location = new System.Drawing.Point(32, 448);
            this.btSair.Name = "btSair";
            this.btSair.Size = new System.Drawing.Size(75, 23);
            this.btSair.TabIndex = 2;
            this.btSair.Text = "Fechar";
            this.btSair.UseVisualStyleBackColor = true;
            this.btSair.Click += new System.EventHandler(this.btSair_Click);
            // 
            // FrmSelecionaPontoFE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 483);
            this.Controls.Add(this.btSair);
            this.Controls.Add(this.btSelecionar);
            this.Controls.Add(this.panel1);
            this.Name = "FrmSelecionaPontoFE";
            this.Text = "FrmSelecionaPontoFE";
            this.Load += new System.EventHandler(this.FrmSelecionaPontoFE_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbRod;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblFeAt;
        private System.Windows.Forms.Button btSelecionar;
        private System.Windows.Forms.Button btSair;
    }
}