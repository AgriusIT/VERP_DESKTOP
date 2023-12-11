' Code By Imran Ali 
' 18-Jul-2013
' Request Against Req ID No 718

Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility.Utility

Public Class AgreementDAL
    'Declare Enum for indexing
    Public Enum enmAgreement
        AgreementId
        AgreementNo
        AgreementDate
        Delivery_Date
        First_Payment
        AgreementType
        Business_Name
        Contact_Name
        Business_Type
        Address
        Phone
        FaxNo
        Email
        StateID
        CityID
        TerritoryID
        Product_Category_Condition
        Term_Condition
        Warranty_Condition
        Termination_Condition
        Status
        'AgreementDetail
        Total_Qty
        Total_Amount
        User_Name
        Discount
        Customer_Name ''TFS1854   
        'arrFile  ''TFS1854
        'Source ''TFS1854
        'AttachmentPath ''TFS1854'
        No_of_Attachment ''TFS1854   
    End Enum

    Public Function Add(ByVal Agreement As AgreementMasterBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR) 'Create Connection Object
        If Con.State = ConnectionState.Closed Then Con.Open() 'Connection Compare if Closed
        Dim trans As SqlTransaction = Con.BeginTransaction ' Set transaction with Conection's Begin Transaction

        Try

            'Create Variable in set INSERT Command
            ''TFS1854 Added new column CustomerName in the query
            Dim strQuery As String = "INSERT INTO AgreementMasterTable(AgreementNo, AgreementDate, Delivery_Date, First_Payment, AgreementType, Business_Name, Contact_Name, Business_Type, Address, Phone, FaxNo, Email, StateID, CityID, TerritoryID, Product_Category_Condition, Term_Condition, Warranty_Condition, Termination_Condition,Status,Total_Qty, Total_Amount,User_Name,Discount,Customer_Name)    " _
            & " VALUES(N'" & Agreement.AgreementNo.Replace("'", "''") & "', N'" & Agreement.AgreementDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & Agreement.Delivery_Date.ToString("yyyy-M-d h:mm:ss tt") & "', " & Agreement.First_Payment & "," & IIf(Agreement.AgreementType = "", "NULL", "N'" & Agreement.AgreementType.Replace("'", "''") & "'") & ", N'" & Agreement.Business_Name.Replace("'", "''") & "', " _
            & " N'" & Agreement.Contact_Name.Replace("'", "''") & "', N'" & Agreement.Business_Type.Replace("'", "''") & "', N'" & Agreement.Address.Replace("'", "''") & "', N'" & Agreement.Phone.Replace("'", "''") & "', N'" & Agreement.FaxNo.Replace("'", "''") & "', " _
            & " N'" & Agreement.Email.Replace("'", "''") & "', " & Agreement.StateID & ", " & Agreement.CityID & ", " & Agreement.TerritoryID & ", N'" & Agreement.Product_Category_Condition.Replace("'", "''") & "', N'" & Agreement.Term_Condition.Replace("'", "''") & "', N'" & Agreement.Warranty_Condition.Replace("'", "''") & "', N'" & Agreement.Termination_Condition.Replace("'", "''") & "', " & IIf(Agreement.Status = True, 1, 0) & ", " & Agreement.Total_Qty & ", " & Agreement.Total_Amount & ", N'" & Agreement.User_Name.Replace("'", "''") & "', " & Val(Agreement.Discount) & ", N'" & Agreement.Customer_Name.Replace("'", "''") & "') Select @@Identity"
            Agreement.AgreementId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strQuery) 'Here Execute Insert Command for Master Information
            AddDetail(Agreement, trans) 'Call Function AddDetail  
            ''TASK TFS1854
            If Agreement.ArrFile.Count > 0 Then
                SaveDocument(Agreement.AgreementId, Agreement.Source, Agreement.AttachmentPath, Agreement.ArrFile, Agreement.AgreementDate, trans)

            End If
            ''END TASK TFS1854
            trans.Commit()
            Return True 'if exception  not create then return true

        Catch ex As Exception
            trans.Rollback() 'in case exception then rollback
            Throw ex
        Finally
            Con.Close() 'Con Closed
        End Try
    End Function
    Public Function AddDetail(ByVal Agreement As AgreementMasterBE, ByVal trans As SqlTransaction) As Boolean
        Try


            Dim strQuery As String = String.Empty 'Declare Variable for SQL Command
            For Each AgreementDt As AgreementDetailBE In Agreement.AgreementDetail 'Checking value from AgreementDetail's Class
                If AgreementDt IsNot Nothing Then
                    strQuery = String.Empty 'Reset INSER Command
                    strQuery = "INSERT INTO AgreementDetailTable(AgreementId, LocationId, ArticleDefId, ArticleSize,Sz1,Qty,Price,CurrentPrice,Comments,Pack_Desc) Values(" & Agreement.AgreementId & ", " & AgreementDt.LocationId & ", " & AgreementDt.ArticleDefId & ", N'" & AgreementDt.ArticleSize.Replace("'", "''") & "', " & AgreementDt.Sz1 & ", " & AgreementDt.Qty & ", " & AgreementDt.Price & ", " & AgreementDt.CurrentPrice & ", N'" & AgreementDt.Comments.Replace("'", "''") & "', N'" & AgreementDt.Pack_Desc.Replace("'", "''") & "') "
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery) 'Here Execute INSERT Command for Detail Information
                End If
            Next
            Return True 'if exception  not create then return true

        Catch ex As Exception
            trans.Rollback() 'In case exception then rollback
            Throw ex
        Finally

        End Try
    End Function


    Public Function Update(ByVal Agreement As AgreementMasterBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR) 'Create Connection Object
        If Con.State = ConnectionState.Closed Then Con.Open() 'Connection Compare if Closed
        Dim trans As SqlTransaction = Con.BeginTransaction ' Set transaction with Conection's Begin Transaction
        Try

            'Create Variable in set INSERT Command
            ''TFS1854 Updating new column CustomerName in the query
            Dim strQuery As String = "UPDATE  AgreementMasterTable SET  AgreementNo=N'" & Agreement.AgreementNo.Replace("'", "''") & "', " _
                                    & " AgreementDate=N'" & Agreement.AgreementDate.ToString("yyyy-M-d h:mm:ss tt") & "'," _
                                    & " Delivery_Date=N'" & Agreement.Delivery_Date.ToString("yyyy-M-d h:mm:ss tt") & "'," _
                                    & " First_Payment=" & Agreement.First_Payment & ", " _
                                    & " AgreementType=" & IIf(Agreement.AgreementType = "", "NULL", "N'" & Agreement.AgreementType.Replace("'", "''") & "'") & ", " _
                                    & " Business_Name=N'" & Agreement.Business_Name.Replace("'", "''") & "', " _
                                    & " Contact_Name=N'" & Agreement.Contact_Name.Replace("'", "''") & "' , " _
                                    & " Business_Type=N'" & Agreement.Business_Type.Replace("'", "''") & "', " _
                                    & " Customer_Name=N'" & Agreement.Customer_Name.Replace("'", "''") & "', " _
                                    & " Address=N'" & Agreement.Address.Replace("'", "''") & "', " _
                                    & " Phone=N'" & Agreement.Phone.Replace("'", "''") & "', " _
                                    & " FaxNo=N'" & Agreement.FaxNo.Replace("'", "''") & "', " _
                                    & " Email=N'" & Agreement.Email.Replace("'", "''") & "', " _
                                    & " StateID=" & Agreement.StateID & ", " _
                                    & " CityID=" & Agreement.CityID & ", " _
                                    & " TerritoryID=" & Agreement.TerritoryID & ", " _
                                    & " Product_Category_Condition=N'" & Agreement.Product_Category_Condition.Replace("'", "''") & "',  " _
                                    & " Term_Condition=N'" & Agreement.Term_Condition.Replace("'", "''") & "', " _
                                    & " Warranty_Condition=N'" & Agreement.Warranty_Condition.Replace("'", "''") & "', " _
                                    & " Termination_Condition=N'" & Agreement.Termination_Condition.Replace("'", "''") & "', " _
                                    & " Status=" & IIf(Agreement.Status = True, 1, 0) & ", Total_Qty=" & Agreement.Total_Qty & ", Total_Amount=" & Agreement.Total_Amount & ", User_Name=N'" & Agreement.User_Name.Replace("'", "''") & "', Discount=" & Val(Agreement.Discount) & "  WHERE AgreementId= " & Agreement.AgreementId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery) 'Here Execute Insert Command for Master Information
            ''TASK TFS1854
            If Agreement.ArrFile.Count > 0 Then
                SaveDocument(Agreement.AgreementId, Agreement.Source, Agreement.AttachmentPath, Agreement.ArrFile, Agreement.AgreementDate, trans)
            End If
            ''END TASK TFS1854
            strQuery = String.Empty 'Clear SQL Comment 
            strQuery = "Delete From AgreementDetailTable WHERE AgreementId=" & Agreement.AgreementId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery) 'Here Execute Delete Command From AgreementDetailTable

            AddDetail(Agreement, trans) 'Call Function AddDetail  
            trans.Commit()
            Return True 'if exception  not create then return true

        Catch ex As Exception
            trans.Rollback() 'in case exception then rollback
            Throw ex
        Finally
            Con.Close() 'Con Closed
        End Try
    End Function


    Public Function Delete(ByVal Agreement As AgreementMasterBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR) 'Create Connection Object
        If Con.State = ConnectionState.Closed Then Con.Open() 'Connection Compare if Closed
        Dim trans As SqlTransaction = Con.BeginTransaction ' Set transaction with Conection's Begin Transaction
        Try

            Dim strQuery As String = String.Empty
            strQuery = String.Empty 'Clear SQL Comment 
            strQuery = "Delete From AgreementDetailTable WHERE AgreementId=" & Agreement.AgreementId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery) 'Here Execute Delete Command From AgreementDetailTable
            'Create Variable in set INSERT Command


            strQuery = String.Empty 'Clear SQL Comment 
            strQuery = "Delete From  AgreementMasterTable  WHERE AgreementId=" & Agreement.AgreementId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery) 'Here Execute Insert Command for Master Information


            trans.Commit()
            Return True 'if exception  not create then return true

        Catch ex As Exception
            trans.Rollback() 'in case exception then rollback
            Throw ex
        Finally
            Con.Close() 'Con Closed
        End Try
    End Function


    Public Function GetAll(Optional ByVal Condition As String = "") As List(Of AgreementMasterBE)
        Try
            Dim strQuery As String = String.Empty
            strQuery = "Select " & IIf(Condition = "All", "", "Top 50") & " AgreementId ,AgreementNo,AgreementDate ,Delivery_Date ,First_Payment ,AgreementType ,Business_Name ,Contact_Name ,Business_Type ,AgreementMasterTable.Address ,Phone,FaxNo ,Email ,StateID ,CityID , TerritoryID ,Product_Category_Condition ,Term_Condition ,Warranty_Condition ,Termination_Condition , AgreementMasterTable.Status ,Total_Qty ,Total_Amount ,User_Name ,Discount ,Customer_Name  , IsNull([No_of_Attachment],0) as [No_of_Attachment] From AgreementMasterTable LEFT OUTER JOIN(Select Count(*) as [No_of_Attachment], DocId From DocumentAttachment WHERE Source='frmAgreement' Group By DocId) Att On Att.DocId =  AgreementMasterTable.AgreementId ORDER BY AgreementNo DESC"
            Dim dr As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strQuery, Nothing)
            Dim AgreementList As New List(Of AgreementMasterBE)
            Dim Agreement As AgreementMasterBE
            If dr.HasRows Then
                While dr.Read
                    Agreement = New AgreementMasterBE
                    Agreement.AgreementId = dr.GetValue(enmAgreement.AgreementId)
                    Agreement.AgreementNo = dr.GetValue(enmAgreement.AgreementNo)
                    Agreement.AgreementDate = dr.GetValue(enmAgreement.AgreementDate)
                    Agreement.Delivery_Date = dr.GetValue(enmAgreement.Delivery_Date)
                    Agreement.First_Payment = dr.GetValue(enmAgreement.First_Payment)
                    Agreement.AgreementType = dr.GetValue(enmAgreement.AgreementType).ToString
                    Agreement.Business_Name = dr.GetValue(enmAgreement.Business_Name).ToString
                    Agreement.Contact_Name = dr.GetValue(enmAgreement.Contact_Name).ToString
                    Agreement.Business_Type = dr.GetValue(enmAgreement.Business_Type).ToString
                    Agreement.Address = dr.GetValue(enmAgreement.Address).ToString
                    Agreement.Phone = dr.GetValue(enmAgreement.Phone).ToString
                    Agreement.FaxNo = dr.GetValue(enmAgreement.FaxNo).ToString
                    Agreement.Email = dr.GetValue(enmAgreement.Email).ToString
                    Agreement.StateID = dr.GetValue(enmAgreement.StateID)
                    Agreement.CityID = dr.GetValue(enmAgreement.CityID)
                    Agreement.TerritoryID = dr.GetValue(enmAgreement.TerritoryID)
                    Agreement.Product_Category_Condition = dr.GetValue(enmAgreement.Product_Category_Condition).ToString
                    Agreement.Term_Condition = dr.GetValue(enmAgreement.Term_Condition).ToString
                    Agreement.Warranty_Condition = dr.GetValue(enmAgreement.Warranty_Condition).ToString
                    Agreement.Termination_Condition = dr.GetValue(enmAgreement.Termination_Condition).ToString
                    Agreement.Status = dr.GetValue(enmAgreement.Status)
                    Agreement.Discount = dr.GetValue(enmAgreement.Discount)
                    Agreement.Customer_Name = dr.GetValue(enmAgreement.Customer_Name).ToString ''TFS1854
                    Agreement.No_of_Attachment = dr.GetValue(enmAgreement.No_of_Attachment) ''TFS1854
                    AgreementList.Add(Agreement)
                End While
            End If
            Return AgreementList
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getBusinessType() As DataTable
        Try
            Dim strQuery As String = "Select DISTINCT Business_Type, Business_Type From AgreementMasterTable ORDER BY 1 ASC"
            Dim dt As New DataTable 'Declare Variable and object of Datatable
            dt = UtilityDAL.GetDataTable(strQuery) 'Call GetDataTable's Function And se in dt as datatable
            Return dt 'Return dt 
        Catch ex As Exception
            Throw ex 'Exception Catch from getdatatable's function
        End Try
    End Function
    Public Function GetProductionCategoryCondition() As String
        Try

            Dim strQuery As String = String.Empty
            strQuery = "Select Product_Category_Condition From AgreementMasterTable WHERE AgreementId in (select Max(Isnull(AgreementId,0)) From AgreementMasterTable)"
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strQuery)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0)
                Else
                    Return ""
                End If
            Else
                Return ""
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetTermAndCondition() As String
        Try

            Dim strQuery As String = String.Empty
            strQuery = "Select Term_Condition From AgreementMasterTable WHERE AgreementId in (select Max(Isnull(AgreementId,0)) From AgreementMasterTable)"
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strQuery)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0)
                Else
                    Return ""
                End If
            Else
                Return ""
            End If


            'Return SQLHelper.ExecuteScaler(SQLHelper.CON_STR, CommandType.Text, strQuery)

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetWarrantyCondition() As String
        Try

            Dim strQuery As String = String.Empty
            strQuery = "Select Warranty_Condition From AgreementMasterTable WHERE AgreementId in (select Max(Isnull(AgreementId,0)) From AgreementMasterTable) "
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strQuery)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0)
                Else
                    Return ""
                End If
            Else
                Return ""
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetTerminationCondition() As String
        Try

            Dim strQuery As String = String.Empty
            strQuery = "Select Termination_Condition From AgreementMasterTable WHERE AgreementId in (select Max(AgreementId) From AgreementMasterTable)"
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strQuery)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0)
                Else
                    Return ""
                End If
            Else
                Return ""
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetDetailRecords(ByVal ReceivedId As Integer) As DataTable
        Try

            Dim strQuery As String = String.Empty
            strQuery = "SELECT dbo.AgreementDetailTable.LocationId, dbo.AgreementDetailTable.ArticleDefId, dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription AS item, " _
                     & " dbo.AgreementDetailTable.ArticleSize AS Unit, dbo.AgreementDetailTable.Sz1 AS Qty, dbo.AgreementDetailTable.Price, Convert(float,0) as Total, dbo.AgreementDetailTable.CurrentPrice, AgreementDetailTable.Sz1 as PackQty, AgreementDetailTable.Comments,ISNULL(AgreementDetailTable.Pack_Desc,dbo.AgreementDetailTable.ArticleSize) as Pack_Desc   " _
                     & " FROM  dbo.ArticleDefView INNER JOIN " _
                     & " dbo.AgreementDetailTable ON dbo.ArticleDefView.ArticleId = dbo.AgreementDetailTable.ArticleDefId WHERE AgreementDetailTable.AgreementId=" & ReceivedId
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strQuery)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetLocation() As DataTable
        Try
            Dim strQuery As String = String.Empty
            strQuery = "Select Location_Id, Location_Name From tblDefLocation"
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strQuery)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SaveDocument(ByVal DocId As Integer, ByVal Source As String, objPath As String, ByVal arrFile As List(Of String), ByVal Date1 As DateTime, ByVal objTrans As SqlTransaction) As Boolean
        Dim cmd As New SqlCommand
        cmd.Connection = objTrans.Connection
        cmd.Transaction = objTrans
        Try

            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("Select DocId, Source, Path + '\' + FileName  as [FileNames]  From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'", objTrans)
            dt.AcceptChanges()


            Dim objdt As New DataTable
            objdt = UtilityDAL.GetDataTable("Select IsNull(Count(*),0)+1 as Cont From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'", objTrans)
            Dim intId As Integer = objdt.Rows(0)(0)

            Dim strSQL As String = String.Empty
            cmd.CommandText = String.Empty
            strSQL = "Delete From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()



            If Not arrFile Is Nothing AndAlso arrFile.Count > 0 Then ''TFS1772
         
                For Each objFile As String In arrFile
                    If IO.File.Exists(objFile) Then
                        If IO.Directory.Exists(objPath) = False Then
                            IO.Directory.CreateDirectory(objPath)
                        End If

                        Dim New_Files As String = intId & "_" & DocId & "_" & Date1.ToString("yyyyMMdd") & "." & objFile.Substring(objFile.LastIndexOf(".") + 1)

                        Dim dr As DataRow
                        dr = dt.NewRow
                        dr(0) = DocId
                        dr(1) = Source
                        dr(2) = objPath & "\" & New_Files
                        dt.Rows.Add(dr)
                        dt.AcceptChanges()
                        If IO.File.Exists(objPath & "\" & New_Files) Then
                            IO.File.Delete(objPath & "\" & New_Files)
                        End If
                        IO.File.Copy(objFile, objPath & "\" & New_Files)
                        intId += 1
                    End If
                Next
            End If


            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For Each r As DataRow In dt.Rows
                        Dim strPath As String = objPath
                        Dim strFileName As String = r.Item("FileNames").ToString.Substring(r.Item("FileNames").ToString.LastIndexOf("\") + 1)
                        cmd.CommandText = String.Empty
                        strSQL = "INSERT INTO DocumentAttachment(DocId, Source, FileName, Path) Values(" & Val(r("DocId").ToString) & ",N'" & r.Item("Source").ToString.Replace("'", "''") & "', N'" & strFileName.Replace("'", "''") & "', N'" & strPath.Replace("'", "''") & "')"
                        cmd.CommandText = strSQL
                        cmd.ExecuteNonQuery()
                    Next
                End If
            End If


        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        End Try
    End Function
End Class
