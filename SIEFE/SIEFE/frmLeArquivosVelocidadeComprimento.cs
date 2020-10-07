using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Button = System.Windows.Forms.Button;






namespace SIEFE
{
    public partial class frmLeArquivosVelocidadeComprimento : Form
    {
        //System.Windows.Forms.ListBox lstbx1 = new System.Windows.Forms.ListBox();
        Uteis cUt = new Uteis();


        public frmLeArquivosVelocidadeComprimento()
        {
            InitializeComponent();
            

        }



        public void frmLeArquivosVelocidadeComprimento_Load(object sender, EventArgs e)
        {
            
           // this.Location = new System.Drawing.Point(10, 10);
           // this.Size = new Size(400, 500);

           // // Botão para selecionar arquivos prn
           // Button btn = new Button();
           // btn.Location = new System.Drawing.Point( 50, 30);
           // btn.Name = "btnSArq"; // botão para selecionar arquivos prn
           // btn.Text = "Seleciona Arquivos de Entrada";

           // btn.Size = new Size(120, 40);
           // btn.Padding = new Padding(0);
           // btn.Click += new EventHandler(this.buttonArq_Click);
           // this.Controls.Add(btn);
           // // fim Botão para selecionar arquivos prn


           // // /////////////ListBox 
           //// listBox1.Location= new System.Drawing.Point(170, 180);


           
           // lstbx1.Location = new System.Drawing.Point(190, 100);
           // lstbx1.Name = "ListViewPrn";
           // lstbx1.Size = new System.Drawing.Size(170, 260);
           // // Set the ListBox to display items in multiple columns.

           // lstbx1.Font = new System.Drawing.Font("Arial", 12, FontStyle.Regular,GraphicsUnit.Pixel);
           // lstbx1.MultiColumn = true;
           // // Set the selection mode to multiple and extended.
           // //lstbx1.SelectionMode = SelectionMode.MultiExtended;
           // lstbx1.SelectionMode = SelectionMode.MultiSimple;
           //// lstbx1.Click+=new EventHandler(this.buttonArq_Click);
           //// Add the ListBox to the form.
           //Controls.Add(lstbx1);
           // // //////////fim Listbox

        }





        //private void buttonArq_Click(object sender, EventArgs e)
        //{
            
        //    // MessageBox.Show(btn1.Name + "Clicked");                 

        //    OpenFileDialog openFileDialogClass = new OpenFileDialog();

        //    openFileDialogClass.InitialDirectory = @"C:\RR\Classificações2020\";
        //    openFileDialogClass.RestoreDirectory = true;
        //    openFileDialogClass.Title = "Arquivos de Velocidade de Comprimento para 2 faixas";
        //    openFileDialogClass.DefaultExt = "prn";
        //    openFileDialogClass.Filter = "prn files (*.prn)|*.prn";
        //    openFileDialogClass.Multiselect = true;

        //    openFileDialogClass.ShowDialog();


        //    foreach (String file in openFileDialogClass.FileNames)
        //    {
        //        //MessageBox.Show(file);
        //        string ofile = cUt.ExtraiArquivo(file);
        //       // ofile=
        //        lstbx1.Items.Add(ofile);
        //    }
        //}


        
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
