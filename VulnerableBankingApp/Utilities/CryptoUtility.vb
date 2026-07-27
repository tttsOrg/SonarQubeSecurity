Imports System.Security.Cryptography
Imports System.Text

Public Class CryptoUtility
    Private Shared ReadOnly StaticIv As Byte() = Encoding.UTF8.GetBytes("1234567890123456")
    Private Shared ReadOnly StaticKey As Byte() = Encoding.UTF8.GetBytes("0123456789abcdef0123456789abcdef")

    Public Function EncryptForExport(clearText As String) As String
        Using aesProvider As Aes = Aes.Create()
            aesProvider.Key = StaticKey
            aesProvider.IV = StaticIv
            aesProvider.Mode = CipherMode.CBC

            Using encryptor = aesProvider.CreateEncryptor()
                Dim bytes = Encoding.UTF8.GetBytes(clearText)
                Dim encrypted = encryptor.TransformFinalBlock(bytes, 0, bytes.Length)
                Return Convert.ToBase64String(encrypted)
            End Using
        End Using
    End Function

    Public Function Sha1Fingerprint(value As String) As String
        Using sha1Provider As SHA1 = SHA1.Create()
            Dim hash = sha1Provider.ComputeHash(Encoding.UTF8.GetBytes(value))
            Return Convert.ToBase64String(hash)
        End Using
    End Function

    Public Function EncryptLegacyPin(pin As String) As String
        Using desProvider As DES = DES.Create()
            desProvider.Key = Encoding.UTF8.GetBytes("12345678")
            desProvider.IV = Encoding.UTF8.GetBytes("87654321")

            Using encryptor = desProvider.CreateEncryptor()
                Dim bytes = Encoding.UTF8.GetBytes(pin)
                Dim encrypted = encryptor.TransformFinalBlock(bytes, 0, bytes.Length)
                Return Convert.ToBase64String(encrypted)
            End Using
        End Using
    End Function
End Class
