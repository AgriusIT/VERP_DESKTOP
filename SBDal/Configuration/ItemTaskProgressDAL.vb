'29-June-2017 TFS1014 : Ali Faisal : Add new functions to save update and delete records.
'03-March-2018 TFS2712 : Ayesha Rehman : Save & Send for approval didn't work properly
Imports System
Imports System.Data.SqlClient
Imports SBModel
Imports SBDal
Public Class ItemTaskProgressDAL
    ''' <summary>
    ''' Ali Faisal : Save the Data of Master and Detail in Task Progress tables.
    ''' </summary>
    ''' <param name="Progress"></param>
    ''' <returns></returns>
    ''' <remarks>29-June-2017 TFS1014 : Ali Faisal</remarks>
    Public Function Save(ByVal Progress As ItemTaskProgressBE, ByRef ProgressId As Integer) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Insert Master records
            str = "Insert Into tblTaskProgressMaster (DocNo,DocDate,VendorId,POId,ItemId,Approved,SendForApproval,Rejected,Voucher) Values(N'" & Progress.ProgressNo & "',N'" & Progress.ProgressDate & "'," & Progress.VendorId & "," & Progress.POId & "," & Progress.ItemId & ",'0','0','0','0') Select @@Identity"
            ''TFS2712 : Change SqlHelper command to get Progress id
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ProgressId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            'Insert Detail records
            For Each obj As ItemTaskProgressDetailBE In Progress.Detail
                'str = "Insert Into tblTaskProgressDetail (ProgressId,ContractDetailId,TaskId,TaskTitle,TaskRate,PrevProgress,CurrentProgress,ApprovedProgress,NetProgress,NetValue) Values(" & "ident_current('tblTaskProgressMaster')" & "," & obj.ContractId & "," & obj.TaskId & ",N'" & obj.TaskTitle & "'," & obj.TaskRate & "," & obj.PreviousProgress & "," & obj.CurrentProgress & "," & obj.ApprovedProgress & "," & obj.NetProgress & "," & obj.NetValue & ")"
                'Ali Faisal : TFS1463 : Add New columns of Qty, Measurment, Contract Value
                str = "Insert Into tblTaskProgressDetail (ProgressId,ContractDetailId,TaskId,TaskTitle,TaskDetail,TaskRate,TaskUnit,PrevProgress,CurrentProgress,ApprovedProgress,NetProgress,NetValue,POQty,TotalMeasurment,ContractValue) Values(" & "ident_current('tblTaskProgressMaster')" & "," & obj.ContractId & "," & obj.TaskId & ",N'" & obj.TaskTitle & "',N'" & obj.TaskDetail & "'," & obj.TaskRate & ",N'" & obj.TaskUnit & "'," & obj.PreviousProgress & "," & obj.CurrentProgress & "," & obj.ApprovedProgress & "," & obj.NetProgress & "," & obj.NetValue & "," & obj.Qty & "," & obj.Measurment & "," & obj.ContractValue & ")"
                'Ali Faisal : TFS1463 : End
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : Update Master and Details records and also Check if detail records exists already then update else insert.
    ''' </summary>
    ''' <param name="Progress"></param>
    ''' <returns></returns>
    ''' <remarks>29-June-2017 TFS1014 : Ali Faisal</remarks>
    Public Function Update(ByVal Progress As ItemTaskProgressBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Update Master records
            str = "Update tblTaskProgressMaster Set DocNo=N'" & Progress.ProgressNo & "' ,DocDate=N'" & Progress.ProgressDate & "' ,VendorId=" & Progress.VendorId & ",POId=" & Progress.POId & " ,ItemId=" & Progress.ItemId & " Where Id = " & Progress.ProgressId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Update Detail records
            For Each obj As ItemTaskProgressDetailBE In Progress.Detail
                'str = "If Exists(Select Id From tblTaskProgressDetail Where Id=" & obj.DetailId & ")  Update tblTaskProgressDetail Set ProgressId = " & Progress.ProgressId & ",ContractDetailId = " & obj.ContractId & ",TaskId = " & obj.TaskId & ",TaskTitle = N'" & obj.TaskTitle & "',TaskRate = " & obj.TaskRate & ",PrevProgress = " & obj.PreviousProgress & ",CurrentProgress = " & obj.CurrentProgress & ",ApprovedProgress = " & obj.ApprovedProgress & ",NetProgress = " & obj.NetProgress & ",NetValue = " & obj.NetValue & " Where Id = " & obj.DetailId & "" _
                '    & " Else Insert Into tblTaskProgressDetail (ProgressId,ContractDetailId,TaskId,TaskTitle,TaskRate,PrevProgress,CurrentProgress,ApprovedProgress,NetProgress,NetValue) Values(" & Progress.ProgressId & "," & obj.ContractId & "," & obj.TaskId & ",N'" & obj.TaskTitle & "'," & obj.TaskRate & "," & obj.PreviousProgress & "," & obj.CurrentProgress & "," & obj.ApprovedProgress & "," & obj.NetProgress & "," & obj.NetValue & ")"
                'Ali Faisal : TFS1463 : Update New added columns
                str = "If Exists(Select Id From tblTaskProgressDetail Where Id=" & obj.DetailId & ")  Update tblTaskProgressDetail Set ProgressId = " & Progress.ProgressId & ",ContractDetailId = " & obj.ContractId & ",TaskId = " & obj.TaskId & ",TaskTitle = N'" & obj.TaskTitle & "',TaskDetail = N'" & obj.TaskDetail & "',TaskRate = " & obj.TaskRate & ",TaskUnit = N'" & obj.TaskUnit & "',PrevProgress = " & obj.PreviousProgress & ",CurrentProgress = " & obj.CurrentProgress & ",ApprovedProgress = " & obj.ApprovedProgress & ",NetProgress = " & obj.NetProgress & ",NetValue = " & obj.NetValue & ",POQty = " & obj.Qty & ",TotalMeasurment = " & obj.Measurment & ",ContractValue = " & obj.ContractValue & " Where Id = " & obj.DetailId & "" _
                    & " Else Insert Into tblTaskProgressDetail (ProgressId,ContractDetailId,TaskId,TaskTitle,TaskDetail,TaskRate,TaskUnit,PrevProgress,CurrentProgress,ApprovedProgress,NetProgress,NetValue,POQty,TotalMeasurment,ContractValue) Values(" & Progress.ProgressId & "," & obj.ContractId & "," & obj.TaskId & ",N'" & obj.TaskTitle & "',N'" & obj.TaskDetail & "'," & obj.TaskRate & ",N'" & obj.TaskUnit & "'," & obj.PreviousProgress & "," & obj.CurrentProgress & "," & obj.ApprovedProgress & "," & obj.NetProgress & "," & obj.NetValue & "," & obj.Qty & "," & obj.Measurment & "," & obj.ContractValue & ")"
                'Ali Faisal : TFS1463 : End
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
    ''' <summary>
    ''' Ali Faisal : Delete the detail and master records.
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks>29-June-2017 TFS1014 : Ali Faisal</remarks>
    Public Function Delete(ByVal Id As Integer) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Delete Detail records
            str = "Delete from tblTaskProgressDetail Where ProgressId = " & Id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete Master records
            str = "Delete from tblTaskProgressMaster Where Id = " & Id & ""
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
    ''' <summary>
    ''' Ali Faisal : Delete the detail record.
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <remarks>29-June-2017 TFS1014 : Ali Faisal</remarks>
    Public Sub DeleteDetail(ByVal Id As Integer)
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Delete Detail records
            str = "Delete from tblTaskProgressDetail Where Id = " & Id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Sub
End Class
