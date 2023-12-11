'' 02-Jul-2014 Task:2715 Imran Ali Stricted Duplicate User Name In User Security (Ravi) 
''19-Oct-2015 Task: 191015 Muhammad Ameen: Put condition in case users is nothing then return false
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Security.Cryptography
'Imports Infragistics.Win.FormattedLinkLabel
Imports System.Net
Imports System.IO
Imports System.Net.Mail
Imports System.Text.RegularExpressions
Imports System.Drawing


Public Class UsersDAL

    Public Function add(ByVal users As Users) As Boolean
        If users Is Nothing Then ' 191015
            Return False
        End If
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        Dim trans As SqlTransaction = con.BeginTransaction
        Try
            If users.UserId > 0 Then
                update(users, trans)

                If Not users.UserCompanyRights.Count = 0 Then
                    Call New UserCompanyRightsDAL().Add(users.UserCompanyRights, trans)
                End If


                If Not users.UserLocationRights.Count = 0 Then
                    Call New UserLocationRightsDAL().Add(users.UserLocationRights, trans)
                End If

                If Not users.UserAccountRights.Count = 0 Then
                    Call New UserAccountRightsDAL().Add(users.UserAccountRights, trans)
                End If

                If Not users.UserAccountRights.Count = 0 Then
                    Call New UserCostCentreRightsDAL().Add(users.UserCostCentreRights, trans)
                End If
                
                ''TASK:988 Display rights based voucher types on voucher entry. Ameen on 22-06-2017 
                If Not users.UserVoucherTypesRights.Count = 0 Then
                    Call New UserVoucherTypesRightsDAL().Add(users.UserVoucherTypesRights, trans)
                End If
                '' END TASK:988 
                'TFS1751: Waqar Raza
                'Start TFS151
                If Not users.UserPOSRights.Count = 0 Then
                    Call New UserPOSRightsDAL().Add(users.UserPOSRights, trans)
                End If
                'End TFS1751
                
            Else
                'Dim _strqueery As String = "insert into tblUser(User_Code ,User_Name, [Password], Email, Block, GroupId, Active,DashBoardRights) values (N'" & users.UserCode & "',N'" & users.UserName & "', N'" & users.Password & "', N'" & users.Email & "', " & IIf(users.Block = True, 1, 0) & ", " & users.GroupId & ",1, " & IIf(users.DashBoardRights = True, 1, 0) & ") Select @@Identity"
                'users.UserId = SQLHelper.ExecuteScaler(trans, CommandType.Text, _strqueery)

                'Task:2715 Check User
                Dim strchkUser As String = String.Empty
                strchkUser = "Select User_Id From tblUser WHERE User_Code=N'" & users.UserCode & "'"
                Dim intchkDuplicateUser As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, strchkUser)
                If intchkDuplicateUser > 0 Then
                    Throw New Exception("User already exists.")
                End If
                'End Task:2715

                'Dim _strqueery As String = "insert into tblUser(User_Code ,User_Name, [Password], Email, Block, GroupId, Active,DashBoardRights,RefUserId) values (N'" & users.UserCode & "',N'" & users.UserName & "', N'" & users.Password & "', N'" & users.Email & "', " & IIf(users.Block = True, 1, 0) & ", " & users.GroupId & ",1, " & IIf(users.DashBoardRights = True, 1, 0) & ", " & users.RefUserId & ") Select @@Identity"
                'users.UserId = SQLHelper.ExecuteScaler(trans, CommandType.Text, _strqueery)
                Dim _strqueery As String = "insert into tblUser(User_Code ,User_Name, [Password], Email, Block, GroupId, Active,DashBoardRights,RefUserId,FullName, User_Picture, ShowCostPriceRights, EmployeeId) values (N'" & users.UserCode & "',N'" & users.UserName & "', N'" & users.Password & "', N'" & users.Email & "', " & IIf(users.Block = True, 1, 0) & ", " & users.GroupId & ",1, " & IIf(users.DashBoardRights = True, 1, 0) & ", " & users.RefUserId & ",'" & users.FullName.Replace("'", "''") & "', N'" & users.UserPicture & "', " & IIf(users.ShowCostPriceRights = True, 1, 0) & ", " & users.EmployeeId & ") Select @@Identity"
                users.UserId = SQLHelper.ExecuteScaler(trans, CommandType.Text, _strqueery)

                Dim str As String = "INSERT INTO tblUserRights(User_Id, Form_Id, View_Rights, Save_Rights, Update_Rights) Select " & users.UserId & ", Form_Id,1,1,1 from tblForm"
                SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
                If users.UserCompanyRights IsNot Nothing Then
                    If users.UserCompanyRights.Count > 0 Then
                        users.UserCompanyRights.Item(0).User_Id = users.UserId
                        Call New UserCompanyRightsDAL().Add(users.UserCompanyRights, trans)
                    End If
                End If

                If users.UserLocationRights IsNot Nothing Then
                    If users.UserLocationRights.Count > 0 Then
                        users.UserLocationRights.Item(0).UserID = users.UserId
                        Call New UserLocationRightsDAL().Add(users.UserLocationRights, trans)
                    End If
                End If

                If users.UserAccountRights IsNot Nothing Then
                    If users.UserAccountRights.Count > 0 Then
                        users.UserAccountRights.Item(0).UserID = users.UserId
                        Call New UserAccountRightsDAL().Add(users.UserAccountRights, trans)
                    End If
                End If

                ''TASK:988 Display rights based voucher types on voucher entry. Ameen on 22-06-2017 
                If users.UserVoucherTypesRights IsNot Nothing Then
                    If users.UserVoucherTypesRights.Count > 0 Then
                        users.UserVoucherTypesRights.Item(0).UserId = users.UserId
                        Call New UserVoucherTypesRightsDAL().Add(users.UserVoucherTypesRights, trans)
                    End If
                End If
                ''END TASK:988

                'TFS1751: Waqar Raza
                'Start TFS1751
                If users.UserPOSRights IsNot Nothing Then
                    If users.UserPOSRights.Count > 0 Then
                        users.UserPOSRights.Item(0).UserId = users.UserId
                        Call New UserPOSRightsDAL().Add(users.UserPOSRights, trans)
                    End If
                End If
                'End TFS1751
            End If


            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            con.Close()
        End Try
    End Function
    Public Function update(ByVal users As Users, ByVal trans As SqlTransaction) As Boolean
        Try
            'Dim _strquery As String = "update tblUser set User_Code = N'" & users.UserCode & "', User_Name=N'" & users.UserName & "', [Password]=N'" & users.Password & "', Email = N'" & users.Email & "', Block=" & IIf(users.Block = True, 1, 0) & ", GroupId = N'" & users.GroupId & "', Active = 1, DashBoardRights=" & IIf(users.DashBoardRights = True, 1, 0) & "where User_Id = " & users.UserId & ""
            'Dim _strquery As String = "update tblUser set User_Code = N'" & users.UserCode & "', User_Name=N'" & users.UserName & "', [Password]=N'" & users.Password & "', Email = N'" & users.Email & "', Block=" & IIf(users.Block = True, 1, 0) & ", GroupId = N'" & users.GroupId & "', Active = 1, DashBoardRights=" & IIf(users.DashBoardRights = True, 1, 0) & ",RefUserId=" & users.RefUserId & " where User_Id = " & users.UserId & ""
            Dim _strquery As String = "update tblUser set User_Code = N'" & users.UserCode & "', User_Name=N'" & users.UserName & "', [Password]=N'" & users.Password & "', Email = N'" & users.Email & "', Block=" & IIf(users.Block = True, 1, 0) & ", GroupId = N'" & users.GroupId & "', Active = 1, DashBoardRights=" & IIf(users.DashBoardRights = True, 1, 0) & ",RefUserId=" & users.RefUserId & ",FullName='" & users.FullName.Replace("'", "''") & "', User_Picture= '" & users.UserPicture.Replace("'", "''") & "', ShowCostPriceRights= " & IIf(users.ShowCostPriceRights = True, 1, 0) & ", ShowMainMenuRights= " & IIf(users.ShowMainMenuRights = True, 1, 0) & ", EmployeeId = " & users.EmployeeId & " where User_Id = " & users.UserId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, _strquery)
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
        End Try
    End Function
    Public Function Delete(ByVal users As Users) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then
            con.Open()

        End If
        Dim trans As SqlTransaction = con.BeginTransaction
        Try

            Dim _strQuery As String
            _strQuery = "delete from tblUser where User_Id = N'" & users.UserId & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, _strQuery)

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAllRecordByGroupId(ByVal GroupId As Integer) As DataTable
        Try
            Dim _strquery As String = "Select User_Id, User_Name, FullName, Email from tblUser WHERE ISNULL(GroupId,1)=" & GroupId
            Dim dt As DataTable = UtilityDAL.GetDataTable(_strquery)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetGroupsUsers(ByVal Groups As String) As DataTable
        Try
            Dim _strquery As String = "Select User_Id, User_Name, FullName, Email from tblUser WHERE ISNULL(GroupId,1) In (" & Groups & ")"
            Dim dt As DataTable = UtilityDAL.GetDataTable(_strquery)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAllRecordGroup() As DataTable
        Try
            Dim _strquery As String = "Select User_Id, User_Name, FullName, Email from tblUser WHERE Active = 1 ORDER BY SortOrder"
            Dim dt As DataTable = UtilityDAL.GetDataTable(_strquery)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function grouplist() As DataTable
        Try
            Dim _strquery As String = "select GroupId ,groupname from tblusergroup"
            Dim dt As DataTable = UtilityDAL.GetDataTable(_strquery)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function DisplayUser(ByVal UserId As Integer) As DataTable
        Dim dt As New DataTable
        Try
            ''TASK TFS4867 : Addition of EmployeeId
            Dim str As String = "Select User_Id, User_Name, User_Code, [Password], Email,  ISNULL(Block,0) as Block, ISNULL(GroupId,0) as GroupId, ISNULL(DashBoardRights,0) as DashBoardRights, IsNull(RefUserId,0) as RefUserId,FullName, User_Picture, Convert(image, Null) As UserImage, ISNULL(ShowCostPriceRights,0) As ShowCostPriceRights, ISNULL(ShowMainMenuRights,0) As ShowMainMenuRights, ISNULL(EmployeeId, 0) AS EmployeeId From tblUser WHERE User_Id=" & UserId & ""

            dt = UtilityDAL.GetDataTable(str)
            'For Each r As DataRow In dt.Rows
            '    If IO.File.Exists(r.Item("User_Picture")) Then
            '        LoadPicture(r, "UserImage", r.Item("User_Picture"))
            '    End If
            'Next
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Private Sub LoadPicture(ByVal ObjDataRow As DataRow, ByVal strImageField As String, ByVal FilePath As String)
    '    Try
    '        If IO.File.Exists(FilePath) Then
    '            If FilePath.Length > 0 Then

    '                Dim objImage As Image = Image.FromFile(FilePath)
    '                objImage = SizeImage(objImage, 400, 600)
    '                Dim ms As New MemoryStream
    '                objImage.Save(ms, Imaging.ImageFormat.Png)
    '                Dim oBytes() As Byte = ms.ToArray
    '                objImage.Dispose()
    '                ms.Close()
    '                'Dim fs As IO.FileStream = New IO.FileStream(FilePath, IO.FileMode.Open, IO.FileAccess.Read)
    '                'Dim OImage As Byte() = New Byte(fs.Length) {}
    '                'fs.Read(OImage, 0, fs.Length)
    '                ObjDataRow(strImageField) = oBytes
    '                'fs.Flush()
    '                'fs.Dispose()
    '                'fs.Close()
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Sub
End Class


