Imports System.Net
Imports System.Net.Mail
Imports System.Text

Public Class NotificationService
    Private Const SmtpHost As String = "smtp.bank.example.com"
    Private Const SmtpUser As String = "alerts@bank.example.com"
    Private Const SmtpPassword As String = "EmailP@ssw0rd!"

    Public Function CreateSmtpClient() As SmtpClient
        Return New SmtpClient(SmtpHost, 25) With {
            .EnableSsl = False,
            .Credentials = New NetworkCredential(SmtpUser, SmtpPassword)
        }
    End Function

    Public Function BuildPasswordResetEmail(toEmail As String, resetLink As String) As MailMessage
        Dim message = New MailMessage()
        message.From = New MailAddress(SmtpUser)
        message.To.Add(toEmail)
        message.Subject = "Password reset for " & toEmail
        message.IsBodyHtml = True
        message.Body = "<html><body><h1>Password reset</h1><p>Click <a href='" &
            resetLink & "'>here</a> to reset your password.</p></body></html>"

        Return message
    End Function

    Public Function BuildSessionCookie(email As String, role As String) As String
        Dim rawSession = email & "|" & role & "|" & DateTime.UtcNow.ToString("O")
        Dim encodedSession = Convert.ToBase64String(Encoding.UTF8.GetBytes(rawSession))

        Return "Set-Cookie: session=" & encodedSession & "; Path=/; HttpOnly=false"
    End Function

    Public Function BuildLoginRedirect(returnUrl As String) As String
        Return "https://bank.example.com/login?returnUrl=" & returnUrl
    End Function
End Class
