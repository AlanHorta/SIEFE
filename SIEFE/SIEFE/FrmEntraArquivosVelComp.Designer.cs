namespace SIEFE
{
    partial class FrmEntraArquivosVelComp
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
            this.btnAcVeComp = new System.Windows.Forms.Button();
            this.btnPSimples = new System.Windows.Forms.Button();
            this.btnLimpaCx = new System.Windows.Forms.Button();
            this.btnCarrega = new System.Windows.Forms.Button();
            this.lstBox1 = new System.Windows.Forms.ListBox();
            this.btnSeleciona = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAcVeComp);
            this.panel1.Controls.Add(this.btnPSimples);
            this.panel1.Controls.Add(this.btnLimpaCx);
            this.panel1.Controls.Add(this.btnCarrega);
            this.panel1.Controls.Add(this.lstBox1);
            this.panel1.Controls.Add(this.btnSeleciona);
            this.panel1.Location = new System.Drawing.Point(22, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(873, 396);
            this.panel1.TabIndex = 0;
            // 
            // btnAcVeComp
            // 
            this.btnAcVeComp.Location = new System.Drawing.Point(32, 199);
            this.btnAcVeComp.Name = "btnAcVeComp";
            this.btnAcVeComp.Size = new System.Drawing.Size(126, 53);
            this.btnAcVeComp.TabIndex = 5;
            this.btnAcVeComp.Text = "AcertaArquivo VelComp";
            this.btnAcVeComp.UseVisualStyleBackColor = true;
            this.btnAcVeComp.Click += new System.EventHandler(this.btnAcVeComp_Click);
            // 
            // btnPSimples
            // 
            this.btnPSimples.Location = new System.Drawing.Point(22, 129);
            this.btnPSimples.Name = "btnPSimples";
            this.btnPSimples.Size = new System.Drawing.Size(136, 51);
            this.btnPSimples.TabIndex = 4;
            this.btnPSimples.Text = "Carrega Arquivos Piata Simples";
            this.btnPSimples.UseVisualStyleBackColor = true;
            this.btnPSimples.Click += new System.EventHandler(this.btnPSimples_Click);
            // 
            // btnLimpaCx
            // 
            this.btnLimpaCx.Location = new System.Drawing.Point(22, 299);
            this.btnLimpaCx.Name = "btnLimpaCx";
            this.btnLimpaCx.Size = new System.Drawing.Size(136, 45);
            this.btnLimpaCx.TabIndex = 3;
            this.btnLimpaCx.Text = "Limpa caixa";
            this.btnLimpaCx.UseVisualStyleBackColor = true;
            this.btnLimpaCx.Click += new System.EventHandler(this.btnLimpaCx_Click);
            // 
            // btnCarrega
            // 
            this.btnCarrega.Location = new System.Drawing.Point(22, 83);
            this.btnCarrega.Name = "btnCarrega";
            this.btnCarrega.Size = new System.Drawing.Size(136, 27);
            this.btnCarrega.TabIndex = 2;
            this.btnCarrega.Text = "Carrega";
            this.btnCarrega.UseVisualStyleBackColor = true;
            this.btnCarrega.Click += new System.EventHandler(this.btnCarrega_Click);
            // 
            // lstBox1
            // 
            this.lstBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstBox1.FormattingEnabled = true;
            this.lstBox1.ItemHeight = 20;
            this.lstBox1.Location = new System.Drawing.Point(207, 20);
            this.lstBox1.Name = "lstBox1";
            this.lstBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstBox1.Size = new System.Drawing.Size(648, 324);
            this.lstBox1.TabIndex = 1;
            this.lstBox1.SelectedIndexChanged += new System.EventHandler(this.lstBox1_SelectedIndexChanged);
            // 
            // btnSeleciona
            // 
            this.btnSeleciona.Location = new System.Drawing.Point(22, 20);
            this.btnSeleciona.Name = "btnSeleciona";
            this.btnSeleciona.Size = new System.Drawing.Size(136, 44);
            this.btnSeleciona.TabIndex = 0;
            this.btnSeleciona.Text = "Seleciona arquivos de entrada";
            this.btnSeleciona.UseVisualStyleBackColor = true;
            this.btnSeleciona.Click += new System.EventHandler(this.btnSeleciona_Click);
            // 
            // FrmEntraArquivosVelComp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(977, 450);
            this.Controls.Add(this.panel1);
            this.Name = "FrmEntraArquivosVelComp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lê arquivos de velocidade e comprimento do 241";
            this.Load += new System.EventHandler(this.FrmEntraArquivosVelComp_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCarrega;
        private System.Windows.Forms.ListBox lstBox1;
        private System.Windows.Forms.Button btnSeleciona;
        private System.Windows.Forms.Button btnLimpaCx;
        private System.Windows.Forms.Button btnPSimples;
        private System.Windows.Forms.Button btnAcVeComp;
    }
}