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
    public partial class FrmGeraTabelaAleat : Form
    {

        C_DataBase objDtb = new C_DataBase();
        C_Rodovia objRod = new C_Rodovia();

        Uteis cUt = new Uteis();

        public int[] aleat = new int[25]; //array 

        Form1 frm1 = new Form1();


        MySqlConnection mysqlCon1;
        MySqlConnection mysqlCon2;
        public MySqlConnection MysqlConClf1;

        MySqlCommand com1;
        MySqlCommand com2;
        public MySqlCommand comcfc1;

        MySqlDataReader reader1;
        MySqlDataReader reader2;
        MySqlDataReader readercfc1;


        int nrod = 0;
        string aRod = "";
        string okm = "";
        public string[] kms = new string[50];

        string strConFE = @"Server=localhost;Database=fe;Uid=root;Pwd=obuldog67;";
        string strConcfc = @"Server=localhost;Database=classfic;Uid=root;Pwd=obuldog67;";



        public FrmGeraTabelaAleat()
        {
            InitializeComponent();
        }




        private void ConectaBancoClassfic()
        {
            using (MysqlConClf1 = new MySqlConnection(strConcfc));
            {
                MysqlConClf1.Open();
                comcfc1 = MysqlConClf1.CreateCommand();
            }
        }


        public void DesconectaBancoClassfic()
        {
            MysqlConClf1.Close();
        }




        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            if ((cmbRod.Text != "") && (txtkm.Text != ""))
            {
                btnGerar.Enabled = true;
            }
            else
            { btnGerar.Enabled = false; }
        }









        private void btnGerar_Click(object sender, EventArgs e)
        {

            Form1 objIni = new Form1();

            objIni.DeletaTabela("tperct1");

            ConectaBancoClassfic();
            //comcfc1.CommandText = "delete from tperct1";
            //comcfc1.ExecuteNonQuery();

            string aRod = cmbRod.Text;

            for (int ind2 = 1; ind2 <= 7; ind2++)
            {
                for (int ind1 = 1; ind1 <= 24; ind1++)
                {
                    if (ind1 < 6 || ind1>21)
                    {
                        switch (ind2)
                        {
                            case 1:
                                aleat[ind1] = cUt.RandomNumber(90, 105);
                                break;
                            case 2:
                                aleat[ind1] = cUt.RandomNumber(90, 115); 
                                break;
                            case 3:
                                aleat[ind1] = cUt.RandomNumber(90, 113); 
                                break;
                            case 4:
                                aleat[ind1] = cUt.RandomNumber(80, 110); 
                                break;
                            case 5:
                                aleat[ind1] = cUt.RandomNumber(80, 140); 
                                break;
                            case 6:
                                aleat[ind1] = cUt.RandomNumber(130, 200); 
                                break;
                            case 7:
                                aleat[ind1] = cUt.RandomNumber(60, 105); 
                                break;
                            default: break;
                        }
                    }
                    else
                    {
                        switch (ind2)
                        {
                            case 1:
                                aleat[ind1] = cUt.RandomNumber(85, 115); 
                                break;
                            case 2:
                                aleat[ind1] = cUt.RandomNumber(90, 126);
                                break;
                            case 3:
                                aleat[ind1] = cUt.RandomNumber(90, 122);
                                break;
                            case 4:
                                aleat[ind1] = cUt.RandomNumber(82, 110);
                                break;

                            case 5:
                                aleat[ind1] = cUt.RandomNumber(85, 110); 
                                break;
                            case 6:
                                aleat[ind1] = cUt.RandomNumber(130, 200); 
                                break;
                            case 7:
                                aleat[ind1] = cUt.RandomNumber(89, 109);
                                break;
                            default: break;
                        }
                    }
                }

                comcfc1.CommandText = "insert into tperct1 (arod, km, DS, Sent, pct1, pct2, pct3, pct4, pct5, pct6, pct7, pct8, pct9, pct10, pct11, pct12, pct13, pct14, ";
                comcfc1.CommandText = comcfc1.CommandText + "pct15, pct16, pct17, pct18, pct19, pct20, pct21, pct22, pct23, pct24)";
                comcfc1.CommandText = comcfc1.CommandText + "Values (" + "'" + cmbRod.Text + "'" + "," + "'" + txtkm.Text + "'" + "," + ind2 + "," + "'AB'" + "," + aleat[1] + "," + aleat[2] + "," + aleat[3] + "," + aleat[4] + ",";
                comcfc1.CommandText = comcfc1.CommandText + aleat[5] + "," + aleat[6] + "," + aleat[7] + "," + aleat[8] + "," + aleat[9] + "," + aleat[10] + ",";
                comcfc1.CommandText = comcfc1.CommandText + aleat[11] + "," + aleat[12] + "," + aleat[13] + "," + aleat[14] + "," + aleat[15] + "," + aleat[16] + ",";
                comcfc1.CommandText = comcfc1.CommandText + aleat[17] + "," + aleat[18] + "," + aleat[19] + "," + aleat[20] + "," + aleat[21] + "," + aleat[22] + ",";
                comcfc1.CommandText = comcfc1.CommandText + aleat[23] + "," + aleat[24] + ")";

                comcfc1.ExecuteNonQuery();
            }


            // BA
            for (int ind2 = 1; ind2 <= 7; ind2++)
            {
                for (int ind1 = 1; ind1 <= 24; ind1++)
                {
                    if (ind1 < 6 || ind1 > 21)
                    {
                        switch (ind2)
                        {
                            case 1:
                                aleat[ind1] = cUt.RandomNumber(135, 165); 
                                break;
                            case 2:
                                aleat[ind1] = cUt.RandomNumber(135, 155);
                                break;
                            case 3:
                                aleat[ind1] = cUt.RandomNumber(87,105);
                                break;
                            case 4:
                                aleat[ind1] = cUt.RandomNumber(87, 106);
                                break;
                            case 5:
                                aleat[ind1] = cUt.RandomNumber(40, 86);
                                break;
                            case 6:
                                aleat[ind1] = cUt.RandomNumber(40, 97);
                                break;
                            case 7:
                                aleat[ind1] = cUt.RandomNumber(40, 87); 
                                break;
                            default: break;
                        }

                    }
                    else
                    {
                        switch (ind2)
                        {
                            case 1:
                                aleat[ind1] = cUt.RandomNumber(88, 150); 
                                break;
                            case 2:
                                aleat[ind1] = cUt.RandomNumber(99, 120); 
                                break;
                            case 3:
                                aleat[ind1] = cUt.RandomNumber(85, 102); 
                                break;
                            case 4:
                                aleat[ind1] = cUt.RandomNumber(87, 102); 
                                break;
                            case 5:
                                aleat[ind1] = cUt.RandomNumber(67, 86); 
                                break;
                            case 6:
                                aleat[ind1] = cUt.RandomNumber(65, 69); 
                                break;
                            case 7:
                                aleat[ind1] = cUt.RandomNumber(68, 87); 
                                break;
                            default: break;
                        }
                    }
                }

                comcfc1.CommandText = "insert into tperct1 (arod, km, DS, Sent, pct1, pct2, pct3, pct4, pct5, pct6, pct7, pct8, pct9, pct10, pct11, pct12, pct13, pct14, ";
                comcfc1.CommandText = comcfc1.CommandText + "pct15, pct16, pct17, pct18, pct19, pct20, pct21, pct22, pct23, pct24)";
                comcfc1.CommandText = comcfc1.CommandText + "Values (" + "'" + cmbRod.Text + "'" + "," + "'" + txtkm.Text + "'" + "," + ind2 + "," + "'BA'" + "," + aleat[1] + "," + aleat[2] + "," + aleat[3] + "," + aleat[4] + ",";
                comcfc1.CommandText = comcfc1.CommandText + aleat[5] + "," + aleat[6] + "," + aleat[7] + "," + aleat[8] + "," + aleat[9] + "," + aleat[10] + ",";
                comcfc1.CommandText = comcfc1.CommandText + aleat[11] + "," + aleat[12] + "," + aleat[13] + "," + aleat[14] + "," + aleat[15] + "," + aleat[16] + ",";
                comcfc1.CommandText = comcfc1.CommandText + aleat[17] + "," + aleat[18] + "," + aleat[19] + "," + aleat[20] + "," + aleat[21] + "," + aleat[22] + ",";
                comcfc1.CommandText = comcfc1.CommandText + aleat[23] + "," + aleat[24] + ")";

                comcfc1.ExecuteNonQuery();
            }



            DesconectaBancoClassfic();
        }









        private void cmbRod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ( (cmbRod.Text !="") && (txtkm.Text !=""))
            {
                btnGerar.Enabled = true;
            }
            else
            { btnGerar.Enabled = false; }
        }

        private void FrmGeraTabelaAleat_Load(object sender, EventArgs e)
        {
            Form1 objIni = new Form1();
            C_pontoCL objPontV = new C_pontoCL();

            objIni.CarregaPontoClassVez(objPontV); // carrega o ponto da clasificação a ser processada da Vez
            objDtb.GetRodovias(ref nrod, objRod);
            PopulaComboRodovias(nrod, objRod);

            cmbRod.Text = objPontV.Rod;
           // if (objPontV.km.Length<3) { objPontV.km = "0" + objPontV.km; }
            txtkm.Text = objPontV.km;

            objIni = null;
            objPontV = null;
            // cmbRod.Text = aRod;
        }



        private void PopulaComboRodovias(int nRod, C_Rodovia objRod)
        {
            cmbRod.Items.Clear();
            int ind1 = 0;
            for (ind1 = 1; ind1 <= nRod; ind1++) { cmbRod.Items.Add(objRod.Rodovia[ind1]); }
        }
    }
}
