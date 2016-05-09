-- MySQL Script generated by MySQL Workbench
-- 05/05/16 09:48:10
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema sentinelwarehousedb
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema sentinelwarehousedb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `sentinelwarehousedb` DEFAULT CHARACTER SET utf8 ;
USE `sentinelwarehousedb` ;

-- -----------------------------------------------------
-- Table `sentinelwarehousedb`.`Client`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sentinelwarehousedb`.`Client` (
  `CIF` VARCHAR(20) NOT NULL,
  `nom` VARCHAR(45) NULL,
  `cognom` VARCHAR(45) NULL,
  `adreça` VARCHAR(45) NULL,
  `codiPostal` VARCHAR(45) NULL,
  `pais` VARCHAR(45) NULL,
  `actiu` TINYINT(1) NULL,
  PRIMARY KEY (`CIF`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sentinelwarehousedb`.`Contacte`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sentinelwarehousedb`.`Contacte` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `personaNom` VARCHAR(45) NOT NULL,
  `telef` VARCHAR(10) NULL,
  `telef2` VARCHAR(10) NULL,
  `mob` VARCHAR(10) NULL,
  `mob2` VARCHAR(10) NULL,
  `correuElectronic` VARCHAR(45) NULL,
  `Client_CIF` VARCHAR(20) NOT NULL,
  PRIMARY KEY (`id`, `Client_CIF`),
  INDEX `fk_Contacte_Client1_idx` (`Client_CIF` ASC),
  CONSTRAINT `fk_Contacte_Client1`
    FOREIGN KEY (`Client_CIF`)
    REFERENCES `sentinelwarehousedb`.`Client` (`CIF`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sentinelwarehousedb`.`Producte`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sentinelwarehousedb`.`Producte` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `nom` VARCHAR(45) NULL,
  `preuKg` DOUBLE NULL,
  `unitatCaixa` INT NULL,
  `EAN13` VARCHAR(15) NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sentinelwarehousedb`.`Comanda`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sentinelwarehousedb`.`Comanda` (
  `codi` INT NOT NULL AUTO_INCREMENT,
  `dataComanda` DATE NULL,
  `dataEntrega` DATE NULL,
  `Client_CIF` VARCHAR(20) NOT NULL,
  `Client_Contacte_id` INT NOT NULL,
  `estat` VARCHAR(20) NULL,
  PRIMARY KEY (`codi`, `Client_CIF`, `Client_Contacte_id`),
  INDEX `fk_Comanda_Client1_idx` (`Client_CIF` ASC, `Client_Contacte_id` ASC),
  CONSTRAINT `fk_Comanda_Client1`
    FOREIGN KEY (`Client_CIF`)
    REFERENCES `sentinelwarehousedb`.`Client` (`CIF`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sentinelwarehousedb`.`LiniaComanda`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sentinelwarehousedb`.`LiniaComanda` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `preuKg` DOUBLE NULL,
  `quantitat` INT NULL,
  `Comanda_codi` INT NOT NULL,
  `Comanda_Client_CIF` VARCHAR(20) NOT NULL,
  `Comanda_Client_Contacte_id` INT NOT NULL,
  `Producte_id` INT NOT NULL,
  PRIMARY KEY (`id`, `Comanda_codi`, `Comanda_Client_CIF`, `Comanda_Client_Contacte_id`, `Producte_id`),
  INDEX `fk_LiniaComanda_Comanda1_idx` (`Comanda_codi` ASC, `Comanda_Client_CIF` ASC, `Comanda_Client_Contacte_id` ASC),
  INDEX `fk_LiniaComanda_Producte1_idx` (`Producte_id` ASC),
  CONSTRAINT `fk_LiniaComanda_Comanda1`
    FOREIGN KEY (`Comanda_codi` , `Comanda_Client_CIF` , `Comanda_Client_Contacte_id`)
    REFERENCES `sentinelwarehousedb`.`Comanda` (`codi` , `Client_CIF` , `Client_Contacte_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_LiniaComanda_Producte1`
    FOREIGN KEY (`Producte_id`)
    REFERENCES `sentinelwarehousedb`.`Producte` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sentinelwarehousedb`.`Albara`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sentinelwarehousedb`.`Albara` (
  `codi` INT NOT NULL AUTO_INCREMENT,
  `dataAlbara` DATETIME NULL,
  `dataEntrega` DATETIME NULL,
  `Comanda_codi` INT NOT NULL,
  `Comanda_Client_CIF` VARCHAR(20) NOT NULL,
  `Comanda_Client_Contacte_id` INT NOT NULL,
  PRIMARY KEY (`codi`, `Comanda_codi`, `Comanda_Client_CIF`, `Comanda_Client_Contacte_id`),
  INDEX `fk_Albara_Comanda1_idx` (`Comanda_codi` ASC, `Comanda_Client_CIF` ASC, `Comanda_Client_Contacte_id` ASC),
  CONSTRAINT `fk_Albara_Comanda1`
    FOREIGN KEY (`Comanda_codi` , `Comanda_Client_CIF` , `Comanda_Client_Contacte_id`)
    REFERENCES `sentinelwarehousedb`.`Comanda` (`codi` , `Client_CIF` , `Client_Contacte_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sentinelwarehousedb`.`LiniaAlbara`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sentinelwarehousedb`.`LiniaAlbara` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `codiTracabilitat` VARCHAR(45) NOT NULL,
  `producteNom` VARCHAR(45) NULL,
  `Albara_codi` INT NOT NULL,
  `Albara_Comanda_codi` INT NOT NULL,
  `Albara_Comanda_Client_CIF` VARCHAR(20) NOT NULL,
  `Albara_Comanda_Client_Contacte_id` INT NOT NULL,
  PRIMARY KEY (`id`, `Albara_codi`, `Albara_Comanda_codi`, `Albara_Comanda_Client_CIF`, `Albara_Comanda_Client_Contacte_id`),
  INDEX `fk_LiniaAlbara_Albara1_idx` (`Albara_codi` ASC, `Albara_Comanda_codi` ASC, `Albara_Comanda_Client_CIF` ASC, `Albara_Comanda_Client_Contacte_id` ASC),
  CONSTRAINT `fk_LiniaAlbara_Albara1`
    FOREIGN KEY (`Albara_codi` , `Albara_Comanda_codi` , `Albara_Comanda_Client_CIF` , `Albara_Comanda_Client_Contacte_id`)
    REFERENCES `sentinelwarehousedb`.`Albara` (`codi` , `Comanda_codi` , `Comanda_Client_CIF` , `Comanda_Client_Contacte_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;