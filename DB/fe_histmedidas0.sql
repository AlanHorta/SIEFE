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
INSERT INTO `histmedidas` VALUES ('RJ-106',2,'Ambos Sentidos','Existência de passarela de pedestres','',NULL,NULL,NULL),('RJ-106',6,'São Gonçalo','Existência de passarela de pedestres','',NULL,NULL,NULL),('RJ-116',7,'Itaperuna','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN.',NULL,NULL,NULL,NULL),('RJ-116',9.5,'Ambos Sentidos','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN.',NULL,NULL,NULL,NULL),('RJ-116',20,'Ambos Sentidos','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN.','Existência de Faixa de pedestres','Existência de Lombada física',NULL,NULL),('RJ-116',21.5,'Ambos Sentidos','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN.','Existência de Faixa de pedestres','Existência de Lombada física',NULL,NULL),('RJ-116',27,'Ambos Sentidos','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN.','Equipamento de Fiscalização Eletrônica presente no local','',NULL,NULL),('RJ-116',28,'Ambos Sentidos','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN.','Equipamento de Fiscalização Eletrônica presente no local','',NULL,NULL),('RJ-116',39,'Duplo','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN.','Equipamento de Fiscalização Eletrônica presente no local','',NULL,NULL),('RJ-116',46,'Duplo','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN.','Equipamento de Fiscalização Eletrônica presente no local','',NULL,NULL),('RJ-116',68,'Duplo','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN.','Equipamento de Fiscalização Eletrônica presente no local','Existência de semáforo no local',NULL,NULL),('RJ-116',70,'Duplo','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN.','Equipamento de Fiscalização Eletrônica presente no local','Existência de faixa de pedestres no local',NULL,NULL),('RJ-116',71.5,'Ambos Sentidos','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN.','Equipamento de Fiscalização Eletrônica presente no local','',NULL,NULL),('RJ-116',78,'Duplo','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN.','Equipamento de Fiscalização Eletrônica presente no local',NULL,NULL,NULL),('RJ-116',88,'Duplo','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN.','Equipamento de Fiscalização Eletrônica presente no local',NULL,NULL,NULL),('RJ-116',95.5,'Ambos Sentidos','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN.','Equipamento de Fiscalização Eletrônica presente no local',NULL,NULL,NULL),('RJ-122',2.5,'Ambos Sentidos','Existe sinalização vertical e horizontal no trecho de acordo com as normas do CONTRAN','','','','');
/*!40000 ALTER TABLE `histmedidas` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-12-12  5:45:53
