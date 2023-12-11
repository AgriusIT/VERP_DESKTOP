Imports SBDal
Public Class frmNotificationList
    Implements IGeneral


    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Sub EditRecord()
        Try
            If grdItems.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then

                frmNotificationConfiguration.NotificationId = grdItems.GetRow.Cells("NotificationId").Value
                frmNotificationConfiguration.ShowDialog()


            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try

            Me.grdItems.DataSource = GetDataTable("SELECT        tblNotificationCenterNotificationList.NotificationId, tblNotificationCenterNotificationList.EventId, tblNotificationCenterCategories.CategoryName +' >'+ tblNotificationCenterEvents.EventTitle as [NotificationEvent], tblNotificationCenterNotificationList.Description, tblNotificationCenterNotificationList.Active, tblNotificationCenterNotificationList.CreatedByUser, tblNotificationCenterNotificationList.CreatedDate " _
                                                & " FROM            tblNotificationCenterEvents INNER JOIN tblNotificationCenterCategories ON tblNotificationCenterEvents.CategoryId = tblNotificationCenterCategories.CategoryId INNER JOIN " _
                                                & " tblNotificationCenterNotificationList ON tblNotificationCenterEvents.EventId = tblNotificationCenterNotificationList.EventId ")

        Catch ex As Exception
            Throw ex
        End Try
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
    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles BtnNew.Click
        Try

            If frmNotificationConfiguration.ShowDialog() = True Then

                Me.GetAllRecords()

            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        Try
            GetAllRecords()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmNotificationList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            GetAllRecords()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub grdItems_LinkClicked(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdItems.LinkClicked
        Try
            If e.Column.Key = "Description" Then

                Me.EditRecord()

            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub BtnEdit_Click(sender As Object, e As EventArgs) Handles BtnEdit.Click
        Try

            Me.EditRecord()

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        Try
            If grdItems.GetRow.RowType = Janus.Windows.GridEX.RowType.Record AndAlso msg_Confirm("Do you want to delete the notification?") = True Then

                Dim dal As New NotificationConfigurationDAL

                If dal.DeleteNotification(Val(grdItems.GetRow.Cells("NotificationId").Value.ToString)) = True Then

                    Me.GetAllRecords()

                End If

            End If
        Catch ex As Exception
            msg_Error(ex.Message)

        End Try
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        Try

            Me.CtrlGrdBar1.mGridPrint.PerformClick()

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub
End Class