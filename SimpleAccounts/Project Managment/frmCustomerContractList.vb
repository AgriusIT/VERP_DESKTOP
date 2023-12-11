Imports SBDal
Imports SBModel
Imports System
Imports System.Data.SqlClient

Public Class frmCustomerContractList
    Implements IGeneral
    Dim objDAL As CustomerContractDAL
    Dim objModel As CustomerContractBE
    Dim DeleteRights As Boolean

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            frmCustomerContract.Contract_Id = 0
            frmCustomerContract.ShowDialog()
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

        Try
            If Me.grdCustomerContact.RootTable.Columns.Contains("Delete") = False Then
                Me.grdCustomerContact.RootTable.Columns.Add("Delete")
                Me.grdCustomerContact.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grdCustomerContact.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grdCustomerContact.RootTable.Columns("Delete").ButtonText = "Delete"
                Me.grdCustomerContact.RootTable.Columns("Delete").Key = "Delete"
                Me.grdCustomerContact.RootTable.Columns("Delete").Caption = "Action"
            End If

            Me.grdCustomerContact.RootTable.Columns("ContractId").Visible = False
            Me.grdCustomerContact.RootTable.Columns("CustomerId").Visible = False
            Me.grdCustomerContact.RootTable.Columns("SOId").Visible = False
            Me.grdCustomerContact.RootTable.Columns("ItemId").Visible = False
            Me.grdCustomerContact.RootTable.Columns("TermId").Visible = False
            Me.grdCustomerContact.RootTable.Columns("BankId").Visible = False

            Me.grdCustomerContact.RootTable.Columns("CustomerName").Width = 250

            Me.grdCustomerContact.RootTable.Columns("SONo").Caption = "SO No"
            Me.grdCustomerContact.RootTable.Columns("RetentionPercentage").Caption = "Ret%"
            Me.grdCustomerContact.RootTable.Columns("RetentionValue").Caption = "Retention Amount"
            Me.grdCustomerContact.RootTable.Columns("MobilizationPercentage").Caption = "Mob%"
            Me.grdCustomerContact.RootTable.Columns("MobilizationValue").Caption = "Mobilization Amount"

            Me.grdCustomerContact.RootTable.Columns("RetentionPercentage").FormatString = "N" & DecimalPointInValue
            Me.grdCustomerContact.RootTable.Columns("RetentionPercentage").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCustomerContact.RootTable.Columns("RetentionPercentage").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdCustomerContact.RootTable.Columns("RetentionValue").FormatString = "N" & DecimalPointInValue
            Me.grdCustomerContact.RootTable.Columns("RetentionValue").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCustomerContact.RootTable.Columns("RetentionValue").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCustomerContact.RootTable.Columns("RetentionValue").TotalFormatString = "N" & DecimalPointInValue
            Me.grdCustomerContact.RootTable.Columns("RetentionValue").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grdCustomerContact.RootTable.Columns("MobilizationPerBill").FormatString = "N" & DecimalPointInValue
            Me.grdCustomerContact.RootTable.Columns("MobilizationPerBill").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCustomerContact.RootTable.Columns("MobilizationPerBill").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCustomerContact.RootTable.Columns("MobilizationPerBill").TotalFormatString = "N" & DecimalPointInValue
            Me.grdCustomerContact.RootTable.Columns("MobilizationPerBill").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grdCustomerContact.RootTable.Columns("MobilizationPercentage").FormatString = "N" & DecimalPointInValue
            Me.grdCustomerContact.RootTable.Columns("MobilizationPercentage").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCustomerContact.RootTable.Columns("MobilizationPercentage").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdCustomerContact.RootTable.Columns("MobilizationValue").FormatString = "N" & DecimalPointInValue
            Me.grdCustomerContact.RootTable.Columns("MobilizationValue").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCustomerContact.RootTable.Columns("MobilizationValue").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCustomerContact.RootTable.Columns("MobilizationValue").TotalFormatString = "N" & DecimalPointInValue
            Me.grdCustomerContact.RootTable.Columns("MobilizationValue").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grdCustomerContact.RootTable.Columns("ChequeNo").FormatString = ""

            Me.grdCustomerContact.RootTable.Columns("ChequeAmount").FormatString = "N" & DecimalPointInValue
            Me.grdCustomerContact.RootTable.Columns("ChequeAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCustomerContact.RootTable.Columns("ChequeAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdCustomerContact.RootTable.Columns("ChequeAmount").TotalFormatString = "N" & DecimalPointInValue
            Me.grdCustomerContact.RootTable.Columns("ChequeAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            objDAL = New CustomerContractDAL
            If objDAL.Delete(Val(Me.grdCustomerContact.CurrentRow.Cells("ContractId").Value.ToString)) = True Then
                Dim VoucherId As Integer = GetVoucherId("frmCustomerContract", Me.grdCustomerContact.CurrentRow.Cells("DocNo").Value.ToString)
                objDAL.DeleteVoucher(VoucherId)
                DeleteTerms()
                'Insert Activity Log
                SaveActivityLog("Config", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.grdCustomerContact.CurrentRow.Cells("DocNo").Value.ToString, True)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub DeleteTerms()
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction

            str = "Delete from tblCustomerContractTerms Where TermId=" & Val(Me.grdCustomerContact.CurrentRow.Cells("TermId").Value.ToString) & " and ContractId=" & Val(Me.grdCustomerContact.CurrentRow.Cells("ContractId").Value.ToString) & " "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            conn.Close()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Sub

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim str As String = ""
            Dim dt As DataTable
            str = "SELECT Contract.ContractId, Contract.DocNo, Contract.DocDate, Contract.CustomerId, COA.detail_title AS CustomerName, Contract.SOId, SO.SalesOrderNo AS SONo, Contract.ItemId, Article.ArticleDescription AS ItemName, Contract.TermId, Contract.RetentionPercentage, Contract.RetentionValue, Contract.MobilizationPerBill, Contract.MobilizationPercentage, Contract.MobilizationValue, Contract.BankId, Contract.ChequeNo,Contract.ChequeAmount, Contract.ChequeDate, Contract.ChequeDetails FROM vwCOADetail AS COA INNER JOIN tblCustomerContractMaster AS Contract INNER JOIN ArticleDefView AS Article ON Contract.ItemId = Article.ArticleId INNER JOIN SalesOrderMasterTable AS SO ON Contract.SOId = SO.SalesOrderId ON COA.coa_detail_id = Contract.CustomerId Order By Contract.ContractId DESC"
            dt = GetDataTable(str)
            Me.grdCustomerContact.DataSource = dt
            Me.grdCustomerContact.RetrieveStructure()

            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub frmCustomerContractList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ApplySecurityRights()
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Public Sub ApplySecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.DeleteRights = True
                Exit Sub
            End If
            Me.Visible = False
            Me.DeleteRights = False
            Me.CtrlGrdBar1.mGridPrint.Enabled = False
            Me.CtrlGrdBar1.mGridExport.Enabled = False
            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Delete" Then
                    Me.DeleteRights = True
                ElseIf Rights.Item(i).FormControlName = "Grid Print" Then
                    Me.CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Export" Then
                    Me.CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdCustomerContact_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdCustomerContact.ColumnButtonClick
        Try
            objDAL = New CustomerContractDAL
            If e.Column.Key = "Delete" Then
                If Me.DeleteRights = True Then
                    If msg_Confirm(str_ConfirmDelete) = True Then
                        If Delete() = True Then
                            msg_Information(str_informDelete)
                            GetAllRecords()
                        End If
                    End If               
                Else
                    ShowErrorMessage("You do not have rights to delete this document")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdCustomerContact_DoubleClick(sender As Object, e As EventArgs) Handles grdCustomerContact.DoubleClick
        Try
            frmCustomerContract.Contract_Id = Val(Me.grdCustomerContact.CurrentRow.Cells("ContractId").Value.ToString)
            frmCustomerContract.editModel = New CustomerContractBE
            frmCustomerContract.editModel.ContractId = Me.grdCustomerContact.CurrentRow.Cells("ContractId").Value.ToString
            frmCustomerContract.editModel.ContractNo = Me.grdCustomerContact.CurrentRow.Cells("DocNo").Value.ToString
            frmCustomerContract.editModel.ContractDate = CType(Me.grdCustomerContact.CurrentRow.Cells("DocDate").Value, Date)
            frmCustomerContract.editModel.CustomerId = Me.grdCustomerContact.CurrentRow.Cells("CustomerId").Value.ToString
            frmCustomerContract.editModel.SOId = Me.grdCustomerContact.CurrentRow.Cells("SOId").Value.ToString
            frmCustomerContract.editModel.ItemId = Me.grdCustomerContact.CurrentRow.Cells("ItemId").Value.ToString
            frmCustomerContract.editModel.BankId = Val(Me.grdCustomerContact.CurrentRow.Cells("BankId").Value.ToString)
            frmCustomerContract.editModel.ChequeNo = Me.grdCustomerContact.CurrentRow.Cells("ChequeNo").Value.ToString
            frmCustomerContract.editModel.ChequeAmount = Me.grdCustomerContact.CurrentRow.Cells("ChequeAmount").Value.ToString
            If IsDBNull(Me.grdCustomerContact.CurrentRow.Cells("ChequeDate").Value) = True Then
                frmCustomerContract.editModel.ChequeDate = Date.MinValue
            Else
                frmCustomerContract.editModel.ChequeDate = Convert.ToDateTime(Me.grdCustomerContact.CurrentRow.Cells("ChequeDate").Value)
            End If
            frmCustomerContract.editModel.ChequeDetails = Me.grdCustomerContact.CurrentRow.Cells("ChequeDetails").Value.ToString
            frmCustomerContract.editModel.RetentionPercentage = Val(Me.grdCustomerContact.CurrentRow.Cells("RetentionPercentage").Value.ToString)
            frmCustomerContract.editModel.RetentionValue = Val(Me.grdCustomerContact.CurrentRow.Cells("RetentionValue").Value.ToString)
            frmCustomerContract.editModel.MobilizationPerBill = Val(Me.grdCustomerContact.CurrentRow.Cells("MobilizationPerBill").Value.ToString)
            frmCustomerContract.editModel.MobilizationPercentage = Val(Me.grdCustomerContact.CurrentRow.Cells("MobilizationPercentage").Value.ToString)
            frmCustomerContract.editModel.MobilizationValue = Val(Me.grdCustomerContact.CurrentRow.Cells("MobilizationValue").Value.ToString)
            Dim TermsAndConditionsId As Integer = Val(Me.grdCustomerContact.CurrentRow.Cells("TermId").Value.ToString)
            frmCustomerContract.editModel.TermId = TermsAndConditionsId
            frmCustomerContract.ShowDialog()
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdCustomerContact.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdCustomerContact.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdCustomerContact.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Customer Contracts"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            ApplySecurityRights()
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class