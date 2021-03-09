## Introduction
The Pokedex Service provides information about various Pokemon for Pokeworld, a make-believe company that is built for Pokemon fans.

## Pre-requisites

This project requires .net core 3.1 or higher. You can [download](https://dotnet.microsoft.com/download/dotnet-core) it from the official website.

## Build and Test

Download or clone this project. From the root folder, run the following commands.

### Build

```
cd src\Pokeworld.Pokedex.Api
dotnet build
```

### Run

```
dotnet run
```
### Test

#### Unit Test

```
cd tests\Pokeworld.Pokedex.UnitTests
dotnet test
```
	

#### Service Test

```
cd tests\Pokeworld.Pokedex.ServiceTests
dotnet test
```

#### Integration Test

```
cd tests\Pokeworld.Pokedex.IntegrationTests
dotnet test
```

### Swagger

```
https://localhost:5001/swagger/index.html
```

### Service test report
![featureReport](https://user-images.githubusercontent.com/12287308/110394716-cd09f980-8064-11eb-84dc-2ca3dc0957b1.PNG)

---

### Considerations to deploy this project to production

- Add health checks
- Create enviroments to handle exceptions differently
- Read configuration from Azure config maps instead of app settings
- Use Azure Application Insights for telemetry