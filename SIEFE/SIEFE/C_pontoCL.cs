using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIEFE
{
   public class C_pontoCL
    {
        public string Rod;
        public string km;
        public string municipio; 
        public string munA;
        public string munB;
        public int sAB;
        public int sBA;
        public int ntfaixas;
        public int nfxsent;             //'Número de faixas por sentido'
        public int qtdclass;     // Número de classificações a serem feitas no ponto. Serve para medição.
                                 // quantidade de classificações a serem feitas no ponto. 1 ou 2. 
                                 //Se a pista for dupla( 4 faixas com 2 por sentido), então conta como 2. 
                                 //Se for só 1 sentido, 2 faixas, então como 1 classificação.  
                                 // Se for pista simples,2 faixas, 1 faixa por sentido, então conta como 1 classificação.
        public int qtdcroquis;
        public int psimples;     // Se 1 então a pista é simples, se 0 então a pista é a pista é dupla
        public string periodo;


    }
}
