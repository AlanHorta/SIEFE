using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIEFE
{
    class C_Placas
    {

        public string Rodovia;
        public string kmEdital;
        public string MunSen;



        //string[] aRod = new string[50];

        public string[] placa = new string[31];             
        public int[] dist= new int[31] ;
        public string[] St = new string[31];  // sentido para onde a placa
        public string[] cant = new string[31];
        public string[] status= new string[31]; // se é inclusão, remoção ou reposicionamento;
        public int[] dta = new int[31]; // se é reposicionamento, então tem distância amtiga, se não é zero
    }
}
