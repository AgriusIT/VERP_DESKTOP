Public Class frmPOSList
    Public Shared ReturnIs As Boolean
    Dim X As Integer = 150
    Dim Y As Integer = 10
    Sub New(ByVal dt As DataTable)

        ' This call is required by the designer.
        InitializeComponent()
        If dt.Rows.Count > 1 Then
            For Each row As DataRow In dt.Rows
                Dim btn As New Button()
                btn.Text = row.Item("POSTitle")
                btn.Name = row.Item("POSTitle")
                btn.FlatStyle = FlatStyle.Flat
                btn.ForeColor = Color.FromArgb(12, 120, 148)
                If X > 0 AndAlso Y > 0 Then
                    Y = Y + 37
                    btn.Location = New Point(X, Y)
                End If
                Me.Controls.Add(btn)
                AddHandler btn.Click, AddressOf GenericClick
            Next
        End If
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Public Sub GenericClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try


            ReturnIs = True
            Dim selectedBtn As Button = sender
            Dim str As String = "SELECT POSTitle, CompanyId, LocationId, CostCenterId, CashAccountId, BankAccountId, SalesPersonId, DeliveryOption, DiscountPer FROM tblPOSConfiguration where POSTitle = '" & selectedBtn.Name & "'"
            Dim dt As DataTable
            dt = GetDataTable(str)
            If dt IsNot Nothing Then
                frmPOSEntry.Title = dt.Rows(0).Item("POSTitle")
                frmPOSEntry.CID = dt.Rows(0).Item("CompanyId")
                frmPOSEntry.LID = dt.Rows(0).Item("LocationId")
                frmPOSEntry.CCID = dt.Rows(0).Item("CostCenterId")
                frmPOSEntry.CAID = dt.Rows(0).Item("CashAccountId")
                frmPOSEntry.BAID = dt.Rows(0).Item("BankAccountId")
                frmPOSEntry.SPID = dt.Rows(0).Item("SalesPersonId")
                frmPOSEntry.DevOption = dt.Rows(0).Item("DeliveryOption")
                frmPOSEntry.DiscountPer = Val(dt.Rows(0).Item("DiscountPer").ToString)
            End If
                frmPOSEntry.ReSetControls()
                Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            ReturnIs = False
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class