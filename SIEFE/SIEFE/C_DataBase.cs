using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using MySql.Data.MySqlClient;

namespace SIEFE
{
    class C_DataBase
    {
        string strConFE = @"Server=localhost;Database=fe;Uid=root;Pwd=obuldog67;";
        MySqlConnection mysqlCon1;
        MySqlConnection mysqlCon2;
        MySqlCommand com1;
        MySqlCommand com2;
        MySqlDataReader reader1;
        MySqlDataReader reader2;


        C_Rodovia objRod = new C_Rodovia();
        Uteis cUt = new Uteis();

        C_pontofe objPFe = new C_pontofe();


        public void ConectaBanco()
        {
            using (mysqlCon1 = new MySqlConnection(strConFE)) ;
            {
                mysqlCon1.Open();
            }
        }

        public void DesconectaBanco()
        {
            mysqlCon1.Close();
        }

        //*******************************************************************
        public void TrazKmsPontosFE(ref int nRod,  string aRod, C_Rodovia objRod)
        {   
            nRod = 0;

            ConectaBanco();
            com1 = mysqlCon1.CreateCommand();
            // "select kmEdital from ponto_fe where Rodovia= " + "'" + cmbRodovia.Text + "'";
            com1.CommandText = "select kmEdital from ponto_fe where Rodovia= " + "'" + aRod + "'" ;
            reader1 = com1.ExecuteReader();

            while (reader1.Read())
            {   nRod++;
                objRod.kms[nRod] = reader1["kmEdital"].ToString();               
            }

            reader1.Close();
            DesconectaBanco();
        }



        // **************************************************************** 

            public void LeRegistroAtual(ref string aRod, ref string okm)
        {
            ConectaBanco();
            com1 = mysqlCon1.CreateCommand();
            com1.CommandText = "select * from ponto_featual";

            reader1 = com1.ExecuteReader();

            while (reader1.Read())
            {
                //Console.WriteLine(reader1["Tipo"].ToString());
                aRod = reader1["Rodovia"].ToString();                
                okm = reader1["kmReal"].ToString();
            }
            reader1.Close();
            DesconectaBanco();
        }

        // ********************************************************************

        // **************************************************************** 



        public void LeRegistroPontoFE(ref string aRod, ref string okm, C_pontofe objPFe)
        {
            ConectaBanco();
            com1 = mysqlCon1.CreateCommand();
            com1.CommandText = "select * from ponto_fe where Rodovia= " + "'" + aRod + "'" + " and kmReal= " + "'" + okm + "'";

            reader1 = com1.ExecuteReader();

            while (reader1.Read())
            {
                //Console.WriteLine(reader1["Tipo"].ToString());
                objPFe.Rodovia = reader1["Rodovia"].ToString();
                objPFe.kmEdital = reader1["kmEdital"].ToString();
                objPFe.MunSen = reader1["MunSen"].ToString();
                objPFe.kmReal = reader1["kmReal"].ToString();

                objPFe.Localidade = reader1["Localidade"].ToString();


                objPFe.Municipio = reader1["Municipio"].ToString();
               
                objPFe.QtdFx = Int32.Parse(reader1["QtdFx"].ToString());
                objPFe.MunA = (reader1["MunA"].ToString());
                objPFe.MunB = reader1["MunB"].ToString();
                objPFe.VelFisc = Int32.Parse(reader1["VelFisc"].ToString());
                objPFe.Lat = reader1["Lat"].ToString();
                objPFe.Longit = reader1["Longit"].ToString();
                objPFe.VMD = Int32.Parse(reader1["VMD"].ToString());
                objPFe.Vel85p = Int32.Parse(reader1["Vel85p"].ToString());
                objPFe.Tipo = reader1["Tipo"].ToString();

                objPFe.Lat2 = reader1["Lat2"].ToString();
                objPFe.Longit2 = reader1["Longit2"].ToString();
                objPFe.Vel85pSB = Int32.Parse(reader1["Vel85pSB"].ToString());
               
                objPFe.VmdB = Int32.Parse(reader1["VmdB"].ToString());

                objPFe.Calc85p = Int32.Parse(reader1["Calc85p"].ToString());
               // objPFe.Calc85p = 0;
            }

            reader1.Close();
            DesconectaBanco();
        }

        // ********************************************************************





        // ********************************************************************



        public void GetRodovias(ref int nRod, C_Rodovia objRod)
        {
            string[] aRod = new string[50];
            nRod = 0;

            ConectaBanco();
            com1 = mysqlCon1.CreateCommand();

            com1.CommandText = "select Rodovia from trodovias";
            reader1 = com1.ExecuteReader();



            while (reader1.Read())
            {
                nRod++;
                objRod.Rodovia[nRod] = reader1["Rodovia"].ToString();
                //objRod.MunA[nRod] = reader1["munA"].ToString();
                //objRod.MunB[nRod] = reader1["munB"].ToString();
            }


            reader1.Close();
            DesconectaBanco();
        }


        // ****************************************************************
        public void GetTiposEqp(ref int ntipo,  C_tipoequip objTipoEqp)
        {
             ntipo = 0;
            ConectaBanco();
            com1 = mysqlCon1.CreateCommand();

            com1.CommandText = "select Tipo from tipoequip";
            reader1 = com1.ExecuteReader();

            while (reader1.Read())
            {
                ntipo++;
                objTipoEqp.tipo[ntipo] = reader1["Tipo"].ToString();
            }
            reader1.Close();
            DesconectaBanco();

        }


        // ****************************************************************
        public void GetMunicipios(string strRod, ref string MunA, ref string MunB)
        {
            ConectaBanco();
            com1 = mysqlCon1.CreateCommand();

            //com1.CommandText = "select MunSen from ponto_fe where Rodovia= " + "'" + cmbRodovia.Text + "'" + " and kmEdital= " + cmbEdital.Text;
            com1.CommandText = "select * from trodovias where Rodovia= " + "'" + strRod + "'";
            reader1 = com1.ExecuteReader();

            while (reader1.Read())
            {

                MunA = reader1["munA"].ToString();
                MunB = reader1["munB"].ToString();
            }


            reader1.Close();
            DesconectaBanco();

        }

        // ****************************************************************


        public Boolean TemRegistro(C_pontofe objpfe, string aTab)

        {
            ConectaBanco();
            com1 = mysqlCon1.CreateCommand();


            com1.CommandText = "select * from " + aTab + " where Rodovia = " + "'" + objpfe.Rodovia + "'" + " and kmEdital = " + "'" + cUt.ConvVirgPonto(objpfe.kmEdital.ToString()) + "'";
            com1.CommandText = com1.CommandText + " and MunSen= " + "'" + objpfe.MunSen + "'";

            reader1 = com1.ExecuteReader();

            if (reader1.Read())
            {
                return true;
            }
            else
            { return false; }

            reader1.Close();
            DesconectaBanco();
        }

        //*************************************************************************8

        public void GravapFE(C_pontofe objpfe, string aTab)
        {
            using (var txscope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    using (mysqlCon1 = new MySqlConnection(strConFE))
                    {
                        mysqlCon1.Open();
                        com1 = mysqlCon1.CreateCommand();

                        if (aTab == "ponto_featual")
                        {
                            com1.CommandText = "delete from ponto_featual";     // Limpa a tabela se existir algum risco
                            com1.ExecuteNonQuery();
                            reader1.Close();
                        }
                        else
                        {
                            com1.CommandText = "insert into acidentes(Rodovia,kmEdital,MunSen,kmReal,Abalroamento,Choque,Colisão,Tombamento,Capotamento,Incendio,Atropelamento) ";
                            com1.CommandText = com1.CommandText + " Values (" + "'" + objpfe.Rodovia + "'" + "," + "'" + cUt.ConvVirgPonto(objpfe.kmEdital.ToString()) + "'" + "," + "'" + objpfe.MunSen + "'" + ",";
                            com1.CommandText = com1.CommandText + "'" + cUt.ConvVirgPonto(objpfe.kmReal.ToString()) + "'" + ",0,0,0,0,0,0,0)";
                            com1.ExecuteNonQuery();
                            reader1.Close();

                            com1.CommandText = "insert into fat_risco(Rodovia,kmEdital,MunSen,fatr1,fatr2,fatr3,fatr4,fatr5,fatr6,fatr7,fatr8,fatr9,fatr10) ";
                            com1.CommandText = com1.CommandText + " Values (" + "'" + objpfe.Rodovia + "'" + "," + "'" + cUt.ConvVirgPonto(objpfe.kmEdital.ToString()) + "'" + "," + "'" + objpfe.MunSen + "'" + ",";
                            com1.CommandText = com1.CommandText + "' ',' ',' ',' ',' ',' ',' ',' ',' ',' ')";
                            com1.ExecuteNonQuery();
                            reader1.Close();

                            com1.CommandText = "insert into histmedidas(Rodovia,kmEdital,MunSen,h1,h2,h3,h4,h5) ";
                            com1.CommandText = com1.CommandText + " Values (" + "'" + objpfe.Rodovia + "'" + "," + "'" + cUt.ConvVirgPonto(objpfe.kmEdital.ToString()) + "'" + "," + "'" + objpfe.MunSen + "'" + ",";
                            com1.CommandText = com1.CommandText + "' ',' ',' ',' ',' ')";
                            com1.ExecuteNonQuery();
                            reader1.Close();

                            com1.CommandText = "insert into teduplo(Rodovia,kmEdital,MunSen,EDuplo,MunSen2,Fx1_1,Fx1_2,Fx1_3,Fx2_1,Fx2_2,Fx2_3,NPistas,NFxs) ";
                            com1.CommandText = com1.CommandText + " Values (" + "'" + objpfe.Rodovia + "'" + "," + "'" + cUt.ConvVirgPonto(objpfe.kmEdital.ToString()) + "'" + "," + "'" + objpfe.MunSen + "'" + ",";
                            com1.CommandText = com1.CommandText + "0,' ',0,0,0,0,0,0,0,0)";
                            com1.ExecuteNonQuery();
                            reader1.Close();

                            com1.CommandText = "insert into tgeometria(Rodovia,kmEdital,MunSen,Aclive,Declive,Plano,Curva,Urbano,Pedestre,Paolongo,Ptrans,Ciclista,Caolongo,Ctrans,UmSentido,SA,SB,Forma) ";
                            com1.CommandText = com1.CommandText + " Values (" + "'" + objpfe.Rodovia + "'" + "," + "'" + cUt.ConvVirgPonto(objpfe.kmEdital.ToString()) + "'" + "," + "'" + objpfe.MunSen + "'" + ",";
                            com1.CommandText = com1.CommandText + "0,0,0,0,0,0,0,0,0,0,0,0," + "'" + objpfe.MunB + "'" + "," + "'" + objpfe.MunA + "'" + ",' ')";
                            com1.ExecuteNonQuery();
                            reader1.Close();

                            com1.CommandText = "insert into tjorn(Rodovia,kmEdital,MunSen,semjorn) ";
                            com1.CommandText = com1.CommandText + " Values (" + "'" + objpfe.Rodovia + "'" + "," + "'" + cUt.ConvVirgPonto(objpfe.kmEdital.ToString()) + "'" + "," + "'" + objpfe.MunSen + "'" + ",";
                            com1.CommandText = com1.CommandText + "' ')";

                            com1.ExecuteNonQuery();
                            reader1.Close();

                            com1.CommandText = "insert into tpaginas(Rodovia,kmEdital,MunSen,p1,p2,p3,p4,p5,p6,p7,p8,p9,p10,p11,p12,p13,p14,p15,p16,p17,p18,p19,p20,p21,p22,p23,p24,p25,p26) ";
                            com1.CommandText = com1.CommandText + " Values (" + "'" + objpfe.Rodovia + "'" + "," + "'" + cUt.ConvVirgPonto(objpfe.kmEdital.ToString()) + "'" + "," + "'" + objpfe.MunSen + "'" + ",";
                            com1.CommandText = com1.CommandText + "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0)";
                            com1.ExecuteNonQuery();
                            reader1.Close();

                            com1.CommandText = "insert into tplacas(Rodovia,kmEdital,MunSen,placa1,dist1,placa2,dist2,placa3,dist3,placa4,dist4,placa5,dist5,placa6,dist6,placa7,dist7,";
                            com1.CommandText = com1.CommandText + "placa8,dist8,placa9,dist9,placa10,dist10)";
                            com1.CommandText = com1.CommandText + " Values (" + "'" + objpfe.Rodovia + "'" + "," + "'" + cUt.ConvVirgPonto(objpfe.kmEdital.ToString()) + "'" + "," + "'" + objpfe.MunSen + "'" + ",";
                            com1.CommandText = com1.CommandText + "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0)";
                            com1.ExecuteNonQuery();
                            reader1.Close();

                            com1.CommandText = "insert into tproj(Rodovia,kmEdital,MunSen,Intro1,Intro2,Tit1,Tit2,DuasFolhas,inclusao,remocao,reposicionar,manter,mesmed)";
                            com1.CommandText = com1.CommandText + " Values (" + "'" + objpfe.Rodovia + "'" + "," + "'" + cUt.ConvVirgPonto(objpfe.kmEdital.ToString()) + "'" + "," + "'" + objpfe.MunSen + "'" + ",";
                            com1.CommandText = com1.CommandText + "' ',' ',' ',' ',0,0,0,0,0,'AGOSTO')";
                            com1.ExecuteNonQuery();
                            reader1.Close();

                            com1.CommandText = "insert into ttemfe(Rodovia,kmEdital,MunSen,vaiterfe,jatinhafe,foiconf,temqb,manter,remover,subst,proj,renova,altera)";
                            com1.CommandText = com1.CommandText + " Values (" + "'" + objpfe.Rodovia + "'" + "," + "'" + cUt.ConvVirgPonto(objpfe.kmEdital.ToString()) + "'" + "," + "'" + objpfe.MunSen + "'" + ",";
                            com1.CommandText = com1.CommandText + "0,0,0,0,0,0,0,0,0,0)";
                            com1.ExecuteNonQuery();
                            reader1.Close();

                        }

                        com1.CommandText = "insert into " + aTab + "(Rodovia,kmEdital,MunSen,kmReal,Localidade,Municipio,QtdFx,MunA,MunB,";
                        com1.CommandText = com1.CommandText + "VelFisc,Lat,Longit,VMD,Vel85p,Tipo,Lat2,Longit2,Vel85pSB,VmdB)";
                        com1.CommandText = com1.CommandText + " Values (" + "'" + objpfe.Rodovia + "'" + "," + "'" + cUt.ConvVirgPonto(objpfe.kmEdital.ToString()) + "'" + "," + "'" + objpfe.MunSen + "'" + ",";
                        com1.CommandText = com1.CommandText + "'" + cUt.ConvVirgPonto(objpfe.kmReal.ToString()) + "'" + "," + "'" + objpfe.Localidade + "'" + "," + "'" + objpfe.Municipio + "'" + ",";
                        com1.CommandText = com1.CommandText + objpfe.QtdFx + "," + "'" + objpfe.MunA + "'" + "," + "'" + objpfe.MunB + "'" + "," + objpfe.VelFisc + ",";
                        com1.CommandText = com1.CommandText + "'" + cUt.ConverteCoord(objpfe.Lat) + "'" + "," + "'" + cUt.ConverteCoord(objpfe.Longit) + "'" + "," + objpfe.VMD + "," + objpfe.Vel85p + ",";
                        com1.CommandText = com1.CommandText + "'" + objpfe.Tipo + "'" + "," + "'" + cUt.ConverteCoord(objpfe.Lat2) + "'" + "," + "'" + cUt.ConverteCoord(objpfe.Longit2) + "'" + "," + objpfe.Vel85pSB + ",";
                        com1.CommandText = com1.CommandText + objpfe.VmdB + ")";

                        //////com1.CommandText = " insert into ponto_fe(Rodovia, kmEdital, MunSen, kmReal, Localidade, Municipio, QtdFx, MunA, MunB, VelFisc, Lat, Longit, VMD, Vel85p,";
                        //////com1.CommandText = com1.CommandText + "Tipo, Lat2, Longit2, Vel85pSB, VmdB) Values('RJ-122', 2.5, 'Ambos Sentidos', 2.5, 'Guapimirim', 'Guapimirim', 2, 'Guapimirim',";
                        //////com1.CommandText = com1.CommandText + "'Cachoeiras de Macacu', 50, ' 22°33 10.86 S',' 42°57 55.01 O',2800,55,'I.A','zero','zero',55,2300)";

                        com1.ExecuteNonQuery();
                        reader1.Close();





                        //The Transaction will be completed    
                        txscope.Complete();
                    }
                }
                catch (Exception ex)
                {
                    // Log error    
                    txscope.Dispose();
                }
            }
               

            // ??????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????



            //cmd.CommandText = "INSERT INTO klant(klant_id, naam, voornaam)   VALUES(@param1,@param2,@param3)";
         
            

           // DesconectaBanco();

        }










        //*************************************************************************

        public void GravapFE2(C_pontofe objpfe, string aTab)
        {
            using (var txscope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                //try
                //{
                    using (mysqlCon1 = new MySqlConnection(strConFE))
                    {
                        mysqlCon1.Open();
                        com1 = mysqlCon1.CreateCommand();

                        if (aTab == "ponto_featual")
                        {
                            com1.CommandText = "delete from ponto_featual";     // Limpa a tabela se existir algum risco
                            com1.ExecuteNonQuery();
                            reader1.Close();
                        }
                        else
                        {
                           

                        }

                        com1.CommandText = "insert into " + aTab + "(Rodovia,kmEdital,MunSen,kmReal,Localidade,Municipio,QtdFx,MunA,MunB,";
                        com1.CommandText = com1.CommandText + "VelFisc,Lat,Longit,VMD,Vel85p,Tipo,Lat2,Longit2,Vel85pSB,VmdB,Calc85p)";
                        com1.CommandText = com1.CommandText + " Values (" + "'" + objpfe.Rodovia + "'" + "," + "'" + cUt.ConvVirgPonto(objpfe.kmEdital.ToString()) + "'" + "," + "'" + objpfe.MunSen + "'" + ",";
                        com1.CommandText = com1.CommandText + "'" + cUt.ConvVirgPonto(objpfe.kmReal.ToString()) + "'" + "," + "'" + objpfe.Localidade + "'" + "," + "'" + objpfe.Municipio + "'" + ",";
                        com1.CommandText = com1.CommandText + objpfe.QtdFx + "," + "'" + objpfe.MunA + "'" + "," + "'" + objpfe.MunB + "'" + "," + objpfe.VelFisc + ",";
                        //com1.CommandText = com1.CommandText + "'" + cUt.ConverteCoord(objpfe.Lat) + "'" + "," + "'" + cUt.ConverteCoord(objpfe.Longit) + "'" + "," + objpfe.VMD + "," + objpfe.Vel85p + ",";
                        com1.CommandText = com1.CommandText + "'" + objpfe.Lat + "'" + "," + "'" + objpfe.Longit + "'" + "," + objpfe.VMD + "," + objpfe.Vel85p + ",";
                       // com1.CommandText = com1.CommandText + "'" + objpfe.Tipo + "'" + "," + "'" + cUt.ConverteCoord(objpfe.Lat2) + "'" + "," + "'" + cUt.ConverteCoord(objpfe.Longit2) + "'" + "," + objpfe.Vel85pSB + ",";
                        com1.CommandText = com1.CommandText + "'" + objpfe.Tipo + "'" + "," + "'" + objpfe.Lat2 + "'" + "," + "'" + objpfe.Longit2 + "'" + "," + objpfe.Vel85pSB + ",";

                    com1.CommandText = com1.CommandText + objpfe.VmdB + "," + objpfe.Calc85p + ")";

                        //////com1.CommandText = " insert into ponto_fe(Rodovia, kmEdital, MunSen, kmReal, Localidade, Municipio, QtdFx, MunA, MunB, VelFisc, Lat, Longit, VMD, Vel85p,";
                        //////com1.CommandText = com1.CommandText + "Tipo, Lat2, Longit2, Vel85pSB, VmdB) Values('RJ-122', 2.5, 'Ambos Sentidos', 2.5, 'Guapimirim', 'Guapimirim', 2, 'Guapimirim',";
                        //////com1.CommandText = com1.CommandText + "'Cachoeiras de Macacu', 50, ' 22°33 10.86 S',' 42°57 55.01 O',2800,55,'I.A','zero','zero',55,2300)";

                        com1.ExecuteNonQuery();
                        reader1.Close();

                        //The Transaction will be completed    
                        txscope.Complete();
                    }
                //}
                //catch (Exception ex)
                //{
                //    // Log error    
                //    txscope.Dispose();
                //}
            }


            // ??????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????



        }

    }
}
