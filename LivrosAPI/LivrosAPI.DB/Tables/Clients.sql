/*<semRastro>*/
/*<semProcedure>*/
CREATE TABLE [dbo].[Clients]
(
  [Id] [nvarchar](450) NOT NULL
 ,[Name] [nvarchar](100) NOT NULL
 ,[Active] [bit] NOT NULL
 ,[RefreshTokenLifeTime] [int] NOT NULL
 ,[AllowedOrigin] [nvarchar](100) NULL
 ,CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY];