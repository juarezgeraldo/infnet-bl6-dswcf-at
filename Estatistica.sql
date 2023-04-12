USE [AZURE_AT]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[QtdPaises] ******/
DROP PROCEDURE [dbo].[QtdPaises]
GO

CREATE PROCEDURE [dbo].[QtdPaises]
AS
	SELECT 
		COUNT(*) as QtdPaises
	FROM Pais 
RETURN 0
GO

/****** Object:  StoredProcedure [dbo].[QtdEstados] ******/
DROP PROCEDURE [dbo].[QtdEstados]
GO

CREATE PROCEDURE [dbo].[QtdEstados]
AS
	SELECT 
		COUNT(*) as QtdEstados
	FROM Estado 
RETURN 0
GO

/****** Object:  StoredProcedure [dbo].[QtdAmigos] ******/
DROP PROCEDURE [dbo].[QtdAmigos]
GO

CREATE PROCEDURE [dbo].[QtdAmigos]
AS
	SELECT 
		COUNT(*) as QtdAmigos
	FROM Amigo 
RETURN 0
GO
