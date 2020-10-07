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
INSERT INTO `tpaginas` VALUES ('RJ-106',2,'Ambos Sentidos',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,1,1,1,1),('RJ-106',6,'São Gonçalo',1,1,1,1,1,1,1,1,1,1,0,1,1,1,1,1,0,1,0,1,1,1,1,1,1,1),('RJ-116',7,'Itaperuna',1,1,1,1,1,1,1,1,1,1,0,1,1,1,1,1,0,1,0,1,1,1,0,1,1,1),('RJ-116',9.5,'Ambos Sentidos',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,0,1,1,1),('RJ-116',20,'Ambos Sentidos',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,0,1,1,1),('RJ-116',21.5,'Ambos Sentidos',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,0,1,1,1),('RJ-116',27,'Ambos Sentidos',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,0,1,1,1),('RJ-116',28,'Ambos Sentidos',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,0,1,1,1),('RJ-116',39,'Duplo',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,1,1,1,1),('RJ-116',46,'Duplo',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,1,1,1,1),('RJ-116',68,'Duplo',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,1,1,1,1),('RJ-116',70,'Duplo',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,1,1,1,1),('RJ-116',71.5,'Ambos Sentidos',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,1,1,1,1),('RJ-116',78,'Duplo',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,0,1,1,1),('RJ-116',88,'Duplo',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,0,1,1,1),('RJ-116',95.5,'Ambos Sentidos',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,1,1,1,1),('RJ-122',2.5,'Ambos Sentidos',0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0);
/*!40000 ALTER TABLE `tpaginas` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-12-11  5:43:10
