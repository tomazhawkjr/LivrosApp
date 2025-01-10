CREATE TABLE [dbo].[FormaCompra]
(
	[Id] INT NOT NULL IDENTITY,
	[Denominacao] VARCHAR(50) NOT NULL,    
	[DataCriacao] smalldatetime not null,
	CONSTRAINT PK_FormaCompra PRIMARY KEY(Id)
)
