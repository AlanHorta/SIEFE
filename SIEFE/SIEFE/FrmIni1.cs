using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


//using GemBox.Spreadsheet;
// usingWord = Microsoft.Office.Interop.Word;


namespace SIEFE
{
    public partial class Form1 : Form
    {

       
            
        string strConFE = @"Server=localhost;Database=fe;Uid=root;Pwd=obuldog67;";
     public string strConcfc = @"Server=localhost;Database=classfic;Uid=root;Pwd=obuldog67;";

        public MySqlConnection mysqlCon1;
         MySqlConnection mysqlCon2;

      public  MySqlConnection MysqlConClf1;
        public MySqlConnection MysqlConClf2;

        public MySqlCommand com1;
         MySqlCommand com2;
       public MySqlCommand comcfc1;
        public MySqlCommand comcfc2;

        public MySqlDataReader reader1;
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

        public int oaleatP=0;
        public int oEduplo = 0;

        int aVezt = 0;
   

        C_excel objExcel = new C_excel();

        Uteis cUt = new Uteis();
        C_PistasFaixas CPtFx = new C_PistasFaixas();
        C_Geometria objGeom = new C_Geometria();
        c_acidentes objAcid = new c_acidentes();
        C_risco objRisco = new C_risco();
        C_historico objHist = new C_historico();
        C_proj objproj = new C_proj();
        C_jorn ObjJorn = new C_jorn();
        C_paginas ObjPags = new C_paginas();

        C_VaiTerOuJaTem objVTOJT = new C_VaiTerOuJaTem();

        C_Placas objPlacas = new C_Placas();

        C_Rodovia objRof = new C_Rodovia();

        C_DataBase objDtb = new C_DataBase();

                
        C_TemFE objTemFE = new C_TemFE();

        C_pdf objPdf = new C_pdf();

        C_Vel objVel = new C_Vel();
        C_Vel2 objVel2 = new C_Vel2();
        

        C_pontofe objpFE = new C_pontofe();

        C_percentuais85 objPct = new C_percentuais85();

        C_Parc85p opjDivp = new C_Parc85p();

        C_pontoCL objPontV = new C_pontoCL(); // classe que contem o ponto de classificação da Vez

        C_MesAno objMesAno = new C_MesAno();
        
        public Form1()
        {
            InitializeComponent();

        }


        // *************************************************************************************************************
        // ************************************************************************************************************* 

          

        private void Form1_Load(object sender, EventArgs e)
        {
          //      timer1.Enabled = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            VeMesAno(objMesAno);

            ConectaBanco();
            ConectaBanco2();
                
            LeRegistroAtual();

            bEDuplo = VerificaSeÉDuplo();

            VeGeometria(objGeom, objpFE);  // Serve para saber se é um ou mais sentidos

            //DesconectaBanco();
            //DesconectaBanco2();
        }


        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************



        public void VeMesAno(C_MesAno objMesAno)
        {
            ConectaBancoClassfic();
            comcfc1.CommandText = "select * from tbmesmedicao";

            readercfc1 = comcfc1.ExecuteReader();


            while (readercfc1.Read())
            {   objMesAno.mes = readercfc1["omes"].ToString();
                objMesAno.ano = "2020";
            }





            DesconectaBancoClassfic();
        }


        public void ConectaBanco()
        {

            using (mysqlCon1 = new MySqlConnection(strConFE)) ;
            {
                mysqlCon1.Open();
                com1 = mysqlCon1.CreateCommand();           

            }


        }

        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************

        public void ConectaBanco2()
        {

            using (mysqlCon2 = new MySqlConnection(strConFE)) ;
            {
                mysqlCon2.Open();
                com2 = mysqlCon2.CreateCommand();

            }

        }

        public void DesconectaBanco()
        {
            mysqlCon1.Close();
        }

        private void DesconectaBanco2()
        {
            mysqlCon2.Close();
        }



        public void DesconectaBancoClassfic()
        {
            MysqlConClf1.Close();
        }
        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************

        public void ConectaBancoClassfic()
        {
            
                 using (MysqlConClf1 = new MySqlConnection(strConcfc)) ;
            {
                MysqlConClf1.Open();
                comcfc1 = MysqlConClf1.CreateCommand();
              

            }

        }






        public void ConectaBancoClassfic2()
        {

            using (MysqlConClf2 = new MySqlConnection(strConcfc)) ;
            {
                MysqlConClf2.Open();
                comcfc2 = MysqlConClf2.CreateCommand();


            }

        }

        public void DesconectaBancoClassfic2()
        {
            MysqlConClf2.Close();
        }







        private void VeSeCalula85p( ref int i85p)
        {
            ConectaBanco();
            ConectaBanco2();
            com1.CommandText = "select Calc85p from ponto_featual";

            reader1 = com1.ExecuteReader();

            while (reader1.Read())
            {
                i85p = Int16.Parse(reader1["Calc85p"].ToString());
            }
            
        }




        public void LeRegistroAtual()
        {
            ConectaBanco();
        

            com1.CommandText = "select * from ponto_featual";

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

                lblkmR2.Text = reader1["kmReal"].ToString();

                VmdB = Int32.Parse(reader1["VmdB"].ToString());


            }
            reader1.Close();
            this.Update();
            DesconectaBanco();
        }



        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************

        public Boolean VerificaSeÉDuplo()
        {
            //bEDuplo
            ConectaBanco2();

            com2.CommandText = "select * from teduplo where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) +"'";
            com2.CommandText = com2.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";   //  tabela que indica se o ponto de FE é duplo ou não

            ////try
            ////{

            //if (reader2.IsClosed==false)
            //{ reader2.Close(); }

            try
            {

                reader2 = com2.ExecuteReader();

                // oEduplo variável public

              //  int oEDuplo;
                if (reader2.Read())
                {
                    //oEDuplo = reader1["EDuplo"].ToString();
                    oEduplo = Int32.Parse((reader2["EDuplo"]).ToString());

                    if (oEduplo == 1)
                    {
                        // Se oEDuplo==1, então o controlador de Velocidade é do Tipo I e só tem uma latidude e longitude

                        lblLat2_2.Text = "";
                        lblLong2_2.Text = "";
                        reader2.Close();

                        return true;
                    }
                    else
                    {
                        reader2.Close();
                        com2.CommandText = "select Lat2,Longit2 from ponto_featual";
                        reader2 = com2.ExecuteReader();

                        if (reader2.Read())
                        {
                            lblLat2_2.Text = reader2["Lat2"].ToString();
                            lblLong2_2.Text = reader2["Longit2"].ToString();
                            reader2.Close();
                            return false;
                        }

                    }
                }
                else
                {
                    reader2.Close();
                    MessageBox.Show("Registro atual não encontrado");
                }

            }





            catch
            {
                MessageBox.Show("Registro atual não encontrado");
            }

            //reader2.Close();
            return true;

        }



        // + "'" + cmbRodovia.Text + "'";



        private void VeSe_E_Municipio(string aRod, ref int EMunA, ref int EMunB )
        {
            com2.CommandText = "select * from trodovias where Rodovia =" + "'" + aRod + "'";
            reader2 = com2.ExecuteReader();
            if (reader2.Read())
            {
                EMunA = Int32.Parse(reader2["EmunA"].ToString());
                EMunB = Int32.Parse(reader2["EmunB"].ToString());
            }
            reader2.Close();

            //DesconectaBanco2();
        }








        private void button1_Click(object sender, EventArgs e)
        {
            String strv = ", ";

            // Salva o registro a ser processado
            // A tabela ponto_featual só deve ter um registro de cada vez


            com1.CommandText = "delete from ponto_featual";     // Limpa a tabela se existir algum risco
            com1.ExecuteNonQuery();



            String aLat = cUt.ConverteCoord(lblLat.Text);   // Tira ' e " da latitude
            String aLong = cUt.ConverteCoord(lblLong.Text);   // Tira ' e " da longitude

            //String aLat =lblLat.Text;
            ////String aLat = "";
            ////String str1 = lblLat.Text.Substring(0, 5);
            ////String str2 = lblLat.Text.Substring(6, 5);
            ////String str3 = lblLat.Text.Substring(12, 1);


            ////aLat = str1 + "'" + str2 + "''" + str3;

            ////String aLong = lblLong.Text;
            ////aLong = aLong.Substring(0, 5) + "'" + aLong.Substring(6, 5) + "''" + aLong.Substring(12, 1);

            //strtxt1 = strtxt1.Substring(0, 5) + "'" + strtxt1.Substring(6, 4) + '"' + strtxt1.Substring(12, 1);


            com1.CommandText = "insert into capina (rod, data, km, sentido, extencao, area) ";
            com1.CommandText = com1.CommandText + "Values (" + "'" + cmbRodovia.Text + "'" + strv + lblkmEdt.Text + strv + "'" + lblSent.Text + "'" + strv + lblkmR.Text + strv;
            com1.CommandText = com1.CommandText + "'" + lblLoc.Text + "'" + strv + "'" + lblMun.Text + "'" + strv + lblQtdFx.Text + strv + "'" + lblMA.Text + "'" + strv;
            com1.CommandText = com1.CommandText + "'" + lblMB.Text + "'" + strv + lblVF.Text + strv + "'" + aLat + "'" + strv + "'" + aLong + "'" + strv;
            com1.CommandText = com1.CommandText + lblVmd.Text + strv + lblV85.Text + strv + "'" + lblTipo.Text + "'" + ")";


            //com1.CommandText = "insert into ponto_featual (Rodovia, kmEdital, MunSen, kmReal, Localidade, Municipio, QtdFx, MunA, MunB, VelFisc, Lat, Longit, VMD, Vel85p, Tipo) ";
            //com1.CommandText = com1.CommandText + "Values ('R', 7, 'Itaperuna', 7, 'Sambaetiba', 'Itaboraí', '2', 'Itaboraí', 'Itaperuna', 50, '22°40 54.39 S', '42°46 42.76 O',4445,64,'I.A')";
            com1.ExecuteNonQuery();





        }


        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //excel.Application.Quit();
            objExcel.Fecha();
           



            mysqlCon1.Close();
            mysqlCon2.Close();
           // objExcel.FechaExcel();
           // objExcel.Fecha();
            this.Close();
        }

        private void cmbRodovia_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbEdital.Items.Clear();
            cmbSentido.Items.Clear();
            //cmbRodovia.Items.Clear();

            com1.CommandText = "select kmEdital from ponto_fe where Rodovia= " + "'" + cmbRodovia.Text + "'";
            reader1 = com1.ExecuteReader();
            while (reader1.Read())
            {
                //Console.WriteLine(reader1["Tipo"].ToString());
                cmbEdital.Items.Add(reader1["kmEdital"].ToString()); // popula a combo com todos os kms do edital da rodovia em questão

            }
            reader1.Close();

        }
        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************

        private void cmbEdital_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSentido.Items.Clear();
            //cmbRodovia.Items.Clear();

            com1.CommandText = "select MunSen from ponto_fe where Rodovia= " + "'" + cmbRodovia.Text + "'" + " and kmEdital= " + cmbEdital.Text;
            reader1 = com1.ExecuteReader();
            while (reader1.Read())
            {
                //Console.WriteLine(reader1["Tipo"].ToString());

                cmbSentido.Items.Add(reader1["MunSen"].ToString()); // popula a combo com todos os sentidos do edital da rodovia e km em questão

            }
            reader1.Close();
        }

        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************

        private void btnSel_Click(object sender, EventArgs e)
        {
            com1.CommandText = "select * from ponto_fe where Rodovia= " + "'" + cmbRodovia.Text + "'" + " and kmEdital= " + cmbEdital.Text;
            com1.CommandText = com1.CommandText + " and MunSen= " + "'" + cmbSentido.Text + "'";
            reader1 = com1.ExecuteReader();

            while (reader1.Read())
            {
                //Console.WriteLine(reader1["Tipo"].ToString());
                blbRod.Text = reader1["Rodovia"].ToString();
                lblSent.Text = reader1["MunSen"].ToString();
                lblkmEdt.Text = reader1["kmEdital"].ToString();
                lblkmR.Text = reader1["kmReal"].ToString();
                lblMun.Text = reader1["Municipio"].ToString();
                lblLoc.Text = reader1["Localidade"].ToString();
                lblQtdFx.Text = reader1["QtdFx"].ToString();
                lblMA.Text = reader1["MunA"].ToString();
                lblMB.Text = reader1["MunB"].ToString();
                lblVF.Text = reader1["VelFisc"].ToString();
                lblLat.Text = reader1["Lat"].ToString();
                lblLong.Text = reader1["Longit"].ToString();
                lblVmd.Text = reader1["VMD"].ToString();
                lblV85.Text = reader1["Vel85p"].ToString();
                lblTipo.Text = reader1["Tipo"].ToString();

                lblkmReal.Text = reader1["kmReal"].ToString();


                if (VerificaSeÉDuplo())
                {
                    lblLat_21.Text = "";
                    lblLong21.Text = "";
                }
                else
                {
                    lblLat_21.Text = reader1["Lat2"].ToString();
                    lblLong21.Text = reader1["Longit2"].ToString();
                }


            }
            reader1.Close();
        }

        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************


        public void Pagina1()
        {
            //objExcel.Pagina(3, 13, 1, lblRod2.Text + "  km " + lblkmR2.Text);
            nsheet = nsheet + 1;

            objExcel.LarguraColunaExcel(nsheet, 2, 10.89);

            objExcel.Pagina(46, 8, nsheet, lblRod2.Text);
            objExcel.Pagina(47, 8, nsheet, "km " + cUt.ConvPontoVirg(lblkmR2.Text));
            objExcel.Pagina(48, 8, nsheet, objMesAno.mes + "/" + objMesAno.ano);

            SetaPaginas(ObjPags, nsheet);

        }

        public void Pagina2()
        {
            nsheet = nsheet + 1;
            objExcel.Pagina(3, 13, nsheet, lblRod2.Text + "  km " + cUt.ConvPontoVirg(lblkmR2.Text));
            objExcel.MudaFonte(3, 13, nsheet, "Arial", 11);

            SetaPaginas(ObjPags, nsheet);
        }

        public void Pagina3()
        {
            nsheet = nsheet + 1;
            npag = npag + 1;
            objExcel.Pagina(3, 13, nsheet, lblRod2.Text + "  km " + cUt.ConvPontoVirg(lblkmR2.Text));
            objExcel.MudaFonte(3, 13, nsheet, "Arial", 11);
            string strtxt1 = "Há muito que o desenvolvimento socioeconômico de nosso Estado vem gerando benefícios e ";
            strtxt1 = strtxt1 + "fomentando condições que nos permitem manter a importante posição de segunda economia brasileira.A introdução dos ";
            strtxt1 = strtxt1 + "veículos de linha econômica, e os constantes incentivos fiscais do Governo Federal, ";
            strtxt1 = strtxt1 + "permitiram que muitos cidadãos brasileiros adquirissem seus veículos. ";

            objExcel.Pagina(8, 3, 3, strtxt1);
            objExcel.Pagina(60, 14, 3, npag.ToString()); // A página 3 é a 1
            objExcel.MudaFonte(60, 14, 3, "Arial", 10);
            SetaPaginas(ObjPags, nsheet);
        }

        public void Pagina4()
        {
            nsheet = nsheet + 1;
            npag = npag + 1;
            string strtxt1 = "";
            objExcel.Pagina(3, 13, nsheet, lblRod2.Text + "  km " + cUt.ConvPontoVirg(lblkmR2.Text));
            objExcel.MudaFonte(3, 13, nsheet, "Arial", 11);
            strtxt1 = "Este corpo técnico da Diretoria de Operação, Monitoramento e Controle de Trânsito do ";
            strtxt1 = strtxt1 + "DER/RJ, recebeu a incumbência de desenvolver, com base nos seus estudos do ";
            strtxt1 = strtxt1 + "monitoramento do tráfego circulante nas rodovias estaduais, estudos que demonstrem a ";
            strtxt1 = strtxt1 + "necessidade de que seja instalado na rodovia " + lblRod2.Text + ", um outro equipamento redutor de velocidade.";


            objExcel.Pagina(11, 3, nsheet, strtxt1);

            objExcel.Pagina(60, 14, nsheet, npag.ToString());
            objExcel.MudaFonte(60, 14, nsheet,"Arial",10);
            SetaPaginas(ObjPags, nsheet);
        }







        public void Pagina5()
        {
            nsheet = nsheet + 1;
            npag = npag + 1;
            string strtxt1 = "";
            string strMunA = "";
            string strMunB = "";

            VeGeometria(objGeom, objpFE);


            int EMunA = 0;
            int EMunB = 0;

            VeSe_E_Municipio(lblRod2.Text, ref EMunA, ref EMunB);
           
            
            if (EMunA==1)
                { strMunA = ", que liga o município de ";  }
            else
                { strMunA = ", que liga "; }



            if (EMunB == 1)
            { strMunB = " ao município de "; }
            else
            { strMunB = " à "; }




            objExcel.Pagina(3, 13, nsheet, lblRod2.Text + "  km " + cUt.ConvPontoVirg(lblkmR2.Text));
            objExcel.MudaFonte(3, 13, nsheet, "Arial", 11);
            strtxt1 = "Trata-se do trecho da rodovia " + lblRod2.Text + strMunA + lblMA2.Text;
            strtxt1 = strtxt1 + strMunB + lblMB2.Text + ", no km " + lblkmR2.Text + " no município de " + lblMun2.Text + ".";
            objExcel.Pagina(23, 3, nsheet, strtxt1);

            strtxt1 = "Coordenadas GPS do Km " + cUt.ConvPontoVirg(lblkmR2.Text) + ":";
            objExcel.Pagina(27, 3, nsheet, strtxt1);



            if (objGeom.umsent==1)
            { 
                if (objpFE.VMD!=0) 
                { strtxt1 = cUt.ReconverteCoord(objpFE.Lat.ToString() ); }
                else
                { strtxt1 = cUt.ReconverteCoord(objpFE.Lat2.ToString()); }
            }
                          
                        
            if (objGeom.umsent == 0) { strtxt1 = cUt.ReconverteCoord(lblLat2.Text); }


            // strtxt1 = lbl    Lat2.Text;
            //strtxt1 = strtxt1.Substring(0, 5) + "'" + strtxt1.Substring(6, 5) + "''" + strtxt1.Substring(strtxt1.Length - 1, 1);
            strtxt1 = "Latitude:        " + strtxt1;
            objExcel.Pagina(28, 3, nsheet, strtxt1);


            if (objGeom.umsent == 1)
            {
                if (objpFE.VMD != 0)
                { strtxt1 = cUt.ReconverteCoord(objpFE.Longit.ToString()); }
                else
                { strtxt1 = cUt.ReconverteCoord(objpFE.Longit2.ToString()); }
            }

            if (objGeom.umsent == 0) { strtxt1 = cUt.ReconverteCoord(lblLong2.Text); }

            
            //strtxt1 = strtxt1.Substring(0, 5) + "'" + strtxt1.Substring(6, 5) + "''" + strtxt1.Substring(strtxt1.Length - 1, 1);
            strtxt1 = "Longitude:     " + strtxt1;
            objExcel.Pagina(29, 3, nsheet, strtxt1);




            objExcel.Pagina(60, 14, nsheet, npag.ToString());
            objExcel.MudaFonte(60, 14, nsheet, "Arial", 10);
            SetaPaginas(ObjPags, nsheet);
        }





        public void Pagina6()
        {
            nsheet = nsheet + 1;
            npag = npag + 1;
            objExcel.Pagina(3, 13, nsheet, lblRod2.Text + "  km " + cUt.ConvPontoVirg(lblkmR2.Text));
            objExcel.MudaFonte(3, 13, nsheet, "Arial", 11);
            objExcel.Pagina(60, 14, 6, npag.ToString());
            objExcel.MudaFonte(60, 14, nsheet, "Arial", 10);
            SetaPaginas(ObjPags, nsheet);
        }

        // ***********************************************************************************






        public void Pagina7()
        {
           
            nsheet = nsheet + 1;
            npag = npag + 1;
            string strtxt1 = "";
            string strLat = "";
            string strLong = "";


            objExcel.Pagina(3, 13, nsheet, lblRod2.Text + "  km " + cUt.ConvPontoVirg(lblkmR2.Text));
            objExcel.MudaFonte(3, 13, nsheet, "Arial", 11);

            VeGeometria(objGeom, objpFE);

            strtxt1 = "Localização do(s) equipamento(s) de fiscalização eletrônica no km " + cUt.ConvPontoVirg(lblkmR2.Text) + " da Rodovia " + lblRod2.Text;
            objExcel.Pagina(39, 3, nsheet, strtxt1);

            if (objGeom.umsent == 1)
            { 
                if (objpFE.VMD != 0)
                {
                      strtxt1 = "Sentido " + objpFE.MunA.ToString() + "/" + objpFE.MunB.ToString();
                      objExcel.Pagina(42, 3, nsheet, strtxt1);
                      strLat = objpFE.Lat.ToString();
                      strLong = objpFE.Longit.ToString();

                }
                else
                {
                    strtxt1 = "Sentido " + objpFE.MunB.ToString() + "/" + objpFE.MunA.ToString();
                    objExcel.Pagina(42, 3, nsheet, strtxt1);
                    strLat = objpFE.Lat2.ToString();
                    strLong = objpFE.Longit2.ToString();

                }


                strtxt1 = "Coordenadas";
                objExcel.Pagina(44, 3, nsheet, strtxt1);

                strtxt1 = "Latitude";
                objExcel.Pagina(44, 6, nsheet, strtxt1);
                strtxt1 = "Longitude";
                objExcel.Pagina(45, 6, nsheet, strtxt1);

                strLat = cUt.ReconverteCoord(strLat);
                objExcel.Pagina(44, 8, nsheet, strLat);

                strLong = cUt.ReconverteCoord(strLong);
                objExcel.Pagina(45, 8, nsheet, strLong);


                //objExcel.Pagina(42, 3, nsheet, strtxt1);
            }


            if (objGeom.umsent != 1)          // Tem os 2 sentidos
            {                              
                if (bEDuplo)
                {                           // Tem 2 sentidos e é duplo 
                    strtxt1 = "Sentido Duplo (" + lblMA2.Text + "/" + lblMB2.Text + " e " + lblMB2.Text + "/" + lblMA2.Text + ")";
                    objExcel.Pagina(42, 3, nsheet, strtxt1);
                    strLat = objpFE.Lat.ToString();
                    strLong = objpFE.Longit.ToString();

                    strtxt1 = "Coordenadas";
                    objExcel.Pagina(44, 3, nsheet, strtxt1);

                    strtxt1 = "Latitude";
                    objExcel.Pagina(44, 6, nsheet, strtxt1);
                    strtxt1 = "Longitude";
                    objExcel.Pagina(45, 6, nsheet, strtxt1);

                    strLat = cUt.ReconverteCoord(strLat);
                    objExcel.Pagina(44, 8, nsheet, strLat);

                    strLong = cUt.ReconverteCoord(strLong);
                    objExcel.Pagina(45, 8, nsheet, strLong);

                }
                else
                {                          // Tem os 2 sentidos mas não é duplo

                    // sentido AB
                    strtxt1 = "Sentido " + objpFE.MunA.ToString() + "/" + objpFE.MunB.ToString();
                    objExcel.Pagina(42, 3, nsheet, strtxt1);
                    strLat = objpFE.Lat.ToString();
                    strLong = objpFE.Longit.ToString();

                    strtxt1 = "Coordenadas";
                    objExcel.Pagina(44, 3, nsheet, strtxt1);

                    strtxt1 = "Latitude";
                    objExcel.Pagina(44, 6, nsheet, strtxt1);
                    strtxt1 = "Longitude";
                    objExcel.Pagina(45, 6, nsheet, strtxt1);

                    strLat = cUt.ReconverteCoord(strLat);
                    objExcel.Pagina(44, 8, nsheet, strLat);

                    strLong = cUt.ReconverteCoord(strLong);
                    objExcel.Pagina(45, 8, nsheet, strLong);



                    // sentido BA
                    strtxt1 = "Sentido " + objpFE.MunB.ToString() + "/" + objpFE.MunA.ToString();
                    objExcel.Pagina(48, 3, nsheet, strtxt1);
                    strLat = objpFE.Lat2.ToString();
                    strLong = objpFE.Longit2.ToString();

                    strtxt1 = "Coordenadas";
                    objExcel.Pagina(50, 3, nsheet, strtxt1);

                    strtxt1 = "Latitude";
                    objExcel.Pagina(50, 6, nsheet, strtxt1);
                    strtxt1 = "Longitude";
                    objExcel.Pagina(51, 6, nsheet, strtxt1);

                    strLat = cUt.ReconverteCoord(strLat);
                    objExcel.Pagina(50, 8, nsheet, strLat);

                    strLong = cUt.ReconverteCoord(strLong);
                    objExcel.Pagina(51, 8, nsheet, strLong);


                }
            }


          





            

            objExcel.Pagina(60, 14, nsheet, npag.ToString());
            objExcel.MudaFonte(60, 14, nsheet, "Arial", 10);
            SetaPaginas(ObjPags, nsheet);
        }





        // ***********************************************************************************

        public void Pagina8()
        {
            nsheet = nsheet + 1;
            npag = npag + 1;
            string strtxt1 = "";

            string strVmdExiste = "";
            

            objExcel.Pagina(3, 13, nsheet, lblRod2.Text + "  km " + cUt.ConvPontoVirg(lblkmR2.Text));
            objExcel.MudaFonte(3, 13, nsheet, "Arial", 11);



            strtxt1 = "2 - LOCALIZAÇÃO DA INSTALAÇÃO";
            objExcel.Pagina(21, 3, nsheet, strtxt1, 14, "Arial Black");



            strtxt1 = "•	Local: " + lblRod2.Text + "- km " + cUt.ConvPontoVirg(lblkmR2.Text) + " - " + lblMun2.Text;
            objExcel.Pagina(22, 3, nsheet, strtxt1,12, "Black");

            strtxt1 = "•	Pista principal ";
            objExcel.Pagina(23, 3, nsheet, strtxt1,12, "Black");

            strtxt1 = "•	Sentido do fluxo fiscalizado: " + lblSent2.Text;
            objExcel.Pagina(24, 3, nsheet, strtxt1,12, "Black");

            strtxt1 = "•	Faixa (s) de trânsito (circulação) fiscalizada (s) (numeração da esquerda para direita): ";
            objExcel.Pagina(25, 3, nsheet, strtxt1,12, "Black");

            VePistaEFaixas(CPtFx);
            VeGeometria(objGeom, objpFE);

            strtxt1 = "";
            objExcel.Pagina(26, 3, nsheet, strtxt1);
            if (CPtFx.Fx1_1 != 0) { strtxt1 = " " + CPtFx.Fx1_1.ToString(); }
            if (CPtFx.Fx1_2 != 0) { strtxt1 = strtxt1 + " - " + CPtFx.Fx1_2.ToString(); }
            if (CPtFx.Fx1_3 != 0) { strtxt1 = strtxt1 + " - " + CPtFx.Fx1_3.ToString(); }

            if ( (strtxt1 != "") && (objGeom.umsent!=1))
            {
                strtxt1 = "Faixa(s) " + strtxt1 + " - Sentido: " + lblMB2.Text;
                objExcel.Pagina(26, 3, nsheet, strtxt1);
            }

            if ((strtxt1 != "") && (objGeom.umsent == 1))
            {
                strtxt1 = "Faixa(s) " + strtxt1 + " - Sentido: " + lblSent2.Text;
                objExcel.Pagina(26, 3, nsheet, strtxt1);
            }


            strtxt1 = "";
            objExcel.Pagina(27, 3, nsheet, strtxt1);
            if (CPtFx.Fx2_1 != 0) { strtxt1 = " " + CPtFx.Fx2_1.ToString(); }
            if (CPtFx.Fx2_2 != 0) { strtxt1 = strtxt1 + " - " + CPtFx.Fx2_2.ToString(); }
            if (CPtFx.Fx2_3 != 0) { strtxt1 = strtxt1 + " - " + CPtFx.Fx2_3.ToString(); }

            if (strtxt1 != "")
            {
                strtxt1 = "Faixa(s) " + strtxt1 + " - Sentido: " + lblMA2.Text;
                objExcel.Pagina(27, 3, nsheet, strtxt1);
            }


            strtxt1 = "3- EQUIPAMENTO";
            objExcel.Pagina(31, 3, nsheet, strtxt1, 14, "Black");

            switch (cUt.VeTipoEquip(lblTipo2.Text))
            {

                case "I":
                    {
                        objExcel.InsereChk(34, 3);
                        objExcel.InsereChkB(33, 3);
                        break;
                    }

                case "II":
                    {
                        objExcel.InsereChk(33, 3);
                        objExcel.InsereChkB(34, 3);
                        break;
                    }

                case "III":
                    {
                        objExcel.InsereChk(34, 3);
                        objExcel.InsereChkB(33, 3);
                        break;
                    }






            }

            //•	N. de pistas:
            // •	N. de faixas de trânsito (circulação) no sentido fiscalizado: 

            strtxt1 = "4-CARACTERÍSTICAS DO LOCAL/TRECHO DA VIA";
            objExcel.Pagina(38, 3, nsheet, strtxt1, 14, "Black");

            objExcel.Pagina(39, 3, nsheet, "·Classificação viária (art. 60 do CTB): Rodovia");

            strtxt1 = CPtFx.NPistas.ToString();
            strtxt1 = cUt.FazExtenso(strtxt1);
            objExcel.Pagina(40, 3, nsheet, "•N. de pista(s): " + strtxt1 + " Pista(s)");
            strtxt1 = CPtFx.NFaixas.ToString();
            strtxt1 = cUt.FazExtenso(strtxt1);
            objExcel.Pagina(41, 3, nsheet, "•N. de faixa(s) de trânsito (circulação) no sentido fiscalizado: " + strtxt1 + " Faixa(s)");




            ////strtxt1 = "Sentido " + lblMB.Text;
            ////objExcel.Pagina(48, 3, 7, strtxt1);

            //Geometria
            VeGeometria(objGeom, objpFE);

            if (objGeom.aclive == 1)
            { objExcel.InsereChk(44, 3); }
            else
            { objExcel.InsereChkB(44, 3); }

            if (objGeom.declive == 1)
            { objExcel.InsereChk(44, 5); }
            else
            { objExcel.InsereChkB(44, 5); }

            if (objGeom.plano == 1)
            { objExcel.InsereChk(44, 7); }
            else
            { objExcel.InsereChkB(44, 7); }

            if (objGeom.curva == 1)
            { objExcel.InsereChk(44, 9); }
            else
            { objExcel.InsereChkB(44, 9); }

            if ( (objGeom.paolongo == 1) || (objGeom.ptrans == 1) )
            { objExcel.InsereChk(51, 6);
                objExcel.InsereChkB(52, 6);
            }
            else
            { objExcel.InsereChkB(51, 6);
                objExcel.InsereChk(52, 6);
            }

            if (objGeom.paolongo == 1)
            { objExcel.InsereChk(51, 8); }
            else
            { objExcel.InsereChkB(51, 8); }

            if (objGeom.ptrans == 1)
            { objExcel.InsereChk(51, 11); }
            else
            { objExcel.InsereChkB(51, 11); }

            if ((objGeom.caolongo == 1) || (objGeom.ctrans == 1))
            { objExcel.InsereChk(54, 6);
                objExcel.InsereChkB(55, 6);
            }
            else
            { objExcel.InsereChkB(54, 6);
                objExcel.InsereChk(55, 6);
            }

            if (objGeom.caolongo == 1)
            { objExcel.InsereChk(54, 8);
            }
            else
            { objExcel.InsereChkB(54, 8); }

            if (objGeom.ctrans == 1)
            { objExcel.InsereChk(54, 11); }
            else
            { objExcel.InsereChkB(54, 11); }


            if (objGeom.urbano == 1)
            { objExcel.InsereChk(46, 6);
                objExcel.InsereChkB(46, 9);
            }
            else
            { objExcel.InsereChkB(46, 6);
                objExcel.InsereChk(46, 9);

            }




            if (objGeom.umsent != 1)
            {

                strtxt1 = "·Fluxo Veícular na pista fiscalizada (VMD): " +cUt.PoePonto(lblVmd2.Text) + " ( Sentido " + lblMB2.Text + " )";
                objExcel.Pagina(48, 3, nsheet, strtxt1);

                strtxt1 = "·Fluxo Veícular na pista fiscalizada (VMD): " + cUt.PoePonto(VmdB.ToString()) + " ( Sentido " + lblMA2.Text + " )";
                objExcel.Pagina(49, 3, nsheet, strtxt1);

            }
            else
            {
                if (objpFE.VMD != 0)
                {
                    strtxt1 = objpFE.MunB.ToString();
                    strVmdExiste = objpFE.VMD.ToString();
                }
                else
                { 
                    strtxt1 = objpFE.MunA.ToString();
                    strVmdExiste = objpFE.VmdB.ToString();
                }

                strtxt1 = "·Fluxo Veícular na pista fiscalizada (VMD): " + cUt.PoePonto(strVmdExiste) + " ( Sentido " + objpFE.MunSen.ToString() + " )";
                objExcel.Pagina(48, 3, nsheet, strtxt1);

                strtxt1 = "·                             ";
                objExcel.Pagina(49, 3, nsheet, strtxt1);
            }


            //strtxt1 = "·                 Fluxo Veícular na pista fiscalizada (VMD): " + lblVmd2.Text + " ( Sentido " + lblMB2.Text + " )";
            //objExcel.Pagina(48, 3, nsheet, strtxt1);

            //if (VmdB!=0)
            //{
            //    strtxt1 = "·Fluxo Veícular na pista fiscalizada (VMD): " + cUt.PoePonto(VmdB.ToString()) + " ( Sentido " + lblMA2.Text + " )";
            //    objExcel.Pagina(49, 3, nsheet, strtxt1);
            //}else
            //{
            //    strtxt1 = "·                             ";
            //    objExcel.Pagina(49, 3, nsheet, strtxt1);
            //}


            //objExcel.Alinhar(44, 3, "Right"); objExcel.Alinhar(44, 5, "Right"); objExcel.Alinhar(44, 7, "Right"); objExcel.Alinhar(44, 9, "Right");

            
            objExcel.Pagina(42, 3, nsheet, ".Geometria:");

           
            objExcel.Pagina(46, 3, nsheet, ".Trecho Urbano:");

          
            objExcel.Pagina(51, 3, nsheet, ".Trânsito de pedestre:");

          //  objExcel.Alinhar(54, 3, "Left");
            objExcel.Pagina(54, 3, nsheet, ".Trânsito de ciclista:");


            
            objExcel.Alinhar2(44, 3, "Right");
            objExcel.Alinhar2(44, 5, "Right");
            objExcel.Alinhar2(44, 7, "Right");
            objExcel.Alinhar2(44, 9, "Right");


            objExcel.Alinhar2(46, 6, "Right");
            objExcel.Alinhar2(46, 9, "Right");

            objExcel.Alinhar2(51, 6, "Right");
            objExcel.Alinhar2(51, 8, "Right");
            objExcel.Alinhar2(51, 11, "Right");

            objExcel.Alinhar2(52, 6, "Right");

            objExcel.Alinhar2(54, 6, "Right");
            objExcel.Alinhar2(54, 8, "Right");
            objExcel.Alinhar2(54, 11, "Right");





            objExcel.Pagina(59, 14, nsheet, npag.ToString());
            objExcel.MudaFonte(60, 14, nsheet, "Arial", 10);
            SetaPaginas(ObjPags, nsheet);
        }


        // *******************************************************************************************************


        public void Pagina9()
        {
            

            nsheet = nsheet + 1;
            npag = npag + 1;
            objExcel.Pagina(3, 13, nsheet, lblRod2.Text + "  km " + cUt.ConvPontoVirg(lblkmR2.Text));
            objExcel.MudaFonte(3, 13, nsheet, "Arial", 11);

            objExcel.LarguraColunaExcel(nsheet, 1, 8.11);
            objExcel.LarguraColunaExcel(nsheet, 2, 8);
            objExcel.LarguraColunaExcel(nsheet, 3, 10.30);
            objExcel.LarguraColunaExcel(nsheet, 4, 7.56);
            objExcel.LarguraColunaExcel(nsheet, 12, 5.89);

            objExcel.Alinhar(5, 3, "Left");
            objExcel.Pagina(5, 3, nsheet, "5 - VELOCIDADE");

            VeGeometria(objGeom, objpFE);

            if (objGeom.umsent == 1)
            { // Se tem só um sentido 
                
                if (objpFE.VMD!=0)
                {
                    objExcel.Pagina(10, 4, nsheet, "Velocidade regulamentada:  " + lblVF2.Text + " km/h");
                    objExcel.Pagina(11, 3, nsheet, "                  Velocidade praticada (85 percentil): " + objpFE.Vel85p.ToString() + " km/h ( Sentido " + objpFE.MunB.ToString() + " )");
                    objExcel.Pagina(12, 3, nsheet, "                                                                 ");
                    objExcel.Pagina(15, 4, nsheet, lblVF2.Text + " km/h");
                    objExcel.Pagina(17, 4, nsheet, lblVF2.Text + " km/h");
                    objExcel.MudaFonte(15, 4, nsheet, "Bold", 12);
                    objExcel.MudaFonte(17, 4, nsheet, "Bold", 12);
                }
                else
                {
                    objExcel.Pagina(10, 4, nsheet, "Velocidade regulamentada:  " + lblVF2.Text + " km/h"); 
                    objExcel.Pagina(11, 3, nsheet, "                  Velocidade praticada (85 percentil): " + objpFE.Vel85pSB.ToString() + " km/h ( Sentido " + objpFE.MunA.ToString() + " )");
                    objExcel.Pagina(12, 3, nsheet, "      ");
                    objExcel.Pagina(15, 4, nsheet, "Velocidade regulamentada: " + lblVF2.Text + " km/h");
                    objExcel.Pagina(17, 4, nsheet, "Velocidade regulamentada: " + lblVF2.Text + " km/h");
                    objExcel.MudaFonte(15, 4, nsheet, "Bold", 12);
                    objExcel.MudaFonte(17, 4, nsheet, "Bold", 12);

                }



                //if (objpFE.VmdB != 0)
                //{
                //    objExcel.Pagina(10, 4, nsheet, "Velocidade regulamentada:  " + lblVF2.Text + " km/h");
                //    objExcel.Pagina(11, 3, nsheet, "                 Velocidade praticada (85 percentil): " + objpFE.Vel85pSB.ToString() + " km/h ( Sentido " + objGeom.sb + " )");
                //    objExcel.Pagina(12, 3, nsheet, "                                                                 ");
                //    objExcel.Pagina(15, 4, nsheet, lblVF2.Text + " km/h");
                //    objExcel.Pagina(17, 4, nsheet, lblVF2.Text + " km/h");
                //    objExcel.MudaFonte(15, 4, nsheet, "Bold", 12);
                //    objExcel.MudaFonte(17, 4, nsheet, "Bold", 12);
                //}

                //objExcel.Pagina(10, 4, nsheet, "Velocidade regulamentada:  " + lblVF2.Text + " km/h");
                //objExcel.Pagina(11, 3, nsheet, "                 Velocidade praticada (85 percentil): " + lblV852.Text + " km/h ( Sentido " + lblSent2.Text + " )");
                //objExcel.Pagina(12, 3, nsheet, "                                                                 ");
                //objExcel.Pagina(15, 4, nsheet, lblVF2.Text + " km/h");
                //objExcel.Pagina(17, 4, nsheet, lblVF2.Text + " km/h");
                //objExcel.MudaFonte(15, 4, nsheet, "Bold", 12);
                //objExcel.MudaFonte(17, 4, nsheet, "Bold", 12);
            }
            else
            {
                // Se tem dois sentidos
                // Vel85pSB É A VELOCIDADE DE 85 PERCENTIL DO outro sentido

                if (Vel85pSB != 0)
                {
                    objExcel.Pagina(10, 4, nsheet, "Velocidade regulamentada:  " + lblVF2.Text + " km/h");
                    objExcel.Pagina(11, 3, nsheet, "                  Velocidade praticada (85 percentil): " + lblV852.Text + " km/h ( Sentido " + objGeom.sa + " )");
                    objExcel.Pagina(12, 3, nsheet, "                  Velocidade praticada (85 percentil): " + Vel85pSB + " km/h ( Sentido " + objGeom.sb + " )");
                    objExcel.Pagina(15, 4, nsheet, "Velocidade regulamentada: " + lblVF2.Text + " km/h");
                    objExcel.Pagina(17, 4, nsheet, "Velocidade regulamentada: " + lblVF2.Text + " km/h");
                    objExcel.MudaFonte(15, 4, nsheet, "Bold", 12);
                    objExcel.MudaFonte(17, 4, nsheet, "Bold", 12);
                }
                else
                {
                    objExcel.Pagina(10, 4, nsheet, "Velocidade regulamentada:  " + lblVF2.Text + " km/h");
                    objExcel.Pagina(11, 3, nsheet, "                  Velocidade praticada (85 percentil): " + lblV852.Text + " km/h ( Sentido " + objGeom.sa + " )");
                    objExcel.Pagina(12, 3, nsheet, "      ");
                    objExcel.Pagina(15, 4, nsheet, "Velocidade regulamentada: " + lblVF2.Text + " km/h");
                    objExcel.Pagina(17, 4, nsheet, "Velocidade regulamentada: " + lblVF2.Text + " km/h");
                    objExcel.MudaFonte(15, 4, nsheet, "Bold", 12);
                    objExcel.MudaFonte(17, 4, nsheet, "Bold", 12);
                }
            }

            objExcel.Alinhar(11, 3, "Left");
            objExcel.Alinhar(12, 3, "Left");


            objExcel.Pagina(15, 9, nsheet, "        ");
            objExcel.Pagina(17, 9, nsheet, "        ");


            objExcel.Pagina(60, 14, nsheet, npag.ToString());
            objExcel.MudaFonte(60, 14, nsheet, "Arial", 10);
            SetaPaginas(ObjPags, nsheet);
        }









        // ***************************************************************************************

        // *************************************************************************************************************
        public void Pagina10_2()
        {
            nsheet = nsheet + 1;
            npag = npag + 1;

            int ind1 = 0;

            int ValInterv = 0;
            double Parc85pct = 0;

            double diferenca = 0;
            int desvio = 0;

            int lin32 = 32;
            int colQ17 = 17;

            int lim1 = 0;
            int lim2 = 0;
            double dlim = 0;


            string oSent = "AB";
            C_tab85p obj85p = new C_tab85p();

            ////double faixaAntV = ((objpFE.Vel85p) - 10) / 10;
            ////faixaAntV = Math.Truncate(faixaAntV);


            VeGeometria(objGeom, objpFE);
            VePontoFE(objpFE);

            DescobreQualFaixaVelocidade(objVel2, objpFE.Vel85p);


            Ve85PectSEntido(obj85p, oSent);

            objExcel.PaginaD(12, 33, nsheet, objpFE.VMD);
            objExcel.PaginaD(13, 33, nsheet, objpFE.VMD * 0.85);

            objExcel.PaginaD(17, 33, nsheet, objpFE.Vel85p);

            if (objpFE.Vel85p < 100)
            { dlim = objpFE.Vel85p / 10; }
            else
            { dlim = objpFE.Vel85p / 100; }

            dlim = Math.Truncate(dlim);
            lim1 = Int32.Parse(dlim.ToString()); // limite superior da escaala de velocidade 85 %

            lim2 = lim1 + 1;  // limite inferior da escala de velocidade abaixo de 85 %


            // Parc85pct = Math.Truncate(objpFE.VMD * 0.85);
            Parc85pct = (objpFE.VMD * 0.85);


           for(ind1=1;ind1<=14;ind1++)
            {

                objExcel.Pagina((ind1+16), 9, nsheet, obj85p.ptcv[ind1]);

            }


           



            objExcel.Pagina(3, 13, nsheet, lblRod2.Text + "  km " + cUt.ConvPontoVirg(lblkmR2.Text));
            objExcel.MudaFonte(3, 13, nsheet, "Arial", 11);
            // objExcel.Pagina(3, 13, nsheet, lblRod2.Text + "  km " + cUt.ConvPontoVirg(lblkmR2.Text));

            objExcel.Pagina(5, 3, nsheet, "Cálculo do 85 Percentil - Sentido " + objpFE.MunA + "/" + objpFE.MunB);



            //if (objGeom.umsent != 1)
            //{
            //    objExcel.Pagina(5, 3, nsheet, "Cálculo do 85 Percentil - Sentido " + lblMA2.Text + "/" + lblMB2.Text);
            //}
            //else
            //{
            //    objExcel.Pagina(5, 3, nsheet, "Cálculo do 85 Percentil - Sentido " + lblSent2.Text);
            //}

            objExcel.Pagina(60, 14, nsheet, npag.ToString());
            objExcel.MudaFonte(60, 14, nsheet, "Arial", 10);

            SetaPaginas(ObjPags, nsheet);


            if (objpFE.VMD == 0) { npag--; }
           


        }


        // *************************************************************************************************************



        // *************************************************************************************************************
        public void Pagina11_2()
        {
            nsheet = nsheet + 1;
            npag = npag + 1;

            int ind1 = 0;

            int ValInterv = 0;
            double Parc85pct = 0;

            double diferenca = 0;
            int desvio = 0;

            int lin32 = 32;
            int colQ17 = 17;

            int lim1 = 0;
            int lim2 = 0;
            double dlim = 0;


            string oSent = "BA";
            C_tab85p obj85p = new C_tab85p();

            ////double faixaAntV = ((objpFE.Vel85p) - 10) / 10;
            ////faixaAntV = Math.Truncate(faixaAntV);


            VeGeometria(objGeom, objpFE);
            VePontoFE(objpFE);

            DescobreQualFaixaVelocidade(objVel2, objpFE.Vel85p);


            Ve85PectSEntido(obj85p, oSent);

            objExcel.PaginaD(12, 33, nsheet, objpFE.VMD);
            objExcel.PaginaD(13, 33, nsheet, objpFE.VMD * 0.85);

            objExcel.PaginaD(17, 33, nsheet, objpFE.Vel85p);

            if (objpFE.Vel85p < 100)
            { dlim = objpFE.Vel85p / 10; }
            else
            { dlim = objpFE.Vel85p / 100; }

            dlim = Math.Truncate(dlim);
            lim1 = Int32.Parse(dlim.ToString()); // limite superior da escaala de velocidade 85 %

            lim2 = lim1 + 1;  // limite inferior da escala de velocidade abaixo de 85 %


            // Parc85pct = Math.Truncate(objpFE.VMD * 0.85);
            Parc85pct = (objpFE.VMD * 0.85);


            for (ind1 = 1; ind1 <= 14; ind1++)
            {

                objExcel.Pagina((ind1 + 16), 9, nsheet, obj85p.ptcv[ind1]);

            }























            objExcel.Pagina(3, 13, nsheet, lblRod2.Text + "  km " + cUt.ConvPontoVirg(lblkmR2.Text));
            objExcel.MudaFonte(3, 13, nsheet, "Arial", 11);
            // objExcel.Pagina(3, 13, nsheet, lblRod2.Text + "  km " + cUt.ConvPontoVirg(lblkmR2.Text));


            objExcel.Pagina(5, 3, nsheet, "Cálculo do 85 Percentil - Sentido " + objpFE.MunB + "/" + objpFE.MunA);

            //if (objGeom.umsent != 1)
            //{
            //    objExcel.Pagina(5, 3, nsheet, "Cálculo do 85 Percentil - Sentido " + lblMA2.Text + "/" + lblMB2.Text);
            //}
            //else
            //{
            //    objExcel.Pagina(5, 3, nsheet, "Cálculo do 85 Percentil - Sentido " + lblSent2.Text);
            //}

            objExcel.Pagina(60, 14, nsheet, npag.ToString());
            objExcel.MudaFonte(60, 14, nsheet, "Arial", 10);

            SetaPaginas(ObjPags, nsheet);

            if (objpFE.VmdB == 0) { npag--; }

        }


        // *************************************************************************************************************












        // *************************************************************************************************************
        public void Pagina10()
        {
            nsheet = nsheet + 1;
            npag = npag + 1;

            int ind1 = 0;

            int ValInterv = 0;
            double Parc85pct = 0;

            double diferenca = 0;
            int desvio=0;

            int lin32 = 32;
            int colQ17 = 17;

            int lim1 = 0;
            int lim2 = 0;
            double dlim = 0;

            ////double faixaAntV = ((objpFE.Vel85p) - 10) / 10;
            ////faixaAntV = Math.Truncate(faixaAntV);


            VeGeometria(objGeom, objpFE);
            VePontoFE(objpFE);

            DescobreQualFaixaVelocidade(objVel2, objpFE.Vel85p);

            objExcel.PaginaD(12, 33, nsheet, objpFE.VMD);
            objExcel.PaginaD(13, 33, nsheet,objpFE.VMD * 0.85);

            objExcel.PaginaD(17, 33, nsheet, objpFE.Vel85p);

            if (objpFE.Vel85p < 100)
            { dlim = objpFE.Vel85p / 10; }
            else
            { dlim = objpFE.Vel85p / 100; }

            dlim= Math.Truncate(dlim);
            lim1 = Int32.Parse(dlim.ToString()); // limite superior da escaala de velocidade 85 %

            lim2 = lim1+1;  // limite inferior da escala de velocidade abaixo de 85 %


           // Parc85pct = Math.Truncate(objpFE.VMD * 0.85);
            Parc85pct = (objpFE.VMD * 0.85);


            double[] Valorp = new double[15];
            for (ind1 = 1; ind1 <= lim1; ind1++)
            {
                if (ind1==1)
                { Valorp[ind1] = (Parc85pct * objVel2.Veln[ind1]) / 19; }
                else
                {
                    
                    { Valorp[ind1] = (Parc85pct * objVel2.Veln[ind1]) / 10; }
                }
            }

            for (ind1 = lim2; ind1 <= 14; ind1++)
            {
                Valorp[ind1] = ((objpFE.VMD - Parc85pct) * objVel2.Veln[ind1]) / 10;                 
            }







            int Qjaforam = 0;

            for (ind1=1;ind1<=lim1;ind1++)
            {
                if (ind1 != lim1)
                {
                    if (ind1 == 1)
                    {
                        for (int ind19 = 1; ind19 <= 19; ind19++)
                        { objExcel.PaginaD(lin32, colQ17, nsheet, Valorp[ind1]);
                            Qjaforam++;
                            lin32++;
                        } // o ind1 vale 1 
                    }

                    else
                    {
                        for (int ind10 = 1; ind10 <= 10; ind10++)
                        { objExcel.PaginaD(lin32, colQ17, nsheet, Valorp[ind1]);
                            Qjaforam++;
                            lin32++;
                        } // o ind1 vale 1 
                    }

                }

                else
                {

                    MontaDivisoresFaixaVelocidade(opjDivp, objpFE.Vel85p);

                    // ind1= lim1
                    int oindagora = ind1;
                    for (int ind10 = 1; ind10 <= 10; ind10++)
                    {
                        //  (Parc85pct * objVel2.Veln[lim1]) / 10;

                        objExcel.PaginaD(lin32, colQ17, nsheet, ( (Parc85pct * objVel2.Veln[lim1]) / opjDivp.partDiv[ind10])) ;
                        Qjaforam++;
                        lin32++;
                        if (Qjaforam == objpFE.Vel85p)
                        { //oindagora = ind1 + 1;
                            ind10 = 11;
                        }
                    } // o ind1 vale 1 

                }
            }







            for (ind1 = lim2; ind1 <= 14; ind1++)
            {
                
                    
                    int oindagora = ind1;
                    for (int ind10 = 1; ind10 <= 10; ind10++)
                    {
                        objExcel.PaginaD(lin32, colQ17, nsheet, Valorp[ind1]);
                        Qjaforam++;
                        lin32++;
                        if (Qjaforam == objpFE.Vel85p)
                        { oindagora = ind1 + 1; }
                    } // o ind1 vale 1 

               
            }









            objExcel.Pagina(3, 13, nsheet, lblRod2.Text + "  km " + cUt.ConvPontoVirg(lblkmR2.Text));
            objExcel.MudaFonte(3, 13, nsheet, "Arial", 11);
            // objExcel.Pagina(3, 13, nsheet, lblRod2.Text + "  km " + cUt.ConvPontoVirg(lblkmR2.Text));

            if (objGeom.umsent != 1)
            {
                objExcel.Pagina(5, 3, nsheet, "Cálculo do 85 Percentil - Sentido " + lblMA2.Text + "/" + lblMB2.Text);
            }
            else
            {
                objExcel.Pagina(5, 3, nsheet, "Cálculo do 85 Percentil - Sentido " + lblSent2.Text);
            }

            objExcel.Pagina(60, 14, nsheet, npag.ToString());
            objExcel.MudaFonte(60, 14, nsheet, "Arial", 10);

            SetaPaginas(ObjPags, nsheet);
        }


        // *************************************************************************************************************


        public void Pagina11()
        {
            int ind1 = 0;

            int ValInterv = 0;
            double Parc85pct = 0;

            double diferenca = 0;
            int desvio = 0;

            int lin32 = 32;
            int colQ17 = 17;

            int lim1 = 0;
            int lim2 = 0;
            double dlim = 0;

            double faixaAntV = ((objpFE.Vel85pSB) - 10) / 10;
            faixaAntV = Math.Truncate(faixaAntV);

            // Se não for duplo, manda pular aqui
            nsheet = nsheet + 1;
            if (objGeom.umsent == 0) // Tem 2 sentidos
            {
                npag = npag + 1; //só incrementa o número da página se entrar aqui
                objExcel.Pagina(3, 13, nsheet, lblRod2.Text + "  km " + cUt.ConvPontoVirg(lblkmR2.Text));
                objExcel.MudaFonte(3, 13, nsheet, "Arial", 11);

                objExcel.Pagina(5, 3, nsheet, "Cálculo do 85 Percentil - Sentido " + lblMB2.Text + "/" + lblMA2.Text);

                objExcel.Pagina(60, 14, nsheet, npag.ToString());
                objExcel.MudaFonte(60, 14, nsheet, "Arial", 10);
                SetaPaginas(ObjPags, nsheet);

                VeGeometria(objGeom, objpFE);
                VePontoFE(objpFE);

                DescobreQualFaixaVelocidade(objVel2, objpFE.Vel85pSB);

                objExcel.PaginaD(12, 33, nsheet, objpFE.VmdB);
                objExcel.PaginaD(13, 33, nsheet, objpFE.VmdB * 0.85);

                objExcel.PaginaD(17, 33, nsheet, objpFE.Vel85pSB);

                if (objpFE.Vel85pSB < 100)
                { dlim = objpFE.Vel85pSB / 10; }
                else
                { dlim = objpFE.Vel85pSB / 100; }

                dlim = Math.Truncate(dlim);
                lim1 = Int32.Parse(dlim.ToString()); // limite superior da escaala de velocidade 85 %

                lim2 = lim1 + 1;  // limite inferior da escala de velocidade abaixo de 85 %


                // Parc85pct = Math.Truncate(objpFE.VMD * 0.85);
                Parc85pct = (objpFE.VmdB * 0.85);


                double[] Valorp = new double[15];
                for (ind1 = 1; ind1 <= lim1; ind1++)
                {
                    if (ind1 == 1)
                    { Valorp[ind1] = (Parc85pct * objVel2.Veln[ind1]) / 19; }
                    else
                    {

                        { Valorp[ind1] = (Parc85pct * objVel2.Veln[ind1]) / 10; }
                    }
                }

                for (ind1 = lim2; ind1 <= 14; ind1++)
                {
                    Valorp[ind1] = ((objpFE.VmdB - Parc85pct) * objVel2.Veln[ind1]) / 10;
                }







                int Qjaforam = 0;

                for (ind1 = 1; ind1 <= lim1; ind1++)
                {
                    if (ind1 != lim1)
                    {
                        if (ind1 == 1)
                        {
                            for (int ind19 = 1; ind19 <= 19; ind19++)
                            {
                                objExcel.PaginaD(lin32, colQ17, nsheet, Valorp[ind1]);
                                Qjaforam++;
                                lin32++;
                            } // o ind1 vale 1 
                        }

                        else
                        {
                            for (int ind10 = 1; ind10 <= 10; ind10++)
                            {
                                objExcel.PaginaD(lin32, colQ17, nsheet, Valorp[ind1]);
                                Qjaforam++;
                                lin32++;
                            } // o ind1 vale 1 
                        }

                    }

                    else
                    {

                        MontaDivisoresFaixaVelocidade(opjDivp, objpFE.Vel85pSB);

                        // ind1= lim1
                        int oindagora = ind1;
                        for (int ind10 = 1; ind10 <= 10; ind10++)
                        {
                            //  (Parc85pct * objVel2.Veln[lim1]) / 10;

                            objExcel.PaginaD(lin32, colQ17, nsheet, ((Parc85pct * objVel2.Veln[lim1]) / opjDivp.partDiv[ind10]));
                            Qjaforam++;
                            lin32++;
                            if (Qjaforam == objpFE.Vel85pSB)
                            { //oindagora = ind1 + 1;
                                ind10 = 11;
                            }
                        } // o ind1 vale 1 

                    }
                }







                for (ind1 = lim2; ind1 <= 14; ind1++)
                {


                    int oindagora = ind1;
                    for (int ind10 = 1; ind10 <= 10; ind10++)
                    {
                        objExcel.PaginaD(lin32, colQ17, nsheet, Valorp[ind1]);
                        Qjaforam++;
                        lin32++;
                        if (Qjaforam == objpFE.Vel85pSB)
                        { oindagora = ind1 + 1; }
                    } // o ind1 vale 1 


                }






            }
        }


        // *************************************************************************************************************


        public void Pagina12()
        {
            String str1 = "";
            nsheet = nsheet + 1;
            npag = npag + 1;
            objExcel.Pagina(3, 12, nsheet, lblRod2.Text + "  km " + cUt.ConvPontoVirg(lblkmR2.Text));
            objExcel.MudaFonte(3, 12, nsheet, "Arial", 11);

            objExcel.Agrupar(7, 3, 7, 13);
            objExcel.Agrupar(8, 3, 8, 10); objExcel.Alinhar(8, 3, "Left");

            objExcel.Agrupar(10, 3, 10, 10); objExcel.Alinhar(10, 3, "Left");
            objExcel.Pagina(10, 3, nsheet, "Até 12 meses antes do início da fiscalização (interstício de 06 meses): ");

            //VeGeometria(objGeom);
            VeAcidentes(objAcid);

            objExcel.Pagina(16, 4, 12, objAcid.abalroamento.ToString());
            objExcel.Pagina(16, 5, 12, objAcid.choque.ToString());
            objExcel.Pagina(16, 6, 12, objAcid.colisao.ToString());
            objExcel.Pagina(16, 7, 12, objAcid.tombamento.ToString());
            objExcel.Pagina(16, 8, 12, objAcid.capotamento.ToString());
            objExcel.Pagina(16, 9, 12, objAcid.incendio.ToString());
            objExcel.Pagina(16, 10, 12, objAcid.atropelamento.ToString());

            str1 = "·       6- NÚMERO (N.) DE ACIDENTES NO LOCAL .............................................................." + npag.ToString();
            objExcel.Pagina(30, 3, 2, str1);

            objExcel.Pagina(60, 14, nsheet, npag.ToString());
            objExcel.MudaFonte(60, 14, nsheet, "Arial", 10);
            SetaPaginas(ObjPags, nsheet);
        }

        // *************************************************************************************************************

        public void Pagina13()
        {
            String str1 = "";
            nsheet = nsheet + 1;
            npag = npag + 1;
            int lin1 = 0;
           

            objExcel.Pagina(3, 12, 13, lblRod2.Text + "  km " + cUt.ConvPontoVirg(lblkmR2.Text));
            objExcel.MudaFonte(3, 12, nsheet, "Arial", 11);

            //VeGeometria(objGeom);
            // VeAcidentes(objAcid);    

            objExcel.Agrupar(10, 3, 10, 6); objExcel.Alinhar(10, 3, "Left");
            objExcel.Pagina(10, 3, nsheet, "Descrição dos fatores de risco –");
            objExcel.Pagina(10, 7, nsheet, " km " + cUt.ConvPontoVirg(lblkmR2.Text));
            objExcel.Pagina(12, 3, nsheet, "A localidade de " + lblLoc2.Text + ", no km " + cUt.ConvPontoVirg(lblkmR2.Text) + " da Rodovia " + lblRod2.Text + ", apresenta fatores de risco, tais como:");


            VeRiscos(objRisco);

            objExcel.Pagina(15, 3, 13, "   ");
            objExcel.Pagina(16, 3, 13, "   ");
            objExcel.Pagina(17, 3, 13, "   ");
            objExcel.Pagina(18, 3, 13, "   ");
            objExcel.Pagina(19, 3, 13, "   ");
            objExcel.Pagina(20, 3, 13, "   ");
            objExcel.Pagina(21, 3, 13, "   ");
            objExcel.Pagina(22, 3, 13, "   ");
            objExcel.Pagina(23, 3, 13, "   ");
            objExcel.Pagina(24, 3, 13, "   ");


            PoeTiraPonto(objRisco.fatr1.ToString(), 15, 3, 13);
            PoeTiraPonto(objRisco.fatr2.ToString(), 16, 3, 13);
            PoeTiraPonto(objRisco.fatr3.ToString(), 17, 3, 13);
            PoeTiraPonto(objRisco.fatr4.ToString(), 18, 3, 13);
            PoeTiraPonto(objRisco.fatr5.ToString(), 19, 3, 13);
            PoeTiraPonto(objRisco.fatr6.ToString(), 20, 3, 13);            
            PoeTiraPonto(objRisco.fatr7.ToString(), 21, 3, 13);
            PoeTiraPonto(objRisco.fatr8.ToString(), 22, 3, 13);
            PoeTiraPonto(objRisco.fatr9.ToString(), 23, 3, 13);
            PoeTiraPonto(objRisco.fatr10.ToString(), 24, 3, 13);

            lin1 = 15;
            objExcel.MudaFonte(lin1, 3, nsheet, "Arial",false, 12); lin1++;
            objExcel.MudaFonte(lin1, 3, nsheet, "Arial", false, 12); lin1++;
            objExcel.MudaFonte(lin1, 3, nsheet, "Arial", false, 12); lin1++;
            objExcel.MudaFonte(lin1, 3, nsheet, "Arial", false, 12); lin1++;
            objExcel.MudaFonte(lin1, 3, nsheet, "Arial", false, 12); lin1++;
            objExcel.MudaFonte(lin1, 3, nsheet, "Arial", false, 12); lin1++;
            objExcel.MudaFonte(lin1, 3, nsheet, "Arial", false, 12); lin1++;
            objExcel.MudaFonte(lin1, 3, nsheet, "Arial", false, 12); lin1++;
            objExcel.MudaFonte(lin1, 3, nsheet, "Arial", false, 12); lin1++;
            objExcel.MudaFonte(lin1, 3, nsheet, "Arial", false, 12);


            str1 = "·       7- POTENCIAL DE RISCO NO LOCAL ........................................................................." + npag.ToString();
            objExcel.Pagina(32, 3, 2, str1);

            objExcel.Pagina(60, 14, nsheet, npag.ToString());
            objExcel.MudaFonte(60, 14, nsheet, "Arial", 10);
            SetaPaginas(ObjPags, nsheet);
        }

        // *************************************************************************************************************


        public void Pagina14()
        {
            nsheet = nsheet + 1;
            npag = npag + 1;

            objExcel.Pagina(3, 12, 14, lblRod2.Text + "  km " + cUt.ConvPontoVirg(lblkmR2.Text));
            objExcel.MudaFonte(3, 12, nsheet, "Arial", 11);
            //VeGeometria(objGeom);
            // VeAcidentes(objAcid);


            objExcel.Pagina(16, 3, 14, "     ");
            objExcel.Pagina(17, 3, 14, "     ");
            objExcel.Pagina(18, 3, 14, "     ");
            objExcel.Pagina(19, 3, 14, "     ");
            objExcel.Pagina(20, 3, 14, "     ");

            VeHistorico(objHist);

            PoeTiraPonto(objHist.h1.ToString(), 16, 3, 14);
            PoeTiraPonto(objHist.h2.ToString(), 17, 3, 14);
            PoeTiraPonto(objHist.h3.ToString(), 18, 3, 14);
            PoeTiraPonto(objHist.h4.ToString(), 19, 3, 14);
            PoeTiraPonto(objHist.h5.ToString(), 20, 3, 14);
            


            objExcel.Pagina(60, 14, nsheet, npag.ToString());
            objExcel.MudaFonte(60, 14, nsheet, "Arial", 10);
            SetaPaginas(ObjPags, nsheet);
        }

        // *************************************************************************************************************

        private void PoeTiraPonto(string str1,int linha,int coluna, int sheet)
        {
            if (str1 != "")
            { objExcel.Pagina(linha, coluna, sheet, str1 + "."); }

        }

        // *************************************************************************************************


        public void Pagina15()
        {
            //Mapa
            String str1 = "";
            int linhapag = 0;
            int colpag = 0;

            int colcab = 0;

            VeGeometria(objGeom, objpFE);

            nsheet = nsheet + 1;
            npag = npag + 1;
            // objExcel.Pagina(3, 15, nsheet, lblRod2.Text + "  km " + cUt.ConvPontoVirg(lblkmR2.Text));

            objExcel.Pagina(6, 3, nsheet, "      ");
            objExcel.Agrupar(6, 3, 47, 15);
            objExcel.Pagina(6, 3, nsheet, "      ");

            ////for (int indl1 = 40; indl1 <= 47; indl1++)
            ////{
            ////    if (indl1 == 5) { indl1++; }

            ////    for (int indc1 = 14; indc1 <= 20; indc1++)
            ////    {
            ////        objExcel.Pagina(indl1, indc1, nsheet, "       "); // Apaga todas as linhas e colunas da pagina
            ////        objExcel.Pagina(3, 24, nsheet, indl1.ToString());
            ////    }

            ////}


            str1 = "·       8 - PROJETO OU CROQUI DO LOCAL ........................................................................" + npag.ToString();
            objExcel.Pagina(34, 3, 2, str1);


            
            if (objGeom.forma=="retrato") { linhapag = 77; colpag = 15; colcab = 11; }
            if (objGeom.forma == "paisagem") { linhapag = 45; colpag = 17 ; colcab = 13; }

            objExcel.Pagina(59, 13, nsheet, "       "); // LIMPA
            objExcel.Pagina(59, 16, nsheet, "       "); // LIMPA
            objExcel.Pagina(59, 17, nsheet, "       "); // LIMPA

            objExcel.Pagina(43, 13, nsheet, "       "); // LIMPA
            objExcel.Pagina(43, 16, nsheet, "       "); // LIMPA
            objExcel.Pagina(43, 17, nsheet, "       "); // LIMPA

            objExcel.Pagina(45, 13, nsheet, "       "); // LIMPA
            objExcel.Pagina(45, 16, nsheet, "       "); // LIMPA
            objExcel.Pagina(45, 17, nsheet, "       "); // LIMPA

          // objExcel.Pagina(43, 17, nsheet, "       "); // LIMPA
            objExcel.Pagina(3, 12, nsheet, "       ");  // LIMPA
            objExcel.Pagina(3, 15, nsheet, "       ");   // LIMPA

            objExcel.Pagina(58, 13, nsheet, "       "); // LIMPA
            objExcel.Pagina(58, 16, nsheet, "       "); // LIMPA

          //  objExcel.Pagina(40, 17, nsheet, "       "); // LIMPA
            objExcel.Pagina(3, 12, nsheet, "       ");  // LIMPA
            objExcel.Pagina(3, 11, nsheet, "       ");  // LIMPA
            objExcel.Pagina(3, 15, nsheet, "       ");   // LIMPA

            if (objGeom.forma == "retrato") { objExcel.Agrupar(3, 11, 3, 12); }
            if (objGeom.forma == "paisagem") { objExcel.Agrupar(3, 13, 3, 15); }
            objExcel.Pagina(3, colcab, nsheet, lblRod2.Text + "  km " + cUt.ConvPontoVirg(lblkmR2.Text),11,"Gray");
            objExcel.MudaFonte(3, colcab, nsheet, "Arial", 11);
            if (objGeom.forma == "retrato") { objExcel.MudaCor("K3:M3", "Gray"); }
            if (objGeom.forma == "paisagem") { objExcel.MudaCor("M3:O3", "Gray"); }


            objExcel.Pagina(linhapag, colpag, nsheet, npag.ToString());
            objExcel.MudaFonte(linhapag, colpag, nsheet, "Arial", 10);
            //objExcel.MudaCor(linhapag, colcab, "Black");
            //objExcel.MudaCor(3, colcab, "Gray");

            //  objExcel.MudaCor(linhapag, colcab, "Black");
            if (objGeom.forma == "retrato") { objExcel.MudaCor("O59:P59", "Black"); }
            if (objGeom.forma == "paisagem") { objExcel.MudaCor("Q43:R43", "Black"); }
            SetaPaginas(ObjPags, nsheet);
        }

        // *************************************************************************************************************
        // *************************************************************************************************************












        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************




        public void Pagina16()
        {
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

            //Boolean temSAB = false;
            //Boolean temSBA = false;
            Boolean entrou = false;

            String strDescTipo = "";

            String aRod1;
            String okm1;
            string strprojTit = "";

            C_pontofe objPFe = new C_pontofe();

            VeTemFE(objTemFE);
         
            //if (objTemFE.VaiTer) && ()

            for (ind1=7;ind1<=57;ind1++)
            {
                objExcel.Pagina(ind1, 3, nsheet, "                                    ");

            }


            if  ((lblTipo2.Text)=="I.A")  { strDescTipo = " Equipamento Redutor de Velocidade com Display.";   }
            if ((lblTipo2.Text) == "I.B") { strDescTipo = " Equipamento Controlador de Velocidade."; }
            if (cUt.VeTipoEquip(lblTipo2.Text) == "II") { strDescTipo = " Equipamento tipo Radar Fixo."; }
            if (cUt.VeTipoEquip(lblTipo2.Text) == "III") { strDescTipo = " Equipamento tipo Semáforo Radar."; }

            C_Placas objPlacasIncMunB = new C_Placas();
            C_Placas objPlacasRemMunB = new C_Placas();
            C_Placas objPlacasRepMunB = new C_Placas();

            C_Placas objPlacasIncMunA = new C_Placas();
            C_Placas objPlacasRemMunA = new C_Placas();
            C_Placas objPlacasRepMunA = new C_Placas();

            string strRod;  string MunA="";  string MunB="";
            
            strRod = lblRod2.Text;
            objDtb.GetMunicipios(lblRod2.Text, ref MunA, ref MunB);  // obtem os municípios limite


            objExcel.Pagina(3, 12, nsheet, lblRod2.Text + "  km " + cUt.ConvPontoVirg(lblkmR2.Text));
            objExcel.MudaFonte(3, 12, nsheet, "Arial", 11);

            objExcel.Desagrupar(24,3);

            // objExcel.Agrupar(24,3,24,13);
            // objExcel.Agrupar(25, 3, 25, 13);
            objExcel.Agrupar(24, 3, 25, 13);
            objExcel.Agrupar(26, 3, 26, 13);

            //VeGeometria(objGeom);
            // VeAcidentes(objAcid);
            // VeHistorico(objHist);




            Veproj(objproj);

            inc1 = (objproj.inclusao==1);
            rem1 = (objproj.remocao==1);
            rep1 = (objproj.reposicionar==1);
            mant1= (objproj.manter == 1);

            objproj.kmEdit = cUt.ConvPontoVirg(objproj.kmEdit);


            if ((!inc1 && !rem1 && !rep1 && !mant1))
            { strproj1 = "O projeto no trecho estudado, da rodovia " + objproj.Rodovia + " no km " + objproj.kmEdit + " não necessita de inclusão, remoção ou reposicionamento de placa(s) ";  }



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

           
            strproj1+= " para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.";
            //objExcel.Pagina(7, 3, nsheet, objproj.intro1.ToString() + " " + lblRod2.Text + " km " + lblkmR2.Text + ", " + objproj.intro2.ToString());

            objExcel.Pagina(7, 3, nsheet, strproj1,12,"Black");






            temOSent = false;





            entrou = false;
            // Escaneia todas as placas em busca de "incluir"
            for (ind1 = 1; ind1 < 12; ind1++)
            {
                com2.CommandText = "select * from tplacas where Rodovia = "; 
                com2.CommandText += "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'";
                com2.CommandText += " and MunSen= " + "'" + lblSent2.Text + "'" + " and S" + ind1.ToString() + "=" + "'" + MunB + "'" ;
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
                        strprojTit = "No sentido " + lblMA2.Text + " / " + lblMB2.Text + " acrescentar nessa ordem:";
                        objExcel.Pagina(linTit, 3, nsheet, strprojTit);
                        linTit++;
                    }
                    objExcel.Pagina(linTit, 3, nsheet, strproj1, 12, "Black");
                    objExcel.MudaFonte(linTit, 3, nsheet, "Arial", false, 12);
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
            { linTit++; linTit++; // Vai para a linha do próximo titulo 
            }
                

            // *************************************************************************************************************************************************




            entrou = false;
            // Escaneia todas as placas em busca de "manter"
            for (ind1 = 1; ind1 < 12; ind1++)
            {
                com2.CommandText = "select * from tplacas where Rodovia = ";
                com2.CommandText += "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'";
                com2.CommandText += " and MunSen= " + "'" + lblSent2.Text + "'" + " and S" + ind1.ToString() + "=" + "'" + MunB + "'";
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
                        strprojTit = "Manter as placas no sentido " + lblMA2.Text + " / " + lblMB2.Text + " nessa ordem:";
                        objExcel.MudaFonte(linTit, 3, nsheet, "Arial", true, 12);
                        objExcel.Pagina(linTit, 3, nsheet, strprojTit);
                        linTit++;
                    }
                    objExcel.Pagina(linTit, 3, nsheet, strproj1, 12, "Black");
                    objExcel.MudaFonte(linTit, 3, nsheet, "Arial", 12);
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
                objExcel.MudaFonte(linTit, 3, nsheet, "Arial", false, 12);
                objExcel.Pagina(linTit, 3, nsheet, strproj1);
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
                com2.CommandText = "select * from tplacas where Rodovia = ";
                com2.CommandText += "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'";
                com2.CommandText += " and MunSen= " + "'" + lblSent2.Text + "'" + " and S" + ind1.ToString() + "=" + "'" + MunB + "'";
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
                        strprojTit = "No sentido " + lblMA2.Text + " / " + lblMB2.Text + " remover nessa ordem:";
                        objExcel.MudaFonte(linTit, 3, nsheet, "Arial", true, 12);
                        objExcel.Pagina(linTit, 3, nsheet, strprojTit);
                        linTit++;
                    }
                    objExcel.Pagina(linTit, 3, nsheet, strproj1, 12, "Black");
                    objExcel.MudaFonte(linTit, 3, nsheet, "Arial", 12);
                     temOSent = true;
                    linTit = linTit + 3;
                    entrou = true;
                }

                reader2.Close();
            }

           

          

            if (entrou)
            {
                linTit++; linTit++; // Vai para a linha do próximo titulo 
            }












            // Outro sentido  ******************************************************************************
            // Outro sentido  ******************************************************************************
            // Outro sentido  ******************************************************************************
            // Outro sentido  ******************************************************************************
            // Outro sentido  ******************************************************************************

            temOSent = false;








            entrou = false;
            // Escaneia todas as placas em busca de "incluir"
            for (ind1 = 1; ind1 < 12; ind1++)
            {
                com2.CommandText = "select * from tplacas where Rodovia = ";
                com2.CommandText += "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'";
                com2.CommandText += " and MunSen= " + "'" + lblSent2.Text + "'" + " and S" + ind1.ToString() + "=" + "'" + MunA + "'";
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
                        strprojTit = "No sentido " + lblMB2.Text + " / " + lblMA2.Text + " acrescentar nessa ordem:";
                        objExcel.MudaFonte(linTit, 3, nsheet, "Arial", true, 12);
                        objExcel.Pagina(linTit, 3, nsheet, strprojTit);
                        linTit++;
                    }
                    objExcel.Pagina(linTit, 3, nsheet, strproj1, 12, "Black");
                    objExcel.MudaFonte(linTit, 3, nsheet, "Arial", 12);
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
                com2.CommandText = "select * from tplacas where Rodovia = ";
                com2.CommandText += "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'";
                com2.CommandText += " and MunSen= " + "'" + lblSent2.Text + "'" + " and S" + ind1.ToString() + "=" + "'" + MunA + "'";
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
                        strprojTit = "Manter as placas no sentido " + lblMB2.Text + " / " + lblMA2.Text + " nessa ordem:";
                        objExcel.MudaFonte(linTit, 3, nsheet, "Arial", true, 12);
                        objExcel.Pagina(linTit, 3, nsheet, strprojTit);
                        linTit++;
                    }
                    objExcel.Pagina(linTit, 3, nsheet, strproj1, 12, "Black");
                    objExcel.MudaFonte(linTit, 3, nsheet, "Arial", 12);
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
                objExcel.MudaFonte(linTit, 3, nsheet, "Arial", false, 12);
                objExcel.Pagina(linTit, 3, nsheet, strproj1);
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







            // *******************************************************************************************************************************************
            entrou = false;
            // Escaneia todas as placas em busca de "remover"
            for (ind1 = 1; ind1 < 12; ind1++)
            {
                com2.CommandText = "select * from tplacas where Rodovia = ";
                com2.CommandText += "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'";
                com2.CommandText += " and MunSen= " + "'" + lblSent2.Text + "'" + " and S" + ind1.ToString() + "=" + "'" + MunA + "'";
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
                        strprojTit = "No sentido " + lblMB2.Text + " / " + lblMA2.Text + " remover nessa ordem:";
                        objExcel.MudaFonte(linTit, 3, nsheet, "Arial", true, 12);
                        objExcel.Pagina(linTit, 3, nsheet, strprojTit);
                        linTit++;
                    }
                    objExcel.Pagina(linTit, 3, nsheet, strproj1, 12, "Black");
                    objExcel.MudaFonte(linTit, 3, nsheet, "Arial", 12);
                     temOSent = true;
                    linTit = linTit + 3;
                    entrou = true;
                }

                reader2.Close();
            }

           



            if (entrou)
            {
                linTit++; linTit++; // Vai para a linha do próximo titulo 
            }










            objExcel.Pagina(60, 14, nsheet, npag.ToString());

            objExcel.MudaFonte(60, 14, nsheet,"Arial",10);

            



            SetaPaginas(ObjPags, nsheet);
        }


        // *************************************************************************************************************




        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************




























        // *************************************************************************************************************
        // ************************************************************************************************************* 
        // *************************************************************************************************************
        // *************************************************************************************************************

        public void Pagina17()
        {
            // Folha projeto 2, se houver
            nsheet = nsheet + 1;

            objExcel.RemovePagina(nsheet);
            nsheet = nsheet - 1;
            TotalFolhas = TotalFolhas - 1;


            if (objproj.DuasFolhas == 1)
            {
                npag = npag + 1;

                objExcel.Pagina(3, 12, nsheet, lblRod2.Text + "  km " + cUt.ConvPontoVirg(objproj.kmEdit));
                objExcel.MudaFonte(3, 12, nsheet, "Arial", 11);

                //VeGeometria(objGeom);
                // VeAcidentes(objAcid);
                // VeHistorico(objHist);

                Veproj(objproj);

                

                objExcel.Pagina(60, 14, nsheet, npag.ToString());
                objExcel.MudaFonte(60, 14, nsheet, "Arial", 10);
                SetaPaginas(ObjPags, nsheet);
            }


        }




        // *************************************************************************************************************

        public void Pagina18()
        {
            // Folha placas
            nsheet = nsheet + 1;
            
            npag = npag + 1;

            objExcel.Pagina(3, 12, nsheet, lblRod2.Text + "  km " + cUt.ConvPontoVirg(objproj.kmEdit)); // rod-xxx kmxxx
            objExcel.MudaFonte(3, 12, nsheet, "Arial", 11);

            //VeGeometria(objGeom);
            // VeAcidentes(objAcid);
            // VeHistorico(objHist);

            objExcel.Pagina(60, 14, nsheet, npag.ToString());
            objExcel.MudaFonte(60, 14, nsheet, "Arial", 10);
            SetaPaginas(ObjPags, nsheet);

        }

                                 
        // *******************************************************************************************************************







        public void Pagina19()
        {
            // Folha placas 2 se houver
            nsheet = nsheet + 1;

            objExcel.RemovePagina(nsheet);
            nsheet = nsheet - 1;
            TotalFolhas = TotalFolhas - 1;


            if (objproj.DuasFolhas == 1)
            {
                npag = npag + 1;

                objExcel.Pagina(3, 12, nsheet, lblRod2.Text + "  km " + cUt.ConvPontoVirg(objproj.kmEdit));
                objExcel.MudaFonte(3, 12, nsheet, "Arial", 11);

                //VeGeometria(objGeom);
                // VeAcidentes(objAcid);
                // VeHistorico(objHist);



                objExcel.Pagina(60, 14, nsheet, npag.ToString());
                objExcel.MudaFonte(60, 14, nsheet, "Arial", 10);
                SetaPaginas(ObjPags, nsheet);
            }
        }

        // *******************************************************************************************************************







        // *******************************************************************************************************************
        public void Pagina20()
        {                                       // p18 da conclusão
            nsheet = nsheet + 1;
            npag = npag + 1;

            int tamparag = 0; // quantidade de caracteres em cada paragrafo 

            int ind1R = 0;
            int indlAg = 0;   // linha a ser agrupada
          

            string[] fatrI = new string[8];
            String str1 = "";
            String str2 = "";

            String strT = "";
            String strSen = "";


            string VmdExistente = "";
            string VelExistente = "";
           

            VeGeometria(objGeom, objpFE);



            objExcel.Pagina(3, 12, nsheet, lblRod2.Text + "  km " + cUt.ConvPontoVirg(objproj.kmEdit));
            objExcel.MudaFonte(3, 12, nsheet, "Arial", 11);

            objExcel.Desagrupar(15, 3, 35, 13);

            for (indlAg = 15; indlAg <= 35; indlAg++) 
            {
               objExcel.Agrupar(indlAg, 3, indlAg, 13);
                objExcel.Pagina(indlAg, 3, nsheet, "               ");
            }

            objExcel.Pagina(17, 3, nsheet, "               ");
           
            objExcel.Pagina(17, 3, TotalFolhas, "                   ");
            objExcel.Desagrupar(15, 3, 35, 13);
            for (indlAg = 15; indlAg <= 35; indlAg++)
            { 
                objExcel.Agrupar(indlAg, 3, indlAg, 13);
                objExcel.Pagina(indlAg, 3, TotalFolhas, "                   ");

            }

            //VeGeometria(objGeom);
            // VeAcidentes(objAcid);
            // VeHistorico(objHist);

            VeTemFE(objTemFE);


            objExcel.Pagina(8, 3, nsheet, "O trecho da Rodovia " + lblRod2.Text + " km " + cUt.ConvPontoVirg(lblkmR2.Text) + ", em " + lblMun2.Text + ", mereceu estudos diversos.");
            objExcel.Pagina(8, 3, TotalFolhas, "O trecho da Rodovia " + lblRod2.Text + " km " + cUt.ConvPontoVirg(objproj.kmEdit) + ", em " + lblMun2.Text + ", mereceu estudos diversos.");

            if (objGeom.umsent == 1)
            {

                if (objpFE.VMD != 0)
                { 
                    VmdExistente = objpFE.VMD.ToString();
                    VelExistente = objpFE.Vel85p.ToString();
                }
                else
                {
                    VmdExistente = objpFE.VmdB.ToString();
                    VelExistente = objpFE.Vel85pSB.ToString();
                }



                //Aferiu-se nas contagens, um VMD (Valor Médio Diário) de 
                str1 = "Aferiu-se nas contagens, um VMD (Valor Médio Diário) de " + cUt.PoePonto(VmdExistente) + " para o sentido " + objpFE.MunSen.ToString() + ".";
                objExcel.Pagina(10, 3, nsheet, str1);
                objExcel.Pagina(10, 3, TotalFolhas, str1);

                str1 = "No gráfico de 85 percentil obteve-se uma velocidade de " + VelExistente + " km/h para o sentido " + objpFE.MunSen.ToString() + ".";
                objExcel.Pagina(13, 3, nsheet, str1);
                objExcel.Pagina(13, 3, TotalFolhas, str1);
            }
            else
            {
                if (VmdB > 0)
                {
                    str1 = "Aferiu-se nas contagens, um VMD (Valor Médio Diário) de " + cUt.PoePonto(lblVmd2.Text) + " para o sentido " + lblMB2.Text;
                    str1 = str1 + " e " + cUt.PoePonto(VmdB.ToString()) + " para o sentido " + lblMA2.Text + ".";

                    objExcel.Pagina(10, 3, nsheet, str1);
                    objExcel.Pagina(10, 3, TotalFolhas, str1);

                    str1 = "No gráfico de 85 percentil obteve-se uma velocidade de " + lblV852.Text + " km/h" + " para " + lblMB2.Text + " e ";
                    str1 = str1 + Vel85pSB.ToString() + " km/h para " + lblMA2.Text + ".";
                    objExcel.Pagina(13, 3, nsheet, str1);
                    objExcel.Pagina(13, 3, TotalFolhas, str1);
                }
            }

            
           
                fatrI[1] = StrMin(objRisco.fatr1.ToString());
                fatrI[2] = StrMin(objRisco.fatr2.ToString());
                fatrI[3] = StrMin(objRisco.fatr3.ToString());
                fatrI[4] = StrMin(objRisco.fatr4.ToString());
                fatrI[5] = StrMin(objRisco.fatr5.ToString());
                fatrI[6] = StrMin(objRisco.fatr6.ToString());
                fatrI[7] = StrMin(objRisco.fatr7.ToString());

            str1 = "Existem no trecho fatores de risco como: ";


            if (fatrI[7] != "")
            {
                fatrI[1] = fatrI[1] + ", ";
                fatrI[2] = fatrI[2] + ", ";
                fatrI[3] = fatrI[3] + ", ";
                fatrI[4] = fatrI[4] + ", ";
                fatrI[5] = fatrI[5] + ", ";
                fatrI[6] = fatrI[6] + " e ";
                fatrI[7] = fatrI[7];

            }
            else
            {

                for (ind1R = 1; ind1R <= 7; ind1R++)
                {

                    if ((ind1R < 7))
                    {
                        if ((fatrI[ind1R + 1] == ""))
                        {
                            fatrI[ind1R - 1] = fatrI[ind1R - 1].Substring(0, fatrI[ind1R - 1].Length - 2) + " e ";

                            ind1R = 8;
                        }
                        else
                        {
                            fatrI[ind1R] = fatrI[ind1R] + ", ";
                        }
                    }
                    else
                    {

                    }

                }

            }
            str1 ="Existem no trecho fatores de risco como: " + fatrI[1] + fatrI[2] + fatrI[3] + fatrI[4] + fatrI[5] + fatrI[6] + fatrI[7] + ".";

           str1= cUt.AcertaOuColoca_o_e(str1); // inclui o " e " no final se o mesmo já não existir

            //96 caracteres por linha
            tamparag = str1.Length;
            int linhaatual = 16;
            float qtlinhasAgrupadas = (tamparag / 96);
            //qtlinhasAgrupadas ++;


           // if (qtlinhasAgrupadas < 1) { qtlinhasAgrupadas = 1; }

           

            objExcel.Pagina(16, 3, nsheet, str1);
            objExcel.Agrupar(linhaatual, 3, linhaatual + (int)(qtlinhasAgrupadas), 13);
            objExcel.MudaFonte(linhaatual, 3, nsheet, "Arial", false, 12);

            objExcel.Pagina(16, 3, TotalFolhas, str1);
            objExcel.Agrupar(linhaatual, 3, linhaatual + (int)(qtlinhasAgrupadas), 13);
            objExcel.MudaFonte(linhaatual, 3, nsheet, "Arial", false, 12);


            linhaatual = linhaatual + (int)(qtlinhasAgrupadas) + 2;

             VeSeTemOuVaiTerFE(objVTOJT);

            switch(lblTipo2.Text)
            {
                case "I.A":
                    strT = " de excesso de velocidade com redução com display ";
                    break;
                case "I.B":
                    strT = " de excesso de velocidade sem display ";
                    break;
                case "II.A":
                    strT = " do tipo radar fixo de excesso de velocidade ";
                    break;
                case "II.B":
                    strT = " do tipo radar fixo de excesso de velocidade ";
                    break;
                case "II.C":
                    strT = " do tipo radar fixo de excesso de velocidade ";
                    break;
                case "III.A":
                    strT = " para avanço de sinal ";
                    break;
                case "III.B":
                    strT = " com semáforo para avanço de sinal e faixa de pedestres ";
                    break;
                case "III.C":
                    strT = " com semáforo para avanço de sinal e excesso de velocidade ";
                    break;
                case "III.D":
                    strT = " com semáforo para avanço de sinal, faixa de pedestres e excesso de velocidade ";
                    break;
                case "III.E":
                    strT = " com semáforo para avanço de sinal, faixa de pedestres e excesso de velocidade ";
                    break;
                default:
                    
                    break;

            }
            
            switch (lblSent2.Text)
            {
                case "Ambos Sentidos":
                    strSen = " para ambos os sentidos";
                        break;
                default:
                    strSen=" no sentido que segue para " + lblSent2.Text;
                    break;
            }
            str1 = "    ";
            if ( (objVTOJT.jatinhafe) && (objVTOJT.foiconf))
            { str1 = "Deve ser considerado que o trecho em questão já vem sendo monitorado com aparelho de fiscalização eletrônica, apresentando sinalização pertinente para o local. ";
                str1 = str1 + "Foi confirmada a necessidade de se manter o aparelho de fiscalização eletrônica.";

                tamparag = str1.Length;
                qtlinhasAgrupadas = (tamparag / 96);
                objExcel.Agrupar(linhaatual, 3, linhaatual + (int)(qtlinhasAgrupadas), 13);

                //objExcel.Pagina(linhaatual, 3, nsheet, "               ");
                //objExcel.Pagina(linhaatual, 3, TotalFolhas, "                   ");
                objExcel.Pagina(linhaatual, 3, nsheet, str1);
                objExcel.Agrupar(linhaatual, 3, linhaatual + (int)(qtlinhasAgrupadas), 13);
                objExcel.MudaFonte(linhaatual, 3, nsheet, "Arial", false, 12);

                objExcel.Pagina(linhaatual, 3, TotalFolhas, str1);
                objExcel.Agrupar(linhaatual, 3, linhaatual + (int)(qtlinhasAgrupadas), 13);
                objExcel.MudaFonte(linhaatual, 3, TotalFolhas, "Arial", false, 12);

                linhaatual = linhaatual + (int)(qtlinhasAgrupadas) + 2;
            }
           
            if ( (objTemFE.TemQB==1) && (objTemFE.Remover == 1) && (objTemFE.Proj==1) )
            {
                str1 = "No trecho em questão foram anteriormente colocados quebra molas como medida de redução de velocidade.";
                str1 = str1 + "Esses quebra molas deverão ser retirados na implantação de novo equipamento de fiscalização eletrônica a ser instalado.";
            }

            if ((objTemFE.TemQB == 1) && (objTemFE.Remover == 1) && (objTemFE.Subst == 1))
            {
                str1 = "No trecho em questão foi colocado quebra molas em substituição ao equipamento de fiscalização eletrônica existente anteriormente.";
                str1 = str1 + "Esses quebra molas deverão ser retirados na implantação de novo equipamento de fiscalização eletrônica a ser instalado.";
            }


            if (objVTOJT.vaiterfe)
            {
                if ( (objTemFE.Renova==1) && (objTemFE.Altera==0) )
                {  // Se for renovação do Estudo
                    str2 = "Foi identificado que existe a necessidade de se manter instalado o equipamento de fiscalização eletrônica" + strT;
                    str2 = str2 + "(Tipo " + lblTipo2.Text + ") " + "no limite de " + lblVF2.Text + " km/h" + " na rodovia " + lblRod2.Text + " no km " + cUt.ConvPontoVirg(lblkmEdt2.Text);
                    str2 = str2 + strSen + ".";

                }
                else
                {
                    // Se for Estudo novo
                    str2 = "Foi identificado que existe a necessidade de se instalar equipamento de fiscalização eletrônica" + strT;
                    str2 = str2 + "(Tipo " + lblTipo2.Text + ") " + "no limite de " + lblVF2.Text + " km/h" + " na rodovia " + lblRod2.Text + " no km " + cUt.ConvPontoVirg(lblkmEdt2.Text);
                    str2 = str2 + strSen + ".";

                }
                
            }
            else
            {
                str2 = "Sendo assim, foi identificado que não existe a necessidade de aparelho de fiscalização eletrônica para o local.";
            }



            tamparag = str2.Length;
            qtlinhasAgrupadas = (tamparag / 80);
            
            
            objExcel.Pagina(linhaatual, 3, nsheet, str2);
            objExcel.Agrupar(linhaatual, 3, linhaatual + (int)(qtlinhasAgrupadas), 13);
            objExcel.MudaFonte(linhaatual, 3, nsheet, "Arial", true, 12);

            objExcel.Pagina(linhaatual, 3, TotalFolhas, str2);
            objExcel.Agrupar(linhaatual, 3, linhaatual + (int)(qtlinhasAgrupadas), 13);
            objExcel.MudaFonte(linhaatual, 3, TotalFolhas, "Arial", true, 12);

            // public void MudaFonte(int i, int j, int sheet, string estilo, int tamanho)



            objExcel.Pagina(60, 14, nsheet, npag.ToString());
            objExcel.MudaFonte(60, 14, nsheet, "Arial", 10);
            SetaPaginas(ObjPags, nsheet);
        }


        // ***********************************************************************************************************





        private string StrMin(string strRic)
        {
            string str2 = "";
            try
            {
                //str2 = strRic.Substring(0).ToLower() + strRic.Substring(1, strRic.Length - 1);
                str2 = strRic.Substring(0).ToLower();
            }
            catch
            {
                str2 = "";
                return str2; 
            }
            return str2;
        }
        // *************************************************************************************************************



        public void Pagina21()
        {
            String str1 = "";
            nsheet = nsheet + 1;
            npag = npag + 1;

            objExcel.Pagina(3, 12, nsheet, lblRod2.Text + "  km " + cUt.ConvPontoVirg(lblkmR2.Text));
            objExcel.MudaFonte(3, 12, nsheet, "Arial", 11);

            str1 = "·       9- RESPONSÁVEL PELA ELABORAÇÃO DE ESTUDO TÉCNICO ............................" + npag.ToString();
            objExcel.Pagina(36, 3, 2, str1);
            str1 = "·      10- RESPONSÁVEL TÉCNICO DO ÓRGÃO DE TRÂNSITO PERANTE O CREA ......." + npag.ToString();
            objExcel.Pagina(38, 3, 2, str1);

            objExcel.Pagina(60, 14, nsheet, npag.ToString());
            objExcel.MudaFonte(60, 14, nsheet, "Arial", 10);
            SetaPaginas(ObjPags, nsheet);
        }


        //*********************************************************************************************************

        public void Pagina22()
        {
            String str1 = "";
            // Material Jornalistico
            VeMaterialJornalistico(ObjJorn);


            nsheet = nsheet + 1;
            npag = npag + 1;

            objExcel.Pagina(3, 12, nsheet, lblRod2.Text + "  km " + cUt.ConvPontoVirg(lblkmR2.Text));
            objExcel.MudaFonte(3, 12, nsheet, "Arial", 11);

            if ( (ObjJorn.jorn1.Trim() == "Sem material jornalístico") || (ObjJorn.jorn1.Trim() == "Sem material jornalistico")  )
            {
                objExcel.Pagina(7, 4, nsheet, ObjJorn.jorn1);
            }
            else
            {
                objExcel.Pagina(7, 4, nsheet, "                                               ");
            }
            objExcel.MudaFonte(7, 4, nsheet, "Arial",false, 12);
            str1 = "·       MATERIAL JORNALÍSTICO / FATORES DE RISCO DE ACIDENTES ......................." + npag.ToString();
            objExcel.Pagina(40, 3, 2, str1);
            


            objExcel.Pagina(60, 14, nsheet, npag.ToString());
            objExcel.MudaFonte(60, 14, nsheet, "Arial", 10);
            SetaPaginas(ObjPags, nsheet);
        }
        //*********************************************************************************************************



        public void Pagina23()
        {
            nsheet = nsheet + 1;

            if ((ObjJorn.jorn1.Trim() != "Sem material jornalístico") && (ObjJorn.jorn1.Trim() != "Sem material jornalistico"))
            {
                npag = npag + 1;

                objExcel.Pagina(3, 12, nsheet, lblRod2.Text + "  km " + cUt.ConvPontoVirg(lblkmR2.Text));
                objExcel.MudaFonte(3, 12, nsheet, "Arial", 11);
                objExcel.Pagina(60, 14, nsheet, npag.ToString());
                objExcel.MudaFonte(60, 14, nsheet, "Arial", 10);
                SetaPaginas(ObjPags, nsheet);
            }

        }
        //*********************************************************************************************************

        public void Pagina24()
        {
            // Fotos


            nsheet = nsheet + 1;
            npag = npag + 1;

            objExcel.Pagina(3, 12, nsheet, lblRod2.Text + "  km " + cUt.ConvPontoVirg(lblkmR2.Text));
            objExcel.MudaFonte(3, 12, nsheet, "Arial", 11);

            objExcel.Agrupar(5, 3, 5, 10);
            objExcel.Pagina(5, 3, nsheet, "Fotos dos Fatores de Risco do km " + cUt.ConvPontoVirg(lblkmEdt2.Text) + " na Rodovia " + lblRod2.Text + ":");

            objExcel.Pagina(31, 3, nsheet, objRisco.fatr1.ToString());
            objExcel.Pagina(57, 3, nsheet, objRisco.fatr2.ToString());

            objExcel.Pagina(60, 14, nsheet, npag.ToString());
            objExcel.MudaFonte(60, 14, nsheet, "Arial", 10);
            SetaPaginas(ObjPags, nsheet);
        }
        //*********************************************************************************************************

        public void Pagina25()
        {
            // Fotos

            nsheet = nsheet + 1;
            npag = npag + 1;

            objExcel.Pagina(3, 12, nsheet, lblRod2.Text + "  km " + cUt.ConvPontoVirg(lblkmR2.Text));
            objExcel.MudaFonte(3, 12, nsheet, "Arial", 11);

            objExcel.Pagina(31, 3, nsheet, objRisco.fatr3.ToString());
            objExcel.Pagina(57, 3, nsheet, objRisco.fatr4.ToString());

            objExcel.Pagina(60, 14, nsheet, npag.ToString());
            objExcel.MudaFonte(60, 14, nsheet, "Arial", 10);
            SetaPaginas(ObjPags, nsheet);
        }
        //*********************************************************************************************************

        public void Pagina26()
        {
            String str1 = "";

            nsheet = nsheet + 1;
            npag = npag + 1;

            objExcel.Pagina(3, 12, nsheet, lblRod2.Text + "  km " + cUt.ConvPontoVirg(lblkmR2.Text));
            objExcel.MudaFonte(3, 12, nsheet, "Arial", 11);

            str1 = "·       CONCLUSÃO ................................................................................................................." + npag.ToString();
            objExcel.Pagina(42, 3, 2, str1);

            objExcel.Pagina(60, 14, nsheet, npag.ToString());
            objExcel.MudaFonte(60, 14, nsheet, "Arial", 10);
            SetaPaginas(ObjPags, nsheet);
        }
        //*********************************************************************************************************

        public void RenomeiaSheets()
        {
            int ind1 = 0;
            for (ind1 = 1; ind1 <= 23; ind1++)
            {
               objExcel.RenameSheet(ind1);
            }
         }





        private void btnGeraEstudo_Click(object sender, EventArgs e)
        {
            
        }

        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************
        // *************************************************************************************************************


        private void btnFecha_Click(object sender, EventArgs e)
        {
            objExcel.Fecha();
        }

        private void cmbSentido_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void VePistaEFaixas(C_PistasFaixas objCPx)
        {
            com2.CommandText = "select * from teduplo where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) +"'";
            com2.CommandText = com2.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";   //  tabela que indica se o ponto de FE é duplo ou não

            reader2 = com2.ExecuteReader();

            if (reader2.Read())
            {
                objCPx.NPistas = Int32.Parse((reader2["NPistas"]).ToString());
                objCPx.NFaixas = Int32.Parse((reader2["NFxs"]).ToString());

                try { objCPx.Fx1_1 = Int32.Parse((reader2["Fx1_1"]).ToString()); } catch { objCPx.Fx1_1 = 0; }
                try { objCPx.Fx1_2 = Int32.Parse((reader2["Fx1_2"]).ToString()); } catch { objCPx.Fx1_2 = 0; }
                try { objCPx.Fx1_3 = Int32.Parse((reader2["Fx1_3"]).ToString()); } catch { objCPx.Fx1_3 = 0; }

                try { objCPx.Fx2_1 = Int32.Parse((reader2["Fx2_1"]).ToString()); } catch { objCPx.Fx2_1 = 0; }
                try { objCPx.Fx2_2 = Int32.Parse((reader2["Fx2_2"]).ToString()); } catch { objCPx.Fx2_2 = 0; }
                try { objCPx.Fx2_3 = Int32.Parse((reader2["Fx2_3"]).ToString()); } catch { objCPx.Fx2_3 = 0; }
            }
            reader2.Close();
          }


        // ****************************************************************************************************

        private int DescobreQualIntervalodeVelocidadepercentil(int ValorVel)
        {
            int val1 = 0;
            int val2 = 0;

            int seq = 0;

            com2.CommandText = "select * from tintervalosvel order by seq";

            reader2 = com2.ExecuteReader();

            while (reader2.Read())
            {
                
                val1 = int.Parse((reader2["interv1"]).ToString());
                val2 = int.Parse((reader2["interv2"]).ToString());

                if ( (ValorVel>=val1) && (ValorVel <= val2) )
                {
                    seq= int.Parse((reader2["seq"]).ToString());
                   // return seq;
                }
                                   
            }
            reader2.Close();

            return seq;  // seq contem o indice de correspondente ao intervalo de velocidade na tabela tintervalosvel;
        }
                                    
            // *************************************************************************************************************




            private void VePercentuais(C_percentuais85 opct,int oaleat)
        {
           
            com2.CommandText = "select * from tpercentuais where seq = " + oaleat;

            reader2 = com2.ExecuteReader();

            if (reader2.Read())
            {
                
                opct.pct[1] = Double.Parse(reader2["pct1"].ToString());
                opct.pct[2] = Double.Parse(reader2["pct2"].ToString());
                opct.pct[3] = Double.Parse(reader2["pct3"].ToString());
                opct.pct[4] = Double.Parse(reader2["pct4"].ToString());
                opct.pct[5] = Double.Parse(reader2["pct5"].ToString());
                opct.pct[6] = Double.Parse(reader2["pct6"].ToString());
                opct.pct[7] = Double.Parse(reader2["pct7"].ToString());
                opct.pct[8] = Double.Parse(reader2["pct8"].ToString());
                opct.pct[9] = Double.Parse(reader2["pct9"].ToString());
                opct.pct[10] = Double.Parse(reader2["pct10"].ToString());

                opct.pct[11] = Double.Parse(reader2["pct11"].ToString());
                opct.pct[12] = Double.Parse(reader2["pct12"].ToString());
                opct.pct[13] = Double.Parse(reader2["pct13"].ToString());
                opct.pct[14] = Double.Parse(reader2["pct14"].ToString());

                //pGeom.umsent = Int32.Parse((reader2["UmSentido"]).ToString());

            }
            reader2.Close();
        }

        // ***************************************************************************************************************

        private void DescobreQualFaixaVelocidade(C_Vel2 objVel2, int Vel85p)
        {
            double seqaux = 0;
            int iseqaux = 0;

            if (Vel85p>100)
            {  seqaux = Vel85p / 100; }
            else
            {  seqaux = Vel85p / 10; }


            
           
          //  seqaux = Int32.Parse((Vel85p.ToString()).Substring(1, 1)); // extrai o primeiro dígito
            seqaux = ( Math.Truncate(seqaux)); // extrai o primeiro dígito
            iseqaux = Int32.Parse(seqaux.ToString());

         //   ' " = 1'

            com2.CommandText = "select * from tperctvel1 where seq=" + seqaux + "";
            reader2 = com2.ExecuteReader();

            if (reader2.Read())
            {
                objVel2.Veln[1] = Double.Parse((reader2["v1-19"]).ToString());                    
                objVel2.Veln[2] = Double.Parse((reader2["v20-29"]).ToString());
                objVel2.Veln[3] = Double.Parse((reader2["v30-39"]).ToString());
                objVel2.Veln[4] = Double.Parse((reader2["v40-49"]).ToString());
                objVel2.Veln[5] = Double.Parse((reader2["v50-59"]).ToString());
                objVel2.Veln[6] = Double.Parse((reader2["v60-69"]).ToString());
                objVel2.Veln[7] = Double.Parse((reader2["v70-79"]).ToString());
                objVel2.Veln[8] = Double.Parse((reader2["v80-89"]).ToString());
                objVel2.Veln[9] = Double.Parse((reader2["v90-99"]).ToString());
                objVel2.Veln[10] = Double.Parse((reader2["v100-109"]).ToString());
                objVel2.Veln[11] = Double.Parse((reader2["v110-119"]).ToString());
                objVel2.Veln[12] = Double.Parse((reader2["v120-129"]).ToString());
                objVel2.Veln[13] = Double.Parse((reader2["v130-139"]).ToString());
                objVel2.Veln[14] = Double.Parse((reader2["vm140"]).ToString());

                               
            }
            reader2.Close();
        }

        // **************************************************************************************************************

        private void MontaDivisoresFaixaVelocidade(C_Parc85p opjDivp, int avel)
        {

            string sind1 = "";
            string sind2 = "";
            int ind1 = 0;
            int ind2 = 0;
            int tam = 0;

            


            sind1 = avel.ToString();
            tam = sind1.Length;

            sind2 = sind1.Substring((tam - 1), 1);
            sind2 = sind2.Trim();
            ind1 = Int32.Parse(sind2);

            int valorDiv = ind1+1;
            for (ind2=0;ind2<=ind1;ind2++)
            {
                opjDivp.partDiv[ind2+1] = valorDiv;
                // opjDivp.partDiv[ind2 + 1] = 10;
            }

            for (ind2 = (ind1 + 2); ind2 <= 10; ind2++)
            {
                 opjDivp.partDiv[ind2] = (10-(valorDiv));
                // opjDivp.partDiv[ind2] = 10;
            }
            
        }
               
        // *******************************************************************************************************************
        private void VePontoFE(C_pontofe objPfe)
        {
            com2.CommandText = "select * from ponto_featual";
            reader2 = com2.ExecuteReader();

            if (reader2.Read())
            {

                //pGeom.declive = Int32.Parse((reader2["Rodovia"]).ToString());
                objPfe.Rodovia = (reader2["Rodovia"]).ToString();
                objPfe.kmEdital = (reader2["kmEdital"]).ToString();
                objPfe.MunSen=(reader2["MunSen"]).ToString();
                objPfe.kmReal=(reader2["kmReal"]).ToString();
                objPfe.Localidade=(reader2["Localidade"]).ToString();
                objPfe.Municipio=(reader2["Municipio"]).ToString();
                objPfe.QtdFx = Int32.Parse((reader2["QtdFx"]).ToString());
                objPfe.MunA=(reader2["MunA"]).ToString();
                objPfe.MunB=(reader2["MunB"]).ToString();
                objPfe.VelFisc= Int32.Parse((reader2["VelFisc"]).ToString());
                objPfe.Lat=(reader2["Lat"]).ToString().ToString();
                objPfe.Longit=(reader2["Longit"]).ToString();
                objPfe.VMD = Int32.Parse((reader2["VMD"]).ToString());

                objPfe.Vel85p= Int32.Parse((reader2["Vel85p"]).ToString());
                
                objPfe.Tipo=(reader2["Tipo"]).ToString();
                objPfe.Lat2=(reader2["Lat2"]).ToString();
                objPfe.Longit2=(reader2["Longit2"]).ToString();
                objPfe.Vel85pSB = Int32.Parse((reader2["Vel85pSB"]).ToString());
                objPfe.VmdB = Int32.Parse((reader2["VmdB"]).ToString());

            
    }
            reader2.Close();

        }

        // **************************************************************************************************************







        // ***************************************************************************************************************

        public void Ve85PectSEntido(C_tab85p obj85p, string oSent)
        {

            com2.CommandText = "select * from t85pct where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'";
            com2.CommandText = com2.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'" + " and Sent= " + "'" + oSent + "'";

            reader2 = com2.ExecuteReader();
            if (reader2.Read())
            {
                obj85p.SEnt = reader2["Sent"].ToString();

                obj85p.kmVinc = reader2["kmVinc"].ToString();

                obj85p.ptcv[1] = (Double.Parse((reader2["Vel19"]).ToString())) / 100;
                obj85p.ptcv[2] = (Double.Parse((reader2["Vel29"]).ToString())) / 100;
                obj85p.ptcv[3] = (Double.Parse((reader2["Vel39"]).ToString())) / 100;
                obj85p.ptcv[4] = (Double.Parse((reader2["Vel49"]).ToString())) / 100;
                obj85p.ptcv[5] = (Double.Parse((reader2["Vel59"]).ToString())) / 100;
                obj85p.ptcv[6] = (Double.Parse((reader2["Vel69"]).ToString())) / 100;
                obj85p.ptcv[7] = (Double.Parse((reader2["Vel79"]).ToString())) / 100;
                obj85p.ptcv[8] = (Double.Parse((reader2["Vel89"]).ToString())) / 100;
                obj85p.ptcv[9] = (Double.Parse((reader2["Vel99"]).ToString())) / 100;
                obj85p.ptcv[10] = (Double.Parse((reader2["Vel109"]).ToString())) / 100;
                obj85p.ptcv[11] = (Double.Parse((reader2["Vel119"]).ToString())) / 100;
                obj85p.ptcv[12] = (Double.Parse((reader2["Vel129"]).ToString())) / 100;
                obj85p.ptcv[13] = (Double.Parse((reader2["Vel139"]).ToString())) / 100;
                obj85p.ptcv[14] = (Double.Parse((reader2["Vel199"]).ToString())) / 100;

            }
            else
            {
                MessageBox.Show("Não encontrada a tabela de 85 percentil para o sentido " + oSent + "!" ,
                "Important Note",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
            }

            reader2.Close();
        }

        // ************************************************************************************************************************






        // *************************************************************************************************************
        public void VeGeometria(C_Geometria pGeom, C_pontofe objPfe)
        {
            com2.CommandText = "select * from tgeometria where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) +"'";
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
                    com2.CommandText = "select * from ponto_featual where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) +"'";
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



        //*********************************************************************************************



        private void VeAcidentes (c_acidentes objAcid)
        {
            com2.CommandText = "select * from acidentes where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) +"'";
            com2.CommandText = com2.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";

            reader2 = com2.ExecuteReader();

            if (reader2.Read())
            {
                objAcid.kmreal =((reader2["kmReal"]).ToString());
                objAcid.abalroamento = Int32.Parse((reader2["Abalroamento"]).ToString());
                objAcid.choque = Int32.Parse((reader2["Choque"]).ToString());
                objAcid.colisao = Int32.Parse((reader2["Colisão"]).ToString());
                objAcid.tombamento = Int32.Parse((reader2["Tombamento"]).ToString());
                objAcid.capotamento = Int32.Parse((reader2["Capotamento"]).ToString());
                objAcid.incendio = Int32.Parse((reader2["Incendio"]).ToString());
                objAcid.atropelamento = Int32.Parse((reader2["Atropelamento"]).ToString());
       

            }
            reader2.Close();
        }


        private void VeRiscos(C_risco objRiscos)
        {
            com2.CommandText = "select * from fat_risco where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) +"'";
            com2.CommandText = com2.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";

            reader2 = com2.ExecuteReader();

            if (reader2.Read())
            {
                objRiscos.Rodovia = (reader2["Rodovia"]).ToString();
                objRiscos.kmEdit = ((reader2["kmEdital"]).ToString());
                objRiscos.MunSen = (reader2["MunSen"]).ToString();

                objRiscos.fatr1 = (reader2["fatr1"]).ToString();
                objRiscos.fatr2 = (reader2["fatr2"]).ToString();
                objRiscos.fatr3 = (reader2["fatr3"]).ToString();
                objRiscos.fatr4 = (reader2["fatr4"]).ToString();
                objRiscos.fatr5 = (reader2["fatr5"]).ToString();
                objRiscos.fatr6 = (reader2["fatr6"]).ToString();
                objRiscos.fatr7 = (reader2["fatr7"]).ToString();

                objRiscos.fatr8 = (reader2["fatr8"]).ToString();
                objRiscos.fatr9 = (reader2["fatr9"]).ToString();
                objRiscos.fatr10 = (reader2["fatr10"]).ToString();

            }
            reader2.Close();

        }


        private void VeHistorico(C_historico objHist)
        {
            com2.CommandText = "select * from histmedidas where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) +"'";
            com2.CommandText = com2.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";

            reader2 = com2.ExecuteReader();

            if (reader2.Read())
            {
                objHist.Rodovia = (reader2["Rodovia"]).ToString();
                objHist.kmEdit = ((reader2["kmEdital"]).ToString());
                objHist.MunSen = (reader2["MunSen"]).ToString();

                objHist.h1 = (reader2["h1"]).ToString();
                objHist.h2 = (reader2["h2"]).ToString();
                objHist.h3 = (reader2["h3"]).ToString();
                objHist.h4 = (reader2["h4"]).ToString();
                objHist.h5 = (reader2["h5"]).ToString();

                }
            reader2.Close();

        }

        private void Veproj(C_proj objProj)
        {
            com2.CommandText = "select * from tproj where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) +"'";
            com2.CommandText = com2.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";

            reader2 = com2.ExecuteReader();

            if (reader2.Read())
            {
                objProj.Rodovia = (reader2["Rodovia"]).ToString();
                objProj.kmEdit =  ((reader2["kmEdital"]).ToString());
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


        private void VeMaterialJornalistico(C_jorn objjorn)
        {

            com2.CommandText = "select * from tjorn where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) +"'";
            com2.CommandText = com2.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";

            reader2 = com2.ExecuteReader();

            if (reader2.Read())
            {
                objjorn.Rodovia = (reader2["Rodovia"]).ToString();
                objjorn.kmEdit = ((reader2["kmEdital"]).ToString());
                objjorn.MunSen = (reader2["MunSen"]).ToString();

                objjorn.jorn1 = (reader2["semjorn"]).ToString();
                
            }
            reader2.Close();
        }

        private void ZeraPaginas(C_paginas objpgs)
        {
            int ind1 = 0;

            for (ind1=0; ind1 < 27; ind1++ )
            {
                objpgs.pagina[ind1] = false;
            }


            com2.CommandText = "update tpaginas set p1=0, p2=0, p3=0, p4=0, p5=0, p6=0, p7=0, p8=0, p9=0, p10=0, p11=0, p12=0, p13=0, p14=0, p15=0, p16=0, p17=0, p18=0, p19=0,";
            com2.CommandText = com2.CommandText + " p20=0, p21=0, p22=0, p23=0, p24=0, p25=0, p26=0";
            com2.CommandText = com2.CommandText + " where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) +"'";
            com2.CommandText = com2.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";
            com2.ExecuteNonQuery();


        }

        private void SetaPaginas(C_paginas objpgs, int ind1)
        {
            objpgs.pagina[ind1] = true;

            com2.CommandText = "update tpaginas set p" + ind1.ToString() + " = 1 where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) +"'";
            com2.CommandText= com2.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";
            com2.ExecuteNonQuery();
        }

        FrmParametros fParam = new FrmParametros();

        private void btParam_Click(object sender, EventArgs e)
        {
           // FrmParametros fParam   = new FrmParametros();
           
        }


        private void VeTemFE(C_TemFE objTemFE)
        {
            com2.CommandText = "select * from ttemfe where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) + "'";
            com2.CommandText = com2.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";

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



        private void VeSeTemOuVaiTerFE(C_VaiTerOuJaTem objVTOJT)
        {
            int Ivaiterfe = 0;

            com2.CommandText = "select * from ttemfe where Rodovia = " + "'" + lblRod2.Text + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(lblkmEdt2.Text) +"'";
            com2.CommandText = com2.CommandText + " and MunSen= " + "'" + lblSent2.Text + "'";

            reader2 = com2.ExecuteReader();

            if (reader2.Read())
            {
                objVTOJT.Rodovia = (reader2["Rodovia"]).ToString();
                objVTOJT.kmEdit = ((reader2["kmEdital"]).ToString());
                objVTOJT.MunSen = (reader2["MunSen"]).ToString();

                Ivaiterfe =  Int32.Parse( (reader2["vaiterfe"].ToString()));

                if (Int32.Parse((reader2["vaiterfe"].ToString())) == 1)
                  {  objVTOJT.vaiterfe = true; }
                  else
                  { objVTOJT.vaiterfe = false; }

                if (Int32.Parse((reader2["jatinhafe"].ToString())) == 1)
                { objVTOJT.jatinhafe = true; }
                else
                { objVTOJT.jatinhafe = false; }

                if (Int32.Parse((reader2["foiconf"].ToString())) == 1)
                { objVTOJT.foiconf = true; }
                else
                { objVTOJT.foiconf = false; }



            }
            reader2.Close();

        }

        // ********************************************************************  //
        
        private void btNovo_Click(object sender, EventArgs e)
        {
            
            
        }

        private void lblkmEdt_Click(object sender, EventArgs e)
        {

        }

        private void btPDF_Click(object sender, EventArgs e)
        {
            
            Grafico();
            objPdf.GeneratePDF(chart1);
        }


        private void Grafico()
        {
          
            // Data arrays.
            string[] seriesArray = { "Cats", "Dogs", "Birds", "Lions", "Tigers" };
            int[] pointsArray = { 1, 2 , 3 , 4 , 5 ,6,7,8,9,10,11,12,13,14,15};

            // Set palette.
            this.chart1.Palette = ChartColorPalette.SeaGreen;
            //this.chart1.ChartAreas[0].AxisX.ScaleView.Zoom(0, 100);

            // Set title.
            this.chart1.Titles.Add("Pets");
            // this.chart1.Series
            // Add series.
            for (int i = 0; i < seriesArray.Length; i++)
            {
                // Add series.
                Series series = this.chart1.Series.Add(seriesArray[i]);

                // Add point.
                series.Points.Add(pointsArray[i]);
            }


        }

        private void btVelocidade_Click(object sender, EventArgs e)
        {
            mysqlCon1.Close();
            mysqlCon2.Close();

            ConectaBancoClassfic();

            npag = 0;
            nsheet = 1;
            string diasem = "";

            int linha = 10;
            int coluna = 2;

            string nomearq = "ContagensViaLight.xlsx";

            // nomearq=cUt.ConvPontoVirg(nomearq);

            nomearq = cUt.ArquivoVirg(nomearq);

            // objExcel.AbreExcel(@"C:\DER-RJ\FiscalizaçãoEletronica\2018\Estudos2018\Relatorio.xlsx", 1);
            objExcel.AbreExcel(@"D:\DER-RJ\ViaLight-Contrato de Manutenção 2014\2018\Dezembro2018\" + nomearq, 1);
           

             objExcel.LeEx(linha, coluna, nsheet);
            DateTime dt = new DateTime(2008, 6, 11);
            //  Console.WriteLine((int)dt.DayOfWeek);
            diasem =(dt.DayOfWeek).ToString();
            diasem= cUt.DiaSem(diasem.Trim());
              //AcumulaDiaSemana
              nomearq = nomearq;



        }

        private void btAtual_Click(object sender, EventArgs e)
        {

        }

        private void LblkmEdt2_Click(object sender, EventArgs e)
        {

        }

        private void LblQtdFx2_Click(object sender, EventArgs e)
        {

        }

        private void BtConvCood_Click(object sender, EventArgs e)
        {
            // botão criado apenas para testar se método de converção de coordenedas funciona.

            int ind1 = 0;
           string[] strcoord = new string[11];
          //  strcoord[1] = "42° 0 7.53 O";
             strcoord[1] = "22°37 56.92 S";     //22°37'56.92 S


            txtCorrdT.Text= cUt.ReconverteCoord(strcoord[1]);

            //for (ind1=1;ind1<=0;ind1++)
            //{
            //    cUt.ReconverteCoord(strcoord[ind1]);
            //}
                             

        }

        private void BtCarac_Click(object sender, EventArgs e)
        {
            //  txtCarac.Text = ( (char)190).ToString();
            this.Update();
        }

        private void selecionarEstudoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSelecionaPontoFE fPontoFE = new FrmSelecionaPontoFE();

            //Frmcadastro.Text = "Cadastro de novo ponto de fiscalização eletrônica";
            ////Frmcadastro fCadastro = new Frmcadastro();
            fPontoFE.ShowDialog();

          //  timer1.Enabled = true;
           
        }



       public void timer1_Tick(object sender, EventArgs e)
        {
            //ConectaBanco();
            //ConectaBanco2();
            LeRegistroAtual();
            bEDuplo = VerificaSeÉDuplo();
            VeGeometria(objGeom, objpFE); // Serve para saber se é um ou mais sentidos

            foreach (Label contr in this.Controls.OfType<Label>())
            { contr.Update();   }

            timer1.Enabled = false;
        }




        private void liberaMemoriaExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void reletórioToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void limpaMemóriaExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            objExcel.EncerraProcessos();
            mysqlCon1.Close();
            mysqlCon2.Close();
            
            this.Close();
        }

        private void parâmetrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fParam.StartPosition = FormStartPosition.CenterScreen;
        
            fParam.ShowDialog();
        }



        private void geraEstudoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //objExcel.EncerraProcessos();

           // C_excel objExcel = new C_excel();

            int i85p = 0;

            LeRegistroAtual();
            VeSeCalula85p(ref i85p);

            bEDuplo = VerificaSeÉDuplo();

            VeGeometria(objGeom, objpFE);  // Serve para saber se é um ou mais sentidos

            ZeraPaginas(ObjPags);

            npag = 0;
            nsheet = 0;

            string nomearq = "Estudo" + lblRod2.Text + "_km" + lblkmR2.Text + ".xlsx";

            // nomearq=cUt.ConvPontoVirg(nomearq);

            nomearq = cUt.ArquivoVirg(nomearq);

            // objExcel.AbreExcel(@"C:\DER-RJ\FiscalizaçãoEletronica\2018\Estudos2018\Relatorio.xlsx", 1);
            objExcel.AbreExcel(@"C:\DER-RJ\FiscalizaçãoEletronica\2020\Estudos2020\" + nomearq, 1);

            Pagina1();
            Pagina2();
            Pagina3();
            Pagina4();
            Pagina5();
            Pagina6();
            Pagina7();
            Pagina8();
            Pagina9();

            if (i85p == 1)
            {
                //Pagina10();
                //Pagina11();


                Pagina10_2();
                Pagina11_2();
            }
            else
            {
                nsheet = nsheet + 1;
                npag = npag + 1;





                /// pagina e km
                objExcel.Pagina(3, 13, nsheet, lblRod2.Text + "  km " + cUt.ConvPontoVirg(lblkmR2.Text));
                objExcel.MudaFonte(3, 13, nsheet, "Arial", 11);

                if (objGeom.umsent != 1)
                {
                    objExcel.Pagina(5, 3, nsheet, "Cálculo do 85 Percentil - Sentido " + lblMA2.Text + "/" + lblMB2.Text);
                }
                else
                {
                    objExcel.Pagina(5, 3, nsheet, "Cálculo do 85 Percentil - Sentido " + lblSent2.Text);
                }

                objExcel.Pagina(60, 14, nsheet, npag.ToString());
                objExcel.MudaFonte(60, 14, nsheet, "Arial", 10);

                SetaPaginas(ObjPags, nsheet);
                /// fim pagina e km
                




                nsheet = nsheet + 1;
                if (objGeom.umsent == 0) // Tem 2 sentidos
                {
                    npag = npag + 1; //só incrementa o número da página se entrar aqui
                    objExcel.Pagina(3, 13, nsheet, lblRod2.Text + "  km " + cUt.ConvPontoVirg(lblkmR2.Text));
                    objExcel.MudaFonte(3, 13, nsheet, "Arial", 11);

                    objExcel.Pagina(5, 3, nsheet, "Cálculo do 85 Percentil - Sentido " + lblMB2.Text + "/" + lblMA2.Text);

                    objExcel.Pagina(60, 14, nsheet, npag.ToString());
                    objExcel.MudaFonte(60, 14, nsheet, "Arial", 10);
                    SetaPaginas(ObjPags, nsheet);
                }




            }

            Pagina12();
            Pagina13();
            Pagina14();

            Pagina15();
            Pagina16();

            // Pagina17();
            Pagina18();
            //  Pagina19();

            Pagina20();

            Pagina21();
            Pagina22();
            //   Pagina23();
            Pagina24();
            Pagina25();
            Pagina26();

            RenomeiaSheets();


            objExcel.FechaExcel();

            // objExcel.WriteToCell(46, 8, "Teste");

            //objExcel.Save();
            //objExcel.Fecha();
            reader1.Close();
        }

        private void cadastrarNovoPontoFEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frmcadastro fCadastro = new Frmcadastro();

            //Frmcadastro.Text = "Cadastro de novo ponto de fiscalização eletrônica";
            ////Frmcadastro fCadastro = new Frmcadastro();
            fCadastro.ShowDialog();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //excel.Application.Quit();
           
            // /////////////////////////////////////////  objExcel.EncerraProcessos();


           // objExcel.Fecha();
            mysqlCon1.Close();
            mysqlCon2.Close();
            // objExcel.FechaExcel();
            // objExcel.Fecha();
           // objExcel = null;
            this.Close();
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void sobreToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Create a new instance of the form.
            AboutBox1 frmSobre = new AboutBox1();
            //// Create two buttons to use as the accept and cancel buttons.
            //Button button1 = new Button();
            //Button button2 = new Button();

            //// Set the text of button1 to "OK".
            //button1.Text = "OK";
            //// Set the position of the button on the form.
            //button1.Location = new Point(10, 10);
            //// Set the text of button2 to "Cancel".
            //button2.Text = "Cancel";
            //// Set the position of the button based on the location of button1.
            //button2.Location
            //   = new Point(button1.Left, button1.Height + button1.Top + 10);
            //// Set the caption bar text of the form.   
            //frmSobre.Text = "My Dialog Box";
            //// Display a help button on the form.
            //frmSobre.HelpButton = true;

            //// Define the border style of the form to a dialog box.
            //frmSobre.FormBorderStyle = FormBorderStyle.FixedDialog;
            //// Set the MaximizeBox to false to remove the maximize box.
            //frmSobre.MaximizeBox = false;
            //// Set the MinimizeBox to false to remove the minimize box.
            //frmSobre.MinimizeBox = false;
            //// Set the accept button of the form to button1.
            //frmSobre.AcceptButton = button1;
            //// Set the cancel button of the form to button2.
            //frmSobre.CancelButton = button2;
            //// Set the start position of the form to the center of the screen.
            //frmSobre.StartPosition = FormStartPosition.CenterScreen;

            //// Add button1 to the form.
            //frmSobre.Controls.Add(button1);
            //// Add button2 to the form.
            //frmSobre.Controls.Add(button2);

            //// Display the form as a modal dialog box.
            frmSobre.ShowDialog();

            

        }

        private void incluirNaBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }




        private void arquivoDeVelocidadeEComprimento2FaixasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Inclui arquivo formato 241 de Velocidade 14 Bins (km/h) 
            // 01-19	20-29	30-39	40-49	50-59	60-69	70-79	80-89	90-99	
            // 100-109	110-119	120-129	130-139	140-199

            // e Comprimento 3 Bins(Moto, Passeio e Comerciais)

            // 2 Lanes (no mesmo sentido) serão somadas para virarem 1

            FrmEntraArquivosVelComp fLavelc = new FrmEntraArquivosVelComp();
            fLavelc.ShowDialog();

        }

        private void limpaBaseClassificToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //
            // Initializes the variables to pass to the MessageBox.Show method.

            string message = "Tem certeza que deseja limpar a base de classificação?";
            string caption = "Deletar a base ???";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.

            result = MessageBox.Show(this, message, caption, buttons,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2,
                MessageBoxOptions.RightAlign);

            if (result == DialogResult.Yes)
            {
                ConectaBancoClassfic();

                comcfc1.CommandText = "delete from tbvelhoradia";
                comcfc1.ExecuteNonQuery();

                comcfc1.CommandText = "delete from tbcomphoradia";
                comcfc1.ExecuteNonQuery();

                DesconectaBancoClassfic();

            }



        }



        private void gerarTabelaAleatórioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmGeraTabelaAleat frmGTa = new FrmGeraTabelaAleat();
            frmGTa.ShowDialog();
        }

        private void gerarDiasVelocidadeComprimentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

        }







        // *******************************************************************
        public void CarregaPontoClassVez(C_pontoCL objPontV)
        {
            ConectaBancoClassfic();
            comcfc1.CommandText = "select * from tbpontovez";

            readercfc1= comcfc1.ExecuteReader();

            
                while (readercfc1.Read())
            {
                objPontV.Rod = readercfc1["Rod"].ToString();
                objPontV.km = readercfc1["km"].ToString();
               // if (objPontV.km.Length<3) { objPontV.km = "0" + objPontV.km; }
                
            }

            DesconectaBancoClassfic();


            ConectaBancoClassfic();

            comcfc1.CommandText = "select * from tbpontos where Rod=" + "'" + objPontV.Rod + "'" + " and " ;
            comcfc1.CommandText += "km=" + "'" + objPontV.km + "'";

            readercfc1=comcfc1.ExecuteReader();

            while (readercfc1.Read())
            {                
                objPontV.municipio = readercfc1["municipio"].ToString();
                objPontV.munA = readercfc1["munA"].ToString();
                objPontV.munB = readercfc1["munB"].ToString();
                objPontV.sAB = Int32.Parse(readercfc1["sAB"].ToString());
                objPontV.sBA = Int32.Parse(readercfc1["sBA"].ToString());
                objPontV.ntfaixas = Int32.Parse(readercfc1["ntfaixas"].ToString());
                objPontV.nfxsent = Int32.Parse(readercfc1["nfxsent"].ToString());
                objPontV.qtdclass = Int32.Parse(readercfc1["qtdclass"].ToString());
                objPontV.qtdcroquis = Int32.Parse(readercfc1["qtdcroquis"].ToString());
                objPontV.psimples= Int32.Parse(readercfc1["psimples"].ToString());
                objPontV.periodo = (readercfc1["periodo"].ToString());
            }

            DesconectaBancoClassfic();
        }
        // **************************************************







        // **************************************************

        private string CarregaDiretorio(ref string caminho)
        {
            ConectaBancoClassfic();

            comcfc1.CommandText = "select * from tbdiretorio";
            readercfc1 = comcfc1.ExecuteReader();

            while (readercfc1.Read())
            { caminho = readercfc1["Caminho"].ToString(); }

            DesconectaBancoClassfic();
            return caminho;
        }

        // **************************************************


        // **************************************************

        private void montaPlanilhaVelocidadeComprimentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

        }

        // **************************************************************************

        private void Capa(string rod, string km)
        {
            // objExcel.Pagina(48, 8, nsheet, "MARÇO/2020");

            objExcel.Agrupar(45, 6, 45, 7);     objExcel.Alinhar(45, 6, "Center");
            objExcel.Agrupar(46, 6, 46, 7);     objExcel.Alinhar(46, 6, "Center");
            objExcel.Agrupar(47, 6, 47, 7);     objExcel.Alinhar(47, 6, "Center");


            objExcel.Pagina(45, 6, 1, rod.ToUpper(), 14,"Black");
            objExcel.Pagina(46, 6, 1,"km " + km, 14, "Black");
            objExcel.Pagina(47, 6, 1, objMesAno.mes + "/" + objMesAno.ano, 14, "Black");
        }

        // **************************************************************************

        private void Apres(string sper)
        {
            string ssentidos = "";

            //objExcel.Desagrupar(3,12);
            //objExcel.MudaCor("L3:L3", "Gray");


            objExcel.SelecionaPagina(npag);

            objExcel.Pagina(3, 9, 2, objPontV.Rod.ToUpper() + "  km " + objPontV.km);
            objExcel.Pagina(3, 12, 3, objPontV.Rod.ToUpper() + "  km " + objPontV.km);


            CarregaPontoClassVez(objPontV);

            objExcel.Pagina(12, 2, npag, "                - MUNICÍPIO: " + objPontV.municipio.ToUpper() , 12, "Black");
            objExcel.Pagina(14, 2, npag, "                - RODOVIA: " + objPontV.Rod.ToUpper(), 12, "Black");
            objExcel.Pagina(16, 2, npag, "                - km: " + objPontV.km, 12, "Black");
            objExcel.Pagina(18, 2, npag, "                - PONTO A: " + objPontV.munA.ToUpper(), 12, "Black");
            objExcel.Pagina(20, 2, npag, "                - PONTO B: " + objPontV.munB.ToUpper(), 12, "Black");

            if ((objPontV.qtdclass == 2) ) { ssentidos = "- DUAS CLASSIFICAÇÕES"; } else { ssentidos = "- UMA CLASSIFICAÇÃO"; }

            if ( (objPontV.sAB==1) && (objPontV.sBA == 1) )  { ssentidos += " (DOIS SENTIDOS)"; } else { ssentidos += " (UM SENTIDO)"; }
            objExcel.Pagina(22, 2, npag, "                " + ssentidos , 12, "Black");

            objExcel.Desagrupar(24, 2);
            objExcel.Agrupar(24, 3, 25, 13);
            objExcel.Agrupar(24, 2, 25, 2);
            ssentidos = "CLASSIFICAÇÃO AB: -";
            if (objPontV.sAB == 1) { ssentidos += " FLUXO DE VEÍCULOS QUE SEGUE DE " + objPontV.munA.ToUpper() + " PARA " + objPontV.munB.ToUpper(); }
            objExcel.Pagina(24, 2, npag, "               ", 12, "Black");
            objExcel.Pagina(24, 3, npag, "                ", 12, "Black");
            objExcel.Pagina(25, 2, npag, "                ", 12, "Black");
            objExcel.Pagina(25, 3, npag, "                ", 12, "Black");
            objExcel.Pagina(26, 2, npag, "                ", 12, "Black");
            objExcel.Pagina(26, 3, npag, "                ", 12, "Black");


            objExcel.Pagina(24, 2, npag, "               -", 12, "Black");
            objExcel.Pagina(24, 3, npag, ssentidos, 12, "Black");

            objExcel.Desagrupar(27, 2);
            objExcel.Agrupar(27, 3, 28, 13);
            objExcel.Agrupar(27, 2, 28, 2);
            ssentidos = "CLASSIFICAÇÃO BA: -";
            if (objPontV.sAB == 1) { ssentidos += " FLUXO DE VEÍCULOS QUE SEGUE DE " + objPontV.munB.ToUpper() + " PARA " + objPontV.munA.ToUpper(); }


            objExcel.Pagina(27, 2, npag, "               ", 12, "Black");
            objExcel.Pagina(27, 3, npag, "                ", 12, "Black");
            objExcel.Pagina(28, 2, npag, "                ", 12, "Black");
            objExcel.Pagina(28, 3, npag, "                ", 12, "Black");
            objExcel.Pagina(29, 2, npag, "                ", 12, "Black");
            objExcel.Pagina(29, 3, npag, "                ", 12, "Black");

            objExcel.Pagina(27, 2, npag, "               -", 12, "Black");
            objExcel.Pagina(27, 3, npag, ssentidos, 12, "Black");

            //sper
            objExcel.Pagina(30, 2, npag, "               - PERÍODO: " + objPontV.periodo, 12, "Black");



            objExcel.Pagina(73, 14, npag, "1",10, "Black");
            objExcel.Pagina(6, 3, 2, "1 - Apresentação ...................................................................................1", 12, "Black");

        }





        // ******************************************************************


        private void Mapa (string Rod, string km)
        {
            objExcel.SelecionaPagina(npag);
            objExcel.Pagina(3, 13, npag, objPontV.Rod.ToUpper() + "  km " + objPontV.km);
            
            
            objExcel.Pagina(67, 14, npag, "2", 10, "Black");
       //   objExcel.Pagina(9, 3, 2, "2 - Mapa ..................................................................................................2", 12, "Black");
            objExcel.Pagina(9, 3, 2, "2 - Mapa ................................................................................................2", 12, "Black");
            objExcel.Alinhar(9, 3, "Left");
        }

        // **********************************************************

        private void Croqui(string Rod, string km, Boolean jafoi)
        {
            objExcel.Pagina(3, 13, npag, objPontV.Rod.ToUpper() + "  km " + objPontV.km);
           
            objExcel.Pagina(60, 14, npag, (npag - 2).ToString(), 10, "Black");
            //   objExcel.Pagina(9, 3, 2, "2 - Mapa ..................................................................................................2", 12, "Black");
            if ((!jafoi))
            { objExcel.Pagina(12, 3, 2, "3 - Croqui ...............................................................................................3", 12, "Black"); }
        }

        // **********************************************************





        private void selecionarDiretórioDeTrabalhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDiretorio fDir = new FrmDiretorio();
            fDir.ShowDialog();                    
        }





        public string CalcularEspacos(string str1)
        {
            string strEspacos = "";
            int numEspacos = 65 - str1.Length;

            for(int ind1=0;ind1<=numEspacos;ind1++)
            {
                strEspacos += " ";
            }
            return strEspacos;
        }






        // **********************************************************

        private void Preenche7DiasVelocidade(C_pontoCL objPontV, Boolean tem2Sent)
        {
            int ahora = 0;

            int[] obj241_vel = new int[15];
            int linha = 0;
            int colna = 0;
            string oDia = "";
            string oSent = "";

            string diasem = "";
            string okm = "";
            int idia = 0; int imes = 0;

            int avanco = 0;
            int diaaux = 0;

            int diaI = 0;
            int diaF = 0;

            string aCor = "";
            objExcel.SelecionaPagina(npag);

            LinkedList<string> ListaDias = new LinkedList<string>();
            string atab = "tbvelhoradia" + cUt.TiraTraco(objPontV.Rod) + "km" + cUt.IncluiZeroEsq(objPontV.km);
            
            ConectaBancoClassfic();

           // if (objPontV.km.Length<3) { okm = "0" + objPontV.km; } else { okm = objPontV.km; }
            okm = objPontV.km;

            comcfc1.CommandText = "select periodo from tbpontos" + " where Rod=" + "'" + objPontV.Rod + "'" + " and ";
            comcfc1.CommandText += "km=" + "'" + okm + "'" ;
            readercfc1 = comcfc1.ExecuteReader();
            while (readercfc1.Read())
            { diaI = Int32.Parse((readercfc1["periodo"].ToString()).Substring(0, 2));  // OBTEM O DIA INICIAL DA CONTAGEM
              diaF = Int32.Parse((readercfc1["periodo"].ToString()).Substring(15, 2)); } // OBTEM O DIA INICIAL DA CONTAGEM



            //comcfc1.CommandText = "select DI from " + atab + " where Rod=" + "'" + objPontV.Rod + "'" + " and ";
            //comcfc1.CommandText += "km=" + "'" + okm + "'" + " group by DI order by DI";
            //readercfc1 = comcfc1.ExecuteReader();

            for (int ind1 = diaI; ind1 <= diaF; ind1++)
            {
                ListaDias.AddLast(ind1.ToString());  // CRIA UMA LISTA DOS DIAS DA CONTAGEM
            }

            //while (readercfc1.Read())
            //{ ListaDias.AddLast(readercfc1["DI"].ToString());   // CRIA UMA LISTA DOS DIAS DA CONTAGEM
            //} 



            DesconectaBancoClassfic();

            objExcel.Pagina(21, 3, 2, "6 - Classificação de Velocidade ..........................................................." + nsheet.ToString(), 12, "Black");
            
           

            // processa contagem dia a dia no sentido AB e depois no sentido BA

            // sentido AB
            // sentido AB
            // sentido AB
            // sentido AB


            // npag = 7;
            foreach (string dia in ListaDias)
            {
                if (diaaux != 0)
                {
                     avanco = (Int32.Parse(dia) - diaaux);
                   // avanco = 1;

                    for (int ind1 = 1; ind1 <= avanco; ind1++)
                    { npag++; }

                }



                if (dia.Length < 2) { oDia = "0" + dia; } else { oDia = dia; }
                oSent = "AB";
                ConectaBancoClassfic();  // sentido AB
                comcfc1.CommandText = "select * from " + atab + " where Rod=" + "'" + objPontV.Rod + "'" + " and ";
                comcfc1.CommandText += "km=" + "'" + okm + "'" + " and DI= " + "'" + oDia + "'";
                comcfc1.CommandText += " and Sent=" + "'" + oSent + "'" + " order by HO";
                readercfc1 = comcfc1.ExecuteReader();

                idia = Int32.Parse(dia); imes = Int32.Parse((objPontV.periodo).Substring(3, 2));
                aCor = "White";
                linha = 12;
                while (readercfc1.Read())
                {
                    idia = Int32.Parse(dia);              imes = Int32.Parse(readercfc1["MO"].ToString());
                    ahora=(Int32.Parse(readercfc1["HO"].ToString()));
                    linha++; colna = 3;
                    obj241_vel[1] = Int32.Parse(readercfc1["Vel19"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_vel[1].ToString(),12,"Black");    colna++;
                    obj241_vel[2] = Int32.Parse(readercfc1["Vel29"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_vel[2].ToString()); colna++;
                    obj241_vel[3] = Int32.Parse(readercfc1["Vel39"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_vel[3].ToString()); colna++;
                    obj241_vel[4] = Int32.Parse(readercfc1["Vel49"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_vel[4].ToString()); colna++;
                    obj241_vel[5] = Int32.Parse(readercfc1["Vel59"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_vel[5].ToString()); colna++;
                    obj241_vel[6] = Int32.Parse(readercfc1["Vel69"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_vel[6].ToString()); colna++;
                    obj241_vel[7] = Int32.Parse(readercfc1["Vel79"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_vel[7].ToString()); colna++;
                    obj241_vel[8] = Int32.Parse(readercfc1["Vel89"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_vel[8].ToString()); colna++;
                    obj241_vel[9] = Int32.Parse(readercfc1["Vel99"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_vel[9].ToString()); colna++;
                    obj241_vel[10] = Int32.Parse(readercfc1["Vel109"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_vel[10].ToString()); colna++;
                    obj241_vel[11] = Int32.Parse(readercfc1["Vel119"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_vel[11].ToString()); colna++;
                    obj241_vel[12] = Int32.Parse(readercfc1["Vel129"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_vel[12].ToString()); colna++;
                    obj241_vel[13] = Int32.Parse(readercfc1["Vel139"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_vel[13].ToString());    colna++;
                    obj241_vel[14] = Int32.Parse(readercfc1["Vel199"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_vel[14].ToString(),12,"Black"); colna++;

                    objExcel.CorFundo(linha, 2, npag, aCor);
                    if (aCor == "White") { aCor = "PBlue"; } else { aCor = "White"; }

                    if (ahora == 24)
                    { objExcel.Pagina(38, 17, npag, nsheet.ToString(), 10, "Black");
                        nsheet++;  // numero da pagina  AB                       
                    } 


                }
                objExcel.Pagina(2, 17, npag, objPontV.Rod + " km " + objPontV.km, 11, "Black");
                objExcel.Pagina(39, 17, npag, objPontV.Rod + " km " + objPontV.km, 11, "Black");
                objExcel.Pagina(76, 17, npag, objPontV.Rod + " km " + objPontV.km, 11, "Black");


                /////////////////CABEÇALHO AB ***************************
                for (int ind1=6;ind1<=6;ind1++)         {objExcel.Agrupar(ind1, 3, ind1, 16);  }
                for (int ind1 = 10; ind1 <= 10; ind1++) { objExcel.Agrupar(ind1, 3, ind1, 16); }


                objExcel.Alinhar(6, 3, "Left");               
                for (int ind1 = 7; ind1 <= 9; ind1++)    
                {objExcel.Alinhar(ind1, 5, "Left");
                    objExcel.Alinhar(ind1, 9, "Left");
                    objExcel.Alinhar(ind1, 12, "Left");

                }               
                objExcel.Alinhar(10, 3, "Center");
                objExcel.Alinhar(11, 3, "Center");


                DateTime dt = new DateTime(2020, imes, idia);
                //  Console.WriteLine((int)dt.DayOfWeek);
                diasem = (dt.DayOfWeek).ToString();
                diasem = cUt.DiaSem(diasem.Trim());
                diasem = diasem.ToUpper();
                if ((diasem == "SÁBADO") || (diasem == "DOMINGO")) { } else { diasem += " FEIRA"; }
                
                objExcel.Pagina(6, 3, npag, "                                                     CONTAGEM CLASSIFICATÓRIA DE VEÍCULOS POR VELOCIDADE", 14, "nada");
                
                objExcel.Pagina(7, 5, npag, "RODOVIA: " + objPontV.Rod);
                objExcel.Pagina(7, 9, npag, "KM: " + objPontV.km);
                objExcel.Pagina(7, 12, npag, "MUNICÍPIO: " + objPontV.municipio.ToUpper());

                //objExcel.Pagina(7, 5, npag, "                                                               RODOVIA: " + objPontV.Rod + "                          KM: " + objPontV.km + "                     MUNICÍPIO: " + objPontV.municipio.ToUpper());

                objExcel.Pagina(8, 5, npag, "PONTO A: " + objPontV.munA.ToUpper());
                objExcel.Pagina(8, 12, npag, "PONTO B: " + objPontV.munB.ToUpper());

                // objExcel.Pagina(8, 5, npag, "                                                               PONTO A: " + objPontV.munA.ToUpper() + CalcularEspacos("PONTO A: " + objPontV.munA.ToUpper()) + "PONTO B: " + objPontV.munB.ToUpper());

                // objExcel.Pagina(9, 3, npag, "                                                               DATA: " + readercfc1["DI"].ToString() + "/" + readercfc1["MO"].ToString() + "/20" + readercfc1["YE"].ToString() + "                                                               DIA DA SEMANA: " + diasem);
                //objExcel.Pagina(9, 3, npag, "                                                               DATA: " + oDia + "/" + (objPontV.periodo).Substring(3, 2) + "/" + (objPontV.periodo).Substring(6, 4) + "                                                               DIA DA SEMANA: " + diasem);
                objExcel.Pagina(9, 5, npag, "DATA: " + oDia + "/" + (objPontV.periodo).Substring(3, 2) + "/" + (objPontV.periodo).Substring(6, 4) );
                objExcel.Pagina(9, 12, npag, "DIA DA SEMANA: " + diasem);

                objExcel.Pagina(10, 3, npag, "SENTIDO AB",14,"nada");




                /////////////////CABEÇALHO BA ***************************
                ///

                
                for (int ind1 = 43; ind1 <= 43; ind1++)
                { objExcel.Agrupar(ind1, 3, ind1, 16); }

                for (int ind1 = 47; ind1 <= 47; ind1++)
                { objExcel.Agrupar(ind1, 3, ind1, 16); }

                objExcel.Alinhar(43, 3, "Left");
                for (int ind1 = 44; ind1 <= 46; ind1++) 
                { objExcel.Alinhar(ind1, 5, "Left");
                    objExcel.Alinhar(ind1, 9, "Left");
                    objExcel.Alinhar(ind1, 12, "Left");

                }
                objExcel.Alinhar(47, 3, "Center");
                objExcel.Alinhar(49, 3, "Center");


                dt = new DateTime(2020, imes, idia);
                //  Console.WriteLine((int)dt.DayOfWeek);
                diasem = (dt.DayOfWeek).ToString();
                diasem = cUt.DiaSem(diasem.Trim());
                diasem = diasem.ToUpper();
                if ((diasem == "SÁBADO") || (diasem == "DOMINGO")) { } else { diasem += " FEIRA"; }

                objExcel.Pagina(43, 3, npag, "                                                     CONTAGEM CLASSIFICATÓRIA DE VEÍCULOS POR VELOCIDADE", 14, "nada");
                objExcel.Pagina(44, 5, npag, "RODOVIA: " + objPontV.Rod);
                objExcel.Pagina(44, 9, npag, "KM: " + objPontV.km);
                objExcel.Pagina(44, 12, npag, "MUNICÍPIO: " + objPontV.municipio.ToUpper());


               // objExcel.Pagina(45, 3, npag,  "                                                               PONTO A: " + objPontV.munA.ToUpper() + CalcularEspacos("PONTO A: " + objPontV.munA.ToUpper()) + "PONTO B: " + objPontV.munB.ToUpper());
                objExcel.Pagina(45, 5, npag, "PONTO A: " + objPontV.munA.ToUpper());
                objExcel.Pagina(45, 12, npag, "PONTO B: " + objPontV.munB.ToUpper());
               


                //objExcel.Pagina(46, 3, npag, "                                                               DATA: " + readercfc1["DI"].ToString() + "/" + readercfc1["MO"].ToString() + "/20" + readercfc1["YE"].ToString() + "                                                               DIA DA SEMANA: " + diasem);
                objExcel.Pagina(46, 5, npag, "DATA: " + oDia + "/" + (objPontV.periodo).Substring(3, 2) + "/" + (objPontV.periodo).Substring(6, 4) );
                objExcel.Pagina(46, 12, npag, "DIA DA SEMANA: " + diasem);


                objExcel.Pagina(47, 3, npag, "SENTIDO BA", 14, "nada");

               // objExcel.RenomeaPlanilha(npag, oDia, readercfc1["MO"].ToString());
                objExcel.RenomeaPlanilha(npag, oDia, (objPontV.periodo).Substring(3, 2));




                /////////////////CABEÇALHO TOTAL ***************************


                for (int ind1 = 80; ind1 <= 80; ind1++)
                { objExcel.Agrupar(ind1, 3, ind1, 16); }

                for (int ind1 = 84; ind1 <= 84; ind1++)
                { objExcel.Agrupar(ind1, 3, ind1, 16); }

                objExcel.Alinhar(80, 3, "Left");
                for (int ind1 = 81; ind1 <= 83; ind1++)
                { objExcel.Alinhar(ind1, 5, "Left");
                    objExcel.Alinhar(ind1, 9, "Left");
                    objExcel.Alinhar(ind1, 12, "Left");

                }
                objExcel.Alinhar(84, 3, "Center");
                objExcel.Alinhar(85, 3, "Center");



                dt = new DateTime(2020, imes, idia);
                //  Console.WriteLine((int)dt.DayOfWeek);
                diasem = (dt.DayOfWeek).ToString();
                diasem = cUt.DiaSem(diasem.Trim());
                diasem = diasem.ToUpper();
                if ((diasem == "SÁBADO") || (diasem == "DOMINGO")) { } else { diasem += " FEIRA"; }

                 
                objExcel.Pagina(80, 3, npag, "                                                     CONTAGEM CLASSIFICATÓRIA DE VEÍCULOS POR VELOCIDADE", 14, "nada");

                objExcel.Pagina(81, 5, npag, "RODOVIA: " + objPontV.Rod);
                objExcel.Pagina(81, 9, npag, "KM: " + objPontV.km);
                objExcel.Pagina(81, 12, npag, "MUNICÍPIO: " + objPontV.municipio.ToUpper());

                objExcel.Pagina(82, 5, npag, "PONTO A: " + objPontV.munA.ToUpper());
                objExcel.Pagina(82, 12, npag, "PONTO B: " + objPontV.munB.ToUpper());
                // objExcel.Pagina(83, 3, npag, "                                                               DATA: " + readercfc1["DI"].ToString() + "/" + readercfc1["MO"].ToString() + "/20" + readercfc1["YE"].ToString() + "                                                               DIA DA SEMANA: " + diasem);
                objExcel.Pagina(83, 5, npag, "DATA: " + oDia + "/" + (objPontV.periodo).Substring(3, 2) + "/" + (objPontV.periodo).Substring(6, 4));
                objExcel.Pagina(83, 12, npag, "DIA DA SEMANA: " + diasem);

                objExcel.Pagina(84, 3, npag, "AMBOS OS SENTIDOS", 14, "nada");

                objExcel.RenomeaPlanilha(npag, oDia, (objPontV.periodo).Substring(3, 2));










                DesconectaBancoClassfic();












                // sentido BA
                // sentido BA
                // sentido BA
                // sentido BA
                oSent = "BA";
                ConectaBancoClassfic();  // sentido BA
                comcfc1.CommandText = "select * from " + atab + " where Rod=" + "'" + objPontV.Rod + "'" + " and ";
                comcfc1.CommandText += "km=" + "'" + okm + "'" + " and DI= " + "'" + oDia + "'";
                comcfc1.CommandText += " and Sent=" + "'" + oSent + "'" + " order by HO";
                readercfc1 = comcfc1.ExecuteReader();

                aCor = "White";
                linha = 49;
                while (readercfc1.Read())
                {
                    idia = Int32.Parse(dia); imes = Int32.Parse(readercfc1["MO"].ToString());
                    ahora = (Int32.Parse(readercfc1["HO"].ToString()));
                    linha++; colna = 3;
                    obj241_vel[1] = Int32.Parse(readercfc1["Vel19"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_vel[1].ToString(),12,"Black"); colna++;
                    obj241_vel[2] = Int32.Parse(readercfc1["Vel29"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_vel[2].ToString()); colna++;
                    obj241_vel[3] = Int32.Parse(readercfc1["Vel39"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_vel[3].ToString()); colna++;
                    obj241_vel[4] = Int32.Parse(readercfc1["Vel49"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_vel[4].ToString()); colna++;
                    obj241_vel[5] = Int32.Parse(readercfc1["Vel59"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_vel[5].ToString()); colna++;
                    obj241_vel[6] = Int32.Parse(readercfc1["Vel69"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_vel[6].ToString()); colna++;
                    obj241_vel[7] = Int32.Parse(readercfc1["Vel79"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_vel[7].ToString()); colna++;
                    obj241_vel[8] = Int32.Parse(readercfc1["Vel89"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_vel[8].ToString()); colna++;
                    obj241_vel[9] = Int32.Parse(readercfc1["Vel99"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_vel[9].ToString()); colna++;
                    obj241_vel[10] = Int32.Parse(readercfc1["Vel109"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_vel[10].ToString()); colna++;
                    obj241_vel[11] = Int32.Parse(readercfc1["Vel119"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_vel[11].ToString()); colna++;
                    obj241_vel[12] = Int32.Parse(readercfc1["Vel129"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_vel[12].ToString()); colna++;
                    obj241_vel[13] = Int32.Parse(readercfc1["Vel139"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_vel[13].ToString()); colna++;
                    obj241_vel[14] = Int32.Parse(readercfc1["Vel199"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_vel[14].ToString()); colna++;

                    objExcel.CorFundo(linha, 2, npag, aCor);
                    if (aCor == "White") { aCor = "PBlue"; } else { aCor = "White"; }

                    if (ahora == 24)
                    {
                        objExcel.Pagina(75, 17, npag, nsheet.ToString(), 10, "Black");
                        nsheet++; // numero da pagina BA
                        
                    } 

                }

                

                DesconectaBancoClassfic();







                // 

                //  AMBOS OS SENTIDOS
                aCor = "White";
                linha = 86;
               for (int ind1=1;ind1<=25;ind1++)
                {
                    
                    linha++; colna = 3;

                    objExcel.SomaCels(linha, colna);   colna++;
                    objExcel.SomaCels(linha, colna);   colna++;
                    objExcel.SomaCels(linha, colna);   colna++;
                    objExcel.SomaCels(linha, colna);   colna++;
                    objExcel.SomaCels(linha, colna);   colna++;
                    objExcel.SomaCels(linha, colna);   colna++;
                    objExcel.SomaCels(linha, colna);   colna++;
                    objExcel.SomaCels(linha, colna);   colna++;
                    objExcel.SomaCels(linha, colna);   colna++;
                    objExcel.SomaCels(linha, colna);   colna++;
                    objExcel.SomaCels(linha, colna);   colna++;
                    objExcel.SomaCels(linha, colna);   colna++;
                    objExcel.SomaCels(linha, colna);   colna++;
                    objExcel.SomaCels(linha, colna);   colna++;
                    objExcel.SomatorioCels(linha, colna);

                    objExcel.CorFundo(linha, 2, npag, aCor);
                    if (aCor == "White") { aCor = "PBlue"; } else { aCor = "White"; }
                }




                // npag++;

                diaaux =Int32.Parse(dia);

                if (tem2Sent)
                {
                    objExcel.Pagina(112, 17, npag, nsheet.ToString(), 10, "Black");  // numero da pagina Total
                    nsheet++;
                }
                objExcel.MudaCor("Q2:Q2", "Gray");
                objExcel.MudaCor("Q39:Q39", "Gray");
                objExcel.MudaCor("Q76:Q76", "Gray");
            }
            //npag--;

            


            

        }


        // *****************************************************
        // *****************************************************



















        // *****************************************************
        // *****************************************************
        private void PreencheResumoVelocidade(C_pontoCL objPontV, string periodo )
        {
            string aCor = "";
            int linha = 0;


            objExcel.SelecionaPagina(npag);


            objExcel.Agrupar(2, 3, 2, 16); objExcel.Alinhar(2, 3, "Center");
            objExcel.Pagina(2, 3, npag, "FUNDAÇÃO DEPARTAMENTO DE ESTRADAS DE RODAGEM DO ESTADO DO RIO DE JANEIRO");

           

            objExcel.Agrupar(7, 3, 7, 16); objExcel.Alinhar(7, 3, "Center");
            objExcel.Pagina(7, 3, npag, "CONTAGEM CLASSIFICATÓRIA DE VEÍCULOS POR VELOCIDADE");

            //objExcel.Agrupar(8, 3, 8, 16); objExcel.Alinhar(8, 3, "Left");
            objExcel.Alinhar(8, 5, "Left"); objExcel.Pagina(8, 5, npag, "RODOVIA: " + objPontV.Rod);
            objExcel.Alinhar(8, 8, "Left"); objExcel.Pagina(8, 8, npag, "  KM: " + objPontV.km);
            objExcel.Alinhar(8, 11, "Left"); objExcel.Pagina(8, 11, npag, "MUNICÍPIO: " + objPontV.municipio.ToUpper());

            //objExcel.Pagina(8, 3, npag, "                          RODOVIA: " + objPontV.Rod + "                          KM: " + objPontV.km + "                     MUNICÍPIO: " + objPontV.municipio.ToUpper());

            // objExcel.Agrupar(9, 3, 9, 16); objExcel.Alinhar(9, 3, "Left");
            objExcel.Alinhar(9, 5, "Left"); objExcel.Pagina(9, 5, npag, "PONTO A: " + objPontV.munA.ToUpper());            
            objExcel.Alinhar(9, 11, "Left"); objExcel.Pagina(9, 11, npag, "PONTO B: " + objPontV.munB.ToUpper());
            
            //objExcel.Pagina(9, 3, npag, "                          PONTO A: " + objPontV.munA.ToUpper() + CalcularEspacos("PONTO A: " + objPontV.munA.ToUpper()) + "PONTO B: " + objPontV.munB.ToUpper());

            objExcel.Agrupar(10, 3, 10, 16); objExcel.Alinhar(10, 3, "Left");
            objExcel.Pagina(10, 3, npag, "                                                               PERÍODO: " + periodo );


            aCor = "White";
            linha = 18;
            for (linha = 18; linha <= 24; linha++)
            {
                objExcel.CorFundo(linha, 2, npag, aCor);
                if (aCor == "White") { aCor = "PBlue"; } else { aCor = "White"; }
            }

            aCor = "White";
            linha = 34;
            for (linha = 34; linha <= 40; linha++)
            {
                objExcel.CorFundo(linha, 2, npag, aCor);
                if (aCor == "White") { aCor = "PBlue"; } else { aCor = "White"; }
            }

            aCor = "White";
            linha = 51;
            for (linha = 51; linha <= 57; linha++)
            {
                objExcel.CorFundo(linha, 2, npag, aCor);
                if (aCor == "White") { aCor = "PBlue"; } else { aCor = "White"; }
            }

            objExcel.Pagina(1, 17, npag, objPontV.Rod + " km " + objPontV.km, 11, "Black");  // rod + km
            objExcel.Pagina(62, 17, npag, nsheet.ToString(), 10, "Black");  //numero da pagina            
            objExcel.Pagina(24, 3, 2, "7 - Classificação de Velocidade - Resumo .........................................." + nsheet.ToString(), 12, "Black");  // indice
            nsheet++;
            objExcel.MudaCor("Q1:Q1", "Gray");
        }

        // *************************************************************








        // *****************************************************
        // *****************************************************
        private void PreencheResumoVelocidadeGraph(C_pontoCL objPontV, string periodo, Boolean tem2Sent)
        {

            objExcel.SelecionaPagina(npag);

            objExcel.Agrupar(67, 4, 67, 16); objExcel.Alinhar(67, 4, "Left");
            
            
            //objExcel.Agrupar(68, 4, 68, 16); 
            objExcel.Alinhar(68, 4, "Left");
           // objExcel.Agrupar(69, 4, 69, 16);
            objExcel.Alinhar(69, 4, "Left");
            objExcel.Agrupar(70, 4, 70, 16); objExcel.Alinhar(70, 4, "Left");

            
            
            objExcel.Agrupar(71, 4, 71, 16); objExcel.Alinhar(71, 4, "Center");

            objExcel.Pagina(67, 4, npag, "                                                                 CONTAGEM CLASSIFICATÓRIA DE VEÍCULOS POR VELOCIDADE");
           


            objExcel.Pagina(68, 6, npag, "RODOVIA: " + objPontV.Rod );
            objExcel.Pagina(68, 9, npag, "  KM: " + objPontV.km);
            objExcel.Pagina(68, 11, npag, "MUNICÍPIO: " + objPontV.municipio.ToUpper());

            objExcel.Pagina(69, 6, npag, "PONTO A: " + objPontV.munA.ToUpper());
            objExcel.Pagina(69, 11, npag, "PONTO B: " + objPontV.munB.ToUpper());

            objExcel.Pagina(70, 4, npag, "                                              PERÍODO: " + objPontV.periodo);
           


            objExcel.Pagina(71, 4, npag, "SENTIDO AB");







            objExcel.Agrupar(120, 4, 120, 16); objExcel.Alinhar(120, 4, "Left");
            
            
            //objExcel.Agrupar(121, 4, 121, 16); 
            objExcel.Alinhar(121, 4, "Left");
            //objExcel.Agrupar(122, 4, 122, 16); 
            objExcel.Alinhar(122, 4, "Left");
           
            
            objExcel.Agrupar(123, 4, 123, 16); objExcel.Alinhar(123, 4, "Left");
            objExcel.Agrupar(124, 4, 124, 16); objExcel.Alinhar(124, 4, "Center");

            objExcel.Pagina(120, 4, npag, "                                                                CONTAGEM CLASSIFICATÓRIA DE VEÍCULOS POR VELOCIDADE");
            
            objExcel.Pagina(121, 6, npag, "RODOVIA: " + objPontV.Rod );
            objExcel.Pagina(121, 9, npag, "KM: " + objPontV.km );
            objExcel.Pagina(121, 11, npag, "MUNICÍPIO: " + objPontV.municipio.ToUpper());

            objExcel.Pagina(122, 6, npag, "PONTO A: " + objPontV.munA.ToUpper() );
            objExcel.Pagina(122, 11, npag, "PONTO B: " + objPontV.munB.ToUpper());

            objExcel.Pagina(123, 4, npag, "                                              PERÍODO: " + objPontV.periodo);
            objExcel.Pagina(124, 4, npag, "SENTIDO BA");






            objExcel.Agrupar(174, 4, 174, 16); objExcel.Alinhar(174, 4, "Left");
           // objExcel.Agrupar(175, 4, 175, 16); 
            objExcel.Alinhar(175, 4, "Left");
            //objExcel.Agrupar(176, 4, 176, 16);
            objExcel.Alinhar(176, 4, "Left");
            objExcel.Agrupar(177, 4, 177, 16); objExcel.Alinhar(177, 4, "Left");
            objExcel.Agrupar(178, 4, 178, 16); objExcel.Alinhar(178, 4, "Center");

            objExcel.Pagina(174, 4, npag, "                                                                CONTAGEM CLASSIFICATÓRIA DE VEÍCULOS POR VELOCIDADE");
            
            objExcel.Pagina(175, 6, npag, "RODOVIA: " + objPontV.Rod );
            objExcel.Pagina(175, 9, npag, "  KM: " + objPontV.km );
            objExcel.Pagina(175, 11, npag, "MUNICÍPIO: " + objPontV.municipio.ToUpper());

            objExcel.Pagina(176, 6, npag, "PONTO A: " + objPontV.munA.ToUpper() );
            objExcel.Pagina(176, 11, npag, "PONTO B: " + objPontV.munB.ToUpper());

            objExcel.Pagina(177, 4, npag, "                                              PERÍODO: " + objPontV.periodo);
            objExcel.Pagina(178, 4, npag, "AMBOS OS SENTIDOS");

            ////objExcel.Pagina(26, 4, npag, "                                              RODOVIA: " + objPontV.Rod + "                          KM: " + objPontV.km + "                     MUNICÍPIO: " + objPontV.municipio.ToUpper());
            ////objExcel.Pagina(27, 4, npag, "                                              PONTO A: " + objPontV.munA.ToUpper() + "                                             PONTO B: " + objPontV.munB.ToUpper());
            ////objExcel.Pagina(28, 4, npag, "                                              PERÍODO: " + periodo);

            ////objExcel.Pagina(46, 4, npag, "                                              RODOVIA: " + objPontV.Rod + "                          KM: " + objPontV.km + "                     MUNICÍPIO: " + objPontV.municipio.ToUpper());
            ////objExcel.Pagina(47, 4, npag, "                                              PONTO A: " + objPontV.munA.ToUpper() + "                                             PONTO B: " + objPontV.munB.ToUpper());
            ////objExcel.Pagina(48, 4, npag, "                                              PERÍODO: " + periodo);

            objExcel.Pagina(64, 17, npag, objPontV.Rod + " km " + objPontV.km, 11, "Black"); // rpd + km
            objExcel.Pagina(117, 17, npag, objPontV.Rod + " km " + objPontV.km, 11, "Black"); // rpd + km
            objExcel.Pagina(171, 17, npag, objPontV.Rod + " km " + objPontV.km, 11, "Black"); // rpd + km


            if (tem2Sent)
            {
                objExcel.Pagina(27, 3, 2, "8 - Classificação de Velocidade - Gráficos .........................................." + nsheet.ToString(), 12, "Black");  // indice


                objExcel.Pagina(116, 17, npag, nsheet.ToString(), 10, "Black"); nsheet++; //numero da pagina
                objExcel.Pagina(170, 17, npag, nsheet.ToString(), 10, "Black"); nsheet++; //numero da pagina
                objExcel.Pagina(223, 17, npag, nsheet.ToString(), 10, "Black"); nsheet++; //numero da pagina

            }
            else
            {
                if (objPontV.sAB == 1)
                {
                    objExcel.Pagina(21, 3, 2, "8 - Classificação de Velocidade - Gráficos ..........................................." + nsheet.ToString(), 12, "Black");  // indice
                    objExcel.Pagina(116, 17, npag, nsheet.ToString(), 10, "Black"); nsheet++; }//numero da pagina

                if (objPontV.sBA == 1)
                {
                    objExcel.Pagina(21, 3, 2, "8 - Classificação de Velocidade - Gráficos ..........................................." + nsheet.ToString(), 12, "Black");  // indice
                    objExcel.Pagina(168, 17, npag, nsheet.ToString(), 10, "Black"); nsheet++; }//numero da pagina
            }

            objExcel.SelecionaPagina(npag);
            objExcel.MudaCor("Q64:Q64", "Gray");
            objExcel.MudaCor("Q117:Q117", "Gray");
            objExcel.MudaCor("Q171:Q171", "Gray");



        }

        // *************************************************************












        // ***********************************************************

        private void Preenche7DiasComprimento(C_pontoCL objPontV, Boolean tem2Sent)
        {
            int[] obj241_cmp = new int[4];
            int linha = 0;
            int colna = 0;
            string oDia = "";
            string oSent = "";

            string diasem = "";
            string okm = "";
            int idia = 0; int imes = 0;

            int ahora = 0;

            int avanco = 0;
            int diaaux = 0;

            string aCor = "";

            int diaI = 0;
            int diaF = 0;


            objExcel.SelecionaPagina(npag);

            LinkedList<string> ListaDias = new LinkedList<string>();
            string atab = "tbcomphoradia" + cUt.TiraTraco(objPontV.Rod) + "km" + cUt.IncluiZeroEsq(objPontV.km);

            ConectaBancoClassfic();

            //if (objPontV.km.Length < 3) { okm = "0" + objPontV.km; } else { okm = objPontV.km; }
            okm = objPontV.km;

            comcfc1.CommandText = "select periodo from tbpontos" + " where Rod=" + "'" + objPontV.Rod + "'" + " and ";
            comcfc1.CommandText += "km=" + "'" + okm + "'" ;
            readercfc1 = comcfc1.ExecuteReader();
            while (readercfc1.Read())
            { diaI= Int32.Parse  ( (readercfc1["periodo"].ToString()).Substring(0,2))    ;  // OBTEM O DIA INICIAL DA CONTAGEM
             diaF = Int32.Parse((readercfc1["periodo"].ToString()).Substring(15, 2)); } // OBTEM O DIA INICIAL DA CONTAGEM



            //comcfc1.CommandText = "select DI from " + atab + " where Rod=" + "'" + objPontV.Rod + "'" + " and ";
            //comcfc1.CommandText += "km=" + "'" + okm + "'" + " group by DI order by DI";
            //readercfc1 = comcfc1.ExecuteReader();

            for (int ind1 = diaI;ind1<=diaF; ind1++)
            {
                ListaDias.AddLast(ind1.ToString());  // CRIA UMA LISTA DOS DIAS DA CONTAGEM
            }

            //while (readercfc1.Read())
            //{ ListaDias.AddLast(readercfc1["DI"].ToString()); } // CRIA UMA LISTA DOS DIAS DA CONTAGEM

            DesconectaBancoClassfic();




            // processa contagem dia a dia no sentido AB e depois no sentido BA

            // sentido AB
            // sentido AB
            // sentido AB
            // sentido AB
            if (objPontV.sAB == 1)   
            {
                objExcel.Pagina(33, 3, 2, "10 - Classificação por Categoria .........................................................." + nsheet.ToString(), 12, "Black");  // indice
            }




            foreach (string dia in ListaDias)

            {

                if (diaaux != 0)
                {
                      avanco = (Int32.Parse(dia) - diaaux);
                   // avanco = 1;

                    for (int ind1 = 1; ind1 <= avanco; ind1++)
                    { npag++; }

                }



                if (dia.Length < 2) { oDia = "0" + dia; } else { oDia = dia; }
                oSent = "AB";
                ConectaBancoClassfic();  // sentido AB
                comcfc1.CommandText = "select * from " + atab + " where Rod=" + "'" + objPontV.Rod + "'" + " and ";
                comcfc1.CommandText += "km=" + "'" + okm + "'" + " and DI= " + "'" + oDia + "'";
                comcfc1.CommandText += " and Sent=" + "'" + oSent + "'" + " order by HO";
                readercfc1 = comcfc1.ExecuteReader();

                idia = Int32.Parse(dia); imes = Int32.Parse((objPontV.periodo).Substring(3, 2));

                aCor = "White";
                linha = 11;
                while (readercfc1.Read())
                {
                    idia = Int32.Parse(dia); imes = Int32.Parse(readercfc1["MO"].ToString());
                    ahora = (Int32.Parse(readercfc1["HO"].ToString()));
                    linha++; colna = 3;
                    obj241_cmp[1] = Int32.Parse(readercfc1["comp1"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_cmp[1].ToString()); colna++; colna++; colna++;
                    obj241_cmp[2] = Int32.Parse(readercfc1["comp2"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_cmp[2].ToString()); colna++; colna++; colna++;
                    obj241_cmp[3] = Int32.Parse(readercfc1["comp3"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_cmp[3].ToString());
                   
                    objExcel.CorFundo(linha, 2, npag, aCor);
                    if (aCor == "White") { aCor = "PBlue"; } else { aCor = "White"; }

                    

                        if ((ahora == 24) && (objPontV.sAB==1))
                    {
                        objExcel.Pagina(1, 12, npag, objPontV.Rod + " km " + objPontV.km, 11, "Black"); // rpd + km
                        objExcel.Pagina(37, 12, npag, nsheet.ToString(), 10, "Black");    //numero da pagina 
                        
                        nsheet++;
                    } 

                 }

                /////////////////CABEÇALHO AB ***************************
                ///
                for (int ind1 = 5; ind1 <= 5; ind1++) { objExcel.Agrupar(ind1, 3, ind1, 11); }
                for (int ind1 = 9; ind1 <= 10; ind1++) { objExcel.Agrupar(ind1, 3, ind1, 11); }

                for (int ind1 = 5; ind1 <= 5; ind1++) { objExcel.Alinhar(ind1, 3, "Left"); }
                for (int ind1 = 9; ind1 <= 10; ind1++) { objExcel.Alinhar(ind1, 3, "Left"); }

                    objExcel.Alinhar(9, 3, "Center");
                objExcel.Alinhar(10, 3, "Center");  

                DateTime dt = new DateTime(2020, imes, idia);
                //  Console.WriteLine((int)dt.DayOfWeek);
                diasem = (dt.DayOfWeek).ToString();
                diasem = cUt.DiaSem(diasem.Trim());
                diasem = diasem.ToUpper();
                if ((diasem == "SÁBADO") || (diasem == "DOMINGO")) { } else { diasem += " FEIRA"; }

                objExcel.Pagina(5, 3, npag, "                                CONTAGEM CLASSIFICATÓRIA POR CATEGORIA DE VEÍCULOS", 14, "nada");

                objExcel.Pagina(6, 4, npag, "RODOVIA: " + objPontV.Rod);                      objExcel.Alinhar(6, 4, "Left");
                objExcel.Pagina(6, 6, npag, "     KM: " + objPontV.km);                           objExcel.Alinhar(6, 6, "Left");
                objExcel.Pagina(6, 8, npag, "MUNICÍPIO: " + objPontV.municipio.ToUpper());      objExcel.Alinhar(6, 8, "Left");


                objExcel.Pagina(7, 4, npag, "PONTO A: " + objPontV.munA.ToUpper());           objExcel.Alinhar(7, 4, "Left");
                objExcel.Pagina(7, 8, npag, "PONTO B: " + objPontV.munB.ToUpper());           objExcel.Alinhar(7, 8, "Left");

                // objExcel.Pagina(8, 3, npag, "                                     DATA: " + readercfc1["DI"].ToString() + "/" + readercfc1["MO"].ToString() + "/20" + readercfc1["YE"].ToString() + "                                                               DIA DA SEMANA: " + diasem);
                objExcel.Pagina(8, 4, npag, "DATA: " + oDia + "/" + (objPontV.periodo).Substring(3, 2) + "/" + (objPontV.periodo).Substring(6, 4));     objExcel.Alinhar(8, 4, "Left");
                objExcel.Pagina(8, 8, npag, "DIA DA SEMANA: " + diasem);         objExcel.Alinhar(8, 8, "Left");

                objExcel.Pagina(9, 3, npag, "SENTIDO AB");
                objExcel.Pagina(10, 3, npag, "CATEGORIA DE VEÍCULOS");


                /////////////////CABEÇALHO BA ***************************
                for (int ind1 = 42; ind1 <= 42; ind1++)
                { objExcel.Agrupar(ind1, 3, ind1, 11);
                    objExcel.Alinhar(ind1, 3, "Left");
                }

                for (int ind1 = 46; ind1 <= 47; ind1++)
                { objExcel.Agrupar(ind1, 3, ind1, 11);
                    objExcel.Alinhar(ind1, 3, "Left");
                }


                for (int ind1 = 43; ind1 <= 45; ind1++) 
                { 
                    objExcel.Alinhar(ind1, 4, "Left");
                    objExcel.Alinhar(ind1, 6, "Left");
                    objExcel.Alinhar(ind1, 8, "Left");
                }
                

                objExcel.Alinhar(46, 3, "Center");
                objExcel.Alinhar(47, 3, "Center");


                dt = new DateTime(2020, imes, idia);
                //  Console.WriteLine((int)dt.DayOfWeek);
                diasem = (dt.DayOfWeek).ToString();
                diasem = cUt.DiaSem(diasem.Trim());
                diasem = diasem.ToUpper();
                if ((diasem == "SÁBADO") || (diasem == "DOMINGO")) { } else { diasem += " FEIRA"; }

                objExcel.Pagina(42, 3, npag, "                                CONTAGEM CLASSIFICATÓRIA POR CATEGORIA DE VEÍCULOS", 14, "nada");
                
                
                objExcel.Pagina(43, 4, npag, "RODOVIA: " + objPontV.Rod);
                objExcel.Pagina(43, 6, npag, "     KM: " + objPontV.km);
                objExcel.Pagina(43, 8, npag, "MUNICÍPIO: " + objPontV.municipio.ToUpper());


                objExcel.Pagina(44, 4, npag, "PONTO A: " + objPontV.munA.ToUpper());
                objExcel.Pagina(44, 8, npag, "PONTO B: " + objPontV.munB.ToUpper());

                objExcel.Pagina(45, 4, npag, "DATA: " + oDia + "/" + (objPontV.periodo).Substring(3, 2) + "/" + (objPontV.periodo).Substring(6, 4));
                objExcel.Pagina(45, 8, npag, "DIA DA SEMANA: " + diasem);


                objExcel.Pagina(46, 3, npag, "SENTIDO BA");  
                objExcel.Pagina(47, 3, npag, "CATEGORIA DE VEÍCULOS");





                /////////////////CABEÇALHO TOTAL ***************************
                ///


                for (int ind1 = 79; ind1 <= 79; ind1++)
                { objExcel.Agrupar(ind1, 3, ind1, 11); }

                for (int ind1 = 83; ind1 <= 84; ind1++)
                { objExcel.Agrupar(ind1, 3, ind1, 11); }



                for (int ind1 = 79; ind1 <= 79; ind1++) { objExcel.Alinhar(ind1, 3, "Left"); }
               
               
                objExcel.Alinhar(83, 3, "Center");
                objExcel.Alinhar(84, 3, "Center");

                dt = new DateTime(2020, imes, idia);
                //  Console.WriteLine((int)dt.DayOfWeek);
                diasem = (dt.DayOfWeek).ToString();
                diasem = cUt.DiaSem(diasem.Trim());
                diasem = diasem.ToUpper();
                if ((diasem == "SÁBADO") || (diasem == "DOMINGO")) { } else { diasem += " FEIRA"; }


                objExcel.Pagina(79, 3, npag, "                                CONTAGEM CLASSIFICATÓRIA POR CATEGORIA DE VEÍCULOS", 14, "nada");
               
                objExcel.Pagina(80, 4, npag, "RODOVIA: " + objPontV.Rod );
                objExcel.Pagina(80, 6, npag, "     KM: " + objPontV.km );
                objExcel.Pagina(80, 8, npag, "MUNICÍPIO: " + objPontV.municipio.ToUpper());

                objExcel.Pagina(81, 4, npag, "PONTO A: " + objPontV.munA.ToUpper());
                objExcel.Pagina(81, 8, npag, "PONTO B: " + objPontV.munB.ToUpper());
                


                objExcel.Pagina(82, 4, npag, "DATA: " + oDia + "/" + (objPontV.periodo).Substring(3, 2) + "/" + (objPontV.periodo).Substring(6, 4));
                objExcel.Pagina(82, 8, npag, "DIA DA SEMANA: " + diasem);

                for (int ind1 = 80; ind1 <= 82; ind1++)
                {
                    objExcel.Alinhar(ind1, 4, "Left");
                    objExcel.Alinhar(ind1, 6, "Left");
                    objExcel.Alinhar(ind1, 8, "Left");
                }


                objExcel.Pagina(83, 3, npag, "AMBOS OS SENTIDOS");
                objExcel.Pagina(84, 3, npag, "CATEGORIA DE VEÍCULOS");

                // objExcel.RenomeaPlanilha(npag, oDia, readercfc1["MO"].ToString() + "-comp");
                objExcel.RenomeaPlanilha(npag, oDia, (objPontV.periodo).Substring(3, 2) + "-comp");




                DesconectaBancoClassfic();











                // sentido BA
                // sentido BA
                // sentido BA
                // sentido BA
                oSent = "BA";
                ConectaBancoClassfic();  // sentido BA
                comcfc1.CommandText = "select * from " + atab + " where Rod=" + "'" + objPontV.Rod + "'" + " and ";
                comcfc1.CommandText += "km=" + "'" + okm + "'" + " and DI= " + "'" + oDia + "'";
                comcfc1.CommandText += " and Sent=" + "'" + oSent + "'" + " order by HO";
                readercfc1 = comcfc1.ExecuteReader();

                aCor = "White";
                linha = 48;
                while (readercfc1.Read())
                {
                    idia = Int32.Parse(dia); imes = Int32.Parse(readercfc1["MO"].ToString());
                    ahora = (Int32.Parse(readercfc1["HO"].ToString()));
                    linha++; colna = 3;
                    obj241_cmp[1] = Int32.Parse(readercfc1["comp1"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_cmp[1].ToString()); colna++; colna++; colna++;
                    obj241_cmp[2] = Int32.Parse(readercfc1["comp2"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_cmp[2].ToString()); colna++; colna++; colna++;
                    obj241_cmp[3] = Int32.Parse(readercfc1["comp3"].ToString()); objExcel.Pagina(linha, colna, npag, obj241_cmp[3].ToString()); colna++;

                    objExcel.CorFundo(linha, 2, npag, aCor);
                    if (aCor == "White") { aCor = "PBlue"; } else { aCor = "White"; }

                    

                    if ((ahora == 24) && (objPontV.sBA == 1))
                    {
                        objExcel.Pagina(38, 12, npag, objPontV.Rod + " km " + objPontV.km, 11, "Black"); // rpd + km
                        objExcel.Pagina(74, 12, npag, nsheet.ToString(), 10, "Black");   //numero da pagina   
                        nsheet++;
                    }
                }



                

                DesconectaBancoClassfic();

                // 

                //  AMBOS OS SENTIDOS
                aCor = "White";
                linha = 85;
                for (int ind1 = 1; ind1 <= 25; ind1++)
                {

                    linha++; colna = 3;
                    // pprimeira posição i=86 e j=3
                    objExcel.SomaCels(linha, colna); colna++; colna++; colna++;
                    objExcel.SomaCels(linha, colna); colna++; colna++; colna++;
                    objExcel.SomaCels(linha, colna); colna++; colna++; colna++;

                    // pprimeira posição i=86 e j=12
                    objExcel.SomatorioCelsComp(linha, colna);

                    objExcel.CorFundo(linha, 2, npag, aCor);
                    if (aCor == "White") { aCor = "PBlue"; } else { aCor = "White"; }

                    if ( (tem2Sent) && (linha==109) )
                    {
                        objExcel.Pagina(75, 12, npag, objPontV.Rod + " km " + objPontV.km, 11, "Black"); // rpd + km
                        objExcel.Pagina(111, 12, npag, nsheet.ToString(), 10, "Black");  //numero da pagina
                        nsheet++;
                    }

                }

                // npag++;

                diaaux = Int32.Parse(dia);

            } // fim foreach

           // npag--;

        }

        // ************************************************************* 




        // ***********************************************************


        private void PreencheResumoComprimento(C_pontoCL objPontV, string periodo)
        {
            string aCor = "";
            int linha = 0;

            objExcel.Pagina(36, 3, 2, "11 - Classificação por Categoria - Resumo ........................................." + nsheet.ToString(), 12, "Black");  // indice

            objExcel.SelecionaPagina(npag);

            objExcel.Agrupar(2, 3, 2, 13); objExcel.Alinhar(2, 3, "Center");
            objExcel.Pagina(2, 3, npag, "FUNDAÇÃO DEPARTAMENTO DE ESTRADAS DE RODAGEM DO ESTADO DO RIO DE JANEIRO");


            for (int ind1 = 7; ind1 <= 7; ind1++) { objExcel.Agrupar(ind1, 3, ind1, 13); }
            for (int ind1 = 10; ind1 <= 10; ind1++) { objExcel.Agrupar(ind1, 3, ind1, 13); }

            for (int ind1 = 7; ind1 <= 7; ind1++) { objExcel.Alinhar(ind1, 3, "Left"); }
            for (int ind1 = 10; ind1 <= 10; ind1++) { objExcel.Alinhar(ind1, 3, "Left"); }


            objExcel.Pagina(7, 3, npag, "                    CONTAGEM CLASSIFICATÓRIA POR CATEGORIA DE VEÍCULOS");
           
            objExcel.Pagina(8, 4, npag, "RODOVIA: " + objPontV.Rod);
            objExcel.Pagina(8, 7, npag, "  KM: " + objPontV.km );
            objExcel.Pagina(8, 9, npag, "MUNICÍPIO: " + objPontV.municipio.ToUpper());
            
            objExcel.Pagina(9, 4, npag, "PONTO A: " + objPontV.munA.ToUpper());
            objExcel.Pagina(9, 9, npag, "PONTO B: " + objPontV.munB.ToUpper());

            for (int ind1 = 8; ind1 <= 9; ind1++)
            {
                objExcel.Alinhar(ind1, 4, "Left");
                objExcel.Alinhar(ind1, 7, "Left");
                objExcel.Alinhar(ind1, 9, "Left");
            }

            objExcel.Pagina(10, 3, npag, "                                         PERÍODO: " + objPontV.periodo);


            aCor = "White";
            linha = 16;
            for(linha=16;linha<=22;linha++)
            {
                objExcel.CorFundo(linha, 2, npag, aCor);
                if (aCor == "White") { aCor = "PBlue"; } else { aCor = "White"; }
            }

            aCor = "White";
            linha = 36;
            for (linha = 36; linha <= 42; linha++)
            {
                objExcel.CorFundo(linha, 2, npag, aCor);
                if (aCor == "White") { aCor = "PBlue"; } else { aCor = "White"; }
            }

            aCor = "White";
            linha = 56;
            for (linha = 56; linha <= 62; linha++)
            {
                objExcel.CorFundo(linha, 2, npag, aCor);
                if (aCor == "White") { aCor = "PBlue"; } else { aCor = "White"; }
            }


            objExcel.Pagina(1, 13, npag, objPontV.Rod + " km " + objPontV.km, 11, "Gray"); // rpd + km
            objExcel.Pagina(65, 13, npag, nsheet.ToString(), 10, "Black");   //numero da pagina
            nsheet++;

            


        }


        // *************************************************************
        // ***********************************************************








        // ***********************************************************


        private void PreencheResumoComprimentoGraph(C_pontoCL objPontV, string periodo)
        {
            objExcel.SelecionaPagina(npag);

            for (int ind1 = 66; ind1 <= 66; ind1++) { objExcel.Agrupar(ind1, 3, ind1, 13); }
            for (int ind1 = 69; ind1 <= 70; ind1++) { objExcel.Agrupar(ind1, 3, ind1, 13); }


            objExcel.Alinhar(66, 3, "Center");
            for (int ind1 = 67; ind1 <= 68; ind1++) { objExcel.Alinhar(ind1, 3, "Left"); }
            objExcel.Alinhar(70, 3, "Center");

            objExcel.Pagina(66, 3, npag, "CONTAGEM CLASSIFICATÓRIA DE VEÍCULOS POR CATEGORIA");
            objExcel.Pagina(67, 4, npag, "RODOVIA: " + objPontV.Rod);
            objExcel.Pagina(67, 7, npag, "  KM: " + objPontV.km );
            objExcel.Pagina(67, 9, npag, "MUNICÍPIO: " + objPontV.municipio.ToUpper());

            objExcel.Pagina(68, 4, npag, "PONTO A: " + objPontV.munA.ToUpper());
            objExcel.Pagina(68, 9, npag, "PONTO B: " + objPontV.munB.ToUpper());


            objExcel.Pagina(69, 3, npag, "                                          PERÍODO: " + objPontV.periodo);
            objExcel.Pagina(70, 3, npag, "SENTIDO AB");




            for (int ind1 = 118; ind1 <= 118; ind1++) { objExcel.Agrupar(ind1, 3, ind1, 13); }
            for (int ind1 = 121; ind1 <= 122; ind1++) { objExcel.Agrupar(ind1, 3, ind1, 13); }
            objExcel.Alinhar(118, 3, "Center");
            objExcel.Pagina(118, 3, npag, "CONTAGEM CLASSIFICATÓRIA DE VEÍCULOS POR CATEGORIA");

            for (int ind1 = 119; ind1 <= 121; ind1++) { objExcel.Alinhar(ind1, 3, "Left"); }
            objExcel.Alinhar(122, 3, "Center");

            objExcel.Pagina(119, 4, npag, "RODOVIA: " + objPontV.Rod );
            objExcel.Pagina(119, 7, npag, "  KM: " + objPontV.km );
            objExcel.Pagina(119, 9, npag, "MUNICÍPIO: " + objPontV.municipio.ToUpper());

            objExcel.Pagina(120, 4, npag, "PONTO A: " + objPontV.munA.ToUpper());
            objExcel.Pagina(120, 9, npag, "PONTO B: " + objPontV.munB.ToUpper());

            objExcel.Pagina(121, 3, npag, "                                          PERÍODO: " + objPontV.periodo);
            objExcel.Pagina(122, 3, npag, "SENTIDO BA");




            for (int ind1 = 171; ind1 <= 171; ind1++) { objExcel.Agrupar(ind1, 3, ind1, 13); }            
            objExcel.Alinhar(171, 3, "Center");
            objExcel.Pagina(171, 3, npag, "CONTAGEM CLASSIFICATÓRIA DE VEÍCULOS POR CATEGORIA");

            for (int ind1 = 174; ind1 <= 175; ind1++) { objExcel.Agrupar(ind1, 3, ind1, 13); }

            for (int ind1 = 172; ind1 <= 174; ind1++) { objExcel.Alinhar(ind1, 3, "Left"); }
            objExcel.Alinhar(175, 3, "Center");

            objExcel.Pagina(172, 4, npag, "RODOVIA: " + objPontV.Rod );
            objExcel.Pagina(172, 7, npag, "  KM: " + objPontV.km );
            objExcel.Pagina(172, 9, npag, "MUNICÍPIO: " + objPontV.municipio.ToUpper());

            objExcel.Pagina(173, 4, npag, "PONTO A: " + objPontV.munA.ToUpper() );
            objExcel.Pagina(173, 9, npag, "PONTO B: " + objPontV.munB.ToUpper());


            objExcel.Pagina(174, 3, npag, "                                          PERÍODO: " + objPontV.periodo);
            objExcel.Pagina(175, 3, npag, "AMBOS OS SENTIDOS");

            objExcel.Pagina(39, 3, 2, "12 - Classificação por Categoria - Gráficos ........................................." + nsheet.ToString(), 12, "Black");  // indice

            if ( (objPontV.sAB==1) && (objPontV.sBA==1) )
              {
                objExcel.Pagina(63, 15, npag, objPontV.Rod + " km " + objPontV.km, 11, "Black"); // rpd + km
                objExcel.Pagina(114, 16, npag, nsheet.ToString(), 10, "Black");   //numero da pagina
                nsheet++;

                objExcel.Pagina(115, 15, npag, objPontV.Rod + " km " + objPontV.km, 11, "Black"); // rpd + km
                objExcel.Pagina(167, 16, npag, nsheet.ToString(), 10, "Black");   //numero da pagina
                nsheet++;

                objExcel.Pagina(168, 15, npag, objPontV.Rod + " km " + objPontV.km, 11, "Black"); // rpd + km
                objExcel.Pagina(220, 16, npag, nsheet.ToString(), 10, "Black");   //numero da pagina
                // essa foi a ultima página
            }


            if ((objPontV.sAB == 1) && (objPontV.sBA == 0))
            {
                objExcel.Pagina(63, 15, npag, objPontV.Rod + " km " + objPontV.km, 11, "Black"); // rpd + km
                objExcel.Pagina(114, 16, npag, nsheet.ToString(), 10, "Black");   //numero da pagina
                nsheet++;                
                // essa foi a ultima página
            }

            if ((objPontV.sAB == 0) && (objPontV.sBA == 1))
            {
                objExcel.Pagina(115, 15, npag, objPontV.Rod + " km " + objPontV.km, 11, "Black"); // rpd + km
                objExcel.Pagina(167, 16, npag, nsheet.ToString(), 10, "Black");   //numero da pagina
                nsheet++;
                // essa foi a ultima página
            }



        }


        // *************************************************************
        // ***********************************************************






        // ************************************************************************
        private void pistaDuplaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Lane 1 é sentido AB     // Lane 2 é sentido BA

            
            string caminho = "";
            string nomearq = "";

            Boolean tem2Sent = false;

            CarregaPontoClassVez(objPontV); // carrega o ponto da clasificação a ser processada da Vez
            caminho = CarregaDiretorio(ref caminho);

            nomearq = objPontV.Rod + "_KM" + objPontV.km + ".xlsx";
            //if (objPontV.km.Length < 3)
            //{ nomearq = objPontV.Rod + "_KM" + "0" + objPontV.km + ".xlsx"; }
            //else
            //{ nomearq = objPontV.Rod + "_KM" + objPontV.km + ".xlsx"; }
            


            //  objExcel.AbreExcel(@"C:\DER-RJ\FiscalizaçãoEletronica\2018\Estudos2018\" + nomearq, 1);
            objExcel.AbreExcel(@caminho + "\\" + nomearq, 1);

            npag = 1;
            Capa(objPontV.Rod, objPontV.km);    npag++; npag++;

            objExcel.Pagina(3, 9, 2, objPontV.Rod + " km" + objPontV.km, 11, "Black"); // rpd + km

            Apres(objPontV.periodo);    npag++;
            //npag = 3; 
            Mapa(objPontV.Rod, objPontV.km);
            npag++; Croqui(objPontV.Rod, objPontV.km, false);

            if (objPontV.qtdcroquis == 2) { npag++; Croqui(objPontV.Rod, objPontV.km, true); } // Se forem 2 croquis no Excel ao invés de 1

            //////npag = 8; // primeira planilha de contagens


            npag++; // Instalação
            objExcel.Pagina(40, 10, npag, (npag - 2).ToString(), 10, "Black");
            //   objExcel.Pagina(9, 3, 2, "2 - Mapa ..................................................................................................2", 12, "Black");
                objExcel.Pagina(15, 3, 2, "4 - Instalação .........................................................................................4", 12, "Black"); 
            objExcel.Pagina(79, 10, npag, (npag - 2+1).ToString(), 10, "Black");
            objExcel.Pagina(118, 10, npag, (npag - 2 + 2).ToString(), 10, "Black");
            nsheet = npag;

            npag++;  // Operação
            nsheet++;
            objExcel.Pagina(18, 3, 2, "5 - Operação ........................................................................................." + nsheet.ToString(), 12, "Black");
            if ( (objPontV.sAB==1) && (objPontV.sBA == 1) )
            {
                objExcel.Pagina(40, 10, npag, (nsheet).ToString(), 10, "Black"); nsheet++;
                objExcel.Pagina(79, 10, npag, (nsheet).ToString(), 10, "Black"); nsheet++;
                tem2Sent = true;

            }

            if ((objPontV.sAB == 1) && (objPontV.sBA != 1))
            { objExcel.Pagina(40, 10, npag, (nsheet).ToString(), 10, "Black"); nsheet++;   tem2Sent = false; }

            if ((objPontV.sAB != 1) && (objPontV.sBA == 1))
            { objExcel.Pagina(40, 10, npag, (nsheet).ToString(), 10, "Black"); nsheet++;   tem2Sent = false; }

            npag++;

            Preenche7DiasVelocidade(objPontV, tem2Sent); npag++;

            //npag = 15;
            PreencheResumoVelocidade(objPontV, objPontV.periodo); npag++;
            PreencheResumoVelocidadeGraph(objPontV, objPontV.periodo, tem2Sent); npag++; // gráfico

            if (objPontV.sAB == 1)
            { 
             objExcel.SelecionaPagina(npag);
                objExcel.Pagina(4, 5, npag, "               Cálculo do 85 Percentil - " + objPontV.munA + "/" + objPontV.munB,  14, "Black"); 
             objExcel.Pagina(63, 15, npag, (nsheet).ToString(), 10, "Black");
             objExcel.Pagina(30, 3, 2, "9 - 85 percentil ....................................................................................." + nsheet.ToString(), 12, "Black");  // indice
             nsheet++;
            }  // 85 percentil AB
            else
            { objExcel.Pagina(30, 3, 2, "9 - 85 percentil ....................................................................................." + nsheet.ToString(), 12, "Black");  // indice
            }


            npag++;
            if (objPontV.sBA == 1)
            { objExcel.Pagina(4, 5, npag, "               Cálculo do 85 Percentil - " + objPontV.munB + "/" + objPontV.munA, 14, "Black");
             objExcel.SelecionaPagina(npag); objExcel.Pagina(63, 15, npag, (nsheet).ToString(), 10, "Black"); nsheet++; 
            } // 85 percentil BA
            npag++;



            //npag = 19; nsheet = 38;  //// primeira planilha de contagens por comprimento
            Preenche7DiasComprimento(objPontV, tem2Sent); npag++;

            //npag = 26;
            PreencheResumoComprimento(objPontV, objPontV.periodo); npag++;
            PreencheResumoComprimentoGraph(objPontV, objPontV.periodo);    // gráfico
        }

        // ************************************************************************













        // ************************************************************************
        private void motoParaPasseioToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // moto para passeio, jogando 70 % das motos para passeio e reduzindo as motos a 30% do seu valor inicial


            string aRod = "";
            string oKm = "";

              CarregaPontoClassVez(objPontV); // carrega o ponto da clasificação a ser processada da Vez
            DeletaTabela("tbvelcomphoradiamodelo");
            DeletaTabela("tbvelcomphoradiamodeloax");



            aRod = cUt.TiraTraco(objPontV.Rod);      oKm = objPontV.km; 
            if (oKm.Length<3) { oKm = "km0" + oKm; } else { oKm = "km" + oKm; }
            CopiaUmaTabelaParaOutra("tbvelcomphoradia" + aRod + oKm, "tbvelcomphoradiamodeloax");

            CopiaUmaTabelaParaOutra("tbvelcomphoradia" + aRod + oKm, "tbvelcomphoradiamodelo");

            DeletaTabela("tbvelcomphoradia" + aRod + oKm);

            AlteraDadosTabela("tbvelcomphoradiamodelo", "tbvelcomphoradia" + aRod + oKm);

            DeletaTabela("tbvelhoradia" + aRod + oKm);
            GravaTabelaVel("tbvelcomphoradia" + aRod + oKm, "tbvelhoradia" + aRod + oKm);

            DeletaTabela("tbcomphoradia" + aRod + oKm);
            GravaTabelaComp("tbvelcomphoradia" + aRod + oKm, "tbcomphoradia" + aRod + oKm);




        }

        // *************************************************************
        // ***********************************************************





         // *******************************************************************
        public void DeletaTabela(string atab)
        {
            ConectaBancoClassfic();

            comcfc1.CommandText = "delete from " + atab;
            comcfc1.ExecuteNonQuery();

            DesconectaBancoClassfic();

        }
        // *******************************************************************





        public void CopiaUmaTabelaParaOutra(string atabO, string atabD)

        {
            // atabO - tabela de origem
            // atabD - tabela de destino

            int ahora = 0;
            int ocomp = 0;

            C_VelComp objVC = new C_VelComp();

            ConectaBancoClassfic();
            ConectaBancoClassfic2();


            comcfc1.CommandText = "select * from " + atabO;
            readercfc1 = comcfc1.ExecuteReader();

            while (readercfc1.Read())
            {
                objVC.aRod = readercfc1["Rod"].ToString();
                objVC.DI = readercfc1["DI"].ToString();
                objVC.MO = readercfc1["MO"].ToString();
                objVC.YE = readercfc1["YE"].ToString();
                objVC.HO = readercfc1["HO"].ToString(); ahora = Int32.Parse(objVC.HO);
                objVC.Sent = readercfc1["Sent"].ToString();
                objVC.km = readercfc1["km"].ToString();
                objVC.comp = readercfc1["comp"].ToString(); ocomp = Int32.Parse(objVC.comp);


                //( 24, 2, 3, 13) As Integer  ' ''24 horas e 2 lanes, 3 Comprimentos, 14 Velocidades
                objVC.dta[ahora, 1, ocomp, 1] = Int32.Parse(readercfc1["Vel19"].ToString());
                objVC.dta[ahora, 1, ocomp, 2] = Int32.Parse(readercfc1["Vel29"].ToString());
                objVC.dta[ahora, 1, ocomp, 3] = Int32.Parse(readercfc1["Vel39"].ToString());
                objVC.dta[ahora, 1, ocomp, 4] = Int32.Parse(readercfc1["Vel49"].ToString());
                objVC.dta[ahora, 1, ocomp, 5] = Int32.Parse(readercfc1["Vel59"].ToString());
                objVC.dta[ahora, 1, ocomp, 6] = Int32.Parse(readercfc1["Vel69"].ToString());
                objVC.dta[ahora, 1, ocomp, 7] = Int32.Parse(readercfc1["Vel79"].ToString());
                objVC.dta[ahora, 1, ocomp, 8] = Int32.Parse(readercfc1["Vel89"].ToString());
                objVC.dta[ahora, 1, ocomp, 9] = Int32.Parse(readercfc1["Vel99"].ToString());
                objVC.dta[ahora, 1, ocomp, 10] = Int32.Parse(readercfc1["Vel109"].ToString());
                objVC.dta[ahora, 1, ocomp, 11] = Int32.Parse(readercfc1["Vel119"].ToString());
                objVC.dta[ahora, 1, ocomp, 12] = Int32.Parse(readercfc1["Vel129"].ToString());
                objVC.dta[ahora, 1, ocomp, 13] = Int32.Parse(readercfc1["Vel139"].ToString());
                objVC.dta[ahora, 1, ocomp, 14] = Int32.Parse(readercfc1["Vel199"].ToString());


                // grava os registros


                
                   
                comcfc2.CommandText = "insert into " + atabD + " (Rod, DI, MO, YE, HO, Sent, km, comp, Vel19, Vel29, Vel39,";
                comcfc2.CommandText +=  " Vel49, Vel59, Vel69, Vel79, Vel89, Vel99,";
                comcfc2.CommandText +=  " Vel109, Vel119, Vel129, Vel139, Vel199) ";
                comcfc2.CommandText +=  "Values (" + "'" + objVC.aRod + "'" + "," + "'" + objVC.DI + "'" + "," + "'" + objVC.MO + "'" + "," + "'" + objVC.YE + "'" + "," + "'" + objVC.HO + "'" + ",";
                comcfc2.CommandText +=  "'" + objVC.Sent + "'" + "," + "'" + objVC.km + "'" + "," + ocomp + "," + objVC.dta[ahora, 1, ocomp, 1] + "," + objVC.dta[ahora, 1, ocomp, 2] + "," + objVC.dta[ahora, 1, ocomp, 3] + "," + objVC.dta[ahora, 1, ocomp, 4] + ",";
                comcfc2.CommandText +=  objVC.dta[ahora, 1, ocomp, 5] + "," + objVC.dta[ahora, 1, ocomp, 6] + "," + objVC.dta[ahora, 1, ocomp, 7] + "," + objVC.dta[ahora, 1, ocomp, 8] + "," + objVC.dta[ahora, 1, ocomp, 9] + ",";
                comcfc2.CommandText +=  objVC.dta[ahora, 1, ocomp, 10] + "," + objVC.dta[ahora, 1, ocomp, 11] + "," + objVC.dta[ahora, 1, ocomp, 12] + "," + objVC.dta[ahora, 1, ocomp, 13] + "," + objVC.dta[ahora, 1, ocomp, 14] + ")";
                comcfc2.ExecuteNonQuery();
                   
               


            }


            DesconectaBancoClassfic();
            DesconectaBancoClassfic2();
        }
        // *******************************************************************









        //***************************************************
        public void AlteraDadosTabelaComerciaisMoto(string atabO, string atabD)
        {
            // atabO - tabela de origem
            // atabD - tabela de destino

            int ahora = 0;
            int ocomp = 0;

            C_VelComp objVC = new C_VelComp();

            ConectaBancoClassfic();
            ConectaBancoClassfic2();


            comcfc1.CommandText = "select * from " + atabO;
            readercfc1 = comcfc1.ExecuteReader();

            while (readercfc1.Read())
            {
                objVC.aRod = readercfc1["Rod"].ToString();
                objVC.DI = readercfc1["DI"].ToString();
                objVC.MO = readercfc1["MO"].ToString();
                objVC.YE = readercfc1["YE"].ToString();
                objVC.HO = readercfc1["HO"].ToString(); ahora = Int32.Parse(objVC.HO);
                objVC.Sent = readercfc1["Sent"].ToString();
                objVC.km = readercfc1["km"].ToString();
                objVC.comp = readercfc1["comp"].ToString(); ocomp = Int32.Parse(objVC.comp);


                //( 24, 2, 3, 13) As Integer  ' ''24 horas e 2 lanes, 3 Comprimentos, 14 Velocidades
                objVC.dta[ahora, 1, ocomp, 1] = Int32.Parse(readercfc1["Vel19"].ToString());
                objVC.dta[ahora, 1, ocomp, 2] = Int32.Parse(readercfc1["Vel29"].ToString());
                objVC.dta[ahora, 1, ocomp, 3] = Int32.Parse(readercfc1["Vel39"].ToString());
                objVC.dta[ahora, 1, ocomp, 4] = Int32.Parse(readercfc1["Vel49"].ToString());
                objVC.dta[ahora, 1, ocomp, 5] = Int32.Parse(readercfc1["Vel59"].ToString());
                objVC.dta[ahora, 1, ocomp, 6] = Int32.Parse(readercfc1["Vel69"].ToString());
                objVC.dta[ahora, 1, ocomp, 7] = Int32.Parse(readercfc1["Vel79"].ToString());
                objVC.dta[ahora, 1, ocomp, 8] = Int32.Parse(readercfc1["Vel89"].ToString());
                objVC.dta[ahora, 1, ocomp, 9] = Int32.Parse(readercfc1["Vel99"].ToString());
                objVC.dta[ahora, 1, ocomp, 10] = Int32.Parse(readercfc1["Vel109"].ToString());
                objVC.dta[ahora, 1, ocomp, 11] = Int32.Parse(readercfc1["Vel119"].ToString());
                objVC.dta[ahora, 1, ocomp, 12] = Int32.Parse(readercfc1["Vel129"].ToString());
                objVC.dta[ahora, 1, ocomp, 13] = Int32.Parse(readercfc1["Vel139"].ToString());
                objVC.dta[ahora, 1, ocomp, 14] = Int32.Parse(readercfc1["Vel199"].ToString());

                if ((objVC.Sent == "AB") && (ocomp == 3))
                {
                    for (int indvel = 1; indvel <= 14; indvel++)
                    {
                        //if (objVC.dta[ahora, 1, 1, indvel] >= 6)
                        if (objVC.dta[ahora, 1, 1, indvel] >= 0)
                        {
                            //objVC.dta[ahora, 1, 2, indvel] += (Int32)Math.Truncate(objVC.dta[ahora, 1, 1, indvel] * 0.7);
                            //objVC.dta[ahora, 1, 1, indvel] = (Int32)Math.Truncate(objVC.dta[ahora, 1, 1, indvel] * 0.3);

                            objVC.dta[ahora, 1, 1, indvel] += (Int32)Math.Truncate(objVC.dta[ahora, 1, 3, indvel] * 0.42);
                            objVC.dta[ahora, 1, 3, indvel] = (Int32)Math.Truncate(objVC.dta[ahora, 1, 3, indvel] * 0.58);
                        }
                    }
                }



                if ((objVC.Sent == "BA") && (ocomp == 3))
                {
                    for (int indvel = 1; indvel <= 14; indvel++)
                    {
                        //if (objVC.dta[ahora, 1, 1, indvel] >= 6)
                        if (objVC.dta[ahora, 1, 1, indvel] >= 0)
                        {
                            //objVC.dta[ahora, 1, 2, indvel] += (Int32)Math.Truncate(objVC.dta[ahora, 1, 1, indvel] * 0.7);
                            //objVC.dta[ahora, 1, 1, indvel] = (Int32)Math.Truncate(objVC.dta[ahora, 1, 1, indvel] * 0.3);

                            objVC.dta[ahora, 1, 1, indvel] += (Int32)Math.Truncate(objVC.dta[ahora, 1, 3, indvel] * 0.40);
                            objVC.dta[ahora, 1, 3, indvel] = (Int32)Math.Truncate(objVC.dta[ahora, 1, 3, indvel] * 0.60);
                        }
                    }
                }
                // grava os registros


                if (ocomp == 3)
                {
                    for (int ind1 = 1; ind1 <= 3; ind1++)
                    {
                        comcfc2.CommandText = "insert into " + atabD + " (Rod, DI, MO, YE, HO, Sent, km, comp, Vel19, Vel29, Vel39,";
                        comcfc2.CommandText += " Vel49, Vel59, Vel69, Vel79, Vel89, Vel99,";
                        comcfc2.CommandText += " Vel109, Vel119, Vel129, Vel139, Vel199) ";
                        comcfc2.CommandText += "Values (" + "'" + objVC.aRod + "'" + "," + "'" + objVC.DI + "'" + "," + "'" + objVC.MO + "'" + "," + "'" + objVC.YE + "'" + "," + "'" + objVC.HO + "'" + ",";
                        comcfc2.CommandText += "'" + objVC.Sent + "'" + "," + "'" + objVC.km + "'" + "," + ind1 + "," + objVC.dta[ahora, 1, ind1, 1] + "," + objVC.dta[ahora, 1, ind1, 2] + "," + objVC.dta[ahora, 1, ind1, 3] + "," + objVC.dta[ahora, 1, ind1, 4] + ",";
                        comcfc2.CommandText += objVC.dta[ahora, 1, ind1, 5] + "," + objVC.dta[ahora, 1, ind1, 6] + "," + objVC.dta[ahora, 1, ind1, 7] + "," + objVC.dta[ahora, 1, ind1, 8] + "," + objVC.dta[ahora, 1, ind1, 9] + ",";
                        comcfc2.CommandText += objVC.dta[ahora, 1, ind1, 10] + "," + objVC.dta[ahora, 1, ind1, 11] + "," + objVC.dta[ahora, 1, ind1, 12] + "," + objVC.dta[ahora, 1, ind1, 13] + "," + objVC.dta[ahora, 1, ind1, 14] + ")";
                        comcfc2.ExecuteNonQuery();
                    }

                }


            }


            DesconectaBancoClassfic();
            DesconectaBancoClassfic2();

        }











        // *******************************************************************
        public void AlteraDadosTabela(string atabO, string atabD)
        {
            // atabO - tabela de origem
            // atabD - tabela de destino

            int ahora = 0;
            int ocomp = 0;

            C_VelComp objVC = new C_VelComp();

            ConectaBancoClassfic();
            ConectaBancoClassfic2();


            comcfc1.CommandText = "select * from " + atabO;
            readercfc1 = comcfc1.ExecuteReader();


            // AB
            while (readercfc1.Read())
            {
                objVC.aRod = readercfc1["Rod"].ToString();
                objVC.DI = readercfc1["DI"].ToString();
                objVC.MO = readercfc1["MO"].ToString();
                objVC.YE = readercfc1["YE"].ToString();
                objVC.HO = readercfc1["HO"].ToString(); ahora = Int32.Parse(objVC.HO);
                objVC.Sent = readercfc1["Sent"].ToString();
                objVC.km = readercfc1["km"].ToString();
                objVC.comp = readercfc1["comp"].ToString(); ocomp = Int32.Parse(objVC.comp);


                //( 24, 2, 3, 13) As Integer  ' ''24 horas e 2 lanes, 3 Comprimentos, 14 Velocidades
                objVC.dta[ahora, 1, ocomp, 1] = Int32.Parse(readercfc1["Vel19"].ToString());
                objVC.dta[ahora, 1, ocomp, 2] = Int32.Parse(readercfc1["Vel29"].ToString());
                objVC.dta[ahora, 1, ocomp, 3] = Int32.Parse(readercfc1["Vel39"].ToString());
                objVC.dta[ahora, 1, ocomp, 4] = Int32.Parse(readercfc1["Vel49"].ToString());
                objVC.dta[ahora, 1, ocomp, 5] = Int32.Parse(readercfc1["Vel59"].ToString());
                objVC.dta[ahora, 1, ocomp, 6] = Int32.Parse(readercfc1["Vel69"].ToString());
                objVC.dta[ahora, 1, ocomp, 7] = Int32.Parse(readercfc1["Vel79"].ToString());
                objVC.dta[ahora, 1, ocomp, 8] = Int32.Parse(readercfc1["Vel89"].ToString());
                objVC.dta[ahora, 1, ocomp, 9] = Int32.Parse(readercfc1["Vel99"].ToString());
                objVC.dta[ahora, 1, ocomp, 10] = Int32.Parse(readercfc1["Vel109"].ToString());
                objVC.dta[ahora, 1, ocomp, 11] = Int32.Parse(readercfc1["Vel119"].ToString());
                objVC.dta[ahora, 1, ocomp, 12] = Int32.Parse(readercfc1["Vel129"].ToString());
                objVC.dta[ahora, 1, ocomp, 13] = Int32.Parse(readercfc1["Vel139"].ToString());
                objVC.dta[ahora, 1, ocomp, 14] = Int32.Parse(readercfc1["Vel199"].ToString());

                if ( (objVC.Sent=="AB") && (ocomp==3) )
                {
                    for (int indvel = 1; indvel <= 14; indvel++)
                    {
                        //if (objVC.dta[ahora, 1, 1, indvel] >= 6)
                            if (objVC.dta[ahora, 1, 1, indvel] >= 0)
                            {
                            //objVC.dta[ahora, 1, 2, indvel] += (Int32)Math.Truncate(objVC.dta[ahora, 1, 1, indvel] * 0.52);
                            //objVC.dta[ahora, 1, 1, indvel] = (Int32)Math.Truncate(objVC.dta[ahora, 1, 1, indvel] * 0.48);
                            //  public int[,,,] dta = new int[25, 3, 4, 15];
                            //objVC.dta[ahora, 1, 1, indvel] += (Int32)Math.Truncate(objVC.dta[ahora, 1, 2, indvel] * 0.08);
                            //objVC.dta[ahora, 1, 2, indvel] = (Int32)Math.Truncate(objVC.dta[ahora, 1, 2, indvel] * 0.92);

                            //objVC.dta[ahora, 1, 1, indvel] = (Int32)Math.Truncate(objVC.dta[ahora, 1, 1, indvel] * 0.62);
                            //objVC.dta[ahora, 1, 2, indvel] += (Int32)Math.Truncate(objVC.dta[ahora, 1, 1, indvel] * 0.38);

                            objVC.dta[ahora, 1, 2, indvel] += (Int32)Math.Truncate(objVC.dta[ahora, 1, 1, indvel] * 0.17);
                            objVC.dta[ahora, 1, 1, indvel] = (Int32)Math.Truncate(objVC.dta[ahora, 1, 1, indvel] * 0.53);

                        }
                    }
                }

                // grava os registros
                
                
                if ((ocomp == 3) && (objVC.Sent == "AB"))
                {
                    for (int ind1 = 1; ind1 <= 3; ind1++)
                    {
                        comcfc2.CommandText = "insert into " + atabD + " (Rod, DI, MO, YE, HO, Sent, km, comp, Vel19, Vel29, Vel39,";
                        comcfc2.CommandText += " Vel49, Vel59, Vel69, Vel79, Vel89, Vel99,";
                        comcfc2.CommandText += " Vel109, Vel119, Vel129, Vel139, Vel199) ";
                        comcfc2.CommandText += "Values (" + "'" + objVC.aRod + "'" + "," + "'" + objVC.DI + "'" + "," + "'" + objVC.MO + "'" + "," + "'" + objVC.YE + "'" + "," + "'" + objVC.HO + "'" + ",";
                        comcfc2.CommandText += "'" + objVC.Sent + "'" + "," + "'" + objVC.km + "'" + "," + ind1 + "," + objVC.dta[ahora, 1, ind1, 1] + "," + objVC.dta[ahora, 1, ind1, 2] + "," + objVC.dta[ahora, 1, ind1, 3] + "," + objVC.dta[ahora, 1, ind1, 4] + ",";
                        comcfc2.CommandText += objVC.dta[ahora, 1, ind1, 5] + "," + objVC.dta[ahora, 1, ind1, 6] + "," + objVC.dta[ahora, 1, ind1, 7] + "," + objVC.dta[ahora, 1, ind1, 8] + "," + objVC.dta[ahora, 1, ind1, 9] + ",";
                        comcfc2.CommandText += objVC.dta[ahora, 1, ind1, 10] + "," + objVC.dta[ahora, 1, ind1, 11] + "," + objVC.dta[ahora, 1, ind1, 12] + "," + objVC.dta[ahora, 1, ind1, 13] + "," + objVC.dta[ahora, 1, ind1, 14] + ")";
                        comcfc2.ExecuteNonQuery();
                    }
                    
                }


            }


                DesconectaBancoClassfic();
                DesconectaBancoClassfic2();





            // SENTIDO BA

            ConectaBancoClassfic();
            ConectaBancoClassfic2();


            comcfc1.CommandText = "select * from " + atabO;
            readercfc1 = comcfc1.ExecuteReader();

            while (readercfc1.Read())
            {
                objVC.aRod = readercfc1["Rod"].ToString();
                objVC.DI = readercfc1["DI"].ToString();
                objVC.MO = readercfc1["MO"].ToString();
                objVC.YE = readercfc1["YE"].ToString();
                objVC.HO = readercfc1["HO"].ToString(); ahora = Int32.Parse(objVC.HO);
                objVC.Sent = readercfc1["Sent"].ToString();
                objVC.km = readercfc1["km"].ToString();
                objVC.comp = readercfc1["comp"].ToString(); ocomp = Int32.Parse(objVC.comp);


                //( 24, 2, 3, 13) As Integer  ' ''24 horas e 2 lanes, 3 Comprimentos, 14 Velocidades
                objVC.dta[ahora, 1, ocomp, 1] = Int32.Parse(readercfc1["Vel19"].ToString());
                objVC.dta[ahora, 1, ocomp, 2] = Int32.Parse(readercfc1["Vel29"].ToString());
                objVC.dta[ahora, 1, ocomp, 3] = Int32.Parse(readercfc1["Vel39"].ToString());
                objVC.dta[ahora, 1, ocomp, 4] = Int32.Parse(readercfc1["Vel49"].ToString());
                objVC.dta[ahora, 1, ocomp, 5] = Int32.Parse(readercfc1["Vel59"].ToString());
                objVC.dta[ahora, 1, ocomp, 6] = Int32.Parse(readercfc1["Vel69"].ToString());
                objVC.dta[ahora, 1, ocomp, 7] = Int32.Parse(readercfc1["Vel79"].ToString());
                objVC.dta[ahora, 1, ocomp, 8] = Int32.Parse(readercfc1["Vel89"].ToString());
                objVC.dta[ahora, 1, ocomp, 9] = Int32.Parse(readercfc1["Vel99"].ToString());
                objVC.dta[ahora, 1, ocomp, 10] = Int32.Parse(readercfc1["Vel109"].ToString());
                objVC.dta[ahora, 1, ocomp, 11] = Int32.Parse(readercfc1["Vel119"].ToString());
                objVC.dta[ahora, 1, ocomp, 12] = Int32.Parse(readercfc1["Vel129"].ToString());
                objVC.dta[ahora, 1, ocomp, 13] = Int32.Parse(readercfc1["Vel139"].ToString());
                objVC.dta[ahora, 1, ocomp, 14] = Int32.Parse(readercfc1["Vel199"].ToString());

                if ((objVC.Sent == "BA") && (ocomp == 3))
                {
                    for (int indvel = 1; indvel <= 14; indvel++)
                    {
                        //if (objVC.dta[ahora, 1, 1, indvel] >= 6)
                        if (objVC.dta[ahora, 1, 1, indvel] >= 0)
                        {
                            //objVC.dta[ahora, 1, 2, indvel] += (Int32)Math.Truncate(objVC.dta[ahora, 1, 1, indvel] * 0.17);
                            //objVC.dta[ahora, 1, 1, indvel] = (Int32)Math.Truncate(objVC.dta[ahora, 1, 1, indvel] * 0.83);
                            //  public int[,,,] dta = new int[25, 3, 4, 15];
                            //objVC.dta[ahora, 1, 1, indvel] += (Int32)Math.Truncate(objVC.dta[ahora, 1, 2, indvel] * 0.08);
                            //objVC.dta[ahora, 1, 2, indvel] = (Int32)Math.Truncate(objVC.dta[ahora, 1, 2, indvel] * 0.92);
                        }
                    }
                }

                // grava os registros


                if ((ocomp == 3) && (objVC.Sent == "BA"))
                {
                    for (int ind1 = 1; ind1 <= 3; ind1++)
                    {
                        comcfc2.CommandText = "insert into " + atabD + " (Rod, DI, MO, YE, HO, Sent, km, comp, Vel19, Vel29, Vel39,";
                        comcfc2.CommandText += " Vel49, Vel59, Vel69, Vel79, Vel89, Vel99,";
                        comcfc2.CommandText += " Vel109, Vel119, Vel129, Vel139, Vel199) ";
                        comcfc2.CommandText += "Values (" + "'" + objVC.aRod + "'" + "," + "'" + objVC.DI + "'" + "," + "'" + objVC.MO + "'" + "," + "'" + objVC.YE + "'" + "," + "'" + objVC.HO + "'" + ",";
                        comcfc2.CommandText += "'" + objVC.Sent + "'" + "," + "'" + objVC.km + "'" + "," + ind1 + "," + objVC.dta[ahora, 1, ind1, 1] + "," + objVC.dta[ahora, 1, ind1, 2] + "," + objVC.dta[ahora, 1, ind1, 3] + "," + objVC.dta[ahora, 1, ind1, 4] + ",";
                        comcfc2.CommandText += objVC.dta[ahora, 1, ind1, 5] + "," + objVC.dta[ahora, 1, ind1, 6] + "," + objVC.dta[ahora, 1, ind1, 7] + "," + objVC.dta[ahora, 1, ind1, 8] + "," + objVC.dta[ahora, 1, ind1, 9] + ",";
                        comcfc2.CommandText += objVC.dta[ahora, 1, ind1, 10] + "," + objVC.dta[ahora, 1, ind1, 11] + "," + objVC.dta[ahora, 1, ind1, 12] + "," + objVC.dta[ahora, 1, ind1, 13] + "," + objVC.dta[ahora, 1, ind1, 14] + ")";
                        comcfc2.ExecuteNonQuery();
                    }

                }


            }


            DesconectaBancoClassfic();
            DesconectaBancoClassfic2();


        }
        // *******************************************************************




        public void GravaTabelaVel( string atabO, string atabD)
        {

            int ahora = 0;
            int ocomp = 0;

            C_VelComp objVC = new C_VelComp();

            ConectaBancoClassfic();
            ConectaBancoClassfic2();

            comcfc1.CommandText = "select * from " + atabO;
            readercfc1 = comcfc1.ExecuteReader();

            while (readercfc1.Read())
            {
                objVC.aRod = readercfc1["Rod"].ToString();
                objVC.DI = readercfc1["DI"].ToString();
                objVC.MO = readercfc1["MO"].ToString();
                objVC.YE = readercfc1["YE"].ToString();
                objVC.HO = readercfc1["HO"].ToString(); ahora = Int32.Parse(objVC.HO);
                objVC.Sent = readercfc1["Sent"].ToString();
                objVC.km = readercfc1["km"].ToString();
                objVC.comp = readercfc1["comp"].ToString(); ocomp = Int32.Parse(objVC.comp);


                //( 24, 2, 3, 13) As Integer  ' ''24 horas e 2 lanes, 3 Comprimentos, 14 Velocidades
                objVC.dta[ahora, 1, ocomp, 1] = Int32.Parse(readercfc1["Vel19"].ToString());
                objVC.dta[ahora, 1, ocomp, 2] = Int32.Parse(readercfc1["Vel29"].ToString());
                objVC.dta[ahora, 1, ocomp, 3] = Int32.Parse(readercfc1["Vel39"].ToString());
                objVC.dta[ahora, 1, ocomp, 4] = Int32.Parse(readercfc1["Vel49"].ToString());
                objVC.dta[ahora, 1, ocomp, 5] = Int32.Parse(readercfc1["Vel59"].ToString());
                objVC.dta[ahora, 1, ocomp, 6] = Int32.Parse(readercfc1["Vel69"].ToString());
                objVC.dta[ahora, 1, ocomp, 7] = Int32.Parse(readercfc1["Vel79"].ToString());
                objVC.dta[ahora, 1, ocomp, 8] = Int32.Parse(readercfc1["Vel89"].ToString());
                objVC.dta[ahora, 1, ocomp, 9] = Int32.Parse(readercfc1["Vel99"].ToString());
                objVC.dta[ahora, 1, ocomp, 10] = Int32.Parse(readercfc1["Vel109"].ToString());
                objVC.dta[ahora, 1, ocomp, 11] = Int32.Parse(readercfc1["Vel119"].ToString());
                objVC.dta[ahora, 1, ocomp, 12] = Int32.Parse(readercfc1["Vel129"].ToString());
                objVC.dta[ahora, 1, ocomp, 13] = Int32.Parse(readercfc1["Vel139"].ToString());
                objVC.dta[ahora, 1, ocomp, 14] = Int32.Parse(readercfc1["Vel199"].ToString());



                if (ocomp == 3)
                {

                    for (int ind1 = 1; ind1 <= 14; ind1++)
                    {  objVC.dta[ahora, 1, 1, ind1] += objVC.dta[ahora, 1, 2, ind1] + objVC.dta[ahora, 1, 3, ind1];  }

                                           
                        comcfc2.CommandText = "insert into " + atabD + " (Rod, DI, MO, YE, HO, Sent, km, Vel19, Vel29, Vel39,";
                        comcfc2.CommandText += " Vel49, Vel59, Vel69, Vel79, Vel89, Vel99,";
                        comcfc2.CommandText += " Vel109, Vel119, Vel129, Vel139, Vel199) ";
                        comcfc2.CommandText += "Values (" + "'" + objVC.aRod + "'" + "," + "'" + objVC.DI + "'" + "," + "'" + objVC.MO + "'" + "," + "'" + objVC.YE + "'" + "," + "'" + objVC.HO + "'" + ",";
                        comcfc2.CommandText += "'" + objVC.Sent + "'" + "," + "'" + objVC.km + "'" + "," + objVC.dta[ahora, 1, 1, 1] + "," + objVC.dta[ahora, 1, 1, 2] + "," + objVC.dta[ahora, 1, 1, 3] + "," + objVC.dta[ahora, 1, 1, 4] + ",";
                        comcfc2.CommandText += objVC.dta[ahora, 1, 1, 5] + "," + objVC.dta[ahora, 1, 1, 6] + "," + objVC.dta[ahora, 1, 1, 7] + "," + objVC.dta[ahora, 1, 1, 8] + "," + objVC.dta[ahora, 1, 1, 9] + ",";
                        comcfc2.CommandText += objVC.dta[ahora, 1, 1, 10] + "," + objVC.dta[ahora, 1, 1, 11] + "," + objVC.dta[ahora, 1, 1, 12] + "," + objVC.dta[ahora, 1, 1, 13] + "," + objVC.dta[ahora, 1, 1, 14] + ")";
                        comcfc2.ExecuteNonQuery();
                   
                }

            }
            DesconectaBancoClassfic();
            DesconectaBancoClassfic2();
        }

        // ************************************************************










        // ****************************************************************
        public void GravaTabelaComp(string atabO, string atabD)
        {
            int ahora = 0;
            int ocomp = 0;

            C_VelComp objVC = new C_VelComp();

            ConectaBancoClassfic();
            ConectaBancoClassfic2();


            comcfc1.CommandText = "select * from " + atabO;
            readercfc1 = comcfc1.ExecuteReader();

            while (readercfc1.Read())
            {
                objVC.aRod = readercfc1["Rod"].ToString();
                objVC.DI = readercfc1["DI"].ToString();
                objVC.MO = readercfc1["MO"].ToString();
                objVC.YE = readercfc1["YE"].ToString();
                objVC.HO = readercfc1["HO"].ToString(); ahora = Int32.Parse(objVC.HO);
                objVC.Sent = readercfc1["Sent"].ToString();
                objVC.km = readercfc1["km"].ToString();
                objVC.comp = readercfc1["comp"].ToString(); ocomp = Int32.Parse(objVC.comp);


                //( 24, 2, 3, 13) As Integer  ' ''24 horas e 2 lanes, 3 Comprimentos, 14 Velocidades
                objVC.dta[ahora, 1, ocomp, 1] = Int32.Parse(readercfc1["Vel19"].ToString());
                objVC.dta[ahora, 1, ocomp, 2] = Int32.Parse(readercfc1["Vel29"].ToString());
                objVC.dta[ahora, 1, ocomp, 3] = Int32.Parse(readercfc1["Vel39"].ToString());
                objVC.dta[ahora, 1, ocomp, 4] = Int32.Parse(readercfc1["Vel49"].ToString());
                objVC.dta[ahora, 1, ocomp, 5] = Int32.Parse(readercfc1["Vel59"].ToString());
                objVC.dta[ahora, 1, ocomp, 6] = Int32.Parse(readercfc1["Vel69"].ToString());
                objVC.dta[ahora, 1, ocomp, 7] = Int32.Parse(readercfc1["Vel79"].ToString());
                objVC.dta[ahora, 1, ocomp, 8] = Int32.Parse(readercfc1["Vel89"].ToString());
                objVC.dta[ahora, 1, ocomp, 9] = Int32.Parse(readercfc1["Vel99"].ToString());
                objVC.dta[ahora, 1, ocomp, 10] = Int32.Parse(readercfc1["Vel109"].ToString());
                objVC.dta[ahora, 1, ocomp, 11] = Int32.Parse(readercfc1["Vel119"].ToString());
                objVC.dta[ahora, 1, ocomp, 12] = Int32.Parse(readercfc1["Vel129"].ToString());
                objVC.dta[ahora, 1, ocomp, 13] = Int32.Parse(readercfc1["Vel139"].ToString());
                objVC.dta[ahora, 1, ocomp, 14] = Int32.Parse(readercfc1["Vel199"].ToString());



                if (ocomp == 3)
                {

                    for (int ind1 = 2; ind1 <= 14; ind1++)
                    {   objVC.dta[ahora, 1, 1, 1] += objVC.dta[ahora, 1, 1, ind1];
                        objVC.dta[ahora, 1, 2, 1] += objVC.dta[ahora, 1, 2, ind1];
                        objVC.dta[ahora, 1, 3, 1] += objVC.dta[ahora, 1, 3, ind1];

                    }


                    comcfc2.CommandText = "insert into " + atabD + " (Rod, DI, MO, YE, HO, Sent, km, comp1, comp2, comp3)";                    
                    comcfc2.CommandText += "Values (" + "'" + objVC.aRod + "'" + "," + "'" + objVC.DI + "'" + "," + "'" + objVC.MO + "'" + "," + "'" + objVC.YE + "'" + "," + "'" + objVC.HO + "'" + ",";
                    comcfc2.CommandText += "'" + objVC.Sent + "'" + "," + "'" + objVC.km + "'" + "," + objVC.dta[ahora, 1, 1, 1] + "," + objVC.dta[ahora, 1, 2, 1] + "," + objVC.dta[ahora, 1, 3, 1] + ")";
                    comcfc2.ExecuteNonQuery();

                }

            }
            DesconectaBancoClassfic();
            DesconectaBancoClassfic2();



        }

        private void arquivoDeVelocidadeEComprimentoPistaSimples2FaixasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Arquivo de Velocidade e Comprimento - PistaSimples - 2 Faixas - 1 faixa por sentido

            FrmEntraArquivosVelComp fLavelc = new FrmEntraArquivosVelComp();
            fLavelc.ShowDialog();



        }

        private void pistaSimplesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void novosDiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmGeraDias frmGeraDias = new FrmGeraDias();
            frmGeraDias.ShowDialog();
        }

        private void geraBAPorABToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Gera sentido BA por AB
            FrmGeraDiasBApAB frmGeraDiasBApAB = new FrmGeraDiasBApAB();
            frmGeraDiasBApAB.ShowDialog();

        }

        private void configuraçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void selecionaOMêsDaMediçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMes frmM = new FrmMes();
            frmM.ShowDialog();
        }

        private void comerciaisParaMotoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // moto para passeio, jogando 70 % das motos para passeio e reduzindo as motos a 30% do seu valor inicial


            string aRod = "";
            string oKm = "";

            CarregaPontoClassVez(objPontV); // carrega o ponto da clasificação a ser processada da Vez
            DeletaTabela("tbvelcomphoradiamodelo");
            DeletaTabela("tbvelcomphoradiamodeloax");



            aRod = cUt.TiraTraco(objPontV.Rod); oKm = objPontV.km;
            if (oKm.Length < 3) { oKm = "km0" + oKm; } else { oKm = "km" + oKm; }
            CopiaUmaTabelaParaOutra("tbvelcomphoradia" + aRod + oKm, "tbvelcomphoradiamodeloax");

            CopiaUmaTabelaParaOutra("tbvelcomphoradia" + aRod + oKm, "tbvelcomphoradiamodelo");

            DeletaTabela("tbvelcomphoradia" + aRod + oKm);

            AlteraDadosTabelaComerciaisMoto("tbvelcomphoradiamodelo", "tbvelcomphoradia" + aRod + oKm);

            DeletaTabela("tbvelhoradia" + aRod + oKm);
            GravaTabelaVel("tbvelcomphoradia" + aRod + oKm, "tbvelhoradia" + aRod + oKm);

            DeletaTabela("tbcomphoradia" + aRod + oKm);
            GravaTabelaComp("tbvelcomphoradia" + aRod + oKm, "tbcomphoradia" + aRod + oKm);


        }

        private void tabelaDePlacasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTabPlacas fPlacas = new FrmTabPlacas();
            fPlacas.ShowDialog();
        }







        private void paginaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            C_pontofe objPFe = new C_pontofe();
            string strRod; string MunA = ""; string MunB = "";

            int npag = 1;
            

            ConectaBanco();

            com1.CommandText = "select * from ponto_featual";
            reader1 = com1.ExecuteReader();
            if (reader1.Read())
            {
                objPFe.Rodovia = reader1["Rodovia"].ToString();
                objPFe.kmEdital = reader1["kmEdital"].ToString();
                objPFe.MunSen = (reader1["MunSen"]).ToString();

                objPFe.MunA = reader1["MunA"].ToString();
                objPFe.MunB = reader1["MunB"].ToString();

                objPFe.Tipo = (reader1["Tipo"]).ToString();

            }
            reader1.Close();

            strRod = objPFe.Rodovia;
            objDtb.GetMunicipios(strRod, ref MunA, ref MunB);  // obtem os municípios limite


            //string ExcelAberto = "Estudo" + strRod + "_km" + objPFe.kmEdital + "_Res798" + ".xlsx";
            //objExcel.FechaAlgumExcel(@"C:\DER-RJ\FiscalizaçãoEletronica\2020\Estudos2020\" + ExcelAberto);  // fecha qualquer instancia do Excel Aberta



            string nomearq = "Estudo" + strRod + "_km" + objPFe.kmEdital + "_Res798" + ".xlsx";
            objExcel.AbreExcel(@"C:\DER-RJ\FiscalizaçãoEletronica\2020\Estudos2020\" + nomearq, "TabelaPlacasAB");


            // para pg 23,24 então linha=41

            // para pg 29 croqui então se paisagem linha=45 S, 

            // para pg 30 linha 40 L

            // conclusão linha 60 N


            // public void Pagina(int i, int j, int sheet, string str1, int size1, string cor)
            // Get ASCII character.
            // public void MudaFonte(int i, int j, int sheet, string estilo, Boolean Neg, int tamanho)

            for (int ind1=3;ind1<=38;ind1++)
            {
                switch(ind1)
                {
                    case (23):
                        {
                            char carcol = 'W';
                            int coluna = ((int)carcol) - 64;  // obtem o numero da coluna N 
                            objExcel.Pagina(41, coluna, ind1, npag.ToString(), 10, "Black") ;
                            objExcel.MudaFonte(41, coluna, ind1, "Arial", false, 10);
                            break;
                        }

                    case (24):
                        {
                            char carcol = 'W';
                            int coluna = ((int)carcol) - 64;  // obtem o numero da coluna N 
                            objExcel.Pagina(41, coluna, ind1, npag.ToString(), 10, "Black");
                            objExcel.MudaFonte(41, coluna, ind1, "Arial", false, 10);
                            break;
                        }

                    case (25):
                        {
                            char carcol = 'M';
                            int coluna = ((int)carcol) - 64;  // obtem o numero da coluna N 
                            objExcel.Pagina(60, coluna, ind1, npag.ToString(), 10, "Black");
                            objExcel.MudaFonte(60, coluna, ind1, "Arial", false, 10);
                                                       
                            break;
                        }

                    case (26):
                        {
                            char carcol = 'M';
                            int coluna = ((int)carcol) - 64;  // obtem o numero da coluna N 
                            objExcel.Pagina(60, coluna, ind1, npag.ToString(), 10, "Black");
                            objExcel.MudaFonte(60, coluna, ind1, "Arial", false, 10);
                            break;
                        }



                    case (28):
                        {
                            char carcol = 'M';
                            int coluna = ((int)carcol) - 64;  // obtem o numero da coluna N 
                            objExcel.Pagina(60, coluna, ind1, npag.ToString(), 10, "Black");
                            objExcel.MudaFonte(60, coluna, ind1, "Arial", false, 10);
                            break;
                        }

                    case (21):
                        {
                            char carcol = 'S';
                            int coluna = ((int)carcol) - 64;  // obtem o numero da coluna N 
                            objExcel.Pagina(45, coluna, ind1, npag.ToString(), 10, "Black");
                            objExcel.MudaFonte(45, coluna, ind1, "Arial", false, 10);
                            break;
                        }

                    case (29):
                        {
                            char carcol = 'S';
                            int coluna = ((int)carcol) - 64;  // obtem o numero da coluna N 
                            objExcel.Pagina(45, coluna, ind1, npag.ToString(), 10, "Black");
                            objExcel.MudaFonte(45, coluna, ind1, "Arial", false, 10);


                            carcol = 'Q';
                            coluna = ((int)carcol) - 64;  // obtem o numero da coluna N 
                            objExcel.Pagina(70, coluna, ind1, npag.ToString(), 10, "Black");
                            objExcel.MudaFonte(70, coluna, ind1, "Arial", false, 10);
                            break;
                        }


                    case (30):
                        {
                            char carcol = 'L';
                            int coluna = ((int)carcol) - 64;  // obtem o numero da coluna N 
                            objExcel.Pagina(40, coluna, ind1, npag.ToString(), 10, "Black");
                            objExcel.MudaFonte(40, coluna, ind1, "Arial", false, 10);
                            break;
                        }



                    case (38):
                        {
                            char carcol = 'N';
                            int coluna = ((int)carcol) - 64;  // obtem o numero da coluna N 
                            objExcel.Pagina(60, coluna, ind1, npag.ToString(), 10, "Black");
                            objExcel.MudaFonte(60, coluna, ind1, "Arial", false, 10);
                            break;
                        }


                    default:
                        {
                            char carcol = 'N';
                            int coluna = ((int)carcol) - 64;  // obtem o numero da coluna N 
                            objExcel.Pagina(60, coluna, ind1, npag.ToString(), 10, "Black");
                            objExcel.MudaFonte(60, coluna, ind1, "Arial", false, 10);
                            break;
                        }
                        
                       
                }
                npag++;
            }











            // RODOVIA + KM 


            // 3L ; acima de p3.1 é 2L ; em 23 e 24 é U1 em 25 e 26 é k1, em 28 é l1; em 29 é R3; 30 é k1; 31,32 até 38 é L3;

            for (int ind1 = 2; ind1 <= 38; ind1++)
            {
                switch (ind1)
                {
                    case (10):
                    case (11):
                    case (12):
                    case (13):
                    case (14):
                        {
                            char carcol = 'L';
                            
                            int coluna = ((int)carcol) - 64;  // obtem o numero da coluna N 
                            objExcel.Agrupar(2, coluna, 2, coluna + 1);
                            objExcel.Pagina(2, coluna, ind1, strRod + "  km " + cUt.ConvPontoVirg(objPFe.kmEdital), 10, "Gray");
                            objExcel.MudaFonte(2, coluna, ind1, "Arial", false, 10);
                            break;
                        }

                    case (23):
                        {
                            char carcol = 'U';
                            int coluna = ((int)carcol) - 64;  // obtem o numero da coluna N 
                            objExcel.Pagina(1, coluna, ind1, strRod + "  km " + cUt.ConvPontoVirg(objPFe.kmEdital), 10, "Gray");
                            objExcel.MudaFonte(1, coluna, ind1, "Arial", false, 10);
                            break;
                        }

                    case (24):
                        {
                            char carcol = 'U';
                            int coluna = ((int)carcol) - 64;  // obtem o numero da coluna N 
                            objExcel.Pagina(1, coluna, ind1, strRod + "  km " + cUt.ConvPontoVirg(objPFe.kmEdital), 10, "Gray");
                            objExcel.MudaFonte(1, coluna, ind1, "Arial", false, 10);
                            break;
                        }

                    case (25):
                        {
                            char carcol = 'K';
                            int coluna = ((int)carcol) - 64;  // obtem o numero da coluna N 
                            objExcel.Pagina(1, coluna, ind1, strRod + "  km " + cUt.ConvPontoVirg(objPFe.kmEdital), 10, "Gray");
                            objExcel.MudaFonte(1, coluna, ind1, "Arial", false, 10);
                            break;
                        }

                    case (26):
                        {
                            char carcol = 'K';
                            int coluna = ((int)carcol) - 64;  // obtem o numero da coluna N 
                            objExcel.Pagina(1, coluna, ind1, strRod + "  km " + cUt.ConvPontoVirg(objPFe.kmEdital), 10, "Gray");
                            objExcel.MudaFonte(1, coluna, ind1, "Arial", false, 10);
                            break;
                        }



                    case (28):
                        {
                            char carcol = 'L';
                            int coluna = ((int)carcol) - 64;  // obtem o numero da coluna N 
                            objExcel.Pagina(1, coluna, ind1, strRod + "  km " + cUt.ConvPontoVirg(objPFe.kmEdital), 10, "Gray");
                            objExcel.MudaFonte(1, coluna, ind1, "Arial", false, 10);
                            break;
                        }

                    case (29):
                        {
                            char carcol = 'R';
                            int coluna = ((int)carcol) - 64;  // obtem o numero da coluna N 
                            objExcel.Pagina(3, coluna, ind1, strRod + "  km " + cUt.ConvPontoVirg(objPFe.kmEdital), 10, "Gray");
                            objExcel.MudaFonte(3, coluna, ind1, "Arial", false, 10);
                            break;
                        }

                    case (30):
                        {
                            char carcol = 'K';
                            int coluna = ((int)carcol) - 64;  // obtem o numero da coluna N 
                            objExcel.Pagina(1, coluna, ind1, strRod + "  km " + cUt.ConvPontoVirg(objPFe.kmEdital), 10, "Gray");
                            objExcel.MudaFonte(1, coluna, ind1, "Arial", false, 10);
                            break;
                        }

                    default:
                        {
                            char carcol = 'L';
                            int coluna = ((int)carcol) - 64;  // obtem o numero da coluna N 

                            //objExcel.Agrupar(1, 11, 1, 12);
                            objExcel.Pagina(3, coluna, ind1, strRod + "  km " + cUt.ConvPontoVirg(objPFe.kmEdital), 10, "Gray");                                                        
                            objExcel.MudaFonte(3, coluna, ind1, "Arial", false, 10);
                            break;
                        }


                }
                
            }


        }





        //******************************************************************





    }


}

   

