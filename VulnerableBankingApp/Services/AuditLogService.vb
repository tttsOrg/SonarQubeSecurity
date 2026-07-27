Imports System.IO

Public Class AuditLogService
    Private Const AuditFolder As String = "C:\Temp\BankAudit"

    Public Sub WriteLoginFailure(email As String, password As String)
        Directory.CreateDirectory(AuditFolder)

        Dim logLine = $"{DateTime.UtcNow:o} LOGIN_FAILED email={email} password={password}"
        File.AppendAllText(Path.Combine(AuditFolder, "security.log"), logLine & Environment.NewLine)
    End Sub

    Public Function ReadAuditFile(fileName As String) As String
        Dim auditPath = Path.Combine(AuditFolder, fileName)
        Return File.ReadAllText(auditPath)
    End Function
End Class
