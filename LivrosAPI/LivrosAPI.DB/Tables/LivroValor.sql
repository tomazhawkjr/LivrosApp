CREATE TABLE [dbo].[LivroValor]
(
	[Id] INT NOT NULL IDENTITY,
	[IdLivro] INT NOT NULL,    
	[IdFormaCompra] INT NOT NULL, 
	[Valor] DECIMAL(10,2) NOT NULL,
	[DataCriacao] smalldatetime not null,
	CONSTRAINT PK_LivroValor PRIMARY KEY(id),
	CONSTRAINT FK_LivroValor_Livro FOREIGN KEY (IdLivro) REFERENCES Livro(Id),
	CONSTRAINT FK_LivroValor_FormatoCompra FOREIGN KEY (IdFormaCompra) REFERENCES FormaCompra(Id)
)
