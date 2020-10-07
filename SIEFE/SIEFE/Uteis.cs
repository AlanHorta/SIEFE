using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SIEFE
{
    public class Uteis
    {
        private String aLat;
        private String aLong;

        public Random random = new Random();






        //*********************************************************************************************





        public string ConverteCoord(string coord)
        {
            int indgrau = 0;
            int indmin = 0;
            int indseg = 0;
            string ssss1 = "zero";

            coord = coord.Trim();


            indgrau = coord.IndexOf("°");
            indmin = coord.IndexOf("'");
            indseg = coord.IndexOf('"', 0);

            if ((indgrau >= 0) && (indmin >= 0) && (indseg >= 0))
            {


                coord = coord.Substring(0, indmin) + " " + coord.Substring(indmin + 1, indseg - 1 - indmin) + " " + coord.Substring(indseg + 1, 1);


                return coord;
            }






            if ((indgrau <= 0) || (coord.Length <= 5))
            {
                return ssss1;
            }

            ssss1 = coord.Substring(0, indmin);
            ssss1 = ssss1 + " " + coord.Substring(indmin + 1, 5);
            ssss1 = ssss1 + " " + coord.Substring(indseg + 1, 1);
            ssss1 = ssss1.Trim();
            return ssss1;
        }

        // *****************************************************************


        public int RandomNumber(int min, int max)
        {
          
            return random.Next(min, max);
        }

        //***********************************************************
        public string ReconverteCoord(string coord)
        {
            int indgrau = 0;
            int indmin = 0;
            int indseg = 0;
            int indsp1 = 0;
            int indsp2 = 0;
            int indsp3 = 0;

            int indponto = 0;
            int len1 = 0;


            int indLatLon = 0;
            string ssss1 = "zero";


            coord = coord.Trim();

            len1 = coord.Length;

            indgrau = coord.IndexOf("°");
            indmin = coord.IndexOf("'", 0);
            indseg = coord.IndexOf('"', 0);

            indsp1 = coord.IndexOf(" ", 0);
            if (indsp1 != -1) { indsp2 = coord.IndexOf(" ", indsp1 + 1); }
            if (indsp2 != -1) { indsp3 = coord.IndexOf(" ", indsp2 + 1); }

            indponto = coord.IndexOf(".", 0);

            //strcoord[1] = "42° 0 7.53 O";
            //               012345678901
            // 22°43 5.60 S
            //strcoord[1] = "22°37 56.92 S";     //22°37'56.92 S
            //               0123456789012

            if (indsp3 != -1)
            {
                // se tem o terceiro espaço
                ssss1 = coord.Substring(0, indsp1) + coord.Substring(indsp1 + 1, (indsp2 - indsp1 - 1)) + "'";
                ssss1 = ssss1 + coord.Substring(indsp2 + 1, (indsp3 - indsp2 - 1)) + '"';
                ssss1 = ssss1 + coord.Substring(indsp3 + 1, 1);
            }
            else
            {
                // se não tem o terceiro espaço
                ssss1 = coord.Substring(0, indsp1) + "'";
                ssss1 = ssss1 + coord.Substring(indsp1 + 1, (indsp2 - indsp1 - 1)) + '"';
                ssss1 = ssss1 + coord.Substring(indsp2 + 1, 1);

            }




            return ssss1;
        }


        // ******************************************************************
        string ReplaceAllSpaces(string input)
        {
            StringBuilder builder = new StringBuilder();
            using (StringReader reader = new StringReader(input))
            {
                while (reader.Peek() != -1)
                {
                    char c = (char)reader.Peek();
                    if (char.IsWhiteSpace(c))
                    {
                        while (char.IsWhiteSpace(c))
                        {
                            reader.Read();
                            c = (char)reader.Peek();
                        }
                        builder.Append("%20");
                    }
                    else builder.Append((char)reader.Read());
                }
            }
            return builder.ToString();
        }



        //*****************************************************************
        public string AcertaOuColoca_o_e(string str1)
        {
            string strax = str1;
            int i = 0;
            int ind1 = 0;
            Boolean achou = false;

            if (strax.IndexOf(" e ") == -1)
            {
                // Coloca o " e " antes da ultima

                while ((i = strax.IndexOf(',', i)) != -1)
                {
                    // Print out the substring.
                    // Console.WriteLine(s.Substring(i));

                    // Increment the index.
                    achou = true;
                    ind1 = i; // salva o indice encontrado
                    i++;
                }

                if (achou)
                {
                    strax = str1.Substring(0, (ind1)) + " e" + str1.Substring((ind1 + 1));
                    return strax;
                }



            }


            else
            { return str1; }


            return str1;
        }

        //*******************************************************************


        public String VeTipoEquip(string str1)
        {
            String str2 = "";

            int index1 = str1.IndexOf('.');

            str2 = str1.Substring(0, index1);

            return str2.Trim();

        }



        public String ConvVirgPonto(string str1)
        {
            String str2 = "";

            int index1 = str1.IndexOf(',');
            if (index1 < 1)
            {
                return str1;
            }

            str2 = str1.Substring(0, index1) + "." + str1.Substring(index1 + 1, 1);

            return str2;

        }

        public String ArquivoVirg(string str1)
        {
            // se o km for decimal então troca a virgula por ponto
            str1.Trim();
            int tam = str1.Length;

            if (tam < 5)
            { var result = MessageBox.Show("Nome do arquivo inválido!");

            }
            else
            {
                if ((str1.Substring(tam - 3, 3) == "xls"))
                {
                    str1 = str1.Substring(0, tam - 4);
                    str1 = ConvPontoVirg(str1);
                    str1 = str1 + ".xls";
                    return str1;

                }

                if ((str1.Substring(tam - 4, 4) == "xlsx"))
                {
                    str1 = str1.Substring(0, tam - 5);
                    str1 = ConvPontoVirg(str1);
                    str1 = str1 + ".xlsx";
                    return str1;
                }
            }
            return "ERRO no nome do arquivo Excel!!!";

        }


        public String ConvPontoVirg(string str1)
        {
            //Double dstr1 = Double.Parse(str1);
            //int intokm = Int32.Parse(Math.Truncate(dstr1));


            String str2 = "";
            int qtdP = 0;

            int index1 = str1.IndexOf('.');
            if (index1 < 1)
            {
                return str1;
            }

            try
            {

                if (str1.Substring(index1 + 1, 3) == "xls")
                {
                    return str1;
                }
            }

            catch { }


            str2 = str1.Substring(0, index1) + "," + str1.Substring(index1 + 1, 1);

            return str2;

        }


        // /////////////////////
        // /////////////////////
        // /////////////////////
        public String ExtraiArquivo(string oarq1)
        {
            int tam = 0;
            int ind1 = 0;
           int pos1 = 0;
           string ocarac = "";

            tam = (oarq1.Length);
       
            for (ind1 = (tam-1); ind1>1; ind1--)
            {
                ocarac = oarq1.Substring(ind1, 1);
                if (ocarac.Equals(@"\"))
                {   pos1 = ind1;
                    ind1 = 1; // termina o for
                }
            }
             // ExtraiArquivo = Right(oarq1, (tam - pos1))
             oarq1= oarq1.Substring(pos1 + 1, (tam - (pos1 + 1))    );     
             return oarq1;
        }
        // ///////////////////////
        // ///////////////////////
        // ///////////////////////




        public String FazExtenso(String str1)
        {
            switch (str1) {
                case "1":
                {
                        return "Uma";
                    break;
                }
                case "2":
                    {
                        return "Duas";
                        break;
                    }

                case "3":
                    {
                        return "Três";
                        break;
                    }

                case "4":
                    {
                        return "Quatro";
                        break;
                    }
                case "5":
                    {
                        return "Cinco";
                        break;
                    }
                case "6":
                    {
                        return "Seis";
                        break;
                    }
            }
            return "000000000000000000000000000000000000000000000000000000000000";
        }

        public Boolean RegValido(String str2)
        {
            str2 = str2.Replace(" ", "");

            if (String.IsNullOrEmpty(str2))
            {
                return false;
            }
            return true;

        }

        public string CompletaZeroEsq(String str2)
        {
            string strax = str2;

            if (str2.Length <2) { strax = "00" + str2; return strax; }
            if (str2.Length < 3) { strax = "0" + str2; return strax; }



            return strax;
        }

        public string PoePonto(string str1)
        {
            int ivalor = Int32.Parse(str1);
            int valor2 = 0;

            if (ivalor >= 1000)
            {               
                valor2 = ivalor - ( ((int)(ivalor / 1000)) * 1000);
                str1 = ((int)(ivalor / 1000)).ToString() + ".";
                str1 = str1 + CompletaZeroEsq((valor2).ToString());
            }

            return str1;
        }


        public string DiaSem(string str1)
        {
            switch (str1)
            {
                case "Monday":
                    {
                        return "segunda";
                        break;
                    }
                case "Tuesday":
                    {
                        return "terça";
                        break;
                    }

                case "Wednesday":
                    {
                        return "quarta";
                        break;
                    }

                case "Thursday":
                    {
                        return "quinta";
                        break;
                    }
                case "Friday":
                    {
                        return "sexta";
                        break;
                    }
                case "Saturday":
                    {
                        return "sábado";
                        break;
                    }

                case "Sunday":
                    {
                        return "domingo";
                        break;
                    }
            }

            return "---------------";
        }
        
        
        // ====================================================


        public int NumeroDiaSemana(string str1)
        {
            switch (str1)
            {
                case "Monday":
                    {
                        return 2;
                        break;
                    }
                case "Tuesday":
                    {
                        return 3;
                        break;
                    }

                case "Wednesday":
                    {
                        return 4;
                        break;
                    }

                case "Thursday":
                    {
                        return 5;
                        break;
                    }
                case "Friday":
                    {
                        return 6;
                        break;
                    }
                case "Saturday":
                    {
                        return 7;
                        break;
                    }

                case "Sunday":
                    {
                        return 1;
                        break;
                    }
            }

            return 0;

        }




        public string SemTraço(string str1)
        {
            int tam = str1.Length;
            string str2 = "";
            for (int ind1=0;ind1<tam;ind1++)
            {
                if (str1[ind1]!='-')
                {
                    str2 += str1[ind1];
                }
            }
            return str2;
        }





        // **************************************************
        // **************************************************
        // **************************************************

       
        public string DuasBarras(string str1)
        {
            int vez = 0;
            int tam = str1.Length;
            string str2 = "";


            // + '\x0010' + '\x0043' 

            vez = 0;
            for (int ind1=0;ind1<tam;ind1++)
            {
                str2 += str1[ind1];
               if (str1[ind1] == '\x005C')
                {
                    //vez++;
                    //if (vez==2) 
                    //{ // segunda vez 
                    //    str2 += '\x005C' + '\x005C';
                    //    vez = 0;
                    //}
                    str2 += '\x005C';
                }
            }
            return str2;
        }

        // **************************************************


        // **************************************************
        public string TiraTraco(string str1)
        {
            string str2="";
            int tam = str1.Length;

            for (int ind1=0; ind1<tam; ind1++)
            { if (str1[ind1]=='-') {  }   else { str2 += str1[ind1]; } }

            return str2;

        }
        // **************************************************

        // **************************************************
        public string IncluiZeroEsq(string str1)
        {
            int tam = str1.Length;
            if (tam==1) { str1 = "00" + str1; }
            if (tam == 2) { str1 = "0" + str1; }

            return str1;
        }
        // **************************************************


    }
}
