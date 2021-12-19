# ASP.NET Core 3.1
## Technologies
- ASP.NET Core 3.1
- Entity Framework Core 3.1
- MySql
## Install package
### Note: MySql.Data.EntityFrameworkCore deprecated so use Pomelo.EntityFrameworkCore.MySql
- Microsoft.AspNetCore.Identity.EntityFrameworkCore 5.0.0
- MySql.Data
- Pomelo.EntityFrameworkCore.MySql 5.0.0
- Microsoft.EntityFrameworkCore.Tools 5.0.0
- Microsoft.EntityFrameworkCore 5.0.5 (minimum 5.0.5 to use Pomelo.EntityFrameworkCore.MySql)
- Microsoft.Extensions.Configuration 5.0.0
- Microsoft.Extensions.Configuration.Json 5.0.0
- FluentValidation.AspNetCore
- Jwt-authencation: https://jasonwatmore.com/post/2019/10/11/aspnet-core-3-jwt-authentication-tutorial-with-example-api
## Command

## Git
### Command
#### First
- git init --- create repo local
- git commit -m "Your commit" --- first commit
- git branch -M main --- move main brach
- git remote add origin https://github.com/tacong56/ANUIShop.git --- add repo local to git origin
- git push -u origin main --- push code to main
### Create branch
- git checkout -b $branch_name --- Switched to a new branch $branch_name
- git commit -am "create branch feature" --- commit
- git push origin $branch_name --- Push your branch to git
### Other
- git checkout: you can see file change
- git branch: you can see branch current
- git checkout $branch_name: change branch current by $branch_name