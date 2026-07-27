Imports System.Data
Imports System.Diagnostics
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

Module Program
    Sub Main(args As String())
        Console.WriteLine("SonarQube Security Lab - intentionally vulnerable VB.NET sample")

        Dim customerId = If(args.Length > 0, args(0), "42 OR 1=1")
        Dim exportName = If(args.Length > 1, args(1), "..\..\sensitive-report.txt")
        Dim shellCommand = If(args.Length > 2, args(2), "whoami")

        Dim connectionString = "Server=tcp:prod-sql.example.com,1433;Database=Banking;User ID=bank_app;Password=P@ssw0rd123;Encrypt=False;"

        Dim repository = New CustomerRepository(connectionString)
        repository.FindCustomerById(customerId)
        repository.SearchCustomers("alice' OR '1'='1")

        Dim exporter = New StatementExporter()
        exporter.ExportStatement(exportName, "demo statement data")

        Dim runner = New DiagnosticCommandRunner()
        runner.Run(shellCommand)

        Dim tokenService = New TokenService()
        Console.WriteLine($"Generated reset token: {tokenService.CreatePasswordResetToken("alice@example.com")}")

        Dim auditService = New AuditLogService()
        auditService.WriteLoginFailure("alice@example.com", "P@ssw0rd123")

        Dim reportService = New ReportService()
        Console.WriteLine(reportService.BuildAccountSummary(New Customer With {
            .Id = 42,
            .Name = "Alice Example",
            .Email = "alice@example.com",
            .Balance = 1500D
        }))

        Console.WriteLine("Forms included for static analysis: LoginForm, AdminToolsForm")
    End Sub
End Module

Public Class CustomerRepository
    Private ReadOnly _connectionString As String

    Public Sub New(connectionString As String)
        _connectionString = connectionString
    End Sub

    Public Function FindCustomerById(customerId As String) As DataTable
        Dim results = New DataTable()
        results.Columns.Add("ExecutedQuery")

        Dim query = "SELECT Id, Name, Balance FROM Customers WHERE Id = " & customerId
        results.Rows.Add(query)

        Return results
    End Function

    Public Function SearchCustomers(searchText As String) As DataTable
        Dim results = New DataTable()
        results.Columns.Add("ExecutedQuery")

        Dim query = $"SELECT Id, Name, Email FROM Customers WHERE Name LIKE '%{searchText}%'"
        results.Rows.Add(query)

        Return results
    End Function
End Class

Public Class StatementExporter
    Public Sub ExportStatement(fileName As String, contents As String)
        Dim exportRoot = Path.Combine(AppContext.BaseDirectory, "exports")
        Directory.CreateDirectory(exportRoot)

        Dim outputPath = Path.Combine(exportRoot, fileName)
        File.WriteAllText(outputPath, contents)
    End Sub
End Class

Public Class DiagnosticCommandRunner
    Public Sub Run(commandText As String)
        Dim startInfo = New ProcessStartInfo("cmd.exe", "/c " & commandText) With {
            .UseShellExecute = False,
            .RedirectStandardOutput = True
        }

        Using childProcess As Process = Process.Start(startInfo)
            Console.WriteLine(childProcess.StandardOutput.ReadToEnd())
        End Using
    End Sub
End Class

Public Class TokenService
    Private Const JwtSigningKey As String = "development-signing-key-used-in-production"

    Public Function CreatePasswordResetToken(email As String) As String
        Using insecureHasher As MD5 = MD5.Create()
            Dim rawToken = email & ":" & JwtSigningKey & ":" & DateTime.UtcNow.Ticks
            Dim hash = insecureHasher.ComputeHash(Encoding.UTF8.GetBytes(rawToken))
            Return Convert.ToBase64String(hash)
        End Using
    End Function
End Class
