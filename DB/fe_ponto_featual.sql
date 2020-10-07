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
INSERT INTO `ponto_featual` VALUES ('RJ-122',2.5,'Ambos Sentidos',2.5,'Guapimirim','Guapimirim',2,'Guapimirim','Cachoeiras de Macacu',50,'22°33 10.86 S','42°57 55.01 O',3559,74,'I.A','zero','zero',72,3476);
/*!40000 ALTER TABLE `ponto_featual` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-12-11  5:43:08
