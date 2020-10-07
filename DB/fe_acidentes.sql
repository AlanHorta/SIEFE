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
INSERT INTO `acidentes` VALUES ('RJ-106',2,'Ambos Sentidos',2,3,4,3,0,1,0,2),('RJ-106',6,'São Gonçalo',6,2,3,1,0,1,0,1),('RJ-116',7,'Itaperuna',7,2,3,2,0,2,1,1),('RJ-116',9.5,'Ambos Sentidos',9.5,3,2,1,0,0,0,1),('RJ-116',20,'Ambos Sentidos',20,0,0,0,0,0,0,0),('RJ-116',21.5,'Ambos Sentidos',21.5,1,1,0,0,0,0,0),('RJ-116',27,'Ambos Sentidos',27,3,2,2,0,0,0,0),('RJ-116',28,'Ambos Sentidos',28,2,1,1,0,0,0,0),('RJ-116',39,'Duplo',39,1,1,0,1,0,0,0),('RJ-116',46,'Duplo',46,0,1,0,0,1,0,0),('RJ-116',68,'Duplo',68,2,0,1,2,0,0,0),('RJ-116',70,'Duplo',70,2,1,1,3,2,0,0),('RJ-116',71.5,'Ambos Sentidos',71.5,1,2,2,0,0,0,0),('RJ-116',78,'Duplo',78,0,1,0,0,0,0,0),('RJ-116',88,'Duplo',88,1,0,2,0,0,0,0),('RJ-116',95.5,'Ambos Sentidos',95.5,0,0,0,0,0,0,0),('RJ-122',2.5,'Ambos Sentidos',2.5,0,0,0,0,0,0,0);
/*!40000 ALTER TABLE `acidentes` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-12-11  5:43:12
