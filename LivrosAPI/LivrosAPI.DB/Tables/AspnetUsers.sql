﻿/*<semRastro>*/
/*<semProcedure>*/
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL
	,[UserName] [nvarchar](256) NULL
	,[NormalizedUserName] [nvarchar](256) NULL
	,[Email] [nvarchar](256) NULL
	,[NormalizedEmail] [nvarchar](256) NULL
	,[EmailConfirmed] [bit] NOT NULL
	,[PasswordHash] [nvarchar](max) NULL
	,[SecurityStamp] [nvarchar](max) NULL
	,[ConcurrencyStamp] [nvarchar](max) NULL
	,[PhoneNumber] [nvarchar](max) NULL
	,[PhoneNumberConfirmed] [bit] NOT NULL
	,[TwoFactorEnabled] [bit] NOT NULL
	,[LockoutEnd] [datetimeoffset](7) NULL
	,[LockoutEnabled] [bit] NOT NULL
	,[AccessFailedCount] [int] NOT NULL
	,CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED ( [Id] ASC) 
) 