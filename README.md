# Abbas_Behjatnia.CTC

## About this solution

This is a layered startup solution based on [Domain Driven Design (DDD)] practises.

### Pre-requirements

* [.NET 8.0+ SDK](https://dotnet.microsoft.com/download/dotnet)

### Configurations

The solution comes with a default configuration that works out of the box. However, you may consider to change the following configuration before running your solution:

* Check the `ConnectionStrings` in `appsettings.json` files under the `Abbas_Behjatnia.CTC.HttpApi.Host` and `Abbas_Behjatnia.CTC.DbMigrator` projects and change it if you need.

### Before running the application

#### Create the Database

Run `Abbas_Behjatnia.CTC.DbMigrator` to create the initial database. This should be done in the first run. It is also needed if a new database migration is added to the solution later.

### Solution structure

This is a layered monolith application that consists of the following applications:

* `Abbas_Behjatnia.CTC.DbMigrator`: A console application which applies the migrations and also seeds the initial data. It is useful on development as well as on production environment.
* `Abbas_Behjatnia.CTC.HttpApi.Host`: ASP.NET Core API application that is used to expose the APIs to the clients.
