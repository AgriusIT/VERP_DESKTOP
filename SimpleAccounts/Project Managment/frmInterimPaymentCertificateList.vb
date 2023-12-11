'03-Oct-2018 TFS4629 : Saad Afzaal : Add new form to save update and delete records through this form.
Imports SBDal
Imports SBModel
Imports System
Imports System.Data.SqlClient

Public Class frmInterimPaymentCertificateList
    Implements IGeneral
    Dim objDAL As IntermPaymentCertificateDAL
    Dim objModel As IntermPaymentCertificateBE
    Dim DeleteRights As Boolean

    ''' <summary>
    ''' Saad Afzaal : Apply grid seeting to hide some columns and also apply filters on specific columns.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>'03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            If Me.grdItermList.RootTable.Columns.Contains("Delete") = False Then
                Me.grdItermList.RootTable.Columns.Add("Delete")
                Me.grdItermList.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grdItermList.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grdItermList.RootTable.Columns("Delete").ButtonText = "Delete"
                Me.grdItermList.RootTable.Columns("Delete").Key = "Delete"
                Me.grdItermList.RootTable.Columns("Delete").Caption = "Action"
            End If
            Me.grdItermList.RootTable.Columns("Id").Visible = False
            Me.grdItermList.RootTable.Columns("CustomerId").Visible = False
            Me.grdItermList.RootTable.Columns("SOId").Visible = False
            Me.grdItermList.RootTable.Columns("ItemId").Visible = False
            Me.grdItermList.RootTable.Columns("ContractId").Visible = False
            Me.grdItermList.RootTable.Columns("Voucher").Visible = False

            Me.grdItermList.RootTable.Columns("SONo").Caption = "SO No"

            Me.grdItermList.RootTable.Columns("CustomerName").Width = 250

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    ''' <summary>
    ''' Saad Afzaal : Calls the Delete function from DAL to remove the data of selected row.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>'03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            objDAL = New IntermPaymentCertificateDAL
            If objDAL.Delete(Val(Me.grdItermList.CurrentRow.Cells("Id").Value.ToString)) = True Then
                'Insert Activity Log by Saad Afzaal on 3-Oct-2018
                SaveActivityLog("Config", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.grdItermList.CurrentRow.Cells("DocNo").Value.ToString, True)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    ''' <summary>
    ''' Saad Afzaal : To show all saved records in history gidr.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim str As String = ""
            Dim dt As DataTable
            str = "SELECT Task.Id, Task.DocNo, Task.DocDate, Task.CustomerId, COA.detail_title AS CustomerName, Task.SOId, SO.SalesOrderNo AS SONo, Task.ItemId, Article.ArticleDescription AS ItemName, ISNULL(Task.Approved, 0) AS Approved, tblCustomerContractMaster.ContractId, ISNULL(Task.SendForApproval,0) As SendForApproval, ISNULL(Task.Rejected,0) As Rejected, ISNULL(Task.Voucher,0) AS Voucher FROM IntermPaymentCertificateMaster AS Task INNER JOIN vwCOADetail AS COA ON Task.CustomerId = COA.coa_detail_id INNER JOIN SalesOrderMasterTable AS SO ON Task.SOId = SO.SalesOrderId INNER JOIN ArticleDefView AS Article ON Task.ItemId = Article.ArticleId INNER JOIN tblCustomerContractMaster ON Task.CustomerId = tblCustomerContractMaster.CustomerId AND Task.SOId = tblCustomerContractMaster.SOId AND Task.ItemId = tblCustomerContractMaster.ItemId ORDER BY Task.Id DESC"
            dt = GetDataTable(str)
            Me.grdItermList.DataSource = dt
            Me.grdItermList.RetrieveStructure()
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

    ''' <summary>
    ''' Saad Afzaal : To add new record.
    ''' </summary>
    ''' <remarks>03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            frmInterimPaymentCertificate.IntermPaymentCertificateId = 0
            frmInterimPaymentCertificate.ShowDialog()
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub



    ''' <summary>
    ''' 'Saad Afzaal : TFS4629 : Row colors change on status based
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdItermList_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdItermList.FormattingRow
        Try
            If e.Row.Cells("Approved").Value = False AndAlso e.Row.Cells("SendForApproval").Value = False AndAlso e.Row.Cells("Voucher").Value = False AndAlso e.Row.Cells("Rejected").Value = False Then
                Dim rowstyle As New Janus.Windows.GridEX.GridEXFormatStyle
                rowstyle.BackColor = Color.LightYellow
                e.Row.RowStyle = rowstyle
            End If
            If e.Row.Cells("Approved").Value = True Then
                Dim rowstyle As New Janus.Windows.GridEX.GridEXFormatStyle
                rowstyle.BackColor = Color.LawnGreen
                e.Row.RowStyle = rowstyle
            End If
            If e.Row.Cells("SendForApproval").Value = True AndAlso e.Row.Cells("Approved").Value = False AndAlso e.Row.Cells("Rejected").Value = False AndAlso e.Row.Cells("Voucher").Value = False Then
                Dim rowstyle As New Janus.Windows.GridEX.GridEXFormatStyle
                rowstyle.BackColor = Color.LightPink
                e.Row.RowStyle = rowstyle
            End If
            If e.Row.Cells("Rejected").Value = True AndAlso e.Row.Cells("SendForApproval").Value = False AndAlso e.Row.Cells("Approved").Value = False AndAlso e.Row.Cells("Voucher").Value = False Then
                Dim rowstyle As New Janus.Windows.GridEX.GridEXFormatStyle
                rowstyle.BackColor = Color.LightGray
                e.Row.RowStyle = rowstyle
            End If
            If e.Row.Cells("Voucher").Value = True AndAlso e.Row.Cells("Approved").Value = True AndAlso e.Row.Cells("Rejected").Value = False AndAlso e.Row.Cells("SendForApproval").Value = False Then
                Dim rowstyle As New Janus.Windows.GridEX.GridEXFormatStyle
                rowstyle.BackColor = Color.Wheat
                e.Row.RowStyle = rowstyle
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    ''' <summary>
    ''' Saad Afzaal : Load from
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
    Private Sub frmItermPayementCertificateList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ApplySecurityRights()
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    ''' <summary>
    ''' Saad Afzaal : Add security rights for standard user to enable/disable buttons on right based. 
    ''' </summary>
    ''' <remarks>'03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
    Public Sub ApplySecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.DeleteRights = True
                'Saad Afzaal : TFS4629 : Rights
                Exit Sub
            End If
            Me.Visible = False
            Me.DeleteRights = False
            'Saad Afzaal : TFS4629 : Rights
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


    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdItermList.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdItermList.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdItermList.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Vendor Contract"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    ''' <summary>
    ''' Saad Afzaal : Delete the record from grid and also from database.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
    Private Sub grdItermList_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdItermList.ColumnButtonClick
        Try
            objDAL = New IntermPaymentCertificateDAL
            If e.Column.Key = "Delete" Then
                If Me.DeleteRights = True Then
                    If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                    If GetApproved(Val(Me.grdItermList.CurrentRow.Cells("Id").Value.ToString)) = False Then
                        If Delete() = True Then
                            msg_Information(str_informDelete)
                            GetAllRecords()
                        End If
                    Else
                        msg_Error(str_ErrorDependentRecordFound)
                    End If
                Else
                    ShowErrorMessage("You do not have rights to delete this document")
                End If

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    ''' <summary>
    ''' Saad Afzaal : Edit records on double click of History grid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
    Private Sub grdItermList_DoubleClick(sender As Object, e As EventArgs) Handles grdItermList.DoubleClick
        Try
            frmInterimPaymentCertificate.IntermPaymentCertificateId = Val(Me.grdItermList.CurrentRow.Cells("Id").Value.ToString)
            frmInterimPaymentCertificate.editModel = New IntermPaymentCertificateBE
            frmInterimPaymentCertificate.ContractId = Me.grdItermList.CurrentRow.Cells("ContractId").Value.ToString
            frmInterimPaymentCertificate.editModel.ProgressNo = Me.grdItermList.CurrentRow.Cells("DocNo").Value.ToString
            frmInterimPaymentCertificate.editModel.ProgressDate = CType(Me.grdItermList.CurrentRow.Cells("DocDate").Value, Date)
            frmInterimPaymentCertificate.editModel.CustomerId = Me.grdItermList.CurrentRow.Cells("CustomerId").Value.ToString
            frmInterimPaymentCertificate.editModel.SOId = Me.grdItermList.CurrentRow.Cells("SOId").Value.ToString
            frmInterimPaymentCertificate.editModel.ItemId = Me.grdItermList.CurrentRow.Cells("ItemId").Value.ToString
            frmInterimPaymentCertificate.SendForApproval = Me.grdItermList.CurrentRow.Cells("SendForApproval").Value.ToString
            frmInterimPaymentCertificate.ShowDialog()
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function GetApproved(ByVal Id As Integer) As Boolean
        Try
            Dim str As String = ""
            Dim dt As DataTable
            str = "Select Id from IntermPaymentCertificateMaster Where Approved = '1' AND Id = " & Id & ""
            dt = GetDataTable(str)
            dt.AcceptChanges()
            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class