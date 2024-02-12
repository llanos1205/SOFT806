# To run the project locally:

1. Restore the solution with

```
dotnet restore
```

3. Create a database called `SOFT703A2` in sqlserver
4. Update the connection string in

```
./SOFT703A2.WebApp/appsettings.json
```

5. Update that database by running from the root of the project (this will create the tables):

``` 
dotnet ef database update --project SOFT703A2.Infrastructure --startup-project SOFT703A2.WebApp
``` 

6. Run the project, this will seed the database with some data (admin user, products,categories, etc)

```
dotnet run --project SOFT703A2.WebApp

Or you can just use the IDE to run it but use the SOFT703A2.WebApp project as the startup project
```

7. Login with the following credentials:

```
    username: admin@gmail.com 
    The password is: P@ssw0rd
```

# To run the project in IIS:

1. Restore the solution with

```
dotnet restore
```

2. Generate an artifact by:

```
dotnet publish --configuration Release -o ./publish

this will generate a folder called publish in the root of the project
```

3. Prepare connection string in

```
./SOFT703A2.WebApp/appsettings.json
```

By this i mean that it has to change from using windows authentication to sql server authentication

```
from this:

Server=localhost;Database=SOFT703A2;Trusted_Connection=True;TrustServerCertificate=True;


to this:

Server=localhost;Database=SOFT703A2;User Id=youruser;Password=yourpassword;TrustServerCertificate=True;
```

4. your databse server must be configured to allow sql server authentication (by default it only allows windows authentication)
5. the user created in the database must have the db_owner role or the db_datareader and db_datawriter roles
6. Create a new website in IIS and fill the website folder with the contents of the publish folder
7. Restart the website

# To create new features:

1. Create a new branch from main with the pattern `feature/<feature-name>`
2. Create a pull request to merge back into main
3. Once approved, merge into main
4. Delete the branch
5. DO NOT PUSH TO MAIN DIRECTLY
