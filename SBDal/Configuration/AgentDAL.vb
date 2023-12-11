Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBModel
Imports SBDAL
Imports SBDAL.SqlHelper
Public Class AgentDAL
   
    Const _New As String = "New"
    Const _Update As String = "Update"
    Const _Delete As String = "Delete"

    ''' <summary>
    ''' Saba Shabbir TFS2562 created a function to get a record according to agentId where double click occur
    ''' </summary>
    ''' <param name="Saba shabbir"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.empty
        Try

            strSQL = " SELECT PropertyProfileAgent.PropertyProfileAgentId, PropertyProfileAgent.PropertyProfileId, PropertyProfileAgent.AgentId, PropertyProfileAgent.Activity, PropertyProfileAgent.CommissionAmount, PropertyProfileAgent.Remarks, tblVoucher.voucher_no FROM tblVoucher INNER JOIN tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id RIGHT OUTER JOIN PropertyProfileAgent LEFT OUTER JOIN Agent ON PropertyProfileAgent.AgentId = Agent.AgentId ON tblVoucherDetail.coa_detail_id = Agent.coa_detail_id WHERE PropertyProfileAgent.PropertyProfileId = " & ID
            'strSQL = "SELECT Agent.Name, Agent.AgentId AS AgentId, PropertyProfileAgent.*, PropertyProfileAgent.PropertyProfileAgentId AS Id, PropertyProfileAgent.PropertyProfileId AS ProfileId, PropertyProfileAgent.AgentId AS Expr4, PropertyProfileAgent.Activity AS Expr5, PropertyProfileAgent.CommissionAmount AS Expr6, PropertyProfileAgent.Remarks AS Expr7 FROM Agent RIGHT OUTER JOIN PropertyProfileAgent ON Agent.AgentId =" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function Save(ByVal Obj As BEAgent) As Boolean
        Dim Conn As New SqlConnection(CON_STR)
        Dim trans As SqlTransaction
        Try
            'Public Property Name As String
            'Public Property FathersName As String
            'Public Property CNIC As String
            'Public Property PrimaryMobile As String
            'Public Property SecondaryMobile As String
            'Public Property coa_detail_id As Integer
            'Public Property AddressLine1 As String
            'Public Property AddressLine2 As String
            'Public Property CityId As Integer
            'Public Property SpecialityId As Integer
            'Public Property Email As String
            'Public Property BloodGroup as String
            'Public Property Active As Boolean
            '
            If Conn.State = ConnectionState.Closed Then Conn.Open()
            trans = Conn.BeginTransaction
            Obj.coa_detail_id = New COADetailDAL().Add(Obj.Account, Obj.Account.MainSubSubID, trans)
            Dim str As String = String.Empty
            'Saba Shabbir: TFS2566 Add a new column in BloodGroup in Agent and update insert query
            str = " INSERT INTO Agent(Name, FathersName, CNIC, PrimaryMobile, SecondaryMobile, AddressLine1, AddressLine2, CityId, Email,  Active, coa_detail_id, SpecialityId, Remarks, BloodGroup) " _
                 & " Values (N'" & Obj.Name & "', N'" & Obj.FathersName & "', N'" & Obj.CNIC & "', N'" & Obj.PrimaryMobile & "' , N'" & Obj.SecondaryMobile & "' , " _
                 & " N'" & Obj.AddressLine1 & "', N'" & Obj.AddressLine2 & "', " & Obj.CityId & " , N'" & Obj.Email & "', " & IIf(Obj.Active = True, 1, 0) & ",  " & Obj.coa_detail_id & ",  " & Obj.SpecialityId & ", N'" & Obj.Remarks.Replace("'", "''") & "', N'" & Obj.BloodGroup.Replace("'", "''") & "') "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            'UtilityDAL.G
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Conn.Close()
        End Try
    End Function
    Public Shared Function Update(ByVal Obj As BEAgent) As Boolean
        Dim Conn As New SqlConnection(CON_STR)
        Dim trans As SqlTransaction
        Try
            If Conn.State = ConnectionState.Closed Then Conn.Open()
            trans = Conn.BeginTransaction
            Dim str As String = String.Empty

            'Saba Shabbir: TFS2566 Add a new column in BloodGroup in Agent and update update query
            'str = "INSERT INTO ProductionProcess(ProcessName, Remarks) Values (N'" & Obj.ProcessName & "', N'" & Obj.Remarks & "') SELECT @@IDENTITY"
            str = " Update Agent SET Name = N'" & Obj.Name & "' , FathersName = N'" & Obj.FathersName & "', CNIC = N'" & Obj.CNIC & "', PrimaryMobile = N'" & Obj.PrimaryMobile & "' " _
                & " , SecondaryMobile = N'" & Obj.SecondaryMobile & "', AddressLine1 = N'" & Obj.AddressLine1 & "', AddressLine2 = N'" & Obj.AddressLine2 & "', CityId = " & Obj.CityId & ", Email = N'" & Obj.Email & "',  Active = " & IIf(Obj.Active = True, 1, 0) & ", coa_detail_id= " & Obj.coa_detail_id & ", SpecialityId= " & Obj.SpecialityId & ", Remarks = N'" & Obj.Remarks.Replace("'", "''") & "'  , BloodGroup = N'" & Obj.BloodGroup & "'" _
                & " WHERE AgentId = " & Obj.AgentId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Call New COADetailDAL().Update(Obj.Account, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Conn.Close()
        End Try
    End Function

    Public Shared Function Delete(ByVal AgentId As Integer) As Boolean
        Dim str As String = String.Empty
        Dim Conn As New SqlConnection(CON_STR)
        Dim trans As SqlTransaction
        Try
            If Conn.State = ConnectionState.Closed Then Conn.Open()
            trans = Conn.BeginTransaction
            str = "DELETE FROM Agent WHERE AgentId = " & AgentId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Conn.Close()
        End Try
    End Function

    Public Shared Function GetAgent(ByVal AgentId As Integer) As DataTable
        Try
            Dim str As String = String.Empty

            'Saba Shabbir: TFS2566 Add a new column in BloodGroup in Agent and update Select query
            str = "SELECT AgentId, Name, FathersName, CNIC, PrimaryMobile, SecondaryMobile, AddressLine1, AddressLine2, CityId, " _
                & " SpecialityId, Email,  Active, coa_detail_id, SpecialityId, Remarks, BloodGroup FROM Agent WHERE AgentId = " & AgentId & ""
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function AddAccount(ByVal Obj As BEAgent, ByVal trans As SqlTransaction) As Integer
        Try

            Dim Code As String = String.Empty
            Dim SubSubId As Integer = CInt(UtilityDAL.GetConfigValue("AgentSubSub", trans))
            Dim strSql As String
            strSql = " SELECT sub_sub_code  AS Title From tblCoaMainsubSub where main_sub_sub_id = " & SubSubId
            'adp = New OleDbDataAdapter(strSql, Con)
            'adp.Fill(dt)


            Dim DT As DataTable = UtilityDAL.GetDataTable(strSql, trans)
            If DT.Rows.Count > 0 Then
                Code = DT.Rows(0).Item(0).ToString
            End If
            'Me.txtCode.Text = Microsoft.VisualBasic.Right(GetNextDocNo(Me.txtMainCode.Text, (Me.txtMainCode.Text.Trim.Length + 1), "tblCOAMainSubSubDetail", "detail_code", (Me.txtMainCode.Text.Trim.Length + 6)), 5)
            'Sub SelectAccountCode()
            '    If Me.ComboBox1.SelectedValue > 0 Then
            '        Dim adp As New OleDbDataAdapter
            '        Dim dt As New DataTable
            '        Dim strSql As String
            '        strSql = " SELECT sub_sub_code  AS Title From tblCoaMainsubSub where main_sub_sub_id = " & Me.ComboBox1.SelectedValue
            '        adp = New OleDbDataAdapter(strSql, Con)
            '        adp.Fill(dt)
            '        Me.txtMainCode.Text = dt.Rows(0).Item(0).ToString
            '    End If
            'End Sub
            '"INSERT INTO [tblCOAMainSubsubDetail]([main_sub_sub_id], [Detail_code], [Detail_title],OpeningBalance, Active, Parent_Id, AccessLevel) "
            If Obj.coa_detail_id = 0 Then
                Dim str As String = String.Empty
                str = " INSERT INTO [tblCOAMainSubsubDetail]([main_sub_sub_id], [Detail_code], [Detail_title],OpeningBalance, Active) " _
                     & " Values (" & SubSubId & ", '" & Code & "', N'" & Obj.Name & "', " & 0 & " , " & 1 & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Else
                Dim str As String = String.Empty
                str = " Update tblCOAMainSubsubDetail SET main_sub_sub_id =" & SubSubId & ", Detail_code = '" & Code & "', Detail_title= N'" & Obj.Name & "', OpeningBalance = 0, Active = 1 "
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            End If
            'trans.Commit()
            'UtilityDAL.G
            Return True
        Catch ex As Exception
            'trans.Rollback()
            Throw ex
            'Finally
            '    Conn.Close()
        End Try
    End Function

    Public Shared Function ValidateAccount(ByVal Key As String) As Boolean
        Try
            Dim SubSub As String = UtilityDAL.GetConfigValue(Key)
            If Not SubSub = "Error" AndAlso Val(SubSub) > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetAll() As DataTable
        Try
            Dim str As String = String.Empty
            str = "SELECT Agent.AgentId, Agent.Name, Agent.FathersName, Agent.CNIC, Agent.PrimaryMobile, Agent.SecondaryMobile, Agent.AddressLine1, Agent.AddressLine2, Agent.CityId, tblListCity.CityName AS City, " _
                & " Agent.SpecialityId, Speciality.Speciality, Agent.Email, Agent.coa_detail_id, vwCOADetail.detail_title AS Account, Agent.Remarks,Agent.BloodGroup, Agent.Active FROM Agent LEFT JOIN tblListCity ON Agent.CityId = tblListCity.CityId LEFT JOIN Speciality on Agent.SpecialityId = Speciality.SpecialityId left outer join vwCOADetail ON Agent.coa_detail_id = vwCOADetail.coa_detail_id "
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetAccountTitle(ByVal AccountId As Integer) As String
        Try
            Dim str As String = String.Empty
            str = "SELECT detail_title FROM tblCOAMainSubsubDetail"
            Dim AccountTitle As String = UtilityDAL.GetDataTable(str).Rows.Item(0).ToString
            Return AccountTitle
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

