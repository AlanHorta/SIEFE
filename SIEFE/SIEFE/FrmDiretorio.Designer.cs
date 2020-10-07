namespace SIEFE
{
    partial class FrmDiretorio
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
            this.button1 = new System.Windows.Forms.Button();
            this.btSv = new System.Windows.Forms.Button();
            this.openFileDialogDir = new System.Windows.Forms.OpenFileDialog();
            this.fdBwrDiaDir = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDF = new System.Windows.Forms.Label();
            this.dTPF = new System.Windows.Forms.DateTimePicker();
            this.lblDI = new System.Windows.Forms.Label();
            this.dTPI = new System.Windows.Forms.DateTimePicker();
            this.lblMunB = new System.Windows.Forms.Label();
            this.lblMunA = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbqtdCq = new System.Windows.Forms.ComboBox();
            this.cmbQtdClass = new System.Windows.Forms.ComboBox();
            this.cmbNFxSent = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rdSBA = new System.Windows.Forms.RadioButton();
            this.rdSAB = new System.Windows.Forms.RadioButton();
            this.rdAmbSent = new System.Windows.Forms.RadioButton();
            this.chKPS = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMun = new System.Windows.Forms.TextBox();
            this.cbNFx = new System.Windows.Forms.ComboBox();
            this.lblMun = new System.Windows.Forms.Label();
            this.lbloKm = new System.Windows.Forms.Label();
            this.lblaRod = new System.Windows.Forms.Label();
            this.lblkm = new System.Windows.Forms.Label();
            this.lblRod = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(88, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(169, 48);
            this.button1.TabIndex = 0;
            this.button1.Text = "Configura Diretório/Configurações";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btSv
            // 
            this.btSv.Location = new System.Drawing.Point(103, 415);
            this.btSv.Name = "btSv";
            this.btSv.Size = new System.Drawing.Size(142, 47);
            this.btSv.TabIndex = 1;
            this.btSv.Text = "Salva Caminho e Configurações";
            this.btSv.UseVisualStyleBackColor = true;
            this.btSv.Click += new System.EventHandler(this.btSv_Click);
            // 
            // openFileDialogDir
            // 
            this.openFileDialogDir.FileName = "openFileDialog";
            this.openFileDialogDir.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogDir_FileOk);
            // 
            // fdBwrDiaDir
            // 
            this.fdBwrDiaDir.HelpRequest += new System.EventHandler(this.fdBwrDiaDir_HelpRequest);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblDF);
            this.panel1.Controls.Add(this.dTPF);
            this.panel1.Controls.Add(this.lblDI);
            this.panel1.Controls.Add(this.dTPI);
            this.panel1.Controls.Add(this.lblMunB);
            this.panel1.Controls.Add(this.lblMunA);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cmbqtdCq);
            this.panel1.Controls.Add(this.btSv);
            this.panel1.Controls.Add(this.cmbQtdClass);
            this.panel1.Controls.Add(this.cmbNFxSent);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.chKPS);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtMun);
            this.panel1.Controls.Add(this.cbNFx);
            this.panel1.Controls.Add(this.lblMun);
            this.panel1.Location = new System.Drawing.Point(2, 112);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(333, 473);
            this.panel1.TabIndex = 2;
            // 
            // lblDF
            // 
            this.lblDF.AutoSize = true;
            this.lblDF.Location = new System.Drawing.Point(14, 381);
            this.lblDF.Name = "lblDF";
            this.lblDF.Size = new System.Drawing.Size(68, 17);
            this.lblDF.TabIndex = 26;
            this.lblDF.Text = "Data Fim:";
            // 
            // dTPF
            // 
            this.dTPF.Location = new System.Drawing.Point(98, 379);
            this.dTPF.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dTPF.Name = "dTPF";
            this.dTPF.Size = new System.Drawing.Size(229, 22);
            this.dTPF.TabIndex = 25;
            // 
            // lblDI
            // 
            this.lblDI.AutoSize = true;
            this.lblDI.Location = new System.Drawing.Point(14, 351);
            this.lblDI.Name = "lblDI";
            this.lblDI.Size = new System.Drawing.Size(78, 17);
            this.lblDI.TabIndex = 24;
            this.lblDI.Text = "Data Início:";
            // 
            // dTPI
            // 
            this.dTPI.Location = new System.Drawing.Point(98, 351);
            this.dTPI.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dTPI.Name = "dTPI";
            this.dTPI.Size = new System.Drawing.Size(229, 22);
            this.dTPI.TabIndex = 23;
            // 
            // lblMunB
            // 
            this.lblMunB.AutoSize = true;
            this.lblMunB.Location = new System.Drawing.Point(103, 64);
            this.lblMunB.Name = "lblMunB";
            this.lblMunB.Size = new System.Drawing.Size(58, 17);
            this.lblMunB.TabIndex = 22;
            this.lblMunB.Text = "lblMunB";
            // 
            // lblMunA
            // 
            this.lblMunA.AutoSize = true;
            this.lblMunA.Location = new System.Drawing.Point(103, 36);
            this.lblMunA.Name = "lblMunA";
            this.lblMunA.Size = new System.Drawing.Size(58, 17);
            this.lblMunA.TabIndex = 21;
            this.lblMunA.Text = "lblMunA";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 17);
            this.label6.TabIndex = 20;
            this.label6.Text = "Municipio B:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 17);
            this.label5.TabIndex = 18;
            this.label5.Text = "Municipio A:";
            // 
            // cmbqtdCq
            // 
            this.cmbqtdCq.FormattingEnabled = true;
            this.cmbqtdCq.Location = new System.Drawing.Point(159, 316);
            this.cmbqtdCq.Name = "cmbqtdCq";
            this.cmbqtdCq.Size = new System.Drawing.Size(66, 24);
            this.cmbqtdCq.TabIndex = 17;
            // 
            // cmbQtdClass
            // 
            this.cmbQtdClass.FormattingEnabled = true;
            this.cmbQtdClass.Location = new System.Drawing.Point(159, 280);
            this.cmbQtdClass.Name = "cmbQtdClass";
            this.cmbQtdClass.Size = new System.Drawing.Size(66, 24);
            this.cmbQtdClass.TabIndex = 16;
            // 
            // cmbNFxSent
            // 
            this.cmbNFxSent.FormattingEnabled = true;
            this.cmbNFxSent.Location = new System.Drawing.Point(159, 245);
            this.cmbNFxSent.Name = "cmbNFxSent";
            this.cmbNFxSent.Size = new System.Drawing.Size(66, 24);
            this.cmbNFxSent.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rdSBA);
            this.panel2.Controls.Add(this.rdSAB);
            this.panel2.Controls.Add(this.rdAmbSent);
            this.panel2.Location = new System.Drawing.Point(13, 93);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(271, 70);
            this.panel2.TabIndex = 14;
            // 
            // rdSBA
            // 
            this.rdSBA.AutoSize = true;
            this.rdSBA.Location = new System.Drawing.Point(146, 36);
            this.rdSBA.Name = "rdSBA";
            this.rdSBA.Size = new System.Drawing.Size(99, 21);
            this.rdSBA.TabIndex = 2;
            this.rdSBA.TabStop = true;
            this.rdSBA.Text = "Sentido BA";
            this.rdSBA.UseVisualStyleBackColor = true;
            // 
            // rdSAB
            // 
            this.rdSAB.AutoSize = true;
            this.rdSAB.Location = new System.Drawing.Point(16, 36);
            this.rdSAB.Name = "rdSAB";
            this.rdSAB.Size = new System.Drawing.Size(99, 21);
            this.rdSAB.TabIndex = 1;
            this.rdSAB.TabStop = true;
            this.rdSAB.Text = "Sentido AB";
            this.rdSAB.UseVisualStyleBackColor = true;
            // 
            // rdAmbSent
            // 
            this.rdAmbSent.AutoSize = true;
            this.rdAmbSent.Location = new System.Drawing.Point(60, 9);
            this.rdAmbSent.Name = "rdAmbSent";
            this.rdAmbSent.Size = new System.Drawing.Size(131, 21);
            this.rdAmbSent.TabIndex = 0;
            this.rdAmbSent.TabStop = true;
            this.rdAmbSent.Text = "Ambos Sentidos";
            this.rdAmbSent.UseVisualStyleBackColor = true;
            // 
            // chKPS
            // 
            this.chKPS.AutoSize = true;
            this.chKPS.Location = new System.Drawing.Point(13, 176);
            this.chKPS.Name = "chKPS";
            this.chKPS.Size = new System.Drawing.Size(114, 21);
            this.chKPS.TabIndex = 12;
            this.chKPS.Text = "Pista Simples";
            this.chKPS.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 316);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Qtd. Croquis:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 280);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "Qtd. Class:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 245);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "N. Faixas p/ sentido:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 209);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "N. Faixas:";
            // 
            // txtMun
            // 
            this.txtMun.Location = new System.Drawing.Point(103, 8);
            this.txtMun.Name = "txtMun";
            this.txtMun.Size = new System.Drawing.Size(181, 22);
            this.txtMun.TabIndex = 7;
            // 
            // cbNFx
            // 
            this.cbNFx.FormattingEnabled = true;
            this.cbNFx.Location = new System.Drawing.Point(159, 208);
            this.cbNFx.Name = "cbNFx";
            this.cbNFx.Size = new System.Drawing.Size(66, 24);
            this.cbNFx.TabIndex = 6;
            // 
            // lblMun
            // 
            this.lblMun.AutoSize = true;
            this.lblMun.Location = new System.Drawing.Point(12, 8);
            this.lblMun.Name = "lblMun";
            this.lblMun.Size = new System.Drawing.Size(71, 17);
            this.lblMun.TabIndex = 0;
            this.lblMun.Text = "Municipio:";
            // 
            // lbloKm
            // 
            this.lbloKm.AutoSize = true;
            this.lbloKm.Location = new System.Drawing.Point(103, 89);
            this.lbloKm.Name = "lbloKm";
            this.lbloKm.Size = new System.Drawing.Size(50, 17);
            this.lbloKm.TabIndex = 26;
            this.lbloKm.Text = "lbloKm";
            // 
            // lblaRod
            // 
            this.lblaRod.AutoSize = true;
            this.lblaRod.Location = new System.Drawing.Point(103, 61);
            this.lblaRod.Name = "lblaRod";
            this.lblaRod.Size = new System.Drawing.Size(56, 17);
            this.lblaRod.TabIndex = 25;
            this.lblaRod.Text = "lblaRod";
            // 
            // lblkm
            // 
            this.lblkm.AutoSize = true;
            this.lblkm.Location = new System.Drawing.Point(12, 89);
            this.lblkm.Name = "lblkm";
            this.lblkm.Size = new System.Drawing.Size(36, 17);
            this.lblkm.TabIndex = 24;
            this.lblkm.Text = "Km: ";
            // 
            // lblRod
            // 
            this.lblRod.AutoSize = true;
            this.lblRod.Location = new System.Drawing.Point(12, 61);
            this.lblRod.Name = "lblRod";
            this.lblRod.Size = new System.Drawing.Size(64, 17);
            this.lblRod.TabIndex = 23;
            this.lblRod.Text = "Rodovia:";
            // 
            // FrmDiretorio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 597);
            this.Controls.Add(this.lbloKm);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblaRod);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblkm);
            this.Controls.Add(this.lblRod);
            this.Name = "FrmDiretorio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Diretório de trabalho";
            this.Load += new System.EventHandler(this.FrmDiretorio_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btSv;
        private System.Windows.Forms.OpenFileDialog openFileDialogDir;
        private System.Windows.Forms.FolderBrowserDialog fdBwrDiaDir;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbqtdCq;
        private System.Windows.Forms.ComboBox cmbQtdClass;
        private System.Windows.Forms.ComboBox cmbNFxSent;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rdSBA;
        private System.Windows.Forms.RadioButton rdSAB;
        private System.Windows.Forms.RadioButton rdAmbSent;
        private System.Windows.Forms.CheckBox chKPS;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMun;
        private System.Windows.Forms.ComboBox cbNFx;
        private System.Windows.Forms.Label lblMun;
        private System.Windows.Forms.Label lblMunB;
        private System.Windows.Forms.Label lblMunA;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbloKm;
        private System.Windows.Forms.Label lblaRod;
        private System.Windows.Forms.Label lblkm;
        private System.Windows.Forms.Label lblRod;
        private System.Windows.Forms.DateTimePicker dTPI;
        private System.Windows.Forms.Label lblDF;
        private System.Windows.Forms.DateTimePicker dTPF;
        private System.Windows.Forms.Label lblDI;
    }
}