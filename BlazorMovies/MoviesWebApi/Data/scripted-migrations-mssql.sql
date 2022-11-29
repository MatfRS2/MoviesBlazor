IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Film] (
    [Id] int NOT NULL IDENTITY,
    [Naslov] nvarchar(max) NULL,
    [DatumPocetkaPrikazivanja] datetime2 NOT NULL,
    [Zanr] nvarchar(max) NULL,
    [Ulozeno] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_Film] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221013195616_Inicijalna', N'6.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Film]') AND [c].[name] = N'Zanr');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Film] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Film] DROP COLUMN [Zanr];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Film]') AND [c].[name] = N'Ulozeno');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Film] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Film] ALTER COLUMN [Ulozeno] money NOT NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Film]') AND [c].[name] = N'Naslov');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Film] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Film] ALTER COLUMN [Naslov] nvarchar(250) NOT NULL;
ALTER TABLE [Film] ADD DEFAULT N'' FOR [Naslov];
GO

ALTER TABLE [Film] ADD [ZanrId] int NOT NULL DEFAULT 0;
GO

CREATE TABLE [Zanr] (
    [Id] int NOT NULL,
    [Naziv] nvarchar(250) NOT NULL,
    CONSTRAINT [PK_Zanr] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_Film_ZanrId] ON [Film] ([ZanrId]);
GO

ALTER TABLE [Film] ADD CONSTRAINT [FK_Film_Zanr_ZanrId] FOREIGN KEY ([ZanrId]) REFERENCES [Zanr] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221110162924_zanrovi', N'6.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Film]') AND [c].[name] = N'DatumPocetkaPrikazivanja');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Film] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Film] ALTER COLUMN [DatumPocetkaPrikazivanja] date NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221111134427_ef-fluent-validation', N'6.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Paket] (
    [Id] int NOT NULL IDENTITY,
    [Naziv] nvarchar(max) NOT NULL,
    [Opis] nvarchar(max) NOT NULL,
    [DatumFormiranja] date NOT NULL,
    CONSTRAINT [PK_Paket] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [FilmPaket] (
    [FilmId] int NOT NULL,
    [PaketId] int NOT NULL,
    CONSTRAINT [PK_FilmPaket] PRIMARY KEY ([FilmId], [PaketId]),
    CONSTRAINT [FK_FilmPaket_Film_FilmId] FOREIGN KEY ([FilmId]) REFERENCES [Film] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_FilmPaket_Paket_PaketId] FOREIGN KEY ([PaketId]) REFERENCES [Paket] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_FilmPaket_PaketId] ON [FilmPaket] ([PaketId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221111160935_dodati-paketi-filmova', N'6.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Paket]') AND [c].[name] = N'Opis');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Paket] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Paket] ALTER COLUMN [Opis] nvarchar(1000) NOT NULL;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Paket]') AND [c].[name] = N'Naziv');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Paket] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Paket] ALTER COLUMN [Naziv] nvarchar(250) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221111163331_dodat-kompozitni-kljuc-za-filmpaket', N'6.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Korisnik] (
    [Id] int NOT NULL IDENTITY,
    [Email] nvarchar(250) NOT NULL,
    [Ime] nvarchar(150) NOT NULL,
    [Prezime] nvarchar(150) NOT NULL,
    [Potroseno] money NOT NULL,
    CONSTRAINT [PK_Korisnik] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Pretplata] (
    [Id] int NOT NULL IDENTITY,
    [Status] nvarchar(max) NOT NULL,
    [Iznos] money NOT NULL,
    [DatumIsteka] date NOT NULL,
    [KorisnikId] int NOT NULL,
    [PaketId] int NOT NULL,
    CONSTRAINT [PK_Pretplata] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Pretplata_Korisnik_KorisnikId] FOREIGN KEY ([KorisnikId]) REFERENCES [Korisnik] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Pretplata_Paket_PaketId] FOREIGN KEY ([PaketId]) REFERENCES [Paket] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Pretplata_KorisnikId] ON [Pretplata] ([KorisnikId]);
GO

CREATE INDEX [IX_Pretplata_PaketId] ON [Pretplata] ([PaketId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221111192640_dodat-kompozitni-kljuc-za-korisnike-pretplate', N'6.0.10');
GO

COMMIT;
GO

