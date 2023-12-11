Imports SBModel
Imports System.Data.SqlClient
Imports SBUtility.Utility
Public Class SystemConfigurationDAL



#Region "Local Functions and Procedures"

#End Region

#Region "Public Functions and Procedures"


    Public Shared Function GetAccountDetail(ByVal key As String, ByVal ObjDataTable As DataTable) As String

        Try

            Dim ObjDataView As DataView = GetFilterDataFromDataTable(ObjDataTable, " AccountID = N'" & key & "'")
            If Not ObjDataView Is Nothing Then

                If ObjDataView.Count > 0 Then
                    Dim dr As DataRowView = ObjDataView(0)
                    Return dr.Item("AccountCode").ToString()

                End If

            End If
            Return String.Empty

        Catch ex As Exception
            Throw ex
        End Try



        Return ""
    End Function


    Public Shared Function GetSystemConfigurationValue(ByVal Key As String) As String

        Try

            Dim ObjDataView As DataView = GetFilterDataFromDataTable(SystemConfigurationDAL.GetAll(), "[Configuration Name] = '" & key & "'")
            If Not ObjDataView Is Nothing Then
                If ObjDataView.Count > 0 Then
                    Dim dr As DataRowView = ObjDataView(0)
                    Return dr.Item("Configuration Value").ToString()
                End If

            End If
            Return String.Empty

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAll(Optional ByVal strCondition As String = "") As DataTable

        Dim objDA As SqlClient.SqlDataAdapter

        Try


            Dim strSQL As String
            strSQL = " SELECT config_no AS [Configuration No], config_name AS [Configuration Name], config_value AS [Configuration Value] " _
                   & " FROM tblGLConfiguration " _
                   & " ORDER BY [Configuration No] "

            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)
            Dim objDataTable As New DataTable("GLConfiguration")
            objDA.Fill(objDataTable)


            Return objDataTable

        Catch ex As SqlException
            Throw ex

        Catch ex As Exception
            Throw ex

        Finally
            objDA = Nothing

        End Try

    End Function

    Public Function GetBarCode(Optional ByVal strCondition As String = "") As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try

            Dim strSQL As String
            strSQL = "  SELECT template_product_barcode_id, 1 AS [Sr No], barcode_name [Barcode Name], image_name, rpt_file_name, with_size_color, is_active Active, left_margin [Left Margin], top_margin [Top Margin]  " _
            & " FROM tblTemplateProductBarcode " _
            & " WHERE (with_size_color = " & IIf(GetSystemConfigurationValue("product_code_only") = "False", 1, 0) & ")"


            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable(EnumHashTableKeyConstants.GetSystemConfigurationList.ToString())
            objDA.Fill(MyCollectionList)

            Return MyCollectionList

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
        End Try

    End Function
    Public Function Update(ByVal SysConfig As SystemConfiguration) As Boolean


        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()

        Dim trans As SqlTransaction = conn.BeginTransaction

        Dim objModelTemp As SystemConfiguration

        Try

            Dim strSQL As String

            For Each objModelTemp In SysConfig.SELECTEDRECORD_ARRAYLIST

                strSQL = " UPDATE tblGLConfiguration SET " _
                       & " config_value = '" & objModelTemp.Config_Value & "' " _
                       & " WHERE config_name = '" & objModelTemp.Config_Name & "'"

                
                ' Execute SQL 
                Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing))


            Next

            ' Commit Transaction .. 
            trans.Commit()

            ''Return
            Return True


        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
    Public Function getDetail_Code(ByVal strDetail_ID As String) As String
        Dim objDA As SqlClient.SqlDataAdapter
        Try


            Dim strSQL As String
            'Return Detail code of account
            strSQL = "select detail_code from tblGlCOAMainSubSubDetail where coa_detail_id='" & strDetail_ID & "'"

            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim objDt As New DataTable
            objDA.Fill(objDt)

            If objDt.Rows.Count > 0 Then
                getDetail_Code = objDt.Rows(0)("Detail_Code").ToString
            Else
                getDetail_Code = ""

            End If


        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
        End Try

    End Function

    Public Function Save_Customer_Signature(ByVal arrImage() As Byte) As Boolean


        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try



            Dim strSQL As String = "Update tblMushroomCommon set Membership_Signature = (@Picture)"

            ' A SqlCommand object is used to execute the SQL statement.
            Dim cmd As New SqlCommand(strSQL)
            Dim cmdParam(0) As SqlParameter
            cmdParam(0) = SQLHelper.CreateParameter("@Picture", SqlDbType.Image, arrImage)

            'With cmd
            '    .Parameters.Add(New SqlParameter("@Picture", SqlDbType.Image)).Value = arrImage

            'End With
            'cmd.Connection = conn
            'cmd.CommandText = strSQL
            'cmd.ExecuteNonQuery()


            '           strSQL = "Update tblMushroomCommon set Membership_Signature = '" & arrImage & "'"
            '
            ''Execute SQL 
            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, cmdParam))



            ''SQL Statement Log

            ''Activity Log  
            ''Commit Traction
            trans.Commit()

            ''Return
            Return True


        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function


#End Region

End Class
