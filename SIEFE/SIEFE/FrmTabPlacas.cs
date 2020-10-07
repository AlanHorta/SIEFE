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
    public partial class FrmTabPlacas : Form
      {

        MySqlConnection mysqlCon1;
        MySqlCommand com1;
        MySqlDataReader reader1;

        Uteis cUt = new Uteis();
        C_Placas objplacas = new C_Placas();

        string strConFE = @"Server=localhost;Database=fe;Uid=root;Pwd=obuldog67;";

        //
        
        public string strConcfc = @"Server=localhost;Database=classfic;Uid=root;Pwd=obuldog67;";

        
        MySqlConnection mysqlCon2;

        public MySqlConnection MysqlConClf1;
        public MySqlConnection MysqlConClf2;

        
        MySqlCommand com2;
        public MySqlCommand comcfc1;
        public MySqlCommand comcfc2;
                
        MySqlDataReader reader2;

        public MySqlDataReader readercfc1;
        public MySqlDataReader readercfc2;

        public Boolean bEDuplo = false;     // true se o aparelho é do tipo I que pega nos dois sentidos
        Boolean EUmSentido = false; // true se o ponto de FE é fiscalizado nos dois sentidos
        int Vel85pSB = 0;
        int VmdB = 0;               // o Vmd do sentido B se houver;

        public int npag = 0;
        public int nsheet = 0;
        public int TotalFolhas = 23;

        public int oaleatP = 0;
        public int oEduplo = 0;

        int aVezt = 0;


        C_excel objExcel = new C_excel();

       

        //



        public FrmTabPlacas()
        {
            InitializeComponent();
        }

        private void FrmTabPlacas_Load(object sender, EventArgs e)
        {
            chkbPp.Checked = false;
                                          
                ConectaBanco();
            ConectaBanco2();
            CarregaGridPlacas();

                CarregaCombosPlacas();
                cmbSentido.SelectedIndex = 1;            
        }




        public void ConectaBanco2()
        {

            using (mysqlCon2 = new MySqlConnection(strConFE)) ;
            {
                mysqlCon2.Open();
                com2 = mysqlCon2.CreateCommand();

            }

        }



        private void DesconectaBanco2()
        {
            mysqlCon2.Close();
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
                oMunSen = (reader1["MunSen"]).ToString();

            }
            reader1.Close();

            // ttabelaplacas - tabela criada para suportar tambem as placas de Dp Distancia de percepção e Dr distancia de reserva
            com1.CommandText = "select * from ttabelaplacas where Rodovia = " + "'" + aRod + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(okm.ToString()) + "'";
            com1.CommandText = com1.CommandText + " and MunSen= " + "'" + oMunSen + "'";

            reader1 = com1.ExecuteReader();

            if (reader1.Read())
            {
                for (ind1 = 1; ind1 < 30; ind1++)
                {
                    strCols = ind1.ToString();
                    if ((Int32.Parse(reader1["dist" + strCols].ToString()) >= 0) && (!(reader1["placa" + strCols].ToString().Equals("0"))))
                    {
                        string ssssplaca = reader1["placa" + strCols].ToString();
                        objplacas.placa[ind1] = reader1["placa" + strCols].ToString();
                        objplacas.dist[ind1] = Int32.Parse(reader1["dist" + strCols].ToString());
                        objplacas.St[ind1] = reader1["S" + strCols].ToString();
                        objplacas.cant[ind1] = reader1["ct" + strCols].ToString();
                        objplacas.status[ind1] = reader1["stt" + strCols].ToString();
                        objplacas.dta[ind1] = Int32.Parse(reader1["dta" + strCols].ToString());

                        DtGVPlacas.Rows.Add(ind1, objplacas.placa[ind1], objplacas.dist[ind1], objplacas.St[ind1], objplacas.cant[ind1], objplacas.status[ind1], objplacas.dta[ind1]);
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
            { cmbPlacaFE.Items.Add(reader1["desc"]); }

            reader1.Close();


            // Combo distância
            com1.CommandText = "select * from tdistancias";
            reader1 = com1.ExecuteReader();
            cmbDist.Items.Clear();
            cmbDantiga.Items.Clear();
            while (reader1.Read())
            {
                cmbDist.Items.Add(reader1["distancia"]);
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

            cmbCanteiro.SelectedIndex = 3;
        }

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

            string str1 = "";
            string str2 = char.ConvertFromUtf32(34);

            if (rdinc.Checked) { ostatus = "incluir"; }
            if (rdrem.Checked) { ostatus = "remover"; }
            if (rdmanter.Checked) { ostatus = "manter"; }
            adistAnt = 0;
            if (rdalt.Checked)
            {
                ostatus = "reposicionar";
                adistAnt = Int32.Parse(cmbDantiga.Text.ToString());
            }





            if (!chkbPp.Checked)
            {

                if (!BuscaNoGrid(cmbPlacaFE.Text, cmbDist.Text, cmbSentido.Text, cmbCanteiro.Text))
                {
                    qtdG = DtGVPlacas.RowCount;
                    // Se não achou a aplaca no grid então inclui
                    DtGVPlacas.Rows.Add(qtdG, cmbPlacaFE.Text, cmbDist.Text, cmbSentido.Text, cmbCanteiro.Text, ostatus, adistAnt);
                }
            }
            else
                 {
                // placas de distância

                qtdG = DtGVPlacas.RowCount;

                str1 = "placa(s) R19 de 60 km/h " + str2 + "VELOCIDADE MÁXIMA PERMITIDA" + str2;
                cmbPlacaFE.Text = str1;
                cmbCanteiro.Text = "no canteiro central";
                cmbDist.Text = "Dp + Dr + 500";
                DtGVPlacas.Rows.Add(qtdG, cmbPlacaFE.Text, cmbDist.Text, cmbSentido.Text, cmbCanteiro.Text, ostatus, adistAnt);


                qtdG = DtGVPlacas.RowCount;

                str1 = "placa(s) R19 de 50 km/h " + str2 + "VELOCIDADE MÁXIMA PERMITIDA" + str2;
                cmbPlacaFE.Text = str1;
                cmbCanteiro.Text = "no canteiro lateral";
                cmbDist.Text = "Dr + 500";
                DtGVPlacas.Rows.Add(qtdG, cmbPlacaFE.Text, cmbDist.Text, cmbSentido.Text, cmbCanteiro.Text, ostatus, adistAnt);

                chkbPp.Checked=false;
               // para o banco de dados usar 10000 e 5000 metros como distância

            }




            }
        // *****************************************************










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
            int maxnplacas = 30;


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


            strConIns = "update tTabelaplacas set ";
            strConIns2 = "";


            qtdpl = DtGVPlacas.RowCount;

            for (ind1 = 0; ind1 < qtdpl - 1; ind1++)
            {

                if (DtGVPlacas[1, ind1].Value != null) { aplaca1 = DtGVPlacas[1, ind1].Value.ToString(); }
                if (DtGVPlacas[2, ind1].Value != null) { adist1 = DtGVPlacas[2, ind1].Value.ToString(); }
                if (DtGVPlacas[3, ind1].Value != null) { osent1 = DtGVPlacas[3, ind1].Value.ToString(); }
                if (DtGVPlacas[4, ind1].Value != null) { ocant1 = DtGVPlacas[4, ind1].Value.ToString(); }
                if (DtGVPlacas[5, ind1].Value != null) { ostatus1 = DtGVPlacas[5, ind1].Value.ToString(); }
                if (DtGVPlacas[6, ind1].Value != null) { adistA1 = DtGVPlacas[6, ind1].Value.ToString(); }

                if (ostatus1 == "incluir") { inclusao = 1; }
                if (ostatus1 == "remover") { remocao = 1; }
                if (ostatus1 == "reposicionar") { reposicionar = 1; }
                if (ostatus1 == "manter") { manter = 1; }



                //strConIns2 = strConIns2 + strv + "'" + aplaca1 + "'" + strv + adist1 + strv + "'" + osent1 + "'" + strv+ "'" + ocant1 + "'";
                if (DtGVPlacas[1, ind1].Value != null)
                {
                    strConIns2 = strConIns2 + "placa" + (ind1 + 1).ToString() + "=" + "'" + aplaca1 + "'" + strv + " dist" + (ind1 + 1).ToString() + "=" + adist1 + strv;
                    strConIns2 = strConIns2 + "S" + (ind1 + 1).ToString() + "=" + "'" + osent1 + "'" + strv + "ct" + (ind1 + 1).ToString() + "=" + "'" + ocant1 + "'" + strv;
                    strConIns2 = strConIns2 + "stt" + (ind1 + 1).ToString() + "=" + "'" + ostatus1 + "'" + strv + "dta" + (ind1 + 1).ToString() + "=" + adistA1;


                    if ((ind1 + 1) < maxnplacas) { strConIns2 = strConIns2 + strv; }
                }

            }


            resto = maxnplacas - qtdpl;
            for (ind1 = qtdpl; ind1 <= maxnplacas; ind1++)
            {
                strConIns2 = strConIns2 + "placa" + (ind1).ToString() + "=" + "'" + 0 + "'" + strv + " dist" + (ind1).ToString() + "=" + 0 + strv;
                strConIns2 = strConIns2 + "S" + (ind1).ToString() + "=" + "'" + 0 + "'" + strv + " ct" + (ind1).ToString() + "=" + "'" + 0 + "'";
                strConIns2 = strConIns2 + strv + " stt" + (ind1).ToString() + "=" + "'" + 0 + "'" + strv + " dta" + (ind1).ToString() + "=" + 0 ;
                if ((ind1) < maxnplacas) { strConIns2 = strConIns2 + strv; }
            }

            //strConIns2 = strConIns2 + ")";
            strConIns = strConIns + strConIns2;
            strConIns = strConIns + " where Rodovia = " + "'" + aRod + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(okm.ToString()) + "'";
            strConIns = strConIns + " and MunSen= " + "'" + oMunSen + "'";

            com1.CommandText = strConIns;
            com1.ExecuteNonQuery();




            strConIns = "update tproj set inclusao=" + inclusao + ", " + "remocao=" + remocao + ", " + "reposicionar=" + reposicionar + ", " + "manter=" + manter;
            strConIns += " where Rodovia = " + "'" + aRod + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(okm.ToString()) + "'" + " and MunSen= " + "'" + oMunSen + "'";
            com1.CommandText = strConIns;
            com1.ExecuteNonQuery();
        }





        private Boolean BuscaNoGrid(string aplaca, string adist, string osent, string ocant)
        {
            int qtdpl = 0;
            int ind1 = 0;

            string aplaca1 = "";
            string adist1 = "";
            string osent1 = "";
            string ocant1 = "";

            qtdpl = DtGVPlacas.RowCount;
            if (qtdpl == 1) { return false; }
            for (ind1 = 0; ind1 < qtdpl; ind1++)
            {

                if (DtGVPlacas[1, ind1].Value != null) { aplaca1 = DtGVPlacas[1, ind1].Value.ToString(); }
                if (DtGVPlacas[2, ind1].Value != null) { adist1 = DtGVPlacas[2, ind1].Value.ToString(); }
                if (DtGVPlacas[3, ind1].Value != null) { osent1 = DtGVPlacas[3, ind1].Value.ToString(); }
                if (DtGVPlacas[4, ind1].Value != null) { ocant1 = DtGVPlacas[4, ind1].Value.ToString(); }




                if (aplaca == aplaca1)
                {
                    if (adist == adist1)
                    {
                        if (osent == osent1)
                        {
                            if (ocant == ocant1)
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

        private void tsBt60fe_Click(object sender, EventArgs e)
        {
            int indpl = -1;
            string str2 = char.ConvertFromUtf32(34);

            string str1 = "placa(s) de " + str2 + "60 km/h - Fiscalização Eletrônica" + str2;

            BuscaComboPlacas(ref indpl, str1);
            if (indpl >= 0)
            { cmbPlacaFE.SelectedIndex = indpl; }
        }

        private void tsBt60fev_Click(object sender, EventArgs e)
        {
            int indpl = -1;
            string str2 = char.ConvertFromUtf32(34);

            string str1 = "placa(s) de " + str2 + "60 km/h - Fiscalização Eletrônica de Velocidade" + str2;

            BuscaComboPlacas(ref indpl, str1);
            if (indpl >= 0)
            { cmbPlacaFE.SelectedIndex = indpl; }
        }

        private void tsBt60r19_Click(object sender, EventArgs e)
        {
            int indpl = -1;
            string str2 = char.ConvertFromUtf32(34);

            string str1 = "placa(s) R19 de 60 km/h " + str2 + "VELOCIDADE MÁXIMA PERMITIDA" + str2;

            BuscaComboPlacas(ref indpl, str1);
            if (indpl >= 0)
            { cmbPlacaFE.SelectedIndex = indpl; }
        }

        private void tsBt50fe_Click(object sender, EventArgs e)
        {

            int indpl = -1;
            string str2 = char.ConvertFromUtf32(34);

            string str1 = "placa(s) de " + str2 + "50 km/h - Fiscalização Eletrônica" + str2;

            BuscaComboPlacas(ref indpl, str1);
            if (indpl >= 0)
            { cmbPlacaFE.SelectedIndex = indpl; }
        }

        private void BuscaComboPlacas(ref int indpl, string str1)
        {

            int index = cmbPlacaFE.FindString(str1);

            if (index >= 0)
            { indpl = index; }
        }

        private void btS2_Click(object sender, EventArgs e)
        {
            if (cmbSentido.SelectedIndex == 0)
            { cmbSentido.SelectedIndex = 1; }
            else
            { cmbSentido.SelectedIndex = 0; }
        }

        private void btSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bt50_Click(object sender, EventArgs e)
        {
            cmbDist.Text = "50";
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

        private void tsBt50r19_Click(object sender, EventArgs e)
        {
            int indpl = -1;
            string str2 = char.ConvertFromUtf32(34);

            string str1 = "placa(s) R19 de 50 km/h " + str2 + "VELOCIDADE MÁXIMA PERMITIDA" + str2;

            BuscaComboPlacas(ref indpl, str1);
            if (indpl >= 0)
            { cmbPlacaFE.SelectedIndex = indpl; }
        }

        private void tsBt50fev_Click(object sender, EventArgs e)
        {

            int indpl = -1;
            string str2 = char.ConvertFromUtf32(34);

            string str1 = "placa(s) de " + str2 + "50 km/h - Fiscalização Eletrônica de Velocidade" + str2;

            BuscaComboPlacas(ref indpl, str1);
            if (indpl >= 0)
            { cmbPlacaFE.SelectedIndex = indpl; }
        }

        private void btRemove_Click(object sender, EventArgs e)
        {
            int rowIndex = 0;

            //   DtGVPlacas.Rows[ind1 - 1].Cells[1].ToString())
            try
            {
                rowIndex = DtGVPlacas.CurrentCell.RowIndex;
                DtGVPlacas.Rows.RemoveAt(rowIndex);
            }
            catch
            { }
        }


        private void VeTemFE(C_TemFE objTemFE, string strRod, string okm, string oSent)
        {
            com2.CommandText = "select * from ttemfe where Rodovia = " + "'" + strRod + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(okm) + "'";
            com2.CommandText = com2.CommandText + " and MunSen= " + "'" + oSent + "'";

            reader2 = com2.ExecuteReader();

            if (reader2.Read())
            {

                objTemFE.VaiTer = Int32.Parse(reader2["vaiterfe"].ToString());
                objTemFE.JaTinha = Int32.Parse(reader2["jatinhafe"].ToString());
                objTemFE.Confirma = Int32.Parse(reader2["foiconf"].ToString());

                objTemFE.TemQB = Int32.Parse(reader2["temQB"].ToString());

                objTemFE.Manter = Int32.Parse(reader2["manter"].ToString());
                objTemFE.Remover = Int32.Parse(reader2["remover"].ToString());

                objTemFE.Subst = Int32.Parse(reader2["subst"].ToString());
                objTemFE.Proj = Int32.Parse(reader2["proj"].ToString());
                objTemFE.Renova = Int32.Parse(reader2["renova"].ToString());
                objTemFE.Altera = Int32.Parse(reader2["altera"].ToString());


            }

            reader2.Close();
        }







        private void Veproj(C_proj objProj, string strRod, string okm, string oSent)
        {
            com2.CommandText = "select * from tproj where Rodovia = " + "'" + strRod + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(okm) + "'";
            com2.CommandText = com2.CommandText + " and MunSen= " + "'" + oSent + "'";

            reader2 = com2.ExecuteReader();

            if (reader2.Read())
            {
                objProj.Rodovia = (reader2["Rodovia"]).ToString();
                objProj.kmEdit = ((reader2["kmEdital"]).ToString());
                objProj.MunSen = (reader2["MunSen"]).ToString();

                objProj.intro1 = (reader2["intro1"]).ToString();
                objProj.intro2 = (reader2["intro2"]).ToString();

                objProj.DuasFolhas = Int32.Parse(reader2["DuasFolhas"].ToString());

                objProj.inclusao = Int32.Parse(reader2["inclusao"].ToString());
                objProj.remocao = Int32.Parse(reader2["remocao"].ToString());
                objProj.reposicionar = Int32.Parse(reader2["reposicionar"].ToString());
                objProj.manter = Int32.Parse(reader2["manter"].ToString());

            }
            reader2.Close();

        }











        private void button2_Click(object sender, EventArgs e)
        {
            // monta tabela de placas Excel

            // Folha projeto 1
            nsheet = nsheet + 1;
            npag = npag + 1;

            int ind1 = 0;

            int linTit = 11;
            Boolean poetit = false;

            String strproj1 = "";
            Boolean inc1 = false;
            Boolean rem1 = false;
            Boolean rep1 = false;
            Boolean mant1 = false;

            Boolean temOSent = false;
            C_TemFE objTemFE = new C_TemFE();
            C_paginas ObjPags = new C_paginas();

            //Boolean temSAB = false;
            //Boolean temSBA = false;
            Boolean entrou = false;

            String strDescTipo = "";

            String aRod1;
            String okm1;
            string strprojTit = "";

            string str2 = char.ConvertFromUtf32(34);

            C_pontofe objPFe = new C_pontofe();
            C_DataBase objDtb = new C_DataBase();
            C_proj objproj = new C_proj();

            

            //if (objTemFE.VaiTer) && ()

            

            //com1.CommandText = "select Rodovia, kmEdital, MunSen from ponto_featual";
            com1.CommandText = "select * from ponto_featual";
            reader1 = com1.ExecuteReader();
            if (reader1.Read())
            {
                objPFe.Rodovia = reader1["Rodovia"].ToString();
                objPFe.kmEdital = reader1["kmEdital"].ToString();
                objPFe.MunSen = (reader1["MunSen"]).ToString();

                objPFe.MunA= reader1["MunA"].ToString();
                objPFe.MunB = reader1["MunB"].ToString();

                objPFe.Tipo = (reader1["Tipo"]).ToString();

            }
            reader1.Close();

           

            if ((objPFe.Tipo) == "I.A") { strDescTipo = " Equipamento Redutor de Velocidade com Display."; }
            if ((objPFe.Tipo) == "I.B") { strDescTipo = " Equipamento Redutor de Velocidade com Display."; }
           // if ((objPFe.Tipo) == "I.B") { strDescTipo = " Equipamento Controlador de Velocidade."; }
            if (cUt.VeTipoEquip(objPFe.Tipo) == "II") { strDescTipo = " Equipamento tipo Radar Fixo."; }
            if (cUt.VeTipoEquip(objPFe.Tipo) == "III") { strDescTipo = " Equipamento tipo Semáforo Radar."; }

            C_Placas objPlacasIncMunB = new C_Placas();
            C_Placas objPlacasRemMunB = new C_Placas();
            C_Placas objPlacasRepMunB = new C_Placas();

            C_Placas objPlacasIncMunA = new C_Placas();
            C_Placas objPlacasRemMunA = new C_Placas();
            C_Placas objPlacasRepMunA = new C_Placas();

            string strRod; string MunA = ""; string MunB = "";

            strRod = objPFe.Rodovia;
            objDtb.GetMunicipios(strRod, ref MunA, ref MunB);  // obtem os municípios limite

            VeTemFE(objTemFE, strRod, objPFe.kmEdital, objPFe.MunSen);

            //"TabelaPlacasAB"

            string nomearq = "Estudo" + strRod + "_km" + objPFe.kmEdital + "_Res798" + ".xlsx";
            objExcel.AbreExcel(@"C:\DER-RJ\FiscalizaçãoEletronica\2020\Estudos2020\" + nomearq, "TabelaPlacasAB");

            objExcel.Agrupar(1, 11, 1, 12);
          //  objExcel.Pagina(1, 11, "TabelaPlacasAB", strRod + "  km " + cUt.ConvPontoVirg(objPFe.kmEdital));
            objExcel.Pagina(1, 11, "TabelaPlacasAB", strRod + "  km " + cUt.ConvPontoVirg(objPFe.kmEdital), 10, "Gray");
           // objExcel.MudaFonte(1, 11, "TabelaPlacasAB", "Arial", 11);

                        
            objExcel.Agrupar(6, 2, 6, 13);
            strproj1 = "SENTIDO CRESCENTE - " + MunA + "/RJ para " + MunB + "/RJ";
            objExcel.Pagina(6, 2, "TabelaPlacasAB", strproj1, 12, "Black");
            objExcel.MudaFonte(6, 2, "TabelaPlacasAB", "Arial", true, 14);


            objExcel.Desagrupar(24, 3);

            // objExcel.Agrupar(24,3,24,13);
            // objExcel.Agrupar(25, 3, 25, 13);
           

            //VeGeometria(objGeom);
            // VeAcidentes(objAcid);
            // VeHistorico(objHist);




            Veproj(objproj, strRod, objPFe.kmEdital, objPFe.MunSen);

            inc1 = (objproj.inclusao == 1);
            rem1 = (objproj.remocao == 1);
            rep1 = (objproj.reposicionar == 1);
            mant1 = (objproj.manter == 1);


            inc1 = true;            // mecher aqui depois   

            objproj.kmEdit = cUt.ConvPontoVirg(objproj.kmEdit);


            if ((!inc1 && !rem1 && !rep1 && !mant1))
            { strproj1 = "O projeto no trecho estudado, da rodovia " + objproj.Rodovia + " no km " + objproj.kmEdit + " não necessita de inclusão, remoção ou reposicionamento de placa(s) "; }



            if ((!inc1 && !rem1 && !rep1 && mant1))
            {
                strproj1 = "O projeto no trecho estudado, da rodovia " + objproj.Rodovia + " no km " + objproj.kmEdit;
                strproj1 += " não necessita de inclusão, remoção ou reposicionamento de placa(s). ";
                strproj1 += "A sinalização deve ser mantida";

            }

            if ((!inc1 && rem1 && !rep1 && mant1))
            {
                strproj1 = "O projeto no trecho estudado, da rodovia " + objproj.Rodovia + " no km " + objproj.kmEdit;
                strproj1 += " não necessita de inclusão nem reposicionamento de placa(s). ";
                strproj1 += "Uma ou mais placas devem ser removidas e o restante da sinalização deve ser mantida ";

            }



            if ((inc1 && !rem1 && !rep1 && mant1))
            {
                strproj1 = "O projeto no trecho estudado, da rodovia " + objproj.Rodovia + " no km " + objproj.kmEdit;
                strproj1 += " necessita de inclusão de placa(s) e que placa(s) existentes sejam mantidas";


            }


            if ((!inc1 && !rem1 && rep1 && !mant1))
            { strproj1 = "O projeto no trecho estudado, da rodovia " + objproj.Rodovia + " no km " + objproj.kmEdit + " consiste no reposicionamento de placa(s) "; }


            if ((!inc1 && rem1 && !rep1 && !mant1))
            { strproj1 = "O projeto no trecho estudado, da rodovia " + objproj.Rodovia + " no km " + objproj.kmEdit + " consiste na remoção de placa(s) "; }

            if ((!inc1 && rem1 && rep1 && !mant1))
            { strproj1 = "O projeto no trecho estudado, da rodovia " + objproj.Rodovia + " no km " + objproj.kmEdit + " consiste na remoção e reposicionamento de placa(s) "; }


            if ((inc1 && !rem1 && !rep1 && !mant1))
            { strproj1 = "O projeto no trecho estudado, da rodovia " + objproj.Rodovia + " no km " + objproj.kmEdit + " consiste na inclusão de placa(s) "; }

            if ((inc1 && !rem1 && rep1 && !mant1))
            { strproj1 = "O projeto no trecho estudado, da rodovia " + objproj.Rodovia + " no km " + objproj.kmEdit + " consiste na inclusão e reposicionamento de placa(s) "; }

            if ((inc1 && rem1 && !rep1 && !mant1))
            { strproj1 = "O projeto no trecho estudado, da rodovia " + objproj.Rodovia + " no km " + objproj.kmEdit + " consiste na inclusão e remoção de placa(s) "; }

            if ((inc1 && rem1 && rep1 && !mant1))
            { strproj1 = "O projeto no trecho estudado, da rodovia " + objproj.Rodovia + " no km " + objproj.kmEdit + " consiste na inclusão, remoção e reposicionamento de placa(s) "; }


            strproj1 += " para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.";
            //objExcel.Pagina(7, 3, nsheet, objproj.intro1.ToString() + " " + lblRod2.Text + " km " + lblkmR2.Text + ", " + objproj.intro2.ToString());


            objExcel.Agrupar(8, 2, 61, 13);
            objExcel.Pagina(8, 2, "TabelaPlacasAB", " ", 12, "Black");
            objExcel.Desagrupar(8, 2, 61, 13);

            objExcel.Agrupar(8, 2, 10, 13);
            objExcel.Pagina(8, 2, "TabelaPlacasAB", strproj1, 12, "Black");
            objExcel.MudaFonte(8, 2, "TabelaPlacasBA", "Arial", true, 12);

            linTit = 12;
            strproj1 = "Incluir as seguintes placas para a Dp e Dr antes e após o trecho crítico:";           
            objExcel.Agrupar(linTit, 2, linTit , 13);
            objExcel.Pagina(linTit, 2, "TabelaPlacasAB", strproj1, 12, "Black");
            linTit = linTit + 2;



            //              PISTA DUPLA
            //strproj1 = "-(1) Uma placa(s) R19 de 60 km / h " + str2 + "VELOCIDADE MÁXIMA PERMITIDA" + str2;
            //strproj1 += " no canteiro central";
            //strproj1 += " entre 57 a 120 metros como Dp + " + "Uma Dr entre 45 a 60 metros + " + " uma distância de 500 metros até o ";
            //strproj1 += strDescTipo;
            //objExcel.Agrupar(linTit, 2, linTit + 2, 13);
            //objExcel.Pagina(linTit, 2, "TabelaPlacasAB", strproj1, 12, "Black");
            //objExcel.MudaFonte(linTit, 2, "TabelaPlacasAB", "Arial", false, 12);
            //linTit = linTit + 4;

            strproj1 = "-(1) Uma placa(s) R19 de 60 km / h " + str2 + "VELOCIDADE MÁXIMA PERMITIDA" + str2;
            strproj1 += " no canteiro lateral";
            strproj1 += " entre 57 a 120 metros como Dp + " + "Uma Dr entre 45 a 60 metros + " + " uma distância de 500 metros até o ";
            strproj1 += strDescTipo;
            objExcel.Agrupar(linTit, 2, linTit + 2, 13);
            objExcel.Pagina(linTit, 2, "TabelaPlacasAB", strproj1, 12, "Black");
            objExcel.MudaFonte(linTit, 2, "TabelaPlacasAB", "Arial", false, 12);
            linTit = linTit + 4;


            //              PISTA DUPLA
            //strproj1 = "-(1) Uma placa(s) R19 de 50 km / h " + str2 + "VELOCIDADE MÁXIMA PERMITIDA" + str2;
            //strproj1 += " no canteiro central";
            //strproj1 += " entre 45 a 60 metros como Dr + " + " uma distância de 500 metros até o ";
            //strproj1 += strDescTipo;
            //objExcel.Agrupar(linTit, 2, linTit + 1, 13);
            //objExcel.Pagina(linTit, 2, "TabelaPlacasAB", strproj1, 12, "Black");
            //objExcel.MudaFonte(linTit, 2, "TabelaPlacasAB", "Arial", false, 12);
            //linTit = linTit + 3;


            strproj1 = "-(1) Uma placa(s) R19 de 50 km / h " + str2 + "VELOCIDADE MÁXIMA PERMITIDA" + str2;
            strproj1 += " no canteiro lateral";
            strproj1 += " entre 45 a 60 metros como Dr + " + " uma distância de 500 metros até o ";
            strproj1 += strDescTipo;
            objExcel.Agrupar(linTit, 2, linTit + 1, 13);
            objExcel.Pagina(linTit, 2, "TabelaPlacasAB", strproj1, 12, "Black");
            objExcel.MudaFonte(linTit, 2, "TabelaPlacasAB", "Arial", false, 12);
            linTit = linTit + 3;

            //              PISTA DUPLA
            //strproj1 = "-(1) Uma placa(s) R19 de 60 km / h " + str2 + "VELOCIDADE MÁXIMA PERMITIDA" + str2;
            //strproj1 += " no canteiro central";
            //strproj1 += " Após uma distância de 500 metros após o ";
            //strproj1 += strDescTipo;
            //objExcel.Agrupar(linTit, 2, linTit + 1, 13);
            //objExcel.Pagina(linTit, 2, "TabelaPlacasAB", strproj1, 12, "Black");
            //objExcel.MudaFonte(linTit, 2, "TabelaPlacasAB", "Arial", false, 12);
            //linTit = linTit + 3;

            strproj1 = "-(1) Uma placa(s) R19 de 60 km / h " + str2 + "VELOCIDADE MÁXIMA PERMITIDA" + str2;
            strproj1 += " no canteiro lateral";
            strproj1 += " a uma distância de 500 metros após o ";
            strproj1 += strDescTipo;
            objExcel.Agrupar(linTit, 2, linTit + 1, 13);
            objExcel.Pagina(linTit, 2, "TabelaPlacasAB", strproj1, 12, "Black");
            objExcel.MudaFonte(linTit, 2, "TabelaPlacasAB", "Arial", false, 12);
            linTit = linTit + 3;


            temOSent = false;



                

            entrou = false;
            // Escaneia todas as placas em busca de "incluir"
            for (ind1 = 1; ind1 < 30; ind1++)
            {
                com2.CommandText = "select * from ttabelaplacas where Rodovia = ";
                com2.CommandText += "'" + strRod + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(objPFe.kmEdital) + "'";
                com2.CommandText += " and MunSen= " + "'" + objPFe.MunSen + "'" + " and S" + ind1.ToString() + "=" + "'" + MunB + "'";
                com2.CommandText += " and stt" + ind1.ToString() + "=" + "'incluir'";
                reader2 = com2.ExecuteReader();
                if (reader2.Read())
                {

                    objPlacasIncMunB.placa[ind1] = reader2["placa" + ind1.ToString()].ToString();
                    objPlacasIncMunB.dist[ind1] = Int32.Parse(reader2["dist" + ind1.ToString()].ToString());
                    objPlacasIncMunB.St[ind1] = reader2["S" + ind1.ToString()].ToString();
                    objPlacasIncMunB.cant[ind1] = reader2["ct" + ind1.ToString()].ToString();
                    objPlacasIncMunB.status[ind1] = reader2["stt" + ind1.ToString()].ToString();
                    objPlacasIncMunB.dta[ind1] = Int32.Parse(reader2["dta" + ind1.ToString()].ToString());

                    if ((objPlacasIncMunB.dist[ind1] == 0))
                    {
                        strproj1 = "-(1) Uma " + objPlacasIncMunB.placa[ind1] + " " + objPlacasIncMunB.cant[ind1] + " junto ao ";
                        strproj1 = strproj1 + strDescTipo;
                    }
                    else
                    {
                        strproj1 = "-(1) Uma " + objPlacasIncMunB.placa[ind1] + " " + objPlacasIncMunB.cant[ind1] + " a " + objPlacasIncMunB.dist[ind1] + " metros ";
                        strproj1 = strproj1 + "do" + strDescTipo;
                    }

                    if (!entrou)
                    {
                        //Titulo "Incluir"
                        strprojTit = "No sentido " + MunA + " / " + MunB + " acrescentar nessa ordem:";
                        objExcel.Agrupar(linTit, 2, linTit , 13);
                        objExcel.Pagina(linTit, 2, "TabelaPlacasAB", strprojTit,12,"Black");
                        objExcel.MudaFonte(linTit, 2, "TabelaPlacasAB", "Arial", true, 12);

                        linTit++;
                    }
                    objExcel.Agrupar(linTit, 2, linTit + 1, 13);
                    objExcel.Pagina(linTit, 2, "TabelaPlacasAB", strproj1, 12, "Black");
                    objExcel.MudaFonte(linTit, 2, "TabelaPlacasAB", "Arial", false, 12);
                    // objExcel.MudaFonte(linTit, 3, nsheet, "Arial", 12);
                    temOSent = true;
                    linTit = linTit + 3;
                    entrou = true;
                }

                reader2.Close();
            }

            if (temOSent)
            {

                //strproj1 = "-(1) Uma pintura de fiscalização eletrônica de velocidade no asfalto a 50 metros do" + strDescTipo;
                //objExcel.MudaFonte(linTit, 3, nsheet, "Arial", false, 12);
                //objExcel.Pagina(linTit, 3, nsheet, strproj1);
                //linTit = linTit + 3;
                //poetit = false;
            }
            else
            {
                //strproj1 = "Não há placas de Fiscalização Eletrônica a acrescentar para esse sentido.";
                //objExcel.MudaFonte(linTit, 3, nsheet, "Arial", 12);
                //objExcel.Pagina(linTit, 3, nsheet, strproj1);
                //linTit = linTit + 3;
                //poetit = false;

            }


            if (entrou)
            {
                linTit++; linTit++; // Vai para a linha do próximo titulo 
            }


            // *************************************************************************************************************************************************




            entrou = false;
            // Escaneia todas as placas em busca de "manter"
            for (ind1 = 1; ind1 < 12; ind1++)
            {
                com2.CommandText = "select * from ttabelaplacas where Rodovia = ";
                com2.CommandText += "'" + strRod + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(objPFe.kmEdital) + "'";
                com2.CommandText += " and MunSen= " + "'" + objPFe.MunSen + "'" + " and S" + ind1.ToString() + "=" + "'" + MunB + "'";
                com2.CommandText += " and stt" + ind1.ToString() + "=" + "'manter'";
                reader2 = com2.ExecuteReader();
                if (reader2.Read())
                {

                    objPlacasIncMunB.placa[ind1] = reader2["placa" + ind1.ToString()].ToString();
                    objPlacasIncMunB.dist[ind1] = Int32.Parse(reader2["dist" + ind1.ToString()].ToString());
                    objPlacasIncMunB.St[ind1] = reader2["S" + ind1.ToString()].ToString();
                    objPlacasIncMunB.cant[ind1] = reader2["ct" + ind1.ToString()].ToString();
                    objPlacasIncMunB.status[ind1] = reader2["stt" + ind1.ToString()].ToString();
                    objPlacasIncMunB.dta[ind1] = Int32.Parse(reader2["dta" + ind1.ToString()].ToString());

                    if ((objPlacasIncMunB.dist[ind1] == 0))
                    {
                        strproj1 = "-(1) Uma " + objPlacasIncMunB.placa[ind1] + " " + objPlacasIncMunB.cant[ind1] + " junto ao ";
                        strproj1 = strproj1 + strDescTipo;
                    }
                    else
                    {
                        strproj1 = "-(1) Uma " + objPlacasIncMunB.placa[ind1] + " " + objPlacasIncMunB.cant[ind1] + " a " + objPlacasIncMunB.dist[ind1] + " metros ";
                        strproj1 = strproj1 + "do" + strDescTipo;
                    }

                    if (!entrou)
                    {
                        // Titulo "Manter"
                        strprojTit = "Manter as placas no sentido " + MunA + " / " + MunB + " nessa ordem:";
                        objExcel.Agrupar(linTit, 2, linTit , 13);
                        objExcel.MudaFonte(linTit, 2, "TabelaPlacasAB", "Arial", true, 12);
                        objExcel.Pagina(linTit, 2, "TabelaPlacasAB", strprojTit);
                        linTit++; linTit++;
                    }
                    objExcel.Agrupar(linTit, 2, linTit + 1, 13);
                    objExcel.Pagina(linTit, 2, "TabelaPlacasAB", strproj1, 12, "Black");
                    objExcel.MudaFonte(linTit, 2, "TabelaPlacasAB", "Arial",false, 12);
                    temOSent = true;
                    linTit = linTit + 3;
                    entrou = true;
                }

                reader2.Close();
            }

            if (temOSent)
            {
                // linTit = linTit - 2;
                strproj1 = "-(1) Uma pintura de fiscalização eletrônica de velocidade no asfalto a 50 metros do" + strDescTipo;
                objExcel.Agrupar(linTit, 2, linTit + 1, 13);
                objExcel.MudaFonte(linTit, 2, "TabelaPlacasAB", "Arial", false, 12);
                objExcel.Pagina(linTit, 2, "TabelaPlacasAB", strproj1);
                linTit = linTit + 3;
                poetit = false;
            }
            else
            {
                //strproj1 = "Não há placas de Fiscalização Eletrônica a acrescentar para esse sentido.";
                //objExcel.MudaFonte(linTit, 3, nsheet, "Arial", 12);
                //objExcel.Pagina(linTit, 3, nsheet, strproj1);
                //linTit = linTit + 3;
                //poetit = false;

            }

            if (entrou)
            {
                linTit++; linTit++; // Vai para a linha do próximo titulo 
            }





            entrou = false;
            // Escaneia todas as placas em busca de "remover"
            for (ind1 = 1; ind1 < 30; ind1++)
            {
                com2.CommandText = "select * from ttabelaplacas where Rodovia = ";
                com2.CommandText += "'" + strRod + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(objPFe.kmEdital) + "'";
                com2.CommandText += " and MunSen= " + "'" + objPFe.MunSen + "'" + " and S" + ind1.ToString() + "=" + "'" + MunB + "'";
                com2.CommandText += " and stt" + ind1.ToString() + "=" + "'remover'";
                reader2 = com2.ExecuteReader();
                if (reader2.Read())
                {

                    objPlacasIncMunB.placa[ind1] = reader2["placa" + ind1.ToString()].ToString();
                    objPlacasIncMunB.dist[ind1] = Int32.Parse(reader2["dist" + ind1.ToString()].ToString());
                    objPlacasIncMunB.St[ind1] = reader2["S" + ind1.ToString()].ToString();
                    objPlacasIncMunB.cant[ind1] = reader2["ct" + ind1.ToString()].ToString();
                    objPlacasIncMunB.status[ind1] = reader2["stt" + ind1.ToString()].ToString();
                    objPlacasIncMunB.dta[ind1] = Int32.Parse(reader2["dta" + ind1.ToString()].ToString());

                    if ((objPlacasIncMunB.dist[ind1] == 0))
                    {
                        strproj1 = "-(1) Uma " + objPlacasIncMunB.placa[ind1] + " " + objPlacasIncMunB.cant[ind1] + " junto ao ";
                        strproj1 = strproj1 + strDescTipo;
                    }
                    else
                    {
                        strproj1 = "-(1) Uma " + objPlacasIncMunB.placa[ind1] + " " + objPlacasIncMunB.cant[ind1] + " a " + objPlacasIncMunB.dist[ind1] + " metros ";
                        strproj1 = strproj1 + "do" + strDescTipo;
                    }

                    if (!entrou)
                    {
                        // Titulo "remover"
                        strprojTit = "No sentido " + MunA + " / " + MunB + " remover nessa ordem:";
                        objExcel.Agrupar(linTit, 2, linTit , 13);
                        objExcel.MudaFonte(linTit, 2, "TabelaPlacasAB", "Arial", true, 12);
                        objExcel.Pagina(linTit, 2, "TabelaPlacasAB", strprojTit);
                        linTit++;
                    }
                    objExcel.Agrupar(linTit, 2, linTit + 1, 13);
                    objExcel.Pagina(linTit, 2, "TabelaPlacasAB", strproj1, 12, "Black");
                    objExcel.MudaFonte(linTit, 2, "TabelaPlacasAB", "Arial", 12);
                    temOSent = true;
                    linTit = linTit + 3;
                    entrou = true;
                }

                reader2.Close();
            }





            if (entrou)
            {
                // linTit++; linTit++; // Vai para a linha do próximo titulo 
                linTit = linTit + 3;
            }



            objExcel.Agrupar(linTit, 2, linTit, 13);
            strproj1 = "Dp: distância de percepção + distância de reação + distância de frenagem";
            objExcel.Pagina(linTit, 2, "TabelaPlacasAB", strproj1, 11, "Black");
            objExcel.MudaFonte(linTit, 2, "TabelaPlacasAB", "Arial Italic", false, 11);
            linTit++;

            objExcel.Agrupar(linTit, 2, linTit, 13);
            strproj1 = "Dr: Distância de reserva";
            objExcel.Pagina(linTit, 2, "TabelaPlacasAB", strproj1, 11, "Black");
            objExcel.MudaFonte(linTit, 2, "TabelaPlacasAB", "Arial Italic", false, 11);


            
            strproj1 = "24";
            objExcel.Pagina(59, 13, "TabelaPlacasAB", strproj1, 10, "Black");
            objExcel.MudaFonte(59, 13, "TabelaPlacasAB", "Arial", false, 10);













            // Outro sentido  ******************************************************************************
            // Outro sentido  ******************************************************************************
            // Outro sentido  ******************************************************************************
            // Outro sentido  ******************************************************************************
            // Outro sentido  ******************************************************************************



            objExcel.SelecionaPagina("TabelaPlacasBA");



            temOSent = false;


            objExcel.Agrupar(1, 11, 1, 12);
            objExcel.Pagina(1, 11, "TabelaPlacasBA", strRod + "  km " + cUt.ConvPontoVirg(objPFe.kmEdital),10,"Gray");
            //objExcel.MudaFonte(1, 11, "TabelaPlacasBA", "Arial", 11);


            objExcel.Agrupar(6, 2, 6, 13);
            strproj1 = "SENTIDO DECRESCENTE - " + MunB + "/RJ para " + MunA + "/RJ";
            objExcel.Pagina(6, 2, "TabelaPlacasBA", strproj1, 12, "Black");
            objExcel.MudaFonte(6, 2, "TabelaPlacasBA", "Arial", true, 14);


            objExcel.Desagrupar(24, 3);

            // objExcel.Agrupar(24,3,24,13);
            // objExcel.Agrupar(25, 3, 25, 13);


            //VeGeometria(objGeom);
            // VeAcidentes(objAcid);
            // VeHistorico(objHist);




            Veproj(objproj, strRod, objPFe.kmEdital, objPFe.MunSen);

            inc1 = (objproj.inclusao == 1);
            rem1 = (objproj.remocao == 1);
            rep1 = (objproj.reposicionar == 1);
            mant1 = (objproj.manter == 1);


            inc1 = true;            // mecher aqui depois   

            objproj.kmEdit = cUt.ConvPontoVirg(objproj.kmEdit);


            if ((!inc1 && !rem1 && !rep1 && !mant1))
            { strproj1 = "O projeto no trecho estudado, da rodovia " + objproj.Rodovia + " no km " + objproj.kmEdit + " não necessita de inclusão, remoção ou reposicionamento de placa(s) "; }



            if ((!inc1 && !rem1 && !rep1 && mant1))
            {
                strproj1 = "O projeto no trecho estudado, da rodovia " + objproj.Rodovia + " no km " + objproj.kmEdit;
                strproj1 += " não necessita de inclusão, remoção ou reposicionamento de placa(s). ";
                strproj1 += "A sinalização deve ser mantida";

            }

            if ((!inc1 && rem1 && !rep1 && mant1))
            {
                strproj1 = "O projeto no trecho estudado, da rodovia " + objproj.Rodovia + " no km " + objproj.kmEdit;
                strproj1 += " não necessita de inclusão nem reposicionamento de placa(s). ";
                strproj1 += "Uma ou mais placas devem ser removidas e o restante da sinalização deve ser mantida ";

            }



            if ((inc1 && !rem1 && !rep1 && mant1))
            {
                strproj1 = "O projeto no trecho estudado, da rodovia " + objproj.Rodovia + " no km " + objproj.kmEdit;
                strproj1 += " necessita de inclusão de placa(s) e que placa(s) existentes sejam mantidas";


            }


            if ((!inc1 && !rem1 && rep1 && !mant1))
            { strproj1 = "O projeto no trecho estudado, da rodovia " + objproj.Rodovia + " no km " + objproj.kmEdit + " consiste no reposicionamento de placa(s) "; }


            if ((!inc1 && rem1 && !rep1 && !mant1))
            { strproj1 = "O projeto no trecho estudado, da rodovia " + objproj.Rodovia + " no km " + objproj.kmEdit + " consiste na remoção de placa(s) "; }

            if ((!inc1 && rem1 && rep1 && !mant1))
            { strproj1 = "O projeto no trecho estudado, da rodovia " + objproj.Rodovia + " no km " + objproj.kmEdit + " consiste na remoção e reposicionamento de placa(s) "; }


            if ((inc1 && !rem1 && !rep1 && !mant1))
            { strproj1 = "O projeto no trecho estudado, da rodovia " + objproj.Rodovia + " no km " + objproj.kmEdit + " consiste na inclusão de placa(s) "; }

            if ((inc1 && !rem1 && rep1 && !mant1))
            { strproj1 = "O projeto no trecho estudado, da rodovia " + objproj.Rodovia + " no km " + objproj.kmEdit + " consiste na inclusão e reposicionamento de placa(s) "; }

            if ((inc1 && rem1 && !rep1 && !mant1))
            { strproj1 = "O projeto no trecho estudado, da rodovia " + objproj.Rodovia + " no km " + objproj.kmEdit + " consiste na inclusão e remoção de placa(s) "; }

            if ((inc1 && rem1 && rep1 && !mant1))
            { strproj1 = "O projeto no trecho estudado, da rodovia " + objproj.Rodovia + " no km " + objproj.kmEdit + " consiste na inclusão, remoção e reposicionamento de placa(s) "; }


            strproj1 += " para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.";
            //objExcel.Pagina(7, 3, nsheet, objproj.intro1.ToString() + " " + lblRod2.Text + " km " + lblkmR2.Text + ", " + objproj.intro2.ToString());


            objExcel.Agrupar(8, 2, 61, 13);
            objExcel.Pagina(8, 2, "TabelaPlacasBA", " ", 12, "Black");
            objExcel.Desagrupar(8, 2, 61, 13);

            objExcel.Alturalinha("TabelaPlacasBA");

            objExcel.Agrupar(8, 2, 10, 13);
            objExcel.Pagina(8, 2, "TabelaPlacasBA", strproj1, 12, "Black");
            objExcel.MudaFonte(8, 2, "TabelaPlacasBA", "Arial", true, 12);

            linTit = 12;
            strproj1 = "Incluir as seguintes placas para a Dp e Dr antes e após o trecho crítico:";
            objExcel.Agrupar(linTit, 2, linTit, 13);
            objExcel.Pagina(linTit, 2, "TabelaPlacasBA", strproj1, 12, "Black");
            linTit = linTit + 2;

            //              PISTA DUPLA
            //strproj1 = "-(1) Uma placa(s) R19 de 60 km / h " + str2 + "VELOCIDADE MÁXIMA PERMITIDA" + str2;
            //strproj1 += " no canteiro central";
            //strproj1 += " entre 57 a 120 metros como Dp + " + "Uma Dr entre 45 a 60 metros + " + " uma distância de 500 metros até o ";
            //strproj1 += strDescTipo;
            //objExcel.Agrupar(linTit, 2, linTit + 2, 13);
            //objExcel.Pagina(linTit, 2, "TabelaPlacasBA", strproj1, 12, "Black");
            //objExcel.MudaFonte(linTit, 2, "TabelaPlacasBA", "Arial", false, 12);
            //linTit = linTit + 4;

            strproj1 = "-(1) Uma placa(s) R19 de 60 km / h " + str2 + "VELOCIDADE MÁXIMA PERMITIDA" + str2;
            strproj1 += " no canteiro lateral";
            strproj1 += " entre 57 a 120 metros como Dp + " + "Uma Dr entre 45 a 60 metros + " + " uma distância de 500 metros até o ";
            strproj1 += strDescTipo;
            objExcel.Agrupar(linTit, 2, linTit + 2, 13);
            objExcel.Pagina(linTit, 2, "TabelaPlacasBA", strproj1, 12, "Black");
            objExcel.MudaFonte(linTit, 2, "TabelaPlacasBA", "Arial", false, 12);
            linTit = linTit + 4;

            //              PISTA DUPLA
            //strproj1 = "-(1) Uma placa(s) R19 de 50 km / h " + str2 + "VELOCIDADE MÁXIMA PERMITIDA" + str2;
            //strproj1 += " no canteiro central";
            //strproj1 += " entre 45 a 60 metros como Dr + " + " uma distância de 500 metros até o ";
            //strproj1 += strDescTipo;
            //objExcel.Agrupar(linTit, 2, linTit + 1, 13);
            //objExcel.Pagina(linTit, 2, "TabelaPlacasBA", strproj1, 12, "Black");
            //objExcel.MudaFonte(linTit, 2, "TabelaPlacasBA", "Arial", false, 12);
            //linTit = linTit + 3;


            strproj1 = "-(1) Uma placa(s) R19 de 50 km / h " + str2 + "VELOCIDADE MÁXIMA PERMITIDA" + str2;
            strproj1 += " no canteiro lateral";
            strproj1 += " entre 45 a 60 metros como Dr + " + " uma distância de 500 metros até o ";
            strproj1 += strDescTipo;
            objExcel.Agrupar(linTit, 2, linTit + 1, 13);
            objExcel.Pagina(linTit, 2, "TabelaPlacasBA", strproj1, 12, "Black");
            objExcel.MudaFonte(linTit, 2, "TabelaPlacasBA", "Arial", false, 12);
            linTit = linTit + 3;

            //              PISTA DUPLA
            //strproj1 = "-(1) Uma placa(s) R19 de 60 km / h " + str2 + "VELOCIDADE MÁXIMA PERMITIDA" + str2;
            //strproj1 += " no canteiro central";
            //strproj1 += " Após uma distância de 500 metros após o ";
            //strproj1 += strDescTipo;
            //objExcel.Agrupar(linTit, 2, linTit + 1, 13);
            //objExcel.Pagina(linTit, 2, "TabelaPlacasBA", strproj1, 12, "Black");
            //objExcel.MudaFonte(linTit, 2, "TabelaPlacasBA", "Arial", false, 12);
            //linTit = linTit + 3;

            strproj1 = "-(1) Uma placa(s) R19 de 60 km / h " + str2 + "VELOCIDADE MÁXIMA PERMITIDA" + str2;
            strproj1 += " no canteiro lateral";
            strproj1 += " a uma distância de 500 metros após o ";
            strproj1 += strDescTipo;
            objExcel.Agrupar(linTit, 2, linTit + 1, 13);
            objExcel.Pagina(linTit, 2, "TabelaPlacasBA", strproj1, 12, "Black");
            objExcel.MudaFonte(linTit, 2, "TabelaPlacasBA", "Arial", false, 12);
            linTit = linTit + 3;


            temOSent = false;





            entrou = false;
            // Escaneia todas as placas em busca de "incluir"
            for (ind1 = 1; ind1 < 30; ind1++)
            {
                com2.CommandText = "select * from ttabelaplacas where Rodovia = ";
                com2.CommandText += "'" + strRod + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(objPFe.kmEdital) + "'";
                com2.CommandText += " and MunSen= " + "'" + objPFe.MunSen + "'" + " and S" + ind1.ToString() + "=" + "'" + MunA + "'";
                com2.CommandText += " and stt" + ind1.ToString() + "=" + "'incluir'";
                reader2 = com2.ExecuteReader();
                if (reader2.Read())
                {

                    objPlacasIncMunB.placa[ind1] = reader2["placa" + ind1.ToString()].ToString();
                    objPlacasIncMunB.dist[ind1] = Int32.Parse(reader2["dist" + ind1.ToString()].ToString());
                    objPlacasIncMunB.St[ind1] = reader2["S" + ind1.ToString()].ToString();
                    objPlacasIncMunB.cant[ind1] = reader2["ct" + ind1.ToString()].ToString();
                    objPlacasIncMunB.status[ind1] = reader2["stt" + ind1.ToString()].ToString();
                    objPlacasIncMunB.dta[ind1] = Int32.Parse(reader2["dta" + ind1.ToString()].ToString());

                    if ((objPlacasIncMunB.dist[ind1] == 0))
                    {
                        strproj1 = "-(1) Uma " + objPlacasIncMunB.placa[ind1] + " " + objPlacasIncMunB.cant[ind1] + " junto ao ";
                        strproj1 = strproj1 + strDescTipo;
                    }
                    else
                    {
                        strproj1 = "-(1) Uma " + objPlacasIncMunB.placa[ind1] + " " + objPlacasIncMunB.cant[ind1] + " a " + objPlacasIncMunB.dist[ind1] + " metros ";
                        strproj1 = strproj1 + "do" + strDescTipo;
                    }

                    if (!entrou)
                    {
                        //Titulo "Incluir"
                        strprojTit = "No sentido " + MunB + " / " + MunA + " acrescentar nessa ordem:";
                        objExcel.Agrupar(linTit, 2, linTit, 13);
                        objExcel.Pagina(linTit, 2, "TabelaPlacasBA", strprojTit, 12, "Black");
                        objExcel.MudaFonte(linTit, 2, "TabelaPlacasBA", "Arial Italic", true, 12);

                        linTit++;
                    }
                    objExcel.Agrupar(linTit, 2, linTit + 1, 13);
                    objExcel.Pagina(linTit, 2, "TabelaPlacasBA", strproj1, 12, "Black");
                    objExcel.MudaFonte(linTit, 2, "TabelaPlacasBA", "Arial Italic", false, 12);
                    // objExcel.MudaFonte(linTit, 3, nsheet, "Arial", 12);
                    temOSent = true;
                    linTit = linTit + 3;
                    entrou = true;
                }

                reader2.Close();
            }

            if (temOSent)
            {

                //strproj1 = "-(1) Uma pintura de fiscalização eletrônica de velocidade no asfalto a 50 metros do" + strDescTipo;
                //objExcel.MudaFonte(linTit, 3, nsheet, "Arial", false, 12);
                //objExcel.Pagina(linTit, 3, nsheet, strproj1);
                //linTit = linTit + 3;
                //poetit = false;
            }
            else
            {
                //strproj1 = "Não há placas de Fiscalização Eletrônica a acrescentar para esse sentido.";
                //objExcel.MudaFonte(linTit, 3, nsheet, "Arial", 12);
                //objExcel.Pagina(linTit, 3, nsheet, strproj1);
                //linTit = linTit + 3;
                //poetit = false;

            }


            if (entrou)
            {
                linTit++; linTit++; // Vai para a linha do próximo titulo 
            }


            // *************************************************************************************************************************************************




            entrou = false;
            // Escaneia todas as placas em busca de "manter"
            for (ind1 = 1; ind1 < 30; ind1++)
            {
                com2.CommandText = "select * from ttabelaplacas where Rodovia = ";
                com2.CommandText += "'" + strRod + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(objPFe.kmEdital) + "'";
                com2.CommandText += " and MunSen= " + "'" + objPFe.MunSen + "'" + " and S" + ind1.ToString() + "=" + "'" + MunA + "'";
                com2.CommandText += " and stt" + ind1.ToString() + "=" + "'manter'";
                reader2 = com2.ExecuteReader();
                if (reader2.Read())
                {

                    objPlacasIncMunB.placa[ind1] = reader2["placa" + ind1.ToString()].ToString();
                    objPlacasIncMunB.dist[ind1] = Int32.Parse(reader2["dist" + ind1.ToString()].ToString());
                    objPlacasIncMunB.St[ind1] = reader2["S" + ind1.ToString()].ToString();
                    objPlacasIncMunB.cant[ind1] = reader2["ct" + ind1.ToString()].ToString();
                    objPlacasIncMunB.status[ind1] = reader2["stt" + ind1.ToString()].ToString();
                    objPlacasIncMunB.dta[ind1] = Int32.Parse(reader2["dta" + ind1.ToString()].ToString());

                    if ((objPlacasIncMunB.dist[ind1] == 0))
                    {
                        strproj1 = "-(1) Uma " + objPlacasIncMunB.placa[ind1] + " " + objPlacasIncMunB.cant[ind1] + " junto ao ";
                        strproj1 = strproj1 + strDescTipo;
                    }
                    else
                    {
                        strproj1 = "-(1) Uma " + objPlacasIncMunB.placa[ind1] + " " + objPlacasIncMunB.cant[ind1] + " a " + objPlacasIncMunB.dist[ind1] + " metros ";
                        strproj1 = strproj1 + "do" + strDescTipo;
                    }

                    if (!entrou)
                    {
                        // Titulo "Manter"
                        strprojTit = "Manter as placas no sentido " + MunB + " / " + MunA + " nessa ordem:";
                        objExcel.Agrupar(linTit, 2, linTit, 13);
                        objExcel.MudaFonte(linTit, 2, "TabelaPlacasBA", "Arial", true, 12);
                        objExcel.Pagina(linTit, 2, "TabelaPlacasBA", strprojTit);
                        linTit++; linTit++;
                    }
                    objExcel.Agrupar(linTit, 2, linTit + 1, 13);
                    objExcel.Pagina(linTit, 2, "TabelaPlacasBA", strproj1, 12, "Black");
                    objExcel.MudaFonte(linTit, 2, "TabelaPlacasBA", "Arial", false, 12);
                    temOSent = true;
                    linTit = linTit + 3;
                    entrou = true;
                }

                reader2.Close();
            }

            if (temOSent)
            {
                // linTit = linTit - 2;
                strproj1 = "-(1) Uma pintura de fiscalização eletrônica de velocidade no asfalto a 50 metros do" + strDescTipo;
                objExcel.Agrupar(linTit, 2, linTit + 1, 13);
                objExcel.MudaFonte(linTit, 2, "TabelaPlacasBA", "Arial", false, 12);
                objExcel.Pagina(linTit, 2, "TabelaPlacasBA", strproj1);
                linTit = linTit + 3;
                poetit = false;
            }
            else
            {
                //strproj1 = "Não há placas de Fiscalização Eletrônica a acrescentar para esse sentido.";
                //objExcel.MudaFonte(linTit, 3, nsheet, "Arial", 12);
                //objExcel.Pagina(linTit, 3, nsheet, strproj1);
                //linTit = linTit + 3;
                //poetit = false;

            }

            if (entrou)
            {
                linTit++; linTit++; // Vai para a linha do próximo titulo 
            }





            entrou = false;
            // Escaneia todas as placas em busca de "remover"
            for (ind1 = 1; ind1 < 12; ind1++)
            {
                com2.CommandText = "select * from ttabelaplacas where Rodovia = ";
                com2.CommandText += "'" + strRod + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(objPFe.kmEdital) + "'";
                com2.CommandText += " and MunSen= " + "'" + objPFe.MunSen + "'" + " and S" + ind1.ToString() + "=" + "'" + MunA + "'";
                com2.CommandText += " and stt" + ind1.ToString() + "=" + "'remover'";
                reader2 = com2.ExecuteReader();
                if (reader2.Read())
                {

                    objPlacasIncMunB.placa[ind1] = reader2["placa" + ind1.ToString()].ToString();
                    objPlacasIncMunB.dist[ind1] = Int32.Parse(reader2["dist" + ind1.ToString()].ToString());
                    objPlacasIncMunB.St[ind1] = reader2["S" + ind1.ToString()].ToString();
                    objPlacasIncMunB.cant[ind1] = reader2["ct" + ind1.ToString()].ToString();
                    objPlacasIncMunB.status[ind1] = reader2["stt" + ind1.ToString()].ToString();
                    objPlacasIncMunB.dta[ind1] = Int32.Parse(reader2["dta" + ind1.ToString()].ToString());

                    if ((objPlacasIncMunB.dist[ind1] == 0))
                    {
                        strproj1 = "-(1) Uma " + objPlacasIncMunB.placa[ind1] + " " + objPlacasIncMunB.cant[ind1] + " junto ao ";
                        strproj1 = strproj1 + strDescTipo;
                    }
                    else
                    {
                        strproj1 = "-(1) Uma " + objPlacasIncMunB.placa[ind1] + " " + objPlacasIncMunB.cant[ind1] + " a " + objPlacasIncMunB.dist[ind1] + " metros ";
                        strproj1 = strproj1 + "do" + strDescTipo;
                    }

                    if (!entrou)
                    {
                        // Titulo "remover"
                        strprojTit = "No sentido " + MunB + " / " + MunA + " remover nessa ordem:";
                        objExcel.Agrupar(linTit, 2, linTit, 13);
                        objExcel.MudaFonte(linTit, 2, "TabelaPlacasBA", "Arial", true, 12);
                        objExcel.Pagina(linTit, 2, "TabelaPlacasBA", strprojTit);
                        linTit++;
                    }
                    objExcel.Agrupar(linTit, 2, linTit + 1, 13);
                    objExcel.Pagina(linTit, 2, "TabelaPlacasBA", strproj1, 12, "Black");
                    objExcel.MudaFonte(linTit, 2, "TabelaPlacasBA", "Arial", 12);
                    temOSent = true;
                    linTit = linTit + 3;
                    entrou = true;
                }

                reader2.Close();
            }





            if (entrou)
            {
               // linTit++; linTit++; // Vai para a linha do próximo titulo 
                linTit = linTit + 3;
            }



            objExcel.Agrupar(linTit, 2, linTit, 13);
            strproj1 = "Dp: distância de percepção + distância de reação + distância de frenagem";
            objExcel.Pagina(linTit, 2, "TabelaPlacasBA", strproj1, 11, "Black");
            objExcel.MudaFonte(57, 2, "TabelaPlacasBA", "Arial", false, 11);
            linTit++;

            objExcel.Agrupar(linTit, 2, linTit, 13);
            strproj1 = "Dr: Distância de reserva";
            objExcel.Pagina(linTit, 2, "TabelaPlacasBA", strproj1, 11, "Black");
            objExcel.MudaFonte(linTit, 2, "TabelaPlacasBA", "Arial", false, 11);



            
            strproj1 = "25";
            objExcel.Pagina(59, 13, "TabelaPlacasBA", strproj1, 10, "Black");
            objExcel.MudaFonte(59, 13, "TabelaPlacasBA", "Arial", false, 10);







            SetaPaginas(ObjPags, nsheet, strRod, objPFe.kmEdital, objPFe.MunSen);

        }





        private void SetaPaginas(C_paginas objpgs, int ind1, string strRod, string okm, string oSent)
        {
            objpgs.pagina[ind1] = true;

            com2.CommandText = "update tpaginas set p" + ind1.ToString() + " = 1 where Rodovia = " + "'" + strRod + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(okm) + "'";
            com2.CommandText = com2.CommandText + " and MunSen= " + "'" + oSent + "'";
            com2.ExecuteNonQuery();
        }




    }
}
