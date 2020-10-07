-- MySQL dump 10.13  Distrib 8.0.13, for Win64 (x86_64)
--
-- Host: localhost    Database: fe
-- ------------------------------------------------------
-- Server version	8.0.13

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `acidentes`
--

DROP TABLE IF EXISTS `acidentes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `acidentes` (
  `Rodovia` varchar(8) NOT NULL,
  `kmEdital` float NOT NULL,
  `MunSen` varchar(45) NOT NULL,
  `kmReal` float NOT NULL,
  `Abalroamento` int(1) NOT NULL DEFAULT '0',
  `Choque` int(1) NOT NULL DEFAULT '0',
  `Colisão` int(1) NOT NULL DEFAULT '0',
  `Tombamento` int(1) NOT NULL DEFAULT '0',
  `Capotamento` int(1) NOT NULL DEFAULT '0',
  `Incendio` int(1) NOT NULL DEFAULT '0',
  `Atropelamento` int(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Rodovia`,`kmEdital`,`MunSen`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `acidentes`
--

LOCK TABLES `acidentes` WRITE;
/*!40000 ALTER TABLE `acidentes` DISABLE KEYS */;
INSERT INTO `acidentes` VALUES ('RJ-106',2,'Ambos Sentidos',2,3,4,3,0,1,0,2),('RJ-106',6,'São Gonçalo',6,2,3,1,0,1,0,1),('RJ-116',7,'Itaperuna',7,2,3,2,0,2,1,1),('RJ-116',9.5,'Ambos Sentidos',9.5,3,2,1,0,0,0,1),('RJ-116',20,'Ambos Sentidos',20,0,0,0,0,0,0,0),('RJ-116',21.5,'Ambos Sentidos',21.5,1,1,0,0,0,0,0),('RJ-116',27,'Ambos Sentidos',27,3,2,2,0,0,0,0),('RJ-116',28,'Ambos Sentidos',28,2,1,1,0,0,0,0),('RJ-116',39,'Duplo',39,1,1,0,1,0,0,0),('RJ-116',46,'Duplo',46,0,1,0,0,1,0,0),('RJ-116',68,'Duplo',68,2,0,1,2,0,0,0),('RJ-116',70,'Duplo',70,2,1,1,3,2,0,0),('RJ-116',71.5,'Ambos Sentidos',71.5,1,2,2,0,0,0,0),('RJ-116',78,'Duplo',78,0,1,0,0,0,0,0),('RJ-116',88,'Duplo',88,1,0,2,0,0,0,0),('RJ-116',95.5,'Ambos Sentidos',95.5,0,0,0,0,0,0,0),('RJ-122',2.5,'Ambos Sentidos',2.5,1,1,0,0,0,0,0),('RJ-122',18,'Ambos Sentidos',18,0,0,1,0,1,0,0),('RJ-122',22,'Ambos Sentidos',22,0,1,1,0,0,0,0),('RJ-122',32,'Ambos Sentidos',32,2,0,1,0,0,0,0),('RJ-186',0.4,'Ambos Sentidos',0.4,0,0,0,0,0,0,0),('RJ-186',38,'Ambos Sentidos',38,0,1,2,0,1,0,0),('RJ-186',44,'Ambos Sentidos',44,0,1,1,0,0,0,1),('RJ-186',47,'Ambos Sentidos',47,0,1,0,0,1,0,0),('RJ-186',54,'Ambos Sentidos',54,1,0,2,0,0,0,0),('RJ-186',61,'Ambos Sentidos',61,2,0,3,0,1,0,0),('RJ-186',92,'Ambos Sentidos',92,2,1,1,0,0,0,1),('RJ-186',4444,'Ambos Sentidos',4444,3,4,2,0,0,0,0);
/*!40000 ALTER TABLE `acidentes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fat_risco`
--

DROP TABLE IF EXISTS `fat_risco`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `fat_risco` (
  `Rodovia` varchar(8) NOT NULL,
  `kmEdital` float NOT NULL,
  `MunSen` varchar(45) NOT NULL,
  `fatr1` tinytext,
  `fatr2` tinytext,
  `fatr3` tinytext,
  `fatr4` tinytext,
  `fatr5` tinytext,
  `fatr6` tinytext,
  `fatr7` tinytext,
  PRIMARY KEY (`Rodovia`,`kmEdital`,`MunSen`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fat_risco`
--

LOCK TABLES `fat_risco` WRITE;
/*!40000 ALTER TABLE `fat_risco` DISABLE KEYS */;
INSERT INTO `fat_risco` VALUES ('RJ-106',2,'Ambos Sentidos','Entrada e saída de veículos','Existência de ponto de ônibus','Curva perigosa','Travessia de pedestres','Existência de retorno','',''),('RJ-106',6,'São Gonçalo','Curva perigosa','Existência de retorno','Travessia de pedestres','','','',''),('RJ-116',7,'Itaperuna','Existência de ponto de ônibus','Travessia de pedestres','Área escolar','Pista dividida','Existência de comércio',NULL,NULL),('RJ-116',9.5,'Ambos Sentidos','Existência de ponto de ônibus','Travessia de pedestres','Área escolar','Existência de Comércio','Trânsito de ciclistas',NULL,NULL),('RJ-116',20,'Ambos Sentidos','Existência de ponto de ônibus','Travessia de pedestres','Existência de comércio','Trânsito de ciclistas','Travessia de pedestres',NULL,NULL),('RJ-116',21.5,'Ambos Sentidos','Área escolar','Existência de ponto de ônibus','Travessia de pedestres','Trânsito de ciclistas','Existência de comércio','',''),('RJ-116',27,'Ambos Sentidos','Existência de ponto de ônibus','Travessia de pedestres','Existência de comércio','Trânsito de ciclistas','  ',NULL,NULL),('RJ-116',28,'Ambos Sentidos','Existência de ponto de ônibus','Travessia de pedestres','Existência de comércio','Trânsito de ciclistas','','',''),('RJ-116',39,'Duplo','Existência de ponto de ônibus','Travessia de pedestres','Existência de comércio','Trânsito de ciclistas','  ',NULL,NULL),('RJ-116',46,'Duplo','Travessia de pedestres','Entrada e saída de veículos','  ','  ','  ',NULL,NULL),('RJ-116',68,'Duplo','Travessia de pedestres','Curva perigosa','  ','  ','  ',NULL,NULL),('RJ-116',70,'Duplo','Travessia de pedestres','Curva perigosa','Entrada e saida de veículos','  ','  ',NULL,NULL),('RJ-116',71.5,'Ambos Sentidos','Travessia de pedestres','Existência de comércio','Entrada e saida de veículos','  ','  ',NULL,NULL),('RJ-116',78,'Duplo','Travessia de pedestres','Existência de curvas','  ','  ','  ',NULL,NULL),('RJ-116',88,'Duplo','Travessia de pedestres','Área escolar','Existência de ponto de ônibus','Entrada e saída de veículos','  ',NULL,NULL),('RJ-116',95.5,'Ambos Sentidos','Entrada e saída de veículos','Existência de ponto de ônibus','Curva perigosa',' ','  ',NULL,NULL),('RJ-122',2.5,'Ambos Sentidos','Área escolar','Entrada e saída de veículos','Existência de comércio','Existência de ponto de ônibus','Trânsito de ciclistas','Travessia de pedestres','Presença de animais'),('RJ-122',18,'Ambos Sentidos','Área escolar','Trânsito de ciclistas','Travessia de pedestres','','','',''),('RJ-122',22,'Ambos Sentidos','Existência de comércio','Trânsito de ciclistas','Travessia de pedestres','Curva perigosa','','',''),('RJ-122',32,'Ambos Sentidos','Área escolar','Entrada e saída de veículos','Existência de ponto de ônibus','Trânsito de ciclistas','Travessia de pedestres','',''),('RJ-186',0.4,'Ambos Sentidos',' ',' ',' ',' ',' ',' ',' '),('RJ-186',38,'Ambos Sentidos','Curva perigosa','Existência de comércio','Travessia de pedestres','Existência de ponto de ônibus','','',''),('RJ-186',44,'Ambos Sentidos','Curva perigosa','Entrada e saída de veículos','Ponte Estreita','','','',''),('RJ-186',47,'Ambos Sentidos','Área escolar','Curva perigosa','Existência de Igreja.','Trânsito de ciclistas','Travessia de pedestres','',''),('RJ-186',54,'Ambos Sentidos','Existência de comércio','Existência de curvas','Existência de ponto de ônibus','Entrada e saída de veículos','','',''),('RJ-186',61,'Ambos Sentidos','Existência de curvas','Entrada e saída de veículos','Trânsito de ciclistas','Existência de comércio','','',''),('RJ-186',92,'Ambos Sentidos','Entrada e saída de veículos','Trânsito de ciclistas','Travessia de pedestres','Curva perigosa','Presença de animais','',''),('RJ-186',4444,'Ambos Sentidos','Área escolar','Trânsito de ciclistas','Travessia de pedestres','Existência de ponto de ônibus','Existência de comércio','','');
/*!40000 ALTER TABLE `fat_risco` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `histmedidas`
--

DROP TABLE IF EXISTS `histmedidas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `histmedidas` (
  `Rodovia` varchar(8) NOT NULL,
  `kmEdital` float NOT NULL,
  `MunSen` varchar(45) NOT NULL,
  `h1` tinytext,
  `h2` tinytext,
  `h3` tinytext,
  `h4` tinytext,
  `h5` tinytext,
  PRIMARY KEY (`Rodovia`,`kmEdital`,`MunSen`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `histmedidas`
--

LOCK TABLES `histmedidas` WRITE;
/*!40000 ALTER TABLE `histmedidas` DISABLE KEYS */;
INSERT INTO `histmedidas` VALUES ('RJ-106',2,'Ambos Sentidos','Existência de passarela de pedestres','',NULL,NULL,NULL),('RJ-106',6,'São Gonçalo','Existência de passarela de pedestres','',NULL,NULL,NULL),('RJ-116',7,'Itaperuna','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN.',NULL,NULL,NULL,NULL),('RJ-116',9.5,'Ambos Sentidos','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN.',NULL,NULL,NULL,NULL),('RJ-116',20,'Ambos Sentidos','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN.','Existência de Faixa de pedestres','Existência de Lombada física',NULL,NULL),('RJ-116',21.5,'Ambos Sentidos','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN.','Existência de Faixa de pedestres','Existência de Lombada física',NULL,NULL),('RJ-116',27,'Ambos Sentidos','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN.','Equipamento de Fiscalização Eletrônica presente no local','',NULL,NULL),('RJ-116',28,'Ambos Sentidos','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN.','Equipamento de Fiscalização Eletrônica presente no local','',NULL,NULL),('RJ-116',39,'Duplo','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN.','Equipamento de Fiscalização Eletrônica presente no local','',NULL,NULL),('RJ-116',46,'Duplo','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN.','Equipamento de Fiscalização Eletrônica presente no local','',NULL,NULL),('RJ-116',68,'Duplo','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN.','Equipamento de Fiscalização Eletrônica presente no local','Existência de semáforo no local',NULL,NULL),('RJ-116',70,'Duplo','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN.','Equipamento de Fiscalização Eletrônica presente no local','Existência de faixa de pedestres no local',NULL,NULL),('RJ-116',71.5,'Ambos Sentidos','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN.','Equipamento de Fiscalização Eletrônica presente no local','',NULL,NULL),('RJ-116',78,'Duplo','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN.','Equipamento de Fiscalização Eletrônica presente no local',NULL,NULL,NULL),('RJ-116',88,'Duplo','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN.','Equipamento de Fiscalização Eletrônica presente no local',NULL,NULL,NULL),('RJ-116',95.5,'Ambos Sentidos','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN.','Equipamento de Fiscalização Eletrônica presente no local',NULL,NULL,NULL),('RJ-122',2.5,'Ambos Sentidos','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN','','','',''),('RJ-122',18,'Ambos Sentidos','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN','','','',''),('RJ-122',22,'Ambos Sentidos','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN','','','',''),('RJ-122',32,'Ambos Sentidos','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN','','','',''),('RJ-186',0.4,'Ambos Sentidos',' ',' ',' ',' ',' '),('RJ-186',38,'Ambos Sentidos','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN','Existência de Faixa de pedestres','Existência de Lombada física','',''),('RJ-186',44,'Ambos Sentidos','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN','Existência de Faixa de pedestres','Existência de Lombada física','',''),('RJ-186',47,'Ambos Sentidos','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN','','','',''),('RJ-186',54,'Ambos Sentidos','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN','','','',''),('RJ-186',61,'Ambos Sentidos','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN','Existência de Faixa de pedestres','Existência de Lombada física','',''),('RJ-186',92,'Ambos Sentidos','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN','','','',''),('RJ-186',4444,'Ambos Sentidos','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN','Existência de Faixa de pedestres','Existência de Lombada física','','');
/*!40000 ALTER TABLE `histmedidas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ponto_fe`
--

DROP TABLE IF EXISTS `ponto_fe`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ponto_fe` (
  `Rodovia` varchar(8) NOT NULL,
  `kmEdital` float NOT NULL,
  `MunSen` varchar(45) NOT NULL,
  `kmReal` float NOT NULL COMMENT 'Km que além de ser o real, tambbém é o km arredondado para um ou dois sentios (se houver)\n',
  `Localidade` varchar(45) NOT NULL,
  `Municipio` varchar(45) NOT NULL,
  `QtdFx` varchar(45) NOT NULL,
  `MunA` varchar(45) NOT NULL,
  `MunB` varchar(45) NOT NULL,
  `VelFisc` int(1) NOT NULL,
  `Lat` varchar(20) NOT NULL,
  `Longit` varchar(20) NOT NULL,
  `VMD` int(1) NOT NULL,
  `Vel85p` int(1) NOT NULL,
  `Tipo` char(6) NOT NULL,
  `Lat2` varchar(45) NOT NULL DEFAULT 'zero' COMMENT 'Latitude do equipamento do outro sentido (se houver)',
  `Longit2` varchar(45) NOT NULL DEFAULT 'zero' COMMENT 'Longitude do equipamento do outro sentido (se houver)',
  `Vel85pSB` int(11) NOT NULL DEFAULT '0',
  `VmdB` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Rodovia`,`kmEdital`,`MunSen`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ponto_fe`
--

LOCK TABLES `ponto_fe` WRITE;
/*!40000 ALTER TABLE `ponto_fe` DISABLE KEYS */;
INSERT INTO `ponto_fe` VALUES ('RJ-106',2,'Ambos Sentidos',2,'Arsenal','São Gonçalo','2','São Gonçalo','Macaé',50,'22°51\'33.85\"S','43° 0\'37.43\"O',28096,63,'I.A','22°51\'37.41\"S','43° 0\'34.87\"O',63,27465),('RJ-106',6,'São Gonçalo',6,'Rio do Ouro','São Gonçalo','2','São Gonçalo','Macaé',50,'22°52\'59.36\"S','42°59\'4.72\"O',25367,63,'I.A','zero','zero',63,0),('RJ-116',7,'Itaperuna',7,'Sambaetiba','Itaboraí','2','Itaboraí','Itaperuna',50,'22°40\'54.39\"S','42°46\'42.76\"O',4176,60,'I.A','zero','zero',0,0),('RJ-116',9.5,'Ambos Sentidos',9.5,'Sambaetiba','Itaboraí','2','Itaboraí','Itaperuna',50,'22°39\'44.94\"S','42°46\'04.99\"O',4935,55,'I.A','zero','zero',55,5376),('RJ-116',20,'Ambos Sentidos',20,'Papucaia','Cachoeiras de Macacu','2','Itaboraí','Itaperuna',50,'22°36\'12.38\"S','42°44\'23.04\"O',2556,46,'I.A','zero','zero',45,2363),('RJ-116',21.5,'Ambos Sentidos',21.5,'Papucaia Ribeira','Cachoeiras de Macacu','2','Itaboraí','Itaperuna',50,' 22°35\'29.63\"S',' 42°43\'36.14\"O',2658,49,'I.A','zero','zero',49,2543),('RJ-116',27,'Ambos Sentidos',27,'Japuiba','Cachoeiras de Macacu','2','Itaboraí','Itaperuna',50,' 22°33\'57.21\"S',' 42°41\'19.24\"O',2422,46,'I.A','zero','zero',48,2387),('RJ-116',28,'Ambos Sentidos',28,'Santana de Japuiba','Cachoeiras de Macacu','2','Itaboraí','Itaperuna',50,'22°33\'36.50\"S','42°41\'17.02\"O',2062,47,'I.A','zero','zero',47,2021),('RJ-116',39,'Duplo',39,'Cachoeiras de Macacu','Cachoeiras de Macacu','2','Itaboraí','Itaperuna',50,'22°29\'13.44\"S','42°39\'48.83\"O',2359,48,'I.A','zero','zero',47,2241),('RJ-116',46,'Duplo',46,'Cachoeiras de Macacu','Cachoeiras de Macacu','2','Itaboraí','Itaperuna',50,'22°25\'59.61\"S','42°38\'26.16\"O',1830,48,'I.A','zero','zero',47,1772),('RJ-116',68,'Duplo',68,'Muri','Nova Friburgo','2','Itaboraí','Itaperuna',50,'22°21\'37.63\"S','42°31\'45.82\"O',4418,49,'I.A','zero','zero',49,4286),('RJ-116',70,'Duplo',70,'Muri','Nova Friburgo','2','Itaboraí','Itaperuna',50,'22°21\'30.21\"S','42°31\'7.13\"O',4187,49,'I.A','zero','zero',49,4148),('RJ-116',71.5,'Ambos Sentidos',71.5,'Muri','Nova Friburgo','2','Itaboraí','Itaperuna',60,'22°20\'49.58\"S','42°30\'29.70\"O',7947,56,'II.B','22°20\'48.99\"S','42°30\'30.67\"O',58,7894),('RJ-116',78,'Duplo',78,'Nova Friburgo','Nova Friburgo','2','Itaboraí','Itaperuna',50,'22°18\'0.74\"S','42°31\'17.18\"O',5480,47,'I.A','zero','zero',47,5239),('RJ-116',88,'Duplo',88,'Nova Friburgo','Nova Friburgo','2','Itaboraí','Itaperuna',50,'22°13\'21.91\"S','42°30\'47.79\"O',1941,44,'I.A','zero','zero',44,1985),('RJ-116',95.5,'Ambos Sentidos',95.5,'Banquete','Bom Jardim','2','Itaboraí','Itaperuna',50,'22°11\'0.47\"S','42°28\'27.23\"O',4385,49,'III.C','22°10\'58.80\"S','42°28\'25.26\"O',47,4148),('RJ-122',2.5,'Ambos Sentidos',2.5,'Guapimirim','Guapimirim','2','Guapimirim','Cachoeiras de Macacu',50,'22°33 10.86 S','42°57 55.01 O',3559,74,'I.A','zero','zero',72,3476),('RJ-122',18,'Ambos Sentidos',18,'São José da Boa Morte','Guapimirim','2','Guapimirim','Cachoeiras de Macacu',50,'22°34 0.17\" S','42°50 10.30\" O',3123,48,'I.A','zero','zero',46,3198),('RJ-122',22,'Ambos Sentidos',22,'Maraporã','Cachoeiras de Macacu','2','Guapimirim','Cachoeiras de Macacu',50,'22°33 53.65 S','42°47 47.31 O',2870,53,'I.A','zero','zero',56,2962),('RJ-122',32,'Ambos Sentidos',32,'Funchal','Cachoeiras de Macacu','2','Guapimirim','Cachoeiras de Macacu',50,'22°31\'27.26 S','42°43\'28.32 O',3749,47,'I.A','zero','zero',48,3607),('RJ-186',0.4,'Ambos Sentidos',0.4,'Chalé','Santo Antônio de Pádua','2','Santo Antônio de Pádua','Bom Jesus do Itabapoana',50,'21°39 30.46 S','42°20 24.01 O',2537,43,'I.A','zero','zero',44,2854),('RJ-186',38,'Ambos Sentidos',38,'Ibitiguaçu','Santo Antônio de Pádua','2','Santo Antônio de Pádua','Bom Jesus do Itabapoana',50,'21°30 6.49 S','42° 4 45.52 O',1265,45,'I.A','zero','zero',44,1239),('RJ-186',44,'Ambos Sentidos',44,'Santo Antônio de Pádua','Santo Antônio de Pádua','2','Santo Antônio de Pádua','Bom Jesus do Itabapoana',50,'21°27 51.77 S','42° 2 21.15 O',1243,43,'I.A','zero','zero',44,1176),('RJ-186',47,'Ambos Sentidos',47,'Monte Alegre','Santo Antônio de Pádua','2','Santo Antônio de Pádua','Bom Jesus do Itabapoana',50,'21°27 9.75 S','42° 0 47.84 O',1342,49,'I.A','zero','zero',48,1285),('RJ-186',54,'Ambos Sentidos',54,'São José de Ubá','São José de Ubá','2','Santo Antônio de Pádua','Bom Jesus do Itabapoana',50,'21°24 36.92 S','41°58 41.85 O',1409,46,'II.A','zero','zero',46,1396),('RJ-186',61,'Ambos Sentidos',61,'São José de Ubá','São José de Ubá','2','Santo Antônio de Pádua','Bom Jesus do Itabapoana',60,'21°21 56.76 S','41°56 7.37 O',1749,57,'I.A','21°21 55.75 S','41°56 6.49 O',59,1672),('RJ-186',92,'Ambos Sentidos',92,'Corrego Seco','Itaperuna','2','Santo Antônio de Pádua','Bom Jesus do Itabapoana',50,'21°12 8.67 S','41°43 6.27 O',1638,72,'I.A','zero','zero',69,1489),('RJ-186',4444,'Ambos Sentidos',4444,'Chalé','Santo Antônio de Pádua','2','Santo Antônio de Pádua','Bom Jesus do Itabapoana',50,'21°39 30.46 S','42°20 24.01 O',1638,43,'I.A','zero','zero',44,1489);
/*!40000 ALTER TABLE `ponto_fe` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ponto_featual`
--

DROP TABLE IF EXISTS `ponto_featual`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ponto_featual` (
  `Rodovia` varchar(8) NOT NULL,
  `kmEdital` float NOT NULL,
  `MunSen` varchar(45) NOT NULL,
  `kmReal` float NOT NULL,
  `Localidade` varchar(45) NOT NULL,
  `Municipio` varchar(45) NOT NULL,
  `QtdFx` int(1) NOT NULL,
  `MunA` varchar(45) NOT NULL,
  `MunB` varchar(45) NOT NULL,
  `VelFisc` int(1) NOT NULL,
  `Lat` varchar(20) NOT NULL,
  `Longit` varchar(20) NOT NULL,
  `VMD` int(1) NOT NULL,
  `Vel85p` int(1) NOT NULL,
  `Tipo` char(6) NOT NULL,
  `Lat2` varchar(45) NOT NULL DEFAULT 'zero' COMMENT 'Latitude do equipamento do outro sentido (se houver)',
  `Longit2` varchar(45) NOT NULL DEFAULT 'zero' COMMENT 'Longitude do equipamento do outro sentido (se houver)',
  `Vel85pSB` int(11) NOT NULL DEFAULT '0',
  `VmdB` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Rodovia`,`kmEdital`,`MunSen`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ponto_featual`
--

LOCK TABLES `ponto_featual` WRITE;
/*!40000 ALTER TABLE `ponto_featual` DISABLE KEYS */;
INSERT INTO `ponto_featual` VALUES ('RJ-186',4444,'Ambos Sentidos',4444,'Chalé','Santo Antônio de Pádua',2,'Santo Antônio de Pádua','Bom Jesus do Itabapoana',50,'21°39 30.46 S','42°20 24.01 O',2537,43,'I.A','zero','zero',44,2854);
/*!40000 ALTER TABLE `ponto_featual` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tcanteiro`
--

DROP TABLE IF EXISTS `tcanteiro`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `tcanteiro` (
  `tonde` varchar(45) NOT NULL,
  PRIMARY KEY (`tonde`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tcanteiro`
--

LOCK TABLES `tcanteiro` WRITE;
/*!40000 ALTER TABLE `tcanteiro` DISABLE KEYS */;
INSERT INTO `tcanteiro` VALUES ('do canteiro central'),('do canteiro lateral'),('no canteiro central'),('no canteiro lateral');
/*!40000 ALTER TABLE `tcanteiro` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tdistancias`
--

DROP TABLE IF EXISTS `tdistancias`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `tdistancias` (
  `distancia` int(11) NOT NULL,
  PRIMARY KEY (`distancia`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tdistancias`
--

LOCK TABLES `tdistancias` WRITE;
/*!40000 ALTER TABLE `tdistancias` DISABLE KEYS */;
INSERT INTO `tdistancias` VALUES (30),(40),(50),(60),(70),(80),(90),(100),(120),(130),(150),(170),(180),(200),(250),(270),(300),(350),(400),(450),(500);
/*!40000 ALTER TABLE `tdistancias` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `teduplo`
--

DROP TABLE IF EXISTS `teduplo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `teduplo` (
  `Rodovia` varchar(8) NOT NULL,
  `kmEdital` float NOT NULL,
  `MunSen` varchar(45) NOT NULL,
  `EDuplo` int(11) NOT NULL,
  `MunSen2` varchar(45) DEFAULT NULL,
  `Fx1_1` int(11) DEFAULT NULL,
  `Fx1_2` int(11) DEFAULT NULL,
  `Fx1_3` int(11) DEFAULT NULL,
  `Fx2_1` int(11) DEFAULT NULL,
  `Fx2_2` int(11) DEFAULT NULL,
  `Fx2_3` int(11) DEFAULT NULL,
  `NPistas` int(11) NOT NULL,
  `NFxs` int(11) NOT NULL,
  PRIMARY KEY (`Rodovia`,`kmEdital`,`MunSen`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `teduplo`
--

LOCK TABLES `teduplo` WRITE;
/*!40000 ALTER TABLE `teduplo` DISABLE KEYS */;
INSERT INTO `teduplo` VALUES ('RJ-106',2,'Ambos Sentidos',0,NULL,1,2,0,1,2,NULL,1,2),('RJ-106',6,'São Gonçalo',1,NULL,1,2,0,0,0,0,1,2),('RJ-116',7,'Itaperuna',1,NULL,1,2,NULL,NULL,NULL,NULL,1,2),('RJ-116',9.5,'Ambos Sentidos',1,NULL,1,NULL,NULL,2,NULL,NULL,1,1),('RJ-116',20,'Ambos Sentidos',1,NULL,1,NULL,NULL,2,NULL,NULL,1,1),('RJ-116',21.5,'Ambos Sentidos',1,NULL,1,NULL,NULL,2,NULL,NULL,1,1),('RJ-116',27,'Ambos Sentidos',1,NULL,1,NULL,NULL,2,NULL,NULL,1,1),('RJ-116',28,'Ambos Sentidos',1,NULL,1,NULL,NULL,2,NULL,NULL,1,1),('RJ-116',39,'Duplo',1,NULL,1,NULL,NULL,2,NULL,NULL,1,1),('RJ-116',46,'Duplo',1,NULL,1,NULL,NULL,2,NULL,NULL,1,1),('RJ-116',68,'Duplo',1,NULL,1,NULL,NULL,2,NULL,NULL,1,1),('RJ-116',70,'Duplo',1,NULL,1,NULL,NULL,2,NULL,NULL,1,1),('RJ-116',71.5,'Ambos Sentidos',0,NULL,1,NULL,NULL,2,NULL,NULL,1,1),('RJ-116',78,'Duplo',1,NULL,1,NULL,NULL,2,NULL,NULL,1,1),('RJ-116',88,'Duplo',1,NULL,1,NULL,NULL,2,NULL,NULL,1,1),('RJ-116',95.5,'Ambos Sentidos',0,NULL,1,1,NULL,2,2,NULL,1,2),('RJ-122',2.5,'Ambos Sentidos',1,' ',1,0,0,2,0,0,1,2),('RJ-122',18,'Ambos Sentidos',1,' ',1,0,0,2,0,0,1,2),('RJ-122',22,'Ambos Sentidos',1,' ',1,0,0,2,0,0,1,2),('RJ-122',32,'Ambos Sentidos',1,' ',1,0,0,2,0,0,1,2),('RJ-186',0.4,'Ambos Sentidos',0,' ',0,0,0,0,0,0,0,0),('RJ-186',38,'Ambos Sentidos',1,' ',1,0,0,2,0,0,1,2),('RJ-186',44,'Ambos Sentidos',1,' ',1,0,0,2,0,0,1,2),('RJ-186',47,'Ambos Sentidos',1,' ',1,0,0,1,0,0,1,2),('RJ-186',54,'Ambos Sentidos',1,' ',1,0,0,2,0,0,1,2),('RJ-186',61,'Ambos Sentidos',0,' ',1,0,0,2,0,0,1,2),('RJ-186',92,'Ambos Sentidos',1,' ',1,0,0,2,0,0,1,2),('RJ-186',4444,'Ambos Sentidos',1,' ',1,0,0,2,0,0,1,1);
/*!40000 ALTER TABLE `teduplo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tgeometria`
--

DROP TABLE IF EXISTS `tgeometria`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `tgeometria` (
  `Rodovia` varchar(8) NOT NULL,
  `kmEdital` float NOT NULL,
  `MunSen` varchar(45) NOT NULL,
  `Aclive` int(11) NOT NULL,
  `Declive` int(11) NOT NULL,
  `Plano` int(11) NOT NULL,
  `Curva` int(11) NOT NULL,
  `Urbano` int(11) NOT NULL,
  `Pedestre` int(11) DEFAULT NULL,
  `Paolongo` int(11) DEFAULT NULL,
  `Ptrans` int(11) DEFAULT NULL,
  `Ciclista` int(11) DEFAULT NULL,
  `Caolongo` int(11) DEFAULT NULL,
  `Ctrans` int(11) DEFAULT NULL,
  `UmSentido` int(11) NOT NULL,
  `SA` varchar(45) DEFAULT NULL,
  `SB` varchar(45) DEFAULT NULL,
  `Forma` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`Rodovia`,`kmEdital`,`MunSen`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tgeometria`
--

LOCK TABLES `tgeometria` WRITE;
/*!40000 ALTER TABLE `tgeometria` DISABLE KEYS */;
INSERT INTO `tgeometria` VALUES ('RJ-106',2,'Ambos Sentidos',0,0,1,1,1,1,1,1,1,1,0,0,'São Gonçalo','Macaé','retrato'),('RJ-106',6,'São Gonçalo',0,0,1,1,1,1,1,1,0,0,0,1,'São Gonçalo','Macaé','retrato'),('RJ-116',7,'Itaperuna',0,0,1,1,1,1,1,1,1,1,1,1,'Itaperuna','-','retrato'),('RJ-116',9.5,'Ambos Sentidos',0,0,1,1,1,1,1,1,1,1,1,0,'Itaperuna','Itaboraí','paisagem'),('RJ-116',20,'Ambos Sentidos',0,0,1,1,1,1,1,1,1,1,1,0,'Itaperuna','Itaboraí','retrato'),('RJ-116',21.5,'Ambos Sentidos',0,0,1,0,1,1,1,1,1,1,1,0,'Itaperuna','Itaboraí','retrato'),('RJ-116',27,'Ambos Sentidos',0,0,1,0,1,1,1,1,1,1,1,0,'Itaperuna','Itaboraí','retrato'),('RJ-116',28,'Ambos Sentidos',0,0,1,0,1,1,1,0,1,1,0,0,'Itaperuna','Itaboraí','retrato'),('RJ-116',39,'Duplo',0,0,1,0,1,1,1,0,1,1,0,0,'Itaperuna','Itaboraí','retrato'),('RJ-116',46,'Duplo',0,0,1,1,1,1,1,0,0,0,0,0,'Itaperuna','Itaboraí','retrato'),('RJ-116',68,'Duplo',0,1,0,1,1,1,1,1,0,0,0,0,'Itaperuna','Itaboraí','retrato'),('RJ-116',70,'Duplo',0,0,0,1,1,1,1,1,0,0,0,0,'Itaperuna','Itaboraí','retrato'),('RJ-116',71.5,'Ambos Sentidos',0,1,0,1,1,1,1,1,0,0,0,0,'Itaperuna','Itaboraí','retrato'),('RJ-116',78,'Duplo',0,1,0,1,1,1,1,0,0,0,0,0,'Itaperuna','Itaboraí','retrato'),('RJ-116',88,'Duplo',0,0,1,1,1,1,1,0,0,0,0,0,'Itaperuna','Itaboraí','retrato'),('RJ-116',95.5,'Ambos Sentidos',0,1,0,1,0,0,0,0,0,0,0,0,'Itaperuna','Itaboraí','retrato'),('RJ-122',2.5,'Ambos Sentidos',0,0,1,1,0,1,1,1,1,1,0,0,'Guapimirim','Cachoeiras de Macacu','paisagem'),('RJ-122',18,'Ambos Sentidos',0,0,1,1,0,1,1,0,1,1,0,0,'Guapimirim','Cachoeiras de Macacu','paisagem'),('RJ-122',22,'Ambos Sentidos',0,0,1,1,0,1,1,0,1,1,0,0,'Guapimirim','Cachoeiras de Macacu','paisagem'),('RJ-122',32,'Ambos Sentidos',0,0,1,0,0,1,1,1,1,1,0,0,'Guapimirim','Cachoeiras de Macacu','paisagem'),('RJ-186',0.4,'Ambos Sentidos',0,0,0,0,0,0,0,0,0,0,0,0,'Santo Antônio de Pádua','Bom Jesus do Itabapoana',' '),('RJ-186',38,'Ambos Sentidos',0,0,1,1,0,1,1,0,1,1,0,0,'Santo Antônio de Pádua','Bom Jesus do Itabapoana','retrato'),('RJ-186',44,'Ambos Sentidos',0,0,1,1,0,1,1,0,1,1,0,0,'Santo Antônio de Pádua','Bom Jesus do Itabapoana','paisagem'),('RJ-186',47,'Ambos Sentidos',0,0,1,1,0,1,1,0,1,1,0,0,'Santo Antônio de Pádua','Bom Jesus do Itabapoana','retrato'),('RJ-186',54,'Ambos Sentidos',0,0,1,1,0,1,1,0,0,0,0,0,'Santo Antônio de Pádua','Bom Jesus do Itabapoana','retrato'),('RJ-186',61,'Ambos Sentidos',0,0,1,1,0,1,1,0,1,1,0,0,'Santo Antônio de Pádua','Bom Jesus do Itabapoana','paisagem'),('RJ-186',92,'Ambos Sentidos',0,1,0,1,0,1,1,0,1,1,0,0,'Santo Antônio de Pádua','Bom Jesus do Itabapoana','retrato'),('RJ-186',4444,'Ambos Sentidos',0,0,1,1,1,1,0,1,1,1,0,0,'Santo Antônio de Pádua','Bom Jesus do Itabapoana','paisagem');
/*!40000 ALTER TABLE `tgeometria` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tipoequip`
--

DROP TABLE IF EXISTS `tipoequip`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `tipoequip` (
  `Tipo` char(6) NOT NULL DEFAULT '-',
  `Desc` varchar(45) NOT NULL DEFAULT '-',
  `NFaixas` int(11) NOT NULL,
  `Psimp` tinyint(1) NOT NULL,
  `TemDisp` tinyint(1) NOT NULL,
  `AvSinal` tinyint(1) NOT NULL,
  `AvFxPed` tinyint(1) NOT NULL,
  `ExVel` tinyint(1) NOT NULL,
  PRIMARY KEY (`Tipo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tipoequip`
--

LOCK TABLES `tipoequip` WRITE;
/*!40000 ALTER TABLE `tipoequip` DISABLE KEYS */;
INSERT INTO `tipoequip` VALUES ('I.A','LOMBADA ELETRÔNICA',2,1,1,0,0,1),('I.B','LOMBADA ELETRÔNICA',2,1,0,0,0,1),('II.A','RADAR FIXO',1,0,0,0,0,1),('II.B','RADAR FIXO',2,0,0,0,0,1),('II.C','RADAR FIXO',3,0,0,0,0,1),('III.A','SEMÁFORO',1,0,0,1,0,0),('III.B','SEMÁFORO',2,0,0,1,1,0),('III.C','SEMÁFORO',2,0,0,1,0,1),('III.D','SEMÁFORO',1,0,0,1,1,1),('III.E','SEMÁFORO',2,0,0,1,1,1);
/*!40000 ALTER TABLE `tipoequip` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tjorn`
--

DROP TABLE IF EXISTS `tjorn`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `tjorn` (
  `Rodovia` varchar(8) NOT NULL,
  `kmEdital` float NOT NULL,
  `MunSen` varchar(45) NOT NULL,
  `semjorn` tinytext NOT NULL,
  PRIMARY KEY (`Rodovia`,`kmEdital`,`MunSen`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tjorn`
--

LOCK TABLES `tjorn` WRITE;
/*!40000 ALTER TABLE `tjorn` DISABLE KEYS */;
INSERT INTO `tjorn` VALUES ('RJ-106',2,'Ambos Sentidos','   '),('RJ-116',7,'Itaperuna','Sem material jornalistico'),('RJ-116',9.5,'Ambos Sentidos','Sem material jornalistico'),('RJ-116',20,'Ambos Sentidos','Sem material jornalistico'),('RJ-116',21.5,'Ambos Sentidos','Sem material jornalistico'),('RJ-116',27,'Ambos Sentidos','Sem material jornalistico'),('RJ-116',28,'Ambos Sentidos','Sem material jornalistico'),('RJ-116',39,'Duplo','  '),('RJ-116',46,'Duplo','  '),('RJ-116',68,'Duplo','    '),('RJ-116',70,'Duplo','    '),('RJ-116',71.5,'Ambos Sentidos','    '),('RJ-116',78,'Duplo','Sem material jornalistico'),('RJ-116',88,'Duplo','Sem material jornalistico'),('RJ-116',95.5,'Ambos Sentidos','   '),('RJ-122',2.5,'Ambos Sentidos','Sem material jornalistico'),('RJ-122',18,'Ambos Sentidos','Sem material jornalistico'),('RJ-122',22,'Ambos Sentidos','Sem material jornalistico'),('RJ-122',32,'Ambos Sentidos',''),('RJ-186',0.4,'Ambos Sentidos',' '),('RJ-186',38,'Ambos Sentidos','Sem material jornalistico'),('RJ-186',44,'Ambos Sentidos',' '),('RJ-186',47,'Ambos Sentidos','Sem material jornalistico'),('RJ-186',54,'Ambos Sentidos','Sem material jornalistico'),('RJ-186',61,'Ambos Sentidos','Sem material jornalistico'),('RJ-186',92,'Ambos Sentidos',' '),('RJ-186',4444,'Ambos Sentidos','Sem material jornalistico');
/*!40000 ALTER TABLE `tjorn` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tpaginas`
--

DROP TABLE IF EXISTS `tpaginas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `tpaginas` (
  `Rodovia` varchar(8) NOT NULL,
  `kmEdital` float NOT NULL,
  `MunSen` varchar(45) NOT NULL,
  `p1` tinyint(4) DEFAULT NULL,
  `p2` tinyint(4) DEFAULT NULL,
  `p3` tinyint(4) DEFAULT NULL,
  `p4` tinyint(4) DEFAULT NULL,
  `p5` tinyint(4) DEFAULT NULL,
  `p6` tinyint(4) DEFAULT NULL,
  `p7` tinyint(4) DEFAULT NULL,
  `p8` tinyint(4) DEFAULT NULL,
  `p9` tinyint(4) DEFAULT NULL,
  `p10` tinyint(4) DEFAULT NULL,
  `p11` tinyint(4) DEFAULT NULL,
  `p12` tinyint(4) DEFAULT NULL,
  `p13` tinyint(4) DEFAULT NULL,
  `p14` tinyint(4) DEFAULT NULL,
  `p15` tinyint(4) DEFAULT NULL,
  `p16` tinyint(4) DEFAULT NULL,
  `p17` tinyint(4) DEFAULT NULL,
  `p18` tinyint(4) DEFAULT NULL,
  `p19` tinyint(4) DEFAULT NULL,
  `p20` tinyint(4) DEFAULT NULL,
  `p21` tinyint(4) DEFAULT NULL,
  `p22` tinyint(4) DEFAULT NULL,
  `p23` tinyint(4) DEFAULT NULL,
  `p24` tinyint(4) DEFAULT NULL,
  `p25` tinyint(4) DEFAULT NULL,
  `p26` tinyint(4) DEFAULT NULL,
  PRIMARY KEY (`Rodovia`,`kmEdital`,`MunSen`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tpaginas`
--

LOCK TABLES `tpaginas` WRITE;
/*!40000 ALTER TABLE `tpaginas` DISABLE KEYS */;
INSERT INTO `tpaginas` VALUES ('RJ-106',2,'Ambos Sentidos',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,1,1,1,1),('RJ-106',6,'São Gonçalo',1,1,1,1,1,1,1,1,1,1,0,1,1,1,1,1,0,1,0,1,1,1,1,1,1,1),('RJ-116',7,'Itaperuna',1,1,1,1,1,1,1,1,1,1,0,1,1,1,1,1,0,1,0,1,1,1,0,1,1,1),('RJ-116',9.5,'Ambos Sentidos',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,0,1,1,1),('RJ-116',20,'Ambos Sentidos',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,0,1,1,1),('RJ-116',21.5,'Ambos Sentidos',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,0,1,1,1),('RJ-116',27,'Ambos Sentidos',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,0,1,1,1),('RJ-116',28,'Ambos Sentidos',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,0,1,1,1),('RJ-116',39,'Duplo',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,1,1,1,1),('RJ-116',46,'Duplo',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,1,1,1,1),('RJ-116',68,'Duplo',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,1,1,1,1),('RJ-116',70,'Duplo',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,1,1,1,1),('RJ-116',71.5,'Ambos Sentidos',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,1,1,1,1),('RJ-116',78,'Duplo',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,0,1,1,1),('RJ-116',88,'Duplo',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,0,1,1,1),('RJ-116',95.5,'Ambos Sentidos',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,1,1,1,1),('RJ-122',2.5,'Ambos Sentidos',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,0,1,1,1),('RJ-122',18,'Ambos Sentidos',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,0,1,1,1),('RJ-122',22,'Ambos Sentidos',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,0,1,1,1),('RJ-122',32,'Ambos Sentidos',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,1,1,1,1),('RJ-186',0.4,'Ambos Sentidos',0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0),('RJ-186',38,'Ambos Sentidos',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,0,1,1,1),('RJ-186',44,'Ambos Sentidos',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,1,1,1,1),('RJ-186',47,'Ambos Sentidos',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,0,1,1,1),('RJ-186',54,'Ambos Sentidos',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,0,1,1,1),('RJ-186',61,'Ambos Sentidos',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,0,1,1,1),('RJ-186',92,'Ambos Sentidos',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,1,1,1,1),('RJ-186',4444,'Ambos Sentidos',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,0,1,1,1);
/*!40000 ALTER TABLE `tpaginas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tplacas`
--

DROP TABLE IF EXISTS `tplacas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `tplacas` (
  `Rodovia` varchar(8) NOT NULL,
  `kmEdital` float NOT NULL,
  `MunSen` varchar(45) NOT NULL,
  `placa1` varchar(200) DEFAULT NULL,
  `dist1` int(11) DEFAULT '0',
  `S1` varchar(45) DEFAULT NULL,
  `ct1` varchar(45) DEFAULT NULL,
  `stt1` varchar(45) DEFAULT NULL,
  `dta1` int(11) DEFAULT NULL,
  `placa2` varchar(200) DEFAULT NULL,
  `dist2` int(11) DEFAULT NULL,
  `S2` varchar(45) DEFAULT NULL,
  `ct2` varchar(45) DEFAULT NULL,
  `stt2` varchar(45) DEFAULT NULL,
  `dta2` int(11) DEFAULT NULL,
  `placa3` varchar(200) DEFAULT NULL,
  `dist3` int(11) DEFAULT NULL,
  `S3` varchar(45) DEFAULT NULL,
  `ct3` varchar(45) DEFAULT NULL,
  `stt3` varchar(45) DEFAULT NULL,
  `dta3` int(11) DEFAULT NULL,
  `placa4` varchar(200) DEFAULT NULL,
  `dist4` int(11) DEFAULT NULL,
  `S4` varchar(45) DEFAULT NULL,
  `ct4` varchar(45) DEFAULT NULL,
  `stt4` varchar(45) DEFAULT NULL,
  `dta4` int(11) DEFAULT NULL,
  `placa5` varchar(200) DEFAULT NULL,
  `dist5` int(11) DEFAULT NULL,
  `S5` varchar(45) DEFAULT NULL,
  `ct5` varchar(45) DEFAULT NULL,
  `stt5` varchar(45) DEFAULT NULL,
  `dta5` int(11) DEFAULT NULL,
  `placa6` varchar(200) DEFAULT NULL,
  `dist6` int(11) DEFAULT NULL,
  `S6` varchar(45) DEFAULT NULL,
  `ct6` varchar(45) DEFAULT NULL,
  `stt6` varchar(45) DEFAULT NULL,
  `dta6` int(11) DEFAULT NULL,
  `placa7` varchar(200) DEFAULT NULL,
  `dist7` int(11) DEFAULT NULL,
  `S7` varchar(45) DEFAULT NULL,
  `ct7` varchar(45) DEFAULT NULL,
  `stt7` varchar(45) DEFAULT NULL,
  `dta7` int(11) DEFAULT NULL,
  `placa8` varchar(200) DEFAULT NULL,
  `dist8` int(11) DEFAULT NULL,
  `S8` varchar(45) DEFAULT NULL,
  `ct8` varchar(45) DEFAULT NULL,
  `stt8` varchar(45) DEFAULT NULL,
  `dta8` int(11) DEFAULT NULL,
  `placa9` varchar(200) DEFAULT NULL,
  `dist9` int(11) DEFAULT NULL,
  `S9` varchar(45) DEFAULT NULL,
  `ct9` varchar(45) DEFAULT NULL,
  `stt9` varchar(45) DEFAULT NULL,
  `dta9` int(11) DEFAULT NULL,
  `placa10` varchar(200) DEFAULT NULL,
  `dist10` int(11) DEFAULT NULL,
  `S10` varchar(45) DEFAULT NULL,
  `ct10` varchar(45) DEFAULT NULL,
  `stt10` varchar(45) DEFAULT NULL,
  `dta10` int(11) DEFAULT NULL,
  PRIMARY KEY (`Rodovia`,`kmEdital`,`MunSen`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tplacas`
--

LOCK TABLES `tplacas` WRITE;
/*!40000 ALTER TABLE `tplacas` DISABLE KEYS */;
INSERT INTO `tplacas` VALUES ('RJ-122',2.5,'Ambos Sentidos','placa(s) de \"50 km/h - Fiscalização Eletrônica de Velocidade \"',300,'Cachoeiras de Macacu','no canteiro lateral','incluir',0,'placa(s) R19 de 50 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"',100,'Cachoeiras de Macacu','no canteiro lateral','incluir',0,'placa(s) R19 de 60 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"',50,'Cachoeiras de Macacu','no canteiro lateral','remover',0,'placa(s) de \"50 km/h - Fiscalização Eletrônica de Velocidade \"',300,'Guapimirim','no canteiro lateral','incluir',0,'placa(s) R19 de 50 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"',150,'Guapimirim','no canteiro lateral','incluir',0,'0',0,'0','0','0',0,'0',0,'0','0','0',0,'0',0,'0','0','0',0,'0',0,'0','0','0',0,'0',0,'0','0','0',0),('RJ-122',18,'Ambos Sentidos','placa(s) de \"50 km/h - Fiscalização Eletrônica de Velocidade \"',300,'Guapimirim','no canteiro lateral','incluir',0,'0',0,'0','0',NULL,NULL,'0',0,'0','0',NULL,NULL,'0',0,'0','0',NULL,NULL,'0',0,'0','0',NULL,NULL,'0',0,'0','0',NULL,NULL,'0',0,'0','0',NULL,NULL,'0',0,'0','0',NULL,NULL,'0',0,'0','0',NULL,NULL,'0',0,'0','0',NULL,NULL),('RJ-122',22,'Ambos Sentidos','placa(s) de \"50 km/h - Fiscalização Eletrônica de Velocidade \"',300,'Cachoeiras de Macacu','no canteiro lateral','incluir',0,'placa(s) de \"60 km/h - Fiscalização Eletrônica\"',165,'Cachoeiras de Macacu','no canteiro lateral','remover',0,'placa(s) R19 de 50 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"',150,'Cachoeiras de Macacu','no canteiro lateral','incluir',0,'placa(s) R19 de 60 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"',90,'Cachoeiras de Macacu','no canteiro lateral','remover',0,'placa(s) de \"50 km/h - Fiscalização Eletrônica de Velocidade \"',300,'Guapimirim','no canteiro lateral','incluir',0,'placa(s) de \"60 km/h - Fiscalização Eletrônica\"',220,'Guapimirim','no canteiro lateral','remover',0,'placa(s) R19 de 60 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"',190,'Guapimirim','no canteiro lateral','remover',0,'placa(s) R19 de 50 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"',150,'Guapimirim','no canteiro lateral','incluir',0,'0',0,'0','0',NULL,NULL,'0',0,'0','0',NULL,NULL),('RJ-122',32,'Ambos Sentidos','placa(s) R19 de 50 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"',200,'Cachoeiras de Macacu','no canteiro lateral','incluir',0,'placa(s) de \"50 km/h - Fiscalização Eletrônica de Velocidade \"',200,'Guapimirim','no canteiro lateral','incluir',0,'placa(s) R19 de 60 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"',200,'Guapimirim','no canteiro lateral','remover',0,'0',0,'0','0',NULL,NULL,'0',0,'0','0',NULL,NULL,'0',0,'0','0',NULL,NULL,'0',0,'0','0',NULL,NULL,'0',0,'0','0',NULL,NULL,'0',0,'0','0',NULL,NULL,'0',0,'0','0',NULL,NULL),('RJ-186',0.4,'Ambos Sentidos','0',0,NULL,NULL,NULL,NULL,'0',0,NULL,NULL,NULL,NULL,'0',0,NULL,NULL,NULL,NULL,'0',0,NULL,NULL,NULL,NULL,'0',0,NULL,NULL,NULL,NULL,'0',0,NULL,NULL,NULL,NULL,'0',0,NULL,NULL,NULL,NULL,'0',0,NULL,NULL,NULL,NULL,'0',0,NULL,NULL,NULL,NULL,'0',0,NULL,NULL,NULL,NULL),('RJ-186',38,'Ambos Sentidos','placa(s) de \"50 km/h - Fiscalização Eletrônica de Velocidade \"',150,'Bom Jesus do Itabapoana','no canteiro lateral','incluir',0,'placa(s) de \"50 km/h - Fiscalização Eletrônica de Velocidade \"',150,'Santo Antônio de Pádua','no canteiro lateral','incluir',0,'placa(s) Lombada a 50 m',300,'Bom Jesus do Itabapoana','no canteiro lateral','remover',0,'placa(s) Lombada',250,'Bom Jesus do Itabapoana','no canteiro lateral','remover',0,'placa(s) Lombada',130,'Bom Jesus do Itabapoana','no canteiro lateral','remover',0,'placa(s) Lombada',200,'Santo Antônio de Pádua','no canteiro lateral','remover',0,'placa(s) Lombada',150,'Santo Antônio de Pádua','no canteiro lateral','remover',0,'placa(s) Lombada',145,'Santo Antônio de Pádua','no canteiro lateral','remover',0,'placa(s) Lombada',245,'Santo Antônio de Pádua','no canteiro lateral','remover',0,'0',0,'0','0',NULL,NULL),('RJ-186',44,'Ambos Sentidos','placa(s) de \"50 km/h - Fiscalização Eletrônica de Velocidade \"',300,'Bom Jesus do Itabapoana','no canteiro lateral','incluir',0,'placa(s) R19 de 50 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"',150,'Bom Jesus do Itabapoana','no canteiro lateral','incluir',0,'Atenção Lombada ',200,'Bom Jesus do Itabapoana','no canteiro lateral','remover',0,'placa(s) de \"50 km/h - Fiscalização Eletrônica de Velocidade \"',300,'Santo Antônio de Pádua','no canteiro lateral','incluir',0,'placa(s) R19 de 50 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"',200,'Santo Antônio de Pádua','no canteiro lateral','incluir',0,'placa(s) de \"50 km/h - Fiscalização Eletrônica de Velocidade \"',100,'Santo Antônio de Pádua','no canteiro lateral','incluir',0,'ATENÇÃO - Lombada ',100,'Santo Antônio de Pádua','no canteiro lateral','remover',0,'0',0,'0','0',NULL,NULL,'0',0,'0','0',NULL,NULL,'0',0,'0','0',NULL,NULL),('RJ-186',47,'Ambos Sentidos','placa(s) de \"50 km/h - Fiscalização Eletrônica de Velocidade \"',300,'Bom Jesus do Itabapoana','no canteiro lateral','incluir',0,'placa(s) R19 de 50 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"',250,'Bom Jesus do Itabapoana','no canteiro lateral','incluir',0,'placa(s) de \"50 km/h - Fiscalização Eletrônica de Velocidade \"',300,'Santo Antônio de Pádua','no canteiro lateral','incluir',0,'placa(s) R19 de 50 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"',150,'Santo Antônio de Pádua','no canteiro lateral','incluir',0,'0',0,'0','0',NULL,NULL,'0',0,'0','0',NULL,NULL,'0',0,'0','0',NULL,NULL,'0',0,'0','0',NULL,NULL,'0',0,'0','0',NULL,NULL,'0',0,'0','0',NULL,NULL),('RJ-186',54,'Ambos Sentidos','placa(s) de \"50 km/h - Fiscalização Eletrônica de Velocidade \"',300,'Santo Antônio de Pádua','no canteiro lateral','incluir',0,'placa(s) R19 de 50 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"',150,'Santo Antônio de Pádua','no canteiro lateral','incluir',0,'LOMBADA a 100 metros',300,'Bom Jesus do Itabapoana','no canteiro lateral','remover',0,'placa(s) R19 de 40 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"',450,'Bom Jesus do Itabapoana','no canteiro lateral','remover',0,'0',0,'0','0',NULL,NULL,'0',0,'0','0',NULL,NULL,'0',0,'0','0',NULL,NULL,'0',0,'0','0',NULL,NULL,'0',0,'0','0',NULL,NULL,'0',0,'0','0',NULL,NULL),('RJ-186',61,'Ambos Sentidos','placa(s) de \"60 km/h - Fiscalização Eletrônica de Velocidade \"',300,'Bom Jesus do Itabapoana','no canteiro lateral','',0,'placa(s) R19 de 60 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"',150,'Bom Jesus do Itabapoana','no canteiro lateral','',0,'placa de lombada a 50 metros',200,'Bom Jesus do Itabapoana','no canteiro lateral','remover',0,'(4) quatro placa(s) de Lombada',300,'Bom Jesus do Itabapoana','no canteiro lateral','remover',0,'placa(s) de \"60 km/h - Fiscalização Eletrônica de Velocidade \"',300,'Santo Antônio de Pádua','no canteiro lateral','incluir',0,'placa(s) R19 de 60 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"',150,'Santo Antônio de Pádua','no canteiro lateral','incluir',0,'placa de Lombada a 50 metros',600,'Santo Antônio de Pádua','no canteiro lateral','remover',0,'(4) quatro placa(s) de Lombada',600,'Santo Antônio de Pádua','no canteiro lateral','remover',0,'placa(s) R19 de 40 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"',300,'Bom Jesus do Itabapoana','no canteiro lateral','remover',0,'0',0,'0','0',NULL,NULL),('RJ-186',92,'Ambos Sentidos','placa(s) de \"50 km/h - Fiscalização Eletrônica de Velocidade \"',300,'Bom Jesus do Itabapoana','no canteiro lateral','incluir',0,'placa(s) R19 de 50 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"',150,'Bom Jesus do Itabapoana','no canteiro lateral','incluir',0,'placa(s) R19 de 60 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"',140,'Bom Jesus do Itabapoana','no canteiro lateral','remover',0,'placa(s) de \"50 km/h - Fiscalização Eletrônica de Velocidade \"',300,'Santo Antônio de Pádua','no canteiro lateral','incluir',0,'placa(s) R19 de 50 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"',150,'Santo Antônio de Pádua','no canteiro lateral','incluir',0,'0',0,'0','0',NULL,0,'0',0,'0','0',NULL,0,'0',0,'0','0',NULL,0,'0',0,'0','0',NULL,0,'0',0,'0','0',NULL,0),('RJ-186',4444,'Ambos Sentidos','placa(s) Lombada com seta',30,'Bom Jesus do Itabapoana','no canteiro lateral','remover',0,'placa(s) de \"50 km/h - Fiscalização Eletrônica de Velocidade \"',300,'Santo Antônio de Pádua','no canteiro lateral','incluir',0,'placa(s) R19 de 50 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"',150,'Santo Antônio de Pádua','no canteiro lateral','incluir',0,'(2) Duas placas Lombada',30,'Santo Antônio de Pádua','no canteiro lateral','remover',0,'(2) Duas placas Lombada',30,'Bom Jesus do Itabapoana','no canteiro lateral','remover',0,'0',0,'0','0',NULL,NULL,'0',0,'0','0',NULL,NULL,'0',0,'0','0',NULL,NULL,'0',0,'0','0',NULL,NULL,'0',0,'0','0',NULL,NULL);
/*!40000 ALTER TABLE `tplacas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tproj`
--

DROP TABLE IF EXISTS `tproj`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `tproj` (
  `Rodovia` varchar(8) NOT NULL,
  `kmEdital` float NOT NULL,
  `MunSen` varchar(45) NOT NULL,
  `Intro1` tinytext NOT NULL,
  `Intro2` tinytext NOT NULL,
  `Tit1` varchar(45) NOT NULL,
  `Tit2` varchar(45) NOT NULL,
  `DuasFolhas` int(11) NOT NULL DEFAULT '0',
  `inclusao` tinyint(4) NOT NULL,
  `remocao` tinyint(4) NOT NULL,
  `reposicionar` tinyint(4) NOT NULL,
  PRIMARY KEY (`Rodovia`,`kmEdital`,`MunSen`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tproj`
--

LOCK TABLES `tproj` WRITE;
/*!40000 ALTER TABLE `tproj` DISABLE KEYS */;
INSERT INTO `tproj` VALUES ('RJ-106',2,'Ambos Sentidos','O projeto de sinalização no trecho estudado, da rodovia ','consiste na inclusão e remoção de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','-','-',0,1,1,0),('RJ-106',6,'São Gonçalo','O projeto de sinalização no trecho estudado, da rodovia ','consiste na inclusão e remoção de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','-','-',0,1,1,0),('RJ-116',7,'Itaperuna','O projeto de sinalização no trecho estudado, da rodovia ','consiste na inclusão de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','','',0,1,1,0),('RJ-116',9.5,'Ambos Sentidos','O projeto de sinalização no trecho estudado, da rodovia ','consiste na inclusão de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','-','-',0,1,1,0),('RJ-116',20,'Ambos Sentidos','O projeto de sinalização no trecho estudado, da rodovia ','consiste na inclusão de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','-','-',0,1,1,0),('RJ-116',21.5,'Ambos Sentidos','O projeto de sinalização no trecho estudado, da rodovia ','consiste na inclusão de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','-','-',0,1,1,0),('RJ-116',27,'Ambos Sentidos','O projeto de sinalização no trecho estudado, da rodovia ','consiste na inclusão de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','-','-',0,1,1,0),('RJ-116',28,'Ambos Sentidos','O projeto de sinalização no trecho estudado, da rodovia ','consiste na inclusão de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','-','-',0,1,1,0),('RJ-116',39,'Duplo','O projeto de sinalização no trecho estudado, da rodovia ','consiste na inclusão de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','-','-',0,1,1,0),('RJ-116',46,'Duplo','O projeto de sinalização no trecho estudado, da rodovia ','consiste na inclusão e remoção de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','-','-',0,1,1,0),('RJ-116',68,'Duplo','O projeto de sinalização no trecho estudado, da rodovia ','consiste na inclusão e remoção de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','-','-',0,1,1,0),('RJ-116',70,'Duplo','O projeto de sinalização no trecho estudado, da rodovia ','consiste na remoção de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','-','-',0,1,1,0),('RJ-116',71.5,'Ambos Sentidos','O projeto de sinalização no trecho estudado, da rodovia ','consiste na pintura do asfalto para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','-','-',0,1,1,0),('RJ-116',78,'Duplo','O projeto de sinalização no trecho estudado, da rodovia ','consiste na inclusão e remoção de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','-','-',0,1,1,0),('RJ-116',88,'Duplo','O projeto de sinalização no trecho estudado, da rodovia ','consiste na inclusão e remoção de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','-','-',0,1,1,0),('RJ-116',95.5,'Ambos Sentidos','O projeto de sinalização no trecho estudado, da rodovia ','consiste na inclusão e remoção de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','-','-',0,1,1,0),('RJ-122',2.5,'Ambos Sentidos',' O projeto de sinalização no trecho estudado, da rodovia ','consiste na inclusão e remoção de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.',' ',' ',0,1,1,0),('RJ-122',18,'Ambos Sentidos',' ',' ',' ',' ',0,1,1,0),('RJ-122',22,'Ambos Sentidos',' ',' ',' ',' ',0,1,1,0),('RJ-122',32,'Ambos Sentidos',' ',' ',' ',' ',0,1,1,0),('RJ-186',0.4,'Ambos Sentidos',' ',' ',' ',' ',0,1,1,0),('RJ-186',38,'Ambos Sentidos',' ',' ',' ',' ',0,1,1,0),('RJ-186',44,'Ambos Sentidos',' ',' ',' ',' ',0,1,1,0),('RJ-186',47,'Ambos Sentidos',' ',' ',' ',' ',0,1,1,0),('RJ-186',54,'Ambos Sentidos',' ',' ',' ',' ',0,1,1,0),('RJ-186',61,'Ambos Sentidos',' ',' ',' ',' ',0,1,1,0),('RJ-186',92,'Ambos Sentidos',' O projeto de sinalização no trecho estudado, da rodovia',' ',' ',' ',0,1,1,0),('RJ-186',4444,'Ambos Sentidos',' ',' ',' ',' ',0,1,1,0);
/*!40000 ALTER TABLE `tproj` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `trodovias`
--

DROP TABLE IF EXISTS `trodovias`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `trodovias` (
  `Rodovia` varchar(15) NOT NULL,
  `munA` varchar(60) NOT NULL,
  `munB` varchar(60) NOT NULL,
  PRIMARY KEY (`Rodovia`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `trodovias`
--

LOCK TABLES `trodovias` WRITE;
/*!40000 ALTER TABLE `trodovias` DISABLE KEYS */;
INSERT INTO `trodovias` VALUES ('RJ-122','Guapimirim','Cachoeiras de Macacu'),('RJ-186','Santo Antônio de Pádua','Bom Jesus do Itabapoana');
/*!40000 ALTER TABLE `trodovias` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ttemfe`
--

DROP TABLE IF EXISTS `ttemfe`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ttemfe` (
  `Rodovia` varchar(8) NOT NULL,
  `kmEdital` float NOT NULL,
  `MunSen` varchar(45) NOT NULL,
  `vaiterfe` tinyint(4) DEFAULT NULL,
  `jatinhafe` tinyint(4) DEFAULT NULL,
  `foiconf` tinyint(4) DEFAULT NULL,
  PRIMARY KEY (`Rodovia`,`kmEdital`,`MunSen`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ttemfe`
--

LOCK TABLES `ttemfe` WRITE;
/*!40000 ALTER TABLE `ttemfe` DISABLE KEYS */;
INSERT INTO `ttemfe` VALUES ('RJ-106',2,'Ambos Sentidos',1,0,0),('RJ-106',6,'São Gonçalo',1,0,0),('RJ-116',7,'Itaperuna',1,0,0),('RJ-116',9.5,'Ambos Sentidos',1,0,0),('RJ-116',20,'Ambos Sentidos',1,0,0),('RJ-116',21.5,'Ambos Sentidos',1,0,0),('RJ-116',27,'Ambos Sentidos',1,1,1),('RJ-116',28,'Ambos Sentidos',1,1,1),('RJ-122',2.5,'Ambos Sentidos',1,0,0),('RJ-122',18,'Ambos Sentidos',1,1,1),('RJ-122',22,'Ambos Sentidos',1,0,0),('RJ-122',32,'Ambos Sentidos',1,1,1),('RJ-186',0.4,'Ambos Sentidos',0,0,0),('RJ-186',38,'Ambos Sentidos',1,1,1),('RJ-186',44,'Ambos Sentidos',1,0,1),('RJ-186',47,'Ambos Sentidos',1,1,1),('RJ-186',54,'Ambos Sentidos',1,1,1),('RJ-186',61,'Ambos Sentidos',1,1,1),('RJ-186',92,'Ambos Sentidos',1,0,1),('RJ-186',4444,'Ambos Sentidos',1,1,1);
/*!40000 ALTER TABLE `ttemfe` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ttipo_fat_risco`
--

DROP TABLE IF EXISTS `ttipo_fat_risco`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ttipo_fat_risco` (
  `fatr` varchar(300) NOT NULL,
  PRIMARY KEY (`fatr`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ttipo_fat_risco`
--

LOCK TABLES `ttipo_fat_risco` WRITE;
/*!40000 ALTER TABLE `ttipo_fat_risco` DISABLE KEYS */;
INSERT INTO `ttipo_fat_risco` VALUES ('Acostamento não delimitado'),('Área escolar'),('Curva perigosa'),('Entrada e saída de veículos'),('Existência de comércio'),('Existência de curvas'),('Existência de Igreja'),('Existência de ponto de ônibus'),('Existência de retorno'),('Ponte Estreita'),('Presença de animais'),('Trânsito de ciclistas'),('Travessia de pedestres');
/*!40000 ALTER TABLE `ttipo_fat_risco` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ttipo_historico`
--

DROP TABLE IF EXISTS `ttipo_historico`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ttipo_historico` (
  `hist` varchar(300) NOT NULL,
  PRIMARY KEY (`hist`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ttipo_historico`
--

LOCK TABLES `ttipo_historico` WRITE;
/*!40000 ALTER TABLE `ttipo_historico` DISABLE KEYS */;
INSERT INTO `ttipo_historico` VALUES ('Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN'),('Existência de Faixa de pedestres'),('Existência de Lombada física'),('Existência de semáforo no local');
/*!40000 ALTER TABLE `ttipo_historico` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ttipo_placas`
--

DROP TABLE IF EXISTS `ttipo_placas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ttipo_placas` (
  `desc` varchar(200) NOT NULL,
  PRIMARY KEY (`desc`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ttipo_placas`
--

LOCK TABLES `ttipo_placas` WRITE;
/*!40000 ALTER TABLE `ttipo_placas` DISABLE KEYS */;
INSERT INTO `ttipo_placas` VALUES ('pintura(s) de fiscalização eletrônica de velocidade no asfalto'),('placa(s) de \"40 km/h - Fiscalização Eletrônica de Velocidade \"'),('placa(s) de \"50 km/h - Fiscalização Eletrônica de Velocidade \"'),('placa(s) de \"60 km/h - Fiscalização Eletrônica de Velocidade \"'),('placa(s) de \"70 km/h - Fiscalização Eletrônica de Velocidade \"'),('placa(s) de \"80 km/h - Fiscalização Eletrônica de Velocidade \"'),('placa(s) de \"90 km/h - Fiscalização Eletrônica de Velocidade \"'),('placa(s) R19 de 40 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"'),('placa(s) R19 de 50 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"'),('placa(s) R19 de 60 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"'),('placa(s) R19 de 70 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"'),('placa(s) R19 de 80 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"'),('placa(s) R19 de 90 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"');
/*!40000 ALTER TABLE `ttipo_placas` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-12-22  3:13:59
