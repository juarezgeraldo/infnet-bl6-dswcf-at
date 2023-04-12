USE [AZURE_AT]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Estado]') AND type in (N'U'))
DROP TABLE [dbo].[Estado]
GO

CREATE TABLE [dbo].[Estado](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](100) NOT NULL,
	[BandeiraId] [varchar](255) NULL,
	[PaisId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Estado]  WITH CHECK ADD  CONSTRAINT [fk_estado_pais] FOREIGN KEY([PaisId])
REFERENCES [dbo].[Pais] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Estado] CHECK CONSTRAINT [fk_estado_pais]
GO


/****** Object:  StoredProcedure [dbo].[IncluiEstado] ******/
DROP PROCEDURE [dbo].[IncluiEstado]
GO

CREATE PROCEDURE [dbo].[IncluiEstado]
	@Nome varchar(100),
	@BandeiraId varchar(255),
	@PaisId int
AS
	INSERT INTO Estado(Nome, BandeiraId, PaisId)
	VALUES (@Nome, @BandeiraId, @PaisId)
	SELECT 
		Id,
		Nome,
		BandeiraId,
		PaisId
	FROM Estado
	WHERE Id = SCOPE_IDENTITY();
RETURN 0
GO

/****** Object:  StoredProcedure [dbo].[AtualizaEstado] ******/
DROP PROCEDURE [dbo].[AlteraEstado]
GO

CREATE PROCEDURE [dbo].[AlteraEstado]
	@Id int,
	@Nome varchar(100),
	@BandeiraId varchar(255),
	@PaisId int
AS
	UPDATE Estado SET
	Nome = @Nome,
	BandeiraId = @BandeiraId,
	PaisId = @PaisId
	WHERE Id = @Id
RETURN 0
GO

/****** Object:  StoredProcedure [dbo].[ExcluiEstado] ******/
DROP PROCEDURE [dbo].[ExcluiEstado]
GO

CREATE PROCEDURE [dbo].[ExcluiEstado]
	@Id int
AS
	DELETE Estado
	WHERE Id = @Id
RETURN 0
GO

/****** Object:  StoredProcedure [dbo].[SelecionaEstados] ******/
DROP PROCEDURE [dbo].[SelecionaEstados]
GO

CREATE PROCEDURE [dbo].[SelecionaEstados]
AS
	SELECT Estado.Id,
		   Estado.Nome,
		   Estado.BandeiraId,
		   Estado.PaisId,
		   Pais.Nome AS PaisNome
	FROM Estado
	INNER JOIN Pais ON Pais.Id = Estado.PaisId
RETURN 0
GO

/****** Object:  StoredProcedure [dbo].[SelecionaEstadoId] ******/
DROP PROCEDURE [dbo].[SelecionaEstadoId]
GO

CREATE PROCEDURE [dbo].[SelecionaEstadoId]
	@Id int
AS
	SELECT Estado.Id,
		   Estado.Nome,
		   Estado.BandeiraId,
		   Estado.PaisId,
		   Pais.Nome AS PaisNome
	FROM Estado
	INNER JOIN Pais ON Pais.Id = Estado.PaisId
	WHERE Estado.Id = @Id
RETURN 0
GO

/****** Object:  StoredProcedure [dbo].[SelecionaEstadoPais] ******/
DROP PROCEDURE [dbo].[SelecionaEstadosPais]
GO

CREATE PROCEDURE [dbo].[SelecionaEstadosPais]
	@PaisId int
AS
	SELECT Estado.Id as Id,
		   Estado.Nome as Nome,
		   Estado.BandeiraId as BandeiraId,
		   Estado.PaisId as PaisId,
		   Pais.Nome as PaisNome
	FROM Estado 
	INNER JOIN Pais ON Pais.Id = Estado.PaisId
	WHERE Estado.PaisId = @PaisId
RETURN 0
GO


