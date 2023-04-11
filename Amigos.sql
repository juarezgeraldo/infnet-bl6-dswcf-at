
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Amigos]  ******/
ALTER TABLE [dbo].[Amigos] DROP CONSTRAINT [fk_amigo_id]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Amigos]') AND type in (N'U'))
DROP TABLE [dbo].[Amigos]
GO

CREATE TABLE [dbo].[Amigos](
	[AmigoId] [int] NOT NULL,
	[MeuAmigoId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AmigoId] ASC,
	[MeuAmigoId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Amigos]  WITH CHECK ADD  CONSTRAINT [fk_amigo_id] FOREIGN KEY([AmigoId])
REFERENCES [dbo].[Amigo] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Amigos] CHECK CONSTRAINT [fk_amigo_id]
GO

/****** Object:  StoredProcedure [dbo].[IncluiMeusAmigos] ******/
DROP PROCEDURE [dbo].[IncluiMeusAmigos]
GO

CREATE PROCEDURE [dbo].[IncluiMeusAmigos]
	@Id int,
	@NewAmigoId int
AS
	INSERT INTO Amigos (AmigoId, MeuAmigoId)
	VALUES (@Id, @NewAmigoId)
RETURN 0
GO


/****** Object:  StoredProcedure [dbo].[ExcluiMeusAmigos] ******/
DROP PROCEDURE [dbo].[ExcluiMeusAmigos]
GO

CREATE PROCEDURE [dbo].[ExcluiMeusAmigos]
	@Id int,
	@OldAmigoId int
AS
	DELETE Amigos
	WHERE AmigoId = @Id AND MeuAmigoId = @OldAmigoId
RETURN 0
GO


/****** Object:  StoredProcedure [dbo].[SelecionaMeusAmigos] ******/
DROP PROCEDURE [dbo].[SelecionaMeusAmigos]
GO

CREATE PROCEDURE [dbo].[SelecionaMeusAmigos]
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
	INNER JOIN Amigos ON Amigo.Id = Amigos.MeuAmigoId
	WHERE AmigoId = @Id
RETURN 0
GO

