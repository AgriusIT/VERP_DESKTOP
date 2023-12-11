'' TASK TFS4050 Controls had gotten merged and update was not occuring. Fixed by Amin on 31-07-2018
Imports SBDal
Imports SBModel
Imports System.Data.OleDb
Imports System.Net.Mail
Imports System.Reflection
Imports Microsoft.Office.Interop
Public Class frmEmailConfiguration
    Implements IGeneral
    Dim EmailTemplate As New EmailTemplateDAL
    Dim ID As Integer = 0
    Private Const EM_CHARFROMPOS As Int32 = &HD7
    Private Declare Function SendMessageLong Lib "user32" Alias "SendMessageA" (ByVal hWnd As IntPtr, ByVal wMsg As Int32, ByVal wParam As Int32, ByVal lParam As Int32) As Long
    Dim Template As String = String.Empty
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If EmailTemplate.Delete(ID) Then
                ReSetControls()
                GetAllRecords()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            grdEmailTemplate.DataSource = EmailTemplate.GetAll()
            Me.grdEmailTemplate.RetrieveStructure()
            Me.grdEmailTemplate.RootTable.Columns("ID").Visible = False
            Me.grdEmailTemplate.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If cmbTemplateTitle.Text <> "Birthday" Then
                If Me.txtHtmlTemplate.Text = "" Then
                    msg_Error("Html template is required.")
                    Me.txtHtmlTemplate.Focus() : IsValidate = False : Exit Function
                End If
            End If
            If Me.cmbTemplateTitle.Text.Length < 1 Then
                msg_Error("Title is required.")
                Me.cmbTemplateTitle.Focus() : IsValidate = False : Exit Function
            End If
            ''EmailTemplate
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            'GetSecurityRights()
            Me.cmbTemplateTitle.Text = "Purchase Inquiry"
            Me.txtHtmlTemplate.Text = String.Empty
            Editor1.BodyHtml = String.Empty
            Template = String.Empty
            Me.BtnSave.Text = "&Save"
            ID = 0
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            Dim HtmlContent As String = String.Empty
            If Not Editor1.BodyHtml Is Nothing Then
                If Editor1.BodyHtml.Length > 0 Then
                    Template = Editor1.BodyHtml
                    HtmlContent = Template
                End If
            End If
            Template += "<Fields> " & txtHtmlTemplate.Text & " </Fields>"
            If EmailTemplate.Add(Me.cmbTemplateTitle.Text, Template, HtmlContent, Me.txtHtmlTemplate.Text) Then
                msg_Information("Record has been saved successfully.")
                GetAllRecords()
                ReSetControls()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            Dim HtmlContent As String = String.Empty
            If Not Editor1.BodyHtml Is Nothing Then
                If Editor1.BodyHtml.Length > 0 Then
                    Template = Editor1.BodyHtml
                    HtmlContent = Template
                End If
            End If
            Template += "<Fields> " & txtHtmlTemplate.Text & " </Fields>"
            If EmailTemplate.Update(Me.cmbTemplateTitle.Text, Template, HtmlContent, Me.txtHtmlTemplate.Text, ID) Then
                msg_Information("Record has been updated successfully.")
                GetAllRecords()
                ReSetControls()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub EditRecord()
        Try
            ID = Val(Me.grdEmailTemplate.GetRow.Cells("ID").Value.ToString)
            Me.cmbTemplateTitle.Text = Me.grdEmailTemplate.GetRow.Cells("Title").Value.ToString
            Me.txtHtmlTemplate.Text = Me.grdEmailTemplate.GetRow.Cells("FieldsContent").Value.ToString
            Me.Editor1.BodyHtml = Me.grdEmailTemplate.GetRow.Cells("HTMLContent").Value.ToString
            Me.BtnSave.Text = "&Update"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Try
            If IsValidate() Then
                If Me.BtnSave.Text = "&Save" Then
                    Save()
                Else
                    Update1()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If IsValidate() Then
                If EmailTemplate.IsExisted(Me.cmbTemplateTitle.Text) = True Then
                    msg_Error("Template is already existed.")
                    Me.cmbTemplateTitle.Focus()
                    Exit Sub
                End If
                If Me.BtnSave.Text = "&Save" Then
                    Save()
                Else
                    Update1()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If e.Tab.Index = 1 Then
                WebBrowser1.Refresh()
                WebBrowser1.DocumentText = Me.txtHtmlTemplate.Text
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmEmailConfiguration_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            'GetSecurityRights()
            'Me.txtTemplateTitle.Text = ""
            'Me.txtHtmlTemplate.Text = ""
            'Me.BtnSave.Text = "&Save"
            'ID = 0
            Me.grdEmailTemplate.AutoSizeColumns()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmEmailConfiguration_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.txtHtmlTemplate.AllowDrop = True
            ReSetControls()
            GetAllRecords()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdEmailTemplate_Click(sender As Object, e As EventArgs) Handles grdEmailTemplate.Click
        Try
            If Me.grdEmailTemplate.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                EditRecord()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        Try
            Delete()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles BtnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                'Me.BtnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.BtnSave.Enabled = False
                    Me.BtnDelete.Enabled = False
                    'Me.BtnPrint.Enabled = False
                    'Me.PrintListToolStripMenuItem.Enabled = False
                    'PrintToolStripMenuItem.Enabled = False
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmPurchaseInquiry)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        'Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If


            Else
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                'Me.BtnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False
                'CtrlGrdBar1.mGridChooseFielder.Enabled = False

                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        'Me.BtnPrint.Enabled = True
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then

                        'End Task:2395
                        'Task:2406 Added Field Chooser Rights
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        'CtrlGrdBar1.mGridChooseFielder.Enabled = True
                        'End Task:2406
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnLoadFields_Click(sender As Object, e As EventArgs) Handles btnLoadFields.Click
        Try
            frmChooseRequiredColumns.StartPosition = FormStartPosition.CenterParent
            frmChooseRequiredColumns.ShowDialog()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnGenerateEmailTemplate_Click(sender As Object, e As EventArgs)
        Try
            'EditorForm()
            '.ShowDialog()
            '  Me.txtHtmlTemplate.Text = EditorForm.HtmlTemplate
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lstColumns_DragDrop(sender As Object, e As DragEventArgs) Handles lstColumns.DragDrop
        Try
            'lstColumns.DoDragDrop(Me.lstColumns.SelectedItem, DragDropEffects.Copy)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbTemplateTitle_TextChanged(sender As Object, e As EventArgs) Handles cmbTemplateTitle.TextChanged
        Dim StrTable As String = String.Empty
        Try
            If Me.cmbTemplateTitle.Text.Length > 0 Then
                'Select Case Me.cmbTemplateTitle.Text
                '    Case "Purchase Inquiry"
                '        StrTable = "PurchaseInquiryDetail"
                '    Case "Quotation"
                '        StrTable = "QuotationDetailTable"
                'End Select
                StrTable = cmbTemplateTitle.Text
                Dim dt1 As DataTable = EmailTemplate.GetColumns(StrTable)

                'For Each Row As DataRow In dt1.Rows
                '    lstColumns.Items.Add(Row(1).ToString)b
                'Next
                Me.lstColumns.DataSource = EmailTemplate.GetColumns(StrTable)
                Me.lstColumns.DisplayMember = "Column_Name"
                Me.lstColumns.ValueMember = "Column_Name"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lstColumns_MouseDown(sender As Object, e As MouseEventArgs) Handles lstColumns.MouseDown
        Try
            lstColumns.DoDragDrop(lstColumns.SelectedItem, DragDropEffects.Copy Or DragDropEffects.Move)

            'Me.lstColumns.DoDragDrop(Me.lstColumns.SelectedItem, DragDropEffects.Copy)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtHtmlTemplate_DragDrop(sender As Object, e As DragEventArgs) Handles txtHtmlTemplate.DragDrop
        Try
            'Me.txtHtmlTemplate.SelectedText = e.Data.GetData(DataFormats.Text).ToString & " "
            'Dim DropInTextbox As TextBox = CType(sender, TextBox)
            'Dim TextToDrop As String = e.Data.GetData(GetType(String))

            ''get rid of leading " - " if there isn't 
            ''anything in the textbox
            'If String.IsNullOrEmpty(DropInTextbox.Text) Then
            '    TextToDrop = TextToDrop.TrimStart(" - ")
            'End If

            'DropInTextbox.Text += TextToDrop
            Dim obj As Object = Me.lstColumns.SelectedItem
            txtHtmlTemplate.SelectedText += obj(0).ToString & ", "
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtHtmlTemplate_DragOver(sender As Object, e As DragEventArgs) Handles txtHtmlTemplate.DragOver
        'Try
        '    If e.Data.GetDataPresent(DataFormats.StringFormat) Then
        '        e.Effect = DragDropEffects.Copy
        '        Me.txtHtmlTemplate.Select(TextBoxCursorPos(txtHtmlTemplate, e.X, e.Y), 0)
        '    Else
        '        e.Effect = DragDropEffects.None
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub
    Public Function TextBoxCursorPos(ByVal txt As TextBox, _
  ByVal X As Single, ByVal Y As Single) As Long
        ' Convert screen coordinates into control coordinates.
        Dim pt As Point = txtHtmlTemplate.PointToClient(New Point(X, _
            Y))

        ' Get the character number
        TextBoxCursorPos = SendMessageLong(txt.Handle, _
            EM_CHARFROMPOS, 0&, CLng(pt.X + pt.Y * &H10000)) _
            And &HFFFF&
    End Function

    Private Sub lstColumns_MouseMove(sender As Object, e As MouseEventArgs) Handles lstColumns.MouseMove
        Try
            'Dim ListUnderMouse As ListBox = CType(sender, ListBox)
            'Dim CurrentItem As DataRowView
            'Dim TextToPass As String = ""
            'If e.Button <> Windows.Forms.MouseButtons.Left _
            '                    OrElse ListUnderMouse.SelectedItems.Count = 0 Then
            '    Exit Sub
            'End If


            'For Each CurrentItem In ListUnderMouse.SelectedItems
            '    TextToPass += String.Format(" - {0}", CurrentItem(ListUnderMouse.DisplayMember).ToString)
            'Next

            'ListUnderMouse.DoDragDrop(TextToPass, DragDropEffects.Copy)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtHtmlTemplate_DragEnter(sender As Object, e As DragEventArgs) Handles txtHtmlTemplate.DragEnter
        'make sure drag drop data can be used
        'If e.Data.GetDataPresent(GetType(String)) And e.AllowedEffect = DragDropEffects.Copy Then
        '    e.Effect = DragDropEffects.Copy
        'Else
        '    e.Effect = DragDropEffects.None
        'End If
        Try
            e.Effect = DragDropEffects.Copy
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
     
    End Sub

    Private Sub cmbTemplateTitle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTemplateTitle.SelectedIndexChanged

    End Sub
End Class