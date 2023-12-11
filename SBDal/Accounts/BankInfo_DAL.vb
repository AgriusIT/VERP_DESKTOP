'' 21-12-2013 ReqID-957   M Ijaz Javed      Bank information entry option
'' 28-Dec-2013 R:M6   Imran Ali Release Bug
''4-Jan-2014 Tsk:2368         Imran Ali             Multi Cheque Layout
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBModel
Imports SBUtility.Utility

Public Class BankInfo_DAL

    Public Function Save(ByVal bank As BankInfo_BE) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Try
            Dim str As String = String.Empty
            'Before against R:M6
            'str = "Insert into tblBankInfo(Holder_Name,Account_No,Branch_Area,Bank_Id) values(N'" & bank.Holder_Name & "', N'" & bank.Account_No & "', N'" & bank.Branch_Area & "'," & bank.Bank_Id & ")Select @@Identity"
            'R:M6 Added Column Branch Phone No.
            'Before against task:2368
            'str = "Insert into tblBankInfo(Holder_Name,Account_No,Branch_Area,Bank_Id, Branch_PhoneNo) values(N'" & bank.Holder_Name & "', N'" & bank.Account_No & "', N'" & bank.Branch_Area & "'," & bank.Bank_Id & ", N'" & bank.BranchPhoneNo.Replace("'", "''") & "')Select @@Identity"
            'Task:2368 Added Column ChequeLayoutId
            str = "Insert into tblBankInfo(Holder_Name,Account_No,Branch_Area,Bank_Id, Branch_PhoneNo,ChequeLayoutId,Bank_Type,Designated_To) values(N'" & bank.Holder_Name & "', N'" & bank.Account_No & "', N'" & bank.Branch_Area & "'," & bank.Bank_Id & ", N'" & bank.BranchPhoneNo.Replace("'", "''") & "', " & bank.ChequeLayoutId & ", N'" & bank.BankType.Replace("'", "''") & "', N'" & bank.DesignatedTo.Replace("'", "''") & "')Select @@Identity"
            'End Task:2368
            'End R:M6
            SQLHelper.ExecuteScaler(trans, CommandType.Text, str)

            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Return False
        Finally
            con.Close()
        End Try
    End Function

    Public Function Update(ByVal bank As BankInfo_BE) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Try
            Dim str As String = String.Empty
            'Before against request no. M6
            'str = "Update tblBankInfo set Holder_Name = N'" & bank.Holder_Name & "', Account_No= N'" & bank.Account_No & "',Branch_Area= N'" & bank.Branch_Area & "',Bank_Id= " & bank.Bank_Id & " Where AccountHolder_Id = " & bank.AccountHolder_Id
            'R:M6 Added Column Branch Phone No.
            'Before against task:2368
            'str = "Update tblBankInfo set Holder_Name = N'" & bank.Holder_Name & "', Account_No= N'" & bank.Account_No & "',Branch_Area= N'" & bank.Branch_Area & "',Bank_Id= " & bank.Bank_Id & ", Branch_PhoneNo=N'" & bank.BranchPhoneNo.Replace("'", "''") & "' Where AccountHolder_Id = " & bank.AccountHolder_Id
            'Task:2368 added column ChequeLayoutId
            str = "Update tblBankInfo set Holder_Name = N'" & bank.Holder_Name & "', Account_No= N'" & bank.Account_No & "',Branch_Area= N'" & bank.Branch_Area & "',Bank_Id= " & bank.Bank_Id & ", Branch_PhoneNo=N'" & bank.BranchPhoneNo & "', ChequeLayoutId=" & bank.ChequeLayoutId & ", Bank_Type=N'" & bank.BankType & "', Designated_To=N'" & bank.DesignatedTo & "' Where AccountHolder_Id = " & bank.AccountHolder_Id
            'End Task:2368

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Return False
        Finally
            con.Close()
        End Try
    End Function

    'Public Function Delete(ByVal bank As BankInfo_BE) As Boolean
    '    Dim con As New SqlConnection(SQLHelper.CON_STR)
    '    If con.State = ConnectionState.Closed Then con.Open()
    '    Dim trans As SqlTransaction = con.BeginTransaction
    '    Try
    '        Dim str As String = String.Empty
    '        str = "Delete from tblBankInfo where AccountHolder_Id = " & bank.AccountHolder_Id

    '        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

    '        trans.Commit()
    '        Return True

    '    Catch ex As Exception
    '        trans.Rollback()
    '        Return False
    '    Finally
    '        con.Close()
    '    End Try
    'End Function

    Public Function GetAllRecords() As DataTable
        Try
            Dim str As String = String.Empty
            'Before against request no. M6
            'str = "SELECT     coa_detail_id,detail_title, Info.AccountHolder_Id, Info.Account_No, Info.Holder_Name, Info.Branch_Area,Info.Bank_Id from dbo.vwCOADetail" _
            '& " LEFT OUTER JOIN (Select * from tblBankInfo) Info ON Info.Bank_Id = dbo.vwCOADetail.coa_detail_id " _
            '& " Where Account_Type = 'Bank'"
            'R:M6 Added Column Branch Phone No.
            'Before against task:2368
            'str = "SELECT     coa_detail_id,detail_title, Info.AccountHolder_Id, Info.Account_No, Info.Holder_Name, Info.Branch_Area,Info.Bank_Id, Info.Branch_PhoneNo from dbo.vwCOADetail" _
            '& " LEFT OUTER JOIN (Select * from tblBankInfo) Info ON Info.Bank_Id = dbo.vwCOADetail.coa_detail_id " _
            '& " Where Account_Type = 'Bank'"
            'Task:2368 Added Column ChequeLayoutId
            str = "SELECT     coa_detail_id,detail_title, Info.AccountHolder_Id, Info.Account_No, Info.Holder_Name, Info.Branch_Area,Info.Bank_Id, Info.Branch_PhoneNo,Info.Bank_Type, Info.Designated_To, Isnull(Info.ChequeLayoutId,0) as ChequeLayoutId from dbo.vwCOADetail" _
            & " LEFT OUTER JOIN (Select * from tblBankInfo) Info ON Info.Bank_Id = dbo.vwCOADetail.coa_detail_id " _
            & " Where Account_Type = 'Bank'"
            'End Task:2368
            'End R:M6
            Dim dt As DataTable
            dt = UtilityDAL.GetDataTable(str)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
