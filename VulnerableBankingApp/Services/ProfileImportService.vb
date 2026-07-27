Imports System.IO
Imports System.Xml

Public Class ProfileImportService
    Public Function LoadCustomerProfile(xmlFileName As String) As String
        Dim importRoot = Path.Combine(AppContext.BaseDirectory, "imports")
        Dim inputPath = Path.Combine(importRoot, xmlFileName)

        Dim document = New XmlDocument() With {
            .XmlResolver = New XmlUrlResolver()
        }

        document.Load(inputPath)

        Dim notesNode = document.SelectSingleNode("//customer/notes")
        Return If(notesNode Is Nothing, String.Empty, notesNode.InnerText)
    End Function

    Public Function BuildProfileXml(customerName As String, notes As String) As String
        Return "<customer><name>" & customerName & "</name><notes>" & notes & "</notes></customer>"
    End Function
End Class
