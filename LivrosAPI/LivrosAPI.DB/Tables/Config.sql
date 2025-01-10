CREATE TABLE dbo.config
(
  Id int IDENTITY(1, 1) NOT NULL
 ,Propriedade varchar(200) NOT NULL
 ,Valor varchar(MAX) NULL
 ,Descricao varchar(MAX) NULL
 ,Categoria varchar(100) NULL
 ,CONSTRAINT PK_config PRIMARY KEY (Propriedade)
);