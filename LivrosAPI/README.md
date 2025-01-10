# Introduction 
Utilizando o template padrão do T4i base .NET Core API 8.0.


# Migrations - Identity
Para criar os objetos relacionados á autenticação e autorização, execute os seguintes comandos no Package Manager Console:

Selecione primeiramente o projeto Identity. Ex.: ContratoSupridorAPI.Identity

```bash
EntityFrameworkCore\Add-Migration InitialCreate -Context ApplicationDbContext
EntityFrameworkCore\Update-Database -Context ApplicationDbContext
```

