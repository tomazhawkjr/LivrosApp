CREATE TABLE [dbo].[Livro]
(
	[Id] INT NOT NULL IDENTITY,
	[Titulo] VARCHAR(40),
    [Editora] VARCHAR(40),
    [Edicao] INT,
    [AnoPublicacao] VARCHAR(4),
	[DataCriacao] smalldatetime not null,
	CONSTRAINT PK_Livro PRIMARY KEY(Id)
)
