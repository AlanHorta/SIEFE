using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using Excel= Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Printing;
//using Spire.Xls;
//using OfficeOpenXml;
//using OfficeOpenXml.Style;
//using ClosedXML.Excel;

// using GemBox.Spreadsheet;




namespace SIEFE
{
    public class C_excel
    {
        // Store the Excel processes before opening.




        // Set license key to use GemBox.Spreadsheet in Free mode.
      //  SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");



        string path = "";
       // _Application excel = new _Excel.Application();
        Application excel = new Excel.Application();


       // private ClosedXML.Excel.XLWorkbook wb = new ClosedXML.Excel.XLWorkbook();
        private Workbook wb;



        private Worksheet ws;
        Excel.Sheets sheets = null;

        Excel.Range range;

        public void AbreExcel(string path, int sheet)
        {
            this.path = path;
            
            this.wb = excel.Workbooks.Open(path);
            sheets = wb.Sheets;
            this.ws = excel.Worksheets[sheet];

            excel.Visible = true;
            excel.DisplayAlerts = false;

            

           // this.wb = excel.Workbooks.Application.DisplayAlerts = false;

            ////System.Runtime.InteropServices.Marshal.ReleaseComObject(wb);
            ////System.Runtime.InteropServices.Marshal.ReleaseComObject(ws);
            ////System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);


        }



        public void AbreExcel(string path, string sheet)
        {
            this.path = path;

            this.wb = excel.Workbooks.Open(path);
            sheets = wb.Sheets;
            this.ws = excel.Worksheets[sheet];

            excel.Visible = true;
            excel.DisplayAlerts = false;

        }



        public void EncerraProcessos()
        {
            System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
            foreach (System.Diagnostics.Process p in process)
            {
                if (!string.IsNullOrEmpty(p.ProcessName))
                {
                    try
                    {
                        p.Kill();
                    }
                    catch { }
                }
            }

        }



        public void FechaExcel()
        {
            foreach (Excel.Worksheet sheet in sheets)
            {
                //Console.WriteLine(sheet.Name);
                Marshal.ReleaseComObject(sheet);
            }

        }


        public void SelecionaPagina(int sheet)
        {

            this.ws = this.wb.Worksheets[sheet];
            wb.Worksheets[sheet].Activate();

            excel.Visible = true;
            //this.wb.Worksheets[sheet].ActiveView.Orientation = PageOrientation.Landscape;
            

        }


        public void Alturalinha(string sheet)
        {

            this.ws = this.wb.Worksheets[sheet];
            this.ws.Rows.RowHeight = 16;
        }


        public void SelecionaPagina(string sheet)
        {

            this.ws = this.wb.Worksheets[sheet];
            wb.Worksheets[sheet].Activate();

            excel.Visible = true;
            //this.wb.Worksheets[sheet].ActiveView.Orientation = PageOrientation.Landscape;


        }

        public void RemovePagina(int sheet)
        {

            this.ws = this.wb.Worksheets[sheet];
            wb.Worksheets[sheet].Delete();
           // ((Excel.ws)this.wb.Worksheets[sheet]).Delete();
                    

            // excel.Visible = true;


        }

        public void MudaFonte(int i, int j, int sheet, string estilo, int tamanho)
        {
            // Get ASCII character.
            char car1 = (char)(j + 96);
            String scar1 = "";
            scar1 = car1 + i.ToString();

            Excel.Range formatRange;

            //estilo= "Arial Black";

            // formatRange = this.ws.get_Range("i33", "i33");
            formatRange = this.ws.get_Range(scar1, scar1);
            formatRange.Font.Name = estilo;
            formatRange.Font.Size = tamanho;
            //this.ws.Cells[i, j].Value2 = "█";
            //////this.ws.Range[i, j].Style.Font.Size = 20;
            //////this.ws.Range[i, j].Style.Font.IsBold = true;

        }




        public void MudaFonte(int i, int j, string sheet, string estilo, int tamanho)
        {
            // Get ASCII character.
            char car1 = (char)(j + 96);
            String scar1 = "";
            scar1 = car1 + i.ToString();

            Excel.Range formatRange;

            //estilo= "Arial Black";

            // formatRange = this.ws.get_Range("i33", "i33");
            formatRange = this.ws.get_Range(scar1, scar1);
            formatRange.Font.Name = estilo;
            formatRange.Font.Size = tamanho;
            //this.ws.Cells[i, j].Value2 = "█";
            //////this.ws.Range[i, j].Style.Font.Size = 20;
            //////this.ws.Range[i, j].Style.Font.IsBold = true;

        }




        public void MudaFonte(int i, int j, string sheet, string estilo, Boolean Neg, int tamanho)
        {
            // Get ASCII character. 
            char car1 = (char)(j + 96);
            String scar1 = "";
            scar1 = car1 + i.ToString();

            Excel.Range formatRange;

            //estilo= "Arial Black";

            // formatRange = this.ws.get_Range("i33", "i33");
            formatRange = this.ws.get_Range(scar1, scar1);
            formatRange.Font.Name = estilo;
            formatRange.Font.Size = tamanho;
            if (Neg) { formatRange.Font.Bold = true; } else { formatRange.Font.Bold = false; }

            //this.ws.Cells[i, j].Value2 = "█";
            //////this.ws.Range[i, j].Style.Font.Size = 20;
            //////this.ws.Range[i, j].Style.Font.IsBold = true;

        }


        public void MudaFonte(int i, int j, int sheet, string estilo, Boolean Neg, int tamanho)
        {
            // Get ASCII character. 
            char car1 = (char)(j + 96);
            String scar1 = "";
            scar1 = car1 + i.ToString();

            Excel.Range formatRange;

            //estilo= "Arial Black";

            // formatRange = this.ws.get_Range("i33", "i33");
            formatRange = this.ws.get_Range(scar1, scar1);
            formatRange.Font.Name = estilo;
            formatRange.Font.Size = tamanho;
            if (Neg) { formatRange.Font.Bold = true; } else { formatRange.Font.Bold = false; }

            //this.ws.Cells[i, j].Value2 = "█";
            //////this.ws.Range[i, j].Style.Font.Size = 20;
            //////this.ws.Range[i, j].Style.Font.IsBold = true;

        }

        public void CorFundo(int i, int j, int sheet, string aCor)
        {
            char car1 = (char)((j) + 96);
            String scar1 = "";
            scar1 = car1 + i.ToString();

            char car2 = (char)(17 + 96);  // é coluna p do Excel
            String scar2 = "";
            scar2 = car2 + i.ToString();

            Excel.Range formatRange;

            switch (aCor)
            {
              case "Red":
                {
                    this.ws.get_Range(scar1, scar2).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                    break;
                }
              case "PBlue":
                {
                    this.ws.get_Range(scar1, scar2).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PowderBlue);
                    break;
                }

             case "White":
                {
                    this.ws.get_Range(scar1, scar2).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    break;
                }
            }


            // formatRange = this.ws.get_Range(scar1, scar1).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
          
           

        }


        public void selecionaCor(string cor)
        {

        }



        public void MudaCor(string Range1, string cor)
        {
           // this.ws.Range[i, j].Style.Font.Color = Color.Gray;
            //if (cor=="Gray")  this.ws.Cells[i, j].Style.Font.Color = Color.Gray;
            //if (cor == "Black") this.ws.Cells[i, j].Style.Font.Color = Color.Black;

            //if (cor == "Gray") this.ws.Range[Range1].Style.Font.Color = Color.Gray;
            //if (cor == "Black") this.ws.Range[Range1].Style.Font.Color = Color.Black;

            // "O59:P59"
            char ch1;
            int linha = 0;
            int coluna = 0;
            Boolean dezena = false;  // Se linha >=10 então dezena é true
            int tam1 = 0;


            
            ch1 = Range1[0];
            try
            { coluna = (byte)ch1 - 64; }
            catch
            {
                System.Windows.Forms.DialogResult result;
                result = System.Windows.Forms.MessageBox.Show("Não foi possível converter a linha");
            }


            if (Range1.Substring(3, 1) == ":") tam1 = 2;
            else tam1 = 1;
                   
               
            linha = Int16.Parse( Range1.Substring(1, tam1));
               

           

           

            Microsoft.Office.Interop.Excel.Range rng = (Microsoft.Office.Interop.Excel.Range)this.ws.Cells[linha, coluna];
          //  rng.Value = cor;

            if (cor == "Black") rng.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            if (cor == "Gray") rng.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray);
            //rng.Characters[7, 9].Font.Color = Color.Red;

        }
          



        public void WriteToCell (int i, int j, string s)
        {
            this.ws.Cells[i, j].Value2 = s;
           
        }

        public void WriteToCell(int i, int j, double s)   // polimorfismo
        {
            this.ws.Cells[i, j].Value2 = s;

        }


        public void RenameSheet(int sheet)
        {
            SelecionaPagina(sheet);
            this.ws.Name="p" + sheet.ToString();

        }



        public void RenomeaPlanilha(int sheet, string dia, string mes)
        {
            SelecionaPagina(sheet);
            //this.ws.Name = "p" + sheet.ToString();
            this.ws.Name = dia + "-" + mes;

        }





        public void WriteToCellD(int i, int j, double doub1)
        {
            this.ws.Cells[i, j].Value2 = doub1;

        }

        public void Desagrupar(int i,int j)
        {
            this.ws.Cells[i,j].UnMerge();
        }

        public void Desagrupar(int i, int j, int i2, int j2)
        {
            this.ws.Range[ws.Cells[i, j], ws.Cells[i2, j2]].UnMerge();
            
        }


        public void Agrupar(int i, int j, int i2, int j2)
        {
            //this.ws.Cells.Range[i,j][i2,j2].Merge();
            this.ws.Range[ws.Cells[i, j], ws.Cells[i2, j2]].Merge();           

        }


        public void Alinhar2(int i, int j, string alinha)
        {
            char ch1= (char)((j) + 64);
            String scar1 = "";
            scar1 = ch1 + i.ToString();



            //this.ws.Range[scar1].Style.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            // this.ws.get_Range("F46","F46").Style.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            // this.ws.Cells[46,6].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            

            switch (alinha)
            {
                case "Center":
                    {
                        this.ws.Cells[i, j].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        break;
                    }

                case "Left":
                    {
                        this.ws.Cells[i, j].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;                       
                        break;
                    }

                case "Right":
                    {
                        this.ws.Cells[i, j].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;                       
                        break;
                    }
            }






        }



        public void Alinhar(int i, int j, string alinha)
        {
            //string startRange = "A1";
            //string endRange = "A1";

           
            Excel.Range c1 = this.ws.Cells[i, j];
            Excel.Range c2 = this.ws.Cells[i, j];

            range = (Excel.Range)this.ws.get_Range(c1, c2);
           

            
            switch (alinha)
            {
                case "Center":
                    {                      
                        range.EntireRow.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        break;
                    }

                case "Left":
                    {                        
                        range.EntireRow.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        break;
                    }

                case "Right":
                    {
                        range.EntireRow.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                        break;
                    }


            }

            

        }


        // *********************************


        public void SomaCels(int i, int j )
        {

            // pprimeira posição i=86 e j=3


            char car1 = (char)((j) + 96);
            String scar1 = "";
            scar1 = car1 + (i-74).ToString();

            char car2 = (char)((j) + 96);
            String scar2 = "";
            scar2 = car2 + (i - 37).ToString();

            // this.ws.Cells[i, j].FormulaLocal = "=Sum(C12+C48)";
            this.ws.Cells[i, j].FormulaLocal = "=(" + scar1 + "+" + scar2 + ")"; 
        }


        //**********************************





        // *********************************


        public void SomatorioCels(int i, int j)
        {
            // na primeira chamada i=84 e j= 3
            char car1 = 'c';
            String scar1 = "";
            scar1 = car1 + (i).ToString();

            char car2 = 'p';
            String scar2 = "";
            scar2 = car2 + (i).ToString();

            // this.ws.Cells[i, j].FormulaLocal = "=Sum(C12+C48)";
            this.ws.Cells[i, j].FormulaLocal = "=Soma(" + scar1 + ":" + scar2 + ")";
        }


        //**********************************



        // *********************************


        public void SomatorioCelsComp(int i, int j)
        {
            // pprimeira posição i=86 e j=12
            char car1 = 'c';
            String scar1 = "";
            scar1 = car1 + (i).ToString();

            char car2 = 'k';
            String scar2 = "";
            scar2 = car2 + (i).ToString();

            // this.ws.Cells[i, j].FormulaLocal = "=Sum(C12+K12)";
            this.ws.Cells[i, j].FormulaLocal = "=Soma(" + scar1 + ":" + scar2 + ")";
        }


        //**********************************







        public void LarguraColunaExcel(int sheet, int coluna, double largura)
        {
            this.ws = this.wb.Worksheets[sheet];
          //  wb.Worksheets[sheet].Width(coluna, largura);     //.SetColumnWidth(coluna, largura);
            wb.Worksheets[sheet].Columns[coluna].ColumnWidth = largura;
        }



        public void InsereChk(int i, int j)
        {
            // Get ASCII character.
            char car1 = (char)(j+64);
            String scar1 = "";
            scar1 = car1 + i.ToString();
                
                Excel.Range formatRange;

            // formatRange = this.ws.get_Range("i33", "i33");
            formatRange = this.ws.get_Range(scar1, scar1);
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
            formatRange.Font.Size = 11;
            
            formatRange.Font.FontStyle = "AmdtSymbols";
            this.ws.Cells[i, j].Value2 = "█  ";
            //////this.ws.Range[i, j].Style.Font.Size = 20;
            //////this.ws.Range[i, j].Style.Font.IsBold = true;
        }


        public void InsereChkB(int i, int j)
        {
            // Get ASCII character.
            char car1 = (char)(j + 96);
            String scar1 = "";
            scar1 = car1 + i.ToString();

            Excel.Range formatRange;

            formatRange = this.ws.get_Range(scar1, scar1);
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Font.Size = 22;
            //this.ws.Cells[i, j].Value2 = "█";
            this.ws.Cells[i, j].Value2 = "□";
          

            //////this.ws.Range[i, j].Style.Font.Size = 20;
            //////this.ws.Range[i, j].Style.Font.IsBold = true;
        }


        public void Save()
        {
            this.wb.Save();
        }

        public void SaveAs (string path)
        {
            this.wb.SaveAs(path);
        }

        public void Fecha()
        {
          
            try
            { 

            if (this.wb != null)
            {
                this.wb.Close();
                excel.Quit();
            }
            }
            catch { }
        }

        
        public void FechaAlgumExcel(string arq1)
        {
            //this.path = path;

            this.wb = excel.Workbooks.Add(arq1);
            //sheets = wb.Sheets;
            //this.ws = excel.Worksheets[1];

            //excel.Visible = true;
            //excel.DisplayAlerts = false;

            //this.wb.Close();

            this.wb.Close(true);
            excel.Quit();

            //try
            //{

            //    if (this.wb != null)
            //    {
                    
            //    }
            //}
            //catch { }

        }


        public string LeEx(int i, int j, int sheet)
        {

            string str1;
            SelecionaPagina(sheet);
            str1= (this.ws.Cells[i, j].Value2);
            return str1;

            // WriteToCell(i, j, str1);

        }

        public double LeExInt(int i, int j, int sheet)
        {

            double num1;
            SelecionaPagina(sheet);

            try
            {
                num1 = (this.ws.Cells[i, j].Value2);
            }

            catch
            {
                num1 = 0.0;
            }
            return num1;

            // WriteToCell(i, j, str1);

        }




        public double LeExDb(int i, int j, int sheet)
        {

            double num1;
            SelecionaPagina(sheet);

            try
            {
                num1 = (this.ws.Cells[i, j].Value2);
            }

            catch
            {
                num1 = 0.0;
            }
            return num1;

            // WriteToCell(i, j, str1);

        }




        public void Pagina(int i, int j, int sheet, string str1)
        {
            SelecionaPagina(sheet);

            WriteToCell(i, j, str1);
          //  MudaFonte(i, j, sheet, "Arial", 10);

        }

        public void Pagina(int i, int j, string sheet, string str1)
        {
            SelecionaPagina(sheet);

            WriteToCell(i, j, str1);
            //  MudaFonte(i, j, sheet, "Arial", 10);
        }


        public void Pagina(int i, int j, int sheet, double oint1)   // polimorfismo
        {
            SelecionaPagina(sheet);

            WriteToCell(i, j, oint1);
            //  MudaFonte(i, j, sheet, "Arial", 10);

        }

        //
        public void Pagina(int i, int j, int sheet, string str1, int size1, string cor)
        {
            SelecionaPagina(sheet);

            WriteToCell(i, j, str1);
            MudaFonte(i, j, sheet, "Arial", size1);

            if (cor == "Gray") this.ws.Cells[i,j].Style.Font.Color = Color.Gray;
            if (cor == "Black") this.ws.Cells[i,j].Style.Font.Color = Color.Black;
            MudaFonte(i, j, sheet, "Arial", size1);
        }

        //

        //
        public void Pagina(int i, int j, string sheet, string str1, int size1, string cor)
        {
            SelecionaPagina(sheet);

            WriteToCell(i, j, str1);
            MudaFonte(i, j, sheet, "Arial", size1);

            if (cor == "Gray") this.ws.Cells[i, j].Style.Font.Color = Color.Gray;
            if (cor == "Black") this.ws.Cells[i, j].Style.Font.Color = Color.Black;
            MudaFonte(i, j, sheet, "Arial", size1);
        }

        //

       
        //

        public void PaginaD(int i, int j, int sheet, double doub1)
        {
            SelecionaPagina(sheet);

            WriteToCellD(i, j, doub1);

        }

    }
}
