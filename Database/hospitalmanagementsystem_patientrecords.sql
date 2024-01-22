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
-- Table structure for table `patientrecords`
--

DROP TABLE IF EXISTS `patientrecords`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `patientrecords` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `PatientFile` varchar(255) DEFAULT NULL,
  `PatientName` varchar(255) DEFAULT NULL,
  `Gender` varchar(255) DEFAULT NULL,
  `PhoneNumber` varchar(15) DEFAULT NULL,
  `ReasonForAppointment` text,
  `Allergies` text,
  `MedicationsAndVaccines` text,
  `TobaccoUse` tinyint(1) DEFAULT NULL,
  `AlcoholConsumptionPerWeek` double DEFAULT NULL,
  `DrugUse` tinyint(1) DEFAULT NULL,
  `DiagnosticId` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `PatientFile` (`PatientFile`),
  KEY `DiagnosticId` (`DiagnosticId`)
) ENGINE=InnoDB AUTO_INCREMENT=46 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patientrecords`
--

LOCK TABLES `patientrecords` WRITE;
/*!40000 ALTER TABLE `patientrecords` DISABLE KEYS */;
INSERT INTO `patientrecords` VALUES (28,'1111111','daaa','Male','045445332','book','fdg','dsf',1,40,1,0),(30,'5767575','zds',NULL,NULL,NULL,'','',0,0,0,0),(31,'6777','sa',NULL,NULL,NULL,'','',0,0,0,0),(33,'6662','cvjc',NULL,NULL,NULL,'','',0,0,0,0),(34,NULL,NULL,NULL,NULL,NULL,'','',0,0,0,0),(36,'345323123','er',NULL,NULL,NULL,'','',0,0,0,0),(37,'1232','qe',NULL,NULL,NULL,'','',0,0,0,0),(38,NULL,NULL,NULL,NULL,NULL,'','',0,0,0,0),(39,'21321111','ashd',NULL,NULL,NULL,'','',0,0,0,0),(40,'998888','cx',NULL,NULL,NULL,'','',0,0,0,0),(41,'4555','dds',NULL,NULL,NULL,'','',0,0,0,0),(42,'1212144444','sasa',NULL,NULL,NULL,'','',0,0,0,0),(43,'546423225','vcv',NULL,NULL,NULL,'','',0,0,0,0),(45,'24388483','agha',NULL,NULL,NULL,'','',0,0,0,0);
/*!40000 ALTER TABLE `patientrecords` ENABLE KEYS */;
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
