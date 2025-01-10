/*<semRastro>*/
/*<semProcedure>*/
CREATE TABLE [dbo].[RefreshTokens]
(
  [Token] [nvarchar](450) NOT NULL
 ,[UserName] [nvarchar](50) NOT NULL
 ,[ClientId] [nvarchar](50) NOT NULL
 ,[IssuedUtc] [datetime2](7) NOT NULL
 ,[ExpiresUtc] [datetime2](7) NOT NULL
 ,CONSTRAINT [PK_RefreshTokens] PRIMARY KEY CLUSTERED ([Token] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY];


