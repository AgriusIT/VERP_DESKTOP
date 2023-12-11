Public Class frmComposeMessage
    Enum Msg
        MsgID
        MsgDate
        UserID
        MsgTitle
        MsgDesc
    End Enum
    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripButton.Click
        RecentControl()
    End Sub
    Sub RecentControl()
        Me.txtMsgID.Text = ""
        Me.dtpMsgDate.Value = Now.ToShortDateString
        Me.txtMsgTitle.Text = ""
        Me.rtbMsgDesc.Text = ""
        Me.SaveToolStripButton.Text = "&Save"
        Me.lstUsers.SelectedIndex = -1
    End Sub
    Sub EditControl()
        Me.txtMsgID.Text = Me.grdMessage.GetRow.Cells("MsgID").Value
        Me.dtpMsgDate.Value = Me.grdMessage.GetRow.Cells("MsgDate").Value
        Me.lstUsers.SelectedValue = Me.grdMessage.GetRow.Cells("UserID").Value
        Me.txtMsgTitle.Text = Me.grdMessage.GetRow.Cells("MsgTitle").Value
        Me.rtbMsgDesc.Text = Me.grdMessage.GetRow.Cells("MsgDesc").Value
        Me.SaveToolStripButton.Text = "&Update"
    End Sub
    Private Sub cmdSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Empty()
        SavenSend_Message()
    End Sub
    Sub SavenSend_Message()
        Dim ObjCm As New OleDb.OleDbCommand
        Dim ObjDt As New DataTable
        'Dim ObjAdp As OleDb.OleDbDataAdapter

    End Sub
    Sub UpdatenSend()
        Dim ObjCm As New OleDb.OleDbCommand
        Dim ObjDt As New DataTable
        'Dim ObjAdp As OleDb.OleDbDataAdapter
    End Sub
    Sub DeleteMsg()
        Dim ObjCm As New OleDb.OleDbCommand
        Dim ObjDt As New DataTable
        'Dim ObjAdp As OleDb.OleDbDataAdapter

    End Sub
    Public Sub FillUserList(ByVal lstUser As ListBox, ByVal SqlStr As String, Optional ByVal SelectedItem As Boolean = False)
        Dim ObjDt As New DataTable
        Dim ObjAdp As OleDb.OleDbDataAdapter
        ObjAdp = New OleDb.OleDbDataAdapter(SqlStr, Con)
        ObjAdp.Fill(ObjDt)
        lstUser.DataSource = ObjDt
        lstUser.DisplayMember = ObjDt.Columns(1).ToString
        lstUser.ValueMember = ObjDt.Columns(0).ToString
    End Sub
    Private Sub frmComposeMessage_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RecentControl()
        'FillUserList(Me.lstUsers, "Select User_ID, User_Name From tblSecurityUser WHERE User_ID <> '" & LoginUserId & "'", False)
        FillUserList(Me.lstUsers, "Select User_ID, User_Name From tblUser WHERE User_ID <> '" & LoginUserId & "'", False)
        FillGrdEx(Me.grdMessage, "select a.MsgId, a.MsgDate, a.UserId, b.User_Name, a.MsgTitle, a.MsgDesc From Message_Head a INNER JOIN tblUser b ON a.UserID = b.User_ID WHERE b.User_id='" & LoginUserId & "'", True)
        Me.dtpMsgDate.Value = Now.ToShortDateString
    End Sub
    Public Sub FillGrdEx(ByVal grdMsg As Janus.Windows.GridEX.GridEX, ByVal SqlStr1 As String, Optional ByVal RetrieveStucture As Boolean = False)
        Dim ObjDt As New DataTable
        Dim ObjAdp As OleDb.OleDbDataAdapter
        ObjAdp = New OleDb.OleDbDataAdapter(SqlStr1, Con)
        ObjAdp.Fill(ObjDt)
        grdMsg.DataSource = ObjDt
        If Not RetrieveStucture = False Then grdMsg.RetrieveStructure()
        grdMsg.RootTable.Columns(0).Caption = "ID"
        grdMsg.RootTable.Columns(1).Caption = "Date"
        grdMsg.RootTable.Columns(2).Caption = "UserID"
        grdMsg.RootTable.Columns(3).Caption = "User Name"
        grdMsg.RootTable.Columns(4).Caption = "Subject"
        grdMsg.RootTable.Columns(5).Caption = "Message"
        'grdMsg.RootTable.Columns(0).Visible = False
        grdMsg.RootTable.Columns(2).Visible = False
    End Sub
    Private Sub grdMessage_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdMessage.DoubleClick
        Me.lstUsers.SelectedIndex = -1
        EditControl()
        Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
    End Sub
    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Me.lstUsers.SelectedIndex = -1
        EditControl()
        Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
    End Sub
    Private Sub Empty()
        If Me.lstUsers.SelectedIndex = -1 Then
            ShowErrorMessage("Please Select a User")
            Me.lstUsers.Focus()
            Exit Sub
        End If
        If Me.txtMsgTitle.Text = "" Then
            ShowErrorMessage(" Empty Field!")
            Me.txtMsgTitle.Focus()
            Exit Sub
        End If
        If Me.rtbMsgDesc.Text = "" Then
            ShowErrorMessage("Empty Field!")
            Me.rtbMsgDesc.Focus()
            Exit Sub
        End If
    End Sub

End Class