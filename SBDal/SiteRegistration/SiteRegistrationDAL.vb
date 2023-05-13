Imports SBDal
Imports SBModel
Imports SBUtility
Imports System
Imports System.Data
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports SBUtility.Utility
Public Class SiteRegistrationDAL

    Public Function Save(ByVal Site As SiteRegisrationBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty


            If Site IsNot Nothing Then

                strSQL = "INSERT INTO SiteRegistrationTable(Registration_No,Registration_Date,ProjectId,Region_Id,CityID,Area_ID,Location,Area_Category,Site_Type,Clutter_Info,Singnal_Info,Visibility_Distance,Traffic_Coming_From,Traffic_Going_To,Traffic_Per_Day,Size_Width,Size_Height,Longitude,Latitude,Sided,Authority,RA,Owner_Name,Owner_Address,Owner_CNIC_No,BankAc1,BankAc2,Availability_Date,UserId,User_Name,EntryDate) " _
                & " VALUES(N'" & Site.Registration_No.Replace("'", "''") & "', Convert(Datetime,N'" & Site.Registration_Date & "',102), " & Site.ProjectId & ", " & Site.Region_ID & ", " & Site.CityID & ", " & Site.Area_ID & ", N'" & Site.Location.Replace("'", "''") & "', N'" & Site.Area_Category.Replace("'", "''") & "', " _
                & " N'" & Site.Site_Type.Replace("'", "''") & "', N'" & Site.Clutter_Info.Replace("'", "''") & "', N'" & Site.Singnal_Info.Replace("'", "''") & "', N'" & Site.Visibility_Distance & "', N'" & Site.Traffic_Coming_From.Replace("'", "''") & "', N'" & Site.Traffic_Going_To.Replace("'", "''") & "', N'" & Site.Traffic_Per_Day & "', N'" & Site.Size_Width.Replace("'", "''") & "', N'" & Site.Size_Height.Replace("'", "''") & "', " _
                & " N'" & Site.Longitude.Replace("'", "''") & "', N'" & Site.Latitude.Replace("'", "''") & "', N'" & Site.Sided.Replace("'", "''") & "', N'" & Site.Authority.Replace("'", "''") & "',N'" & Site.RA.Replace("'", "''") & "', N'" & Site.Owner_Name.Replace("'", "''") & "', N'" & Site.Owner_Address.Replace("'", "''") & "', N'" & Site.Owner_CNIC_No.Replace("'", "''") & "', N'" & Site.Bank_Ac_No1.Replace("'", "''") & "', N'" & Site.Bank_Ac_No2.Replace("'", "''") & "', Convert(Datetime, N'" & Site.Availability_Date & "',102), N'" & Site.UserId & "', N'" & Site.UserName.Replace("'", "''") & "', Convert(datetime, N'" & Site.EntryDate & "',102))Select @@Identity"
                Site.SiteRegistrationID = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)

                SaveDetail(Site, trans)
            Else
                Throw New Exception("Some of data is not provided.")
                Return False
            End If


            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function Update(ByVal Site As SiteRegisrationBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty


            If Site IsNot Nothing Then

                strSQL = "UPDATE SiteRegistrationTable SET Registration_No=N'" & Site.Registration_No.Replace("'", "''") & "'," _
                 & "  Registration_Date=Convert(Datetime,N'" & Site.Registration_Date & "',102)," _
                 & "  ProjectId=" & Site.ProjectId & ", " _
                 & "  Region_Id=" & Site.Region_ID & ", " _
                 & "  CityID=" & Site.CityID & ", " _
                 & "  Area_ID=" & Site.Area_ID & ", " _
                 & "  Location=N'" & Site.Location.Replace("'", "''") & "', " _
                 & "  Area_Category=N'" & Site.Area_Category.Replace("'", "''") & "', " _
                 & "  Site_Type=N'" & Site.Site_Type.Replace("'", "''") & "', " _
                 & "  Clutter_Info=N'" & Site.Clutter_Info.Replace("'", "''") & "', " _
                 & "  Singnal_Info=N'" & Site.Singnal_Info.Replace("'", "''") & "', " _
                 & "  Visibility_Distance=N'" & Site.Visibility_Distance & "', " _
                 & "  Traffic_Coming_From=N'" & Site.Traffic_Coming_From.Replace("'", "''") & "', " _
                 & "  Traffic_Going_To=N'" & Site.Traffic_Going_To.Replace("'", "''") & "', " _
                 & "  Traffic_Per_Day=N'" & Site.Traffic_Per_Day & "',  " _
                 & "  Size_Width=N'" & Site.Size_Width.Replace("'", "''") & "', " _
                 & "  Size_Height=N'" & Site.Size_Height.Replace("'", "''") & "', " _
                 & "  Longitude=N'" & Site.Longitude.Replace("'", "''") & "', " _
                 & "  Latitude=N'" & Site.Latitude.Replace("'", "''") & "', " _
                 & "  Sided=N'" & Site.Sided.Replace("'", "''") & "', " _
                 & "  Authority=N'" & Site.Authority.Replace("'", "''") & "', " _
                 & "  RA=N'" & Site.RA.Replace("'", "''") & "', " _
                 & "  Owner_Name=N'" & Site.Owner_Name.Replace("'", "''") & "', " _
                 & "  Owner_Address=N'" & Site.Owner_Address.Replace("'", "''") & "', " _
                 & "  Owner_CNIC_No=N'" & Site.Owner_CNIC_No.Replace("'", "''") & "', " _
                 & "  BankAc1=N'" & Site.Bank_Ac_No1.Replace("'", "''") & "', " _
                 & "  BankAc2=N'" & Site.Bank_Ac_No2.Replace("'", "''") & "', " _
                 & "  Availability_Date=Convert(Datetime, N'" & Site.Availability_Date & "',102), " _
                 & "  UserId=N'" & Site.UserId & "', " _
                 & "  User_Name= N'" & Site.UserName.Replace("'", "''") & "', " _
                 & "  EntryDate= Convert(datetime, N'" & Site.EntryDate & "',102) WHERE SiteRegistrationID=" & Site.SiteRegistrationID & ""

                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                strSQL = ""
                strSQL = "Delete From SiteRegistrationCostDetailTable WHERE SiteRegistrationID=" & Site.SiteRegistrationID & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                SaveDetail(Site, trans)
            Else
                Throw New Exception("some of data is not provided.")
                Return False
            End If




            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function SaveDetail(ByVal Site As SiteRegisrationBE, ByVal trans As SqlTransaction) As Boolean
        Try

            Dim strSQL As String = String.Empty
            If Site IsNot Nothing Then
                If Site.SiteRegistrationCostDetail.Count > 0 Then
                    For Each objSiteCost As SiteRegistrationCostDetailBE In Site.SiteRegistrationCostDetail
                        strSQL = ""
                        strSQL = "INSERT INTO SiteRegistrationCostDetailTable(SiteRegistrationID,coa_detail_id,Amount,Tenure_Start,Tenure_End,Payee_Name,Payment_Term,Remarks)" _
                        & " VALUES(" & Site.SiteRegistrationID & ", " & objSiteCost.coa_detail_id & ", " & objSiteCost.Amount & ", Convert(datetime, N'" & objSiteCost.Tenure_Start & "', 102),Convert(datetime, N'" & objSiteCost.Tenure_End & "', 102), N'" & objSiteCost.Payee_Name.Replace("'", "''") & "', N'" & objSiteCost.Payment_Term.Replace("'", "''") & "', N'" & objSiteCost.Remarks.Replace("'", "''") & "')"
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                    Next
                End If
            End If
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function Delete(ByVal Site As SiteRegisrationBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty


            strSQL = ""
            strSQL = "Delete From SiteRegistrationCostDetailTable WHERE SiteRegistrationID=" & Site.SiteRegistrationID & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            strSQL = ""
            strSQL = "Delete From SiteRegistrationTable WHERE SiteRegistrationID=" & Site.SiteRegistrationID & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)


            'strSQL = ""
            'strSQL = "Delete From SiteRegistrationDocumentsTable WHERE SiteRegistrationID=" & Site.SiteRegistrationID & ""
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function GetAllRecords(Optional ByVal Condition As String = "") As DataTable
        Try
            Dim strSQL As String = String.Empty
            strSQL = "SELECT " & IIf(Condition = "All", "", "Top 50") & "  SiteReg.SiteRegistrationID, SiteReg.Registration_No, SiteReg.Registration_Date, Region.StateName as Region, City.CityName as City, Territory.TerritoryName as Area, SiteReg.Location, " _
                     & " SiteReg.Area_Category, SiteReg.Site_Type, SiteReg.Clutter_Info, SiteReg.Singnal_Info, SiteReg.Visibility_Distance, SiteReg.Traffic_Coming_From,  " _
                     & " SiteReg.Traffic_Going_To, SiteReg.Traffic_Per_Day, SiteReg.Size_Width, SiteReg.Size_Height, SiteReg.Longitude, SiteReg.Latitude, SiteReg.Sided,  " _
                     & " SiteReg.Authority, SiteReg.RA,SiteReg.Owner_Name, SiteReg.Owner_Address, SiteReg.Owner_CNIC_No, SiteReg.BankAc1, SiteReg.BankAc2, SiteReg.Availability_Date,  " _
                     & " SiteReg.Region_Id, SiteReg.CityID, SiteReg.Area_ID, IsNull(SiteReg.ProjectId,0) as ProjectId " _
                     & " FROM dbo.SiteRegistrationTable AS SiteReg LEFT OUTER JOIN " _
                     & " dbo.tblListState AS Region ON SiteReg.Region_Id = Region.StateId LEFT OUTER JOIN " _
                     & " dbo.tblListCity AS City ON SiteReg.CityID = City.CityId LEFT OUTER JOIN " _
                     & " dbo.tblListTerritory AS Territory ON SiteReg.Area_ID = Territory.TerritoryId ORDER BY  SiteReg.SiteRegistrationID DESC "

            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetDetailRecord(ByVal SiteRegID As Integer) As DataTable
        Try
            Dim strSQL As String = String.Empty
            strSQL = "SELECT SiteRegCost.coa_detail_id, COA.detail_code, COA.detail_title, SiteRegCost.Amount, SiteRegCost.Tenure_Start, SiteRegCost.Tenure_End, " _
                     & " SiteRegCost.Payee_Name, SiteRegCost.Payment_Term, SiteRegCost.Remarks " _
                     & " FROM dbo.SiteRegistrationCostDetailTable AS SiteRegCost INNER JOIN " _
                     & " dbo.vwCOADetail AS COA ON SiteRegCost.coa_detail_id = COA.coa_detail_id WHERE SiteRegCost.SiteRegistrationID=" & SiteRegID & " ORDER BY SiteRegCost.SiteCostID ASC "

            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetDetailMaxRecord() As DataTable
        Try
            Dim strSQL As String = String.Empty
            strSQL = "SELECT SiteRegCost.coa_detail_id, COA.detail_code, COA.detail_title, convert(float,0) as Amount, getdate() as  Tenure_Start, Getdate() as Tenure_End, " _
                    & " '' as Payee_Name, '' as Payment_Term, '' as Remarks " _
                    & " FROM dbo.SiteRegistrationCostDetailTable AS SiteRegCost INNER JOIN " _
                    & " dbo.vwCOADetail AS COA ON SiteRegCost.coa_detail_id = COA.coa_detail_id WHERE SiteRegCost.SiteRegistrationID IN (Select IsNull(Max(SiteRegistrationID),0) as SiteRegistrationID From SiteRegistrationCostDetailTable) "

            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
