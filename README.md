# SaaS for Shops
## How to Run

### 1. Configure `appsettings.json`

Update the configuration in your API project's `appsettings.json` file.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": ""
  },
}
 ```

### 2. Apply Database Migrations

Run the following commands from the **solution root directory**.

**Add a migration** (only if no migrations exist):

```powershell
dotnet ef migrations add Mig_1 --project .\Infrastructure\Infrastructure.csproj --startup-project .\API\API.csproj
```

**Update the database:**

```powershell
dotnet ef database update --project .\Infrastructure\Infrastructure.csproj --startup-project .\API\API.csproj
```

### 3. Run the API

Run the API project using Visual Studio or the .NET CLI.
