Imports SBUtility
Imports SBModel
Imports System.Data.SqlClient
Public Class rptTrialDal

    Public Function GetDataTable(ByVal ToDate As Date, Optional ByVal Level As Integer = 3) As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try

            Dim StrSQL As New System.Text.StringBuilder
            Dim cn As New SqlClient.SqlConnection(SQLHelper.CON_STR)
            Dim cm As New SqlClient.SqlCommand
            cm.CommandType = CommandType.StoredProcedure

            Dim ParmFromDate As Date = "01-" & ToDate.ToString("MMM") & "-" & ToDate.Date.Year
            Dim ParmToDate As Date = Date.DaysInMonth(ToDate.Year, ToDate.Month) & "-" & ToDate.ToString("MMM") & "-" & ToDate.Date.Year

            cm.CommandText = "sp_rpt_trial"

            cm.Connection = cn
            cm.CommandTimeout = 500
            cm.Parameters.Add("@FromDate", SqlDbType.DateTime)
            cm.Parameters.Add("@ToDate", SqlDbType.DateTime)

            cm.Parameters.Item("@FromDate").Value = ParmFromDate
            cm.Parameters.Item("@ToDate").Value = ParmToDate

            objDA = New SqlClient.SqlDataAdapter(cm)
            Dim MyCollectionList As New DataTable("GetRecords")

            objDA.Fill(MyCollectionList)
            Dim DV As DataView = MyCollectionList.DefaultView
            DV.Sort = "detail_code desc"

            Dim MyCollectionList1 As New DataTable("ModifiedRecords")
            MyCollectionList1 = ModifyDataTable(DV.ToTable, Level).Copy
            Return MyCollectionList1

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
        End Try

    End Function

    Public Function ModifyDataTable(ByVal CurrentDT As DataTable, Optional ByVal Level As Integer = 4) As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try

            Dim StrSQL As New System.Text.StringBuilder
            Dim cn As New SqlClient.SqlConnection(SQLHelper.CON_STR)
            Dim cm As New SqlClient.SqlCommand
            Dim StrColName As String = String.Empty
            If Level = 1 Then
                StrColName = "Main"
            ElseIf Level = 2 Then
                StrColName = "sub"
            ElseIf Level = 3 Then
                StrColName = "sub_sub"
            ElseIf Level = 4 Then
                StrColName = "Detail"
            End If

            If Not CurrentDT Is Nothing Then
                If Not CurrentDT.Rows.Count = 0 Then
                    Dim objDtTrial As New DataTable
                    'Adding Columns
                    '//Type Column
                    Dim dc As DataColumn
                    dc = New DataColumn("Type")
                    dc.Caption = "Type"
                    dc.DataType = GetType(String)
                    objDtTrial.Columns.Add(dc)

                    '//Code Column
                    Dim dc1 As DataColumn
                    dc1 = New DataColumn("Code")
                    dc1.Caption = "Code"
                    dc1.DataType = GetType(String)
                    objDtTrial.Columns.Add(dc1)

                    '//Account Column
                    Dim dc2 As DataColumn
                    dc2 = New DataColumn("Account")
                    dc2.Caption = "Account"
                    dc2.DataType = GetType(String)
                    objDtTrial.Columns.Add(dc2)

                    '//Opening Column
                    Dim dc3 As DataColumn
                    dc3 = New DataColumn("Opening")
                    dc3.Caption = "Opening"
                    dc3.DataType = GetType(Double)
                    objDtTrial.Columns.Add(dc3)

                    '//Opening Column
                    Dim dc4 As DataColumn
                    dc4 = New DataColumn("Debit")
                    dc4.Caption = "Debit"
                    dc4.DataType = GetType(Double)
                    objDtTrial.Columns.Add(dc4)

                    '//Opening Column
                    Dim dc5 As DataColumn
                    dc5 = New DataColumn("Credit")
                    dc5.Caption = "Credit"
                    dc5.DataType = GetType(Double)
                    objDtTrial.Columns.Add(dc5)

                    '//Opening Column
                    Dim dc6 As DataColumn
                    dc6 = New DataColumn("Closing")
                    dc6.Caption = "Opening"
                    dc6.DataType = GetType(Double)
                    objDtTrial.Columns.Add(dc6)

                    'For Each r As DataRow In MyCollectionList.Rows
                    '    If Not objDtTrial.Columns.Contains(r.Item("ArticleGroupName")) Then
                    '        objDtTrial.Columns.Add(New DataColumn(r.Item("ArticleGroupName"), GetType(System.Double)))
                    '    End If
                    'Next

                    'dc = New DataColumn("Total Sale", GetType(System.Double))
                    'objDtTrial.Columns.Add(dc)

                    'Dim dtp As Date = Date.MinValue
                    Dim dr As DataRow
                    Dim StrAccount As String = String.Empty
                    For Each r As DataRow In CurrentDT.Rows
                        If Not objDtTrial.Rows.Count > 0 AndAlso StrAccount = String.Empty Then
                            dr = objDtTrial.NewRow
                        ElseIf Not StrAccount = r.Item(StrColName & "_code") Then
                            'objDtTrial.Rows.InsertAt(dr, 0)
                            dr = objDtTrial.NewRow
                            objDtTrial.Rows.InsertAt(dr, 0)
                            'If r.Item("SalesDate") = dtp Then
                            '    dr.Item(0) = r.Item("SalesDate").ToString
                            '    dr.Item(r.Item("ArticleGroupName")) = r.Item("Col")
                            '    dtp = r.Item("SalesDate")
                            '    dr.Item("Total Sale") = dr.Item("Total Sale") + r.Item("Col")
                            'Else
                        End If
                        dr = objDtTrial.NewRow
                        StrAccount = r.Item(StrColName & "_code")
                        dr.Item("Code") = r.Item(StrColName & "_code")
                        dr.Item("Account") = r.Item(StrColName & "_Title")
                        dr.Item("Opening") = Val(dr.Item("Opening").ToString) + Val(r.Item("opening"))
                        dr.Item("Debit") = Val(dr.Item("Debit").ToString) + Val(r.Item("debit_amount"))
                        dr.Item("Credit") = Val(dr.Item("Credit").ToString) + Val(r.Item("credit_amount"))
                        dr.Item("Closing") = Val(dr.Item("Closing").ToString) + Val(r.Item("balance"))
                        dr.Item("Type") = r.Item("main_type")
                    Next
                    Return objDtTrial
                Else
                    Return CurrentDT
                End If
            Else
                Return CurrentDT
            End If

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
        End Try

    End Function

End Class
