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
    public partial class FrmMes : Form
    {
        Form1 objIni = new Form1();

        public FrmMes()
        {
            InitializeComponent();
        }

        private void FrmMes_Load(object sender, EventArgs e)
        {
            cmbMesMed.Items.Clear();

            objIni.ConectaBancoClassfic();


            objIni.comcfc1.CommandText = "select * from tbmes";
            objIni.readercfc1 = objIni.comcfc1.ExecuteReader();
           
            while (objIni.readercfc1.Read())
            { cmbMesMed.Items.Add((objIni.readercfc1["omes"]).ToString()); } 
            
            objIni.comcfc1.Dispose();




            objIni.comcfc1.CommandText = "select * from tbmesmedicao";
            objIni.readercfc1 = objIni.comcfc1.ExecuteReader();

            while (objIni.readercfc1.Read())
            { cmbMesMed.Text=((objIni.readercfc1["omes"]).ToString()); }

            objIni.comcfc1.Dispose();





            objIni.DesconectaBancoClassfic();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int nmes = 0;
            objIni.ConectaBancoClassfic();

            objIni.comcfc1.CommandText = "delete from tbmesmedicao";

            objIni.comcfc1.ExecuteNonQuery();
            objIni.comcfc1.Dispose();

            objIni.comcfc1.CommandText = "select * from tbmes where omes=" + "'" + cmbMesMed.Text + "'";
            objIni.readercfc1 = objIni.comcfc1.ExecuteReader();


            while (objIni.readercfc1.Read())
            { nmes=( Int32.Parse( (objIni.readercfc1["nmes"]).ToString())  ); }

            objIni.comcfc1.Dispose();
            
            objIni.DesconectaBancoClassfic();



            objIni.ConectaBancoClassfic();

            objIni.comcfc1.CommandText = "insert into tbmesmedicao (nmes, omes) Values (" + nmes + "," + "'" + cmbMesMed.Text + "'" + ")";
            objIni.comcfc1.ExecuteNonQuery();
            objIni.comcfc1.Dispose();

            objIni.DesconectaBancoClassfic();
        }
    }
}
