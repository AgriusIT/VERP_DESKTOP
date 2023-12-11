'23-Jun-2015 Task#123062015 Ahmad Sharif: Design Dialog box for comments & written all the logic
'24-Jun-2015 Task#124062015 Ahmad Sharif: add KeyDown event for listening keys on dialog box

'Start Task#123062015 
Imports System.Data.OleDb

Public Class frmProjectVisitComments

    Private _ProjectCode As Integer  'Task#123062015 for storing ProjectCode

    'Task#123062015 Property of _ProjectCode for setting and getting _ProjectCode value
    Public Property ProjectCode() As Integer
        Get
            Return _ProjectCode
        End Get
        Set(ByVal value As Integer)
            _ProjectCode = value
        End Set
    End Property

    'Task#124062015 add KeyDown event for listening keys on dialog box
    Private Sub frmProjectVisitComments_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try

            If e.KeyCode = Keys.Escape Then
                CloseDialogBox()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CloseDialogBox()
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'End Task#124062015

    Private Sub frmProjectVisitComments_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try

            fillComments()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Task#123062015
    Private Sub fillComments()
        Try

            Dim objCommand As New OleDbCommand
            Dim objCon As OleDbConnection
            objCon = Con        'passing the global connection object to our local connection object

            'checking if connection is already opened, then close the connection 
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If

            objCon.Open()           'open the connection
            objCommand.Connection = objCon

            Dim reader As OleDbDataReader       'making OleDbDataReader object for storing query result

            'objCommand.CommandText = "select DirecotrComments,GMComments,ASMComments,ManagerComments,RPComments,OthersComments from tblProjectVisit Where ProjectId=" & ProjectCode

            objCommand.CommandText = "select DirecotrComments,GMComments,ASMComments,ManagerComments,RPComments,OthersComments from tblProjectVisit Where projectvisitid=" & ProjectCode
            'projectvisitid
            reader = objCommand.ExecuteReader()

            'checking reader object has values or not
            If reader.HasRows Then
                'Reading values row by row from reader object
                While reader.Read()
                    Me.txtDirector.Text = reader.GetValue(0).ToString
                    Me.txtGM.Text = reader.GetValue(1).ToString
                    Me.txtASM.Text = reader.GetValue(2).ToString
                    Me.txtManager.Text = reader.GetValue(3).ToString
                    Me.txtRP.Text = reader.GetValue(4).ToString
                    Me.txtOthers.Text = reader.GetValue(5).ToString
                End While
            Else
                Exit Sub    'exit from this Sub if reader object doesn't have any row/record
            End If

            objCon.Close()      'closing the opened connection

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Task#124062015 Toolstrip Cancel button event for closing dialog box
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            CloseDialogBox()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Task#124062015 Toolstrip Print button event for printing
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try

            AddRptParam("@ProjectId", ProjectCode)
            AddRptParam("@DirectorComments", Me.txtDirector.Text)
            AddRptParam("@GMComments", Me.txtGM.Text)
            AddRptParam("@ASMComments", Me.txtASM.Text)
            AddRptParam("@ManagerComments", Me.txtManager.Text)
            AddRptParam("@RPComments", Me.txtRP.Text)
            AddRptParam("@OtherComments", Me.txtOthers.Text)

            ShowReport("rptProjectVisitComments")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task#124062015

End Class
'End Task#123062015