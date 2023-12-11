Imports System.Windows.Forms
Imports SBDal
Public Class frmSalesInquiryUserSelection

    Property OldUsers As String
    Property OldGroups As String


    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmSalesInquiryUserSelection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FillCombos("All")

            If OldGroups.Length > 0 Then
                Me.lstUserGroups.SelectItemsByIDs(OldGroups)
            End If

            If OldUsers.Length > 0 Then
                Me.lstUsers.SelectItemsByIDs(OldUsers)
            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Sub FillCombos(Optional Condition As String = "")
        Try

            If Condition = "Groups" Or Condition = "All" Then

                Dim dtUSerGroup As DataTable = New UserGroupDAL().Getallrecord()
                dtUSerGroup.AcceptChanges()
                Me.lstUserGroups.ListItem.ValueMember = dtUSerGroup.Columns(0).ColumnName.ToString
                Me.lstUserGroups.ListItem.DisplayMember = dtUSerGroup.Columns(1).ColumnName.ToString
                Me.lstUserGroups.ListItem.DataSource = dtUSerGroup
                Me.lstUserGroups.DeSelect()

            End If

            If Condition = "Users" Or Condition = "All" Then

                Dim dtUser As New DataTable
                dtUser = New UsersDAL().GetAllRecordGroup
                For Each dr As DataRow In dtUser.Rows
                    dr.BeginEdit()
                    dr.Item("User_Name") = Decrypt(dr.Item("User_Name"))
                    dr.EndEdit()
                Next
                dtUser.AcceptChanges()
                Me.lstUsers.ListItem.ValueMember = dtUser.Columns(0).ColumnName.ToString
                Me.lstUsers.ListItem.DisplayMember = dtUser.Columns(1).ColumnName.ToString
                Me.lstUsers.ListItem.DataSource = dtUser

                Me.lstUsers.DeSelect()

            End If

      
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
