Imports System.IO
Imports System.Reflection

Public Class Utils
    'Dim _imageStream As Stream
    'Dim _textStreamReader As StreamReader
    'Dim _assembly As [Assembly]

    Public Shared Function GetManifestResourceNames() As String()
        Try
            Return [Assembly].GetExecutingAssembly().GetManifestResourceNames()

            'Return _assembly.GetManifestResourceNames()

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function GetManifestResourceStream(ByVal ScriptFile As String) As System.IO.Stream
        Try
            Return [Assembly].GetExecutingAssembly().GetManifestResourceStream(ScriptFile)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
