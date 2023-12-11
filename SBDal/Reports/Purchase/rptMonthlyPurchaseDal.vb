Imports SBUtility
Imports SBModel
Imports System.Data.SqlClient
Public Class rptMonthlyPurchaseDal

    Public Function GetDataTable(ByVal ToDate As Date, Optional ByVal FormType As String = "Purchase") As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try

            Dim StrSQL As New System.Text.StringBuilder
            Dim cn As New SqlClient.SqlConnection(SQLHelper.CON_STR)
            Dim cm As New SqlClient.SqlCommand
            cm.CommandType = CommandType.Text
            Dim ParmFromDate As Date = "01-" & ToDate.ToString("MMM") & "-" & ToDate.Date.Year
            Dim ParmToDate As Date = Date.DaysInMonth(ToDate.Year, ToDate.Month) & "-" & ToDate.ToString("MMM") & "-" & ToDate.Date.Year

            If FormType = "Purchase" Then
                cm.CommandText = "select convert(char,ReceivingDate,103) as ReceivingDate,receivingNo,ReceivingQty,ReceivingAmount from ReceivingMasterTable where ReceivingDate >='" & ParmFromDate & "' and ReceivingDate <= '" & ParmToDate & "' and ReceivingNo like 'Pur%'"
            Else
                cm.CommandText = "select convert(char,DispatchDate,103) as DispatchDate,DispatchNo,DispatchQty,DispatchAmount from DispatchMasterTable where DispatchDate >='" & ParmFromDate & "' and DispatchDate <= '" & ParmToDate & "' "

            End If
            
            cm.Connection = cn
            cm.CommandTimeout = 500
            ' cm.Parameters.Add("@Date", SqlDbType.DateTime)

            'cm.Parameters.Item("@Date").Value = ToDate

            objDA = New SqlClient.SqlDataAdapter(cm)
            Dim MyCollectionList As New DataTable("GetRecords")

            objDA.Fill(MyCollectionList)

            Return MyCollectionList
        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
        End Try

    End Function


End Class
