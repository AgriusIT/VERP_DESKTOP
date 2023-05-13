Imports SBModel
Imports SBDal
Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class COADAL
    Enum enmCOA
        coa_main_id
        main_sub_id
        main_sub_sub_id
        coa_detail_id
        detail_code
        detail_title
        account_type
        DrBS_Note_Id
        CrBS_Note_Id
        PL_Note_Id
        DrBS_Note_Title
        CrBS_Note_Title
        PL_Note_Title
        Note_Type
        Mobile
        Email
    End Enum
    Public Function GetCOAList() As List(Of COABE)
        Try
            Dim Query As String = String.Empty
            Query = "Select coa_main_id, main_sub_id, main_sub_sub_id, coa_detail_id, detail_code, detail_title, account_type, DrBS_Note_Id, CrBS_Note_Id, PL_Note_Id, DrBS_Note_Title, CrBS_Note_Title, PL_Note_Title, Note_Type, Contact_Mobile as Mobile, Contact_Email as Email From vwCOADetail WHERE detail_title <> '' AND Active=1 ORDER BY detail_title ASC"
            Dim COALIST As New List(Of COABE)
            Dim dr As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, Query, Nothing)
            Dim COA As COABE

            If dr.HasRows Then
                Do While dr.Read
                    COA = New COABE
                    COA.coa_main_id = Val(dr.GetValue(enmCOA.coa_main_id).ToString)
                    COA.main_sub_id = Val(dr.GetValue(enmCOA.main_sub_id).ToString)
                    COA.main_sub_sub_id = Val(dr.GetValue(enmCOA.main_sub_sub_id).ToString)
                    COA.coa_detail_id = Val(dr.GetValue(enmCOA.coa_detail_id).ToString)
                    COA.detail_code = dr.GetValue(enmCOA.detail_code).ToString
                    COA.detail_title = dr.GetValue(enmCOA.detail_title).ToString
                    COA.account_type = dr.GetValue(enmCOA.account_type).ToString
                    COA.DrBS_Note_Id = Val(dr.GetValue(enmCOA.DrBS_Note_Id).ToString)
                    COA.CrBS_Note_Id = Val(dr.GetValue(enmCOA.CrBS_Note_Id).ToString)
                    COA.PL_note_id = Val(dr.GetValue(enmCOA.PL_Note_Id).ToString)
                    COA.DrBS_Note_Title = dr.GetValue(enmCOA.DrBS_Note_Title).ToString
                    COA.CrBS_Note_Title = dr.GetValue(enmCOA.CrBS_Note_Title).ToString
                    COA.PL_Note_Title = dr.GetValue(enmCOA.PL_Note_Title).ToString
                    COA.note_type = dr.GetValue(enmCOA.Note_Type).ToString
                    COA.Mobile = dr.GetValue(enmCOA.Mobile).ToString
                    COA.Email = dr.GetValue(enmCOA.Email).ToString
                    COALIST.Add(COA)
                Loop
            End If
            Return COALIST
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
