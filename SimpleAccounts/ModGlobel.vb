''2-Jan-2014   Tsk:2367    Imran Ali             Calculation Problem
''03-Mar-2014  Task:2452    Imran Ali  4-ALPHABETIC order of items in sale and purchase window
''10-Mar-2014   Task:2483  Imran Ali  Cutomer Balance Not Match in Daily Working Report
''14-Mar-2014 TASK:2491  Imran Ali  Numeric Validation 
'Task No 2543 Append One Function For Validation Of Numeric Values 
''18-Apr-2014 TASK:2577 Imran Ali Send Branded SMS Functionlity
''28-4-2014 TASK:M35 Imran Ali Show All Record In History Of InvoiceBased Payment/Receipt
''9-May-2014 TASK:2618 Imran Ali Sort Order Unit List On Stock Adjustment 
''26-May-2014 TASK:M43 Imran Ali DateTimePicker Format
''13-Sep-2014 Task:2844 Imran Ali Posted Balance Show On Invoice Based Receipt/Payment
''13-Sep-2014 Task:2845 Imran Ali Send SMS on Invoice Based Payment/Receipt
''15-Jun-2015 TASK#1 Ahmad Sharif add user emial parameter
''25-Jun-2015 Task#125062015 Ahmad Sharif: global function for date lock
'14-Jul-2015 Task#14072015 Ahmad Sharif: Getting prefix from database based on document date and module
'25-Jul-2015 'Task#25072015 Ahmad Sharif: Calculating working days and holidays in current running month
'27-Jul-2015 'Task#27072015 Ahmad Sharif: Update Woriking Days Configuratin in ConfigValuesTable, Function for Getting server date
'01-Aug-2015 Task#01082015 Ahmad Sharif: Send Employee Attendance Report via Email 
'03-Aug-2015 Task#03082015 Ahmad Sharif: Checking how many Application Instances are opened in Same Machine
'21-10-2015 Task211015 Muhammad Ameen: Remove equal sign between current and document date from IsDateLock
''27-10-15 TASKM2710151 Imran Ali: Create Duplication Voucher On Update Mode.


Imports SBDal
Imports SBModel
Imports System.Data.OleDb
Imports System.Security.Cryptography
Imports Infragistics.Win.FormattedLinkLabel
Imports System.Data.SqlClient
Imports System.Net
Imports System.IO
Imports System.Net.Mail
Imports System.Text.RegularExpressions
Imports Ionic.Zip
Imports System.Net.NetworkInformation
Imports System.Management
Imports Janus.Windows.GridEX
Imports System.Security.AccessControl
Imports System.Text  'Added by Syed Irfan Ahmad on 19 Feb 2018, Task 2411
Imports Microsoft.Office.Interop

Module ModGlobel
    Public Enum EnmGroupRights
        FormId
        FormName
        FormControlId
        FormControlName
        Rights
        UserId
    End Enum
    Public GetEmailConfig As List(Of EmailSeeting)

    Public LicenseKey As String
    Public LicenseExpiry As DateTime = "31-Dec-2016 23:59:59"
    Public LicenseExpiryType As String 'e.g Software will be expired on expiry date
    Public LicenseVersion As String
    Public LicenseModuleList As String
    Public LicenseSystemId As String
    Public LicenseSystemId1 As String = String.Empty
    Public LicenseSystemId2 As String = String.Empty
    Public LicenseStatus As String
    Public LicenseTerminals As Integer
    Public LicenseDBName As String = String.Empty
    Public LicenseUsers As String = String.Empty

    Public TitleOfDb As String = ""
    Public IsEnhancedSecurity As Boolean = False
    Public ConCompany As String
    Public ConServerName As String '= Con1.DataSource '"rai"
    Public ConDBName As String '= Con1.Database '"SimplePos"
    Public ConUserId As String '= "sa"
    Public ConPassword As String '= "Sa"
    Public ReportPath As String = String.Empty
    Public ConnectionString As String = "Provider=SQLOLEDB.1;Password=" & ConPassword & ";Persist Security Info=True;Connection Timeout=120;User ID=" & ConUserId & ";Initial Catalog=" & ConDBName & ";Data Source=" & ConServerName
    Public LoginUserCode As String
    Public LoginUserId As Integer
    Public LoginUserName As String
    Public LoginUserEmail As String         'TASK#1 Add Logged In user email
    Public LoginGroup As String
    Public LoginGroupId As Integer
    Public LoginDashBoardRights As Boolean
    Public ShowCostPriceRights As Boolean
    Public LoginUserPassword As String
    Public LoginUserRememberMe As Boolean
    Public str_ReportParam As String = ""
    'RAFAY: Task Start
    Public str_Company As String = "V-ERP (UAE)"
    Public str_Company1 As String = "V-ERP (PAK)"
    Public str_Company2 As String = "V-ERP (Remms-PAK)"
    Public str_Company3 As String = "V-ERP (Remms-UAE)"
    Public str_Company4 As String = "V-ERP (KSA)"
    Public str_Company5 As String = "V-ERP (MY)"
    Public companycountry As String = ""
    Public companyinitials As String
    Public CompanyPrefix As String = ""
    'Rafay : Task End
    Public str_Support_Email As String = "v@agriusit.com"
    Public str_Support_Phone As String = ""

    Public str_MessageHeader As String = "Agriusit"
    Public strZeroIndexItem As String = ".... Select Any Value ...."
    Public str_ConfirmSave As String = "Do you want to save?"
    Public str_ConfirmCreateVendorQuotation As String = "Do you want to create quotation against selected items?"
    Public str_ConfirmUpdate As String = "Do you want to update?"
    Public str_ConfirmDelete As String = "Do you want to delete?"
    Public str_ConfirmDeliver As String = "Do you Want to Deliver?"
    Public str_ConfirmGridClear As String = "Do you want to clear existing records?"
    Public str_ConfirmRefresh As String = "Do you want to refresh?"

    Public str_informSave As String = "Record saved successfully"
    Public str_informUpdate As String = "Record updated successfully"
    Public str_informDelete As String = "Record deleted successfully"
    Public str_informBackup As String = "Database Backup successfully"
    Public str_informRestore As String = "Database Restore successfully"

    Public str_ErrorViewRight As String = "You don't have rights to view this form"
    Public str_ErrorInvalidUser As String = "Invalid user name or password"
    Public str_ErrorInActiveUser As String = "User is inactive, please contact your administrator"

    Public str_ErrorNoRecordFound As String = "No record found in the grid"
    Public str_ErrorNoRecordExist As String = "No record Exist"
    Public str_ErrorDependentRecordFound As String = "Sorry record can't be deleted because dependent record exist"
    Public str_ErrorDependentUpdateRecordFound As String = "Sorry record can't be update becuase order has been delivered"
    Public str_ErrorDependentUpdateRowFound As String = "Sorry this row has been updated you can't update it"
    Public str_ErrorPreviouseDateRecordUpdateAllow As String = "Sorry You are accessing from Previous date limit"
    Public str_ErrorPreviouseDateRecordDeleteAllow As String = "Sorry You are accessing from Previous date limit"
    Public str_ErrorStockNotEnough As String = String.Format("{0} stock is not enough", String.Empty)
    Public str_DisplayDateFormat As String = "dd/MMM/yyyy"
    Public fnt_FormsFont As System.Drawing.Font = New System.Drawing.Font("Verdana", 8, FontStyle.Regular, GraphicsUnit.Point)

    Public str_ApplicationStartUpPath As String = String.Empty
    Public gobjLocationId As Integer = 0
    Public str_Client_Name As String = String.Empty
    Public str_Client_Address As String = String.Empty
    Public UserPriceAllowedRights As Boolean
    Public UserPostingRights As Boolean
    Public CompanyNameHeader As String = String.Empty
    Public CompanyAddressHeader As String = String.Empty
    Public ShowCompanyAddress As Boolean = False
    Public GroupRights As List(Of GroupRights) 'GroupRights Variable Array
    Public Rights As List(Of GroupRights)
    Public GroupType As String = String.Empty
    Public IsDrillDown As Boolean = False
    Public TradePrice As Double = 0D
    Public Freight_Rate As Double = 0D
    Public MarketReturns_Rate As Double = 0D
    Public GST_Applicable As Boolean = False
    Public FlatRate_Applicable As Boolean = False
    Public FlatRate As Double = 0D
    Public IsAttachmentFile As Boolean = False
    Public IsEmailAlert As Boolean = False
    Public AdminEmail As String = String.Empty
    Public _FileExportPath As String = String.Empty
    Public _GetVersionInfo As String = String.Empty
    Public _CurrentRegisterStatus As String = String.Empty
    Public _EmployeePicPath As String = String.Empty
    Public _UserPicPath As String = String.Empty
    Public _CompanyLogoPath As String = String.Empty
    Public _ArticlePicPath As String = String.Empty
    Public _BackupDBPath As String = String.Empty
    Public Connection_String As String = String.Empty
    Public ConnectionStringMaster As String = String.Empty
    Public _SvrName As String = String.Empty
    Public _DB_Name As String = String.Empty
    Public _User_Id As String = String.Empty
    Public _DB_Password As String = String.Empty
    Public Get_ConnectionString As New SqlConnectionStringBuilder
    Public get_ConnectionStringMaster As New SqlConnectionStringBuilder
    Public _flgRestoreData As Boolean
    Public MyCompanyId As Integer = 0I
    Public MyCompanyRightsList As List(Of UserCompanyRightsBE)
    Public MyMainMenuViwRights As List(Of Users)
    Public MyLocationRightsList As List(Of UserLocationRightsBE)
    Public MyCompany As List(Of CompanyInfo)
    Public DateLockList As New List(Of DateLockBE)
    Public flgDateLock As Boolean = False
    Public MyDateLock As DateTime
    Private _TabControl As TabControl
    Public ConfigValuesDataTable As DataTable
    Public objConfigValueList As New List(Of ConfigSystem)
    Public strConfigType As String
    ''Tsk:2359 Declare Variables
    Public DecimalPointInValue As Integer = 0I
    Public DecimalPointInQty As Integer = 0I
    Public DecimalPointInValue1 As Decimal = 0.0
    ''End Tsk:2359
    ''03-Mar-2014  Task:2452    Imran Ali  4-ALPHABETIC order of items in sale and purchase window
    Public ItemSortOrder As Boolean = False
    Public ItemSortOrderByCode As Boolean = False
    Public ItemSortOrderByName As Boolean = False
    Public ItemAscending As Boolean = False
    Public ItemDescending As Boolean = False

    Public AcSortOrder As Boolean = False
    Public AcSortOrderByCode As Boolean = False
    Public AcSortOrderByName As Boolean = False
    Public AcAscending As Boolean = False
    Public AcDescending As Boolean = False
    'End Task:2452
    Public intRegularExpresion As New System.Text.RegularExpressions.Regex("^[0-9].*$")
    Public SMSMask As String = String.Empty 'Task:2577 Added Variable for API Mask
    Public TotalAmountRounding As Integer = 0I
    Public objSMSTemplateList As New List(Of SMSTemplateParameter)
    Public objTemplateKey As String = String.Empty
    Public FindLocationId As Integer = 0I
    Public FindEventKey As String = String.Empty
    Public ReportViewerForContainer As Windows.Forms.Form
    Public Const str_ConfirmSendSMSMessage As String = "Do you want to send sms ?"
    Public Const str_ConfirmPrintVoucher As String = "Do you want to print voucher ?"
    Public Const str_ConfirmPrintVoucher_Slip As String = "Do you want to print voucher or slip ?"
    Public strAdminMobileNo As String = String.Empty
    Public SoftwareVersion As String = String.Empty
    Public GetServerDate As DateTime = Date.MinValue

    'Task#125062015 get date lock
    Public CurrentDate As DateTime = Date.Now
    Public blnDateLockPermission As Boolean = False
    Public DateLockPermissionList As List(Of DateLockPermissionListByUserID)
    Public dayOfWeek As String = String.Empty
    Public BirthdayOfEmployee As String = String.Empty
    Public HasBackup As Boolean = False

    Public BackupStartTime As DateTime
    Public BackupEndTime As DateTime

    Public _dtNotificationActivity As DataTable
    Public blnSystemWiseMDI As Boolean = False
    Public blnUserWiseMDI As Boolean = False
    Public VoucherIDForPost As Integer = 0
    Public PDFPath As String = String.Empty
    'Ali Faisal : Declare and Assign values for Crystal Report Print and Export Rights on 13-April-2017
    Public IsCrystalReportPrint As Boolean = True
    Public IsCrystalReportExport As Boolean = True
    Public HideLogosIcons As Boolean = False
    'Public Clone As Boolean = False
    'TFS1569: Waqar Raza: Added a globel variable to use it in Qoutation Screen
    Public QoutationItemID As Integer = 0

    ' >>>>>>>>>> Added by Syed Irfan Ahmad on 19 Feb 2018 for Task 2411 >>>>>>>>>>
    Public SysMessages As New StringBuilder  'to record system messages
    Public AgriusMessageLogger As New MessageLogger
    '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    Dim dtResult As DataTable
    Dim dtEmail As DataTable
    Dim AllFields As List(Of String)
    Dim EmailTemplate As String = String.Empty
    Dim EmailDAL As New EmailTemplateDAL
    Dim AfterFieldsElement As String = String.Empty
    Dim html As StringBuilder
    Dim UserEmail As String = String.Empty
    Dim EmailBody As String = String.Empty
    Dim CC As String = ""
    Dim BCC As String = ""

    Public Function GetDateLock() As DateTime
        Try
            Dim serverDate As DateTime
            Dim dt As New DataTable
            dt = GetDataTable("Select getDate() as CurrentDate")
            serverDate = dt.Rows(0).Item("CurrentDate")

            Dim dt2 As New DataTable
            ''Commented Against TFS4679 : 03-10-2018
            ' dt2 = GetDataTable("SELECT DateLock,DateLock_Type,NoOfDays FROM tblDateLock WHERE  DateLockId IN (SELECT MAX(DateLockId) FROM tblDateLock WHERE IsNull(Lock,0)=1) And IsNull(Lock,0)=1")
            dt2 = GetDataTable("if Exists (Select DateLockId FROM tblDateLock where DateLock_Type ='Relevant' And IsNull(Lock,0)=1) " _
                    & " SELECT DateLock,DateLock_Type,NoOfDays,EntryDate  FROM tblDateLock WHERE  DateLockId IN ( " _
                    & " SELECT MAX(DateLockId) FROM tblDateLock WHERE IsNull(Lock,0)=1 And DateLock_Type ='Relevant')  " _
                    & " And IsNull(Lock,0)=1 " _
                    & " else " _
                    & " SELECT DateLock,DateLock_Type,NoOfDays,EntryDate  FROM tblDateLock WHERE  DateLockId IN ( " _
                    & " SELECT MAX(DateLockId) FROM tblDateLock WHERE IsNull(Lock,0)=1 And DateLock_Type ='Fixed') " _
                    & " And IsNull(Lock,0)=1 ")
            If dt2.Rows.Count > 0 Then
                If dt2.Rows(0).Item("DateLock_Type").ToString = "Relevant" Then
                    serverDate = serverDate.AddDays(-(Val(dt2.Rows(0).Item("NoOfDays").ToString) - 1))
                Else
                    serverDate = CDate(dt2.Rows(0).Item("DateLock").ToString)
                End If
            Else
                serverDate = Date.Now.AddYears(-20)
            End If

            CurrentDate = serverDate
            Return CurrentDate
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Birthdate() As Boolean
        Try

            Dim dt2 As New DataTable
            Dim Counter As Integer
            dt2 = GetDataTable("Select Employee_Name from tbldefEmployee where ISNULL(Active,0)=1 and  Day(DOB) = Day('" & Date.Now.ToString("yyyy-M-d 00:00:00") & "') AND Month(DOB) = Month('" & Date.Now.ToString("yyyy-M-d 00:00:00") & "')")
            If dt2.Rows.Count > 0 Then
                For Each row As DataRow In dt2.Rows
                    If Counter = 0 Then
                        BirthdayOfEmployee = "" & row.Item("Employee_Name") & ""
                    Else
                        BirthdayOfEmployee += ", " & row.Item("Employee_Name") & ""
                    End If
                    Counter += 1
                Next
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    '' Method Num to only extract numbers from a string
    Public Function Num(ByVal value As String) As Integer
        Dim returnVal As String = String.Empty
        Dim collection As MatchCollection = Regex.Matches(value, "\d+")
        For Each m As Match In collection
            returnVal += m.ToString()
        Next
        Return Convert.ToInt32(returnVal)
    End Function


    '' Task#125062015

    'Get Server DateTime 
    Public Function ServerDate(Optional trans As OleDbTransaction = Nothing) As DateTime
        Try
            Dim dt As New DataTable
            dt = GetDataTable("SELECT DATEADD(Hour, 1, GETDATE())", trans)
            dt.AcceptChanges()
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    If Not IsDBNull(dt.Rows(0).Item(0)) Then
                        GetServerDate = Convert.ToDateTime(dt.Rows(0).Item(0))
                    Else
                        GetServerDate = Date.Now
                    End If
                Else
                    GetServerDate = Date.Now
                End If
            Else
                GetServerDate = Date.Now
            End If
            Return GetServerDate
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'End Server Date Code

    Public Function GetExpenseByLC(ByVal CostCenterID As Integer) As Dictionary(Of String, Integer)
        Try

            Dim ExpenseIDList As New Dictionary(Of String, Integer)
            Dim dt As New DataTable

            dt = GetDataTable("Select tblVoucherDetail.CostCenterID, SUM(IsNull(debit_amount,0)) as ExpAmount, tblVoucherDetail.coa_detail_id From tblVoucherDetail INNER JOIN tblVoucher On tblVoucher.Voucher_Id =  tblVoucherDetail.Voucher_Id INNER JOIN vwCOADetail  on vwCOADetail.coa_detail_id  = tblVoucherDetail.coa_detail_id WHERE tblVoucherDetail.CostCenterID=" & CostCenterID & "  AND tblVoucher.Source <> 'frmImport' and debit_amount>0 Group By tblVoucherDetail.CostCenterID, tblVoucherDetail.coa_detail_id")
            dt.AcceptChanges()
            If dt.HasErrors = False Then
                If dt.Rows.Count > 0 Then
                    For Each dst As DataRow In dt.Rows

                        ExpenseIDList.Add(Val(dst.Item(2)), Val(dst.Item(1)))

                    Next
                    Return ExpenseIDList
                Else
                    Return ExpenseIDList
                End If
            Else
                Return ExpenseIDList
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function CheckCurrentStockByItem(ByVal ItemId As Integer, ByVal Qty As Double, Optional ByVal GridEx As Janus.Windows.GridEX.GridEX = Nothing, Optional ByVal DocNo As String = "", Optional ByVal trans As OleDbTransaction = Nothing) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Select ArticleDefId, IsNull(SUM(Isnull(InQty,0)-Isnull(OutQty,0)),0) as CurrentStock From StockDetailTable WHERE ArticleDefId=" & ItemId & " " & IIf(DocNo.Length > 0, " AND StockTransId Not In(Select StockTransId From StockMasterTable WHERE DocNo='" & DocNo.Replace("'", "''") & "')", "") & "  Group By ArticleDefId"
            Dim dt As New DataTable
            dt = GetDataTable(strSQL, trans)
            Dim dblCurrentStock As Double = 0D
            Dim RowFormat As New Janus.Windows.GridEX.GridEXFormatStyle
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    dblCurrentStock = Val(dt.Rows(0).Item(1).ToString)
                    If Qty > dblCurrentStock Then
                        If GridEx IsNot Nothing Then
                            RowFormat.BackColor = Color.LightPink
                            GridEx.CurrentRow.RowStyle = RowFormat
                        End If
                        Throw New Exception(Chr(10) & "Stock is not enough.")
                        Return True
                    Else
                        Return False
                    End If
                Else
                    If GridEx IsNot Nothing Then
                        RowFormat.BackColor = Color.LightPink
                        GridEx.CurrentRow.RowStyle = RowFormat
                    End If
                    Throw New Exception(Chr(10) & "Stock is not enough.")
                    Return True
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetMultiLanguageCodes() As DataTable
        Try
            Dim dt As New DataTable
            dt.Columns.Add("Culture", GetType(System.String))
            dt.Columns.Add("Identifier", GetType(System.String))
            dt.Columns.Add("Country", GetType(System.String))
            dt.TableName = "record"
            dt.AcceptChanges()
            If IO.File.Exists(Application.StartupPath & "\LanguageSchema.xml") Then
                dt.ReadXml(Application.StartupPath & "\LanguageSchema.xml")
                dt.AcceptChanges()
            Else
                Dim dr As DataRow
                dr = dt.NewRow
                dr(0) = "en-US"
                dr(1) = "0x0409"
                dr(2) = "English-United State"
                dt.Rows.InsertAt(dr, 0)
                dt.AcceptChanges()
                dr = dt.NewRow
                dr(0) = "ur"
                dr(1) = "0x0020"
                dr(2) = "Urdu"
                dt.Rows.InsertAt(dr, 0)
                dt.AcceptChanges()
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ChangeInputLanguage(ByVal objCultureInfo As System.Globalization.CultureInfo)
        Try
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(objCultureInfo)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function SaveSMSLog(ByVal Message As String, ByVal CellNo As String, ByVal SMSType As String) As Boolean
        If Not CellNo.Length > 9 Then Return False
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As OleDbTransaction = Con.BeginTransaction
        Dim objCMD As New OleDbCommand
        Try
            If CellNo.Length <= 11 Then
                CellNo = "92" & Microsoft.VisualBasic.Right(CellNo, 10)
            Else
                CellNo = CellNo
            End If
            objCMD.Connection = Con
            objCMD.Transaction = trans
            objCMD.CommandText = "INSERT INTO tblSMSLog(SMSLogDate, SMSBody,SMSType,PhoneNo,CreatedByUserID) VALUES(Convert(DateTime,'" & Date.Now.ToString("yyyy-M-d hh:mm:ss tt") & "',102),N'" & Message.Replace("'", "''") & "',N'" & SMSType.Replace("'", "''") & "', N'" & CellNo.Replace("'", "''") & "'," & LoginUserId & " )Select @@Identity"
            objCMD.CommandType = CommandType.Text
            objCMD.ExecuteNonQuery()

            trans.Commit()
            Return True


        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function SaveSMSLog(trans As OleDbTransaction, ByVal Message As String, ByVal CellNo As String, ByVal SMSType As String) As Boolean
        If Not CellNo.Length > 9 Then Return False
        Dim objCMD As New OleDbCommand
        Try
            If CellNo.Length <= 11 Then
                CellNo = "92" & Microsoft.VisualBasic.Right(CellNo, 10)
            Else
                CellNo = CellNo
            End If
            objCMD.Connection = Con
            objCMD.Transaction = trans
            objCMD.CommandText = "INSERT INTO tblSMSLog(SMSLogDate, SMSBody,SMSType,PhoneNo,CreatedByUserID) VALUES(Convert(DateTime,'" & Date.Now.ToString("yyyy-M-d hh:mm:ss tt") & "',102),N'" & Message.Replace("'", "''") & "',N'" & SMSType.Replace("'", "''") & "', N'" & CellNo.Replace("'", "''") & "'," & LoginUserId & " )Select @@Identity"
            objCMD.CommandType = CommandType.Text
            objCMD.ExecuteNonQuery()
            'trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            'Con.Close()
        End Try
    End Function

    ''TASK TFS4513
    Public Function SaveEmailLog(ByVal DocumentNo As String, ByVal Email As String, ByVal Source As String, ByVal Activity As String) As Boolean
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As OleDbTransaction = Con.BeginTransaction
        Dim objCMD As New OleDbCommand
        Try
            objCMD.Connection = Con
            objCMD.Transaction = trans
            objCMD.CommandText = "INSERT INTO tblEmailLog(LogDate, DocumentNo, Source, Email, Activity, UserName) VALUES(Convert(DateTime,'" & Date.Now.ToString("yyyy-M-d hh:mm:ss tt") & "',102), N'" & DocumentNo.Replace("'", "''") & "',N'" & Source.Replace("'", "''") & "', N'" & Email.Replace("'", "''") & "', N'" & Activity.Replace("'", "''") & "', N'" & LoginUserName & "' )Select @@Identity"
            objCMD.CommandType = CommandType.Text
            objCMD.ExecuteNonQuery()
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    '   [ID] [int] IDENTITY(1,1) NOT NULL,
    '[LogDate] [datetime] NULL,
    '[Body] [nvarchar](1000) NULL,
    '[Source] [nvarchar](100) NULL,
    '[Email] [nvarchar](25) NULL,
    '[SentStatus] [nvarchar](15) NULL,
    '[SentDate] [datetime] NULL,
    '[DeliveryStatus] [nvarchar](15) NULL,
    '[DeliveryDate] [datetime] NULL,
    '[TransactionID] [numeric](18, 0) NULL,
    '[ProcessLogID] [int] NULL,
    '[CreatedByUserID] [int] NULL,
    '[SentByUserID] [int] NULL
    Public Function SaveEmailLog(trans As OleDbTransaction, ByVal Body As String, ByVal Email As String, ByVal Source As String) As Boolean
        Dim objCMD As New OleDbCommand
        Try
            objCMD.Connection = Con
            objCMD.Transaction = trans
            objCMD.CommandText = "INSERT INTO tblEmailLog(LogDate, Body, Source,Email, CreatedByUserID) VALUES(Convert(DateTime,'" & Date.Now.ToString("yyyy-M-d hh:mm:ss tt") & "',102),N'" & Body.Replace("'", "''") & "',N'" & Source.Replace("'", "''") & "', N'" & Email.Replace("'", "''") & "'," & LoginUserId & " )Select @@Identity"
            objCMD.CommandType = CommandType.Text
            objCMD.ExecuteNonQuery()
            'trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            'Con.Close()
        End Try
    End Function
    Public Function SaveEmailLog(trans As SqlTransaction, ByVal Body As String, ByVal Email As String, ByVal Source As String) As Boolean
        Dim objCMD As New SqlCommand
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Try
            'If CellNo.Length <= 11 Then
            '    CellNo = "92" & Microsoft.VisualBasic.Right(CellNo, 10)
            'Else
            '    CellNo = CellNo
            'End If
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            objCMD.Connection = conn
            objCMD.Transaction = trans
            objCMD.CommandText = "INSERT INTO tblEmailLog(LogDate, Body, Source,Email, CreatedByUserID) VALUES(Convert(DateTime,'" & Date.Now.ToString("yyyy-M-d hh:mm:ss tt") & "',102),N'" & Body.Replace("'", "''") & "',N'" & Source.Replace("'", "''") & "', N'" & Email.Replace("'", "''") & "'," & LoginUserId & " )Select @@Identity"
            objCMD.CommandType = CommandType.Text
            objCMD.ExecuteNonQuery()
            'trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            'Con.Close()
        End Try
    End Function





    ''END TASK TFS4513
    Public Function GetEnventKeyList() As List(Of SMSConfigBE)
        Try

            Dim EventKeyList As New List(Of SMSConfigBE)
            Dim EventKey As SMSConfigBE = Nothing

            Dim strSQL As String = "Select ID, Config_Key, IsNull(Enabled,0) as Enabled, IsNull(EnabledAdmin,0) as EnabledAdmin From SMSConfigurationTable"
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For Each r As DataRow In dt.Rows
                        EventKey = New SMSConfigBE
                        EventKey.ID = Val(r.Item("ID").ToString)
                        EventKey.Event_Key = r.Item("Config_Key").ToString
                        If IsDBNull(r.Item("Enabled")) Then
                            EventKey.Enable = False
                        Else
                            EventKey.Enable = Convert.ToBoolean(r.Item("Enabled"))
                        End If
                        If IsDBNull(r.Item("EnabledAdmin")) Then
                            EventKey.EnabledAdmin = False
                        Else
                            EventKey.EnabledAdmin = Convert.ToBoolean(r.Item("EnabledAdmin"))
                        End If
                        EventKeyList.Add(EventKey)
                    Next
                End If
            End If
            SMSConfigBE.EventKeyList = EventKeyList
            Return EventKeyList
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetSMSConfig(ByVal Key As String) As SMSConfigBE
        Try
            FindEventKey = Key
            Dim EventKey As New SMSConfigBE
            If Not SMSConfigBE.EventKeyList Is Nothing Then
                EventKey = SMSConfigBE.EventKeyList.Find(AddressOf FilterSMSConfig)
            End If

            If EventKey Is Nothing Then
                EventKey = New SMSConfigBE
                EventKey.Enable = False
            End If
            FindEventKey = String.Empty
            Return EventKey
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function FilterSMSConfig(ByVal Config As SMSConfigBE) As Boolean
        Try
            If Config.Event_Key = FindEventKey Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetPendingSMS() As List(Of SMSLogBE)
        Try
            Return SBDal.SMSTemplateDAL.GetAllRecordsByPendingSMS
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub GetAllSMSTemplate()
        Try
            objSMSTemplateList = SMSTemplatesDAL.SMSTemplateList
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function FindSMSTemplate(ByVal objTemplate As SMSTemplateParameter) As Boolean
        Try
            If objTemplate.Key = objTemplateKey Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetSMSTemplate(ByVal Key As String) As SMSTemplateParameter
        Try
            objTemplateKey = Key
            Dim objSMSTemplate As New SMSTemplateParameter
            objSMSTemplate = objSMSTemplateList.Find(AddressOf FindSMSTemplate)
            objTemplateKey = String.Empty
            Return objSMSTemplate
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetLocationList() As List(Of LocationBE)
        Try

            Dim strSQL As String = String.Empty
            Dim LocationList As New List(Of LocationBE)
            strSQL = "Select location_id,location_code,location_name,comments,IsNull(sort_order,0) as sort_order,location_address,location_phone,location_fax,location_url,location_type,IsNull(RestrictedItems,0) as RestrictedItems,Mobile_No From tblDefLocation"
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            Dim Location As LocationBE
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For Each r As DataRow In dt.Rows
                        Location = New LocationBE
                        Location.Comments = r.Item("Comments").ToString
                        Location.Location_Address = r.Item("Location_Address").ToString
                        Location.Location_Code = r.Item("Location_Code").ToString
                        Location.Location_Fax = r.Item("Location_Fax").ToString
                        Location.Location_Id = Val(Val(r.Item("Location_Id").ToString))
                        Location.Location_Name = r.Item("Location_Name").ToString
                        Location.Location_Phone = r.Item("Location_Phone").ToString
                        Location.Location_Type = r.Item("Location_Type").ToString
                        Location.Location_URL = r.Item("Location_URL").ToString
                        Location.Mobile_No = r.Item("Mobile_No").ToString
                        Location.RestrictedItems = Convert.ToBoolean(r.Item("RestrictedItems").ToString)
                        Location.Sort_Order = Val(r.Item("Sort_Order").ToString)
                        LocationList.Add(Location)
                    Next
                End If
            End If
            LocationBE.LocationList = LocationList

            Return LocationList
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function FindLocation(ByVal Location As LocationBE) As Boolean
        Try
            If Location.Location_Id = FindLocationId Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetLocation(ByVal LocationId As Integer) As LocationBE
        Try
            FindLocationId = LocationId
            Dim Location As LocationBE = LocationBE.LocationList.Find(AddressOf FindLocation)
            FindLocationId = 0I
            Return Location
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetEmployeeBarcodeStickerData() As DataTable
        Try
            Dim strSQL As String = "Select Employee_Id, Employee_Code, Employee_Name as Employee_Name From tblDefEmployee"
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            Dim objCRFU As New CRUFLIDAutomation.FontEncoder 'Create Object 
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For Each r As DataRow In dt.Rows
                        r("Employee_Code") = objCRFU.Code128("?" & r.Item("Employee_Code").ToString, 0)
                    Next
                End If
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function StringFixedLength(ByVal Text As String, ByVal Length As Integer) As String
        Try
            Dim ReturnString As String = String.Empty
            Dim str As String = String.Empty
            If Text.Trim.Length > Length Then
                str = Text.Substring(0, Length)
            Else
                str = String.Concat(Text, Microsoft.VisualBasic.Space(Length - Text.Trim.Length))
            End If
            ReturnString = str
            Return ReturnString
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function getChequeSerialNo(ByVal BankAcId As Integer) As String ''TASK271115 Ameen
        Try
            Dim chequeNo As String = String.Empty
            Dim dt As DataTable = GetDataTable("Select Min(IsNull(ChequeNo,0)) From ChequeDetailTable  WHERE Cheque_Issued <> 1 And ChequeSerialId in (Select Max(ChequeSerialId) From ChequeMasterTable WHERE BankAcId=" & BankAcId & ")")
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    chequeNo = dt.Rows(0).Item(0).ToString
                    If chequeNo <> "" Then
                        'chequeNo += 1
                        Return chequeNo
                    Else
                        Return ""
                    End If

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
    'Public Function getChequeSerialNo(ByVal BankAcId As Integer) As String
    '    Try
    '        Dim chequeNo As String = String.Empty
    '        Dim dt As DataTable = GetDataTable("Select Max(ChequeNo) From ChequeDetailTable WHERE ChequeSerialDtId In(Select Min(ChequeSerialDtId) From ChequeDetailTable WHERE Isnull(VoucherDetailId,0) = 0 And ChequeSerialId in (Select ChequeSerialId From ChequeMasterTable WHERE BankAcId=" & BankAcId & ")")
    '        If dt IsNot Nothing Then
    '            If dt.Rows.Count > 0 Then
    '                chequeNo = dt.Rows(0).Item(0).ToString
    '                Return chequeNo
    '            Else
    '                Return ""
    '            End If
    '        Else
    '            Return ""
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    Public Function IsValidateChequeSerialNo(ByVal BankAcId As Integer, ByVal ChequeNo As String) As Boolean
        Try
            If ChequeNo.Trim.Length = 0 Then Return True
            Dim dt As DataTable = GetDataTable("Select ChequeNo, VoucherDetailId From ChequeDetailTable WHERE  ChequeSerialId in (Select ChequeSerialId From ChequeMasterTable WHERE BankAcId=" & BankAcId & ") AND ChequeNo=N'" & ChequeNo & "'")
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    If (dt.Rows(0).Item(0).ToString <> "" AndAlso Val(dt.Rows(0).Item(1).ToString) <> 0) Then
                        If msg_Confirm("Cheque No: " & ChequeNo.Replace("'", "''") & " is already issued. Do you want to proceed. ?") = False Then
                            Return False
                        Else
                            Return True
                        End If
                    Else
                        Return True
                    End If
                Else
                    Throw New Exception("Cheque no: [" & ChequeNo & "] is not valid")
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function IsValidateChequeReceipt(ByVal ChequeNo As String, ByRef customerID As Int32, voucher_id As Integer) As Boolean
        Try
            Dim dt As DataTable = GetDataTable("Select tblVoucherDetail.Cheque_No, tblVoucherDetail.Cheque_Date From tblVoucherDetail left join tblVoucher on tblVoucherDetail.voucher_id = tblVoucher.voucher_id WHERE tblVoucherDetail.Cheque_No = '" & ChequeNo & "' And tblVoucher.voucher_type_id=5 And  tblVoucherDetail.coa_detail_id='" & customerID & "' and tblVoucher.Voucher_id <> " & voucher_id & "") ' And Convert(Varchar, Cheque_Date, 102) = '" & ChequeDate & "'")

            If dt.Rows.Count > 0 Then
                If (dt.Rows(0).Item(0).ToString <> "") Then
                    If msg_Confirm("Cheque no: [" & ChequeNo & "] is already received. Do you want to proceed. ?") = False Then
                        Return False
                    Else
                        Return True
                    End If
                Else
                    Return True
                End If
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function IsValidateChequePayment(ByVal ChequeNo As String, Voucher_Id As Integer) As Boolean
        Try
            Dim dt As DataTable = GetDataTable("Select tblVoucherDetail.Cheque_No, tblVoucherDetail.Cheque_Date From tblVoucherDetail left join tblVoucher on tblVoucherDetail.voucher_id = tblVoucher.voucher_id WHERE tblVoucherDetail.Cheque_No = '" & ChequeNo & "' And tblVoucher.voucher_type_id = 4 AND tblVoucher.Voucher_id <> " & Voucher_Id & "") ' And Convert(Varchar, Cheque_Date, 102) = '" & ChequeDate & "'")

            If dt.Rows.Count > 0 Then
                If (dt.Rows(0).Item(0).ToString <> "") Then
                    If msg_Confirm("Cheque no: [" & ChequeNo & "] is already issued") = False Then
                        Return False
                    Else
                        Return True
                    End If
                Else
                    Return True
                End If
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function IsValidateInvoiceNo(ByVal InvoiceNo As String, ByRef vendorID As Int32, ByRef receivingNo As String) As Boolean
        Try
            Dim dt As DataTable = GetDataTable("Select ReceivingMasterTable.VendorId, ReceivingMasterTable.vendor_invoice_no From ReceivingMasterTable WHERE ReceivingMasterTable.VendorId = '" & vendorID & "' And ReceivingMasterTable.vendor_invoice_no ='" & InvoiceNo & "' And ReceivingNo <> '" & receivingNo & "'") ' And Convert(Varchar, Cheque_Date, 102) = '" & ChequeDate & "'")

            If dt.Rows.Count > 0 Then
                If (dt.Rows(0).Item(0).ToString <> "") Then
                    If msg_Confirm("Invoice no: [" & InvoiceNo & "] is already entered. Do you want to proceed. ?") = False Then
                        Return False
                    Else
                        Return True
                    End If
                Else
                    Return True
                End If
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' This Function is made to check if the login user has any Accounts Mapped with it or not
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Ayesha Rehman :TFS3322 :User Wise Chart of Account Groups</remarks>
    Function GetMappedUserId() As Integer
        Dim Count As Integer = 0
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction

            str = "Select COUNT(*) from COAUserMapping where [User_Id] = " & LoginGroupId
            Count = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            trans.Commit()
            Return Count
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
    ''' <summary>
    ''' This function is made to get config value for Different documents of Account Module to show COA which is mapped with user group 
    ''' </summary>
    ''' <param name="FormName"></param>
    ''' <returns></returns>
    ''' <remarks>TFS3322 : Ayesha Rehman : 15-05-2018</remarks>
    Public Function getGroupAccountsConfigforAccounts(ByVal FormName As String) As Boolean
        Try
            Dim cbVocher As Boolean = False
            Dim cbPayment As Boolean = False
            Dim cbReceipt As Boolean = False
            Dim cbExpense As Boolean = False
            Dim cbValues As String = String.Empty
            cbValues = getConfigValueByType("COAGroupWithAccountsModule").ToString()
            If cbValues.Length > 0 Then
                Dim arday() As String = cbValues.Split("|")
                If arday.Length > 3 Then
                    'Vocher&False|Payment&True|Receipt&True|Expense&True
                    cbVocher = Convert.ToBoolean(arday(0).Trim.Substring(8))
                    cbPayment = Convert.ToBoolean(arday(1).Trim.Substring(8))
                    cbReceipt = Convert.ToBoolean(arday(2).Trim.Substring(8))
                    cbExpense = Convert.ToBoolean(arday(3).Trim.Substring(8))
                End If
            End If

            Select Case FormName
                Case "frmVoucherNew"
                    Return cbVocher
                Case "frmVendorPayment"
                    Return cbPayment
                Case "frmPaymentNew"
                    Return cbPayment
                Case "frmCustomerCollection"
                    Return cbReceipt
                Case "frmOldCustomerCollection"
                    Return cbReceipt
                Case "frmExpense"
                    Return cbExpense
            End Select

            Return False

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' This function is made to get config value for Different documents of Inventory Module to show COA which is mapped with user group 
    ''' </summary>
    ''' <param name="FormName"></param>
    ''' <returns></returns>
    ''' <remarks>TFS3322 : Ayesha Rehman : 15-05-2018</remarks>
    Public Function getGroupAccountsConfigforInventory(ByVal FormName As String) As Boolean
        Try
            Dim cbStoreIssuance As Boolean = False
            Dim cbReturnStoreIssuance As Boolean = False
            Dim cbValues As String = String.Empty
            cbValues = getConfigValueByType("COAGroupWithInventoryModule").ToString()
            If cbValues.Length > 0 Then
                Dim arday() As String = cbValues.Split("|")
                If arday.Length > 1 Then
                    'SI&False|RSI&True|SD&True|SR&True
                    cbStoreIssuance = Convert.ToBoolean(arday(0).Trim.Substring(3))
                    cbReturnStoreIssuance = Convert.ToBoolean(arday(1).Trim.Substring(4))

                End If
            End If

            Select Case FormName
                Case "frmStoreIssuence"
                    Return cbStoreIssuance
                Case "frmStoreIssuenceNew"
                    Return cbStoreIssuance
                Case "frmReturnStoreIssuence"
                    Return cbReturnStoreIssuance
            End Select

            Return False

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' This function is made to get config value for Different documents of Purchase Module to show COA which is mapped with user group 
    ''' </summary>
    ''' <param name="FormName"></param>
    ''' <returns></returns>
    ''' <remarks>TFS3322 : Ayesha Rehman : 15-05-2018</remarks>
    Public Function getGroupAccountsConfigforPurchase(ByVal FormName As String) As Boolean
        Try
            Dim cbPurchaseDemand As Boolean = False
            Dim cbPurchaseOrder As Boolean = False
            Dim cbGRN As Boolean = False
            Dim cbPurchase As Boolean = False
            Dim cbPurchaseReturn As Boolean = False
            Dim cbValues As String = String.Empty
            cbValues = getConfigValueByType("COAGroupWithPurchaseModule").ToString()
            If cbValues.Length > 0 Then
                Dim arday() As String = cbValues.Split("|")
                If arday.Length > 4 Then
                    'PD&False|PO&True|GRN&True|Purchase&True|PR&True
                    cbPurchaseDemand = Convert.ToBoolean(arday(0).Trim.Substring(3))
                    cbPurchaseOrder = Convert.ToBoolean(arday(1).Trim.Substring(3))
                    cbGRN = Convert.ToBoolean(arday(2).Trim.Substring(4))
                    cbPurchase = Convert.ToBoolean(arday(3).Trim.Substring(9))
                    cbPurchaseReturn = Convert.ToBoolean(arday(4).Trim.Substring(3))
                End If
            End If

            Select Case FormName
                Case "frmPurchaseDemand"
                    Return cbPurchaseDemand
                Case "frmPurchaseOrderNew"
                    Return cbPurchaseOrder
                Case "frmReceivingNote"
                    Return cbGRN
                Case "frmPurchaseNew"
                    Return cbPurchase
                Case "frmPurchaseReturn"
                    Return cbPurchaseReturn
            End Select

            Return False

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' This function is made to get config value for Different documents to show COA which is mapped with user group 
    ''' </summary>
    ''' <param name="FormName"></param>
    ''' <returns></returns>
    ''' <remarks>TFS3322 : Ayesha Rehman : 15-05-2018</remarks>
    Public Function getGroupAccountsConfigforSales(ByVal FormName As String) As Boolean
        Try
            Dim cbQuotation As Boolean = False
            Dim cbSalesOrder As Boolean = False
            Dim cbDeliveryChalan As Boolean = False
            Dim cbSales As Boolean = False
            Dim cbSalesReturn As Boolean = False
            Dim cbValues As String = String.Empty
            cbValues = getConfigValueByType("COAGroupWithSalesModule").ToString()
            If cbValues.Length > 0 Then
                Dim arday() As String = cbValues.Split("|")
                If arday.Length > 4 Then
                    'Quotation&False|SalesOrder&True|DeliveryChalan&True|Sales&True|SalesReturn&True
                    cbQuotation = Convert.ToBoolean(arday(0).Trim.Substring(10))
                    cbSalesOrder = Convert.ToBoolean(arday(1).Trim.Substring(11))
                    cbDeliveryChalan = Convert.ToBoolean(arday(2).Trim.Substring(15))
                    cbSales = Convert.ToBoolean(arday(3).Trim.Substring(6))
                    cbSalesReturn = Convert.ToBoolean(arday(4).Trim.Substring(12))
                End If
            End If

            Select Case FormName
                Case "frmQoutationNew"
                    Return cbQuotation
                Case "frmSalesOrderNew"
                    Return cbSalesOrder
                Case "frmDeliveryChalan"
                    Return cbDeliveryChalan
                Case "frmSales"
                    Return cbSales
                Case "frmSalesReturn"
                    Return cbSalesReturn
            End Select

            Return False

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getConfigValueList() As List(Of ConfigSystem)
        Try
            objConfigValueList = GetConfigValueByAll() 'New ConfigSystemDAL().GetConfigValueByAll
            Return objConfigValueList
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetConfigValueByAll() As List(Of ConfigSystem)
        Dim objCon As New OleDbConnection(Con.ConnectionString)
        Dim ConfigList As New List(Of ConfigSystem)
        Dim Config As ConfigSystem
        Try
            Dim strSQL As String = "Select Config_Id, Config_Value, Config_Type,IsActive From ConfigValuesTable"
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Open()
            Dim cmd As New OleDbCommand(strSQL, objCon)
            Dim dr As OleDb.OleDbDataReader = cmd.ExecuteReader 'SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)
            If dr.HasRows Then
                Do While dr.Read
                    Try
                        Config = New ConfigSystem
                        Config.Config_Id = Val(dr.GetValue(0).ToString)
                        Config.Config_Value = dr.GetValue(1).ToString
                        Config.Config_Type = dr.GetValue(2).ToString
                        If IsDBNull(dr.GetValue(3)) Then
                            Config.IsActive = False
                        Else
                            Config.IsActive = Convert.ToBoolean(dr.GetValue(3).ToString)
                        End If
                        ConfigList.Add(Config)
                    Catch ex As Exception
                    End Try
                Loop
            End If
            Return ConfigList
        Catch ex As Exception
            Throw ex
        Finally

            objCon.Close()
        End Try
    End Function
    Public Function getConfigValueByType(ByVal Key As String) As String
        Try
            strConfigType = Key
            Dim config As ConfigSystem = objConfigValueList.Find(AddressOf FilterConfigValue)
            strConfigType = String.Empty
            If config IsNot Nothing Then
                If config.Config_Value Is Nothing Then
                    config.Config_Value = DBNull.Value.ToString
                End If
                Return config.Config_Value
            Else
                Return "Error"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function FilterConfigValue(ByVal objConfig As ConfigSystem) As Boolean
        Try
            If objConfig.Config_Type = strConfigType Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetConfigValuesdt() As DataTable
        Try
            Return GetDataTable("Select Config_Id, Config_Value, Config_Type From ConfigValuesTable")
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetItemList() As List(Of SBModel.ArticleList)
        Try
            Dim strQuery As String = String.Empty
            strQuery = "Select ArticleId, ArticleCode, ArticleDescription, ArticleSizeName, ArticleColorName, PurchasePrice, SalePrice, MasterID From ArticleDefView WHERE Active=1 ORDER BY SortOrder Asc "
            Dim dr As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strQuery, Nothing)
            Dim ArticleList As New List(Of ArticleList)
            Dim Article As ArticleList
            If dr.HasRows Then
                While dr.Read
                    Article = New ArticleList
                    Article.ArticleId = dr.GetValue(0) 'ArticleId
                    Article.ArticleCode = dr.GetValue(1) 'Article Code
                    Article.ArticleDescription = dr.GetValue(2) 'ArticleDescription
                    Article.ArticleSizeName = dr.GetValue(3) ' Size
                    Article.ArticleColorName = dr.GetValue(4) 'Color
                    Article.PurchasePrice = dr.GetValue(5) 'Purchase Price
                    Article.SellingPrice = dr.GetValue(6) 'Sale Price
                    Article.MasterId = dr.GetValue(7) ' MasterID
                    ArticleList.Add(Article)
                End While
            End If
            Return ArticleList
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function CompanyList() As List(Of CompanyInfo)
        Try
            Return MyCompanies() 'CompanyInfoDAL.MyCompany
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function MyCompanies() As List(Of CompanyInfo)
        Dim objCon As New OleDbConnection(Con.ConnectionString)
        Try

            Dim strQuery As String = "Select * From CompanyDefTable"
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Open()
            Dim cmd As New OleDbCommand(strQuery, objCon)
            Dim dr As OleDbDataReader = cmd.ExecuteReader 'SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strQuery, Nothing)
            Dim CompanyList As New List(Of CompanyInfo)
            Dim Company As CompanyInfo
            If dr IsNot Nothing Then
                If dr.HasRows Then
                    Do While dr.Read
                        Try
                            Company = New CompanyInfo
                            Company.CompanyID = Val(dr.GetValue(0).ToString) ' CompanyId
                            Company.CompanyName = dr.GetValue(1).ToString
                            Company.LegalName = dr.GetValue(2).ToString
                            Company.Phone = dr.GetValue(3).ToString
                            Company.Fax = dr.GetValue(4).ToString
                            Company.Email = dr.GetValue(5).ToString
                            Company.WebPage = dr.GetValue(6).ToString
                            Company.Address = dr.GetValue(7).ToString
                            Company.CostCenterId = Val(dr.GetValue(8).ToString)
                            Company.Prefix = dr.GetValue(9).ToString
                            CompanyList.Add(Company)
                        Catch ex As Exception
                        End Try
                    Loop
                End If
            End If
            Return CompanyList
        Catch ex As Exception
            Throw ex
        Finally


            objCon.Close()
        End Try
    End Function
    Public Function CompanyRightsList() As List(Of UserCompanyRightsBE)
        Try
            MyCompanyRightsList = UserCompanyRightsList(LoginUserId)
            Return MyCompanyRightsList 'UserCompanyRightsDAL.UserCompanyRightsList
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function MainMenuViewRights() As List(Of Users)
        Try
            MyMainMenuViwRights = UserMainMenuViewRight(LoginUserId)
            Return MyMainMenuViwRights
        Catch ex As Exception

        End Try

    End Function

    Public Function LocationRightsList() As List(Of UserLocationRightsBE)
        Try
            MyLocationRightsList = UserLocationRightsList(LoginUserId)
            Return MyLocationRightsList 'UserCompanyRightsDAL.UserCompanyRightsList
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UserCompanyRightsList(ByVal UserId As Integer) As List(Of UserCompanyRightsBE)
        Dim objCon As New OleDbConnection(Con.ConnectionString)
        Try


            'Dim strQuery As String = "Select * from tblUserCompanyRights "
            '' R912 Filter Company Rights By User

            Dim strQuery As String = "Select * from tblUserCompanyRights WHERE User_Id=" & UserId & ""
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Open()
            Dim cmd As New OleDbCommand(strQuery, objCon)
            Dim dr As OleDbDataReader = cmd.ExecuteReader  'SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strQuery, Nothing)
            Dim RightsList As New List(Of UserCompanyRightsBE)
            Dim Rights As UserCompanyRightsBE
            If dr.HasRows Then
                Do While dr.Read
                    Rights = New UserCompanyRightsBE
                    Rights.CompanyRightsId = Val(dr.GetValue(0).ToString)
                    Rights.User_Id = Val(dr.GetValue(1).ToString)
                    Rights.CompanyId = Val(dr.GetValue(2).ToString)
                    If Not IsDBNull(dr.GetValue(3)) Then
                        Rights.Rights = dr.GetValue(3)
                    Else
                        Rights.Rights = False
                    End If
                    RightsList.Add(Rights)
                Loop
            End If
            Return RightsList
        Catch ex As Exception
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Function

    ''TFS2543 hide Right base menu
    ''SAbaShabbir
    Public Function UserMainMenuViewRight(ByVal UserId As Integer) As List(Of Users)
        Dim objCon As New OleDbConnection(Con.ConnectionString)
        Try

            'Dim strQuery As String = "SELECT tblForms.FormName, tblUserRights.User_ID, tblUserRights.View_Rights, tblUserRights.Form_ID, tblForms.FormId FROM tblForms INNER JOIN tblUserRights ON tblForms.FormId = tblUserRights.Form_ID WHERE (tblUserRights.User_ID =" & UserId & ")"
            Dim strQuery As String = "SELECT tblForms.FormName, tblUser.User_Id, tblRights.Rights, tblFormsControls.FormId, tblFormsControls.SortOrder, tblFormsControls.Active, tblRights.RightsId, tblRights.GroupId, tblRights.FormId AS Expr1, tblRights.FormControlId AS Expr2, tblForms.FormName FROM tblFormsControls INNER JOIN tblRights ON tblFormsControls.FormControlId = tblRights.FormControlId INNER JOIN tblUser ON tblRights.GroupId = tblUser.GroupId LEFT OUTER JOIN tblForms ON tblFormsControls.FormId = tblForms.FormId WHERE (tblFormsControls.FormControlName = 'View') AND Rights = 1 And (tblRights.GroupId = " & LoginGroupId & ")"
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Open()
            Dim cmd As New OleDbCommand(strQuery, objCon)
            Dim dr As OleDbDataReader = cmd.ExecuteReader  'SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strQuery, Nothing)
            Dim RightsList As New List(Of Users)
            Dim Rights As Users
            'Dim strArray() As String
            'Dim ArrayList As New List(Of String)
            If dr.HasRows Then
                Do While dr.Read
                    Rights = New Users
                    Rights.FormName = dr.GetValue(0).ToString
                    Rights.UserId = Val(dr.GetValue(1).ToString)
                    Rights.ShowMainMenuRights = dr.GetValue(2).ToString
                    Rights.FormId = Val(dr.GetValue(3).ToString)
                    ''store values in model
                    'strArray = dr.GetValue(1).ToString
                    RightsList.Add(Rights)
                Loop
            End If
            'Dim ArrayList As New List(Of String)
            'If dr.HasRows Then
            '    Do While dr.Read
            '        Dim strArray() As String
            '    Loop
            'End If
            Return RightsList
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UserLocationRightsList(ByVal UserId As Integer) As List(Of UserLocationRightsBE)
        Dim objCon As New OleDbConnection(Con.ConnectionString)
        Try


            'Dim strQuery As String = "Select * from tblUserCompanyRights "
            '' R912 Filter Company Rights By User

            Dim strQuery As String = "Select * from tblUserLocationRights WHERE UserId=" & UserId & ""
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Open()
            Dim cmd As New OleDbCommand(strQuery, objCon)
            Dim dr As OleDbDataReader = cmd.ExecuteReader  'SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strQuery, Nothing)
            Dim RightsList As New List(Of UserLocationRightsBE)
            Dim Rights As UserLocationRightsBE
            If dr.HasRows Then
                Do While dr.Read
                    Rights = New UserLocationRightsBE
                    Rights.ID = Val(dr.GetValue(0).ToString)
                    Rights.UserID = Val(dr.GetValue(1).ToString)
                    Rights.Location_ID = Val(dr.GetValue(2).ToString)
                    If Not IsDBNull(dr.GetValue(3)) Then
                        Rights.Rights = dr.GetValue(3)
                    Else
                        Rights.Rights = False
                    End If
                    RightsList.Add(Rights)
                Loop
            End If
            Return RightsList
        Catch ex As Exception
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Function
    Public Function getReminders(ByVal ReminderFromDate As DateTime) As DataTable
        Try
            getReminders = Nothing
            Dim strSQL As String = "SELECT dbo.tblReminder.ReminderId, dbo.tblReminderDetail.ReminderDetailId, dbo.tblReminderDetail.User_Id, dbo.tblReminderDetail.User_Reminder_Date, " _
                  & "    dbo.tblReminderDetail.User_Reminder_Time, dbo.tblReminder.Subject, dbo.tblReminder.Reminder_Description, dbo.tblReminder.[User],  " _
                  & "   dbo.tblReminderDetail.Dismiss " _
                  & "  FROM dbo.tblReminder INNER JOIN " _
                  & " dbo.tblReminderDetail ON dbo.tblReminder.ReminderId = dbo.tblReminderDetail.ReminderId WHERE Case When User_Reminder_Time is not NULL Then Convert(Varchar, (Convert(varchar, User_Reminder_Date, 102) + ' ' + User_Reminder_Time), 101) ELSE User_Reminder_Date End < = '" & ReminderFromDate & "'  AND User_ID=" & LoginUserId & " AND ISNULL(Dismiss,0)=0"
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            getReminders = dt
            Return getReminders
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetMonths() As DataTable
        Try
            Dim dt As New DataTable
            dt.Columns.Add("Month", GetType(System.UInt32))
            dt.Columns.Add("Month_Name", GetType(System.String))
            Dim dr As DataRow
            For i As Integer = 1 To 12
                dr = dt.NewRow
                dr(0) = i
                dr(1) = GetMonthName(i)
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetMonthName(ByVal Month As Integer) As String
        Try
            GetMonthName = Nothing
            Select Case Month
                Case 1
                    GetMonthName = "January"
                Case 2
                    GetMonthName = "February"
                Case 3
                    GetMonthName = "March"
                Case 4
                    GetMonthName = "April"
                Case 5
                    GetMonthName = "May"
                Case 6
                    GetMonthName = "June"
                Case 7
                    GetMonthName = "July"
                Case 8
                    GetMonthName = "August"
                Case 9
                    GetMonthName = "September"
                Case 10
                    GetMonthName = "October"
                Case 11
                    GetMonthName = "November"
                Case 12
                    GetMonthName = "December"
            End Select
            Return GetMonthName
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetYears() As DataTable
        Try
            Dim dt As New DataTable
            dt.Columns.Add("Year", GetType(System.Int32))
            Dim dr As DataRow
            For i As Integer = 2007 To Date.Now.Year
                dr = dt.NewRow
                dr(0) = i
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetConnectionString() As SqlClient.SqlConnectionStringBuilder
        Try
            Connection_String = "Data Source=" & _SvrName & ";Initial Catalog=" & _DB_Name & ";" & IIf(_User_Id = "", "Integrated Security=True", "Integrated Security=False;User ID=" & _User_Id & ";Password=" & _DB_Password & "") & ";Connect Timeout=240"
            Dim sqlConString As New SqlClient.SqlConnectionStringBuilder(Connection_String)
            Get_ConnectionString = sqlConString
            Return sqlConString
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetConnectionStringMaster() As SqlClient.SqlConnectionStringBuilder
        Try
            ConnectionStringMaster = "Data Source=" & _SvrName & ";Initial Catalog=master;" & IIf(_User_Id = "", "Integrated Security=True", "Integrated Security=False;User ID=" & _User_Id & ";Password=" & _DB_Password & "") & ";Connect Timeout=240"
            Dim sqlConString As New SqlClient.SqlConnectionStringBuilder(ConnectionStringMaster)
            get_ConnectionStringMaster = sqlConString
            Return sqlConString
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SpellChecker(ByVal txtBox As Infragistics.Win.UltraWinEditors.UltraTextEditor) As Boolean
        Try
            Dim uSpellChecker As New Infragistics.Win.UltraWinSpellChecker.UltraSpellChecker
            uSpellChecker.SpellOptions.LanguageParser = Win.UltraWinSpellChecker.LanguageType.English
            uSpellChecker.Mode = Win.UltraWinSpellChecker.SpellCheckingMode.AsYouType
            uSpellChecker.UnderlineSpellingErrorStyle = Win.UltraWinSpellChecker.UnderlineErrorsStyle.SingleLine
            uSpellChecker.UnderlineSpellingErrorColor = Color.Red
            txtBox.SpellChecker = uSpellChecker
            uSpellChecker.Dispose()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetVersionInfo() As String
        Try
            'Dim VersionId As Integer = 0

            If Not getConfigValueByType("VersionId").ToString = "Error" Then
                SoftwareVersion = Val(getConfigValueByType("VersionId").ToString)
            Else
                SoftwareVersion = 0
            End If
            Select Case SoftwareVersion
                Case Is = 1
                    _GetVersionInfo = "Basic Edition"
                Case Is = 2
                    _GetVersionInfo = "Small Business Edition"
                Case Is = 3
                    _GetVersionInfo = "Corporate Edition"
                Case Is = 4
                    _GetVersionInfo = "Enterprise Edition"
                Case Is = 5
                    _GetVersionInfo = "Enterprise Edition Plus"
                Case Is = 0
                    _GetVersionInfo = "Basic Edition"
            End Select
            Return _GetVersionInfo
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function FileExportPath() As String
        Try
            _FileExportPath = getConfigValueByType("FileExportPath").ToString
            If _FileExportPath.Length > 0 Then
                Return _FileExportPath
            Else
                _FileExportPath = str_ApplicationStartUpPath & "\EmailAttachments"
                Return _FileExportPath
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function AdminEmails() As String
        Try
            AdminEmail = getConfigValueByType("AdminEmailId").ToString
            If AdminEmail.Length > 0 Then
                Return AdminEmail
            Else
                Return "info@siriussolution.com"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function IsEmailAttachment() As Boolean
        Try
            Dim IsAttachment As Boolean = Convert.ToBoolean(getConfigValueByType("EmailAttachment"))
            IsAttachmentFile = IsAttachment
            Return IsAttachmentFile
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function EmailAlter() As Boolean
        Try
            Dim IsEmail_Alert As Boolean = Convert.ToBoolean(getConfigValueByType("EmailAlert").ToString)
            IsEmailAlert = IsEmail_Alert
            Return IsEmailAlert
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetCostManagement(ByVal ArticleDefId As Integer) As DataTable
        Try
            Dim str As String = "Select ISNULL(TradePrice,0) as TradePrice, ISNULL(Freight,0) as Freight, ISNULL(MarketReturns,0) as MarketReturns, ISNULL(GST_Applicable,0) as GST_Applicable, ISNULL(FlatRate_Applicable,0) as FlatRate_Applicable, ISNULL(FlatRate,0) as FlatRate From ArticleDefTable WHERE ArticleId=" & ArticleDefId & " and (TradePrice Is Not Null Or TradePrice <> 0)"
            Dim dt As DataTable = GetDataTable(str)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Return dt
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ConfigRights(ByVal UserId As Integer)
        Try
            GroupRights = GetRights(UserId) 'New GroupRightsBL().GetRights(UserId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetRights(ByVal UserId As Integer) As List(Of GroupRights)
        'Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        Dim objCon As New OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Open Then objCon.Close()
        objCon.Open()
        'Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            'Dim CommandParameter As SqlClient.SqlParameter() = {SQLHelper.CreateParameter("UserId", SqlDbType.Int, UserId)}
            Dim cmd As New OleDbCommand("SP_GetRights " & UserId & "", objCon)
            Dim dr As OleDbDataReader = cmd.ExecuteReader '(trans, CommandType.StoredProcedure, "SP_GetRights", CommandParameter)

            Dim GroupRightsList As New List(Of GroupRights)
            'GroupRightsList = New List(Of GroupRights)
            Dim GroupRights As GroupRights
            If Not dr Is Nothing Then
                If dr.HasRows Then
                    If dr.IsClosed = False Then
                        Do While dr.Read
                            GroupRights = New GroupRights
                            GroupRights.FormId = Val(dr.GetValue(EnmGroupRights.FormId).ToString)
                            GroupRights.FormName = dr.GetValue(EnmGroupRights.FormName)
                            GroupRights.FormControlId = Val(dr.GetValue(EnmGroupRights.FormControlId).ToString)
                            GroupRights.FormControlName = dr.GetValue(EnmGroupRights.FormControlName)
                            If Not IsDBNull(dr.GetValue(EnmGroupRights.Rights)) Then
                                GroupRights.Rights = dr.GetValue(EnmGroupRights.Rights)
                            Else
                                GroupRights.Rights = False
                            End If
                            GroupRights.UserId = Val(dr.GetValue(EnmGroupRights.UserId).ToString)
                            GroupRightsList.Add(GroupRights)
                        Loop
                    End If
                End If
            End If
            'trans.Commit()
            Return GroupRightsList
        Catch ex As SqlClient.SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Function
    Public Sub GetSIRIUSPartner(ByVal str As String)

        Try
            If str.Length > 0 Then
                str_Company = str
                str_MessageHeader = str
            Else
                str_Company = str_Company
                str_MessageHeader = str_MessageHeader
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function GetCompanyDataTable() As DataTable
        Try
            Dim dt As New DataTable
            'If Not System.IO.File.Exists(str_ApplicationStartUpPath & "\CompanyList.xml") Then Return dt
            'dt.Columns.Add("ID", System.Type.GetType("System.String"))
            'dt.Columns.Add("Name", System.Type.GetType("System.String"))
            'dt.Columns.Add("Server", System.Type.GetType("System.String"))
            'dt.Columns.Add("User ID", System.Type.GetType("System.String"))
            'dt.Columns.Add("Password", System.Type.GetType("System.String"))
            'dt.Columns.Add("DBName", System.Type.GetType("System.String"))
            'If Not System.IO.File.Exists(str_ApplicationStartUpPath & "\CompanyConnectionInfo.xml") Then Return dt
            'dt.Columns.Add("Id", System.Type.GetType("System.String"))
            'dt.Columns.Add("Title", System.Type.GetType("System.String"))
            'dt.Columns.Add("Servername", System.Type.GetType("System.String"))
            'dt.Columns.Add("UserID", System.Type.GetType("System.String"))
            'dt.Columns.Add("Password", System.Type.GetType("System.String"))
            'dt.Columns.Add("DBName", System.Type.GetType("System.String"))
            ''Dim dr As DataRow
            'Dim myReader As Xml.XmlReader
            'myReader = Xml.XmlReader.Create(str_ApplicationStartUpPath & "\CompanyList.xml")
            'While myReader.Read
            '    If myReader.NodeType = Xml.XmlNodeType.Element Then
            '        If myReader.Name = "Company" Then
            '            dr = dt.NewRow
            '            dr("ID") = myReader.GetAttribute("ID").ToString
            '            dr("Name") = myReader.GetAttribute("Name").ToString
            '            dr("Server") = myReader.GetAttribute("Server").ToString
            '            dr("User ID") = myReader.GetAttribute("UserID").ToString
            '            dr("Password") = myReader.GetAttribute("Password").ToString
            '            dr("DBName") = myReader.GetAttribute("DBName").ToString
            '            dt.Rows.Add(dr)
            '        Else
            '        End If
            '    End If
            'End While
            ' dt.ReadXml(str_ApplicationStartUpPath & "\CompanyConnectionInfo.Xml")
            Dim FilePath As String = str_ApplicationStartUpPath
            Try
                If IO.File.ReadAllText(str_ApplicationStartUpPath & "\CompanyConnectionInfo.Xml").StartsWith("FilePath") Then
                    Dim str As String() = IO.File.ReadAllText(str_ApplicationStartUpPath & "\CompanyConnectionInfo.Xml").Split("|")
                    FilePath = str(1).ToString
                End If
            Catch ex As Exception

            End Try
            If Not IO.File.Exists(str_ApplicationStartUpPath & "\CompanyConnectionInfo.Xml") Then
                Throw New Exception("Can't find database connection information." & Chr(10) & Chr(10) & "Please check that folder [" & str_ApplicationStartUpPath & "] is accessable or contact your Agrius ERP administrator.")
            End If
            Dim ds As New DataSet
            Dim dtCon As New DataTable
            ds.ReadXml(FilePath & "\CompanyConnectionInfo.Xml")
            dtCon = ds.Tables(0)
            dt = dtCon
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetDataTable(ByVal strSql As String, Optional ByVal trans As OleDbTransaction = Nothing) As DataTable
        Try
            Dim cmd As New OleDbCommand
            If trans Is Nothing Then
                cmd.Connection = New OleDbConnection(Con.ConnectionString)
                If cmd.Connection.State = ConnectionState.Closed Then cmd.Connection.Open()
            Else
                cmd.Connection = trans.Connection
            End If
            cmd.CommandTimeout = 300
            cmd.CommandText = strSql

            If Not trans Is Nothing Then cmd.Transaction = trans
            Dim da As New OleDbDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            If trans Is Nothing Then cmd.Connection.Close()
            dt.AcceptChanges()
            Return dt
        Catch ex As OleDbException
            If Not trans Is Nothing Then trans.Rollback()
            If ex.ErrorCode = -21472178333 Then Exit Try
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function msg_Confirm(ByVal strMessage As String, Optional ByVal Print As Boolean = False, Optional ByVal DirectVoucherPrinting As Boolean = False)
        'Dim frm As frmMessages
        frmMessages.Print = Print
        frmMessages.DirectVoucherPrinting = DirectVoucherPrinting
        If frmMessages.MsgBox(strMessage) = MsgBoxResult.Yes Then
            Return True
        Else
            Return False

        End If
        'If MsgBox(strMessage, MsgBoxStyle.YesNo + MsgBoxStyle.Question, str_MessageHeader) = MsgBoxResult.Yes Then
        '    Return True
        'Else
        '    Return False
        'End If
    End Function
    ''' <summary>
    ''' TASK TFS1609
    ''' </summary>
    ''' <param name="strMessage"></param>
    ''' <param name="Print"></param>
    ''' <param name="DirectVoucherPrinting"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function msg_Confirm2(ByVal strMessage As String, Optional ByVal Print As Boolean = False, Optional ByVal DirectVoucherPrinting As Boolean = False, Optional ByVal FinancialImpact As Boolean = False, Optional ByVal VoucherNo As String = "", Optional ByVal Save As Boolean = False, Optional ByVal AddNewVoucher As Boolean = False, Optional ByVal FirstVoucherNo As String = "", Optional ByVal VoucherReference As String = "")
        'Dim frm As frmMessages
        frmImportPopup.Print = Print
        frmImportPopup.DirectVoucherPrinting = DirectVoucherPrinting
        frmImportPopup.FinancialImpact = FinancialImpact
        frmImportPopup.VoucherNo = VoucherNo
        frmImportPopup.VoucherNo = VoucherNo
        frmImportPopup.Save = Save
        frmImportPopup.AddNewVoucher = AddNewVoucher
        frmImportPopup.FirstVoucherNo = FirstVoucherNo
        frmImportPopup.VoucherReference = VoucherReference
        If frmImportPopup.MsgBox(strMessage) = MsgBoxResult.Yes Then
            Return True
        Else
            Return False
        End If
        'If MsgBox(strMessage, MsgBoxStyle.YesNo + MsgBoxStyle.Question, str_MessageHeader) = MsgBoxResult.Yes Then
        '    Return True
        'Else
        '    Return False
        'End If
    End Function
    Public Function msg_Confirm1(ByVal strMessage As String)
        Dim frm As New frmSalesPrintMessage
        If frm.MsgBox(strMessage) = MsgBoxResult.Yes Then
            Return True
        Else
            Return False

        End If
        'If MsgBox(strMessage, MsgBoxStyle.YesNo + MsgBoxStyle.Question, str_MessageHeader) = MsgBoxResult.Yes Then
        '    Return True
        'Else
        '    Return False
        'End If
    End Function
    Public Function msg_Confirm_DualPrint(ByVal strMessage As String, ByVal Voucher As Boolean, ByVal Slip As Boolean, ByVal frm As frmMessages)

        If frm.MsgBox_Print(strMessage) = MsgBoxResult.Yes Then
            Return True
        Else
            Return False

        End If
        'If MsgBox(strMessage, MsgBoxStyle.YesNo + MsgBoxStyle.Question, str_MessageHeader) = MsgBoxResult.Yes Then
        '    Return True
        'Else
        '    Return False
        'End If
    End Function


    Public Function GetRecords(ByVal strsql As String) As DataTable
        Try
            Dim da As New OleDbDataAdapter(strsql, Con)
            Dim dt As New DataTable("GetRecords")
            da.Fill(dt)
            dt.AcceptChanges()
            Return dt
        Catch ex As OleDb.OleDbException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally

        End Try
    End Function
    Public Sub msg_Information(ByVal strMessage As String, Optional ByVal MessageWaitTime As Integer = 10)
        Try

            frmMain.ShowErrorNotification(strMessage, , MessageWaitTime)


            'MsgBox(strMessage, MsgBoxStyle.Information, str_MessageHeader)
            ' ''Dim frm As New frmMessage
            ' ''frm.MsgBox(strMessage, str_MessageHeader)

            '' ''frmMessage.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub msg_Error(ByVal strMessage As String, Optional ByVal MessageWaitTime As Integer = 10)
        Try
            'MsgBox(strMessage, MsgBoxStyle.Critical, str_MessageHeader)

            ' ''Dim frm As New frmErrorMessage
            ' ''frm.ShowMsg(strMessage)
            frmMain.ShowErrorNotification(strMessage, MsgBoxStyle.Critical, MessageWaitTime)
        Catch ex As Exception

        End Try
    End Sub
    '**************************************************************************
    ' This function takes voucher type as paramter and returns voucher type id
    '**************************************************************************
    Public Function GetVoucherTypeId(ByVal strVoucherType As String) As Long
        Dim strQuery As String
        strQuery = "SELECT Voucher_Type_ID, Voucher_Type FROM tblDefVoucherType WHERE voucher_type = '" & strVoucherType & "'"

        Dim dt As New DataTable
        Dim adp As New OleDbDataAdapter
        adp = New OleDbDataAdapter(strQuery, Con)
        adp.Fill(dt)
        dt.AcceptChanges()
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0).Item(0).ToString
        Else
            Return "Error"
        End If
        'lngVoucherTypeId = objCommand.ExecuteScalar

    End Function

    '**************************************************************************
    ' This function takes voucher Master Id as paramter and returns voucher id
    '**************************************************************************
    Public Function GetVoucherId(ByVal Source As String, ByVal VoucherCode As String, Optional ByVal objTrans As OleDb.OleDbTransaction = Nothing) As Long

        Dim strQuery As String = String.Empty
        ''10-Mar-2014   Task:2483  Imran Ali  Cutomer Balance Not Match in Daily Working Report
        'strQuery = "SELECT Voucher_ID FROM tblVoucher where source='" & Source & "' and voucher_code='" & VoucherCode & "'"
        ''Task:2483 Added Clause Voucher_No
        'strQuery = "SELECT Voucher_ID FROM tblVoucher where source='" & Source & "' and voucher_code='" & VoucherCode & "' AND voucher_no='" & VoucherCode & "'"
        ''In this query voucher_code condition is excluded because at some places voucher_code is not saved and actually voucher_no and voucher_code remained same. on 13-12-17
        strQuery = "SELECT Voucher_ID FROM tblVoucher where source='" & Source & "' AND voucher_no='" & VoucherCode & "'"

        'End Task:2483

        Dim dt As New DataTable
        Dim adp As New OleDbDataAdapter
        Dim cmd As New OleDbCommand
        If objTrans Is Nothing Then
            'adp = New OleDbDataAdapter(strQuery, Con)
            cmd.Connection = Con
        Else
            'adp = New OleDbDataAdapter(strQuery, objTrans.Connection)
            cmd.Connection = objTrans.Connection
            cmd.Transaction = objTrans
        End If
        cmd.CommandText = strQuery
        cmd.CommandType = CommandType.Text
        adp.SelectCommand = cmd
        adp.Fill(dt)
        dt.AcceptChanges()
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0).Item(0).ToString
        Else
            Return 0
        End If
        'lngVoucherTypeId = objCommand.ExecuteScalar
    End Function
    Public Function GetVoucherId2(ByVal Source As String, ByVal VoucherCode As String, Optional ByVal objTrans As OleDb.OleDbTransaction = Nothing) As Long

        Dim strQuery As String = String.Empty


        Dim objcon As New OleDbConnection(Con.ConnectionString)
        Dim Trans As OleDbTransaction
        If objcon.State = ConnectionState.Closed Then objcon.Open()
        Trans = objcon.BeginTransaction
        Try
            strQuery = "SELECT Voucher_ID FROM tblVoucher where source='" & Source & "' and voucher_code='" & VoucherCode & "' AND voucher_no='" & VoucherCode & "'"
            Dim dt As New DataTable

            Dim cmd As New OleDbCommand
            cmd.Connection = objcon
            cmd.Transaction = Trans
            cmd.CommandType = CommandType.Text
            cmd.CommandText = strQuery
            Dim adp As New OleDbDataAdapter(cmd)
            adp.Fill(dt)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(0).ToString
            Else
                Return 0
            End If

        Catch ex As Exception

        End Try

    End Function


    '**************************************************************************
    ' This function takes voucher Master Id as paramter and returns voucher id
    '**************************************************************************
    Public Function GetAccountBalance(ByVal AccountId As Integer, Optional DocNo As String = "") As Long

        Dim strQuery As String
        strQuery = "SELECT isnull(sum(debit_amount) - sum(credit_amount),0) from tblvoucherdetail INNER JOIN tblVoucher On tblVoucher.Voucher_Id = tblVoucherDetail.Voucher_Id where IsNull(Post,0)=1 AND tblvoucherdetail.coa_detail_id=" & AccountId & " " & IIf(DocNo.Length > 0, " AND tblVoucher.Voucher_Code <> N'" & DocNo.Replace("'", "''") & "'", "")
        Dim dt As New DataTable
        Dim adp As New OleDbDataAdapter
        Try
            adp = New OleDbDataAdapter(strQuery, Con)
            adp.Fill(dt)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(0).ToString
            Else
                Return 0
            End If
            'lngVoucherTypeId = objCommand.ExecuteScalar

        Catch ex As Exception
            Return 0
        End Try

    End Function

    '*******************************************************************************
    ' This function takes DocId and DocSource and Remove Attachments associted with it
    '******************************************************************************
    Public Function RemoveAttachments(ByVal DocId As Integer, ByVal Source As String) As Boolean
        ''TFS4652 : AyeshaRehman : Attachment removal option on Sales Order before saving.
        Dim objCommand As New OleDbCommand
        Dim objTrans As OleDbTransaction
        If Con.State = ConnectionState.Closed Or Con.State = ConnectionState.Broken Then Con.Open()
        objTrans = Con.BeginTransaction
        Try
            objCommand = New OleDbCommand
            objCommand.Connection = Con
            objCommand.CommandText = "Delete From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'"
            objCommand.Transaction = objTrans
            objCommand.ExecuteNonQuery()

            objTrans.Commit()
            Return True
        Catch ex As Exception
            objTrans.Rollback()
            Return False
        End Try
    End Function
    '************************************************************************************************
    ' This function takes DocId and DocSource and Remove Attachments associted with it from Directory
    '************************************************************************************************
    Public Function RemoveAttachmentsFromDirectory(ByVal DocId As Integer, ByVal Source As String) As Boolean
        Try
            Dim dt As DataTable = GetDataTable("Select DocId,FileName,Path From DocumentAttachment WHERE (source = N'" & Source & "') and DocId = " & DocId & "")
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For Each row As DataRow In dt.Rows
                        If IO.File.Exists(row.Item("Path").ToString & "\" & row.Item("FileName").ToString) Then
                            IO.File.Delete(row.Item("Path").ToString & "\" & row.Item("FileName").ToString)
                        End If
                    Next
                End If
            End If
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function

    '**************************************************************************
    ' This function takes voucher Master Id as paramter and returns voucher id
    '**************************************************************************
    Public Function GetAccountBalance(ByVal AccountId As Integer, ByVal AsOn As Date) As Long

        Dim strQuery As String
        strQuery = "SELECT     ISNULL(SUM(dbo.tblVoucherDetail.debit_amount) - SUM(dbo.tblVoucherDetail.credit_amount), 0) AS Bal " & _
                    " FROM         dbo.tblVoucherDetail INNER JOIN " & _
                    " dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id " & _
                    " where tblVoucherDetail.coa_detail_id = " & AccountId & " and convert(datetime , convert(varchar, left(tblVoucher.voucher_date,11),102),102) <='" & AsOn.Date.ToString("yyyy-MM-dd") & "'"

        Dim dt As New DataTable
        Dim adp As New OleDbDataAdapter
        Try
            adp = New OleDbDataAdapter(strQuery, Con)
            adp.Fill(dt)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(0).ToString
            Else
                Return 0
            End If
            'lngVoucherTypeId = objCommand.ExecuteScalar

        Catch ex As Exception
            Return 0
        End Try

    End Function

    '**************************************************************************
    ' This function will be used to make voucher entry from various forms
    '**************************************************************************
    Public Function SaveVoucherEntry(ByVal lngVoucherTypeId As Long, ByVal strVoucherNo As String, ByVal dtVoucherDate As Date, _
                                ByVal strChequeNo As String, ByVal dtChequePostDate As Date, ByVal DebitCOADetailId As Long, ByVal CreditCOADetailId As Long, _
                                ByVal lngDebitAmount As Long, ByVal lngCreditAmount As Long, ByVal strSaveWhat As String, Optional ByVal Source As String = "UN-KNOWN", Optional ByVal VoucherCode As String = "UN-KNOWN", Optional ByVal DeleteOld As Boolean = False)


        Dim objCommand As New OleDbCommand
        Dim objTrans As OleDbTransaction
        Dim lngVoucherMasterId As Long
        If Con.State = ConnectionState.Closed Or Con.State = ConnectionState.Broken Then Con.Open()
        objTrans = Con.BeginTransaction
        Try


            If strSaveWhat = "Master" Then
                objCommand.Connection = Con
                objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                                       & " cheque_no, cheque_date,post,Source,voucher_code)" _
                                       & " VALUES(1, 1, " & lngVoucherTypeId & ", '" & strVoucherNo & "', '" & dtVoucherDate & "', '" _
                                       & IIf(strChequeNo.Trim.Length > 0, strChequeNo, "NULL") & "', '" & IIf(dtChequePostDate.ToString <> "", dtChequePostDate, Nothing) & "', 0,'" & Source & "','" & VoucherCode & "')" _
                                       & " SELECT @@IDENTITY"

                objCommand.Transaction = objTrans

                lngVoucherMasterId = objCommand.ExecuteScalar

            ElseIf strSaveWhat = "Detail" Then
                If lngDebitAmount > 0 Then

                    '***********************
                    'Inserting Debit Amount
                    '***********************
                    objCommand = New OleDbCommand
                    objCommand.Connection = Con
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount) " _
                                           & " VALUES(" & lngVoucherMasterId & ", 1, " & DebitCOADetailId & ", " & lngDebitAmount & ", 0)"

                    objCommand.Transaction = objTrans
                    objCommand.ExecuteNonQuery()

                ElseIf lngCreditAmount > 0 Then

                    '***********************
                    'Inserting Credit Amount
                    '***********************
                    objCommand = New OleDbCommand
                    objCommand.Connection = Con
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount) " _
                                           & " VALUES(" & lngVoucherMasterId & ", 1, " & CreditCOADetailId & ", " & 0 & ",  " & lngCreditAmount & ")"
                    objCommand.Transaction = objTrans
                    objCommand.ExecuteNonQuery()
                End If

            ElseIf strSaveWhat = "Both" Then
                If DeleteOld = True Then

                    '***********************
                    'Deleting Master
                    '***********************
                    objCommand = New OleDbCommand
                    objCommand.Connection = Con
                    objTrans = Con.BeginTransaction
                    objCommand.CommandText = "delete from tblVoucherDetail where voucher_Id in(select voucher_Id from tblVoucher where source='" & Source & "' and voucher_code='" & VoucherCode & "')"

                    objCommand.Transaction = objTrans

                    objCommand.ExecuteNonQuery()

                    '***********************
                    'Deleting Detail
                    '***********************
                    objCommand = New OleDbCommand
                    objCommand.Connection = Con
                    objCommand.CommandText = "delete from tblVoucher where source='" & Source & "' and voucher_code='" & VoucherCode & "'"

                    objCommand.Transaction = objTrans

                    objCommand.ExecuteNonQuery()

                End If

                '***********************
                'Inserting Master
                '***********************
                objCommand = New OleDbCommand
                objCommand.Connection = Con

                objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                                        & " cheque_no, cheque_date,post,Source,voucher_code)" _
                                        & " VALUES(1, 1, " & lngVoucherTypeId & ", '" & strVoucherNo & "', '" & dtVoucherDate & "', '" _
                                        & IIf(strChequeNo.Trim.Length > 0, strChequeNo, "NULL") & "', '" & IIf(dtChequePostDate.ToString <> "", dtChequePostDate, Nothing) & "', 0,'" & Source & "','" & VoucherCode & "')" _
                                        & " SELECT @@IDENTITY"

                objCommand.Transaction = objTrans

                lngVoucherMasterId = objCommand.ExecuteScalar

                '***********************
                'Inserting Debit Amount
                '***********************
                objCommand = New OleDbCommand
                objCommand.Connection = Con
                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount) " _
                                       & " VALUES(" & lngVoucherMasterId & ", 1, " & DebitCOADetailId & ", " & lngDebitAmount & ", 0)"

                objCommand.Transaction = objTrans
                objCommand.ExecuteNonQuery()

                '***********************
                'Inserting Credit Amount
                '***********************
                objCommand = New OleDbCommand
                objCommand.Connection = Con
                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount) " _
                                       & " VALUES(" & lngVoucherMasterId & ", 1, " & CreditCOADetailId & ", " & 0 & ",  " & lngCreditAmount & ")"
                objCommand.Transaction = objTrans
                objCommand.ExecuteNonQuery()
            End If

            objTrans.Commit()
            Return True
        Catch ex As Exception
            objTrans.Rollback()
            Return False
        End Try

    End Function

    '**************************************************************************
    ' This function will be used to update voucher entry from various forms
    '**************************************************************************
    Public Function UpdateVoucherEntry(ByVal lngVoucherTypeId As Long, ByVal strVoucherNo As String, ByVal dtVoucherDate As Date, _
                                ByVal strChequeNo As String, ByVal dtChequePostDate As Date, ByVal DebitCOADetailId As Long, ByVal CreditCOADetailId As Long, _
                                ByVal lngDebitAmount As Long, ByVal lngCreditAmount As Long, ByVal strSaveWhat As String, Optional ByVal Source As String = "UN-KNOWN", Optional ByVal VoucherCode As String = "UN-KNOWN", Optional ByVal DeleteOld As Boolean = False)

        Dim objCommand As New OleDbCommand
        Dim objTrans As OleDbTransaction
        Dim lngVoucherMasterId As Long
        If Con.State = ConnectionState.Closed Or Con.State = ConnectionState.Broken Then Con.Open()
        objTrans = Con.BeginTransaction
        Try


            lngVoucherMasterId = GetVoucherId(Source, VoucherCode)
            '***********************
            'updating Master
            '***********************
            objCommand = New OleDbCommand
            objCommand.Connection = Con



            objCommand.CommandText = "update tblVoucher set voucher_date='" & dtVoucherDate & "', cheque_no='" & IIf(strChequeNo.Trim.Length > 0, strChequeNo, "NULL") & "'," _
                                    & "  cheque_date ='" & IIf(dtChequePostDate.ToString <> "", dtChequePostDate, Nothing) & "' where voucher_id=" & lngVoucherMasterId

            objCommand.Transaction = objTrans

            objCommand.ExecuteNonQuery()

            '***********************
            'Deleting Detail
            '***********************
            objCommand = New OleDbCommand
            objCommand.Connection = Con

            objCommand.CommandText = "delete from tblVoucherDetail where voucher_Id =" & lngVoucherMasterId

            objCommand.Transaction = objTrans

            objCommand.ExecuteNonQuery()

            '***********************
            'Inserting Debit Amount
            '***********************
            objCommand = New OleDbCommand
            objCommand.Connection = Con
            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount) " _
                                   & " VALUES(" & lngVoucherMasterId & ", 1, " & DebitCOADetailId & ", " & lngDebitAmount & ", 0)"

            objCommand.Transaction = objTrans
            objCommand.ExecuteNonQuery()

            '***********************
            'Inserting Credit Amount
            '***********************
            objCommand = New OleDbCommand
            objCommand.Connection = Con
            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount) " _
                                   & " VALUES(" & lngVoucherMasterId & ", 1, " & CreditCOADetailId & ", " & 0 & ",  " & lngCreditAmount & ")"
            objCommand.Transaction = objTrans
            objCommand.ExecuteNonQuery()

            objTrans.Commit()
            Return True
        Catch ex As Exception
            objTrans.Rollback()
            Return False
        End Try

    End Function
    Public Function GetFormRights(ByVal FormName As EnumForms) As DataTable
        Dim cmd As New OleDbCommand
        Try
            cmd.Connection = Con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "SP_GetUserFormRights"

            cmd.Parameters.Add("@User_ID", OleDbType.Integer).Value = LoginUserId
            cmd.Parameters.Add("@FormName", OleDbType.VarChar, 100).Value = FormName.ToString()

            Dim da As New OleDbDataAdapter(cmd)
            Dim dt As New DataTable("FormRights")
            da.Fill(dt)
            dt.AcceptChanges()
            Return dt

        Catch ex As OleDb.OleDbException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            cmd.Connection.Close()
            cmd.Parameters.Clear()
            cmd = Nothing
        End Try
    End Function
    'Public Function GetRights(ByVal FormName As String, ByVal UserId As Integer) As DataTable
    '    Dim cmd As New OleDbCommand
    '    Try
    '        cmd.Connection = Con
    '        cmd.CommandType = CommandType.StoredProcedure
    '        cmd.CommandText = "SP_GetRights"

    '        cmd.Parameters.Add("@FormName", OleDbType.VarChar).Value = FormName
    '        cmd.Parameters.Add("@UserId", OleDbType.Integer).Value = UserId

    '        Dim da As New OleDbDataAdapter(cmd)
    '        Dim dt As New DataTable
    '        dt.Namespace = "Rights"
    '        dt.TableName = "SecurityRights"
    '        da.Fill(dt)
    '        Return dt

    '    Catch ex As OleDbException
    '        Throw ex
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        cmd.Connection.Close()
    '        cmd.Parameters.Clear()
    '        cmd = Nothing
    '    End Try
    'End Function
    Public Sub GetFormRights(ByVal FormName As String)
        Try
            If Not GroupRights Is Nothing Then
                Rights = New List(Of GroupRights)
                For Each RightsDetail As GroupRights In GroupRights
                    If RightsDetail.FormName = FormName Then
                        Rights.Add(RightsDetail)
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function GetSerialNo(ByVal FirstChar As String, ByVal TableName As String, ByVal FieldSerialNo As String) As String
        Try
            Dim Serial As Integer = 0
            Dim SerialNo As String = String.Empty
            Dim str As String
            str = "Select ISNULL(Max(Right(" & FieldSerialNo & ",5)),0) as Serial From " & TableName & " WHERE LEFT(" & FieldSerialNo & "," & FirstChar.Length & ")='" & FirstChar & "'"
            Dim dt As DataTable = GetDataTable(str)
            If Not dt Is Nothing Then
                If dt.Rows.Count < 0 Then
                    Serial = 0
                Else
                    Serial = CInt(dt.Rows(0).Item(0))
                End If
            End If
            Serial = Serial + 1
            SerialNo = CStr(FirstChar + Microsoft.VisualBasic.Right("00000" + CStr(Serial), 5))
            Return SerialNo
        Catch ex As Exception
            Throw ex
        Finally
        End Try
    End Function
    Enum EnumGroupType
        Standard_User
        Administrator
        Guest
    End Enum
    Enum EnumStatus
        Open
        Close
        'Reject
        'DeActive
        All
    End Enum
    Enum EnumForms
        Non
        DefMainAcc
        frmDetailAccount
        frmSubAccount
        frmSubSubAccount
        frmDefArea
        frmDefCategory
        frmDefCity
        frmDefColor
        frmDefCustomer
        frmDefEmployee
        frmDefSize
        frmDefCompany
        frmDefLpo
        frmDefTransporter
        frmDefType
        frmDefVendor
        SimpleItemDefForm
        frmDefUser
        frmCustomerCollection
        frmPurchaseReturn
        frmSales
        frmSalesReturn
        frmUpdatebitlyAndTransporter
        frmVendorPayment
        frmVoucher
        frmPurchase
        frmPurchaseOrder
        frmSaleOrder
        frmStockDispatch
        frmStockReceive
        frmStoreIssuence
        rptInventoryForm
        rptStockForm
        rptTrialBalance
        frmSOStatus
        frmBudget
        frmCustomerPlanning
        frmChequeTransfer
        frmPOStatus
        frmSystemConfiguration
        rtpInventoryLevel
        frmExpense
        rptCategoryWiseSaleReport
        rptPriceChangeReport
        rptStockAccountsReport
        frmDefBatch
        frmAttendance
        frmRptStockStatment
        frmInventoryLevel
        rptStockReportWithCritera
        frmYearlySaleTarget
        frmCostCenter
        rptSalesChart
        rptSalesmanMonthlySalesReport
        rptDailyWorkingRport
        frmDefSecurityGroup
        Vendors
        Employee
        Stock
        Purchase
        Sales
        Accounts
        AccountsReports
        frmTasks
        frmProjects
        frmTypes
        frmStatus
        frmComposeMessage
        frmMessageView
        frmPaymentVoucherNew
        frmReceiptVoucherNew
        frmOpening
        ' frmAddCustomersss
        'frmAddVendors
        frmProductionStore
        frmReturnablegatepass
        frmInwardGatePass
        FrmEmailconfig
        frmStockAdjustment
        frmDepartmentWiseProduction
        frmLeaveApplication
        rptSalesManTarget
        SummaryofSalesInvoices
        SummaryofSalesTaxInvoices
        SummaryofSalesInvoicesReturn
        rptVoucher
        DemandSales
        WeightReport
        DemandSummary
        DamageBudget
        DeliveryChallanDetail
        DeliveryChallanSummary
        frmRptDSRSummary
        NetSalesReport
        SalesCertificateIssued
        frmRptSalesCertificateLedger
        AdvanceReceiptsSO
        frmRptCustomerSalesContribution
        frmRptDSRStatement
        frmRptBankReconciliation
        CashFlowStatement
        CashFlowStatementStandard
        rptExpenses
        CashReceiptDetailAgainstEmployee
        PLNotesDetail
        'DamageBudget
        'DemandSales
        FrmVoucherCheckList
        'DemandSummary
        frmBalanceSheet
        frmProfitAndLoss
        BalanceSheetNotesSummary
        'FrmVoucherCheckList
        WithHoldingTaxCertificate
        frmRptProjectBasedTransactionDetail
        frmRptDirectorDebitors
        'CashReceiptDetailAgainstEmployee
        StoreIssuanceSummary
        StoreIssuanceDetail
        StockStatementByLPO
        StockStatementWithSize
        frmStockStatmentBySize
        frmRptGrdStockStatement
        RptGridItemSalesHistory
        frmRptGrdStockInOutDetail
        frmGrdRptLocationWiseStockLedger
        frmGrdRptProjectWiseStockLedger
        frmGrdRptStockStatementUnitWise
        frmGrd_Prod_DC_WiseStock
        frmRptArticleBarcode
        frmGrdCostSheetComparisonWithStock
        frmGrdPlanComparison
        frmGrdArticleLedger
        frmRptRental
        StoreIssuanceDetailBatchWise
        frmGrdRptLocationWiseStockStatementNew
        frmGrdRptLocationWiseStockStatement
        frmRptLocationWiseClosingStock
        WarrantyDetailReport
        DispatchStatus
        frmGrdArticleLedgerByPack
        frmRptGrdStockStatementByPack
        frmRptInvoiceAgingFormated
        rptPLComparison
        PLComparisonDetailAccount
        PLComparisonSubSubAccount
        PLDetailAccountSummary
        PLSubSubAccountSummary
        'frmGrdPlanComparison
        frmSalesInquiry
        frmPurchaseInquiry
        frmVendorQuotation
        frmSalesInquiryRights
        frmInquiryComparisonStatement
        frmMaterialEstimation
        frmMaterialAllocation
        frmCostCentreReshuffle
        frmGrdRptClosingStockByGRNnDC
        InvoiceBasedPaymentSummaryReport
        frmHolidySetup
        frmGrdRptAttendanceRegisterUpdate
        frmCashRequest
        frmEmployeeStatusList
        frmClaim
        frmItemBulk
        frmAttendanceEmployees
        frmDefGroupVoucherApproval
        DailyAttendance
        frmrptRoutePlanGatePass
        frmModelList
        frmTermsandConditions
        frmServiceItemTask
        frmGrdRptRackWiseClosingStock
        frmDefJobCard
        frmUserWiseCustomer
        frmIncomeTaxOrSalesTaxAccount
        frmAdvanceType
        frmCustomerBottomSaleRate ''TFS1663
        frmGrdRptPurchaseDemandStatus ''TFS1769
        frmCOAGroupsToUserMapping ''TFS2225
        frmCOAGroupsToAccountsMapping ''TFS2222
        ChartOfAccountGroups ''TFS2219
        frmApprovalRejectionReason ''TFS2375
        frmApprovalProcess ''TFS2375
        frmApprovalStages ''TFS2375
        frmGrdRptAgingReceiveables '' TFS
        'Changes added by Murtaza (12/30/2022)
        frmGrdStockMovement
        frmCustomerDetailReport
        'Changes added by Murtaza (12/30/2022)
    End Enum
    Public Sub AddRptParam(ByVal ParamName As String, ByVal ParamValue As String)
        If str_ReportParam.Length > 1 Then
            str_ReportParam = str_ReportParam + "&" + ParamName.ToString + "|" + ParamValue.ToString
        Else
            str_ReportParam = ParamName.ToString + "|" + ParamValue.ToString
        End If
    End Sub
    Public Sub ApplyStyleSheet(ByVal control As Control)

        If TypeOf control Is Form Then
            control.Font = fnt_FormsFont
            If frmModProperty IsNot Nothing Then
                control.BackColor = frmModProperty.BackColor
            Else
                control.BackColor = Color.White 'Color.FromArgb(226, 235, 247)
            End If
            '  control.BackgroundImage = frmMain.BackgroundImage
            ' control.BackgroundImageLayout = ImageLayout.Stretch
        End If


        If Not control.Name = "frmHome" Then
            For Each ctl As Control In control.Controls
                If ctl.HasChildren Then
                    'ctl.BackColor = Color.Transparent
                    If TypeOf ctl Is Janus.Windows.GridEX.GridEX Then
                        ''change layout of grid
                        Dim grd As Janus.Windows.GridEX.GridEX = CType(ctl, Janus.Windows.GridEX.GridEX)
                        grd.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
                        grd.GridLines = GridLines.Horizontal
                        grd.VisualStyle = VisualStyle.Office2007
                        'grd.GridLineColor = Color.Red
                        grd.Font = New Font("Segoe UI", 10, FontStyle.Regular)
                        'grd.BackColor = Color.Transparent

                    End If
                    If TypeOf ctl Is GroupBox Or TypeOf ctl Is TableLayoutPanel Then
                        ' ctl.BackColor = Color.Transparent
                    End If

                    ApplyStyleSheet(ctl)

                Else

                    'ctl.BackColor = Color.Transparent


                    If TypeOf ctl Is DateTimePicker Then
                        Dim dtp As DateTimePicker = CType(ctl, DateTimePicker)
                        If Not dtp.Format = DateTimePickerFormat.Time Then 'Task:M43 Set Status
                            dtp.Format = DateTimePickerFormat.Custom
                            dtp.CustomFormat = str_DisplayDateFormat
                        ElseIf dtp.Format = DateTimePickerFormat.Time Then
                            'Task:M43 Set Time Format
                            dtp.Format = DateTimePickerFormat.Time
                            dtp.CustomFormat = "hh:mm:ss tt"
                            'End Task:M43
                        End If

                    ElseIf TypeOf ctl Is Infragistics.Win.UltraWinGrid.UltraCombo Then

                        Dim blnListSeachStartWith As Boolean = False
                        Dim blnListSeachEndWith As Boolean = False
                        Dim blnListSeachContains As Boolean = False

                        If getConfigValueByType("ListSearchStartWith").ToString <> "Error" Then
                            blnListSeachStartWith = Convert.ToBoolean(getConfigValueByType("ListSearchStartWith").ToString)
                        End If


                        If getConfigValueByType("ListSearchContains").ToString <> "Error" Then
                            blnListSeachContains = Convert.ToBoolean(getConfigValueByType("ListSearchContains").ToString)
                        End If



                        Dim ultraCombo As Infragistics.Win.UltraWinGrid.UltraCombo = CType(ctl, Infragistics.Win.UltraWinGrid.UltraCombo)
                        ultraCombo.AllowNull = Win.DefaultableBoolean.True
                        If blnListSeachStartWith = True Then
                            ultraCombo.AutoCompleteMode = Win.AutoCompleteMode.Suggest
                            ultraCombo.AutoSuggestFilterMode = Win.AutoSuggestFilterMode.StartsWith
                        End If

                        If blnListSeachContains = True Then
                            ultraCombo.AutoCompleteMode = Win.AutoCompleteMode.Suggest
                            ultraCombo.AutoSuggestFilterMode = Win.AutoSuggestFilterMode.Contains
                        End If


                    ElseIf TypeOf ctl Is GroupBox Then
                        ctl.BackColor = frmModProperty.BackColor

                    ElseIf TypeOf ctl Is GroupBox Or TypeOf ctl Is CheckBox Or TypeOf ctl Is RadioButton Or TypeOf ctl Is TableLayoutPanel Or TypeOf ctl Is Janus.Windows.UI.Dock.UIPanel Or TypeOf ctl Is Panel Then
                        ctl.BackColor = Color.Transparent
                        If control.Name IsNot "pnlHeader" Or control.Name IsNot "pnlHeader1" Or control.Name IsNot "pnlHeader2" Or control.Name IsNot "pnlHeader3" Or control.Name IsNot "pnlHeader4" Or control.Name IsNot "pnlHeader5" Or control.Name IsNot "pnlHeader6" Or control.Name IsNot "pnlHeader7" Or control.Name IsNot "pnlHeader8" Or control.Name IsNot "pnlHeader9" Or control.Name IsNot "pnlHeader10" Or TypeOf ctl Is Panel Then
                            'control.BackColor = Color.Transparent
                            If TypeOf ctl Is ComboBox Or TypeOf ctl Is TextBox Or TypeOf ctl Is CheckBox Or TypeOf ctl Is GroupBox Or TypeOf ctl Is RadioButton Then
                                ctl.BackColor = Color.Transparent
                                ctl.ForeColor = Color.Black
                                ctl.Font = New Font("Segoe UI", 9, FontStyle.Regular)
                            ElseIf TypeOf ctl Is Button Then
                                ctl.ForeColor = Color.Black
                                ctl.BackColor = Color.FromArgb(226, 235, 247)
                            ElseIf TypeOf ctl Is Label Then
                                ctl.BackColor = Color.Transparent
                                ctl.ForeColor = Color.Black
                                ctl.Font = New Font("Segoe UI", 9, FontStyle.Regular Or FontStyle.Italic)
                                Dim LabelAdapter As New Label
                                Dim old As Padding = LabelAdapter.Padding
                                ctl.Padding = New Padding(old.Left, old.Top, old.Right, old.Bottom)
                            End If
                        End If
                    ElseIf TypeOf control Is Panel Then
                        control.BackColor = Color.Transparent
                        If control.Name = "pnlHeader" Or control.Name = "pnlHeader1" Or control.Name = "pnlHeader2" Or control.Name = "pnlHeader3" Or control.Name = "pnlHeader4" Or control.Name = "pnlHeader5" Or control.Name = "pnlHeader6" Or control.Name = "pnlHeader7" Or control.Name = "pnlHeader8" Or control.Name = "pnlHeader9" Or control.Name = "pnlHeader10" Then
                            control.BackColor = Color.FromArgb(0, 120, 215)
                            control.Size = New Size(825, 40)
                            'control.ForeColor = Color.White
                            If ctl.Name = "lblHeader" Or ctl.Name = "lblHeader1" Or ctl.Name = "lblHeader2" Or ctl.Name = "lblHeader3" Or ctl.Name = "lblHeader4" Or ctl.Name = "lblHeader5" Or ctl.Name = "lblHeader6" Or ctl.Name = "lblHeader7" Or ctl.Name = "lblHeader8" Or ctl.Name = "lblHeader9" Or ctl.Name = "lblHeader10" Then
                                ctl.BackColor = Color.Transparent
                                ctl.ForeColor = Color.White

                            ElseIf ctl.Name = "btnHeader" Then
                                ctl.ForeColor = Color.White
                                ctl.BackgroundImage = My.Resources.btn_back

                            End If
                            'control.Name IsNot "Infragistics.Win.Misc.UltraPanelClientAreaUnsafe" Or' 
                        ElseIf TypeOf control Is Panel Then
                            If control.Name IsNot "pnlHeader" Or control.Name IsNot "pnlHeader1" Or control.Name IsNot "pnlHeader2" Or control.Name IsNot "pnlHeader3" Then
                                control.BackColor = Color.Transparent
                                If TypeOf ctl Is ComboBox Or TypeOf ctl Is TextBox Or TypeOf ctl Is Label Or TypeOf ctl Is CheckBox Or TypeOf ctl Is GroupBox Or TypeOf ctl Is RadioButton Then
                                    ctl.Font = New Font("Segoe UI", 9)
                                    ctl.ForeColor = Color.Black
                                    ctl.Font = New Font("Segoe UI", 9, FontStyle.Regular)
                                    Dim LabelAdapter As New Label
                                    Dim old As Padding = LabelAdapter.Padding
                                    ctl.Padding = New Padding(old.Left, old.Top, old.Right, old.Bottom)
                                ElseIf TypeOf ctl Is Button Then
                                    ctl.ForeColor = Color.Black
                                    ctl.BackColor = Color.FromArgb(226, 235, 247)
                                End If
                            End If
                        End If
                    ElseIf TypeOf ctl Is Label Then
                        If ctl.Name = "lblHeader" Or ctl.Name = "lblHeader1" Or ctl.Name = "lblHeader2" Or ctl.Name = "lblHeader3" Or ctl.Name = "lblHeader4" Or ctl.Name = "lblHeader5" Or ctl.Name = "lblHeader6" Or ctl.Name = "lblHeader7" Or ctl.Name = "lblHeader8" Or ctl.Name = "lblHeader9" Or ctl.Name = "lblHeader10" Then
                            ctl.BackColor = Color.Transparent
                            ctl.ForeColor = Color.White
                        ElseIf ctl.Name = "lblHeading" Then
                            ctl.BackColor = Color.Transparent
                            ctl.ForeColor = Color.Black
                            ctl.Size = New Size(85, 18)
                            ctl.Font = New Font("Segoe UI", FontStyle.Regular)
                        Else
                            ctl.BackColor = Color.Transparent
                            ctl.ForeColor = Color.Black  ''lables of a form 
                            ctl.Font = New Font("Segoe UI", 9, FontStyle.Regular)
                            Dim LabelAdapter As New Label
                            Dim old As Padding = LabelAdapter.Padding
                            ctl.Padding = New Padding(old.Left, old.Top, old.Right, old.Bottom)
                        End If
                    ElseIf TypeOf ctl Is ComboBox Or TypeOf ctl Is GroupBox Or TypeOf ctl Is TextBox Or TypeOf ctl Is Label Or TypeOf ctl Is CheckBox Then
                        'ctl.BackColor = Color.Transparent
                        ctl.Font = New Font("Segoe UI", 9)
                        ctl.ForeColor = Color.Black ''in list of combo boxes
                        'ElseIf TypeOf ctl Is Form Then
                        '    ctl.BackColor = Color.Gray
                        'End If
                    End If
                End If
            Next
        End If
    End Sub
    'Public Sub StyleSheet(ByVal Control As Control)
    '    Try
    '        If TypeOf Control Is Windows.Forms.Form Then
    '            Control.BackColor = frmMain.BackColor
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
    Public Function UiGetStockDataTable(ByVal ICode As Integer)
        Dim objdt As New DataTable
        Dim objda As OleDb.OleDbDataAdapter
        Try
            Dim sqlstr As String = "SELECT  ArticleDefView.ArticleId AS Id, ArticleDefView.ArticleDescription AS Item, ArticleDefView.ArticleCode AS Code, " & _
              "ArticleDefView.ArticleSizeName AS Size, ArticleDefView.ArticleColorName AS Combination, ArticleDefView.SalePrice AS Price,  " & _
               "                       ArticleDefView.PurchasePrice, vw_articlestock.Stock  , ArticleDefView.SizeRangeID as [Size ID]FROM ArticleDefView, vw_articlestock  " & _
            "WHERE(ArticleDefView.ArticleId = " & ICode & " And (ArticleDefView.Active = 1))"
            objda = New OleDb.OleDbDataAdapter(sqlstr, Con)
            objda.Fill(objdt)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            objdt = Nothing
        End Try
        Return objdt
    End Function
    Public Function GetStockById(ByVal ItemId As Integer, Optional ByVal LocationId As Integer = 0, Optional ByVal Unit As String = "Loose") As Double
        Dim str As String = ""
        Try
            '' Below else section is added against TASK TFS1490
            If getConfigValueByType("BagStock").ToString = "False" Then
                str = "SP_STOCK " & ItemId & ", " & IIf(getConfigValueByType("CheckCurrentStockByLocation").ToString = "True", LocationId, 0) & ",'" & Unit & "'"
            Else

                str = "SP_BAGSTOCK " & ItemId & ", " & IIf(getConfigValueByType("CheckCurrentStockByLocation").ToString = "True", LocationId, 0) & ",'" & Unit & "'"
            End If
            Dim dt As DataTable = GetDataTable(str)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                Return Convert.ToDouble(dt.Rows(0).Item("Stock").ToString)
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    ''' <summary>
    ''' This Fuction is Added to get Stock By Batch No
    ''' </summary>
    ''' <param name="ItemId"></param>
    ''' <param name="LocationId"></param>
    ''' <param name="Unit"></param>
    ''' <returns></returns>
    ''' <remarks>TFS2739 :Ayesha Rehman : 02-04-2018 : LotWise Rate </remarks>
    Public Function GetStockByBatch(ByVal ItemId As Integer, Optional ByVal LocationId As Integer = 0, Optional ByVal Unit As String = "Loose", Optional ByVal BatchNo As String = "") As Double
        Dim str As String = ""
        Try
            '' Below else section is added against TASK TFS1490
            If getConfigValueByType("BagStock").ToString = "False" Then
                str = "SP_STOCKBYBATCH " & ItemId & ", " & IIf(getConfigValueByType("CheckCurrentStockByLocation").ToString = "True", LocationId, 0) & ",'" & Unit & "','" & BatchNo & "'"
            Else

                str = "SP_BAGSTOCKBYBATCH " & ItemId & ", " & IIf(getConfigValueByType("CheckCurrentStockByLocation").ToString = "True", LocationId, 0) & ",'" & Unit & "','" & BatchNo & "'"
            End If
            Dim dt As DataTable = GetDataTable(str)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                Return Convert.ToDouble(dt.Rows(0).Item("Stock").ToString)
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' This Fuction is Added to check if document is approved at some stage or not??
    ''' </summary>
    ''' <param name="DocumentNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 'rafay update
    Public Function ValidateApprovalProcessInProgress(ByVal DocumentNo As String, Optional ByVal Source As String = "") As Boolean
        Dim str As String = ""
        Try

            str = "SELECT     ApprovalHistory.DocumentNo " _
                  & "FROM   ApprovalHistoryDetail LEFT OUTER JOIN " _
                  & "ApprovalHistory ON ApprovalHistoryDetail.AprovalHistoryId = ApprovalHistory.ApprovalHistoryId " _
                  & " Where ApprovalHistory.Status in ('') and ApprovalHistory.Status in ('Approved') And DocumentNo ='" & DocumentNo & "'" _
                  & " " & IIf(Source <> String.Empty, " And ApprovalHistory.Source ='" & Source & "'", "") & ""
            Dim dt As DataTable = GetDataTable(str)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    ''' <summary>
    ''' This Fuction is Added to check if document after rejection is in Progress??
    ''' </summary>
    ''' <param name="DocumentNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateApprovalProcessIsInProgressAgain(ByVal DocumentNo As String, Optional ByVal Source As String = "") As Boolean
        Dim str As String = ""
        Try

            str = "SELECT     ApprovalHistory.DocumentNo " _
                  & "FROM   ApprovalHistoryDetail LEFT OUTER JOIN " _
                  & "ApprovalHistory ON ApprovalHistoryDetail.AprovalHistoryId = ApprovalHistory.ApprovalHistoryId " _
                  & " Where ApprovalHistory.Status in ('InProgress' , 'Approved') And DocumentNo ='" & DocumentNo & "'" _
                  & " " & IIf(Source <> String.Empty, " And ApprovalHistory.Source ='" & Source & "'", "") & ""
            Dim dt As DataTable = GetDataTable(str)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    ''' <summary>
    ''' This Fuction is Added to check if document has entered in approval Process Or not?
    ''' </summary>
    ''' <param name="DocumentNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateApprovalProcessMapped(ByVal DocumentNo As String, Optional ByVal Source As String = "") As Boolean
        Dim str As String = ""
        Try

            str = "Select * from ApprovalHistory where DocumentNo = '" & DocumentNo & "'" _
                  & " " & IIf(Source <> String.Empty, " And ApprovalHistory.Source ='" & Source & "'", "") & ""
            Dim dt As DataTable = GetDataTable(str)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    ' ''' <summary>
    ' ''' TASK TFS1490
    ' ''' </summary>
    ' ''' <param name="ItemId"></param>
    ' ''' <param name="LocationId"></param>
    ' ''' <param name="Unit"></param>
    ' ''' <returns></returns>
    ' ''' <remarks> This method will be called when Bag Stock Configuration is checked.</remarks>
    'Public Function GetBagStockById(ByVal ItemId As Integer, Optional ByVal LocationId As Integer = 0, Optional ByVal Unit As String = "Loose") As Double
    '    Try

    '        Dim str As String = "SP_BAGSTOCK" & ItemId & ", " & IIf(getConfigValueByType("CheckCurrentStockByLocation").ToString = "True", LocationId, 0) & ",'" & Unit & "'"
    '        Dim dt As DataTable = GetDataTable(str)
    '        dt.AcceptChanges()
    '        If dt.Rows.Count > 0 Then
    '            Return Convert.ToDouble(dt.Rows(0).Item("Stock").ToString)
    '        Else
    '            Return 0
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    Public Function GetStockByIdForStoreIssuence(ByVal ItemId As Integer, Optional ByVal LocationId As Integer = 0, Optional ByVal Unit As String = "Loose", Optional ByVal CostCentreID As Integer = 0) As Double
        Dim str As String = ""
        Try
            ''TASK TFS1490
            If getConfigValueByType("CheckCurrentStockByLocation").ToString = "False" Then
                str = "SP_CheckStockForStoreIssuence " & ItemId & ", " & IIf(getConfigValueByType("CheckCurrentStockByLocation").ToString = "True", LocationId, 0) & ",'" & Unit & "',  " & IIf(CostCentreID > 0, CostCentreID, 0) & ""
            Else
                str = "SP_CheckBagStockForStoreIssuence " & ItemId & ", " & IIf(getConfigValueByType("CheckCurrentStockByLocation").ToString = "True", LocationId, 0) & ",'" & Unit & "',  " & IIf(CostCentreID > 0, CostCentreID, 0) & ""
            End If
            Dim dt As DataTable = GetDataTable(str)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                Return Convert.ToDouble(dt.Rows(0).Item("Stock").ToString)
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetUserPriceAllowedRights(ByVal UserId As Integer) As Boolean
        Try
            Dim str As String
            Dim dt As New DataTable
            Dim adp As OleDb.OleDbDataAdapter
            str = "select IsNull(PriceAllowedRight,0) As PriceAllowedRight From tblUser WHERE User_ID= " & UserId & ""
            adp = New OleDb.OleDbDataAdapter(str, Con)
            adp.Fill(dt)
            dt.AcceptChanges()
            Return dt.Rows(0).Item("PriceAllowedRight")
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetUserPostingRights(ByVal UserId As Integer) As Boolean
        Try
            Dim str As String
            Dim dt As New DataTable
            Dim adp As OleDb.OleDbDataAdapter
            str = "select IsNull(PostingRight,0) As PostingRight From tblUser WHERE User_ID= " & UserId & ""
            adp = New OleDb.OleDbDataAdapter(str, Con)
            adp.Fill(dt)
            dt.AcceptChanges()
            Return dt.Rows(0).Item("PostingRight")
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetUserInspectionRights(ByVal UserId As Integer) As Boolean
        Try
            Dim str As String
            Dim dt As New DataTable
            Dim adp As OleDb.OleDbDataAdapter
            str = "Select IsNull(InspectionRight,0) as InspectionRight From tblUser WHERE User_Id = " & UserId & ""
            adp = New OleDb.OleDbDataAdapter(str, Con)
            adp.Fill(dt)
            dt.AcceptChanges()
            Return dt.Rows(0).Item("InspectionRight")
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub GetSecurityByPostingUser(ByVal flg As Boolean, ByVal btnUpdate As Windows.Forms.ToolStripButton, ByVal btnDelete As Windows.Forms.ToolStripButton)
        Try
            If flg = False Then
                If btnUpdate.Text = "&Update" Then
                    btnUpdate.Enabled = False
                Else
                    btnUpdate.Enabled = True
                    btnDelete.Enabled = False
                End If
                If btnDelete.Text = "&Delete" Then
                    btnDelete.Enabled = False
                Else
                    btnDelete.Enabled = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetSecurityByPostingUser(ByVal flg As Boolean, ByVal btnUpdate As Windows.Forms.ToolStripSplitButton, ByVal btnDelete As Windows.Forms.ToolStripButton)
        Try
            If flg = False Then
                If btnUpdate.Text = "&Update" Then
                    btnUpdate.Enabled = False
                Else
                    btnUpdate.Enabled = True
                    btnDelete.Enabled = False
                End If
                If btnDelete.Text = "&Delete" Then
                    btnDelete.Enabled = False
                Else
                    btnDelete.Enabled = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub CompanyHeader()
        Try
            Dim str As String = String.Empty
            str = "select Config_Value From ConfigValuesTable WHERE config_id=29"
            Dim dt As DataTable = GetDataTable(str)
            CompanyNameHeader = dt.Rows(0).Item("Config_Value").ToString
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub CompanyAddrssHeader()
        Try
            Dim str As String = String.Empty
            str = "select Config_Value From ConfigValuesTable WHERE config_id=30"
            Dim dt As DataTable = GetDataTable(str)
            CompanyAddressHeader = dt.Rows(0).Item("Config_Value").ToString
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function GetFinancialYear() As DataTable
        Try
            Dim str As String = "Select * From tblDefFinancialYear WHERE Status=1"
            Dim dt As DataTable = GetDataTable(str)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetRestrictedItemFlg(ByVal LocationId As Integer) As Boolean
        Try
            Dim str As String = String.Empty
            str = "Select RestrictedItems From tblDefLocation WHERE Location_Id=" & LocationId & " AND RestrictedItems IS Not Null"
            Dim dt As DataTable = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                Return Convert.ToBoolean(dt.Rows(0).Item(0).ToString)
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetToEmail(ByVal Account As String) As String
        Try
            Dim str As String = "SP_EmailAccount '" & Account & "'"
            Dim dt As DataTable = GetDataTable(str)
            If Not dt Is Nothing Then
                If Not dt.Rows.Count = 0 Then
                    Return dt.Rows(0).Item("Email").ToString
                Else
                    Return String.Empty
                End If
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAdministrator(ByVal UserId) As String
        Try
            Dim str As String = "SELECT dbo.tblUserGroup.GroupType, dbo.tblUser.User_ID " _
                     & " FROM dbo.tblUser INNER JOIN " _
                     & " dbo.tblUserGroup ON ISNULL(dbo.tblUser.GroupId, Ident_Current('tblUserGoupId')) = dbo.tblUserGroup.GroupId WHERE dbo.tblUser.User_ID=" & UserId
            Dim dt As DataTable = GetDataTable(str)
            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).ItemArray(0).ToString
                Else
                    Return "Error"
                End If
            Else
                Return "Error"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function IsCostCentreReshuffled(ByVal CostCentreId As Integer) As Boolean
        Try
            Dim str As String = "SELECT CostCentreFrom " _
                     & " FROM dbo.CostCentreReshuffle Where CostCentreFrom =" & CostCentreId & ""
            'Dim str As String = "SELECT Distinct IsNull(CostCentreReshuffleId, 0) As CostCentreReshuffleId  " _
            '        & " FROM dbo.tblVoucherDetail Where CostCenterID =" & CostCentreId & ""

            Dim dt As DataTable = GetDataTable(str)
            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0).Item(0) > 0 Then
                        Return True
                    Else
                        Return False
                    End If

                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function IsVoucherCostCentreReshuffled(ByVal VoucherDetailId As Integer) As Boolean
        Try
            'Dim str As String = "SELECT CostCentreFrom " _
            '         & " FROM dbo.CostCentreReshuffle Where CostCentreTo =" & CostCentreId & ""
            'Dim str As String = "SELECT Distinct IsNull(CostCentreReshuffleId, 0) As CostCentreReshuffleId  " _
            '        & " FROM dbo.tblVoucherDetail Where CostCenterID =" & CostCentreId & ""
            Dim str As String = "SELECT Distinct IsNull(CostCentreReshuffleId, 0) As CostCentreReshuffleId  " _
                   & " FROM dbo.tblVoucherDetail Where voucher_detail_id =" & VoucherDetailId & ""

            Dim dt As DataTable = GetDataTable(str)
            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0).Item(0) > 0 Then
                        Return True
                    Else
                        Return False
                    End If

                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub LoadPicture(ByVal ObjDataRow As DataRow, ByVal strImageField As String, ByVal FilePath As String)
        Try
            If IO.File.Exists(FilePath) Then
                If FilePath.Length > 0 Then

                    Dim objImage As Image = Image.FromFile(FilePath)
                    objImage = SizeImage(objImage, 400, 600)
                    Dim ms As New MemoryStream
                    objImage.Save(ms, Imaging.ImageFormat.Png)
                    Dim oBytes() As Byte = ms.ToArray
                    objImage.Dispose()
                    ms.Close()
                    'Dim fs As IO.FileStream = New IO.FileStream(FilePath, IO.FileMode.Open, IO.FileAccess.Read)
                    'Dim OImage As Byte() = New Byte(fs.Length) {}
                    'fs.Read(OImage, 0, fs.Length)
                    ObjDataRow(strImageField) = oBytes
                    'fs.Flush()
                    'fs.Dispose()
                    'fs.Close()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Public Function Encrypt(ByVal strText As String) As String
        Dim strEncrKey As String = "simpleaccounts"
        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
        Try
            Dim bykey() As Byte = System.Text.Encoding.UTF8.GetBytes(Microsoft.VisualBasic.Left(strEncrKey, 8))
            Dim InputByteArray() As Byte = System.Text.Encoding.UTF8.GetBytes(strText)
            Dim des As New DESCryptoServiceProvider
            Dim ms As New IO.MemoryStream
            Dim cs As New CryptoStream(ms, des.CreateEncryptor(bykey, IV), CryptoStreamMode.Write)
            cs.Write(InputByteArray, 0, InputByteArray.Length)
            cs.FlushFinalBlock()
            Dim strEncrypt As String = Convert.ToBase64String(ms.ToArray())
            ms.Close()
            cs.Close()
            des.Clear()
            Return strEncrypt
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function
    Public Function Decrypt(ByVal strText As String) As String
        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
        Dim sDecrKey As String = "simpleaccounts"
        Dim inputByteArray(strText.Length) As Byte
        Try
            Dim byKey() As Byte = System.Text.Encoding.UTF8.GetBytes(Microsoft.VisualBasic.Left(sDecrKey, 8))
            Dim des As New DESCryptoServiceProvider
            inputByteArray = Convert.FromBase64String(strText)
            Dim ms As New IO.MemoryStream
            Dim cs As New CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8
            Dim strDecrypt = encoding.GetString(ms.ToArray())
            ms.Close()
            cs.Close()
            des.Clear()
            Return strDecrypt
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Sub GetLicenseDetails()
        Try
            Dim LicenseKey As String = Decrypt(getConfigValueByType("LicenseKey").ToString)

            If LicenseKey.Contains("|") Then

                Dim str As String() = LicenseKey.Split("|")
                For Each key As String In str
                    If key.Contains("=") Then
                        Dim strKey As String() = key.Split("=")

                        Select Case strKey(0).ToString
                            'Case "key"
                            '    LicenseKey = strKey(1).ToString

                            Case "expiry"
                                LicenseExpiry = CType(strKey(1).ToString, DateTime).ToString("dd-MMM-yyyy")
                                'Case "expiry_type"
                                '    LicenseExpiryType = strKey(1).ToString
                                'Case "version"
                                '    LicenseVersion = strKey(1).ToString
                                'Case "modules"
                                '    LicenseModuleList = strKey(1).ToString
                                'Case "systemid"
                                '    LicenseSystemId = strKey(1).ToString
                                '    If LicenseSystemId = "INVALID" Then
                                '        LicenseSystemId = ""
                                '    End If
                                'Case "systemid1"
                                '    LicenseSystemId1 = strKey(1).ToString
                                '    If LicenseSystemId1 = "INVALID" Then
                                '        LicenseSystemId1 = ""
                                '    End If
                                'Case "systemid2"
                                '    LicenseSystemId2 = strKey(1).ToString
                                '    If LicenseSystemId2 = "INVALID" Then
                                '        LicenseSystemId2 = ""
                                '    End If
                                'Case "terminals"
                                '    LicenseTerminals = Val(strKey(1).ToString)
                            Case "dbname"
                                'LicenseDBName = Val(strKey(1).ToString)
                                'Syed Irfan Ahmad, Task 2411. Above statement is commented and below one is added
                                'on 15 Feb 2018. 
                                '***** Suggestion: WHOEVER USED Val() IN THE ABOVE COMMENTED STATEMENT MUST BE FINED 
                                '***************** AT LEAST 1 MILLION RUPEES. WITH THIS AMOUNT, THE WHOLE COMPANY SHOULD
                                '***************** ENJOY A WEEK'S TRIP TO NORTHERN AREAS
                                LicenseDBName = strKey(1)
                            Case "users"
                                LicenseUsers = strKey(1)
                        End Select


                    End If
                Next

            Else

                Dim VDate As DateTime

                Dim dt As DataTable = GetDataTable(" select min(voucher_date) from tblVoucher ")

                If dt.Rows.Count > 0 Then
                    VDate = dt.Rows(0).Item(0).ToString
                Else
                    VDate = Date.Now()
                End If

                If VDate <= CType("30-Nov-2016 23:59:59", DateTime) Then

                    VDate = "31-Dec-2016 23:59:59"

                Else
                    VDate = VDate.AddMonths(1)
                End If

                LicenseExpiry = VDate.ToString("dd-MMM-yyyy")
                LicenseKey = String.Empty
                LicenseSystemId = String.Empty
                LicenseModuleList = String.Empty
                LicenseVersion = String.Empty
                LicenseExpiryType = String.Empty
                LicenseUsers = String.Empty
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function EncryptLicense(ByVal strText As String) As String
        Dim strEncrKey As String = "AgriusERPSystem"
        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
        Try
            Dim bykey() As Byte = System.Text.Encoding.UTF8.GetBytes(Microsoft.VisualBasic.Left(strEncrKey, 8))
            Dim InputByteArray() As Byte = System.Text.Encoding.UTF8.GetBytes(strText)
            Dim des As New DESCryptoServiceProvider
            Dim ms As New IO.MemoryStream
            Dim cs As New CryptoStream(ms, des.CreateEncryptor(bykey, IV), CryptoStreamMode.Write)
            cs.Write(InputByteArray, 0, InputByteArray.Length)
            cs.FlushFinalBlock()
            Dim strEncrypt As String = Convert.ToBase64String(ms.ToArray())
            ms.Close()
            cs.Close()
            des.Clear()
            Return strEncrypt
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function
    Public Function DecryptLicense(ByVal strText As String) As String
        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
        Dim sDecrKey As String = "AgriusERPSystem"
        Dim inputByteArray(strText.Length) As Byte
        Try
            Dim byKey() As Byte = System.Text.Encoding.UTF8.GetBytes(Microsoft.VisualBasic.Left(sDecrKey, 8))
            Dim des As New DESCryptoServiceProvider
            inputByteArray = Convert.FromBase64String(strText)
            Dim ms As New IO.MemoryStream
            Dim cs As New CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8
            Dim strDecrypt = encoding.GetString(ms.ToArray())
            ms.Close()
            cs.Close()
            des.Clear()
            Return strDecrypt
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Sub LicenseUpdates()
        Try
            Dim LicenseKey As String = Decrypt(getConfigValueByType("LicenseKey").ToString)

            If LicenseKey.Contains("|") Then

                Dim str As String() = LicenseKey.Split("|")
                For Each key As String In str
                    If key.Contains("=") Then
                        Dim strKey As String() = key.Split("=")

                        Select Case strKey(0).ToString
                            Case "key"
                                LicenseKey = strKey(1).ToString

                            Case "expiry"
                                LicenseExpiry = CType(strKey(1).ToString, DateTime).AddYears(8).ToString("dd-MMM-yyyy")
                            Case "expiry_type"
                                LicenseExpiryType = strKey(1).ToString
                            Case "version"
                                LicenseVersion = strKey(1).ToString
                            Case "modules"
                                LicenseModuleList = strKey(1).ToString
                            Case "systemid"
                                LicenseSystemId = strKey(1).ToString
                                If LicenseSystemId = "INVALID" Then
                                    LicenseSystemId = ""
                                End If
                            Case "systemid1"
                                LicenseSystemId1 = strKey(1).ToString
                                If LicenseSystemId1 = "INVALID" Then
                                    LicenseSystemId1 = ""
                                End If
                            Case "systemid2"
                                LicenseSystemId2 = strKey(1).ToString
                                If LicenseSystemId2 = "INVALID" Then
                                    LicenseSystemId2 = ""
                                End If
                            Case "terminals"
                                LicenseTerminals = Val(strKey(1).ToString)
                            Case "dbname"
                                'LicenseDBName = Val(strKey(1).ToString)
                                'Syed Irfan Ahmad, Task 2411. Above statement is commented and below one is added
                                'on 15 Feb 2018. 
                                '***** Suggestion: WHOEVER USED Val() IN THE ABOVE COMMENTED STATEMENT MUST BE FINED 
                                '***************** AT LEAST 1 MILLION RUPEES. WITH THIS AMOUNT, THE WHOLE COMPANY SHOULD
                                '***************** ENJOY A WEEK'S TRIP TO NORTHERN AREAS
                                LicenseDBName = strKey(1)

                        End Select


                    End If
                Next

            Else

                Dim VDate As DateTime

                Dim dt As DataTable = GetDataTable(" select min(voucher_date) from tblVoucher ")

                If dt.Rows.Count > 0 Then
                    VDate = dt.Rows(0).Item(0).ToString
                Else
                    VDate = Date.Now()
                End If

                If VDate <= CType("30-Nov-2016 23:59:59", DateTime) Then

                    VDate = "31-Dec-2016 23:59:59"

                Else
                    VDate = VDate.AddMonths(1)
                End If

                LicenseExpiry = VDate.ToString("dd-MMM-yyyy")
                LicenseKey = String.Empty
                LicenseSystemId = String.Empty
                LicenseModuleList = String.Empty
                LicenseVersion = String.Empty
                LicenseExpiryType = String.Empty

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub TabControl_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs)
        Dim g As Graphics = e.Graphics
        Dim tp As TabPage = _TabControl.TabPages(e.Index)
        Dim br As Brush
        Dim sf As New StringFormat

        Dim r As New RectangleF(e.Bounds.X, e.Bounds.Y + 2, e.Bounds.Width, e.Bounds.Height - 2)

        sf.Alignment = StringAlignment.Center

        Dim strTitle As String = tp.Text

        'If the current index is the Selected Index, change the color 
        If _TabControl.SelectedIndex = e.Index Then

            'this is the background color of the tabpage header
            br = New SolidBrush(Color.LightSteelBlue) ' chnge to your choice
            g.FillRectangle(br, e.Bounds)

            'this is the foreground color of the text in the tab header
            br = New SolidBrush(Color.Black) ' change to your choice
            g.DrawString(strTitle, _TabControl.Font, br, r, sf)

        Else

            'these are the colors for the unselected tab pages 
            br = New SolidBrush(Color.LightBlue) ' Change this to your preference
            g.FillRectangle(br, e.Bounds)
            br = New SolidBrush(Color.Black)
            g.DrawString(strTitle, _TabControl.Font, br, r, sf)

        End If
    End Sub
    Public Function GetPackData(ByVal ArticleId As Integer) As DataTable
        Try
            'Before Against Task:2367
            'Dim strSQL As String = "Select ArticlePackId, PackName, Isnull(PackQty,0) as PackQty From ArticleDefPackTable WHERE ArticleMasterId In (Select MasterId From ArticleDefView WHERE ArticleId=" & ArticleId & ")"
            'Task:2367 Change IsNull Function At PackQty
            'Before against task:2618
            'Dim strSQL As String = "Select ArticlePackId, PackName, Isnull(PackQty,1) as PackQty From ArticleDefPackTable WHERE ArticleMasterId In (Select MasterId From ArticleDefView WHERE ArticleId=" & ArticleId & ")"
            'End Task:2367
            ''9-May-2014 TASK:2618 Imran Ali Sort Order Unit List On Stock Adjustment  
            ''TFS1964 : Getting PackRate from ArticleDefPackTable
            Dim strSQL As String = "Select ArticlePackId, PackName, Isnull(PackQty,1) as PackQty , Isnull(PackRate,1) as PackRate From ArticleDefPackTable WHERE ArticleMasterId In (Select MasterId From ArticleDefView WHERE ArticleId=" & ArticleId & ") ORDER BY PackName ASC "
            'End Task:2618

            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            If Not dt.Rows.Count > 0 Then
                Dim dr As DataRow
                dr = dt.NewRow
                dr(0) = 0
                dr(1) = "Loose"
                dr(2) = 1
                dt.Rows.InsertAt(dr, 0)
                dr = dt.NewRow
                dr(0) = 1
                dr(1) = "Pack"
                dr(2) = 1
                dt.Rows.InsertAt(dr, 1)
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''Task:2384 Added Function Check Numeric Value
    Public Function CheckNumericValue(ByVal input As String, ByVal sender As Object) As Boolean
        Try
            ''14-Mar-2014 TASK:2491  Imran Ali  Numeric Validation 
            If input.Length > 0 Then
                If Not IsNumeric(input) Then
                    If TypeOf sender Is TextBox Then
                        Dim txtbox As TextBox = CType(sender, TextBox)
                        txtbox.Focus()
                    ElseIf TypeOf sender Is RichTextBox Then
                        Dim txtbox As RichTextBox = CType(sender, RichTextBox)
                        txtbox.Focus()
                    End If
                    Return False
                Else
                    Return True
                End If
            Else
                Return True
            End If
            'End Task:2491
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'End Task:2384
    'Task No 2537 Function to Load The Curent Balance Value Against the ComboBox Selection for Payment Voucher New and Recepiet Voucher New
    'Select coa_detail_id , sum(debit_amount-credit_amount)as currentbalance  from tblVoucherDetail where coa_detail_id=54  group by coa_detail_id
    Public Function GetCurrentBalance(ByVal AccountID As Integer, Optional trans As OleDbTransaction = Nothing) As Double
        Try
            Dim Balance As Double = 0

            Dim str As String
            'Before against task:2844 
            'str = "Select coa_detail_id , sum(debit_amount-credit_amount)as currentbalance  from tblVoucherDetail where coa_detail_id=" & AccountID & "  group by coa_detail_id"
            'Task:2844 GET Current Balance With Posted Vouchers
            str = "Select tblVoucherDetail.coa_detail_id , sum(debit_amount-credit_amount)as currentbalance  from tblVoucherDetail INNER JOIN tblVoucher On tblVoucher.Voucher_Id = tblVoucherDetail.Voucher_Id where tblvoucherdetail.coa_detail_id=" & AccountID & "  and tblVoucher.Post=1 group by tblVoucherDetail.coa_detail_id"
            'End Task:2844
            Dim dt As DataTable = GetDataTable(str, trans)
            If Not dt Is Nothing Then
                'Task:M35 Validation Chnaged
                'If dt.Rows.Count < 0 Then 
                If dt.Rows.Count <= 0 Then
                    'End Task:M35
                    Balance = 0
                Else
                    Balance = CDbl(Val(dt.Rows(0).Item(1).ToString))
                End If
            End If

            Return Balance
        Catch ex As Exception
            Throw ex
        Finally
        End Try
    End Function
    'Task No 2543 Append One Function For Validation Of Numeric Values 
    Public Sub NumValidation(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            If e.KeyChar = "."c Then
                e.Handled = (CType(sender, TextBox).Text.IndexOf("."c) <> -1)
            ElseIf e.KeyChar = "-"c Then
                e.Handled = (CType(sender, TextBox).Text.IndexOf("-"c) <> -1)
            ElseIf e.KeyChar <> ControlChars.Back Then
                e.Handled = ("0123456789".IndexOf(e.KeyChar) = -1)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Task No 1616 Append One Function For Validation Of Numeric Values 
    Public Sub cmbNumValidation(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            If e.KeyChar = "."c Then
                e.Handled = (CType(sender, ComboBox).Text.IndexOf("."c) <> -1)
            ElseIf e.KeyChar = "-"c Then
                e.Handled = (CType(sender, ComboBox).Text.IndexOf("-"c) <> -1)
            ElseIf e.KeyChar <> ControlChars.Back Then
                e.Handled = ("0123456789".IndexOf(e.KeyChar) = -1)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Task:2577 Added Method Send Branded SMS
    Public Sub SendBrandedSMS(ByVal objPhoneNo As String, ByVal objMsgBody As String)
        Try
            Dim strLang As String = String.Empty
            strLang = getConfigValueByType("SMSLanguage").ToString.Replace("Error", "English").Replace("''", "English")
            SMSMask = Decrypt(getConfigValueByType("BrandedSMSMask").ToString)
            Dim BrandedSMSUser As String = getConfigValueByType("BrandedSMSUser").ToString
            Dim BrandedSMSPassword As String = Decrypt(getConfigValueByType("BrandedSMSPassword").ToString)
            Dim strPhoneNo As String = String.Empty 'Manipulate Phone No.
            strPhoneNo = "92" & Microsoft.VisualBasic.Right(objPhoneNo.Replace("-", "").Replace(".", "").Replace("+", "").Replace(" ", "").Replace("(", "").Replace(")", ""), 10)
            Dim strPost As String = "id=" & BrandedSMSUser & "&pass=" & BrandedSMSPassword & "&mask=" & IIf(SMSMask.Length > 0, SMSMask, "SMS4") & "&to=" & objPhoneNo + "&lang=" & strLang & "&msg=" + objMsgBody + "&type=xml"
            Dim WebReq As WebRequest = WebRequest.Create("http://outreach.pk/api/sendsms.php/sendsms/url?")
            WebReq.Method = "POST"
            WebReq.ContentLength = System.Text.Encoding.UTF8.GetByteCount(strPost)
            WebReq.ContentType = "application/x-www-form-urlencoded"

            Dim MyStreamWriter As New IO.StreamWriter(WebReq.GetRequestStream)
            MyStreamWriter.Write(strPost)
            MyStreamWriter.Close()
            Dim MyRespStream As New IO.StreamReader(WebReq.GetResponse.GetResponseStream)
            Dim str As String = MyRespStream.ReadToEnd
            MyRespStream.Close()

            Dim Doc As New System.Xml.XmlDocument
            Doc.LoadXml(str)

            Dim objNodeList As System.Xml.XmlNodeList = Doc.SelectNodes("/corpsms")
            Dim strCode As String = objNodeList(0).SelectSingleNode("code").InnerText
            Dim strType As String = objNodeList(0).SelectSingleNode("type").InnerText
            Dim strResponse As String = objNodeList(0).SelectSingleNode("response").InnerText
            Dim strTransactionID As String = objNodeList(0).SelectSingleNode("transactionID").InnerText


        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''13-Sep-2014 Task:2845 Imran Ali Send SMS on Invoice Based Payment/Receipt
    Public Sub SendBrandedSMS(ByVal objSMSLog As SBModel.SMSLogBE)
        Try

            If objSMSLog.PhoneNo.Length = 0 Then Exit Sub
            Dim strLang As String = String.Empty
            strLang = getConfigValueByType("SMSLanguage").ToString.Replace("Error", "English").Replace("''", "English")
            SMSMask = Decrypt(getConfigValueByType("BrandedSMSMask").ToString)
            Dim BrandedSMSUser As String = getConfigValueByType("BrandedSMSUser").ToString
            Dim BrandedSMSPassword As String = Decrypt(getConfigValueByType("BrandedSMSPassword").ToString)
            Dim strPhoneNo As String = String.Empty 'Manipulate Phone No.
            strPhoneNo = objSMSLog.PhoneNo.Replace("-", "").Replace(".", "").Replace("+", "").Replace(" ", "").Replace("(", "").Replace(")", "")
            If strPhoneNo.Length <= 11 Then
                strPhoneNo = "92" & Microsoft.VisualBasic.Right(strPhoneNo, 10)
            Else
                strPhoneNo = strPhoneNo
            End If
            objSMSLog.PhoneNo = strPhoneNo
            Dim strPost As String = "id=" & BrandedSMSUser & "&pass=" & BrandedSMSPassword & "&mask=" & IIf(SMSMask.Length > 0, SMSMask, "SMS4") & "&to=" & objSMSLog.PhoneNo + "&lang=" & strLang & "&msg=" + objSMSLog.SMSBody + "&type=xml"
            Dim WebReq As WebRequest = WebRequest.Create("http://outreach.pk/api/sendsms.php/sendsms/url?")
            WebReq.Method = "POST"
            WebReq.ContentLength = System.Text.Encoding.UTF8.GetByteCount(strPost)
            WebReq.ContentType = "application/x-www-form-urlencoded"

            Dim MyStreamWriter As New IO.StreamWriter(WebReq.GetRequestStream)
            MyStreamWriter.Write(strPost)
            MyStreamWriter.Close()
            Dim MyRespStream As New IO.StreamReader(WebReq.GetResponse.GetResponseStream)
            Dim str As String = MyRespStream.ReadToEnd
            MyRespStream.Close()
            Dim Reader As StringReader = New StringReader(str)
            Dim XMLDataSet As New DataSet
            XMLDataSet.ReadXml(Reader)
            objSMSLog.TransactionID = XMLDataSet.Tables(0).Rows(0)(3)
            System.Threading.Thread.Sleep(500)


            Dim strQuery As String = "id=" & BrandedSMSUser & "&pass=" & BrandedSMSPassword & "&transaction=" & objSMSLog.TransactionID & ""
            Dim oWebReq As WebRequest = WebRequest.Create("http://outreach.pk/api/sendsms.php/delivery/status?")
            oWebReq.Method = "POST"
            oWebReq.ContentType = "application/x-www-form-urlencoded"
            Dim oMyStreamWriter As New IO.StreamWriter(oWebReq.GetRequestStream)
            oMyStreamWriter.Write(strQuery)
            oMyStreamWriter.Close()
            Dim oMyRespStream As New IO.StreamReader(oWebReq.GetResponse.GetResponseStream)
            Dim Result As String = oMyRespStream.ReadToEnd
            oMyRespStream.Close()
            System.Threading.Thread.Sleep(500)


            Dim objReader As StringReader = New StringReader(Result)
            Dim objXMLDataSet As New DataSet
            objXMLDataSet.ReadXml(objReader)

            If objXMLDataSet.Tables.Count > 0 Then
                If objXMLDataSet.Tables(1).Rows(0)(2).ToString.ToUpper = "ERROR" Then
                    Call SBDal.SMSTemplateDAL.ErrorLog(Date.Now, XMLDataSet.Tables(0).Rows(0)(2), objSMSLog.TransactionID, objSMSLog.SMSLogID, XMLDataSet.Tables(0).Rows(0)(0))
                    Call SBDal.SMSTemplateDAL.UpdateErrorLog(Date.Now, XMLDataSet.Tables(0).Rows(0)(1), objSMSLog.TransactionID, objSMSLog.SMSLogID)
                Else
                    Dim strDate() As String = objXMLDataSet.Tables(1).Rows(0)(1).ToString.Split("-")
                    Dim myTransactionDate As DateTime
                    If strDate.Length = 3 Then
                        myTransactionDate = CDate(strDate(2).Split(" ")(0) & "-" & strDate(1) & "-" & strDate(0) & " " & strDate(2).Split(" ")(1))
                    Else
                        myTransactionDate = Date.Now
                    End If
                    objSMSLog.DeliveryDate = myTransactionDate
                    objSMSLog.DeliveryStatus = objXMLDataSet.Tables(1).Rows(0)(2)
                    objSMSLog.SentDate = myTransactionDate
                    objSMSLog.SentStatus = objXMLDataSet.Tables(1).Rows(0)(2)
                    objSMSLog.SentByUserID = LoginUserId
                    Call SBDal.SMSTemplateDAL.UpdateTransaction(objSMSLog)
                End If
            Else
                If XMLDataSet.Tables(0).Rows(0)(1).ToString.ToUpper = "ERROR" Then
                    Call SBDal.SMSTemplateDAL.ErrorLog(Date.Now, XMLDataSet.Tables(0).Rows(0)(2), objSMSLog.TransactionID, objSMSLog.SMSLogID, XMLDataSet.Tables(0).Rows(0)(0))
                End If
                Call SBDal.SMSTemplateDAL.UpdateErrorLog(Date.Now, XMLDataSet.Tables(0).Rows(0)(1), objSMSLog.TransactionID, objSMSLog.SMSLogID)
            End If

        Catch ex As Exception
            'Throw ex
        End Try
    End Sub
    'End Task:2845

    Public Sub SetFillBackgroundColor(ByVal Ctrl As Control)
        Try
            If TypeOf Ctrl Is TextBox Then
                Ctrl.BackColor = Color.Ivory
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub SetDefaultFillBackgroundColor(ByVal Ctrl As Control)
        Try
            If TypeOf Ctrl Is TextBox Then
                Ctrl.BackColor = Color.White
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub DataSetShowReport(ByVal objReportName As String, ByVal objDataSet As DataSet, Optional ByVal ShowReportInContainer As Boolean = False, Optional ByVal strReportPath As String = "")
        Try
            Dim frmCrReport As New frmCrReportViewer
            frmCrReport.ReportName = objReportName
            frmCrReport.objDataSet = objDataSet
            frmCrReport.ReportPath = strReportPath
            If ShowReportInContainer = False Then
                frmCrReport.Show()
                frmCrReport.BringToFront()
            Else
                ReportViewerForContainer = frmCrReport
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function SizeImage(ByVal img As Image, ByVal width As Integer, ByVal height As Integer) As Image
        Dim newBit As New Bitmap(width, height) 'new blank bitmap
        Dim g As Graphics = Graphics.FromImage(newBit)
        'change interpolation for reduction quality
        g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        g.DrawImage(img, 0, 0, width, height)
        Return newBit
    End Function
    Private Function SizeBitmap(ByVal img As Bitmap, ByVal width As Integer, ByVal height As Integer) As Bitmap
        Dim newBit As New Bitmap(width, height) 'new blank bitmap
        Dim g As Graphics = Graphics.FromImage(newBit)
        'change interpolation for reduction quality
        g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        g.DrawImage(img, 0, 0, width, height)
        Return newBit
    End Function
    Public Sub SetBgColorForTextBox(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If TypeOf sender Is TextBox Then
                Dim txt As TextBox = CType(sender, TextBox)
                If txt.Text.Length > 0 Then
                    txt.BackColor = Color.Ivory
                Else
                    txt.BackColor = Color.White
                End If
            ElseIf TypeOf sender Is ComboBox Then
                Dim cmb As ComboBox = CType(sender, ComboBox)
                If cmb.Text.Length > 0 Then
                    cmb.BackColor = Color.Ivory
                Else
                    cmb.BackColor = Color.White
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub UploadAutoAttendance(Optional ByVal Condition As String = "")
        Dim cmd As New SqlClient.SqlCommand
        Dim objCon As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim trans As SqlClient.SqlTransaction = objCon.BeginTransaction
        Dim dt As New DataTable
        Try


            cmd.Connection = objCon
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 120

            Dim strSQL As String = String.Empty

            ' strSQL = "INSERT INTO tblAttendanceDetail(EmpID,AttendanceDate,AttendanceType,AttendanceTime,AttendanceStatus,ShiftId, Auto,Flexibility_In_Time,Flexibility_Out_Time,Sch_In_Time,Sch_Out_Time) "
            ' Dim strQuery As String = " SELECT EMP.Employee_Id, CONVERT(DATETIME,CONVERT(VARCHAR,C.CHECKTIME,102),102) AS AttendanceDate, CASE WHEN C.CHECKTYPE='I' THEN 'In' ELSE 'Out' End AttendanceType, C.CHECKTIME AS AttendanceTime, 'Present' AS AttendanceStatus, S.ShiftId, 1 AS Auto, CONVERT(DATETIME,Convert(varchar,C.CHECKTIME,102) + ' ' + S.FlexInTime,102) AS FLEXINTIME, " _
            ' & " CONVERT(DATETIME,Convert(varchar,C.CHECKTIME,102) + ' ' + S.FlexOutTime,102) AS FLEXOUTTIME, CONVERT(DATETIME,Convert(varchar,C.CHECKTIME,102) + ' ' + S.ShiftStartTime,102) AS SHIFTSTARTTIME, CONVERT(DATETIME,Convert(varchar,C.CHECKTIME,102) + ' ' + S.ShiftEndTime,102) AS SHIFTENDTIME "
            ' Dim strSQL2 As String = "  FROM dbo.ShiftScheduleTable AS SST INNER JOIN  dbo.ShiftTable AS S ON SST.ShiftId = S.ShiftId INNER JOIN " _
            ' & " dbo.ShiftGroupTable AS SG ON SST.ShiftGroupId = SG.ShiftGroupId RIGHT OUTER JOIN dbo.tblDefEmployee AS EMP LEFT OUTER JOIN  dbo.USERINFO AS U INNER JOIN " _
            ' & " dbo.CheckInOut AS C ON U.USERID = C.USERID ON EMP.Employee_ID = U.TAGID ON SG.ShiftGroupId = EMP.ShiftGroupId WHERE C.CHECKTIME NOT IN(SELECT ATTENDANCETIME FROM tblAttendanceDetail) AND CONVERT(DATETIME,CONVERT(VARCHAR,C.CHECKTIME,102),102) is not null"
            ' Dim strSQL1 As String = ", Emp.Employee_Name, Emp.Employee_Code,Emp.Mobile,Emp.Father_Name,SG.ShiftGroupName, Dept.EmployeeDeptName as Department, Desig.EmployeeDesignationName as Designation FROM dbo.ShiftScheduleTable AS SST INNER JOIN  dbo.ShiftTable AS S ON SST.ShiftId = S.ShiftId INNER JOIN " _
            ' & " dbo.ShiftGroupTable AS SG ON SST.ShiftGroupId = SG.ShiftGroupId RIGHT OUTER JOIN dbo.tblDefEmployee AS EMP LEFT OUTER JOIN  dbo.USERINFO AS U INNER JOIN " _
            '& " dbo.CheckInOut AS C ON U.USERID = C.USERID ON EMP.Employee_ID = U.TAGID ON SG.ShiftGroupId = EMP.ShiftGroupId Left Outer Join EmployeeDeptDefTable Dept on Dept.EmployeeDeptId = Emp.Dept_Id LEFT OUTER JOIN EmployeeDesignationDefTable Desig on Desig.EmployeeDesignationId = Emp.Desig_Id  WHERE C.CHECKTIME NOT IN(SELECT ATTENDANCETIME FROM tblAttendanceDetail) AND CONVERT(DATETIME,CONVERT(VARCHAR,C.CHECKTIME,102),102) is not null"

            ' dt = UtilityDAL.GetDataTable(CStr(strQuery & strSQL1), trans)
            ' dt.AcceptChanges()
            ' If dt.Rows.Count = 0 Then
            '     trans.Commit()
            '     objCon.Close()
            '     Exit Sub
            ' End If


            ' cmd.CommandText = strSQL.ToString & strQuery.ToString & strSQL2
            ' cmd.ExecuteNonQuery()
            ' trans.Commit()

            ' Dim strAdminMob As String = String.Empty
            'If GetSMSConfig("Employee Attendance").Enable = True Then
            '    If GetSMSConfig("Employee Attendance").EnabledAdmin = True Then
            '        strAdminMob = strAdminMobileNo
            '    Else
            '        strAdminMob = String.Empty
            '    End If
            '    If dt IsNot Nothing Then
            '        For Each r As DataRow In dt.Rows
            '            Dim strPhones() As String = {}
            '            Dim strPhon As String = String.Empty
            '            strPhones = CStr(r.Item("Mobile") & ";" & strAdminMob).ToString.Replace("|", "").Replace(",", "").Replace(".", "").Split(";")
            '            If strPhones.Length > 0 Then
            '                For Each strPhone As String In strPhones
            '                    If strPhone.Length <= 11 Then
            '                        strPhon = "92" & Microsoft.VisualBasic.Right(strPhone, 10).ToString
            '                    Else
            '                        strPhon = strPhone
            '                    End If
            '                    strSQL = String.Empty
            '                    If Not IsDBNull(r.Item("AttendanceDate")) Then
            '                        If strPhon.Length > 10 Then
            '                            strSQL = "INSERT INTO tblSMSLog(SMSLogDate, SMSBody,SMSType,PhoneNo,CreatedByUserID) " _
            '                            & " VALUES(Convert(DateTime,'" & CDate(r.Item("AttendanceDate")).ToString("yyyy-M-d hh:mm:ss tt") & "', 102),'" & CStr(r.Item("Employee_Name").ToString.Replace("'", "''") & " is " & IIf(r.Item("AttendanceType").ToString = "In", "arrived", "departure") & " at " & CDate(r.Item("AttendanceTime")) & " on " & CDate(r.Item("AttendanceTime")).ToString("dddd") & " Automated By:www.SIRIUS.net").Replace("'", "''") & "','Employee Attendance','" & strPhon.Replace("'", "''") & "'," & LoginUserId & ")"
            '                            cmd.CommandText = strSQL
            '                            cmd.ExecuteNonQuery()
            '                        End If
            '                    End If
            '                    strPhon = String.Empty
            '                Next
            '            End If
            '        Next
            '    End If
            'End If




            dt.Columns.Add("Employee_ID", GetType(System.Int32))
            dt.Columns.Add("AttendanceDate", GetType(System.DateTime))
            dt.Columns.Add("AttendanceType", GetType(System.String))
            dt.Columns.Add("AttendanceTime", GetType(System.DateTime))
            dt.Columns.Add("AttendanceStatus", GetType(System.String))
            dt.Columns.Add("ShiftId", GetType(System.Int32))
            dt.Columns.Add("ShiftStartTime", GetType(System.DateTime))
            dt.Columns.Add("ShiftEndTime", GetType(System.DateTime))
            dt.Columns.Add("FlexInTime", GetType(System.DateTime))
            dt.Columns.Add("FlexOutTime", GetType(System.DateTime))
            dt.Columns.Add("Employee_Name", GetType(System.String))
            dt.Columns.Add("Employee_Code", GetType(System.String))
            dt.Columns.Add("Father_Name", GetType(System.String))
            dt.Columns.Add("ShiftGroupName", GetType(System.String))
            dt.Columns.Add("Department", GetType(System.String))
            dt.Columns.Add("Designation", GetType(System.String))
            dt.Columns.Add("Mobile", GetType(System.String))
            dt.Columns.Add("MachineSN", GetType(System.String))
            dt.AcceptChanges()


            Dim dtAttData As New DataTable
            dtAttData = UtilityDAL.GetDataTable("SELECT U.TAGID, CHK.CHECKTIME, CHK.CHECKTYPE, Emp.Employee_Name, Emp.Employee_Code, Emp.Father_Name, Emp.Gender, Emp.DOB, Emp.CityName,  " _
            & " Emp.StateName, Emp.[Address], Emp.Mobile, Emp.Email, Emp.ShiftId, Emp.ShiftGroupName, Emp.Dept_Division, Emp.PayRoll_Division, EmployeeDeptName as Department,EmployeeDesignationName as Designation, Convert(DateTime,IsNull(Emp.ShiftStartTime,'8:30:00 AM')) as ShiftStartTime, Convert(DateTime,IsNull(Emp.ShiftEndTime,'6:00:00 PM')) as ShiftEndTime, Convert(DateTime,IsNull(Emp.FlexIntime,'9:00:00 AM')) as FlexInTime, Convert(DateTime,IsNull(Emp.FlexOutTime,'6:00:00 PM')) as FlexOutTime, IsNull(Emp.NightShift,0) as NightShift, ISNULL(CHK.sn,'') AS MachineSN  " _
            & " FROM dbo.CHECKINOUT AS CHK INNER JOIN dbo.USERINFO AS U ON CHK.USERID = U.USERID INNER JOIN dbo.EmployeesView AS Emp ON U.TAGID = Emp.Employee_ID WHERE Convert(DateTime,CHK.CHECKTIME,102) NOT IN(SELECT Convert(DateTime,ATTENDANCETIME,102) as ATTENDANCETIME FROM tblAttendanceDetail WHERE Convert(DateTime,ATTENDANCETIME,102) Is Not Null) AND CONVERT(DATETIME,CONVERT(VARCHAR,CHK.CHECKTIME,102),102) is not null Order By U.TAGID, CHK.CHECKTIME ", trans)
            dtAttData.AcceptChanges()
            Dim dtAttDetail As New DataTable


            If dtAttData.Rows.Count > 0 Then
                For Each dtRow As DataRow In dtAttData.Rows

                    Dim strAttendanceType As String = String.Empty
                    Dim dtCheckAtt As New DataTable
                    Dim SchInTime As DateTime
                    Dim SchOutTime As DateTime
                    Dim FlexIntime As DateTime
                    Dim FlexOutTime As DateTime


                    Dim intSchInDays As Double = CDate(dtRow.Item("Checktime")).Date.Subtract(CDate(dtRow.Item("ShiftStartTime")).Date).Days
                    Dim intSchOutDays As Double = CDate(dtRow.Item("Checktime")).Date.Subtract(CDate(dtRow.Item("ShiftEndTime")).Date).Days
                    Dim intFlexInDays As Double = CDate(dtRow.Item("Checktime")).Date.Subtract(CDate(dtRow.Item("FlexInTime")).Date).Days
                    Dim intFlexOutDays As Double = CDate(dtRow.Item("Checktime")).Date.Subtract(CDate(dtRow.Item("FlexOutTime")).Date).Days


                    SchInTime = CDate(dtRow.Item("ShiftStartTime")).AddDays(intSchInDays)
                    SchOutTime = CDate(dtRow.Item("ShiftEndTime")).AddDays(intSchOutDays)
                    FlexIntime = CDate(dtRow.Item("FlexInTime")).AddDays(intFlexInDays)
                    FlexOutTime = CDate(dtRow.Item("FlexOutTime")).AddDays(intFlexOutDays)


                    dtCheckAtt = UtilityDAL.GetDataTable("Select EmpID, Count(Case When AttendanceType='In' Then 'In' Else null End) as InCount, Count(Case When AttendanceType='Out' Then 'Out' Else null End) as OutOut From tblAttendanceDetail WHERE EmpID=" & Val(dtRow.Item("TAGID").ToString) & " AND (Convert(Varchar,AttendanceDate,102)= Convert(Datetime, '" & CDate(dtRow.Item("CHECKTIME")).ToString("yyyy-M-d 00:00:00") & "',102)) Group by EmpID ", trans)
                    dtCheckAtt.AcceptChanges()
                    If dtCheckAtt.Rows.Count > 0 Then

                        If Val(dtCheckAtt.Rows(0).Item(1).ToString) > Val(dtCheckAtt.Rows(0).Item(2).ToString) Then
                            strAttendanceType = "Out"
                        Else
                            If Convert.ToBoolean(dtRow.Item("NightShift")) = True Then
                                If CDate(dtRow.Item("Checktime")) > SchInTime Then
                                    strAttendanceType = "In"
                                Else
                                    strAttendanceType = "Out"
                                End If
                            Else
                                strAttendanceType = "In"
                            End If
                        End If
                    Else
                        If Convert.ToBoolean(dtRow.Item("NightShift")) = True Then
                            If CDate(dtRow.Item("Checktime")) > SchInTime Then
                                strAttendanceType = "In"
                            Else
                                strAttendanceType = "Out"
                            End If
                        Else
                            strAttendanceType = "In"
                        End If
                    End If

                    strSQL = "INSERT INTO tblAttendanceDetail(EmpID,AttendanceDate,AttendanceType,AttendanceTime,AttendanceStatus,ShiftId, Auto,Flexibility_In_Time,Flexibility_Out_Time,Sch_In_Time,Sch_Out_Time,MachineSN)" _
                    & " VALUES(" & Val(dtRow.Item("TAGID").ToString) & ",Convert(DateTime,'" & CDate(dtRow.Item("CHECKTIME")).ToString("yyyy-M-d") & "',102),'" & strAttendanceType & "', " _
                    & " Convert(DateTime,'" & CDate(dtRow.Item("CHECKTIME")).ToString("yyyy-M-d hh:mm:ss tt") & "',102),'Present'," & Val(dtRow.Item("ShiftId").ToString) & ",1, " _
                    & " Convert(DateTime,'" & FlexIntime.ToString("yyyy-M-d hh:mm:ss tt") & "',102),Convert(DateTime,'" & FlexOutTime.ToString("yyyy-M-d hh:mm:ss tt") & "',102),Convert(DateTime,'" & SchInTime.ToString("yyyy-M-d hh:mm:ss tt") & "',102),Convert(DateTime,'" & SchOutTime.ToString("yyyy-M-d hh:mm:ss tt") & "',102), N'" & dtRow.Item("MachineSN").ToString & "') "
                    cmd.CommandText = ""
                    cmd.CommandText = strSQL
                    cmd.ExecuteNonQuery()


                    Dim dr As DataRow
                    dr = dt.NewRow
                    dr(0) = Val(dtRow.Item("TAGID").ToString) 'Employee_ID
                    dr(1) = CDate(dtRow.Item("CHECKTIME").ToString).Date  'Attendance Date
                    dr(2) = strAttendanceType 'AttendanceType
                    dr(3) = dtRow.Item("CHECKTIME").ToString 'AttendanceTime
                    dr(4) = "Present" 'AttendanceStatus
                    dr(5) = Val(dtRow.Item("ShiftId").ToString) 'Shift ID
                    dr(6) = SchInTime 'Sch In Time
                    dr(7) = SchOutTime 'Sch Out Time
                    dr(8) = FlexIntime 'Flexibility In Time
                    dr(9) = FlexOutTime 'Flexibilty Out Time
                    dr(10) = dtRow.Item("Employee_Name").ToString()
                    dr(11) = dtRow.Item("Employee_Code").ToString()
                    dr(12) = dtRow.Item("Father_Name").ToString()
                    dr(13) = dtRow.Item("ShiftGroupName").ToString()
                    dr(14) = dtRow.Item("Department").ToString()
                    dr(15) = dtRow.Item("Designation").ToString()
                    dr(16) = dtRow.Item("Mobile").ToString()
                    dr(17) = dtRow.Item("MachineSN").ToString()
                    dt.Rows.Add(dr)
                    dt.AcceptChanges()

                Next
            End If


            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            objCon.Close()
        End Try

        Try


            Dim strEmpPhone As String = String.Empty
            Dim strPhones() As String = {}
            Dim strPhon As String = String.Empty


            If dt IsNot Nothing Then
                For Each r As DataRow In dt.Rows

                    'If GetSMSConfig("Employee Attendance").Enable = True Then
                    '    strEmpPhone = CStr(r.Item("Mobile")).ToString.Replace("-", "").Replace(")", "").Replace("(", "").Replace("@", "").Replace("+", "").Replace(".", "")
                    'Else
                    '    Array.Clear(strPhones, 0, 0)
                    'End If
                    'If GetSMSConfig("Employee Attendance").EnabledAdmin = True Then
                    '    strAdminMob = strAdminMobileNo.Replace("-", "").Replace(")", "").Replace("(", "").Replace("@", "").Replace("+", "").Replace(".", "")
                    'Else
                    '    strAdminMob = String.Empty
                    'End If
                    'strPhones = CStr(strEmpPhone & ";" & strAdminMob).Split(";")

                    'If strPhones.Length > 0 Then
                    '    For Each strPhone As String In strPhones

                    '        If strPhone.Length <= 11 Then
                    '            strPhon = "92" & Microsoft.VisualBasic.Right(strPhone, 10).ToString
                    '        Else
                    '            strPhon = strPhone
                    '        End If
                    '        strSQL = String.Empty
                    '        If Not IsDBNull(r.Item("AttendanceDate")) Then
                    '            If strPhon.Length > 10 Then
                    '                strSQL = "INSERT INTO tblSMSLog(SMSLogDate, SMSBody,SMSType,PhoneNo,CreatedByUserID) " _
                    '                & " VALUES(Convert(DateTime,'" & CDate(r.Item("AttendanceDate")).ToString("yyyy-M-d hh:mm:ss tt") & "', 102),'" & CStr(r.Item("Employee_Name").ToString.Replace("'", "''") & " is " & IIf(r.Item("AttendanceType").ToString = "In", "arrived", "departure") & " at " & CDate(r.Item("AttendanceTime")) & " on " & CDate(r.Item("AttendanceTime")).ToString("dddd") & " Automated By:www.SIRIUS.net").Replace("'", "''") & "','Employee Attendance','" & strPhon.Replace("'", "''") & "'," & LoginUserId & ")"
                    '                cmd.CommandText = strSQL
                    '                cmd.ExecuteNonQuery()
                    '            End If
                    '        End If
                    '        strPhon = String.Empty
                    '    Next
                    'End If

                    '...................... Send SMS .............................
                    Dim strAdminMobile As String = String.Empty
                    Dim strEmpMobile As String = String.Empty

                    If GetSMSConfig("Employee Attendance").Enable = True Then
                        strEmpMobile = CStr(r.Item("Mobile")).ToString.Replace("-", "").Replace(")", "").Replace("(", "").Replace("@", "").Replace("+", "").Replace(".", "")
                    End If

                    If GetSMSConfig("Employee Attendance").EnabledAdmin = True Then
                        strAdminMobile = strAdminMobileNo.Replace("-", "").Replace(")", "").Replace("(", "").Replace("@", "").Replace("+", "").Replace(".", "")
                    End If

                    'If msg_Confirm(str_ConfirmSendSMSMessage) = False Then Exit Try
                    Dim strDetailMessage As String = String.Empty
                    Dim objTemp As New SMSTemplateParameter

                    Dim obj As Object = Nothing
                    If r.Item("AttendanceType").ToString = "In" Then
                        obj = GetSMSTemplate("Attendance In Time")
                    Else
                        obj = GetSMSTemplate("Attendance Out Time")
                    End If
                    If obj IsNot Nothing Then
                        objTemp.SMSTemplate = CType(obj, SMSTemplateParameter).SMSTemplate
                        Dim strMessage As String = objTemp.SMSTemplate
                        strMessage = strMessage.Replace("@Employee_Name", r.Item("Employee_Name").ToString).Replace("@Father_Name", r.Item("Father_Name").ToString).Replace("@Employee_Code", r.Item("Employee_Code").ToString).Replace("@AttendanceDate", CDate(r.Item("AttendanceTime")).ToShortDateString).Replace("@AttendanceTime", CDate(r.Item("AttendanceTime")).ToString("yyyy-M-d hh:mm:ss tt")).Replace("@AttendanceStatus", r.Item("AttendanceType").ToString).Replace("@Department", r.Item("Department").ToString).Replace("@Designation", r.Item("Designation").ToString).Replace("@Shift", r.Item("ShiftGroupName").ToString).Replace("@CompanyName", CompanyTitle).Replace("@SIRIUS", "Automated by www.siriussolution.com")
                        Dim strMobiles() As String = CStr(strEmpMobile & ";" & strAdminMobile).Replace(",", ";").Replace("|", ";").Replace("^", ";").Split(";")
                        Dim strPhone As String = String.Empty
                        If strMobiles.Length > 0 Then
                            For Each str_Phone As String In strMobiles
                                If str_Phone.Length >= 10 Then
                                    strPhone = str_Phone
                                    If strPhone.Length < 12 Then
                                        strPhone = "92" & Microsoft.VisualBasic.Right(strPhone, 10)
                                    Else
                                        strPhone = strPhone
                                    End If
                                    If strPhone.Length > 11 Then
                                        SaveSMSLog(strMessage, strPhone, "Attendance")
                                    End If
                                End If
                                strPhone = String.Empty
                            Next
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Public Function InsertBreakAttendance() As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = " " _
                     & " INSERT INTO tblAttendanceDetail(EmpId,AttendanceDate,AttendanceType,AttendanceTime,AttendanceStatus,ShiftId,Flexibility_In_Time,Flexibility_Out_Time,Sch_In_Time,Sch_Out_Time) " _
                     & " SELECT dbo.tblDefEmployee.Employee_ID, CONVERT(datetime, CONVERT(varchar, GETDATE(), 102), 102) AS AttendanceDate, 'Out' AS AttendanceType, CASE WHEN SpecialDayBreakTime <> '" & Date.Now.ToString("dddd") & "' Then Convert(DateTime,Convert(varchar,GetDate(),102) + ' ' + dbo.ShiftTable.BreakStartTime,102)  ELSE  Convert(DateTime,Convert(varchar,GetDate(),102) + ' ' + dbo.ShiftTable.SpecialDayBreakStartTime,102)  End  " _
                     & " AS AttendanceTime, 'Break' AS AttendanceStatus, dbo.tblDefEmployee.ShiftGroupId AS ShiftId, CONVERT(datetime, CONVERT(varchar, GETDATE(), 102)  " _
                     & " + ' ' + dbo.ShiftTable.FlexInTime, 102) AS Flexibility_In_Time, CONVERT(datetime, CONVERT(varchar, GETDATE(), 102) + ' ' + dbo.ShiftTable.FlexOutTime, 102)  " _
                     & " AS Flexibility_Out_Time, CONVERT(datetime, CONVERT(varchar, GETDATE(), 102) + ' ' + dbo.ShiftTable.ShiftStartTime, 102) AS Sch_In_Time, CONVERT(datetime,  " _
                     & " CONVERT(varchar, GETDATE(), 102) + ' ' + dbo.ShiftTable.ShiftEndTime, 102) AS Sch_Out_Time " _
                     & " FROM dbo.tblDefEmployee LEFT OUTER JOIN dbo.ShiftTable INNER JOIN dbo.ShiftScheduleTable ON dbo.ShiftTable.ShiftId = dbo.ShiftScheduleTable.ShiftId INNER JOIN " _
                     & " dbo.ShiftGroupTable ON dbo.ShiftScheduleTable.ShiftGroupId = dbo.ShiftGroupTable.ShiftGroupId ON dbo.tblDefEmployee.ShiftGroupId = dbo.ShiftGroupTable.ShiftGroupId INNER JOIN " _
                     & " (SELECT EmpId, MIN(AttendanceTime) AS AttendanceTime FROM dbo.tblAttendanceDetail WHERE (CONVERT(varchar, AttendanceDate, 102) = CONVERT(varchar, GETDATE(), 102)) AND (dbo.tblAttendanceDetail.AttendanceType='In') " _
                     & " GROUP BY EmpId) AS EmpAtt ON EmpAtt.EmpId = dbo.tblDefEmployee.Employee_ID WHERE (Convert(DateTime,GetDate(),102) > CASE WHEN SpecialDayBreakTime <> '" & Date.Now.ToString("dddd") & "' Then Convert(DateTime,Convert(varchar,GetDate(),102) + ' ' + dbo.ShiftTable.BreakStartTime,102)  ELSE  Convert(DateTime,Convert(varchar,GetDate(),102) + ' ' + dbo.ShiftTable.SpecialDayBreakStartTime,102) End)  AND dbo.tblDefEmployee.Employee_ID Not In(Select EmpID From tblAttendanceDetail WHERE (Convert(varchar,AttendanceDate,102)=Convert(dateTime,Convert(varchar,getDate(),102),102)) AND (dbo.tblAttendanceDetail.AttendanceStatus='Break'))"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
#Region "Enum LCDocuments"
    Private Enum enmLCMaster
        LCID
        LCNo
        LCDate
        LCAccountId
        LCExpenseAccountId
        ImportName
        PerformaNo
        PerformaDate
        PaymentMode
        LCBeforeDate
        PortOfLoading
        PortOfDischarge
        SupplierAccountId
        IndenterAccountId
        PartialShipment
        Transhipment
        LatestDateOfShipment
        LSBDate
        Insurrance
        ExchangeRate
        BankPaidAmount
        ShipingCharges
        PortCharges
        AssessedValue
        DDForCMCC
        DDForETO
        TotalQty
        TotalAmount
        TotalDuty
        SalesTax
        AdditionalSalesTax
        AdvanceIncomeTax
        UserName
        EntryDate
        CostCenterId
        ExciseDutyPercent
        ExciseDuty
        ImportValue
        OtherCharges
        Advising_Bank
        Special_Instruction
        Reference_No
        Performa_No
        Performa_Date
        Vessel
        BL_No
        BL_Date
        ETD_Date
        ETA_Date
        Clearing_Agent
        TransporterID
        OpenedBy
        Expiry_Date
        CurrencyType
        CurrencyRate
        LCDocID
        ExpenseByLC
    End Enum

    Private Enum enmGrdDetail
        LCDetailId
        LCId
        LocationId
        ArticleDefId
        ArticleSize
        PackDesc
        Sz1
        Sz7
        Qty
        Price
        TotalAmount
        Insurrance
        AddCostPercent
        AssessedValue
        DutyPercent
        Duty
        SaleTaxPercent
        SaleTax
        AddSaleTaxPercent
        AddSaleTax
        AdvIncomeTaxPercent
        AdvIncomeTax
        Comments
        PurchaseAccountId
        Exch_Rate
        Import_Value
        ExciseDutyPercent
        ExciseDuty
        Other_Charges
        Net_Amount
    End Enum
#End Region

    Public Function UpgrdateLCDocumentByCostCenter(ByVal CostCenterId As Integer) As Boolean
        Dim LC As LCBE = Nothing
        Try


            If CostCenterId <= 0 Then Return False
            Dim dt As New DataTable
            Dim _AddCostAccountId As Integer = 0I
            Dim _CustomDutyAccountId As Integer = 0I
            Dim _SalesTaxAccountId As Integer = 0I
            Dim _AdditionalSalesTaxAccountId As Integer = 0I
            Dim _AdvanceIncomeTaxAccountId As Integer = 0I
            Dim _ExciseDutyAccountId As Integer = 0I


            If getConfigValueByType("AdditionalCostAccountId").ToString <> "Error" Then
                _AddCostAccountId = getConfigValueByType("AdditionalCostAccountId").ToString
            End If

            If getConfigValueByType("CustomDutyAccountId").ToString <> "Error" Then
                _CustomDutyAccountId = getConfigValueByType("CustomDutyAccountId").ToString
            End If

            If getConfigValueByType("LCSalesTaxAccountId").ToString <> "Error" Then
                _SalesTaxAccountId = getConfigValueByType("LCSalesTaxAccountId").ToString
            End If

            If getConfigValueByType("AdditionalSalesTaxAccountId").ToString <> "Error" Then
                _AdditionalSalesTaxAccountId = getConfigValueByType("AdditionalSalesTaxAccountId").ToString
            End If


            If getConfigValueByType("AdvanceIncomeTaxAccountId").ToString <> "Error" Then
                _AdvanceIncomeTaxAccountId = getConfigValueByType("AdvanceIncomeTaxAccountId").ToString
            End If

            If getConfigValueByType("ExciseDutyAccountId").ToString <> "Error" Then
                _ExciseDutyAccountId = getConfigValueByType("ExciseDutyAccountId").ToString
            End If

            dt = GetDataTable("Select * From LCMasterTable WHERE CostCenter=" & CostCenterId & "")
            If dt.HasErrors = False Then
                If dt.Rows.Count > 0 Then
                    For Each r As DataRow In dt.Rows

                        LC = New LCBE
                        With LC
                            .LCID = Val(r(enmLCMaster.LCID).ToString)
                            .LCNo = r(enmLCMaster.LCNo).ToString
                            .LCDate = r(enmLCMaster.LCDate)
                            .LCAccountId = Val(r(enmLCMaster.LCAccountId).ToString)
                            .LCExpenseAccountId = Val(r(enmLCMaster.LCExpenseAccountId).ToString)
                            .ImportName = r(enmLCMaster.ImportName).ToString
                            .PerformaNo = String.Empty 'Me.txtPerformaNo.Text
                            .PerformaDate = Date.Now 'Me.dtpPerformaDate.Value
                            .PaymentMode = String.Empty 'Me.cmbPaymentMethod.Text
                            .LCBoreDate = Date.Now 'Me.dtpLCBeforeDate.Value
                            .PortOfDischarge = r(enmLCMaster.PortOfDischarge).ToString
                            .PortOfLoading = r(enmLCMaster.PortOfLoading).ToString
                            .SupplierAccountId = Val(r(enmLCMaster.SupplierAccountId).ToString)
                            .IndenterAccountId = Val(r(enmLCMaster.IndenterAccountId).ToString)
                            .PartialShipment = r(enmLCMaster.PartialShipment)
                            .Transhipment = r(enmLCMaster.Transhipment)
                            .LatestDateOfShipment = r(enmLCMaster.LatestDateOfShipment)
                            .LSBDate = r(enmLCMaster.LSBDate)
                            .Insurrance = Val(r(enmLCMaster.Insurrance).ToString)
                            .ExchangeRate = Val(r(enmLCMaster.ExchangeRate).ToString)
                            .BankPaidAmount = 0 'Val(Me.txtBankPaidAmount.Text)
                            .ShipingCharges = Val(r(enmLCMaster.ShipingCharges).ToString)
                            .PortCharges = Val(r(enmLCMaster.PortCharges).ToString)
                            .DDForCMCC = Val(r(enmLCMaster.DDForCMCC).ToString)
                            .DDForETO = Val(r(enmLCMaster.DDForETO).ToString)
                            .TotalAmount = Val(r(enmLCMaster.TotalAmount).ToString)
                            .TotalDuty = Val(r(enmLCMaster.TotalDuty).ToString)
                            .TotalQty = Val(r(enmLCMaster.TotalQty).ToString)
                            .SalesTax = Val(r(enmLCMaster.SalesTax).ToString)
                            .AdditionalSalesTax = Val(r(enmLCMaster.AdditionalSalesTax).ToString)
                            .AdvanceIncomeTax = Val(r(enmLCMaster.AdvanceIncomeTax).ToString)
                            .AssessedValue = Val(r(enmLCMaster.AssessedValue).ToString)
                            .UserName = LoginUserName
                            .EntryDate = Date.Now
                            .CostCenterId = Val(r(enmLCMaster.CostCenterId).ToString)
                            .ExciseDutyPercent = 0 'Val(Me.txtExciseDutyPercent.Text)
                            .ExciseDuty = Val(r(enmLCMaster.DDForETO).ToString) 'Val(Me.txtExciseDuty.Text)
                            .LCDocId = Val(r(enmLCMaster.LCDocID).ToString)
                            .OthersCharges = Val(r(enmLCMaster.OtherCharges).ToString)
                            .ExpenseByLC = Val(r(enmLCMaster.ExpenseByLC).ToString)


                            Dim dtDetail As New DataTable
                            dtDetail = GetDataTable("Select *, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription From LCDetailTable INNER JOIN ArticleDefView on ArticleDefView.ArticleId = LCDetailTable.ArticleDefId WHERE LCID=" & Val(r(enmLCMaster.LCID).ToString) & "")
                            dtDetail.AcceptChanges()



                            If dtDetail.HasErrors = False Then

                                If dtDetail.Rows.Count > 0 Then
                                    .LCDetail = New List(Of LCDetailBE)
                                    Dim objLCDetail As LCDetailBE
                                    For Each row As DataRow In dtDetail.Rows
                                        With .LCDetail
                                            objLCDetail = New LCDetailBE
                                            With objLCDetail
                                                .AddCostPercent = Val(row.Item(enmGrdDetail.AddCostPercent).ToString)
                                                .AddSaleTax = Val(row.Item(enmGrdDetail.AddSaleTax).ToString)
                                                .AddSaleTaxPercent = Val(row.Item(enmGrdDetail.AddSaleTaxPercent).ToString)
                                                .AdvIncomeTax = Val(row.Item(enmGrdDetail.AdvIncomeTax).ToString)
                                                .AdvIncomeTaxPercent = Val(row.Item(enmGrdDetail.AdvIncomeTaxPercent).ToString)
                                                .ArticleDefId = Val(row.Item(enmGrdDetail.ArticleDefId).ToString)
                                                .ArticleSize = row.Item(enmGrdDetail.ArticleSize).ToString
                                                .AssessedValue = Val(row.Item(enmGrdDetail.AssessedValue).ToString)
                                                .Comments = row.Item(enmGrdDetail.Comments).ToString
                                                .Duty = Val(row.Item(enmGrdDetail.Duty).ToString)
                                                .DutyPercent = Val(row.Item(enmGrdDetail.DutyPercent).ToString)
                                                .Insurrance = Val(row.Item(enmGrdDetail.Insurrance).ToString)
                                                .LCDetailId = 0I
                                                .LCId = Val(row.Item(enmLCMaster.LCID).ToString)
                                                .LocationId = Val(row.Item(enmGrdDetail.LocationId).ToString)
                                                .PackDesc = row.Item(enmGrdDetail.PackDesc).ToString
                                                .Price = Val(row.Item(enmGrdDetail.Price).ToString)
                                                .Qty = IIf(.ArticleSize.ToString = "Loose", Val(row.Item(enmGrdDetail.Qty).ToString), (Val(row.Item(enmGrdDetail.Qty).ToString) * Val(row.Item(enmGrdDetail.Sz7).ToString)))
                                                .SaleTax = Val(row.Item(enmGrdDetail.SaleTax).ToString)
                                                .SaleTaxPercent = Val(row.Item(enmGrdDetail.SaleTaxPercent).ToString)
                                                .Sz1 = Val(row.Item(enmGrdDetail.Sz1).ToString)
                                                .Sz7 = Val(row.Item(enmGrdDetail.Sz7).ToString)
                                                .TotalAmount = Val(row.Item(enmGrdDetail.TotalAmount).ToString)
                                                .PurchaseAccountId = Val(row.Item(enmGrdDetail.PurchaseAccountId).ToString)
                                                .Exch_Rate = Val(row.Item(enmGrdDetail.Exch_Rate).ToString)
                                                .Import_Value = Val(row.Item(enmGrdDetail.Import_Value).ToString)
                                                .LedgerComments = String.Empty
                                                .ArticleDescription = row.Item("ArticleDescription").ToString
                                                .ExciseDutyPercent = Val(row.Item(enmGrdDetail.ExciseDutyPercent).ToString)
                                                .ExciseDuty = Val(row.Item(enmGrdDetail.ExciseDuty).ToString)
                                                .Other_Charges = Val(row.Item(enmGrdDetail.Other_Charges).ToString)
                                                .Net_Amount = Val(row.Item(enmGrdDetail.Net_Amount).ToString)
                                                .AdditionalCostAccountId = _AddCostAccountId
                                                .CustomDutyAccountId = _CustomDutyAccountId
                                                .SalesTaxAccountId = _SalesTaxAccountId
                                                .AdditionalSalesTaxAccountId = _AdditionalSalesTaxAccountId
                                                .AdvanceIncomeTaxAccountId = _AdvanceIncomeTaxAccountId
                                                .ExciseDutyAccountId = _ExciseDutyAccountId
                                            End With

                                            .Add(objLCDetail)
                                        End With
                                    Next

                                End If
                            End If
                            .ActivityLog = New ActivityLog
                            With .ActivityLog
                                .User_Name = LoginUserName
                                .UserID = LoginUserId
                                .Source = "frmImport"
                                .FormCaption = "Import Document"
                                .LogDateTime = Date.Now
                            End With
                        End With
                    Next
                End If
            End If

            If LC IsNot Nothing Then
                If New LCDAL().Update(LC) = True Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function GetProductEdition() As String
        Try
            If getConfigValueByType("EnabledCorporateVer").ToString <> "" Then
                If getConfigValueByType("EnabledCorporateVer").ToString = "True" Then
                    Return "Corporate Edition"
                Else
                    Return "Basic Edition"
                End If
            Else
                Return "Basic Edition"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetFormAccess(ByVal key As String) As Boolean
        Try

            If GetProductEdition() = "Corporate Edition" Then
                Return True
            Else
                If key = "rptBalanceSheetDetail" Or _
                key = "BalanceSheetFrmt" Or _
                key = "ProfitAndLossFrmt" Or _
                key = "rptPLDetail" Or _
                key = "frmDefItem" Or _
                key = "frmDefItem" Or _
                key = "frmInventoryConfiguration" Or _
                key = "frmDefLocation" Or _
                key = "frmItemDefGroup" Or _
                key = "frmDefItemType" Or _
                key = "frmDefItemGender" Or _
                key = "frmDefItemUnit" Or _
                key = "frmSalesOrder" Or _
                key = "frmSales" Or _
                key = "frmSalesReturn" Or _
                key = "frmPurchaseOrder" Or _
                key = "frmPurchase" Or _
                key = "frmPurchaseReturn" Or _
                key = "frmGrdRptStockStatement" Or _
                key = "frmInventoryConfiguration" Or _
                key = "frmDefLocation" Or _
                key = "frmItemDefGroup" Or _
                key = "frmDefItemType" Or _
                key = "frmDefItemGender" Or _
                key = "frmDefItemUnit" Or _
                key = "frmSalesOrder" Or _
                key = "frmSales" Or _
                key = "frmSalesReturn" Or _
                key = "frmPurchaseOrder" Or _
                key = "frmPurchase" Or _
                key = "frmPurchaseReturn" _
                Or key = "frmGrdRptStockStatement" Then
                    Return False
                Else
                    Return True
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Enum enmForms
        '-------- SimpleAccounts/Assets/-----------------
        AssetCondition
        AssetType
        frmAdd
        frmAsset
        frmAssetCategory
        frmAssetLocation
        frmAssetStatus
        '-------- SimpleAccounts/Chart Of accounts -----
        DefMainAcc
        frmBSandPLNotesDetail
        frmCostCenter
        frmDetailAccount
        frmDetailAccountCat
        frmSubAccount
        frmSubSubAccount
        '-------- SimpleAccounts/Configuration ---------
        CompanyAndConnectionInfo
        frmAddAccount
        frmAdministorTools
        frmAllowanceType
        frmChequeLayouts
        frmCompanyInformation
        frmCustomerDiscounts
        frmDBList
        frmDeductionType
        frmDefAllocateShiftShedule
        frmDefArea
        frmDefArticleDepartment
        frmDefBatch
        frmDefCategory
        frmDefCategoryold
        frmDefCity
        frmDefColor
        frmDefCommissionBySaleman
        frmDefCustomer
        frmDefCustomerType
        frmDefDepartment
        frmDefDivision
        frmDefEmpDesignation
        frmDefEmployee
        frmDefEmployeeOld
        frmDefGender
        frmDefLeaveEncashment
        frmDefPayRollDivision
        frmDefServices
        frmDefShift
        frmDefShiftGroup
        frmDefSize
        frmDefSubCategory
        frmDefTransporter
        frmDefType
        frmDefUnit
        frmDefVehicle
        frmDefVendor
        FrmEmailconfig
        frmEmailTemplate
        frmEmployeeArticleCostRate
        frmGrdCustomerBasedTarget
        frmInventoryLevel
        frmlatetimeSlot
        FrmLocation
        frmPaymentTermsType
        frmproductionSteps
        frmRootPlan
        frmSalaryType
        frmSalesTarget
        frmServersList
        frmSystemConfigurationNew
        frmTerminal
        FrmVehicle
        frmVendorType
        frmYearlySaleTarget
        frmSimpleItemDefForm
        frmDefArticle
        '------------ SimpleAccounts/CRM ------------
        frmLeads
        frmStatus
        frmTasks
        frmTaskWork
        frmTypes
        '------------ SimpleAccounts/New/Configuration
        AddItems
        AddUserGroup
        frmAddBillEmbroidery
        frmAddChequeBookSerial
        frmAddItem
        frmAdjustmentType
    End Enum

    Public Sub CreateCurrentDBBackup()
        Try

            Dim objConStringBuilder As New OleDb.OleDbConnectionStringBuilder(Con.ConnectionString)
            Dim strFileName As String = String.Empty
            Dim strFilePath As String = String.Empty
            strFilePath = Application.StartupPath & "\temp"
            If Not IO.Directory.Exists(strFilePath) Then
                IO.Directory.CreateDirectory(strFilePath)
            End If

            Dim strSQL As String = String.Empty
            strFileName = objConStringBuilder.Item("Initial Catalog").ToString & "_" & Date.Now.ToString("yyyyMMddhhmmss") & ".bak"
            strSQL = "BACKUP DATABASE " & objConStringBuilder.Item("Initial Catalog").ToString & " TO DISK='" & strFilePath & "\" & strFileName.ToString & "'"

            If Con.State = ConnectionState.Closed Then Con.Open()
            Dim cmd As New OleDbCommand(strSQL, Con)
            cmd.CommandTimeout = 120
            cmd.ExecuteNonQuery()


            If Con.State = ConnectionState.Closed Then Con.Open()
            strSQL = String.Empty
            strSQL = "INSERT INTO DatabaseBackupLog(LogDate,DbName,BackupFile,BackupPath,Terminal,Status) VALUES(Convert(DateTime,'" & Date.Now.ToString("yyyy-M-d hh:mm:ss tt") & "',102),'" & objConStringBuilder.Item("Initial Catalog").ToString & "','" & strFileName.ToString & "', '" & strFilePath.Replace("'", "''") & "','" & My.Computer.Name.ToString.Replace("'", "''") & "','Successfully')Select @@Identity"
            cmd = New OleDbCommand(strSQL, Con)
            Dim intLogId As Integer = cmd.ExecuteScalar()



        Catch ex As Exception
            'Throw ex
        End Try
    End Sub
    Public Sub UserDecrypt()
        Try

            Dim strSQL As String = String.Empty
            strSQL = "Select * From tblUser WHERE FullName <> ''"
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()

            If dt.Rows.Count > 0 Then
                For Each r As DataRow In dt.Rows
                    If Con.State = ConnectionState.Closed Then Con.Open()
                    Dim cmd As New OleDbCommand("Update tblUser Set FullName='" & Decrypt(r.Item("User_Name").ToString) & "' WHERE User_ID=" & Val(r.Item("User_Id").ToString) & "", Con)
                    cmd.ExecuteNonQuery()
                Next
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private _DocDate As DateTime
    Public Function IsDateLock(ByVal DocDate As DateTime) As Boolean
        Try

            _DocDate = DocDate
            'Dim obj As New DateLockPermissionListByUserID
            'If DateLockPermissionList IsNot Nothing Then
            '    If DateLockPermissionList.Count > 0 Then
            '        obj = DateLockPermissionList.Find(AddressOf FindPermissionUser)
            '        If obj IsNot Nothing Then
            '            Return False
            '        End If
            '    End If
            'End If
            Dim dtDateLockPermission As New DataTable
            dtDateLockPermission = GetDateLockPermissionByUserId()
            If dtDateLockPermission IsNot Nothing Then
                If dtDateLockPermission.Rows.Count > 0 Then
                    For Each row As DataRow In dtDateLockPermission.Rows
                        If getUserIdForDateLock(row.Item("UserID").ToString) = True Then
                            If _DocDate.Date >= CDate(row.Item("DateLockFrom")).Date AndAlso _DocDate.Date <= CDate(row.Item("DateLockTo")).Date Then
                                Return False
                            End If
                        End If
                    Next
                End If
            End If
            GetDateLock() ''TFS4679
            If CDate(CurrentDate.ToString("yyyy-M-d")) > CDate(DocDate.ToString("yyyy-M-d")) Then 'Task211015
                Return True
            Else
                Return False
            End If
            'Dim strSQL As String = "Select Count(*) as LockCount from tblDateLock WHERE IsNull(Lock,0)=1 AND (Convert(Datetime,Convert(Varchar, DateLock,102),102) >= Convert(DateTime,'" & DocDate.ToString("yyyy-M-d 00:00:00") & "',102)) "
            'Dim dt As New DataTable
            'dt = GetDataTable(strSQL)
            'dt.AcceptChanges()
            'If dt IsNot Nothing Then
            '    If dt.Rows.Count > 0 Then
            '        If Val(dt.Rows(0).Item(0).ToString) > 0 Then
            '            Return True
            '        Else
            '            Return False
            '        End If
            '    Else
            '        Return False
            '    End If
            'Else
            '    Return False
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' This Sub is made to get DateLock Permission List By UserID
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetDateLockPermissionByUserId() As DataTable
        Try
            'Return UtilityDAL.GetDataTable("Select * from tblDateLockPermission where IsNull(Lock,0) = 1 and Id in (Select Max(Id) from tblDateLockPermission where IsNull(Lock,0) = 1 and USERID = " & LoginUser.LoginUserId & ") ")
            Return UtilityDAL.GetDataTable("Select * from tblDateLockPermission where IsNull(Lock,0) = 1")
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function getUserIdForDateLock(ByVal user As String) As Boolean
        Try
            If user.Length > 0 Then
                Dim arday() As String = user.Split(",")
                For x As Integer = 0 To arday.Length() - 1
                    If Val(arday(x).ToString) = LoginUser.LoginUserId Then
                        Return True
                    End If
                Next
            End If
            Return False
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function FindPermissionUser(ByVal obj As DateLockPermissionListByUserID) As Boolean
        Try


            If _DocDate.Date >= obj.DateFrom.Date And _DocDate.Date <= obj.DateTo.Date Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetDayOfEndMonth(ByVal intMonth As Integer, ByVal intYear As Integer) As Integer
        Try
            Select Case intMonth
                Case 1
                    Return 31
                Case 2
                    If Date.IsLeapYear(intYear) = False Then
                        Return 28
                    Else
                        Return 29
                    End If
                Case 3
                    Return 31
                Case 4
                    Return 30
                Case 5
                    Return 31
                Case 6
                    Return 30
                Case 7
                    Return 31
                Case 8
                    Return 31
                Case 9
                    Return 30
                Case 10
                    Return 31
                Case 11
                    Return 30
                Case 12
                    Return 31
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function StockTransId(ByVal DocNo As String, ByVal trans As OleDb.OleDbTransaction) As Integer
        Try
            Dim dt As New DataTable
            dt = GetDataTable("Select StockTransId From StockMasterTable WHERE DocNo='" & DocNo & "'", trans)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                    Return Val(dt.Rows(0).Item(0).ToString)
                Else
                    Return 0I
                End If
            Else
                Return 0I
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function StockTransId(ByVal DocNo As String) As Integer
        Try
            Dim dt As New DataTable
            dt = GetDataTable("Select StockTransId From StockMasterTable WHERE DocNo='" & DocNo & "'")
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                    Return Val(dt.Rows(0).Item(0).ToString)
                Else
                    Return 0I
                End If
            Else
                Return 0I
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetReceivingId(ByVal DispatchId As Integer, ByVal trans As OleDb.OleDbTransaction) As DataTable
        Try
            Dim dt As New DataTable
            dt = GetDataTable("Select ReceivingId, ReceivingNo From ReceivingMasterTable WHERE PurchaseOrderId='" & DispatchId & "'", trans)
            dt.AcceptChanges()
            Return dt
            'If dt.Rows.Count > 0 Then
            '    If Val(dt.Rows(0).Item(0).ToString) > 0 Then
            '        Return Val(dt.Rows(0).Item(0).ToString)
            '    Else
            '        Return 0I
            '    End If
            'Else
            '    Return 0I
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function IsAllowPayment() As Boolean
        Try

            If getConfigValueByType("AllowPaymentZeroBalance").ToString.ToUpper = "TRUE" Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetGLAccountId(ByVal ItemId As Integer, Optional ByVal Condition As String = "") As Integer
        Try
            Dim dt As New DataTable
            dt = GetDataTable("Select IsNull(SubSubId,0) as InventoryAcId, IsNull(SalesAccountId,0) as SalesAccountId, IsNull(CGSAccountId,0) as CSGAccountId From ArticleGroupDefTable WHERE ArticleGroupId In(Select ArticleGroupId From ArticleDefView WHERE ArticleId=" & ItemId & ")")
            dt.AcceptChanges()

            If dt.Rows.Count > 0 Then
                If Condition = "Inventory" Then
                    Return Val(dt.Rows(0).Item(0).ToString)
                ElseIf Condition = "Sales" Then
                    Return Val(dt.Rows(0).Item(1).ToString)
                ElseIf Condition = "CGS" Then
                    Return Val(dt.Rows(0).Item(2).ToString)
                End If
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAvgRateByItem(ByVal ItemID As Integer) As Integer
        Try
            Dim dt As New DataTable
            dt = GetDataTable("Select IsNull(Cost_Price,0) as CostPrice, IsNull(PurchasePrice,0) as PurchasePrice  From ArticleDefTable WHERE ArticleId=" & ItemID & "")
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                If Val(dt.Rows(0).Item(0).ToString) = 0 Then
                    Return Val(dt.Rows(0).Item(1).ToString)
                Else
                    If Not (Val(dt.Rows(0).Item(0).ToString) + 100) > Val(dt.Rows(0).Item(1).ToString) Then
                        If getConfigValueByType("AvgRate").ToString.ToUpper = "TRUE" Then
                            Return Val(dt.Rows(0).Item(0).ToString)
                        Else
                            Val(dt.Rows(0).Item(1).ToString)
                        End If
                    Else
                        Val(dt.Rows(0).Item(1).ToString)
                    End If
                End If
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetRateByItem(ByVal ItemID As Integer) As String
        Try
            Dim dt As New DataTable
            dt = GetDataTable("Select IsNull(Cost_Price,0) as CostPrice, IsNull(PurchasePrice,0) as PurchasePrice,ISNULL(CurrencyAmount, 0) as CurrencyAmount  From ArticleDefTable WHERE ArticleId=" & ItemID & "")
            dt.AcceptChanges()
            Dim hs As New Hashtable()

            Dim strValues As String = String.Format("{0},{1},{2}", Val(dt.Rows(0).Item(0).ToString), Val(dt.Rows(0).Item(1).ToString), Val(dt.Rows(0).Item(2).ToString))

            'If dt.Rows.Count > 0 Then
            '    If Val(dt.Rows(0).Item(0).ToString) = 0 Then
            '        Return Val(dt.Rows(0).Item(1).ToString)
            '    Else
            '        If Not (Val(dt.Rows(0).Item(0).ToString) + 100) > Val(dt.Rows(0).Item(1).ToString) Then
            '            If getConfigValueByType("AvgRate").ToString.ToUpper = "TRUE" Then
            '                Return Val(dt.Rows(0).Item(0).ToString)
            '            Else
            '                Val(dt.Rows(0).Item(1).ToString)
            '            End If
            '        Else
            '            Val(dt.Rows(0).Item(1).ToString)
            '        End If
            '    End If
            'Else
            '    Return 0
            'End If
            Return strValues
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Task#14072015 Getting prefix from database based on document date and module
    Public Function GetPrefix(ByVal DocDate As Date, ByVal Type As String) As String

        Dim trans As OleDbTransaction
        If Con.State = ConnectionState.Closed Then Con.Open()
        trans = Con.BeginTransaction()
        Dim reader As OleDbDataReader

        Try
            Dim Prefix As String = String.Empty
            Dim strSQL As String = String.Empty

            Dim cmd As New OleDbCommand
            cmd.Connection = Con
            cmd.Transaction = trans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text

            strSQL = "select Prefix from tblPrefix where Module = '" & Type.ToString & "' and '" & DocDate & "' between start_date and end_date"
            cmd.CommandText = strSQL
            reader = cmd.ExecuteReader()

            If reader.HasRows Then
                While reader.Read()
                    Prefix = reader("Prefix").ToString
                End While
            End If

            trans.Commit()
            Return Prefix

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    'Task#14072015





    'Task#23072015 Get All Due Invoices whose Email Status is Pending
    Public Function GetAllDuePendingInoices() As DataTable
        Try
            Dim serverDate As DateTime
            Dim dt As New DataTable
            dt = GetDataTable("Select getDate() as CurrentDate")
            dt.AcceptChanges()
            serverDate = dt.Rows(0).Item("CurrentDate")

            dt = New DataTable

            Dim strSql As String = String.Empty

            strSql = "select dbo.SalesMasterTable.SalesId, dbo.SalesMasterTable.SalesNo,Convert(datetime,dbo.SalesMasterTable.SalesDate,102) as SalesDate,dbo.SalesMasterTable.SalesAmount," _
            & " Convert(varchar,DATEADD(d,ISNULL(dbo.SalesMasterTable.DueDays, 0), dbo.SalesMasterTable.SalesDate),102) as [Due Date], " _
            & " dbo.vwCOADetail.detail_code, dbo.vwCOADetail.detail_title, dbo.vwCOADetail.CityName, dbo.vwCOADetail.Contact_Email " _
            & " from dbo.SalesMasterTable inner join dbo.vwCOADetail " _
            & " on dbo.SalesMasterTable.CustomerCode = dbo.vwCOADetail.coa_detail_id " _
            & " where (Convert(varchar,DATEADD(d,ISNULL(dbo.SalesMasterTable.DueDays, 0), dbo.SalesMasterTable.SalesDate),102)= Convert(datetime,'" & serverDate.ToString("yyyy-M-d 00:00:00") & "',102) ) " _
            & " And isNull(Email_Status,0)= 0"

            dt = GetDataTable(strSql)
            dt.AcceptChanges()

            Return dt

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function SendPendingInvoiceViaEmail(ByVal dt As DataTable) As Boolean
        Try
            Dim MessageBody As String = String.Empty

            For Each row As DataRow In dt.Rows

                If row("Contact_Email").ToString.Length <= 0 Then Exit For

                MessageBody = "Dear " & row("detail_title").ToString & "," & Environment.NewLine & Environment.NewLine
                MessageBody += "We are reminding you that your invoice no. " & row("SalesNo").ToString & " is due on " & row("Due Date") & " and due amount is " & row("SalesAmount") & "." & "Please Pay the due amount as soon as possible." & Environment.NewLine & Environment.NewLine
                MessageBody += "Please free to contact our office if you have any quries regarding to this invoice." & Environment.NewLine & Environment.NewLine
                MessageBody += "Automated by : www.siriussolution.com"

                Try

                    GetEmailConfig = New EmailSettingDAL().GetEmailSetting(String.Empty, getConfigValueByType("DefaultEmailId").ToString)

                    If GetEmailConfig Is Nothing Then Exit Function

                    Dim sendEmail As New MailMessage()

                    sendEmail.To.Add(row("Contact_Email").ToString)
                    'sendEmail.To.Add("ahmadsharif0017@gmail.com")
                    sendEmail.Subject = "Reminder : Invoice Due"
                    sendEmail.Body = MessageBody
                    sendEmail.From = New MailAddress(GetEmailConfig(0).Email)

                    sendEmail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
                    Dim Client As New SmtpClient(GetEmailConfig(0).SmtpServer)
                    Client.Port = GetEmailConfig(0).port
                    'Client.Host = GetEmailConfig(0).Host

                    Dim EmailPwd As String = Decrypt(GetEmailConfig(0).EmailPassword)
                    Client.Credentials = New Net.NetworkCredential(GetEmailConfig(0).Email, EmailPwd)
                    Client.EnableSsl = IIf(GetEmailConfig(0).ssl = True, True, False)
                    Client.DeliveryMethod = SmtpDeliveryMethod.Network
                    Client.Send(sendEmail)
                    'sendEmail.Dispose()

                    If Con.State = ConnectionState.Closed Then Con.Open()

                    Dim strSql As String = String.Empty
                    Dim cmd As New OleDbCommand
                    cmd.CommandType = CommandType.Text
                    cmd.Connection = Con


                    strSql = "Update SalesMasterTable set Email_Status = 1 Where SalesId =" & CInt(row("SalesId").ToString)
                    cmd.CommandText = strSql
                    cmd.ExecuteNonQuery()

                    Return True
                Catch ex As Exception
                    Throw ex
                End Try
            Next

            Return False
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'End Task#23072015
    Public Sub AttendanceDayOff()
        Dim objCon As New OleDbConnection(Con.ConnectionString)
        Dim trans As OleDb.OleDbTransaction = Nothing
        Try
            Dim strDay() As String = getConfigValueByType("DayOff").ToString.Split(",")
            Dim i As Integer = 1I
            If strDay.Length > 0 Then
                For Each strdayname As String In strDay
                    If strdayname.Length > 0 Then
                        Dim currentDay As String = CDate(GetDataTable("Select GetDate() as CurrentDate").Rows(0).Item(0)).AddDays(i).ToString("dddd")
                        Dim DayOffDate As DateTime = CDate(GetDataTable("Select GetDate() as CurrentDate").Rows(0).Item(0)).AddDays(i)
                        If strdayname.ToUpper = currentDay.ToUpper Then
                            If objCon.State = ConnectionState.Closed Then objCon.Open()
                            trans = objCon.BeginTransaction
                            Dim cmd As New OleDb.OleDbCommand
                            cmd.Connection = objCon
                            cmd.Transaction = trans
                            cmd.CommandTimeout = 120
                            cmd.CommandType = CommandType.Text
                            Dim strQuery As String = "INSERT INTO tblAttendanceDetail(EmpId, AttendanceDate,AttendanceType,AttendanceStatus,ShiftId) Select Employee_Id, Convert(DateTime,'" & DayOffDate.ToString("yyyy-M-d") & "',102) as AttendanceDate, 'Off', 'Day Off', ShiftId From EmployeesView WHERE (Convert(DateTime,'" & DayOffDate.ToString("yyyy-M-d 00:00:00") & "',102) not in (Select AttendanceDate From tblAttendanceDetail WHERE AttendanceStatus='Day Off') AND Active=1)"
                            cmd.CommandText = strQuery
                            cmd.ExecuteNonQuery()
                            trans.Commit()
                        End If
                    End If
                    i += 1
                Next
            End If

        Catch ex As Exception
            If trans IsNot Nothing Then trans.Rollback()
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Sub

    Public Function GetBrandedSMSBalance() As String
        Try

            If My.Computer.Network.IsAvailable = False Then Return ""

            Dim strUrl As String = "http://outreach.pk/api/sendsms.php/balance/status?"
            Dim oWebReq As WebRequest = WebRequest.Create(strUrl)
            oWebReq.Method = "POST"
            oWebReq.ContentType = "application/x-www-form-urlencoded"
            Dim oMyStreamWriter As New IO.StreamWriter(oWebReq.GetRequestStream)
            oMyStreamWriter.Write("id=" & getConfigValueByType("BrandedSMSUser").ToString & "&pass=" & Decrypt(getConfigValueByType("BrandedSMSPassword").ToString) & "")
            oMyStreamWriter.Close()
            Dim oMyRespStream As New IO.StreamReader(oWebReq.GetResponse.GetResponseStream)
            Dim Result As String = oMyRespStream.ReadToEnd
            oMyRespStream.Close()
            System.Threading.Thread.Sleep(500)


            Dim objReader As StringReader = New StringReader(Result)
            Dim objXMLDataSet As New DataSet
            objXMLDataSet.ReadXml(objReader)

            If objXMLDataSet.Tables.Count > 0 Then
                Return objXMLDataSet.Tables(0).Rows(0)(2).ToString.ToUpper()
            Else
                Return ""
            End If

        Catch ex As Exception
            'throw ex
        End Try
    End Function


    'Task#25072015 Calculating working days and holidays in current running month
    Public Function GetWorkingDaysInCurrentMonth() As Integer
        Try
            Dim str As String = getConfigValueByType("DayOff").ToString       'getting DayOff configuration, return array of checked day if any day check in configuration
            Dim workingDays As Integer = 0I
            Dim holidays As Integer = 0I

            Dim ServerDate As DateTime = GettingServerDate()        'getting server date

            Dim firstDate As New DateTime(ServerDate.Year, ServerDate.Month, 1)                 'get first date of current month
            Dim totalDaysInMonth As Integer = CDate(ServerDate).DaysInMonth(ServerDate.Year, ServerDate.Month)  'getting total days in current month
            Dim lastDate As New DateTime(ServerDate.Year, ServerDate.Month, totalDaysInMonth)       'get last date in current month

            Dim strArr() As String
            strArr = str.Split(",")     'split array of day off on comma 

            If strArr.Length < 0 Then Return 26 'Return 26 working days by default, if no day checked in DayOff configuration

            While firstDate <= lastDate
                dayOfWeek = firstDate.DayOfWeek.ToString
                If dayOfWeek = Array.Find(strArr, AddressOf getDayName) Then
                    holidays += 1           'increment in holidays count by one
                    firstDate = firstDate.AddDays(1)        'increment in date by one
                Else
                    firstDate = firstDate.AddDays(1)        'increment in date by one
                End If
            End While


            workingDays = totalDaysInMonth - holidays      'calculating working days in current month by subtracting totalDaysInMonth - holidays

            Return workingDays

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'fucntion for searching element in array
    Public Function getDayName(ByVal obj As String) As Boolean
        Try
            If obj.ToString.ToUpper = dayOfWeek.ToString.ToUpper Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'End Task#25072015


    'Task#27072015 Update Woriking Days Configuratin in ConfigValuesTable
    Public Sub UpdateWorkingDaysConfiguration(ByVal workingDays As Integer)
        Dim objCon As New OleDbConnection(Con.ConnectionString)
        Dim trans As OleDbTransaction = Nothing

        Try
            Dim strSQL As String = String.Empty

            If objCon.State = ConnectionState.Closed Then objCon.Open()
            trans = objCon.BeginTransaction()

            Dim cmd As New OleDbCommand
            cmd.Connection = objCon
            cmd.Transaction = trans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text

            strSQL = "Update configvaluestable SET config_value = '" & workingDays.ToString & "' where config_type='Working_Days'"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()

            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Sub


    'Getting Server Date
    Public Function GettingServerDate() As DateTime
        Try
            Dim serverDate As DateTime
            Dim dt As New DataTable
            dt = GetDataTable("Select getDate() as CurrentDate")
            dt.AcceptChanges()

            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    serverDate = dt.Rows(0).Item("CurrentDate")
                Else
                    serverDate = DateTime.Now
                End If
            End If

            Return serverDate

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'End Task#27072015

    'Task#01082015 Send Employee Attendance Report via Email 
    Public Sub SendEmployeeAttendanceViaEmail()
        GetEmailConfig = New EmailSettingDAL().GetEmailSetting(String.Empty, getConfigValueByType("DefaultEmailId").ToString)
        If GetEmailConfig Is Nothing Then Exit Sub

        Dim objCon As New OleDbConnection(Con.ConnectionString)
        Dim trans As OleDbTransaction
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        trans = objCon.BeginTransaction()

        Try
            If My.Computer.Network.IsAvailable Then
                Dim strSQL As String = String.Empty
                Dim cmd As New OleDbCommand
                cmd.Connection = objCon
                cmd.Transaction = trans
                cmd.CommandTimeout = 120
                cmd.CommandType = CommandType.Text

                strSQL = "select * from tblAttendanceAlert where Status='Pending' And Email <> '' "
                cmd.CommandText = strSQL

                Dim dt As New DataTable
                Dim adapt As New OleDbDataAdapter(cmd)

                adapt.Fill(dt)
                dt.AcceptChanges()

                If dt Is Nothing Then Exit Sub
                If dt.Rows.Count = 0 Then Exit Sub




                If Not dt Is Nothing Then
                    If dt.Rows.Count > 0 Then
                        For Each row As DataRow In dt.Rows
                            Dim sendEmail As New MailMessage()

                            sendEmail.To.Add(row("Email").ToString)
                            'sendEmail.To.Add("ahmadsharif0017@gmail.com")
                            sendEmail.Subject = "Attendance Report"
                            sendEmail.Body = row("Message").ToString
                            sendEmail.From = New MailAddress(GetEmailConfig(0).Email)

                            sendEmail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
                            Dim Client As New SmtpClient(GetEmailConfig(0).SmtpServer)
                            Client.Port = GetEmailConfig(0).port
                            'Client.Host = GetEmailConfig(0).Host

                            Dim EmailPwd As String = Decrypt(GetEmailConfig(0).EmailPassword)
                            Client.Credentials = New Net.NetworkCredential(GetEmailConfig(0).Email, EmailPwd)
                            Client.EnableSsl = IIf(GetEmailConfig(0).ssl = True, True, False)
                            Client.DeliveryMethod = SmtpDeliveryMethod.Network
                            Client.Send(sendEmail)

                            strSQL = String.Empty
                            strSQL = "Update tblAttendanceAlert set Status='Sent' where ID=" & Val(row("ID").ToString)
                            cmd.CommandText = strSQL
                            cmd.ExecuteNonQuery()
                        Next
                    End If
                End If
            End If

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            ShowErrorMessage(ex.Message)
        Finally
            objCon.Close()
        End Try
    End Sub
    'End Task#01082015

    'Task#03082015 Checking how many Application Instances are opened in Same Machine
    Public Function RestrictSingleApplicationInstance() As Boolean
        Try
            If UBound(Diagnostics.Process.GetProcessesByName(Diagnostics.Process.GetCurrentProcess.ProcessName)) > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'End Task#03082015


    Public Function CheckInvAdjustmentDependedVoucher(ByVal VoucherId As Integer) As Boolean
        Try
            Dim dt As New DataTable
            dt = GetDataTable("Select Count(*) From InvoiceAdjustmentTable WHERE VoucherDetailId in(Select Voucher_Detail_Id From tblVoucherDetail WHERE Voucher_Id in (Select Voucher_Id From tblVoucher WHERE Voucher_Id=" & VoucherId & "))")

            dt.AcceptChanges()
            If dt Is Nothing Then
                Return False
            Else
                If dt.Rows.Count > 0 Then
                    If Val(dt.Rows(0).Item(0).ToString) Then
                        Return True
                    Else
                        Return False
                    End If

                Else
                    Return False
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Task#29082015 Getting last entered Sales Certificate
    Public Function GetLastSalesCertificate(ByVal Prefix As String) As String
        Dim objCon As New OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim objTrans As OleDbTransaction = objCon.BeginTransaction()

        Try
            Dim cmd As New OleDbCommand
            cmd.Connection = objCon
            cmd.Transaction = objTrans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text

            Dim strSQL As String = String.Empty
            strSQL = "SELECT TOP 1 IsNull(Right(Reference_No,7),0)+1 FROM SalesCertificateTable  WHERE LEFT(Reference_No," & Prefix.Length & ")=N'" & Prefix.Replace("'", "''") & "' order by SaleCertificateId desc"
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()
            Dim Serial As Integer = 0I
            Dim SerialNo As String = String.Empty
            If dt.Rows.Count > 0 Then
                If dt IsNot Nothing Then
                    Serial = CInt(dt.Rows(0).Item(0).ToString)
                Else
                    Serial = 1
                End If
            Else
                Serial = 1
            End If
            SerialNo = Prefix & Right("0000000" & CStr(Serial), 7)
            Return SerialNo

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function
    'End Task#29082015
    Public Function CreateDuplicationVoucher(Voucher_Id As Integer, strActivity As String, trans As OleDbTransaction) As Boolean

        Dim cmd As New OleDbCommand
        cmd.Connection = trans.Connection
        cmd.Transaction = trans
        cmd.CommandType = CommandType.Text
        cmd.CommandTimeout = 120

        Try


            Dim strSQL As String = String.Empty
            Dim dttmpVoucherColumns As New DataTable
            strSQL = "Select * From Information_Schema.Columns Where Table_Name='tmptblVoucher' AND Column_Name <> 'tmp_Voucher_Id'"
            dttmpVoucherColumns = GetDataTable(strSQL, trans)
            dttmpVoucherColumns.AcceptChanges()
            Dim strtmpColumns As String = String.Empty
            For Each r As DataRow In dttmpVoucherColumns.Rows
                If strtmpColumns.Length > 0 Then
                    strtmpColumns += "," & "[" & r.Item("Column_Name").ToString & "]"
                Else
                    strtmpColumns = "[" & r.Item("Column_Name").ToString & "]"
                End If
            Next


            Dim dtvouchercolumns As New DataTable
            strSQL = ""
            strSQL = "Select * From Information_Schema.Columns Where Table_Name='tblVoucher' AND column_Name in (Select Column_Name From information_schema.columns where table_name='tmptblVoucher')"
            dtvouchercolumns = GetDataTable(strSQL, trans)
            dtvouchercolumns.AcceptChanges()
            Dim strColumns As String = String.Empty
            For Each r As DataRow In dtvouchercolumns.Rows
                If strColumns.Length > 0 Then
                    strColumns += "," & "[" & r.Item("Column_Name").ToString & "]"
                Else
                    strColumns = "[" & r.Item("Column_Name").ToString & "]"
                End If
            Next


            strSQL = ""
            strSQL = "INSERT INTO tmptblVoucher(" & strtmpColumns.ToString & ") Select " & strColumns & ", Convert(DateTime,Getdate(),102) as tmp_entry_date, " & LoginUserId & ", N'" & LoginUserName.Replace("'", "''") & "', N'" & System.Environment.MachineName.ToString.Replace("'", "''") & "', N'" & strActivity.Replace("'", "''") & "'  From tblVoucher WHERE Voucher_Id=" & Voucher_Id & ""
            cmd.CommandText = ""
            cmd.CommandText = strSQL
            Dim intVoucherID As Integer = cmd.ExecuteScalar()


            Dim dttmpVoucherDetailColumns As New DataTable
            strSQL = ""
            strSQL = "Select * From Information_Schema.Columns Where Table_Name='tmptblVoucherDetail' AND Column_Name <> 'tmp_Voucher_Detail_Id'"
            dttmpVoucherDetailColumns = GetDataTable(strSQL, trans)
            dttmpVoucherDetailColumns.AcceptChanges()
            Dim strtmpDetailColumns As String = String.Empty
            For Each r As DataRow In dttmpVoucherDetailColumns.Rows
                If strtmpDetailColumns.Length > 0 Then
                    strtmpDetailColumns += "," & "[" & r.Item("Column_Name").ToString & "]"
                Else
                    strtmpDetailColumns = "[" & r.Item("Column_Name").ToString & "]"
                End If
            Next

            Dim dtvoucherDetailcolumns As New DataTable
            strSQL = ""
            strSQL = "Select * From Information_Schema.Columns Where Table_Name='tblVoucherDetail' AND column_Name in (Select Column_Name From information_schema.columns where table_name='tmptblVoucherDetail')"
            dtvoucherDetailcolumns = GetDataTable(strSQL, trans)
            dtvoucherDetailcolumns.AcceptChanges()
            Dim strDetailColumns As String = String.Empty
            For Each r As DataRow In dtvoucherDetailcolumns.Rows
                If strDetailColumns.Length > 0 Then
                    strDetailColumns += "," & "[" & r.Item("Column_Name").ToString & "]"
                Else
                    strDetailColumns = "[" & r.Item("Column_Name").ToString & "]"
                End If
            Next


            strSQL = ""
            strSQL = "INSERT INTO tmptblVoucherDetail(" & strtmpDetailColumns & ") Select Ident_Current('tmptblVoucher'), " & strDetailColumns & " From tblVoucherDetail WHERE Voucher_ID=" & Voucher_Id & ""
            cmd.CommandText = ""
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()


            'trans.Commit()

            Return True


        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally

        End Try
    End Function

    'Public Function GetCostPriceForRawMaterial(ArticleId As Integer, Optional trans As OleDbTransaction = Nothing) As DataTable
    '    Try

    '        Dim dt As New DataTable
    '        dt = GetDataTable("SP_CostPriceForRawMaterial " & ArticleId & "", trans)
    '        dt.AcceptChanges()

    '        Return dt
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    'Public Function DatabaseBackupPro() As Boolean
    '    Dim time As DateTime = New DateTime
    '    Dim flag As Boolean = False
    '    Try
    '        ServerDate()
    '        time = GetServerDate
    '        Dim day As String = getConfigValueByType("BackupScheduleDays")
    '        Dim str() As String = day.Split("|")
    '        If str(0).Length > 0 AndAlso str(1).Length > 0 Then
    '            For Each s As String In str
    '                Dim strday() As String = s.Split("^")
    '                If strday(0) = time.ToString("ddd") AndAlso Convert.ToBoolean(strday(1)) = True Then
    '                    flag = True
    '                    Exit For
    '                End If
    '            Next
    '        End If

    '        If flag = False Then
    '            Return False
    '        End If
    '        Dim query As String = String.Empty
    '        Dim dt As New DataTable
    '        query = "Select Count(*) from DatabaseBackupProcess Where (Convert(varchar, BackupDate, 102) =   Convert(datetime, '" & time.ToString("yyyy-M-d 00:00:00") & "', 102)) "
    '        dt = GetDataTable(query)
    '        dt.AcceptChanges()
    '        If dt.Rows.Count > 0 Then
    '            If dt.Rows(0).Item(0) > 0 Then
    '                flag = False
    '            Else
    '                flag = True
    '            End If
    '        End If
    '        If flag = False Then
    '            Return False
    '        End If
    '        Dim schedule As String = getConfigValueByType("BackupSuitableTime")
    '        Dim schedularr() As String = schedule.Split("^")
    '        If schedularr(0).Length > 0 AndAlso schedularr(1).Length > 0 Then
    '            Dim str1 As String = schedularr(0)
    '            If str1 = "Any" Then
    '                CreateBackup()
    '            Else
    '                Dim strTimes() As String = schedularr(1).Split("|")
    '                If strTimes(0).Length > 0 AndAlso strTimes(1).Length > 0 Then
    '                    If time.ToShortTimeString >= strTimes(0) And time.ToShortTimeString <= strTimes(1) Then
    '                        CreateBackup()
    '                    End If
    '                End If
    '            End If
    '        End If
    '        Return True
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try

    'End Function
    'Public Sub Zip(ByVal FilePath As String)
    '    Try

    '        Dim FileName As String = FilePath.Substring(FilePath.LastIndexOf("\") + 1)
    '        Dim Zip As New ZipFile
    '        Zip.AddFile(FilePath)
    '        Zip.Save(FilePath.Substring(0, FilePath.LastIndexOf("\")) & "\" & FileName.Replace(".Bak", "") & ".zip")
    '    Catch ex As Exception
    '    Finally
    '        File.Delete(FilePath)
    '    End Try
    'End Sub
    'END TASK-TFS-46
    Public Function CreateBackup() As Boolean

        Dim strDate As String = String.Empty
        Dim strBackup As String = String.Empty
        Dim buLocation As String = String.Empty
        'Dim frmPB As frmTakingDatabaseBackup

        Try

            'frmPB.Width = 400
            'frmPB.Height = 75
            'frmPB.Text = "Taking backup of data please wait ..."
            'frmPB.StartPosition = FormStartPosition.CenterScreen

            'Dim pb As New ProgressBar
            'pb.Style = ProgressBarStyle.Marquee
            'pb.Enabled = True
            'pb.Dock = DockStyle.Fill
            'frmPB.Controls.Add(pb)
            'frmTakingDatabaseBackup.Show()
            'frmTakingDatabaseBackup.BringToFront()
            'frmTakingDatabaseBackup.TopMost = True
            Application.DoEvents()

            Dim ConStrBuilder As New OleDbConnectionStringBuilder(Con.ConnectionString)
            Dim dbName As String = ConStrBuilder.Item("Initial Catalog")
            ConStrBuilder.Item("Initial Catalog") = "master"
            Dim Con1 As New OleDbConnection(ConStrBuilder.ConnectionString.ToString)
            If Con1.State = ConnectionState.Closed Then Con1.Open()
            buLocation = getConfigValueByType("DatabaseBackup").ToString()
            Application.DoEvents()

            Dim ServerName As String = ConStrBuilder.DataSource
            If ServerName = "." Then
                ServerName = Environment.MachineName
            End If
            If ServerName.Contains("\") Then
                ServerName = ServerName.Substring(0, ServerName.LastIndexOf("\"))
            End If
            Application.DoEvents()
            'If Environment.MachineName = ServerName Then
            If Not buLocation.Trim.Length > 0 Then
                '// When configuration backup path is empty

                If Not IO.Directory.Exists(Application.StartupPath & "\Database\Backup") Then
                    IO.Directory.CreateDirectory(Application.StartupPath & "\Database\Backup")
                End If
                buLocation = Application.StartupPath & "\Database\Backup"
            Else
                '// When backup path is set
                '// Checking path exist and creating folder
                If Not IO.Directory.Exists(buLocation) Then
                    Try
                        IO.Directory.CreateDirectory(buLocation)
                    Catch ex As Exception

                        If Not IO.Directory.Exists(Application.StartupPath & "\Database\Backup") Then
                            IO.Directory.CreateDirectory(Application.StartupPath & "\Database\Backup")
                        End If

                        buLocation = Application.StartupPath & "\Database\Backup"

                    End Try
                End If

            End If

            Application.DoEvents()
            Try
                '//Changing Window's login user access to write on backup directory
                AddDirectorySecurity(buLocation, "Everyone", FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception

            End Try

            Application.DoEvents()
            Dim mPassword As String = Decrypt(getConfigValueByType("DatabaseBackupPassword").ToString())
            strDate = Date.Now.Date.ToString("dd/MM/yyyy")

            Dim BackupFileName As String = String.Empty
            For i As Integer = 0 To 10

                If Not File.Exists(buLocation + "\" + dbName + strDate.Replace("/", "") + IIf(i = 0, "", "_" & i.ToString) + ".Bak") Then
                    BackupFileName = buLocation + "\" + dbName + strDate.Replace("/", "") + IIf(i = 0, "", "_" & i.ToString) + ".Bak"
                    Exit For
                End If

            Next

            Application.DoEvents()
            If BackupFileName.Trim.Length = 0 Then
                Exit Function
            End If
            'strBackup = "Backup Database " & dbName & " To Disk='" & buLocation + "\" + dbName + strDate.Replace("/", "") + ".Bak" & "' WITH MEDIAPASSWORD = '" & mPassword & "'"
            strBackup = "Backup Database " & dbName & " To Disk='" & BackupFileName & "'"
            Dim SqlCommand As OleDbCommand = New OleDbCommand(strBackup, Con1)
            SqlCommand.CommandTimeout = 600
            SqlCommand.ExecuteNonQuery()
            Application.DoEvents()
            HasBackup = True
            Con1.Close()
            ConStrBuilder.Item("Initial Catalog") = dbName
            Dim time As DateTime = New DateTime
            ServerDate()
            time = GetServerDate
            Dim Con2 As New OleDbConnection(ConStrBuilder.ConnectionString.ToString)
            If Con2.State = ConnectionState.Closed Then Con2.Open()
            Application.DoEvents()

            Dim SqlCommand1 As OleDbCommand = New OleDbCommand()
            SqlCommand1.Connection = Con2
            SqlCommand1.CommandText = "Insert into DatabaseBackupProcess(BackupDate, Status, UserID, System) Values( Convert(datetime, '" & time.ToString("yyyy-M-d 00:00:00") & "', 102), 1, " & LoginUserId & ", N'" & Environment.MachineName.ToString() & "') "
            SqlCommand1.ExecuteNonQuery()
            Application.DoEvents()

            'SqlCommand1.CommandText = "Insert into DatabaseBackupLog(LogDate, Status, UserID, UserName, Terminal, BackupPath, BackupFile, DbName ) Values( Convert(datetime, '" & time.ToString("yyyy-M-d 00:00:00") & "', 102), 1, " & LoginUserId & ",  '" & LoginUserName & "', N'" & Environment.MachineName.ToString() & "', '" & BackupFileName & "', '" & dbName & "') "
            ''SqlCommand.CommandTimeout = 600
            'SqlCommand1.ExecuteNonQuery()
            'Compressed("'" & Me.txtlocation.Text + "\" + Me.cmbDatabases.Text + strDate.Replace("/", "") + ".Bak" & "'")

            Application.DoEvents()

            If buLocation.Length > 0 Then
                ' Dim strFile As String = "'" & buLocation + "\" + dbName + strDate.Replace("/", "") + ".Bak" & "'"
                If mPassword <> "Error" AndAlso mPassword.Trim.Length > 0 Then
                    Zip(BackupFileName.Replace("'", ""), mPassword)
                Else
                    Zip(BackupFileName.Replace("'", ""))
                End If
            End If

            'End If
            Application.DoEvents()

        Catch ex As Exception

            Throw ex

        Finally

            '    frmTakingDatabaseBackup.Close()

        End Try
    End Function
    Sub AddDirectorySecurity(ByVal FileName As String, ByVal Account As String, ByVal Rights As FileSystemRights, ByVal ControlType As AccessControlType)
        ' Create a new DirectoryInfoobject.
        Dim dInfo As New DirectoryInfo(FileName)

        ' Get a DirectorySecurity object that represents the 
        ' current security settings.
        Dim dSecurity As DirectorySecurity = dInfo.GetAccessControl()

        ' Add the FileSystemAccessRule to the security settings. 
        dSecurity.AddAccessRule(New FileSystemAccessRule(Account, Rights, ControlType))

        ' Set the new access settings.
        dInfo.SetAccessControl(dSecurity)

    End Sub


    'Public Sub SaveInstallmentSMS()

    '    Dim objcon As New OleDbConnection(Con.ConnectionString)
    '    If objcon.State = ConnectionState.Closed Then objcon.Open()
    '    Dim trans As OleDbTransaction = objcon.BeginTransaction
    '    Try

    '        Dim cmd As New OleDbCommand
    '        cmd.Connection = objcon
    '        cmd.Transaction = trans
    '        cmd.CommandTimeout = 300
    '        cmd.CommandType = CommandType.Text

    '        ServerDate(trans)

    '        Dim strQuery As String = String.Empty

    '        strQuery = String.Empty
    '        strQuery = "Select Count(*) as Cont From InstallmentSMSProcessTable WHERE (Convert(Varchar,ProcessDate,102)=Convert(DateTime,'" & GetServerDate.ToString("yyyy-M-d 00:00:00") & "',102))"
    '        cmd.CommandText = strQuery
    '        Dim intProcessID As Integer = cmd.ExecuteScalar


    '        If intProcessID = 0 Then

    '            strQuery = String.Empty
    '            strQuery = "INSERT INTO InstallmentSMSProcessTable(ProcessDate,UserID, UserName) Values(Convert(DateTime,'" & GetServerDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102)," & LoginUserId & ",N'" & LoginUserName.Replace("'", "''") & "')"
    '            cmd.CommandText = strQuery
    '            cmd.ExecuteNonQuery()

    '            strQuery = String.Empty
    '            strQuery = "Select * from InstallmentScheduleSMSTable WHERE (Convert(Varchar,SMSDate,102) = Convert(DateTime,'" & GetServerDate.ToString("yyyy-M-d 00:00:00") & "',102)) AND SMSType In(N'Upcoming Payment',N'Due Payment')"
    '            Dim dtSMS5 As New DataTable
    '            dtSMS5 = GetDataTable(strQuery, trans)
    '            dtSMS5.AcceptChanges()

    '            If dtSMS5.Rows.Count > 0 Then

    '                For Each r As DataRow In dtSMS5.Rows

    '                    strQuery = String.Empty
    '                    strQuery = "Select Count(*) as cont From tblVoucher INNER JOIN tblVoucherDetail on tblVoucher.Voucher_ID = tblVoucherDetail.Voucher_ID WHERE voucher_type_id in(1,3,5) And IsNull(Credit_Amount,0) > 0 And tblVoucherDetail.coa_detail_id=" & Val(r.Item("CustomerID").ToString) & " AND (Convert(varchar, Voucher_date,102) = Convert(DateTime,'" & GetServerDate.ToString("yyyy-M-d 00:00:00") & "',102)) "
    '                    cmd.CommandText = strQuery
    '                    Dim intRecv As Integer = cmd.ExecuteScalar

    '                    If intRecv = 0 Then

    '                        strQuery = String.Empty
    '                        strQuery = "INSERT INTO tblSMSLog(SMSLogDate, SMSBody, SMSType, PhoneNo)" _
    '                            & " VALUES(Convert(DateTime,'" & GetServerDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),N'" & r.Item("SMSBody").ToString.Replace("'", "''") & "',N'Installment',N'" & IIf(Len(r.Item("MobileNo").ToString.Replace("'", "''")) <= 11, "92" & Right(r.Item("MobileNo").ToString.Replace("'", "''"), 10), r.Item("MobileNo").ToString.Replace("'", "''")) & "')"
    '                        cmd.CommandText = strQuery
    '                        cmd.ExecuteNonQuery()

    '                    End If

    '                Next
    '            End If


    '            strQuery = String.Empty
    '            strQuery = "Select * from InstallmentScheduleSMSTable WHERE (Convert(varchar,Year(SMSDate)) +''+ Convert(Varchar,Month(SMSDate)))='" & GetServerDate.ToString("yyyyM") & "' AND  (Convert(Varchar,SMSDate,102) <= Convert(DateTime,'" & GetServerDate.ToString("yyyy-M-d 00:00:00") & "',102))  AND SMSType=N'Overdue Payment'"
    '            Dim dtSMS7 As New DataTable
    '            dtSMS7 = GetDataTable(strQuery, trans)
    '            dtSMS7.AcceptChanges()

    '            If dtSMS7.Rows.Count > 0 Then

    '                For Each r As DataRow In dtSMS7.Rows

    '                    strQuery = String.Empty
    '                    strQuery = "Select Count(*) as cont From tblVoucher INNER JOIN tblVoucherDetail on tblVoucher.Voucher_ID = tblVoucherDetail.Voucher_ID WHERE voucher_type_id in(1,3,5) And IsNull(Credit_Amount,0) > 0 And tblVoucherDetail.coa_detail_id=" & Val(r.Item("CustomerID").ToString) & " AND  (Convert(varchar,Year(Voucher_Date)) +''+ Convert(Varchar,Month(Voucher_Date)))='" & GetServerDate.ToString("yyyyM") & "' AND (Convert(varchar, Voucher_date,102) <= Convert(DateTime,'" & GetServerDate.ToString("yyyy-M-d 00:00:00") & "',102)) "
    '                    cmd.CommandText = strQuery
    '                    Dim intRecv As Integer = cmd.ExecuteScalar

    '                    If intRecv = 0 Then

    '                        strQuery = String.Empty
    '                        strQuery = "INSERT INTO tblSMSLog(SMSLogDate, SMSBody, SMSType, PhoneNo)" _
    '                            & " VALUES(Convert(DateTime,'" & GetServerDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),N'" & r.Item("SMSBody").ToString.Replace("'", "''") & "',N'Installment',N'" & IIf(Len(r.Item("MobileNo").ToString.Replace("'", "''")) <= 11, "92" & Right(r.Item("MobileNo").ToString.Replace("'", "''"), 10), r.Item("MobileNo").ToString.Replace("'", "''")) & "')"
    '                        cmd.CommandText = strQuery
    '                        cmd.ExecuteNonQuery()

    '                    End If
    '                Next
    '            End If
    '        End If


    '        trans.Commit()


    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw ex
    '    Finally
    '        objcon.Close()
    '    End Try
    'End Sub
    Public Function DatabaseBackupPro() As Boolean
        Dim time As DateTime = New DateTime
        Dim flag As Boolean = False
        Try
            'ServerDate()
            'time = GetServerDate
            'Dim day As String = getConfigValueByType("BackupScheduleDays")
            'Dim str() As String = day.Split("|")
            'If str.Length > 0 Then
            '    For Each s As String In str
            '        Dim strday() As String = s.Split("^")
            '        If strday.Length > 0 Then
            '            If strday(0) = time.ToString("ddd") AndAlso Convert.ToBoolean(strday(1)) = True Then
            '                flag = True
            '                Exit For
            '            End If
            '        End If
            '    Next
            'End If

            'If flag = False Then
            '    Return False
            'End If
            Dim query As String = String.Empty
            Dim dt As New DataTable
            query = "Select Count(*) from DatabaseBackupProcess Where (Convert(varchar, BackupDate, 102) =   Convert(datetime, '" & Date.Now.ToString("yyyy-M-d 00:00:00") & "', 102)) "
            dt = GetDataTable(query)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then


                If dt.Rows(0).Item(0) > 0 Then
                    'flag = False
                Else
                    Dim conBuilder As New SqlConnectionStringBuilder(SQLHelper.CON_STR)
                    If GetConfigValue("DNSHostForSMS").ToString.ToUpper = Environment.MachineName.ToString.ToUpper Or conBuilder.DataSource.Contains(Environment.MachineName) Then

                        CreateBackup()

                    End If
                    '  flag = True
                End If
            End If
            'If flag = False Then
            '    Return False
            'End If
            'Dim schedule As String = getConfigValueByType("BackupSuitableTime")
            'Dim schedularr() As String = schedule.Split("^")
            'If schedularr.Length > 0 Then
            '    Dim str1 As String = schedularr(0)
            '    If str1 = "Any" Then
            '        CreateBackup()
            '    Else
            '        Dim strTimes() As String = schedularr(1).Split("|")
            '        If strTimes.Length > 0 Then
            '            BackupStartTime = strTimes(0)
            '            BackupEndTime = strTimes(1)
            '            If time.ToShortTimeString >= strTimes(0) And time.ToShortTimeString <= strTimes(1) Then
            '                CreateBackup()
            '            End If
            '        End If
            '    End If
            'End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Sub Zip(ByVal FilePath As String, Optional Password As String = "")
        Try

            Dim FileName As String = FilePath.Substring(FilePath.LastIndexOf("\") + 1)
            Dim Zip As New ZipFile
            If Password.Trim.Length > 0 Then
                Zip.Password = Password
            End If
            Zip.AddFile(FilePath)
            Zip.Save(FilePath.Substring(0, FilePath.LastIndexOf("\")) & "\" & FileName.Replace(".Bak", "") & ".zip")
        Catch ex As Exception
        Finally
            File.Delete(FilePath)
        End Try
    End Sub
    'END TASK-TFS-46
    'Public Function CreateBackup() As Boolean

    '    Dim strDate As String = String.Empty
    '    Dim strBackup As String = String.Empty
    '    Dim buLocation As String = String.Empty
    '    Try

    '        Dim ConStrBuilder As New OleDbConnectionStringBuilder(Con.ConnectionString)
    '        Dim dbName As String = ConStrBuilder.Item("Initial Catalog")
    '        ConStrBuilder.Item("Initial Catalog") = "master"
    '        Dim Con1 As New OleDbConnection(ConStrBuilder.ConnectionString.ToString)
    '        If Con1.State = ConnectionState.Closed Then Con1.Open()
    '        buLocation = getConfigValueByType("DatabaseBackup").ToString()

    '        Dim ServerName As String = ConStrBuilder.DataSource
    '        If ServerName = "." Then
    '            ServerName = Environment.MachineName
    '        End If
    '        If ServerName.Contains("\") Then
    '            ServerName.Substring(0, ServerName.LastIndexOf("\"))
    '        End If
    '        If Environment.MachineName = ServerName Then
    '            If Not buLocation = Application.StartupPath & "\Database\Backup" Then
    '                If Not buLocation.Length > 0 Then
    '                    If Not IO.Directory.Exists(Application.StartupPath & "\Database\Backup") Then
    '                        IO.Directory.CreateDirectory(Application.StartupPath & "\Database\Backup")

    '                    End If
    '                    buLocation = Application.StartupPath & "\Database\Backup"

    '                End If
    '            End If
    '            Dim mPassword As String = getConfigValueByType("DatabaseBackupPassword").ToString()
    '            strDate = Date.Now.Date.ToString("dd/MM/yyyy")
    '            strBackup = "Backup Database " & dbName & " To Disk='" & buLocation + "\" + dbName + strDate.Replace("/", "") + ".Bak" & "' WITH MEDIAPASSWORD = '" & mPassword & "'"
    '            Dim SqlCommand As OleDbCommand = New OleDbCommand(strBackup, Con1)
    '            SqlCommand.CommandTimeout = 600
    '            SqlCommand.ExecuteNonQuery()
    '            HasBackup = True
    '            Con1.Close()
    '            ConStrBuilder.Item("Initial Catalog") = dbName
    '            Dim time As DateTime = New DateTime
    '            ServerDate()
    '            time = GetServerDate
    '            Dim Con2 As New OleDbConnection(ConStrBuilder.ConnectionString.ToString)
    '            If Con2.State = ConnectionState.Closed Then Con2.Open()
    '            Dim sql As String = "Insert into DatabaseBackupProcess(BackupDate, Status, UserID, System) Values( Convert(datetime, '" & time.ToString("yyyy-M-d 00:00:00") & "', 102), 1, " & LoginUserId & ", N'" & Environment.MachineName.ToString() & "') "
    '            Dim SqlCommand1 As OleDbCommand = New OleDbCommand(sql, Con2)
    '            'SqlCommand.CommandTimeout = 600
    '            SqlCommand1.ExecuteNonQuery()
    '            'Compressed("'" & Me.txtlocation.Text + "\" + Me.cmbDatabases.Text + strDate.Replace("/", "") + ".Bak" & "'")
    '            If buLocation.Length > 0 Then
    '                Dim strFile As String = "'" & buLocation + "\" + dbName + strDate.Replace("/", "") + ".Bak" & "'"
    '                Zip(strFile.Replace("'", ""))
    '            End If
    '        End If


    '    Catch ex As Exception
    '        HasBackup = False
    '        Throw ex
    '    End Try
    'End Function

    Public Sub GetNotificationConfig(UserId As Integer, FormName As String)
        Try

            _dtNotificationActivity = New DataTable
            _dtNotificationActivity = GetDataTable("SP_NotificationConfig " & UserId & ",'" & FormName & "'")
            _dtNotificationActivity.AcceptChanges()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function GetNotificationActivityConfig(UserId As Integer, FormName As String) As DataTable
        Try
            Return GetDataTable("SP_NotificationConfig " & UserId & ",'" & FormName & "'")
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Enum enmNotificationConfig
        NotificationActivityId
        FormId
        GroupID
        UserId
        NotificationActivityName
        FormCaption
        FormModule
        SMS
        Email
        Notifications
        FormName
        AccessKey
    End Enum
    Public Sub SaveNotifications(dr As DataRow, DocNo As String, DocDate As DateTime, Msg As String, Optional AssignedTo As Integer = 0)
        Dim objCon As New OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Dim cmd As New OleDbCommand
        cmd.Connection = trans.Connection
        cmd.Transaction = trans
        cmd.CommandTimeout = 300
        cmd.CommandType = CommandType.Text
        Try

            ServerDate(trans)

            Dim dtUsers As New DataTable
            dtUsers = GetDataTable("Select User_ID, GroupId, Fax as Mobile, Email From tblUser WHERE Active=1 AND GroupId=" & Val(dr.Item("GroupId").ToString) & " " & IIf(AssignedTo > 0, " AND User_ID=" & AssignedTo & "", "") & "", trans)
            dtUsers.AcceptChanges()

            If dr.Item(enmNotificationConfig.SMS).ToString.ToUpper = "TRUE" Then
                For Each r As DataRow In dtUsers.Rows
                    SaveSMSLog(trans, dr.Item("NotificationActivityName").ToString & Chr(10) & "" & Msg, IIf(r.Item("Mobile").ToString.Length <= 11, "92" & Right(r.Item("Mobile").ToString, 10), r.Item("Mobile").ToString), "Notification")
                Next

            ElseIf dr.Item(enmNotificationConfig.Email).ToString.ToUpper = "TRUE" Then
                cmd.CommandText = String.Empty
                For Each r As DataRow In dtUsers.Rows
                    cmd.CommandText = "INSERT INTO MailSentBoxTable(ToEmail, CC, BCC, Subject, Body, Status, Attachment) " _
                                    & " VALUES(N'" & r.Item("Email").ToString.Replace("'", "''") & "', null, null, N'" & dr.Item("NotificationActivityName").ToString.Replace("'", "''") & "', N'" & Msg.Replace("'", "''") & "', N'Pending', null) SELECT @@Identity"
                    cmd.ExecuteNonQuery()
                Next
            ElseIf dr.Item(enmNotificationConfig.Notifications).ToString.ToUpper = "TRUE" Then
                cmd.CommandText = String.Empty
                cmd.CommandText = "INSERT INTO tblNotifications(NotificationActivityId, DocNo,DocDate,Remarks,EntryDate) VALUES(" & Val(dr.Item("NotificationActivityId").ToString) & ",N'" & DocNo.Replace("'", "''") & "',Convert(DateTime,'" & DocDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),N'" & Msg.Replace("'", "''") & "',Convert(datetime,'" & GetServerDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102))Select @@Identity"
                Dim id As Integer = cmd.ExecuteScalar()

                cmd.CommandText = ""
                cmd.CommandText = "INSERT INTO tblNotificationsDetail(NotificationId,UserId,ReadMsg) Select " & id & ",User_Id, 0 from tblUser where GroupId=" & LoginGroupId & " " & IIf(AssignedTo > 0, " AND User_ID=" & AssignedTo & "", "") & ""
                cmd.ExecuteNonQuery()
            End If

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Sub

    Public Sub UpdateReadNotifications()
        Dim objcon As New OleDbConnection(Con.ConnectionString)
        If objcon.State = ConnectionState.Closed Then objcon.Open()
        Dim trans As OleDb.OleDbTransaction = objcon.BeginTransaction
        Dim cmd As New OleDbCommand
        cmd.Connection = trans.Connection
        cmd.Transaction = trans
        cmd.CommandTimeout = 300
        cmd.CommandType = CommandType.Text
        Try

            cmd.CommandText = String.Empty
            cmd.CommandText = "Update tblNotificationsDetail Set ReadMsg=1 WHERE UserId =" & LoginUserId & " AND IsNull(ReadMsg,0) <> 1"
            cmd.ExecuteNonQuery()

            trans.Commit()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    'Public Function RemoveLayouts(ReleaseVersion As String, MachineName As String) As Boolean
    '    Try
    '        Dim dt As New DataTable
    '        dt = GetDataTable("Select Count(*) As cont From tblLayoutRemove WHERE Release_Version='" & ReleaseVersion.Replace("'", "''") & "' AND SystemName='" & MachineName.Replace("'", "''") & "' AND IsNull(Status,0)=1")
    '        dt.AcceptChanges()

    '        If dt.Rows.Count > 0 Then
    '            If Val(dt.Rows(0).Item(0).ToString) > 0 Then
    '                Return False
    '            Else
    '                Return True
    '            End If
    '        Else
    '            Return True
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    'Public Function GetMACAddress() As String
    '    Try
    '        Dim nics() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces()
    '        Dim sMacAddress As String = ""
    '        For Each adapter As NetworkInterface In nics

    '            If sMacAddress = "" Then
    '                'Dim properties As IPInterfaceProperties = adapter.GetIPProperties()
    '                sMacAddress = adapter.GetPhysicalAddress().ToString()
    '            End If
    '        Next
    '        Return sMacAddress
    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function
    Public Function GetMACAddress() As String
        Try
            Dim cpuID As String = String.Empty
            Dim mc As ManagementClass = New ManagementClass("Win32_NetworkAdapterConfiguration")
            If mc IsNot Nothing Then
                Dim moc As ManagementObjectCollection = mc.GetInstances()
                For Each mo As ManagementObject In moc
                    If (cpuID = String.Empty And (mo.Properties("IPEnabled").Value) = True) Then
                        cpuID = mo.Properties("MacAddress").Value.ToString()
                    End If
                Next
            End If
            Return cpuID
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetMACAddressList() As String
        Try
            Dim cpuID As String = String.Empty
            Dim mc As ManagementClass = New ManagementClass("Win32_NetworkAdapterConfiguration")
            Dim moc As ManagementObjectCollection = mc.GetInstances()
            If mc IsNot Nothing Then
                For Each mo As ManagementObject In moc
                    If (CBool(mo.Properties("IPEnabled").Value) = True) Then
                        cpuID = cpuID & mo.Properties("MacAddress").Value.ToString()
                        cpuID = cpuID & ""
                    End If
                Next
            End If
            Return cpuID
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetMotherboard() As String
        Try
            Dim mthrbrd As String = String.Empty
            Dim mos As ManagementObjectSearcher = New ManagementObjectSearcher("Select * from win32_BaseBoard")
            If mos IsNot Nothing Then
                For Each mo As ManagementObject In mos.Get()
                    mthrbrd = mthrbrd & mo("SerialNumber").ToString()
                Next
            End If
            Return mthrbrd
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetBIOS() As String
        Try
            Dim Bios As String = String.Empty
            Dim mos As ManagementObjectSearcher = New ManagementObjectSearcher("Select * from win32_BIOS")
            If mos IsNot Nothing Then
                For Each mo As ManagementObject In mos.Get()
                    Bios = Bios & mo("SerialNumber").ToString()
                Next
            End If
            Return Bios
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CreateDuplicationQuotation(ByVal QuotationId As Integer, ByVal ActivityLog As String, ByVal trans As OleDb.OleDbTransaction, Optional ByVal LoginUserId As Integer = 0, Optional ByVal LoginUserName As String = "") As Boolean

        Dim cmd As New OleDb.OleDbCommand
        cmd.Connection = trans.Connection
        cmd.Transaction = trans
        cmd.CommandType = CommandType.Text
        cmd.CommandTimeout = 120

        Try


            Dim strSQL As String = String.Empty
            Dim dttmpVoucherColumns As New DataTable
            strSQL = "Select * From Information_Schema.Columns Where Table_Name='QuotationHistory' AND Column_Name <> 'QuotationHistoryId'"
            dttmpVoucherColumns = GetDataTable(strSQL, trans)
            dttmpVoucherColumns.AcceptChanges()
            Dim strtmpColumns As String = String.Empty
            For Each r As DataRow In dttmpVoucherColumns.Rows
                If strtmpColumns.Length > 0 Then
                    strtmpColumns += "," & "[" & r.Item("Column_Name").ToString & "]"
                Else
                    strtmpColumns = "[" & r.Item("Column_Name").ToString & "]"
                End If
            Next


            Dim dtvouchercolumns As New DataTable
            strSQL = ""
            strSQL = "Select * From Information_Schema.Columns Where Table_Name='QuotationMasterTable' AND Column_Name in (Select Column_Name From information_schema.columns where table_name='QuotationHistory')"
            dtvouchercolumns = UtilityDAL.GetDataTable(strSQL, )
            dtvouchercolumns.AcceptChanges()
            Dim strColumns As String = String.Empty
            For Each r As DataRow In dtvouchercolumns.Rows
                If strColumns.Length > 0 Then
                    strColumns += "," & "[" & r.Item("Column_Name").ToString & "]"
                Else
                    strColumns = "[" & r.Item("Column_Name").ToString & "]"
                End If
            Next


            strSQL = ""
            strSQL = "INSERT INTO QuotationHistory(" & strtmpColumns.ToString & ") Select " & strColumns & ", Convert(DateTime,'" & Date.Now.ToString("yyyy-M-d hh:mm:ss tt") & "',102), " & LoginUserId & ", N'" & LoginUserName.Replace("'", "''") & "', N'" & System.Environment.MachineName.ToString.Replace("'", "''") & "',N'" & ActivityLog.Replace("'", "''") & "'  From QuotationMasterTable WHERE QuotationId=" & QuotationId & ""
            cmd.CommandText = ""
            cmd.CommandText = strSQL
            Dim intVoucherID As Integer = cmd.ExecuteScalar()


            Dim dttmpVoucherDetailColumns As New DataTable
            strSQL = ""
            strSQL = "Select * From Information_Schema.Columns Where Table_Name='QuotationDetailHistory' AND Column_Name <> 'QuotationDetailHistoryId'"
            dttmpVoucherDetailColumns = GetDataTable(strSQL, trans)
            dttmpVoucherDetailColumns.AcceptChanges()
            Dim strtmpDetailColumns As String = String.Empty
            For Each r As DataRow In dttmpVoucherDetailColumns.Rows
                If strtmpDetailColumns.Length > 0 Then
                    strtmpDetailColumns += "," & "[" & r.Item("Column_Name").ToString & "]"
                Else
                    strtmpDetailColumns = "[" & r.Item("Column_Name").ToString & "]"
                End If
            Next

            Dim dtvoucherDetailcolumns As New DataTable
            strSQL = ""
            strSQL = "Select * From Information_Schema.Columns Where Table_Name='QuotationDetailTable' AND Column_Name in (Select Column_Name From information_schema.columns where table_name='QuotationDetailHistory')"
            dtvoucherDetailcolumns = GetDataTable(strSQL, trans)
            dtvoucherDetailcolumns.AcceptChanges()
            Dim strDetailColumns As String = String.Empty
            For Each r As DataRow In dtvoucherDetailcolumns.Rows
                If strDetailColumns.Length > 0 Then
                    strDetailColumns += "," & "[" & r.Item("Column_Name").ToString & "]"
                Else
                    strDetailColumns = "[" & r.Item("Column_Name").ToString & "]"
                End If
            Next


            strSQL = ""
            ''Commented Against TFS4335
            ' strSQL = "INSERT INTO QuotationDetailHistory(" & strtmpDetailColumns & ") Select Ident_Current('QuotationHistory'), " & strDetailColumns & " From QuotationDetailTable WHERE QuotationId =" & QuotationId & ""
            strSQL = " INSERT INTO QuotationDetailHistory([QuotationHistoryId],[QuotationDetailId],[QuotationId],[LocationId],[ArticleDefId],[ArticleSize],[Sz1],[Sz2],[Sz3],[Sz4],[Sz5],[Sz6],[Sz7],[Qty],[Price],[CurrentPrice],[DeliveredQty],[SalesTax_Percentage],[SchemeQty],[Discount_Percentage],[DeliveredSchemeQty],[PurchasePrice],[PackPrice],[Pack_Desc],[Comments],[ItemDescription],[BrandName],[Specification],[RegistrationNo],[TradePrice],[Item_Info],[Pack_40Kg_Weight],[TenderSrNo],[CostPrice],[SED_Tax_Percent],[SED_Tax_Amount],[SOQuantity],[DeliveredTotalQty],[BaseCurrencyId],[BaseCurrencyRate],[CurrencyId],[CurrencyRate],[CurrencyAmount],[RequirementDescription],[PurchaseInquiryDetailId],[Alternate],[VendorQuotationDetailId],[HeadArticleId],[SerialNo],[PurchaseInquiryId]) " _
                             & " Select Ident_Current('QuotationHistory'),[QuotationDetailId],[QuotationId],[LocationId],[ArticleDefId],[ArticleSize],[Sz1],[Sz2],[Sz3],[Sz4],[Sz5],[Sz6],[Sz7],[Qty],[Price],[CurrentPrice],[DeliveredQty],[SalesTax_Percentage],[SchemeQty],[Discount_Percentage],[DeliveredSchemeQty],[PurchasePrice],[PackPrice],[Pack_Desc],[Comments],[ItemDescription],[BrandName],[Specification],[RegistrationNo],[TradePrice],[Item_Info],[Pack_40Kg_Weight],[TenderSrNo],[CostPrice],[SED_Tax_Percent],[SED_Tax_Amount],[SOQuantity],[DeliveredTotalQty],[BaseCurrencyId],[BaseCurrencyRate],[CurrencyId],[CurrencyRate],[CurrencyAmount],[RequirementDescription],[PurchaseInquiryDetailId],[Alternate] ,[VendorQuotationDetailId],[HeadArticleId],[SerialNo],[PurchaseInquiryId] From QuotationDetailTable WHERE QuotationId = " & QuotationId & " "
            cmd.CommandText = ""


            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()


            'trans.Commit()

            Return True


        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally

        End Try
    End Function
    Public Function CreateDuplicationSalesOrder(ByVal SalesOrderId As Integer, ByVal ActivityLog As String, ByVal trans As OleDb.OleDbTransaction, Optional ByVal LoginUserId As Integer = 0, Optional ByVal LoginUserName As String = "") As Boolean

        Dim cmd As New OleDb.OleDbCommand
        cmd.Connection = trans.Connection
        cmd.Transaction = trans
        cmd.CommandType = CommandType.Text
        cmd.CommandTimeout = 120

        Try


            Dim strSQL As String = String.Empty
            Dim dttmpVoucherColumns As New DataTable
            strSQL = "Select * From Information_Schema.Columns Where Table_Name='SalesOrderHistory' AND Column_Name <> 'SalesOrderHistoryId'"
            dttmpVoucherColumns = GetDataTable(strSQL, trans)
            dttmpVoucherColumns.AcceptChanges()
            Dim strtmpColumns As String = String.Empty
            For Each r As DataRow In dttmpVoucherColumns.Rows
                If strtmpColumns.Length > 0 Then
                    strtmpColumns += "," & "[" & r.Item("Column_Name").ToString & "]"
                Else
                    strtmpColumns = "[" & r.Item("Column_Name").ToString & "]"
                End If
            Next


            Dim dtvouchercolumns As New DataTable
            strSQL = ""
            strSQL = "Select * From Information_Schema.Columns Where Table_Name='SalesOrderMasterTable' AND column_Name in (Select Column_Name From information_schema.columns where table_name='SalesOrderHistory')"
            dtvouchercolumns = GetDataTable(strSQL, trans)
            dtvouchercolumns.AcceptChanges()
            Dim strColumns As String = String.Empty
            For Each r As DataRow In dtvouchercolumns.Rows
                If strColumns.Length > 0 Then
                    strColumns += "," & "[" & r.Item("Column_Name").ToString & "]"
                Else
                    strColumns = "[" & r.Item("Column_Name").ToString & "]"
                End If
            Next


            strSQL = ""
            strSQL = "INSERT INTO SalesOrderHistory(" & strtmpColumns.ToString & ") Select " & strColumns & ", Convert(DateTime,'" & Date.Now.ToString("yyyy-M-d hh:mm:ss tt") & "',102), " & LoginUserId & ", N'" & LoginUserName.Replace("'", "''") & "', N'" & System.Environment.MachineName.ToString.Replace("'", "''") & "',N'" & ActivityLog.Replace("'", "''") & "'  From SalesOrderMasterTable WHERE SalesOrderId=" & SalesOrderId & ""
            cmd.CommandText = ""
            cmd.CommandText = strSQL
            Dim intVoucherID As Integer = cmd.ExecuteScalar()


            Dim dttmpVoucherDetailColumns As New DataTable
            strSQL = ""
            strSQL = "Select * From Information_Schema.Columns Where Table_Name='SalesOrderDetailHistory' AND Column_Name <> 'SalesOrderDetailHistoryId'"
            dttmpVoucherDetailColumns = GetDataTable(strSQL, trans)
            dttmpVoucherDetailColumns.AcceptChanges()
            Dim strtmpDetailColumns As String = String.Empty
            For Each r As DataRow In dttmpVoucherDetailColumns.Rows
                If strtmpDetailColumns.Length > 0 Then
                    strtmpDetailColumns += "," & "[" & r.Item("Column_Name").ToString & "]"
                Else
                    strtmpDetailColumns = "[" & r.Item("Column_Name").ToString & "]"
                End If
            Next

            Dim dtvoucherDetailcolumns As New DataTable
            strSQL = ""
            strSQL = "Select * From Information_Schema.Columns Where Table_Name='SalesOrderDetailTable' AND Column_Name in (Select Column_Name From information_schema.columns where table_name='SalesOrderDetailHistory')"
            dtvoucherDetailcolumns = GetDataTable(strSQL, trans)
            dtvoucherDetailcolumns.AcceptChanges()
            Dim strDetailColumns As String = String.Empty
            For Each r As DataRow In dtvoucherDetailcolumns.Rows
                If strDetailColumns.Length > 0 Then
                    strDetailColumns += "," & "[" & r.Item("Column_Name").ToString & "]"
                Else
                    strDetailColumns = "[" & r.Item("Column_Name").ToString & "]"
                End If
            Next


            strSQL = ""
            strSQL = "INSERT INTO SalesOrderDetailHistory(" & strtmpDetailColumns & ") Select Ident_Current('SalesOrderHistory'), " & strDetailColumns & " From SalesOrderDetailTable WHERE SalesOrderId=" & SalesOrderId & ""
            cmd.CommandText = ""
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()


            'trans.Commit()

            Return True


        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally

        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : TFS4415 : Configuration based POS revision is require
    ''' </summary>
    ''' <param name="SalesId"></param>
    ''' <param name="ActivityLog"></param>
    ''' <param name="trans"></param>
    ''' <param name="LoginUserId"></param>
    ''' <param name="LoginUserName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CreateDuplicateSales(ByVal SalesId As Integer, ByVal ActivityLog As String, ByVal trans As OleDb.OleDbTransaction, Optional ByVal LoginUserId As Integer = 0, Optional ByVal LoginUserName As String = "") As Boolean
        Dim cmd As New OleDb.OleDbCommand
        cmd.Connection = trans.Connection
        cmd.Transaction = trans
        cmd.CommandType = CommandType.Text
        cmd.CommandTimeout = 120
        Try
            Dim strSQL As String = String.Empty
            Dim dttmpVoucherColumns As New DataTable
            strSQL = "Select * From Information_Schema.Columns Where Table_Name='SalesHistory' AND Column_Name <> 'SalesHistoryId'"
            dttmpVoucherColumns = GetDataTable(strSQL, trans)
            dttmpVoucherColumns.AcceptChanges()
            Dim strtmpColumns As String = String.Empty
            For Each r As DataRow In dttmpVoucherColumns.Rows
                If strtmpColumns.Length > 0 Then
                    strtmpColumns += "," & "[" & r.Item("Column_Name").ToString & "]"
                Else
                    strtmpColumns = "[" & r.Item("Column_Name").ToString & "]"
                End If
            Next
            Dim dtvouchercolumns As New DataTable
            strSQL = ""
            strSQL = "Select * From Information_Schema.Columns Where Table_Name='SalesMasterTable' AND column_Name in (Select Column_Name From information_schema.columns where table_name='SalesHistory')"
            dtvouchercolumns = GetDataTable(strSQL, trans)
            dtvouchercolumns.AcceptChanges()
            Dim strColumns As String = String.Empty
            Dim splitColumns() As String
            If strtmpColumns.Length > 0 Then
                splitColumns = strtmpColumns.Split(",")
            End If

            Dim Counter As Integer = 0
            For Each r As DataRow In dtvouchercolumns.Rows
                Dim tmp As String = splitColumns.GetValue(Counter).ToString
                Dim strColumn As String = "[" & r.Item("Column_Name").ToString & "]"
                If tmp = strColumn Then
                    If strColumns.Length > 0 Then
                        strColumns += "," & strColumn
                    Else
                        strColumns = strColumn
                    End If
                Else
                    If tmp <> "[tmp_Entry_Date]" AndAlso tmp <> "[tmp_User_ID]" AndAlso tmp <> "[tmp_User_Name]" AndAlso tmp <> "[tmp_Terminal]" AndAlso tmp <> "[tmp_activity_log]" Then
                        splitColumns.SetValue("", Counter)
                    End If
                End If
                Counter += 1
            Next
            strtmpColumns = String.Empty
            For Each _ColumnName As String In splitColumns
                If _ColumnName <> "" Then
                    If strtmpColumns.Length > 0 Then
                        strtmpColumns += "," & _ColumnName
                    Else
                        strtmpColumns = _ColumnName
                    End If
                End If
            Next

            strSQL = ""
            strSQL = "INSERT INTO SalesHistory(" & strtmpColumns.ToString & ") Select " & strColumns & ", Convert(DateTime,'" & Date.Now.ToString("yyyy-M-d hh:mm:ss tt") & "',102), " & LoginUserId & ", N'" & LoginUserName.Replace("'", "''") & "', N'" & System.Environment.MachineName.ToString.Replace("'", "''") & "',N'" & ActivityLog.Replace("'", "''") & "'  From SalesMasterTable WHERE SalesId=" & SalesId & ""
            cmd.CommandText = ""
            cmd.CommandText = strSQL
            Dim intVoucherID As Integer = cmd.ExecuteScalar()


            Dim dttmpVoucherDetailColumns As New DataTable
            strSQL = ""
            strSQL = "Select * From Information_Schema.Columns Where Table_Name='SalesDetailHistory' AND Column_Name <> 'SaleDetailHistoryId'"
            dttmpVoucherDetailColumns = GetDataTable(strSQL, trans)
            dttmpVoucherDetailColumns.AcceptChanges()
            Dim strtmpDetailColumns As String = String.Empty
            For Each r As DataRow In dttmpVoucherDetailColumns.Rows
                If strtmpDetailColumns.Length > 0 Then
                    strtmpDetailColumns += "," & "[" & r.Item("Column_Name").ToString & "]"
                Else
                    strtmpDetailColumns = "[" & r.Item("Column_Name").ToString & "]"
                End If
            Next

            Dim dtvoucherDetailcolumns As New DataTable
            strSQL = ""
            strSQL = "Select * From Information_Schema.Columns Where Table_Name='SalesDetailTable' AND Column_Name in (Select Column_Name From information_schema.columns where table_name='SalesDetailHistory')"
            dtvoucherDetailcolumns = GetDataTable(strSQL, trans)
            dtvoucherDetailcolumns.AcceptChanges()
            Dim strDetailColumns As String = String.Empty
            For Each r As DataRow In dtvoucherDetailcolumns.Rows
                If strDetailColumns.Length > 0 Then
                    strDetailColumns += "," & "[" & r.Item("Column_Name").ToString & "]"
                Else
                    strDetailColumns = "[" & r.Item("Column_Name").ToString & "]"
                End If
            Next

            strSQL = ""
            strSQL = "INSERT INTO SalesDetailHistory(" & strtmpDetailColumns & ") Select Ident_Current('SalesHistory'), " & strDetailColumns & " From SalesDetailTable WHERE SalesId=" & SalesId & ""
            cmd.CommandText = ""
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()

            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally

        End Try
    End Function






    Public Function GetDuplicateVouchers(VoucherId As Integer) As DataTable
        Try

            Dim dt As New DataTable
            dt = GetDataTable("Select tmp_voucher_id, voucher_id, voucher_no,voucher_date,tmp_User_Name,tmp_Entry_Date From tmptblVoucher where voucher_id=" & VoucherId & " Order By tmp_voucher_id DESC")
            dt.AcceptChanges()

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetDuplicateVouchers(VoucherNo As String) As DataTable
        Try

            Dim dt As New DataTable
            dt = GetDataTable("Select tmp_voucher_id, voucher_id, voucher_no,voucher_date,tmp_User_Name,tmp_Entry_Date From tmptblVoucher where voucher_No='" & VoucherNo & "' Order By tmp_voucher_id DESC")
            dt.AcceptChanges()

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub CreateContextMenu(VoucherID As Integer, objToolMenu As ToolStripSplitButton)
        Try
            If objToolMenu.DropDownItems.Count > 0 Then objToolMenu.DropDownItems.Clear()
            Dim dt As New DataTable
            dt = GetDuplicateVouchers(VoucherID)
            dt.AcceptChanges()

            Dim i As Integer = 1
            For Each r As DataRow In dt.Rows
                Dim toolmenuitem As New ToolStripMenuItem
                toolmenuitem.Tag = r.ItemArray(0).ToString
                toolmenuitem.Text = i & "<" & CDate(IIf(IsDBNull(r.ItemArray(5)) = True, r.ItemArray(3), r.ItemArray(5))).ToString("dd-MMM-yyyy hh:mm:ss  tt") & "<" & r.ItemArray(4).ToString
                AddHandler toolmenuitem.Click, AddressOf GetDuplivateVoucher_Click
                objToolMenu.DropDownItems.Add(toolmenuitem)

                i += 1
            Next
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub GetDuplivateVoucher_Click(sender As Object, e As EventArgs)
        Try

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub



    Public Function AvgDayBy(CustomerCode As Integer) As Double
        Dim objCon As New OleDb.OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim objtrans As OleDbTransaction = objCon.BeginTransaction
        Try


            Dim dt As New DataTable









        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''16-Nov-2015 TASK-TFS-46 Cost Price For Production And Store Issuence.
    Public Function GetCostPriceForProduction(ArticleId As Integer, Optional trans As OleDbTransaction = Nothing) As DataTable
        Try

            Dim dt As New DataTable
            dt = GetDataTable("SP_CostPriceForProduction " & ArticleId & "", trans)
            dt.AcceptChanges()

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetCostPriceForRawMaterial(ArticleId As Integer, Optional trans As OleDbTransaction = Nothing) As DataTable
        Try

            Dim dt As New DataTable
            dt = GetDataTable("SP_CostPriceForRawMaterial " & ArticleId & "", trans)
            dt.AcceptChanges()

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'END TASK-TFS-46

    Public Sub SaveInstallmentSMS()

        Dim objcon As New OleDbConnection(Con.ConnectionString)
        If objcon.State = ConnectionState.Closed Then objcon.Open()
        Dim trans As OleDbTransaction = objcon.BeginTransaction
        Try

            Dim cmd As New OleDbCommand
            cmd.Connection = objcon
            cmd.Transaction = trans
            cmd.CommandTimeout = 300
            cmd.CommandType = CommandType.Text

            ServerDate(trans)

            Dim strQuery As String = String.Empty

            strQuery = String.Empty
            strQuery = "Select Count(*) as Cont From InstallmentSMSProcessTable WHERE (Convert(Varchar,ProcessDate,102)=Convert(DateTime,'" & GetServerDate.ToString("yyyy-M-d 00:00:00") & "',102))"
            cmd.CommandText = strQuery
            Dim intProcessID As Integer = cmd.ExecuteScalar


            If intProcessID = 0 Then

                strQuery = String.Empty
                strQuery = "INSERT INTO InstallmentSMSProcessTable(ProcessDate,UserID, UserName) Values(Convert(DateTime,'" & GetServerDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102)," & LoginUserId & ",N'" & LoginUserName.Replace("'", "''") & "')"
                cmd.CommandText = strQuery
                cmd.ExecuteNonQuery()

                strQuery = String.Empty
                strQuery = "Select * from InstallmentScheduleSMSTable WHERE (Convert(Varchar,SMSDate,102) = Convert(DateTime,'" & GetServerDate.ToString("yyyy-M-d 00:00:00") & "',102)) AND SMSType In(N'Upcoming Payment',N'Due Payment')"
                Dim dtSMS5 As New DataTable
                dtSMS5 = GetDataTable(strQuery, trans)
                dtSMS5.AcceptChanges()

                If dtSMS5.Rows.Count > 0 Then

                    For Each r As DataRow In dtSMS5.Rows

                        strQuery = String.Empty
                        strQuery = "Select Count(*) as cont From tblVoucher INNER JOIN tblVoucherDetail on tblVoucher.Voucher_ID = tblVoucherDetail.Voucher_ID WHERE voucher_type_id in(1,3,5) And IsNull(Credit_Amount,0) > 0 And tblVoucherDetail.coa_detail_id=" & Val(r.Item("CustomerID").ToString) & " AND (Convert(varchar, Voucher_date,102) = Convert(DateTime,'" & GetServerDate.ToString("yyyy-M-d 00:00:00") & "',102)) "
                        cmd.CommandText = strQuery
                        Dim intRecv As Integer = cmd.ExecuteScalar

                        If intRecv = 0 Then

                            strQuery = String.Empty
                            strQuery = "INSERT INTO tblSMSLog(SMSLogDate, SMSBody, SMSType, PhoneNo)" _
                                & " VALUES(Convert(DateTime,'" & GetServerDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),N'" & r.Item("SMSBody").ToString.Replace("'", "''") & "',N'Installment',N'" & IIf(Len(r.Item("MobileNo").ToString.Replace("'", "''")) <= 11, "92" & Right(r.Item("MobileNo").ToString.Replace("'", "''"), 10), r.Item("MobileNo").ToString.Replace("'", "''")) & "')"
                            cmd.CommandText = strQuery
                            cmd.ExecuteNonQuery()

                        End If

                    Next
                End If


                strQuery = String.Empty
                strQuery = "Select * from InstallmentScheduleSMSTable WHERE (Convert(varchar,Year(SMSDate)) +''+ Convert(Varchar,Month(SMSDate)))='" & GetServerDate.ToString("yyyyM") & "' AND  (Convert(Varchar,SMSDate,102) <= Convert(DateTime,'" & GetServerDate.ToString("yyyy-M-d 00:00:00") & "',102))  AND SMSType=N'Overdue Payment'"
                Dim dtSMS7 As New DataTable
                dtSMS7 = GetDataTable(strQuery, trans)
                dtSMS7.AcceptChanges()

                If dtSMS7.Rows.Count > 0 Then

                    For Each r As DataRow In dtSMS7.Rows

                        strQuery = String.Empty
                        strQuery = "Select Count(*) as cont From tblVoucher INNER JOIN tblVoucherDetail on tblVoucher.Voucher_ID = tblVoucherDetail.Voucher_ID WHERE voucher_type_id in(1,3,5) And IsNull(Credit_Amount,0) > 0 And tblVoucherDetail.coa_detail_id=" & Val(r.Item("CustomerID").ToString) & " AND  (Convert(varchar,Year(Voucher_Date)) +''+ Convert(Varchar,Month(Voucher_Date)))='" & GetServerDate.ToString("yyyyM") & "' AND (Convert(varchar, Voucher_date,102) <= Convert(DateTime,'" & GetServerDate.ToString("yyyy-M-d 00:00:00") & "',102)) "
                        cmd.CommandText = strQuery
                        Dim intRecv As Integer = cmd.ExecuteScalar

                        If intRecv = 0 Then

                            strQuery = String.Empty
                            strQuery = "INSERT INTO tblSMSLog(SMSLogDate, SMSBody, SMSType, PhoneNo)" _
                                & " VALUES(Convert(DateTime,'" & GetServerDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),N'" & r.Item("SMSBody").ToString.Replace("'", "''") & "',N'Installment',N'" & IIf(Len(r.Item("MobileNo").ToString.Replace("'", "''")) <= 11, "92" & Right(r.Item("MobileNo").ToString.Replace("'", "''"), 10), r.Item("MobileNo").ToString.Replace("'", "''")) & "')"
                            cmd.CommandText = strQuery
                            cmd.ExecuteNonQuery()

                        End If
                    Next
                End If
            End If


            trans.Commit()


        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            objcon.Close()
        End Try
    End Sub

    Public Function RemoveLayouts(ReleaseVersion As String, MachineName As String) As Boolean
        Try
            Dim dt As New DataTable
            dt = GetDataTable("Select Count(*) As cont From tblLayoutRemove WHERE Release_Version='" & ReleaseVersion.Replace("'", "''") & "' AND SystemName='" & MachineName.Replace("'", "''") & "' AND IsNull(Status,0)=1")
            dt.AcceptChanges()

            If dt.Rows.Count > 0 Then
                If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function IsCurrencyTransaction() As Boolean
        Dim dt As New DataTable
        Dim str As String = String.Empty
        Try
            str = "Select IsNull(Min(IsNull(CurrencyId, 0)), 0) As CurrencyId From tblVoucherDetail Where CurrencyId > 0 And CurrencyId Is Not Null"
            dt = GetDataTable(str)
            dt.AcceptChanges()
            If Val(dt.Rows.Item(0).Item(0).ToString) > 0 Then
                Return True
            Else
                Return False
            End If
            'If Not dt Is Nothing Then
            '    If dt.Rows.Count > 0 Then
            '        Return True
            '    Else
            '        Return False
            '    End If
            'Else
            '    Return False
            'End If

        Catch ex As Exception

        End Try
    End Function
    Public Function GetBasicCurrencyName(ByVal currencyId As Integer) As String
        Dim str As String = String.Empty
        Dim dt As New DataTable
        Try
            str = "Select currency_code FROM tblCurrency Where currency_id =" & currencyId & ""
            dt = GetDataTable(str)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                Return dt.Rows.Item(0).Item(0).ToString
            Else
                Dim empString As String = String.Empty
                Return empString
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetPrefix(ByVal FormName As String) As String
        Dim sqlQuery As String = ""
        Dim prefixName As String = ""
        Try
            sqlQuery = "Select PrefixName From FormDocumentPrefixTable Where FormName ='" & FormName.Replace("'", "''") & "'"
            Dim dt As DataTable = GetDataTable(sqlQuery)
            If dt.Rows.Count > 0 Then
                prefixName = dt.Rows(0).Item(0).ToString
            End If
            Return prefixName
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function CheckLocationWiseMinusStock(ByVal LocationId As Integer) As Boolean
        Try
            Dim strSQL As String = " Select AllowMinusStock FROM tblDefLocation Where location_id =" & LocationId & ""
            Dim dt As DataTable = GetDataTable(strSQL)
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item(0) = False Then
                    Return False
                Else
                    Return True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub GetCrystalReportRights()
        Try
            If LoginGroup = "Administrator" Then
                IsCrystalReportPrint = True
                IsCrystalReportExport = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    IsCrystalReportPrint = False
                    IsCrystalReportExport = False
                    Exit Sub
                End If
            Else
                IsCrystalReportExport = False
                IsCrystalReportPrint = False
                For Each RightsDt As GroupRights In Rights
                    ''TASK TFS1384 replaced Crystal Print and Crystal Export with Report Print and Report Export on 07-09-2017
                    If RightsDt.FormControlName = "Report Print" Then
                        IsCrystalReportPrint = True
                    ElseIf RightsDt.FormControlName = "Report Export" Then
                        IsCrystalReportExport = True
                    End If
                    ''End TASK TFS1384
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' This method shows userid against username.
    ''' </summary>
    ''' <param name="UserName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUserId(ByVal UserName As String) As Integer
        Try
            Dim userId As String = "Select User_ID From tblUser Where User_Name ='" & Encrypt(UserName) & "'"
            Dim DT As DataTable = GetDataTable(userId)
            If DT.Rows.Count > 0 Then
                Dim UserId1 As Integer = DT.Rows(0).Item(0)
                Return UserId1
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function GetRowValuesIntoString(ByVal dt As DataTable, ByVal ColumnName As String,
                                      Optional ByVal Separator As String = ",") As String
        'Function added by Waqar Raza on 2 Feb 2018. Task ???
        'Reads all rows in the datatable and construct a string from values

        Dim ReturnValue As String = ""
        Dim Counter As Integer = 0

        For Each row As DataRow In dt.Rows
            If Counter = 0 Then
                ReturnValue = row.Item(ColumnName)
            Else
                ReturnValue += Separator & " " & row.Item(ColumnName)
            End If
            Counter += 1
        Next

        Return ReturnValue

    End Function

    Public Function GetMACAddressListNew(ByVal MACAddressList As String) As Boolean
        'Added by Syed Irfan Ahmad on 19 Feb 2018, Task No: 2411

        Try
            Dim cpuID As String = String.Empty
            Dim mc As ManagementClass = New ManagementClass("Win32_NetworkAdapterConfiguration")
            Dim moc As ManagementObjectCollection = mc.GetInstances()
            If mc IsNot Nothing Then
                For Each mo As ManagementObject In moc
                    If (CBool(mo.Properties("IPEnabled").Value) = True) Then
                        cpuID = cpuID & mo.Properties("MacAddress").Value.ToString()
                        cpuID = cpuID & ""
                    End If
                Next
            Else
                'Error reading MAC address
                MACAddressList = ""
                Return False
            End If
            frmMain.MACAddressList = cpuID
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function



    'This function will convert numbers into words. Added by Abubakar Siddiq
    Public Function NumberToWords(number As Integer) As String
        If number = 0 Then
            Return "Zero"
        End If
        If number < 0 Then
            Return "minus " & NumberToWords(Math.Abs(number))
        End If
        Dim words As String = ""
        If (number \ 1000000) > 0 Then
            words += NumberToWords(number \ 1000000) & " Million "
            number = number Mod 1000000
        End If
        If (number \ 1000) > 0 Then
            words += NumberToWords(number \ 1000) & " Thousand "
            number = number Mod 1000
        End If
        If (number \ 100) > 0 Then
            words += NumberToWords(number \ 100) & " Hundred "
            number = number Mod 100
        End If
        If number > 0 Then
            If words <> "" Then
                words += "and "
            End If
            'Dim unitsMap = New String() {"Zero", "One", "Two", "Three", "Four", "Five", _
            '    "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", _
            '    "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", _
            '    "Eighteen", "Nineteen"}
            Dim unitsMap = New String() {"Zero", "1", "2", "3", "4", "5", _
                "6", "7", "8", "9", "10", "11", _
                "12", "13", "14", "15", "16", "17", _
                "18", "19"}
            'Dim tensMap = New String() {"Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", _
            '    "Sixty", "Seventy", "Eighty", "Ninety"}
            Dim tensMap = New String() {"Zero", "10", "20", "30", "40", "50", _
                "60", "70", "80", "90"}
            If number < 20 Then
                words += unitsMap(number)
            Else
                words += tensMap(number \ 10)
                If (number Mod 10) > 0 Then
                    words += "-" & unitsMap(number Mod 10)
                End If
            End If
        End If
        Return words
    End Function

    Public Function AveragePrice(ByVal ArticleDefId As Integer, ByVal docDate As DateTime) As Decimal
        Try
            Dim str As String = "Select case when Sum(StockTable.BalanceQty) > 0 Then isNull((Sum(StockTable.BalanceAmount) / Sum(StockTable.BalanceQty)),0) " &
                                 "Else ArticleDeftable.Cost_Price " &
                                 "End " &
                                 "As Average " &
                                 "from stockmastertable " &
                                 "Inner Join " &
                                 "(select stockmastertable.StockTransId , stockdetailtable.ArticleDefId , isNull(stockdetailtable.InQty,0) As InQty , isNull(stockdetailtable.OutQty,0) As OutQty, " &
                                 "stockdetailtable.Rate , isNull(isNull(stockdetailtable.InQty,0) * isNull(stockdetailtable.Rate,0),0) As InAmount ," &
                                 "isNull(isNull(stockdetailtable.OutQty,0) * isNull(stockdetailtable.Rate,0),0) As OutAmount , " &
                                 "(isNull(isNull(stockdetailtable.InQty,0) - isNull(stockdetailtable.OutQty,0),0)) As BalanceQty , " &
                                 "Abs(isNull(isNull(isNull(stockdetailtable.InQty,0) * isNull(stockdetailtable.Rate,0),0) - " &
                                 "isNull(isNull(stockdetailtable.OutQty,0) * isNull(stockdetailtable.Rate,0),0),0)) As BalanceAmount, " &
                                 "stockmastertable.DocDate " &
                                 "from stockdetailtable " &
                                 "Left Outer Join stockmastertable On stockdetailtable.StockTransId = stockmastertable.StockTransId " &
                                 "where stockdetailtable.ArticleDefId = " & ArticleDefId & " And stockmastertable.DocDate <= '" & docDate.ToString("yyyy-MM-dd") & " 23:59:59') " &
                                 "As StockTable on stockmastertable.StockTransId = StockTable.StockTransId " &
                                 "Inner Join ArticleDeftable On StockTable.ArticleDefId = ArticleDeftable.ArticleId " &
                                 "Group by ArticleDeftable.Cost_Price"

            Dim dt As DataTable = GetDataTable(str)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For Each row As DataRow In dt.Rows
                        Return row.Item("Average")
                    Next
                Else
                    Return 0
                End If
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' TASK TFS3111
    ''' </summary>
    ''' <param name="AccountId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsParentAccount(ByVal AccountId As Integer) As Boolean
        Dim ReturnValue As Boolean = False
        Try
            Dim qryParentAccount As String = "Select COUNT(*) AS Counter From tblCOAMainSubSubDetail Where Parent_Id = " & AccountId & ""
            Dim dtParentAccount As DataTable = GetDataTable(qryParentAccount)
            If dtParentAccount.Rows.Count > 0 AndAlso dtParentAccount.Rows(0).Item(0) > 0 Then
                ReturnValue = True
            Else
                ReturnValue = False
            End If
            Return ReturnValue
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    '    Try
    'Dim cpuID As String = String.Empty
    'Dim mc As ManagementClass = New ManagementClass("Win32_NetworkAdapterConfiguration")
    'Dim moc As ManagementObjectCollection = mc.GetInstances()
    '        If mc IsNot Nothing Then
    '            For Each mo As ManagementObject In moc
    '                If (CBool(mo.Properties("IPEnabled").Value) = True) Then
    '                    cpuID = cpuID & mo.Properties("MacAddress").Value.ToString()
    '                    cpuID = cpuID & ""
    '                End If
    '            Next
    '        Else
    ''Error reading MAC address
    '            MACAddressList = ""
    '            Return False
    '        End If
    '        MACAddressList = cpuID
    '        Return True
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    Public Sub CreateTempTableAndInsertValues(ByVal dataTable As DataTable, ByVal tableName As String)
        Dim objcon As New OleDbConnection(Con.ConnectionString)
        If objcon.State = ConnectionState.Closed Then objcon.Open()
        Dim trans As OleDbTransaction = objcon.BeginTransaction
        Dim SqlQuery As String = String.Empty
        Dim StoreProcedureQuery As String = String.Empty
        Try
            Dim cmd As New OleDbCommand
            cmd.Connection = objcon
            cmd.Transaction = trans
            cmd.CommandTimeout = 300
            cmd.CommandType = CommandType.Text
            SqlQuery = " IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '" & tableName & "') " _
                       & " CREATE TABLE " & tableName & ""
            SqlQuery += "( "
            For Each column As DataColumn In dataTable.Columns
                Dim type = column.DataType.ToString
                Dim name As String = column.ColumnName
                'If name = "Total Salary" Then

                '    Dim a As String = ""

                'End If
                name = "[" & name & "]"


                Select Case type
                    Case "System.Int32"
                        type = "Int"
                        SqlQuery += " " & name & " " & type & " ,"
                    Case "System.String"
                        type = "nvarchar(300)"
                        SqlQuery += " " & name & " " & type & " ,"
                    Case "System.Double"
                        type = "float"
                        SqlQuery += " " & name & " " & type & " ,"
                    Case "System.Decimal"
                        type = "decimal"
                        SqlQuery += " " & name & " " & type & " ,"
                    Case "System.Int16"
                        type = "Int"
                        SqlQuery += " " & name & " " & type & " ,"
                    Case "System.Boolean"
                        type = "bit"
                        SqlQuery += " " & name & " " & type & ", "
                    Case "System.Byte"
                        type = "byte"
                        SqlQuery += " " & name & " " & type & ", "
                    Case "System.DateTime"
                        type = "DateTime"
                        SqlQuery += " " & name & " " & type & ", "
                    Case "System.Char"
                        type = "nvarchar(300)"
                        SqlQuery += " " & name & " " & type & ", "
                End Select

            Next
            SqlQuery = SqlQuery.Remove(SqlQuery.LastIndexOf(","))

            SqlQuery += " )"

            SqlQuery += " ELSE DROP TABLE " & tableName & " "
            'SqlQuery += " )"

            cmd.CommandText = SqlQuery
            cmd.ExecuteNonQuery()
            ''''
            SqlQuery = " IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '" & tableName & "') " _
                & " CREATE TABLE " & tableName & ""
            SqlQuery += "( "


            Dim Query = " IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='sp_SalarySheet') " _
                     & " DROP Procedure sp_SalarySheet "
            cmd.CommandText = Query
            cmd.ExecuteNonQuery()

            StoreProcedureQuery = " CREATE Procedure sp_SalarySheet " _
                     & " @ProcessId int " _
                     & " AS " _
                     & " Begin " _
                     & " SELECT "
            For Each column As DataColumn In dataTable.Columns
                Dim type = column.DataType.ToString
                Dim name As String = column.ColumnName
                If name.StartsWith("False") = False AndAlso name.StartsWith("True") = False AndAlso name.StartsWith("Fixed") = False AndAlso name.StartsWith("0") = False Then
                    StoreProcedureQuery += "SalarySheet.[" & name & "], "
                End If

                name = "[" & name & "]"
                Select Case type
                    Case "System.Int32"
                        type = "Int"
                        SqlQuery += " " & name & " " & type & " ,"
                    Case "System.String"
                        type = "nvarchar(300)"
                        SqlQuery += " " & name & " " & type & " ,"
                    Case "System.Double"
                        type = "float"
                        SqlQuery += " " & name & " " & type & " ,"
                    Case "System.Decimal"
                        type = "decimal"
                        SqlQuery += " " & name & " " & type & " ,"
                    Case "System.Int16"
                        type = "Int"
                        SqlQuery += " " & name & " " & type & " ,"
                    Case "System.Boolean"
                        type = "bit"
                        SqlQuery += " " & name & " " & type & ", "
                    Case "System.Byte"
                        type = "byte"
                        SqlQuery += " " & name & " " & type & ", "
                    Case "System.DateTime"
                        type = "DateTime"
                        SqlQuery += " " & name & " " & type & ", "
                    Case "System.Char"
                        type = "nvarchar(300)"
                        SqlQuery += " " & name & " " & type & ", "
                End Select
            Next
            SqlQuery = SqlQuery.Remove(SqlQuery.LastIndexOf(","))
            SqlQuery += " )"
            cmd.CommandText = SqlQuery
            cmd.ExecuteNonQuery()


            '' Store Procedure Creation
            'SqlQuery = " IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='sp_SalarySheet') " _
            '           & " DROP Procedure sp_SalarySheet "
            'cmd.CommandText = SqlQuery
            'cmd.ExecuteNonQuery()



            StoreProcedureQuery += " tblSalaryProcess.UserName,  tblSalaryProcess.SalaryYear, tblSalaryProcess.SalaryMonth "
            StoreProcedureQuery += " FROM tempSalarySheet AS SalarySheet LEFT OUTER JOIN tblSalaryProcess ON SalarySheet.ProcessId = tblSalaryProcess.SalaryProcessId " _
            & " WHERE SalarySheet.ProcessId =@ProcessId END "
            cmd.CommandText = StoreProcedureQuery
            cmd.ExecuteNonQuery()
            trans.Commit()
            Dim BulkInsertion As New SqlBulkCopy(SQLHelper.CON_STR, SqlBulkCopyOptions.TableLock)
            BulkInsertion.DestinationTableName = "tempSalarySheet"
            BulkInsertion.WriteToServer(dataTable)
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            objcon.Close()
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS3360 : Account and Item search with Contains/ Start With filter Require on Transaction screens
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="StartWith"></param>
    ''' <param name="Contains"></param>
    ''' <remarks></remarks>
    Public Sub UltraDropDownSearching(sender As Object, ByVal StartWith As Boolean, ByVal Contains As Boolean)
        Try
            If StartWith = True Then
                sender.AutoCompleteMode = Win.AutoCompleteMode.Suggest
                sender.AutoSuggestFilterMode = Win.AutoSuggestFilterMode.StartsWith
            End If

            If Contains = True Then
                sender.AutoCompleteMode = Win.AutoCompleteMode.Suggest
                sender.AutoSuggestFilterMode = Win.AutoSuggestFilterMode.Contains
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub SetComboBoxMaxWidth(cmb As ComboBox)

        'Start Task 3913 Saad Afzaal Adjust combo box drop down list width to longest string width 

        Dim length = 0
        Dim maxlength = 0
        Dim i As Integer = 0
        Dim g As Graphics = cmb.CreateGraphics
        Dim stringsize As New SizeF

        For i = 0 To cmb.Items.Count - 1
            'cmbCashAccount.SelectedIndex = i
            stringsize = g.MeasureString((cmb.GetItemText(cmb.Items(i))), cmb.Font)
            length = stringsize.Width
            If length > maxlength Then
                maxlength = length
            End If
        Next i

        cmb.DropDownWidth = maxlength

        'End Task

    End Sub
    ''' <summary>
    ''' TASK TFS4260 Done on 15-08-2016
    ''' </summary>
    ''' <param name="ItemId"></param>
    ''' <param name="_Date"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAverageRate(ByVal ItemId As Integer, ByVal _Date As DateTime) As Double
        Try
            Dim objDt As New DataTable
            objDt = UtilityDAL.GetDataTable("Select ArticleDefId, SUM(Isnull(InQty,0)-IsNull(OutQty,0)) as Current_Stock, SUM(((Isnull(InAmount,0)-IsNull(OutAmount,0)))) as CurrentAvgRate, Case WHEN SUM(((Isnull(InAmount,0)-IsNull(OutAmount,0)))) < 0 THEN -SUM(((Isnull(InAmount,0)-IsNull(OutAmount,0)))) WHEN SUM(((Isnull(InAmount,0)-IsNull(OutAmount,0)))) = 0  THEN 1 ELSE SUM(((Isnull(InAmount,0)-IsNull(OutAmount,0)))) END / CASE WHEN SUM(Isnull(InQty,0)-IsNull(OutQty,0)) < 0 THEN -SUM(Isnull(InQty,0)-IsNull(OutQty,0)) WHEN SUM(Isnull(InQty,0)-IsNull(OutQty,0)) = 0  THEN 1 ELSE SUM(Isnull(InQty,0)-IsNull(OutQty,0)) END AS Rate From StockDetailTable INNER JOIN StockMasterTable ON StockDetailTable.StockTransId = StockMasterTable.StockTransId WHERE ArticleDefId=" & ItemId & " And Convert(DateTime, StockMasterTable.DocDate, 102) <= Convert(Varchar, '" & _Date.ToString("yyyy-M-d 23:59:59") & "', 102) Group By ArticleDefId ")
            If objDt IsNot Nothing Then
                If objDt.Rows.Count > 0 Then
                    If objDt.Rows(0).Item("Current_Stock") > 0 Then
                        Return objDt.Rows(0).Item("Rate")
                    Else
                        Return 0
                    End If
                Else
                    Return 0
                End If
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    'Task Saad Auto Followup email on Sale Invoice

    Public Sub AutoFollwupEmail()
        Try

            Dim userIds As String = getConfigValueByType("FollowUpEmailUsers").ToString()

            Dim userIdArray() As String = userIds.Split(",")

            Dim str = String.Empty

            str = "select Case when DATEADD(Day, tblCustomer.CreditDays , SalesMasterTable.SalesDate) > getDate() then 1 else 0 End As CreditLimit , " _
            & "case when (isNull(InvoiceBasedReceiptsTable.InvoiceBasedReceiptAmount,0) + isNull(InvoiceAdjustment.AdjustmentAmount,0) " _
            & "+ isNull(VoucherTable.ReceiptAmount,0)) >= isNull(SalesMasterTable.SalesAmount,0) then 1 else 0 End As CreditAmount , isNull(tblCustomer.CreditDays,0) As CreditDays " _
            & ", SalesMasterTable.SalesId , SalesMasterTable.SalesNo , vwCOADetail.detail_code As CustomerCode , tblCustomer.CustomerName , " _
            & "SalesMasterTable.SalesDate , isNull(SalesMasterTable.SalesAmount,0) As InvoiceAmount , " _
            & "(isNull(VoucherTable.ReceiptAmount,0) + isNull(InvoiceBasedReceiptsTable.InvoiceBasedReceiptAmount,0) + isNull(InvoiceAdjustment.AdjustmentAmount,0)) as ReciptAmount , " _
            & "isNull(SalesMasterTable.SalesAmount,0) - (isNull(VoucherTable.ReceiptAmount,0) + isNull(InvoiceBasedReceiptsTable.InvoiceBasedReceiptAmount,0) + " _
            & "isNull(InvoiceAdjustment.AdjustmentAmount,0)) As BalanceAmount , DATEADD(Day, isNull(tblCustomer.CreditDays,0) , SalesMasterTable.SalesDate) As DueDate , " _
            & "isNull(VoucherTable.ReceiptAmount,0) As VoucherReceiptAmount , isNull(InvoiceBasedReceiptsTable.InvoiceBasedReceiptAmount,0) As InvoiceBasedReceiptAmount , " _
            & "isNull(InvoiceAdjustment.AdjustmentAmount,0) As AdjustAmount " _
            & "from SalesMasterTable " _
            & "Left Outer Join tblCustomer on SalesMasterTable.CustomerCode = tblCustomer.AccountId " _
            & "Left Outer Join " _
            & "(select sum(isNull(tblVoucherDetail.NetAmount,0)) as ReceiptAmount , tblVoucherDetail.InvoiceId from tblVoucherDetail " _
            & "Left outer join tblVoucher on tblVoucherDetail.voucher_id = tblVoucher.voucher_id " _
            & "where tblVoucher.voucher_type_id = 4 Or tblVoucher.voucher_type_id = 5 " _
            & "group by tblVoucherDetail.InvoiceId) " _
            & "As VoucherTable on SalesMasterTable.SalesId = VoucherTable.InvoiceId " _
            & "Left Outer Join " _
            & "(select sum(isNull(InvoiceBasedReceiptsDetails.ReceiptAmount,0)) as InvoiceBasedReceiptAmount , InvoiceBasedReceiptsDetails.InvoiceId " _
            & "from InvoiceBasedReceiptsDetails group by InvoiceBasedReceiptsDetails.InvoiceId) " _
            & "As InvoiceBasedReceiptsTable on SalesMasterTable.SalesId = InvoiceBasedReceiptsTable.InvoiceId " _
            & "Left Outer Join " _
            & "(select sum(isNull(InvoiceAdjustmentTable.AdjustmentAmount,0)) as AdjustmentAmount , InvoiceAdjustmentTable.InvoiceId from InvoiceAdjustmentTable " _
            & "group by InvoiceAdjustmentTable.InvoiceId) As InvoiceAdjustment on SalesMasterTable.SalesId = InvoiceAdjustment.InvoiceId Left Outer Join vwCOADetail On SalesMasterTable.CustomerCode = vwCOADetail.coa_detail_id " _
            '& "where SalesMasterTable.SalesId = 792"

            Dim dt As New DataTable

            dt = GetDataTable(str)

            Dim dtCopy As New DataTable
            Dim dtUser As New DataTable
            dtResult = New DataTable

            dtResult.Columns.Add("SalesId", System.Type.GetType("System.Int32"))
            dtResult.Columns.Add("SalesNo", System.Type.GetType("System.String"))
            dtResult.Columns.Add("CustomerCode", System.Type.GetType("System.Int32"))
            dtResult.Columns.Add("CustomerName", System.Type.GetType("System.String"))
            dtResult.Columns.Add("SalesDate", System.Type.GetType("System.DateTime"))
            dtResult.Columns.Add("InvoiceAmount", System.Type.GetType("System.Double"))
            dtResult.Columns.Add("ReceiptAmount", System.Type.GetType("System.Double"))
            dtResult.Columns.Add("BalanceAmount", System.Type.GetType("System.Double"))
            dtResult.Columns.Add("DueDate", System.Type.GetType("System.DateTime"))

            If dt.Rows.Count > 0 Then

                For Each row As DataRow In dt.Rows
                    If Val(row.Item("CreditLimit").ToString) = 0 AndAlso Val(row.Item("CreditAmount").ToString) = 0 Then

                        Dim R As DataRow = dtResult.NewRow

                        R("SalesId") = Val(row.Item("SalesId").ToString)
                        R("SalesNo") = row.Item("SalesNo").ToString
                        R("CustomerCode") = Val(row.Item("CustomerCode").ToString)
                        R("CustomerName") = row.Item("CustomerName").ToString
                        R("SalesDate") = row.Item("SalesDate")
                        R("InvoiceAmount") = Val(row.Item("InvoiceAmount").ToString)
                        R("ReceiptAmount") = Val(row.Item("ReciptAmount").ToString)
                        R("BalanceAmount") = Val(row.Item("BalanceAmount").ToString)
                        R("DueDate") = row.Item("DueDate")

                        dtResult.Rows.Add(R)

                    End If
                Next
            End If

            If dtResult.Rows.Count > 0 Then

                dtCopy = dtResult.Copy

                For Each Id As String In userIdArray
                    Dim userId As Integer = CInt(Id)
                    str = "select Email from tblUser where User_ID = " & userId
                    dtUser = GetDataTable(str)
                    UserEmail = dtUser.Rows(0).Item("Email").ToString

                    For Each Row As DataRow In dtCopy.Rows
                        checkEmailLog(Row.Item("SalesNo").ToString, userId)
                    Next

                    If dtResult.Rows.Count > 0 Then

                        GetTemplate("Sales Followup")

                        If EmailTemplate.Length > 0 Then

                            GetEmailData()
                            FormatStringBuilder(dtEmail)

                            CreateOutLookMail()

                            For Each Row As DataRow In dtResult.Rows
                                ' Saad Save Email Log while sending email to selected users
                                SaveEmailLog(Row.Item("SalesNo").ToString, UserEmail, "frmSales", "Save")

                                ' Saad Save Activity Log while sending email to selected users
                                SaveActivityLog("Email", "Sales Followup Email", EnumActions.Save, LoginUserId, EnumRecordType.Configuration, Row.Item("SalesNo").ToString, True)

                            Next

                        End If

                    Else
                        dtResult = dtCopy.Copy
                    End If

                Next
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ' Task Saad Check If email is exist in Email Log
    Public Sub checkEmailLog(DocNo As String, userId As Integer)
        Dim dtUser As New DataTable
        Dim Email As String = String.Empty
        Dim str = String.Empty

        Dim dtEmailLog As New DataTable

        str = "select Email from tblUser where User_ID = " & userId
        dtUser = GetDataTable(str)
        Email = dtUser.Rows(0).Item("Email").ToString

        str = "select DocumentNo , Email from tblEmailLog where Email = '" & Email & "'"
        dtEmailLog = GetDataTable(str)

        For Each Row As DataRow In dtEmailLog.Rows
            If DocNo = Row.Item("DocumentNo").ToString AndAlso Email = Row.Item("Email").ToString Then

                For Each Row1 As DataRow In dtResult.Rows
                    If Row1.Item("SalesNo").ToString = DocNo Then
                        dtResult.Rows.Remove(Row1)
                        dtResult.AcceptChanges()
                        Exit For
                    End If
                Next
            End If
        Next
    End Sub

    ' Task set data in email grid to send email to perticular user
    Public Sub GetEmailData()
        Dim Dr As DataRow
        Try
            For Each Row As DataRow In dtResult.Rows
                Dr = dtEmail.NewRow
                For Each col As String In AllFields
                    If Row.Table.Columns.Contains(col) Then
                        Dr.Item(col) = Row.Item(col).ToString
                    End If
                Next
                dtEmail.Rows.Add(Dr)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ' Task get Email Template
    Public Sub GetTemplate(ByVal Title As String)
        Dim Fields As String = String.Empty

        Try
            dtEmail = New DataTable
            EmailTemplate = EmailDAL.GetTemplate(Title)
            If EmailTemplate.Length > 0 Then
                Dim i, j As Integer
                i = EmailTemplate.IndexOf("<Fields>") + "<Fields>".Length
                j = EmailTemplate.IndexOf("</Fields>") - i

                Dim Searched As String = "</Fields>"
                AfterFieldsElement = EmailTemplate.Substring(EmailTemplate.IndexOf(Searched) + Searched.Length)
                Fields = EmailTemplate.Substring(i, j)
                Dim WOAtTheRate As String = Fields.Replace("@", "")
                Dim WOSpace As String = WOAtTheRate.Replace(" ", "")
                Dim IndexOfFieldElement As Integer = EmailTemplate.IndexOf("<Fields>")
                If IndexOfFieldElement > 0 Then
                    EmailTemplate = EmailTemplate.Remove(IndexOfFieldElement)
                End If
                AllFields = New List(Of String)

                dtEmail.Columns.Clear()
                For Each word As String In WOSpace.Split(",")
                    Dim TrimSpace As String = word.Trim()
                    If dtResult.Columns.Contains(TrimSpace) Then
                        If dtEmail.Columns.Contains(TrimSpace) = False Then
                            dtEmail.Columns.Add(TrimSpace)
                        End If
                        If AllFields.Contains(TrimSpace) = False Then
                            AllFields.Add(TrimSpace)
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FormatStringBuilder(ByVal dt As DataTable)
        Try
            html = New StringBuilder
            html.Append(EmailTemplate)
            html.Append("<table border = '1'>")

            'Building the Header row.
            html.Append("<tr>")
            For Each column As DataColumn In dt.Columns
                Dim ColumnName As String = ""
                Dim Pattern = "([a-z?])[_ ]?([A-Z])"
                If column.ColumnName = "SerialNo" Then
                    ColumnName = "Sr#"
                Else
                    ColumnName = Regex.Replace(column.ColumnName, Pattern, "$1 $2")
                End If
                html.Append("<th>")
                html.Append(ColumnName)
                html.Append("</th>")
            Next
            html.Append("</tr>")
            For Each row As DataRow In dt.Rows
                html.Append("<tr>")
                For Each column As DataColumn In dt.Columns
                    html.Append("<td>")
                    If column.ColumnName = "RequirementDescription" Then
                        Dim var = row(column.ColumnName).ToString.Split(System.Environment.NewLine.ToCharArray())
                        Dim Lines As String = ""
                        For Each Line As String In var
                            If Line.Length > 0 Then
                                If Lines.Length > 0 Then
                                    Lines += "<br/>" & Line
                                Else
                                    Lines = Line
                                End If
                            End If
                        Next
                        html.Append(Lines)
                    Else
                        html.Append(row(column.ColumnName))
                    End If
                    html.Append("</td>")
                Next
                html.Append("</tr>")
            Next

            'Table end.
            html.Append("</table>")
            html.Append(AfterFieldsElement)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ' Task Saad Send Email on outlook to particular user
    Public Sub CreateOutLookMail(Optional ByVal _AutoEmail As Boolean = False)
        Try
            Dim oApp As Outlook.Application = New Outlook.Application
            Dim mailItem As Outlook.MailItem = oApp.CreateItem(Outlook.OlItemType.olMailItem)
            mailItem.Subject = "Follow up email of Sales Invoice"
            mailItem.To = UserEmail
            mailItem.Importance = Outlook.OlImportance.olImportanceNormal
            'If _AutoEmail = False Then
            'mailItem.Display(mailItem)
            'mailItem.HTMLBody = html.ToString + mailItem.HTMLBody
            'EmailBody = html.ToString

            'Else
            mailItem.HTMLBody = html.ToString + mailItem.HTMLBody
            EmailBody = html.ToString
            mailItem.Send()
            'End If
            mailItem = Nothing
            oApp = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' TASK TFS4391
    ''' </summary>
    ''' <param name="SalesId"></param>
    ''' <param name="ActivityLog"></param>
    ''' <param name="trans"></param>
    ''' <param name="LoginUserId"></param>
    ''' <param name="LoginUserName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    'Public Function CreateDuplicateSales(ByVal SalesId As Integer, ByVal ActivityLog As String, ByVal trans As OleDb.OleDbTransaction, Optional ByVal LoginUserId As Integer = 0, Optional ByVal LoginUserName As String = "") As Boolean
    '    Dim cmd As New OleDb.OleDbCommand
    '    cmd.Connection = trans.Connection
    '    cmd.Transaction = trans
    '    cmd.CommandType = CommandType.Text
    '    cmd.CommandTimeout = 120
    '    Try
    '        Dim strSQL As String = String.Empty
    '        Dim dttmpVoucherColumns As New DataTable
    '        strSQL = "Select * From Information_Schema.Columns Where Table_Name='SalesHistory' AND Column_Name <> 'SalesHistoryId'"
    '        dttmpVoucherColumns = GetDataTable(strSQL, trans)
    '        dttmpVoucherColumns.AcceptChanges()
    '        Dim strtmpColumns As String = String.Empty
    '        For Each r As DataRow In dttmpVoucherColumns.Rows
    '            If strtmpColumns.Length > 0 Then
    '                strtmpColumns += "," & "[" & r.Item("Column_Name").ToString & "]"
    '            Else
    '                strtmpColumns = "[" & r.Item("Column_Name").ToString & "]"
    '            End If
    '        Next
    '        Dim dtvouchercolumns As New DataTable
    '        strSQL = ""
    '        strSQL = "Select * From Information_Schema.Columns Where Table_Name='SalesMasterTable' AND column_Name in (Select Column_Name From information_schema.columns where table_name='SalesHistory')"
    '        dtvouchercolumns = GetDataTable(strSQL, trans)
    '        dtvouchercolumns.AcceptChanges()
    '        Dim strColumns As String = String.Empty

    '        Dim strArrayColumns() As String
    '        Dim splitColumns() As String
    '        If strtmpColumns.Length > 0 Then
    '            splitColumns = strtmpColumns.Split(",")
    '        End If

    '        Dim Counter As Integer = 0
    '        For Each r As DataRow In dtvouchercolumns.Rows
    '            Dim tmp As String = splitColumns.GetValue(Counter).ToString
    '            Dim strColumn As String = "[" & r.Item("Column_Name").ToString & "]"
    '            If tmp = strColumn Then

    '                strArrayColumns.SetValue(strColumn, Counter)
    '                'If strColumns.Length > 0 Then
    '                '    strColumns += "," & strColumn
    '                'Else
    '                '    strColumns = strColumn
    '                'End If
    '            Else
    '                If tmp <> "[tmp_Entry_Date]" AndAlso tmp <> "[tmp_User_ID]" AndAlso tmp <> "[tmp_User_Name]" AndAlso tmp <> "[tmp_Terminal]" AndAlso tmp <> "[tmp_activity_log]" Then
    '                    splitColumns.SetValue("", Counter)
    '                End If
    '            End If
    '                Counter += 1
    '        Next
    '        strtmpColumns = String.Empty
    '        For Each _ColumnName As String In splitColumns
    '            If _ColumnName <> "" Then
    '                If strtmpColumns.Length > 0 Then
    '                    strtmpColumns += "," & _ColumnName
    '                Else
    '                    strtmpColumns = _ColumnName
    '                End If
    '            End If
    '        Next

    '        For Each _ColumnName As String In strArrayColumns
    '            If _ColumnName <> "" Then
    '                If strColumns.Length > 0 Then
    '                    strColumns += "," & _ColumnName
    '                Else
    '                    strColumns = _ColumnName
    '                End If
    '            End If
    '        Next

    '        strSQL = ""
    '        strSQL = "INSERT INTO SalesHistory(" & strtmpColumns.ToString & ") Select " & strColumns & ", Convert(DateTime,'" & Date.Now.ToString("yyyy-M-d hh:mm:ss tt") & "',102), " & LoginUserId & ", N'" & LoginUserName.Replace("'", "''") & "', N'" & System.Environment.MachineName.ToString.Replace("'", "''") & "',N'" & ActivityLog.Replace("'", "''") & "'  From SalesMasterTable WHERE SalesId=" & SalesId & ""
    '        cmd.CommandText = ""
    '        cmd.CommandText = strSQL
    '        Dim intVoucherID As Integer = cmd.ExecuteScalar()


    '        Dim dttmpVoucherDetailColumns As New DataTable
    '        strSQL = ""
    '        strSQL = "Select * From Information_Schema.Columns Where Table_Name='SalesDetailHistory' AND Column_Name <> 'SaleDetailHistoryId'"
    '        dttmpVoucherDetailColumns = GetDataTable(strSQL, trans)
    '        dttmpVoucherDetailColumns.AcceptChanges()
    '        Dim strtmpDetailColumns As String = String.Empty
    '        For Each r As DataRow In dttmpVoucherDetailColumns.Rows
    '            If strtmpDetailColumns.Length > 0 Then
    '                strtmpDetailColumns += "," & "[" & r.Item("Column_Name").ToString & "]"
    '            Else
    '                strtmpDetailColumns = "[" & r.Item("Column_Name").ToString & "]"
    '            End If
    '        Next

    '        Dim dtvoucherDetailcolumns As New DataTable
    '        strSQL = ""
    '        strSQL = "Select * From Information_Schema.Columns Where Table_Name='SalesDetailTable' AND Column_Name in (Select Column_Name From information_schema.columns where table_name='SalesDetailHistory')"
    '        dtvoucherDetailcolumns = GetDataTable(strSQL, trans)
    '        dtvoucherDetailcolumns.AcceptChanges()
    '        Dim strDetailColumns As String = String.Empty
    '        Dim strArrayDetailColumns() As String

    '        Dim splitDetailColumns() As String
    '        If strtmpDetailColumns.Length > 0 Then
    '            splitDetailColumns = strtmpDetailColumns.Split(",")
    '        End If

    '        'For Each r As DataRow In dtvoucherDetailcolumns.Rows
    '        '    If strDetailColumns.Length > 0 Then
    '        '        strDetailColumns += "," & "[" & r.Item("Column_Name").ToString & "]"
    '        '    Else
    '        '        strDetailColumns = "[" & r.Item("Column_Name").ToString & "]"
    '        '    End If
    '        'Next
    '        Dim Counter1 As Integer = 0
    '        For Each r As DataRow In dtvoucherDetailcolumns.Rows
    '            Dim tmp As String = splitDetailColumns.GetValue(Counter1).ToString
    '            Dim strColumn As String = "[" & r.Item("Column_Name").ToString & "]"
    '            If tmp = strColumn Then
    '                strArrayDetailColumns.SetValue(strColumn, Counter1)
    '                'If strColumns.Length > 0 Then
    '                '    strDetailColumns += "," & strColumn
    '                'Else
    '                '    strDetailColumns = strColumn
    '                'End If
    '            Else
    '                'If tmp <> "[tmp_Entry_Date]" AndAlso tmp <> "[tmp_User_ID]" AndAlso tmp <> "[tmp_User_Name]" AndAlso tmp <> "[tmp_Terminal]" AndAlso tmp <> "[tmp_activity_log]" Then
    '                splitDetailColumns.SetValue("", Counter1)
    '                'End If
    '            End If
    '            Counter1 += 1
    '        Next
    '        strtmpDetailColumns = String.Empty
    '        For Each _ColumnName As String In splitDetailColumns
    '            If _ColumnName <> "" Then
    '                If strtmpDetailColumns.Length > 0 Then
    '                    strtmpDetailColumns += "," & _ColumnName
    '                Else
    '                    strtmpDetailColumns = _ColumnName
    '                End If
    '            End If
    '        Next
    '        For Each _ColumnName As String In strArrayDetailColumns
    '            If _ColumnName <> "" Then
    '                If strDetailColumns.Length > 0 Then
    '                    strDetailColumns += "," & _ColumnName
    '                Else
    '                    strDetailColumns = _ColumnName
    '                End If
    '            End If
    '        Next


    '        strSQL = ""
    '        strSQL = "INSERT INTO SalesDetailHistory(" & strtmpDetailColumns & ") Select Ident_Current('SalesHistory'), " & strDetailColumns & " From SalesDetailTable WHERE SalesId=" & SalesId & ""
    '        cmd.CommandText = ""
    '        cmd.CommandText = strSQL
    '        cmd.ExecuteNonQuery()


    '        'trans.Commit()

    '        Return True


    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw ex
    '    Finally

    '    End Try
    'End Function
    ''' <summary>
    ''' TASK TFS4695
    ''' </summary>
    ''' <param name="ConfigValue"></param>
    ''' <param name="ConfigType"></param>
    ''' <remarks></remarks>
    Public Sub UpdateConfigValue(ByVal ConfigValue As String, ByVal ConfigType As String)
        Dim objCon As New OleDbConnection(Con.ConnectionString)
        Dim trans As OleDbTransaction = Nothing

        Try
            Dim strSQL As String = String.Empty

            If objCon.State = ConnectionState.Closed Then objCon.Open()
            trans = objCon.BeginTransaction()

            Dim cmd As New OleDbCommand
            cmd.Connection = objCon
            cmd.Transaction = trans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text

            strSQL = "Update configvaluestable SET config_value = N'" & ConfigValue & "' WHERE config_type=N'" & ConfigType & "'"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()

            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Sub
End Module
