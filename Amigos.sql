USE [AZURE_AT]
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Amigos]  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Amigos]') AND type in (N'U'))
DROP TABLE [dbo].[Amigos]
GO

CREATE TABLE [dbo].[Amigos](
	[Id] [int] NOT NULL,
	[AmigoId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[AmigoId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Amigos]  WITH CHECK ADD  CONSTRAINT [fk_amigo_id] FOREIGN KEY([Id])
REFERENCES [dbo].[Amigo] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Amigos] CHECK CONSTRAINT [fk_amigo_id]
GO

/****** Object:  StoredProcedure [dbo].[IncluiAmigoList] ******/
DROP PROCEDURE [dbo].[IncluiAmigoList]
GO

CREATE PROCEDURE [dbo].[IncluiAmigoList]
	@Id int,
	@AmigoId int
AS
	INSERT INTO Amigos (Id, AmigoId)
	VALUES (@Id, @AmigoId)
RETURN 0
GO


/****** Object:  StoredProcedure [dbo].[ExcluiAmigoList] ******/
DROP PROCEDURE [dbo].[ExcluiAmigoList]
GO

CREATE PROCEDURE [dbo].[ExcluiAmigoList]
	@Id int,
	@AmigoId int
AS
	DELETE Amigos
	WHERE Id = @Id AND AmigoId = @AmigoId
RETURN 0
GO


/****** Object:  StoredProcedure [dbo].[SelecionaAmigosAmigo] ******/
DROP PROCEDURE [dbo].[SelecionaAmigosAmigo]
GO

CREATE PROCEDURE [dbo].[SelecionaAmigosAmigo]
	@Id int
AS
	SELECT 
		Amigo.Id,
		Amigo.Nome,
		Amigo.Sobrenome,
		Amigo.FotografiaId,
		Amigo.Email,
		Amigo.Telefone,
		Amigo.Nascimento,
		Amigo.PaisId,
		Amigo.EstadoId
	FROM Amigo
	INNER JOIN Amigos ON Amigo.Id = Amigos.AmigoId
	WHERE Amigos.Id = @Id
RETURN 0
GO

