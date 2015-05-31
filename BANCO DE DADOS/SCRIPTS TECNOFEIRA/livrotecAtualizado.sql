Create database livrotec;
use livrotec;

/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50153
Source Host           : localhost:3306
Source Database       : livrotec

Target Server Type    : MYSQL
Target Server Version : 50153
File Encoding         : 65001

Date: 2011-11-24 18:33:41
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `tbl_acervo`
-- ----------------------------
DROP TABLE IF EXISTS `tbl_acervo`;
CREATE TABLE `tbl_acervo` (
  `cod_acervo` bigint(20) NOT NULL AUTO_INCREMENT,
  `tbl_autor_cod_autor` int(10) unsigned DEFAULT NULL,
  `tbl_genero_cod_genero` int(10) unsigned DEFAULT NULL,
  `tbl_tipo_acervo_cod_tipo_acervo` int(10) unsigned DEFAULT NULL,
  `tbl_idioma_cod_idioma` int(10) unsigned DEFAULT NULL,
  `tbl_editora_cod_editora` int(10) unsigned DEFAULT NULL,
  `titulo` varchar(255) NOT NULL,
  `sub_titulo` varchar(255) DEFAULT NULL,
  `data_aquisicao` date NOT NULL,
  `exemplar` varchar(20) NOT NULL,
  `volume` varchar(20) DEFAULT NULL,
  `edicao` varchar(50) DEFAULT NULL,
  `data_edicao` date DEFAULT NULL,
  `observacoes` text,
  `numero_paginas` int(11) DEFAULT NULL,
  `preco` double DEFAULT NULL,
  `cdd` varchar(35) DEFAULT NULL,
  `cdu` varchar(35) DEFAULT NULL,
  `cutter` varchar(35) DEFAULT NULL,
  `isbn` varchar(35) DEFAULT NULL,
  `status_acervo` varchar(20) NOT NULL,
  `referencia_bibliografica` text,
  `resenha` text,
  `periodicidade` varchar(100) DEFAULT NULL,
  `codigobarra` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`cod_acervo`),
  KEY `tbl_editora_cod_editora` (`tbl_editora_cod_editora`),
  KEY `tbl_idioma_cod_idioma` (`tbl_idioma_cod_idioma`),
  KEY `tbl_tipo_acervo_cod_tipo_acervo` (`tbl_tipo_acervo_cod_tipo_acervo`),
  KEY `tbl_genero_cod_genero` (`tbl_genero_cod_genero`),
  KEY `tbl_autor_cod_autor` (`tbl_autor_cod_autor`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of tbl_acervo
-- ----------------------------

-- ----------------------------
-- Table structure for `tbl_autor`
-- ----------------------------
DROP TABLE IF EXISTS `tbl_autor`;
CREATE TABLE `tbl_autor` (
  `cod_autor` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `autor` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`cod_autor`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of tbl_autor
-- ----------------------------

-- ----------------------------
-- Table structure for `tbl_cliente`
-- ----------------------------
DROP TABLE IF EXISTS `tbl_cliente`;
CREATE TABLE `tbl_cliente` (
  `cod_leitor` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `cliente` varchar(200) NOT NULL,
  `rg` varchar(20) DEFAULT NULL,
  `cpf` varchar(20) DEFAULT NULL,
  `dt_nascimento` date DEFAULT NULL,
  `dt_cadastro` date NOT NULL,
  `sexo` char(1) NOT NULL,
  `responsavel` varchar(200) DEFAULT NULL,
  `observacao` text,
  `matricula` int(11) DEFAULT NULL,
  `turma` varchar(10) DEFAULT NULL,
  `periodo` char(1) DEFAULT NULL,
  `multa` double DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `cep` varchar(20) DEFAULT NULL,
  `logradouro` varchar(255) DEFAULT NULL,
  `cidade` varchar(255) DEFAULT NULL,
  `bairro` varchar(255) DEFAULT NULL,
  `numero` tinyint(4) DEFAULT NULL,
  `complemento` varchar(255) DEFAULT NULL,
  `uf` char(2) DEFAULT NULL,
  `telefone` bigint(20) DEFAULT NULL,
  `celular` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`cod_leitor`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of tbl_cliente
-- ----------------------------

-- ----------------------------
-- Table structure for `tbl_editora`
-- ----------------------------
DROP TABLE IF EXISTS `tbl_editora`;
CREATE TABLE `tbl_editora` (
  `cod_editora` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `razao_social` varchar(255) NOT NULL,
  `nome_fantasia` varchar(255) NOT NULL,
  `cnpj` varchar(20) DEFAULT NULL,
  `site` varchar(255) DEFAULT NULL,
  `contato` text,
  `email` varchar(255) DEFAULT NULL,
  `telefone` varchar(14) DEFAULT NULL,
  `fax` varchar(14) DEFAULT NULL,
  `cep` varchar(20) DEFAULT NULL,
  `logradouro` varchar(255) DEFAULT NULL,
  `cidade` varchar(255) DEFAULT NULL,
  `bairro` varchar(255) DEFAULT NULL,
  `numero` tinyint(4) DEFAULT NULL,
  `complemento` varchar(255) DEFAULT NULL,
  `uf` char(2) DEFAULT NULL,
  PRIMARY KEY (`cod_editora`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of tbl_editora
-- ----------------------------

-- ----------------------------
-- Table structure for `tbl_emprestimo`
-- ----------------------------
DROP TABLE IF EXISTS `tbl_emprestimo`;
CREATE TABLE `tbl_emprestimo` (
  `cod_emprestimo` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `tbl_acervo_cod_acervo` bigint(20) NOT NULL,
  `tbl_cliente_cod_leitor` int(10) unsigned NOT NULL,
  `data_aluguel` date DEFAULT NULL,
  `data_devolucao` date DEFAULT NULL,
  `estado` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`cod_emprestimo`),
  KEY `tbl_cliente_cod_leitor` (`tbl_cliente_cod_leitor`),
  KEY `tbl_acervo_cod_acervo` (`tbl_acervo_cod_acervo`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of tbl_emprestimo
-- ----------------------------

-- ----------------------------
-- Table structure for `tbl_genero`
-- ----------------------------
DROP TABLE IF EXISTS `tbl_genero`;
CREATE TABLE `tbl_genero` (
  `cod_genero` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `genero` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`cod_genero`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of tbl_genero
-- ----------------------------

-- ----------------------------
-- Table structure for `tbl_idioma`
-- ----------------------------
DROP TABLE IF EXISTS `tbl_idioma`;
CREATE TABLE `tbl_idioma` (
  `cod_idioma` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `idioma` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`cod_idioma`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of tbl_idioma
-- ----------------------------

-- ----------------------------
-- Table structure for `tbl_tipo_acervo`
-- ----------------------------
DROP TABLE IF EXISTS `tbl_tipo_acervo`;
CREATE TABLE `tbl_tipo_acervo` (
  `cod_tipo_acervo` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `tipo_acervo` varchar(200) NOT NULL,
  `emprestimo` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`cod_tipo_acervo`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of tbl_tipo_acervo
-- ----------------------------

-- ----------------------------
-- Table structure for `tbl_usuario`
-- ----------------------------
DROP TABLE IF EXISTS `tbl_usuario`;
CREATE TABLE `tbl_usuario` (
  `cod_usuario` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `nome` varchar(200) NOT NULL,
  `login` varchar(255) NOT NULL,
  `senha` text NOT NULL,
  `tipo` int(11) NOT NULL,
  `cod_seguranca` varchar(30) NOT NULL,
  PRIMARY KEY (`cod_usuario`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of tbl_usuario
-- ----------------------------
