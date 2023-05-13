'25-July-2017 TFS# 1045 : Waqar Raza : Add Save, Update and Delete functions to save update and delete records from tblUserWiseCustomerList Table.
Imports System
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Public Class UserWiseCustomerDAL
    ''' <summary>
    '''Waqar Raza: Add Save function to insert the records in tblUserWiseCustomerList Table.
    ''' </summary>
    ''' <param name="list">Pass the parameter named List as object of the UserWiseCustomerBE</param>
    ''' <returns></returns>
    ''' <remarks>25-July-2017 TFS# 1045 : Waqar Raza</remarks>
    Public Function Save(ByVal list As List(Of UserWiseCustomerBE)) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            For Each obj As UserWiseCustomerBE In list
                str = "Insert Into tblUserWiseCustomerList (UserId,UserName,CustomerId,CustomerName) Values(" & obj.UserId & ",N'" & obj.UserName & "'," & obj.CustomerId & ",N'" & obj.CustomerName & "')"
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
    '25-July-2017 TFS# 1045 : Waqar Raza : End Save Function
    ''' <summary>
    '''Waqar Raza: Add Update function to insert the records in tblUserWiseCustomerList Table.
    ''' </summary>
    ''' <param name="list">Pass the parameter named List as object of the UserWiseCustomerBE</param>
    ''' <returns></returns>
    ''' <remarks>25-July-2017 TFS# 1045 : Waqar Raza</remarks>
    Public Function Update(ByVal list As List(Of UserWiseCustomerBE)) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            For Each obj As UserWiseCustomerBE In list
                str = "If Exists(Select UWCID From tblUserWiseCustomerList Where UWCID=" & obj.UWCID & ")  Update tblUserWiseCustomerList Set UserId = " & obj.UserId & ", UserName = N'" & obj.UserName & "', CustomerId = " & obj.CustomerId & ", CustomerName = N'" & obj.CustomerName & "' Where UWCID =" & obj.UWCID & "" _
                    & " Else Insert Into tblUserWiseCustomerList (UserId,UserName,CustomerId,CustomerName) Values(" & obj.UserId & ",N'" & obj.UserName & "'," & obj.CustomerId & ",N'" & obj.CustomerName & "')"
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
    '25-July-2017 TFS# 1045 : Waqar Raza : End Update Fucntion
    ''' <summary>
    '''Waqar Raza: Add Delete function to insert the records in tblUserWiseCustomerList Table.
    ''' </summary>
    ''' <param name="Id">Pass Item Id as Parameter to delete the records of selected item in dropdown</param>
    ''' <returns></returns>
    ''' <remarks>25-July-2017 TFS# 1045 : Waqar Raza</remarks>
    Public Function DeleteDetail(ByVal Id As Integer) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            str = "Delete from tblUserWiseCustomerList Where UWCId = " & Id & ""
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
    '25-July-2017 TFS# 1045 : Waqar Raza : End Delete Function
End Class
