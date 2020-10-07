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
    public partial class FrmGeraDias : Form
    {
        Form1 frm1 = new Form1();
        Uteis cUt = new Uteis();

        C_DataBase objDtb = new C_DataBase();
        C_Rodovia objRod = new C_Rodovia();
        C_percet objPct = new C_percet();

        C_VelMod objVel = new C_VelMod();
        C_CompMod objComp = new C_CompMod();

        int nrod = 0;
        string aRod = "";
        string okm = "";

        int odiasemana=0; //  1 até 7.  1 é domingo , 7 é sabado

        int ahora = 0;
        float opct = 0;

        string strConFE = @"Server=localhost;Database=fe;Uid=root;Pwd=obuldog67;";
        string strConcfc = @"Server=localhost;Database=classfic;Uid=root;Pwd=obuldog67;";


        MySqlConnection mysqlCon1;
        MySqlConnection mysqlCon2;
        MySqlConnection MysqlConClf1;
        MySqlConnection MysqlConClf2;

        MySqlCommand com1;
        MySqlCommand com2;
        MySqlCommand comcfc1;
        MySqlCommand comcfc2;


        MySqlDataReader reader1;
        MySqlDataReader reader2;
        MySqlDataReader readercfc1;




        public void DesconectaBancoClassfic()
        {
            MysqlConClf1.Close();
        }
       
        // *************************************************************************************************************

        public void ConectaBancoClassfic()
        {
            using (MysqlConClf1 = new MySqlConnection(strConcfc)) ;
            {
                MysqlConClf1.Open();
                comcfc1 = MysqlConClf1.CreateCommand();

            }
        }




        public void DesconectaBancoClassfic2()
        {
            MysqlConClf2.Close();
        }

        // *************************************************************************************************************

        public void ConectaBancoClassfic2()
        {
            using (MysqlConClf2 = new MySqlConnection(strConcfc)) ;
            {
                MysqlConClf2.Open();
                comcfc2 = MysqlConClf2.CreateCommand();

            }
        }





        public FrmGeraDias()
        {
            InitializeComponent();
        }




        private void FrmGeraDias_Load(object sender, EventArgs e)
        {
            Form1 objIni = new Form1();
            C_pontoCL objPontV = new C_pontoCL();

            btnAlteraDiaBase.Enabled = false;
            objDtb.GetRodovias(ref nrod, objRod);
            PopulaComboRodovias(nrod, objRod);
            cmbRod.Text = aRod;

            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            for (int inddia1=1; inddia1 <= 31; inddia1++)
            {
                comboBox1.Items.Add(inddia1);
                comboBox2.Items.Add(inddia1);
            }

            objIni.CarregaPontoClassVez(objPontV);

            cmbRod.Text = objPontV.Rod;
            txtkm.Text = objPontV.km; 

        }





        private void PopulaComboRodovias(int nRod, C_Rodovia objRod)
        {
            cmbRod.Items.Clear();
            int ind1 = 0;
            for (ind1 = 1; ind1 <= nRod; ind1++) { cmbRod.Items.Add(objRod.Rodovia[ind1]); }
        }















        // ***************************************************************************************

        private void button1_Click(object sender, EventArgs e)
        {
            // Inicialmente Gera a semana de 20 até 26 de abril 2020
            int indDS = 0; // indice do dia da sema
            string aRod = "";
            string oKm = "";


            C_pontoCL objPontV = new C_pontoCL();
            Form1 ibjIni = new Form1();

            ibjIni.CarregaPontoClassVez(objPontV);
            aRod = cUt.TiraTraco(objPontV.Rod);
            oKm = objPontV.km;


            if (oKm.Length < 3)
            {
                oKm = "0" + oKm;
            }


            ConectaBancoClassfic();

            if ((Int32.Parse(txtkm.Text) < 10))
            { txtkm.Text = "0" + txtkm.Text; }


            





            // Processa dia de origem para gerar dia gerado
            for (int ind1 = 26; ind1 <= 27; ind1++)

            {


                comcfc1.CommandText = "select * from tperct1 where arod=" + "'" + cmbRod.Text + "'";                
                comcfc1.CommandText = comcfc1.CommandText + " and km=" + "'" + objPontV.km + "'" + " and Sent = " + "'AB'";
                comcfc1.CommandText = comcfc1.CommandText + " order by DS";
                readercfc1 = comcfc1.ExecuteReader();

                while (readercfc1.Read())
                {
                    //indDS++;
                    indDS = Int32.Parse((readercfc1["DS"]).ToString());
                    objPct.aRod = (readercfc1["arod"]).ToString();
                    objPct.okm = (readercfc1["km"]).ToString();
                    objPct.DS[indDS] = Int32.Parse((readercfc1["DS"]).ToString());
                    objPct.pct[1, indDS] = Int32.Parse((readercfc1["pct1"]).ToString());
                    objPct.pct[2, indDS] = Int32.Parse((readercfc1["pct2"]).ToString());
                    objPct.pct[3, indDS] = Int32.Parse((readercfc1["pct3"]).ToString());
                    objPct.pct[4, indDS] = Int32.Parse((readercfc1["pct4"]).ToString());
                    objPct.pct[5, indDS] = Int32.Parse((readercfc1["pct5"]).ToString());
                    objPct.pct[6, indDS] = Int32.Parse((readercfc1["pct6"]).ToString());
                    objPct.pct[7, indDS] = Int32.Parse((readercfc1["pct7"]).ToString());
                    objPct.pct[8, indDS] = Int32.Parse((readercfc1["pct8"]).ToString());
                    objPct.pct[9, indDS] = Int32.Parse((readercfc1["pct9"]).ToString());
                    objPct.pct[10, indDS] = Int32.Parse((readercfc1["pct10"]).ToString());
                    objPct.pct[11, indDS] = Int32.Parse((readercfc1["pct11"]).ToString());
                    objPct.pct[12, indDS] = Int32.Parse((readercfc1["pct12"]).ToString());
                    objPct.pct[13, indDS] = Int32.Parse((readercfc1["pct13"]).ToString());
                    objPct.pct[14, indDS] = Int32.Parse((readercfc1["pct14"]).ToString());
                    objPct.pct[15, indDS] = Int32.Parse((readercfc1["pct15"]).ToString());
                    objPct.pct[16, indDS] = Int32.Parse((readercfc1["pct16"]).ToString());
                    objPct.pct[17, indDS] = Int32.Parse((readercfc1["pct17"]).ToString());
                    objPct.pct[18, indDS] = Int32.Parse((readercfc1["pct18"]).ToString());
                    objPct.pct[19, indDS] = Int32.Parse((readercfc1["pct19"]).ToString());
                    objPct.pct[20, indDS] = Int32.Parse((readercfc1["pct20"]).ToString());
                    objPct.pct[21, indDS] = Int32.Parse((readercfc1["pct21"]).ToString());
                    objPct.pct[22, indDS] = Int32.Parse((readercfc1["pct22"]).ToString());
                    objPct.pct[23, indDS] = Int32.Parse((readercfc1["pct23"]).ToString());
                    objPct.pct[24, indDS] = Int32.Parse((readercfc1["pct24"]).ToString());

                }
                readercfc1.Close();
                comcfc1.Dispose();




                if (ind1==29)
                {
                    ind1 = ind1;
                }

                DateTime dt = new DateTime(2020, 8, ind1);
                odiasemana = cUt.NumeroDiaSemana((dt.DayOfWeek).ToString());


                // if ((ind1 == 26) || (ind1 == 27) || (ind1 == 28) || (ind1 == 29) || (ind1 == 30) || (ind1 == 31))
               // if ((ind1 == 25) || (ind1 == 30) || (ind1 == 31))
                   // if ((ind1 == 26) || (ind1 == 27) || (ind1 == 28) || (ind1 == 29) || (ind1 == 30))
                    if ((ind1 == 25) || (ind1 == 28) || (ind1 == 29) || (ind1 == 30))
                    { // não faz nada para rj-106 km 37 AB
                    // não faz nada para rj-186 km 102 AB
                }
                else
                {
                    // Sentido AB
                    comcfc1.CommandText = "select * from tbvelcomphoradia" + aRod + "km" + oKm + " where Rod=" + "'" + cmbRod.Text + "'" + " and DI=" + "'" + txtDia.Text + "'";
                    comcfc1.CommandText = comcfc1.CommandText + " and km=" + "'" + objPontV.km + "'" + " and Sent=" + "'AB'" + " order by HO";

                    readercfc1 = comcfc1.ExecuteReader();



                    while (readercfc1.Read())
                    {
                        objVel.aRod = readercfc1["Rod"].ToString();
                        objVel.DI = readercfc1["DI"].ToString();
                        objVel.MO = readercfc1["MO"].ToString();
                        objVel.YE = readercfc1["YE"].ToString();
                        objVel.HO = readercfc1["HO"].ToString();
                        objVel.km = readercfc1["km"].ToString();
                        objVel.Sent = readercfc1["Sent"].ToString();

                        objComp.comp1 = Int32.Parse(readercfc1["comp"].ToString());  // estou usando a variável comp1 para o comprimento, não importando se é moto, passeio ou comerciais

                        objVel.Vel19 = Int32.Parse(readercfc1["Vel19"].ToString());
                        objVel.Vel29 = Int32.Parse(readercfc1["Vel29"].ToString());
                        objVel.Vel39 = Int32.Parse(readercfc1["Vel39"].ToString());
                        objVel.Vel49 = Int32.Parse(readercfc1["Vel49"].ToString());
                        objVel.Vel59 = Int32.Parse(readercfc1["Vel59"].ToString());
                        objVel.Vel69 = Int32.Parse(readercfc1["Vel69"].ToString());
                        objVel.Vel79 = Int32.Parse(readercfc1["Vel79"].ToString());
                        objVel.Vel89 = Int32.Parse(readercfc1["Vel89"].ToString());
                        objVel.Vel99 = Int32.Parse(readercfc1["Vel99"].ToString());
                        objVel.Vel109 = Int32.Parse(readercfc1["Vel109"].ToString());
                        objVel.Vel119 = Int32.Parse(readercfc1["Vel119"].ToString());
                        objVel.Vel129 = Int32.Parse(readercfc1["Vel129"].ToString());
                        objVel.Vel139 = Int32.Parse(readercfc1["Vel139"].ToString());
                        objVel.Vel199 = Int32.Parse(readercfc1["Vel199"].ToString());

                        ahora = Int32.Parse(objVel.HO);
                        opct = (objPct.pct[ahora, odiasemana]);
                        opct = opct / 100;

                        objVel.Vel19 = (int)Math.Round(objVel.Vel19 * opct);
                        objVel.Vel29 = (int)Math.Round(objVel.Vel29 * opct);
                        objVel.Vel39 = (int)Math.Round(objVel.Vel39 * opct);
                        objVel.Vel49 = (int)Math.Round(objVel.Vel49 * opct);
                        objVel.Vel59 = (int)Math.Round(objVel.Vel59 * opct);
                        objVel.Vel69 = (int)Math.Round(objVel.Vel69 * opct);
                        objVel.Vel79 = (int)Math.Round(objVel.Vel79 * opct);
                        objVel.Vel89 = (int)Math.Round(objVel.Vel89 * opct);
                        objVel.Vel99 = (int)Math.Round(objVel.Vel99 * opct);
                        objVel.Vel109 = (int)Math.Round(objVel.Vel109 * opct);
                        objVel.Vel119 = (int)Math.Round(objVel.Vel119 * opct);
                        objVel.Vel129 = (int)Math.Round(objVel.Vel129 * opct);
                        objVel.Vel139 = (int)Math.Round(objVel.Vel139 * opct);
                        objVel.Vel199 = (int)Math.Round(objVel.Vel199 * opct);

                        ConectaBancoClassfic2();
                        comcfc2.CommandText = "insert into tbvelcomphoradia" + aRod + "km" + oKm + " (Rod, DI, MO, YE, HO, Sent, km, comp, Vel19, Vel29, Vel39,";
                        comcfc2.CommandText = comcfc2.CommandText + " Vel49, Vel59, Vel69, Vel79, Vel89, Vel99,";
                        comcfc2.CommandText = comcfc2.CommandText + " Vel109, Vel119, Vel129, Vel139, Vel199) ";
                        // comcfc2.CommandText = comcfc2.CommandText + "Values (" + "'" + objVel.aRod + "'" + "," + "'" + objVel.DI + "'" + "," + "'" + objVel.MO + "'" + "," + "'" + objVel.YE + "'" + "," + "'" + objVel.HO + "'" + ",";
                        comcfc2.CommandText = comcfc2.CommandText + "Values (" + "'" + objVel.aRod + "'" + "," + "'" + ind1.ToString() + "'" + "," + "'" + objVel.MO + "'" + "," + "'" + objVel.YE + "'" + "," + "'" + objVel.HO + "'" + ",";
                        comcfc2.CommandText = comcfc2.CommandText + "'" + objVel.Sent + "'" + "," + "'" + objPontV.km + "'" + "," + objComp.comp1 + "," + objVel.Vel19 + "," + objVel.Vel29 + "," + objVel.Vel39 + "," + objVel.Vel49 + ",";
                        comcfc2.CommandText = comcfc2.CommandText + objVel.Vel59 + "," + objVel.Vel69 + "," + objVel.Vel79 + "," + objVel.Vel89 + "," + objVel.Vel99 + ",";
                        comcfc2.CommandText = comcfc2.CommandText + objVel.Vel109 + "," + objVel.Vel119 + "," + objVel.Vel129 + "," + objVel.Vel139 + "," + objVel.Vel199 + ")";
                        comcfc2.ExecuteNonQuery();

                        DesconectaBancoClassfic2();

                    }
                }
                readercfc1.Close();
                comcfc1.Dispose();

                // fim sentido AB











                // SENTIDO BA

                comcfc1.CommandText = "select * from tperct1 where arod=" + "'" + cmbRod.Text + "'";
                //  comcfc1.CommandText = "select * from tperct1";
                comcfc1.CommandText = comcfc1.CommandText + " and km=" + "'" + objPontV.km + "'" + " and Sent = " + "'BA'";

                comcfc1.CommandText = comcfc1.CommandText + " order by DS";
                readercfc1 = comcfc1.ExecuteReader();

                while (readercfc1.Read())
                {
                    //indDS++;
                    indDS = Int32.Parse((readercfc1["DS"]).ToString());
                    objPct.aRod = (readercfc1["arod"]).ToString();
                    objPct.okm = (readercfc1["km"]).ToString();
                    objPct.DS[indDS] = Int32.Parse((readercfc1["DS"]).ToString());
                    objPct.pct[1, indDS] = Int32.Parse((readercfc1["pct1"]).ToString());
                    objPct.pct[2, indDS] = Int32.Parse((readercfc1["pct2"]).ToString());
                    objPct.pct[3, indDS] = Int32.Parse((readercfc1["pct3"]).ToString());
                    objPct.pct[4, indDS] = Int32.Parse((readercfc1["pct4"]).ToString());
                    objPct.pct[5, indDS] = Int32.Parse((readercfc1["pct5"]).ToString());
                    objPct.pct[6, indDS] = Int32.Parse((readercfc1["pct6"]).ToString());
                    objPct.pct[7, indDS] = Int32.Parse((readercfc1["pct7"]).ToString());
                    objPct.pct[8, indDS] = Int32.Parse((readercfc1["pct8"]).ToString());
                    objPct.pct[9, indDS] = Int32.Parse((readercfc1["pct9"]).ToString());
                    objPct.pct[10, indDS] = Int32.Parse((readercfc1["pct10"]).ToString());
                    objPct.pct[11, indDS] = Int32.Parse((readercfc1["pct11"]).ToString());
                    objPct.pct[12, indDS] = Int32.Parse((readercfc1["pct12"]).ToString());
                    objPct.pct[13, indDS] = Int32.Parse((readercfc1["pct13"]).ToString());
                    objPct.pct[14, indDS] = Int32.Parse((readercfc1["pct14"]).ToString());
                    objPct.pct[15, indDS] = Int32.Parse((readercfc1["pct15"]).ToString());
                    objPct.pct[16, indDS] = Int32.Parse((readercfc1["pct16"]).ToString());
                    objPct.pct[17, indDS] = Int32.Parse((readercfc1["pct17"]).ToString());
                    objPct.pct[18, indDS] = Int32.Parse((readercfc1["pct18"]).ToString());
                    objPct.pct[19, indDS] = Int32.Parse((readercfc1["pct19"]).ToString());
                    objPct.pct[20, indDS] = Int32.Parse((readercfc1["pct20"]).ToString());
                    objPct.pct[21, indDS] = Int32.Parse((readercfc1["pct21"]).ToString());
                    objPct.pct[22, indDS] = Int32.Parse((readercfc1["pct22"]).ToString());
                    objPct.pct[23, indDS] = Int32.Parse((readercfc1["pct23"]).ToString());
                    objPct.pct[24, indDS] = Int32.Parse((readercfc1["pct24"]).ToString());

                }
                readercfc1.Close();
                comcfc1.Dispose();






                //if ((ind1 == 26) || (ind1 == 27) || (ind1 == 28) || (ind1 == 29) || (ind1 == 30) || (ind1 == 31))
                // if ((ind1 == 25) || (ind1 == 30) || (ind1 == 31))
                if ((ind1 == 25) || (ind1 == 28) || (ind1 == 29) || (ind1 == 30))
                { // não faz nada para rj-106 km 49 BA
                }
                else
                {

                    // SEntido BA
                    comcfc1.CommandText = "select * from tbvelcomphoradia" + aRod + "km" + oKm + " where Rod=" + "'" + cmbRod.Text + "'" + " and DI=" + "'" + txtDia.Text + "'";
                    comcfc1.CommandText = comcfc1.CommandText + " and km=" + "'" + objPontV.km + "'" + " and Sent=" + "'BA'" + " order by HO";


                    readercfc1 = comcfc1.ExecuteReader();


                    while (readercfc1.Read())
                    {

                        objVel.aRod = readercfc1["Rod"].ToString();
                        objVel.DI = readercfc1["DI"].ToString();
                        objVel.MO = readercfc1["MO"].ToString();
                        objVel.YE = readercfc1["YE"].ToString();
                        objVel.HO = readercfc1["HO"].ToString();
                        objVel.km = readercfc1["km"].ToString();
                        objVel.Sent = readercfc1["Sent"].ToString();

                        objComp.comp1 = Int32.Parse(readercfc1["comp"].ToString());  // estou usando a variável comp1 para o comprimento, não importando se é moto, passeio ou comerciais


                        objVel.Vel19 = Int32.Parse(readercfc1["Vel19"].ToString());
                        objVel.Vel29 = Int32.Parse(readercfc1["Vel29"].ToString());
                        objVel.Vel39 = Int32.Parse(readercfc1["Vel39"].ToString());
                        objVel.Vel49 = Int32.Parse(readercfc1["Vel49"].ToString());
                        objVel.Vel59 = Int32.Parse(readercfc1["Vel59"].ToString());
                        objVel.Vel69 = Int32.Parse(readercfc1["Vel69"].ToString());
                        objVel.Vel79 = Int32.Parse(readercfc1["Vel79"].ToString());
                        objVel.Vel89 = Int32.Parse(readercfc1["Vel89"].ToString());
                        objVel.Vel99 = Int32.Parse(readercfc1["Vel99"].ToString());
                        objVel.Vel109 = Int32.Parse(readercfc1["Vel109"].ToString());
                        objVel.Vel119 = Int32.Parse(readercfc1["Vel119"].ToString());
                        objVel.Vel129 = Int32.Parse(readercfc1["Vel129"].ToString());
                        objVel.Vel139 = Int32.Parse(readercfc1["Vel139"].ToString());
                        objVel.Vel199 = Int32.Parse(readercfc1["Vel199"].ToString());

                        ahora = Int32.Parse(objVel.HO);
                        opct = (objPct.pct[ahora, odiasemana]);
                        opct = opct / 100;

                        objVel.Vel19 = (int)Math.Round(objVel.Vel19 * opct);
                        objVel.Vel29 = (int)Math.Round(objVel.Vel29 * opct);
                        objVel.Vel39 = (int)Math.Round(objVel.Vel39 * opct);
                        objVel.Vel49 = (int)Math.Round(objVel.Vel49 * opct);
                        objVel.Vel59 = (int)Math.Round(objVel.Vel59 * opct);
                        objVel.Vel69 = (int)Math.Round(objVel.Vel69 * opct);
                        objVel.Vel79 = (int)Math.Round(objVel.Vel79 * opct);
                        objVel.Vel89 = (int)Math.Round(objVel.Vel89 * opct);
                        objVel.Vel99 = (int)Math.Round(objVel.Vel99 * opct);
                        objVel.Vel109 = (int)Math.Round(objVel.Vel109 * opct);
                        objVel.Vel119 = (int)Math.Round(objVel.Vel119 * opct);
                        objVel.Vel129 = (int)Math.Round(objVel.Vel129 * opct);
                        objVel.Vel139 = (int)Math.Round(objVel.Vel139 * opct);
                        objVel.Vel199 = (int)Math.Round(objVel.Vel199 * opct);

                        ConectaBancoClassfic2();

                        comcfc2.CommandText = "insert into tbvelcomphoradia" + aRod + "km" + oKm + " (Rod, DI, MO, YE, HO, Sent, km, comp, Vel19, Vel29, Vel39,";
                        comcfc2.CommandText = comcfc2.CommandText + " Vel49, Vel59, Vel69, Vel79, Vel89, Vel99,";
                        comcfc2.CommandText = comcfc2.CommandText + " Vel109, Vel119, Vel129, Vel139, Vel199) ";
                        // comcfc2.CommandText = comcfc2.CommandText + "Values (" + "'" + objVel.aRod + "'" + "," + "'" + objVel.DI + "'" + "," + "'" + objVel.MO + "'" + "," + "'" + objVel.YE + "'" + "," + "'" + objVel.HO + "'" + ",";
                        comcfc2.CommandText = comcfc2.CommandText + "Values (" + "'" + objVel.aRod + "'" + "," + "'" + ind1.ToString() + "'" + "," + "'" + objVel.MO + "'" + "," + "'" + objVel.YE + "'" + "," + "'" + objVel.HO + "'" + ",";
                        comcfc2.CommandText = comcfc2.CommandText + "'" + objVel.Sent + "'" + "," + "'" + objPontV.km + "'" + "," + objComp.comp1 + "," + objVel.Vel19 + "," + objVel.Vel29 + "," + objVel.Vel39 + "," + objVel.Vel49 + ",";
                        comcfc2.CommandText = comcfc2.CommandText + objVel.Vel59 + "," + objVel.Vel69 + "," + objVel.Vel79 + "," + objVel.Vel89 + "," + objVel.Vel99 + ",";
                        comcfc2.CommandText = comcfc2.CommandText + objVel.Vel109 + "," + objVel.Vel119 + "," + objVel.Vel129 + "," + objVel.Vel139 + "," + objVel.Vel199 + ")";
                        comcfc2.ExecuteNonQuery();

                        DesconectaBancoClassfic2();



                    }
                }
                readercfc1.Close();
                comcfc1.Dispose();
                // fim sentido BA


            }


            DesconectaBancoClassfic();

            ibjIni.DeletaTabela("tbvelcomphoradiamodelo");
            ibjIni.DeletaTabela("tbvelcomphoradiamodeloax");


            ibjIni.CopiaUmaTabelaParaOutra("tbvelcomphoradia" + aRod + "km" + oKm, "tbvelcomphoradiamodeloax");

            ibjIni.CopiaUmaTabelaParaOutra("tbvelcomphoradia" + aRod + "km" + oKm, "tbvelcomphoradiamodelo");

            ibjIni.DeletaTabela("tbvelcomphoradia" + aRod + "km" + oKm);

            ibjIni.AlteraDadosTabela("tbvelcomphoradiamodelo", "tbvelcomphoradia" + aRod + "km" + oKm);

            ibjIni.DeletaTabela("tbvelhoradia" + aRod + "km" + oKm);
            ibjIni.GravaTabelaVel("tbvelcomphoradia" + aRod + "km" + oKm, "tbvelhoradia" + aRod + "km" + oKm);

            ibjIni.DeletaTabela("tbcomphoradia" + aRod + "km" + oKm);
            ibjIni.GravaTabelaComp("tbvelcomphoradia" + aRod + "km" + oKm, "tbcomphoradia" + aRod + "km" + oKm);


        }  // fim butão 


        // ***************************************************************************************








        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAlteraDiaBase_Click(object sender, EventArgs e)
        {
            string strmes = "04";
            
            ConectaBancoClassfic();
            

            comcfc1.CommandText = "update tbvelhoradiaRJ106KM3 set MO=" + "'" + strmes + "'";
            comcfc1.ExecuteNonQuery();

            comcfc1.CommandText = "update tbcomphoradiaRJ106KM3 set MO=" + "'" + strmes + "'";
            comcfc1.ExecuteNonQuery();

            comcfc1.CommandText = "update tbvelhoradiaRJ106KM3 set DI=" + "'" + comboBox2.Text + "'";
            comcfc1.CommandText = comcfc1.CommandText + " where DI=" + "'" + comboBox1.Text + "'";
            comcfc1.ExecuteNonQuery();

            comcfc1.CommandText = "update tbcomphoradiaRJ106KM3 set DI=" + "'" + comboBox2.Text + "'";
            comcfc1.CommandText = comcfc1.CommandText + " where DI=" + "'" + comboBox1.Text + "'";
            comcfc1.ExecuteNonQuery();

            DesconectaBancoClassfic();
        }
    }
}
