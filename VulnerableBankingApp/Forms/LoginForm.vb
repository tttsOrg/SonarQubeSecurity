Imports System.Windows.Forms

Public Class LoginForm
    Inherits Form

    Private ReadOnly _emailTextBox As TextBox
    Private ReadOnly _passwordTextBox As TextBox
    Private ReadOnly _loginButton As Button
    Private ReadOnly _repository As CustomerRepository
    Private ReadOnly _auditLogService As AuditLogService

    Public Sub New()
        Text = "Bank Login"
        Width = 420
        Height = 180

        _repository = New CustomerRepository("Server=localhost;Database=Banking;User ID=sa;Password=local-admin;Encrypt=False;")
        _auditLogService = New AuditLogService()

        _emailTextBox = New TextBox() With {
            .Left = 20,
            .Top = 20,
            .Width = 350,
            .Text = "alice@example.com"
        }

        _passwordTextBox = New TextBox() With {
            .Left = 20,
            .Top = 55,
            .Width = 350,
            .UseSystemPasswordChar = False,
            .Text = "password"
        }

        _loginButton = New Button() With {
            .Left = 20,
            .Top = 95,
            .Width = 120,
            .Text = "Login"
        }

        AddHandler _loginButton.Click, AddressOf LoginButton_Click

        Controls.Add(_emailTextBox)
        Controls.Add(_passwordTextBox)
        Controls.Add(_loginButton)
    End Sub

    Private Sub LoginButton_Click(sender As Object, e As EventArgs)
        _repository.SearchCustomers(_emailTextBox.Text)
        _auditLogService.WriteLoginFailure(_emailTextBox.Text, _passwordTextBox.Text)
        MessageBox.Show("Login failed")
    End Sub
End Class
