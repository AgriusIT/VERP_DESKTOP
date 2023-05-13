Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility
Imports System.Net
Imports System.Drawing
Imports System.IO
Public Class CostCentreReshuffleDAL
    Public Function Add(ByVal obj As CostCentreReshuffle) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = " IF NOT EXISTS(Select CostCentreReshuffleId FROM CostCentreReshuffle Where CostCentreReshuffleId =" & obj.CostCentreReshuffleId & ") INSERT INTO CostCentreReshuffle(CostCentreFrom, CostCentreTo, Dated, UserName) " _
            & " VALUES(" & obj.CostCentreFrom & ", " & obj.CostCentreTo & ", '" & obj.Dated.ToString("yyyy-M-dd hh:mm:ss tt") & "', N'" & obj.UserName.Replace("'", "''") & "') " _
            & " Else Update CostCentreReshuffle Set CostCentreFrom = " & obj.CostCentreFrom & ", CostCentreTo =  " & obj.CostCentreTo & ", Dated = '" & obj.Dated.ToString("yyyy-M-dd hh:mm:ss tt") & "', UserName= N'" & obj.UserName.Replace("'", "''") & "' Where CostCentreReshuffleId = " & obj.CostCentreReshuffleId & " Select @@IDENTITY "
            Dim CostCentreReshuffleId As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            If CostCentreReshuffleId = 0 Then
                CostCentreReshuffleId = obj.CostCentreReshuffleId
                UpdateVoucher(obj.CostCentreFrom, obj.CostCentreTo, CostCentreReshuffleId, trans)
            Else
                SaveVoucher(obj.CostCentreFrom, obj.CostCentreTo, CostCentreReshuffleId, trans)
            End If
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function UpdateVoucher(ByVal CostCentreFrom As Integer, ByVal CostCentreTo As Integer, ByVal CostCentreReshuffleId As Integer, ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = " Update tblVoucherDetail Set CostCenterID = " & CostCentreTo & ", CostCentreReshuffleId =  " & CostCentreReshuffleId & ", OldCostCenterID =  " & CostCentreFrom & " Where  CostCenterID = " & CostCentreFrom & " And CostCentreReshuffleId = " & CostCentreReshuffleId & ""
            Dim _costCentreReshuffleId As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            'trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function SaveVoucher(ByVal CostCentreFrom As Integer, ByVal CostCentreTo As Integer, ByVal CostCentreReshuffleId As Integer, ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = " Update tblVoucherDetail Set CostCenterID = " & CostCentreTo & ", CostCentreReshuffleId =  " & CostCentreReshuffleId & ", OldCostCenterID =  " & CostCentreFrom & " Where  CostCenterID = " & CostCentreFrom & ""
            Dim _costCentreReshuffleId As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            'trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Delete(ByVal CostCentreReshuffleId As Integer, ByVal CostCentreFrom As Integer, ByVal CostCentreTo As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = " Delete FROM CostCentreReshuffle Where CostCentreReshuffleId =" & CostCentreReshuffleId & " "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            SetCostCentreNull(CostCentreFrom, CostCentreTo, CostCentreReshuffleId, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function SetCostCentreNull(ByVal CostCentreFrom As Integer, ByVal CostCentreTo As Integer, ByVal CostCentreReshuffleId As Integer, ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = " Update tblVoucherDetail Set CostCentreReshuffleId = " & "NULL" & ", CostCenterID = " & CostCentreFrom & ", OldCostCenterID = " & "NULL" & " Where  CostCentreReshuffleId = " & CostCentreReshuffleId & " And CostCenterID = " & CostCentreTo & ""
            Dim _costCentreReshuffleId As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            'trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetAll()
        Dim Str As String = ""
        Try
            Str = " Select Reshuffle.CostCentreReshuffleId, Reshuffle.CostCentreFrom, Reshuffle.CostCentreTo, Reshuffle.Dated, A.Name As CCFrom, B.Name As CCTo, Reshuffle.UserName From CostCentreReshuffle As Reshuffle INNER JOIN tblDefCostCenter As A ON Reshuffle.CostCentreFrom =  A.CostCenterID INNER JOIN tblDefCostCenter B ON Reshuffle.CostCentreTo = B.CostCenterID"
            Dim dt As DataTable = UtilityDAL.GetDataTable(Str)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
