CREATE TABLE [dbo].[Autor]
(
	[Id] INT NOT NULL IDENTITY,
	[Nome] VARCHAR(40),
	[DataCriacao] smalldatetime not null,
	CONSTRAINT PK_Autor PRIMARY KEY(Id)
)
