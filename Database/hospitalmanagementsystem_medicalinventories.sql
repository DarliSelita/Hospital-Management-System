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
-- Table structure for table `medicalinventories`
--

DROP TABLE IF EXISTS `medicalinventories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `medicalinventories` (
  `MedicalInventoryId` int NOT NULL AUTO_INCREMENT,
  `MedicationId` int DEFAULT NULL,
  `QuantityOnHand` int DEFAULT NULL,
  `ReorderLevel` int DEFAULT NULL,
  `SupplierInformation` varchar(255) DEFAULT NULL,
  `ManufacturingDate` datetime DEFAULT NULL,
  `ExpiryDate` datetime DEFAULT NULL,
  `StorageLocation` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`MedicalInventoryId`),
  KEY `MedicationId` (`MedicationId`),
  CONSTRAINT `medicalinventories_ibfk_1` FOREIGN KEY (`MedicationId`) REFERENCES `medications` (`MedicationId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=131 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `medicalinventories`
--

LOCK TABLES `medicalinventories` WRITE;
/*!40000 ALTER TABLE `medicalinventories` DISABLE KEYS */;
INSERT INTO `medicalinventories` VALUES (3,65,1800,1,'MedPharma','2024-01-21 11:58:50','2025-07-23 11:58:49','Tirane'),(4,2,1800,1,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(5,3,1800,2,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(6,4,1800,2,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(7,5,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(8,6,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(9,7,1800,1,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(10,8,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(11,9,1800,1,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(12,10,1800,2,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(13,11,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(14,12,1800,2,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(15,13,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(16,14,1800,1,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(17,15,1800,2,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(18,16,1800,2,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(19,17,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(20,18,1800,2,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(21,19,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(22,20,1800,1,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(23,21,1800,2,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(24,22,1800,2,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(25,23,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(26,24,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(27,25,1800,1,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(28,26,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(29,27,1800,1,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(30,28,1800,2,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(31,29,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(32,30,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(33,31,1800,2,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(34,33,1800,1,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(35,34,1798,2,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(36,35,1800,2,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(37,36,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(38,37,1800,2,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(39,38,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(40,39,1800,1,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(41,40,1800,2,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(42,41,1800,2,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(43,42,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(44,43,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(45,44,1800,1,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(46,45,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(47,46,1800,1,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(48,47,1800,2,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(49,48,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(50,49,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(51,50,1800,2,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(52,51,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(53,52,1800,1,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(54,53,1800,2,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(55,54,1800,2,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(56,55,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(57,56,1800,2,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(58,57,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(59,58,1800,1,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(60,59,1800,2,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(61,60,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(62,61,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(63,62,1800,1,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(64,63,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(65,64,1800,1,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(66,65,1800,2,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(67,66,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(68,67,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(69,68,1800,2,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(70,69,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(71,70,1800,1,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(72,71,1800,2,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(73,72,1800,2,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(74,73,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(75,74,1800,2,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(76,75,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(77,82,1800,1,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(78,83,1800,2,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(79,84,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(80,85,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(81,86,1800,2,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(82,87,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(83,88,1800,1,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(84,89,1800,2,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(85,90,1800,2,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(86,91,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(87,92,1800,2,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(88,93,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(89,94,1800,1,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(90,95,1800,2,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(91,96,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(92,97,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(93,98,1800,2,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(94,99,1800,3,'CoPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Fier'),(95,100,1800,1,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane'),(96,101,1800,2,'MedPharma','2022-10-21 15:23:35','2026-04-21 15:23:35','Tirane');
/*!40000 ALTER TABLE `medicalinventories` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-01-22  9:31:54
