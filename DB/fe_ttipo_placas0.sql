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
-- Table structure for table `ttipo_placas`
--

DROP TABLE IF EXISTS `ttipo_placas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ttipo_placas` (
  `desc` varchar(200) NOT NULL,
  PRIMARY KEY (`desc`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ttipo_placas`
--

LOCK TABLES `ttipo_placas` WRITE;
/*!40000 ALTER TABLE `ttipo_placas` DISABLE KEYS */;
INSERT INTO `ttipo_placas` VALUES ('pintura(s) de fiscalização eletrônica de velocidade no asfalto'),('placa(s) de \"40 km/h - Fiscalização Eletrônica de Velocidade \"'),('placa(s) de \"50 km/h - Fiscalização Eletrônica de Velocidade \"'),('placa(s) de \"60 km/h - Fiscalização Eletrônica de Velocidade \"'),('placa(s) de \"70 km/h - Fiscalização Eletrônica de Velocidade \"'),('placa(s) de \"80 km/h - Fiscalização Eletrônica de Velocidade \"'),('placa(s) de \"90 km/h - Fiscalização Eletrônica de Velocidade \"'),('placa(s) R19 de 40 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"'),('placa(s) R19 de 50 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"'),('placa(s) R19 de 60 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"'),('placa(s) R19 de 70 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"'),('placa(s) R19 de 80 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"'),('placa(s) R19 de 90 km/h \"VELOCIDADE MÁXIMA PERMITIDA\"');
/*!40000 ALTER TABLE `ttipo_placas` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-12-12  5:45:56
