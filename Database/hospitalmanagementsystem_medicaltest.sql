CREATE DATABASE  IF NOT EXISTS `hospitalmanagementsystem` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `hospitalmanagementsystem`;
-- MySQL dump 10.13  Distrib 8.0.32, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: hospitalmanagementsystem
-- ------------------------------------------------------
-- Server version	8.0.32

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `medicaltest`
--

DROP TABLE IF EXISTS `medicaltest`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `medicaltest` (
  `TestId` int NOT NULL AUTO_INCREMENT,
  `TestName` varchar(255) DEFAULT NULL,
  `TestDate` datetime DEFAULT NULL,
  `FilePath` varchar(255) DEFAULT NULL,
  `DiagnosticId` int DEFAULT NULL,
  `TestFile` blob,
  PRIMARY KEY (`TestId`),
  KEY `DiagnosticId` (`DiagnosticId`),
  CONSTRAINT `medicaltest_ibfk_1` FOREIGN KEY (`DiagnosticId`) REFERENCES `patientrecords` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=33 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `medicaltest`
--

LOCK TABLES `medicaltest` WRITE;
/*!40000 ALTER TABLE `medicaltest` DISABLE KEYS */;
INSERT INTO `medicaltest` VALUES (25,'X-Ray','2024-01-18 21:36:11','C:\\Users\\user\\Downloads\\Ushtrime mbi Baze te Dhenash - Elton Domnori.pdf',34,NULL),(26,'Checkup','2024-01-18 21:41:50','C:\\Users\\user\\Downloads\\Leksioni IV Arkitekture Kompjuteri.pdf',37,NULL),(27,'X-Ray','2024-01-18 21:53:34','C:\\Users\\user\\Downloads\\Ushtrime mbi Baze te Dhenash - Elton Domnori.pdf',40,NULL),(28,'Checkup','2024-01-18 21:53:44','C:\\Users\\user\\Downloads\\UMT_Project.pdf',40,NULL),(29,'X-Ray','2024-01-18 21:55:31','C:\\Users\\user\\Downloads\\Ushtrime mbi Baze te Dhenash - Elton Domnori.pdf',41,NULL),(30,'X-Ray','2024-01-18 22:03:03','C:\\Users\\user\\Downloads\\Ushtrime mbi Baze te Dhenash - Elton Domnori.pdf',43,NULL),(32,'X-Ray','2024-01-18 22:11:35','C:\\Users\\user\\Downloads\\Leksioni III Arkitekture.pdf',45,NULL);
/*!40000 ALTER TABLE `medicaltest` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-01-22  9:31:53
