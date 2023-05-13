'Rafay:created this screen to show detail report
Imports SBDal
Imports SBModel
Imports System.Data.OleDb


Public Class frmTicketDetail
    Implements IGeneral


    Private Sub grd_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs)

    End Sub
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            'Changes added by Murtaza Ahmad(11/21/2022)
            Dim str As String
            'str = "select TM.TicketNo,TM.TicketDate,TM.SerialNo,CMT.ContractNo,TM.CallerName,TM.CustomerName,TM.CompanyName,TM.ContactVia,TM.ContactTime,TM.Severity,Item.Brand,Item.ModelNo,TM.InitialProblem,TM.Status,TM.ContractId,TD.TicketId,TD.OnsiteEngineer,TD.TimeOnSite,TD.ActivityStart,TD.ActivityEnd,TD.TicketState,TD.PNUsed,TD.Partno,CMT.ContractId AS Id,CMT.SLAType,CMT.Site  from   TicketMasterTable TM LEFT OUTER JOIN TicketDetailTable TD ON TM.TicketId = TD.TicketId LEFT OUTER JOIN " & _
            '"ContractMasterTable CMT ON TM.ContractId = CMT.ContractId LEFT OUTER JOIN " & _
            '"(Select distinct CDT.ContractId, CDT.Brand,CDT.ModelNo from ContractDetailTable CDT) AS Item ON CMT.ContractId = Item.ContractId ORDER BY TM.TicketId DESC"
            str = "TicketDetailReport'" & rdbpartwisereport.Checked & "'"
            'Changes added by Murtaza Ahmad(11/21/2022)
            Dim dt As DataTable
            dt = GetDataTable(str)
            'grd.RootTable.Columns("ContractId").Visible = False
            'grd.RootTable.Columns("TicketId").Visible = False
            'grd.RootTable.Columns("Id").Visible = False
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()

            'grdSaved.RootTable.Columns("ContractId").Visible = False
            ' grdSaved.RootTable.Columns("ContractId1").Visible = False
            'grdSaved.RootTable.Columns("Id").Visible = False
            'grdSaved.RootTable.Columns("TicketId").Visible = False
            'grdSaved.RootTable.Columns("TicketNo").Width = 150
            'grdSaved.RootTable.Columns("ContractNo").Width = 100
            'Me.grdSaved.RootTable.Columns("TimeOnSite").FormatString = "h:mm:ss tt"
            'Me.grdSaved.RootTable.Columns("ActivityStart").FormatString = "h:mm:ss tt"
            'Me.grdSaved.RootTable.Columns("ActivityEnd").FormatString = "h:mm:ss tt"
            'grd.RootTable.Columns("Id").Visible = False
            'ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
       
    End Sub


    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        '    Try
        '        If LoginGroup = "Administrator" Then
        '            Me.Visible = True
        '            DoHaveSaveRights = True
        '            DoHaveUpdateRights = True
        '            DoHaveDeleteRights = True
        '            Me.CtrlGrdBar1.mGridPrint.Enabled = True
        '            Me.CtrlGrdBar1.mGridExport.Enabled = True
        '            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
        '            Exit Sub
        '        End If
        '        If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
        '            If RegisterStatus = EnumRegisterStatus.Expired Then
        '                Me.Visible = False
        '                DoHaveSaveRights = False
        '                DoHaveUpdateRights = False
        '                DoHaveDeleteRights = False
        '                Me.CtrlGrdBar1.mGridPrint.Enabled = False
        '                Me.CtrlGrdBar1.mGridExport.Enabled = False
        '                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
        '                Exit Sub
        '            End If
        '        Else
        '            Me.Visible = False
        '            DoHaveSaveRights = False
        '            DoHaveUpdateRights = False
        '            DoHaveDeleteRights = False
        '            Me.CtrlGrdBar1.mGridPrint.Enabled = False
        '            Me.CtrlGrdBar1.mGridExport.Enabled = False
        '            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
        '            For Each RightsDt As GroupRights In Rights
        '                If RightsDt.FormControlName = "View" Then
        '                    Me.Visible = True
        '                ElseIf RightsDt.FormControlName = "Print" Then
        '                    Me.CtrlGrdBar1.mGridPrint.Enabled = True
        '                ElseIf RightsDt.FormControlName = "Export" Then
        '                    Me.CtrlGrdBar1.mGridExport.Enabled = True
        '                ElseIf RightsDt.FormControlName = "Field Chooser" Then
        '                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
        '                ElseIf RightsDt.FormControlName = "Save" Then
        '                    DoHaveSaveRights = True
        '                ElseIf RightsDt.FormControlName = "Update" Then
        '                    DoHaveUpdateRights = True
        '                ElseIf RightsDt.FormControlName = "Done" Then
        '                    DoHaveDeleteRights = True
        '                End If
        '            Next
        '        End If
        '    Catch ex As Exception
        '        Throw ex
        '    End Try
    End Sub


    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub grd_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs)

    End Sub

    Private Sub frmTicketDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'GetAllRecords()
        GetSecurityRights()
    End Sub

    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                ''  Me.btnSave.Enabled = True
                'Rafay:give rights to admin to view print ,export,choosefielder 
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                '' DoHaveDeleteRights = True
                'Rafay:Task End
                Exit Sub
            End If
            Me.Visible = False
            'Me.btnSave.Enabled = False
            'Rafay:Task Start
            Me.CtrlGrdBar1.mGridPrint.Enabled = False
            Me.CtrlGrdBar1.mGridExport.Enabled = False
            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
            'Rafay:Task End
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Print" Then
                    CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Export" Then
                    CtrlGrdBar1.mGridExport.Enabled = True

                End If

                'Rafay:Task End
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            ' CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            'Dim ClosingDate As DateTime = Convert.ToDateTime(getConfigValueByType("EndOfDate").ToString)
            'Dim PreviouseRecordShow As Boolean = Convert.ToBoolean(getConfigValueByType("PreviouseRecordShow").ToString)

            Dim str As String = String.Empty
            str = "TicketDetailReport'" & rdbpartwisereport.Checked & "','" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "','" & Me.dtpTo.Value.ToString("yyyy-M-d 00:00:00") & "'"

            Dim dt As DataTable
            dt = GetDataTable(str)
            'grd.RootTable.Columns("ContractId").Visible = False
            'grd.RootTable.Columns("TicketId").Visible = False
            'grd.RootTable.Columns("Id").Visible = False
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()

            'grdSaved.RootTable.Columns("ContractId").Visible = False
            '' grdSaved.RootTable.Columns("ContractId1").Visible = False
            'grdSaved.RootTable.Columns("Id").Visible = False
            'grdSaved.RootTable.Columns("TicketId").Visible = False
            grdSaved.RootTable.Columns("TicketNo").Width = 150
            'grdSaved.RootTable.Columns("ContractNo").Width = 100
            'grdSaved.RootTable.Columns("TimeOnSite").FormatString = "h:mm:ss tt"
            'grdSaved.RootTable.Columns("ActivityStart").FormatString = "h:mm:ss tt"
            'grdSaved.RootTable.Columns("ActivityEnd").FormatString = "h:mm:ss tt"
            'grd.RootTable.Columns("Id").Visible = False
            'ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnLoadAll.Click
        Try
            GetAllRecords()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub rdbdetailreport_CheckedChanged(sender As Object, e As EventArgs) Handles rdbdetailreport.CheckedChanged

    End Sub
End Class