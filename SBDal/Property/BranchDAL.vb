Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBModel
Imports SBDAL
Imports SBDAL.SqlHelper
Public Class BranchDAL

    Const _New As String = "New"
    Const _Update As String = "Update"
    Const _Delete As String = "Delete"

    Public Shared Function Save(ByVal Obj As BEBranch) As Boolean
        Dim Conn As New SqlConnection(CON_STR)
        Dim trans As SqlTransaction
        Try
            '       Public Property BranchId As Integer
            'Public Property Name As String
            'Public Property AreaId As Integer
            'Public Property Employee_ID As Integer
            'Public Property AddressLine1 As String
            'Public Property AddressLine2 As String
            'Public Property LandlinePhone As String
            'Public Property CellPhone As String
            'Public Property Remarks As String
            'Public Property CityId As Integer
            'Public Property Active As Boolean

            If Conn.State = ConnectionState.Closed Then Conn.Open()
            trans = Conn.BeginTransaction
            Dim str As String = String.Empty
            str = " INSERT INTO Branch(Name, AreaId, Employee_ID, AddressLine1, AddressLine2, LandlinePhone, CellPhone, Remarks, CityId,  Active) " _
                 & " Values (N'" & Obj.Name & "', " & Obj.AreaId & ", " & Obj.Employee_ID & ", N'" & Obj.AddressLine1 & "' , N'" & Obj.AddressLine2 & "' , " _
                 & " N'" & Obj.LandlinePhone & "', N'" & Obj.CellPhone & "',  N'" & Obj.Remarks & "', " & Obj.CityId & " ," & IIf(Obj.Active = True, 1, 0) & ")"
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
    Public Shared Function Update(ByVal Obj As BEBranch) As Boolean
        Dim Conn As New SqlConnection(CON_STR)
        Dim trans As SqlTransaction
        Try
            If Conn.State = ConnectionState.Closed Then Conn.Open()
            trans = Conn.BeginTransaction
            Dim str As String = String.Empty
            str = " Update Branch SET Name = N'" & Obj.Name & "' , AreaId = " & Obj.AreaId & ", Employee_ID = " & Obj.Employee_ID & ", AddressLine1 = N'" & Obj.AddressLine1 & "' " _
                & " , AddressLine2 = N'" & Obj.AddressLine2 & "', LandlinePhone = N'" & Obj.LandlinePhone & "', CellPhone = N'" & Obj.CellPhone & "', CityId = " & Obj.CityId & ", Remarks = N'" & Obj.Remarks & "',  Active = " & IIf(Obj.Active = True, 1, 0) & " " _
                & " WHERE BranchId = " & Obj.BranchId & ""
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

    Public Shared Function Delete(ByVal BranchId As Integer) As Boolean
        Dim str As String = String.Empty
        Dim Conn As New SqlConnection(CON_STR)
        Dim trans As SqlTransaction
        Try
            If Conn.State = ConnectionState.Closed Then Conn.Open()
            trans = Conn.BeginTransaction
            str = "DELETE FROM Branch WHERE BranchId = " & BranchId
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

    Public Shared Function GetAgent(ByVal BranchId As Integer) As DataTable
        Try
            Dim str As String = String.Empty
            str = "SELECT BranchId, Name, AreaId, Employee_ID, AddressLine1, AddressLine2, LandlinePhone, CellPhone, Remarks, " _
                & " CityId, Email,  Active WHERE BranchId = " & BranchId & ""
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetAll() As DataTable
        Try
            Dim str As String = String.Empty
            str = "SELECT Branch.BranchId, Branch.Name, Branch.Employee_ID, Employee.Employee_Name AS Employee, Branch.CellPhone, Branch.LandlinePhone, Branch.CityId, City.CityName AS City, " _
                & " Branch.AreaId, Territory.TerritoryName AS Area, Branch.AddressLine1, Branch.AddressLine2, Branch.Remarks, " _
                & " Branch.Active FROM Branch LEFT OUTER JOIN tblListTerritory AS Territory ON Branch.AreaId = Territory.TerritoryId " _
                & " LEFT OUTER JOIN tblListCity as City ON Branch.CityId = City.CityId LEFT OUTER JOIN tblDefEmployee AS Employee ON Branch.Employee_Id = Employee.Employee_ID "
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

