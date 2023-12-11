'22-June-2017 TFS# 981 : Ali Faisal : Add Save, Update and Delete functions to save update and delete records from ArticleDefTaskDetails Table.
Imports System
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Public Class ServiceItemTaskDAL
    ''' <summary>
    ''' Ali Faisal : Add Save function to insert the records in ArticleDefTaskDetails Table.
    ''' </summary>
    ''' <param name="Item">Pass the parameter named Item as object of the ServicesDetailTaskBE</param>
    ''' <returns></returns>
    ''' <remarks>22-June-2017 TFS# 981 : Ali Faisal</remarks>
    Public Function Save(ByVal Item As ServiceItemTaskBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)

        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            For Each obj As ServiceItemDetailTaskBE In Item.Detail
                str = "Insert Into ArticleDefTaskDetails (ItemId,TaskTitle,TaskDetail,TaskUnit,TaskRate,SortOrder,Active) Values(" & Item.ItemId & ",N'" & obj.TaskTitle & "',N'" & obj.TaskDetail & "',N'" & obj.TaskUnit & "'," & obj.TaskRate & "," & obj.SortOrder & "," & IIf(obj.Active = True, 1, 0) & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                'check if query executed successfully
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
    '22-June-2017 TFS# 981 : Ali Faisal : End Save function

    ''' <summary>
    ''' Ali Faisal : Add Update function to update the records from ArticleDefTaskDetails Table if exists otherwise insert the record.
    ''' </summary>
    ''' <param name="Item">Pass the parameter named Item as object of the ServicesDetailTaskBE</param>
    ''' <returns></returns>
    ''' <remarks>22-June-2017 TFS# 981 : Ali Faisal </remarks>
    Public Function Update(ByVal Item As ServiceItemTaskBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            For Each obj As ServiceItemDetailTaskBE In Item.Detail
                str = "If Exists(Select Id From ArticleDefTaskDetails Where Id=" & obj.TaskId & ")  Update ArticleDefTaskDetails Set ItemId = " & Item.ItemId & ", TaskTitle = N'" & obj.TaskTitle & "', TaskDetail = N'" & obj.TaskDetail & "', TaskUnit = N'" & obj.TaskUnit & "', TaskRate = " & obj.TaskRate & ", SortOrder = " & obj.SortOrder & ", Active = " & IIf(obj.Active = True, 1, 0) & " Where Id=" & obj.TaskId & "" _
                    & " Else Insert Into ArticleDefTaskDetails (ItemId,TaskTitle,TaskDetail,TaskUnit,TaskRate,SortOrder,Active) Values(" & Item.ItemId & ",N'" & obj.TaskTitle & "',N'" & obj.TaskDetail & "',N'" & obj.TaskUnit & "'," & obj.TaskRate & "," & obj.SortOrder & "," & IIf(obj.Active = True, 1, 0) & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
    '22-June-2017 TFS# 981 : Ali Faisal : End Update function

    ''' <summary>
    ''' Ali Faisal : Add Delete function to remove the records from ArticleDefTaskDetails Table.
    ''' </summary>
    ''' <param name="ItemId">Pass Item Id as Parameter to delete the records of selected item in dropdown</param>
    ''' <returns></returns>
    ''' <remarks>22-June-2017 TFS# 981 : Ali Faisal </remarks>
    Public Function Delete(ByVal ItemId As Integer) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            str = "Delete from ArticleDefTaskDetails Where ItemId = " & ItemId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
    '22-June-2017 TFS# 981 : Ali Faisal : End Delete function

    ''' <summary>
    ''' Ali Faisal : Add DeleteDetail function to remove the records from ArticleDefTaskDetails Table.
    ''' It is used to remove the selected row from the grid and also from database if any
    ''' </summary>
    ''' <param name="Id">Pass the Detail Id of Task</param>
    ''' <returns></returns>
    ''' <remarks>29-June-2017 TFS# 981 : Ali Faisal </remarks>
    Public Function DeleteDetail(ByVal Id As Integer) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            str = "Delete from ArticleDefTaskDetails Where Id = " & Id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
    '29-June-2017 TFS# 981 : Ali Faisal : End DeleteDetail function
End Class
