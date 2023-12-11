Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility
Imports System.Net
Public Class PurchaseInquiryVendorsDAL
    Public Function Add(ByVal obj As PurchaseInquiryMaster, ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Try
            For Each objMod As PurchaseInquiryVendors In obj.VendorsList
                Dim strSQL As String = String.Empty
                strSQL = "INSERT INTO PurchaseInquiryVendors(PurchaseInquiryId, VendorId, Email) " _
                & " VALUES(" & obj.PurchaseInquiryId & ", " & objMod.VendorId & ", '" & objMod.Email.Replace("'", "''") & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Next
            'trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function Update(ByVal objModList As List(Of PurchaseInquiryVendors), ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction

        Try
            For Each objMod As PurchaseInquiryVendors In objModList
                Dim strSQL As String = String.Empty
                strSQL = "Update PurchaseInquiryVendors  SET PurchaseInquiryId= " & objMod.PurchaseInquiryId & ", VendorId = '" & objMod.VendorId & "', Email='" & objMod.Email.Replace("'", "''") & "' WHERE SalesInquiryDetailId=" & objMod.PurchaseInquiryVendorsId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Next

            'trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()

            Throw ex
        Finally
            Con.Close()
        End Try

    End Function

    Public Function Delete(ByVal ID As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Delete From PurchaseInquiryVendors WHERE PurchaseInquiryVendorsId=" & ID & ""
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
    Public Function DeleteSIIWise(ByVal PurchaseInquiryId As Integer, ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Delete From PurchaseInquiryVendors WHERE PurchaseInquiryId=" & PurchaseInquiryId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetVendors(ByVal PurchaseInquiryId As Integer) As DataTable
        Try
            'PurchaseInquiryVendorsId()
            'PurchaseInquiryId()
            'VendorId()
            'Email()

            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("SELECT PurchaseInquiryVendors.PurchaseInquiryVendorsId, PurchaseInquiryVendors.PurchaseInquiryId, PurchaseInquiryVendors.VendorId, vwCOADetail.detail_title As VendorName, Case When PurchaseInquiryVendors.Email <> '' Then PurchaseInquiryVendors.Email Else vwCOADetail.Contact_Email End As Email FROM PurchaseInquiryVendors LEFT JOIN vwCOADetail ON PurchaseInquiryVendors.VendorId = vwCOADetail.coa_detail_id Where PurchaseInquiryId =" & PurchaseInquiryId & "")
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
