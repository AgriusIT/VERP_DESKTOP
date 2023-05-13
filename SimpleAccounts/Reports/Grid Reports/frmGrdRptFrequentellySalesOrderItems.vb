Public Class frmGrdRptFrequentellySalesOrderItems

    Private Sub GetAllRecords()

        Try

            Dim dt As DataTable
            Dim strSQL As String = String.Empty

            strSQL = "SELECT ArticleCode as Code, ArticleDescription as Description, ArticleTypeName TypeName, ArticleCompanyName as CompanyName, ArticleGroupName as GroupName FROM ArticleDefView WHERE ArticleId NOT IN (SELECT ArticleDefId FROM salesorderdetailtable inner join salesordermastertable ON " _
            & "salesorderdetailtable.salesorderid=salesordermastertable.salesorderid WHERE SalesOrderDate > dateadd(day,-10,salesorderdate))"

            dt = GetDataTable(strSQL)
            dt.AcceptChanges()

            Me.grdHistory.DataSource = dt
            Me.grdHistory.RetrieveStructure()

            Me.grdHistory.RootTable.Columns("TypeName").Caption = "Type Name"
            Me.grdHistory.RootTable.Columns("CompanyName").Caption = "Company Name"
            Me.grdHistory.RootTable.Columns("GroupName").Caption = "Group Name"

            Me.grdHistory.AutoSizeColumns()

            CtrlGrdBar1_Load(Nothing, Nothing)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click

        Try
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub frmGrdRptFrequentellySalesOrderItems_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdHistory.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdHistory.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdHistory.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If

            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Frequently Sales Order Items"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Try

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

End Class