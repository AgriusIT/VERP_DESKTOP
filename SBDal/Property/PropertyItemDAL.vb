Imports SBModel
Imports System
Imports System.Data.SqlClient
Public Class PropertyItemDAL
    Function Add(ByVal objModel As PropertyItemBE) As Boolean
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
    Function Add(ByVal objModel As PropertyItemBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "INSERT INTO PropertyItem (Title, EntryDate, PlotNo, Sector, Block, PropertyTypeId, PlotSize, TerritoryId, Ownership, DemandAmount, Remarks, Active, Features ,SourceMobileNo) values (N'" & objModel.Title.Replace("'", "''") & "', '" & objModel.EntryDate.ToString("yyyy-MM-dd hh:mm:ss") & "', N'" & objModel.PlotNo.Replace("'", "''") & "', N'" & objModel.Sector.Replace("'", "''") & "', N'" & objModel.Block.Replace("'", "''") & "', " & objModel.PropertyTypeId & ", N'" & objModel.PlotSize.Replace("'", "''") & "', " & objModel.TerritoryId & ", N'" & objModel.Ownership.Replace("'", "''") & "', N'" & objModel.DemandAmount & "', N'" & objModel.Remarks.Replace("'", "''") & "', " & IIf(objModel.Active = True, 1, 0) & ", N'" & objModel.Features & "',N'" & objModel.SourceMobileNo.Replace("'", "''") & "') Select @@Identity"
            objModel.PropertyItemId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.FormCaption = "Property Item"
            objModel.ActivityLog.ActivityName = "Save"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = objModel.Title.ToString
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Update(ByVal objModel As PropertyItemBE) As Boolean
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
    Function Update(ByVal objModel As PropertyItemBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update PropertyItem set Title= N'" & objModel.Title.Replace("'", "''") & "', EntryDate= '" & objModel.EntryDate.ToString("yyyy-MM-dd hh:mm:ss") & "', PlotNo= N'" & objModel.PlotNo.Replace("'", "''") & "', Sector= N'" & objModel.Sector.Replace("'", "''") & "', Block= N'" & objModel.Block.Replace("'", "''") & "', PropertyTypeId= " & objModel.PropertyTypeId & ", PlotSize= N'" & objModel.PlotSize.Replace("'", "''") & "', TerritoryId= " & objModel.TerritoryId & ", Ownership= N'" & objModel.Ownership.Replace("'", "''") & "', DemandAmount= N'" & objModel.DemandAmount & "', Remarks= N'" & objModel.Remarks.Replace("'", "''") & "', Active= " & IIf(objModel.Active = True, 1, 0) & ", Features= N'" & objModel.Features & "', SourceMobileNo = N'" & objModel.SourceMobileNo.Replace("'", "''") & "' where PropertyItemId=" & objModel.PropertyItemId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.FormCaption = "Property Item"
            objModel.ActivityLog.ActivityName = "Update"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = objModel.Title.ToString
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Delete(ByVal PropertyItemId As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Delete(PropertyItemId, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Delete(ByVal PropertyItemId As Integer, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from PropertyItem  where PropertyItemId= " & PropertyItemId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetAll() As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = "SELECT PropertyItem.PropertyItemId, PropertyItem.Title, PropertyItem.EntryDate, PropertyItem.PlotNo, PropertyItem.Sector, PropertyItem.Block, PropertyItem.PropertyTypeId, PropertyItem.Features, PropertyItem.SourceMobileNo ,PropertyType.PropertyType, PropertyItem.PlotSize, PropertyItem.TerritoryId, tblListTerritory.TerritoryName, PropertyItem.Ownership, PropertyItem.DemandAmount, PropertyItem.Remarks, PropertyItem.Active FROM PropertyItem LEFT OUTER JOIN PropertyType ON PropertyItem.PropertyTypeId = PropertyType.PropertyTypeId LEFT OUTER JOIN tblListTerritory ON PropertyItem.TerritoryId = tblListTerritory.TerritoryId WHERE PropertyItem.Active = 1 ORDER BY PropertyItem.PropertyItemId DESC"
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select PropertyItemId, Title, EntryDate, PlotNo, Sector, Block, PropertyTypeId, PlotSize, TerritoryId, Ownership, DemandAmount, Remarks, Active, Features , SourceMobileNo from PropertyItem  where PropertyItemId=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class