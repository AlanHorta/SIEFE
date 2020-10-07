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
    public partial class FrmParametros : Form
    {
        Uteis cUt = new Uteis();
        C_PistasFaixas CPtFx = new C_PistasFaixas();
        C_Geometria objGeom = new C_Geometria();
        c_acidentes objAcid = new c_acidentes();
        C_risco objRisco = new C_risco();
        C_historico objHist = new C_historico();
        C_proj objproj = new C_proj();
        C_jorn ObjJorn = new C_jorn();
        C_paginas ObjPags = new C_paginas();

        C_TemFE objTemFE = new C_TemFE();


        MySqlConnection mysqlCon2;
        MySqlCommand com2;
        public MySqlCommand comcfc2;
        MySqlDataReader reader2;

       



        //   Form1 objfIni1 = new Form1();


        MySqlConnection mysqlCon1;
        MySqlCommand com1;
        MySqlDataReader reader1;

        Boolean bEDuplo = false;     // true se o aparelho é do tipo I que pega nos dois sentidos
        Boolean EUmSentido = false; // true se o ponto de FE é fiscalizado nos dois sentidos
        int Vel85pSB = 0;
        int VmdB = 0;               // o Vmd do sentido B se houver;
        string strConFE = @"Server=localhost;Database=fe;Uid=root;Pwd=obuldog67;";
        public string strConcfc = @"Server=localhost;Database=classfic;Uid=root;Pwd=obuldog67;";

        string NovoRisco = "";
        string RemoveRisco = "";


        public MySqlConnection MysqlConClf1;
        public MySqlCommand comcfc1;
        public MySqlDataReader readercfc1;




        LinkedList<double> list85p = new LinkedList<double>();


        public FrmParametros()
        {
            InitializeComponent();
        }





        private void FrmParametros_Load(object sender, EventArgs e)
        {
            

            ConectaBanco();

            Boolean bEDuplo = false;     // true se o aparelho é do tipo I que pega nos dois sentidos
            Boolean EUmSentido = false; // true se o ponto de FE é fiscalizado nos dois sentidos
            int Vel85pSB = 0;
            int VmdB = 0;               // o Vmd do sentido B se houver;

                

            com1.CommandText = "select * from ponto_featual";  //  ponto_featual só pode ter um único registro por vez

            reader1 = com1.ExecuteReader();

            while (reader1.Read())
            {
                //Console.WriteLine(reader1["Tipo"].ToString());
                lblRod2.Text = reader1["Rodovia"].ToString();
                lblSent2.Text = reader1["MunSen"].ToString();
                lblkmEdt2.Text = reader1["kmEdital"].ToString();
                lblkmR2.Text = reader1["kmReal"].ToString();
                lblMun2.Text = reader1["Municipio"].ToString();
                lblLoc2.Text = reader1["Localidade"].ToString();
                lblQtdFx2.Text = reader1["QtdFx"].ToString();
                lblMA2.Text = reader1["MunA"].ToString();
                lblMB2.Text = reader1["MunB"].ToString();
                lblVF2.Text = reader1["VelFisc"].ToString();
                lblLat2.Text = reader1["Lat"].ToString();
                lblLong2.Text = reader1["Longit"].ToString();
                lblVmd2.Text = reader1["VMD"].ToString();
                lblV852.Text = reader1["Vel85p"].ToString();
                lblTipo2.Text = reader1["Tipo"].ToString();

                
                if (    (reader1["Lat2"].ToString()!="zero" )  && (reader1["Longit2"].ToString()!="zero" )    )
                    {
                      ckBEDuplo.Checked = false;
                      ckBEDuplo.Enabled = false; 
                    }

                else { ckBEDuplo.Enabled = true; }



                if (Int16.Parse((reader1["Calc85p"]).ToString()) == 1)

                    { checkBox85p.Checked = true; }
                    else
                    { checkBox85p.Checked = false; }
                

            }





            reader1.Close();

            VeRiscos(objRisco);
            CarregaRiscos(objRisco);

            VeHist(objHist);
            CarregaHistorico(objHist);


            VerAcidentes(objAcid);
            CarregaAcidentes(objAcid);

            VerJornalistico(ObjJorn);
            CarregaJornalistico(ObjJorn);

            VerFaixas(CPtFx);
            CarregaFaixas(CPtFx);

            VerGeometria(objGeom);
            CarregaGeometria(objGeom);

            VeTemFE(objTemFE);
            CarregaTemFE(objTemFE);

        }





        private void ConectaBanco()
        {

            using (mysqlCon1 = new MySqlConnection(strConFE));
            {
                mysqlCon1.Open();
                com1 = mysqlCon1.CreateCommand();

                //MessageBox.Show("Teste conecta e fecha banco OK!");

            }
        }


        private void DesconectaBanco()
        {

            using (mysqlCon1 = new MySqlConnection(strConFE)) ;
            {
                mysqlCon1.Close();

                //MessageBox.Show("Teste conecta e fecha banco OK!");

            }
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





        // *************************************************
        public void ConectaBancoClassfic()
        {

            using (MysqlConClf1 = new MySqlConnection(strConcfc)) ;
            {
                MysqlConClf1.Open();
                comcfc1 = MysqlConClf1.CreateCommand();


            }

        }




        // ************************************************

        public void DesconectaBancoClassfic()
        {
            MysqlConClf1.Close();
        }
        // ****************************************************







        // ****************************************************





        private void btFechar_Click(object sender, EventArgs e)
        {
            DesconectaBanco();
            ////fPlacas.Close();
            this.Close();
        }



        // *************************************************
        private void CarregaHistorico(C_historico objhist)
        {

            // LstVRisco.Columns.Add("Fator de Risco", 1000, HorizontalAlignment.Left);
            //LstVRisco.Columns.Add("Fator de Risco");
            cmbHist.Items.Clear();
            com1.CommandText = "select * from ttipo_historico";

            reader1 = com1.ExecuteReader();

            while (reader1.Read())
            {
                cmbHist.Items.Add((reader1["hist"]).ToString());
            }
            reader1.Close();

        }

        // *************************************************
        private void CarregaRiscos(C_risco objRiscos)
        {

            // LstVRisco.Columns.Add("Fator de Risco", 1000, HorizontalAlignment.Left);
            //LstVRisco.Columns.Add("Fator de Risco");
            cmbFRisco.Items.Clear();
            com1.CommandText = "select * from ttipo_fat_risco";

            reader1 = com1.ExecuteReader();

            while (reader1.Read())
            {                
                cmbFRisco.Items.Add((reader1["fatr"]).ToString());
            }
            reader1.Close();

        }

        //***********************************************
       

        private void VeRiscos(C_risco objRiscos)
        {
            int QtItens = 0;
            QtItens = LstVRisco.Items.Count;

            ColumnHeader header1;
            header1 = new ColumnHeader();
            QtItens = LstVRisco.Items.Count;

            LstVRisco.Items.Clear();
            QtItens = LstVRisco.Items.Count;

            // LstVRisco.Columns.Add("Fator de Risco", 1000, HorizontalAlignment.Left);
            //LstVRisco.Columns.Add("Fator de Risco");
            LstVRisco.View = View.Details;
            LstVRisco.Width = 305;
            //header1.Text = "Fator de Risco";
            header1.Text = "";
            header1.TextAlign = HorizontalAlignment.Left;
            header1.Width = 299;
            QtItens = LstVRisco.Items.Count;
            LstVRisco.Columns.Add(header1);

            QtItens = LstVRisco.Items.Count;

            com1.CommandText = "select * from fat_risco where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'";
            com1.CommandText = com1.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";

            reader1 = com1.ExecuteReader();

            if (reader1.Read())
            {
               // objRiscos.Rodovia = "sss";
                objRiscos.Rodovia = (reader1["Rodovia"]).ToString().Trim();
                objRiscos.kmEdit = ((reader1["kmEdital"]).ToString());
                objRiscos.MunSen = (reader1["MunSen"]).ToString();

                QtItens = LstVRisco.Items.Count;
                objRiscos.fatr1 = (reader1["fatr1"]).ToString().Trim();
                if (objRiscos.fatr1.Trim()!="") { LstVRisco.Items.Add(objRiscos.fatr1); }

                QtItens = LstVRisco.Items.Count;
                objRiscos.fatr2 = (reader1["fatr2"]).ToString().Trim();
                if (objRiscos.fatr2.Trim() != "") { LstVRisco.Items.Add(objRiscos.fatr2); }

                objRiscos.fatr3 = (reader1["fatr3"]).ToString().Trim();
                if (objRiscos.fatr3.Trim() != "") { LstVRisco.Items.Add(objRiscos.fatr3); }

                objRiscos.fatr4 = (reader1["fatr4"]).ToString().Trim();
                if (cUt.RegValido(objRiscos.fatr4)) { LstVRisco.Items.Add(objRiscos.fatr4); }

                objRiscos.fatr5 = (reader1["fatr5"]).ToString().Trim();
                if (cUt.RegValido(objRiscos.fatr5)) { LstVRisco.Items.Add(objRiscos.fatr5); }

                objRiscos.fatr6 = (reader1["fatr6"]).ToString().Trim();
                if (objRiscos.fatr6 != "") { LstVRisco.Items.Add(objRiscos.fatr6); }

                objRiscos.fatr7 = (reader1["fatr7"]).ToString().Trim();
                if (objRiscos.fatr7 != "") { LstVRisco.Items.Add(objRiscos.fatr7); }

                objRiscos.fatr8 = (reader1["fatr8"]).ToString().Trim();
                if (objRiscos.fatr8 != "") { LstVRisco.Items.Add(objRiscos.fatr8); }

                objRiscos.fatr9 = (reader1["fatr9"]).ToString().Trim();
                if (objRiscos.fatr9 != "") { LstVRisco.Items.Add(objRiscos.fatr9); }

                objRiscos.fatr10 = (reader1["fatr10"]).ToString().Trim();
                if (objRiscos.fatr10 != "") { LstVRisco.Items.Add(objRiscos.fatr10); }

                LstVRisco.View = View.Details;
                                             
            }

            reader1.Close();

        }


        //***********************************************


        private void VeHist(C_historico objHist)
        {
            int QtItens = 0;
            int nColun = 0;

            QtItens = LstHist.Items.Count;
            nColun = LstHist.Columns.Count;
            ColumnHeader header2;
            header2 = new ColumnHeader();
            QtItens = LstHist.Items.Count;

            LstHist.Items.Clear();
            QtItens = LstHist.Items.Count;

            // LstVRisco.Columns.Add("Fator de Risco", 1000, HorizontalAlignment.Left);
            //LstVRisco.Columns.Add("Fator de Risco");
            LstHist.View = View.Details;
            LstHist.Width = 591;
            //header1.Text = "Fator de Risco";
            header2.Text = "";
            header2.TextAlign = HorizontalAlignment.Left;
            header2.Width = 585;
            QtItens = LstHist.Items.Count;
            LstHist.Columns.Add(header2);

            QtItens = LstHist.Items.Count;

            com1.CommandText = "select * from histmedidas where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'";
            com1.CommandText = com1.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";

            reader1 = com1.ExecuteReader();

            if (reader1.Read())
            {
                // objHist.Rodovia = "sss";
                objHist.Rodovia = (reader1["Rodovia"]).ToString();
                objHist.kmEdit = ((reader1["kmEdital"]).ToString());
                objHist.MunSen = (reader1["MunSen"]).ToString();

                QtItens = LstHist.Items.Count;
                objHist.h1 = (reader1["h1"]).ToString().Trim();
                if (objHist.h1 != "") { LstHist.Items.Add(objHist.h1); }

                QtItens = LstHist.Items.Count;
                objHist.h2 = (reader1["h2"]).ToString().Trim();
                if (objHist.h2 != "") { LstHist.Items.Add(objHist.h2); }

                objHist.h3 = (reader1["h3"]).ToString().Trim();
                if (objHist.h3 != "") { LstHist.Items.Add(objHist.h3); }

                objHist.h4 = (reader1["h4"]).ToString().Trim();
                if (cUt.RegValido(objHist.h4)) { LstHist.Items.Add(objHist.h4); }

                objHist.h5 = (reader1["h5"]).ToString().Trim();
                if (cUt.RegValido(objHist.h5)) { LstHist.Items.Add(objHist.h5); }


                LstHist.View = View.Details;

            }

            reader1.Close();

        }


        // ***************************************************************************************************


        private void cmbFRisco_SelectedIndexChanged(object sender, EventArgs e)
        {


            //com1.CommandText = "insert into ponto_featual (Rodovia, kmEdital, MunSen, kmReal, Localidade, Municipio, QtdFx, MunA, MunB, VelFisc, Lat, Longit, VMD, Vel85p, Tipo) ";
            //com1.CommandText = com1.CommandText + "Values ('R', 7, 'Itaperuna', 7, 'Sambaetiba', 'Itaboraí', '2', 'Itaboraí', 'Itaperuna', 50, '22°40 54.39 S', '42°46 42.76 O',4445,64,'I.A')";
            //            com1.ExecuteNonQuery();


            if (VeSeExisteListaRisco(cmbFRisco.Text)==false)
            { LstVRisco.Items.Add(cmbFRisco.Text);
                NovoRisco = cmbFRisco.Text;
            }
        }

        //********************************************************
                
        private Boolean VeSeExisteListaHist(string str1)
        {

            int nriscos = 0;
            int ind1 = 0;
            nriscos = LstHist.Items.Count;

            for (ind1 = 1; ind1 < nriscos; ind1++)
            {
                if (LstHist.Items[ind1].Text == str1)
                { return true; }
            }
            return false;
        }

        //********************************************************


        private Boolean VeSeExisteListaRisco(string str1)
        {
            
            int nriscos = 0;
            int ind1 = 0;
            nriscos=LstVRisco.Items.Count;

            for (ind1=1;ind1<nriscos;ind1++)
            {
                if (LstVRisco.Items[ind1].Text == str1)
                { return true; }
            }
            return false;
        }
        //*********************************************


        private void btInclui_Click(object sender, EventArgs e)
        {

            int nriscos = 0;
            int ind1 = 0;
            int indftr = 0;
            string sfatr = "";
            nriscos = (LstVRisco.Items.Count);
            //string sql = "UPDATE CLIENTE SET NOME=@Nome, ENDERECO=@Endereco, CEP=@Cep, BAIRRO=@Bairro, " +
            //     "CIDADE=@Cidade, UF=@Uf, TELEFONE=@Telefone WHERE ID=@Id";

            sfatr = LstVRisco.Items[0].Text;
            sfatr = "";

            for (ind1 = 1; ind1 <= nriscos; ind1++)
            {
                if (ind1<10) { sfatr = sfatr + "fatr" + ind1.ToString() + "='" + LstVRisco.Items[ind1-1].Text + "', "; }                               
                else                
                    { sfatr = sfatr + "fatr" + ind1.ToString() + "='" + LstVRisco.Items[ind1-1].Text + "'"; }
            }

            for (ind1 = (nriscos+1); ind1 <= 10; ind1++)
            {
                if (ind1 < 10) { sfatr = sfatr + "fatr" + ind1.ToString() + "='" + "', "; }
                else
                { sfatr = sfatr + "fatr" + ind1.ToString() + "='" + "'"; }
            }

                indftr = nriscos;
           
            if (indftr<=10)
            {
                com1.CommandText = "update fat_risco set " + sfatr;
                com1.CommandText = com1.CommandText + " where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital= " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'";
                com1.CommandText = com1.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'"; 
            }

            com1.ExecuteNonQuery();
            //com1.CommandText = com1.CommandText + "Values ('R', 7, 'Itaperuna', 7, 'Sambaetiba', 'Itaboraí', '2', 'Itaboraí', 'Itaperuna', 50, '22°40 54.39 S', '42°46 42.76 O',4445,64,'I.A')";
            //            com1.ExecuteNonQuery();

        }

        private void LstVRisco_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            foreach (ListViewItem eachItem in
                   this.LstVRisco.SelectedItems)
            {
                this.LstVRisco.Items.Remove(eachItem);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void cmbHist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (VeSeExisteListaHist(cmbHist.Text) == false)
            {
                LstHist.Items.Add(cmbHist.Text);
                //NovoHist = cmbHist.Text;
            }
        }


        //*************************************************************************

        private void btHist_Click(object sender, EventArgs e)
        {
            int tam = 0;
            int nhist = 0;
            int ind1 = 0;
            int indhst = 0;
            string shist = "";
            nhist = (LstHist.Items.Count);
            //string sql = "UPDATE CLIENTE SET NOME=@Nome, ENDERECO=@Endereco, CEP=@Cep, BAIRRO=@Bairro, " +
            //     "CIDADE=@Cidade, UF=@Uf, TELEFONE=@Telefone WHERE ID=@Id";

            shist = LstHist.Items[0].Text;
            shist = "";

            for (ind1 = 1; ind1 <= nhist; ind1++)
            {
                 shist = shist + "h" + ind1.ToString() + "='" + LstHist.Items[ind1 - 1].Text + "', ";
                //if (ind1 < 5) { shist = shist + "h" + ind1.ToString() + "='" + LstHist.Items[ind1 - 1].Text + "', "; }
                //else
                //{ shist = shist + "h" + ind1.ToString() + "='" + LstHist.Items[ind1 - 1].Text + "'"; }
            }
            
            tam = shist.Length;
            shist = shist.Substring(0, tam - 2); // tira a ultima virgula indesejada


            
            if (nhist < 5)  
            {               
                for (ind1= nhist+1; ind1<=5;ind1++)
                { shist = shist + ", h" + ind1.ToString() + "='" + "'"; }
            }
            //for (ind1 = (nhist + 1); ind1 <= 6; ind1++)
            //{
            //    if (ind1 < 6) { shist = shist + "h" + ind1.ToString() + "='" + "', "; }
            //    else
            //    { shist = shist + "h" + ind1.ToString() + "='" + "'"; }
            //}

            indhst = nhist;

           
                com1.CommandText = "update histmedidas set " + shist;
                com1.CommandText = com1.CommandText + " where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital= " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'";
                com1.CommandText = com1.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";
           

            com1.ExecuteNonQuery();
            //com1.CommandText = com1.CommandText + "Values ('R', 7, 'Itaperuna', 7, 'Sambaetiba', 'Itaboraí', '2', 'Itaboraí', 'Itaperuna', 50, '22°40 54.39 S', '42°46 42.76 O',4445,64,'I.A')";
            //            com1.ExecuteNonQuery();


        }

        private void LstHist_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem eachItem in
                  this.LstHist.SelectedItems)
            {
                this.LstHist.Items.Remove(eachItem);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        //*******************************************************************************
        ////FrmPlacas fPlacas = new FrmPlacas();
        private void btPLFE_Click(object sender, EventArgs e)
        {
            FrmPlacas fPlacas = new FrmPlacas();
            fPlacas.ShowDialog();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        //*******************************************************************************
         private void VerAcidentes (c_acidentes objacid)
        {
            com1.CommandText= "select * from acidentes where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'";
            com1.CommandText = com1.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";

            reader1 = com1.ExecuteReader();

            if (reader1.Read())
            {
                objacid.kmreal = (reader1["kmReal"].ToString());

                objacid.abalroamento = Int32.Parse(reader1["Abalroamento"].ToString());
                objacid.choque = Int32.Parse(reader1["Choque"].ToString());
                objacid.colisao = Int32.Parse(reader1["Colisão"].ToString());
                objacid.tombamento = Int32.Parse(reader1["Tombamento"].ToString());
                objacid.capotamento = Int32.Parse(reader1["Capotamento"].ToString());
                objacid.incendio = Int32.Parse(reader1["Incendio"].ToString());
                objacid.atropelamento = Int32.Parse(reader1["Atropelamento"].ToString());

            }

            reader1.Close();
        }


        //*******************************************************************************
        private void VerJornalistico(C_jorn objJorn)
        {
            com1.CommandText = "select * from tjorn where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'";
            com1.CommandText = com1.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";

            reader1 = com1.ExecuteReader();

            if (reader1.Read())
            {  objJorn.jorn1 = (reader1["semjorn"].ToString());  }

            reader1.Close();


        }

        //*******************************************************************************
        private void CarregaJornalistico(C_jorn objJorn)
        {
            if (objJorn.jorn1 == "Sem material jornalístico")
            {
                chkbxJorn.Checked = true;
            }
                       
        }
        
        //**********************************************************************

        private void CarregaAcidentes(c_acidentes objacid)
        {
            int ind1 = 0;

            cmbAbal.Items.Clear();
            cmbChoque.Items.Clear();
            cmbColisao.Items.Clear();
            cmbTomb.Items.Clear();
            cmbCapot.Items.Clear();
            cmbInc.Items.Clear();
            cmbAtrop.Items.Clear();

            for (ind1=0;ind1<=20;ind1++)
            {
                cmbAbal.Items.Add(ind1);
                cmbChoque.Items.Add(ind1);
                cmbColisao.Items.Add(ind1);
                cmbTomb.Items.Add(ind1);
                cmbCapot.Items.Add(ind1);
                cmbInc.Items.Add(ind1);
                cmbAtrop.Items.Add(ind1);
            }

            cmbAbal.Text = objAcid.abalroamento.ToString();
            cmbChoque.Text = objAcid.choque.ToString();
            cmbColisao.Text = objAcid.colisao.ToString();
            cmbTomb.Text = objAcid.tombamento.ToString();
            cmbCapot.Text = objAcid.capotamento.ToString();
            cmbInc.Text = objAcid.incendio.ToString();
            cmbAtrop.Text = objAcid.atropelamento.ToString();
        }


        //*******************************************************************************]
       private void VerFaixas(C_PistasFaixas objPfx)
        {
            com1.CommandText = "select * from teduplo where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'";
            com1.CommandText = com1.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";

            reader1 = com1.ExecuteReader();

            if (reader1.Read())
            {


                if (reader1["EDuplo"].ToString() != null)
                {
                    if (Int32.Parse(reader1["EDuplo"].ToString()) == 1)
                    { objPfx.bEduplo = true; }
                    else
                    { objPfx.bEduplo = false; }
                }
                else
                { objPfx.bEduplo = false; }



                if ( reader1["MunSen2"].ToString()!= null )
                         { objPfx.MunSen2 = reader1["MunSen2"].ToString(); }
                     else
                         { objPfx.MunSen2 = "";}

                objPfx.NPistas = Int32.Parse(reader1["NPistas"].ToString());
                objPfx.NFaixas = Int32.Parse(reader1["NFxs"].ToString());
                objPfx.Fx1_1 = Int32.Parse(reader1["Fx1_1"].ToString());
                objPfx.Fx1_2 = Int32.Parse(reader1["Fx1_2"].ToString());
                objPfx.Fx1_3 = Int32.Parse(reader1["Fx1_3"].ToString());
                objPfx.Fx2_1 = Int32.Parse(reader1["Fx2_1"].ToString());
                objPfx.Fx2_2 = Int32.Parse(reader1["Fx2_2"].ToString());
                objPfx.Fx2_3 = Int32.Parse(reader1["Fx2_3"].ToString());

                
            }

            reader1.Close();

        }


        //***************************************************************

        private void CarregaFaixas(C_PistasFaixas objPfx)
            {
            int ind1 = 0;

              ckBEDuplo.Checked = objPfx.bEduplo;


            for(ind1=0;ind1<=4;ind1++)
            {
                cmbFx1_1.Items.Add(ind1);
                cmbFx1_2.Items.Add(ind1);
                cmbFx1_3.Items.Add(ind1);

                cmbFx2_1.Items.Add(ind1);
                cmbFx2_2.Items.Add(ind1);
                cmbFx2_3.Items.Add(ind1);

                cmbNfx.Items.Add(ind1);
                cmbNP.Items.Add(ind1);
            }


            cmbFx1_1.Text = objPfx.Fx1_1.ToString();
            cmbFx1_2.Text = objPfx.Fx1_2.ToString();
            cmbFx1_3.Text = objPfx.Fx1_3.ToString();

            cmbFx2_1.Text = objPfx.Fx2_1.ToString();
            cmbFx2_2.Text = objPfx.Fx2_2.ToString();
            cmbFx2_3.Text = objPfx.Fx2_3.ToString();

            cmbNfx.Text = objPfx.NFaixas.ToString();
            cmbNP.Text = objPfx.NPistas.ToString();
        }
        //***************************************************************

        private void VerGeometria(C_Geometria objgeom)
        {
            com1.CommandText = "select * from tgeometria where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'";
            com1.CommandText = com1.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";

            reader1 = com1.ExecuteReader();

            if (reader1.Read())
            {

                objgeom.aclive = Int32.Parse(reader1["Aclive"].ToString());
                objgeom.declive = Int32.Parse(reader1["Declive"].ToString());
                objgeom.plano = Int32.Parse(reader1["Plano"].ToString());
                objgeom.curva = Int32.Parse(reader1["Curva"].ToString());
                objgeom.urbano = Int32.Parse(reader1["Urbano"].ToString());
                objgeom.pedestre = Int32.Parse(reader1["Pedestre"].ToString());
                objgeom.paolongo = Int32.Parse(reader1["Paolongo"].ToString());
                objgeom.ptrans = Int32.Parse(reader1["Ptrans"].ToString());
                objgeom.ciclista = Int32.Parse(reader1["Ciclista"].ToString());
                objgeom.caolongo = Int32.Parse(reader1["Caolongo"].ToString());
                objgeom.ctrans = Int32.Parse(reader1["Ctrans"].ToString());
                objgeom.umsent = Int32.Parse(reader1["UmSentido"].ToString());
                objgeom.sa = reader1["SA"].ToString();
                objgeom.sb = reader1["SB"].ToString();
                objgeom.forma = reader1["Forma"].ToString();

            }
            reader1.Close();

           
        }

      //*************************************************************
        private void CarregaGeometria(C_Geometria objgeom)
        {

            if (objgeom.aclive == 1) { ckBAclive.Checked = true; } else { ckBAclive.Checked = false; }
            if (objgeom.declive == 1) { ckBDeclive.Checked = true; } else { ckBDeclive.Checked = false; }
            if (objgeom.plano == 1) { ckBPlano.Checked = true; } else { ckBPlano.Checked = false; }
            if (objgeom.curva == 1) { ckBCurva.Checked = true; } else { ckBCurva.Checked = false; }
            if (objgeom.urbano == 1) { ckBUrbano.Checked = true; } else { ckBUrbano.Checked = false; }
           
            // if (objgeom.pedestre == 1) { ckBPedestre.Checked = true; } else { ckBPedestre.Checked = false; }
            if (objgeom.paolongo == 1) { ckBxPedAl.Checked = true; } else { ckBxPedAl.Checked = false; }
            if (objgeom.ptrans == 1) { ckBxPedTrans.Checked = true; } else { ckBxPedTrans.Checked = false; }
           // if (objgeom.ciclista == 1) { CkBCicli.Checked = true; } else { CkBCicli.Checked = false; }
            if (objgeom.caolongo == 1) { ckBxCicAl.Checked = true; } else { ckBxCicAl.Checked = false; }
            if (objgeom.ctrans == 1) { ckBxCictrans.Checked = true; } else { ckBxCictrans.Checked = false; }

            if (objgeom.umsent == 1) { ckBUmSent.Checked = true; } else { ckBUmSent.Checked = false; }
            if (objgeom.forma=="paisagem") { rdPaisagem.Checked = true; } else { rdRet.Checked = true; }

            cmbSA.Items.Clear();
            cmbSB.Items.Clear();

            com1.CommandText = "select * from trodovias where Rodovia = " + "'" + lblRod2.Text + "'";

            reader1 = com1.ExecuteReader();

            if (reader1.Read())
            {
                cmbSA.Items.Add(reader1["MunA"].ToString());
                cmbSB.Items.Add(reader1["MunB"].ToString());
            }
            reader1.Close();

          
            cmbSA.Text = objgeom.sa;
            cmbSB.Text = objgeom.sb;

        }



        //**********************************************************
        private void VeTemFE(C_TemFE objTemFE)
        {
            com1.CommandText = "select * from ttemfe where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'";
            com1.CommandText = com1.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";

            reader1 = com1.ExecuteReader();

            if (reader1.Read())
            {
                
                objTemFE.VaiTer = Int32.Parse(reader1["vaiterfe"].ToString());
                objTemFE.JaTinha = Int32.Parse(reader1["jatinhafe"].ToString());
                objTemFE.Confirma = Int32.Parse(reader1["foiconf"].ToString());

                objTemFE.TemQB = Int32.Parse(reader1["temQB"].ToString());

                objTemFE.Manter = Int32.Parse(reader1["manter"].ToString());
                objTemFE.Remover = Int32.Parse(reader1["remover"].ToString());

                objTemFE.Subst = Int32.Parse(reader1["subst"].ToString());
                objTemFE.Proj = Int32.Parse(reader1["proj"].ToString());

                objTemFE.Renova = Int32.Parse(reader1["renova"].ToString());
                objTemFE.Altera = Int32.Parse(reader1["altera"].ToString());

            }

            reader1.Close();
                  
        }





        //***************************************************
        private void CarregaTemFE(C_TemFE objTemFE)
        {
            if (objTemFE.VaiTer == 1) { ckBxVaiTer.Checked = true; } else { ckBxVaiTer.Checked = false; }
            if (objTemFE.JaTinha == 1) { ckBxJaTinha.Checked = true; } else { ckBxJaTinha.Checked = false; }
            if (objTemFE.Confirma == 1) { ckBxConfirma.Checked = true; } else { ckBxConfirma.Checked = false; }

            if (objTemFE.TemQB == 1) { ckTemQM.Checked = true; panel11.Enabled = true; panel12.Enabled = true; }
            else { ckTemQM.Checked = false; panel11.Enabled = false; panel12.Enabled = false; }

            if (objTemFE.Manter == 1) { rdManter.Checked = true; } else { rdManter.Checked = false; }
            if (objTemFE.Remover == 1) { rdRemover.Checked = true; } else { rdRemover.Checked = false; }

            if (objTemFE.Subst == 1) { rdSubst.Checked = true; } else { rdSubst.Checked = false; }
            if (objTemFE.Proj == 1) { rdDeProj.Checked = true; } else { rdDeProj.Checked = false; }


            if (objTemFE.Renova == 1) {chkRen.Checked = true; } else { chkRen.Checked = false; }


        }


        // **************************************************


        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                com1.CommandText = "update acidentes set ";
                com1.CommandText = com1.CommandText + "Abalroamento=" + cmbAbal.Text + ", Choque=" + cmbChoque.Text + ", Colisão=" + cmbColisao.Text;
                com1.CommandText = com1.CommandText + ", Tombamento=" + cmbTomb.Text + ", Capotamento=" + cmbCapot.Text + ", Incendio=" + cmbInc.Text + ", Atropelamento=" + cmbAtrop.Text;
                com1.CommandText = com1.CommandText + " where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital= " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'";
                com1.CommandText = com1.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";

                com1.ExecuteNonQuery();
            }

            catch { MessageBox.Show("Não foi possível gravar acidentes!"); }


            if (chkbxJorn.Checked)
            {
                try
                {
                    com1.CommandText = "update tjorn set ";
                    com1.CommandText = com1.CommandText + "semjorn=" + "'Sem material jornalístico'";
                    com1.CommandText = com1.CommandText + " where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital= " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'"; 
                    com1.CommandText = com1.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";

                    com1.ExecuteNonQuery();
                }

                catch { MessageBox.Show("Não foi possível gravar material jornalístico!"); }

            }

            else
            {
                try
                {
                    com1.CommandText = "update tjorn set ";
                    com1.CommandText = com1.CommandText + "semjorn=" + "''";
                    com1.CommandText = com1.CommandText + " where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital= " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'";
                    com1.CommandText = com1.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";

                    com1.ExecuteNonQuery();
                }

                catch { MessageBox.Show("Não foi possível gravar material jornalístico!"); }

            }



        }


        //*******************************************************************************

        private void button2_Click(object sender, EventArgs e)
        {
            int ieduplo = 0;

            try
            {
                if (ckBEDuplo.Checked) 
                { ieduplo = 1; }  else { ieduplo = 0; }


                com1.CommandText = "update teduplo set ";
                com1.CommandText = com1.CommandText + "EDuplo=" + ieduplo + ", Fx1_1=" + cmbFx1_1.Text + ", Fx1_2=" + cmbFx1_2.Text;
                com1.CommandText = com1.CommandText + ", Fx1_3=" + cmbFx1_3.Text + ", Fx2_1=" + cmbFx2_1.Text + ", Fx2_2=" + cmbFx2_2.Text + ", Fx2_3=" + cmbFx2_3.Text;
                com1.CommandText = com1.CommandText + ", NPistas=" + cmbNP.Text + ", NFxs=" + cmbNfx.Text;


                com1.CommandText = com1.CommandText + " where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital= " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'";
                com1.CommandText = com1.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";

                com1.ExecuteNonQuery();
            }

            catch { MessageBox.Show("Não foi possível gravar acidentes!"); }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void lblnfx_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //*******************************************************************************

        private void button3_Click(object sender, EventArgs e)
        {


            int iAclive=0;
            int iDeclive = 0;
            int iPlano = 0;
            int iCurva = 0;
            int iUrbano = 0;
            int iPedestre = 0;
            int iPaolongo = 0;
            int iPtrans = 0;
            int iCiclista = 0;
            int iCaolongo = 0;
            int iCtrans = 0;
            int iUmSentido = 0;
            int iretrato = 0;
            int ipaisagem = 0;

            string stretrato = "";


            if (ckBAclive.Checked) { iAclive = 1; } else { iAclive = 0; }
            if (ckBDeclive.Checked) { iDeclive = 1; } else { iDeclive = 0; }
            if (ckBPlano.Checked) { iPlano=1; } else { iPlano=0; }
            if (ckBCurva.Checked) { iCurva=1; } else { iCurva=0; }
            if (ckBUrbano.Checked) { iUrbano=1; } else { iUrbano=0; }

            if (ckBxPedAl.Checked) { iPaolongo = 1; }
            if (ckBxPedTrans.Checked) { iPtrans = 1; }

            if (ckBxCicAl.Checked) { iCaolongo = 1; }
            if (ckBxCictrans.Checked) { iCtrans = 1; }
         


            //////if (ckBPedestre.Checked) { iPedestre=1; } else { iPedestre=0; }
            //////if (rdPaL.Checked) { iPaolongo=1; } else { iPaolongo=0; }
            //////if  (rdPtrans.Checked) { iPtrans = 1; } else { iPtrans=0; }


            //////if (CkBCicli.Checked) { iCiclista=1; } else { iCiclista=0; }
            //////if (rdCaL.Checked) { iCaolongo=1; } else { iCaolongo=0; }
            //////if (rdBcicTrans.Checked) { iCtrans=1; } else { iCtrans=0; }



            if (ckBUmSent.Checked) { iUmSentido=1; } else { iUmSentido=0; }

            if (rdRet.Checked) { stretrato = "retrato"; } else { stretrato = "paisagem"; }

            if (rdPaisagem.Checked) { ipaisagem = 1; } else { ipaisagem = 0; }

            // cmbSA.Text;
            // cmbSB.Text;

            // Grava Geometria
            com1.CommandText = "update tgeometria set Aclive=" + iAclive + ", Declive=" + iDeclive + ", Plano=" + iPlano + ", Curva=" + iCurva + ", Urbano=" + iUrbano;
            com1.CommandText = com1.CommandText + ", Pedestre=" + iPedestre + ", Paolongo=" + iPaolongo + ", Ptrans=" + iPtrans + ", Ciclista=" + iCiclista;
            com1.CommandText = com1.CommandText + ", Caolongo=" + iCaolongo + ", Ctrans=" + iCtrans + ", UmSentido=" + iUmSentido;
            com1.CommandText = com1.CommandText + ", SA=" + "'" + cmbSA.Text + "'" + ", SB=" + "'" + cmbSB.Text + "'" + ", Forma=" + "'" + stretrato + "'";

            com1.CommandText = com1.CommandText + " where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital= " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'";
            com1.CommandText = com1.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";

            com1.ExecuteNonQuery();


        }

        //*******************************************************************************



        private void ckBPedestre_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void CkBCicli_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void btSair_Click(object sender, EventArgs e)
        {
            DesconectaBanco();
            this.Close();
        }

        private void btAplicaFE_Click(object sender, EventArgs e)
        {
            int ivaiter = 0;
            int ijatinha = 0;
            int iconfirma = 0;

            if (ckBxVaiTer.Checked) { ivaiter = 1; } else { ivaiter = 0; }
            if (ckBxJaTinha.Checked) { ijatinha = 1; } else { ijatinha = 0; }
            if (ckBxConfirma.Checked) { iconfirma = 1; } else { iconfirma = 0; }


            com1.CommandText = "update ttemfe set vaiterfe=" + ivaiter + ", jatinhafe=" + ijatinha + ", foiconf=" + iconfirma;
            com1.CommandText = com1.CommandText + " where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital= " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'";
            com1.CommandText = com1.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";

            com1.ExecuteNonQuery();

        }

        private void button1_Click_2(object sender, EventArgs e)
        {

        }

        private void btGrvFx_Click(object sender, EventArgs e)
        {
            int ieduplo = 0;

            try
            {
                if (ckBEDuplo.Checked)
                { ieduplo = 1; }
                else { ieduplo = 0; }


                com1.CommandText = "update teduplo set ";
                com1.CommandText = com1.CommandText + "EDuplo=" + ieduplo + ", Fx1_1=" + cmbFx1_1.Text + ", Fx1_2=" + cmbFx1_2.Text;
                com1.CommandText = com1.CommandText + ", Fx1_3=" + cmbFx1_3.Text + ", Fx2_1=" + cmbFx2_1.Text + ", Fx2_2=" + cmbFx2_2.Text + ", Fx2_3=" + cmbFx2_3.Text;
                com1.CommandText = com1.CommandText + ", NPistas=" + cmbNP.Text + ", NFxs=" + cmbNfx.Text;


                com1.CommandText = com1.CommandText + " where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital= " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'";
                com1.CommandText = com1.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";

                com1.ExecuteNonQuery();
            }

            catch { MessageBox.Show("Não foi possível gravar acidentes!"); }
        }

        private void btGravGeom_Click(object sender, EventArgs e)
        {
            int iAclive = 0;
            int iDeclive = 0;
            int iPlano = 0;
            int iCurva = 0;
            int iUrbano = 0;
            int iPedestre = 0;
            int iPaolongo = 0;
            int iPtrans = 0;
            int iCiclista = 0;
            int iCaolongo = 0;
            int iCtrans = 0;
            int iUmSentido = 0;
            int iretrato = 0;
            int ipaisagem = 0;

            string stretrato = "";


            if (ckBAclive.Checked) { iAclive = 1; } else { iAclive = 0; }
            if (ckBDeclive.Checked) { iDeclive = 1; } else { iDeclive = 0; }
            if (ckBPlano.Checked) { iPlano = 1; } else { iPlano = 0; }
            if (ckBCurva.Checked) { iCurva = 1; } else { iCurva = 0; }
            if (ckBUrbano.Checked) { iUrbano = 1; } else { iUrbano = 0; }

            if (ckBxPedAl.Checked) { iPaolongo = 1; }
            if (ckBxPedTrans.Checked) { iPtrans = 1; }

            if (ckBxCicAl.Checked) { iCaolongo = 1; }
            if (ckBxCictrans.Checked) { iCtrans = 1; }



            //////if (ckBPedestre.Checked) { iPedestre=1; } else { iPedestre=0; }
            //////if (rdPaL.Checked) { iPaolongo=1; } else { iPaolongo=0; }
            //////if  (rdPtrans.Checked) { iPtrans = 1; } else { iPtrans=0; }


            //////if (CkBCicli.Checked) { iCiclista=1; } else { iCiclista=0; }
            //////if (rdCaL.Checked) { iCaolongo=1; } else { iCaolongo=0; }
            //////if (rdBcicTrans.Checked) { iCtrans=1; } else { iCtrans=0; }



            if (ckBUmSent.Checked) { iUmSentido = 1; } else { iUmSentido = 0; }

            if (rdRet.Checked) { stretrato = "retrato"; } else { stretrato = "paisagem"; }

            if (rdPaisagem.Checked) { ipaisagem = 1; } else { ipaisagem = 0; }

            // cmbSA.Text;
            // cmbSB.Text;

            // Grava Geometria;

            com1.CommandText = "update tgeometria set Aclive=" + iAclive + ", Declive=" + iDeclive + ", Plano=" + iPlano + ", Curva=" + iCurva + ", Urbano=" + iUrbano;
            com1.CommandText = com1.CommandText + ", Pedestre=" + iPedestre + ", Paolongo=" + iPaolongo + ", Ptrans=" + iPtrans + ", Ciclista=" + iCiclista;
            com1.CommandText = com1.CommandText + ", Caolongo=" + iCaolongo + ", Ctrans=" + iCtrans + ", UmSentido=" + iUmSentido;
            com1.CommandText = com1.CommandText + ", SA=" + "'" + cmbSA.Text + "'" + ", SB=" + "'" + cmbSB.Text + "'" + ", Forma=" + "'" + stretrato + "'";

            com1.CommandText = com1.CommandText + " where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital= " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'";
            com1.CommandText = com1.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";

            com1.ExecuteNonQuery();
        }



        private void btAplicaFE_Click_1(object sender, EventArgs e)
        {
            int ivaiter = 0;
            int ijatinha = 0;
            int iconfirma = 0;

            int itemqb = 0;

            int imanter = 0;
            int iremover = 0;

            int isubst = 0;
            int iproj = 0;

            int irenova = 0;


            if (ckBxVaiTer.Checked) { ivaiter = 1; } else { ivaiter = 0; }
            if (ckBxJaTinha.Checked) { ijatinha = 1; } else { ijatinha = 0; }
            if (ckBxConfirma.Checked) { iconfirma = 1; } else { iconfirma = 0; }

            if (ckTemQM.Checked) { itemqb = 1; } else { itemqb = 0; }

            if (rdManter.Checked) { imanter = 1; } else { imanter = 0; }
            if (rdRemover.Checked) { iremover = 1; } else { iremover = 0; }

            if (rdSubst.Checked) { isubst = 1; } else { isubst = 0; }
            if (rdDeProj.Checked) { iproj = 1; } else { iproj = 0; }

            if (chkRen.Checked) { irenova = 1; } else { irenova = 0; }

            com1.CommandText = "update ttemfe set vaiterfe=" + ivaiter + ", jatinhafe=" + ijatinha + ", foiconf=" + iconfirma + ", temqb=" + itemqb;
            com1.CommandText = com1.CommandText + ", manter=" + imanter + ", remover=" + iremover + ", subst=" + isubst + ", proj=" + iproj;
            com1.CommandText = com1.CommandText + ", renova=" + irenova;
            com1.CommandText = com1.CommandText + " where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital= " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'";
            com1.CommandText = com1.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";

            com1.ExecuteNonQuery();

            VeTemFE(objTemFE);
            CarregaTemFE(objTemFE);

        }






        private void btSair_Click_1(object sender, EventArgs e)
        {
            DesconectaBanco();
            this.Close();
        }

        private void rdRemover_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ckTemQM_CheckedChanged(object sender, EventArgs e)
        {
            if (ckTemQM.Checked==true)
            {
                panel11.Enabled = true;
                panel12.Enabled = true;
            }else
            {
                panel11.Enabled = false;
                panel12.Enabled = false;
            }


        }





        // ******************************************************************************

        private void button2_Click_1(object sender, EventArgs e)
        {
            C_excel objExcel = new C_excel();
            C_pontofe objpFE = new C_pontofe();

            int[] D85p = new int[15];
            double valorD = 0;

            string kmvinc = ""; // km da contagem vinculada a esse estudo
            int qtdCroquis = 0;

            //valorD =Math.Truncate( 0.2354 * 100);
            //D85p[1] = Int32.Parse(valorD.ToString());



            ConectaBanco2();

            VeGeometria(objGeom, objpFE);

            VekmContagemVinculado(ref kmvinc);      // kmvinc é o km da contagem vinculada a esse estudo

            ConectaBancoClassfic();
            comcfc1.CommandText = "select qtdcroquis from tbpontos where Rod= " + "'" + lblRod2.Text + "'" + " and km= " + "'" + kmvinc + "'";
            readercfc1 = comcfc1.ExecuteReader();


            if (readercfc1.Read())  {  qtdCroquis = Int32.Parse( readercfc1["qtdcroquis"].ToString());  }


            DesconectaBancoClassfic();

            // MessageBox.Show(btn1.Name + "Clicked");                 

            OpenFileDialog openFileDialogClass = new OpenFileDialog();

                openFileDialogClass.InitialDirectory = @"C:\RR\Classificações2020\";
                openFileDialogClass.RestoreDirectory = true;
                openFileDialogClass.Title = "Arquivo Excel com a contagem de referencia";
                openFileDialogClass.DefaultExt = "xlsx";
                openFileDialogClass.Filter = "xlsx files (*.xlsx)|*.xlsx";
                openFileDialogClass.Multiselect = false;

                openFileDialogClass.ShowDialog();





            foreach (String file in openFileDialogClass.FileNames)
            {
                //objExcel.AbreExcel(@"C:\DER-RJ\FiscalizaçãoEletronica\2020\Estudos2020\" + nomearq, 1);
              

                objExcel.AbreExcel(@file, 16 + qtdCroquis);  // 17 é a folha do 85%AB

                // list85p.Clear();


                if (objpFE.VMD != 0)
                {

                    // SENTIDO AB

                    for (int ind1 = 1; ind1 <= 14; ind1++)
                    {
                        valorD = Math.Truncate(objExcel.LeExDb(ind1 + 15, 11, 16 + qtdCroquis) * 100);
                        D85p[ind1] = Int32.Parse((valorD).ToString());
                        // list85p.AddLast(valorD);
                    }

                    com1.CommandText = "update t85pct set Vel19 = " + D85p[1] + ", Vel29 = " + D85p[2] + ", Vel39 = " + D85p[3];
                    com1.CommandText = com1.CommandText + ", Vel49 = " + D85p[4] + ", Vel59 = " + D85p[5] + ", Vel69 = " + D85p[6];
                    com1.CommandText = com1.CommandText + ", Vel79 = " + D85p[7] + ", Vel89 = " + D85p[8] + ", Vel99 = " + D85p[9];
                    com1.CommandText = com1.CommandText + ", Vel109 = " + D85p[10] + ", Vel119 = " + D85p[11] + ", Vel129 = " + D85p[12];
                    com1.CommandText = com1.CommandText + ", Vel139 = " + D85p[13] + ", Vel199 = " + D85p[14];

                    com1.CommandText = com1.CommandText + " where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital= " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'";
                    com1.CommandText = com1.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'" + " and Sent= " + "'AB'" ;
                    com1.ExecuteNonQuery();

                }




                if (objpFE.VmdB != 0)
                {

                    // SENTIDO BA

                    for (int ind1 = 1; ind1 <= 14; ind1++)
                    {
                        valorD = Math.Truncate(objExcel.LeExDb(ind1 + 15, 11, 17 + qtdCroquis) * 100);
                        D85p[ind1] = Int32.Parse((valorD).ToString());
                        // list85p.AddLast(valorD);
                    }

                    com1.CommandText = "update t85pct set Vel19 = " + D85p[1] + ", Vel29 = " + D85p[2] + ", Vel39 = " + D85p[3];
                    com1.CommandText = com1.CommandText + ", Vel49 = " + D85p[4] + ", Vel59 = " + D85p[5] + ", Vel69 = " + D85p[6];
                    com1.CommandText = com1.CommandText + ", Vel79 = " + D85p[7] + ", Vel89 = " + D85p[8] + ", Vel99 = " + D85p[9];
                    com1.CommandText = com1.CommandText + ", Vel109 = " + D85p[10] + ", Vel119 = " + D85p[11] + ", Vel129 = " + D85p[12];
                    com1.CommandText = com1.CommandText + ", Vel139 = " + D85p[13] + ", Vel199 = " + D85p[14];

                    com1.CommandText = com1.CommandText + " where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital= " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'";
                    com1.CommandText = com1.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'" + " and Sent= " + "'BA'";
                    com1.ExecuteNonQuery();

                }


            }

            DesconectaBanco2();

        }

        //*******************************************************************************










        // ***************************************************
        
        private void VekmContagemVinculado(ref string kmvinc)
        {
            com2.CommandText = "select kmVinc from t85pct where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'";
            com2.CommandText = com2.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";

            reader2 = com2.ExecuteReader();

            if (reader2.Read())

            { kmvinc= reader2["kmVinc"].ToString();  }

            reader2.Close();
        }

        // *************************************************




        // *************************************************************************************************************
        public void VeGeometria(C_Geometria pGeom, C_pontofe objPfe)
        {
            com2.CommandText = "select * from tgeometria where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'";
            com2.CommandText = com2.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";

            ////try
            {
                reader2 = com2.ExecuteReader();

                if (reader2.Read())
                {
                    pGeom.aclive = Int32.Parse((reader2["Aclive"]).ToString());
                    pGeom.declive = Int32.Parse((reader2["Declive"]).ToString());
                    pGeom.plano = Int32.Parse((reader2["Plano"]).ToString());
                    pGeom.curva = Int32.Parse((reader2["Curva"]).ToString());
                    pGeom.urbano = Int32.Parse((reader2["Urbano"]).ToString());
                    pGeom.pedestre = Int32.Parse((reader2["Pedestre"]).ToString());
                    pGeom.paolongo = Int32.Parse((reader2["Paolongo"]).ToString());
                    pGeom.ptrans = Int32.Parse((reader2["Ptrans"]).ToString());
                    pGeom.ciclista = Int32.Parse((reader2["Ciclista"]).ToString());
                    pGeom.caolongo = Int32.Parse((reader2["Caolongo"]).ToString());
                    pGeom.ctrans = Int32.Parse((reader2["Ctrans"]).ToString());

                    pGeom.umsent = Int32.Parse((reader2["UmSentido"]).ToString());
                    pGeom.sa = reader2["SA"].ToString();
                    pGeom.sb = reader2["SB"].ToString();
                    pGeom.forma = reader2["Forma"].ToString();

                }
                reader2.Close();




                if (pGeom.umsent == 0)  // Se tem mais de um sentido
                {
                    com2.CommandText = "select * from ponto_featual where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'";
                    com2.CommandText = com2.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";

                    reader2 = com2.ExecuteReader();

                    if (reader2.Read())

                    {
                        objPfe.MunSen = (reader2["MunSen"]).ToString();
                        objPfe.MunA = (reader2["MunA"]).ToString();
                        objPfe.MunB = (reader2["MunB"]).ToString();

                        objPfe.VMD = Int32.Parse((reader2["VMD"]).ToString());
                        objPfe.VmdB = Int32.Parse((reader2["VmdB"]).ToString());

                        objPfe.Vel85p = Int32.Parse((reader2["Vel85p"]).ToString());
                        objPfe.Vel85pSB = Int32.Parse((reader2["Vel85pSB"]).ToString());

                        objPfe.Lat = (reader2["Lat"]).ToString().ToString();
                        objPfe.Longit = (reader2["Longit"]).ToString();

                        objPfe.Lat2 = (reader2["Lat2"]).ToString();
                        objPfe.Longit2 = (reader2["Longit2"]).ToString();



                        Vel85pSB = Int32.Parse((reader2["Vel85pSB"]).ToString());
                    }
                    reader2.Close();
                }


                if (pGeom.umsent == 1)  // Se tem mais de um sentido
                {
                    com2.CommandText = "select * from ponto_featual where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'";
                    com2.CommandText = com2.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";

                    reader2 = com2.ExecuteReader();

                    if (reader2.Read())

                    {
                        objPfe.MunSen = (reader2["MunSen"]).ToString();
                        objPfe.MunA = (reader2["MunA"]).ToString();
                        objPfe.MunB = (reader2["MunB"]).ToString();

                        objPfe.VMD = Int32.Parse((reader2["VMD"]).ToString());
                        objPfe.VmdB = Int32.Parse((reader2["VmdB"]).ToString());

                        objPfe.Vel85p = Int32.Parse((reader2["Vel85p"]).ToString());
                        objPfe.Vel85pSB = Int32.Parse((reader2["Vel85pSB"]).ToString());
                        Vel85pSB = Int32.Parse((reader2["Vel85pSB"]).ToString());

                        objPfe.Lat = (reader2["Lat"]).ToString().ToString();
                        objPfe.Longit = (reader2["Longit"]).ToString();

                        objPfe.Lat2 = (reader2["Lat2"]).ToString();
                        objPfe.Longit2 = (reader2["Longit2"]).ToString();
                    }
                    reader2.Close();
                }


            }

            ////catch { MessageBox.Show("Sem registro Atual!"); }
        }

    }
}
