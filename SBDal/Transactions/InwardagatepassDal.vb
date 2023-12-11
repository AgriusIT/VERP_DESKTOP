'06-07-2015 Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Data.SqlClient
Public Class InwardagatepassDal
    Public Function Add(ByVal inwardgatepass As Inwardgatepassmaster) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim _strqureey As String = "insert into Inwardgatepassmastertable (Inwardgatepassdate, InwardGatePassNo, BillNo, PartyName, Category, CityId, Drivername, VehicleNo, TotalQty, TotalAmount, Remarks, UserName, Entrydate)" _
            & " values (N'" & inwardgatepass.Inwardgatepassdate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & inwardgatepass.InwardGatePassNo & "', N'" & inwardgatepass.BillNo.ToString.Replace("'", "''") & "', N'" & inwardgatepass.PartyName.ToString.Replace("'", "''") & "', N'" & inwardgatepass.Category.ToString.Replace("'", "''") & "', N'" & inwardgatepass.CityId & "',N'" & inwardgatepass.Drivername.ToString.Replace("'", "''") & "', N'" & inwardgatepass.VehicleNo.ToString.Replace("'", "''") & "'," & inwardgatepass.TotalQty & "," & inwardgatepass.TotalAmount & ",N'" & inwardgatepass.Remarks.ToString.Replace("'", "''") & "',  N'" & inwardgatepass.UserName.ToString.Replace("'", "''") & "',N'" & inwardgatepass.Entrydate.ToString("yyyy-M-d h:mm:ss tt") & "' ) Select @@Identity"
            inwardgatepass.InwardgatepassId = SQLHelper.ExecuteScaler(trans, CommandType.Text, _strqureey)
            Call Adddetail(inwardgatepass, inwardgatepass.InwardgatepassId, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Adddetail(ByVal inwardgatepass As Inwardgatepassmaster, ByVal MasterId As Integer, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim inwardgatepasslist As List(Of Inwardgatepassdetail) = inwardgatepass.inwardgatepassdetail
            For Each inwardgatepassdetail As Inwardgatepassdetail In inwardgatepasslist
                Dim strquery As String = "insert into Inwardgatepassdetailtable (InwardgatepassId, Detail, Unit, Qty, Price ,PreviousQty)" _
                & " values (N'" & MasterId & "',N'" & inwardgatepassdetail.Detail.ToString.Replace("'", "''") & "', N'" & inwardgatepassdetail.Unit & "', N'" & inwardgatepassdetail.Qty & "', N'" & inwardgatepassdetail.Price & "'," & inwardgatepassdetail.PreviousQty & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strquery)
            Next
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function update(ByVal inwardgatepass As Inwardgatepassmaster) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        Dim trans As SqlTransaction = con.BeginTransaction
        Try
            Dim _strquery As String = "update Inwardgatepassmastertable set Inwardgatepassdate = N'" & inwardgatepass.Inwardgatepassdate.ToString("yyyy-M-d h:mm:ss tt") & "', InwardGatePassNo=N'" & inwardgatepass.InwardGatePassNo & "',  BillNo = N'" & inwardgatepass.BillNo.ToString.Replace("'", "''") & "', PartyName = N'" & inwardgatepass.PartyName.ToString.Replace("'", "''") & "', Category = N'" & inwardgatepass.Category.ToString.Replace("'", "''") & "', CityId = N'" & inwardgatepass.CityId & "', Drivername = N'" & inwardgatepass.Drivername.ToString.Replace("'", "''") & "', VehicleNo = N'" & inwardgatepass.VehicleNo.ToString.Replace("'", "''") & "', TotalQty = N'" & inwardgatepass.TotalQty & "', TotalAmount = N'" & inwardgatepass.TotalAmount & "', Remarks=N'" & inwardgatepass.Remarks.ToString.Replace("'", "''") & "', UserName = N'" & inwardgatepass.UserName.ToString.Replace("'", "''") & "', Entrydate = N'" & inwardgatepass.Entrydate.ToString("yyyy-M-d h:mm:ss tt") & "' where inwardgatepassid = N'" & inwardgatepass.InwardgatepassId & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, _strquery)
            _strquery = "delete from inwardgatepassdetailtable where inwardgatepassid = N'" & inwardgatepass.InwardgatepassId & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, _strquery)
            Call Adddetail(inwardgatepass, inwardgatepass.InwardgatepassId, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()

            Throw ex
        Finally
            con.Close()
        End Try
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Condition">I or O</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Getallrecord(Optional ByVal Condition As String = "") As DataTable
        Try
            'Marked Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            'Dim _strquery As String = "SELECT dbo.Inwardgatepassmastertable.InwardgatepassId, dbo.Inwardgatepassmastertable.Inwardgatepassdate,dbo.Inwardgatepassmastertable.InwardgatepassNo, dbo.Inwardgatepassmastertable.BillNo, " _
            '        & "  dbo.Inwardgatepassmastertable.PartyName, dbo.Inwardgatepassmastertable.Category, dbo.tblListCity.CityName,  " _
            '          & " dbo.Inwardgatepassmastertable.DriverName, dbo.Inwardgatepassmastertable.VehicleNo,   " _
            '           & "  dbo.Inwardgatepassmastertable.CityId,Inwardgatepassmastertable.Remarks, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status] " _
            '          & " FROM dbo.Inwardgatepassmastertable LEFT OUTER JOIN " _
            '        & "   dbo.tblListCity  ON dbo.tblListCity.CityId = dbo.Inwardgatepassmastertable.CityId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = InwardGatePassMasterTable.InwardGatePassNo  ORDER BY 1 DESC"
            'Marked Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            'Altered Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            Dim _strquery As String = "SELECT dbo.Inwardgatepassmastertable.InwardgatepassId, dbo.Inwardgatepassmastertable.Inwardgatepassdate,dbo.Inwardgatepassmastertable.InwardgatepassNo, dbo.Inwardgatepassmastertable.BillNo, " _
                    & "  dbo.Inwardgatepassmastertable.PartyName, dbo.Inwardgatepassmastertable.Category, dbo.tblListCity.CityName,  " _
                      & " dbo.Inwardgatepassmastertable.DriverName, dbo.Inwardgatepassmastertable.VehicleNo,   " _
                       & "  dbo.Inwardgatepassmastertable.CityId,Inwardgatepassmastertable.Remarks, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status],Inwardgatepassmastertable.UserName as 'User Name' " _
                      & " FROM dbo.Inwardgatepassmastertable LEFT OUTER JOIN " _
                    & "   dbo.tblListCity  ON dbo.tblListCity.CityId = dbo.Inwardgatepassmastertable.CityId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = InwardGatePassMasterTable.InwardGatePassNo  ORDER BY 1 DESC"
            'Altered Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms

            '// R@! Shahid: 03-Sep-2015 to implement filter of inward and outward
            If Condition = "I" Then
                _strquery = "SELECT dbo.Inwardgatepassmastertable.InwardgatepassId, dbo.Inwardgatepassmastertable.Inwardgatepassdate,dbo.Inwardgatepassmastertable.InwardgatepassNo, dbo.Inwardgatepassmastertable.BillNo, " _
                            & "  dbo.Inwardgatepassmastertable.PartyName, dbo.Inwardgatepassmastertable.Category, dbo.tblListCity.CityName,  " _
                            & " dbo.Inwardgatepassmastertable.DriverName, dbo.Inwardgatepassmastertable.VehicleNo,   " _
                            & "  dbo.Inwardgatepassmastertable.CityId,Inwardgatepassmastertable.Remarks, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status],Inwardgatepassmastertable.UserName as 'User Name' " _
                            & " FROM dbo.Inwardgatepassmastertable LEFT OUTER JOIN " _
                            & "   dbo.tblListCity  ON dbo.tblListCity.CityId = dbo.Inwardgatepassmastertable.CityId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = InwardGatePassMasterTable.InwardGatePassNo " _
                            & " Where Substring (Inwardgatepassmastertable.InwardgatepassNo,1,1)='I' ORDER BY 1 DESC"

            ElseIf Condition = "O" Then

                _strquery = "SELECT dbo.Inwardgatepassmastertable.InwardgatepassId, dbo.Inwardgatepassmastertable.Inwardgatepassdate,dbo.Inwardgatepassmastertable.InwardgatepassNo, dbo.Inwardgatepassmastertable.BillNo, " _
                            & "  dbo.Inwardgatepassmastertable.PartyName, dbo.Inwardgatepassmastertable.Category, dbo.tblListCity.CityName,  " _
                            & " dbo.Inwardgatepassmastertable.DriverName, dbo.Inwardgatepassmastertable.VehicleNo,   " _
                            & "  dbo.Inwardgatepassmastertable.CityId,Inwardgatepassmastertable.Remarks, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status],Inwardgatepassmastertable.UserName as 'User Name' " _
                            & " FROM dbo.Inwardgatepassmastertable LEFT OUTER JOIN " _
                            & "   dbo.tblListCity  ON dbo.tblListCity.CityId = dbo.Inwardgatepassmastertable.CityId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = InwardGatePassMasterTable.InwardGatePassNo  " _
                            & " Where Substring (Inwardgatepassmastertable.InwardgatepassNo,1,1)='O' ORDER BY 1 DESC"
            End If

            Dim dt As DataTable = UtilityDAL.GetDataTable(_strquery)
            Return dt

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function DetailRecord(ByVal MasterId As Integer) As DataTable
        Try
            Dim str As String = "Select InwardgatepassId, Detail,Unit, PreviousQty, Qty, Price From Inwardgatepassdetailtable WHERE InwardgatepassId=" & MasterId
            Dim dt As DataTable = UtilityDAL.GetDataTable(str)
            dt.AcceptChanges()
            dt.Columns.Add("Amount", GetType(System.Double))
            dt.Columns("Amount").Expression = "Qty*Price"
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Delete(ByVal InwardGatepass As Inwardgatepassmaster) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then
            con.Open()

        End If
        Dim trans As SqlTransaction = con.BeginTransaction
        Try

            Dim _strQuery As String
            _strQuery = "delete from inwardgatepassdetailtable where inwardgatepassid = N'" & InwardGatepass.InwardgatepassId & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, _strQuery)

            _strQuery = "delete from inwardgatepassMastertable where inwardgatepassid = N'" & InwardGatepass.InwardgatepassId & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, _strQuery)
            trans.Commit()

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetUnit() As DataTable
        Try
            Return UtilityDAL.GetDataTable("Select DISTINCT Unit, Unit From inwardgatepassdetailtable WHERE Unit is not Null")

        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
