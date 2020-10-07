using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using MySql.Data.MySqlClient;


namespace SIEFE
{
    public partial class FrmPlacas : Form
    {

        MySqlConnection mysqlCon1;
        MySqlCommand com1;
        MySqlDataReader reader1;

        Uteis cUt = new Uteis();
        C_Placas objplacas = new C_Placas();

        string strConFE = @"Server=localhost;Database=fe;Uid=root;Pwd=obuldog67;";


        public FrmPlacas()
        {
            InitializeComponent();
        }

        private void FrmPlacas_Load(object sender, EventArgs e)
        {
            ConectaBanco();
            CarregaGridPlacas();

            CarregaCombosPlacas();
            cmbSentido.SelectedIndex = 1;

        }


        private void CarregaGridPlacas()
        {
            //DataGridViewTextBoxColumn[] Gcol = new DataGridViewTextBoxColumn[31];
            int ind1 = 1;
            String strCols = "";

            string aRod = "";
            string okm = "";
            string oMunSen = "";
            string ocant = "";


            DtGVPlacas.Rows.Clear();
            DtGVPlacas.ColumnCount = 7;
            DtGVPlacas.Columns[0].Name = "N. placa";
            DtGVPlacas.Columns[0].Width = 50;
            DtGVPlacas.Columns[1].Name = "Placa";
            DtGVPlacas.Columns[1].Width = 350;
            DtGVPlacas.Columns[2].Name = "Distância (m)";
            DtGVPlacas.Columns[2].Width = 60;
            DtGVPlacas.Columns[3].Name = "Sentido";
            DtGVPlacas.Columns[3].Width = 200;
            DtGVPlacas.Columns[4].Name = "Canteiro";
            DtGVPlacas.Columns[4].Width = 100;
            DtGVPlacas.Columns[5].Name = "Status";
            DtGVPlacas.Columns[5].Width = 80;
            DtGVPlacas.Columns[6].Name = "Dist. Antiga";
            DtGVPlacas.Columns[6].Width = 60;

            com1.CommandText = "select Rodovia, kmEdital, MunSen from ponto_featual";
            reader1 = com1.ExecuteReader();
            if (reader1.Read())
            {
                aRod = reader1["Rodovia"].ToString();
                okm = reader1["kmEdital"].ToString();
                oMunSen=(reader1["MunSen"]).ToString();
                
            }
            reader1.Close();


            com1.CommandText= "select * from tplacas where Rodovia = " + "'" + aRod + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(okm.ToString()) +"'";
            com1.CommandText = com1.CommandText + " and MunSen= " + "'" + oMunSen + "'";

            reader1 = com1.ExecuteReader();

            if (reader1.Read())
            {
                for (ind1 = 1; ind1 < 11; ind1++)
                {
                    strCols = ind1.ToString();
                    if (       (Int32.Parse(reader1["dist" + strCols].ToString())>=0) &&   (!(reader1["placa" + strCols].ToString().Equals("0")))   )
                    {
                        string ssssplaca = reader1["placa" + strCols].ToString();
                    objplacas.placa[ind1] = reader1["placa" + strCols].ToString();
                    objplacas.dist[ind1] = Int32.Parse(reader1["dist" + strCols].ToString());
                    objplacas.St[ind1] = reader1["S" + strCols].ToString();
                    objplacas.cant[ind1] = reader1["ct" + strCols].ToString();
                    objplacas.status[ind1]= reader1["stt" + strCols].ToString();
                    objplacas.dta[ind1]= Int32.Parse(reader1["dta" + strCols].ToString());

                        DtGVPlacas.Rows.Add(ind1, objplacas.placa[ind1], objplacas.dist[ind1], objplacas.St[ind1],objplacas.cant[ind1], objplacas.status[ind1], objplacas.dta[ind1]);
                    }
                }
               
            }


            reader1.Close();

            //DataTable tbPlacas =  new DataTable();
            //tbPlacas.Load(reader1);

            //DtGVPlacas.DataSource = tbPlacas;

        }



        // *****************************************************

        private void CarregaCombosPlacas()
        {
            string aRod = "";
          
            // Combo placas
            cmbPlacaFE.Items.Clear();

            com1.CommandText = "select * from ttipo_placas";
            reader1 = com1.ExecuteReader();

            while (reader1.Read())
            {cmbPlacaFE.Items.Add(reader1["desc"]);}

            reader1.Close();


            // Combo distância
            com1.CommandText = "select * from tdistancias";
            reader1 = com1.ExecuteReader();
            cmbDist.Items.Clear();
            cmbDantiga.Items.Clear();
            while (reader1.Read())
            { cmbDist.Items.Add(reader1["distancia"]);
              cmbDantiga.Items.Add(reader1["distancia"]);
            }

            reader1.Close();



            // busca rodovia
            com1.CommandText = "select Rodovia from ponto_featual";
            reader1 = com1.ExecuteReader();
            if (reader1.Read())
            { aRod = reader1["Rodovia"].ToString(); }
            reader1.Close();

            // combo sentido
            com1.CommandText = "select * from trodovias where Rodovia = " + "'" + aRod + "'";
            reader1 = com1.ExecuteReader();
            cmbSentido.Items.Clear();
            if (reader1.Read())
            { 
              cmbSentido.Items.Add(reader1["munA"]);
              cmbSentido.Items.Add(reader1["munB"]);
            }

            reader1.Close();


            // Combo Canteiro
            com1.CommandText = "select * from tcanteiro";
            reader1 = com1.ExecuteReader();
            cmbCanteiro.Items.Clear();

            while (reader1.Read())
            { cmbCanteiro.Items.Add(reader1["tonde"].ToString()); }
            reader1.Close();

            cmbCanteiro.SelectedIndex = 3;        }

        // *********************************************************


        private void ConectaBanco()
        {

            using (mysqlCon1 = new MySqlConnection(strConFE)) ;
            {
                mysqlCon1.Open();
                com1 = mysqlCon1.CreateCommand();

                //MessageBox.Show("Teste conecta e fecha banco OK!");

            }
        }
        // *****************************************************

        private void DesconectaBanco()
        {

            using (mysqlCon1 = new MySqlConnection(strConFE)) ;
            {
                mysqlCon1.Close();

                //MessageBox.Show("Teste conecta e fecha banco OK!");

            }
        }

        private void btIncluir_Click(object sender, EventArgs e)
        {
            int qtdG = 0;
            String ostatus = "";
            int adistAnt = 0;

            if (!BuscaNoGrid(cmbPlacaFE.Text, cmbDist.Text, cmbSentido.Text, cmbCanteiro.Text ))
            {
                qtdG = DtGVPlacas.RowCount;
                // Se não achou a aplaca no grid então inclui

                if (rdinc.Checked) { ostatus = "incluir"; }
                if (rdrem.Checked) { ostatus = "remover"; }
                if (rdmanter.Checked) { ostatus = "manter"; }
                adistAnt = 0;
                if (rdalt.Checked)
                {
                    ostatus = "reposicionar";
                    adistAnt = Int32.Parse(cmbDantiga.Text.ToString());
                }

                

                DtGVPlacas.Rows.Add(qtdG, cmbPlacaFE.Text, cmbDist.Text, cmbSentido.Text, cmbCanteiro.Text, ostatus, adistAnt);

            }
        }
        // *****************************************************


        private Boolean BuscaNoGrid(string aplaca, string adist, string osent, string ocant)
        {
            int qtdpl = 0;
            int ind1 = 0;

            string aplaca1= "";
            string adist1 = "";
            string osent1 = "";
            string ocant1 = "";

            qtdpl = DtGVPlacas.RowCount;
            if (qtdpl == 1) { return false; }
            for (ind1=0;ind1<qtdpl;ind1++)
            {
                
                if (DtGVPlacas[1, ind1].Value != null) { aplaca1 = DtGVPlacas[1, ind1].Value.ToString(); }
                if (DtGVPlacas[2, ind1].Value != null) { adist1 = DtGVPlacas[2, ind1].Value.ToString(); }
                if (DtGVPlacas[3, ind1].Value != null) { osent1 = DtGVPlacas[3, ind1].Value.ToString(); }
                if (DtGVPlacas[4, ind1].Value != null) { ocant1 = DtGVPlacas[4, ind1].Value.ToString(); }
                



                if (aplaca==aplaca1)
                {
                    if (adist == adist1)
                    {
                        if (osent == osent1)
                        {
                           if (ocant== ocant1)
                            {
                                // Placa já existe no grid
                                return true;
                            }
                        }

                    }
                }
                              

            }
            return false; // Não achou a placa no grid

        }
        //***********************************************************


        private void button1_Click(object sender, EventArgs e)
        {
            int rowIndex = 0;

            //   DtGVPlacas.Rows[ind1 - 1].Cells[1].ToString())
            try { 
            rowIndex=DtGVPlacas.CurrentCell.RowIndex;
            DtGVPlacas.Rows.RemoveAt(rowIndex);
            }
            catch
            { }

        }

        private void btSair_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }


        // *****************************************************
     
       private void BuscaComboPlacas(ref int indpl, string str1)
        {
                       
            int index = cmbPlacaFE.FindString(str1);

            if (index>=0)
            { indpl = index; }
        }


            // ********************************************************

        private void btSalvar_Click(object sender, EventArgs e)
        {
            int qtdpl = 0;
            int ind1 = 0;

            string aRod = "";
            string okm = "";
            string oMunSen = "";

            string strConIns = "";
            string strConIns2 = "";

            string aplaca1 = "";
            string adist1 = "";
            string osent1 = "";
            string ocant1 = "";
            string ostatus1 = "";
            string adistA1 = "";

            int inclusao = 0;
            int remocao = 0;
            int reposicionar = 0;
            int manter = 0;

         string strv = ", ";

            int resto = 0;

            ////com1.CommandText = "delete from tplacas";
            ////com1.ExecuteNonQuery();

            com1.CommandText = "select Rodovia, kmEdital, MunSen from ponto_featual";
            reader1 = com1.ExecuteReader();
            if (reader1.Read())
            {
                aRod = reader1["Rodovia"].ToString();
                okm = reader1["kmEdital"].ToString();
                oMunSen = (reader1["MunSen"]).ToString();

            }
            reader1.Close();

            ////"update tpaginas set p1=0, p2=0, p3=0, p4=0, p5=0, p6=0, p7=0, p8=0, p9=0, p10=0, p11=0, p12=0, p13=0, p14=0, p15=0, p16=0, p17=0, p18=0, p19=0,";
            ////com2.CommandText = com2.CommandText + " p20=0, p21=0, p22=0, p23=0, p24=0, p25=0, p26=0";
            ////com2.CommandText = com2.CommandText + " where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) +"'";
            ////com2.CommandText = com2.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";
            ////com2.ExecuteNonQuery();


            //cmd.CommandText = "INSERT INTO klant(klant_id, naam, voornaam)   VALUES(@param1,@param2,@param3)";

            //strConIns = "insert into tplacas(Rodovia,kmEdital,MunSen,placa1,dist1,S1,ct1,placa2,dist2,S2,ct2,placa3,dist3,S3,ct3,placa4,dist4,S4,ct4,placa5,dist5,S5,ct5,";
            //strConIns = strConIns + "placa6,dist6,S6,ct6,placa7,dist7,S7,ct7,placa8,dist8,S8,ct8,placa9,dist9,S9,ct9,placa10,dist10,S10,ct10)";
            //strConIns = strConIns + " Values(" + "'" + aRod + "'" + strv + okm + strv + "'" + oMunSen + "'";


            strConIns = "update tplacas set ";
            strConIns2 = "";


            qtdpl = DtGVPlacas.RowCount;
          
            for (ind1 = 0; ind1 < qtdpl-1; ind1++)
            {

                if (DtGVPlacas[1, ind1].Value != null) { aplaca1 = DtGVPlacas[1, ind1].Value.ToString(); }
                if (DtGVPlacas[2, ind1].Value != null) { adist1 = DtGVPlacas[2, ind1].Value.ToString(); }
                if (DtGVPlacas[3, ind1].Value != null) { osent1 = DtGVPlacas[3, ind1].Value.ToString(); }
                if (DtGVPlacas[4, ind1].Value != null) { ocant1 = DtGVPlacas[4, ind1].Value.ToString(); }
                if (DtGVPlacas[5, ind1].Value != null) { ostatus1 = DtGVPlacas[5, ind1].Value.ToString(); }
                if (DtGVPlacas[6, ind1].Value != null) { adistA1 = DtGVPlacas[6, ind1].Value.ToString(); }

                if (ostatus1 == "incluir") {inclusao = 1; }
                if (ostatus1 == "remover") { remocao = 1; }
                if (ostatus1 == "reposicionar") { reposicionar = 1; }
                if (ostatus1 == "manter") { manter = 1; }



                //strConIns2 = strConIns2 + strv + "'" + aplaca1 + "'" + strv + adist1 + strv + "'" + osent1 + "'" + strv+ "'" + ocant1 + "'";
                if (DtGVPlacas[1, ind1].Value != null)
                {
                    strConIns2 = strConIns2 + "placa" + (ind1 + 1).ToString() + "=" + "'" + aplaca1 + "'" + strv + " dist" + (ind1 + 1).ToString() + "=" + adist1 + strv;
                    strConIns2 = strConIns2 + "S" + (ind1 + 1).ToString() + "=" + "'" + osent1 + "'" + strv + "ct" + (ind1 + 1).ToString() + "=" + "'" + ocant1 + "'" + strv;
                    strConIns2 = strConIns2 + "stt" + (ind1 + 1).ToString() + "=" + "'" + ostatus1 + "'" + strv + "dta" + (ind1 + 1).ToString() + "=" + adistA1;


                    if ((ind1 + 1) < 12) { strConIns2 = strConIns2 + strv; }
                }

            }


            resto = 12 - qtdpl;
            for (ind1 = qtdpl; ind1 <= 12; ind1++)
            {
                strConIns2 = strConIns2 + "placa" + (ind1).ToString() + "=" + "'" + 0 + "'" + strv + " dist" + (ind1).ToString() + "=" + 0 + strv;
                strConIns2 = strConIns2 + "S" + (ind1).ToString() + "=" + "'" + 0 + "'" + strv + " ct" + (ind1).ToString() + "=" + "'" + 0 + "'" ;
                if ((ind1) < 12) { strConIns2 = strConIns2 + strv; }
            }

            //strConIns2 = strConIns2 + ")";
            strConIns = strConIns + strConIns2;
            strConIns= strConIns + " where Rodovia = " + "'" + aRod + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(okm.ToString()) + "'";
            strConIns = strConIns +  " and MunSen= " + "'" + oMunSen + "'";

            com1.CommandText = strConIns;
            com1.ExecuteNonQuery();




            strConIns = "update tproj set inclusao=" + inclusao + ", " + "remocao=" + remocao + ", " + "reposicionar=" + reposicionar + ", " + "manter=" + manter;
            strConIns+= " where Rodovia = " + "'" + aRod + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(okm.ToString()) + "'" + " and MunSen= " + "'" + oMunSen + "'";
            com1.CommandText = strConIns;
            com1.ExecuteNonQuery();

        }

        private void rdalt_CheckedChanged(object sender, EventArgs e)
        {
            if (rdalt.Checked)
            { cmbDantiga.Enabled = true; }
            else
            { cmbDantiga.Enabled = false; }
        }

        private void CmbPlacaFE_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
        }

        private void Bt60FEV_Click(object sender, EventArgs e)
        {
          
        }

        private void Bt60r19_Click(object sender, EventArgs e)
        {
         

        }

        private void Bt50FE_Click(object sender, EventArgs e)
        {
        }

        private void Bt50FEV_Click(object sender, EventArgs e)
        {
            
        }

        private void Bt50r19_Click(object sender, EventArgs e)
        {
            
        }

        private void TsBt60fe_Click(object sender, EventArgs e)
        {

            int indpl = -1;
            string str2 = char.ConvertFromUtf32(34);

            string str1 = "placa(s) de " + str2 + "60 km/h - Fiscalização Eletrônica" + str2;

            BuscaComboPlacas(ref indpl, str1);
            if (indpl >= 0)
            { cmbPlacaFE.SelectedIndex = indpl; }
        }

        private void TsBt60fev_Click(object sender, EventArgs e)
        {
            int indpl = -1;
            string str2 = char.ConvertFromUtf32(34);

            string str1 = "placa(s) de " + str2 + "60 km/h - Fiscalização Eletrônica de Velocidade" + str2;

            BuscaComboPlacas(ref indpl, str1);
            if (indpl >= 0)
            { cmbPlacaFE.SelectedIndex = indpl; }
        }

        private void TsBt60r19_Click(object sender, EventArgs e)
        {
            int indpl = -1;
            string str2 = char.ConvertFromUtf32(34);

            string str1 = "placa(s) R19 de 60 km/h " + str2 + "VELOCIDADE MÁXIMA PERMITIDA" + str2;

            BuscaComboPlacas(ref indpl, str1);
            if (indpl >= 0)
            { cmbPlacaFE.SelectedIndex = indpl; }
        }

        private void TsBt50fe_Click(object sender, EventArgs e)
        {

            int indpl = -1;
            string str2 = char.ConvertFromUtf32(34);

            string str1 = "placa(s) de " + str2 + "50 km/h - Fiscalização Eletrônica" + str2;

            BuscaComboPlacas(ref indpl, str1);
            if (indpl >= 0)
            { cmbPlacaFE.SelectedIndex = indpl; }
        }

        private void TsBt50fev_Click(object sender, EventArgs e)
        {
            int indpl = -1;
            string str2 = char.ConvertFromUtf32(34);

            string str1 = "placa(s) de " + str2 + "50 km/h - Fiscalização Eletrônica de Velocidade" + str2;

            BuscaComboPlacas(ref indpl, str1);
            if (indpl >= 0)
            { cmbPlacaFE.SelectedIndex = indpl; }
        }

        private void TsBt50r19_Click(object sender, EventArgs e)
        {
            int indpl = -1;
            string str2 = char.ConvertFromUtf32(34);

            string str1 = "placa(s) R19 de 50 km/h " + str2 + "VELOCIDADE MÁXIMA PERMITIDA" + str2;

            BuscaComboPlacas(ref indpl, str1);
            if (indpl >= 0)
            { cmbPlacaFE.SelectedIndex = indpl; }
        }

        private void CmbSeq_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bt50_Click(object sender, EventArgs e)
        {
            cmbDist.Text = "50";
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void bt100_Click(object sender, EventArgs e)
        {
            cmbDist.Text = "100";
        }

        private void bt150_Click(object sender, EventArgs e)
        {
            cmbDist.Text = "150";
        }

        private void bt200_Click(object sender, EventArgs e)
        {
            cmbDist.Text = "200";
        }

        private void bt250_Click(object sender, EventArgs e)
        {
            cmbDist.Text = "250";
        }

        private void bt300_Click(object sender, EventArgs e)
        {
            cmbDist.Text = "300";
        }

        private void bt350_Click(object sender, EventArgs e)
        {
            cmbDist.Text = "350";
        }

        private void bt400_Click(object sender, EventArgs e)
        {
            cmbDist.Text = "400";
        }

        private void bt450_Click(object sender, EventArgs e)
        {
            cmbDist.Text = "450";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void btS2_Click(object sender, EventArgs e)
        {
            if (cmbSentido.SelectedIndex==0)
            { cmbSentido.SelectedIndex = 1; }
            else
            { cmbSentido.SelectedIndex = 0; }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        // *****************************************************
    }
}
