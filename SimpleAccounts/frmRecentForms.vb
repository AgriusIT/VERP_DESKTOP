Public Class frmRecentForms
    Dim UiBtn As Janus.Windows.EditControls.UIButton
    Private Sub frmRecentForms_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            For Each r As DataRow In FillRecentForms.Rows
                UiBtn = New Janus.Windows.EditControls.UIButton
                UiBtn.Text = r.ItemArray(0).ToString
                UiBtn.Name = r.ItemArray(1).ToString
                UiBtn.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
                UiBtn.Size = New System.Drawing.Size(160, 160)
                UiBtn.Font = New System.Drawing.Font("Verdana", 12.0!, FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
                UiBtn.TextVerticalAlignment = Janus.Windows.EditControls.TextAlignment.Center
                AddHandler UiBtn.Click, AddressOf OpenForm
                Me.FlowLayoutPanel1.Controls.Add(CType(UiBtn, Janus.Windows.EditControls.UIButton))
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function FillRecentForms() As DataTable
        Try
            Dim str As String = String.Empty
            str = "Select DISTINCT TOP 12 LogFormCaption, tblForm.Form_Name From tblActivityLog INNER JOIN tblForm ON tblForm.Form_Caption = LogFormCaption "
            Return GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Private Sub UiButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UiButton1.Click
    '    Try

    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub UiButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Private Sub OpenForm(ByVal sender As Object, ByVal e As EventArgs)
        Try

            For Each flwPanel As Control In FlowLayoutPanel1.Controls
                If TypeOf flwPanel Is Janus.Windows.EditControls.UIButton Then
                    If flwPanel.Name = sender.Name Then
                        GetFormKey(sender.name)
                    End If
                End If
            Next

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetFormKey(ByVal Form_Name As String)
        Try
            Dim str As String = String.Empty
            str = "Select * From tblForm WHERE Form_Name='" & Form_Name & "'"
            Dim dt As DataTable = GetDataTable(str)
            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    frmMain.LoadControl("" & dt.Rows(0).Item("AccessKey").ToString & "")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class