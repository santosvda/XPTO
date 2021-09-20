# XPTO
## Felipe Dos Santos

XPTO é um projeto de estudo com o objetivo de simular o gerenciamento de produtos.

## Tecnologias usadas
- SQL Server
- C#
- EF Core
- Vue.js
- Quasar

## Endpoints
- API: http://localhost:5000/api
- Swagger: http://localhost:5000/swagger
- Vue-App: http://localhost:8080/

## Necessário
Tenha certeza que você possui instalado:
- SQL Server
- .NET Core 2.2.207
https://dotnet.microsoft.com/download/dotnet/thank-you/sdk-2.2.207-windows-x64-installer
- Node.js
https://nodejs.org/en/

Uma das premissas do projeto é apenas salvar o produto no bancos de dados após adicionar com sucesso ao DataLake 'Fake Store', então tenha certeza que você possui acesso a internet 
- Mais sobre [FakeStoreAPI]

## Executando a API
**Confira se está correto a string de conexão em XPTO.API/appsettings.json e XPTO.API/appsettings.Development.json**
- OBS: Banco de dados será criado automaticamente após rodar o projeto pela primeira vez

Em /XPTO.API/ execute:
```sh
dotnet run
```
## Executando o APP
Em /XPTO-APP execute:
```sh
npm install
```
Logo após:
```sh
npx quasar dev
```
OU instale o quasar CLI:
```sh
npm install -g @quasar/cli
```
E então execute o projeto
```sh
quasar dev
```

**Have fun!**

   [fakestoreapi]: <https://fakestoreapi.com/docs>
