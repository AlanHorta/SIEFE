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
INSERT INTO `ponto_fe` VALUES ('RJ-106',2,'Ambos Sentidos',2,'Arsenal','São Gonçalo','2','São Gonçalo','Macaé',50,'22°51\'33.85\"S','43° 0\'37.43\"O',28096,63,'I.A','22°51\'37.41\"S','43° 0\'34.87\"O',63,27465),('RJ-106',6,'São Gonçalo',6,'Rio do Ouro','São Gonçalo','2','São Gonçalo','Macaé',50,'22°52\'59.36\"S','42°59\'4.72\"O',25367,63,'I.A','zero','zero',63,0),('RJ-116',7,'Itaperuna',7,'Sambaetiba','Itaboraí','2','Itaboraí','Itaperuna',50,'22°40\'54.39\"S','42°46\'42.76\"O',4176,60,'I.A','zero','zero',0,0),('RJ-116',9.5,'Ambos Sentidos',9.5,'Sambaetiba','Itaboraí','2','Itaboraí','Itaperuna',50,'22°39\'44.94\"S','42°46\'04.99\"O',4935,55,'I.A','zero','zero',55,5376),('RJ-116',20,'Ambos Sentidos',20,'Papucaia','Cachoeiras de Macacu','2','Itaboraí','Itaperuna',50,'22°36\'12.38\"S','42°44\'23.04\"O',2556,46,'I.A','zero','zero',45,2363),('RJ-116',21.5,'Ambos Sentidos',21.5,'Papucaia Ribeira','Cachoeiras de Macacu','2','Itaboraí','Itaperuna',50,' 22°35\'29.63\"S',' 42°43\'36.14\"O',2658,49,'I.A','zero','zero',49,2543),('RJ-116',27,'Ambos Sentidos',27,'Japuiba','Cachoeiras de Macacu','2','Itaboraí','Itaperuna',50,' 22°33\'57.21\"S',' 42°41\'19.24\"O',2422,46,'I.A','zero','zero',48,2387),('RJ-116',28,'Ambos Sentidos',28,'Santana de Japuiba','Cachoeiras de Macacu','2','Itaboraí','Itaperuna',50,'22°33\'36.50\"S','42°41\'17.02\"O',2062,47,'I.A','zero','zero',47,2021),('RJ-116',39,'Duplo',39,'Cachoeiras de Macacu','Cachoeiras de Macacu','2','Itaboraí','Itaperuna',50,'22°29\'13.44\"S','42°39\'48.83\"O',2359,48,'I.A','zero','zero',47,2241),('RJ-116',46,'Duplo',46,'Cachoeiras de Macacu','Cachoeiras de Macacu','2','Itaboraí','Itaperuna',50,'22°25\'59.61\"S','42°38\'26.16\"O',1830,48,'I.A','zero','zero',47,1772),('RJ-116',68,'Duplo',68,'Muri','Nova Friburgo','2','Itaboraí','Itaperuna',50,'22°21\'37.63\"S','42°31\'45.82\"O',4418,49,'I.A','zero','zero',49,4286),('RJ-116',70,'Duplo',70,'Muri','Nova Friburgo','2','Itaboraí','Itaperuna',50,'22°21\'30.21\"S','42°31\'7.13\"O',4187,49,'I.A','zero','zero',49,4148),('RJ-116',71.5,'Ambos Sentidos',71.5,'Muri','Nova Friburgo','2','Itaboraí','Itaperuna',60,'22°20\'49.58\"S','42°30\'29.70\"O',7947,56,'II.B','22°20\'48.99\"S','42°30\'30.67\"O',58,7894),('RJ-116',78,'Duplo',78,'Nova Friburgo','Nova Friburgo','2','Itaboraí','Itaperuna',50,'22°18\'0.74\"S','42°31\'17.18\"O',5480,47,'I.A','zero','zero',47,5239),('RJ-116',88,'Duplo',88,'Nova Friburgo','Nova Friburgo','2','Itaboraí','Itaperuna',50,'22°13\'21.91\"S','42°30\'47.79\"O',1941,44,'I.A','zero','zero',44,1985),('RJ-116',95.5,'Ambos Sentidos',95.5,'Banquete','Bom Jardim','2','Itaboraí','Itaperuna',50,'22°11\'0.47\"S','42°28\'27.23\"O',4385,49,'III.C','22°10\'58.80\"S','42°28\'25.26\"O',47,4148),('RJ-122',2.5,'Ambos Sentidos',2.5,'Guapimirim','Guapimirim','2','Guapimirim','Cachoeiras de Macacu',50,'22°33 10.86 S','42°57 55.01 O',3559,74,'I.A','zero','zero',72,3476);
/*!40000 ALTER TABLE `ponto_fe` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-12-12  5:45:57
