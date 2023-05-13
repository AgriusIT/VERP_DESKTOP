Imports System.Data.OleDb

Imports SBModel
Imports SBDal
Imports SBUtility

Public Class FrmAddCustomers
    Implements IGeneral
    Public FormType As String
    Private objModel As COADeail
    Public IsFormOpen As Boolean = False ''TFS3684 : Ayesha Rehman : 27-06-2018
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub FrmAddCustomers_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F4 Then
            btnSave_Click(Nothing, Nothing)
        End If
        If e.KeyCode = Keys.Escape Then
            btnCancel_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub FrmAddCustomers_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            If FormType = "Customer" Then
                Me.Text = "Add New Customer "
                lblHeader.Text = "Customer's"
                'FillDropDown(ComboBox1, "SELECT     main_sub_sub_id, sub_sub_title " _
                '& " FROM dbo.tblCOAMainSubSub " _
                '& "WHERE     (account_type = '" & FormType & "') ")
            ElseIf FormType = "Expense" Then

                Me.Text = "Add New Expense A/C"
                lblHeader.Text = "Expense's"

            ElseIf FormType = "Cash" Then
                Me.Text = "Cash Balances"
                lblHeader.Text = "Cash Balances"

            ElseIf FormType = "Bank" Then
                Me.Text = "Bank Balances"
                lblHeader.Text = "Bank Balances"


            ElseIf FormType = "General" Then
                Me.Text = "General Account"
                lblHeader.Text = "General Account"

            ElseIf FormType = "CostCenter" Then
                Me.Text = "Cost Center"
                lblHeader.Text = "Cost Center"
                'Else
                '    Me.Text = "Add New Vendors"
                '    Label3.Text = "Vendor's"

                ''FillDropDown(ComboBox1, "SELECT     main_sub_sub_id, sub_sub_title " _
                '' & " FROM dbo.tblCOAMainSubSub " _
                ''& "WHERE     (account_type = '" & FormType & "') ")
                'FillDropDown(ComboBox1, "SELECT    main_sub_sub_id, sub_sub_title " _
                '& " FROM dbo.tblCOAMainSubSub " _
                '& "WHERE      (account_type = '" & FormType & "') ")

            End If

            FillDropDown(ComboBox1, "SELECT main_sub_sub_id, sub_sub_title " _
            & " FROM dbo.tblCOAMainSubSub " _
            & "WHERE      (account_type IN('" & FormType & "' " & IIf(FormType.ToString = "Vendor", ",'LC'", "") & ")) ")

            If Me.ComboBox1.Items.Count > 1 Then
                Me.ComboBox1.SelectedIndex = 1
            End If
            IsFormOpen = True
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Sub SelectAccountCode()
        If Me.ComboBox1.SelectedValue > 0 Then
            Dim adp As New OleDbDataAdapter
            Dim dt As New DataTable
            Dim strSql As String
            strSql = " SELECT sub_sub_code  AS Title From tblCoaMainsubSub where main_sub_sub_id = " & Me.ComboBox1.SelectedValue
            adp = New OleDbDataAdapter(strSql, Con)
            adp.Fill(dt)
            'Me.txtMainCode.Text = dt.Rows(0).Item(0).ToString
        End If
    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Save()
            ''Start TFS3684 :  Ayesha Rehman : Added LOC
            If Me.Text.ToString.Equals("Add New Vendor") AndAlso frmDefVendor.IsLoadedForm Then
                frmDefVendor.BindGrid()
                frmDefVendor.DataGridView1_DoubleClick(Nothing, Nothing)
            End If
            IsFormOpen = False
            ''End TFS3684
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        objModel = New COADeail
        objModel.DetailTitle = txtName.Text.ToString.Replace("'", "''")
        objModel.Active = Me.chkActive.Checked

    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        ComboBox1.SelectedIndex = 0
        txtName.Text = ""


    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Application.DoEvents()
            If Me.ComboBox1.SelectedIndex > 0 Then
                'If msg_Confirm(str_ConfirmSave) = True Then
                FillModel()
                Dim DL As New SBDal.COADetailDAL
                DL.Add(objModel, Me.ComboBox1.SelectedValue)
                ReSetControls()
                'End If
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
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

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ReSetControls()
    End Sub
End Class