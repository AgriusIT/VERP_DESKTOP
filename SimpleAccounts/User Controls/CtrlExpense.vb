Public Class CtrlExpense
    Dim lbl As New Label
    Dim flgCompanyRights As Boolean = False
    Dim DateFrom As DateTime
    Dim DateTo As DateTime

    Private _MyCheckBox As New CheckBox
    Public Property MyCheckBox() As CheckBox
        Get
            Return _MyCheckBox
        End Get
        Set(ByVal value As CheckBox)
            _MyCheckBox = value
        End Set
    End Property
    Private _IncludeUnPostedVoucher As Boolean = False

    Public Property IncludeUnPostedVoucher() As Boolean
        Get
            Return _IncludeUnPostedVoucher
        End Get
        Set(ByVal value As Boolean)
            _IncludeUnPostedVoucher = value
        End Set
    End Property

    Private _dtpDateFrom As DateTimePicker
    Public Property dtpDateFrom() As DateTimePicker
        Get
            Return _dtpDateFrom
        End Get
        Set(ByVal value As DateTimePicker)
            _dtpDateFrom = value

        End Set
    End Property

    Private _dtExpense As DataTable
    Public Property dtExpense() As DataTable
        Get
            Return _dtExpense
        End Get
        Set(ByVal value As DataTable)
            _dtExpense = value
        End Set
    End Property

    Private _dtpDateTo As DateTimePicker
    Public Property dtpDateTo() As DateTimePicker
        Get
            Return _dtpDateTo
        End Get
        Set(ByVal value As DateTimePicker)
            _dtpDateTo = value

        End Set
    End Property
    Private Sub CtrlExpense_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
           
            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If

            If dtpDateFrom Is Nothing Then
                dtpDateFrom = New DateTimePicker
                dtpDateFrom.Value = Now
            End If
            If dtpDateTo Is Nothing Then
                dtpDateTo = New DateTimePicker
                dtpDateTo.Value = Now
            End If

        Catch ex As Exception

        End Try
    End Sub
    Public Sub GetExpense()
        Try


            DateFrom = Me.dtpDateFrom.Value
            DateTo = Me.dtpDateTo.Value

            Me.Controls.Add(lbl)
            lbl.BackColor = Color.White
            Me.lbl.AutoSize = False
            Me.lbl.TextAlign = ContentAlignment.MiddleCenter
            Me.lbl.Dock = DockStyle.Fill
            Me.lbl.BringToFront()

            Me.lbl.Text = "Loading..."
            Application.DoEvents()

            IncludeUnPostedVoucher = MyCheckBox.Checked
            If bkgExpense.IsBusy Then Exit Sub
            Me.bkgExpense.RunWorkerAsync()
            Do While bkgExpense.IsBusy
                Application.DoEvents()
            Loop

            Me.lblExpenseAmt.Text = FormatNumber(Val(dtExpense.Rows(0).ItemArray(1)), 2, TriState.True)

            Me.lbl.Visible = False
        Catch ex As OleDb.OleDbException
            If ex.ErrorCode = "-2146232060" Then
                Me.lbl.Text = "Could not connect to the database, please check server"
            Else
                Me.lbl.Text = ex.Message.ToString
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function Expense() As DataTable
        Try

            Dim str As String = String.Empty
            str = "select 'Expense'  as Detail, ISNULL(Round(sum(isnull(a.debit_Amount,0)),0),0)  as Amount from tblvoucherdetail a inner join tblvoucher b on a.voucher_id =b.voucher_id inner join vwCOAdetail c on a.coa_detail_id=c.coa_detail_id where c.account_type= 'Expense' AND b.Post In(" & IIf(IncludeUnPostedVoucher = True, "1,0,NULL", "1") & ") and (Convert(Varchar, b.voucher_date,102) between  Convert(DateTime, '" & DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & DateTo.ToString("yyyy-M-d 23:59:59") & "',102)) " & IIf(flgCompanyRights = True, " AND c.CompanyId=" & MyCompanyId & " ", "") & ""
            dtExpense = GetDataTable(str)
            Return dtExpense
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub bkgExpense_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bkgExpense.DoWork
        Expense()
    End Sub
    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            Dim opening As Integer = 0 'GetAccountOpeningBalance(0, Me.DateTimePicker1.Value.Year & "-" & Me.DateTimePicker1.Value.Month & "-" & Me.DateTimePicker1.Value.Day & " 00:00:00")
            AddRptParam("FromDate", dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("ToDate", dtpDateTo.Value.ToString("yyyy-M-d 23:59:59"))
            Dim fromDate As String = dtpDateFrom.Value.Year & "," & dtpDateFrom.Value.Month & "," & dtpDateFrom.Value.Day & ",0,0,0"
            Dim ToDate As String = dtpDateTo.Value.Year & "," & dtpDateTo.Value.Month & "," & dtpDateTo.Value.Day & ",23,59,59"
            ShowReport("rptExpenseStatment", "{vw_Expenses.voucher_date} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ") " & IIf(IncludeUnPostedVoucher = True, " and {vw_Expenses.post} IN [True,False]", "  and {vw_Expenses.post} IN [True]") & "", "Nothing", "Nothing", , Val(opening).ToString)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
