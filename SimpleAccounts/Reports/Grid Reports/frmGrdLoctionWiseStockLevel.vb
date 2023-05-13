Public Class frmGrdLoctionWiseStockLevel
    Public Level As Integer = 0
    Public LocationId As Integer = 0
    Private Sub frmGrdLoctionWiseStockLevel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub FillGrid()
        Try
            Dim dt As New DataTable
            dt = GetDataTable("SP_LocationWiseStockLevel " & Level & "")
            dt.TableName = "StockLevel"
            Dim dv As New DataView
            dv.Table = dt
            If LocationId > 0 Then
                dv.RowFilter = "LocationId=" & LocationId & ""
            End If
            Me.grdSaved.DataSource = dv.ToTable
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns(0).Visible = False
            Me.grdSaved.RootTable.Columns(1).Visible = False
            Me.grdSaved.AutoSizeColumns()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Location Wise Stock Level"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class