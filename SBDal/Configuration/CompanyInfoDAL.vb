''11-June-2014 TASK:2677 Imran Ali Commercial Invoice Configuration On Company Information (Ravi)
Imports System.Data.SqlClient
Imports SBModel
Public Class CompanyInfoDAL
    Public Function AddRecord(ByVal modObj As CompanyInfo) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty
            'Before against task:2677
            'strSQL = "Insert Into CompanyDefTable(CompanyId, CompanyName, LegalName, Phone, Fax, Email, WebPage, Address, CostCenterId,Prefix) Select ISNULL(Max(CompanyId),0) + 1, N'" & modObj.CompanyName & "', N'" & modObj.LegalName & "', N'" & modObj.Phone & "', N'" & modObj.Fax & "', N'" & modObj.Email & "', N'" & modObj.WebPage & "', N'" & modObj.Address & "', " & modObj.CostCenterId & ", N'" & modObj.PreFix & "' From CompanyDefTable"
            'Task:2677 Added Field CommerialInvoice
            'strSQL = "Insert Into CompanyDefTable(CompanyId, CompanyName, LegalName, Phone, Fax, Email, WebPage, Address, CostCenterId,Prefix, CommercialInvoice) Select ISNULL(Max(CompanyId),0) + 1, N'" & modObj.CompanyName & "', N'" & modObj.LegalName & "', N'" & modObj.Phone & "', N'" & modObj.Fax & "', N'" & modObj.Email & "', N'" & modObj.WebPage & "', N'" & modObj.Address & "', " & modObj.CostCenterId & ", N'" & modObj.Prefix & "'," & IIf(modObj.CommercialInvoice = True, 1, 0) & " From CompanyDefTable"
            'End Task:2677
            strSQL = "Insert Into CompanyDefTable(CompanyId, CompanyName, LegalName, Phone, Fax, Email, WebPage, Address, CostCenterId,Prefix, CommercialInvoice,SalesTaxAccountId) Select ISNULL(Max(CompanyId),0) + 1, N'" & modObj.CompanyName & "', N'" & modObj.LegalName & "', N'" & modObj.Phone & "', N'" & modObj.Fax & "', N'" & modObj.Email & "', N'" & modObj.WebPage & "', N'" & modObj.Address & "', " & modObj.CostCenterId & ", N'" & modObj.Prefix & "'," & IIf(modObj.CommercialInvoice = True, 1, 0) & ", " & modObj.SalesTaxAccountId & " From CompanyDefTable"
            modObj.CompanyID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL))

            modObj.ActivityLog.ActivityName = "Save"
            modObj.ActivityLog.RecordType = "Configuration"
            modObj.ActivityLog.RefNo = modObj.CompanyID
            UtilityDAL.BuildActivityLog(modObj.ActivityLog, trans)
            trans.Commit()

            Return True
        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Update_Record(ByVal modobj As CompanyInfo) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            'Before against task:2677
            'strSQL = "Update CompanyDefTable set CompanyName=N'" & modobj.CompanyName & "', LegalName=N'" & modobj.LegalName & "', Phone=N'" & modobj.Phone & "', Fax=N'" & modobj.Fax & "', Email=N'" & modobj.Email & "', WebPage=N'" & modobj.WebPage & "', Address=N'" & modobj.Address & "', CostCenterId=" & modobj.CostCenterId & ", Prefix=N'" & modobj.Prefix & "' WHERE CompanyID=" & modobj.CompanyID
            'Task:2677 Added Field CommercialInvoice
            'strSQL = "Update CompanyDefTable set CompanyName=N'" & modobj.CompanyName & "', LegalName=N'" & modobj.LegalName & "', Phone=N'" & modobj.Phone & "', Fax=N'" & modobj.Fax & "', Email=N'" & modobj.Email & "', WebPage=N'" & modobj.WebPage & "', Address=N'" & modobj.Address & "', CostCenterId=" & modobj.CostCenterId & ", Prefix=N'" & modobj.Prefix & "', CommercialInvoice=" & IIf(modobj.CommercialInvoice = True, 1, 0) & " WHERE CompanyID=" & modobj.CompanyID
            'End Task:2677
            strSQL = "Update CompanyDefTable set CompanyName=N'" & modobj.CompanyName & "', LegalName=N'" & modobj.LegalName & "', Phone=N'" & modobj.Phone & "', Fax=N'" & modobj.Fax & "', Email=N'" & modobj.Email & "', WebPage=N'" & modobj.WebPage & "', Address=N'" & modobj.Address & "', CostCenterId=" & modobj.CostCenterId & ", Prefix=N'" & modobj.Prefix & "', CommercialInvoice=" & IIf(modobj.CommercialInvoice = True, 1, 0) & ", SalesTaxAccountId = " & modobj.SalesTaxAccountId & " WHERE CompanyID=" & modobj.CompanyID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)


            modobj.ActivityLog.ActivityName = "Update"
            modobj.ActivityLog.RecordType = "Configuration"
            modobj.ActivityLog.RefNo = modobj.CompanyID
            UtilityDAL.BuildActivityLog(modobj.ActivityLog, trans)
            trans.Commit()

            Return True

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Delete_Record(ByVal modobj As CompanyInfo) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Delete From CompanyDefTable WHERE CompanyID=" & modobj.CompanyID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            modobj.ActivityLog.ActivityName = "Delete"
            modobj.ActivityLog.RecordType = "Configuration"
            modobj.ActivityLog.RefNo = modobj.CompanyID
            UtilityDAL.BuildActivityLog(modobj.ActivityLog, trans)
            trans.Commit()
            Return True
        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function GetAllRecord() As DataTable
        Try
            Dim strSQL As String = String.Empty
            'Before comments against task:2677
            'strSQL = "select a.CompanyId,a.CompanyName,a.LegalName,a.Phone,a.Fax,a.EMail,a.Webpage,a.Address, ISNULL(a.CostCenterId,0), b.name as [Cost Center], a.Prefix From CompanyDefTAble a LEFT OUTER JOIN tblDefCostCenter b ON a.CostCenterID = b.CostCenterID "
            'Task:2677 Added Field CommercialInvoice
            'strSQL = "select a.CompanyId,a.CompanyName,a.LegalName,a.Phone,a.Fax,a.EMail,a.Webpage,a.Address, ISNULL(a.CostCenterId,0), b.name as [Cost Center], a.Prefix, IsNull(a.CommercialInvoice,0) as CommercialInvoice From CompanyDefTAble a LEFT OUTER JOIN tblDefCostCenter b ON a.CostCenterID = b.CostCenterID "
            'End Task:2677
            strSQL = "SELECT a.CompanyId, a.CompanyName, a.LegalName, a.Phone, a.Fax, a.EMail, a.webpage WebPage, a.address Address, ISNULL(a.CostCenterId, 0) AS CostCenterId, b.Name AS [Cost Center], ISNULL(a.SalesTaxAccountId,0) SalesTaxAccountId, c.detail_title AS SalesTaxAccount, a.Prefix, ISNULL(a.CommercialInvoice, 0) AS CommercialInvoice FROM CompanyDefTable AS a LEFT OUTER JOIN vwCOADetail AS c ON a.SalesTaxAccountId = c.coa_detail_id LEFT OUTER JOIN tblDefCostCenter AS b ON a.CostCenterId = b.CostCenterID"
            Dim dt As New DataTable
            Dim da As SqlClient.SqlDataAdapter
            da = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetCompCode() As DataTable
        Try
            Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
            If Not Con.State Then Con.Open()
            Dim ID As Integer = 0
            Dim strSQL As String = "Select ISNULL(Max(CompanyId),0)+1 as CompanyID From CompanyDefTable "
            Dim Dt As New DataTable
            Dim Da As SqlClient.SqlDataAdapter
            Da = New SqlClient.SqlDataAdapter(strSQL, Con)
            Da.Fill(Dt)

            If Dt.Rows.Count > 0 Then
                ID = Dt.Rows(0).Item(0).ToString()
            End If

            Return Dt
            Con.Close()
            Dt = Nothing
            Da = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function MyCompany() As List(Of CompanyInfo)
        Try
            Dim strQuery As String = "Select * From CompanyDefTable"
            Dim dr As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strQuery, Nothing)
            Dim CompanyList As New List(Of CompanyInfo)
            Dim Company As CompanyInfo
            If dr.HasRows Then
                Do While dr.Read
                    Company = New CompanyInfo
                    Company.CompanyID = dr.GetValue(0) ' CompanyId
                    Company.CompanyName = dr.GetValue(1).ToString
                    Company.LegalName = dr.GetValue(2).ToString
                    Company.Phone = dr.GetValue(3).ToString
                    Company.Fax = dr.GetValue(4).ToString
                    Company.Email = dr.GetValue(5).ToString
                    Company.WebPage = dr.GetValue(6).ToString
                    Company.Address = dr.GetValue(7).ToString
                    Company.CostCenterId = Val(dr.GetValue(8).ToString)
                    Company.Prefix = dr.GetValue(9).ToString
                    'Task:2677 Added Field Commercial Invoice
                    If Not IsDBNull(dr.GetValue(10)) Then
                        Company.CommercialInvoice = dr.GetValue(10)
                    Else
                        Company.CommercialInvoice = False
                    End If
                    'End Task:2677
                    CompanyList.Add(Company)
                Loop
            End If
            Return CompanyList
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
