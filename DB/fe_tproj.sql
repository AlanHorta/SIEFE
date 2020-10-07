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
  PRIMARY KEY (`Rodovia`,`kmEdital`,`MunSen`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tproj`
--

LOCK TABLES `tproj` WRITE;
/*!40000 ALTER TABLE `tproj` DISABLE KEYS */;
INSERT INTO `tproj` VALUES ('RJ-106',2,'Ambos Sentidos','O projeto de sinalização no trecho estudado, da rodovia ','consiste na inclusão e remoção de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','-','-',0),('RJ-106',6,'São Gonçalo','O projeto de sinalização no trecho estudado, da rodovia ','consiste na inclusão e remoção de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','-','-',0),('RJ-116',7,'Itaperuna','O projeto de sinalização no trecho estudado, da rodovia ','consiste na inclusão de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','','',0),('RJ-116',9.5,'Ambos Sentidos','O projeto de sinalização no trecho estudado, da rodovia ','consiste na inclusão de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','-','-',0),('RJ-116',20,'Ambos Sentidos','O projeto de sinalização no trecho estudado, da rodovia ','consiste na inclusão de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','-','-',0),('RJ-116',21.5,'Ambos Sentidos','O projeto de sinalização no trecho estudado, da rodovia ','consiste na inclusão de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','-','-',0),('RJ-116',27,'Ambos Sentidos','O projeto de sinalização no trecho estudado, da rodovia ','consiste na inclusão de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','-','-',0),('RJ-116',28,'Ambos Sentidos','O projeto de sinalização no trecho estudado, da rodovia ','consiste na inclusão de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','-','-',0),('RJ-116',39,'Duplo','O projeto de sinalização no trecho estudado, da rodovia ','consiste na inclusão de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','-','-',0),('RJ-116',46,'Duplo','O projeto de sinalização no trecho estudado, da rodovia ','consiste na inclusão e remoção de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','-','-',0),('RJ-116',68,'Duplo','O projeto de sinalização no trecho estudado, da rodovia ','consiste na inclusão e remoção de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','-','-',0),('RJ-116',70,'Duplo','O projeto de sinalização no trecho estudado, da rodovia ','consiste na remoção de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','-','-',0),('RJ-116',71.5,'Ambos Sentidos','O projeto de sinalização no trecho estudado, da rodovia ','consiste na pintura do asfalto para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','-','-',0),('RJ-116',78,'Duplo','O projeto de sinalização no trecho estudado, da rodovia ','consiste na inclusão e remoção de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','-','-',0),('RJ-116',88,'Duplo','O projeto de sinalização no trecho estudado, da rodovia ','consiste na inclusão e remoção de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','-','-',0),('RJ-116',95.5,'Ambos Sentidos','O projeto de sinalização no trecho estudado, da rodovia ','consiste na inclusão e remoção de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.','-','-',0),('RJ-122',2.5,'Ambos Sentidos',' O projeto de sinalização no trecho estudado, da rodovia ','consiste na inclusão e remoção de placas para sinalizar e alertar os condutores de veículos quanto aos riscos do local e a necessidade de controle da velocidade.',' ',' ',0);
/*!40000 ALTER TABLE `tproj` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-12-11  5:42:56
