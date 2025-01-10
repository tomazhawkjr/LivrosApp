CREATE TABLE [dbo].[LivroAutor]
(
	[Id] INT NOT NULL IDENTITY,
	[IdAutor] INT,
    [IdLivro] INT,
    [DataCriacao] smalldatetime not null,
    CONSTRAINT PK_LivroAutor PRIMARY KEY(id),
    CONSTRAINT FK_LivroAutor_Livro FOREIGN KEY (IdLivro) REFERENCES Livro(Id),
    CONSTRAINT FK_LivroAutor_Autor FOREIGN KEY (IdAutor) REFERENCES Autor(Id)
)
