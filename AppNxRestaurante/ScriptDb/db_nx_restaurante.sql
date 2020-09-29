-- MySQL dump 10.13  Distrib 8.0.15, for Win64 (x86_64)
--
-- Host: localhost    Database: db_nx_restaurante
-- ------------------------------------------------------
-- Server version	8.0.15

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
-- Table structure for table `t_camarero`
--

DROP TABLE IF EXISTS `t_camarero`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `t_camarero` (
  `id_camarero` int(18) NOT NULL,
  `v_nombre` varchar(45) NOT NULL,
  `v_apellido1` varchar(45) NOT NULL,
  `v_apellido2` varchar(45) DEFAULT NULL,
  `b_activo` tinyint(4) NOT NULL,
  `f_creacion` datetime NOT NULL,
  `f_modificacion` datetime DEFAULT NULL,
  PRIMARY KEY (`id_camarero`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `t_camarero`
--

LOCK TABLES `t_camarero` WRITE;
/*!40000 ALTER TABLE `t_camarero` DISABLE KEYS */;
INSERT INTO `t_camarero` VALUES (11222333,'Andrea','Gonzales','Rave',1,'2020-09-28 01:12:09',NULL),(15151515,'Anderson','Rios','Vago',0,'0001-01-01 00:00:00',NULL);
/*!40000 ALTER TABLE `t_camarero` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `t_cliente`
--

DROP TABLE IF EXISTS `t_cliente`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `t_cliente` (
  `id_cliente` int(18) NOT NULL,
  `v_nombre` varchar(45) NOT NULL,
  `v_apellido1` varchar(45) NOT NULL,
  `v_apellido2` varchar(45) DEFAULT NULL,
  `b_activo` tinyint(4) NOT NULL,
  `f_creacion` datetime NOT NULL,
  `f_modificacion` datetime DEFAULT NULL,
  PRIMARY KEY (`id_cliente`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `t_cliente`
--

LOCK TABLES `t_cliente` WRITE;
/*!40000 ALTER TABLE `t_cliente` DISABLE KEYS */;
INSERT INTO `t_cliente` VALUES (1017143560,'Milker','Sanchez','Mosquera',1,'2020-09-25 22:11:40',NULL);
/*!40000 ALTER TABLE `t_cliente` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `t_cocinero`
--

DROP TABLE IF EXISTS `t_cocinero`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `t_cocinero` (
  `id_cocinero` int(18) NOT NULL,
  `v_nombre` varchar(45) NOT NULL,
  `v_apellido1` varchar(45) NOT NULL,
  `v_apellido2` varchar(45) DEFAULT NULL,
  `b_activo` tinyint(4) NOT NULL,
  `f_creacion` datetime NOT NULL,
  `f_modificacion` datetime DEFAULT NULL,
  PRIMARY KEY (`id_cocinero`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `t_cocinero`
--

LOCK TABLES `t_cocinero` WRITE;
/*!40000 ALTER TABLE `t_cocinero` DISABLE KEYS */;
INSERT INTO `t_cocinero` VALUES (1234567,'Jorge Celedon','Paz','García',1,'2020-09-27 18:54:08',NULL);
/*!40000 ALTER TABLE `t_cocinero` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `t_detalle_factura`
--

DROP TABLE IF EXISTS `t_detalle_factura`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `t_detalle_factura` (
  `id_detalle_factura` bigint(20) NOT NULL AUTO_INCREMENT,
  `id_factura` bigint(20) NOT NULL,
  `id_cocinero` int(18) NOT NULL,
  `v_plato` varchar(45) NOT NULL,
  `d_importe` decimal(18,2) NOT NULL,
  `d_valor` decimal(18,2) DEFAULT NULL,
  `b_activo` tinyint(4) NOT NULL,
  `f_creacion` datetime NOT NULL,
  `f_modificacion` datetime DEFAULT NULL,
  PRIMARY KEY (`id_detalle_factura`),
  KEY `fk_tfactura_idx` (`id_factura`),
  KEY `fk_tcocinero_idx` (`id_cocinero`),
  CONSTRAINT `fk_tCocinero` FOREIGN KEY (`id_cocinero`) REFERENCES `t_cocinero` (`id_cocinero`),
  CONSTRAINT `fk_tFactura` FOREIGN KEY (`id_factura`) REFERENCES `t_factura` (`id_factura`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `t_detalle_factura`
--

LOCK TABLES `t_detalle_factura` WRITE;
/*!40000 ALTER TABLE `t_detalle_factura` DISABLE KEYS */;
INSERT INTO `t_detalle_factura` VALUES (1,1,1234567,'prueba plato',50000.00,NULL,0,'0001-01-01 00:00:00',NULL),(2,1,1234567,'gratinado',25000.00,NULL,0,'0001-01-01 00:00:00',NULL);
/*!40000 ALTER TABLE `t_detalle_factura` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `t_factura`
--

DROP TABLE IF EXISTS `t_factura`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `t_factura` (
  `id_factura` bigint(20) NOT NULL AUTO_INCREMENT,
  `id_cliente` int(18) NOT NULL,
  `id_mesa` varchar(20) NOT NULL,
  `id_camarero` int(18) NOT NULL,
  `f_factura` datetime NOT NULL,
  `b_activo` tinyint(4) NOT NULL,
  `f_creacion` datetime NOT NULL,
  `f_modificacion` datetime DEFAULT NULL,
  PRIMARY KEY (`id_factura`),
  KEY `id_cliente_idx` (`id_cliente`),
  KEY `id_camarero_idx` (`id_camarero`),
  KEY `id_mesa_idx` (`id_mesa`),
  CONSTRAINT `fk_tcamarero` FOREIGN KEY (`id_camarero`) REFERENCES `t_camarero` (`id_camarero`),
  CONSTRAINT `fk_tcliente` FOREIGN KEY (`id_cliente`) REFERENCES `t_cliente` (`id_cliente`),
  CONSTRAINT `fk_tmesa` FOREIGN KEY (`id_mesa`) REFERENCES `t_mesa` (`id_mesa`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `t_factura`
--

LOCK TABLES `t_factura` WRITE;
/*!40000 ALTER TABLE `t_factura` DISABLE KEYS */;
INSERT INTO `t_factura` VALUES (1,1017143560,'mesa1',15151515,'2020-09-27 22:00:46',1,'2020-09-27 22:00:52',NULL);
/*!40000 ALTER TABLE `t_factura` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `t_log`
--

DROP TABLE IF EXISTS `t_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `t_log` (
  `id_log` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `f_date` datetime DEFAULT NULL,
  `v_thread` varchar(255) DEFAULT NULL,
  `v_level` varchar(50) DEFAULT NULL,
  `v_logger` varchar(255) DEFAULT NULL,
  `v_message` varchar(4000) DEFAULT NULL,
  `v_exception` varchar(2000) DEFAULT NULL,
  PRIMARY KEY (`id_log`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `t_log`
--

LOCK TABLES `t_log` WRITE;
/*!40000 ALTER TABLE `t_log` DISABLE KEYS */;
/*!40000 ALTER TABLE `t_log` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `t_mesa`
--

DROP TABLE IF EXISTS `t_mesa`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `t_mesa` (
  `id_mesa` varchar(20) NOT NULL,
  `n_maxComensales` int(11) NOT NULL,
  `v_ubicacion` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id_mesa`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `t_mesa`
--

LOCK TABLES `t_mesa` WRITE;
/*!40000 ALTER TABLE `t_mesa` DISABLE KEYS */;
INSERT INTO `t_mesa` VALUES ('mesa1',5,'Al lado de la puerta');
/*!40000 ALTER TABLE `t_mesa` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-09-29  1:02:31
