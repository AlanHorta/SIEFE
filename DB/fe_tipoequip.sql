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
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-12-11  5:42:56