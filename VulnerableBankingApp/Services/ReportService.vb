Imports System.Text

Public Class ReportService
    Public Function BuildAccountSummary(customer As Customer) As String
        Dim builder = New StringBuilder()
        builder.AppendLine("<html>")
        builder.AppendLine("<body>")
        builder.AppendLine("<h1>Account Summary</h1>")
        builder.AppendLine("<p>Name: " & customer.Name & "</p>")
        builder.AppendLine("<p>Email: " & customer.Email & "</p>")
        builder.AppendLine("<p>Balance: " & customer.Balance.ToString("C") & "</p>")
        builder.AppendLine("</body>")
        builder.AppendLine("</html>")
        Return builder.ToString()
    End Function

    Public Function CalculateRiskScore(balance As Decimal, failedLogins As Integer, isPrivileged As Boolean) As Integer
        Dim score = 0

        If balance > 10000D Then
            score += 20
        End If

        If failedLogins > 3 Then
            score += failedLogins * 10
        End If

        If isPrivileged Then
            score += 50
        End If

        Return score
    End Function
End Class
