Public Class frmMessageView
    Enum MsgDt
        MsgDetailID
        IDMsg
        ID_User
        Status
    End Enum
    Sub RecentControl()
        Me.txtUserID.Text = ""
        Me.txtMsgID.Text = ""
        Me.TextBox1.Text = ""
        Me.TextBox2.Text = ""
        Me.RichTextBox1.Text = ""
    End Sub
    Private Sub NewToolStripButton_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripButton.Click
        'FillGridEx(Me.grdMsgDetail, "SELECT DISTINCT Message_Detail.MsgDetailID, Message_Head.MsgID, Message_Head.MsgDate, Message_Detail.ID_User, tblSecurityUser.User_Name, Message_Head.MsgTitle, Message_Head.MsgDesc  FROM  Message_Head INNER JOIN Message_Detail ON Message_Head.MsgID = Message_Detail.IDMsg INNER JOIN tblSecurityUser ON Message_Detail.ID_User = tblSecurityUser.User_ID WHERE Message_Detail.ID_User = '" & LoginUserId & "' AND Message_Head.UserID <> '" & LoginUserId & "'", True)
        FillGridEx(Me.grdMsgDetail, "SELECT DISTINCT Message_Detail.MsgDetailID, Message_Head.MsgID, Message_Head.MsgDate, Message_Detail.ID_User, tblUser.User_Name, Message_Head.MsgTitle, Message_Head.MsgDesc  FROM  Message_Head INNER JOIN Message_Detail ON Message_Head.MsgID = Message_Detail.IDMsg INNER JOIN tblUser ON Message_Detail.ID_User = tblUser.User_ID WHERE Message_Detail.ID_User = '" & LoginUserId & "' AND Message_Head.UserID <> '" & LoginUserId & "'", True)
    End Sub
    Private Sub frmMessageView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RecentControl()
        'FillGridEx(Me.grdMsgDetail, "SELECT DISTINCT Message_Detail.MsgDetailID, Message_Head.MsgID, Message_Head.MsgDate, Message_Detail.ID_User, tblSecurityUser.User_Name, Message_Head.MsgTitle, Message_Head.MsgDesc  FROM  Message_Head INNER JOIN Message_Detail ON Message_Head.MsgID = Message_Detail.IDMsg INNER JOIN tblSecurityUser ON Message_Detail.ID_User = tblSecurityUser.User_ID WHERE Message_Detail.ID_User = '" & LoginUserId & "' AND Message_Head.UserID <> '" & LoginUserId & "'", True)
        FillGridEx(Me.grdMsgDetail, "SELECT DISTINCT Message_Detail.MsgDetailID, Message_Head.MsgID, Message_Head.MsgDate, Message_Detail.ID_User, tblUser.User_Name, Message_Head.MsgTitle, Message_Head.MsgDesc  FROM  Message_Head INNER JOIN Message_Detail ON Message_Head.MsgID = Message_Detail.IDMsg INNER JOIN tblUser ON Message_Detail.ID_User = tblUser.User_ID WHERE Message_Detail.ID_User = '" & LoginUserId & "' AND Message_Head.UserID <> '" & LoginUserId & "'", True)

    End Sub
    Public Sub FillGridEx(ByVal msgDetail As Janus.Windows.GridEX.GridEX, ByVal SqlStr As String, Optional ByVal RetrieveStucture As Boolean = False)
        Dim objdt As New DataTable
        Dim objadp As OleDb.OleDbDataAdapter
        objadp = New OleDb.OleDbDataAdapter(SqlStr, Con)
        objadp.Fill(objdt)
        msgDetail.DataSource = objdt
        If Not RetrieveStucture = False Then msgDetail.RetrieveStructure()
        Me.grdMsgDetail.RootTable.Columns(0).Caption = "DetailID"
        Me.grdMsgDetail.RootTable.Columns(1).Caption = "MsgID"
        Me.grdMsgDetail.RootTable.Columns(2).Caption = "Date"
        Me.grdMsgDetail.RootTable.Columns(3).Caption = "UserID"
        Me.grdMsgDetail.RootTable.Columns(4).Caption = "User Name"
        Me.grdMsgDetail.RootTable.Columns(5).Caption = "Subject"
        Me.grdMsgDetail.RootTable.Columns(6).Caption = "Message"
        Me.grdMsgDetail.RootTable.Columns(2).Width = 100
        Me.grdMsgDetail.RootTable.Columns(4).Width = 175
        Me.grdMsgDetail.RootTable.Columns(5).Width = 300
        Me.grdMsgDetail.RootTable.Columns(0).Visible = False
        Me.grdMsgDetail.RootTable.Columns(1).Visible = False
        Me.grdMsgDetail.RootTable.Columns(3).Visible = False
        Me.grdMsgDetail.RootTable.Columns(6).Visible = False
    End Sub
    Sub EditControl()
        Me.txtMsgID.Text = Me.grdMsgDetail.CurrentRow.Cells(1).Value
        Me.dtpMsgDate.Value = Me.grdMsgDetail.CurrentRow.Cells(2).Value
        Me.txtUserID.Text = Me.grdMsgDetail.CurrentRow.Cells(3).Value
        Me.TextBox1.Text = Me.grdMsgDetail.CurrentRow.Cells(5).Value
        Me.TextBox2.Text = Me.grdMsgDetail.CurrentRow.Cells(4).Value
        Me.RichTextBox1.Text = Me.grdMsgDetail.CurrentRow.Cells(6).Value
    End Sub
    Private Sub CutToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutToolStripButton.Click
        RecentControl()
    End Sub
    Private Sub grdMsgDetail_DoubleClick1(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdMsgDetail.DoubleClick
        EditControl()
    End Sub
End Class
