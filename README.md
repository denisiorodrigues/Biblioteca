# Biblioteca
Projeto educacional para aplicar as boas práticas e implementações aprendias no curso de "*REST com ASP.NET Core WebAPI*" da trilha de Dotnet Core da plataforma https://desenvolvedor.io/

# Frameworks utilizados
* EntityFrameworkCore
    - Sqlserver
    - Sqlite
* FluentValidation
* Automapper


# Comandos Dotnet CLI
## Gerando Código Automático
**Gerando Scafold Controller com o dotnet code generation**
```
dotnet aspnet-codegenerator controller -name TodoItemsController -async -api -m TodoItem -dc TodoContext -outDir Controllers
```

> Nota: `Tem que adicionar a referência abaixo`
>
>```sh
>dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
>```

# EntityFramework CLI
**Atualizar a base de dados com as migrations**
```
dotnet-ef database update -v
```

# Dependencias
Microsoft.EntityFrameworkCore.Sqlite
```
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 5.0.9
```

Pacote para gerenciamento do certificado HTTPS

Referência: [docs.microsoft.com](https://docs.microsoft.com/pt-br/aspnet/core/security/authentication/certauth?view=aspnetcore-3.1)

```
dotnet add package Microsoft.AspNetCore.Authentication.Certificate --version 3.1.18
```