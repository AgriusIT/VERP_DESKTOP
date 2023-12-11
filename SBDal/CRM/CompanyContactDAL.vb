Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility
Imports System.Net


Public Class CompanyContactDAL

    Public Function Add(ByVal objMod As CompanyContactBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction

        Try


            Dim strSQL As String = String.Empty
            strSQL = "INSERT INTO tblCompanyContacts(RefCompanyId, ContactName, Designation,Mobile, Phone,Fax,Email,Address,IndexNo,Type,Company,Department,NamePrefix, CompanyLocation, CompanyLocationId) " _
            & " VALUES(" & objMod.RefCompanyId & ",'" & objMod.ContactName.Replace("'", "''") & "','" & objMod.Designation.Replace("'", "''") & "','" & objMod.Mobile.Replace("'", "''") & "', '" & objMod.Phone.Replace("'", "''") & "','" & objMod.Fax.Replace("'", "''") & "','" & objMod.Email.Replace("'", "''") & "','" & objMod.Address.Replace("'", "''") & "','" & objMod.IndexNo & "','" & objMod.Type.Replace("'", "''") & "','" & objMod.Company.Replace("'", "''") & "','" & objMod.Department.Replace("'", "''") & "','" & objMod.NamePrefix.Replace("'", "''") & "', '" & objMod.CompanyLocation.Replace("'", "''") & "', " & objMod.CompanyLocationId & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)


            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()

            Throw ex
        Finally
            Con.Close()
        End Try

    End Function

    Public Function Update(ByVal objMod As CompanyContactBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction

        Try

            Dim strSQL As String = String.Empty
            strSQL = "Update tblCompanyContacts  SET RefCompanyId= " & objMod.RefCompanyId & ", ContactName='" & objMod.ContactName.Replace("'", "''") & "', Designation='" & objMod.Designation.Replace("'", "''") & "',Mobile='" & objMod.Mobile.Replace("'", "''") & "', Phone='" & objMod.Phone.Replace("'", "''") & "',Fax='" & objMod.Fax.Replace("'", "''") & "',Email='" & objMod.Email.Replace("'", "''") & "',Address='" & objMod.Address.Replace("'", "''") & "',IndexNo='" & objMod.IndexNo & "', Type='" & objMod.Type.Replace("'", "''") & "',Company='" & objMod.Company.Replace("'", "''") & "',Department='" & objMod.Department.Replace("'", "''") & "',NamePrefix='" & objMod.NamePrefix.Replace("'", "''") & "', CompanyLocation='" & objMod.CompanyLocation.Replace("'", "''") & "', CompanyLocationId='" & objMod.CompanyLocationId & "' WHERE PK_ID=" & objMod.PK_ID & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)


            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()

            Throw ex
        Finally
            Con.Close()
        End Try

    End Function

    Public Function Delete(ByVal objMod As CompanyContactBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction

        Try

            Dim strSQL As String = String.Empty
            strSQL = "Delete From tblCompanyContacts WHERE PK_ID=" & objMod.PK_ID & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)


            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()

            Throw ex
        Finally
            Con.Close()
        End Try

    End Function
    Public Function GetAllRecords() As DataTable
        Try

            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("SELECT Contcs.PK_Id, IsNull(COA.coa_detail_id,0) as RefCompanyId, COA.detail_code, COA.detail_title, Contcs.Company,Contcs.NamePrefix,Contcs.ContactName, Contcs.Designation, Contcs.Mobile, Contcs.Phone, Contcs.Fax, Contcs.Email, Contcs.Address,Contcs.IndexNo,Contcs.Type,Contcs.Department, Contcs.CompanyLocation, Contcs.CompanyLocationId FROM dbo.TblCompanyContacts AS Contcs LEFT OUTER JOIN  dbo.vwCOADetail AS COA ON Contcs.RefCompanyId = COA.coa_detail_id ")
            dt.AcceptChanges()

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
