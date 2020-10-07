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
    public partial class Form2 : Form
    {

        C_DataBase objDB = new C_DataBase();
        C_Rodovia objRod = new C_Rodovia();

        int nrod = 0;

        Boolean zera = false;



        public Form2()
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

        private void Form2_Load(object sender, EventArgs e)
        {

            //objDB.ConectaBanco();

            objDB.GetRodovias(ref nrod, objRod);
            PopulaComboRodovias(nrod, objRod);


        }




        // *****************************************************************
        private void PopulaComboRodovias(int nRod, C_Rodovia objRod)
        {
            cmbRod.Items.Clear();
            int ind1 = 0;
            for (ind1=1; ind1<=nRod;ind1++) { cmbRod.Items.Add(objRod.Rodovia[ind1]); }
        }
        
        //******************************************************************



        private void btCadastrar_Click(object sender, EventArgs e)
        {

        }

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
            if ( (!char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)13) )
            {
                e.Handled = true;
                TxtBxKmEdit.Text = "";
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
            this.TxtBxKmEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckKeys);
        }

        private void TxtBxKmEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }

    
}
