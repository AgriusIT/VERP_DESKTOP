Imports System
Imports Janus.Windows.GridEX
Imports Janus.Windows.EditControls
Public Class frmMyCompany
    Dim MyCompId As Integer = 0I
    Public CompanyId As Integer = 0I
    Public Function getMyCompany(ByVal Company As SBModel.UserCompanyRightsBE) As Boolean
        Try
            If Company.User_Id = LoginUserId Then
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmMyCompany_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If Me.FlowLayoutPanel1.Controls.Count > 0 Then
                Me.FlowLayoutPanel1.Controls.Clear()
            End If
            ''R912 Comments Line
            'MyCompanyRightsList = CompanyRightsList.FindAll(AddressOf getMyCompany)
            If MyCompanyRightsList IsNot Nothing Then
                If MyCompanyRightsList.Count > 0 Then
                    Dim MyUiButton As Janus.Windows.EditControls.UIButton
                    'For i As Integer = 0 To MyCompanyRightsList.Count - 1
                    For i As Integer = 0 To CompanyRightsList.Count - 1
                        Dim MyComp As New SBModel.CompanyInfo
                        MyCompId = MyCompanyRightsList.Item(i).CompanyId
                        MyComp = CompanyList.Find(AddressOf GetCompanyName)
                        MyUiButton = New Janus.Windows.EditControls.UIButton
                        MyUiButton.Text = MyComp.CompanyName
                        MyUiButton.Name = "btn" & MyComp.CompanyName & "@" & MyComp.CompanyID & ""
                        MyUiButton.Size = New Size(120, 80)
                        MyUiButton.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
                        AddHandler MyUiButton.Click, AddressOf GetCompanyId
                        Me.FlowLayoutPanel1.Controls.Add(CType(MyUiButton, Janus.Windows.EditControls.UIButton))
                    Next
                Else
                    Me.Close()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function GetCompanyName(ByVal Company As SBModel.CompanyInfo) As Boolean
        Try
            If Company.CompanyID = MyCompId Then
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Private Sub GetCompanyId(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim uiButon As Janus.Windows.EditControls.UIButton = CType(sender, Janus.Windows.EditControls.UIButton)
            Dim strId As String = uiButon.Name
            CompanyId = strId.Substring(strId.LastIndexOf("@") + 1)
            DialogResult = Windows.Forms.DialogResult.Yes
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

End Class