using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SIEFE
{
    public partial class FrmEntraArquivosVelComp : Form
    {
        string strConcfc = @"Server=localhost;Database=classfic;Uid=root;Pwd=obuldog67;";

        Uteis cUt = new Uteis();
        RegTamVel2 obj241 = new RegTamVel2();

      //  RegTamVel2 obj241_vel = new RegTamVel2();
        int[] obj241_vel = new int[15];

        MySqlConnection mysqlCon1;
        MySqlConnection mysqlCon2;
        MySqlConnection MysqlConClf1;

        MySqlCommand com1;
        MySqlCommand com2;
        MySqlCommand comcfc1;

        MySqlDataReader reader1;
        MySqlDataReader reader2;
        MySqlDataReader readercfc1;



        // Creating a LinkedList of Strings 
        LinkedList<String> listArquivos = new LinkedList<String>();


       

        private void DesconectaBanco()
        {
            MysqlConClf1.Close();
        }

        private void DesconectaBancoClassfic()
        {
            MysqlConClf1.Close();
        }


        private void ConectaBancoClassfic()
        {

            using (MysqlConClf1 = new MySqlConnection(strConcfc)) ;
            {
                MysqlConClf1.Open();
                comcfc1 = MysqlConClf1.CreateCommand();                
            }

        }




        public FrmEntraArquivosVelComp()
        {
            InitializeComponent();
            
        }

        private void btnSeleciona_Click(object sender, EventArgs e)
        {
            // MessageBox.Show(btn1.Name + "Clicked");                 

            OpenFileDialog openFileDialogClass = new OpenFileDialog();

            openFileDialogClass.InitialDirectory = @"C:\RR\Classificações2020\";
            openFileDialogClass.RestoreDirectory = true;
            openFileDialogClass.Title = "Arquivos de Velocidade de Comprimento para 2 faixas";
            openFileDialogClass.DefaultExt = "prn";
            openFileDialogClass.Filter = "prn files (*.prn)|*.prn";
            openFileDialogClass.Multiselect = true;

            openFileDialogClass.ShowDialog();


            foreach (String file in openFileDialogClass.FileNames)
            {
                listArquivos.Clear();

                //MessageBox.Show(file);
                //string ofile = cUt.ExtraiArquivo(file);
                listArquivos.AddLast(file); // adiciona o nome completo do arquivo com o caminho do diretório a lista
                lstBox1.Items.Add(file);
            }
        }




        private void btnLimpaCx_Click(object sender, EventArgs e)
        {
            lstBox1.Items.Clear();

            // obj241.dta[24, 2, 3, 14] = 342; // só um exemplo: 24 horas, sentido BA, 
                                          // Veículo comercial e Velocidade entre 140/h e 199 km/h. 342 veículos

        }




        private void btnCarrega_Click(object sender, EventArgs e)
        {
            string str1 = "";
            foreach(String item in lstBox1.SelectedItems)
            {
               
                str1 = item;
                //   MessageBox.Show(item);
                // processa cada item que é o arquivo prn

                //  Processa6Linhas(str1); // São 3 linhas de comprimento na lane1 e 3 linhas de comprimento na lane2

                Processa6LinhasEArquivo(str1); // São 3 linhas de comprimento na lane1 e 3 linhas de comprimento na lane2
            }
            MessageBox.Show("Fim Carga de arquivos");
        }


        // /////

        private void Processa6LinhasEArquivo(string str1) // Monta o arquivo de velocidade
        {
            // obj241.dta = null;

            int aleat = 0;

            string ahora = "";
            int iahora = 0;
            int iSent = 0;
            int pripos = 14; // posição inicial dos veículos no registro
            var fileStream = new FileStream(str1, FileMode.Open, FileAccess.Read);


            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                line = streamReader.ReadLine(); // Lê a primeira linha com dados de data e etc...
                {
                    obj241.aRod = line.Substring(13, 2) + "-" + line.Substring(15, 3);
                    obj241.oStrSent = line.Substring(18, 2);

                    if (obj241.oStrSent == "AB") { iSent = 1; }
                    if (obj241.oStrSent == "BA") { iSent = 2; }

                    obj241.oKm = line.Substring(22, 3);
                    obj241.oDa = (line.Substring(36, 2));
                    obj241.oMes = (line.Substring(34, 2));
                    obj241.oAno = (line.Substring(38, 2));
                }

                for (int ind1 = 1; ind1 <= 5; ind1++)
                { line = streamReader.ReadLine(); } // Lê as proximas 5 linhas




                string strtabVelComp = "tbvelcomphoradia" +  cUt.SemTraço(obj241.aRod) + "km" + obj241.oKm;
                if (VerificaSeTabelaExiste("classfic", strtabVelComp)) // passa os parâmetros nome do banco e tabela
                {   // A tabela já existe  
                }
                else
                {
                    // A tabela não existe, então cria a mesma
                    CriaTabelaVelComp(strtabVelComp);
                }



                string strtabVel = "tbvelhoradia" + cUt.SemTraço(obj241.aRod) + "km" + obj241.oKm;                
                if (VerificaSeTabelaExiste("classfic", strtabVel)) // passa os parâmetros nome do banco e tabela
                {   // A tabela já existe  
                }
                else
                {
                    // A tabela não existe, então cria a mesma
                    CriaTabelaVel(strtabVel);
                }


                string strtabComp = "tbcomphoradia" + cUt.SemTraço(obj241.aRod) + "km" + obj241.oKm;
                if (VerificaSeTabelaExiste("classfic", strtabComp)) // passa os parâmetros nome do banco e tabela
                {   // A tabela já existe  
                }
                else
                {
                    // A tabela não existe, então cria a mesma
                    CriaTabelaComp(strtabComp);
                }







                while (((line = streamReader.ReadLine()) != null) && (line.Substring(0, 2) == "01"))// Lê a 1a. linha de contagens da mesma hora
                {

                    for (int ind1 = 1; ind1 <= 3; ind1++)   // lane1
                    {
                        pripos = 13;
                        if (ind1 > 1) { line = streamReader.ReadLine(); } // começa a ler as linhas das contagens
                        obj241.aHora = line.Substring(8, 2);
                        iahora = Int32.Parse(obj241.aHora);
                        // Public horaH(25, 3, 4, 15) As Integer  ' ''24 horas e 2 lanes, 3 Comprimentos, 14 Velocidades
                        switch (ind1)
                        {
                            case 1: // comprimento 1 lane1
                                for (int ind2 = 1; ind2 <= 14; ind2++)
                                {
                                    obj241.dta[iahora, 1, 1, ind2] = Int32.Parse(line.Substring(pripos, 4));
                                    if (ind2 == 14) { aleat = cUt.RandomNumber(1, 100); if (aleat < 97) obj241.dta[iahora, 1, 1, ind2] = 0; }
                                    pripos = pripos + 5;
                                }
                                break;
                            case 2:  // comprimento 2   lane1
                                for (int ind2 = 1; ind2 <= 14; ind2++)
                                {
                                    obj241.dta[iahora, 1, 2, ind2] = Int32.Parse(line.Substring(pripos, 4));
                                    if (ind2 == 14) { aleat = cUt.RandomNumber(1, 100); if (aleat < 97) obj241.dta[iahora, 1, 2, ind2] = 0; }

                                    pripos = pripos + 5;
                                }
                                break;
                            case 3:  // comprimento 3   lane1
                                for (int ind2 = 1; ind2 <= 14; ind2++)
                                {
                                    obj241.dta[iahora, 1, 3, ind2] = Int32.Parse(line.Substring(pripos, 4));
                                    if (ind2 == 14) { aleat = cUt.RandomNumber(1, 100); if (aleat < 97) obj241.dta[iahora, 1, 3, ind2] = 0; }
                                    pripos = pripos + 5;
                                }
                                break;
                            default:
                                // code block
                                MessageBox.Show("Não pode entrar aqui.");
                                break;
                        }

                    }







                    for (int ind1 = 1; ind1 <= 3; ind1++)    // lane2
                    {
                        pripos = 13;
                        line = streamReader.ReadLine();  // começa a ler as linhas das contagens
                        obj241.aHora = line.Substring(8, 2);
                        iahora = Int32.Parse(obj241.aHora);
                        // Public horaH(25, 3, 4, 15) As Integer  ' ''24 horas e 2 lanes, 3 Comprimentos, 14 Velocidades
                        switch (ind1)
                        {
                            case 1: // comprimento 1 lane 2
                                for (int ind2 = 1; ind2 <= 14; ind2++)
                                {
                                    obj241.dta[iahora, 2, 1, ind2] = Int32.Parse(line.Substring(pripos, 4));
                                    if (ind2 == 14) { aleat = cUt.RandomNumber(1, 100); if (aleat < 97) obj241.dta[iahora, 2, 1, ind2] = 0; }
                                    pripos = pripos + 5;
                                }
                                break;
                            case 2:  // comprimento 2 lane 2
                                for (int ind2 = 1; ind2 <= 14; ind2++)
                                {
                                    obj241.dta[iahora, 2, 2, ind2] = Int32.Parse(line.Substring(pripos, 4));
                                    if (ind2 == 14) { aleat = cUt.RandomNumber(1, 100); if (aleat < 97) obj241.dta[iahora, 2, 2, ind2] = 0; }
                                    pripos = pripos + 5;
                                }
                                break;
                            case 3:   // comprimento 3 lane 2
                                for (int ind2 = 1; ind2 <= 14; ind2++)
                                {
                                    obj241.dta[iahora, 2, 3, ind2] = Int32.Parse(line.Substring(pripos, 4));
                                    if (ind2 == 14) { aleat = cUt.RandomNumber(1, 100); if (aleat < 97) obj241.dta[iahora, 2, 3, ind2] = 0; }
                                    pripos = pripos + 5;
                                }
                                break;
                            default:
                                // code block
                                MessageBox.Show("Não pode entrar aqui.");
                                break;
                        }


                    }






                    // terminou de carregar as 6 linhas de uma mesma hora
                    // soma as 2 lanes do mesmo sentido
                    // 24 horas e 2 lanes, 3 Comprimentos, 14 Velocidades
                   
                    for (int ind1 = 1; ind1 <= 14; ind1++)
                    {
                        obj241.dta[iahora, 1, 1, ind1] = obj241.dta[iahora, 1, 1, ind1] + obj241.dta[iahora, 2, 1, ind1]; // soma lane1 e lane2 no comprimento1
                        obj241.dta[iahora, 1, 2, ind1] = obj241.dta[iahora, 1, 2, ind1] + obj241.dta[iahora, 2, 2, ind1]; // soma lane1 e lane2 no comprimento2
                        obj241.dta[iahora, 1, 3, ind1] = obj241.dta[iahora, 1, 3, ind1] + obj241.dta[iahora, 2, 3, ind1]; // soma lane1 e lane2 no comprimento3

                        // soma os 3 comprimentos
                        obj241_vel[ind1] = obj241.dta[iahora, 1, 1, ind1] + obj241.dta[iahora, 1, 2, ind1] + obj241.dta[iahora, 1, 3, ind1];
                    }

                    GravaVelocidadeComp(obj241, obj241_vel, strtabVelComp,1);


                    GravaVelocidade(obj241, obj241_vel, strtabVel);

                    for (int ind1 = 2; ind1 <= 14; ind1++) // soma as velocidades para a tabela de comprimento
                    {
                        obj241.dta[iahora, 1, 1, 1] = obj241.dta[iahora, 1, 1, 1] + obj241.dta[iahora, 1, 1, ind1]; // comprimento 1
                        obj241.dta[iahora, 1, 2, 1] = obj241.dta[iahora, 1, 2, 1] + obj241.dta[iahora, 1, 2, ind1]; // comprimento 2
                        obj241.dta[iahora, 1, 3, 1] = obj241.dta[iahora, 1, 3, 1] + obj241.dta[iahora, 1, 3, ind1]; // comprimento 3
                    }

                    GravaComprimento(obj241, strtabComp,1);


                } // fim do while
            }



        } //  Fim Processa6LinhasEArquivo


        // 
        /// <summary>
        /// 
        /// 

        // /////

        private void Processa6LinhasEArquivoPSimples(string str1) // Monta o arquivo de velocidade
        {
            // obj241.dta = null;

            int aleat = 0;

            string ahora = "";
            int iahora = 0;
            int iSent = 0;
            int pripos = 14; // posição inicial dos veículos no registro
            var fileStream = new FileStream(str1, FileMode.Open, FileAccess.Read);


            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                line = streamReader.ReadLine(); // Lê a primeira linha com dados de data e etc...
                {
                    obj241.aRod = line.Substring(13, 2) + "-" + line.Substring(15, 3);
                    obj241.oStrSent = line.Substring(18, 2);

                    if (obj241.oStrSent == "AB") { iSent = 1; }
                    if (obj241.oStrSent == "BA") { iSent = 2; }
                    if (obj241.oStrSent == "00") 
                    { // pista simples lane 1 é AB e lane 2 é BA
                    }



                    obj241.oKm = line.Substring(22, 3);
                    obj241.oDa = (line.Substring(36, 2));
                    obj241.oMes = (line.Substring(34, 2));
                    obj241.oAno = (line.Substring(38, 2));
                }

                for (int ind1 = 1; ind1 <= 5; ind1++)
                { line = streamReader.ReadLine(); } // Lê as proximas 5 linhas




                string strtabVelComp = "tbvelcomphoradia" + cUt.SemTraço(obj241.aRod) + "km" + obj241.oKm;
                if (VerificaSeTabelaExiste("classfic", strtabVelComp)) // passa os parâmetros nome do banco e tabela
                {   // A tabela já existe  
                }
                else
                {
                    // A tabela não existe, então cria a mesma
                    CriaTabelaVelComp(strtabVelComp);
                }



                string strtabVel = "tbvelhoradia" + cUt.SemTraço(obj241.aRod) + "km" + obj241.oKm;
                if (VerificaSeTabelaExiste("classfic", strtabVel)) // passa os parâmetros nome do banco e tabela
                {   // A tabela já existe  
                }
                else
                {
                    // A tabela não existe, então cria a mesma
                    CriaTabelaVel(strtabVel);
                }


                string strtabComp = "tbcomphoradia" + cUt.SemTraço(obj241.aRod) + "km" + obj241.oKm;
                if (VerificaSeTabelaExiste("classfic", strtabComp)) // passa os parâmetros nome do banco e tabela
                {   // A tabela já existe  
                }
                else
                {
                    // A tabela não existe, então cria a mesma
                    CriaTabelaComp(strtabComp);
                }







                while (((line = streamReader.ReadLine()) != null) && (line.Substring(0, 2) == "01"))// Lê a 1a. linha de contagens da mesma hora
                {

                    for (int ind1 = 1; ind1 <= 3; ind1++)   // lane1
                    {
                        obj241.oStrSent = "AB";
                        pripos = 13;
                        if (ind1 > 1) { line = streamReader.ReadLine(); } // começa a ler as linhas das contagens
                        obj241.aHora = line.Substring(8, 2);
                        iahora = Int32.Parse(obj241.aHora);
                        // Public horaH(25, 3, 4, 15) As Integer  ' ''24 horas e 2 lanes, 3 Comprimentos, 14 Velocidades
                        switch (ind1)
                        {
                            case 1: // comprimento 1 lane1
                                for (int ind2 = 1; ind2 <= 14; ind2++)
                                {
                                    obj241.dta[iahora, 1, 1, ind2] = Int32.Parse(line.Substring(pripos, 4));
                                    if (ind2 == 14) { aleat = cUt.RandomNumber(1, 100); if (aleat < 97) obj241.dta[iahora, 1, 1, ind2] = 0; }
                                    pripos = pripos + 5;
                                }
                                break;
                            case 2:  // comprimento 2   lane1
                                for (int ind2 = 1; ind2 <= 14; ind2++)
                                {
                                    obj241.dta[iahora, 1, 2, ind2] = Int32.Parse(line.Substring(pripos, 4));
                                    if (ind2 == 14) { aleat = cUt.RandomNumber(1, 100); if (aleat < 97) obj241.dta[iahora, 1, 2, ind2] = 0; }

                                    pripos = pripos + 5;
                                }
                                break;
                            case 3:  // comprimento 3   lane1
                                for (int ind2 = 1; ind2 <= 14; ind2++)
                                {
                                    obj241.dta[iahora, 1, 3, ind2] = Int32.Parse(line.Substring(pripos, 4));
                                    if (ind2 == 14) { aleat = cUt.RandomNumber(1, 100); if (aleat < 97) obj241.dta[iahora, 1, 3, ind2] = 0; }
                                    pripos = pripos + 5;
                                }
                                break;
                            default:
                                // code block
                                MessageBox.Show("Não pode entrar aqui.");
                                break;
                        }

                    }

                    // terminou de carregar as 3 linhas de uma mesma hora  para a LANE1                  
                    // 24 horas e 2 lanes, 3 Comprimentos, 14 Velocidades

                    for (int ind1 = 1; ind1 <= 14; ind1++)
                    {    // soma os 3 comprimentos
                        obj241_vel[ind1] = obj241.dta[iahora, 1, 1, ind1] + obj241.dta[iahora, 1, 2, ind1] + obj241.dta[iahora, 1, 3, ind1];
                    }

                    GravaVelocidadeComp(obj241, obj241_vel, strtabVelComp,1);
                    GravaVelocidade(obj241, obj241_vel, strtabVel);

                    for (int ind1 = 2; ind1 <= 14; ind1++) // soma as velocidades para a tabela de comprimento
                    {
                        obj241.dta[iahora, 1, 1, 1] = obj241.dta[iahora, 1, 1, 1] + obj241.dta[iahora, 1, 1, ind1]; // comprimento 1
                        obj241.dta[iahora, 1, 2, 1] = obj241.dta[iahora, 1, 2, 1] + obj241.dta[iahora, 1, 2, ind1]; // comprimento 2
                        obj241.dta[iahora, 1, 3, 1] = obj241.dta[iahora, 1, 3, 1] + obj241.dta[iahora, 1, 3, ind1]; // comprimento 3
                    }

                    GravaComprimento(obj241, strtabComp,1);













                    // *****************************************************************LANE2*************************************************
                    for (int ind1 = 1; ind1 <= 3; ind1++)    // lane2
                    {
                        obj241.oStrSent = "BA";
                        pripos = 13;
                        line = streamReader.ReadLine();  // começa a ler as linhas das contagens
                        obj241.aHora = line.Substring(8, 2);
                        iahora = Int32.Parse(obj241.aHora);
                        // Public horaH(25, 3, 4, 15) As Integer  ' ''24 horas e 2 lanes, 3 Comprimentos, 14 Velocidades
                        switch (ind1)
                        {
                            case 1: // comprimento 1 lane 2
                                for (int ind2 = 1; ind2 <= 14; ind2++)
                                {
                                    obj241.dta[iahora, 2, 1, ind2] = Int32.Parse(line.Substring(pripos, 4));
                                    if (ind2 == 14) { aleat = cUt.RandomNumber(1, 100); if (aleat < 97) obj241.dta[iahora, 2, 1, ind2] = 0; }
                                    pripos = pripos + 5;
                                }
                                break;
                            case 2:  // comprimento 2 lane 2
                                for (int ind2 = 1; ind2 <= 14; ind2++)
                                {
                                    obj241.dta[iahora, 2, 2, ind2] = Int32.Parse(line.Substring(pripos, 4));
                                    if (ind2 == 14) { aleat = cUt.RandomNumber(1, 100); if (aleat < 97) obj241.dta[iahora, 2, 2, ind2] = 0; }
                                    pripos = pripos + 5;
                                }
                                break;
                            case 3:   // comprimento 3 lane 2
                                for (int ind2 = 1; ind2 <= 14; ind2++)
                                {
                                    obj241.dta[iahora, 2, 3, ind2] = Int32.Parse(line.Substring(pripos, 4));
                                    if (ind2 == 14) { aleat = cUt.RandomNumber(1, 100); if (aleat < 97) obj241.dta[iahora, 2, 3, ind2] = 0; }
                                    pripos = pripos + 5;
                                }
                                break;
                            default:
                                // code block
                                MessageBox.Show("Não pode entrar aqui.");
                                break;
                        }


                    }


                    // terminou de carregar as 3 linhas de uma mesma hora  para a LANE2                  
                    // 24 horas e 2 lanes, 3 Comprimentos, 14 Velocidades

                    for (int ind1 = 1; ind1 <= 14; ind1++)
                    {    // soma os 3 comprimentos
                        obj241_vel[ind1] = obj241.dta[iahora, 2, 1, ind1] + obj241.dta[iahora, 2, 2, ind1] + obj241.dta[iahora, 2, 3, ind1];
                    }

                    GravaVelocidadeComp(obj241, obj241_vel, strtabVelComp,2);
                    GravaVelocidade(obj241, obj241_vel, strtabVel);

                    for (int ind1 = 2; ind1 <= 14; ind1++) // soma as velocidades para a tabela de comprimento
                    {
                        obj241.dta[iahora, 2, 1, 1] = obj241.dta[iahora, 2, 1, 1] + obj241.dta[iahora, 2, 1, ind1]; // comprimento 1
                        obj241.dta[iahora, 2, 2, 1] = obj241.dta[iahora, 2, 2, 1] + obj241.dta[iahora, 2, 2, ind1]; // comprimento 2
                        obj241.dta[iahora, 2, 3, 1] = obj241.dta[iahora, 2, 3, 1] + obj241.dta[iahora, 2, 3, ind1]; // comprimento 3
                    }

                    GravaComprimento(obj241, strtabComp,2);














                } // fim do while
            }



        } //  Fim Processa6LinhasEArquivo









        /// ////////////////////
        /// </summary>
        /// </param>

        private void Processa6Linhas(string str1) // Monta o arquivo de velocidade
        {
            string ahora = "";
            int iahora = 0;
            int iSent = 0;
            int pripos = 14; // posição inicial dos veículos no registro
            var fileStream = new FileStream(str1, FileMode.Open, FileAccess.Read);
            

            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                line = streamReader.ReadLine(); // Lê a primeira linha com dados de data e etc...
                 {
                    obj241.aRod = line.Substring(0, 2) + "-" + line.Substring(2, 3);
                    obj241.oStrSent = line.Substring(7, 2);

                    if (obj241.oStrSent =="AB") { iSent = 1; }
                    if (obj241.oStrSent =="BA") { iSent = 2; }

                    obj241.oKm = line.Substring(10, 2);
                    obj241.oDa= (line.Substring(36, 2));
                    obj241.oMes = (line.Substring(34, 2));
                    obj241.oAno= (line.Substring(38, 2));                    
                }

                for (int ind1=1; ind1<=5; ind1++)
                {  line = streamReader.ReadLine(); } // Lê as proximas 5 linhas


                while (   ((line = streamReader.ReadLine()) != null) && (line.Substring(0,2)=="01" ) )// Lê a 1a. linha de contagens da mesma hora
                {

                    for (int ind1 = 1; ind1 <= 3; ind1++)
                    {
                        pripos = 13;
                        if (ind1 > 1) { line = streamReader.ReadLine(); } // começa a ler as linhas das contagens
                        obj241.aHora = line.Substring(8, 2);
                        iahora = Int32.Parse(obj241.aHora);
                        // Public horaH(25, 3, 4, 15) As Integer  ' ''24 horas e 2 lanes, 3 Comprimentos, 14 Velocidades
                        switch (ind1)
                        {
                            case 1:
                               for (int ind2 = 1; ind2 <= 14; ind2++)
                                {
                                    obj241.dta[iahora, 1, 1, ind2] = Int32.Parse(line.Substring(pripos, 4));
                                    pripos = pripos + 5;
                                }
                                break;
                            case 2:
                                for (int ind2 = 1; ind2 <= 14; ind2++)
                                {
                                    obj241.dta[iahora, 1, 2, ind2] = Int32.Parse(line.Substring(pripos, 4));
                                    pripos = pripos + 5;
                                }
                                break;
                            case 3:
                                for (int ind2 = 1; ind2 <= 14; ind2++)
                                {
                                    obj241.dta[iahora, 1, 3, ind2] = Int32.Parse(line.Substring(pripos, 4));
                                    pripos = pripos + 5;
                                }
                                break;
                            default:
                                // code block
                                MessageBox.Show("Não pode entrar aqui.");
                                break;
                        }

                    }

                    for (int ind1 = 1; ind1 <= 3; ind1++)
                    {
                        pripos = 13;
                        line = streamReader.ReadLine();  // começa a ler as linhas das contagens
                        obj241.aHora = line.Substring(8, 2);
                        iahora = Int32.Parse(obj241.aHora);
                        // Public horaH(25, 3, 4, 15) As Integer  ' ''24 horas e 2 lanes, 3 Comprimentos, 14 Velocidades
                        switch (ind1)
                        {
                            case 1:
                                for (int ind2 = 1; ind2 <= 14; ind2++)
                                {
                                    obj241.dta[iahora, 2, 1, ind2] = Int32.Parse(line.Substring(pripos, 4));
                                    pripos = pripos + 5;
                                }
                                break;
                            case 2:
                                for (int ind2 = 1; ind2 <= 14; ind2++)
                                {
                                    obj241.dta[iahora, 2, 2, ind2] = Int32.Parse(line.Substring(pripos, 4));
                                    pripos = pripos + 5;
                                }
                                break;
                            case 3:
                                for (int ind2 = 1; ind2 <= 14; ind2++)
                                {
                                    obj241.dta[iahora, 2, 3, ind2] = Int32.Parse(line.Substring(pripos, 4));
                                    pripos = pripos + 5;
                                }
                                break;
                            default:
                                // code block
                                MessageBox.Show("Não pode entrar aqui.");
                                break;
                        }


                    }

                    // terminou de carregar as 6 linhas de uma mesma hora
                    // soma as 2 lanes do mesmo sentido
                    // 24 horas e 2 lanes, 3 Comprimentos, 14 Velocidades
                    for (int ind1=1;ind1<=14;ind1++)
                    {
                        obj241.dta[iahora, 1, 1, ind1] = obj241.dta[iahora, 1, 1, ind1] + obj241.dta[iahora, 2, 1, ind1]; // soma lane1 e lane2 no comprimento1
                        obj241.dta[iahora, 1, 2, ind1] = obj241.dta[iahora, 1, 2, ind1] + obj241.dta[iahora, 2, 2, ind1]; // soma lane1 e lane2 no comprimento2
                        obj241.dta[iahora, 1, 3, ind1] = obj241.dta[iahora, 1, 3, ind1] + obj241.dta[iahora, 2, 3, ind1]; // soma lane1 e lane2 no comprimento3

                        // soma os 3 comprimentos
                        obj241_vel[ind1] = obj241.dta[iahora, 1, 1, ind1] + obj241.dta[iahora, 1, 2, ind1] + obj241.dta[iahora, 1, 3, ind1];
                    }

                    //GravaVelocidade(obj241, obj241_vel);

                    for (int ind1=2;ind1<=14;ind1++) // soma as velocidades para a tabela de comprimento
                    {
                        obj241.dta[iahora, 1, 1, 1] = obj241.dta[iahora, 1, 1, 1] + obj241.dta[iahora, 1, 1, ind1]; // comprimento 1
                        obj241.dta[iahora, 1, 2, 1] = obj241.dta[iahora, 1, 2, 1] + obj241.dta[iahora, 1, 2, ind1]; // comprimento 2
                        obj241.dta[iahora, 1, 3, 1] = obj241.dta[iahora, 1, 3, 1] + obj241.dta[iahora, 1, 3, ind1]; // comprimento 3
                    }
                   
                   // GravaComprimento(obj241);


                } // fim do while
            }



        } //  Fim Processa6Linhas





            
        // *****************************************************************

        private Boolean VerificaSeTabelaExiste(string strbanco, string strtab)
        {
            string atab = "";

            ConectaBancoClassfic();

           // strtab = "tbcomphoradia";
            //comcfc1.CommandText = "select count(*) from " + strtab;
            //comcfc1.CommandText += "where table_schema =" + strbanco;
            // comcfc1.CommandText += "where table_name =" + strtab;

            //"SELECT Count(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'MyTable'"

            // comcfc1.CommandText = "select * from tbvelhoradiaRJ106KM31";
            //  comcfc1.CommandText = "select Rod from " +  strtab ;


            //comcfc1.CommandText = "select count(*) from INFORMATION_SCHEMA.tables ";
            //comcfc1.CommandText += "where table_name =" + strtab;
            comcfc1.CommandText = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES";
           // comcfc1.CommandText += " WHERE TABLE_SCHEMA = " + strbanco;


              readercfc1 = comcfc1.ExecuteReader();
            //   comcfc1.ExecuteNonQuery();
            strtab = strtab.ToLower();
            while (readercfc1.Read())
            {
                atab = (readercfc1["TABLE_NAME"]).ToString();
                if (atab == strtab)
                {
                    readercfc1.Close();
                    DesconectaBanco();
                    return true;
                }
                
            }
            DesconectaBanco();
            return false;
        }



        // ********************************************

        private void CriaTabelaVelComp(string strtab)
        {
            ConectaBancoClassfic();

           // strtab = "RJ106km15";
            comcfc1.CommandText = "Create table " + strtab + " ";
            comcfc1.CommandText += "(`Rod` varchar(45) NOT NULL, `DI` varchar(45) NOT NULL, ";
            comcfc1.CommandText += "`MO` varchar(45) NOT NULL, `YE` varchar(45) NOT NULL, ";
            comcfc1.CommandText += "`HO` varchar(45) NOT NULL, `Sent` varchar(45) NOT NULL, ";
            comcfc1.CommandText += "`km` varchar(45) NOT NULL, `comp` int(11) NOT NULL, `Vel19` int(11) NOT NULL, ";
            comcfc1.CommandText += "`Vel29` int(11) NOT NULL, `Vel39` int(11) NOT NULL, ";
            comcfc1.CommandText += "`Vel49` int(11) NOT NULL, `Vel59` int(11) NOT NULL, ";
            comcfc1.CommandText += "`Vel69` int(11) NOT NULL, `Vel79` int(11) NOT NULL, ";
            comcfc1.CommandText += "`Vel89` int(11) NOT NULL, `Vel99` int(11) NOT NULL, ";
            comcfc1.CommandText += "`Vel109` int(11) NOT NULL, `Vel119` int(11) NOT NULL, ";
            comcfc1.CommandText += "`Vel129` int(11) NOT NULL, `Vel139` int(11) NOT NULL, ";
            comcfc1.CommandText += "`Vel199` int(11) NOT NULL, ";
            comcfc1.CommandText += "PRIMARY KEY(`Rod`,`DI`,`MO`,`YE`,`HO`,`Sent`,`km`,`comp`) )";

            comcfc1.ExecuteNonQuery();

            DesconectaBanco();

        }

        // ***********************************



        // ********************************************

        private void CriaTabelaVel(string strtab)
        {

            ConectaBancoClassfic();

            comcfc1.CommandText = "Create table " + strtab + " ";
            comcfc1.CommandText += "(`Rod` varchar(45) NOT NULL, `DI` varchar(45) NOT NULL, ";
            comcfc1.CommandText +="`MO` varchar(45) NOT NULL, `YE` varchar(45) NOT NULL, ";
            comcfc1.CommandText +="`HO` varchar(45) NOT NULL, `Sent` varchar(45) NOT NULL, ";
            comcfc1.CommandText +="`km` varchar(45) NOT NULL, `Vel19` int(11) NOT NULL, ";
            comcfc1.CommandText +="`Vel29` int(11) NOT NULL, `Vel39` int(11) NOT NULL, ";
            comcfc1.CommandText +="`Vel49` int(11) NOT NULL, `Vel59` int(11) NOT NULL, ";
            comcfc1.CommandText +="`Vel69` int(11) NOT NULL, `Vel79` int(11) NOT NULL, ";
            comcfc1.CommandText +="`Vel89` int(11) NOT NULL, `Vel99` int(11) NOT NULL, ";
            comcfc1.CommandText +="`Vel109` int(11) NOT NULL, `Vel119` int(11) NOT NULL, ";
            comcfc1.CommandText +="`Vel129` int(11) NOT NULL, `Vel139` int(11) NOT NULL, ";
            comcfc1.CommandText +="`Vel199` int(11) NOT NULL, ";
            comcfc1.CommandText += "PRIMARY KEY(`Rod`,`DI`,`MO`,`YE`,`HO`,`Sent`,`km`) )";

            comcfc1.ExecuteNonQuery();

            DesconectaBanco();

        }

        // ***********************************


        // ********************************************

        private void CriaTabelaComp(string strtab)
        {
            ConectaBancoClassfic();

            comcfc1.CommandText = "Create table " + strtab + " ";
            comcfc1.CommandText += "(`Rod` varchar(45) NOT NULL, `DI` varchar(45) NOT NULL, ";
            comcfc1.CommandText += "`MO` varchar(45) NOT NULL, `YE` varchar(45) NOT NULL, ";
            comcfc1.CommandText += "`HO` varchar(45) NOT NULL, `Sent` varchar(45) NOT NULL, ";
            comcfc1.CommandText += "`km` varchar(45) NOT NULL, `comp1` int(11) NOT NULL, ";
            comcfc1.CommandText += "`comp2` int(11) NOT NULL, `comp3` int(11) NOT NULL, ";           
            comcfc1.CommandText += "PRIMARY KEY(`Rod`,`DI`,`MO`,`YE`,`HO`,`Sent`,`km`) )";

            comcfc1.ExecuteNonQuery();

            DesconectaBanco();

        }



        // ***********************************

        private void GravaVelocidadeComp(RegTamVel2 obj241, int[] obj241_vel, string strtab, int lane)
        {
            ConectaBancoClassfic();

            // string strtab = "tbvelhoradia" + obj241.aRod + "km" + obj241.oKm;
            //strtab= "tbvelhoradia" + "RJ106" + "km" + obj241.oKm;

            //  soma os 3 comprimentos
            // obj241_vel[ind1] = obj241.dta[iahora, 1, 1, ind1] + obj241.dta[iahora, 1, 2, ind1] + obj241.dta[iahora, 1, 3, ind1];
            int iahora = Int32.Parse(obj241.aHora);

            for (int ind1 = 1; ind1 <= 3; ind1++)
            {
                comcfc1.CommandText = "insert into " + strtab + " (Rod, DI, MO, YE, HO, Sent, km, comp, Vel19, Vel29, Vel39,";
                comcfc1.CommandText = comcfc1.CommandText + " Vel49, Vel59, Vel69, Vel79, Vel89, Vel99,";
                comcfc1.CommandText = comcfc1.CommandText + " Vel109, Vel119, Vel129, Vel139, Vel199) ";
                comcfc1.CommandText = comcfc1.CommandText + "Values (" + "'" + obj241.aRod + "'" + "," + "'" + obj241.oDa + "'" + "," + "'" + obj241.oMes + "'" + "," + "'" + obj241.oAno + "'" + "," + "'" + obj241.aHora + "'" + ",";
                comcfc1.CommandText = comcfc1.CommandText + "'" + obj241.oStrSent + "'" + "," + "'" + obj241.oKm + "'" + "," + ind1 + "," + obj241.dta[iahora, lane, ind1, 1] + "," + obj241.dta[iahora, lane, ind1, 2] + "," + obj241.dta[iahora, lane, ind1, 3] + "," + obj241.dta[iahora, lane, ind1, 4] + ",";
                comcfc1.CommandText = comcfc1.CommandText + obj241.dta[iahora, lane, ind1, 5] + "," + obj241.dta[iahora, lane, ind1, 6] + "," + obj241.dta[iahora, lane, ind1, 7] + "," + obj241.dta[iahora, lane, ind1, 8] + "," + obj241.dta[iahora, lane, ind1, 9] + ",";
                comcfc1.CommandText = comcfc1.CommandText + obj241.dta[iahora, lane, ind1, 10] + "," + obj241.dta[iahora, lane, ind1, 11] + "," + obj241.dta[iahora, lane, ind1, 12] + "," + obj241.dta[iahora, lane, ind1, 13] + "," + obj241.dta[iahora, lane, ind1, 14] + ")";
                comcfc1.ExecuteNonQuery();
            }
            
            DesconectaBanco();

        }

        // ***********************************







        // ***********************************
        private void GravaVelocidade(RegTamVel2 obj241, int[] obj241_vel, string strtab)
            {
                ConectaBancoClassfic();

               // string strtab = "tbvelhoradia" + obj241.aRod + "km" + obj241.oKm;
                //strtab= "tbvelhoradia" + "RJ106" + "km" + obj241.oKm;
                                   
                comcfc1.CommandText = "insert into " + strtab + " (Rod, DI, MO, YE, HO, Sent, km, Vel19, Vel29, Vel39,";
                comcfc1.CommandText = comcfc1.CommandText + " Vel49, Vel59, Vel69, Vel79, Vel89, Vel99,";
                comcfc1.CommandText = comcfc1.CommandText + " Vel109, Vel119, Vel129, Vel139, Vel199) ";
                comcfc1.CommandText = comcfc1.CommandText + "Values (" + "'" + obj241.aRod + "'" + "," + "'" + obj241.oDa + "'" + "," + "'" + obj241.oMes + "'" + "," + "'" + obj241.oAno + "'" + "," + "'" + obj241.aHora + "'" + "," ;
                comcfc1.CommandText = comcfc1.CommandText + "'" + obj241.oStrSent + "'" + "," + "'" + obj241.oKm + "'" + "," + obj241_vel[1] + "," + obj241_vel[2] + "," + obj241_vel[3] + "," + obj241_vel[4] + "," ;
                comcfc1.CommandText = comcfc1.CommandText + obj241_vel[5] + "," + obj241_vel[6] + "," + obj241_vel[7] + "," + obj241_vel[8] + "," + obj241_vel[9] + ",";
                comcfc1.CommandText = comcfc1.CommandText + obj241_vel[10] + "," + obj241_vel[11] + "," + obj241_vel[12] + "," + obj241_vel[13] + "," + obj241_vel[14] +")";
                comcfc1.ExecuteNonQuery();
                       
                DesconectaBanco();   
            }
        // ***********************************







        private void GravaComprimento(RegTamVel2 obj241, string strtab, int lane)
        {
            int iahora = Int32.Parse(obj241.aHora);
            ConectaBancoClassfic();

            comcfc1.CommandText = "insert into " + strtab + "(Rod, DI, MO, YE, HO, Sent, km, comp1, comp2, comp3)";
             comcfc1.CommandText = comcfc1.CommandText + "Values (" + "'" + obj241.aRod + "'" + "," + "'" + obj241.oDa + "'" + "," + "'" + obj241.oMes + "'" + "," + "'" + obj241.oAno + "'" + "," + "'" + obj241.aHora + "'" + ",";
            comcfc1.CommandText = comcfc1.CommandText + "'" + obj241.oStrSent + "'" + "," + "'" + obj241.oKm + "'" + "," + obj241.dta[iahora, lane, 1, 1] + "," + obj241.dta[iahora, lane, 2, 1] + "," + obj241.dta[iahora, lane, 3, 1] + ")";
            comcfc1.ExecuteNonQuery();

            DesconectaBanco();
        }

        private void FrmEntraArquivosVelComp_Load(object sender, EventArgs e)
        {
            string aRod = "";
            string okm = "";

            C_pontoCL objPontV = new C_pontoCL();


            Form1 objIni = new Form1();

            objIni.CarregaPontoClassVez(objPontV); // carrega o ponto da clasificação a ser processada da Vez
            aRod = objPontV.Rod;    okm = objPontV.km;

            ConectaBancoClassfic();

            comcfc1.CommandText = "select psimples from tbpontos where Rod =" + "'" + aRod + "'" + " and km=" + "'" + okm + "'";
            
            readercfc1 = comcfc1.ExecuteReader();

            if (readercfc1.Read())
            {

                if (Int32.Parse(readercfc1["psimples"].ToString()) == 1)
                {
                    btnCarrega.Enabled = false;
                    btnCarrega.Visible = false;
                    btnPSimples.Enabled = true;
                    btnPSimples.Visible = true;
                    btnPSimples.Location = new Point(22, 83);
                }
                else
                {
                    btnCarrega.Enabled = true;
                    btnCarrega.Visible = true;
                    btnPSimples.Enabled = false;
                    btnPSimples.Visible = true;
                    // btnPSimples.Location = new Point(22, 83);
                }
            }
            comcfc1.Dispose();

           DesconectaBancoClassfic();

            objPontV = null;
            objIni = null;

            
        }

        private void btnPSimples_Click(object sender, EventArgs e)
        {
            string str1 = "";
            foreach (String item in lstBox1.SelectedItems)
            {

                str1 = item;
                //   MessageBox.Show(item);
                // processa cada item que é o arquivo prn

                //  Processa6Linhas(str1); // São 3 linhas de comprimento na lane1 e 3 linhas de comprimento na lane2

                Processa6LinhasEArquivoPSimples(str1); // São 3 linhas de comprimento na lane1 e 3 linhas de comprimento na lane2
            }
            MessageBox.Show("Fim Carga de arquivos");



        }









// ***********************************************************************
// **************************************************************************



        private void btnAcVeComp_Click(object sender, EventArgs e)
        {
            // acerta arquivos de velocidade e comprimento para 14 vel x 3 comp x 2 lanes
            // em arquivos que erroneamente foram programados como  14 vel  x 2 lanes x 3 comp 

            int ind1 = 0;
            string str1 = "";
            foreach (String item in lstBox1.SelectedItems)
            {
                ind1++;
                str1 = item;
                //   MessageBox.Show(item);
                // processa cada item que é o arquivo prn

                //  Processa6Linhas(str1); // São 3 linhas de comprimento na lane1 e 3 linhas de comprimento na lane2

                Converte14v2lanes3compEm14v3comp2lanes(str1,ind1); // São 3 linhas de comprimento na lane1 e 3 linhas de comprimento na lane2
            }
            MessageBox.Show("Fim Conversão de arquivos");




        }








       

        private void Converte14v2lanes3compEm14v3comp2lanes(string str1, int index1)
        {
            string[] linhaA = new string[7];
            var fileStream = new FileStream(str1, FileMode.Open, FileAccess.Read);




            //  C:\RR\Classificações2020\RJ-186\km 28\Dados2
            var path1 = @"C:\RR\Classificações2020\RJ-186\km 28\Dados2\data" + index1.ToString() + ".prn";
            StreamWriter fs = new StreamWriter(path1);

            Boolean continua = true;
            int ind2 = 0;

            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                for (int ind1=1; ind1<=6;ind1++)
                {  linhaA[ind1]= streamReader.ReadLine(); // lê as 6 primeiras linhas
                }

                fs.WriteLine(linhaA[1]); fs.WriteLine(linhaA[2]); fs.WriteLine(linhaA[3]); fs.WriteLine(linhaA[4]);
                fs.WriteLine(linhaA[6]); fs.WriteLine(linhaA[5]);

                ind2 = 1;
               
                
                
                while (continua)
                {
                 linhaA[ind2] = streamReader.ReadLine();

                   if ((linhaA[ind2].Substring(0,2))=="06" )
                    {
                            ind2++;
                            if (ind2<=6)
                            {
                                //linhaA[ind2] = streamReader.ReadLine();
                            }
                            else
                            {
                                ind2 = 1;
                            // escreve os 6 registros no arquivo de saída com a ordem desejada
                            fs.WriteLine(linhaA[1]); fs.WriteLine(linhaA[3]); fs.WriteLine(linhaA[5]); 
                            fs.WriteLine(linhaA[2]); fs.WriteLine(linhaA[4]); fs.WriteLine(linhaA[6]);
                        }
                        
                    }
                      else
                    { 
                        continua = false;
                        fs.Close();
                    }
                    

                }

                streamReader.Close();




            }
        }

        private void lstBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
