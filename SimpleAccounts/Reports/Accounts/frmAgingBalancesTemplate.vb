Imports System.Data
Public Class frmAgingBalancesTemplate
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
            If Not IO.File.Exists(Application.StartupPath & "\TemplateAgingBalance.xml") Then
                Dim dr As DataRow
                dr = dtData.NewRow
                dr(1) = "Default"
                dr(2) = 30
                dr(3) = 60
                dr(4) = "30_60"
                dr(5) = 90
                dr(6) = "60_90"
                dr(7) = 90
                dr(8) = "90+"
                dtData.Rows.Add(dr)
                dtData.WriteXml(Application.StartupPath & "\TemplateAgingBalance.xml")
            Else
                dtData.ReadXml(Application.StartupPath & "\TemplateAgingBalance.xml")
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
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If Not IO.File.Exists(Application.StartupPath & "\TemplateAgingBalance.xml") Then
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
                    dtData.Rows.Add(dr)
                    dtData.WriteXml(Application.StartupPath & "\TemplateAgingBalance.xml")
                    Me.txtFormatName.Text = String.Empty
                    Me.txtAging.Text = String.Empty
                    Me.txt1stAging.Text = String.Empty
                    Me.txt1stAgingName.Text = String.Empty
                    Me.txt2ndAging.Text = String.Empty
                    Me.txt2ndAgingName.Text = String.Empty
                    Me.txt3rdAging.Text = String.Empty
                    Me.txt3rdAgingName.Text = String.Empty
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
                    dtData.Rows.Add(dr)
                    dtData.WriteXml(Application.StartupPath & "\TemplateAgingBalance.xml")
                    Me.txtFormatName.Text = String.Empty
                    Me.txtAging.Text = String.Empty
                    Me.txt1stAging.Text = String.Empty
                    Me.txt1stAgingName.Text = String.Empty
                    Me.txt2ndAging.Text = String.Empty
                    Me.txt2ndAgingName.Text = String.Empty
                    Me.txt3rdAging.Text = String.Empty
                    Me.txt3rdAgingName.Text = String.Empty
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
                dr(3) = 60
                dr(4) = "30_60"
                dr(5) = 90
                dr(6) = "60_90"
                dr(7) = 90
                dr(8) = "90+"
                dtData.Rows.Add(dr)
                dtData.WriteXml(Application.StartupPath & "\TemplateAgingBalance.xml")
                Me.txtFormatName.Text = String.Empty
                Me.txtAging.Text = String.Empty
                Me.txt1stAging.Text = String.Empty
                Me.txt1stAgingName.Text = String.Empty
                Me.txt2ndAging.Text = String.Empty
                Me.txt2ndAgingName.Text = String.Empty
                Me.txt3rdAging.Text = String.Empty
                Me.txt3rdAgingName.Text = String.Empty
                Me.btnSave.Text = "&Save"
                DialogResult = Windows.Forms.DialogResult.Yes
            Else
                dtData.WriteXml(Application.StartupPath & "\TemplateAgingBalance.xml")
                Me.txtFormatName.Text = String.Empty
                Me.txtAging.Text = String.Empty
                Me.txt1stAging.Text = String.Empty
                Me.txt1stAgingName.Text = String.Empty
                Me.txt2ndAging.Text = String.Empty
                Me.txt2ndAgingName.Text = String.Empty
                Me.txt3rdAging.Text = String.Empty
                Me.txt3rdAgingName.Text = String.Empty
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

            If IsOpenForm = True Then Me.txt3rdAgingName.Text = Me.txt3rdAging.Text.ToString & "+" 'Me.txt2ndAgingName.Text.Substring(Me.txt2ndAgingName.Text.LastIndexOf("_") + 1) & "_" & Me.txt3rdAging.Text
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class