# MeshaTasksBackend

> Essa é a API para administrar todo o front-end de task e sua autenticação

## Comandos para Desenvolver

### Banco de Dados

`docker-compose -f ./Backend/Ci/Develop/docker-compose.yml up`

### Aplicação

`dotnet watch --project ./Backend`

### Testes Unitários

`dotnet test ./UnitTests/UnitTests.csproj `

### Migrações

`dotnet ef database update --project ./Backend`
