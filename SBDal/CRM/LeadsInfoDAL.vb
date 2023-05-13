Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Data.SqlClient
Public Class LeadsInfoDAL
    Dim LeadNo As String = String.Empty

    Public Function Add(ByVal Leads As LeadInfoBE) As Integer
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim int As Integer = 0
            LeadNo = GetDocumentNo(trans)
            Dim str As String = String.Empty
            str = "INSERT INTO tblLeads(LeadTopic, LeadName, CompanyName, JobTitle,BusinessType, Address,City, State, BusinessPhone, MobilePhone,OtherPhone, Fax,Email, WebSite, LeadDescription, LeadNo, AccountId, FollowupHistory,Followup,EntryDate, AssignedTo) " _
            & " VALUES(N'" & Leads.LeadTopic.Replace("'", "''") & "', N'" & Leads.LeadName.Replace("'", "''") & "', N'" & Leads.CompanyName.Replace("'", "''") & "', N'" & Leads.JobTitle.Replace("'", "''") & "', N'" & Leads.BusinessType.Replace("'", "''") & "', N'" & Leads.Address.Replace("'", "''") & "', N'" & Leads.City.Replace("'", "''") & "', N'" & Leads.State.Replace("'", "''") & "',  N'" & Leads.BusinessPhone.Replace("'", "''") & "', N'" & Leads.MobilePhone.Replace("'", "''") & "', N'" & Leads.otherphone.Replace("'", "''") & "', N'" & Leads.Fax.Replace("'", "''") & "', N'" & Leads.Email.Replace("'", "''") & "', N'" & Leads.WebSite.Replace("'", "''") & "', N'" & Leads.LeadDescription.Replace("'", "''") & "', N'" & LeadNo.Replace("'", "''") & "'," & Leads.AccountId & ", '" & Leads.FollowupHistory.Replace("'", "''") & "',Convert(DateTime,'" & Leads.Followup.ToString("yyyy-M-d hh:mm:ss tt") & "',102),Convert(Datetime,'" & Leads.EntryDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102), " & Leads.AssignedTo & ") Select @@Identity"
            int = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            trans.Commit()
            Return int
        Catch ex As SqlException
            trans.Rollback()
            Throw New Exception(SQLHelper.GetSQLErrorMessage(ex.Number))
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Update(ByVal Leads As LeadInfoBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "Update tblLeads SET LeadTopic=N'" & Leads.LeadTopic.Replace("'", "''") & "'," _
                & " LeadName=N'" & Leads.LeadName.Replace("'", "''") & "', " _
                & " CompanyName=N'" & Leads.CompanyName.Replace("'", "''") & "', " _
                & " JobTitle=N'" & Leads.JobTitle.Replace("'", "''") & "', " _
                & " BusinessType=N'" & Leads.BusinessType.Replace("'", "''") & "', " _
                & " Address=N'" & Leads.Address.Replace("'", "''") & "', " _
                & " City=N'" & Leads.City.Replace("'", "''") & "', " _
                & " State=N'" & Leads.State.Replace("'", "''") & "', " _
                & " BusinessPhone=N'" & Leads.BusinessPhone.Replace("'", "''") & "', " _
                & " MobilePhone=N'" & Leads.MobilePhone.Replace("'", "''") & "', " _
                & " OtherPhone=N'" & Leads.otherphone.Replace("'", "''") & "', " _
                & " Fax=N'" & Leads.Fax.Replace("'", "''") & "', " _
                & " Email=N'" & Leads.Email.Replace("'", "''") & "', " _
                & " WebSite=N'" & Leads.WebSite.Replace("'", "''") & "', " _
                & " AssignedTo= '" & Leads.AssignedTo & "', " _
                & " LeadDescription=N'" & Leads.LeadDescription.Replace("'", "''") & "', LeadNo=N'" & Leads.LeadNo.Replace("'", "''") & "',AccountId=" & Leads.AccountId & ", FollowupHistory='" & Leads.FollowupHistory.Replace("'", "''") & "',Followup=Convert(DateTime,'" & Leads.Followup.ToString("yyyy-M-d hh:mm:ss tt") & "',102) WHERE LeadId=" & Leads.LeadId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As SqlException
            trans.Rollback()
            Throw New Exception(SQLHelper.GetSQLErrorMessage(ex.Number))
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Delete(ByVal Leads As LeadInfoBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "DELETE From tblLeads WHERE Leadid=" & Leads.LeadId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetAll(Optional ByVal Condition As String = "", Optional ByVal Fromdate As DateTime = Nothing, Optional ByVal Todate As DateTime = Nothing) As DataTable
        Try
            Dim str As String = String.Empty
            str = "Select " & IIf(Condition = "All", "", "Top 50") & " LeadId,LeadTopic,LeadName,JobTitle,CompanyName,BusinessType,Address,City,State,BusinessPhone,MobilePhone,OtherPhone,tblLeads.Fax,tblLeads.Email,tblLeads.WebSite,LeadDescription,LeadNo,AccountId,FollowupHistory,Followup,EntryDate,AssignedTo, tblUser.User_Name As [Assigned To] from tblLeads LEFT OUTER JOIN tblUser On tblLeads.AssignedTo= tblUser.User_ID Where LeadId > 0 "
            If Not Fromdate = Date.MinValue Then
                str += " and Convert(varchar, EntryDate, 102) >= Convert(datetime, N'" & Fromdate.ToString("yyyy-M-d 00:00:00") & "', 102)"
            End If
            If Not Todate = Date.MinValue Then
                str += " and Convert(varchar, EntryDate, 102) <= Convert(datetime, N'" & Todate.ToString("yyyy-M-d 23:59:59") & "', 102)"
            End If
            str += " ORDER BY LeadNo DESC"
            Dim dt As DataTable = UtilityDAL.GetDataTable(str)
            For Each dr As DataRow In dt.Rows
                dr.BeginEdit()
                If Not dr("Assigned To") Is DBNull.Value Then
                    dr("Assigned To") = UtilityDAL.Decrypt(dr("Assigned To").ToString)
                End If
                dr.EndEdit()
            Next
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetDocumentNo(Optional ByVal trans As SqlTransaction = Nothing) As String
        Try
            Return UtilityDAL.GetSerialNo("LD-" & Right(Date.Now.Year, 2) & "-", "tblLeads", "LeadNo", trans)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'TFS1859:Rai haider : Check Duplicate record for Mobile number in LeadsInfo
    Public Function IsMobileExist(ByVal Mobilenumber As String) As Boolean

        Try
            Dim str As String = String.Empty
            str = "select MobilePhone from tblLeads where MobilePhone =N'" & Mobilenumber.Replace("'", "''") & "'"

            Dim dt As DataTable
            dt = UtilityDAL.GetDataTable(str)
            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function IsMobileExist(ByVal Mobilenumber As String, pk As String) As Boolean

        Try
            Dim str As String = String.Empty
            str = "select MobilePhone from tblLeads where MobilePhone =N'" & Mobilenumber.Replace("'", "''") & "' And LeadNo <>N'" & Pk.Replace("'", "''") & "'"

            Dim dt As DataTable
            dt = UtilityDAL.GetDataTable(str)
            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    'End TFS1859:06-Dec-17
End Class
