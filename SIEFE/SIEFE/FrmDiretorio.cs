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
using System.Globalization;

namespace SIEFE
{
    public partial class FrmDiretorio : Form
    {

        Form1 objIni = new Form1();
        Uteis c_Ut = new Uteis();

        C_pontoCL objPontV = new C_pontoCL();


        public FrmDiretorio()
        {
            InitializeComponent();
        }




        private void FrmDiretorio_Load(object sender, EventArgs e)
        {
            dTPI.Format= DateTimePickerFormat.Custom;
            dTPF.Format = DateTimePickerFormat.Custom;
            //"MMMM dd, yyyy - dddd";
            dTPI.CustomFormat= "dd/MM/yyyy - dddd";
            dTPF.CustomFormat = "dd/MM/yyyy - dddd";

            CarregaDiretorioEconfigurações();

        }








        // *******************************************************************

        private void CarregaDiretorioEconfigurações()
        {
            string aRJ = "";
            string oKm = "";

            string dateString, format;
            DateTime result;
            CultureInfo provider = CultureInfo.InvariantCulture;

            format = "dd/MM/yyyy";

            for (int ind1 = 1; ind1 <= 4; ind1++) { cbNFx.Items.Add(ind1); cmbNFxSent.Items.Add(ind1); }
            for (int ind1 = 1; ind1 <= 2; ind1++) { cmbQtdClass.Items.Add(ind1); cmbqtdCq.Items.Add(ind1); }

            objIni.ConectaBancoClassfic();

            objIni.comcfc1.CommandText = "select * from tbpontovez";
            objIni.readercfc1 = objIni.comcfc1.ExecuteReader();
            while (objIni.readercfc1.Read())
            {
                aRJ = (objIni.readercfc1["Rod"]).ToString();
                oKm = (objIni.readercfc1["km"]).ToString();
            }

            objIni.comcfc1.Dispose();

            lblaRod.Text = aRJ;
            lbloKm.Text = oKm;


            // Cconecta o banco FE
            objIni.ConectaBanco();
            objIni.com1.CommandText = "select * from trodovias where Rodovia=" + "'" + aRJ + "'";
            objIni.reader1 = objIni.com1.ExecuteReader();
            if (objIni.reader1.Read())
            {
                objPontV.munA = objIni.reader1["munA"].ToString();      lblMunA.Text = objPontV.munA;
                objPontV.munB = objIni.reader1["munB"].ToString();      lblMunB.Text = objPontV.munB;
            }
            objIni.DesconectaBanco();
            // Desconecta o banco FE



            objIni.comcfc1.CommandText = "select * from tbpontos where Rod=" + "'" + aRJ + "'" + " and km=" + "'" + oKm + "'";
            objIni.readercfc1 = objIni.comcfc1.ExecuteReader();
            while (objIni.readercfc1.Read())
            {
                objPontV.Rod = objIni.readercfc1["Rod"].ToString();
                objPontV.km = objIni.readercfc1["km"].ToString();
                objPontV.municipio = objIni.readercfc1["municipio"].ToString(); txtMun.Text = objPontV.municipio;
                //objPontV.munA = objIni.readercfc1["munA"].ToString(); lblMunA.Text = objPontV.munA;
                //objPontV.munB = objIni.readercfc1["munB"].ToString(); lblMunB.Text = objPontV.munB;
                objPontV.sAB = Int32.Parse(objIni.readercfc1["sAB"].ToString());
                objPontV.sBA = Int32.Parse(objIni.readercfc1["sBA"].ToString());
                objPontV.ntfaixas = Int32.Parse(objIni.readercfc1["ntfaixas"].ToString());
                objPontV.nfxsent = Int32.Parse(objIni.readercfc1["nfxsent"].ToString());
                objPontV.qtdclass = Int32.Parse(objIni.readercfc1["qtdclass"].ToString());
                objPontV.qtdcroquis = Int32.Parse(objIni.readercfc1["qtdcroquis"].ToString());
                objPontV.psimples = Int32.Parse(objIni.readercfc1["psimples"].ToString());
                objPontV.periodo = (objIni.readercfc1["periodo"].ToString());

                if ((objPontV.sAB == 1) && (objPontV.sBA == 1)) // Se ambos os sentidos
                { rdAmbSent.Checked = true; }
                else { if (objPontV.sAB == 1) { rdSAB.Checked = true; } else { rdSBA.Checked = true; } }


                cbNFx.Text = objPontV.ntfaixas.ToString();
                cmbNFxSent.Text = objPontV.nfxsent.ToString();
                cmbQtdClass.Text = objPontV.qtdclass.ToString();
                cmbqtdCq.Text = objPontV.qtdcroquis.ToString();

                
                result = DateTime.ParseExact((objPontV.periodo).Substring(0, 10), format, provider);
                dTPI.Value= result;
                result = DateTime.ParseExact((objPontV.periodo).Substring(15, 10), format, provider);
                dTPF.Value = result;

                if (objPontV.psimples == 1) { chKPS.Checked = true; }
            }




            objIni.DesconectaBancoClassfic();

        }








        // ************************************************
       

        private void button1_Click(object sender, EventArgs e)
        {
            string aRJ = "";
            string oKm = "";
            string caminho = "";
            string ncaminho = "";

             string openFileName, folderName;


        objIni.ConectaBancoClassfic();

            objIni.comcfc1.CommandText = "select * from tbdiretorio";
            //objIni.comcfc1.ExecuteReader();

            objIni.readercfc1 = objIni.comcfc1.ExecuteReader();
            while (objIni.readercfc1.Read())
            {
                caminho = (objIni.readercfc1["Caminho"]).ToString();
            }
            objIni.DesconectaBancoClassfic();


            fdBwrDiaDir.SelectedPath= @caminho;
            DialogResult result = fdBwrDiaDir.ShowDialog();

            if (result == DialogResult.OK)
            {
                openFileName = fdBwrDiaDir.SelectedPath;
                ExtraiRJ(@openFileName, 1, ref aRJ, ref oKm);
                lblaRod.Text = aRJ;
                lbloKm.Text = oKm;
                //  C:\RR\Classificações2020\RJ-106\km 73
            }




            //// OpenFileDialog openFileDialogClass = new OpenFileDialog();
            // openFileDialogDir.InitialDirectory = @caminho;

            // // openFileDialogClass.InitialDirectory = @"C:\RR\Classificações2020\";
            // openFileDialogDir.RestoreDirectory = true;
            // openFileDialogDir.Title = "Diretório";
            // // openFileDialogClass.DefaultExt = "prn";
            // openFileDialogDir.Filter = "all files (*.*)|*.*";
            // openFileDialogDir.Multiselect = true;

            // openFileDialogDir.ShowDialog();
        }









        private void btSv_Click(object sender, EventArgs e)
        {
            string ncaminho = "";
            string aRJ = "";
            string oKm = "";

            int irdSAB = 0;
            int irdSBA = 0;
            int ichKPS = 0;

            Boolean ExistePonto = false;

            objIni.ConectaBancoClassfic();

            ncaminho = fdBwrDiaDir.SelectedPath;
            ncaminho = c_Ut.DuasBarras(ncaminho);
            ExtraiRJ(ncaminho, 2, ref aRJ, ref oKm);

            objIni.comcfc1.CommandText = "select * from tbpontos where Rod=" + "'" + aRJ + "'" + " and km=" + "'" + oKm + "'";
            objIni.readercfc1 = objIni.comcfc1.ExecuteReader();
            if (objIni.readercfc1.Read())
            {
                lblaRod.Text = objIni.readercfc1["Rod"].ToString();
                lbloKm.Text= objIni.readercfc1["km"].ToString();
                //txtMun.Text= objIni.readercfc1["municipio"].ToString();
                lblMunA.Text= objIni.readercfc1["munA"].ToString();
                lblMunB.Text = objIni.readercfc1["munB"].ToString();

                objPontV.sAB = Int32.Parse(objIni.readercfc1["sAB"].ToString());
                objPontV.sBA = Int32.Parse(objIni.readercfc1["sBA"].ToString());

                if ((objPontV.sAB == 1) && (objPontV.sBA == 1)) // Se ambos os sentidos
                { rdAmbSent.Checked = true; }
                else { if (objPontV.sAB == 1) { rdSAB.Checked = true; } else { rdSBA.Checked = true; } }

                objPontV.sAB = Int32.Parse(objIni.readercfc1["sAB"].ToString());
                objPontV.sBA = Int32.Parse(objIni.readercfc1["sBA"].ToString());




                cbNFx.Text = (objIni.readercfc1["ntfaixas"].ToString());
                cmbNFxSent.Text = (objIni.readercfc1["nfxsent"].ToString());
                cmbQtdClass.Text = (objIni.readercfc1["qtdclass"].ToString());
                cmbqtdCq.Text = (objIni.readercfc1["qtdcroquis"].ToString());

                if (Int32.Parse(objIni.readercfc1["psimples"].ToString())== 1) { chKPS.Checked = true; }
                objPontV.periodo = (objIni.readercfc1["periodo"].ToString());

                ExistePonto = true;
            }
            objIni.readercfc1.Close();
            objIni.comcfc1.Dispose();




            if (ExistePonto)
            {
                objIni.comcfc1.CommandText = "delete from tbpontovez";
                objIni.comcfc1.ExecuteNonQuery();
                objIni.comcfc1.Dispose();

                ExtraiRJ(ncaminho, 2, ref aRJ, ref oKm);
                objIni.comcfc1.CommandText = "insert into tbpontovez (Rod, km) Values (" + "'" + aRJ + "'" + "," + "'" + oKm + "'" + ")";
                objIni.comcfc1.ExecuteNonQuery();
                objIni.comcfc1.Dispose();


                objIni.comcfc1.CommandText = "delete from tbdiretorio";
                objIni.comcfc1.ExecuteNonQuery();
                objIni.comcfc1.Dispose();

                objIni.comcfc1.CommandText = "insert into tbdiretorio (Caminho) Values (" + "'" + ncaminho + "'" + ")";
                objIni.comcfc1.ExecuteNonQuery();
                objIni.comcfc1.Dispose();
            }









            if (!ExistePonto)
            {

                // Início da Transação
                // MysqlConClf1
                MySqlTransaction transaction;
                transaction = objIni.MysqlConClf1.BeginTransaction();
                objIni.comcfc1.Connection = objIni.MysqlConClf1;
                objIni.comcfc1.Transaction = transaction;


                try
                {

                    //string ncaminho2 = "";

                    ncaminho = fdBwrDiaDir.SelectedPath;
                    // ncaminho2 = @ncaminho;

                    //ncaminho = @"C:\\RR" ;
                    // ncaminho = "C:\\\\RR";

                    ncaminho = c_Ut.DuasBarras(ncaminho);


                    objIni.comcfc1.CommandText = "delete from tbdiretorio";
                    objIni.comcfc1.ExecuteNonQuery();
                    objIni.comcfc1.Dispose();

                    objIni.comcfc1.CommandText = "insert into tbdiretorio (Caminho) Values (" + "'" + ncaminho + "'" + ")";
                    objIni.comcfc1.ExecuteNonQuery();
                    objIni.comcfc1.Dispose();

                    objIni.comcfc1.CommandText = "delete from tbpontovez";
                    objIni.comcfc1.ExecuteNonQuery();
                    objIni.comcfc1.Dispose();

                    ExtraiRJ(ncaminho, 2, ref aRJ, ref oKm);
                    objIni.comcfc1.CommandText = "insert into tbpontovez (Rod, km) Values (" + "'" + aRJ + "'" + "," + "'" + oKm + "'" + ")";
                    objIni.comcfc1.ExecuteNonQuery();
                    objIni.comcfc1.Dispose();


                    // Cconecta o banco FE
                    objIni.ConectaBanco();
                    objIni.com1.CommandText = "select * from trodovias where Rodovia=" + "'" + aRJ + "'";
                    objIni.reader1 = objIni.com1.ExecuteReader();
                    if (objIni.reader1.Read())
                    {
                        objPontV.munA = objIni.reader1["munA"].ToString(); lblMunA.Text = objPontV.munA;
                        objPontV.munB = objIni.reader1["munB"].ToString(); lblMunB.Text = objPontV.munB;
                    }
                    objIni.DesconectaBanco();
                    // Desconecta o banco FE


                    objIni.comcfc1.CommandText = "delete from tbpontos where Rod=" + "'" + aRJ + "'" + " and km=" + "'" + oKm + "'";
                    objIni.comcfc1.ExecuteNonQuery();
                    objIni.comcfc1.Dispose();


                    if (rdSAB.Checked) { irdSAB = 1; } else { irdSAB = 0; }
                    if (rdSBA.Checked) { irdSBA = 1; } else { irdSBA = 0; }
                    if (chKPS.Checked) { ichKPS = 1; } else { ichKPS = 0; }

                    if (rdAmbSent.Checked) { irdSAB = 1; irdSBA = 1; }

                    // implementar posteriormente o codigo para incluir o período corretamente
                    objIni.comcfc1.CommandText = "insert into tbpontos (Rod, km, municipio, munA, munB, sAB, sBA, ntfaixas, nfxsent, qtdclass, qtdcroquis, psimples,periodo)";
                    objIni.comcfc1.CommandText += " Values (" + "'" + aRJ + "'" + "," + "'" + oKm + "'" + "," + "'" + txtMun.Text + "'" + "," + "'" + lblMunA.Text + "'" + "," + "'" + lblMunB.Text + "'";
                    objIni.comcfc1.CommandText += "," + irdSAB + "," + irdSBA + "," + cbNFx.Text + "," + cmbNFxSent.Text + "," + cmbQtdClass.Text + "," + cmbqtdCq.Text + "," + ichKPS + "," + "'24/08/2020 ATÉ 30/08/2020'" + ")";

                    objIni.comcfc1.ExecuteNonQuery();
                    objIni.comcfc1.Dispose();

                    //+ "'AB'"

                  



                    transaction.Commit();


                }

                catch
                {
                    transaction.Rollback();
                    MessageBox.Show("Por favor, preencha todos os campos");

                }

            }
            
            objIni.DesconectaBancoClassfic();
        }










        // ****************************************************
        private void ExtraiRJ(string caminho, int nbarra,  ref string aRJ, ref string oKm)
        {
            aRJ = "";
            oKm = "";
            int tam = caminho.Length;

            for (int ind1=tam-1; ind1>0; ind1--)
            { 
             if (caminho[ind1]!='\\')
                { oKm = caminho[ind1] + oKm; }
                else
                {
                    if (nbarra == 2) { ind1--; }

                    for (int ind2=ind1-1;ind2>0; ind2--)
                    {
                        if (caminho[ind2] != '\\')
                        { aRJ = caminho[ind2] + aRJ; } else { ind2 = 0; }
                    }
                    ind1 = 0;
                }
            
            }

            oKm = (oKm.Substring(2)).Trim();

        }

        // ****************************************************










        private void fdBwrDiaDir_HelpRequest(object sender, EventArgs e)
        {

        }

        private void openFileDialogDir_FileOk(object sender, CancelEventArgs e)
        {
            int ind1 = 0;
            ind1 = ind1;
        }
        // ****************************************************



    }
}
