'TaskId:2532 Revised Sales Certificate
''01-Jul-2014 TASK:2708 Imran Ali Add new fields Registrtion for and sales tax % in Sales Certificate
''05-Jul-2014 TAS:2717 Imran Ali Add new field Reference No In Sales Certificate
''16-Aug-2014 Task:2788 Imran Ali Color Field in Sales Certificate Form
''12-Sep-2014 Task:2841 Imran Ali Retrive Last Record In Sales Certificate
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility.Utility

Public Class SalesCertificateDAL
    Public Function Save(ByVal SalesCertificate As SalesCertificateBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim objTrans As SqlTransaction = Con.BeginTransaction
        Try
           
            SalesCertificate.SaleCertificateNo = GetNextDocNo(SalesCertificate.SaleCertificateNo.Substring(0, SalesCertificate.SaleCertificateNo.Length - 5), objTrans)
            Dim strSQL As String = String.Empty
            'strSQL = "INSERT INTO SalesCertificateTable(SaleCertificateNo, SaleCertificateDate, DeliveredTo, Engine_No, Chassis_No, ArticleDefId,Model_Desc, Max_Laden_Weight, Max_Weight_Front_Axel, Max_Weight_Rear_Axel, Tyre_Front_Wheel, Tyre_Rear_Wheel, Base_Wheel, Comments, SalesId, SalesDetailId, UserName) " _
            '            & " VALUES(N'" & SalesCertificate.SaleCertificateNo.Replace("'", "''") & "', N'" & SalesCertificate.SaleCertificateDate.ToString("yyyy-M-d h:mm:ss tt") & "',N'" & SalesCertificate.DeliveredTo.Replace("'", "''") & "', N'" & SalesCertificate.Engine_No.Replace("'", "''") & "', " _
            '            & " N'" & SalesCertificate.Chassis_No.Replace("'", "''") & "', " & SalesCertificate.ArticleDefId & ", N'" & SalesCertificate.Model_Desc.Replace("'", "''") & "',  N'" & SalesCertificate.Max_Laden_Weight.Replace("'", "''") & "', N'" & SalesCertificate.Max_Weight_Front_Axel.Replace("'", "''") & "', " _
            '            & " N'" & SalesCertificate.Max_Weight_Rear_Axel.Replace("'", "''") & "', N'" & SalesCertificate.Tyre_Front_Wheel.Replace("'", "''") & "', N'" & SalesCertificate.Tyre_Rear_Wheel.Replace("'", "''") & "', N'" & SalesCertificate.Base_Wheel.Replace("'", "''") & "', N'" & SalesCertificate.Comments.Replace("'", "''") & "', " & SalesCertificate.SalesId & ", " & SalesCertificate.SalesDetailId & ", N'" & SalesCertificate.UserName.Replace("'", "''") & "')Select @@Identity"
            'SalesCertificate.SaleCertificateId = SQLHelper.ExecuteScaler(objTrans, CommandType.Text, strSQL)
            'TaskId:2532 Revised Sales Certificate ,Add One Field of ModelCode In Insert Query 
            'Before against task:2708
            'Before against task:2678
            'strSQL = "INSERT INTO SalesCertificateTable(SaleCertificateNo, SaleCertificateDate, DeliveredTo, Engine_No, Chassis_No, ArticleDefId, ModelCode,Model_Desc, Max_Laden_Weight, Max_Weight_Front_Axel, Max_Weight_Rear_Axel, Tyre_Front_Wheel, Tyre_Rear_Wheel, Base_Wheel, Comments, SalesId, SalesDetailId, UserName) " _
            '& " VALUES(N'" & SalesCertificate.SaleCertificateNo.Replace("'", "''") & "', N'" & SalesCertificate.SaleCertificateDate.ToString("yyyy-M-d h:mm:ss tt") & "',N'" & SalesCertificate.DeliveredTo.Replace("'", "''") & "', N'" & SalesCertificate.Engine_No.Replace("'", "''") & "', " _
            '& " N'" & SalesCertificate.Chassis_No.Replace("'", "''") & "', " & SalesCertificate.ArticleDefId & ", N'" & SalesCertificate.ModelCode.Replace("'", "''") & "',N'" & SalesCertificate.Model_Desc.Replace("'", "''") & "',  N'" & SalesCertificate.Max_Laden_Weight.Replace("'", "''") & "', N'" & SalesCertificate.Max_Weight_Front_Axel.Replace("'", "''") & "', " _
            '& " N'" & SalesCertificate.Max_Weight_Rear_Axel.Replace("'", "''") & "', N'" & SalesCertificate.Tyre_Front_Wheel.Replace("'", "''") & "', N'" & SalesCertificate.Tyre_Rear_Wheel.Replace("'", "''") & "', N'" & SalesCertificate.Base_Wheel.Replace("'", "''") & "', N'" & SalesCertificate.Comments.Replace("'", "''") & "', " & SalesCertificate.SalesId & ", " & SalesCertificate.SalesDetailId & ", N'" & SalesCertificate.UserName.Replace("'", "''") & "')Select @@Identity"
            'Before against task:2717
            'Task:2678 Added Field InvoiceAmount, SalesTax, Address, NTN
            ' strSQL = "INSERT INTO SalesCertificateTable(SaleCertificateNo, SaleCertificateDate, DeliveredTo, Engine_No, Chassis_No, ArticleDefId, ModelCode,Model_Desc, Max_Laden_Weight, Max_Weight_Front_Axel, Max_Weight_Rear_Axel, Tyre_Front_Wheel, Tyre_Rear_Wheel, Base_Wheel, Comments, SalesId, SalesDetailId, UserName, InvoiceAmount,SalesTax, Address, NTN) " _
            '& " VALUES(N'" & SalesCertificate.SaleCertificateNo.Replace("'", "''") & "', N'" & SalesCertificate.SaleCertificateDate.ToString("yyyy-M-d h:mm:ss tt") & "',N'" & SalesCertificate.DeliveredTo.Replace("'", "''") & "', N'" & SalesCertificate.Engine_No.Replace("'", "''") & "', " _
            '& " N'" & SalesCertificate.Chassis_No.Replace("'", "''") & "', " & SalesCertificate.ArticleDefId & ", N'" & SalesCertificate.ModelCode.Replace("'", "''") & "',N'" & SalesCertificate.Model_Desc.Replace("'", "''") & "',  N'" & SalesCertificate.Max_Laden_Weight.Replace("'", "''") & "', N'" & SalesCertificate.Max_Weight_Front_Axel.Replace("'", "''") & "', " _
            '& " N'" & SalesCertificate.Max_Weight_Rear_Axel.Replace("'", "''") & "', N'" & SalesCertificate.Tyre_Front_Wheel.Replace("'", "''") & "', N'" & SalesCertificate.Tyre_Rear_Wheel.Replace("'", "''") & "', N'" & SalesCertificate.Base_Wheel.Replace("'", "''") & "', N'" & SalesCertificate.Comments.Replace("'", "''") & "', " & SalesCertificate.SalesId & ", " & SalesCertificate.SalesDetailId & ", N'" & SalesCertificate.UserName.Replace("'", "''") & "', " & SalesCertificate.InvoiceAmount & ", " & SalesCertificate.SalesTax & ", N'" & SalesCertificate.Address.Replace("'", "''") & "', N'" & SalesCertificate.NTN.Replace("'", "''") & "')Select @@Identity"
            'End Task:2678
            'Task:2708 Added Fields RegistrationFor, Tax_Percent  
            'Task:2708
            'Task:2717 Added Field Reference_No
            'strSQL = "INSERT INTO SalesCertificateTable(SaleCertificateNo, SaleCertificateDate, DeliveredTo, Engine_No, Chassis_No, ArticleDefId, ModelCode,Model_Desc, Max_Laden_Weight, Max_Weight_Front_Axel, Max_Weight_Rear_Axel, Tyre_Front_Wheel, Tyre_Rear_Wheel, Base_Wheel, Comments, SalesId, SalesDetailId, UserName, InvoiceAmount,SalesTax, Address, NTN,RegistrationFor,Tax_Percent,Reference_No) " _
            ' & " VALUES(N'" & SalesCertificate.SaleCertificateNo.Replace("'", "''") & "', N'" & SalesCertificate.SaleCertificateDate.ToString("yyyy-M-d h:mm:ss tt") & "',N'" & SalesCertificate.DeliveredTo.Replace("'", "''") & "', N'" & SalesCertificate.Engine_No.Replace("'", "''") & "', " _
            ' & " N'" & SalesCertificate.Chassis_No.Replace("'", "''") & "', " & SalesCertificate.ArticleDefId & ", N'" & SalesCertificate.ModelCode.Replace("'", "''") & "',N'" & SalesCertificate.Model_Desc.Replace("'", "''") & "',  N'" & SalesCertificate.Max_Laden_Weight.Replace("'", "''") & "', N'" & SalesCertificate.Max_Weight_Front_Axel.Replace("'", "''") & "', " _
            ' & " N'" & SalesCertificate.Max_Weight_Rear_Axel.Replace("'", "''") & "', N'" & SalesCertificate.Tyre_Front_Wheel.Replace("'", "''") & "', N'" & SalesCertificate.Tyre_Rear_Wheel.Replace("'", "''") & "', N'" & SalesCertificate.Base_Wheel.Replace("'", "''") & "', N'" & SalesCertificate.Comments.Replace("'", "''") & "', " & SalesCertificate.SalesId & ", " & SalesCertificate.SalesDetailId & ", N'" & SalesCertificate.UserName.Replace("'", "''") & "', " & SalesCertificate.InvoiceAmount & ", " & SalesCertificate.SalesTax & ", N'" & SalesCertificate.Address.Replace("'", "''") & "', N'" & SalesCertificate.NTN.Replace("'", "''") & "', N'" & SalesCertificate.RegistrationFor.Replace("'", "''") & "', " & SalesCertificate.Tax_Percent & ", N'" & SalesCertificate.Reference_No.Replace("'", "''") & "')Select @@Identity"
            'End Task:27
            'Task:2788 Added Field Color.

            'Ahmad Sharif : Modify the strSQL query and add two new columns in it, Modification on 04-06-2015
            'strSQL = "INSERT INTO SalesCertificateTable(SaleCertificateNo, SaleCertificateDate, DeliveredTo, Engine_No, Chassis_No, ArticleDefId, ModelCode,Model_Desc, Max_Laden_Weight, Max_Weight_Front_Axel, Max_Weight_Rear_Axel, Tyre_Front_Wheel, Tyre_Rear_Wheel, Base_Wheel, Comments, SalesId, SalesDetailId, UserName, InvoiceAmount,SalesTax, Address, NTN,RegistrationFor,Tax_Percent,Reference_No,Color,DcNo,Remarks) " _
            '& " VALUES(N'" & SalesCertificate.SaleCertificateNo.Replace("'", "''") & "', N'" & SalesCertificate.SaleCertificateDate.ToString("yyyy-M-d h:mm:ss tt") & "',N'" & SalesCertificate.DeliveredTo.Replace("'", "''") & "', N'" & SalesCertificate.Engine_No.Replace("'", "''") & "', " _
            '& " N'" & SalesCertificate.Chassis_No.Replace("'", "''") & "', " & SalesCertificate.ArticleDefId & ", N'" & SalesCertificate.ModelCode.Replace("'", "''") & "',N'" & SalesCertificate.Model_Desc.Replace("'", "''") & "',  N'" & SalesCertificate.Max_Laden_Weight.Replace("'", "''") & "', N'" & SalesCertificate.Max_Weight_Front_Axel.Replace("'", "''") & "', " _
            '& " N'" & SalesCertificate.Max_Weight_Rear_Axel.Replace("'", "''") & "', N'" & SalesCertificate.Tyre_Front_Wheel.Replace("'", "''") & "', N'" & SalesCertificate.Tyre_Rear_Wheel.Replace("'", "''") & "', N'" & SalesCertificate.Base_Wheel.Replace("'", "''") & "', N'" & SalesCertificate.Comments.Replace("'", "''") & "', " & SalesCertificate.SalesId & ", " & SalesCertificate.SalesDetailId & ", N'" & SalesCertificate.UserName.Replace("'", "''") & "', " & SalesCertificate.InvoiceAmount & ", " & SalesCertificate.SalesTax & ", N'" & SalesCertificate.Address.Replace("'", "''") & "', N'" & SalesCertificate.NTN.Replace("'", "''") & "', N'" & SalesCertificate.RegistrationFor.Replace("'", "''") & "', " & SalesCertificate.Tax_Percent & ", N'" & SalesCertificate.Reference_No.Replace("'", "''") & "',N'" & SalesCertificate.Color.Replace("'", "''") & "',N'" & SalesCertificate.DC_NO.Replace("'", "''") & "', N'" & SalesCertificate.Remarks.Replace("'", "''") & "')Select @@Identity"



            strSQL = "INSERT INTO SalesCertificateTable(SaleCertificateNo, SaleCertificateDate, DeliveredTo, Engine_No, Chassis_No, ArticleDefId, ModelCode,Model_Desc, Max_Laden_Weight, Max_Weight_Front_Axel, Max_Weight_Rear_Axel, Tyre_Front_Wheel, Tyre_Rear_Wheel, Base_Wheel, Comments, SalesId, SalesDetailId, UserName, InvoiceAmount,SalesTax, Address, NTN,RegistrationFor,Tax_Percent,Reference_No,Color,DcNo,Remarks, FatherName,PersonCast,AdvanceAmount,MeterNo,Installment,RegistrationNo,ContractDate) " _
         & " VALUES(N'" & SalesCertificate.SaleCertificateNo.Replace("'", "''") & "', N'" & SalesCertificate.SaleCertificateDate.ToString("yyyy-M-d h:mm:ss tt") & "',N'" & SalesCertificate.DeliveredTo.Replace("'", "''") & "', N'" & SalesCertificate.Engine_No.Replace("'", "''") & "', " _
         & " N'" & SalesCertificate.Chassis_No.Replace("'", "''") & "', " & SalesCertificate.ArticleDefId & ", N'" & SalesCertificate.ModelCode.Replace("'", "''") & "',N'" & SalesCertificate.Model_Desc.Replace("'", "''") & "',  N'" & SalesCertificate.Max_Laden_Weight.Replace("'", "''") & "', N'" & SalesCertificate.Max_Weight_Front_Axel.Replace("'", "''") & "', " _
         & " N'" & SalesCertificate.Max_Weight_Rear_Axel.Replace("'", "''") & "', N'" & SalesCertificate.Tyre_Front_Wheel.Replace("'", "''") & "', N'" & SalesCertificate.Tyre_Rear_Wheel.Replace("'", "''") & "', N'" & SalesCertificate.Base_Wheel.Replace("'", "''") & "', N'" & SalesCertificate.Comments.Replace("'", "''") & "', " & SalesCertificate.SalesId & ", " & SalesCertificate.SalesDetailId & ", N'" & SalesCertificate.UserName.Replace("'", "''") & "', " & SalesCertificate.InvoiceAmount & ", " & SalesCertificate.SalesTax & ", N'" & SalesCertificate.Address.Replace("'", "''") & "', N'" & SalesCertificate.NTN.Replace("'", "''") & "', N'" & SalesCertificate.RegistrationFor.Replace("'", "''") & "', " & SalesCertificate.Tax_Percent & ", N'" & SalesCertificate.Reference_No.Replace("'", "''") & "',N'" & SalesCertificate.Color.Replace("'", "''") & "',N'" & SalesCertificate.DC_NO.Replace("'", "''") & "', N'" & SalesCertificate.Remarks.Replace("'", "''") & "', N'" & SalesCertificate.FatherName.Replace("'", "''") & "',N'" & SalesCertificate.Person_Cast.Replace("'", "''") & "'," & Val(SalesCertificate.AdvanceAmount) & ",N'" & SalesCertificate.MeterNo.Replace("'", "''") & "'," & SalesCertificate.Installment & ",N'" & SalesCertificate.RegistrationNo.Replace("'", "''") & "'," & IIf(SalesCertificate.ContractDate = DateTime.MinValue, "NULL", "Convert(dateTime,'" & SalesCertificate.ContractDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ")Select @@Identity"

            SalesCertificate.SaleCertificateId = SQLHelper.ExecuteScaler(objTrans, CommandType.Text, strSQL)

            strSQL = String.Empty
            strSQL = "Update SalesDetailTable SET SaleCertificateId=" & SalesCertificate.SaleCertificateId & " , IsReturnedSalesCertificate = 0 WHERE SalesId=" & SalesCertificate.SalesId & " AND SaleDetailId=" & SalesCertificate.SalesDetailId & " AND ArticleDefId=" & SalesCertificate.ArticleDefId & ""
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

            objTrans.Commit()
            Return True

        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Update(ByVal SalesCertificate As SalesCertificateBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim objTrans As SqlTransaction = Con.BeginTransaction
        Try
            'strSQL = "UPDATE SalesCertificateTable  SET  SaleCertificateNo=N'" & SalesCertificate.SaleCertificateNo.Replace("'", "''") & "', SaleCertificateDate=N'" & SalesCertificate.SaleCertificateDate.ToString("yyyy-M-d h:mm:ss tt") & "', DeliveredTo=N'" & SalesCertificate.DeliveredTo.Replace("'", "''") & "', Engine_No=N'" & SalesCertificate.Engine_No.Replace("'", "''") & "', Chassis_No=N'" & SalesCertificate.Chassis_No.Replace("'", "''") & "', ArticleDefId=" & SalesCertificate.ArticleDefId & "', Model_Desc=N'" & SalesCertificate.Model_Desc.Replace("'", "''") & "', Max_Laden_Weight=N'" & SalesCertificate.Max_Laden_Weight.Replace("'", "''") & "', Max_Weight_Front_Axel=N'" & SalesCertificate.Max_Weight_Front_Axel.Replace("'", "''") & "', Max_Weight_Rear_Axel=N'" & SalesCertificate.Max_Weight_Rear_Axel.Replace("'", "''") & "', Tyre_Front_Wheel=N'" & SalesCertificate.Tyre_Front_Wheel.Replace("'", "''") & "', Tyre_Rear_Wheel=N'" & SalesCertificate.Tyre_Rear_Wheel.Replace("'", "''") & "', Base_Wheel=N'" & SalesCertificate.Base_Wheel.Replace("'", "''") & "', Comments=N'" & SalesCertificate.Comments.Replace("'", "''") & "', SalesId=" & SalesCertificate.SalesId & ", SalesDetailId=" & SalesCertificate.SalesDetailId & ", UserName=N'" & SalesCertificate.UserName.Replace("'", "''") & "' WHERE SaleCertificateId=" & SalesCertificate.SaleCertificateId & ""
            'TaskId:2532 Revised Sales Certificate ,Add One Field of ModelCode In Update Query 

            Dim strSQL As String = String.Empty
            'Before against task:2708
            'Before against task:2678
            'strSQL = "UPDATE SalesCertificateTable  SET  SaleCertificateNo=N'" & SalesCertificate.SaleCertificateNo.Replace("'", "''") & "', SaleCertificateDate=N'" & SalesCertificate.SaleCertificateDate.ToString("yyyy-M-d h:mm:ss tt") & "', DeliveredTo=N'" & SalesCertificate.DeliveredTo.Replace("'", "''") & "', Engine_No=N'" & SalesCertificate.Engine_No.Replace("'", "''") & "', Chassis_No=N'" & SalesCertificate.Chassis_No.Replace("'", "''") & "', ArticleDefId=N'" & SalesCertificate.ArticleDefId & "', ModelCode=N'" & SalesCertificate.ModelCode.Replace("'", "''") & "', Model_Desc=N'" & SalesCertificate.Model_Desc.Replace("'", "''") & "', Max_Laden_Weight=N'" & SalesCertificate.Max_Laden_Weight.Replace("'", "''") & "', Max_Weight_Front_Axel=N'" & SalesCertificate.Max_Weight_Front_Axel.Replace("'", "''") & "', Max_Weight_Rear_Axel=N'" & SalesCertificate.Max_Weight_Rear_Axel.Replace("'", "''") & "', Tyre_Front_Wheel=N'" & SalesCertificate.Tyre_Front_Wheel.Replace("'", "''") & "', Tyre_Rear_Wheel=N'" & SalesCertificate.Tyre_Rear_Wheel.Replace("'", "''") & "', Base_Wheel=N'" & SalesCertificate.Base_Wheel.Replace("'", "''") & "', Comments=N'" & SalesCertificate.Comments.Replace("'", "''") & "', SalesId=" & SalesCertificate.SalesId & ", SalesDetailId=" & SalesCertificate.SalesDetailId & ", UserName=N'" & SalesCertificate.UserName.Replace("'", "''") & "' WHERE SaleCertificateId=" & SalesCertificate.SaleCertificateId & ""
            'Task:2678 Added Field InvoiceAmount, SalesTax
            'strSQL = "UPDATE SalesCertificateTable  SET  SaleCertificateNo=N'" & SalesCertificate.SaleCertificateNo.Replace("'", "''") & "', SaleCertificateDate=N'" & SalesCertificate.SaleCertificateDate.ToString("yyyy-M-d h:mm:ss tt") & "', DeliveredTo=N'" & SalesCertificate.DeliveredTo.Replace("'", "''") & "', Engine_No=N'" & SalesCertificate.Engine_No.Replace("'", "''") & "', Chassis_No=N'" & SalesCertificate.Chassis_No.Replace("'", "''") & "', ArticleDefId=N'" & SalesCertificate.ArticleDefId & "', ModelCode=N'" & SalesCertificate.ModelCode.Replace("'", "''") & "', Model_Desc=N'" & SalesCertificate.Model_Desc.Replace("'", "''") & "', Max_Laden_Weight=N'" & SalesCertificate.Max_Laden_Weight.Replace("'", "''") & "', Max_Weight_Front_Axel=N'" & SalesCertificate.Max_Weight_Front_Axel.Replace("'", "''") & "', Max_Weight_Rear_Axel=N'" & SalesCertificate.Max_Weight_Rear_Axel.Replace("'", "''") & "', Tyre_Front_Wheel=N'" & SalesCertificate.Tyre_Front_Wheel.Replace("'", "''") & "', Tyre_Rear_Wheel=N'" & SalesCertificate.Tyre_Rear_Wheel.Replace("'", "''") & "', Base_Wheel=N'" & SalesCertificate.Base_Wheel.Replace("'", "''") & "', Comments=N'" & SalesCertificate.Comments.Replace("'", "''") & "', SalesId=" & SalesCertificate.SalesId & ", SalesDetailId=" & SalesCertificate.SalesDetailId & ", UserName=N'" & SalesCertificate.UserName.Replace("'", "''") & "', InvoiceAmount=" & SalesCertificate.InvoiceAmount & ", SalesTax=" & SalesCertificate.SalesTax & ", Address=N'" & SalesCertificate.Address.Replace("'", "''") & "', NTN=N'" & SalesCertificate.NTN.Replace("'", "''") & "' WHERE SaleCertificateId=" & SalesCertificate.SaleCertificateId & ""
            'End Task:2678
            'TAsk:2708
            'strSQL = "UPDATE SalesCertificateTable  SET  SaleCertificateNo=N'" & SalesCertificate.SaleCertificateNo.Replace("'", "''") & "', SaleCertificateDate=N'" & SalesCertificate.SaleCertificateDate.ToString("yyyy-M-d h:mm:ss tt") & "', DeliveredTo=N'" & SalesCertificate.DeliveredTo.Replace("'", "''") & "', Engine_No=N'" & SalesCertificate.Engine_No.Replace("'", "''") & "', Chassis_No=N'" & SalesCertificate.Chassis_No.Replace("'", "''") & "', ArticleDefId=N'" & SalesCertificate.ArticleDefId & "', ModelCode=N'" & SalesCertificate.ModelCode.Replace("'", "''") & "', Model_Desc=N'" & SalesCertificate.Model_Desc.Replace("'", "''") & "', Max_Laden_Weight=N'" & SalesCertificate.Max_Laden_Weight.Replace("'", "''") & "', Max_Weight_Front_Axel=N'" & SalesCertificate.Max_Weight_Front_Axel.Replace("'", "''") & "', Max_Weight_Rear_Axel=N'" & SalesCertificate.Max_Weight_Rear_Axel.Replace("'", "''") & "', Tyre_Front_Wheel=N'" & SalesCertificate.Tyre_Front_Wheel.Replace("'", "''") & "', Tyre_Rear_Wheel=N'" & SalesCertificate.Tyre_Rear_Wheel.Replace("'", "''") & "', Base_Wheel=N'" & SalesCertificate.Base_Wheel.Replace("'", "''") & "', Comments=N'" & SalesCertificate.Comments.Replace("'", "''") & "', SalesId=" & SalesCertificate.SalesId & ", SalesDetailId=" & SalesCertificate.SalesDetailId & ", UserName=N'" & SalesCertificate.UserName.Replace("'", "''") & "', InvoiceAmount=" & SalesCertificate.InvoiceAmount & ", SalesTax=" & SalesCertificate.SalesTax & ", Address=N'" & SalesCertificate.Address.Replace("'", "''") & "', NTN=N'" & SalesCertificate.NTN.Replace("'", "''") & "', RegistrationFor=N'" & SalesCertificate.RegistrationFor.Replace("'", "''") & "', Tax_Percent=" & SalesCertificate.Tax_Percent & " WHERE SaleCertificateId=" & SalesCertificate.SaleCertificateId & ""
            'End Task:2708
            'Task:2717 Added Field Reference_No
            'strSQL = "UPDATE SalesCertificateTable  SET  SaleCertificateNo=N'" & SalesCertificate.SaleCertificateNo.Replace("'", "''") & "', SaleCertificateDate=N'" & SalesCertificate.SaleCertificateDate.ToString("yyyy-M-d h:mm:ss tt") & "', DeliveredTo=N'" & SalesCertificate.DeliveredTo.Replace("'", "''") & "', Engine_No=N'" & SalesCertificate.Engine_No.Replace("'", "''") & "', Chassis_No=N'" & SalesCertificate.Chassis_No.Replace("'", "''") & "', ArticleDefId=N'" & SalesCertificate.ArticleDefId & "', ModelCode=N'" & SalesCertificate.ModelCode.Replace("'", "''") & "', Model_Desc=N'" & SalesCertificate.Model_Desc.Replace("'", "''") & "', Max_Laden_Weight=N'" & SalesCertificate.Max_Laden_Weight.Replace("'", "''") & "', Max_Weight_Front_Axel=N'" & SalesCertificate.Max_Weight_Front_Axel.Replace("'", "''") & "', Max_Weight_Rear_Axel=N'" & SalesCertificate.Max_Weight_Rear_Axel.Replace("'", "''") & "', Tyre_Front_Wheel=N'" & SalesCertificate.Tyre_Front_Wheel.Replace("'", "''") & "', Tyre_Rear_Wheel=N'" & SalesCertificate.Tyre_Rear_Wheel.Replace("'", "''") & "', Base_Wheel=N'" & SalesCertificate.Base_Wheel.Replace("'", "''") & "', Comments=N'" & SalesCertificate.Comments.Replace("'", "''") & "', SalesId=" & SalesCertificate.SalesId & ", SalesDetailId=" & SalesCertificate.SalesDetailId & ", UserName=N'" & SalesCertificate.UserName.Replace("'", "''") & "', InvoiceAmount=" & SalesCertificate.InvoiceAmount & ", SalesTax=" & SalesCertificate.SalesTax & ", Address=N'" & SalesCertificate.Address.Replace("'", "''") & "', NTN=N'" & SalesCertificate.NTN.Replace("'", "''") & "', RegistrationFor=N'" & SalesCertificate.RegistrationFor.Replace("'", "''") & "', Tax_Percent=" & SalesCertificate.Tax_Percent & ",Reference_No=N'" & SalesCertificate.Reference_No.Replace("'", "''") & "' WHERE SaleCertificateId=" & SalesCertificate.SaleCertificateId & ""
            'End Taks:2717
            'Task:2788 Added Color Field
            strSQL = "UPDATE SalesCertificateTable  SET  SaleCertificateNo=N'" & SalesCertificate.SaleCertificateNo.Replace("'", "''") & "', SaleCertificateDate=N'" & SalesCertificate.SaleCertificateDate.ToString("yyyy-M-d h:mm:ss tt") & "', DeliveredTo=N'" & SalesCertificate.DeliveredTo.Replace("'", "''") & "', Engine_No=N'" & SalesCertificate.Engine_No.Replace("'", "''") & "', Chassis_No=N'" & SalesCertificate.Chassis_No.Replace("'", "''") & "', ArticleDefId=N'" & SalesCertificate.ArticleDefId & "', ModelCode=N'" & SalesCertificate.ModelCode.Replace("'", "''") & "', Model_Desc=N'" & SalesCertificate.Model_Desc.Replace("'", "''") & "', Max_Laden_Weight=N'" & SalesCertificate.Max_Laden_Weight.Replace("'", "''") & "', Max_Weight_Front_Axel=N'" & SalesCertificate.Max_Weight_Front_Axel.Replace("'", "''") & "', Max_Weight_Rear_Axel=N'" & SalesCertificate.Max_Weight_Rear_Axel.Replace("'", "''") & "', Tyre_Front_Wheel=N'" & SalesCertificate.Tyre_Front_Wheel.Replace("'", "''") & "', Tyre_Rear_Wheel=N'" & SalesCertificate.Tyre_Rear_Wheel.Replace("'", "''") & "', Base_Wheel=N'" & SalesCertificate.Base_Wheel.Replace("'", "''") & "', Comments=N'" & SalesCertificate.Comments.Replace("'", "''") & "', SalesId=" & SalesCertificate.SalesId & ", SalesDetailId=" & SalesCertificate.SalesDetailId & ", UserName=N'" & SalesCertificate.UserName.Replace("'", "''") & "', InvoiceAmount=" & SalesCertificate.InvoiceAmount & ", SalesTax=" & SalesCertificate.SalesTax & ", Address=N'" & SalesCertificate.Address.Replace("'", "''") & "', NTN=N'" & SalesCertificate.NTN.Replace("'", "''") & "', RegistrationFor=N'" & SalesCertificate.RegistrationFor.Replace("'", "''") & "', Tax_Percent=" & SalesCertificate.Tax_Percent & ",Reference_No=N'" & SalesCertificate.Reference_No.Replace("'", "''") & "',Color=N'" & SalesCertificate.Color.Replace("'", "''") & "' ,DcNo=N'" & SalesCertificate.DC_NO.Replace("'", "''") & "',Remarks=N'" & SalesCertificate.Remarks.Replace("'", "''") & "', FatherName=N'" & SalesCertificate.FatherName.Replace("'", "''") & "',PersonCast=N'" & SalesCertificate.Person_Cast.Replace("'", "''") & "',AdvanceAmount=" & Val(SalesCertificate.AdvanceAmount) & ",MeterNo=N'" & SalesCertificate.MeterNo.Replace("'", "''") & "',Installment=" & SalesCertificate.Installment & ",RegistrationNo=N'" & SalesCertificate.RegistrationNo.Replace("'", "''") & "',ContractDate=" & IIf(SalesCertificate.ContractDate = DateTime.MinValue, "NULL", "Convert(dateTime,'" & SalesCertificate.ContractDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & " WHERE SaleCertificateId=" & SalesCertificate.SaleCertificateId & ""
            'End Task:2788
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)


            strSQL = String.Empty
            'Ahmad Sharif: added two column DcNo and Remarsk in update query
            strSQL = "Update SalesDetailTable SET SaleCertificateId=" & SalesCertificate.SaleCertificateId & " WHERE SalesId=" & SalesCertificate.SalesId & " AND SaleDetailId=" & SalesCertificate.SalesDetailId & " AND ArticleDefId=" & SalesCertificate.ArticleDefId & ""
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

            objTrans.Commit()
            Return True

        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Delete(ByVal SalesCertificate As SalesCertificateBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim objTrans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty
            strSQL = "Delete From SalesCertificateTable WHERE SaleCertificateId=" & SalesCertificate.SaleCertificateId & ""
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

            strSQL = String.Empty
            strSQL = "Update SalesDetailTable SET SaleCertificateId=NULL WHERE SalesId=" & SalesCertificate.SalesId & " AND SaleDetailId=" & SalesCertificate.SalesDetailId & " AND ArticleDefId=" & SalesCertificate.ArticleDefId & ""
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

            objTrans.Commit()
            Return True

        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    ''' <summary>
    ''' Thsi Function Is made to Return The Issued Certificate
    ''' </summary>
    ''' <param name="SalesCertificate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReturnCertificate(ByVal SalesCertificate As SalesCertificateBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim objTrans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty
            strSQL = "Delete From SalesCertificateTable WHERE SaleCertificateId=" & SalesCertificate.SaleCertificateId & ""
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

            strSQL = String.Empty
            strSQL = "Update SalesDetailTable SET SaleCertificateId=NULL , IsReturnedSalesCertificate = 1 WHERE SalesId=" & SalesCertificate.SalesId & " AND SaleDetailId=" & SalesCertificate.SalesDetailId & " AND ArticleDefId=" & SalesCertificate.ArticleDefId & ""
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

            strSQL = String.Empty
            strSQL = "INSERT INTO SalesCertificateReturnTable(SaleCertificateReturnNo, SaleCertificateReturnDate, DeliveredTo, Engine_No, Chassis_No, ArticleDefId, ModelCode,Model_Desc, Max_Laden_Weight, Max_Weight_Front_Axel, Max_Weight_Rear_Axel, Tyre_Front_Wheel, Tyre_Rear_Wheel, Base_Wheel, Comments, SalesId, SalesDetailId, UserName, InvoiceAmount,SalesTax, Address, NTN,RegistrationFor,Tax_Percent,Reference_No,Color,DcNo,Remarks, FatherName,PersonCast,AdvanceAmount,MeterNo,Installment,RegistrationNo,ContractDate) " _
                     & " VALUES(N'" & SalesCertificate.SaleCertificateNo.Replace("'", "''") & "', N'" & SalesCertificate.SaleCertificateDate.ToString("yyyy-M-d h:mm:ss tt") & "',N'" & SalesCertificate.DeliveredTo.Replace("'", "''") & "', N'" & SalesCertificate.Engine_No.Replace("'", "''") & "', " _
                     & " N'" & SalesCertificate.Chassis_No.Replace("'", "''") & "', " & SalesCertificate.ArticleDefId & ", N'" & SalesCertificate.ModelCode.Replace("'", "''") & "',N'" & SalesCertificate.Model_Desc.Replace("'", "''") & "',  N'" & SalesCertificate.Max_Laden_Weight.Replace("'", "''") & "', N'" & SalesCertificate.Max_Weight_Front_Axel.Replace("'", "''") & "', " _
                     & " N'" & SalesCertificate.Max_Weight_Rear_Axel.Replace("'", "''") & "', N'" & SalesCertificate.Tyre_Front_Wheel.Replace("'", "''") & "', N'" & SalesCertificate.Tyre_Rear_Wheel.Replace("'", "''") & "', N'" & SalesCertificate.Base_Wheel.Replace("'", "''") & "', N'" & SalesCertificate.Comments.Replace("'", "''") & "', " & SalesCertificate.SalesId & ", " & SalesCertificate.SalesDetailId & ", N'" & SalesCertificate.UserName.Replace("'", "''") & "', " & SalesCertificate.InvoiceAmount & ", " & SalesCertificate.SalesTax & ", N'" & SalesCertificate.Address.Replace("'", "''") & "', N'" & SalesCertificate.NTN.Replace("'", "''") & "', N'" & SalesCertificate.RegistrationFor.Replace("'", "''") & "', " & SalesCertificate.Tax_Percent & ", N'" & SalesCertificate.Reference_No.Replace("'", "''") & "',N'" & SalesCertificate.Color.Replace("'", "''") & "',N'" & SalesCertificate.DC_NO.Replace("'", "''") & "', N'" & SalesCertificate.Remarks.Replace("'", "''") & "', N'" & SalesCertificate.FatherName.Replace("'", "''") & "',N'" & SalesCertificate.Person_Cast.Replace("'", "''") & "'," & Val(SalesCertificate.AdvanceAmount) & ",N'" & SalesCertificate.MeterNo.Replace("'", "''") & "'," & SalesCertificate.Installment & ",N'" & SalesCertificate.RegistrationNo.Replace("'", "''") & "'," & IIf(SalesCertificate.ContractDate = DateTime.MinValue, "NULL", "Convert(dateTime,'" & SalesCertificate.ContractDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102)") & ")Select @@Identity"

            SalesCertificate.SaleCertificateId = SQLHelper.ExecuteScaler(objTrans, CommandType.Text, strSQL)

            strSQL = String.Empty
            strSQL = "Update SalesDetailTable SET SaleCertificateReturnId = " & SalesCertificate.SaleCertificateId & " WHERE SalesId=" & SalesCertificate.SalesId & " AND SaleDetailId=" & SalesCertificate.SalesDetailId & " AND ArticleDefId=" & SalesCertificate.ArticleDefId & ""
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

            objTrans.Commit()
            Return True

        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetNextDocNo(ByVal Prefix As String, Optional ByVal objtrans As SqlTransaction = Nothing) As String
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Select Isnull(Max(Right(SaleCertificateNo,5)),0)+1 From SalesCertificateTable WHERE LEFT(SaleCertificateNo," & Prefix.Length & ")=N'" & Prefix.Replace("'", "''") & "'"
            Dim dt As New DataTable
            Dim Serial As Integer = 0I
            Dim SerialNo As String = String.Empty
            dt = UtilityDAL.GetDataTable(strSQL, objtrans)
            If dt IsNot Nothing Then
                Serial = CInt(dt.Rows(0).Item(0).ToString)
            Else
                Serial = 1
            End If
            SerialNo = Prefix & Right("00000" & CStr(Serial), 5)
            Return SerialNo
        Catch ex As Exception
            If objtrans IsNot Nothing Then objtrans.Rollback()
            Throw ex
        End Try
    End Function
    ''Task No #4 Ali Ansari Gettinhg Auto Reference No
    Public Function GetNextRefNo(ByVal Prefix As String, Optional ByVal objtrans As SqlTransaction = Nothing) As String
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Select Isnull(Max(Right(Reference_No,4)),0)+1 From SalesCertificateTable WHERE LEFT(Reference_No," & Prefix.Length & ")=N'" & Prefix.Replace("'", "''") & "'"
            Dim dt As New DataTable
            Dim Serial As Integer = 0I
            Dim SerialNo As String = String.Empty
            dt = UtilityDAL.GetDataTable(strSQL, objtrans)
            If dt IsNot Nothing Then
                Serial = CInt(dt.Rows(0).Item(0).ToString)
            Else
                Serial = 1
            End If
            SerialNo = Prefix & Right("0000" & CStr(Serial), 4)
            Return SerialNo
        Catch ex As Exception
            If objtrans IsNot Nothing Then objtrans.Rollback()
            Throw ex
        End Try
    End Function
    ''Task No #4 Ali Ansari Gettinhg Auto Reference No

    Public Function GetAllRecords(Optional ByVal SalesNo As String = "", Optional ByVal EngineNo As String = "", Optional ByVal ChassisNo As String = "", Optional ByVal IsPending As Boolean = True, Optional IsIssued As Boolean = False, Optional IsReturned As Boolean = False, Optional VendorId As Integer = 0, Optional Remarks As String = "", Optional TopRecord As String = "") As DataTable
        Try
            Dim strSQL As String = String.Empty
            'Before against task:2708
            'strSQL = "SELECT dbo.SalesMasterTable.CustomerCode, COA.detail_code, COA.detail_title, dbo.SalesMasterTable.SalesId, dbo.SalesMasterTable.SalesNo, " _
            '       & " dbo.SalesMasterTable.SalesDate, Art.ArticleCode, Art.ArticleDescription, Sdt.Engine_No, Sdt.Chassis_No, Sdt.Qty, IsNull(Sdt.SaleCertificateId,0) as SaleCertificateId,Sdt.ArticleDefId, Sdt.SaleDetailId " _
            '       & " FROM dbo.SalesMasterTable INNER JOIN " _
            '       & " dbo.SalesDetailTable AS Sdt ON dbo.SalesMasterTable.SalesId = Sdt.SalesId INNER JOIN " _
            '       & " dbo.ArticleDefView AS Art ON Sdt.ArticleDefId = Art.ArticleId INNER JOIN " _
            '       & " dbo.vwCOADetail AS COA ON dbo.SalesMasterTable.CustomerCode = COA.coa_detail_id WHERE dbo.SalesMasterTable.CustomerCode <> 0 AND Sdt.Engine_No <> ''"
            'Task:2708 Added fields RegistrationFor, Tax_Percent
            'strSQL = "SELECT dbo.SalesMasterTable.CustomerCode, COA.detail_code, COA.detail_title, dbo.SalesMasterTable.SalesId, dbo.SalesMasterTable.SalesNo, " _
            '     & " dbo.SalesMasterTable.SalesDate, Art.ArticleCode, Art.ArticleDescription, Sdt.Engine_No, Sdt.Chassis_No, Sdt.Qty, IsNull(Sdt.SaleCertificateId,0) as SaleCertificateId,Sdt.ArticleDefId, Sdt.SaleDetailId " _
            '     & " FROM dbo.SalesMasterTable INNER JOIN " _
            '     & " dbo.SalesDetailTable AS Sdt ON dbo.SalesMasterTable.SalesId = Sdt.SalesId INNER JOIN " _
            '     & " dbo.ArticleDefView AS Art ON Sdt.ArticleDefId = Art.ArticleId INNER JOIN " _
            '     & " dbo.vwCOADetail AS COA ON dbo.SalesMasterTable.CustomerCode = COA.coa_detail_id WHERE dbo.SalesMasterTable.CustomerCode <> 0 AND Sdt.Engine_No <> ''"
            'Task:2708
            'Before against task:2717
            'strSQL = "SELECT dbo.SalesMasterTable.CustomerCode, COA.detail_code, COA.detail_title, dbo.SalesMasterTable.SalesId, dbo.SalesMasterTable.SalesNo, " _
            '   & " dbo.SalesMasterTable.SalesDate, Art.ArticleCode, Art.ArticleDescription, Sdt.Engine_No, Sdt.Chassis_No, Sdt.Qty, IsNull(Sdt.SaleCertificateId,0) as SaleCertificateId,Sdt.ArticleDefId, Sdt.SaleDetailId " _
            '   & " FROM dbo.SalesMasterTable INNER JOIN " _
            '   & " dbo.SalesDetailTable AS Sdt ON dbo.SalesMasterTable.SalesId = Sdt.SalesId INNER JOIN " _
            '   & " dbo.ArticleDefView AS Art ON Sdt.ArticleDefId = Art.ArticleId INNER JOIN " _
            '   & " dbo.vwCOADetail AS COA ON dbo.SalesMasterTable.CustomerCode = COA.coa_detail_id WHERE dbo.SalesMasterTable.CustomerCode <> 0 AND Sdt.Engine_No <> ''"
            'Task:2717 Added Field Reference_No

            'strSQL = "SELECT dbo.SalesMasterTable.CustomerCode, COA.detail_code, COA.detail_title, dbo.SalesMasterTable.SalesId, dbo.SalesMasterTable.SalesNo, " _
            ' & " dbo.SalesMasterTable.SalesDate, Art.ArticleCode, Art.ArticleDescription, Sdt.Engine_No, Sdt.Chassis_No, Sdt.Qty, IsNull(Sdt.SaleCertificateId,0) as SaleCertificateId,Sdt.ArticleDefId, Sdt.SaleDetailId " _
            ' & " FROM dbo.SalesMasterTable INNER JOIN " _
            ' & " dbo.SalesDetailTable AS Sdt ON dbo.SalesMasterTable.SalesId = Sdt.SalesId INNER JOIN " _
            ' & " dbo.ArticleDefView AS Art ON Sdt.ArticleDefId = Art.ArticleId INNER JOIN " _
            ' & " dbo.vwCOADetail AS COA ON dbo.SalesMasterTable.CustomerCode = COA.coa_detail_id WHERE dbo.SalesMasterTable.CustomerCode <> 0 AND Sdt.Engine_No <> ''"

            'Ahmad Sharif : Modify the strSQL query and add two new columns in it, Modification on 04-06-2015
            'strSQL = "SELECT " & IIf(TopRecord = "", "", "TOP 50") & " dbo.SalesMasterTable.CustomerCode,dbo.SalesMasterTable.DcNO, dbo.SalesMasterTable.Remarks , COA.detail_code, COA.detail_title, dbo.SalesMasterTable.SalesId, dbo.SalesMasterTable.SalesNo, " _
            ' & " dbo.SalesMasterTable.SalesDate, Art.ArticleCode, Art.ArticleDescription, Sdt.Engine_No, Sdt.Chassis_No, Sdt.Qty, IsNull(Sdt.SaleCertificateId,0) as SaleCertificateId,Sdt.ArticleDefId, Sdt.SaleDetailId " _
            ' & " FROM dbo.SalesMasterTable INNER JOIN " _
            ' & " dbo.SalesDetailTable AS Sdt ON dbo.SalesMasterTable.SalesId = Sdt.SalesId INNER JOIN " _
            ' & " dbo.ArticleDefView AS Art ON Sdt.ArticleDefId = Art.ArticleId INNER JOIN " _
            ' & " dbo.vwCOADetail AS COA ON dbo.SalesMasterTable.CustomerCode = COA.coa_detail_id WHERE dbo.SalesMasterTable.CustomerCode <> 0 AND Sdt.Engine_No <> ''"

            'End Task:2717


            ''Commented Against TFS3524
            '' strSQL = "SELECT " & IIf(TopRecord = "", "", "TOP 50") & " dbo.SalesMasterTable.CustomerCode,dbo.SalesMasterTable.DcNO, dbo.SalesMasterTable.Remarks , COA.detail_code, COA.detail_title, dbo.SalesMasterTable.SalesId, dbo.SalesMasterTable.SalesNo, " _
            ''& " dbo.SalesMasterTable.SalesDate, Art.ArticleCode, Art.ArticleDescription, Sdt.Engine_No, Sdt.Chassis_No, Sdt.Qty, IsNull(Sdt.SaleCertificateId,0) as SaleCertificateId,Sdt.ArticleDefId, Sdt.SaleDetailId, Sdt.Other_Comments As [Other Comments] " _
            ''& " FROM dbo.SalesMasterTable INNER JOIN " _
            ''& " dbo.SalesDetailTable AS Sdt ON dbo.SalesMasterTable.SalesId = Sdt.SalesId INNER JOIN " _
            ''& " dbo.ArticleDefView AS Art ON Sdt.ArticleDefId = Art.ArticleId INNER JOIN " _
            ''& " dbo.vwCOADetail AS COA ON dbo.SalesMasterTable.CustomerCode = COA.coa_detail_id WHERE dbo.SalesMasterTable.CustomerCode <> 0 AND Sdt.Engine_No <> ''"
            ''Start TFS3524 : Ayesha Rehman: 13-06-2018
            strSQL = "SELECT " & IIf(TopRecord = "", "", "TOP 50") & " dbo.SalesMasterTable.CustomerCode,dbo.SalesMasterTable.DcNO, dbo.SalesMasterTable.Remarks , COA.detail_code, COA.detail_title, dbo.SalesMasterTable.SalesId, dbo.SalesMasterTable.SalesNo, " _
           & " dbo.SalesMasterTable.SalesDate, Art.ArticleCode, Art.ArticleDescription, Sdt.Engine_No, Sdt.Chassis_No, Sdt.Qty, IsNull(Sdt.SaleCertificateId,0) as SaleCertificateId,IsNull(Sdt.IsReturnedSalesCertificate,0) as IsReturnedSalesCertificate,IsNull(Sdt.SaleCertificateReturnId,0) as SaleCertificateReturnId, Sdt.ArticleDefId, Sdt.SaleDetailId, Sdt.Other_Comments As [Other Comments] , 0 as InReturn " _
           & " FROM dbo.SalesMasterTable INNER JOIN " _
           & " dbo.SalesDetailTable AS Sdt ON dbo.SalesMasterTable.SalesId = Sdt.SalesId INNER JOIN " _
           & " dbo.ArticleDefView AS Art ON Sdt.ArticleDefId = Art.ArticleId INNER JOIN " _
           & " dbo.vwCOADetail AS COA ON dbo.SalesMasterTable.CustomerCode = COA.coa_detail_id WHERE dbo.SalesMasterTable.CustomerCode <> 0 "

            If SalesNo.Length > 0 Then
                strSQL += " AND dbo.SalesMasterTable.SalesNo LIKE '%" & SalesNo & "%'"
            End If
            If EngineNo.Length > 0 Then
                strSQL += " AND sdt.Engine_No LIKE N'%" & EngineNo & "%'"
            End If
            If ChassisNo.Length > 0 Then
                strSQL += " AND sdt.Chassis_No LIKE N'%" & ChassisNo & "%'"
            End If
            ''12-Sep-2014 Task:2841 Imran Ali Retrive Last Record In Sales Certificate
            If IsPending = True Then
                strSQL += " AND IsNull(SaleCertificateId,0)=0"
            End If
            If IsIssued = True Then
                strSQL += " AND IsNull(SaleCertificateId,0) > 0 "
            End If
            ''Start TFS3524
            If IsReturned = True Then
                strSQL += " AND (IsNull(SaleCertificateId,0)=0 And IsNull(IsReturnedSalesCertificate,0) = 1) "
            End If
            ''End TFS3524
            If IsPending = False AndAlso IsIssued = False Then
                strSQL += " AND (IsNull(SaleCertificateId,0) >=0 or IsNull(IsReturnedSalesCertificate,0) = 1) "
            End If
            If VendorId > 0 Then
                strSQL += " AND IsNull(dbo.SalesMasterTable.CustomerCode,0)=" & VendorId & ""
            End If
            If Remarks.Length > 0 Then
                strSQL += " AND dbo.SalesMasterTable.Remarks LIKE '%" & Remarks.Replace("'", "''") & "%'"
            End If
            'End Task:2841
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAll(Optional ByVal SalesNo As String = "", Optional ByVal EngineNo As String = "", Optional ByVal ChassisNo As String = "", Optional VendorId As Integer = 0, Optional Remarks As String = "", Optional TopRecord As String = "") As DataTable
        Try
            Dim strSQL As String = String.Empty
            strSQL = "SELECT " & IIf(TopRecord = "", "", "TOP 50") & " dbo.SalesMasterTable.CustomerCode,dbo.SalesMasterTable.DcNO, dbo.SalesMasterTable.Remarks , COA.detail_code, COA.detail_title, dbo.SalesMasterTable.SalesId, dbo.SalesMasterTable.SalesNo, " _
& " dbo.SalesMasterTable.SalesDate, Art.ArticleCode, Art.ArticleDescription, Sdt.Engine_No, Sdt.Chassis_No, Sdt.Qty, IsNull(Sdt.SaleCertificateId,0) as SaleCertificateId,IsNull(Sdt.IsReturnedSalesCertificate,0) as IsReturnedSalesCertificate, IsNull(Sdt.SaleCertificateReturnId,0) as SaleCertificateReturnId , Sdt.ArticleDefId, Sdt.SaleDetailId, Sdt.Other_Comments As [Other Comments] , 1 as InReturn" _
& " FROM dbo.SalesMasterTable INNER JOIN " _
& " dbo.SalesDetailTable AS Sdt ON dbo.SalesMasterTable.SalesId = Sdt.SalesId INNER JOIN " _
& " dbo.ArticleDefView AS Art ON Sdt.ArticleDefId = Art.ArticleId INNER JOIN " _
& " dbo.vwCOADetail AS COA ON dbo.SalesMasterTable.CustomerCode = COA.coa_detail_id INNER JOIN " _
& " SalesCertificateReturnTable AS SR on Sdt.ArticleDefId = SR.ArticleDefId and Sdt.SalesId = SR.SalesId And Sdt.SaleDetailId = SR.SalesDetailId  WHERE dbo.SalesMasterTable.CustomerCode <> 0 AND Sdt.Engine_No <> ''"

            If SalesNo.Length > 0 Then
                strSQL += " AND dbo.SalesMasterTable.SalesNo LIKE '%" & SalesNo & "%'"
            End If
            If EngineNo.Length > 0 Then
                strSQL += " AND sdt.Engine_No LIKE N'%" & EngineNo & "%'"
            End If
            If ChassisNo.Length > 0 Then
                strSQL += " AND sdt.Chassis_No LIKE N'%" & ChassisNo & "%'"
            End If
            If VendorId > 0 Then
                strSQL += " AND IsNull(dbo.SalesMasterTable.CustomerCode,0)=" & VendorId & ""
            End If
            If Remarks.Length > 0 Then
                strSQL += " AND dbo.SalesMasterTable.Remarks LIKE '%" & Remarks.Replace("'", "''") & "%'"
            End If
            strSQL += " Union All SELECT " & IIf(TopRecord = "", "", "TOP 50") & " dbo.SalesMasterTable.CustomerCode,dbo.SalesMasterTable.DcNO, dbo.SalesMasterTable.Remarks , COA.detail_code, COA.detail_title, dbo.SalesMasterTable.SalesId, dbo.SalesMasterTable.SalesNo, " _
         & " dbo.SalesMasterTable.SalesDate, Art.ArticleCode, Art.ArticleDescription, Sdt.Engine_No, Sdt.Chassis_No, Sdt.Qty, IsNull(Sdt.SaleCertificateId,0) as SaleCertificateId,IsNull(Sdt.IsReturnedSalesCertificate,0) as IsReturnedSalesCertificate,IsNull(Sdt.SaleCertificateReturnId,0) as SaleCertificateReturnId, Sdt.ArticleDefId, Sdt.SaleDetailId, Sdt.Other_Comments As [Other Comments] , 0 as InReturn" _
         & " FROM dbo.SalesMasterTable INNER JOIN " _
         & " dbo.SalesDetailTable AS Sdt ON dbo.SalesMasterTable.SalesId = Sdt.SalesId INNER JOIN " _
         & " dbo.ArticleDefView AS Art ON Sdt.ArticleDefId = Art.ArticleId INNER JOIN " _
         & " dbo.vwCOADetail AS COA ON dbo.SalesMasterTable.CustomerCode = COA.coa_detail_id WHERE dbo.SalesMasterTable.CustomerCode <> 0"

            If SalesNo.Length > 0 Then
                strSQL += " AND dbo.SalesMasterTable.SalesNo LIKE '%" & SalesNo & "%'"
            End If
            If EngineNo.Length > 0 Then
                strSQL += " AND sdt.Engine_No LIKE N'%" & EngineNo & "%'"
            End If
            If ChassisNo.Length > 0 Then
                strSQL += " AND sdt.Chassis_No LIKE N'%" & ChassisNo & "%'"
            End If
            If VendorId > 0 Then
                strSQL += " AND IsNull(dbo.SalesMasterTable.CustomerCode,0)=" & VendorId & ""
            End If
            If Remarks.Length > 0 Then
                strSQL += " AND dbo.SalesMasterTable.Remarks LIKE '%" & Remarks.Replace("'", "''") & "%'"
            End If

            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAllRecordsofReturn(Optional ByVal SalesNo As String = "", Optional ByVal EngineNo As String = "", Optional ByVal ChassisNo As String = "", Optional VendorId As Integer = 0, Optional Remarks As String = "", Optional TopRecord As String = "") As DataTable
        Try
            Dim strSQL As String = String.Empty
            strSQL = "SELECT " & IIf(TopRecord = "", "", "TOP 50") & " dbo.SalesMasterTable.CustomerCode,dbo.SalesMasterTable.DcNO, dbo.SalesMasterTable.Remarks , COA.detail_code, COA.detail_title, dbo.SalesMasterTable.SalesId, dbo.SalesMasterTable.SalesNo, " _
        & " dbo.SalesMasterTable.SalesDate, Art.ArticleCode, Art.ArticleDescription, Sdt.Engine_No, Sdt.Chassis_No, Sdt.Qty, IsNull(Sdt.SaleCertificateId,0) as SaleCertificateId,IsNull(Sdt.IsReturnedSalesCertificate,0) as IsReturnedSalesCertificate, IsNull(Sdt.SaleCertificateReturnId,0) as SaleCertificateReturnId , Sdt.ArticleDefId, Sdt.SaleDetailId, Sdt.Other_Comments As [Other Comments] , 1 as InReturn" _
        & " FROM dbo.SalesMasterTable INNER JOIN " _
        & " dbo.SalesDetailTable AS Sdt ON dbo.SalesMasterTable.SalesId = Sdt.SalesId INNER JOIN " _
        & " dbo.ArticleDefView AS Art ON Sdt.ArticleDefId = Art.ArticleId INNER JOIN " _
        & " dbo.vwCOADetail AS COA ON dbo.SalesMasterTable.CustomerCode = COA.coa_detail_id INNER JOIN " _
        & " SalesCertificateReturnTable AS SR on Sdt.ArticleDefId = SR.ArticleDefId and Sdt.SalesId = SR.SalesId And Sdt.SaleDetailId = SR.SalesDetailId  WHERE dbo.SalesMasterTable.CustomerCode <> 0 AND Sdt.Engine_No <> ''"

            If SalesNo.Length > 0 Then
                strSQL += " AND dbo.SalesMasterTable.SalesNo LIKE '%" & SalesNo & "%'"
            End If
            If EngineNo.Length > 0 Then
                strSQL += " AND sdt.Engine_No LIKE N'%" & EngineNo & "%'"
            End If
            If ChassisNo.Length > 0 Then
                strSQL += " AND sdt.Chassis_No LIKE N'%" & ChassisNo & "%'"
            End If
            If VendorId > 0 Then
                strSQL += " AND IsNull(dbo.SalesMasterTable.CustomerCode,0)=" & VendorId & ""
            End If
            If Remarks.Length > 0 Then
                strSQL += " AND dbo.SalesMasterTable.Remarks LIKE '%" & Remarks.Replace("'", "''") & "%'"
            End If
            strSQL += " order by SR.SaleCertificateReturnNo desc"
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
