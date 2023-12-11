Imports System
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Public Class POSConfigurationDAL

    Public Function Save(ByVal POS As POSConfigurationBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            str = "Insert Into tblPOSConfiguration(POSTitle,CompanyId,LocationId,CostCenterId,CashAccountId,BankAccountId, SalesPersonId, DeliveryOption,Active, DiscountPer) Values(N'" & POS.POSTitle & "'," & POS.CompanyId & "," & POS.LocationId & "," & POS.CostCenterId & "," & POS.CashAccountId & "," & POS.BankAccountId & "," & POS.SalesPersonId & ",'" & POS.DeliveryOption & "','" & POS.Active & "','" & POS.DiscountPer & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function SaveCreditDetail(ByVal list As List(Of POSDetailConfigurationBE)) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            For Each obj As POSDetailConfigurationBE In list
                str = "INSERT INTO tblCreditCardAccount(POSTitle, MachineTitle, BankAccountId) Values(N'" & obj.POSTitle & "', N'" & obj.MachineTitle & "', " & obj.BankAccountId2 & ")"
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

    Public Function Update(ByVal POS As POSConfigurationBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            str = "Update tblPOSConfiguration Set POSTitle=N'" & POS.POSTitle & "',CompanyId =" & POS.CompanyId & " ,LocationId=" & POS.LocationId & ",CostCenterId=" & POS.CostCenterId & " ,CashAccountId=" & POS.CashAccountId & ",BankAccountId=" & POS.BankAccountId & ",SalesPersonId=" & POS.SalesPersonId & ", DeliveryOption = '" & POS.DeliveryOption & "', Active= '" & POS.Active & "', DiscountPer= '" & POS.DiscountPer & "' WHERE POSId = " & POS.POSId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
        End Try
    End Function

    Public Function UpdateCreditDetail(ByVal list As List(Of POSDetailConfigurationBE)) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            For Each obj As POSDetailConfigurationBE In list
                str = "If Exists(Select ISNULL(CreditCardId, 0) as CreditCardId From tblCreditCardAccount Where CreditCardId=" & obj.CreditCardId & ") Update tblCreditCardAccount Set POSTitle =N'" & obj.POSTitle & "', MachineTitle=N'" & obj.MachineTitle & "',BankAccountId =" & obj.BankAccountId2 & "  WHERE CreditCardId = " & obj.CreditCardId & "" _
                 & " Else INSERT INTO tblCreditCardAccount(POSTitle, MachineTitle, BankAccountId) Values(N'" & obj.POSTitle & "', N'" & obj.MachineTitle & "', " & obj.BankAccountId2 & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
        End Try
    End Function
    Public Function DeleteDetail(ByVal Id As Integer) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            str = "Delete from tblCreditCardAccount Where CreditCardID = " & Id & ""
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
End Class
