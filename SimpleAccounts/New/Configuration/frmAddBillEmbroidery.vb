Public Class frmAddBillEmbroidery
    Public _DocDate As DateTime
    Public _DocNo As String
    Public _CustomerId As String
    Public _Note As String
    Public _CompanyId As String

    Private Sub frmAddBillEmbroidery_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillDropDown(Me.cmbInvoice, "Select DocId, DocNo + ' ~ ' + Convert(Varchar, Left(DocDate,11), 102) From tblBillAnalysisMaster WHERE DocId not In(Select Isnull(RefDocId,0) as RefDocId From SalesMasterTable)")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Try
            If Me.cmbInvoice.SelectedIndex = -1 Then Exit Sub
            Dim dt As New DataTable
            dt = GetDataTable("Select DocDate, CustomerId, Note, CompanyId,DocNo From tblBillAnalysisMaster WHERE DocId=" & Me.cmbInvoice.SelectedValue & "")
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    _DocDate = dt.Rows(0).Item("DocDate")
                    _CustomerId = dt.Rows(0).Item("CustomerId")
                    _Note = dt.Rows(0).Item("Note")
                    _CompanyId = dt.Rows(0).Item("CompanyId")
                    _DocNo = dt.Rows(0).Item("DocNo")
                    DialogResult = Windows.Forms.DialogResult.Yes
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmAddBillEmbroidery_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class