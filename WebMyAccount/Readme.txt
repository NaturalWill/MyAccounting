dotnet ef migrations add Migration --project  WebMyAccount

dotnet ef database update --project  WebMyAccount

Update-Package -ProjectName "WebMyAccount" -Reinstall