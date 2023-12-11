Public Class frmRptDSRStatement
    Private DocDate As DateTime
    Private SalePersonId As Integer = 0I
    Private Sub frmRptDSRStatement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.DateTimePicker1.Value = Date.Now
            FillDropDown(Me.cmbSalesman, "Select Employee_Id, Employee_Name From tblDefEmployee WHERE isnull(SalePerson,0)=1")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If Me.cmbSalesman.SelectedIndex = 0 Then
                ShowErrorMessage("Please select sale person")
                Me.cmbSalesman.Focus()
                Exit Sub
            End If

            DocDate = Me.DateTimePicker1.Value
            SalePersonId = Me.cmbSalesman.SelectedValue

            If BackgroundWorker1.IsBusy Then Exit Sub
            BackgroundWorker1.RunWorkerAsync()
            Do While BackgroundWorker1.IsBusy
                Application.DoEvents()
            Loop
            AddRptParam("@DocumentDate", Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@SALEPERSON", Me.cmbSalesman.SelectedValue)
            ShowReport("rptDSRstatement")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub GetData()
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As OleDb.OleDbTransaction = Con.BeginTransaction
        Dim cmd As New OleDb.OleDbCommand
        cmd.Connection = Con
        cmd.Transaction = trans
        Try
            Dim strQuery As String
            strQuery = "SELECT CONVERT(DateTime, CONVERT(Varchar, LEFT(dbo.SalesMasterTable.SalesDate, 11), 102), 102) AS Date, ISNULL(dbo.tblCustomer.RootPlanId, 0) AS RootPlanId, " _
                 & " dbo.SalesMasterTable.EmployeeCode, dbo.SalesDetailTable.ArticleDefId, SUM(ISNULL(dbo.SalesDetailTable.Qty, 0)) AS Qty,  " _
                 & " SUM(ISNULL(dbo.SalesDetailTable.SampleQty, 0)) AS SchQty, dbo.SalesDetailTable.CurrentPrice, dbo.SalesDetailTable.Price " _
                 & " FROM  dbo.SalesMasterTable INNER JOIN " _
                 & " dbo.SalesDetailTable ON dbo.SalesMasterTable.SalesId = dbo.SalesDetailTable.SalesId INNER JOIN " _
                 & " dbo.tblCustomer ON dbo.SalesMasterTable.CustomerCode = dbo.tblCustomer.AccountId " _
                 & " WHERE (CONVERT(DateTime, CONVERT(Varchar, LEFT(dbo.SalesMasterTable.SalesDate, 11), 102), 102) = Convert(DateTime , '" & DocDate.ToString("yyyy-M-d 00:00:00") & "', 102)) AND dbo.SalesMasterTable.EmployeeCode=" & SalePersonId & " " _
                 & " GROUP BY CONVERT(DateTime, CONVERT(Varchar, LEFT(dbo.SalesMasterTable.SalesDate, 11), 102), 102), ISNULL(dbo.tblCustomer.RootPlanId, 0),  " _
                 & " dbo.SalesMasterTable.EmployeeCode, dbo.SalesDetailTable.ArticleDefId, dbo.SalesDetailTable.CurrentPrice, dbo.SalesDetailTable.Price  HAVING (SUM(ISNULL(dbo.SalesDetailTable.Qty, 0)) <> 0 OR SUM(ISNULL(dbo.SalesDetailTable.SampleQty, 0)) <> 0) "

            Dim dtSales As New DataTable
            dtSales = GetDataTable(strQuery, trans)


            strQuery = String.Empty
            strQuery = "Truncate Table tblDSRStatementTemp"
            cmd.CommandText = strQuery
            cmd.ExecuteNonQuery()
            If dtSales IsNot Nothing Then
                If dtSales.Rows.Count > 0 Then
                    For Each r As DataRow In dtSales.Rows
                        If r IsNot Nothing Then
                            If r.ItemArray.Length > 0 Then
                                strQuery = String.Empty
                                strQuery = "INSERT INTO tblDSRStatementTemp(DocDate, RootPlanId, SalePerson, ArticleDefId, Delivered_Qty, Damage_Qty, Unsold_Qty, SRet_Qty, Sch_Qty, CurrentPrice, Price, RetSch_Qty) " _
                                & " VALUES('" & r("Date") & "', " & r("RootPlanId") & ", " & r("EmployeeCode") & ", " & r("ArticleDefId") & ", " & r("Qty") & ",0,0,0," & r("SchQty") & ", " & r("CurrentPrice") & ", " & r("Price") & ",0) "
                                cmd.CommandText = strQuery
                                cmd.ExecuteNonQuery()
                            End If
                        End If
                    Next
                End If
            End If


            strQuery = String.Empty
            strQuery = "SELECT CONVERT(DateTime, CONVERT(Varchar, LEFT(dbo.SalesReturnMasterTable.SalesReturnDate, 11), 102), 102) AS Date, ISNULL(dbo.tblCustomer.RootPlanId, 0) AS RootPlanId, " _
                     & " dbo.SalesReturnMasterTable.EmployeeCode, dbo.SalesReturnDetailTable.ArticleDefId, SUM(ISNULL(dbo.SalesReturnDetailTable.Qty, 0)) AS Qty,  " _
                     & " Sum(isnull(SalesReturnDetailTable.SampleQty,0)) AS SchQty, dbo.SalesReturnDetailTable.CurrentPrice, dbo.SalesReturnDetailTable.Price " _
                     & " FROM  dbo.SalesReturnMasterTable INNER JOIN " _
                     & " dbo.SalesReturnDetailTable ON dbo.SalesReturnMasterTable.SalesReturnId = dbo.SalesReturnDetailTable.SalesReturnId INNER JOIN " _
                     & " dbo.tblCustomer ON dbo.SalesReturnMasterTable.CustomerCode = dbo.tblCustomer.AccountId INNER JOIN tblDefLocation on tblDefLocation.Location_ID = SalesReturnDetailTable.LocationId " _
                     & " WHERE (CONVERT(DateTime, CONVERT(Varchar, LEFT(dbo.SalesReturnMasterTable.SalesReturnDate, 11), 102), 102) = Convert(DateTime , '" & DocDate.ToString("yyyy-M-d 00:00:00") & "', 102)) AND dbo.SalesReturnMasterTable.EmployeeCode=" & SalePersonId & " AND tblDefLocation.Location_Type = 'Damage'   " _
                     & " GROUP BY CONVERT(DateTime, CONVERT(Varchar, LEFT(dbo.SalesReturnMasterTable.SalesReturnDate, 11), 102), 102), ISNULL(dbo.tblCustomer.RootPlanId, 0),  " _
                     & " dbo.SalesReturnMasterTable.EmployeeCode, dbo.SalesReturnDetailTable.ArticleDefId, dbo.SalesReturnDetailTable.CurrentPrice, dbo.SalesReturnDetailTable.Price HAVING (SUM(ISNULL(dbo.SalesReturnDetailTable.Qty, 0)) <> 0  Or Sum(isnull(SalesReturnDetailTable.SampleQty,0)) <> 0)"

            Dim dtSalesRet As New DataTable
            dtSalesRet = GetDataTable(strQuery, trans)


            If dtSalesRet IsNot Nothing Then
                If dtSalesRet.Rows.Count > 0 Then
                    For Each r As DataRow In dtSalesRet.Rows
                        If r IsNot Nothing Then
                            If r.ItemArray.Length > 0 Then
                                strQuery = String.Empty
                                strQuery = "INSERT INTO tblDSRStatementTemp(DocDate, RootPlanId, SalePerson, ArticleDefId, Delivered_Qty, Damage_Qty, Unsold_Qty, SRet_Qty, Sch_Qty, CurrentPrice, Price,RetSch_Qty) " _
                                & " VALUES('" & r("Date") & "', " & r("RootPlanId") & ", " & r("EmployeeCode") & ", " & r("ArticleDefId") & ", 0," & r("Qty") & ",0,0,0, " & r("CurrentPrice") & ", " & r("Price") & "," & r("SchQty") & ") "
                                cmd.CommandText = strQuery
                                cmd.ExecuteNonQuery()
                            End If
                        End If
                    Next
                End If
            End If


            strQuery = String.Empty
            strQuery = "SELECT CONVERT(DateTime, CONVERT(Varchar, LEFT(dbo.SalesReturnMasterTable.SalesReturnDate, 11), 102), 102) AS Date, ISNULL(dbo.tblCustomer.RootPlanId, 0) AS RootPlanId, " _
                     & " dbo.SalesReturnMasterTable.EmployeeCode, dbo.SalesReturnDetailTable.ArticleDefId, SUM(ISNULL(dbo.SalesReturnDetailTable.Qty, 0)) AS Qty,  " _
                     & " Sum(ISNULL(SalesReturnDetailTable.SampleQty,0)) AS SchQty, dbo.SalesReturnDetailTable.CurrentPrice, dbo.SalesReturnDetailTable.Price " _
                     & " FROM  dbo.SalesReturnMasterTable INNER JOIN " _
                     & " dbo.SalesReturnDetailTable ON dbo.SalesReturnMasterTable.SalesReturnId = dbo.SalesReturnDetailTable.SalesReturnId INNER JOIN " _
                     & " dbo.tblCustomer ON dbo.SalesReturnMasterTable.CustomerCode = dbo.tblCustomer.AccountId INNER JOIN tblDefLocation on tblDefLocation.Location_ID = SalesReturnDetailTable.LocationId " _
                     & " WHERE (CONVERT(DateTime, CONVERT(Varchar, LEFT(dbo.SalesReturnMasterTable.SalesReturnDate, 11), 102), 102) = Convert(DateTime , '" & DocDate.ToString("yyyy-M-d 00:00:00") & "', 102)) AND dbo.SalesReturnMasterTable.EmployeeCode=" & SalePersonId & " AND tblDefLocation.Location_Type <> 'Damage'  " _
                     & " GROUP BY CONVERT(DateTime, CONVERT(Varchar, LEFT(dbo.SalesReturnMasterTable.SalesReturnDate, 11), 102), 102), ISNULL(dbo.tblCustomer.RootPlanId, 0),  " _
                     & " dbo.SalesReturnMasterTable.EmployeeCode, dbo.SalesReturnDetailTable.ArticleDefId, dbo.SalesReturnDetailTable.CurrentPrice, dbo.SalesReturnDetailTable.Price HAVING (SUM(ISNULL(dbo.SalesReturnDetailTable.Qty, 0)) <> 0  Or Sum(ISNULL(SalesReturnDetailTable.SampleQty,0)) <> 0)"

            Dim dtSalesDmg As New DataTable
            dtSalesDmg = GetDataTable(strQuery, trans)


            If dtSalesDmg IsNot Nothing Then
                If dtSalesDmg.Rows.Count > 0 Then
                    For Each r As DataRow In dtSalesDmg.Rows
                        If r IsNot Nothing Then
                            If r.ItemArray.Length > 0 Then
                                strQuery = String.Empty
                                strQuery = "INSERT INTO tblDSRStatementTemp(DocDate, RootPlanId, SalePerson, ArticleDefId, Delivered_Qty, Damage_Qty, Unsold_Qty, SRet_Qty, Sch_Qty, CurrentPrice, Price,RetSch_Qty) " _
                                & " VALUES('" & r("Date") & "', " & r("RootPlanId") & ", " & r("EmployeeCode") & ", " & r("ArticleDefId") & ", 0,0," & r("Qty") & ",0,0, " & r("CurrentPrice") & ", " & r("Price") & ", " & r("SchQty") & ") "
                                cmd.CommandText = strQuery
                                cmd.ExecuteNonQuery()
                            End If
                        End If
                    Next
                End If
            End If

            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Sub
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            GetData()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            If Me.cmbSalesman.SelectedIndex = 0 Then
                ShowErrorMessage("Please select sale person")
                Me.cmbSalesman.Focus()
                Exit Sub
            End If

            DocDate = Me.DateTimePicker1.Value
            SalePersonId = Me.cmbSalesman.SelectedValue

            If BackgroundWorker1.IsBusy Then Exit Sub
            BackgroundWorker1.RunWorkerAsync()
            Do While BackgroundWorker1.IsBusy
                Application.DoEvents()
            Loop
            AddRptParam("@DocumentDate", Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@SALEPERSON", Me.cmbSalesman.SelectedValue)
            ShowReport("rptDSRstatement", , , , True)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class