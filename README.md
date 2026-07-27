# SonarQube Security Lab

This is a small VB.NET console solution created to test SonarQube Cloud analysis.
It intentionally contains insecure patterns so the scanner can report security
hotspots and warnings.

## Intentional Issues

- Hard-coded database credentials in `Program.vb`
- Disabled SQL encryption in the connection string
- SQL query built with string concatenation
- File path built from untrusted input
- Shell command execution from untrusted input
- MD5 hashing for token generation
- Hard-coded signing key
- Windows Forms login/admin screens that pass UI input into unsafe operations
- Audit logging that writes passwords to disk
- AES encryption with static key and IV
- SHA1 fingerprinting
- HTML report generation from unsanitized customer data
- XML parsing with an external resolver enabled
- XML generation through raw string concatenation
- Transfer SQL built from account input
- Predictable transfer references generated with `Random`
- Hard-coded backup token and clear-text HTTP endpoint
- Certificate validation callback that accepts every certificate
- SMTP credentials hard-coded in notification service
- Email reset HTML built from untrusted values
- Session cookie without secure attributes
- Login redirect URL built from untrusted input

Do not use this code as an application template. It is deliberately vulnerable.

## Build

```powershell
dotnet build .\SonarQubeSecurityLab.sln --config Release
```

## SonarCloud Scan Example

Replace the placeholders with your organization, project key, and token.

```powershell
dotnet tool install --global dotnet-sonarscanner

dotnet sonarscanner begin `
  /k:"your-project-key" `
  /o:"your-organization" `
  /d:sonar.host.url="https://sonarcloud.io" `
  /d:sonar.token="%SONAR_TOKEN%"

dotnet build .\SonarQubeSecurityLab.sln --config Release

dotnet sonarscanner end /d:sonar.token="%SONAR_TOKEN%"
```
