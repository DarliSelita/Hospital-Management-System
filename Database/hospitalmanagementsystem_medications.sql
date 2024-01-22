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
-- Table structure for table `medications`
--

DROP TABLE IF EXISTS `medications`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `medications` (
  `MedicationId` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) DEFAULT NULL,
  `Dosage` varchar(255) DEFAULT NULL,
  `Frequency` varchar(255) DEFAULT NULL,
  `Price` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`MedicationId`)
) ENGINE=InnoDB AUTO_INCREMENT=102 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `medications`
--

LOCK TABLES `medications` WRITE;
/*!40000 ALTER TABLE `medications` DISABLE KEYS */;
INSERT INTO `medications` VALUES (2,'Serenitol','10mg','Once a day',19.99),(3,'ProximaXen','20mg','Twice a day',29.99),(4,'FlexiCalm','15mg','Once a day',24.99),(5,'BioRegenix','25mg','Three times a day',34.99),(6,'EquiVitae','30mg','Once a day',39.99),(7,'NeuroZen','5mg','Once a day',14.99),(8,'PurityPlus','40mg','Twice a day',44.99),(9,'OmniRelief','10mg','Once a day',19.99),(10,'CardiaGuard','15mg','Once a day',24.99),(11,'DuraFlex','25mg','Twice a day',34.99),(12,'VitaCure','12.5mg','Once a day',22.99),(13,'ZenithFlow','30mg','Once a day',39.99),(14,'LunaPulse','10mg','Once a day',19.99),(15,'AlloHeal','15mg','Once a day',24.99),(16,'SolaceSoothe','20mg','Twice a day',29.99),(17,'VitaPro','25mg','Three times a day',34.99),(18,'Tranquilix','15mg','Once a day',24.99),(19,'RapidRestore','30mg','Once a day',39.99),(20,'NeoVital','10mg','Once a day',19.99),(21,'ZephyrAid','20mg','Twice a day',29.99),(22,'MediTonic','15mg','Once a day',24.99),(23,'ResilientRx','25mg','Three times a day',34.99),(24,'HarmonyEase','30mg','Once a day',39.99),(25,'EverGlow','5mg','Once a day',14.99),(26,'QuantumWell','40mg','Twice a day',44.99),(27,'VitaFlare','10mg','Once a day',19.99),(28,'HarmonyDose','15mg','Once a day',24.99),(29,'BioBalance','25mg','Three times a day',34.99),(30,'OptiViva','30mg','Once a day',39.99),(31,'RadiantRevive','12.5mg','Once a day',22.99),(32,'ZenovaSync','30mg','Once a day',39.99),(33,'UltraElixir','10mg','Once a day',19.99),(34,'CoreRejuvenate','15mg','Once a day',24.99),(35,'ZenithBlend','20mg','Twice a day',29.99),(36,'VitaPulse','25mg','Three times a day',34.99),(37,'AstraZenith','15mg','Once a day',24.99),(38,'OptiFlex','30mg','Once a day',39.99),(39,'BioHarmony','10mg','Once a day',19.99),(40,'NovaCure','20mg','Twice a day',29.99),(41,'DynaVibe','15mg','Once a day',24.99),(42,'TranquilAura','25mg','Three times a day',34.99),(43,'NeuroBliss','30mg','Once a day',39.99),(44,'PinnacleRelief','5mg','Once a day',14.99),(45,'QuantaWell','40mg','Twice a day',44.99),(46,'Meditide','10mg','Once a day',19.99),(47,'HarmonyWave','15mg','Once a day',24.99),(48,'EquiViva','25mg','Three times a day',34.99),(49,'OptiHeal','30mg','Once a day',39.99),(50,'SolisAid','12.5mg','Once a day',22.99),(51,'QuantumEase','30mg','Once a day',39.99),(52,'BioVerve','10mg','Once a day',19.99),(53,'ZenithSpark','15mg','Once a day',24.99),(54,'VitalAegis','20mg','Twice a day',29.99),(55,'SerenityPlus','25mg','Three times a day',34.99),(56,'LunaVitality','15mg','Once a day',24.99),(57,'EquiPulse','30mg','Once a day',39.99),(58,'NovaGlo','10mg','Once a day',19.99),(59,'PureCalm','15mg','Once a day',24.99),(60,'VitaSoothe','25mg','Three times a day',34.99),(61,'RadiantGlow','30mg','Once a day',39.99),(62,'ZenithAura','5mg','Once a day',14.99),(63,'AstraEase','40mg','Twice a day',44.99),(64,'PurityBlend','10mg','Once a day',19.99),(65,'OptiRevive','15mg','Once a day',24.99),(66,'ResilientBlend','25mg','Three times a day',34.99),(67,'LunaElixir','30mg','Once a day',39.99),(68,'SereneWave','12.5mg','Once a day',22.99),(69,'NeuroHarmony','30mg','Once a day',39.99),(70,'VitaSpark','10mg','Once a day',19.99),(71,'TranquilBlend','15mg','Once a day',24.99),(72,'HarmonyFlare','20mg','Twice a day',29.99),(73,'SolisDose','25mg','Three times a day',34.99),(74,'QuantumWave','15mg','Once a day',24.99),(75,'MediTonic','30mg','Once a day',39.99),(76,'BioVitality','10mg','Once a day',19.99),(77,'LunaFlex','15mg','Once a day',24.99),(78,'ZenovaVibe','25mg','Three times a day',34.99),(79,'CorePulse','30mg','Once a day',39.99),(80,'PinnacleCalm','5mg','Once a day',14.99),(81,'EquiAura','40mg','Twice a day',44.99),(82,'VitaHarmony','10mg','Once a day',19.99),(83,'RadiantBlend','15mg','Once a day',24.99),(84,'EverEase','25mg','Three times a day',34.99),(85,'ZephyrSoothe','30mg','Once a day',39.99),(86,'OptiRelief','12.5mg','Once a day',22.99),(87,'NeuroPulse','30mg','Once a day',39.99),(88,'TranquilRevive','10mg','Once a day',19.99),(89,'BioRevitalize','15mg','Once a day',24.99),(90,'NovaCalm','20mg','Twice a day',29.99),(91,'HarmonyVibe','25mg','Three times a day',34.99),(92,'QuantumRevive','15mg','Once a day',24.99),(93,'VitaBliss','30mg','Once a day',39.99),(94,'LunaEase','10mg','Once a day',19.99),(95,'ZenithRevitalize','15mg','Once a day',24.99),(96,'SolaceBlend','25mg','Three times a day',34.99),(97,'EquiRevive','30mg','Once a day',39.99),(98,'BioVitalize','12.5mg','Once a day',22.99),(99,'OptiBliss','30mg','Once a day',39.99),(100,'RadiantEase','10mg','Once a day',19.99),(101,'TranquilGlow','15mg','Once a day',24.99);
/*!40000 ALTER TABLE `medications` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-01-22  9:31:57
