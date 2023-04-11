USE [AZURE_AT]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Pais]  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pais]') AND type in (N'U'))
DROP TABLE [dbo].[Pais]
GO

CREATE TABLE [dbo].[Pais](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](255) NOT NULL,
	[BandeiraId] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  StoredProcedure [dbo].[IncluiPais] ******/
DROP PROCEDURE [dbo].[IncluiPais]
GO

CREATE PROCEDURE [dbo].[IncluiPais]
	@Nome varchar(255),
	@BandeiraId varchar(255)
AS
	INSERT INTO Pais (Nome, BandeiraId)
	VALUES(@Nome, @BandeiraId);
	SELECT 
		Id,
		Nome,
		BandeiraId
	FROM Pais
	WHERE Id = SCOPE_IDENTITY();
RETURN 0
GO

/****** Object:  StoredProcedure [dbo].[AlteraPais] ******/
DROP PROCEDURE [dbo].[AlteraPais]
GO

CREATE PROCEDURE [dbo].[AlteraPais]
	@Id int,
	@Nome varchar(255),
	@BandeiraId varchar(255)
AS
	UPDATE Pais SET
	Nome = @Nome,
	BandeiraId = @BandeiraId
	WHERE Id = @Id;
	SELECT
		Id,
		Nome,
		BandeiraId
	FROM Pais
	WHERE Id = @Id;
RETURN 0
GO


/****** Object:  StoredProcedure [dbo].[ExcluiPais]  ******/
DROP PROCEDURE [dbo].[ExcluiPais]
GO

CREATE PROCEDURE [dbo].[ExcluiPais]
	@Id int
AS
	DELETE Pais
	WHERE Id = @Id
RETURN 0
GO

/****** Object:  StoredProcedure [dbo].[SelecionaPaises] ******/
DROP PROCEDURE [dbo].[SelecionaPaises]
GO

CREATE PROCEDURE [dbo].[SelecionaPaises]
AS
	SELECT 
		Id,
		Nome,
		BandeiraId
	FROM Pais
RETURN 0
GO

/****** Object:  StoredProcedure [dbo].[SelecionaPaisId] ******/
DROP PROCEDURE [dbo].[SelecionaPaisId]
GO

CREATE PROCEDURE [dbo].[SelecionaPaisId]
	@Id int
AS
	SELECT 
		Id,
		Nome,
		BandeiraId
	FROM Pais
	WHERE Id = @Id
RETURN 0
GO





