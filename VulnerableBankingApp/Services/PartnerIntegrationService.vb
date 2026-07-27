Imports System.IO
Imports System.Net.Http

Public Class PartnerIntegrationService
    Private Const PartnerApiKey As String = "partner-api-key-12345"

    Public Function BuildPartnerAccountUrl(partnerHost As String, accountId As String) As String
        Return "http://" & partnerHost & "/api/accounts/" & accountId & "?apiKey=" & PartnerApiKey
    End Function

    Public Function DownloadPartnerAccount(partnerHost As String, accountId As String) As String
        Using client = New HttpClient()
            Dim url = BuildPartnerAccountUrl(partnerHost, accountId)
            Return client.GetStringAsync(url).Result
        End Using
    End Function

    Public Sub SavePartnerPayload(fileName As String, payload As String)
        Dim partnerFolder = Path.Combine(AppContext.BaseDirectory, "partner-payloads")
        Directory.CreateDirectory(partnerFolder)

        Dim outputPath = Path.Combine(partnerFolder, fileName)
        File.WriteAllText(outputPath, payload)
    End Sub

    Public Function BuildCustomerCsvRow(customerName As String, email As String, notes As String) As String
        Return customerName & "," & email & "," & notes
    End Function

    Public Function BuildPartnerError(exception As Exception) As String
        Return "Partner sync failed: " & exception.ToString()
    End Function
End Class
