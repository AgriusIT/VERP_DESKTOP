Imports Janus.Windows.GridEX

Public Class frmGrdRptSalesSummaryandDetail
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
        Me.grd.DataSource = dt
        Me.grd.RetrieveStructure()
        grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
        grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
        If text = "Sales Return Detail Report" Then

            grd.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty
            grd.RootTable.Columns("Qty").TotalFormatString = "N" & DecimalPointInQty
            grd.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            grd.RootTable.Columns("Price").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Price").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Price").FormatString = "N" & DecimalPointInValue

            grd.RootTable.Columns("Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Amount").FormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("Amount").TotalFormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            grd.RootTable.Columns("NetAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("NetAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("NetAmount").FormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("NetAmount").TotalFormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("NetAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            grd.RootTable.Columns("GST").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("GST").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("GST").FormatString = "N" & DecimalPointInValue

            grd.RootTable.Columns("SalesTax").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("SalesTax").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("SalesTax").FormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("SalesTax").TotalFormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("SalesTax").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            grd.RootTable.Columns("SalesReturnDate").FormatString = str_DisplayDateFormat

            Me.grd.AutoSizeColumns()
        End If
        'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
        If text = "Sales Detail Report" Then

            grd.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty
            grd.RootTable.Columns("Qty").TotalFormatString = "N" & DecimalPointInQty
            grd.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            grd.RootTable.Columns("Total Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Total Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Total Qty").FormatString = "N" & DecimalPointInQty
            grd.RootTable.Columns("Total Qty").TotalFormatString = "N" & DecimalPointInQty
            grd.RootTable.Columns("Total Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            grd.RootTable.Columns("PDP").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("PDP").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("PDP").FormatString = "N" & DecimalPointInQty

            grd.RootTable.Columns("GrossAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("GrossAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("GrossAmount").FormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("GrossAmount").TotalFormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("GrossAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            grd.RootTable.Columns("DiscountFactor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("DiscountFactor").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("DiscountFactor").FormatString = "N" & DecimalPointInValue

            grd.RootTable.Columns("DiscountValue").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("DiscountValue").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("DiscountValue").FormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("DiscountValue").TotalFormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("DiscountValue").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            grd.RootTable.Columns("Total Disc Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Total Disc Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Total Disc Amount").FormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("Total Disc Amount").TotalFormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("Total Disc Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            grd.RootTable.Columns("Price After Disc.").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Price After Disc.").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Price After Disc.").FormatString = "N" & DecimalPointInValue

            grd.RootTable.Columns("GST").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("GST").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("GST").FormatString = "N" & DecimalPointInValue
            
            grd.RootTable.Columns("ExclusiveTaxAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("ExclusiveTaxAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("ExclusiveTaxAmount").FormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("ExclusiveTaxAmount").TotalFormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("ExclusiveTaxAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            grd.RootTable.Columns("TaxAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("TaxAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("TaxAmount").FormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("TaxAmount").TotalFormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("TaxAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            grd.RootTable.Columns("NetAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("NetAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("NetAmount").FormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("NetAmount").TotalFormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("NetAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            grd.RootTable.Columns("DiscRatio").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("DiscRatio").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("DiscRatio").FormatString = "N" & DecimalPointInQty
            grd.RootTable.Columns("DiscRatio").TotalFormatString = "N" & DecimalPointInQty
            grd.RootTable.Columns("DiscRatio").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Average

            grd.RootTable.Columns("ExpiryDate").FormatString = str_DisplayDateFormat
            grd.RootTable.Columns("SalesDate").FormatString = str_DisplayDateFormat

            Me.grd.AutoSizeColumns()
        End If
        'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
        If text = "Sales Summary Report" Then
            grd.RootTable.Columns("SalesQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grd.RootTable.Columns("SalesAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            ''R982 Set Formating And Aggregate Function
            grd.RootTable.Columns("Net_Value").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grd.RootTable.Columns("SalesAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            grd.RootTable.Columns("SalesQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("SalesAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Net_Value").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("OtherExpenses").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("AdjDiscount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'grd.RootTable.Columns("InvoiceDiscount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'grd.RootTable.Columns("FuelExpense").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            grd.RootTable.Columns("SalesQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("SalesAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Net_Value").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("OtherExpenses").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("AdjDiscount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far



            grd.RootTable.Columns("SalesQty").FormatString = "N" & DecimalPointInQty
            grd.RootTable.Columns("SalesAmount").FormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("Net_Value").FormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("OtherExpenses").FormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("AdjDiscount").FormatString = "N" & DecimalPointInValue

            grd.RootTable.Columns("Net_Value").Width = 100

            grd.RootTable.Columns("SalesQty").TotalFormatString = "N" & DecimalPointInQty
            grd.RootTable.Columns("SalesAmount").TotalFormatString = "N" & DecimalPointInValue
            grd.RootTable.Columns("Net_Value").TotalFormatString = "N" & DecimalPointInValue

        End If
        'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
        If text = "Sales Return Summary Report" Then
            grd.RootTable.Columns("ReturnAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grd.RootTable.Columns("ReturnAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("ReturnAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("ReturnAmount").FormatString = "N" & DecimalPointInQty
            grd.RootTable.Columns("ReturnAmount").TotalFormatString = "N" & DecimalPointInQty
            grd.RootTable.Columns("ReturnQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grd.RootTable.Columns("ReturnQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("ReturnQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("ReturnQty").FormatString = "N" & DecimalPointInQty
            grd.RootTable.Columns("ReturnQty").TotalFormatString = "N" & DecimalPointInQty

        End If
        'If text = "Sales Customer Wise Summary Report" Then
        '    grd.RootTable.Columns("Total Invoice Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
        '    grd.RootTable.Columns("Total Amount Received").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
        '    grd.RootTable.Columns("Total Sales Return").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
        '    'Dim group As GridEXGroup
        '    'Dim column As GridEXColumn
        '    'grd.RootTable.Groups.Clear()
        '    'column = grd.RootTable.Columns("Customer")
        '    'group = New GridEXGroup(column, SortOrder.Ascending)
        '    'grd.RootTable.Groups.Add(group)
        'End If

        'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
        If text = "Sales Customer Wise Detail Report" Then

            grd.RootTable.Columns("Sales Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grd.RootTable.Columns("Sales Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Sales Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Sales Amount").FormatString = "N" & DecimalPointInQty
            grd.RootTable.Columns("Sales Amount").TotalFormatString = "N" & DecimalPointInQty


            grd.RootTable.Columns("Received Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grd.RootTable.Columns("Received Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Received Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Received Amount").FormatString = "N" & DecimalPointInQty
            grd.RootTable.Columns("Received Amount").TotalFormatString = "N" & DecimalPointInQty

            grd.RootTable.Columns("Return Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grd.RootTable.Columns("Return Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Return Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Return Amount").FormatString = "N" & DecimalPointInQty
            grd.RootTable.Columns("Return Amount").TotalFormatString = "N" & DecimalPointInQty

            grd.RootTable.Columns("Cleared After Days").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grd.RootTable.Columns("Cleared After Days").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Cleared After Days").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Cleared After Days").FormatString = "N" & DecimalPointInQty
            grd.RootTable.Columns("Cleared After Days").TotalFormatString = "N" & DecimalPointInQty


            grd.RootTable.Columns("Balance").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grd.RootTable.Columns("Balance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Balance").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Balance").FormatString = "N" & DecimalPointInQty
            grd.RootTable.Columns("Balance").TotalFormatString = "N" & DecimalPointInQty
        End If

        'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
        If text = "Sales Customer Wise Summary Report" Then
            grd.RootTable.Columns("Total Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grd.RootTable.Columns("Total Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Total Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Total Amount").FormatString = "N" & DecimalPointInQty
            grd.RootTable.Columns("Total Amount").TotalFormatString = "N" & DecimalPointInQty


            grd.RootTable.Columns("Total Received").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grd.RootTable.Columns("Total Received").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Total Received").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Total Received").FormatString = "N" & DecimalPointInQty
            grd.RootTable.Columns("Total Received").TotalFormatString = "N" & DecimalPointInQty

            grd.RootTable.Columns("Total Return").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grd.RootTable.Columns("Total Return").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Total Return").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Total Return").FormatString = "N" & DecimalPointInQty
            grd.RootTable.Columns("Total Return").TotalFormatString = "N" & DecimalPointInQty

            grd.RootTable.Columns("Balance").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grd.RootTable.Columns("Balance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Balance").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("Balance").FormatString = "N" & DecimalPointInQty
            grd.RootTable.Columns("Balance").TotalFormatString = "N" & DecimalPointInQty

            'No of Invoices
            grd.RootTable.Columns("No of Invoices").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grd.RootTable.Columns("No of Invoices").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("No of Invoices").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns("No of Invoices").FormatString = "N" & DecimalPointInQty
            grd.RootTable.Columns("No of Invoices").TotalFormatString = "N" & DecimalPointInQty
        End If
        CtrlGrdBar3_Load(Nothing, Nothing)
        LoadLayout()
        ' Add any initialization after the InitializeComponent() call.

        'Private Sub SaveLayout()
        '    Try
        '        If Me.Label1.Text = "Sales Detail Report" Then
        '            If IO.Directory.Exists(Application.ExecutablePath & "\..\Layouts") = False Then
        '                IO.Directory.CreateDirectory(Application.ExecutablePath & "\..\Layouts")
        '            End If
        '            Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name & "_" & "Sales Detail Report", IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
        '            Me.grd.SaveLayoutFile(fs)
        '            fs.Flush()
        '            fs.Close()
        '            fs.Dispose()
        '            msg_Information("Sales Detail layout saved successfully.")
        '        ElseIf Me.Label1.Text = "Sales Summary Report" Then
        '            If IO.Directory.Exists(Application.ExecutablePath & "\..\Layouts") = False Then
        '                IO.Directory.CreateDirectory(Application.ExecutablePath & "\..\Layouts")
        '            End If
        '            Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name & "_" & "Sales Summary Report", IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
        '            Me.grd.SaveLayoutFile(fs)
        '            fs.Flush()
        '            fs.Close()
        '            fs.Dispose()
        '            msg_Information("Sales Summary Layout saved successfully.")
        '        ElseIf Me.Label1.Text = "Sales Return Detail Report" Then
        '            If IO.Directory.Exists(Application.ExecutablePath & "\..\Layouts") = False Then
        '                IO.Directory.CreateDirectory(Application.ExecutablePath & "\..\Layouts")
        '            End If
        '            Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name & "_" & "Sales Return Detail Report", IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
        '            Me.grd.SaveLayoutFile(fs)
        '            fs.Flush()
        '            fs.Close()
        '            fs.Dispose()
        '            msg_Information("Sales Return Detail layout saved successfully.")
        '        ElseIf Me.Label1.Text = "Sales Return Summary Report" Then
        '            If IO.Directory.Exists(Application.ExecutablePath & "\..\Layouts") = False Then
        '                IO.Directory.CreateDirectory(Application.ExecutablePath & "\..\Layouts")
        '            End If
        '            Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name & "_" & "Sales Return Summary Report", IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
        '            Me.grd.SaveLayoutFile(fs)
        '            fs.Flush()
        '            fs.Close()
        '            fs.Dispose()
        '            msg_Information("Sales Return Summary layout saved successfully.")
        '        End If
        '    Catch ex As Exception
        '        ShowErrorMessage(ex.Message)
        '    End Try
    End Sub
    Private Sub LoadLayout()
        Try
            If Me.Label1.Text = "Sales Detail Report" Then
                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name & "_" & "Sales Detail Report") Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name & "_" & "Sales Detail Report", IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                    Me.grd.LoadLayoutFile(fs)
                    fs.Close()
                    fs.Dispose()
                End If
            ElseIf Me.Label1.Text = "Sales Summary Report" Then
                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name & "_" & "Sales Summary Report") Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name & "_" & "Sales Summary Report", IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                    Me.grd.LoadLayoutFile(fs)
                    fs.Close()
                    fs.Dispose()
                End If
            ElseIf Me.Label1.Text = "Sales Return Detail Report" Then
                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name & "_" & "Sales Return Detail Report") Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name & "_" & "Sales Return Detail Report", IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                    Me.grd.LoadLayoutFile(fs)
                    fs.Close()
                    fs.Dispose()
                End If
                ''TFS1824
            ElseIf Me.Label1.Text = "Sales Return Summary Report" Then
                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name & "_" & "Sales Return Summary Report") Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name & "_" & "Sales Return Summary Report", IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
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
            If Me.Label1.Text = "Sales Detail Report" Then
                If IO.Directory.Exists(Application.ExecutablePath & "\..\Layouts") = False Then
                    IO.Directory.CreateDirectory(Application.ExecutablePath & "\..\Layouts")
                End If
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name & "_" & "Sales Detail Report", IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                Me.grd.SaveLayoutFile(fs)
                fs.Flush()
                fs.Close()
                fs.Dispose()
                msg_Information("Sales Detail layout saved successfully.")
            ElseIf Me.Label1.Text = "Sales Summary Report" Then
                If IO.Directory.Exists(Application.ExecutablePath & "\..\Layouts") = False Then
                    IO.Directory.CreateDirectory(Application.ExecutablePath & "\..\Layouts")
                End If
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name & "_" & "Sales Summary Report", IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                Me.grd.SaveLayoutFile(fs)
                fs.Flush()
                fs.Close()
                fs.Dispose()
                msg_Information("Sales Summary Layout saved successfully.")
            ElseIf Me.Label1.Text = "Sales Return Detail Report" Then
                If IO.Directory.Exists(Application.ExecutablePath & "\..\Layouts") = False Then
                    IO.Directory.CreateDirectory(Application.ExecutablePath & "\..\Layouts")
                End If
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name & "_" & "Sales Return Detail Report", IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                Me.grd.SaveLayoutFile(fs)
                fs.Flush()
                fs.Close()
                fs.Dispose()
                msg_Information("Sales Return Detail layout saved successfully.")
            ElseIf Me.Label1.Text = "Sales Return Summary Report" Then
                If IO.Directory.Exists(Application.ExecutablePath & "\..\Layouts") = False Then
                    IO.Directory.CreateDirectory(Application.ExecutablePath & "\..\Layouts")
                End If
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name & "_" & "Sales Return Summary Report", IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                Me.grd.SaveLayoutFile(fs)
                fs.Flush()
                fs.Close()
                fs.Dispose()
                msg_Information("Sales Return Summary layout saved successfully.")
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
            If Me.Label1.Text = "Sales Detail Report" Then
                Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle & vbCrLf & "Sales Detail Report"
            ElseIf Me.Label1.Text = "Sales Summary Report" Then
                Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle & vbCrLf & "Sales Summary Report"
            ElseIf Me.Label1.Text = "Sales Return Detail Report" Then
                Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle & vbCrLf & "Sales Return Detail Report"
            ElseIf Me.Label1.Text = "Sales Return Summary Report" Then
                Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle & vbCrLf & "Sales Return Summary Report"
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class