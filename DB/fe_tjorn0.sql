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
INSERT INTO `tjorn` VALUES ('RJ-106',2,'Ambos Sentidos','   '),('RJ-116',7,'Itaperuna','Sem material jornalistico'),('RJ-116',9.5,'Ambos Sentidos','Sem material jornalistico'),('RJ-116',20,'Ambos Sentidos','Sem material jornalistico'),('RJ-116',21.5,'Ambos Sentidos','Sem material jornalistico'),('RJ-116',27,'Ambos Sentidos','Sem material jornalistico'),('RJ-116',28,'Ambos Sentidos','Sem material jornalistico'),('RJ-116',39,'Duplo','  '),('RJ-116',46,'Duplo','  '),('RJ-116',68,'Duplo','    '),('RJ-116',70,'Duplo','    '),('RJ-116',71.5,'Ambos Sentidos','    '),('RJ-116',78,'Duplo','Sem material jornalistico'),('RJ-116',88,'Duplo','Sem material jornalistico'),('RJ-116',95.5,'Ambos Sentidos','   '),('RJ-122',2.5,'Ambos Sentidos','  ');
/*!40000 ALTER TABLE `tjorn` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-12-12  5:45:50
