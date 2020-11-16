# IdentityServer4 and ASPNET Core Identity implemantation.
This project focuses using IdentityServer4 as central Identity Provider (idP) with ASPNET Core Identity.

# How to configure IS4
This project based on the tutorial at https://deblokt.com/2020/01/24/01-identityserver4-quickstart-net-core-3-1/

Since the tutorial is written for Visual Studio on Windows, some of the scripts needs to be updated for MacOS/Linux

```
// Entity Tools.
# dotnet tool install -g dotnet-ef
 
// You may need to add Microsoft.EntityFrameworkCore.Tools package via Nuget before going forward
// Execute these two commands to create migrations for Operation store and Configuration store

# dotnet-ef migrations add InitialPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb -v

# dotnet-ef migrations add InitialConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb -v

// Execute these two commands to migrate to PostgreSQL DB.
# dotnet-ef database update --context PersistedGrantDbContext -v
# dotnet-ef database update --context ConfigurationDbContext -v
```

After Scaffolding ASPNEt Core Identity (which is already done in this repo), create DB migrations for it

```
// Create migrations
# dotnet-ef migrations add InitialIdentityDbMigration -c IdentityServerContext -o Data/Migrations/AspNetIdentity/AspNetIdentityDb -v

// Migrate to the DB
# dotnet-ef database update --context IdentityServerContext -v
```

Define your Clients
https://identityserver4.readthedocs.io/en/latest/quickstarts/6_aspnet_identity.html#config-cs

