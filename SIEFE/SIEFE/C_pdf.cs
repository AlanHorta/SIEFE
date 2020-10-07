using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;//E A BIBLIOTECA ITEXTSHARP E SUAS EXTENÇÕES
using iTextSharp.text;//ESTENSAO 1 (TEXT)
using iTextSharp.text.pdf;//ESTENSAO 2 (PDF)



using System.Windows.Forms;

using System.Windows.Forms.DataVisualization.Charting;



namespace SIEFE
{
    class C_pdf
    {
        public void GeneratePDF( Chart chart1 )
        {

            //


            Document doc = new Document(PageSize.A4);//criando e estipulando o tipo da folha usada
            doc.SetMargins(40, 40, 40, 80);//estibulando o espaçamento das margens que queremos
            doc.AddCreationDate();//adicionando as configuracoes

            //caminho onde sera criado o pdf + nome desejado
            //OBS: o nome sempre deve ser terminado com .pdf
            string caminho = @"C:\Temp\" + "CONTRATO.pdf";

            //criando o arquivo pdf embranco, passando como parametro a variavel                
            //doc criada acima e a variavel caminho 
            //tambem criada acima.
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.Create));

            doc.Open();

            //criando uma string vazia
            string dados = "";

            //criando a variavel para paragrafo
            Paragraph paragrafo = new Paragraph(dados, new Font(Font.NORMAL, 14));
           

            //etipulando o alinhamneto
            paragrafo.Alignment = Element.ALIGN_JUSTIFIED;
            //Alinhamento Justificado

            //AQUI ONDE VAMOS ADICIONAR A VARIAVEL DO TIPO "Font"
            paragrafo.Font = new Font(Font.NORMAL, 14, (int)System.Drawing.FontStyle.Bold);

            //adicioando texto
            paragrafo.Add("TESTE TESTE TESTE");
            //acidionado paragrafo ao documento
            doc.Add(paragrafo);
       
            paragrafo.Clear();
            //AQUI ONDE VAMOS ADICIONAR A VARIAVEL DO TIPO "Font"
            paragrafo.Font = new Font(Font.NORMAL, 12, (int)System.Drawing.FontStyle.Regular);

            //adicioando texto
            paragrafo.Add("teste2 teste2 teste2");
            //acidionado paragrafo ao documento
            doc.Add(paragrafo);


            using (MemoryStream stream = new MemoryStream())
            {
                chart1.SaveImage(stream, ChartImageFormat.Png);
                iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(stream.GetBuffer());
                chartImage.ScalePercent(75f);
                for (int IntLocI = 1; IntLocI <= 1; IntLocI++)
                {
                    doc.Add(chartImage);
                }

            }

                //fechando documento para que seja salva as alteraçoes.
                doc.Close();

            //Abrindo o arquivo após cria-lo.
            System.Diagnostics.Process.Start(caminho);




            //

            

        }



        /// <summary>
        /// //////////////////////////////////////////////////////////
        /// </summary>
        /// 


     








    }
}

