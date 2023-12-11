''TFS4584 : Ayesha Rehman : 22-10-2018 : Purchase and Purchase Return Detail and Summary Report.
Public Class frmGrdRptPurchaseSummaryandDetail
    Public Shared formText As String = ""
    Public DoHaveGridPrintRights As Boolean = False
    Public DoHaveGridExportRights As Boolean = False
    Public DoHaveGridFeildChosserRights As Boolean = False
    Public Sub New(ByVal dt As DataTable, ByVal text As String, ByVal print As Boolean, ByVal export As Boolean, ByVal feildChosser As Boolean)

        ' This call is required by the designer.
        InitializeComponent()
        'Me.Label1.Text = formText
        'Me.CtrlGrdBar3.mGridChooseFielder.Enabled = DoHaveGridFeildChosserRights
        'Me.CtrlGrdBar3.mGridPrint.Enabled = DoHaveGridPrintRights
        'Me.CtrlGrdBar3.mGridExport.Enabled = DoHaveGridExportRights
        Me.Label1.Text = text
        Me.CtrlGrdBar3.mGridChooseFielder.Enabled = feildChosser
        Me.CtrlGrdBar3.mGridPrint.Enabled = print
        Me.CtrlGrdBar3.mGridExport.Enabled = export
        Me.CtrlGrdBar3.mGridSaveLayouts.Enabled = False
        Me.grd.DataSource = dt
        Me.grd.RetrieveStructure()
        grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
        grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        If text = "Purchase Summary Report" Or text = "Purchase Return Summary Report" Then

            grd.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grd.RootTable.Columns("Additional Tax").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            ''R982 Set Formating And Aggregate Function
            grd.RootTable.Columns("Tax").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grd.RootTable.Columns("Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum


            grd.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Additional Tax").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Tax").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            grd.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Additional Tax").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Tax").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far


            grd.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty
            grd.RootTable.Columns("Additional Tax").FormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("Tax").FormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("Amount").FormatString = "N" & DecimalPointInValue


            grd.RootTable.Columns("Qty").TotalFormatString = "N" & DecimalPointInQty
            grd.RootTable.Columns("Additional Tax").TotalFormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("Tax").TotalFormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("Amount").TotalFormatString = "N" & DecimalPointInValue

            Me.grd.AutoSizeColumns()
        End If
        If text = "Purchase Detail Report" Then

            grd.RootTable.Columns("ReceivingQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grd.RootTable.Columns("ReceivingAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grd.RootTable.Columns("ReceivingQty").FormatString = "N" & DecimalPointInQty
            grd.RootTable.Columns("ReceivingAmount").FormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("ReceivingQty").TotalFormatString = "N" & DecimalPointInQty
            grd.RootTable.Columns("ReceivingAmount").TotalFormatString = "N" & DecimalPointInValue

            grd.RootTable.Columns("ReceivingQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("ReceivingAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("ReceivingQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("ReceivingAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            grd.RootTable.Columns("TotalInvwardExpense").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grd.RootTable.Columns("TotalInvwardExpense").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("TotalInvwardExpense").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("TotalInvwardExpense").FormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("TotalInvwardExpense").TotalFormatString = "N" & DecimalPointInValue

            grd.RootTable.Columns("CreditDays").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grd.RootTable.Columns("CreditDays").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("CreditDays").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("CreditDays").FormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("CreditDays").TotalFormatString = "N" & DecimalPointInValue

            grd.RootTable.Columns("Additional Tax").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            ''R982 Set Formating And Aggregate Function
            grd.RootTable.Columns("Tax").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grd.RootTable.Columns("Additional Tax").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Tax").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Additional Tax").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Tax").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Additional Tax").FormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("Tax").FormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("Additional Tax").TotalFormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("Tax").TotalFormatString = "N" & DecimalPointInValue

        End If
        If text = "Purchase Return Detail Report" Then
            grd.RootTable.Columns("PurchaseReturnAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grd.RootTable.Columns("PurchaseReturnAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("PurchaseReturnAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("PurchaseReturnAmount").FormatString = "N" & DecimalPointInQty
            grd.RootTable.Columns("PurchaseReturnAmount").TotalFormatString = "N" & DecimalPointInQty
            grd.RootTable.Columns("PurchaseReturnQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grd.RootTable.Columns("PurchaseReturnQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("PurchaseReturnQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("PurchaseReturnQty").FormatString = "N" & DecimalPointInQty
            grd.RootTable.Columns("PurchaseReturnQty").TotalFormatString = "N" & DecimalPointInQty

            grd.RootTable.Columns("Additional Tax").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            ''R982 Set Formating And Aggregate Function
            grd.RootTable.Columns("Tax").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grd.RootTable.Columns("Additional Tax").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Tax").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Additional Tax").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Tax").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Additional Tax").FormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("Tax").FormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("Additional Tax").TotalFormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("Tax").TotalFormatString = "N" & DecimalPointInValue

        End If
        Me.grd.RootTable.Columns("DocDate").FormatString = str_DisplayDateFormat
        CtrlGrdBar3_Load(Nothing, Nothing)
        LoadLayout()
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub LoadLayout()
        Try
            If Me.Label1.Text = "Purchase Detail Report" Then
                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name & "_" & "Purchase Detail Report") Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name & "_" & "Purchase Detail Report", IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                    Me.grd.LoadLayoutFile(fs)
                    fs.Close()
                    fs.Dispose()
                End If
            ElseIf Me.Label1.Text = "Purchase Summary Report" Then
                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name & "_" & "Purchase Summary Report") Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name & "_" & "Purchase Summary Report", IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                    Me.grd.LoadLayoutFile(fs)
                    fs.Close()
                    fs.Dispose()
                End If
            ElseIf Me.Label1.Text = "Purchase Return Detail Report" Then
                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name & "_" & "Purchase Return Detail Report") Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name & "_" & "Purchase Return Detail Report", IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                    Me.grd.LoadLayoutFile(fs)
                    fs.Close()
                    fs.Dispose()
                End If
                ''TFS1824
            ElseIf Me.Label1.Text = "Purchase Return Summary Report" Then
                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name & "_" & "Purchase Return Summary Report") Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name & "_" & "Purchase Return Summary Report", IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                    Me.grd.LoadLayoutFile(fs)
                    fs.Close()
                    fs.Dispose()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnSaveLayout_Click(sender As Object, e As EventArgs) Handles btnSaveLayout.Click
        Try
            SaveLayout()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub SaveLayout()
        Try
            If Me.Label1.Text = "Purchase Detail Report" Then
                If IO.Directory.Exists(Application.ExecutablePath & "\..\Layouts") = False Then
                    IO.Directory.CreateDirectory(Application.ExecutablePath & "\..\Layouts")
                End If
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name & "_" & "Purchase Detail Report", IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                Me.grd.SaveLayoutFile(fs)
                fs.Flush()
                fs.Close()
                fs.Dispose()
                msg_Information("Purchase Detail layout saved successfully.")
            ElseIf Me.Label1.Text = "Purchase Summary Report" Then
                If IO.Directory.Exists(Application.ExecutablePath & "\..\Layouts") = False Then
                    IO.Directory.CreateDirectory(Application.ExecutablePath & "\..\Layouts")
                End If
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name & "_" & "Purchase Summary Report", IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                Me.grd.SaveLayoutFile(fs)
                fs.Flush()
                fs.Close()
                fs.Dispose()
                msg_Information("Purchase Summary Layout saved successfully.")
            ElseIf Me.Label1.Text = "Purchase Return Summary Report" Then
                If IO.Directory.Exists(Application.ExecutablePath & "\..\Layouts") = False Then
                    IO.Directory.CreateDirectory(Application.ExecutablePath & "\..\Layouts")
                End If
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name & "_" & "Purchase Return Summary Report", IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                Me.grd.SaveLayoutFile(fs)
                fs.Flush()
                fs.Close()
                fs.Dispose()
                msg_Information("Purchase Return Summary layout saved successfully.")
            ElseIf Me.Label1.Text = "Purchase Return Detail Report" Then
                If IO.Directory.Exists(Application.ExecutablePath & "\..\Layouts") = False Then
                    IO.Directory.CreateDirectory(Application.ExecutablePath & "\..\Layouts")
                End If
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name & "_" & "Purchase Return Detail Report", IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                Me.grd.SaveLayoutFile(fs)
                fs.Flush()
                fs.Close()
                fs.Dispose()
                msg_Information("Purchase Return Detail layout saved successfully.")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar3.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            If Me.Label1.Text = "Purchase Detail Report" Then
                Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle & vbCrLf & "Purchase Detail Report"
            ElseIf Me.Label1.Text = "Purchase Summary Report" Then
                Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle & vbCrLf & "Purchase Summary Report"
            ElseIf Me.Label1.Text = "Purchase Return Detail Report" Then
                Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle & vbCrLf & "Purchase Return Detail Report"
            ElseIf Me.Label1.Text = "Purchase Return Summary Report" Then
                Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle & vbCrLf & "Purchase Return Summary Report"
            End If
        Catch ex As Exception
        End Try
    End Sub

End Class