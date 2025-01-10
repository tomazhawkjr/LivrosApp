CREATE TABLE [dbo].[Assunto]
(
	[Id] INT NOT NULL IDENTITY,
	[Descricao] VARCHAR(20),
	[DataCriacao] smalldatetime not null,
	CONSTRAINT PK_Assunto PRIMARY KEY(Id)
)
