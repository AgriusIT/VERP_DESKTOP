Public Class frmGrdRptCOADetail
    Public Sub FillGrid()
        Try



            Dim dt As New DataTable
            dt = GetDataTable("select main_code +'~'+  main_title as [Main Account], sub_code +'~'+ sub_title as [Sub Account], sub_sub_code +'~'+ sub_sub_title as [Sub Sub Account],detail_code as [Account Code],detail_title as [Account Title],account_type as [Account Type],coa_detail_id  from vwCOADetail where detail_title <> ''")
            dt.AcceptChanges()
            Me.UltraTree1.DataSource = dt

          

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmGrdRptCOADetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
End Class