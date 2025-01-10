CREATE TABLE [dbo].[LivroAssunto]
(
	[Id] INT NOT NULL IDENTITY,
	[IdAssunto] INT,
    [IdLivro] INT,
	[DataCriacao] smalldatetime not null,
	CONSTRAINT PK_LivroAssunto PRIMARY KEY(Id),
    CONSTRAINT FK_LivroAssunto_Livro FOREIGN KEY (IdLivro) REFERENCES Livro(Id),
    CONSTRAINT FK_LivroAssunto_Assunto FOREIGN KEY (IdAssunto) REFERENCES Assunto(Id)
)
