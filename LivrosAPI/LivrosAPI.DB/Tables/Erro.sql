CREATE TABLE [dbo].[Erro]
(
  [id] [int] IDENTITY(1, 1) NOT NULL
 ,[aplicacao] [varchar](100) NOT NULL
 ,[dataHora] [datetime] NOT NULL
 ,[stackTrace] [varchar](MAX) NOT NULL
 ,[mensagem] [varchar](MAX) NOT NULL
 ,[informacaoAdicional] [varchar](1000) NULL
 ,CONSTRAINT [PK_Erro] PRIMARY KEY CLUSTERED ([id] ASC) WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
