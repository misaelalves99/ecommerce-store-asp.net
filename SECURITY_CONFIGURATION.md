# Security Configuration

## Secret handling

Reusable credentials must not be committed to this repository.

The tracked `appsettings.json` intentionally keeps `ConnectionStrings:DefaultConnection` empty.

For local development, supply the connection string through ASP.NET Core configuration outside Git.

PowerShell example:

```powershell
$env:ConnectionStrings__DefaultConnection = "<local-connection-string>"
dotnet run
```

The placeholder is documentation only. Never replace it with a real credential in a tracked file.

Local secret-bearing files such as `appsettings.Local.json` and `.env` are ignored by Git.

## Evidence boundary

Configuration files, database providers and migrations do not by themselves establish active database persistence or production readiness.

## Historical exposure

Removing a credential from the current tree does not remove historical Git objects.

Any previously exposed credential that remains valid or reusable must be rotated independently.

This remediation does not rewrite Git history.
