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
  `dist1` int(11) DEFAULT NULL,
  `S1` varchar(45) DEFAULT NULL,
  `placa2` varchar(200) DEFAULT NULL,
  `dist2` int(11) DEFAULT NULL,
  `S2` varchar(45) DEFAULT NULL,
  `placa3` varchar(200) DEFAULT NULL,
  `dist3` int(11) DEFAULT NULL,
  `S3` varchar(45) DEFAULT NULL,
  `placa4` varchar(200) DEFAULT NULL,
  `dist4` int(11) DEFAULT NULL,
  `S4` varchar(45) DEFAULT NULL,
  `placa5` varchar(200) DEFAULT NULL,
  `dist5` int(11) DEFAULT NULL,
  `S5` varchar(45) DEFAULT NULL,
  `placa6` varchar(200) DEFAULT NULL,
  `dist6` int(11) DEFAULT NULL,
  `S6` varchar(45) DEFAULT NULL,
  `placa7` varchar(200) DEFAULT NULL,
  `dist7` int(11) DEFAULT NULL,
  `S7` varchar(45) DEFAULT NULL,
  `placa8` varchar(200) DEFAULT NULL,
  `dist8` int(11) DEFAULT NULL,
  `S8` varchar(45) DEFAULT NULL,
  `placa9` varchar(200) DEFAULT NULL,
  `dist9` int(11) DEFAULT NULL,
  `S9` varchar(45) DEFAULT NULL,
  `placa10` varchar(200) DEFAULT NULL,
  `dist10` int(11) DEFAULT NULL,
  `S10` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`Rodovia`,`kmEdital`,`MunSen`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tplacas`
--

LOCK TABLES `tplacas` WRITE;
/*!40000 ALTER TABLE `tplacas` DISABLE KEYS */;
INSERT INTO `tplacas` VALUES ('RJ-122',2.5,'Ambos Sentidos','0',0,NULL,'0',0,NULL,'0',0,NULL,'0',0,NULL,'0',0,NULL,'0',0,NULL,'0',0,NULL,'0',0,NULL,'0',0,NULL,'0',0,NULL);
/*!40000 ALTER TABLE `tplacas` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-12-11  5:43:13
