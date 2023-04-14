USE [AZURE_AT]
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Amigo] ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Amigo]') AND type in (N'U'))
DROP TABLE [dbo].[Amigo]
GO

CREATE TABLE [dbo].[Amigo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NOT NULL,
	[Sobrenome] [varchar](50) NOT NULL,
	[FotografiaId] [varchar](255) NULL,
	[Email] [varchar](50) NOT NULL,
	[Telefone] [varchar](25) NULL,
	[Nascimento] [date] NULL,
	[PaisId] [int] NOT NULL,
	[EstadoId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  StoredProcedure [dbo].[IncluiAmigo] ******/
DROP PROCEDURE [dbo].[IncluiAmigo]
GO

CREATE PROCEDURE [dbo].[IncluiAmigo]
	@Nome varchar(50),
	@Sobrenome varchar(50),
	@FotografiaId varchar(255),
	@Email varchar(50),
	@Telefone varchar(50),
	@Nascimento date,
	@PaisId int,
	@EstadoId int
AS
	INSERT INTO Amigo(
						Nome,
						Sobrenome,
						FotografiaId,
						Email,
						Telefone,
						Nascimento,
						PaisId,
						EstadoId
						)
	VALUES (
		@Nome,
		@Sobrenome,
		@FotografiaId,
		@Email,
		@Telefone,
		@Nascimento,
		@PaisId,
		@EstadoId
		);
	SELECT
		Id,
		Nome,
		Sobrenome,
		FotografiaId,
		Email,
		Telefone,
		Nascimento,
		PaisId,
		EstadoId
	FROM Amigo
	WHERE Id = SCOPE_IDENTITY()
RETURN 0
GO

/****** Object:  StoredProcedure [dbo].[AlteraAmigo] ******/
DROP PROCEDURE [dbo].[AlteraAmigo]
GO

CREATE PROCEDURE [dbo].[AlteraAmigo]
	@Id int,
	@Nome varchar(50),
	@Sobrenome varchar(50),
	@FotografiaId varchar(255),
	@Email varchar(50),
	@Telefone varchar(50),
	@Nascimento date,
	@PaisId int,
	@EstadoId int
AS
	UPDATE Amigo SET
		Nome = @Nome,
		Sobrenome = @Sobrenome,
		FotografiaId = @FotografiaId,
		Email = @Email,
		Telefone = @Telefone,
		Nascimento = @Nascimento,
		PaisId = @PaisId,
		EstadoId = @EstadoId
	WHERE Id = @Id
RETURN 0
GO


/****** Object:  StoredProcedure [dbo].[ExcluiAmigo] ******/
DROP PROCEDURE [dbo].[ExcluiAmigo]
GO

CREATE PROCEDURE [dbo].[ExcluiAmigo]
	@Id int
AS
	DELETE Amigo
	WHERE Id = @Id
RETURN 0
GO

/****** Object:  StoredProcedure [dbo].[SelecionaAmigos] ******/
DROP PROCEDURE [dbo].[SelecionaAmigos]
GO

CREATE PROCEDURE [dbo].[SelecionaAmigos]
AS
	SELECT
		Id,
		Nome,
		Sobrenome,
		FotografiaId,
		Email,
		Telefone,
		Nascimento,
		PaisId,
		EstadoId
	FROM Amigo
RETURN 0
GO


/****** Object:  StoredProcedure [dbo].[SelecionaAmigoId] ******/
DROP PROCEDURE [dbo].[SelecionaAmigoId]
GO

CREATE PROCEDURE [dbo].[SelecionaAmigoId]
@Id int
AS
	SELECT
		Id,
		Nome,
		Sobrenome,
		FotografiaId,
		Email,
		Telefone,
		Nascimento,
		PaisId,
		EstadoId
	FROM Amigo
	WHERE Id = @Id
RETURN 0
GO

