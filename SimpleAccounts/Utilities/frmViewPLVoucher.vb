Imports SBDal
Imports SBModel

Public Class frmViewPLVoucher
    'Public _startDate As DateTime
    'Public _endDate As DateTime
    'Public Function GetAll() As Janus.Windows.GridEX.GridEX
    '    Try
    '        Dim dt As DataTable = New PLVoucherDAL().GetAllRecords(_startDate.Date.ToString("yyyy-M-d 00:00:00"), _endDate.Date.ToString("yyyy-M-d 23:59:59"))
    '        dt.AcceptChanges()
    '        Me.grdPLVoucher.DataSource = Nothing
    '        Me.grdPLVoucher.DataSource = dt
    '        Me.grdPLVoucher.RetrieveStructure()
    '        Me.grdPLVoucher.RootTable.Columns("coa_detail_id").Visible = False
    '        Me.grdPLVoucher.RootTable.Columns("Gl_note_id").Visible = False
    '        Me.grdPLVoucher.RootTable.Columns("Balance").Visible = False

    '        Me.grdPLVoucher.RootTable.Columns("detail_title").Caption = "Account Description"
    '        Me.grdPLVoucher.RootTable.Columns("note_title").Caption = "PL Note"
    '        Me.grdPLVoucher.RootTable.Columns("Debit_Amount").Caption = "Debit"
    '        Me.grdPLVoucher.RootTable.Columns("Credit_Amount").Caption = "Credit"
    '        Me.grdPLVoucher.RootTable.Columns("Debit_Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
    '        Me.grdPLVoucher.RootTable.Columns("Credit_Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
    '        Me.grdPLVoucher.RootTable.Columns("Balance").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

    '        Me.grdPLVoucher.RootTable.Columns("Debit_Amount").FormatString = "N"
    '        Me.grdPLVoucher.RootTable.Columns("Credit_Amount").FormatString = "N"
    '        Me.grdPLVoucher.RootTable.Columns("Balance").FormatString = "N"

    '        Me.grdPLVoucher.RootTable.Columns("Debit_Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        Me.grdPLVoucher.RootTable.Columns("Credit_Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        Me.grdPLVoucher.RootTable.Columns("Balance").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

    '        Me.grdPLVoucher.RootTable.Columns("Debit_Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        Me.grdPLVoucher.RootTable.Columns("Credit_Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        Me.grdPLVoucher.RootTable.Columns("Balance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
    '        Me.grdPLVoucher.AutoSizeColumns()

    '        Return grdPLVoucher
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    'Private Sub frmViewPLVoucher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '    Try
    '        GetAll()
    '        Me.TextBox1.Text = Val(Me.grdPLVoucher.GetTotal(Me.grdPLVoucher.RootTable.Columns("Debit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)) - Val(Me.grdPLVoucher.GetTotal(Me.grdPLVoucher.RootTable.Columns("Credit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum))
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
    'Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
    '    Me.CtrlGrdBar1.txtGridTitle.Text = "Profti and Loss View"
    'End Sub
End Class