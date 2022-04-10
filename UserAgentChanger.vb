Module UserAgentChanger
    <Runtime.InteropServices.DllImport("urlmon.dll", CharSet:=Runtime.InteropServices.CharSet.Ansi)>
    Private Function UrlMkSetSessionOption(
        ByVal dwOption As Integer,
        ByVal pBuffer As String,
        ByVal dwBufferLength As Integer,
        ByVal dwReserved As Integer) As Integer
    End Function

    Const URLMON_OPTION_USERAGENT As Integer = &H10000001
    Const URLMON_OPTION_USERAGENT_REFRESH As Integer = &H10000002

    Public Sub SetUserAgent(ByVal UserAgent As String)
        UrlMkSetSessionOption(URLMON_OPTION_USERAGENT_REFRESH, vbNullString, 0, 0)
        UrlMkSetSessionOption(URLMON_OPTION_USERAGENT, UserAgent, UserAgent.Length, 0)
    End Sub
End Module
