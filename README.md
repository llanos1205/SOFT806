# you can ignore all the other steps if you have docker installed and run:
1. run compose, this should take a while as it generates the database and the webapp and the migrations and seed data
    ```
    docker-compose up -d
    ```
2. verify the application in the browser
    ```
    http://localhost:3000
    ```
# To run the project locally:

1. Restore the solution with

```
dotnet restore
```

3. Create a database called `SOFT806` in sqlserver
4. Update the connection string in

```
./SOFT806.WebApp/appsettings.Local.json
```

5. Update that database by running from the root of the project (this will create the tables):

``` 
dotnet ef database update --project SOFT806.Infrastructure --startup-project SOFT806.WebApp
``` 

Alternatively you can run the base_migration.sql file in the SQLScripts folder in root of the project to create the database and the tables using your database explorer of choice
but you'll need to create the database manually before running the script

``` 
SQLScripts/base_migration.sql
SQLScripts/seed_migration.sql
``` 

6. Run the project
```
dotnet run --project SOFT806.WebApp

Or you can just use the IDE to run it but use the SOFT806.WebApp project as the startup project
```

7. Login with the following credentials:

```
    username: admin@gmail.com 
    The password is: P@ssw0rd
```
