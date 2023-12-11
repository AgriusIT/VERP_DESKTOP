Imports SBModel
Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class ProductAssemblyTableDAL
    Function Add(ByVal objModel As List(Of ProductAssemblyTableBE)) As Boolean
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
    Function Add(ByVal objMod As List(Of ProductAssemblyTableBE), trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            For Each objModel As ProductAssemblyTableBE In objMod
                strSQL = "insert into  ProductAssemblyTable (SalesId, LocationId, ArticleDefId, ArticleSize, Qty, Price) values (N'" & objModel.SalesId & "', N'" & objModel.LocationId & "', N'" & objModel.ArticleDefId & "', N'" & objModel.ArticleSize.Replace("'", "''") & "', N'" & objModel.Qty & "', N'" & objModel.Price & "') Select @@Identity"
                objModel.ProductAssemblyId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            Next
            'objModel.ActivityLog.ActivityName = "Save"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Update(ByVal objModel As ProductAssemblyTableBE) As Boolean
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
    Function Update(ByVal objModel As ProductAssemblyTableBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update ProductAssemblyTable set SalesId= N'" & objModel.SalesId & "', LocationId= N'" & objModel.LocationId & "', ArticleDefId= N'" & objModel.ArticleDefId & "', ArticleSize= N'" & objModel.ArticleSize.Replace("'", "''") & "', Qty= N'" & objModel.Qty & "', Price= N'" & objModel.Price & "' where ProductAssemblyId=" & objModel.ProductAssemblyId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.ActivityName = "Update"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Delete(ByVal objModel As ProductAssemblyTableBE) As Boolean
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
    Function Delete(ByVal objModel As ProductAssemblyTableBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from ProductAssemblyTable  where ProductAssemblyId= " & objModel.ProductAssemblyId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.ActivityName = "Delete"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetAll() As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ProductAssemblyId, SalesId, LocationId, ArticleDefId, ArticleSize, Qty, Price from ProductAssemblyTable  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ProductAssemblyId, SalesId, LocationId, ArticleDefId, ArticleSize, Qty, Price from ProductAssemblyTable  where ProductAssemblyId=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class



