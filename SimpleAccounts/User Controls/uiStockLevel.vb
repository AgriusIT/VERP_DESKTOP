'2015-06-28 Task# 201506028 Ali Ansari Rectifying Stock Report Selection 
Imports System.Configuration
Public Class uiStockLevel
    Dim lbl As New Label
    Dim isOpenedForm As Boolean = False
    Dim dt As DataTable
    Dim StockLevel As String = String.Empty
    Dim obj As Object
    Public Sub GetStockLevel()
        Try
            Me.Controls.Add(lbl)
            Me.lbl.BackColor = Color.White
            Me.lbl.AutoSize = False
            Me.lbl.Dock = DockStyle.Fill
            Me.lbl.TextAlign = ContentAlignment.MiddleCenter
            Me.lbl.BringToFront()

            Me.lbl.Text = "Loading..."
            Application.DoEvents()

            If Me.BackgroundWorker1.IsBusy Then Exit Sub
            Me.BackgroundWorker1.RunWorkerAsync()
            Do While BackgroundWorker1.IsBusy
                Application.DoEvents()
            Loop

            GetStockLevelCount()

        Catch ex As Exception
            ' Throw ex
        Finally
            lbl.Visible = False
        End Try
    End Sub
    Private Sub uiStockLevel_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If ConfigurationManager.AppSettings("StockLevel") IsNot Nothing Then
                Me.ComboBox1.Text = ConfigurationManager.AppSettings("StockLevel")
                StockLevel = Me.ComboBox1.Text
            Else
                Me.ComboBox1.SelectedIndex = 0
                StockLevel = Me.ComboBox1.Text
            End If
            isOpenedForm = True
        Catch ex As Exception
            ' ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            If Me.ComboBox1.SelectedIndex = -1 Then Exit Sub
            Dim Config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
            Dim ConfigSection As ConfigurationSection = Config.AppSettings
            If ConfigSection IsNot Nothing Then
                If ConfigSection.IsReadOnly = False AndAlso ConfigSection.SectionInformation.IsLocked = False Then
                    If Config.AppSettings.Settings("StockLevel") IsNot Nothing Then
                        Config.AppSettings.Settings("StockLevel").Value = Me.ComboBox1.Text
                    Else
                        Config.AppSettings.Settings.Add("StockLevel", Me.ComboBox1.Text)
                    End If
                    Config.Save()
                End If
            End If
            obj = 0D
            If Me.ComboBox1.SelectedIndex = 0 Then
                StockLevel = "MinLevel"
            ElseIf Me.ComboBox1.SelectedIndex = 1 Then
                StockLevel = "OptLevel"
            ElseIf Me.ComboBox1.SelectedIndex = 2 Then
                StockLevel = "MaxLevel"
            Else
                StockLevel = "MinLevel"
            End If
            'StockLevel = Me.ComboBox1.Text
            If Me.BackgroundWorker1.IsBusy Then Exit Sub
            Me.BackgroundWorker1.RunWorkerAsync()
            Do While BackgroundWorker1.IsBusy
                Application.DoEvents()
            Loop
            GetStockLevelCount()
        Catch ex As Exception
            'ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub GetStockLevelCount()
        Try
            Me.LinkLabel1.Text = Val(obj)
            Application.DoEvents()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            'Dim dt As DataTable = GetDataTable("SP_StockLevel '" & Me.ComboBox1.Text & "'")
            'If dt IsNot Nothing Then
            '    If dt.Rows.Count > 0 Then
            '        'AddRptParam("Stock_Level", Me.ComboBox1.Text)
            '        'ShowReport("rptStockLevel", , , , , , , dt)
            '        Dim frm As New frmGrdLocationWiseStockLevelSummary
            '        ApplyStyleSheet(frm)
            '        frm.StartPosition = FormStartPosition.CenterParent
            '        frm.Level = Me.ComboBox1.SelectedIndex
            '        frm.ShowDialog()
            '        frm.BringToFront()
            '        Exit Sub
            '    End If
            'End If
            If Not frmMain.Panel2.Controls.Contains(frmGrdRptMinimumStockLevel) Then
                'Altered Against #Task#201506028  Show Proper Stock Report
                frmMain.LoadControl("frmGrdRptMinimumStockLevel")
            Else
                frmMain.LoadControl("frmGrdRptMinimumStockLevel")
            End If
            'Altered Against #Task#201506028  Show Proper Stock Report
            If Me.ComboBox1.SelectedIndex = 0 Then
                frmGrdRptMinimumStockLevel.StockLevel = "MinLevel"
            ElseIf Me.ComboBox1.SelectedIndex = 1 Then
                frmGrdRptMinimumStockLevel.StockLevel = "OptLevel"
            ElseIf Me.ComboBox1.SelectedIndex = 2 Then
                frmGrdRptMinimumStockLevel.StockLevel = "MaxLevel"
            End If
            frmGrdRptMinimumStockLevel.FillGrid()
            '            Marked Against #Task#201506028  Show Proper Stock Report
            'frmMain.LoadControl("frmGrdRptMinimumStockLevel")
            'End If
            '            Marked Against #Task#201506028  Show Proper Stock Report
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        'dt = GetDataTable("SP_StockLevel '" & StockLevel & "'")
        'obj = dt.Compute("Count(ArticleId)", "")
        Try

            Dim strSQL As String = String.Empty
            If StockLevel = "MinLevel " Then
                strSQL = "Select Count(*) From (Select ArticleId,IsNull(StockLevel,0) StockLevel,Case When IsNull(Stock.CurrentStock,0) < IsNull(StockLevel,0) Then IsNull(Stock.CurrentStock,0) Else 0 End  as [Min Level Stock]  From ArticleDefView LEFT OUTER JOIN (Select ArticleDefId, SUM(IsNull(InQty,0)-IsNull(OutQty,0)) CurrentStock From StockDetailTable Group By ArticleDefId Having SUM(IsNull(InQty,0)-IsNull(OutQty,0)) <> 0) Stock On  Stock.ArticleDefId = ArticleDefView.ArticleId ) a WHERE a.[Min Level Stock] <> 0  AND a.StockLevel <> 0"
            ElseIf StockLevel = "MaxLevel" Then
                strSQL = "Select Count(*) From (Select ArticleId,IsNull(StockLevelMax,0) StockLevel,Case When IsNull(Stock.CurrentStock,0) >= IsNull(StockLevelMax,0)  Then IsNull(Stock.CurrentStock,0) Else 0 End  as [Max Level Stock]  From ArticleDefView LEFT OUTER JOIN (Select ArticleDefId, SUM(IsNull(InQty,0)-IsNull(OutQty,0)) CurrentStock From StockDetailTable Group By ArticleDefId Having SUM(IsNull(InQty,0)-IsNull(OutQty,0)) <> 0) Stock On  Stock.ArticleDefId = ArticleDefView.ArticleId) a WHERE a.[Max Level Stock] <> 0  AND a.StockLevel <> 0"
            ElseIf StockLevel = "OptLevel" Then
                strSQL = "Select Count(*) From (Select ArticleId,IsNull(StockLevelOpt,0) StockLevel,Case When IsNull(Stock.CurrentStock,0) >= IsNull(StockLevelOpt,0) And IsNull(Stock.CurrentStock,0) <= IsNull(StockLevelOpt,0) Then IsNull(Stock.CurrentStock,0) Else 0 End  as [Opt Level Stock]  From ArticleDefView LEFT OUTER JOIN (Select ArticleDefId, SUM(IsNull(InQty,0)-IsNull(OutQty,0)) CurrentStock From StockDetailTable Group By ArticleDefId Having SUM(IsNull(InQty,0)-IsNull(OutQty,0)) <> 0) Stock On  Stock.ArticleDefId = ArticleDefView.ArticleId) a WHERE a.[Opt Level Stock] <> 0  AND a.StockLevel <> 0"
            Else
                strSQL = "Select Count(*) From (Select ArticleId,IsNull(StockLevel,0) StockLevel,Case When IsNull(Stock.CurrentStock,0) < IsNull(StockLevel,0) Then IsNull(Stock.CurrentStock,0) Else 0 End  as [Min Level Stock]  From ArticleDefView LEFT OUTER JOIN (Select ArticleDefId, SUM(IsNull(InQty,0)-IsNull(OutQty,0)) CurrentStock From StockDetailTable Group By ArticleDefId Having SUM(IsNull(InQty,0)-IsNull(OutQty,0)) <> 0) Stock On  Stock.ArticleDefId = ArticleDefView.ArticleId ) a WHERE a.[Min Level Stock] <> 0  AND a.StockLevel <> 0"
            End If
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()

            If dt.Rows.Count > 0 Then
                obj = Val(dt.Rows(0).Item(0).ToString)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
