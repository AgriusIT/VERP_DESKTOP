Imports System.Data
Public Class frmAgingBalancesTemplateNew
    Dim dtData As DataTable
    Dim IsOpenForm As Boolean = False
    Private Sub frmAgingBalancesTemplate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.txtFormatName.Text = String.Empty
            Me.txtAging.Text = String.Empty
            Me.txt1stAging.Text = String.Empty
            Me.txt1stAgingName.Text = String.Empty
            Me.txt2ndAging.Text = String.Empty
            Me.txt2ndAgingName.Text = String.Empty
            Me.txt3rdAging.Text = String.Empty
            Me.txt3rdAgingName.Text = String.Empty
            Me.txt4thAging.Text = String.Empty
            Me.txt4thAgingName.Text = String.Empty
            Me.txt5thAging.Text = String.Empty
            Me.txt5thAgingName.Text = String.Empty
            Me.txt6thAging.Text = String.Empty
            Me.txt6thAgingName.Text = String.Empty
            IsOpenForm = True
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub GetAllRecords(Optional ByVal Condition As String = "")
        Try
            dtData = New DataTable
            dtData.TableName = "tblAgingTemplate"
            dtData.Columns.Add("Id", GetType(System.Int32))
            dtData.Columns("Id").AutoIncrement = True
            dtData.Columns("Id").AutoIncrementSeed = 1
            dtData.Columns("Id").AutoIncrementStep = 1
            dtData.Columns.Add("Format_Name", GetType(System.String))
            dtData.Columns.Add("Aging", GetType(System.Int32))
            dtData.Columns.Add("1stAging", GetType(System.Int32))
            dtData.Columns.Add("1stAgingName", GetType(System.String))
            dtData.Columns.Add("2ndAging", GetType(System.Int32))
            dtData.Columns.Add("2ndAgingName", GetType(System.String))
            dtData.Columns.Add("3rdAging", GetType(System.Int32))
            dtData.Columns.Add("3rdAgingName", GetType(System.String))
            dtData.Columns.Add("4thAging", GetType(System.Int32))
            dtData.Columns.Add("4thAgingName", GetType(System.String))
            dtData.Columns.Add("5thAging", GetType(System.Int32))
            dtData.Columns.Add("5thAgingName", GetType(System.String))
            dtData.Columns.Add("6thAging", GetType(System.Int32))
            dtData.Columns.Add("6thAgingName", GetType(System.String))
            If Not IO.File.Exists(Application.StartupPath & "\TemplateAgingBalanceNew.xml") Then
                Dim dr As DataRow
                dr = dtData.NewRow
                dr(1) = "Default"
                dr(2) = 1
                dr(3) = 30
                dr(4) = "1_30"
                dr(5) = 60
                dr(6) = "30_60"
                dr(7) = 90
                dr(8) = "60_90"
                dr(9) = 180
                dr(10) = "90_180"
                dr(11) = 270
                dr(12) = "180_270"
                dr(13) = 360
                dr(14) = "360+"
                dtData.Rows.Add(dr)
                dtData.WriteXml(Application.StartupPath & "\TemplateAgingBalanceNew.xml")
            Else
                dtData.ReadXml(Application.StartupPath & "\TemplateAgingBalanceNew.xml")
            End If

            Me.grdConnectionInfo.DataSource = dtData

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Me.txtFormatName.Text = String.Empty
            Me.txtAging.Text = String.Empty
            Me.txt1stAging.Text = String.Empty
            Me.txt1stAgingName.Text = String.Empty
            Me.txt2ndAging.Text = String.Empty
            Me.txt2ndAgingName.Text = String.Empty
            Me.txt3rdAging.Text = String.Empty
            Me.txt3rdAgingName.Text = String.Empty
            Me.txt4thAging.Text = String.Empty
            Me.txt4thAgingName.Text = String.Empty
            Me.txt5thAging.Text = String.Empty
            Me.txt5thAgingName.Text = String.Empty
            Me.txt6thAging.Text = String.Empty
            Me.txt6thAgingName.Text = String.Empty
            Me.btnSave.Text = "&Save"
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function IsValidate() As Boolean
        Try
            If Me.txtFormatName.Text = String.Empty Then
                ShowErrorMessage("Enter Formate name.")
                Me.txtFormatName.Focus()
                Return False
            End If
            If Me.txtAging.Text = String.Empty Or Val(Me.txtAging.Text) = 0 Then
                ShowErrorMessage("Enter start aging.")
                Me.txtAging.Focus()
                Return False
            End If
            If Me.txt1stAging.Text = String.Empty Or Val(Me.txt1stAging.Text) = 0 Then
                ShowErrorMessage("Enter 1st Aging")
                Me.txt1stAging.Focus()
                Return False
            End If
            If Me.txt2ndAging.Text = String.Empty Or Val(Me.txt2ndAging.Text) = 0 Then
                ShowErrorMessage("Enter 2nd Aging")
                Me.txt2ndAging.Focus()
                Return False
            End If
            If Me.txt3rdAging.Text = String.Empty Or Val(Me.txt3rdAging.Text) = 0 Then
                ShowErrorMessage("Enter 3rd Aging")
                Me.txt3rdAging.Focus()
                Return False
            End If
            If Me.txt4thAging.Text = String.Empty Or Val(Me.txt4thAging.Text) = 0 Then
                ShowErrorMessage("Enter 4th Aging")
                Me.txt4thAging.Focus()
                Return False
            End If
            If Me.txt5thAging.Text = String.Empty Or Val(Me.txt5thAging.Text) = 0 Then
                ShowErrorMessage("Enter 5th Aging")
                Me.txt5thAging.Focus()
                Return False
            End If
            If Me.txt6thAging.Text = String.Empty Or Val(Me.txt6thAging.Text) = 0 Then
                ShowErrorMessage("Enter 6th Aging")
                Me.txt6thAging.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If Not IO.File.Exists(Application.StartupPath & "\TemplateAgingBalanceNew.xml") Then
                    Throw New Exception("File not exist")
                    Exit Sub
                End If
                If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                    Dim dr As DataRow
                    dr = dtData.NewRow
                    dr(1) = Me.txtFormatName.Text
                    dr(2) = Val(Me.txtAging.Text)
                    dr(3) = Val(Me.txt1stAging.Text)
                    dr(4) = Me.txt1stAgingName.Text
                    dr(5) = Val(Me.txt2ndAging.Text)
                    dr(6) = Me.txt2ndAgingName.Text
                    dr(7) = Val(Me.txt3rdAging.Text)
                    dr(8) = Me.txt3rdAgingName.Text
                    dr(9) = Val(Me.txt4thAging.Text)
                    dr(10) = Me.txt4thAgingName.Text
                    dr(11) = Val(Me.txt5thAging.Text)
                    dr(12) = Me.txt5thAgingName.Text
                    dr(13) = Val(Me.txt6thAging.Text)
                    dr(14) = Me.txt6thAgingName.Text
                    dtData.Rows.Add(dr)
                    dtData.WriteXml(Application.StartupPath & "\TemplateAgingBalanceNew.xml")
                    Me.txtFormatName.Text = String.Empty
                    Me.txtAging.Text = String.Empty
                    Me.txt1stAging.Text = String.Empty
                    Me.txt1stAgingName.Text = String.Empty
                    Me.txt2ndAging.Text = String.Empty
                    Me.txt2ndAgingName.Text = String.Empty
                    Me.txt3rdAging.Text = String.Empty
                    Me.txt3rdAgingName.Text = String.Empty
                    Me.txt4thAging.Text = String.Empty
                    Me.txt4thAgingName.Text = String.Empty
                    Me.txt5thAging.Text = String.Empty
                    Me.txt5thAgingName.Text = String.Empty
                    Me.txt6thAging.Text = String.Empty
                    Me.txt6thAgingName.Text = String.Empty
                    Me.btnSave.Text = "&Save"
                    GetAllRecords()
                    DialogResult = Windows.Forms.DialogResult.Yes
                Else
                    Dim drRow As DataRow() = dtData.Select("Id=" & Me.grdConnectionInfo.GetRow.Cells("Id").Value & "")
                    dtData.Rows.Remove(drRow(0))
                    dtData.Select()
                    Dim dr As DataRow
                    dr = dtData.NewRow
                    dr(1) = Me.txtFormatName.Text
                    dr(2) = Val(Me.txtAging.Text)
                    dr(3) = Val(Me.txt1stAging.Text)
                    dr(4) = Me.txt1stAgingName.Text
                    dr(5) = Val(Me.txt2ndAging.Text)
                    dr(6) = Me.txt2ndAgingName.Text
                    dr(7) = Val(Me.txt3rdAging.Text)
                    dr(8) = Me.txt3rdAgingName.Text
                    dr(9) = Val(Me.txt4thAging.Text)
                    dr(10) = Me.txt4thAgingName.Text
                    dr(11) = Val(Me.txt5thAging.Text)
                    dr(12) = Me.txt5thAgingName.Text
                    dr(13) = Val(Me.txt6thAging.Text)
                    dr(14) = Me.txt6thAgingName.Text
                    dtData.Rows.Add(dr)
                    dtData.WriteXml(Application.StartupPath & "\TemplateAgingBalanceNew.xml")
                    Me.txtFormatName.Text = String.Empty
                    Me.txtAging.Text = String.Empty
                    Me.txt1stAging.Text = String.Empty
                    Me.txt1stAgingName.Text = String.Empty
                    Me.txt2ndAging.Text = String.Empty
                    Me.txt2ndAgingName.Text = String.Empty
                    Me.txt3rdAging.Text = String.Empty
                    Me.txt3rdAgingName.Text = String.Empty
                    Me.txt4thAging.Text = String.Empty
                    Me.txt4thAgingName.Text = String.Empty
                    Me.txt5thAging.Text = String.Empty
                    Me.txt5thAgingName.Text = String.Empty
                    Me.txt6thAging.Text = String.Empty
                    Me.txt6thAgingName.Text = String.Empty
                    Me.btnSave.Text = "&Save"
                    GetAllRecords()
                    DialogResult = Windows.Forms.DialogResult.Yes
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdConnectionInfo_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdConnectionInfo.DoubleClick
        Try
            If Me.grdConnectionInfo.RowCount = 0 Then Exit Sub
            Me.txtFormatName.Text = Me.grdConnectionInfo.GetRow.Cells("Format_Name").Value.ToString
            Me.txtAging.Text = Val(Me.grdConnectionInfo.GetRow.Cells("Aging").Value.ToString)
            Me.txt1stAging.Text = Val(Me.grdConnectionInfo.GetRow.Cells("1stAging").Value.ToString)
            Me.txt1stAgingName.Text = Me.grdConnectionInfo.GetRow.Cells("1stAgingName").Value.ToString
            Me.txt2ndAging.Text = Val(Me.grdConnectionInfo.GetRow.Cells("2ndAging").Value.ToString)
            Me.txt2ndAgingName.Text = Me.grdConnectionInfo.GetRow.Cells("2ndAgingName").Value.ToString
            Me.txt3rdAging.Text = Val(Me.grdConnectionInfo.GetRow.Cells("3rdAging").Value.ToString)
            Me.txt3rdAgingName.Text = Me.grdConnectionInfo.GetRow.Cells("3rdAgingName").Value.ToString
            Me.txt4thAging.Text = Val(Me.grdConnectionInfo.GetRow.Cells("4thAging").Value.ToString)
            Me.txt4thAgingName.Text = Me.grdConnectionInfo.GetRow.Cells("4thAgingName").Value.ToString
            Me.txt5thAging.Text = Val(Me.grdConnectionInfo.GetRow.Cells("5thAging").Value.ToString)
            Me.txt5thAgingName.Text = Me.grdConnectionInfo.GetRow.Cells("5thAgingName").Value.ToString
            Me.txt6thAging.Text = Val(Me.grdConnectionInfo.GetRow.Cells("6thAging").Value.ToString)
            Me.txt6thAgingName.Text = Me.grdConnectionInfo.GetRow.Cells("6thAgingName").Value.ToString
            Me.btnSave.Text = "&Update"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            grdConnectionInfo_DoubleClick(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try

            Dim drRow As DataRow() = dtData.Select("Id=" & Me.grdConnectionInfo.GetRow.Cells("Id").Value & "")
            dtData.Rows.Remove(drRow(0))
            dtData.Select()
            If dtData.Rows.Count = 0 Then
                Dim dr As DataRow
                dr = dtData.NewRow
                dr(1) = "Default"
                dr(2) = 1
                dr(3) = 30
                dr(4) = "1_30"
                dr(5) = 60
                dr(6) = "30_60"
                dr(7) = 90
                dr(8) = "60_90"
                dr(9) = 180
                dr(10) = "90_180"
                dr(11) = 270
                dr(12) = "180_270"
                dr(13) = 360
                dr(14) = "360+"
                dtData.Rows.Add(dr)
                dtData.WriteXml(Application.StartupPath & "\TemplateAgingBalanceNew.xml")
                Me.txtFormatName.Text = String.Empty
                Me.txtAging.Text = String.Empty
                Me.txt1stAging.Text = String.Empty
                Me.txt1stAgingName.Text = String.Empty
                Me.txt2ndAging.Text = String.Empty
                Me.txt2ndAgingName.Text = String.Empty
                Me.txt3rdAging.Text = String.Empty
                Me.txt3rdAgingName.Text = String.Empty
                Me.txt4thAging.Text = String.Empty
                Me.txt4thAgingName.Text = String.Empty
                Me.txt5thAging.Text = String.Empty
                Me.txt5thAgingName.Text = String.Empty
                Me.txt6thAging.Text = String.Empty
                Me.txt6thAgingName.Text = String.Empty
                Me.btnSave.Text = "&Save"
                DialogResult = Windows.Forms.DialogResult.Yes
            Else
                dtData.WriteXml(Application.StartupPath & "\TemplateAgingBalanceNew.xml")
                Me.txtFormatName.Text = String.Empty
                Me.txtAging.Text = String.Empty
                Me.txt1stAging.Text = String.Empty
                Me.txt1stAgingName.Text = String.Empty
                Me.txt2ndAging.Text = String.Empty
                Me.txt2ndAgingName.Text = String.Empty
                Me.txt3rdAging.Text = String.Empty
                Me.txt3rdAgingName.Text = String.Empty
                Me.txt4thAging.Text = String.Empty
                Me.txt4thAgingName.Text = String.Empty
                Me.txt5thAging.Text = String.Empty
                Me.txt5thAgingName.Text = String.Empty
                Me.txt6thAging.Text = String.Empty
                Me.txt6thAgingName.Text = String.Empty
                Me.btnSave.Text = "&Save"
                DialogResult = Windows.Forms.DialogResult.Yes
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txt1stAging_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt1stAging.TextChanged
        Try

            If IsOpenForm = True Then Me.txt1stAgingName.Text = Me.txtAging.Text & "_" & Me.txt1stAging.Text

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txt2ndAging_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt2ndAging.TextChanged
        Try

            If IsOpenForm = True Then Me.txt2ndAgingName.Text = Me.txt1stAgingName.Text.Substring(Me.txt1stAgingName.Text.LastIndexOf("_") + 1) & "_" & Me.txt2ndAging.Text

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txt3rdAging_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt3rdAging.TextChanged
        Try

            If IsOpenForm = True Then Me.txt3rdAgingName.Text = Me.txt2ndAgingName.Text.Substring(Me.txt2ndAgingName.Text.LastIndexOf("_") + 1) & "_" & Me.txt3rdAging.Text
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txt4thAging_TextChanged(sender As Object, e As EventArgs) Handles txt4thAging.TextChanged
        Try

            If IsOpenForm = True Then Me.txt4thAgingName.Text = Me.txt3rdAgingName.Text.Substring(Me.txt3rdAgingName.Text.LastIndexOf("_") + 1) & "_" & Me.txt4thAging.Text
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txt5thAging_TextChanged(sender As Object, e As EventArgs) Handles txt5thAging.TextChanged
        Try

            If IsOpenForm = True Then Me.txt5thAgingName.Text = Me.txt4thAgingName.Text.Substring(Me.txt4thAgingName.Text.LastIndexOf("_") + 1) & "_" & Me.txt5thAging.Text
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txt6thAging_TextChanged(sender As Object, e As EventArgs) Handles txt6thAging.TextChanged
        Try

            If IsOpenForm = True Then Me.txt6thAgingName.Text = Me.txt6thAging.Text.ToString & "+" 'Me.txt2ndAgingName.Text.Substring(Me.txt2ndAgingName.Text.LastIndexOf("_") + 1) & "_" & Me.txt3rdAging.Text
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub pnlHeader_Paint(sender As Object, e As PaintEventArgs) Handles pnlHeader.Paint

    End Sub
End Class