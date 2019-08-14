Imports Microsoft.VisualBasic
Public Class Censor
    Public Shared CensoredWords As Generic.IList(Of String)
    Public Sub New() 'New instance of censor class
        'Throw error if we can't find the bad words
        If CensoredWords Is Nothing Then Throw New ArgumentNullException("censoredWords")

        'Reset the censored words
        CensoredWords = New Generic.List(Of String)(CensoredWords)
    End Sub

    Public Function CensorText(ByVal strInput As String) As String

        'If theres no text to censor then throw an error
        If strInput Is Nothing Then Throw New ArgumentNullException("text")
        Dim strCensoredText As String = strInput 'output

        For Each censoredWord As String In CensoredWords 'for each bad word we have
            Dim regularExpression As String = ToRegexPattern(censoredWord) 'Make a new regular expression to match it
            strCensoredText = Regex.Replace(strCensoredText, regularExpression, AddressOf StarCensoredMatch, RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant) 'call StarCensoredMatch if we find the regex in the string
        Next

        Return strCensoredText
    End Function

    Private Shared Function StarCensoredMatch(ByVal m As Match) As String
        Dim word As String = m.Captures(0).Value    '| Replace each bad letter with
        Return New String("*"c, word.Length)        '| a star character
    End Function

    Private Function ToRegexPattern(ByVal wildcardSearch As String) As String
        'Convert out bad word to a regex search pattern

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

