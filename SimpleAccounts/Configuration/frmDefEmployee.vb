''2-May-2014 Task:2593 Imran Ali  Enhancement in employee attendance.
''17-Jul-2014 Task:2747 Imran Ali Add new field reference employee (Burraq)
'13-May-2015 Task 20150505 Ali Ansari Add prefix in name and save layout of history
'2015-05-20 Task#20150511 Adding Region,Zone and Belt in Customer Form
''07-June-2015 Task# 07A-June-2015 Ahmad Sharif: Refine update query
''07-June-2015 Task# 07B-June-2015 AhmadSharif: Combo box fill when edit
'Task# G08062015 Ahmad Sharif 
'09-June-2015 Task# A1-09-06-2015 Ahmad Sharif: Add exception messages
'13-Jun-2015 Task#1 13-Jun-2015 Ahmad Sharif: Check if Path greater then zero then if path doesn't exist then create path first
'16-Jun-2015 Task# 201506014  Ali Ansari Add Reports of Experence, Joining and Bank Account Opening
'17-Jun-2015 Task# 201506015  Ali Ansari Add Employee Type and Confirmation Date 
'17-Jun-2015 Task# 2016060016  Ali Ansari Save Promotion On Changing Designation
'18-Aug-2015 Task#18082015 Ahmad Sharif add factor rate field in design and save,update
'' 16-9-2015 TASKM169151 Imran Ali Add new field Value_In in Salary Detail Grid for the purpose of Value in Fixed or Percentage On Gross Salary
'6-March-2018 Task2645 Ayesha Rehman :Contract ending date of emplyee require on Employee Enrollment
'' TFS3566 : Ayesha Rehman : User right based Cost Centers in dropdowns and also history according to that Cost Centers on Employee Enrollment Configuration based.
'' TFS4433 : Ayesha Rehman : 13-09-2018 There is no attachment button in the employee configuration screen, where we can upload the concerned contractual documents. 
Imports System.Drawing.Image
Imports System.Data.oledb
Imports System.Math
Imports System.IO
' 'Task No 2544 Add New Function to retrive data on employee information Report and to show  employee information Report 
Public Class frmDefEmployee
    Implements IGeneral

    Dim EmployeeAccountId As Integer
    Dim EmpSalaryAccountId As Integer = 0I
    Dim EmpReceiveableAccountId As Integer = 0I
    Dim EmpInsuranceAccountId As Integer = 0I
    Dim EmpWHTaxAccountId As Integer = 0I
    Dim EmpEOBIAccountId As Integer = 0I
    Dim EmpESSIAccountId As Integer = 0I
    Dim EmpGratuityAccountId As Integer = 0I
    Dim EmpAllowanceAccountId As Integer = 0I
    Dim EmpSalariesAccountId As Double = 0I
    Dim _strImagePath As String = String.Empty
    Dim IsLoadedForm As Boolean = False
    Dim intEmployeeAccountHeadId As Integer = 0I
    Dim blnEmpSimpleAcHead As Boolean = False
    Dim blnEmpDeptAcHead As Boolean = False
    Dim EmpAccountList As List(Of SBModel.EmployeeAccountsBE)
    Dim identity As Integer = 0I
    Dim flgCostCenterRights As Boolean = False ''TFS3566
    Dim arrFile As List(Of String) ''TFS4433

    Enum enmEmployee
        EmployeeID
        Prefix 'Task #20150505
        EmployeeName
        EmployeeCode
        FatherName
        NIC
        NTN
        Gender
        MartialStatus
        Religion
        DOB
        City_ID
        CityName
        'Task# 20150511 Adding Index of State,Region,Zone,Belt
        StateID
        StateName
        RegionID
        RegionName
        ZoneId
        ZoneName
        BeltId
        BeltName
        'Task# 20150511 Adding Index of State,Region,Zone,Belt
        Address
        Phone
        Mobile
        Email
        JoiningDate
        ConfirmationDate 'Task#201506015 Add Confirmation Date
        EmployeeTypeId    'Task#201506015 Add  EmployeeType Id
        EmployeeTypeName  'Task#201506015 Add  Employee Type Name 
        DepartmentID
        EmployeeDesignationName
        EmployeeDeptName
        Division_Name
        PayRollDivisionName
        Desig_ID
        Salary
        Active
        LeavingDate
        Comments
        SalePerson
        EmpAccountId
        EmpAccountDesc
        Reference
        PessiNo
        EobiNo
        EmpPicture
        ShiftGroupId
        EmpReceiveableAccountId
        EmpReceiveableAccountDesc
        Sale_Order_Person
        AlternateEmpNo
        AttendanceDate
        ContractEndingDate ''TFS2645
        Family_Code
        ID_Remark
        Qualification
        Blood_Group
        Language
        Social_Security_No
        Insurance_No
        Emergency_No
        Passport_No
        BankAccount_No
        NIC_Place
        Domicile
        Relation
        InReplacementNewCode
        Previous_Code
        Last_Update
        JobType
        Dept_Division
        PayRoll_Division
        RefEmployeeId 'Task:2746 Added Index
        BankAcName
        'Factor          'Task#18082015 add Factor rate in enum by Ahmad Sharif
        ShiftGroupName
        ShiftName
        NoOfAttachments
        ReportingTo
        OfficialEmail
    End Enum
    ''' <summary>
    ''' Ali Faisal : Exception handling on 05-Nov-2018
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmDefEmployee_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F4 Then
                'btnSave_Click(Nothing, Nothing)
                SaveToolStripButton_Click(Nothing, Nothing)    'Task#1 13-Jun-2015 call save method on keydown F4
            End If
            If e.KeyCode = Keys.Escape Then
                btnCancel_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmEmployee_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Application.DoEvents()
            If getConfigValueByType("EmployeeHeadAccountId").ToString <> "Error" Then
                intEmployeeAccountHeadId = Val(getConfigValueByType("EmployeeHeadAccountId"))
            End If
            If getConfigValueByType("EmpSimpleAccountHead").ToString <> "Error" Then
                blnEmpSimpleAcHead = getConfigValueByType("EmpSimpleAccountHead")
            End If
            If getConfigValueByType("EmpDepartmentAccountHead").ToString <> "Error" Then
                blnEmpDeptAcHead = getConfigValueByType("EmpDepartmentAccountHead")
            End If
            ''Start TFS3566  19-06-2018 : Ayesha Rehman
            If Not getConfigValueByType("RightBasedCostCenters").ToString = "Error" Then
                flgCostCenterRights = getConfigValueByType("RightBasedCostCenters")
            End If
            ''End TFS3566
            FillCombo()
            ''Task#1 13-Jun-2015 fill combo boxes
            FillCombobox("State")
            FillCombobox("Region")
            FillCombobox("Zone")
            FillCombobox("Belt")
            FillCombobox("City")
            ''End Task#1 13-Jun-2015
            'Task:2747 Call Ref Employee 
            FillCombo("RefEmployee")
            FillCombo("CostCentre")
            'End Task:2747 
            RefreshControls()
            DisplayRecord()
            IsLoadedForm = True
            ShowHeaderCompany()
            Get_All(frmModProperty.Tags)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub frmDefEmployee_LocationChanged(sender As Object, e As EventArgs) Handles Me.LocationChanged

    End Sub

    ''Task#1 13-Jun-2015 on shown fill combo boxes and chkActive to true and dtpLeaving check to false
    Private Sub frmDefEmployee_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.chkActive.Checked = True         'chkActive checked by default
            Me.dtpLeaving.Checked = False       'dtpLeaving unchecked by default
            Me.DtpConfirmation.Checked = False  'dtpLeaving unchecked by default Ali Ansari Task#201506015
            'Fill combo boxes
            FillCombobox("State")
            FillCombobox("Region")
            FillCombobox("Zone")
            FillCombobox("Belt")
            FillCombobox("City")
            'Altered Against Task#201506015 Call Fill Combo of EmployeeType Ali Ansari
            FillCombobox("EmployeeType")
            'Altered Against Task#201506015 Call Fill Combo of EmployeeType Ali Ansari

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Function for filling combo boxes
    ''' <summary>
    ''' Ali Faisal : Exception handling on 05-Nov-2018
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Private Sub FillCombobox(Optional ByVal Condition As String = "")
        Try
            If Condition = "State" Then
                FillDropDown(Me.cmbState, "select * from tblListState")
            ElseIf Condition = "Region" Then
                FillDropDown(Me.cmbRegion, "select * from tblListRegion")
            ElseIf Condition = "Zone" Then
                FillDropDown(Me.cmbZone, "Select * from tblListZone")
            ElseIf Condition = "Belt" Then
                FillDropDown(Me.cmbBelt, "select * from tblListBelt")
            ElseIf Condition = "City" Then
                FillDropDown(Me.ddlCity, "select * from tblListCity")
            ElseIf Condition = "ReportingTo" Then
                FillDropDown(Me.cmbReportingTo, "SELECT Employee_ID, Employee_Name, Employee_Code, EmployeeDeptName, EmployeeDesignationName, Gender, CityName, Phone FROM EmployeesView WHERE Active = 1")
                'Altered Against Task#201506014 Call Fill Combo of EmployeeType Ali Ansari
            ElseIf Condition = "EmployeeType" Then
                FillDropDown(Me.CmbEmployeeType, "select EmployeeTypeId,EmployeeTypeName from tblEmployeeType order by EmployeeTypeName")
                'Altered Against Task#201506014 Call Fill Combo of EmployeeType Ali Ansari
            ElseIf Condition = "EmpBank" Then
                Dim str As String = String.Empty
                str = "select Distinct Bank_Ac_Name AS BankName, Bank_Ac_Name As [Bank Name] from tblDefEmployee where Bank_Ac_Name <> '' order by Bank_Ac_Name ASC"
                FillDropDown(cmbBankAcName, str, False)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''Task#1 13-Jun-2015
    ''' <summary>
    ''' Ali Faisal : Exception handling on 05-Nov-2018
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DisplayRecord()
        Dim str As String
        'Before against task:2747
        'str = "SELECT Emp.Employee_ID, Emp.Employee_Name, Emp.Employee_Code, Emp.Father_Name, Emp.NIC, Emp.NTN, Emp.Gender, Emp.Martial_Status, " _
        '      & " Emp.Religion, Emp.DOB, emp.City_ID, City.CityName, Emp.Address, Emp.Phone, Emp.Mobile, Emp.Email, Emp.Joining_Date, emp.Dept_ID, Dept.EmployeeDeptName, " _
        '      & " emp.Desig_ID, Desig.EmployeeDesignationName, Emp.Salary, Emp.Active, Emp.Leaving_Date, Emp.Comments,Emp.SalePerson, IsNull(Emp.EmpSalaryAccountId,0) as EmpSalaryAccountId, COA.detail_title, Emp.Reference, Emp.PessiNo, Emp.EobiNo, Emp.EmpPicture, ISNULL(Emp.ShiftGroupId,0) as ShiftGroupId, ISNULL(Emp.ReceiveableAccountId,0) as ReceiveableAccountId, COA1.detail_title as ReceiveableAccountDesc, ISNULL(Emp.Sale_Order_Person,0) as Sale_Order_Person, Emp.AlternateEmpNo, Emp.AttendanceDate,Family_Code,ID_Remark,Qualification,Blood_Group,Language,Social_Security_No,Insurance_No,Emergency_No,Passport_No,BankAccount_No,NIC_Place,Domicile,Relation,InReplacementNewCode,Previous_Code,Last_Update,JobType,ISNULL(Dept_Division,0) as Dept_Divisin,ISNULL(PayRoll_Division,0) as Payroll_Division " _
        '      & " FROM dbo.tblDefEmployee Emp LEFT OUTER JOIN " _
        '      & " dbo.EmployeeDesignationDefTable Desig ON Emp.Desig_ID = Desig.EmployeeDesignationId LEFT OUTER JOIN " _
        '      & " dbo.tblListCity City ON Emp.City_ID = City.CityId LEFT OUTER JOIN EmployeeDeptDefTable Dept On Dept.EmployeeDeptId = Emp.Dept_Id LEFT OUTER JOIN VWCOADETAIL COA ON COA.coa_detail_id = emp.EmpSalaryAccountId  LEFT OUTER JOIN VWCOADETAIL COA1 ON COA1.coa_detail_id = Emp.ReceiveableAccountId  "
        'Task:2747 Added Field RefEmployeeId
        'Marked Against Task# 20150505 Ali Ansari
        'str = "SELECT Emp.Employee_ID, Emp.Employee_Name, Emp.Employee_Code, Emp.Father_Name, Emp.NIC, Emp.NTN, Emp.Gender, Emp.Martial_Status, " _
        '            & " Emp.Religion, Emp.DOB, emp.City_ID, City.CityName, Emp.Address, Emp.Phone, Emp.Mobile, Emp.Email, Emp.Joining_Date, emp.Dept_ID, Desig.EmployeeDesignationName, Dept.EmployeeDeptName, tblDefDivision.Division_Name, tblDefPayRollDivision.PayRollDivisionName, " _
        '            & " emp.Desig_ID,  Emp.Salary, Emp.Active, Emp.Leaving_Date, Emp.Comments,Emp.SalePerson, IsNull(Emp.EmpSalaryAccountId,0) as EmpSalaryAccountId, COA.detail_title, Emp.Reference, Emp.PessiNo, Emp.EobiNo, Emp.EmpPicture, ISNULL(Emp.ShiftGroupId,0) as ShiftGroupId, ISNULL(Emp.ReceiveableAccountId,0) as ReceiveableAccountId, COA1.detail_title as ReceiveableAccountDesc, ISNULL(Emp.Sale_Order_Person,0) as Sale_Order_Person, Emp.AlternateEmpNo, Emp.AttendanceDate,Family_Code,ID_Remark,Qualification,Blood_Group,Language,Social_Security_No,Insurance_No,Emergency_No,Passport_No,BankAccount_No,NIC_Place,Domicile,Relation,InReplacementNewCode,Previous_Code,Last_Update,JobType,ISNULL(Dept_Division,0) as Dept_Divisin,ISNULL(PayRoll_Division,0) as Payroll_Division, IsNull(Emp.RefEmployeeId,0) as RefEmployeeId, Emp.Bank_Ac_Name " _
        '            & " FROM dbo.tblDefEmployee Emp LEFT OUTER JOIN " _
        '            & " dbo.EmployeeDesignationDefTable Desig ON Emp.Desig_ID = Desig.EmployeeDesignationId LEFT OUTER JOIN " _
        '            & " dbo.tblListCity City ON Emp.City_ID = City.CityId LEFT OUTER JOIN EmployeeDeptDefTable Dept On Dept.EmployeeDeptId = Emp.Dept_Id LEFT OUTER JOIN VWCOADETAIL COA ON COA.coa_detail_id = emp.EmpSalaryAccountId  LEFT OUTER JOIN VWCOADETAIL COA1 ON COA1.coa_detail_id = Emp.ReceiveableAccountId LEFT OUTER JOIN tblDefDivision On tblDefDivision.Division_Id = Emp.Dept_Division LEFT OUTER JOIN tblDefPayRollDivision On tblDefPayRollDivision.PayRollDivision_Id = Emp.PayRoll_Division"

        'Marked Against Task# 20150511 Ali Ansari
        'Altered Against Task# 20150505 Ali Ansari

        'str = "SELECT Emp.Employee_ID,emp.prefix, Emp.Employee_Name, Emp.Employee_Code, Emp.Father_Name, Emp.NIC, Emp.NTN, Emp.Gender, Emp.Martial_Status, " _
        '     & " Emp.Religion, Emp.DOB, emp.City_ID, City.CityName, Emp.Address, Emp.Phone, Emp.Mobile, Emp.Email, Emp.Joining_Date, emp.Dept_ID, Desig.EmployeeDesignationName, Dept.EmployeeDeptName, tblDefDivision.Division_Name, tblDefPayRollDivision.PayRollDivisionName, " _
        '     & " emp.Desig_ID,  Emp.Salary, Emp.Active, Emp.Leaving_Date, Emp.Comments,Emp.SalePerson, IsNull(Emp.EmpSalaryAccountId,0) as EmpSalaryAccountId, COA.detail_title, Emp.Reference, Emp.PessiNo, Emp.EobiNo, Emp.EmpPicture, ISNULL(Emp.ShiftGroupId,0) as ShiftGroupId, ISNULL(Emp.ReceiveableAccountId,0) as ReceiveableAccountId, COA1.detail_title as ReceiveableAccountDesc, ISNULL(Emp.Sale_Order_Person,0) as Sale_Order_Person, Emp.AlternateEmpNo, Emp.AttendanceDate,Family_Code,ID_Remark,Qualification,Blood_Group,Language,Social_Security_No,Insurance_No,Emergency_No,Passport_No,BankAccount_No,NIC_Place,Domicile,Relation,InReplacementNewCode,Previous_Code,Last_Update,JobType,ISNULL(Dept_Division,0) as Dept_Divisin,ISNULL(PayRoll_Division,0) as Payroll_Division, IsNull(Emp.RefEmployeeId,0) as RefEmployeeId, Emp.Bank_Ac_Name " _
        '     & " FROM dbo.tblDefEmployee Emp LEFT OUTER JOIN " _
        '     & " dbo.EmployeeDesignationDefTable Desig ON Emp.Desig_ID = Desig.EmployeeDesignationId LEFT OUTER JOIN " _
        '     & " dbo.tblListCity City ON Emp.City_ID = City.CityId LEFT OUTER JOIN EmployeeDeptDefTable Dept On Dept.EmployeeDeptId = Emp.Dept_Id LEFT OUTER JOIN VWCOADETAIL COA ON COA.coa_detail_id = emp.EmpSalaryAccountId  LEFT OUTER JOIN VWCOADETAIL COA1 ON COA1.coa_detail_id = Emp.ReceiveableAccountId LEFT OUTER JOIN tblDefDivision On tblDefDivision.Division_Id = Emp.Dept_Division LEFT OUTER JOIN tblDefPayRollDivision On tblDefPayRollDivision.PayRollDivision_Id = Emp.PayRoll_Division"
        ''Altered Against Task# 20150505 Ali Ansari

        'Marked Against Task# 20150511 Ali Ansari
        'Altered Against Task# 20150511 Ali Ansari Add State,Region,Zone and Belt
        'Marked Against Task#201506014 Display Employee Type and Leaving Date Ali Ansari
        'str = "SELECT Emp.Employee_ID,emp.prefix, Emp.Employee_Name, Emp.Employee_Code, Emp.Father_Name, Emp.NIC, Emp.NTN, Emp.Gender, Emp.Martial_Status, " _
        '     & " Emp.Religion, Emp.DOB, emp.City_ID, City.CityName,isnull(emp.stateid,0) as StateId,tblListState.statename,isnull(emp.regionid,0) as RegionId,tblListRegion.RegionName,isnull(emp.zoneid,0) as ZoneId,tblListZone.Zonename,isnull(emp.beltid,0) as BeltId,tblListBelt.Beltname, " _
        '     & " Emp.Address, Emp.Phone, Emp.Mobile, Emp.Email, Emp.Joining_Date, emp.Dept_ID, Desig.EmployeeDesignationName, Dept.EmployeeDeptName, tblDefDivision.Division_Name, tblDefPayRollDivision.PayRollDivisionName, " _
        '     & " emp.Desig_ID,  Emp.Salary, Emp.Active, Emp.Leaving_Date, Emp.Comments,Emp.SalePerson, IsNull(Emp.EmpSalaryAccountId,0) as EmpSalaryAccountId, COA.detail_title, Emp.Reference, Emp.PessiNo, Emp.EobiNo, Emp.EmpPicture, ISNULL(Emp.ShiftGroupId,0) as ShiftGroupId, ISNULL(Emp.ReceiveableAccountId,0) as ReceiveableAccountId, COA1.detail_title as ReceiveableAccountDesc, ISNULL(Emp.Sale_Order_Person,0) as Sale_Order_Person, Emp.AlternateEmpNo, Emp.AttendanceDate,Family_Code,ID_Remark,Qualification,Blood_Group,Language,Social_Security_No,Insurance_No,Emergency_No,Passport_No,BankAccount_No,NIC_Place,Domicile,Relation,InReplacementNewCode,Previous_Code,Last_Update,JobType,ISNULL(Dept_Division,0) as Dept_Divisin,ISNULL(PayRoll_Division,0) as Payroll_Division, IsNull(Emp.RefEmployeeId,0) as RefEmployeeId, Emp.Bank_Ac_Name " _
        '     & " FROM dbo.tblDefEmployee Emp LEFT OUTER JOIN " _
        '     & " dbo.EmployeeDesignationDefTable Desig ON Emp.Desig_ID = Desig.EmployeeDesignationId LEFT OUTER JOIN " _
        '     & " dbo.tblListCity City ON Emp.City_ID = City.CityId LEFT OUTER JOIN EmployeeDeptDefTable Dept On Dept.EmployeeDeptId = Emp.Dept_Id LEFT OUTER JOIN " _
        '     & " VWCOADETAIL COA ON COA.coa_detail_id = emp.EmpSalaryAccountId  LEFT OUTER JOIN " _
        '     & " VWCOADETAIL COA1 ON COA1.coa_detail_id = Emp.ReceiveableAccountId LEFT OUTER JOIN " _
        '     & " tblDefDivision On tblDefDivision.Division_Id = Emp.Dept_Division LEFT OUTER JOIN " _
        '     & " tblDefPayRollDivision On tblDefPayRollDivision.PayRollDivision_Id = Emp.PayRoll_Division " _
        '     & "left join dbo.tblListState on emp.stateid = dbo.tblListState.stateid " _
        '     & "left join dbo.tblListregion on emp.regionid = dbo.tblListRegion.Regionid " _
        '     & "left join dbo.tblListzone on emp.Zoneid = dbo.tblListZone.Zoneid " _
        '     & "left join dbo.tblListbelt on emp.beltid = dbo.tblListBelt.beltid "

        'Altered Against Task# 20150511 Ali Ansari
        'End Task:2747
        'Marked Against Task#201506014 Display Employee Type and Leaving Date Ali Ansari

        'Altered Against Task#201506014 Display Employee Type and Leaving Date Ali Ansari
        'str = "SELECT Emp.Employee_ID,emp.prefix, Emp.Employee_Name, Emp.Employee_Code, Emp.Father_Name, Emp.NIC, Emp.NTN, Emp.Gender, Emp.Martial_Status, " _
        '             & " Emp.Religion, Emp.DOB, emp.City_ID, City.CityName,isnull(emp.stateid,0) as StateId,tblListState.statename,isnull(emp.regionid,0) as RegionId,tblListRegion.RegionName,isnull(emp.zoneid,0) as ZoneId,tblListZone.Zonename,isnull(emp.beltid,0) as BeltId,tblListBelt.Beltname, " _
        '             & " Emp.Address, Emp.Phone, Emp.Mobile, Emp.Email, Emp.Joining_Date,emp.ConfirmationDate,IsNull(emp.Factor_Rate,0) as Factor,emp.EmployeeTypeId,tblemployeetype.EmployeeTypeName, emp.Dept_ID, Desig.EmployeeDesignationName, Dept.EmployeeDeptName, tblDefDivision.Division_Name, tblDefPayRollDivision.PayRollDivisionName, " _
        '             & " emp.Desig_ID,  Emp.Salary, Emp.Active, Emp.Leaving_Date, Emp.Comments,Emp.SalePerson, IsNull(Emp.EmpSalaryAccountId,0) as EmpSalaryAccountId, COA.detail_title, Emp.Reference, Emp.PessiNo, Emp.EobiNo, Emp.EmpPicture, ISNULL(Emp.ShiftGroupId,0) as ShiftGroupId, ISNULL(Emp.ReceiveableAccountId,0) as ReceiveableAccountId, COA1.detail_title as ReceiveableAccountDesc, ISNULL(Emp.Sale_Order_Person,0) as Sale_Order_Person, Emp.AlternateEmpNo, Emp.AttendanceDate,Family_Code,ID_Remark,Qualification,Blood_Group,Language,Social_Security_No,Insurance_No,Emergency_No,Passport_No,BankAccount_No,NIC_Place,Domicile,Relation,InReplacementNewCode,Previous_Code,Last_Update,JobType,ISNULL(Dept_Division,0) as Dept_Divisin,ISNULL(PayRoll_Division,0) as Payroll_Division, IsNull(Emp.RefEmployeeId,0) as RefEmployeeId, Emp.Bank_Ac_Name " _
        '             & " FROM dbo.tblDefEmployee Emp LEFT OUTER JOIN " _
        '             & " dbo.EmployeeDesignationDefTable Desig ON Emp.Desig_ID = Desig.EmployeeDesignationId LEFT OUTER JOIN " _
        '             & " dbo.tblListCity City ON Emp.City_ID = City.CityId LEFT OUTER JOIN EmployeeDeptDefTable Dept On Dept.EmployeeDeptId = Emp.Dept_Id LEFT OUTER JOIN " _
        '             & " VWCOADETAIL COA ON COA.coa_detail_id = emp.EmpSalaryAccountId  LEFT OUTER JOIN " _
        '             & " VWCOADETAIL COA1 ON COA1.coa_detail_id = Emp.ReceiveableAccountId LEFT OUTER JOIN " _
        '             & " tblDefDivision On tblDefDivision.Division_Id = Emp.Dept_Division LEFT OUTER JOIN " _
        '             & " tblDefPayRollDivision On tblDefPayRollDivision.PayRollDivision_Id = Emp.PayRoll_Division " _
        '             & "left join dbo.tblListState on emp.stateid = dbo.tblListState.stateid " _
        '             & "left join dbo.tblListregion on emp.regionid = dbo.tblListRegion.Regionid " _
        '             & "left join dbo.tblListzone on emp.Zoneid = dbo.tblListZone.Zoneid " _
        '             & "left join dbo.tblListbelt on emp.beltid = dbo.tblListBelt.beltid " _
        '             & "left join dbo.tblemployeetype on emp.EmployeeTypeId = dbo.tblemployeetype.EmployeeTypeId "

        'str = "SELECT Emp.Employee_ID,emp.prefix, Emp.Employee_Name, Emp.Employee_Code, Emp.Father_Name, Emp.NIC, Emp.NTN, Emp.Gender, Emp.Martial_Status, " _
        '     & " Emp.Religion, Emp.DOB, emp.City_ID, City.CityName,isnull(emp.stateid,0) as StateId,tblListState.statename,isnull(emp.regionid,0) as RegionId,tblListRegion.RegionName,isnull(emp.zoneid,0) as ZoneId,tblListZone.Zonename,isnull(emp.beltid,0) as BeltId,tblListBelt.Beltname, " _
        '     & " Emp.Address, Emp.Phone, Emp.Mobile, Emp.Email, Emp.Joining_Date,emp.ConfirmationDate,emp.EmployeeTypeId,tblemployeetype.EmployeeTypeName, emp.Dept_ID, Desig.EmployeeDesignationName, Dept.EmployeeDeptName, tblDefDivision.Division_Name, tblDefPayRollDivision.PayRollDivisionName, " _
        '     & " emp.Desig_ID,  Emp.Salary, Emp.Active, Emp.Leaving_Date, Emp.Comments,Emp.SalePerson, IsNull(Emp.EmpSalaryAccountId,0) as EmpSalaryAccountId, COA.detail_title, Emp.Reference, Emp.PessiNo, Emp.EobiNo, Emp.EmpPicture, ISNULL(Emp.ShiftGroupId,0) as ShiftGroupId, ISNULL(Emp.ReceiveableAccountId,0) as ReceiveableAccountId, COA1.detail_title as ReceiveableAccountDesc, ISNULL(Emp.Sale_Order_Person,0) as Sale_Order_Person, Emp.AlternateEmpNo, Emp.AttendanceDate,Family_Code,ID_Remark,Qualification,Blood_Group,Language,Social_Security_No,Insurance_No,Emergency_No,Passport_No,BankAccount_No,NIC_Place,Domicile,Relation,InReplacementNewCode,Previous_Code,Last_Update,JobType,ISNULL(Dept_Division,0) as Dept_Divisin,ISNULL(PayRoll_Division,0) as Payroll_Division, IsNull(Emp.RefEmployeeId,0) as RefEmployeeId, Emp.Bank_Ac_Name " _
        '     & " FROM dbo.tblDefEmployee Emp LEFT OUTER JOIN " _
        '     & " dbo.EmployeeDesignationDefTable Desig ON Emp.Desig_ID = Desig.EmployeeDesignationId LEFT OUTER JOIN " _
        '     & " dbo.tblListCity City ON Emp.City_ID = City.CityId LEFT OUTER JOIN EmployeeDeptDefTable Dept On Dept.EmployeeDeptId = Emp.Dept_Id LEFT OUTER JOIN " _
        '     & " VWCOADETAIL COA ON COA.coa_detail_id = emp.EmpSalaryAccountId  LEFT OUTER JOIN " _
        '     & " VWCOADETAIL COA1 ON COA1.coa_detail_id = Emp.ReceiveableAccountId LEFT OUTER JOIN " _
        '     & " tblDefDivision On tblDefDivision.Division_Id = Emp.Dept_Division LEFT OUTER JOIN " _
        '     & " tblDefPayRollDivision On tblDefPayRollDivision.PayRollDivision_Id = Emp.PayRoll_Division " _
        '     & "left join dbo.tblListState on emp.stateid = dbo.tblListState.stateid " _
        '     & "left join dbo.tblListregion on emp.regionid = dbo.tblListRegion.Regionid " _
        '     & "left join dbo.tblListzone on emp.Zoneid = dbo.tblListZone.Zoneid " _
        '     & "left join dbo.tblListbelt on emp.beltid = dbo.tblListBelt.beltid " _
        '     & "left join dbo.tblemployeetype on emp.EmployeeTypeId = dbo.tblemployeetype.EmployeeTypeId "
        'Altered Against Task#201506014 Display Employee Type and Leaving Date Ali Ansari
        'Ali Faisal : TFS1250 : Add CNIC Expiry Date column
        'Ayesha Rehman : TFS2645: Add Contract Ending Date column
        Try
            str = "SELECT Emp.Employee_ID,emp.prefix, Emp.Employee_Name, Emp.Employee_Code, Emp.Father_Name, Emp.NIC, Emp.NTN, Emp.Gender, Emp.Martial_Status, " _
             & " Emp.Religion, Emp.DOB, emp.City_ID, emp.CityName,isnull(emp.stateid,0) as StateId,emp.statename,isnull(emp.regionid,0) as RegionId,emp.RegionName,isnull(emp.zoneid,0) as ZoneId,emp.Zonename,isnull(emp.beltid,0) as BeltId,tblListBelt.Beltname, " _
             & " Emp.Address, Emp.Phone, Emp.Mobile, Emp.Email, Emp.Joining_Date,emp.ConfirmationDate,emp.EmployeeTypeId,tblemployeetype.EmployeeTypeName, emp.Dept_ID, Emp.EmployeeDesignationName, Emp.EmployeeDeptName, tblDefDivision.Division_Name, tblDefPayRollDivision.PayRollDivisionName, " _
             & " emp.Desig_ID,  Emp.Salary, Emp.Active, Emp.Leaving_Date, Emp.Comments,Emp.SalePerson, IsNull(Emp.EmpSalaryAccountId,0) as EmpSalaryAccountId, COA.detail_title, Emp.Reference, Emp.PessiNo, Emp.EobiNo, Emp.EmpPicture, ISNULL(Emp.ShiftGroupId,0) as ShiftGroupId, ISNULL(Emp.ReceiveableAccountId,0) as ReceiveableAccountId, COA1.detail_title as ReceiveableAccountDesc, ISNULL(Emp.Sale_Order_Person,0) as Sale_Order_Person, Emp.AlternateEmpNo, Emp.AttendanceDate, Emp.ContractEndingDate, Family_Code,ID_Remark,Qualification,Blood_Group,Language,Social_Security_No,Insurance_No,Emergency_No,Passport_No,BankAccount_No,NIC_Place,Domicile,Relation,InReplacementNewCode,Previous_Code,Last_Update,JobType,ISNULL(Dept_Division,0) as Dept_Divisin,ISNULL(PayRoll_Division,0) as Payroll_Division, IsNull(Emp.RefEmployeeId,0) as RefEmployeeId, Emp.Bank_Ac_Name,Emp.ShiftGroupName as [Shift Group], Emp.ShiftName as [Shift], Emp.CostCentre, dbo.tbldefcostcenter.Name As CostCentreName, Emp.IsDailyWages, Emp.CNICExpiryDate , IsNull([No_of_Attachment],0) as [No_of_Attachment], Emp.ReportingTo,Emp.OfficialEmail " _
             & " FROM dbo.EmployeesView Emp LEFT OUTER JOIN " _
             & " VWCOADETAIL COA ON COA.coa_detail_id = emp.EmpSalaryAccountId  LEFT OUTER JOIN " _
             & " VWCOADETAIL COA1 ON COA1.coa_detail_id = Emp.ReceiveableAccountId LEFT OUTER JOIN " _
             & " tblDefDivision On tblDefDivision.Division_Id = Emp.Dept_Division LEFT OUTER JOIN " _
             & " tblDefPayRollDivision On tblDefPayRollDivision.PayRollDivision_Id = Emp.PayRoll_Division " _
             & "left join dbo.tblListbelt on emp.beltid = dbo.tblListBelt.beltid " _
             & "left join dbo.tbldefcostcenter on emp.CostCentre = dbo.tbldefcostcenter.CostCenterID " _
             & "left join dbo.tblemployeetype on emp.EmployeeTypeId = dbo.tblemployeetype.EmployeeTypeId " _
            & "LEFT OUTER JOIN(Select Count(*) as [No_of_Attachment], DocId From DocumentAttachment WHERE Source='" & Me.Name & "' Group By DocId) Att On Att.DocId =  Emp.Employee_ID "
            ''Start TFS3566
            If flgCostCenterRights = True Then
                str += " where emp.CostCentre in (Select CostCentre_Id  FROM  tblUserCostCentreRights  where UserID = " & LoginUserId & " and (CostCentre_Id is Not Null) )"
            End If
            ''End TFS3566


            FillGridEx(grdSaved, str)
            Me.grdSaved.RetrieveStructure()
            grdSaved.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic

            For i As Integer = 0 To grdSaved.RootTable.Columns.Count - 1
                grdSaved.RootTable.Columns(i).Visible = False
            Next
            If Not grdSaved.RowCount > 0 Then Exit Sub
            grdSaved.RootTable.Columns(enmEmployee.EmployeeID).Visible = False
            grdSaved.RootTable.Columns(enmEmployee.EmpReceiveableAccountId).Visible = False
            grdSaved.RootTable.Columns(enmEmployee.EmployeeName).Visible = True
            grdSaved.RootTable.Columns(enmEmployee.EmployeeCode).Visible = True
            grdSaved.RootTable.Columns(enmEmployee.FatherName).Visible = True
            grdSaved.RootTable.Columns(enmEmployee.NIC).Visible = True
            grdSaved.RootTable.Columns(enmEmployee.Salary).Visible = True
            grdSaved.RootTable.Columns(enmEmployee.CityName).Visible = True
            grdSaved.RootTable.Columns(enmEmployee.Gender).Visible = True
            grdSaved.RootTable.Columns(enmEmployee.ShiftGroupName).Visible = True
            grdSaved.RootTable.Columns(enmEmployee.ShiftName).Visible = True
            ''Start TFS4433
            Me.grdSaved.RootTable.Columns("No_of_Attachment").Visible = True
            Me.grdSaved.RootTable.Columns("No_of_Attachment").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            Me.grdSaved.RootTable.Columns("No_of_Attachment").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            Me.grdSaved.RootTable.Columns("No_of_Attachment").Caption = "No Of Attachments"
            ''End TFS4433

            grdSaved.RootTable.Columns(enmEmployee.EmployeeDesignationName).Visible = True
            grdSaved.RootTable.Columns(enmEmployee.EmployeeDeptName).Visible = True
            grdSaved.RootTable.Columns(enmEmployee.Division_Name).Visible = True
            grdSaved.RootTable.Columns(enmEmployee.PayRollDivisionName).Visible = True
            'Me.grdSaved.RootTable.Columns(enmEmployee.Factor).Visible = False        'Task#18082015 hide the Factor column in grid by AHmad Sharif
            'Altered Against Task# 20150505 Ali Ansari
            grdSaved.RootTable.Columns(enmEmployee.Prefix).Visible = True
            grdSaved.RootTable.Columns(enmEmployee.Prefix).Width = 50
            'Altered Against Task# 20150505 Ali Ansari
            grdSaved.RootTable.Columns(enmEmployee.EmployeeDesignationName).Caption = "Desig"
            grdSaved.RootTable.Columns(enmEmployee.EmployeeDeptName).Caption = "Dept"
            grdSaved.RootTable.Columns(enmEmployee.Division_Name).Caption = "Disivion"
            grdSaved.RootTable.Columns(enmEmployee.PayRollDivisionName).Caption = "Sub Division"
            'Task 201506014 Make Employee Type Visible
            '
            grdSaved.RootTable.Columns(enmEmployee.EmployeeTypeName).Caption = "Employee Type"
            grdSaved.RootTable.Columns(enmEmployee.EmployeeTypeName).Width = 175
            'Task 201506014 Make Employee Type Visible

            grdSaved.RootTable.Columns(enmEmployee.Prefix).Caption = "Title"
            grdSaved.RootTable.Columns(enmEmployee.EmployeeID).Caption = "ID"
            grdSaved.RootTable.Columns(enmEmployee.EmployeeName).Caption = "Name"
            grdSaved.RootTable.Columns(enmEmployee.EmployeeCode).Caption = "Code"
            grdSaved.RootTable.Columns(enmEmployee.FatherName).Caption = "Father Name"
            grdSaved.RootTable.Columns(enmEmployee.NIC).Caption = "NIC"
            grdSaved.RootTable.Columns(enmEmployee.Salary).Caption = "Salary"
            grdSaved.RootTable.Columns(enmEmployee.CityName).Caption = "City"
            grdSaved.RootTable.Columns(enmEmployee.Gender).Caption = "Gender"
            grdSaved.RootTable.Columns("IsDailyWages").Caption = "Is Daily Wages"
            grdSaved.RootTable.Columns("CostCentreName").Caption = " Cost Centre Name"
            grdSaved.RootTable.Columns("CostCentreName").Width = 100
            grdSaved.RootTable.Columns(enmEmployee.EmployeeID).Width = 50
            grdSaved.RootTable.Columns(enmEmployee.EmployeeName).Width = 175
            grdSaved.RootTable.Columns(enmEmployee.EmployeeCode).Width = 75
            grdSaved.RootTable.Columns(enmEmployee.FatherName).Width = 175
            grdSaved.RootTable.Columns(enmEmployee.NIC).Width = 50
            grdSaved.RootTable.Columns(enmEmployee.Salary).Width = 100
            grdSaved.RootTable.Columns(enmEmployee.CityName).Width = 100
            grdSaved.RootTable.Columns(enmEmployee.Gender).Width = 50
            grdSaved.RootTable.Columns(enmEmployee.Salary).FormatString = "N"

            'Task#18082015 formatting on Factor Rate column in grid by Ahmad Sharif
            'Me.grdSaved.RootTable.Columns(enmEmployee.Factor).FormatString = "N"
            'Me.grdSaved.RootTable.Columns(enmEmployee.Factor).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'End Task#18082015

            'Altered Against Task# 20150511 Ali Ansari displaying Zone,Region,Belt and State
            grdSaved.RootTable.Columns("IsDailyWages").Visible = True
            grdSaved.RootTable.Columns("CostCentre").Visible = False
            grdSaved.RootTable.Columns("CostCentreName").Visible = True
            grdSaved.RootTable.Columns(enmEmployee.StateName).Visible = True
            grdSaved.RootTable.Columns(enmEmployee.RegionName).Visible = True
            grdSaved.RootTable.Columns(enmEmployee.ZoneName).Visible = True
            grdSaved.RootTable.Columns(enmEmployee.BeltName).Visible = True
            grdSaved.RootTable.Columns(enmEmployee.StateName).Width = 100
            grdSaved.RootTable.Columns(enmEmployee.RegionName).Width = 100
            grdSaved.RootTable.Columns(enmEmployee.ZoneName).Width = 100
            grdSaved.RootTable.Columns(enmEmployee.BeltName).Width = 100
            'Altered Against Task# 20150511 Ali Ansari displaying Zone,Region,Belt and State
            'Ali Faisal : TFS1250 : Visibility false and formating of CNIC expiry date column
            Me.grdSaved.RootTable.Columns("CNICExpiryDate").Visible = False
            Me.grdSaved.RootTable.Columns("CNICExpiryDate").FormatString = "" & str_DisplayDateFormat
            'Ayesha Rehman : TFS2645 : Visibility false and formating of Contract Ending date column
            Me.grdSaved.RootTable.Columns("ContractEndingDate").Visible = True
            Me.grdSaved.RootTable.Columns("ContractEndingDate").FormatString = "" & str_DisplayDateFormat

            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RefreshControls()
        Try

            Me.BtnSave.Text = "&Save"
            txtReligion.Text = ""
            txtName.Text = ""
            txtOfficialEmail.Text = ""
            txtCode.Text = ""
            txtFather.Text = ""
            txtNIC.Text = ""
            txtNTN.Text = ""
            txtAddress.Text = ""
            txtPhone.Text = ""
            txtMobile.Text = ""
            txtEmail.Text = ""
            txtSalary.Text = ""
            txtComments.Text = ""
            Me.txtrefrance.Text = String.Empty
            Me.txtpessiNo.Text = String.Empty
            Me.txtEobiNo.Text = String.Empty
            Me.txtReceiveableAccount.Text = String.Empty
            Me.pbemployee.Image = Nothing
            Me.txtAccountDescription.Text = String.Empty
            ddlGender.SelectedIndex = 0
            ddlCity.SelectedIndex = 0
            ddlDept.SelectedIndex = 0
            ddlDesignation.SelectedIndex = 0
            ddlMaritalStatus.SelectedIndex = 0
            chkActive.Checked = True
            chkSalePerson.Checked = False
            Me.chkSaleOrderPerson.Checked = False
            dtpDOB.Value = Date.Today 'Format(Date.Today, "dd/MM/yyyy")
            dtpLeaving.Value = Date.Today 'Format(Now, "dd/MM/yyyy")
            Me.dtpLeaving.Checked = False
            Me.txtCode.Text = GetNextDocNo("E", 5, "tblDefEmployee", "Employee_Code")
            Me.EmpSalaryAccountId = 0
            Me.EmpReceiveableAccountId = 0
            Me.EmployeeAccountId = 0
            ddlDesignation.SelectedIndex = 0
            Me.txtSalary.Text = String.Empty
            Me.txtAccountDescription.Text = String.Empty
            Me.txtReceiveableAccount.Text = String.Empty
            Me.txtAlternateEmpNo.Text = String.Empty
            Me.txtName.Focus()
            Me.TABHISTORY.SelectedTab = Me.TABHISTORY.Tabs(0).TabPage.Tab
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            'Me.cmbType.SelectedIndex = 0
            'Me.txtEmpAccount.Text = String.Empty
            Me.dtpAttendanceDate.Value = Now
            Me.dtpAttendanceDate.Checked = False

            ''Start TFS2645
            Me.dtpContractEndingDate.Value = Now
            Me.dtpContractEndingDate.Checked = False
            ''End TFS2645

            Me.txtFamilyCode.Text = String.Empty
            Me.txtIdRemark.Text = String.Empty
            If Not Me.cmbQualification.SelectedIndex = -1 Then Me.cmbQualification.Text = String.Empty
            Me.cmbQualification.Text = String.Empty
            If Not Me.cmbBloodGroup.SelectedIndex = -1 Then Me.cmbBloodGroup.Text = String.Empty
            If Not Me.cmbLanguage.SelectedIndex = -1 Then Me.cmbLanguage.Text = String.Empty
            Me.txtSocialSecurityNo.Text = String.Empty
            Me.txtInsuranceNo.Text = String.Empty
            Me.txtEmergencyNo.Text = String.Empty
            Me.txtPassportNo.Text = String.Empty
            Me.txtBankAccountNo.Text = String.Empty
            Me.txtNicPlace.Text = String.Empty
            If Not Me.cmbDomicile.SelectedIndex = -1 Then Me.cmbDomicile.Text = String.Empty
            Me.cmbDomicile.Text = String.Empty
            If Not Me.cmbRelation.SelectedIndex = -1 Then Me.cmbRelation.Text = String.Empty
            Me.cmbRelation.Text = String.Empty
            Me.txtReplacementNewCode.Text = String.Empty
            Me.txtPreviousCode.Text = String.Empty
            Me.dtpLastUpdate.Value = Date.Now
            If Not Me.cmbJobType.SelectedIndex = -1 Then Me.cmbJobType.Text = String.Empty

            Me.cmbJobType.Text = String.Empty

            If Not Me.cmbDivision.SelectedIndex = -1 Then Me.cmbDivision.SelectedIndex = 0
            If Not Me.cmbPayrollDivision.SelectedIndex = -1 Then Me.cmbPayrollDivision.SelectedIndex = 0
            If Not Me.cmbReferenceEmployee.SelectedIndex = -1 Then Me.cmbReferenceEmployee.SelectedIndex = 0 ' Reseting Reference Employee
            'Altered Against 20150505 Reset Prefix Combo
            If Not Me.CmbPrefix.SelectedIndex = -1 Then Me.CmbPrefix.SelectedIndex = 0 ' Reseting Prefix
            'Altered Against 20150505 Reset Prefix Combo

            'Altered Against 20150505 Reset State,Region,Zone,belt Combos
            If Not Me.cmbState.SelectedIndex = -1 Then Me.CmbPrefix.SelectedIndex = 0 ' Reseting State
            If Not Me.cmbRegion.SelectedIndex = -1 Then Me.CmbPrefix.SelectedIndex = 0 ' Reseting Region
            If Not Me.cmbZone.SelectedIndex = -1 Then Me.CmbPrefix.SelectedIndex = 0 ' Reseting Zone
            If Not Me.cmbBelt.SelectedIndex = -1 Then Me.CmbPrefix.SelectedIndex = 0 ' Reseting Belt

            'Altered Against 20150505 Reset State,Region,Zone,belt Combos
            'Altered Against 201506014 Reset Employee Type Combo
            If Not Me.CmbEmployeeType.SelectedIndex = -1 Then Me.CmbEmployeeType.SelectedIndex = 0 'Task#20150614  Reseting Employee Type
            If Not Me.cmbCostCentre.SelectedIndex = -1 Then Me.cmbCostCentre.SelectedIndex = 0
            'Altered Against 201506014 Reset Employee Type Combo
            Me.cmbShiftGroup.SelectedIndex = 0      'Task#1 13-Jun-2015 resetting Shift Group
            Me.cmbState.SelectedIndex = 0           'Task#1 13-Jun-2015 resetting State
            Me.cmbReportingTo.SelectedIndex = 0
            'Me.txtFactorRate.Text = String.Empty    'Task#18082015 empty factor rate textbox by Ahmad Sharif
            Me.cbDailyWages.Checked = False
            'Ali Faisal : TFS1250 : Reset to default value
            Me.dtpCNICExpiry.Value = Date.Now
            FillCombobox("EmpBank")
            Me.cmbBankAcName.Text = String.Empty
            ''Start TFS3566  19-06-2018 : Ayesha Rehman
            If Not getConfigValueByType("RightBasedCostCenters").ToString = "Error" Then
                flgCostCenterRights = getConfigValueByType("RightBasedCostCenters")
            End If
            ''End TFS3566
            ''Start TFS4433
            arrFile = New List(Of String)
            Me.btnAttachments.Text = "Attachment (" & arrFile.Count & ")"
            ''End TFS4433
            GetSecurityRights()
            DisplayRecord()
            EmpAcDetail(-1)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function Save() As Boolean

        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        objCon = Con 'New oledbConnection("Password=sa;Integrated Security Info=False;User ID=sa;Password=lumensoft2003; Initial Catalog=SimplePOS;Data Source=MKhalid")

        If objCon.State = ConnectionState.Open Then objCon.Close()

        objCon.Open()
        objCommand.Connection = objCon

        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.CommandType = CommandType.Text



            objCommand.Transaction = trans

            'Ali Faisal : TFS1250 : Altered to save CNIC Expiry Date
            'Ayesha Rehman : TFS2645: Altered to save Contract Ending Date
            objCommand.CommandText = "Insert into tblDefEmployee (Employee_Name, Employee_Code, Father_Name, NIC, NTN, Gender, Martial_Status, Religion, DOB, City_ID, Address, Phone, Mobile, Email, " _
                                            & " Joining_Date, Dept_ID, Desig_ID, Salary, Active, Leaving_Date, Comments, SalePerson, EmpSalaryAccountId, Reference,PessiNo,EobiNo,EmpPicture, ShiftGroupId,ReceiveableAccountId, Sale_Order_Person, AlternateEmpNo, AttendanceDate,ContractEndingDate,Family_Code,ID_Remark,Qualification,Blood_Group,Language,Social_Security_No,Insurance_No,Emergency_No,Passport_No,BankAccount_No,NIC_Place,Domicile,Relation,InReplacementNewCode,Previous_Code,Last_Update,JobType,Dept_Division,PayRoll_Division,RefEmployeeId,Bank_Ac_Name,prefix,Stateid,regionid,zoneid,beltid,EmployeeTypeId,ConfirmationDate, IsDailyWages, CostCentre, CNICExpiryDate, ReportingTo, OfficialEmail) values( " _
                                           & " N'" & txtName.Text.Replace("'", "''") & "',N'" & txtCode.Text.Replace("'", "''") & "',N'" & txtFather.Text.Replace("'", "''") & "',N'" & txtNIC.Text & "',N'" & txtNTN.Text & "',N'" & ddlGender.SelectedItem.ToString & "',N'" & ddlMaritalStatus.SelectedItem.ToString & "', " _
                                           & " N'" & txtReligion.Text & "',N'" & dtpDOB.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & IIf(ddlCity.SelectedIndex = 0, "Null", ddlCity.SelectedValue) & ",N'" & txtAddress.Text.Replace("'", "''") & "',N'" & txtPhone.Text.Replace("'", "''") & "',N'" & txtMobile.Text.Replace("'", "''") & "',N'" & txtEmail.Text.Replace("'", "''") & "', " _
                                           & " N'" & dtpJoining.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & IIf(ddlDept.SelectedIndex = 0, "Null", ddlDept.SelectedValue) & "," & IIf(ddlDesignation.SelectedIndex = 0, "Null", ddlDesignation.SelectedValue) & ", " & Val(txtSalary.Text) & "," & Abs(Val(chkActive.Checked)) & ", " _
                                           & " " & IIf(chkActive.Checked = True, "NULL", IIf(chkActive.Checked = True, "NULL", IIf(dtpLeaving.Checked = True, "'" & dtpLeaving.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL"))) & ", N'" & txtComments.Text & "', " & IIf(Me.chkSalePerson.Checked = False, 0, 1) & ", " & EmpSalaryAccountId & ", N'" & Me.txtrefrance.Text & "', N'" & Me.txtpessiNo.Text & "',N'" & Me.txtEobiNo.Text & "',N'" & _strImagePath & "', " & Val(Me.cmbShiftGroup.SelectedValue) & ", " & Val(Me.EmpReceiveableAccountId) & ", " & IIf(Me.chkSaleOrderPerson.Checked = True, 1, 0) & ", " & Val(Me.txtAlternateEmpNo.Text) & ", " & IIf(Me.dtpAttendanceDate.Checked = False, "NULL", "N'" & dtpAttendanceDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'") & "," & IIf(Me.dtpContractEndingDate.Checked = False, "NULL", "N'" & dtpContractEndingDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'") & ",N'" & txtFamilyCode.Text.Replace("'", "''") & "', " _
                                           & " N'" & txtIdRemark.Text.Replace("'", "''") & "'," & IIf(cmbQualification.Text.Length = 0, "NULL", "N'" & cmbQualification.Text.Replace("'", "''") & "'") & "," & IIf(cmbBloodGroup.Text.Length = 0, "NULL", "N'" & cmbBloodGroup.Text.Replace("'", "''") & "'") & "," & IIf(cmbLanguage.Text.Length = 0, "NULL", "N'" & cmbLanguage.Text.Replace("'", "''") & "'") & ",N'" & txtSocialSecurityNo.Text.Replace("'", "''") & "',N'" & txtInsuranceNo.Text.Replace("'", "''") & "',N'" & txtEmergencyNo.Text.Replace("'", "''") & "',N'" & txtPassportNo.Text.Replace("'", "''") & "',N'" & txtBankAccountNo.Text & "',N'" & txtNicPlace.Text.Replace("'", "''") & "'," & IIf(cmbDomicile.Text.Length = 0, "NULL", "N'" & cmbDomicile.Text.Replace("'", "''") & "'") & "," & IIf(cmbRelation.Text.Length = 0, "NULL", "N'" & cmbRelation.Text.Replace("'", "''") & "'") & ",N'" & txtReplacementNewCode.Text.Replace("'", "''") & "',N'" & txtPreviousCode.Text.Replace("'", "''") & "',N'" & dtpLastUpdate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & IIf(cmbJobType.Text.Length = 0, "NULL", "N'" & cmbJobType.Text.Replace("'", "''") & "'") & "," & IIf(cmbDivision.SelectedIndex = 0, 0, cmbDivision.SelectedValue) & "," & IIf(cmbPayrollDivision.SelectedIndex = 0, 0, cmbPayrollDivision.SelectedValue) & ", " & IIf(Me.cmbReferenceEmployee.SelectedIndex > 0, Me.cmbReferenceEmployee.SelectedValue, "NULL") & ", " & IIf(cmbBankAcName.Text.Length > 0, "N'" & Me.cmbBankAcName.Text.Replace("'", "''") & "'", "NULL") & ",'" & CmbPrefix.Text & "'," & IIf(cmbState.SelectedIndex = 0, "Null", cmbState.SelectedValue) & "," & IIf(cmbRegion.SelectedIndex = 0, "Null", cmbRegion.SelectedValue) & "," & IIf(cmbZone.SelectedIndex = 0, "Null", cmbZone.SelectedValue) & "," & IIf(cmbBelt.SelectedIndex = 0, "Null", cmbBelt.SelectedValue) & "," _
                                           & " " & IIf(CmbEmployeeType.SelectedIndex = 0, "Null", CmbEmployeeType.SelectedValue) & "," & IIf(Me.DtpConfirmation.Checked = False, "NULL", "N'" & DtpConfirmation.Value.ToString("yyyy-M-d h:mm:ss tt") & "'") & " , " & IIf(Me.cbDailyWages.Checked = False, 0, 1) & ", " & IIf(Me.cmbCostCentre.SelectedIndex <= 0, 0, Me.cmbCostCentre.SelectedValue) & ", '" & dtpCNICExpiry.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & IIf(cmbReportingTo.SelectedIndex = 0, "Null", cmbReportingTo.SelectedValue) & ",N'" & txtOfficialEmail.Text.Replace("'", "''") & "') Select @@Identity"
            'Altered Against Task#201506014 Save Employee Type and Leaving Date Ali Ansari
            identity = Convert.ToInt32(objCommand.ExecuteScalar())


            If EmpSalaryAccountId = 0 Then
                '' Create New Salary Account 
                objCommand.CommandText = ""
                objCommand.CommandText = "INSERT INTO  tblCOAMainSubSubDetail(main_sub_sub_id,detail_code,detail_title,Active) VALUES(" & intEmployeeAccountHeadId & ", N'" & GetCOACode(intEmployeeAccountHeadId, trans) & "',N'" & Me.txtName.Text.Replace("'", "''") & " SALARY PAY A/C',1) Select @@Identity"
                Dim intEmployeeAcId As Integer = objCommand.ExecuteScalar()


                '' Update Salary Account In Employee Information 
                objCommand.CommandText = ""
                objCommand.CommandText = "Update tblDefEmployee Set EmpSalaryAccountId=" & intEmployeeAcId & " WHERE Employee_Id=" & identity & ""
                objCommand.ExecuteNonQuery()
                '' End ................................
            Else

                objCommand.CommandText = ""
                objCommand.CommandText = "Update tblDefEmployee Set EmpSalaryAccountId=" & EmpSalaryAccountId & " WHERE Employee_Id=" & identity & ""
                objCommand.ExecuteNonQuery()
            End If

            objCommand.CommandText = ""

            Dim dt As New DataTable
            Dim da As New OleDbDataAdapter
            objCommand.CommandText = "Select ISNULL(Max(Convert(int,IsNull(BADGENUMBER,0))),0)+1 From USERINFO "
            da.SelectCommand = objCommand
            da.Fill(dt)


            objCommand.CommandText = ""
            objCommand.CommandText = "INSERT INTO USERINFO(BADGENUMBER, SSN,NAME,GENDER,TITLE,BIRTHDAY,HIREDDAY,OPHONE,Type,TagId,LUNCHDURATION) VALUES( " _
            & " N'" & Val(dt.Rows(0).Item(0).ToString) & "', N'" & Me.txtCode.Text.Replace("'", "''") & "',N'" & Me.txtName.Text.Replace("'", "''") & "', " _
            & " N'" & IIf(Me.ddlGender.Text = "Female", "F", "M") & "', N'" & Me.ddlDesignation.Text & "', Convert(DateTime,N'" & Me.dtpDOB.Value.Date & "',102), Convert(DateTime,N'" & Me.dtpJoining.Value.Date & "',102),  " _
            & " N'" & Me.txtMobile.Text.Replace("'", "''") & "', 'Employee'," & identity & ",1)Select @@Identity"
            Dim objUserId As Object = objCommand.ExecuteScalar()

            objCommand.CommandText = ""
            objCommand.CommandText = "UPDATE USERINFO SET BADGENUMBER=N'" & objUserId & "' WHERE USERID=" & objUserId & ""
            objCommand.ExecuteNonQuery()

            objCommand.CommandText = ""
            objCommand.CommandText = "Update tblDefEmployee Set AlternateEmpNo=" & objUserId & " WHERE Employee_Id=" & identity & ""
            objCommand.ExecuteNonQuery()


            ''TFS4433 : Save Attachments against Document
            If arrFile IsNot Nothing Then

                If arrFile.Count > 0 Then
                    SaveDocument(identity, Me.Name, trans)
                End If

            End If
            ''End TFS4433

            trans.Commit()



            If Save1() = True Then
                Save = True
            End If
            Save = True
            Try
                ''insert Activity Log
                SaveActivityLog("Config", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, identity, True)
            Catch ex As Exception

            End Try
            identity = 0
        Catch ex As Exception
            trans.Rollback()
            Save = False
            Throw ex
        End Try

    End Function
    ''' <summary>
    ''' This Function Is Added to save Attachments with documents
    ''' </summary>
    ''' <param name="DocId"></param>
    ''' <param name="Source"></param>
    ''' <param name="objTrans"></param>
    ''' <returns>Ayesha Rehman : 27-03-2018</returns>
    ''' <remarks></remarks>
    Public Function SaveDocument(ByVal DocId As Integer, ByVal Source As String, ByVal objTrans As OleDb.OleDbTransaction) As Boolean
        Dim cmd As New OleDbCommand
        cmd.Connection = objTrans.Connection
        cmd.Transaction = objTrans
        Try

            Dim dt As New DataTable
            dt = GetDataTable("Select DocId, Source, Path + '\' + FileName  as [FileNames]  From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'", objTrans)
            dt.AcceptChanges()


            Dim objdt As New DataTable
            objdt = GetDataTable("Select IsNull(Count(*),0)+1 as Cont From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'", objTrans)
            Dim intId As Integer = objdt.Rows(0)(0)

            Dim strSQL As String = String.Empty
            cmd.CommandText = String.Empty
            strSQL = "Delete From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()

            Dim objPath As String = getConfigValueByType("FileAttachmentPath").ToString

            If arrFile.Count > 0 Then
                For Each objFile As String In arrFile
                    If IO.File.Exists(objFile) Then
                        If IO.Directory.Exists(objPath) = False Then
                            IO.Directory.CreateDirectory(objPath)
                        End If
                        Dim New_Files As String = intId & "_" & DocId & "_" & Date.Now.ToString("yyyyMMdd") & "." & objFile.Substring(objFile.LastIndexOf(".") + 1)
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
    ''' <summary>
    ''' This Event is Added for Attachments to the Employee
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAttachments_Click(sender As Object, e As EventArgs) Handles btnAttachments.Click
        Try
            SetAttachments()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' TASK TFS4433
    ''' </summary>
    ''' <remarks> This function handle Attachment on Agreemnet Screen </remarks>
    Private Sub SetAttachments()
        Try
            Dim intCountAttachedFiles As Integer = 0I
            OpenFileDialog1.FileName = String.Empty
            OpenFileDialog1.Filter = "Word Documents|*.doc|Excel Worksheets|*.xls|Portable Document Format|*.pdf|Corel Draw Files|*.cdr|All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG|" + _
            "All files (.)|*.*"
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim a As Integer = 0I
                For a = 0 To OpenFileDialog1.FileNames.Length - 1
                    arrFile.Add(OpenFileDialog1.FileNames(a))
                Next a
                If Me.BtnSave.Text <> "&Save" Then
                    If Me.grdSaved.RowCount > 0 Then
                        intCountAttachedFiles = Val(grdSaved.CurrentRow.Cells("No_of_Attachment").Value)
                    End If
                End If
                Me.btnAttachments.Text = "Attachment (" & arrFile.Count + intCountAttachedFiles & ")"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Exception handling on 05-Nov-2018
    ''' </summary>
    ''' <param name="strCondition"></param>
    ''' <remarks></remarks>
    Private Sub FillCombo(Optional ByVal strCondition As String = "")
        Dim str As String = String.Empty
        Try
            If strCondition = String.Empty Then
                'If strCondition = "Item" Then
                str = String.Empty
                str = "Select CityID, CityName from tblListCity"
                FillDropDown(ddlCity, str)

                'ElseIf strCondition = "Category" Then
                str = String.Empty
                str = "Select EmployeeDeptID, EmployeeDeptName, IsNull(DeptAccountHeadId,0) as DeptAccountHeadId from EmployeeDeptDefTable order by 2"
                FillDropDown(ddlDept, str)

                'ElseIf strCondition = "ItemFilter" Then
                str = String.Empty
                str = "Select EmployeeDesignationID, EmployeeDesignationName from EmployeeDesignationDefTable order by 2"
                FillDropDown(ddlDesignation, str)

                'Shifting Group 
                str = String.Empty
                str = "Select ShiftGroupId, ShiftGroupName From ShiftGroupTable WHERE Active=1"
                FillDropDown(Me.cmbShiftGroup, str)

                'str = String.Empty
                'str = "Select SalaryExpTypeId, SalaryExpType From SalaryExpenseType WHERE flgAdvance=1"
                'FillDropDown(Me.cmbType, str)

                'Qualification
                str = String.Empty
                str = "Select Distinct Qualification,Qualification from tblDefEmployee"
                FillDropDown(cmbQualification, str, False)

                'Blood_Group
                str = String.Empty
                str = "Select Distinct Blood_Group,Blood_Group from tblDefEmployee"
                FillDropDown(cmbBloodGroup, str, False)

                'Domicile
                str = String.Empty
                str = "Select Distinct Domicile,Domicile from tblDefEmployee"
                FillDropDown(cmbDomicile, str, False)

                'Relation
                str = String.Empty
                str = "Select Distinct Relation,Relation from tblDefEmployee"
                FillDropDown(cmbRelation, str, False)

                'Job Type
                str = String.Empty
                str = "Select Distinct JobType,JobType from tblDefEmployee"
                FillDropDown(cmbRelation, str, False)
                'End If

                str = String.Empty
                str = "select stateid,statename from tblliststate where active = 1 order by sortorder"
                FillDropDown(cmbState, str, True)
                'Altered Against Task#20150511 fill combo of state
                str = String.Empty
                str = "SELECT Employee_ID, Employee_Name, Employee_Code, EmployeeDeptName, EmployeeDesignationName, Gender, CityName, Phone FROM EmployeesView WHERE Active = 1"
                FillDropDown(cmbReportingTo, str, True)

                ElseIf strCondition = "division" Then
                    FillDropDown(cmbDivision, "Select Division_Id,Division_Name from tblDefDivision Where Dept_Id = " & Me.ddlDept.SelectedValue & " ")
                ElseIf strCondition = "payrolldividion" Then
                    FillDropDown(cmbPayrollDivision, "Select PayRollDivision_Id,PayRollDivisionName from tblDefPayRollDivision Where Division_Id = " & Me.cmbDivision.SelectedValue & " ")
                ElseIf strCondition = "RefEmployee" Then
                    FillDropDown(Me.cmbReferenceEmployee, "Select Employee_Id, Employee_Name, Employee_Code From tblDefEmployee Where Active = 1 ORDER BY 2 ASC") ''TASKTFS75 added and set active =1
                ElseIf strCondition = "CostCentre" Then
                    ''Start TFS3566
                    If flgCostCenterRights = False Then
                        FillDropDown(Me.cmbCostCentre, "Select CostCenterID, Name, Code, SortOrder From tblDefCostCenter Where Active = 1 ORDER BY 2 ASC") ''TASKTFS75 added and set active =1
                    Else
                        FillDropDown(Me.cmbCostCentre, " SELECT  CostCenterID,Name,Code , SortOrder FROM tblDefCostCenter  where ISNULL(CostCenterID, 0) in (select ISNULL(CostCentre_Id, 0) AS CostCenterId FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) And Active = 1 ORDER BY 2 ASC ") ''TASKTFS75 added and set active =1
                    End If
                    ''End TFS3566
                End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub
    ''' <summary>
    ''' Ali Faisal : Exception handling on 05-Nov-2018
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FormValidate() As Boolean
        Try
            If txtName.Text = "" Then
                msg_Error("Please Enter Employee Name")
                txtName.Focus() : FormValidate = False : Exit Function
            End If

            If txtOfficialEmail.Text = "" Then
                msg_Error("Please Enter Official Email")
                txtOfficialEmail.Focus() : FormValidate = False : Exit Function
            End If

            If txtCode.Text = "" Then
                msg_Error("Please Enter Employee Code")
                txtCode.Focus() : FormValidate = False : Exit Function
            End If
            If Not Me.CmbEmployeeType.SelectedIndex > 0 Then
                msg_Error("Please select a Employee Type") : CmbEmployeeType.Focus() : Return (False) : Exit Function
            End If
            If Not Me.cmbReportingTo.SelectedIndex > 0 Then
                msg_Error("Please select Reporting To") : cmbReportingTo.Focus() : Return (False) : Exit Function
            End If
            'If Not Me.ddlCity.SelectedIndex > 0 Then
            '    msg_Error("Please select a city") : ddlCity.Focus() : Return False : Exit Function
            'End If
            If Not Me.ddlDept.SelectedIndex > 0 Then
                msg_Error("Please select a Department") : ddlDept.Focus() : Return (False) : Exit Function
            End If
            If Not Me.ddlDesignation.SelectedIndex > 0 Then
                msg_Error("Please select a Designation") : ddlDesignation.Focus() : Return False : Exit Function
            End If
            'If Not Me.cmbShiftGroup.SelectedIndex > 0 Then
            '    msg_Error("Please select a Shift Group") : Me.cmbShiftGroup.Focus() : Return False : Exit Function
            'End If
            ''Start TFS3566
            If flgCostCenterRights = True Then
                If Not Me.cmbCostCentre.SelectedIndex > 0 Then
                    msg_Error("Please select a Cost Center") : Me.cmbCostCentre.Focus() : Return False : Exit Function
                End If
            End If
            ''End TFS3566
            If blnEmpSimpleAcHead = True Then
                intEmployeeAccountHeadId = intEmployeeAccountHeadId
                If intEmployeeAccountHeadId = 0 Then
                    msg_Error("Employee's account head must be configured from configuration") : Return False : Exit Function
                End If
            ElseIf blnEmpDeptAcHead = True Then
                intEmployeeAccountHeadId = CType(Me.ddlDept.SelectedItem, DataRowView).Row.Item("DeptAccountHeadId").ToString
                If intEmployeeAccountHeadId = 0 Then
                    msg_Error("Employee's account head must be configured from Department") : Return False : Exit Function
                End If
            Else
                intEmployeeAccountHeadId = intEmployeeAccountHeadId
                If intEmployeeAccountHeadId = 0 Then
                    msg_Error("Employee's account head must be configured from configuration") : Return False : Exit Function
                End If
            End If

            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function

    Private Function Update_Record() As Boolean
        Dim CurrentDesignation As Integer = 0I
        Dim CurrentDepartment As Integer = 0I
        Dim CurrentSalary As Integer = 0I
        Dim Increment As Integer = 0I
        Dim IncrementedSalary As Integer = 0I
        Dim PromotionCode As String = String.Empty
        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim da As New OleDbDataAdapter
        Dim dt As New DataTable

        Dim i As Integer = 0

        objCon = Con 'New oledbConnection("Password=sa;Integrated Security Info=False;User ID=sa;Password=lumensoft2003; Initial Catalog=SimplePOS;Data Source=MKhalid")

        If objCon.State = ConnectionState.Open Then objCon.Close()

        objCon.Open()
        objCommand.Connection = objCon

        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.CommandType = CommandType.Text

            objCommand.Transaction = trans


            'objCon.BeginTransaction()
            'objCommand.CommandText = "Update tblDefEmployee set Employee_Name=N'" & txtName.Text & "',Employee_Code=N'" & txtCode.Text & "', Father_Name=N'" & txtFather.Text & "', " _
            '                        & " NIC=N'" & txtNIC.Text & "', NTN=N'" & txtNTN.Text & "', Gender=N'" & ddlGender.SelectedItem.ToString & "', " _
            '                        & " Martial_Status=N'" & ddlMaritalStatus.SelectedItem.ToString & "', Religion=N'" & txtReligion.Text & "', DOB=N'" & Format(dtpDOB.Value, "yyyy/MM/dd") & "', " _
            '                        & " City_ID=" & IIf(ddlCity.SelectedIndex = 0, "Null", ddlCity.SelectedValue) & ", Address=N'" & txtAddress.Text & "', Phone=N'" & txtPhone.Text & "', Mobile=N'" & txtMobile.Text & "', Email=N'" & txtEmail.Text & "', " _
            '                        & " Joining_Date=N'" & Format(dtpJoining.Value, "yyyy/MM/dd") & "', Dept_ID=" & IIf(ddlDept.SelectedIndex = 0, "Null", ddlDept.SelectedValue) & ", Desig_ID=" & IIf(ddlDesignation.SelectedIndex = 0, "Null", ddlDesignation.SelectedValue) & ", Salary=" & Val(txtSalary.Text) & ", " _
            '                        & " Active=" & Abs(Val(chkActive.Checked)) & ", Leaving_Date= " & IIf(chkActive.Checked = True, "NULL", IIf(dtpLeaving.Checked, Format(dtpLeaving.Value, "yyyy/MM/dd"), "NULL")) & ", Comments=N'" & txtComments.Text & "' , SalePerson=" & IIf(Me.chkSalePerson.Checked = False, 0, 1) & ", EmpSalaryAccountId= " & EmpSalaryAccountId & ",Reference = N'" & Me.txtrefrance.Text & "', PessiNo = N'" & Me.txtpessiNo.Text & "',EobiNo = N'" & Me.txtEobiNo.Text & "', EmpPicture = N'" & _strImagePath & "' " _
            '                        & " Where Employee_ID= " & txtEmpID.Text & ""

            'objCommand.ExecuteNonQuery()
            'Before against task:2747
            'objCommand.CommandText = "Update tblDefEmployee set Employee_Name=N'" & txtName.Text.Replace("'", "''") & "',Employee_Code=N'" & txtCode.Text & "', Father_Name=N'" & txtFather.Text.Replace("'", "''") & "', " _
            '                        & " NIC=N'" & txtNIC.Text & "', NTN=N'" & txtNTN.Text & "', Gender=N'" & ddlGender.SelectedItem.ToString & "', " _
            '                        & " Martial_Status=N'" & ddlMaritalStatus.SelectedItem.ToString & "', Religion=N'" & txtReligion.Text.Replace("'", "''") & "', DOB=N'" & dtpDOB.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
            '                        & " City_ID=" & IIf(ddlCity.SelectedIndex = 0, "Null", ddlCity.SelectedValue) & ", Address=N'" & txtAddress.Text.Replace("'", "''") & "', Phone=N'" & txtPhone.Text.Replace("'", "''") & "', Mobile=N'" & txtMobile.Text & "', Email=N'" & txtEmail.Text.Replace("'", "''") & "', " _
            '                        & " Joining_Date=N'" & dtpJoining.Value.ToString("yyyy-M-d h:mm:ss tt") & "', Dept_ID=" & IIf(ddlDept.SelectedIndex = 0, "Null", ddlDept.SelectedValue) & ", Desig_ID=" & IIf(ddlDesignation.SelectedIndex = 0, "Null", ddlDesignation.SelectedValue) & ", Salary=" & Val(txtSalary.Text) & ", " _
            '                        & " Active=" & Abs(Val(chkActive.Checked)) & ", Leaving_Date= " & IIf(chkActive.Checked = True, "NULL", IIf(dtpLeaving.Checked, dtpLeaving.Value.ToString("yyyy-M-d h:mm:ss tt"), "NULL")) & ", Comments=N'" & txtComments.Text.Replace("'", "''") & "' , SalePerson=" & IIf(Me.chkSalePerson.Checked = False, 0, 1) & ", EmpSalaryAccountId= " & EmpSalaryAccountId & ",Reference = N'" & Me.txtrefrance.Text.Replace("'", "''") & "', PessiNo = N'" & Me.txtpessiNo.Text.Replace("'", "''") & "',EobiNo = N'" & Me.txtEobiNo.Text.Replace("'", "''") & "', EmpPicture = N'" & _strImagePath & "', ShiftGroupId=" & Val(Me.cmbShiftGroup.SelectedValue) & ", ReceiveableAccountId=" & Val(Me.EmpReceiveableAccountId) & ", Sale_Order_Person=" & IIf(Me.chkSaleOrderPerson.Checked = True, 1, 0) & ", AlternateEmpNo=" & Val(Me.txtAlternateEmpNo.Text) & ", AttendanceDate=" & IIf(Me.dtpAttendanceDate.Checked = False, "NULL", "N'" & dtpAttendanceDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", " _
            '                        & " Family_Code=N'" & txtFamilyCode.Text.Replace("'", "''") & "', ID_Remark= N'" & txtIdRemark.Text & "',Qualification=" & IIf(cmbQualification.Text.Length = 0, "NULL", "N'" & cmbQualification.Text.Replace("'", "''") & "'") & ",Blood_Group=" & IIf(cmbBloodGroup.Text.Length = 0, "NULL", "N'" & cmbBloodGroup.Text & "'") & ",Language=" & IIf(cmbLanguage.Text.Length = 0, "NULL", "N'" & cmbLanguage.Text.Replace("'", "''") & "'") & ",Social_Security_No=N'" & txtSocialSecurityNo.Text.Replace("'", "''") & "',Insurance_No=N'" & txtInsuranceNo.Text.Replace("'", "''") & "',Emergency_No=N'" & txtEmergencyNo.Text.Replace("'", "''") & "',Passport_No=N'" & txtPassportNo.Text.Replace("'", "''") & "',BankAccount_No=N'" & txtPassportNo.Text.Replace("'", "''") & "',NIC_Place=N'" & txtNicPlace.Text.Replace("'", "''") & "',Domicile=" & IIf(cmbDomicile.Text.Length = 0, "NULL", "N'" & cmbDomicile.Text.Replace("'", "''") & "'") & ",Relation=" & IIf(cmbRelation.Text.Length = 0, "NULL", "N'" & cmbRelation.Text.Replace("'", "''") & "'") & ",InReplacementNewCode=N'" & txtReplacementNewCode.Text.Replace("'", "''") & "',Previous_Code=N'" & txtPreviousCode.Text.Replace("'", "''") & "',Last_Update=N'" & dtpLastUpdate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',JobType=" & IIf(cmbJobType.Text.Length = 0, "NULL", "N'" & cmbJobType.Text.Replace("'", "''") & "'") & ",Dept_Division= " & IIf(cmbDivision.SelectedIndex = 0, 0, cmbDivision.SelectedValue) & ",PayRoll_Division= " & IIf(cmbPayrollDivision.SelectedIndex = 0, 0, cmbPayrollDivision.SelectedValue) & " " _
            '                        & " Where Employee_ID= " & txtEmpID.Text & ""
            'Task:2747 Added Field RefEmployeeId
            objCommand.CommandText = ""
            'objCommand.CommandText = "Update tblDefEmployee set Employee_Name=N'" & txtName.Text.Replace("'", "''") & "',Employee_Code=N'" & txtCode.Text & "', Father_Name=N'" & txtFather.Text.Replace("'", "''") & "', " _
            '                   & " NIC=N'" & txtNIC.Text & "', NTN=N'" & txtNTN.Text & "', Gender=N'" & ddlGender.SelectedItem.ToString & "', " _
            '                   & " Martial_Status=N'" & ddlMaritalStatus.SelectedItem.ToString & "', Religion=N'" & txtReligion.Text.Replace("'", "''") & "', DOB=N'" & dtpDOB.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
            '                   & " City_ID=" & IIf(ddlCity.SelectedIndex = 0, "Null", ddlCity.SelectedValue) & ", Address=N'" & txtAddress.Text.Replace("'", "''") & "', Phone=N'" & txtPhone.Text.Replace("'", "''") & "', Mobile=N'" & txtMobile.Text & "', Email=N'" & txtEmail.Text.Replace("'", "''") & "', " _
            '                   & " Joining_Date=N'" & dtpJoining.Value.ToString("yyyy-M-d h:mm:ss tt") & "', Dept_ID=" & IIf(ddlDept.SelectedIndex = 0, "Null", ddlDept.SelectedValue) & ", Desig_ID=" & IIf(ddlDesignation.SelectedIndex = 0, "Null", ddlDesignation.SelectedValue) & ", Salary=" & Val(txtSalary.Text) & ", " _
            '                   & " Active=" & Abs(Val(chkActive.Checked)) & ", Leaving_Date= " & IIf(chkActive.Checked = True, "NULL", IIf(dtpLeaving.Checked, dtpLeaving.Value.ToString("yyyy-M-d h:mm:ss tt"), "NULL")) & ", Comments=N'" & txtComments.Text.Replace("'", "''") & "' , SalePerson=" & IIf(Me.chkSalePerson.Checked = False, 0, 1) & ", EmpSalaryAccountId= " & EmpSalaryAccountId & ",Reference = N'" & Me.txtrefrance.Text.Replace("'", "''") & "', PessiNo = N'" & Me.txtpessiNo.Text.Replace("'", "''") & "',EobiNo = N'" & Me.txtEobiNo.Text.Replace("'", "''") & "', EmpPicture = N'" & _strImagePath & "', ShiftGroupId=" & Val(Me.cmbShiftGroup.SelectedValue) & ", ReceiveableAccountId=" & Val(Me.EmpReceiveableAccountId) & ", Sale_Order_Person=" & IIf(Me.chkSaleOrderPerson.Checked = True, 1, 0) & ", AlternateEmpNo=" & Val(Me.txtAlternateEmpNo.Text) & ", AttendanceDate=" & IIf(Me.dtpAttendanceDate.Checked = False, "NULL", "N'" & dtpAttendanceDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", " _
            '                   & " Family_Code=N'" & txtFamilyCode.Text.Replace("'", "''") & "', ID_Remark= N'" & txtIdRemark.Text & "',Qualification=" & IIf(cmbQualification.Text.Length = 0, "NULL", "N'" & cmbQualification.Text.Replace("'", "''") & "'") & ",Blood_Group=" & IIf(cmbBloodGroup.Text.Length = 0, "NULL", "N'" & cmbBloodGroup.Text & "'") & ",Language=" & IIf(cmbLanguage.Text.Length = 0, "NULL", "N'" & cmbLanguage.Text.Replace("'", "''") & "'") & ",Social_Security_No=N'" & txtSocialSecurityNo.Text.Replace("'", "''") & "',Insurance_No=N'" & txtInsuranceNo.Text.Replace("'", "''") & "',Emergency_No=N'" & txtEmergencyNo.Text.Replace("'", "''") & "',Passport_No=N'" & txtPassportNo.Text.Replace("'", "''") & "',BankAccount_No=N'" & txtPassportNo.Text.Replace("'", "''") & "',NIC_Place=N'" & txtNicPlace.Text.Replace("'", "''") & "',Domicile=" & IIf(cmbDomicile.Text.Length = 0, "NULL", "N'" & cmbDomicile.Text.Replace("'", "''") & "'") & ",Relation=" & IIf(cmbRelation.Text.Length = 0, "NULL", "N'" & cmbRelation.Text.Replace("'", "''") & "'") & ",InReplacementNewCode=N'" & txtReplacementNewCode.Text.Replace("'", "''") & "',Previous_Code=N'" & txtPreviousCode.Text.Replace("'", "''") & "',Last_Update=N'" & dtpLastUpdate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',JobType=" & IIf(cmbJobType.Text.Length = 0, "NULL", "N'" & cmbJobType.Text.Replace("'", "''") & "'") & ",Dept_Division= " & IIf(cmbDivision.SelectedIndex = 0, 0, cmbDivision.SelectedValue) & ",PayRoll_Division= " & IIf(cmbPayrollDivision.SelectedIndex = 0, 0, cmbPayrollDivision.SelectedValue) & ", RefEmployeeId=" & IIf(Me.cmbReferenceEmployee.SelectedIndex > 0, Me.cmbReferenceEmployee.SelectedValue, "NULL") & " " _
            '                   & " Where Employee_ID= " & txtEmpID.Text & ""
            'End Task:2747
            'Marked Against Task# 20150505 Ali Ansari
            'objCommand.CommandText = "Update tblDefEmployee set Employee_Name=N'" & txtName.Text.Replace("'", "''") & "',Employee_Code=N'" & txtCode.Text & "', Father_Name=N'" & txtFather.Text.Replace("'", "''") & "', " _
            '                  & " NIC=N'" & txtNIC.Text & "', NTN=N'" & txtNTN.Text & "', Gender=N'" & ddlGender.SelectedItem.ToString & "', " _
            '                  & " Martial_Status=N'" & ddlMaritalStatus.SelectedItem.ToString & "', Religion=N'" & txtReligion.Text.Replace("'", "''") & "', DOB=N'" & dtpDOB.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
            '                  & " City_ID=" & IIf(ddlCity.SelectedIndex = 0, "Null", ddlCity.SelectedValue) & ", Address=N'" & txtAddress.Text.Replace("'", "''") & "', Phone=N'" & txtPhone.Text.Replace("'", "''") & "', Mobile=N'" & txtMobile.Text & "', Email=N'" & txtEmail.Text.Replace("'", "''") & "', " _
            '                  & " Joining_Date=N'" & dtpJoining.Value.ToString("yyyy-M-d h:mm:ss tt") & "', Dept_ID=" & IIf(ddlDept.SelectedIndex = 0, "Null", ddlDept.SelectedValue) & ", Desig_ID=" & IIf(ddlDesignation.SelectedIndex = 0, "Null", ddlDesignation.SelectedValue) & ", Salary=" & Val(txtSalary.Text) & ", " _
            '                  & " Active=" & Abs(Val(chkActive.Checked)) & ", Leaving_Date= " & IIf(chkActive.Checked = True, "NULL", IIf(dtpLeaving.Checked, dtpLeaving.Value.ToString("yyyy-M-d h:mm:ss tt"), "NULL")) & ", Comments=N'" & txtComments.Text.Replace("'", "''") & "' , SalePerson=" & IIf(Me.chkSalePerson.Checked = False, 0, 1) & ", EmpSalaryAccountId= " & EmpSalaryAccountId & ",Reference = N'" & Me.txtrefrance.Text.Replace("'", "''") & "', PessiNo = N'" & Me.txtpessiNo.Text.Replace("'", "''") & "',EobiNo = N'" & Me.txtEobiNo.Text.Replace("'", "''") & "', EmpPicture = N'" & _strImagePath & "', ShiftGroupId=" & Val(Me.cmbShiftGroup.SelectedValue) & ", ReceiveableAccountId=" & Val(Me.EmpReceiveableAccountId) & ", Sale_Order_Person=" & IIf(Me.chkSaleOrderPerson.Checked = True, 1, 0) & ", AlternateEmpNo=" & Val(Me.txtAlternateEmpNo.Text) & ", AttendanceDate=" & IIf(Me.dtpAttendanceDate.Checked = False, "NULL", "N'" & dtpAttendanceDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", " _
            '                  & " Family_Code=N'" & txtFamilyCode.Text.Replace("'", "''") & "', ID_Remark= N'" & txtIdRemark.Text & "',Qualification=" & IIf(cmbQualification.Text.Length = 0, "NULL", "N'" & cmbQualification.Text.Replace("'", "''") & "'") & ",Blood_Group=" & IIf(cmbBloodGroup.Text.Length = 0, "NULL", "N'" & cmbBloodGroup.Text & "'") & ",Language=" & IIf(cmbLanguage.Text.Length = 0, "NULL", "N'" & cmbLanguage.Text.Replace("'", "''") & "'") & ",Social_Security_No=N'" & txtSocialSecurityNo.Text.Replace("'", "''") & "',Insurance_No=N'" & txtInsuranceNo.Text.Replace("'", "''") & "',Emergency_No=N'" & txtEmergencyNo.Text.Replace("'", "''") & "',Passport_No=N'" & txtPassportNo.Text.Replace("'", "''") & "',BankAccount_No=N'" & txtBankAccountNo.Text.Replace("'", "''") & "',NIC_Place=N'" & txtNicPlace.Text.Replace("'", "''") & "',Domicile=" & IIf(cmbDomicile.Text.Length = 0, "NULL", "N'" & cmbDomicile.Text.Replace("'", "''") & "'") & ",Relation=" & IIf(cmbRelation.Text.Length = 0, "NULL", "N'" & cmbRelation.Text.Replace("'", "''") & "'") & ",InReplacementNewCode=N'" & txtReplacementNewCode.Text.Replace("'", "''") & "',Previous_Code=N'" & txtPreviousCode.Text.Replace("'", "''") & "',Last_Update=N'" & dtpLastUpdate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',JobType=" & IIf(cmbJobType.Text.Length = 0, "NULL", "N'" & cmbJobType.Text.Replace("'", "''") & "'") & ",Dept_Division= " & IIf(cmbDivision.SelectedIndex = 0, 0, cmbDivision.SelectedValue) & ",PayRoll_Division= " & IIf(cmbPayrollDivision.SelectedIndex = 0, 0, cmbPayrollDivision.SelectedValue) & ", RefEmployeeId=" & IIf(Me.cmbReferenceEmployee.SelectedIndex > 0, Me.cmbReferenceEmployee.SelectedValue, "NULL") & ", Bank_Ac_Name='" & Me.txtBankAcName.Text.Replace("'", "''") & "' " _
            '                  & " Where Employee_ID= " & txtEmpID.Text & ""
            'Marked Against Task# 20150505 Ali Ansari

            'Marked Against Task# 20150505 Ali Ansari
            ''Altered Against Task# 20150505 Ali Ansari
            ''Set prefix in Employee table
            'objCommand.CommandText = "Update tblDefEmployee set Employee_Name=N'" & txtName.Text.Replace("'", "''") & "',Employee_Code=N'" & txtCode.Text & "', Father_Name=N'" & txtFather.Text.Replace("'", "''") & "', " _
            '                  & " NIC=N'" & txtNIC.Text & "', NTN=N'" & txtNTN.Text & "', Gender=N'" & ddlGender.SelectedItem.ToString & "', " _
            '                  & " Martial_Status=N'" & ddlMaritalStatus.SelectedItem.ToString & "', Religion=N'" & txtReligion.Text.Replace("'", "''") & "', DOB=N'" & dtpDOB.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
            '                  & " City_ID=" & IIf(ddlCity.SelectedIndex = 0, "Null", ddlCity.SelectedValue) & ", Address=N'" & txtAddress.Text.Replace("'", "''") & "', Phone=N'" & txtPhone.Text.Replace("'", "''") & "', Mobile=N'" & txtMobile.Text & "', Email=N'" & txtEmail.Text.Replace("'", "''") & "', " _
            '                  & " Joining_Date=N'" & dtpJoining.Value.ToString("yyyy-M-d h:mm:ss tt") & "', Dept_ID=" & IIf(ddlDept.SelectedIndex = 0, "Null", ddlDept.SelectedValue) & ", Desig_ID=" & IIf(ddlDesignation.SelectedIndex = 0, "Null", ddlDesignation.SelectedValue) & ", Salary=" & Val(txtSalary.Text) & ", " _
            '                  & " Active=" & Abs(Val(chkActive.Checked)) & ", Leaving_Date= " & IIf(chkActive.Checked = True, "NULL", IIf(dtpLeaving.Checked, dtpLeaving.Value.ToString("yyyy-M-d h:mm:ss tt"), "NULL")) & ", Comments=N'" & txtComments.Text.Replace("'", "''") & "' , SalePerson=" & IIf(Me.chkSalePerson.Checked = False, 0, 1) & ", EmpSalaryAccountId= " & EmpSalaryAccountId & ",Reference = N'" & Me.txtrefrance.Text.Replace("'", "''") & "', PessiNo = N'" & Me.txtpessiNo.Text.Replace("'", "''") & "',EobiNo = N'" & Me.txtEobiNo.Text.Replace("'", "''") & "', EmpPicture = N'" & _strImagePath & "', ShiftGroupId=" & Val(Me.cmbShiftGroup.SelectedValue) & ", ReceiveableAccountId=" & Val(Me.EmpReceiveableAccountId) & ", Sale_Order_Person=" & IIf(Me.chkSaleOrderPerson.Checked = True, 1, 0) & ", AlternateEmpNo=" & Val(Me.txtAlternateEmpNo.Text) & ", AttendanceDate=" & IIf(Me.dtpAttendanceDate.Checked = False, "NULL", "N'" & dtpAttendanceDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", " _
            '                  & " Family_Code=N'" & txtFamilyCode.Text.Replace("'", "''") & "', ID_Remark= N'" & txtIdRemark.Text & "',Qualification=" & IIf(cmbQualification.Text.Length = 0, "NULL", "N'" & cmbQualification.Text.Replace("'", "''") & "'") & ",Blood_Group=" & IIf(cmbBloodGroup.Text.Length = 0, "NULL", "N'" & cmbBloodGroup.Text & "'") & ",Language=" & IIf(cmbLanguage.Text.Length = 0, "NULL", "N'" & cmbLanguage.Text.Replace("'", "''") & "'") & ",Social_Security_No=N'" & txtSocialSecurityNo.Text.Replace("'", "''") & "',Insurance_No=N'" & txtInsuranceNo.Text.Replace("'", "''") & "',Emergency_No=N'" & txtEmergencyNo.Text.Replace("'", "''") & "',Passport_No=N'" & txtPassportNo.Text.Replace("'", "''") & "',BankAccount_No=N'" & txtBankAccountNo.Text.Replace("'", "''") & "',NIC_Place=N'" & txtNicPlace.Text.Replace("'", "''") & "',Domicile=" & IIf(cmbDomicile.Text.Length = 0, "NULL", "N'" & cmbDomicile.Text.Replace("'", "''") & "'") & ",Relation=" & IIf(cmbRelation.Text.Length = 0, "NULL", "N'" & cmbRelation.Text.Replace("'", "''") & "'") & ",InReplacementNewCode=N'" & txtReplacementNewCode.Text.Replace("'", "''") & "',Previous_Code=N'" & txtPreviousCode.Text.Replace("'", "''") & "',Last_Update=N'" & dtpLastUpdate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',JobType=" & IIf(cmbJobType.Text.Length = 0, "NULL", "N'" & cmbJobType.Text.Replace("'", "''") & "'") & ",Dept_Division= " & IIf(cmbDivision.SelectedIndex = 0, 0, cmbDivision.SelectedValue) & ",PayRoll_Division= " & IIf(cmbPayrollDivision.SelectedIndex = 0, 0, cmbPayrollDivision.SelectedValue) & ", RefEmployeeId=" & IIf(Me.cmbReferenceEmployee.SelectedIndex > 0, Me.cmbReferenceEmployee.SelectedValue, "NULL") & ", Bank_Ac_Name='" & Me.txtBankAcName.Text.Replace("'", "''") & "', " _
            '                  & " Prefix = '" & CmbPrefix.Text & "'  Where Employee_ID= " & txtEmpID.Text & ""
            ''Set prefix in Employee table
            ''Altered Against Task# 20150505 Ali Ansari
            'Marked Against Task# 20150505 Ali Ansari



            'Altered Against Task# 20150511 Ali Ansari
            'Update Region,Zone,State, Belt in Employee
            'Marked Against Task#201506014 Update Employee Type and Leaving Date Ali Ansari
            ''07-June-2015 Task# 07A-06-2015 Ahmad Sharif: Refine update query
            'objCommand.CommandText = "Update tblDefEmployee set Employee_Name=N'" & txtName.Text.Replace("'", "''") & "',Employee_Code=N'" & txtCode.Text & "', Father_Name=N'" & txtFather.Text.Replace("'", "''") & "', " _
            '                  & " NIC=N'" & txtNIC.Text & "', NTN=N'" & txtNTN.Text & "', Gender=N'" & ddlGender.SelectedItem.ToString & "', " _
            '                  & " Martial_Status=N'" & ddlMaritalStatus.SelectedItem.ToString & "', Religion=N'" & txtReligion.Text.Replace("'", "''") & "', DOB=N'" & dtpDOB.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
            '                  & " City_ID=" & IIf(ddlCity.SelectedIndex = 0, "Null", ddlCity.SelectedValue) & ", Address=N'" & txtAddress.Text.Replace("'", "''") & "', Phone=N'" & txtPhone.Text.Replace("'", "''") & "', Mobile=N'" & txtMobile.Text & "', Email=N'" & txtEmail.Text.Replace("'", "''") & "', " _
            '                  & " Joining_Date=N'" & dtpJoining.Value.ToString("yyyy-M-d h:mm:ss tt") & "', Dept_ID=" & IIf(ddlDept.SelectedIndex = 0, "Null", ddlDept.SelectedValue) & ", Desig_ID=" & IIf(ddlDesignation.SelectedIndex = 0, "Null", ddlDesignation.SelectedValue) & ", Salary=" & Val(txtSalary.Text) & ", " _
            '                  & " Active=" & Abs(Val(chkActive.Checked)) & ", Leaving_Date=" & IIf(chkActive.Checked = True, "NULL", IIf(dtpLeaving.Checked = True, "'" & dtpLeaving.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL")) & ", Comments=N'" & txtComments.Text.Replace("'", "''") & "' , SalePerson=" & IIf(Me.chkSalePerson.Checked = False, 0, 1) & ", EmpSalaryAccountId= " & EmpSalaryAccountId & ",Reference = N'" & Me.txtrefrance.Text.Replace("'", "''") & "', PessiNo = N'" & Me.txtpessiNo.Text.Replace("'", "''") & "',EobiNo = N'" & Me.txtEobiNo.Text.Replace("'", "''") & "', EmpPicture = N'" & _strImagePath & "', ShiftGroupId=" & Val(Me.cmbShiftGroup.SelectedValue) & ", ReceiveableAccountId=" & Val(Me.EmpReceiveableAccountId) & ", Sale_Order_Person=" & IIf(Me.chkSaleOrderPerson.Checked = True, 1, 0) & ", AlternateEmpNo=" & Val(Me.txtAlternateEmpNo.Text) & ", AttendanceDate=" & IIf(Me.dtpAttendanceDate.Checked = False, "NULL", "N'" & dtpAttendanceDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", " _
            '                  & " Family_Code=N'" & txtFamilyCode.Text.Replace("'", "''") & "', ID_Remark= N'" & txtIdRemark.Text & "',Qualification=" & IIf(cmbQualification.Text.Length = 0, "NULL", "N'" & cmbQualification.Text.Replace("'", "''") & "'") & ",Blood_Group=" & IIf(cmbBloodGroup.Text.Length = 0, "NULL", "N'" & cmbBloodGroup.Text & "'") & ",Language=" & IIf(cmbLanguage.Text.Length = 0, "NULL", "N'" & cmbLanguage.Text.Replace("'", "''") & "'") & ",Social_Security_No=N'" & txtSocialSecurityNo.Text.Replace("'", "''") & "',Insurance_No=N'" & txtInsuranceNo.Text.Replace("'", "''") & "',Emergency_No=N'" & txtEmergencyNo.Text.Replace("'", "''") & "',Passport_No=N'" & txtPassportNo.Text.Replace("'", "''") & "',BankAccount_No=N'" & txtBankAccountNo.Text.Replace("'", "''") & "',NIC_Place=N'" & txtNicPlace.Text.Replace("'", "''") & "',Domicile=" & IIf(cmbDomicile.Text.Length = 0, "NULL", "N'" & cmbDomicile.Text.Replace("'", "''") & "'") & ",Relation=" & IIf(cmbRelation.Text.Length = 0, "NULL", "N'" & cmbRelation.Text.Replace("'", "''") & "'") & ",InReplacementNewCode=N'" & txtReplacementNewCode.Text.Replace("'", "''") & "',Previous_Code=N'" & txtPreviousCode.Text.Replace("'", "''") & "',Last_Update=N'" & dtpLastUpdate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',JobType=" & IIf(cmbJobType.Text.Length = 0, "NULL", "N'" & cmbJobType.Text.Replace("'", "''") & "'") & ",Dept_Division= " & IIf(cmbDivision.SelectedIndex = 0, 0, cmbDivision.SelectedValue) & ",PayRoll_Division= " & IIf(cmbPayrollDivision.SelectedIndex = 0, 0, cmbPayrollDivision.SelectedValue) & ", RefEmployeeId=" & IIf(Me.cmbReferenceEmployee.SelectedIndex > 0, Me.cmbReferenceEmployee.SelectedValue, "NULL") & ", Bank_Ac_Name='" & Me.txtBankAcName.Text.Replace("'", "''") & "', " _
            '                  & " Prefix = '" & CmbPrefix.Text & "',stateid = " & IIf(cmbState.SelectedIndex = 0, "Null", cmbState.SelectedValue) & ",RegionId = " & IIf(cmbRegion.SelectedIndex = 0, "Null", cmbRegion.SelectedValue) & ",zoneid = " & IIf(cmbZone.SelectedIndex = 0, "Null", cmbZone.SelectedValue) & ",beltid = " & IIf(cmbBelt.SelectedIndex = 0, "Null", cmbBelt.SelectedValue) & " Where Employee_ID= " & txtEmpID.Text & ""
            ''end Task# 07A-06-2015
            'Update Region,Zone,State, Belt in Employee
            'Altered Against Task# 20150511 Ali Ansari
            'Marked Against Task#201506014 Update Employee Type and Leaving Date Ali Ansari

            'Altered Against Task#201506016 Get Employee Current Designation, if changed then insert into promotion table

            'objCommand.CommandText = ""
            ''da = Nothing
            'dt.Clear()
            'objCommand.CommandText = "Select salary,desig_id,Dept_ID From tblDefEmployee WHERE Employee_ID=" & Val(txtEmpID.Text) & ""


            'da.SelectCommand = objCommand
            'da.Fill(dt)
            'dt.AcceptChanges()
            'If dt IsNot Nothing Then
            '    If dt.Rows.Count = 1 Then
            '        CurrentSalary = Val(dt.Rows(0).Item(0).ToString)
            '        CurrentDesignation = Val(dt.Rows(0).Item(1).ToString)
            '        CurrentDepartment = Val(dt.Rows(0).Item(2).ToString)
            '    End If
            'End If
            'If Len(CurrentDesignation) > 0 And CurrentDesignation <> ddlDesignation.SelectedValue Then
            '    PromotionCode = GetNextDocNo("EP", 5, "tblEmployeePromotion", "Ref_No")
            '    Increment = Val(txtSalary.Text) - CurrentSalary
            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "INSERT INTO  tblEmployeePromotion(PromotionType,Ref_No,Ref_Date,EmployeeId," _
            '                             & " OldDepartmentId,DepartmentId, OldDesignationId," _
            '                             & "DesignationId,BasicSalary,NewBasicSalary,Increament_Salary," _
            '                             & " Status,EntryDate,UserName ) VALUES('Increment', '" & PromotionCode & "'," _
            '                             & " '" & Date.Today & "'," & txtEmpID.Text & "," & CurrentDepartment & "," _
            '                             & "" & ddlDept.SelectedValue & "," & CurrentDesignation & "," _
            '                             & " " & ddlDesignation.SelectedValue & "," _
            '                             & " " & CurrentSalary & "," & txtSalary.Text & "," & Increment & ",1," _
            '                             & " '" & Date.Today & "','" & LoginUserName & "') Select @@Identity"
            '    objCommand.ExecuteNonQuery()

            'End If

            'Altered Against Task#201506014 Get Employee Current Designation, if changed then insert into promotion table
            'Altered Against Task#201506014 Update Employee Type and Leaving Date Ali Ansari
            'Task#18082015 add column Factor in update query by Ahmad Sharif
            'objCommand.CommandText = "Update tblDefEmployee set Employee_Name=N'" & txtName.Text.Replace("'", "''") & "',Employee_Code=N'" & txtCode.Text & "', Father_Name=N'" & txtFather.Text.Replace("'", "''") & "', " _
            '                  & " NIC=N'" & txtNIC.Text & "', NTN=N'" & txtNTN.Text & "', Gender=N'" & ddlGender.SelectedItem.ToString & "', " _
            '                  & " Martial_Status=N'" & ddlMaritalStatus.SelectedItem.ToString & "', Religion=N'" & txtReligion.Text.Replace("'", "''") & "', DOB=N'" & dtpDOB.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
            '                  & " City_ID=" & IIf(ddlCity.SelectedIndex = 0, "Null", ddlCity.SelectedValue) & ", Address=N'" & txtAddress.Text.Replace("'", "''") & "', Phone=N'" & txtPhone.Text.Replace("'", "''") & "', Mobile=N'" & txtMobile.Text & "', Email=N'" & txtEmail.Text.Replace("'", "''") & "', " _
            '                  & " Joining_Date=N'" & dtpJoining.Value.ToString("yyyy-M-d h:mm:ss tt") & "', Dept_ID=" & IIf(ddlDept.SelectedIndex = 0, "Null", ddlDept.SelectedValue) & ", Desig_ID=" & IIf(ddlDesignation.SelectedIndex = 0, "Null", ddlDesignation.SelectedValue) & ", Salary=" & Val(txtSalary.Text) & ", " _
            '                  & " Active=" & Abs(Val(chkActive.Checked)) & ", Leaving_Date=" & IIf(chkActive.Checked = True, "NULL", IIf(dtpLeaving.Checked = True, "'" & dtpLeaving.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL")) & ", Comments=N'" & txtComments.Text.Replace("'", "''") & "' , SalePerson=" & IIf(Me.chkSalePerson.Checked = False, 0, 1) & ", EmpSalaryAccountId= " & EmpSalaryAccountId & ",Reference = N'" & Me.txtrefrance.Text.Replace("'", "''") & "', PessiNo = N'" & Me.txtpessiNo.Text.Replace("'", "''") & "',EobiNo = N'" & Me.txtEobiNo.Text.Replace("'", "''") & "', EmpPicture = N'" & _strImagePath & "', ShiftGroupId=" & Val(Me.cmbShiftGroup.SelectedValue) & ", ReceiveableAccountId=" & Val(Me.EmpReceiveableAccountId) & ", Sale_Order_Person=" & IIf(Me.chkSaleOrderPerson.Checked = True, 1, 0) & ", AlternateEmpNo=" & Val(Me.txtAlternateEmpNo.Text) & ", AttendanceDate=" & IIf(Me.dtpAttendanceDate.Checked = False, "NULL", "N'" & dtpAttendanceDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", " _
            '                  & " Family_Code=N'" & txtFamilyCode.Text.Replace("'", "''") & "', ID_Remark= N'" & txtIdRemark.Text & "',Qualification=" & IIf(cmbQualification.Text.Length = 0, "NULL", "N'" & cmbQualification.Text.Replace("'", "''") & "'") & ",Blood_Group=" & IIf(cmbBloodGroup.Text.Length = 0, "NULL", "N'" & cmbBloodGroup.Text & "'") & ",Language=" & IIf(cmbLanguage.Text.Length = 0, "NULL", "N'" & cmbLanguage.Text.Replace("'", "''") & "'") & ",Social_Security_No=N'" & txtSocialSecurityNo.Text.Replace("'", "''") & "',Insurance_No=N'" & txtInsuranceNo.Text.Replace("'", "''") & "',Emergency_No=N'" & txtEmergencyNo.Text.Replace("'", "''") & "',Passport_No=N'" & txtPassportNo.Text.Replace("'", "''") & "',BankAccount_No=N'" & txtBankAccountNo.Text.Replace("'", "''") & "',NIC_Place=N'" & txtNicPlace.Text.Replace("'", "''") & "',Domicile=" & IIf(cmbDomicile.Text.Length = 0, "NULL", "N'" & cmbDomicile.Text.Replace("'", "''") & "'") & ",Relation=" & IIf(cmbRelation.Text.Length = 0, "NULL", "N'" & cmbRelation.Text.Replace("'", "''") & "'") & ",InReplacementNewCode=N'" & txtReplacementNewCode.Text.Replace("'", "''") & "',Previous_Code=N'" & txtPreviousCode.Text.Replace("'", "''") & "',Last_Update=N'" & dtpLastUpdate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',JobType=" & IIf(cmbJobType.Text.Length = 0, "NULL", "N'" & cmbJobType.Text.Replace("'", "''") & "'") & ",Dept_Division= " & IIf(cmbDivision.SelectedIndex = 0, 0, cmbDivision.SelectedValue) & ",PayRoll_Division= " & IIf(cmbPayrollDivision.SelectedIndex = 0, 0, cmbPayrollDivision.SelectedValue) & ", RefEmployeeId=" & IIf(Me.cmbReferenceEmployee.SelectedIndex > 0, Me.cmbReferenceEmployee.SelectedValue, "NULL") & ", Bank_Ac_Name='" & Me.txtBankAcName.Text.Replace("'", "''") & "', " _
            '                  & " EmployeeTypeId = " & IIf(CmbEmployeeType.SelectedIndex = 0, "Null", CmbEmployeeType.SelectedValue) & ", " _
            '                  & " ConfirmationDate = " & IIf(Me.DtpConfirmation.Checked = False, "NULL", "N'" & DtpConfirmation.Value.ToString("yyyy-M-d h:mm:ss tt") & "'") & "," _
            '                  & " Prefix = '" & CmbPrefix.Text & "',stateid = " & IIf(cmbState.SelectedIndex = 0, "Null", cmbState.SelectedValue) & ",RegionId = " & IIf(cmbRegion.SelectedIndex = 0, "Null", cmbRegion.SelectedValue) & ",zoneid = " & IIf(cmbZone.SelectedIndex = 0, "Null", cmbZone.SelectedValue) & ",beltid = " & IIf(cmbBelt.SelectedIndex = 0, "Null", cmbBelt.SelectedValue) & ",Factor_Rate=" & Val(Me.txtFactorRate.Text.Replace("'", "''").ToString) & " Where Employee_ID= " & txtEmpID.Text & ""
            'Ali Faisal : TFS1250 : Altered to Update CNIC Expiry Date
            objCommand.CommandText = "Update tblDefEmployee set Employee_Name=N'" & txtName.Text.Replace("'", "''") & "',Employee_Code=N'" & txtCode.Text & "', Father_Name=N'" & txtFather.Text.Replace("'", "''") & "', " _
                              & " NIC=N'" & txtNIC.Text & "', NTN=N'" & txtNTN.Text & "', Gender=N'" & ddlGender.SelectedItem.ToString & "', " _
                              & " Martial_Status=N'" & ddlMaritalStatus.SelectedItem.ToString & "', Religion=N'" & txtReligion.Text.Replace("'", "''") & "', DOB=N'" & dtpDOB.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                              & " City_ID=" & IIf(ddlCity.SelectedIndex = 0, "Null", ddlCity.SelectedValue) & ", Address=N'" & txtAddress.Text.Replace("'", "''") & "', Phone=N'" & txtPhone.Text.Replace("'", "''") & "', Mobile=N'" & txtMobile.Text & "', Email=N'" & txtEmail.Text.Replace("'", "''") & "', " _
                              & " Joining_Date=N'" & dtpJoining.Value.ToString("yyyy-M-d h:mm:ss tt") & "', Dept_ID=" & IIf(ddlDept.SelectedIndex = 0, "Null", ddlDept.SelectedValue) & ", Desig_ID=" & IIf(ddlDesignation.SelectedIndex = 0, "Null", ddlDesignation.SelectedValue) & ", Salary=" & Val(txtSalary.Text) & ", " _
                              & " Active=" & Abs(Val(chkActive.Checked)) & ", Leaving_Date=" & IIf(chkActive.Checked = True, "NULL", IIf(dtpLeaving.Checked = True, "'" & dtpLeaving.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL")) & ", Comments=N'" & txtComments.Text.Replace("'", "''") & "' , SalePerson=" & IIf(Me.chkSalePerson.Checked = False, 0, 1) & ", EmpSalaryAccountId= " & EmpSalaryAccountId & ",Reference = N'" & Me.txtrefrance.Text.Replace("'", "''") & "', PessiNo = N'" & Me.txtpessiNo.Text.Replace("'", "''") & "',EobiNo = N'" & Me.txtEobiNo.Text.Replace("'", "''") & "', EmpPicture = N'" & _strImagePath & "', ShiftGroupId=" & Val(Me.cmbShiftGroup.SelectedValue) & ", ReceiveableAccountId=" & Val(Me.EmpReceiveableAccountId) & ", Sale_Order_Person=" & IIf(Me.chkSaleOrderPerson.Checked = True, 1, 0) & ", AlternateEmpNo=" & Val(Me.txtAlternateEmpNo.Text) & ", AttendanceDate=" & IIf(Me.dtpAttendanceDate.Checked = False, "NULL", "N'" & dtpAttendanceDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", ContractEndingDate=" & IIf(Me.dtpContractEndingDate.Checked = False, "NULL", "N'" & dtpContractEndingDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", " _
                              & " Family_Code=N'" & txtFamilyCode.Text.Replace("'", "''") & "', ID_Remark= N'" & txtIdRemark.Text & "',Qualification=" & IIf(cmbQualification.Text.Length = 0, "NULL", "N'" & cmbQualification.Text.Replace("'", "''") & "'") & ",Blood_Group=" & IIf(cmbBloodGroup.Text.Length = 0, "NULL", "N'" & cmbBloodGroup.Text & "'") & ",Language=" & IIf(cmbLanguage.Text.Length = 0, "NULL", "N'" & cmbLanguage.Text.Replace("'", "''") & "'") & ",Social_Security_No=N'" & txtSocialSecurityNo.Text.Replace("'", "''") & "',Insurance_No=N'" & txtInsuranceNo.Text.Replace("'", "''") & "',Emergency_No=N'" & txtEmergencyNo.Text.Replace("'", "''") & "',Passport_No=N'" & txtPassportNo.Text.Replace("'", "''") & "',BankAccount_No=N'" & txtBankAccountNo.Text.Replace("'", "''") & "',NIC_Place=N'" & txtNicPlace.Text.Replace("'", "''") & "',Domicile=" & IIf(cmbDomicile.Text.Length = 0, "NULL", "N'" & cmbDomicile.Text.Replace("'", "''") & "'") & ",Relation=" & IIf(cmbRelation.Text.Length = 0, "NULL", "N'" & cmbRelation.Text.Replace("'", "''") & "'") & ",InReplacementNewCode=N'" & txtReplacementNewCode.Text.Replace("'", "''") & "',Previous_Code=N'" & txtPreviousCode.Text.Replace("'", "''") & "',Last_Update=N'" & dtpLastUpdate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',JobType=" & IIf(cmbJobType.Text.Length = 0, "NULL", "N'" & cmbJobType.Text.Replace("'", "''") & "'") & ",Dept_Division= " & IIf(cmbDivision.SelectedIndex = 0, 0, cmbDivision.SelectedValue) & ",PayRoll_Division= " & IIf(cmbPayrollDivision.SelectedIndex = 0, 0, cmbPayrollDivision.SelectedValue) & ", RefEmployeeId=" & IIf(Me.cmbReferenceEmployee.SelectedIndex > 0, Me.cmbReferenceEmployee.SelectedValue, "NULL") & ", Bank_Ac_Name=" & IIf(cmbBankAcName.Text.Length > 0, "'" & Me.cmbBankAcName.Text.Replace("'", "''") & "'", "NULL") & ", " _
                              & " EmployeeTypeId = " & IIf(CmbEmployeeType.SelectedIndex = 0, "Null", CmbEmployeeType.SelectedValue) & ", " _
                              & " ConfirmationDate = " & IIf(Me.DtpConfirmation.Checked = False, "NULL", "N'" & DtpConfirmation.Value.ToString("yyyy-M-d h:mm:ss tt") & "'") & "," _
                              & " Prefix = '" & CmbPrefix.Text & "',stateid = " & IIf(cmbState.SelectedIndex = 0, "Null", cmbState.SelectedValue) & ",RegionId = " & IIf(cmbRegion.SelectedIndex = 0, "Null", cmbRegion.SelectedValue) & ",zoneid = " & IIf(cmbZone.SelectedIndex = 0, "Null", cmbZone.SelectedValue) & ",beltid = " & IIf(cmbBelt.SelectedIndex = 0, "Null", cmbBelt.SelectedValue) & ", IsDailyWages= " & IIf(Me.cbDailyWages.Checked = True, 1, 0) & ", CostCentre = " & IIf(Me.cmbCostCentre.SelectedIndex <= 0, 0, Me.cmbCostCentre.SelectedValue) & ", CNICExpiryDate = '" & dtpCNICExpiry.Value.ToString("yyyy-M-d h:mm:ss tt") & "',ReportingTo = " & IIf(cmbReportingTo.SelectedIndex = 0, "Null", cmbReportingTo.SelectedValue) & ", OfficialEmail=N'" & txtOfficialEmail.Text.Replace("'", "''") & "' Where Employee_ID= " & txtEmpID.Text & ""

            '
            'EmployeeTypeId = " & IIf(CmbEmployeeType.SelectedIndex = 0, "Null", CmbEmployeeType.SelectedValue) & "
            'Altered Against Task#201506014 Update Employee Type and Leaving Date Ali Ansari


            objCommand.ExecuteNonQuery()

            Dim intEmployeeAcId As Integer = 0I
            If EmpSalaryAccountId = 0 Then
                '' Create New Salary Account 
                objCommand.CommandText = ""
                objCommand.CommandText = "INSERT INTO  tblCOAMainSubSubDetail(main_sub_sub_id,detail_code,detail_title,Active) VALUES(" & intEmployeeAccountHeadId & ", N'" & GetCOACode(intEmployeeAccountHeadId, trans) & "',N'" & Me.txtName.Text.Replace("'", "''") & " SALARY PAY A/C',1) Select @@Identity"
                intEmployeeAcId = objCommand.ExecuteScalar()


                '' Update Salary Account In Employee Information 
                objCommand.CommandText = ""
                objCommand.CommandText = "Update tblDefEmployee Set EmpSalaryAccountId=" & intEmployeeAcId & " WHERE Employee_Id=" & Val(txtEmpID.Text) & ""
                objCommand.ExecuteNonQuery()

            Else

                objCommand.CommandText = ""
                objCommand.CommandText = "Select Employee_Id from tblDefEmployee WHERE EmpSalaryAccountId=" & EmpSalaryAccountId & ""
                Dim intId As Integer = objCommand.ExecuteScalar()

                If intId = 0 Then

                    objCommand.CommandText = ""
                    objCommand.CommandText = "Update tblDefEmployee Set EmpSalaryAccountId=" & EmpSalaryAccountId & " WHERE Employee_Id=" & Val(txtEmpID.Text) & ""
                    objCommand.ExecuteNonQuery()

                    '' End ................................
                End If
            End If


            objCommand.CommandText = ""
            objCommand.CommandText = "Select Count(*) FROM USERINFO WHERE TAGID=" & Val(txtEmpID.Text) & ""
            'da = Nothing
            dt.Clear()
            da.SelectCommand = objCommand
            da.Fill(dt)
            dt.AcceptChanges()

            If Val(dt.Rows(0).Item(0).ToString) <= 0 Then

                objCommand.CommandText = ""
                objCommand.CommandText = "Select ISNULL(Max(Convert(int,IsNull(BADGENUMBER,0))),0)+1 From USERINFO "
                da.SelectCommand = objCommand
                da.Fill(dt)


                objCommand.CommandText = ""
                objCommand.CommandText = "INSERT INTO USERINFO(BADGENUMBER, SSN,NAME,GENDER,TITLE,BIRTHDAY,HIREDDAY,OPHONE,Type,TagId,LUNCHDURATION) VALUES( " _
                & " N'" & Val(dt.Rows(0).Item(0).ToString) & "', N'" & Me.txtCode.Text.Replace("'", "''") & "',N'" & Me.txtName.Text.Replace("'", "''") & "', " _
                & " N'" & IIf(Me.ddlGender.Text = "Female", "F", "M") & "', N'" & Me.ddlDesignation.Text & "', Convert(DateTime,N'" & Me.dtpDOB.Value.Date & "',102), Convert(DateTime,N'" & Me.dtpJoining.Value.Date & "',102),  " _
                & " N'" & Me.txtMobile.Text.Replace("'", "''") & "', 'Employee'," & Val(Me.txtEmpID.Text) & ",1)Select @@Identity"
                Dim objUserId As Object = objCommand.ExecuteScalar()

                objCommand.CommandText = ""
                objCommand.CommandText = "UPDATE USERINFO SET BADGENUMBER=N'" & objUserId & "' WHERE USERID=" & objUserId & ""
                objCommand.ExecuteNonQuery()

                objCommand.CommandText = ""
                objCommand.CommandText = "Update tblDefEmployee Set AlternateEmpNo=" & objUserId & " WHERE Employee_Id=" & Val(Me.txtEmpID.Text) & ""
                objCommand.ExecuteNonQuery()


            Else


                Dim dt2 As New DataTable
                Dim da2 As New OleDbDataAdapter
                objCommand.CommandText = ""
                objCommand.CommandText = "Select Count(*) From USERINFO  WHERE BADGENUMBER=N'" & Val(Me.txtAlternateEmpNo.Text) & "'"
                da2.SelectCommand = objCommand
                da2.Fill(dt2)
                dt2.AcceptChanges()
                dt2.AcceptChanges()

                If Val(dt2.Rows(0).Item(0).ToString) > 1 Then


                    Dim dt3 As New DataTable
                    Dim da3 As New OleDbDataAdapter
                    objCommand.CommandText = ""
                    objCommand.CommandText = "Select ISNULL(Max(Convert(int,IsNull(BADGENUMBER,0))),0)+1 From USERINFO "
                    da3.SelectCommand = objCommand
                    da3.Fill(dt3)
                    dt3.AcceptChanges()

                    Me.txtAlternateEmpNo.Text = Val(dt3.Rows(0).Item(0).ToString)


                    objCommand.CommandText = ""
                    objCommand.CommandText = "UPDATE USERINFO SET BADGENUMBER=N'" & Val(Me.txtAlternateEmpNo.Text) & "' WHERE TagId=" & Val(Me.txtEmpID.Text) & ""
                    objCommand.ExecuteNonQuery()

                End If


                objCommand.CommandText = ""
                objCommand.CommandText = "UPDATE USERINFO SET SSN=N'" & Me.txtCode.Text.Replace("'", "''") & "',NAME=N'" & Me.txtName.Text.Replace("'", "''") & "',GENDER=N'" & IIf(Me.ddlGender.Text = "Female", "F", "M") & "',TITLE=N'" & Me.ddlDesignation.Text & "',BIRTHDAY=Convert(DateTime,N'" & Me.dtpDOB.Value.Date & "',102),HIREDDAY=Convert(DateTime,N'" & Me.dtpJoining.Value.Date & "',102),STREET=N'" & Me.txtAddress.Text.Replace("'", "''") & "',CITY=N'" & Me.ddlCity.Text & "',OPHONE=N'" & Me.txtMobile.Text.Replace("'", "''") & "' WHERE TagId=" & Val(Me.txtEmpID.Text) & ""
                objCommand.ExecuteNonQuery()

            End If
            ''TFS4433 : Update Attachments against Document
            If arrFile IsNot Nothing Then

                If arrFile.Count > 0 Then
                    SaveDocument(Val(txtEmpID.Text), Me.Name, trans)
                End If

            End If
            ''End TFS4433
            trans.Commit()

            If Update1() = True Then
                Update_Record = True
            End If
            Update_Record = True

            Try
                ''insert Activity Log
                SaveActivityLog("Config", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, txtEmpID.Text, True)
            Catch ex As Exception

            End Try

        Catch ex As Exception
            trans.Rollback()
            Update_Record = False
            Throw ex
        End Try


    End Function

    Sub EditRecord()
        Try

            txtEmpID.Text = grdSaved.CurrentRow.Cells(enmEmployee.EmployeeID).Value
            txtName.Text = grdSaved.CurrentRow.Cells(enmEmployee.EmployeeName).Value
            txtCode.Text = grdSaved.CurrentRow.Cells(enmEmployee.EmployeeCode).Value
            txtFather.Text = grdSaved.CurrentRow.Cells(enmEmployee.FatherName).Value & ""
            txtNIC.Text = grdSaved.CurrentRow.Cells(enmEmployee.NIC).Value & ""
            txtNTN.Text = grdSaved.CurrentRow.Cells(enmEmployee.NTN).Value & ""
            ddlGender.SelectedIndex = ddlGender.FindStringExact((grdSaved.CurrentRow.Cells(enmEmployee.Gender).Value)) 'grdSaved.currentrow.Cells(8).Value & ""
            ddlMaritalStatus.SelectedIndex = ddlMaritalStatus.FindStringExact((grdSaved.CurrentRow.Cells(enmEmployee.MartialStatus).Value))
            txtReligion.Text = grdSaved.CurrentRow.Cells(enmEmployee.Religion).Value & ""
            dtpDOB.Value = grdSaved.CurrentRow.Cells(enmEmployee.DOB).Value & ""
            If grdSaved.CurrentRow.Cells(enmEmployee.City_ID).Value & "" <> "" Then
                ddlCity.SelectedValue = Val(grdSaved.CurrentRow.Cells(enmEmployee.City_ID).Value)
            Else
                ddlCity.SelectedIndex = 0
            End If

            txtAddress.Text = grdSaved.CurrentRow.Cells(enmEmployee.Address).Value & ""
            txtPhone.Text = grdSaved.CurrentRow.Cells(enmEmployee.Phone).Value & ""

            txtMobile.Text = grdSaved.CurrentRow.Cells(enmEmployee.Mobile).Value & ""
            txtEmail.Text = grdSaved.CurrentRow.Cells(enmEmployee.Email).Value & ""

            dtpJoining.Value = grdSaved.CurrentRow.Cells(enmEmployee.JoiningDate).Value & ""
            If grdSaved.CurrentRow.Cells(enmEmployee.DepartmentID).Value & "" <> "" Then
                ddlDept.SelectedValue = Val(grdSaved.CurrentRow.Cells(enmEmployee.DepartmentID).Value)
            Else
                ddlDept.SelectedIndex = 0
            End If
            If grdSaved.CurrentRow.Cells(enmEmployee.Desig_ID).Value & "" <> "" Then
                ddlDesignation.SelectedValue = Val(grdSaved.CurrentRow.Cells(enmEmployee.Desig_ID).Value)
            Else
                ddlDesignation.SelectedIndex = 0
            End If
            txtSalary.Text = grdSaved.CurrentRow.Cells(enmEmployee.Salary).Value & ""
            If Not IsDBNull(grdSaved.CurrentRow.Cells(enmEmployee.Active).Value) Then
                chkActive.Checked = grdSaved.CurrentRow.Cells(enmEmployee.Active).Value
            Else
                chkActive.Checked = False
            End If
            If grdSaved.CurrentRow.Cells(enmEmployee.LeavingDate).Value & "" <> "" Then
                dtpLeaving.Value = grdSaved.CurrentRow.Cells(enmEmployee.LeavingDate).Value & ""
            End If
            txtComments.Text = grdSaved.CurrentRow.Cells(enmEmployee.Comments).Value & ""
            If Not IsDBNull(grdSaved.GetRow.Cells(enmEmployee.SalePerson).Value) Then
                chkSalePerson.Checked = grdSaved.GetRow.Cells(enmEmployee.SalePerson).Value
            Else
                chkSalePerson.Checked = False
            End If
            If Not IsDBNull(grdSaved.GetRow.Cells(enmEmployee.Sale_Order_Person).Value) Then
                Me.chkSaleOrderPerson.Checked = grdSaved.GetRow.Cells(enmEmployee.Sale_Order_Person).Value
            Else
                Me.chkSaleOrderPerson.Checked = False
            End If
            If Not IsDBNull(grdSaved.GetRow.Cells("IsDailyWages").Value) Then
                Me.cbDailyWages.Checked = grdSaved.GetRow.Cells("IsDailyWages").Value
            Else
                Me.cbDailyWages.Checked = False
            End If
            'IsDailyWages
            Me.cmbCostCentre.SelectedValue = Val(Me.grdSaved.GetRow.Cells("CostCentre").Value.ToString)

            EmpSalaryAccountId = Me.grdSaved.GetRow.Cells(enmEmployee.EmpAccountId).Value
            Me.txtAccountDescription.Text = Me.grdSaved.GetRow.Cells(enmEmployee.EmpAccountDesc).Text
            Me.txtrefrance.Text = grdSaved.CurrentRow.Cells(enmEmployee.Reference).Text.ToString
            Me.txtpessiNo.Text = grdSaved.CurrentRow.Cells(enmEmployee.PessiNo).Text.ToString
            Me.txtEobiNo.Text = grdSaved.CurrentRow.Cells(enmEmployee.EobiNo).Text.ToString
            Me.cmbShiftGroup.SelectedValue = grdSaved.GetRow.Cells(enmEmployee.ShiftGroupId).Value
            If Not IsDBNull(grdSaved.CurrentRow.Cells(enmEmployee.EmpPicture).Text) Then
                If IO.File.Exists(grdSaved.CurrentRow.Cells(enmEmployee.EmpPicture).Text) Then
                    Try
                        _strImagePath = grdSaved.CurrentRow.Cells(enmEmployee.EmpPicture).Text
                        Me.pbemployee.ImageLocation = grdSaved.CurrentRow.Cells(enmEmployee.EmpPicture).Text
                        pbemployee.Update()

                    Catch ex As Exception

                    End Try
                Else
                    Me.pbemployee.Image = Nothing
                End If
            Else
                Me.pbemployee.Image = Nothing
            End If
            Me.txtAlternateEmpNo.Text = Me.grdSaved.GetRow.Cells(enmEmployee.AlternateEmpNo).Value.ToString
            Me.EmpReceiveableAccountId = Me.grdSaved.GetRow.Cells(enmEmployee.EmpReceiveableAccountId).Value
            Me.txtReceiveableAccount.Text = Me.grdSaved.GetRow.Cells(enmEmployee.EmpReceiveableAccountDesc).Value.ToString
            If IsDBNull(Me.grdSaved.GetRow.Cells(enmEmployee.AttendanceDate).Value) Then
                Me.dtpAttendanceDate.Value = Now
                Me.dtpAttendanceDate.Checked = False
            Else

                Me.dtpAttendanceDate.Value = Me.grdSaved.GetRow.Cells(enmEmployee.AttendanceDate).Value
                Me.dtpAttendanceDate.Checked = True
            End If
            ''Start TFS2645
            If IsDBNull(Me.grdSaved.GetRow.Cells(enmEmployee.ContractEndingDate).Value) Then
                Me.dtpContractEndingDate.Value = Now
                Me.dtpContractEndingDate.Checked = False
            Else

                Me.dtpContractEndingDate.Value = Me.grdSaved.GetRow.Cells(enmEmployee.ContractEndingDate).Value
                Me.dtpContractEndingDate.Checked = True
            End If
            ''End TFS2645
            txtFamilyCode.Text = Me.grdSaved.GetRow.Cells(enmEmployee.Family_Code).Text
            Me.txtIdRemark.Text = Me.grdSaved.GetRow.Cells(enmEmployee.ID_Remark).Text
            Me.cmbQualification.Text = Me.grdSaved.GetRow.Cells(enmEmployee.Qualification).Text
            Me.cmbBloodGroup.Text = Me.grdSaved.GetRow.Cells(enmEmployee.Blood_Group).Text
            Me.cmbLanguage.Text = Me.grdSaved.GetRow.Cells(enmEmployee.Language).Text
            Me.txtSocialSecurityNo.Text = Me.grdSaved.GetRow.Cells(enmEmployee.Social_Security_No).Text
            Me.txtInsuranceNo.Text = Me.grdSaved.GetRow.Cells(enmEmployee.Insurance_No).Text
            Me.txtEmergencyNo.Text = Me.grdSaved.GetRow.Cells(enmEmployee.Emergency_No).Text
            Me.txtPassportNo.Text = Me.grdSaved.GetRow.Cells(enmEmployee.Passport_No).Text
            Me.txtBankAccountNo.Text = Me.grdSaved.GetRow.Cells(enmEmployee.BankAccount_No).Text
            Me.txtNicPlace.Text = Me.grdSaved.GetRow.Cells(enmEmployee.NIC_Place).Text
            Me.cmbDomicile.Text = Me.grdSaved.GetRow.Cells(enmEmployee.Domicile).Text
            Me.cmbRelation.Text = Me.grdSaved.GetRow.Cells(enmEmployee.Relation).Text
            Me.txtReplacementNewCode.Text = Me.grdSaved.GetRow.Cells(enmEmployee.InReplacementNewCode).Text
            Me.txtPreviousCode.Text = Me.grdSaved.GetRow.Cells(enmEmployee.Previous_Code).Text
            If IsDBNull(Me.grdSaved.GetRow.Cells(enmEmployee.Last_Update).Value) Then
                Me.dtpLastUpdate.Value = Date.Now
            Else
                Me.dtpLastUpdate.Value = Me.grdSaved.GetRow.Cells(enmEmployee.Last_Update).Value
            End If
            Me.cmbJobType.Text = Me.grdSaved.GetRow.Cells(enmEmployee.JobType).Text
            Me.cmbDivision.SelectedValue = Me.grdSaved.GetRow.Cells(enmEmployee.Dept_Division).Value
            Me.cmbPayrollDivision.SelectedValue = Me.grdSaved.GetRow.Cells(enmEmployee.PayRoll_Division).Value
            Me.cmbReferenceEmployee.SelectedValue = Me.grdSaved.GetRow.Cells(enmEmployee.RefEmployeeId).Value 'Task: 2747 Get Ref Employee Id
            If Not IsDBNull(Me.grdSaved.GetRow.Cells("Bank_Ac_Name").Value) Then
                Me.cmbBankAcName.Text = Me.grdSaved.GetRow.Cells("Bank_Ac_Name").Value.ToString
            Else
                If Not Me.cmbBankAcName.SelectedIndex = -1 Then Me.cmbBankAcName.SelectedIndex = 0
            End If
            'Altered By Ali Ansari Task# 20150505
            Me.CmbPrefix.Text = Me.grdSaved.GetRow.Cells("Prefix").Value.ToString
            'Altered By Ali Ansari Task# 20150505
            'Altered By Ali Ansari Task# 20150511 displaying state,region,zone,belt
            If grdSaved.CurrentRow.Cells(enmEmployee.StateID).Value & "" <> "" Then
                cmbState.SelectedValue = Val(grdSaved.CurrentRow.Cells(enmEmployee.StateID).Value)
            Else
                cmbState.SelectedIndex = 0
            End If
            If grdSaved.CurrentRow.Cells("ReportingTo").Value & "" <> "" Then
                cmbReportingTo.SelectedValue = Val(grdSaved.CurrentRow.Cells("ReportingTo").Value)
            Else
                cmbReportingTo.SelectedIndex = 0
            End If

            txtOfficialEmail.Text = grdSaved.CurrentRow.Cells("OfficialEmail").Value.ToString
            If grdSaved.CurrentRow.Cells(enmEmployee.RegionID).Value & "" <> "" Then
                cmbRegion.SelectedValue = Val(grdSaved.CurrentRow.Cells(enmEmployee.RegionID).Value)
            Else
                cmbRegion.SelectedIndex = 0
            End If

            If grdSaved.CurrentRow.Cells(enmEmployee.ZoneId).Value & "" <> "" Then
                cmbZone.SelectedValue = Val(grdSaved.CurrentRow.Cells(enmEmployee.ZoneId).Value)
            Else
                cmbZone.SelectedIndex = 0
            End If

            ''07-June-2015 Task# 07B-June-2015 AhmadSharif: Combo box fill when edit
            If grdSaved.CurrentRow.Cells(enmEmployee.City_ID).Value & "" <> "" Then
                ddlCity.SelectedValue = Val(grdSaved.CurrentRow.Cells(enmEmployee.City_ID).Value)
            End If
            ''end Task# 07B-June-2015

            If grdSaved.CurrentRow.Cells(enmEmployee.BeltId).Value & "" <> "" Then
                cmbBelt.SelectedValue = Val(grdSaved.CurrentRow.Cells(enmEmployee.BeltId).Value)
            Else
                cmbBelt.SelectedIndex = 0
            End If



            'Altered By Ali Ansari Task# 20150511 displaying state,region,zone,belt

            'Altered By Ali Ansari Task# 201506014 displaying Employee Type ID

            If grdSaved.CurrentRow.Cells(enmEmployee.EmployeeTypeId).Value & "" <> "" Then
                CmbEmployeeType.SelectedValue = Val(grdSaved.CurrentRow.Cells(enmEmployee.EmployeeTypeId).Value)
            End If
            If grdSaved.CurrentRow.Cells(enmEmployee.ConfirmationDate).Value & "" <> "" Then
                DtpConfirmation.Value = grdSaved.CurrentRow.Cells(enmEmployee.ConfirmationDate).Value & ""
            End If

            'Me.txtFactorRate.Text = Me.grdSaved.CurrentRow.Cells(enmEmployee.Factor).Value.ToString       'Task#18082015 edit by  Ahmad Sharif
            'Altered By Ali Ansari Task# 201506014 displaying Employee Type ID
            'Ali Faisal : TFS1250 : Fill control value with CNIC expiry date
            If IsDBNull(Me.grdSaved.GetRow.Cells("CNICExpiryDate").Value) Then
                Me.dtpCNICExpiry.Value = Date.Now
            Else
                Me.dtpCNICExpiry.Value = Me.grdSaved.CurrentRow.Cells("CNICExpiryDate").Value & ""
            End If

            BtnSave.Text = "&Update"
            'Code By Imran Ali
            '6-25-2013

            EmpAcDetail(Val(Me.txtEmpID.Text)) 'Call Method of Employee Account Detail
            ''Start TFS4433
            arrFile = New List(Of String)
            Dim intCountAttachedFiles As Integer = 0I
            If Me.BtnSave.Text <> "&Save" Then
                If Me.grdSaved.RowCount > 0 Then
                    intCountAttachedFiles = Val(grdSaved.CurrentRow.Cells("No_of_Attachment").Value)
                    Me.btnAttachments.Text = "Attachment (" & intCountAttachedFiles & ")"
                End If
            End If

            ''End TFS4433
            GetSecurityRights()
            Me.TABHISTORY.SelectedTab = Me.TABHISTORY.Tabs(0).TabPage.Tab
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            msg_Error("An error occured while opening record: " & ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Try
            RefreshControls()

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Exception handling on 05-Nov-2018
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Try
            If FormValidate() Then
                If BtnSave.Text = "&Save" Or BtnSave.Text = "Save" Then
                    'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    If Save() Then
                        If Not _strImagePath = String.Empty Then
                            If Not pbemployee Is Nothing Then
                                Try

                                    Dim DirInfo As IO.DirectoryInfo = New IO.DirectoryInfo(_EmployeePicPath)
                                    Dim FolderSecurity As New Security.AccessControl.DirectorySecurity
                                    FolderSecurity.AddAccessRule(New Security.AccessControl.FileSystemAccessRule(Environment.UserDomainName & "\\" & Environment.UserName, Security.AccessControl.FileSystemRights.FullControl, Security.AccessControl.AccessControlType.Allow))
                                    DirInfo.SetAccessControl(FolderSecurity)

                                    If File.Exists(_strImagePath) Then
                                        File.Delete(_strImagePath)
                                        'Dim fs As FileStream = File.OpenRead(_strImagePath)
                                        'Dim Buffer1(fs.Length) As Byte
                                        'fs.Read(Buffer1, 0, Buffer1.Length)
                                        pbemployee.Image.Save(_strImagePath, System.Drawing.Imaging.ImageFormat.Jpeg)
                                        'fs.Flush()
                                        'fs.Dispose()
                                        'fs.Close()
                                    Else
                                        pbemployee.Image.Save(_strImagePath, System.Drawing.Imaging.ImageFormat.Jpeg)
                                    End If
                                Catch ex As Exception
                                    Throw ex
                                Finally
                                    Me.lblProgress.Visible = False

                                End Try
                            End If
                        End If
                        'msg_Information(str_informSave)
                        Me.lblProgress.Visible = False 'Task:2593 Set Status
                        RefreshControls()
                    Else
                        'msg_Error("Record has not been added")
                        Throw New Exception

                    End If
                Else
                    'Update Query
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update_Record() Then
                        Me.lblProgress.Text = "Processing Pleae Wait ..."
                        Me.lblProgress.Visible = True
                        Application.DoEvents()
                        If Not _strImagePath = String.Empty Then
                            If Not pbemployee Is Nothing Then
                                Try
                                    Dim DirInfo As IO.DirectoryInfo = New IO.DirectoryInfo(_EmployeePicPath)
                                    Dim FolderSecurity As New Security.AccessControl.DirectorySecurity
                                    FolderSecurity.AddAccessRule(New Security.AccessControl.FileSystemAccessRule(Environment.UserDomainName & "\\" & Environment.UserName, Security.AccessControl.FileSystemRights.FullControl, Security.AccessControl.AccessControlType.Allow))
                                    DirInfo.SetAccessControl(FolderSecurity)

                                    If File.Exists(_strImagePath) Then
                                        File.Delete(_strImagePath)
                                        'Dim fs As FileStream = File.OpenRead(_strImagePath)
                                        'Dim Buffer1(fs.Length) As Byte
                                        'fs.Read(Buffer1, 0, Buffer1.Length)
                                        pbemployee.Image.Save(_strImagePath, System.Drawing.Imaging.ImageFormat.Jpeg)
                                        'fs.Flush()
                                        'fs.Dispose()
                                        'fs.Close()
                                    Else
                                        pbemployee.Image.Save(_strImagePath, System.Drawing.Imaging.ImageFormat.Jpeg)
                                    End If

                                Catch ex As Exception
                                    Throw ex
                                Finally
                                    Me.lblProgress.Visible = False

                                End Try
                            End If
                        End If
                        'msg_Information(str_informUpdate)
                        Me.lblProgress.Visible = False 'Task:2593 Set Status
                        RefreshControls()
                    Else
                        'msg_Error("Record has not been updated")
                        Throw New Exception
                    End If
                End If
            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Try
            Me.RefreshControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Exception handling on 05-Nov-2018
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Try
            If Not Me.grdSaved.GetRow Is Nothing Then
                Me.EditRecord()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub DeleteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        If Not Me.grdSaved.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If

        'If IsValidToDelete("tblDefEmployee", "Employee_id", grdSaved.CurrentRow.Cells(0).Value.ToString) = True Then
        If msg_Confirm(str_ConfirmDelete) = True Then
            Try
                Dim cm As New OleDbCommand

                If Con.State = ConnectionState.Closed Then Con.Open()
                cm.Connection = Con

                cm.CommandText = "Select distinct coa_detail_id from tblVoucherDetail WHERE coa_detail_id=" & Val(Me.grdSaved.GetRow.Cells("EmpSalaryAccountId").Value.ToString) & ""
                Dim intId As Integer = cm.ExecuteScalar
                If intId > 0 Then
                    Throw New Exception(str_ErrorDependentRecordFound) : Exit Sub
                End If

                'cm.CommandText = ""
                'cm.CommandText = "Delete From tblCOAMainSubSubDetail WHERE coa_detail_id=" & Val(Me.grdSaved.GetRow.Cells("EmpSalaryAccountId").Value.ToString) & ""
                'cm.ExecuteNonQuery()

                cm.CommandText = ""
                cm.CommandText = "DELETE FROM USERINFO WHERE TagId=" & grdSaved.CurrentRow.Cells(0).Value.ToString & ""
                cm.ExecuteNonQuery()


                cm.CommandText = ""
                cm.CommandText = "delete from tblDefEmployee where Employee_id=" & grdSaved.CurrentRow.Cells(0).Value.ToString
                cm.ExecuteNonQuery()



                If Delete() = True Then
                End If

                msg_Information(str_informDelete)

                Try
                    ''insert Activity Log
                    SaveActivityLog("Config", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, grdSaved.CurrentRow.Cells(0).Value.ToString.ToString, True)
                Catch ex As Exception
                    Throw ex
                End Try
                RefreshControls()
                'Me.DisplayRecord()
            Catch ex As Exception
                msg_Error("Error occured while deleting record: " & ex.Message)
            Finally
                Con.Close()
            End Try
        End If
        'Else
        'msg_Error(str_ErrorDependentRecordFound)
        'End If
    End Sub

    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Me.BtnAddEmployee.Enabled = True
                Me.BtnTarget.Enabled = True
                Me.btnSalaryType.Enabled = True
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
9:              CtrlGrdBar1.mGridChooseFielder.Enabled = True
                'Exit Sub
                'End If
            Else
                If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                    Dim dt As DataTable = GetFormRights(EnumForms.frmDefEmployee)
                    If Not dt Is Nothing Then
                        If Not dt.Rows.Count = 0 Then
                            If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                                Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                            Else
                                Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                            End If
                            Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                            Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                            Me.BtnPrint.Enabled = dt.Rows(0).Item("Set Target_Rights").ToString
                            Me.BtnPrint.Enabled = dt.Rows(0).Item("Add Salary Type_Rights").ToString
                            Me.BtnPrint.Enabled = dt.Rows(0).Item("Add Employee Type_Rights").ToString
                            Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                        End If
                    End If
                Else
                    'Me.Visible = False
                    Me.BtnSave.Enabled = False
                    Me.BtnDelete.Enabled = False
                    Me.BtnPrint.Enabled = False
                    Me.BtnAddEmployee.Enabled = False
                    Me.BtnTarget.Enabled = False
                    Me.btnSalaryType.Enabled = False
                    CtrlGrdBar1.mGridPrint.Enabled = False
                    CtrlGrdBar1.mGridExport.Enabled = False
                    CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    'CtrlGrdBar1.mGridPrint.Enabled = False
                    'CtrlGrdBar1.mGridExport.Enabled = False

                    For i As Integer = 0 To Rights.Count - 1
                        If Rights.Item(i).FormControlName = "View" Then
                            'Me.Visible = True
                        ElseIf Rights.Item(i).FormControlName = "Save" Then
                            If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                        ElseIf Rights.Item(i).FormControlName = "Update" Then
                            If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                        ElseIf Rights.Item(i).FormControlName = "Delete" Then
                            Me.BtnDelete.Enabled = True
                        ElseIf Rights.Item(i).FormControlName = "Print" Then
                            Me.BtnPrint.Enabled = True
                        ElseIf Rights.Item(i).FormControlName = "Set Target" Then
                            Me.BtnPrint.Enabled = True
                        ElseIf Rights.Item(i).FormControlName = "Add Salary Type" Then
                            Me.BtnPrint.Enabled = True
                        ElseIf Rights.Item(i).FormControlName = "Add Employee Type" Then
                            Me.BtnPrint.Enabled = True
                        ElseIf Rights.Item(i).FormControlName = "Print" Then
                            CtrlGrdBar1.mGridPrint.Enabled = True
                        ElseIf Rights.Item(i).FormControlName = "Export" Then
                            CtrlGrdBar1.mGridExport.Enabled = True
                        ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                            CtrlGrdBar1.mGridChooseFielder.Enabled = True
                            'CtrlGrdBar1.mGridPrint.Enabled = True
                            'ElseIf Rights.Item(i).FormControlName = "Export" Then
                            'CtrlGrdBar1.mGridExport.Enabled = True
                            'ElseIf Rights.Item(i).FormControlName = "Post" Then
                            'me.chkPost.Visible = True
                        End If
                    Next
                End If
            End If

            'Task#1 13-Jun-2015 apply security right on tab
            If (Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "Save") AndAlso Me.TABHISTORY.SelectedTab.Index = 0 Then
                Me.BtnDelete.Visible = False
                Me.BtnPrint.Visible = False
                Me.BtnSave.Visible = True
            Else
                Me.BtnDelete.Visible = True
                Me.BtnPrint.Visible = True
                If Me.TABHISTORY.SelectedTab.Index = 1 Then
                    Me.BtnSave.Visible = False
                Else
                    Me.BtnSave.Visible = True
                End If
            End If
            'End Task#1 13-Jun-2015

        Catch ex As Exception
            'msg_Error(ex.Message)
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Exception handling on 05-Nov-2018
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTarget.Click
        Try
            If Val(grdSaved.CurrentRow.Cells(0).Value.ToString) > 0 Then
                frmSalesTarget.id = Val(grdSaved.CurrentRow.Cells(0).Value.ToString)
                frmSalesTarget.ename = grdSaved.CurrentRow.Cells(1).Value.ToString
                ApplyStyleSheet(frmSalesTarget)
                frmSalesTarget.ShowDialog()

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSaved_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        'If Not Me.grdSaved.GetRow Is Nothing Then
        '    Me.EditRecord()
        'End If

        ''Task#1 13-06-2015 Add Check on grdSaved to check on double click if row less than zero than exit
        Try
            If Me.grdSaved.Row < 0 Then
                Exit Sub
            Else
                Me.EditRecord()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
        ''End Task#1 13-06-2015

    End Sub

    Private Sub btnpicturebrowes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpicturebrowes.Click
        Try
            'Task#1 13-Jun-2015 Check if Path greater then zero then if path doesn't exist then create path first
            If _EmployeePicPath.Length > 0 Then
                If Not System.IO.Directory.Exists(_EmployeePicPath) Then
                    System.IO.Directory.CreateDirectory(_EmployeePicPath)
                End If
            End If
            'End Task#1 13-Jun-2015

            If Not IO.Directory.Exists(_EmployeePicPath) Then
                ShowErrorMessage("Folder not exist")
                Me.btnpicturebrowes.Focus()
                Exit Sub
            End If

            If Me.txtName.Text = String.Empty Then Exit Sub
            If Me.txtCode.Text = String.Empty Then Exit Sub
            'Dim a As New OpenFileDialog
            Me.OpenFileDialog1.Filter = "Image File |*.*jpg"
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                If System.IO.File.Exists(Me.OpenFileDialog1.FileName) Then
                    _strImagePath = _EmployeePicPath & "\" + OpenFileDialog1.FileName.Replace(OpenFileDialog1.FileName, Me.txtName.Text + "-" + Me.txtCode.Text + ".jpg")
                    Me.pbemployee.ImageLocation = OpenFileDialog1.FileName
                    '_strImagePath = str_ApplicationStartUpPath & "\EmployeesPicture\" + a.SafeFileName.Replace(a.SafeFileName, Me.txtName.Text + "-" + Me.txtCode.Text + ".jpg")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
        End Try
    End Sub

    Public Function Get_All(ByVal Id As String)
        Try
            Get_All = Nothing
            If IsLoadedForm = True Then
                Dim dt As DataTable = GetDataTable("Select * From tblDefEmployee WHERE Employee_ID=N'" & Id & "'")
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        txtEmpID.Text = dt.Rows(0).Item("Employee_Id").ToString
                        txtName.Text = dt.Rows(0).Item("Employee_Name").ToString
                        txtOfficialEmail.Text = dt.Rows(0).Item("OfficialEmail").ToString
                        txtCode.Text = dt.Rows(0).Item("Employee_Code").ToString
                        txtFather.Text = dt.Rows(0).Item("Father_Name").ToString
                        txtNIC.Text = dt.Rows(0).Item("NIC").ToString
                        txtNTN.Text = dt.Rows(0).Item("NTN").ToString
                        ddlGender.SelectedIndex = ddlGender.FindStringExact((dt.Rows(0).Item("Gender").ToString)) 'dt.Rows(0).Item(8).Value & ""
                        ddlMaritalStatus.SelectedIndex = ddlMaritalStatus.FindStringExact((dt.Rows(0).Item("Martial_Status").ToString))
                        txtReligion.Text = dt.Rows(0).Item("Religion").ToString
                        dtpDOB.Value = dt.Rows(0).Item("DOB").ToString
                        If dt.Rows(0).Item("City_ID") <> "" Then
                            ddlCity.SelectedValue = Val(dt.Rows(0).Item("City_ID"))
                        Else
                            ddlCity.SelectedIndex = 0
                        End If

                        txtAddress.Text = dt.Rows(0).Item("Address").ToString
                        txtPhone.Text = dt.Rows(0).Item("Phone").ToString

                        txtMobile.Text = dt.Rows(0).Item("Mobile").ToString
                        txtEmail.Text = dt.Rows(0).Item("Email").ToString

                        dtpJoining.Value = dt.Rows(0).Item("Joining_Date").ToString
                        If dt.Rows(0).Item("Dept_ID").ToString <> "" Then
                            ddlDept.SelectedValue = Val(dt.Rows(0).Item("Dept_ID").ToString)
                        Else
                            ddlDept.SelectedIndex = 0
                        End If
                        If dt.Rows(0).Item("Desig_ID").ToString <> "" Then
                            ddlDesignation.SelectedValue = Val(dt.Rows(0).Item("Desig_ID").ToString)
                        Else
                            ddlDesignation.SelectedIndex = 0
                        End If
                        txtSalary.Text = dt.Rows(0).Item("Salary").ToString
                        If Not IsDBNull(dt.Rows(0).Item("Active")) Then
                            chkActive.Checked = dt.Rows(0).Item("Active").ToString
                        Else
                            chkActive.Checked = False
                        End If

                        If Not IsDBNull(dt.Rows(0).Item("IsDailyWages")) Then
                            Me.cbDailyWages.Checked = dt.Rows(0).Item("IsDailyWages").ToString
                        Else
                            Me.cbDailyWages.Checked = False
                        End If
                        'IsDailyWages
                        If dt.Rows(0).Item("Leaving_Date").ToString <> "" Then
                            dtpLeaving.Value = dt.Rows(0).Item("Leaving_Date").ToString
                        End If
                        txtComments.Text = dt.Rows(0).Item("Comments").ToString
                        If Not IsDBNull(dt.Rows(0).Item("SalePerson").ToString) Then
                            chkSalePerson.Checked = dt.Rows(0).Item("SalePerson").ToString
                        Else
                            chkSalePerson.Checked = False
                        End If
                        If Not IsDBNull(dt.Rows(0).Item("Sale_Order_Person").ToString) Then
                            Me.chkSaleOrderPerson.Checked = dt.Rows(0).Item("Sale_Order_Person").ToString
                        Else
                            chkSaleOrderPerson.Checked = False
                        End If
                        If Not IsDBNull(dt.Rows(0).Item("AlternateEmpNo").ToString) Then
                            Me.txtAlternateEmpNo.Text = dt.Rows(0).Item("AlternateEmpNo")
                        Else
                            Me.txtAlternateEmpNo.Text = String.Empty
                        End If
                        EmpSalaryAccountId = dt.Rows(0).Item("EmpSalaryAccountId").Value
                        'Me.txtAccountDescription.Text = dt.Rows(0).Item(enmEmployee.EmpAccountDesc).Text
                        Me.txtrefrance.Text = dt.Rows(0).Item("Reference").ToString
                        Me.txtpessiNo.Text = dt.Rows(0).Item("PessiNo").ToString
                        Me.txtEobiNo.Text = dt.Rows(0).Item("EobiNo").ToString
                        Me.cmbShiftGroup.SelectedValue = dt.Rows(0).Item("ShiftGroupId").ToString
                        EmpReceiveableAccountId = dt.Rows(0).Item("ReceiveableAccountId").ToString
                        'Me.txtReceiveableAccount.Text = dt.Rows(0).Item("ReceiveableAccountDesc").ToString
                        If Not IsDBNull(dt.Rows(0).Item("EmpPicture").ToString) Then
                            If IO.File.Exists(dt.Rows(0).Item("EmpPicture").ToString) Then
                                Try
                                    Dim fs As FileStream = File.OpenRead(dt.Rows(0).Item("EmpPicture").ToString)
                                    Me.pbemployee.Image = Image.FromStream(fs, False, False)
                                Catch ex As Exception

                                End Try
                            Else
                                Me.pbemployee.Image = Nothing
                            End If
                        Else
                            Me.pbemployee.Image = Nothing
                        End If

                        txtFamilyCode.Text = dt.Rows(0).Item("Family_Code").ToString
                        Me.txtIdRemark.Text = dt.Rows(0).Item("ID_Remark").ToString
                        Me.cmbQualification.Text = dt.Rows(0).Item("Qualification").ToString
                        Me.cmbBloodGroup.Text = dt.Rows(0).Item("Blood_Group").ToString
                        Me.cmbLanguage.Text = dt.Rows(0).Item("Language").ToString
                        Me.txtSocialSecurityNo.Text = dt.Rows(0).Item("Social_Security_No").ToString
                        Me.txtInsuranceNo.Text = dt.Rows(0).Item("Insurance_No").ToString
                        Me.txtEmergencyNo.Text = dt.Rows(0).Item("Emergency_No").ToString
                        Me.txtPassportNo.Text = dt.Rows(0).Item("Passport_No").ToString
                        Me.txtBankAccountNo.Text = dt.Rows(0).Item("BankAccount_No").ToString
                        Me.txtNicPlace.Text = dt.Rows(0).Item("NIC_Place").ToString
                        Me.cmbDomicile.Text = dt.Rows(0).Item("Domicile").ToString
                        Me.cmbRelation.Text = dt.Rows(0).Item("Relation").ToString
                        Me.txtReplacementNewCode.Text = dt.Rows(0).Item("InReplacementNewCode").ToString
                        Me.txtPreviousCode.Text = dt.Rows(0).Item("Previous_Code").ToString
                        If IsDBNull(dt.Rows(0).Item("Last_Update").ToString) Then
                            Me.dtpLastUpdate.Value = Date.Now
                        Else
                            Me.dtpLastUpdate.Value = dt.Rows(0).Item("Last_Update").ToString
                        End If
                        Me.cmbJobType.Text = dt.Rows(0).Item("JobType").ToString
                        Me.cmbDivision.SelectedValue = dt.Rows(0).Item("Dept_Division")
                        Me.cmbPayrollDivision.SelectedValue = dt.Rows(0).Item("PayRoll_Division")
                        'Altered Against Task# 20150505 Ali Ansari
                        Me.CmbPrefix.Text = dt.Rows(0).Item("Prefix").ToString
                        'Altered Against Task# 20150505 Ali Ansari

                        'Altered Against Task# 20150511 Ali Ansari adding state,region,zone,belt
                        If dt.Rows(0).Item("Stateid") <> "" Then
                            cmbState.SelectedValue = Val(dt.Rows(0).Item("StateID"))
                        Else
                            cmbState.SelectedIndex = 0
                        End If
                        If dt.Rows(0).Item("ReportingTo") <> "" Then
                            cmbReportingTo.SelectedValue = Val(dt.Rows(0).Item("ReportingTo"))
                        Else
                            cmbReportingTo.SelectedIndex = 0
                        End If
                        If dt.Rows(0).Item("RegionId") <> "" Then
                            cmbRegion.SelectedValue = Val(dt.Rows(0).Item("RegionId"))
                        Else
                            cmbRegion.SelectedIndex = 0
                        End If

                        If dt.Rows(0).Item("ZoneId") <> "" Then
                            cmbZone.SelectedValue = Val(dt.Rows(0).Item("ZoneId"))
                        Else
                            cmbZone.SelectedIndex = 0
                        End If

                        If dt.Rows(0).Item("BeltId") <> "" Then
                            cmbBelt.SelectedValue = Val(dt.Rows(0).Item("BeltId"))
                        Else
                            cmbBelt.SelectedIndex = 0
                        End If
                        'Altered Against Task# 20150511 Ali Ansari adding state,region,zone,belt
                        Me.BtnSave.Text = "&Update"
                        Me.GetSecurityRights()
                        IsDrillDown = True
                    End If
                End If
                IsDrillDown = False
            End If
            Return Get_All
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        Try
            Dim str As String
            Dim Id As Integer = 0I

            If getConfigValueByType("EmployeeHeadAccountId").ToString <> "Error" Then
                intEmployeeAccountHeadId = Val(getConfigValueByType("EmployeeHeadAccountId"))
            End If
            If getConfigValueByType("EmpSimpleAccountHead").ToString <> "Error" Then
                blnEmpSimpleAcHead = getConfigValueByType("EmpSimpleAccountHead")
            End If
            If getConfigValueByType("EmpDepartmentAccountHead").ToString <> "Error" Then
                blnEmpDeptAcHead = getConfigValueByType("EmpDepartmentAccountHead")
            End If
            ''Start TFS3566  19-06-2018 : Ayesha Rehman
            If Not getConfigValueByType("RightBasedCostCenters").ToString = "Error" Then
                flgCostCenterRights = getConfigValueByType("RightBasedCostCenters")
            End If
            ''End TFS3566
            Id = Me.ddlCity.SelectedValue
            str = "Select CityID, CityName from tblListCity"
            FillDropDown(ddlCity, str)
            Me.ddlCity.SelectedValue = Id

            Id = Me.ddlDept.SelectedValue
            'ElseIf strCondition = "Category" Then
            str = "Select EmployeeDeptID, EmployeeDeptName,IsNull(DeptAccountHeadId,0) as DeptAccountHeadId from EmployeeDeptDefTable"
            FillDropDown(ddlDept, str)
            Me.ddlDept.SelectedValue = Id

            Id = Me.ddlDesignation.SelectedValue
            'ElseIf strCondition = "ItemFilter" Then
            str = "Select EmployeeDesignationID, EmployeeDesignationName from EmployeeDesignationDefTable"
            FillDropDown(ddlDesignation, str)
            Me.ddlDesignation.SelectedValue = Id

            Id = Me.cmbShiftGroup.SelectedValue
            'Shifting Group 
            str = "Select ShiftGroupId, ShiftGroupName From ShiftGroupTable WHERE Active=1"
            FillDropDown(Me.cmbShiftGroup, str)
            Me.cmbShiftGroup.SelectedValue = Id


            'Id = Me.cmbType.SelectedIndex
            'str = String.Empty
            'str = "Select SalaryExpTypeId, SalaryExpType From SalaryExpenseType WHERE flgAdvance=1 "
            'FillDropDown(Me.cmbType, str)
            'Me.cmbType.SelectedIndex = Id


            'Task:2747 Call Ref Employee
            Id = Me.cmbReferenceEmployee.SelectedIndex
            FillCombo("RefEmployee")
            Me.cmbReferenceEmployee.SelectedIndex = Id
            'end Task:2747

            'Task:2747 Call Ref Employee
            Id = Me.CmbEmployeeType.SelectedIndex
            FillCombo("EmployeeType")
            Me.CmbEmployeeType.SelectedIndex = Id
            'end Task:2747

            ''Start TFS3566
            Id = Me.cmbCostCentre.SelectedIndex
            If flgCostCenterRights = False Then
                FillDropDown(Me.cmbCostCentre, "Select CostCenterID, Name, Code, SortOrder From tblDefCostCenter Where Active = 1 ORDER BY 2 ASC") ''TASKTFS75 added and set active =1
            Else
                FillDropDown(Me.cmbCostCentre, " SELECT  CostCenterID,Name,Code , SortOrder FROM tblDefCostCenter  where ISNULL(CostCenterID, 0) in (select ISNULL(CostCentre_Id, 0) AS CostCenterId FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) And Active = 1 ORDER BY 2 ASC ") ''TASKTFS75 added and set active =1
            End If
            Me.cmbCostCentre.SelectedIndex = Id
            ''End TFS3566
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            ApplyStyleSheet(frmAddAccount)
            If frmAddAccount.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If frmAddAccount.AccountId > 0 Then
                    EmpSalaryAccountId = frmAddAccount.AccountId
                    Me.txtAccountDescription.Text = frmAddAccount.AccountDesc
                    frmAddAccount.AccountId = 0
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnReceiveableAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReceiveableAccount.Click
        Try
            ApplyStyleSheet(frmAddAccount)
            If frmAddAccount.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If frmAddAccount.AccountId > 0 Then
                    EmpReceiveableAccountId = frmAddAccount.AccountId
                    Me.txtReceiveableAccount.Text = frmAddAccount.AccountDesc
                    frmAddAccount.AccountId = 0
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub btnInsuranceAc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsuranceAc.Click
    '    Try
    '        ApplyStyleSheet(frmAddAccount)
    '        If frmAddAccount.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '            If frmAddAccount.AccountId > 0 Then
    '                EmpInsuranceAccountId = frmAddAccount.AccountId
    '                Me.txtInsurranceAc.Text = frmAddAccount.AccountDesc
    '                frmAddAccount.AccountId = 0
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    'Private Sub btnWHTaxAc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWHTaxAc.Click
    '    Try
    '        ApplyStyleSheet(frmAddAccount)
    '        If frmAddAccount.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '            If frmAddAccount.AccountId > 0 Then
    '                EmpWHTaxAccountId = frmAddAccount.AccountId
    '                Me.txtWHTax.Text = frmAddAccount.AccountDesc
    '                frmAddAccount.AccountId = 0
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    'Private Sub btnEOBIAc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEOBIAc.Click
    '    Try
    '        ApplyStyleSheet(frmAddAccount)
    '        If frmAddAccount.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '            If frmAddAccount.AccountId > 0 Then
    '                EmpEOBIAccountId = frmAddAccount.AccountId
    '                Me.txtEOBIAc.Text = frmAddAccount.AccountDesc
    '                frmAddAccount.AccountId = 0
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    'Private Sub btnESSIAc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnESSIAc.Click
    '    Try
    '        ApplyStyleSheet(frmAddAccount)
    '        If frmAddAccount.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '            If frmAddAccount.AccountId > 0 Then
    '                EmpESSIAccountId = frmAddAccount.AccountId
    '                Me.txtESSIAc.Text = frmAddAccount.AccountDesc
    '                frmAddAccount.AccountId = 0
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    'Private Sub btnGratuityFund_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGratuityFund.Click
    '    Try
    '        ApplyStyleSheet(frmAddAccount)
    '        If frmAddAccount.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '            If frmAddAccount.AccountId > 0 Then
    '                EmpGratuityAccountId = frmAddAccount.AccountId
    '                Me.txtGratuityFund.Text = frmAddAccount.AccountDesc
    '                frmAddAccount.AccountId = 0
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    'Private Sub btnAllowanceAc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllowanceAc.Click
    '    Try
    '        ApplyStyleSheet(frmAddAccount)
    '        If frmAddAccount.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '            If frmAddAccount.AccountId > 0 Then
    '                EmpAllowanceAccountId = frmAddAccount.AccountId
    '                Me.txtAllownaceAc.Text = frmAddAccount.AccountDesc
    '                frmAddAccount.AccountId = 0
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub chkSalePerson_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSalePerson.CheckedChanged
        Try
            If IsLoadedForm = False Then Exit Sub

            'If Me.chkSalePerson.Checked = True Then
            '    Me.chkSalePerson.Checked = True
            '    Me.chkSaleOrderPerson.Checked = False
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub chkSaleOrderPerson_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSaleOrderPerson.CheckedChanged
        Try
            If IsLoadedForm = False Then Exit Sub
            'If Me.chkSaleOrderPerson.Checked = True Then
            '    Me.chkSaleOrderPerson.Checked = True
            '    Me.chkSalePerson.Checked = False
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub btnEmpAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        ApplyStyleSheet(frmAddAccount)
    '        If frmAddAccount.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '            If frmAddAccount.AccountId > 0 Then
    '                EmpSalariesAccountId = frmAddAccount.AccountId
    '                Me.txtEmpAccount.Text = frmAddAccount.AccountDesc
    '                frmAddAccount.AccountId = 0
    '            End If
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub btnAddEmpAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            'Imran Ali
            '25-6-2013 


            'Validation here
            'If Me.cmbType.SelectedIndex = -1 Then Exit Sub
            'If cmbType.SelectedIndex = 0 Then
            '    ShowErrorMessage("Please select account")
            '    Me.btnAddEmpAccount.Focus()
            '    Exit Sub
            'End If
            'If Me.cmbType.SelectedIndex = 0 Then
            '    ShowErrorMessage("Please select salary type")
            '    Me.cmbType.Focus()
            '    Exit Sub
            'End If

            'Dim dt As DataTable = CType(Me.grdSalaryDetail.DataSource, DataTable) ' GridEx DataSource Convert to DataTable
            'Dim dr As DataRow 'Declare Variable of DataRow
            'dr = dt.NewRow 'Set for New Row
            'dr("Account_Id") = EmpSalariesAccountId
            'dr("Type_Id") = Me.cmbType.SelectedValue
            'dr("Account Description") = Me.txtEmpAccount.Text
            'dr("Type") = Me.cmbType.Text
            'dr("Amount") = 0
            'dr("Flag Recv") = 0
            'dt.Rows.InsertAt(dr, 0) 'Row Insert in DataTable

            'dt.AcceptChanges() 'All Change Accept
            'EmpSalariesAccountId = 0 'Reset AccountId
            'Me.txtEmpAccount.Text = String.Empty 'Reset
            'Me.cmbType.SelectedIndex = 0 ' Reset
            'Me.btnAddEmpAccount.Focus() 'Focus

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub EmpAcDetail(ByVal EmpId As Integer)
        Try

            'Imran Ali
            '25-6-2013 

            Dim str As String = String.Empty
            'str = "SELECT dbo.tblEmployeeAccounts.Account_Id, dbo.tblEmployeeAccounts.Type_Id, dbo.vwCOADetail.detail_title AS [Account Description], " _
            '      & " dbo.SalaryExpenseType.SalaryExpType AS Type, ISNULL(tblEmployeeAccounts.Amount,0) as Amount, ISNULL(tblEmployeeAccounts.flgReceivable,0) as [Flag Recv] " _
            '      & " FROM  dbo.SalaryExpenseType INNER JOIN " _
            '      & " dbo.tblEmployeeAccounts ON dbo.SalaryExpenseType.SalaryExpTypeId = dbo.tblEmployeeAccounts.Type_Id INNER JOIN " _
            '      & " dbo.vwCOADetail ON dbo.tblEmployeeAccounts.Account_Id = dbo.vwCOADetail.coa_detail_id WHERE tblEmployeeAccounts.Employee_Id=" & EmpId
            'str = "Select AccountId as Account_Id,SalaryExpTypeId as Type_Id,SalaryExpType as [Type], IsNull(EmpAc.Account_Id,0) as EmpAcId,IsNull(EmpAc.Amount,0) as Amount, IsNull(flgAdvance,0) as flgAdvance From SalaryExpenseType " _
            '      & " LEFT OUTER JOIN(Select Account_Id, Amount,Type_Id,Employee_Id From tblEmployeeAccounts  WHERE Employee_Id=" & EmpId & ") " _
            '      & " EmpAc on EmpAc.Type_Id = SalaryExpenseType.SalaryExptypeId "
            'TASKM169151 Added New Field Value_In
            str = "Select AccountId as Account_Id,SalaryExpTypeId as Type_Id,SalaryExpType as [Type], IsNull(EmpAc.Account_Id,0) as EmpAcId, Isnull(salaryExpenseType.ApplyValue,'Fixed') as [Amount Type], IsNull(EmpAc.Amount,0) as Amount, 0 as NetAmount, IsNull(flgAdvance,0) as flgAdvance, IsNull(flgReceivable,0) as flgReceivable From SalaryExpenseType " _
             & " LEFT OUTER JOIN(Select DISTINCT Account_Id, Amount,Type_Id,Employee_Id,IsNull(flgReceivable,0) as flgReceivable From tblEmployeeAccounts  WHERE Employee_Id=" & EmpId & " AND Employee_Id <> 0) " _
             & " EmpAc on EmpAc.Type_Id = SalaryExpenseType.SalaryExptypeId"
            'End Task TASKM169151

            Dim dt As New DataTable
            dt = GetDataTable(str)
            dt.AcceptChanges()

            If dt Is Nothing Then Exit Sub
            dt.Columns.Add("Action", GetType(System.String))

            dt.Columns("NetAmount").Expression = "iif([Amount Type]='Fixed',[Amount], (iif([Amount Type]='Percentage', (Amount/100) * " & Val(Me.txtSalary.Text) & ", 0)))"

            Me.grdSalaryDetail.DataSource = dt
            Me.grdSalaryDetail.RetrieveStructure()

            Dim dtAc As New DataTable
            dtAc = GetDataTable("Select coa_detail_id, detail_title From vwCOADetail where detail_title <> '' Union Select 0 as coa_detail_id, '' as detail_title ")
            dtAc.AcceptChanges()
            Me.grdSalaryDetail.RootTable.Columns("EmpAcId").Caption = "Account"
            Me.grdSalaryDetail.RootTable.Columns("EmpAcId").HasValueList = True
            Me.grdSalaryDetail.RootTable.Columns("EmpAcId").EditType = Janus.Windows.GridEX.EditType.Combo
            Me.grdSalaryDetail.RootTable.Columns("EmpAcId").ValueList.PopulateValueList(dtAc.DefaultView, "coa_detail_id", "detail_title")
            For c As Integer = 0 To Me.grdSalaryDetail.RootTable.Columns.Count - 1
                If Me.grdSalaryDetail.RootTable.Columns(c).Index <> 3 AndAlso Me.grdSalaryDetail.RootTable.Columns(c).Index <> 5 AndAlso Me.grdSalaryDetail.RootTable.Columns(c).Index <> 7 Then
                    Me.grdSalaryDetail.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
            Me.grdSalaryDetail.RootTable.Columns("Account_Id").Visible = False
            Me.grdSalaryDetail.RootTable.Columns("Type_Id").Visible = False
            Me.grdSalaryDetail.RootTable.Columns("flgAdvance").Visible = False
            Me.grdSalaryDetail.RootTable.Columns("flgReceivable").Caption = "Receivable"
            'Me.grdSalaryDetail.RootTable.Columns("Account Description").Width = 200
            Me.grdSalaryDetail.RootTable.Columns("Type").Width = 150
            Me.grdSalaryDetail.RootTable.Columns("Action").Visible = True
            Me.grdSalaryDetail.RootTable.Columns("Action").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
            Me.grdSalaryDetail.RootTable.Columns("Action").ButtonText = "Delete"
            Me.grdSalaryDetail.RootTable.Columns("Action").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell

            Me.grdSalaryDetail.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdSalaryDetail.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Me.grdSalaryDetail.RootTable.Columns("NetAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grdSalaryDetail.RootTable.Columns("NetAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalaryDetail.RootTable.Columns("NetAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalaryDetail.RootTable.Columns("NetAmount").FormatString = "N"
            Me.grdSalaryDetail.RootTable.Columns("NetAmount").TotalFormatString = "N"

            Me.grdSalaryDetail.RootTable.Columns("Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalaryDetail.RootTable.Columns("Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSalaryDetail.RootTable.Columns("Amount").FormatString = "N"
            Me.grdSalaryDetail.RootTable.Columns("Amount").TotalFormatString = "N"

            Me.grdSalaryDetail.AutoSizeColumns()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            Dim EmpAccount As SBModel.EmployeeAccountsBE
            EmpAccount = New SBModel.EmployeeAccountsBE
            EmpAccount.Employee_Id = grdSaved.CurrentRow.Cells(enmEmployee.EmployeeID).Value
            If New SBDal.EmployeeAccountsDAL().Delete(EmpAccount) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            Me.grdSalaryDetail.UpdateData()
            EmpAccountList = New List(Of SBModel.EmployeeAccountsBE)
            Dim EmpAccount As SBModel.EmployeeAccountsBE 'Declare Variable of Employee Account Class
            For Each objRow As Janus.Windows.GridEX.GridEXRow In Me.grdSalaryDetail.GetRows
                EmpAccount = New SBModel.EmployeeAccountsBE 'Create Object of Employee Account Class
                EmpAccount.Account_Id = IIf(objRow.Cells("flgAdvance").Value = False, Val(objRow.Cells("Account_Id").Value.ToString), Val(objRow.Cells("EmpAcId").Value.ToString))
                EmpAccount.Employee_Id = IIf(identity > 0, identity, Val(Me.txtEmpID.Text))
                EmpAccount.Type_Id = objRow.Cells("Type_Id").Value
                EmpAccount.FlgReceivable = objRow.Cells("flgReceivable").Value
                EmpAccount.Sort_Order = 1
                EmpAccount.Amount = Val(objRow.Cells("Amount").Value.ToString)
                EmpAccount.Active = True
                EmpAccountList.Add(EmpAccount)
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            FillModel()
            If New SBDal.EmployeeAccountsDAL().Save(EmpAccountList) = True Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            FillModel()
            If New SBDal.EmployeeAccountsDAL().Update(EmpAccountList) = True Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub UltraTabControl1_SelectedTabChanging(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles TABHISTORY.SelectedTabChanging
        Try
            If e.Tab.Index = 1 Then
                Me.btnSalaryType.Visible = True
                Me.btnAttachments.Visible = False
                'Me.lblAcEmployeeName.Text = "" & Me.txtName.Text & "-" & Me.txtCode.Text
            Else
                Me.btnSalaryType.Visible = False
                Me.btnAttachments.Visible = True
            End If
            'GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles TABHISTORY.SelectedTabChanged
        Try
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSalaryType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalaryType.Click
        Try
            Dim id As Integer = 0I
            Dim frm As New frmSalaryType
            ApplyStyleSheet(frm)
            If frm.ShowDialog() = Windows.Forms.DialogResult.Yes Then
                'id = Me.cmbType.SelectedIndex
                'Dim Str As String = "Select SalaryExpTypeId, SalaryExpType From SalaryExpenseType WHERE flgAdvance=1"
                'FillDropDown(Me.cmbType, Str)
                'Me.cmbType.SelectedIndex = id
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GridEX1_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs)
        Try

            If e.Column.Key = "Action" Then
                Me.grdSalaryDetail.GetRow.Delete()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click

    End Sub

    Private Sub EmployeeBarCToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EmployeeBarCToolStripMenuItem.Click
        Try
            GetCrystalReportRights()
            Dim frm As New frmEmployeeCardViewer
            frm.ReportName = "rptEmployeeCard.rdlc"
            frm._Dt = GetEmpData()
            frm.Show()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function GetEmpData() As DataTable
        Try

            Dim strSQL As String = String.Empty
            ShowHeaderCompany()
            strSQL = "SP_EmployeeInformation"
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()
            Dim dv As New DataView
            dt.TableName = "dtEmployeeInformation"
            dv.Table = dt
            Dim strFilter As String = String.Empty
            If dt IsNot Nothing Then
                strFilter = " Employee_Code <> ''"
                strFilter += " AND Employee_Id=" & grdSaved.GetRow.Cells(enmEmployee.EmployeeID).Value & ""
            End If
            dv.RowFilter = strFilter.ToString
            Dim dtData As DataTable = CType(dv.ToTable, DataTable)
            dtData.AcceptChanges()
            Dim objCRFU As New CRUFLIDAutomation.FontEncoder 'Create Object 
            For Each r As DataRow In dtData.Rows  'Loop 
                r.BeginEdit()
                r("BarCode") = objCRFU.Code128("?" & r.Item("Employee_Code").ToString, 0)
                r("Company") = CompanyTitle
                r.EndEdit()
            Next
            dt.AcceptChanges()
            Return dtData 'dv.ToTable
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function getEmpData(ByVal EmpId As Integer) As DataTable
        Try
            Dim strSQL As String = String.Empty
            strSQL = "SP_Final_Clearance_Certificate " & EmpId & ""
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            If dt IsNot Nothing Then
                For Each r As DataRow In dt.Rows
                    If IO.File.Exists(r.Item("EmpPicture")) Then
                        LoadPicture(r, "EmpImage", r.Item("EmpPicture"))
                    End If
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub OutDoorDutySlipToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutDoorDutySlipToolStripMenuItem.Click
        Try
            GetCrystalReportRights()
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            ShowReport("rptOutdoorDuty", , "Nothing", "Nothing", , , , GetEmpData(Me.grdSaved.GetRow.Cells(enmEmployee.EmployeeID).Value))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub FinalClearanceLetterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FinalClearanceLetterToolStripMenuItem.Click
        Try
            GetCrystalReportRights()
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            ShowReport("rptFinal_Clearance_Certificate", , "Nothing", "Nothing", , , , GetEmpData(Me.grdSaved.GetRow.Cells(enmEmployee.EmployeeID).Value))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub RegisgnationLetterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RegisgnationLetterToolStripMenuItem.Click
        Try
            GetCrystalReportRights()
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            ShowReport("rptResignation", , "Nothing", "Nothing", , , , GetEmpData(Me.grdSaved.GetRow.Cells(enmEmployee.EmployeeID).Value))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub LeavingApplicationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LeavingApplicationToolStripMenuItem.Click
        Try
            GetCrystalReportRights()
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            ShowReport("rptLeaveApplicationForm", , "Nothing", "Nothing", , , , GetEmpData(Me.grdSaved.GetRow.Cells(enmEmployee.EmployeeID).Value))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ddlDept_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlDept.SelectedIndexChanged
        Try
            If Not Me.ddlDept.SelectedIndex = -1 Then
                FillCombo("division")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbDivision_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDivision.SelectedIndexChanged
        Try
            If Not Me.cmbDivision.SelectedIndex = -1 Then
                FillCombo("payrolldividion")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub FinalSettlementToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FinalSettlementToolStripMenuItem.Click
        Try
            GetCrystalReportRights()
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            Dim dt As New DataTable
            dt = GetDataTable("SP_RptEmployeeFinalSettlement " & Me.grdSaved.GetRow.Cells(enmEmployee.EmployeeID).Value & "")

            For Each r As DataRow In dt.Rows
                r.BeginEdit()
                LoadPicture(r, "EmpImg", r.Item("EmpPicture").ToString)
                r.EndEdit()
            Next
            ShowReport("rptEmpFinalSettlement", , , , , , , dt)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        Try
            '' Request No 872
            '' 18-11-2013 by Imran  '
            '' Card Header Print for Grays Textile mills
            GetCrystalReportRights()
            Dim frm As New frmEmployeeCardViewer
            frm.ReportName = "rptEmployeeCardFooter.rdlc" 'Set Report path
            frm._Dt = GetEmpData()
            frm.Show()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        Try
            '' Request No 872
            '' 18-11-2013 by Imran  '
            '' Card Header Print for Grays Textile mills
            GetCrystalReportRights()
            Dim frm As New frmEmployeeCardViewer
            frm.ReportName = "rptEmployeeCardHeader.rdlc" 'Set Report path
            frm._Dt = GetEmpData()
            frm.Show()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GridEX1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Nothing, Nothing)
        End If
        If e.KeyCode = Keys.Delete Then
            DeleteToolStripButton_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub Label23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub BtnPrint_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.ButtonClick

    End Sub

    Private Sub EmployeeInformationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EmployeeInformationToolStripMenuItem.Click
        Try
            GetCrystalReportRights()
            'Task No 2544 Call the function to show  employee information Report 
            ShowReport("rptEmployeeInfo", , , , False, , , ReportEmployeInformation)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Task No 2544 Add New Function to retrive data on employee information Report 
    Public Function ReportEmployeInformation() As DataTable
        Dim adp As New OleDbDataAdapter
        Dim dt As New DataTable
        Try
            Dim sql As String = "SELECT dbo.EmployeeDesignationDefTable.EmployeeDesignationName, dbo.tblDefDivision.Division_Name, dbo.EmployeeDeptDefTable.EmployeeDeptName," _
                          & " dbo.ShiftGroupTable.ShiftGroupName, dbo.tblDefPayRollDivision.PayRollDivisionName, dbo.tblDefEmployee.Employee_ID, dbo.tblDefEmployee.Employee_Name, " _
                         & " dbo.tblDefEmployee.AlternateEmpNo, dbo.tblDefEmployee.AttendanceDate, dbo.tblDefEmployee.Employee_Code, dbo.tblDefEmployee.Father_Name, " _
         & " dbo.tblDefEmployee.NIC, dbo.tblDefEmployee.NTN, dbo.tblDefEmployee.Martial_Status, dbo.tblDefEmployee.Religion, dbo.tblDefEmployee.Phone, " _
         & " dbo.tblDefEmployee.Mobile, dbo.tblDefEmployee.Email, dbo.tblDefEmployee.Joining_Date, dbo.tblDefEmployee.Salary, dbo.tblDefEmployee.Active, " _
          & " dbo.tblDefEmployee.Leaving_Date, dbo.tblDefEmployee.Comments, dbo.tblDefEmployee.EmployeeCommission, dbo.tblDefEmployee.SalePerson, " _
          & " dbo.tblDefEmployee.Reference, dbo.tblDefEmployee.PessiNo, dbo.tblDefEmployee.EobiNo, dbo.tblDefEmployee.Social_Security_No,  " _
          & "  dbo.tblDefEmployee.Insurance_No, dbo.tblDefEmployee.Emergency_No, dbo.tblDefEmployee.Passport_No, dbo.tblDefEmployee.BankAccount_No, " _
         & "   dbo.tblDefEmployee.NIC_Place, dbo.tblDefEmployee.Domicile, dbo.tblDefEmployee.Relation, dbo.tblDefEmployee.InReplacementNewCode, " _
          & "  dbo.tblDefEmployee.Previous_Code, dbo.tblDefEmployee.Last_Update, dbo.tblDefEmployee.JobType, dbo.tblDefEmployee.Family_Code, " _
          & "  dbo.tblDefEmployee.ID_Remark, dbo.tblDefEmployee.Qualification, dbo.tblDefEmployee.Blood_Group, dbo.tblDefEmployee.[Language], dbo.tblDefEmployee.Gender, " _
          & "  dbo.tblDefEmployee.DOB, dbo.tblListCity.CityName, dbo.tblDefEmployee.Address, dbo.tblDefEmployee.EmpPicture,Convert(Image,'') as Image_Stream, dbo.ShiftGroupTable.ShiftGroupId, " _
          & "  dbo.tblDefEmployee.City_ID, dbo.tblDefDivision.Division_Id, dbo.tblDefPayRollDivision.PayRollDivision_Id, dbo.tblDefEmployee.Dept_ID, " _
                       & "dbo.tblDefEmployee.Desig_ID, CONVERT(Varchar(3000), '') AS BarCode, CONVERT(Varchar, '') AS Company " _
               & "  FROM dbo.tblDefEmployee LEFT OUTER JOIN" _
                       & "   dbo.EmployeeDesignationDefTable ON dbo.tblDefEmployee.Desig_ID = dbo.EmployeeDesignationDefTable.EmployeeDesignationId LEFT OUTER JOIN " _
                       & "   dbo.tblDefPayRollDivision ON dbo.tblDefEmployee.PayRoll_Division = dbo.tblDefPayRollDivision.PayRollDivision_Id LEFT OUTER JOIN " _
                       & "   dbo.tblDefDivision ON dbo.tblDefEmployee.Dept_Division = dbo.tblDefDivision.Division_Id LEFT OUTER JOIN " _
                       & "   dbo.EmployeeDeptDefTable ON dbo.tblDefEmployee.Dept_ID = dbo.EmployeeDeptDefTable.EmployeeDeptId LEFT OUTER JOIN " _
                        & "  dbo.ShiftGroupTable ON dbo.tblDefEmployee.ShiftGroupId = dbo.ShiftGroupTable.ShiftGroupId LEFT OUTER JOIN " _
                       & "   dbo.tblListCity ON dbo.tblDefEmployee.City_ID = dbo.tblListCity.CityId  where dbo.tblDefEmployee.Employee_ID= " & grdSaved.CurrentRow.Cells("Employee_ID").Value.ToString


            dt = GetDataTable(sql)

            For Each r As DataRow In dt.Rows   'Task No 2544 Add New Chdck To Validate The Load Picture Function 
                If r.Item("EmpPicture").ToString <> "" AndAlso IO.File.Exists(r.Item("EmpPicture").ToString) Then

                    r.BeginEdit()
                    LoadPicture(r, "Image_Stream", r.Item("EmpPicture").ToString)
                    r.EndEdit()
                End If
            Next
            Return dt
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function

    Public Function GetCOACode(ByVal MainAcId As Integer, Optional ByVal trans As OleDb.OleDbTransaction = Nothing) As String
        Try

            Dim strSQL As String = String.Empty
            Dim strSerialAccountCode As String = String.Empty
            strSQL = "Select sub_sub_Code From tblCOAMainSubSub WHERE Main_sub_sub_Id=" & MainAcId & ""
            Dim dt As New DataTable
            dt = GetDataTable(strSQL, trans)
            dt.AcceptChanges()
            Dim strMainAcCode As String = String.Empty
            If dt.Rows.Count > 0 Then
                strMainAcCode = dt.Rows(0).Item(0).ToString
            End If
            If strMainAcCode.Length <= 0 Then Return "" : Exit Function
            strSQL = "Select IsNull(Max(right(detail_code,5)),0)+1 as Serial From tblCOAMainSubSubDetail WHERE LEFT(detail_code," & strMainAcCode.Length & ")='" & strMainAcCode & "' AND main_sub_sub_Id=" & MainAcId & ""

            Dim dtSerial As New DataTable
            dtSerial = GetDataTable(strSQL, trans)
            dtSerial.AcceptChanges()
            If dtSerial.Rows.Count > 0 Then
                strSerialAccountCode = strMainAcCode & "-" & CStr(Microsoft.VisualBasic.Right("00000" & CStr(dtSerial.Rows(0).Item(0).ToString), 5))
            End If

            Return strSerialAccountCode

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    'Altered By Ali Ansari Against Task# 20150505
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Employee"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbState_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbState.SelectedIndexChanged
        Try
            FillDropDown(Me.ddlCity, "Select * from tblListCity where STateId=" & Me.cmbState.SelectedValue)
            'Task#20150511 Ali Ansari Fill Combos of Region, Zone and Belt
            FillDropDown(Me.cmbRegion, "Select * from tblListregion where stateId=" & Me.cmbState.SelectedValue)
            'Task#20150511 Ali Ansari Fill Combos of Region, Zone and Belt

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbRegion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbRegion.SelectedIndexChanged
        Try
            'Task#20150511 Ali Ansari Fill Combos of Region, Zone and Belt
            FillDropDown(Me.cmbZone, "Select * from tblListzone where regionId=" & Me.cmbRegion.SelectedValue)
            'Task#20150511 Ali Ansari Fill Combos of Region, Zone and Belt
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbZone_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbZone.SelectedIndexChanged
        Try
            'Task#20150511 Ali Ansari Fill Combos of Region, Zone and Belt
            FillDropDown(Me.cmbBelt, "Select * from tblListbelt where ZoneId=" & Me.cmbZone.SelectedValue)
            'Task#20150511 Ali Ansari Fill Combos of Region, Zone and Belt
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Task# G08062015 Ahmad Sharif: Add Radio btn checked event for Active checkbo
    Private Sub chkActive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkActive.CheckedChanged

        Try
            If chkActive.Checked = True Then
                Me.dtpLeaving.Checked = False
            Else
                Me.dtpLeaving.Checked = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)            ''Task# A1-09-06-2015 add exception message
        End Try
    End Sub
    'End Task# G08062015
    'Task# G08062015 Ahmad Sharif: Add Radio btn checked event for dtpLeaving checkbox
    Private Sub dtpLeaving_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpLeaving.ValueChanged
        Try
            If dtpLeaving.Checked = True Then
                chkActive.Checked = False
            Else
                chkActive.Checked = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)  ''Task# A1-09-06-2015 add exception message
        End Try
    End Sub
    'Altered Against Task# 2015060014 Appointment Letter Report Calling Ali Ansari
    Private Sub AppointmentLetterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AppointmentLetterToolStripMenuItem.Click
        Try
            GetCrystalReportRights()
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            ShowReport("rptJoingLetter", , , , , , , GetEmpData(Me.grdSaved.GetRow.Cells(enmEmployee.EmployeeID).Value))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Altered Against Task# 2015060014 Appointment Letter Report Calling Ali Ansari
    'Altered Against Task# 2015060014 Get Employee Information Ali Ansari
    Public Function getEmployee(ByVal EmpId As Integer) As DataTable
        Try
            Dim strSQL As String = String.Empty
            'strSQL = "SP_Final_Clearance_Certificate " & EmpId & ""
            strSQL = "select a.employee_id as EmpID,a.Employee_name as EmpName,a.Employee_Code as EmpCode," _
                    & " a.Father_Name as FatName,a.NIC,a.Gender,a.Martial_Status,a.Religion,a.DOB, " _
                    & " a.City_ID as CityID,b.cityName,a.Address,a.Phone,a.Mobile,a.Email,a.Joining_Date, " _
                    & " a.Dept_id as DepId,c.EmployeeDeptName as DepName,a.Desig_Id as DesId,d.employeedesignationname as DesName,a.Salary,a.Active,a.Leaving_Date,emppicture, a.ConfirmationDate  " _
                     & " from tblDefEmployee a " _
                    & " left join tblListCity b on a.City_ID = b.CityId " _
                    & " left join EmployeeDeptDefTable c on a.dept_id = c.EmployeeDeptId " _
                    & " left join EmployeeDesignationDefTable d on a.desig_id = d.EmployeeDesignationId " _
                    & " where a.employee_id = " & Me.grdSaved.GetRow.Cells(enmEmployee.EmployeeID).Value & ""

            Dim DTEmpExperience As New DataTable
            DTEmpExperience = GetDataTable(strSQL)
            DTEmpExperience.AcceptChanges()
            If DTEmpExperience IsNot Nothing Then
                For Each r As DataRow In DTEmpExperience.Rows
                    If IO.File.Exists(r.Item("EmpPicture")) Then
                        LoadPicture(r, "EmpImage", r.Item("EmpPicture"))
                    End If
                Next
            End If
            Return DTEmpExperience
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Altered Against Task# 2015060014 Get Employee Information Ali Ansari
    'Altered Against Task# 2015060014 Experience Letter Report Calling Ali Ansari
    Private Sub ExperienceCertificateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExperienceCertificateToolStripMenuItem.Click
        Try
            GetCrystalReportRights()
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            If Me.grdSaved.GetRow.Cells("Active").Value = True Then
                ShowErrorMessage("Employee selected is active, report is available for in-active employee")

                Exit Sub
            End If
            ShowReport("rptEmployeeExperienceCertificate", , "Nothing", "Nothing", , , , getEmployee(Me.grdSaved.GetRow.Cells(enmEmployee.EmployeeID).Value))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try


    End Sub
    'Altered Against Task# 2015060014 Experience Letter Report Calling Ali Ansari
    'Altered Against Task# 2015060014 Bank Account Letter Report Calling Ali Ansari
    Private Sub BankAccountOpeningToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BankAccountOpeningToolStripMenuItem.Click
        Try
            GetCrystalReportRights()
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            ShowReport("rptEmployeeBankAccountCertificate", , "Nothing", "Nothing", , , , getEmployee(Me.grdSaved.GetRow.Cells(enmEmployee.EmployeeID).Value))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Altered Against Task# 2015060014 Bank Account Letter Report Calling Ali Ansari
    Private Sub BtnAddEmployee_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAddEmployee.Click
        Try
            Call ApplyStyleSheet(frmDefEmployeeType)
            frmDefEmployeeType.ShowDialog()
            Call FillCombo("EmployeeType")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Altered Against Task# 2015060014 Confirmation Report Calling Ali Ansari
    Private Sub EmployeeConfirmationLetterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            ShowReport("rptConfirmation", , "Nothing", "Nothing", , , , getEmployee(Me.grdSaved.GetRow.Cells(enmEmployee.EmployeeID).Value))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Altered Against Task# 2015060014 Confirmation Report Calling Ali Ansari

    Private Sub ConfirmationLetterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConfirmationLetterToolStripMenuItem.Click
        Try
            GetCrystalReportRights()
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            ShowReport("rptConfirmationLetter", , "Nothing", "Nothing", , , , getEmployee(Me.grdSaved.GetRow.Cells(enmEmployee.EmployeeID).Value))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdSalaryDetail_ColumnButtonClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSalaryDetail.ColumnButtonClick
        Try
            Me.grdSalaryDetail.GetRow.Delete()
            Me.grdSalaryDetail.UpdateData()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSalaryDetail_EditingCell(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.EditingCellEventArgs) Handles grdSalaryDetail.EditingCell
        Try
            If e.Column.DataMember <> "Amount" Then
                If Me.grdSalaryDetail.GetRow.Cells("flgAdvance").Value.ToString = "True" Then
                    e.Cancel = False
                Else
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Task#18082015 Factor Rate textbox just accept numeric and dot value (Ahmad Sharif)
    Private Sub txtFactorRate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFactorRate.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task#18082015

    Private Sub txtSalary_Leave(sender As Object, e As EventArgs) Handles txtSalary.Leave
        Try
            If Not grdSalaryDetail.DataSource Is Nothing Then

                CType(Me.grdSalaryDetail.DataSource, DataTable).Columns("NetAmount").Expression = "iif([Amount Type]='Fixed',[Amount], (iif([Amount Type]='Percentage', (Amount/100) * " & Val(Me.txtSalary.Text) & ", 0)))"
                Me.grdSalaryDetail.UpdateData()
            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub grdSaved_LinkClicked(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSaved.LinkClicked
        Try
            If e.Column.Key = "No_of_Attachment" Then
                Dim frm As New frmAttachmentView
                frm._Source = Me.Name
                frm._VoucherId = Val(Me.grdSaved.GetRow.Cells(enmEmployee.EmployeeID).Value.ToString)
                frm.ShowDialog()
                DisplayRecord()
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ToolStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked

    End Sub
End Class