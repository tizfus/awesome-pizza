# Awesome Pizza

Come pizzeria "Awesome Pizza" voglio creare il mio nuovo portale per gestire gli ordini dei miei clienti. Il portale non richiede la registrazione dell'utente per poter ordinare le sue pizze. Il pizzaiolo vede la coda degli ordini e li può prendere in carico uno alla volta. Quando la pizza è pronta, il pizzaiolo passa all'ordine successivo. L'utente riceve il suo codice d'ordine e può seguire lo stato dell'ordine fino all'evasione.

Come team, procediamo allo sviluppo per iterazioni. Decidiamo che nella prima iterazione non sarà disponibile un'interfaccia grafica, ma verranno create delle API al fine di ordinare le pizze e aggiornarne lo stato. 

Decidiamo di utilizzare dotnet framework (versione a tua scelta) e di progettare anche i test di unità sul codice oggetto di sviluppo. 

## Development Configuration

### Run locally
You can use the docker-compose configuration which runs the application with the staging profile and exposes it on **port 80**.
```sh
docker compose up
```

### VisualStudio Code

#### Devcontainer
The project comes with a DevContainer with dotnet-sdk 8.0. You must have a `.ssh` folder on your home.

#### Code Tasks

##### Create migration
This command will create a new migration file inside the project `AwesomePizza.Persistence`. Entity Framework uses the configuration for the DbContext specified in  `startup-project`.
```sh
dotnet-ef migrations add <MIGRATION_NAME> --project AwesomePizza.Persistence --startup-project AwesomePizza.API
```

##### Run tests
Runs all tests, print the name of those that fail.
```sh
dotnet test -v q 1> /dev/null 
```

##### Run API
```sh
dotnet run --project AwesomePizza.API
```