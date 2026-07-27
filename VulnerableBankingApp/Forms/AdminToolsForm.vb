Imports System.Diagnostics
Imports System.Windows.Forms

Public Class AdminToolsForm
    Inherits Form

    Private ReadOnly _commandTextBox As TextBox
    Private ReadOnly _runButton As Button
    Private ReadOnly _outputTextBox As TextBox

    Public Sub New()
        Text = "Admin Tools"
        Width = 640
        Height = 420

        _commandTextBox = New TextBox() With {
            .Left = 20,
            .Top = 20,
            .Width = 480,
            .Text = "ipconfig"
        }

        _runButton = New Button() With {
            .Left = 515,
            .Top = 18,
            .Width = 80,
            .Text = "Run"
        }

        _outputTextBox = New TextBox() With {
            .Left = 20,
            .Top = 60,
            .Width = 575,
            .Height = 290,
            .Multiline = True,
            .ScrollBars = ScrollBars.Vertical
        }

        AddHandler _runButton.Click, AddressOf RunButton_Click

        Controls.Add(_commandTextBox)
        Controls.Add(_runButton)
        Controls.Add(_outputTextBox)
    End Sub

    Private Sub RunButton_Click(sender As Object, e As EventArgs)
        Dim startInfo = New ProcessStartInfo("cmd.exe", "/c " & _commandTextBox.Text) With {
            .UseShellExecute = False,
            .RedirectStandardOutput = True
        }

        Using childProcess As Process = Process.Start(startInfo)
            _outputTextBox.Text = childProcess.StandardOutput.ReadToEnd()
        End Using
    End Sub
End Class
