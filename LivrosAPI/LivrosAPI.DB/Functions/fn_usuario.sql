CREATE function [dbo].[fn_usuario](@idUsuario varchar(100))
returns table 
as return (
 select 
u.Id
,u.Nome
,u.Ativo
,u.DataCriacao
,anu.Email
,anu.PhoneNumber
,Perfil = anr.Name
from Usuario u
inner join AspNetUsers anu on anu.Id = u.IdAspNetUsers
inner join AspNetUserRoles anur on anur.UserId = u.IdAspNetUsers
inner join AspNetRoles anr on anr.Id = anur.RoleId
where u.IdAspNetUsers = @idUsuario
)