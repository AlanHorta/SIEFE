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
INSERT INTO `tgeometria` VALUES ('RJ-106',2,'Ambos Sentidos',0,0,1,1,1,1,1,1,1,1,0,0,'São Gonçalo','Macaé','retrato'),('RJ-106',6,'São Gonçalo',0,0,1,1,1,1,1,1,0,0,0,1,'São Gonçalo','Macaé','retrato'),('RJ-116',7,'Itaperuna',0,0,1,1,1,1,1,1,1,1,1,1,'Itaperuna','-','retrato'),('RJ-116',9.5,'Ambos Sentidos',0,0,1,1,1,1,1,1,1,1,1,0,'Itaperuna','Itaboraí','paisagem'),('RJ-116',20,'Ambos Sentidos',0,0,1,1,1,1,1,1,1,1,1,0,'Itaperuna','Itaboraí','retrato'),('RJ-116',21.5,'Ambos Sentidos',0,0,1,0,1,1,1,1,1,1,1,0,'Itaperuna','Itaboraí','retrato'),('RJ-116',27,'Ambos Sentidos',0,0,1,0,1,1,1,1,1,1,1,0,'Itaperuna','Itaboraí','retrato'),('RJ-116',28,'Ambos Sentidos',0,0,1,0,1,1,1,0,1,1,0,0,'Itaperuna','Itaboraí','retrato'),('RJ-116',39,'Duplo',0,0,1,0,1,1,1,0,1,1,0,0,'Itaperuna','Itaboraí','retrato'),('RJ-116',46,'Duplo',0,0,1,1,1,1,1,0,0,0,0,0,'Itaperuna','Itaboraí','retrato'),('RJ-116',68,'Duplo',0,1,0,1,1,1,1,1,0,0,0,0,'Itaperuna','Itaboraí','retrato'),('RJ-116',70,'Duplo',0,0,0,1,1,1,1,1,0,0,0,0,'Itaperuna','Itaboraí','retrato'),('RJ-116',71.5,'Ambos Sentidos',0,1,0,1,1,1,1,1,0,0,0,0,'Itaperuna','Itaboraí','retrato'),('RJ-116',78,'Duplo',0,1,0,1,1,1,1,0,0,0,0,0,'Itaperuna','Itaboraí','retrato'),('RJ-116',88,'Duplo',0,0,1,1,1,1,1,0,0,0,0,0,'Itaperuna','Itaboraí','retrato'),('RJ-116',95.5,'Ambos Sentidos',0,1,0,1,0,0,0,0,0,0,0,0,'Itaperuna','Itaboraí','retrato'),('RJ-122',2.5,'Ambos Sentidos',0,0,0,0,0,0,0,0,0,0,0,0,'0','0','0');
/*!40000 ALTER TABLE `tgeometria` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-12-11  5:43:04
