''14-Mar-2014 TASK2490 Imran Ali New Bug In Release 2.1.1.6
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports SBDal.SQLHelper
Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class frmAddChequeBookSerial
    Implements IGeneral
    Dim ChequeBook As ChequeSerialBE
    Dim ChequeSerialId As Integer = 0

    Private Sub frmAddChequeBookSerial_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 16 -1-14
            If e.KeyCode = Keys.F4 Then
                btnSave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then

                btnNew_Click(Nothing, Nothing)
                Exit Sub
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub


    Private Sub frmAddChequeBookSerial_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'R-974 Ehtisham ul Haq user friendly system modification on 16-1-14 

        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            FillCombos("bank")
            FillCombos("branch")
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage("An error occured while loading the record" & Chr(10) & ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If New ChequeSerialDAL().Delete(ChequeBook) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            If Condition = "bank" Then
                FillDropDown(Me.cmbAccounts, "Select coa_detail_id ,detail_title from tblCOAMainSubSubDetail WHERE main_sub_sub_id in(select main_sub_sub_id from tblcoamainsubsub where account_type='Bank')")
            End If

            If Condition = "branch" Then
                FillDropDown(Me.cmbBranch, "Select Distinct BranchName,BranchName from ChequeMasterTable", False)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            ChequeBook = New ChequeSerialBE
            ChequeBook.ChequeSerialId = ChequeSerialId
            ChequeBook.BankAcId = Me.cmbAccounts.SelectedValue
            ChequeBook.BranchName = Me.cmbBranch.Text
            ChequeBook.Cheque_No_From = Me.txtCheque_No_From.Text
            ChequeBook.Cheque_No_To = Me.txtCheque_No_To.Text
            ChequeBook.UserName = LoginUserName
            ChequeBook.Status = False
            ChequeBook.EntryDate = Date.Now
            ChequeBook.ChequeBookDetail = New List(Of ChequeBookDetailBE)
            Dim intSerial As Integer = Val(Me.txtCheque_No_From.Text) - Val(Me.txtCheque_No_To.Text)
            Dim Serial As String = String.Empty
            Dim SerialNo As String = String.Empty
            Dim ChequeBookDt As ChequeBookDetailBE
            Dim strSerial As String = String.Empty
            For j As Integer = 0 To Me.txtCheque_No_From.Text.Length - 1
                strSerial += "0"
            Next
            For i As Integer = 0 To Math.Abs(intSerial)
                SerialNo = Microsoft.VisualBasic.Right(strSerial.ToString + CStr(Me.txtCheque_No_From.Text + i), Me.txtCheque_No_From.Text.Length)
                ChequeBookDt = New ChequeBookDetailBE
                ChequeBookDt.ChequeSerialId = ChequeSerialId
                ChequeBookDt.ChequeNo = SerialNo
                ChequeBookDt.Cheque_Issued = False
                ChequeBookDt.VoucherDetailId = 0
                ChequeBook.ChequeBookDetail.Add(ChequeBookDt)
            Next
            Dim s As String = String.Empty
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.grdDataHistory.DataSource = New ChequeSerialDAL().GetAllRecords()
            Me.grdDataHistory.RetrieveStructure()
            Me.grdDataHistory.RootTable.Columns("ChequeSerialId").Visible = False
            Me.grdDataHistory.RootTable.Columns("BankAcId").Visible = False
            Me.grdDataHistory.RootTable.Columns("detail_title").Caption = "Bank"
            Me.grdDataHistory.RootTable.Columns("BranchName").Caption = "Branch"
            Me.grdDataHistory.RootTable.Columns("Cheque_No_From").Caption = "Cheque Serial From"
            Me.grdDataHistory.RootTable.Columns("Cheque_No_To").Caption = "Cheque Serial To"
            Me.grdDataHistory.RootTable.Columns("Status").Visible = False
            'Task:2490 Remove Column UserName, EntryDate

            'Me.grdDataHistory.RootTable.Columns("").Visible = False
            'Me.grdDataHistory.RootTable.Columns("").Visible = False

            Me.grdDataHistory.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbAccounts.SelectedIndex = 0 Then
                ShowErrorMessage("Please select an account")
                Me.cmbAccounts.Focus()
                Return False
            End If

            If Me.cmbBranch.Text.Length = 0 Then
                ShowErrorMessage("Please enter/select branch name")
                Me.cmbBranch.Focus()
                Return False
            End If

            If Me.txtCheque_No_From.Text = String.Empty Then
                ShowErrorMessage("Please enter start serial no")
                Me.txtCheque_No_From.Focus()
                Return False
            End If

            If Me.txtCheque_No_To.Text = String.Empty Then
                ShowErrorMessage("Please enter end serial no")
                Me.txtCheque_No_To.Focus()
                Return False
            End If


            If Val(Me.txtCheque_No_To.Text) < Val(Me.txtCheque_No_From.Text) Then
                ShowErrorMessage("Start chqeue no is grater than end cheque no")
                Me.txtCheque_No_To.Focus()
                Return False
            End If

            If Val(Me.txtCheque_No_From.Text) > Val(Me.txtCheque_No_To.Text) Then
                ShowErrorMessage("End cheque no is grater than start cheque no")
                Me.txtCheque_No_From.Focus()
                Return False
            End If

            FillModel()
            Return True
        Catch ex As Exception

        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            ChequeSerialId = 0I
            Me.cmbAccounts.SelectedIndex = 0I
            FillCombos("branch")
            If Not Me.cmbBranch.SelectedIndex = -1 Then Me.cmbBranch.Text = String.Empty
            Me.txtCheque_No_From.Text = String.Empty
            Me.txtCheque_No_To.Text = String.Empty
            Me.btnSave.Text = "&Save"
            GetAllRecords()
            Me.cmbAccounts.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New ChequeSerialDAL().Add(ChequeBook) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons
        
    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            If New ChequeSerialDAL().Update(ChequeBook) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If Me.btnSave.Text = "&Save" Then
                    'R-974 Ehtisham ul Haq user friendly system modification on 16-1-14 

                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    If Save() = True Then
                        DialogResult = Windows.Forms.DialogResult.Yes
                        'msg_Information(str_informSave)
                        ReSetControls()
                    End If
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    'R-974 Ehtisham ul Haq user friendly system modification on 16-1-14 

                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    If Update1() = True Then
                        DialogResult = Windows.Forms.DialogResult.Yes
                        'msg_Information(str_informUpdate)
                        ReSetControls()
                    End If
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Me.grdDataHistory.RowCount = 0 Then Exit Sub
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            'R-974 Ehtisham ul Haq user friendly system modification on 16-1-14 

            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            ChequeBook = New ChequeSerialBE
            ChequeBook.ChequeSerialId = Me.grdDataHistory.GetRow.Cells("ChequeSerialId").Value
            If Delete() = True Then
                DialogResult = Windows.Forms.DialogResult.Yes
                'msg_Information(str_informDelete)
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub grdDataHistory_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdDataHistory.DoubleClick
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            If Me.grdDataHistory.RowCount = 0 Then Exit Sub
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub EditRecords()
        Try
            If Me.grdDataHistory.RowCount = 0 Then Exit Sub
            ChequeSerialId = Me.grdDataHistory.CurrentRow.Cells("ChequeSerialId").Value
            Me.cmbAccounts.SelectedValue = Me.grdDataHistory.CurrentRow.Cells("BankAcId").Value
            Me.cmbBranch.Text = Me.grdDataHistory.CurrentRow.Cells("BranchName").Value
            Me.txtCheque_No_From.Text = Me.grdDataHistory.CurrentRow.Cells("Cheque_No_From").Value
            Me.txtCheque_No_To.Text = Me.grdDataHistory.CurrentRow.Cells("Cheque_No_To").Value
            Me.btnSave.Text = "&Update"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdDataHistory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdDataHistory.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 16-1-14
        If e.KeyCode = Keys.F2 Then
            btnNew_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            btnDelete_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub
End Class