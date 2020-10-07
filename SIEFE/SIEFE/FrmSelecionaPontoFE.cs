using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIEFE
{
    public partial class FrmSelecionaPontoFE : Form
    {

        C_Rodovia objRod = new C_Rodovia();

        C_DataBase objDtb = new C_DataBase();

        C_pontofe objPFe = new C_pontofe();

        C_Geometria objGeom = new C_Geometria();

       // Uteis cUt = new Uteis();

        Form1 frm1 = new Form1();

        int nrod = 0;
        string aRod = "";
        string okm = "";
        public string[] kms = new string[50];

        public FrmSelecionaPontoFE()
        {
            InitializeComponent();
        }


      



        private void PopulaComboRodovias(int nRod, C_Rodovia objRod)
        {
            cmbRod.Items.Clear();
            int ind1 = 0;
            for (ind1 = 1; ind1 <= nRod; ind1++) { cmbRod.Items.Add(objRod.Rodovia[ind1]); }
        }



        public void PopulaComboKms (string aRod)
        {
            int ind1 = 0;
            objDtb.TrazKmsPontosFE(ref nrod, aRod, objRod);
            listBox1.Items.Clear();
            

            for (ind1=1;ind1<=nrod;ind1++) { listBox1.Items.Add("km: " + objRod.kms[ind1]); }
        }




        private void cmbRod_SelectedIndexChanged(object sender, EventArgs e)
        {

            aRod = cmbRod.SelectedItem.ToString();
            PopulaComboKms(aRod);


        }

        private void FrmSelecionaPontoFE_Load(object sender, EventArgs e)
        {
            Form1 objF1 = new Form1();

            try
            {
                // objDtb.ConectaBanco();
                listBox1.SelectionMode = SelectionMode.One;
                objDtb.LeRegistroAtual(ref aRod, ref okm);
                lblFeAt.Text = aRod + " km: " + okm;
                //
                objDtb.GetRodovias(ref nrod, objRod);
                PopulaComboRodovias(nrod, objRod);
                cmbRod.Text = aRod;
                              
                PopulaComboKms(aRod);
                objF1.timer1.Enabled = true;

            }
            catch
            {

            }
        }

        private void btSair_Click(object sender, EventArgs e)
        {
            //Form1 fInicio = new Form1();
            //fInicio.ShowDialog();
            //this.Dispose();
            //frm1.Update();
            frm1.timer1.Enabled = true;
            
            this.Close();


        }

        



        private void btSelecionar_Click(object sender, EventArgs e)
        {
            try
            {

                if (cmbRod.SelectedIndex >= 0)
                {
                    objPFe.Rodovia = (cmbRod.SelectedItem.ToString());
                }
                else
                {
                    objPFe.Rodovia = lblFeAt.Text.Substring(0, 6);
                }

                objPFe.kmReal = (listBox1.SelectedItem.ToString()).Substring(4);
                objDtb.LeRegistroPontoFE(ref objPFe.Rodovia, ref objPFe.kmReal, objPFe);
                objDtb.GravapFE2(objPFe, "ponto_featual");

                //frm1.LeRegistroAtual();
                //frm1.bEDuplo = frm1.VerificaSeÉDuplo();
                //frm1.VeGeometria(objGeom);  // Serve para saber se é um ou mais sentidos
            }
            catch (Exception err)
            {
                int error = 0;
                error = error;     
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{

                if (cmbRod.SelectedIndex >= 0)
                {
                    objPFe.Rodovia = (cmbRod.SelectedItem.ToString());
                }
                else
                {
                    objPFe.Rodovia = lblFeAt.Text.Substring(0, 6);
                }

                objPFe.kmReal = (listBox1.SelectedItem.ToString()).Substring(4);
                objDtb.LeRegistroPontoFE(ref objPFe.Rodovia, ref objPFe.kmReal, objPFe);
                objDtb.GravapFE2(objPFe, "ponto_featual");

            //frm1.ConectaBanco();
            //frm1.ConectaBanco2();
            //    frm1.LeRegistroAtual();
            //    frm1.bEDuplo = frm1.VerificaSeÉDuplo();


            //frm1.VeGeometria(objGeom);
            //this.Close();
            //this.Update();
            //frm1.Update(); 
           
            //frm1.Show();
            //this.Show();

            //}
            //catch (Exception err)
            //{
            //    int error = 0;
            //    error = error;
            //}
        }




    }
}
