DROP DATABASE IF EXISTS `fruitshop`;
CREATE DATABASE `fruitshop`;
USE `fruitshop`;


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------



-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;

SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------


CREATE TABLE `CustomerSet`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Username` longtext NOT NULL, 
	`Password` longtext NOT NULL, 
	`Balance` double NOT NULL);

ALTER TABLE `CustomerSet` ADD PRIMARY KEY (`Id`);





CREATE TABLE `OwnedProductSet`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Quantity` int NOT NULL, 
	`CustomerId` int NOT NULL, 
	`ProductId` int NOT NULL);

ALTER TABLE `OwnedProductSet` ADD PRIMARY KEY (`Id`);





CREATE TABLE `ProductSet`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` longtext NOT NULL, 
	`Price` double NOT NULL, 
	`Quantity` int NOT NULL);

ALTER TABLE `ProductSet` ADD PRIMARY KEY (`Id`);







-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------


-- Creating foreign key on `CustomerId` in table 'OwnedProductSet'

ALTER TABLE `OwnedProductSet`
ADD CONSTRAINT `FK_CustomerOwnedProduct`
    FOREIGN KEY (`CustomerId`)
    REFERENCES `CustomerSet`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerOwnedProduct'

CREATE INDEX `IX_FK_CustomerOwnedProduct`
    ON `OwnedProductSet`
    (`CustomerId`);



-- Creating foreign key on `ProductId` in table 'OwnedProductSet'

ALTER TABLE `OwnedProductSet`
ADD CONSTRAINT `FK_ProductOwnedProduct`
    FOREIGN KEY (`ProductId`)
    REFERENCES `ProductSet`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;


-- Creating non-clustered index for FOREIGN KEY 'FK_ProductOwnedProduct'

CREATE INDEX `IX_FK_ProductOwnedProduct`
    ON `OwnedProductSet`
    (`ProductId`);



-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
