Imports SBModel
Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class PropertyProfileDAL

    Dim VoucherId As Integer = 0
    Function Add(ByVal objModel As PropertyProfileBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Add(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Add(ByVal objModel As PropertyProfileBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "insert into  PropertyProfile (DocNo, DocDate, InvId, BranchId, Status, CostCenterId, UserName, CommissionAmount, Margin) values (N'" & objModel.DocNo.Replace("'", "''") & "', N'" & objModel.DocDate.ToString("yyyy-MM-dd hh:mm:ss") & "', N'" & objModel.InvId & "', N'" & objModel.Branch.BranchId & "', N'" & objModel.Status.Replace("'", "''") & "', N'" & objModel.CostCenterId & "', N'" & LoginUser.LoginUserName & "', N'" & objModel.CommissionAmount & "', N'" & objModel.Margin & "') Select @@Identity"
            objModel.PropertyProfileId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)

            Dim Str As String = String.Empty
            Str = "INSERT INTO tblDefCostCenter(Name,Code,CostCenterGroup, Active) VALUES(N'" & objModel.InvName & "-" & objModel.DocNo.Replace("'", "''") & "',N'" & objModel.DocNo.Replace("'", "''") & "','" & objModel.Branch.Name & "', 'True')Select @@Identity"
            objModel.CostCenterId = SQLHelper.ExecuteScaler(trans, CommandType.Text, Str)
            Str = String.Empty
            Str = "Update PropertyProfile Set CostCenterId=" & objModel.CostCenterId & " WHERE PropertyProfileId=" & objModel.PropertyProfileId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, Str)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.ActivityName = "Save"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Update(ByVal objModel As PropertyProfileBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Update(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Update(ByVal objModel As PropertyProfileBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update PropertyProfile set DocNo= N'" & objModel.DocNo.Replace("'", "''") & "', DocDate= N'" & objModel.DocDate.ToString("yyyy-MM-dd hh:mm:ss") & "', InvId= N'" & objModel.InvId & "', BranchId= N'" & objModel.Branch.BranchId & "', Status= N'" & objModel.Status.Replace("'", "''") & "', CostCenterId= N'" & objModel.CostCenterId & "', CommissionAmount= N'" & objModel.CommissionAmount & "', Margin= N'" & objModel.Margin & "', ModifiedUserName= N'" & objModel.ModifiedUser & "', ModifiedDate= N'" & objModel.ModifiedDate.ToString("yyyy-MM-dd hh:mm:ss") & "' where PropertyProfileId=" & objModel.PropertyProfileId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.ActivityName = "Update"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Function Delete(ByVal objModel As PropertyProfileBE) As Boolean
    '    Dim Con As New SqlConnection(SQLHelper.CON_STR)
    '    If Con.State = ConnectionState.Closed Then Con.Open()
    '    Dim trans As SqlTransaction = Con.BeginTransaction()
    '    Try
    '        Delete(objModel, trans)
    '        trans.Commit()
    '        Return True
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    Function Delete(ByVal objModel As PropertyProfileBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Dim PropertyNo As String = String.Empty
        Try

            strSQL = String.Empty
            Dim dtPropertyNo As DataTable = UtilityDAL.GetDataTable("SELECT DocNo AS PropertyNo from PropertyProfile WHERE PropertyProfileId = " & objModel.PropertyProfileId & "", trans)
            If dtPropertyNo.Rows.Count > 0 Then
                PropertyNo = dtPropertyNo.Rows(0).Item(0).ToString
            End If

            ''Voucher deletion
            ''PropertyPurchase
            strSQL = "Delete from tblVoucherDetail WHERE voucher_id IN ( SELECT voucher_id FROM tblVoucher WHERE voucher_no IN (SELECT VoucherNo FROM PropertyPurchase WHERE SerialNo = '" & PropertyNo & "'))"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = " DELETE FROM tblVoucher WHERE voucher_no IN (SELECT VoucherNo FROM PropertyPurchase WHERE SerialNo = '" & PropertyNo & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            ''PropertySales
            strSQL = "DELETE from tblVoucherDetail WHERE voucher_id IN ( SELECT voucher_id FROM tblVoucher WHERE voucher_no IN (SELECT VoucherNo FROM PropertySales WHERE SerialNo = '" & PropertyNo & "'))"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = " DELETE FROM tblVoucher WHERE voucher_no IN (SELECT VoucherNo FROM PropertySales WHERE SerialNo = '" & PropertyNo & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            ''TASK TFS4412
            strSQL = "DELETE from tblVoucherDetail WHERE voucher_id IN ( SELECT voucher_id FROM tblVoucher WHERE voucher_no IN (SELECT AdjustedVoucherNo FROM PropertySales WHERE SerialNo = '" & PropertyNo & "'))"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = " DELETE FROM tblVoucher WHERE voucher_no IN (SELECT AdjustedVoucherNo FROM PropertySales WHERE SerialNo = '" & PropertyNo & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            ''END TASK TFS4412
            ''ProfitSharingMaster
            strSQL = "DELETE from tblVoucherDetail WHERE voucher_id IN (SELECT voucher_id FROM tblVoucher WHERE voucher_no IN (SELECT voucher_no FROM ProfitSharingMaster WHERE PropertyProfileId = " & objModel.PropertyProfileId & "))"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = " DELETE FROM tblVoucher WHERE voucher_no IN (SELECT voucher_no FROM ProfitSharingMaster WHERE PropertyProfileId = " & objModel.PropertyProfileId & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            ''InvestmentBooking
            strSQL = "DELETE from tblVoucherDetail WHERE voucher_id IN (SELECT voucher_id FROM tblVoucher WHERE voucher_no IN (SELECT VoucherNo FROM InvestmentBooking WHERE PropertyProfileId = " & objModel.PropertyProfileId & "))"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = " DELETE FROM tblVoucher WHERE voucher_no IN (SELECT VoucherNo FROM InvestmentBooking WHERE PropertyProfileId = " & objModel.PropertyProfileId & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            ''PropertyProfileAgent
            strSQL = "DELETE from tblVoucherDetail WHERE voucher_id IN (SELECT voucher_id FROM tblVoucher WHERE voucher_no IN (SELECT VoucherNo FROM PropertyProfileAgent WHERE PropertyProfileId = " & objModel.PropertyProfileId & "))"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = " DELETE FROM tblVoucher WHERE voucher_no IN (SELECT VoucherNo FROM PropertyProfileAgent WHERE PropertyProfileId = " & objModel.PropertyProfileId & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            ''PropertyProfileDealer
            strSQL = "DELETE from tblVoucherDetail WHERE voucher_id IN (SELECT voucher_id FROM tblVoucher WHERE voucher_no IN (SELECT VoucherNo FROM PropertyProfileDealer WHERE PropertyProfileId = " & objModel.PropertyProfileId & "))"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = " DELETE FROM tblVoucher WHERE voucher_no IN (SELECT VoucherNo FROM PropertyProfileDealer WHERE PropertyProfileId = " & objModel.PropertyProfileId & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            ''End voucher deletion

            strSQL = "Delete from PropertyPurchase WHERE SerialNo = '" & PropertyNo & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = String.Empty
            strSQL = "Delete from PropertySales WHERE SerialNo = '" & PropertyNo & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'ProfitSharingMaster
            strSQL = "Delete from ProfitSharingDetail WHERE ProfitSharingId IN (SELECT ProfitSharingId From ProfitSharingMaster WHERE PropertyProfileId = " & objModel.PropertyProfileId & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = "Delete from ProfitSharingMaster WHERE PropertyProfileId= " & objModel.PropertyProfileId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'InvestmentBooking
            strSQL = "Delete from InvestmentBooking WHERE PropertyProfileId= " & objModel.PropertyProfileId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'PropertyProfileAgent
            strSQL = "Delete from PropertyProfileAgent WHERE PropertyProfileId= " & objModel.PropertyProfileId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            'PropertyProfileDealer
            strSQL = "Delete from PropertyProfileDealer WHERE PropertyProfileId= " & objModel.PropertyProfileId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            strSQL = "Delete from PropertyProfile  WHERE PropertyProfileId= " & objModel.PropertyProfileId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)



            'Dim Voucher As New VouchersMaster()
            'Voucher.VNo = PropertyNo
            'Voucher.VoucherNo = PropertyNo
            'Voucher.ActivityLog = New ActivityLog()
            'Call New VouchersDAL().Delete(Voucher, trans)
            'objModel.ActivityLog.ActivityName = "Delete"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetAll() As DataTable
        Dim strSQL As String = String.Empty
        Try
            'strSQL = " Select PropertyProfileId, DocNo, DocDate, InvId, BranchId, Status, CostCenterId from PropertyProfile  "
            strSQL = " SELECT      PropertyProfile.PropertyProfileId, PropertyProfile.InvId, PropertyProfile.DocNo, PropertyProfile.DocDate,  PropertyProfile.BranchId, PropertyProfile.Status, PropertyProfile.CostCenterId ," _
                     & " PropertyItem.PropertyItemId, PropertyItem.Title, PropertyType.PropertyType, PropertyItem.PlotNo, PropertyItem.Sector, PropertyItem.Block, PropertyItem.PlotSize, tblListTerritory.TerritoryName,tblListCity.CityName, iif(PropertyProfile.PropertyProfileId in (select PropertyProfileId from ProfitSharingMaster), 1, 0) as ProfitShare, IsNull(PropertyProfile.IsDealCompleted, 0) AS IsDealCompleted, PropertyProfile.UserName, PropertyProfile.ModifiedUserName, PropertyProfile.ModifiedDate " _
                     & " FROM tblListTerritory INNER JOIN" _
                      & " tblListCity ON tblListTerritory.CityId = tblListCity.CityId INNER JOIN " _
                      & " PropertyItem ON tblListTerritory.TerritoryId = PropertyItem.TerritoryId RIGHT OUTER JOIN " _
                      & " PropertyProfile ON PropertyItem.PropertyItemId = PropertyProfile.InvId LEFT OUTER JOIN " _
                      & " PropertyType ON PropertyItem.PropertyTypeId = PropertyType.PropertyTypeId " & IIf(LoginUser.LoginUserGroup = "Administrator", "", " WHERE PropertyProfile.UserName = '" & LoginUser.LoginUserName & "'") & ""
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select PropertyProfileId, DocNo, DocDate, InvId, BranchId, Status, CostCenterId from PropertyProfile  where PropertyProfileId=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Delete(ByVal objModel As PropertyProfileBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Delete(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
