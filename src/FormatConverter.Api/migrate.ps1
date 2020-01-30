if(Test-Path ("FormatConverter.db"))
{
	Remove-Item -path FormatConverter.db
}

Remove-Item ..\FormatConverter.DataAccess.Migrations\Migrations\* -include *.cs
dotnet ef migrations add UpdatedModels -c DataContext --project ../FormatConverter.DataAccess.Migrations
dotnet ef database update