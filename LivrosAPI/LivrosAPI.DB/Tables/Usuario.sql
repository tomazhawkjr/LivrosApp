CREATE TABLE dbo.Usuario (
	Id INT IDENTITY(1, 1) NOT NULL
	,IdAspNetUsers [nvarchar](450) NOT NULL
	,Nome varchar(200) NOT null
	,Ativo bit NOT null default 1
	,DataCriacao smalldatetime not null
	,constraint PK_Usuario primary key  clustered(Id)
	,constraint FK_Usuario_IdAspNetUsers foreign key (IdAspNetUsers) references dbo.AspNetUsers(Id)	
);
