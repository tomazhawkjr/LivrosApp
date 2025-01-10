/*<semRastro>*/
/*<semProcedure>*/
CREATE TABLE [dbo].[AspNetUserRoles]
(
  [UserId] [nvarchar](450) NOT NULL
 ,[RoleId] [nvarchar](450) NOT NULL
 ,[Discriminator] [nvarchar](MAX) NOT NULL
 ,[UserId1] [nvarchar](450) NULL
 ,[RoleId1] [nvarchar](450) NULL
 ,CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED (
    [UserId] ASC
   ,[RoleId] ASC)
);
