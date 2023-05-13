'03-Oct-2018 TFS4629 : Saad Afzaal  : Add new functions to save update and delete records.
Imports System
Imports System.Data.SqlClient
Imports SBModel
Imports SBDal

Public Class IntermPaymentCertificateDAL

    ''' <summary>
    ''' Saad Afzaal : Save the Data of Master and Detail in Interm Payment Certificate
    ''' </summary>
    ''' <param name="Progress"></param>
    ''' <returns></returns>
    ''' <remarks>03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
    Public Function Save(ByVal Progress As IntermPaymentCertificateBE, ByRef ProgressId As Integer) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Insert Master records
            str = "Insert Into IntermPaymentCertificateMaster (DocNo,DocDate,CustomerId,SOId,ItemId,Approved,SendForApproval,Rejected,Voucher) Values(N'" & Progress.ProgressNo & "',N'" & Progress.ProgressDate & "'," & Progress.CustomerId & "," & Progress.SOId & "," & Progress.ItemId & ",'0','0','0','0') Select @@Identity"
            ''TFS2712 : Change SqlHelper command to get Progress id
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ProgressId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            'Insert Detail records
            For Each obj As IntermPaymentCertificateDetailBE In Progress.Detail
                'str = "Insert Into tblTaskProgressDetail (ProgressId,ContractDetailId,TaskId,TaskTitle,TaskRate,PrevProgress,CurrentProgress,ApprovedProgress,NetProgress,NetValue) Values(" & "ident_current('tblTaskProgressMaster')" & "," & obj.ContractId & "," & obj.TaskId & ",N'" & obj.TaskTitle & "'," & obj.TaskRate & "," & obj.PreviousProgress & "," & obj.CurrentProgress & "," & obj.ApprovedProgress & "," & obj.NetProgress & "," & obj.NetValue & ")"
                'Saad Afzaal : TFS4629 : Add New columns of Qty, Measurment, Contract Value
                str = "Insert Into IntermPaymentCertificateDetail (ProgressId,ContractDetailId,TaskId,TaskTitle,TaskDetail,TaskRate,TaskUnit,PrevProgress,CurrentProgress,ApprovedProgress,NetProgress,NetValue,SOQty,TotalMeasurment,ContractValue) Values(" & "ident_current('IntermPaymentCertificateMaster')" & "," & obj.ContractId & "," & obj.TaskId & ",N'" & obj.TaskTitle & "',N'" & obj.TaskDetail & "'," & obj.TaskRate & ",N'" & obj.TaskUnit & "'," & obj.PreviousProgress & "," & obj.CurrentProgress & "," & obj.ApprovedProgress & "," & obj.NetProgress & "," & obj.NetValue & "," & obj.Qty & "," & obj.Measurment & "," & obj.ContractValue & ")"
                'Saad Afzaal : TFS4629 : End
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
    ''' Saad Afzaal : Update Master and Details records and also Check if detail records exists already then update else insert.
    ''' </summary>
    ''' <param name="Progress"></param>
    ''' <returns></returns>
    ''' <remarks>03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
    Public Function Update(ByVal Progress As IntermPaymentCertificateBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Update Master records
            str = "Update IntermPaymentCertificateMaster Set DocNo=N'" & Progress.ProgressNo & "' ,DocDate=N'" & Progress.ProgressDate & "' ,CustomerId=" & Progress.CustomerId & ",SOId=" & Progress.SOId & " ,ItemId=" & Progress.ItemId & " Where Id = " & Progress.ProgressId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Update Detail records
            For Each obj As IntermPaymentCertificateDetailBE In Progress.Detail
                'Saad Afzaal : TFS4629 : Update New added columns
                str = "If Exists(Select Id From IntermPaymentCertificateDetail Where Id=" & obj.DetailId & ")  Update IntermPaymentCertificateDetail Set ProgressId = " & Progress.ProgressId & ",ContractDetailId = " & obj.ContractId & ",TaskId = " & obj.TaskId & ",TaskTitle = N'" & obj.TaskTitle & "',TaskDetail = N'" & obj.TaskDetail & "',TaskRate = " & obj.TaskRate & ",TaskUnit = N'" & obj.TaskUnit & "',PrevProgress = " & obj.PreviousProgress & ",CurrentProgress = " & obj.CurrentProgress & ",ApprovedProgress = " & obj.ApprovedProgress & ",NetProgress = " & obj.NetProgress & ",NetValue = " & obj.NetValue & ",SOQty = " & obj.Qty & ",TotalMeasurment = " & obj.Measurment & ",ContractValue = " & obj.ContractValue & " Where Id = " & obj.DetailId & "" _
                    & " Else Insert Into IntermPaymentCertificateDetail (ProgressId,ContractDetailId,TaskId,TaskTitle,TaskDetail,TaskRate,TaskUnit,PrevProgress,CurrentProgress,ApprovedProgress,NetProgress,NetValue,SOQty,TotalMeasurment,ContractValue) Values(" & Progress.ProgressId & "," & obj.ContractId & "," & obj.TaskId & ",N'" & obj.TaskTitle & "',N'" & obj.TaskDetail & "'," & obj.TaskRate & ",N'" & obj.TaskUnit & "'," & obj.PreviousProgress & "," & obj.CurrentProgress & "," & obj.ApprovedProgress & "," & obj.NetProgress & "," & obj.NetValue & "," & obj.Qty & "," & obj.Measurment & "," & obj.ContractValue & ")"
                'Saad Afzaal : TFS4629 : End
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
    ''' Saad Afzaal : Delete the detail and master records.
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks>03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
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
            str = "Delete from IntermPaymentCertificateDetail Where ProgressId = " & Id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete Master records
            str = "Delete from IntermPaymentCertificateMaster Where Id = " & Id & ""
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
    ''' Saad Afzaal : Delete the detail record.
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <remarks>03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
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
            str = "Delete from IntermPaymentCertificateDetail Where Id = " & Id & ""
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
