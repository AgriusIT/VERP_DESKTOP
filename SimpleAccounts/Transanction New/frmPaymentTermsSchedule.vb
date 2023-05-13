Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Public Class frmPaymentTermsSchedule
    Implements IGeneral
    Public OrderType As String
    Public OrderId As Integer
    Public OrderNo As String
    Public FormName As String
    Private PayTypeSchedule As List(Of PaymentTermsScheduleBE)
    Public Enum enmOrderType
        SO
        PO
    End Enum

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If New PaymentTermsScheduleDAL().Delete(PayTypeSchedule) Then
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
            FillDropDown(Me.cmbPaymentType, "Select * from tblPaymentType")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try

            If Me.grd.RowCount = 0 Then Exit Sub
            PayTypeSchedule = New List(Of PaymentTermsScheduleBE)
            Dim PayTypeSch As PaymentTermsScheduleBE
            For Each objrow As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                PayTypeSch = New PaymentTermsScheduleBE
                PayTypeSch.PayTypeId = Val(objrow.Cells("PayTypeId").Value.ToString)
                PayTypeSch.SchDate = objrow.Cells("SchDate").Value
                PayTypeSch.Amount = Val(objrow.Cells("Amount").Value.ToString)
                PayTypeSch.OrderId = OrderId
                PayTypeSch.OrderType = OrderType
                PayTypeSchedule.Add(PayTypeSch)
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim dtdata As New DataTable

            dtdata = New PaymentTermsScheduleDAL().GetDetailRecords(OrderId, OrderType)
            Me.grd.DataSource = dtdata
            Dim dt As New DataTable
            dt = New PaymentTypeDAL().GetAll
            Me.grd.RootTable.Columns("PayTypeId").ValueList.PopulateValueList(dt.DefaultView, "PayTypeId", "PaymentType")


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.grd.RowCount = 0 Then
                ShowErrorMessage("Record not in grid.")
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            If (New PaymentTermsScheduleDAL().CheckExistingData(OrderId, OrderType)) = True Then
                Me.btnSave.Text = "&Update"
            Else
                Me.btnSave.Text = "&Save"
            End If
            Me.dtpScheduleDate.Value = Now
            If Not Me.cmbPaymentType.SelectedIndex = -1 Then Me.cmbPaymentType.SelectedIndex = 0
            Me.txtAmount.Text = String.Empty
            Me.txtOrderNo.Text = OrderNo
            GetAllRecords()
            Me.dtpScheduleDate.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New PaymentTermsScheduleDAL().Save(PayTypeSchedule) Then
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

    End Function

    Private Sub frmPaymentTermsSchedule_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Private Sub frmPaymentTermsSchedule_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try

            If FormName = enmOrderType.PO Then
                OrderType = enmOrderType.PO.ToString
                Me.lblHeading.Text = "Payment Schedule"
                Me.Text = "Payment Schedule"
            ElseIf FormName = enmOrderType.SO Then
                OrderType = enmOrderType.SO.ToString
                Me.lblHeading.Text = "Receipt Schedule"
                Me.Text = "Receipt Schedule"
            End If

            FillCombos()
            ReSetControls()


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''
    ''
    ''
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Try

            If Me.dtpScheduleDate.Value = Date.MinValue Then
                ShowErrorMessage("Schedule date invalid.")
                Me.dtpScheduleDate.Focus()
                Return
            End If
            If Me.cmbPaymentType.SelectedIndex = 0 Then
                ShowErrorMessage("Please select payment type.")
                Me.cmbPaymentType.Focus()
                Return
            End If
            If Not Val(Me.txtAmount.Text) <> 0 Then
                ShowErrorMessage("Please enter amount.")
                Me.txtAmount.Focus()
                Return
            End If
            Dim dtData As DataTable = CType(Me.grd.DataSource, DataTable)
            Dim dr As DataRow
            dtData.TableName = "Defaul"
            dr = dtData.NewRow
            dr(0) = Me.dtpScheduleDate.Value
            dr(1) = Me.cmbPaymentType.SelectedValue
            dr(3) = Val(Me.txtAmount.Text)
            dtData.Rows.Add(dr)
            dtData.AcceptChanges()

            Me.txtAmount.Text = String.Empty
            If Not Me.cmbPaymentType.SelectedIndex = -1 Then Me.cmbPaymentType.SelectedIndex = 0
            Me.dtpScheduleDate.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            If Me.grd.RowCount = 0 Then Exit Sub
            If e.Column.Key = "Column1" Then
                Me.grd.CurrentRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                ReSetControls()
            Else
                Throw New Exception("Record not saved.")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try

            If Me.grd.RowCount = 0 Then Exit Sub
            If IsValidate() = True Then
                If Delete() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                ReSetControls()
            Else
                Throw New Exception("Can't delete record.")
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmPaymentTermsSchedule_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                SendKeys.Send("{TAB}")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnPaymentType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPaymentType.Click
        Try
            ApplyStyleSheet(frmPaymentTermsType)
            If frmPaymentTermsType.ShowDialog = Windows.Forms.DialogResult.Yes Then
                Dim id As Integer = 0I
                id = Me.cmbPaymentType.SelectedIndex
                FillCombos()
                Me.cmbPaymentType.SelectedIndex = id


                Dim dt As New DataTable
                dt = New PaymentTypeDAL().GetAll
                Me.grd.RootTable.Columns("PayTypeId").ValueList.PopulateValueList(dt.DefaultView, "PayTypeId", "PaymentType")

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class