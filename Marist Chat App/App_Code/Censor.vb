Imports Microsoft.VisualBasic
Public Class Censor
    Public Shared CensoredWords As Generic.IList(Of String)
    Public Sub New()
        If CensoredWords Is Nothing Then Throw New ArgumentNullException("censoredWords")
        CensoredWords = New Generic.List(Of String)(CensoredWords)
    End Sub

    Public Function CensorText(ByVal text As String) As String
        If text Is Nothing Then Throw New ArgumentNullException("text")
        Dim censoredText As String = text

        For Each censoredWord As String In CensoredWords
            Dim regularExpression As String = ToRegexPattern(censoredWord)
            censoredText = Regex.Replace(censoredText, regularExpression, AddressOf StarCensoredMatch, RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant)
        Next

        Return censoredText
    End Function

    Private Shared Function StarCensoredMatch(ByVal m As Match) As String
        Dim word As String = m.Captures(0).Value
        Return New String("*"c, word.Length)
    End Function

    Private Function ToRegexPattern(ByVal wildcardSearch As String) As String
        Dim regexPattern As String = Regex.Escape(wildcardSearch)
        regexPattern = regexPattern.Replace("\*", ".*?")
        regexPattern = regexPattern.Replace("\?", ".")

        If regexPattern.StartsWith(".*?") Then
            regexPattern = regexPattern.Substring(3)
            regexPattern = "(^\b)*?" & regexPattern
        End If

        regexPattern = "\b" & regexPattern & "\b"
        Return regexPattern
    End Function
End Class

