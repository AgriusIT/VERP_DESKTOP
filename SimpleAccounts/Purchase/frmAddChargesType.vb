Imports System.Windows.Forms
Imports SBDal
Imports SBModel
Imports System.Data.OleDb


Public Class frmAddChargesType
    Dim DetailDAL As New VendorQuotationDetailDAL

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            'Ali Faisal : TFS1314 : Validation for proper name of charges types 
            If Me.txtChargesType.Text = "" Then
                msg_Information("Please enter the valid charges type name")
                Exit Sub
            End If
            If Me.OK_Button.Text = "Add" Then
                Dim dt As DataTable
                dt = GetDataTable("SELECT TypeName FROM VendorQuotationChargesType Where TypeName = '" & Me.txtChargesType.Text & "'")
                If dt.Rows.Count > 0 Then
                    'Ali Faisal : TFS1314 : To Validate that entered name exists already in record.
                    If dt.Rows(0).Item(0).ToString = Me.txtChargesType.Text Then
                        msg_Information("Entered type exists already")
                        Exit Sub
                    End If
                End If
                DetailDAL.AddChargesType(Me.txtChargesType.Text)
                frmVendorQuotation.DisplayChargesTypes()
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
                'Ali Faisal : TFS1314 : Update charges type in edit mode
            Else
                If DetailDAL.UpdateChargesTypes(Me.grdSaved.CurrentRow.Cells("VenorQuotationChargesTypeId").Value.ToString, Me.txtChargesType.Text) = True Then
                    ResetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
      
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        If Me.Cancel_Button.Text = "Cancel" Then
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Close()
            'Ali Faisal : TFS1314 : Delete charges type
        Else
            If DetailDAL.DeleteChargesTypes(Me.grdSaved.CurrentRow.Cells("VenorQuotationChargesTypeId").Value.ToString) = True Then
                msg_Information(str_informDelete)
                ResetControls()
            Else
                msg_Information(str_ErrorDependentRecordFound)
            End If
        End If
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1314 : Get the history of saved charges types
    ''' </summary>
    ''' <remarks>Ali Faisal : TFS1314 : 11-Aug-2017</remarks>
    Private Sub GetAllRecords()
        Try
            Dim str As String = ""
            Dim dt As DataTable
            str = "SELECT VenorQuotationChargesTypeId,TypeName FROM VendorQuotationChargesType"
            dt = GetDataTable(str)
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("VenorQuotationChargesTypeId").Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1314 : Reset controls to default values
    ''' </summary>
    ''' <remarks>Ali Faisal : TFS1314 : 11-Aug-2017</remarks>
    Private Sub ResetControls()
        Try
            Me.txtChargesType.Text = ""
            Me.OK_Button.Text = "Add"
            Me.Cancel_Button.Text = "Cancel"
            Me.UltraTabControl2.Tabs(0).Selected = True
            GetAllRecords()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmAddChargesType_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ResetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1314 : To fill the controls on double click event of history grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Ali Faisal : TFS1314 : 11-Aug-2017</remarks>
    Private Sub grdSaved_DoubleClick(sender As Object, e As EventArgs) Handles grdSaved.DoubleClick
        Try
            Me.txtChargesType.Text = Me.grdSaved.CurrentRow.Cells("TypeName").Value.ToString
            Me.OK_Button.Text = "Update"
            Me.Cancel_Button.Text = "Delete"
            Me.UltraTabControl2.Tabs(0).Selected = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

End Class
