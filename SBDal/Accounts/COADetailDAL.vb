Imports System.Data
Imports System.Data.SqlClient
Imports SBModel
Public Class COADetailDAL
    Public Function Add(ByVal objModel As COADeail, ByVal SubSubId As Integer, Optional ByVal trans As SqlTransaction = Nothing) As Integer
        Try

            ''get Sub Sub Code
            Dim dtSubSub As DataTable = UtilityDAL.GetDataTable("SELECT sub_sub_code    FROM tblCOAMainSubSub   WHERE(main_sub_sub_id = " & SubSubId & ")", trans)
            Dim SubSubCode As String = String.Empty
            dtSubSub.AcceptChanges()
            If dtSubSub.Rows.Count > 0 Then
                SubSubCode = dtSubSub.Rows(0).Item(0).ToString
            End If
            ''get next Doc No
            'Dim DocNo As String = Microsoft.VisualBasic.Right(UtilityDAL.GetNextDocNo(SubSubCode, 11, "tblCOAMainSubSubDetail", "detail_code", trans), 5)
            Dim DocNo As String = Microsoft.VisualBasic.Right(UtilityDAL.GetNextDocNo(SubSubCode, (SubSubCode.Trim.Length + 1), "tblCOAMainSubSubDetail", "detail_code", trans, (SubSubCode.Trim.Length + 6)), 5)

            'insert into accounts
            Dim strSQL As String = "INSERT INTO [tblCOAMainSubsubDetail]([main_sub_sub_id], [Detail_code], [Detail_title], [Active]) " & _
                                    "VALUES( " & SubSubId & ", N'" & SubSubCode & "-" & DocNo & "', N'" & objModel.DetailTitle.Trim.Replace("'", "''") & "', " & IIf(objModel.Active = True, 1, 0) & ") Select @@Identity"
            If Not trans Is Nothing Then

                objModel.COADetailID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL))

            Else
                Dim Con As New SqlConnection(SQLHelper.CON_STR)
                Con.Open()
                Dim trans1 As SqlTransaction = Con.BeginTransaction()

                Try
                    objModel.COADetailID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans1, CommandType.Text, strSQL))
                    trans1.Commit()

                Catch ex As SqlException
                    trans1.Rollback()
                    Throw ex
                Catch ex As Exception
                    trans1.Rollback()
                    Throw ex
                Finally
                    Con.Close()
                End Try

            End If
            Return objModel.COADetailID
        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Update(ByVal objModel As COADeail, ByVal trans As SqlTransaction) As Boolean
        Try

            'insert into accounts
            Dim strSQL As String = "update tblCOAMainSubsubDetail set Detail_title = N'" & objModel.DetailTitle.Trim.Replace("'", "''") & _
            "' where coa_detail_id = " & objModel.COADetailID

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            Return True

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function GetAccountName(ByVal Title As String, ByVal AccountId As Integer) As Boolean

        Try

            Dim Exist As String = "False"

            Dim Query As String

            Query = "IF EXISTS (SELECT coa_detail_id FROM vwCOADetail where main_sub_sub_id = " & AccountId & " and detail_title = '" & Title & "') select 'True' as AsExist Else select 'False' as AsExist"

            Dim dt As DataTable

            dt = UtilityDAL.GetDataTable(Query)

            For Each row As DataRow In dt.Rows

                Exist = row.Item("AsExist")

            Next

            If (Exist = "True") Then

                Return True
            Else

                Return False

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
