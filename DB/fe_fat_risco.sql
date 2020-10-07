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
-- Table structure for table `fat_risco`
--

DROP TABLE IF EXISTS `fat_risco`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `fat_risco` (
  `Rodovia` varchar(8) NOT NULL,
  `kmEdital` float NOT NULL,
  `MunSen` varchar(45) NOT NULL,
  `fatr1` tinytext,
  `fatr2` tinytext,
  `fatr3` tinytext,
  `fatr4` tinytext,
  `fatr5` tinytext,
  `fatr6` tinytext,
  `fatr7` tinytext,
  PRIMARY KEY (`Rodovia`,`kmEdital`,`MunSen`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fat_risco`
--

LOCK TABLES `fat_risco` WRITE;
/*!40000 ALTER TABLE `fat_risco` DISABLE KEYS */;
INSERT INTO `fat_risco` VALUES ('RJ-106',2,'Ambos Sentidos','Entrada e saída de veículos','Existência de ponto de ônibus','Curva perigosa','Travessia de pedestres','Existência de retorno','',''),('RJ-106',6,'São Gonçalo','Curva perigosa','Existência de retorno','Travessia de pedestres','','','',''),('RJ-116',7,'Itaperuna','Existência de ponto de ônibus','Travessia de pedestres','Área escolar','Pista dividida','Existência de comércio',NULL,NULL),('RJ-116',9.5,'Ambos Sentidos','Existência de ponto de ônibus','Travessia de pedestres','Área escolar','Existência de Comércio','Trânsito de ciclistas',NULL,NULL),('RJ-116',20,'Ambos Sentidos','Existência de ponto de ônibus','Travessia de pedestres','Existência de comércio','Trânsito de ciclistas','Travessia de pedestres',NULL,NULL),('RJ-116',21.5,'Ambos Sentidos','Área escolar','Existência de ponto de ônibus','Travessia de pedestres','Trânsito de ciclistas','Existência de comércio','',''),('RJ-116',27,'Ambos Sentidos','Existência de ponto de ônibus','Travessia de pedestres','Existência de comércio','Trânsito de ciclistas','  ',NULL,NULL),('RJ-116',28,'Ambos Sentidos','Existência de ponto de ônibus','Travessia de pedestres','Existência de comércio','Trânsito de ciclistas','','',''),('RJ-116',39,'Duplo','Existência de ponto de ônibus','Travessia de pedestres','Existência de comércio','Trânsito de ciclistas','  ',NULL,NULL),('RJ-116',46,'Duplo','Travessia de pedestres','Entrada e saída de veículos','  ','  ','  ',NULL,NULL),('RJ-116',68,'Duplo','Travessia de pedestres','Curva perigosa','  ','  ','  ',NULL,NULL),('RJ-116',70,'Duplo','Travessia de pedestres','Curva perigosa','Entrada e saida de veículos','  ','  ',NULL,NULL),('RJ-116',71.5,'Ambos Sentidos','Travessia de pedestres','Existência de comércio','Entrada e saida de veículos','  ','  ',NULL,NULL),('RJ-116',78,'Duplo','Travessia de pedestres','Existência de curvas','  ','  ','  ',NULL,NULL),('RJ-116',88,'Duplo','Travessia de pedestres','Área escolar','Existência de ponto de ônibus','Entrada e saída de veículos','  ',NULL,NULL),('RJ-116',95.5,'Ambos Sentidos','Entrada e saída de veículos','Existência de ponto de ônibus','Curva perigosa',' ','  ',NULL,NULL),('RJ-122',2.5,'Ambos Sentidos','Área escolar','Entrada e saída de veículos','Existência de comércio','Existência de ponto de ônibus','Trânsito de ciclistas','Travessia de pedestres','');
/*!40000 ALTER TABLE `fat_risco` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-12-11  5:43:07
