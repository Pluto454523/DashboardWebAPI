# Dashboard Web Api


### How to migrate database
```bash
dotnet tool install --global dotnet-ef
dotnet ef migrations add "Initial" --project ./Infra --startup-project ./API --output-dir ./Database/Migrations
dotnet ef database update --project ./Infra --startup-project ./API
```