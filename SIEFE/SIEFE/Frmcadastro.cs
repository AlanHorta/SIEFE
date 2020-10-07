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
    public partial class Frmcadastro : Form
    {

        C_DataBase objDB = new C_DataBase();
        C_Rodovia objRod = new C_Rodovia();

        int nrod = 0;
        int ntipo= 0;

        Boolean zera = false;

        C_pontofe objpfe = new C_pontofe();
        C_tipoequip objtipoE = new C_tipoequip();

        Uteis cUt = new Uteis();
        

        string[] TipoEq = new string[11];

        public Frmcadastro()
        {
            InitializeComponent();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void Frmcadastro_Load(object sender, EventArgs e)
        {

            //objDB.ConectaBanco();

            objDB.GetRodovias(ref nrod, objRod);
            PopulaComboRodovias(nrod, objRod);

            objDB.GetTiposEqp(ref ntipo, objtipoE);
            PopulaTipos(ntipo, objtipoE);

        }




        // *****************************************************************
        private void PopulaComboRodovias(int nRod, C_Rodovia objRod)
        {
            cmbRod.Items.Clear();
            int ind1 = 0;
            for (ind1 = 1; ind1 <= nRod; ind1++) { cmbRod.Items.Add(objRod.Rodovia[ind1]); }
        }

        //******************************************************************



        private void btCadastrar_Click(object sender, EventArgs e)
        {
            CarregaPFE(objpfe);
            if (VeSefeRegistroExiste(objpfe, "ponto_fe"))
            { MessageBox.Show("registro já cadastrado em ponto_fe!"); }
            else
            {
                if (VeSefeRegistroExiste(objpfe, "ponto_featual"))
                { MessageBox.Show("registro já cadastrado em ponto_featual!"); }
                else
                { objDB.GravapFE(objpfe, "ponto_fe");
                    objDB.GravapFE(objpfe, "ponto_featual");
                    MessageBox.Show("Registro gravado com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }


        //******************************************************************

        private void cmbRod_SelectedIndexChanged(object sender, EventArgs e)
        {
            string MunA = "", MunB = "";
            objDB.GetMunicipios(cmbRod.Text, ref MunA, ref MunB);
            lblMunA.Text = MunA;
            lblMunB.Text = MunB;

            cmbSent.Items.Clear();
            cmbSent.Items.Add("Ambos Sentidos");
            cmbSent.Items.Add(MunA);
            cmbSent.Items.Add(MunB);
        }





        private void CheckKeys(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (e.KeyChar != '.') && (e.KeyChar != (char)13) && (e.KeyChar != (char)9) && (e.KeyChar != (char)8) && (e.KeyChar != (char)95))
            {
                e.Handled = true;
                
                //LimpaCxTxt();
            }
        }

        private void CheckKeys2(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (e.KeyChar != '.') && (e.KeyChar != (char)13) && (e.KeyChar != (char)9) && (e.KeyChar != (char)8) && (e.KeyChar != (char)95))
            {
                e.Handled = true;
                
               
                //LimpaCxTxt();
            }
        }


        private void CheckKeysInt(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)8) && (e.KeyChar != (char)13) && (e.KeyChar != (char)9) && (e.KeyChar != (char)95))
            {
                e.Handled = true;
                ////txtBKmR.Text = "";
                
                //LimpaCxTxt();
            }
        }



        private void LimpaCxTxt()
        {

        }

        private void cmbSent_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbSent_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void TxtBxKmEdit_TextChanged(object sender, EventArgs e)
        {
            // this.TxtBxKmEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckKeys);
        }

        private void TxtBxKmEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            // if (!char.IsNumber(e.KeyChar) && (e.KeyChar != (char)190))
            //////////if (!char.IsNumber(e.KeyChar))
            //////////{
            //////////    e.Handled = true;
            //////////    TxtBxKmEdit.Text = "";
            //////////}
        }

        private void txtBKmR_TextChanged(object sender, EventArgs e)
        {

        }




        private void CarregaPFE(C_pontofe objpFE)
        {


            try
            {
                objpFE.Rodovia = cmbRod.Text;
                //////////TxtBxKmEdit.Text = "2,5";
                objpFE.kmEdital = ((TxtBxKmEdit.Text));
                objpFE.MunSen = cmbSent.Text;
                objpFE.kmReal = ((txtBKmR.Text));
                objpFE.Localidade = txtBLoc.Text;
                objpFE.Municipio = txtBMun.Text;
                objpFE.QtdFx = Int32.Parse(txtBQtd.Text);
                objpFE.MunA = lblMunA.Text;
                objpFE.MunB = lblMunB.Text;
                objpFE.VelFisc = Int32.Parse(txtBVf.Text);
                objpFE.Lat = txtBLat.Text;
                objpFE.Longit = txtBLong.Text;
                objpFE.VMD = Int32.Parse(txtBVmd.Text);
                objpFE.Vel85p = Int32.Parse(txtBV85.Text);
                objpFE.Tipo = cmbTipo.Text;
                objpFE.Lat2 = txtBLat2.Text;
                objpFE.Longit2 = txtBLong2.Text;
                objpFE.Vel85pSB = Int32.Parse(txtB85_2.Text.ToString());
                objpFE.VmdB = Int32.Parse(txtBVMD2.Text.ToString());
            }
            catch
            {
                DialogResult result;
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                result=MessageBox.Show("Dados incompletos ou errados!", "Verifique" , buttons);
               

            }
            
        }






        private Boolean VeSefeRegistroExiste(C_pontofe objpFE, string aTab)
        {
            // ve se o registro de ponto de fiscalização eletrônica existe
            try
            {
                if (objDB.TemRegistro(objpFE, aTab))
                { return true; }
                else
                { return false; }
            }

            catch
            { return false; }
        }


        private void PopulaTipos(int ntipo, C_tipoequip TipoEq )
        {
            cmbTipo.Items.Clear();
            int ind1 = 0;
            for (ind1 = 1; ind1 <= ntipo; ind1++) { cmbTipo.Items.Add(TipoEq.tipo[ind1]); }
            
        }

        private void txtBQtd_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtZera_Click(object sender, EventArgs e)
        {
            txtBLat2.Text = "zero";
            txtBLong2.Text = "zero";
        }



        private void BtSair_Click(object sender, EventArgs e)
        {
            //this.Close();
            
            //var fm1 = new Frmcadastro();
            //ClearTextBoxes(fm1);

            if (!VeSefeRegistroExiste(objpfe, "ponto_featual"))
            {
                if (DialogResult.Yes == MessageBox.Show("Sair sem salvar?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    this.Dispose();
                    //Sua rotina de exclusão
                    //Confirmando exclusão para o usuário
                   

                }
            }
            else
            {
                this.Dispose();
            }


        }

        public void ClearTextBoxes(Frmcadastro form)
        {
            //foreach (Control control in form.Controls)
            //{
            //    Control ct1;

            //    control.Text = "";
            //    ct1 = control.GetType();

            //    //if (control.GetType() == typeof(TextBox))
            //    //{
            //    //                        control.Text = "";
            //    //}

            //}
        }

        private void TxtBVf_TextChanged(object sender, EventArgs e)
        {

        }
    }



}
