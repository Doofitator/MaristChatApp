Imports System.Security.Cryptography
Imports Microsoft.VisualBasic

Public NotInheritable Class Encryption
    'https://docs.microsoft.com/en-us/dotnet/visual-basic/programming-guide/language-features/strings/walkthrough-encrypting-and-decrypting-strings

    ' // This class is taken almost directly from the last chat app, however it is virtually the same as the above tutorial link //
    ' // I'm going to be honest, I don't really know what I'm doing in regards to encryption but it works and I even kinda documented it so... //

    Private TripleDes As New TripleDESCryptoServiceProvider
    Private Function TruncateHash(ByVal key As String, ByVal length As Integer) As Byte()

        Dim sha1 As New SHA1CryptoServiceProvider

        ' Hash the key.
        Dim keyBytes() As Byte = System.Text.Encoding.Default.GetBytes(key)
        Dim hash() As Byte = sha1.ComputeHash(keyBytes)

        ' Truncate or pad the hash.
        ReDim Preserve hash(length - 1)
        Return hash
    End Function
    Sub New(ByVal key As String) 'make new encryption
        TripleDes.Key = TruncateHash(key, TripleDes.KeySize \ 8) '| Set crypto values
        TripleDes.IV = TruncateHash("", TripleDes.BlockSize \ 8) '|
    End Sub
    Public Function EncryptData(ByVal plaintext As String) As String

        ' Get an array of bytes from the input string
        Dim plaintextBytes() As Byte = System.Text.Encoding.Default.GetBytes(plaintext)

        ' New memoryStream
        Dim ms As New System.IO.MemoryStream
        ' New cryptoStream to write to
        Dim encStream As New CryptoStream(ms, TripleDes.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write)

        ' Write the bytearray to the memoryStream using the cryptoStream
        encStream.Write(plaintextBytes, 0, plaintextBytes.Length)
        encStream.FlushFinalBlock()

        ' Convert the encryption back to a string (in base64)
        Return Convert.ToBase64String(ms.ToArray)
    End Function
    Public Function DecryptData(ByVal encryptedtext As String) As String

        ' Get an array of bytes from the input string
        Dim encryptedBytes() As Byte = Convert.FromBase64String(encryptedtext)

        ' New MemoryStream
        Dim ms As New System.IO.MemoryStream
        ' New cryptoStream to write to
        Dim decStream As New CryptoStream(ms, TripleDes.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write)

        ' Write the byte array to the memoryStream using the CryptoStream (ie. decode)
        decStream.Write(encryptedBytes, 0, encryptedBytes.Length)
        decStream.FlushFinalBlock()

        ' Convert the plaintext stream to a string.
        Return System.Text.Encoding.Default.GetString(ms.ToArray)
    End Function
End Class