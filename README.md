# XPTO
## Felipe Dos Santos

XPTO is a study project with the objective of simulating the management of a product registry.

## Technologies used
- SQL Server
- C#
- EF Core
- Vue.js
- Quasar

## Endpoints
- API: http://localhost:5000/api
- Swagger: http://localhost:5000/swagger
- Vue-App: http://localhost:8080/

## Preparing for build
Make sure you have instaled
- SQL Server
- .NET Core 2.2.207
https://dotnet.microsoft.com/download/dotnet/thank-you/sdk-2.2.207-windows-x64-installer
- Node.js
https://nodejs.org/en/

One of the premises is to only create the product after successfully adding it to the Data Lake 'Fake Store', so make sure you have an internet connection.
- More about [FakeStoreAPI]

## Running API
**Make sure that the connection string at XPTO.API/appsettings.json and XPTO.API/appsettings.Development.json is correct**
- OBS: Database will be created automatically after running the project for the first time..

At /XPTO.API/ run:
```sh
dotnet run
```
## Running APP
At /XPTO-APP run:
```sh
npx quasar dev
```
OR Alternatively you can install quasar CLI:
```sh
npm install -g @quasar/cli
```
Then run the project:
```sh
quasar dev
```

**Have fun!**

   [fakestoreapi]: <https://fakestoreapi.com/docs>