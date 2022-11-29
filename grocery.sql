-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: Jul 07, 2021 at 04:12 PM
-- Server version: 5.7.31
-- PHP Version: 7.3.21

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `grocery`
--
CREATE DATABASE IF NOT EXISTS `grocery` DEFAULT CHARACTER SET utf8 COLLATE utf8_unicode_ci;
USE `grocery`;

-- --------------------------------------------------------

--
-- Table structure for table `admin`
--

DROP TABLE IF EXISTS `admin`;
CREATE TABLE IF NOT EXISTS `admin` (
  `username` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `password` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `admin`
--

INSERT INTO `admin` (`username`, `password`) VALUES
('admin', '1234');

-- --------------------------------------------------------

--
-- Table structure for table `bill`
--

DROP TABLE IF EXISTS `bill`;
CREATE TABLE IF NOT EXISTS `bill` (
  `Detail_ID` int(11) NOT NULL AUTO_INCREMENT,
  `Date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `Master_ID` int(11) DEFAULT NULL,
  `Name_English` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Price` float DEFAULT NULL,
  `Discount` float DEFAULT NULL,
  `Quantity` double(4,2) NOT NULL,
  `Bill_total` float DEFAULT NULL,
  PRIMARY KEY (`Detail_ID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `category`
--

DROP TABLE IF EXISTS `category`;
CREATE TABLE IF NOT EXISTS `category` (
  `category_ID` varchar(100) COLLATE utf8_unicode_ci NOT NULL,
  `category_Name` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `company_Name` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`category_ID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `employee`
--

DROP TABLE IF EXISTS `employee`;
CREATE TABLE IF NOT EXISTS `employee` (
  `ID` varchar(100) COLLATE utf8_unicode_ci NOT NULL,
  `Name` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Position` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Tlephone` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Username` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Password` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Stand-in structure for view `final_bill`
-- (See below for the actual view)
--
DROP VIEW IF EXISTS `final_bill`;
CREATE TABLE IF NOT EXISTS `final_bill` (
`invoice_ID` int(11)
,`Date` timestamp
,`Total` float
,`Discount_Total` float
,`Paid` float
,`Balance` float
,`Username` varchar(50)
,`Detail_ID` int(11)
,`Master_ID` int(11)
,`Name_English` varchar(100)
,`Price` float
,`Discount` float
,`Quantity` double(4,2)
,`Bill_total` float
,`company_name` varchar(100)
,`footer_Message1` varchar(200)
,`footer_Message2` varchar(200)
,`company_Address` varchar(100)
,`tlephone` varchar(100)
);

-- --------------------------------------------------------

--
-- Table structure for table `invoice`
--

DROP TABLE IF EXISTS `invoice`;
CREATE TABLE IF NOT EXISTS `invoice` (
  `invoice_ID` int(11) NOT NULL AUTO_INCREMENT,
  `Date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `Total` float DEFAULT NULL,
  `Discount_Total` float DEFAULT NULL,
  `Paid` float DEFAULT NULL,
  `Balance` float DEFAULT NULL,
  `Username` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`invoice_ID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `item`
--

DROP TABLE IF EXISTS `item`;
CREATE TABLE IF NOT EXISTS `item` (
  `ID` varchar(100) COLLATE utf8_unicode_ci NOT NULL,
  `Name_English` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Category` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Price` float DEFAULT NULL,
  `Discount` float DEFAULT NULL,
  `Quantity` float DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `profile`
--

DROP TABLE IF EXISTS `profile`;
CREATE TABLE IF NOT EXISTS `profile` (
  `company_ID` varchar(100) COLLATE utf8_unicode_ci NOT NULL,
  `company_name` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `company_Address` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `tlephone` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `footer_Message1` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `footer_Message2` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `username` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `password` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`company_ID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Structure for view `final_bill`
--
DROP TABLE IF EXISTS `final_bill`;

DROP VIEW IF EXISTS `final_bill`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `final_bill`  AS  select `invoice`.`invoice_ID` AS `invoice_ID`,`invoice`.`Date` AS `Date`,`invoice`.`Total` AS `Total`,`invoice`.`Discount_Total` AS `Discount_Total`,`invoice`.`Paid` AS `Paid`,`invoice`.`Balance` AS `Balance`,`invoice`.`Username` AS `Username`,`bill`.`Detail_ID` AS `Detail_ID`,`bill`.`Master_ID` AS `Master_ID`,`bill`.`Name_English` AS `Name_English`,`bill`.`Price` AS `Price`,`bill`.`Discount` AS `Discount`,`bill`.`Quantity` AS `Quantity`,`bill`.`Bill_total` AS `Bill_total`,`profile`.`company_name` AS `company_name`,`profile`.`footer_Message1` AS `footer_Message1`,`profile`.`footer_Message2` AS `footer_Message2`,`profile`.`company_Address` AS `company_Address`,`profile`.`tlephone` AS `tlephone` from ((`invoice` join `bill` on((`invoice`.`invoice_ID` = `bill`.`Master_ID`))) join `profile`) ;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
