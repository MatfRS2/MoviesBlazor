CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

START TRANSACTION;

CREATE TABLE `Film` (
    `Id` int NOT NULL,
    `Naslov` nvarchar(max) NULL,
    `DatumPocetkaPrikazivanja` datetime2 NOT NULL,
    `Zanr` nvarchar(max) NULL,
    `Ulozeno` decimal(18,2) NOT NULL,
    PRIMARY KEY (`Id`)
);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20221013195616_Inicijalna', '6.0.10');

COMMIT;

START TRANSACTION;

ALTER TABLE `Film` DROP COLUMN `Zanr`;

ALTER TABLE `Film` MODIFY `Ulozeno` money NOT NULL;

ALTER TABLE `Film` MODIFY `Naslov` nvarchar(250) NOT NULL DEFAULT '';

ALTER TABLE `Film` ADD `ZanrId` int NOT NULL DEFAULT 0;

CREATE TABLE `Zanr` (
    `Id` int NOT NULL,
    `Naziv` nvarchar(250) NOT NULL,
    PRIMARY KEY (`Id`)
);

CREATE INDEX `IX_Film_ZanrId` ON `Film` (`ZanrId`);

ALTER TABLE `Film` ADD CONSTRAINT `FK_Film_Zanr_ZanrId` FOREIGN KEY (`ZanrId`) REFERENCES `Zanr` (`Id`) ON DELETE CASCADE;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20221110162924_zanrovi', '6.0.10');

COMMIT;

START TRANSACTION;

ALTER TABLE `Film` MODIFY `DatumPocetkaPrikazivanja` date NOT NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20221111134427_ef-fluent-validation', '6.0.10');

COMMIT;

START TRANSACTION;

CREATE TABLE `Paket` (
    `Id` int NOT NULL,
    `Naziv` nvarchar(max) NOT NULL,
    `Opis` nvarchar(max) NOT NULL,
    `DatumFormiranja` date NOT NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `FilmPaket` (
    `FilmId` int NOT NULL,
    `PaketId` int NOT NULL,
    PRIMARY KEY (`FilmId`, `PaketId`),
    CONSTRAINT `FK_FilmPaket_Film_FilmId` FOREIGN KEY (`FilmId`) REFERENCES `Film` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_FilmPaket_Paket_PaketId` FOREIGN KEY (`PaketId`) REFERENCES `Paket` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_FilmPaket_PaketId` ON `FilmPaket` (`PaketId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20221111160935_dodati-paketi-filmova', '6.0.10');

COMMIT;

START TRANSACTION;

ALTER TABLE `Paket` MODIFY `Opis` nvarchar(1000) NOT NULL;

ALTER TABLE `Paket` MODIFY `Naziv` nvarchar(250) NOT NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20221111163331_dodat-kompozitni-kljuc-za-filmpaket', '6.0.10');

COMMIT;

START TRANSACTION;

CREATE TABLE `Korisnik` (
    `Id` int NOT NULL,
    `Email` nvarchar(250) NOT NULL,
    `Ime` nvarchar(150) NOT NULL,
    `Prezime` nvarchar(150) NOT NULL,
    `Potroseno` money NOT NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `Pretplata` (
    `Id` int NOT NULL,
    `Status` nvarchar(max) NOT NULL,
    `Iznos` money NOT NULL,
    `DatumIsteka` date NOT NULL,
    `KorisnikId` int NOT NULL,
    `PaketId` int NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Pretplata_Korisnik_KorisnikId` FOREIGN KEY (`KorisnikId`) REFERENCES `Korisnik` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Pretplata_Paket_PaketId` FOREIGN KEY (`PaketId`) REFERENCES `Paket` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_Pretplata_KorisnikId` ON `Pretplata` (`KorisnikId`);

CREATE INDEX `IX_Pretplata_PaketId` ON `Pretplata` (`PaketId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20221111192640_dodat-kompozitni-kljuc-za-korisnike-pretplate', '6.0.10');

COMMIT;


