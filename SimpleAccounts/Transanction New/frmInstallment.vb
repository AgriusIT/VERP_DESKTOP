''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
''22-Feb-2014  TASK:M20 Imran Ali Load Sales Order On Customer Planing
''03-Mar-2014  Task:2452    Imran Ali  4-ALPHABETIC order of items in sale and purchase window
''06-Sep-2014 Task:2832 Imran Ali Production Plan Status (Converters)
Imports System.Data.OleDb
Imports SBModel

Public Class frmInstallment
    Implements IGeneral

    Dim dt As DataTable
    Dim Mode As String = "Normal"
    Dim IsOpenForm As Boolean = False
    Dim IsEditMode As Boolean = False
    Dim intInstallmentId As Integer = 0I
    Enum Customer
        Id
        Name
        Code
        State
        City
        Territory
        ExpiryDate
        Discount
        Other_Exp
        Fuel
        CNG
        TransitInsurance
        Credit_Limit
        Type
        Email
        PhoneNo
        Mobile
        SubSubTitle
        SaleMan
    End Enum
    Enum grdDetail
        InstallmentId
        Month_Start_Date
        Month_End_Date
        Due_Date
        Installments
        DeleteButton
    End Enum
    Private Sub txtMonthlyInstallmentPerMonth_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMonthlyNoOfInstallment.KeyPress, txtMonthlyInstallmentPerMonth.KeyPress, txtRemainingAmount.KeyPress, txtInvoiceAmount.KeyPress, txtAdvance.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.BtnSave.Enabled = False
                    Me.BtnDelete.Enabled = False
                    Me.BtnPrint.Enabled = False
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmSalesReturn)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If


            Else
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False

                For Each Rightsdt As GroupRights In Rights
                    If Rightsdt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rightsdt.FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Print" Then
                        Me.BtnPrint.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Dim objCon As New OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()

        Me.txtPONo.Text = GetDocumentNo()

        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Dim cmd As New OleDbCommand


        Try


            cmd.CommandType = CommandType.Text
            cmd.Connection = objCon
            cmd.Transaction = trans
            cmd.CommandTimeout = 300


            If Me.grdSaved.RowCount = 0 Then Return False
            intInstallmentId = Val(Me.grdSaved.GetRow.Cells("InstallmentId").Value.ToString)

            cmd.CommandText = ""
            cmd.CommandText = "Delete From InstallmentDetailTable WHERE InstallmentId=" & intInstallmentId & ""
            cmd.ExecuteNonQuery()

            cmd.CommandText = ""
            cmd.CommandText = "Delete From InstallmentMasterTable WHERE InstallmentId=" & intInstallmentId & ""
            cmd.ExecuteNonQuery()

            cmd.CommandText = ""
            cmd.CommandText = "Delete From InstallmentWitnessOne WHERE InstallmentID=" & intInstallmentId & ""
            cmd.ExecuteNonQuery()

            cmd.CommandText = ""
            cmd.CommandText = "Delete From InstallmentWitnessTwo WHERE InstallmentID=" & intInstallmentId & ""
            cmd.ExecuteNonQuery()

            trans.Commit()
            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String = String.Empty
            If Condition = String.Empty Then
                str = "SELECT     vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name, vwCOADetail.detail_code as [Code],tblListState.StateName as State, tblListCity.CityName as City,  " & _
                                   "tblListTerritory.TerritoryName as Territory , tblCustomer.ExpiryDate, tblCustomer.Discper as [Discount] ,tblCustomer.otherexpanses as [Other Expense], tblCustomer.Fuel as Fuel, tblCustomer.CNG as CNG , tblCustomer.Cridtlimt as Limit, dbo.vwCOADetail.account_type as Type, isnull(customertypes,0) as typeid, vwCOADetail.Contact_Email as Email, vwCOADetail.Contact_Phone as Phone, vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title, IsNull(vwCOADetail.SaleMan,0) as SaleManId " & _
                                   "FROM  tblCustomer LEFT OUTER JOIN " & _
                                   "tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                                   "tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                                   "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                                   "vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " & _
                                   "WHERE  vwCOADetail.coa_detail_id is not  null "

                If getConfigValueByType("Show Vendor On Sales") = "True" Then
                    str += " AND (vwCOADetail.account_type in( 'Customer','Vendor' )) "
                Else
                    str += " AND (vwCOADetail.account_type in( 'Customer')) "
                End If
                'If flgCompanyRights = True Then
                '    Str += " AND vwCOADetail.CompanyId=" & MyCompanyId
                'End If
                'If IsEditMode = False Then
                '    Str += " AND vwCOADetail.Active=1"
                'Else
                '    Str += " AND vwCOADetail.Active in(0,1,NULL)"
                'End If
                str += " order by tblCustomer.Sortorder, vwCOADetail.detail_title "
                FillUltraDropDown(cmbVendor, str)
                Me.cmbVendor.Rows(0).Activate()
                If cmbVendor.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Id).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Territory).Hidden = False
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.State).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.ExpiryDate).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Fuel).Hidden = True
                    ' CNG Changes
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.CNG).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Other_Exp).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("typeid").Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Discount).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Credit_Limit).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Type).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Name).Width = 300
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Credit_Limit).Width = 80
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Discount).Width = 80
                    'Task:2373 Column Formating
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.SubSubTitle).Header.Caption = "Ac Head"
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.SubSubTitle).Width = 120
                    'End Task:2373
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.SaleMan).Hidden = True
                End If

            ElseIf Condition = "Invoice" Then
                FillUltraDropDown(Me.cmbInvoice, "Select SaleDetailID, SalesNo as [Invoice No], SalesDate as [Invoice Date], ArticleDefView.ArticleDescription as [Item], SalesDetailTable.Chassis_No as [Chassis No], SalesDetailTable.Engine_No as [Engine No], ArticleDefView.ArticleColorName as [Color], (IsNull(SalesDetailTable.Qty,0)*IsNull(SalesDetailTable.Price,0))+((IsNull(TaxPercent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))+((IsNull(SEDPercent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0)))) as [Invoice Amount], IsNull(CashPaid,0) as [Receipt], SalesDetailTable.SalesID, SalesDetailTable.ArticleDefID From SalesDetailTable INNER JOIN SalesMasterTable On SalesMasterTable.SalesID = SalesDetailTable.SalesID INNER JOIN ArticleDefView on ArticleDefView.ArticleID = SalesDetailTable.ArticleDefID WHERE SalesDetailTable.SaleDetailId not in(Select DISTINCT IsNull(SalesDetailID,0) as SalesetailID From InstallmentMasterTable) AND SalesMasterTable.CustomerCode=" & Me.cmbVendor.Value & " Order By SalesNo DESC")
                Me.cmbInvoice.Rows(0).Activate()
                Me.cmbInvoice.DisplayLayout.Bands(0).Columns("SaleDetailID").Hidden = True
                Me.cmbInvoice.DisplayLayout.Bands(0).Columns("SalesID").Hidden = True
                Me.cmbInvoice.DisplayLayout.Bands(0).Columns("ArticleDefID").Hidden = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try


            Dim strSQL As String = String.Empty
            If Condition = "Master" Then

                strSQL = String.Empty
                strSQL = "SELECT Recv.InstallmentId, Recv.Doc_No, Recv.Doc_Date, Recv.CustomerCode, COA.detail_code as [Customer Ac Code], COA.detail_title as [Customer], Recv.Remarks,Recv.Product_Name, Recv.ChassisNo, Recv.Model as [Engine No],Recv.Color, Recv.InvoiceId, " _
               & " Recv.Period_Start_Date, Recv.Period_End_Date, Recv.InvoiceAmount, Recv.AdvanceAmount, Recv.RemainigAmount, Recv.InstallmentPerMonth, Recv.DueDays, Recv.UserName, " _
               & " Recv.EntryDate, IsNull(Recv.SalesID,0) as SalesID,IsNull(Recv.SalesDetailID,0) as SalesDetailID,IsNull(Recv.ArticleDefID,0) as ArticleDefID,  Recv.FatherName, Recv.Cast, Recv.CNIC, Recv.EngineNo, Recv.RegistrationNo, Recv.AC, SalesMasterTable.SalesNo as [Invoice No], SalesMasterTable.SalesDate as [Invoice Date], ArticleDefView.ArticleDescription as [Item] FROM dbo.InstallmentMasterTable AS Recv INNER JOIN dbo.vwCOADetail AS COA ON Recv.CustomerCode = COA.coa_detail_id LEFT OUTER JOIN SalesMasterTable on SalesMasterTable.SalesID = Recv.SalesID LEFT OUTER JOIN SalesDetailTable on SalesDetailTable.SalesID = SalesMasterTable.SalesID And Recv.SalesDetailID= SalesDetailTable.SaleDetailID AND SalesMasterTable.SalesID = SalesDetailTable.SalesID LEFT OUTER JOIN ArticleDefView on ArticleDefView.ArticleID =  Recv.ArticleDefID ORDER BY Recv.InstallmentId DESC "

                Dim dtMaster As New DataTable
                dtMaster = GetDataTable(strSQL)
                dtMaster.AcceptChanges()
                Me.grdSaved.DataSource = dtMaster
                Me.grdSaved.RetrieveStructure()

                Me.grdSaved.RootTable.Columns("InstallmentId").Visible = False
                Me.grdSaved.RootTable.Columns("CustomerCode").Visible = False
                Me.grdSaved.RootTable.Columns("InvoiceId").Visible = False
                Me.grdSaved.RootTable.Columns("SalesID").Visible = False
                Me.grdSaved.RootTable.Columns("SalesDetailID").Visible = False
                Me.grdSaved.RootTable.Columns("ArticleDefID").Visible = False

                Me.grdSaved.RootTable.Columns("Doc_Date").FormatString = "dd/MMM/yyyy"
                Me.grdSaved.RootTable.Columns("InvoiceAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grdSaved.RootTable.Columns("AdvanceAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grdSaved.RootTable.Columns("RemainigAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

                Me.grdSaved.RootTable.Columns("InvoiceAmount").FormatString = "N" & DecimalPointInValue
                Me.grdSaved.RootTable.Columns("AdvanceAmount").FormatString = "N" & DecimalPointInValue
                Me.grdSaved.RootTable.Columns("RemainigAmount").FormatString = "N" & DecimalPointInValue
                Me.grdSaved.RootTable.Columns("InstallmentPerMonth").FormatString = "N" & DecimalPointInValue

                Me.grdSaved.RootTable.Columns("InvoiceAmount").TotalFormatString = "N" & DecimalPointInValue
                Me.grdSaved.RootTable.Columns("AdvanceAmount").TotalFormatString = "N" & DecimalPointInValue
                Me.grdSaved.RootTable.Columns("RemainigAmount").TotalFormatString = "N" & DecimalPointInValue
                Me.grdSaved.RootTable.Columns("InstallmentPerMonth").TotalFormatString = "N" & DecimalPointInValue

                Me.grdSaved.RootTable.Columns("InvoiceAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns("AdvanceAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns("RemainigAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns("InstallmentPerMonth").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

                Me.grdSaved.RootTable.Columns("InvoiceAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns("AdvanceAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns("RemainigAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns("InstallmentPerMonth").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

                Me.grdSaved.AutoSizeColumns()


            ElseIf Condition = "Detail" Then
                strSQL = "Select InstallmentDetailTable.InstallmentId, Convert(DateTime,Month_Start_Date,102) as [Month_Date],  Convert(DateTime,Month_End_Date,102) as [End_Date], Convert(DateTime,Due_Date,102) as Due_Date, IsNull(Installments,0) as Installments From InstallmentDetailTable INNER JOIN InstallmentMasterTable on InstallmentMasterTable.InstallmentId = InstallmentDetailTable.InstallmentId WHERE InstallmentDetailTable.InstallmentId =" & intInstallmentId & "  ORDER BY InstallmentDetailID ASC "
                Dim dtdetail As New DataTable
                dtdetail = GetDataTable(strSQL)
                dtdetail.AcceptChanges()



                Me.grd.DataSource = dtdetail
            ElseIf Condition = "WitnessOne" Then
                strSQL = "Select WitnessID, Name, FatherName, Cast, CNIC, Address, Phone From InstallmentWitnessOne Where InstallmentID =" & intInstallmentId & " "
                Dim dtWitnessOne As New DataTable
                dtWitnessOne = GetDataTable(strSQL)
                dtWitnessOne.AcceptChanges()
                ''Populate witness one fields
                Me.txtWitnessOneName.Text = dtWitnessOne.Rows.Item(0).Item("Name").ToString
                Me.txtWitnessOneFather.Text = dtWitnessOne.Rows.Item(0).Item("FatherName").ToString
                Me.txtWitnessOneCNIC.Text = dtWitnessOne.Rows.Item(0).Item("CNIC").ToString
                Me.txtWitnessOneCast.Text = dtWitnessOne.Rows.Item(0).Item("Cast").ToString
                Me.txtWitnessOneAddress.Text = dtWitnessOne.Rows.Item(0).Item("Address").ToString
                Me.txtWitnessOnePhone.Text = dtWitnessOne.Rows.Item(0).Item("Phone").ToString


            ElseIf Condition = "WitnessTwo" Then
                strSQL = "Select WitnessID, Name, FatherName, Cast, CNIC, Address, Phone From InstallmentWitnessTwo Where InstallmentID =" & intInstallmentId & " "
                Dim dtWitnessTwo As New DataTable
                dtWitnessTwo = GetDataTable(strSQL)
                dtWitnessTwo.AcceptChanges()
                ''Populate witness one fields
                Me.txtWitnessTwoName.Text = dtWitnessTwo.Rows.Item(0).Item("Name").ToString
                Me.txtWitnessTwoFather.Text = dtWitnessTwo.Rows.Item(0).Item("FatherName").ToString
                Me.txtWitnessTwoCNIC.Text = dtWitnessTwo.Rows.Item(0).Item("CNIC").ToString
                Me.txtWitnessTwoCast.Text = dtWitnessTwo.Rows.Item(0).Item("Cast").ToString
                Me.txtWitnessTwoAddress.Text = dtWitnessTwo.Rows.Item(0).Item("Address").ToString
                Me.txtWitnessTwoPhone.Text = dtWitnessTwo.Rows.Item(0).Item("Phone").ToString

            End If
            '           [WitnessID] [int] IDENTITY(1,1) NOT NULL,
            '[Name] [nvarchar](50) NOT NULL,
            '[FatherName] [nvarchar](50) NULL,
            '[Cast] [nvarchar](50) NULL,
            '[CNIC] [nvarchar](50) NULL,
            '[Address] [nvarchar](100) NULL,
            '[InstallmentID] [int] N

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try


            If Val(Me.txtAdvance.Text) > Val(Me.txtInvoiceAmount.Text) Then
                ShowErrorMessage("Your Advance Amount Exceeded From Invoice Amount.")
                Me.txtAdvance.Focus()
                Return False
            End If

            If Val(Me.txtInvoiceAmount.Text) < Val(Me.txtAdvance.Text) Then
                ShowErrorMessage("Your Advance Amount Exceeded From Invoice Amount.")
                Me.txtAdvance.Focus()
                Return False
            End If

            If Me.cmbVendor.ActiveRow Is Nothing Or Me.cmbVendor.Value = 0 Then
                ShowErrorMessage("Please select customer.")
                Me.cmbVendor.Focus()
                Return False
            End If


            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            intInstallmentId = 0I
            Me.BtnSave.Text = "&Save"
            Me.txtPONo.Text = GetDocumentNo()
            Me.dtpPODate.Value = Date.Now
            Me.dtpPeriodStartDate.Value = Date.Now
            Me.dtpPeriodEndDate.Value = Date.Now
            Me.cmbVendor.Rows(0).Activate()
            Me.txtInvoiceAmount.Text = String.Empty
            Me.txtAdvance.Text = String.Empty
            Me.txtRemainingAmount.Text = String.Empty
            Me.txtRemarks.Text = String.Empty
            Me.txtMonthlyInstallmentPerMonth.Text = String.Empty
            Me.txtMonthlyNoOfInstallment.Text = String.Empty
            Me.txtProductName.Text = String.Empty
            Me.txtEngineNo.Text = String.Empty
            Me.txtColor.Text = String.Empty
            Me.txtRegistrationNo.Text = String.Empty
            Me.txtFather.Text = String.Empty
            Me.txtCast.Text = String.Empty
            Me.txtCNIC.Text = String.Empty
            Me.txtAC.Text = String.Empty

            ''Get Witnesses fields empty
            'Me.gbWitnessOne.Visible = False
            Me.txtWitnessOneFather.Text = String.Empty
            Me.txtWitnessOneAddress.Text = String.Empty
            Me.txtWitnessOneCast.Text = String.Empty
            Me.txtWitnessOneCNIC.Text = String.Empty
            Me.txtWitnessOneName.Text = String.Empty
            Me.txtWitnessOnePhone.Text = String.Empty

            Me.gbWitnessTwo.Visible = False
            Me.txtWitnessTwoFather.Text = String.Empty
            Me.txtWitnessTwoAddress.Text = String.Empty
            Me.txtWitnessTwoCast.Text = String.Empty
            Me.txtWitnessTwoCNIC.Text = String.Empty
            Me.txtWitnessTwoName.Text = String.Empty
            Me.txtWitnessTwoPhone.Text = String.Empty


            FillCombos("Invoice")
            Me.cmbInvoice.Enabled = True
            GetAllRecords("Detail")
            GetAllRecords("Master")
            ApplySecurity(SBUtility.Utility.EnumDataMode.New)
            Me.cmbVendor.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

        Me.txtPONo.Text = GetDocumentNo()
        Dim objCon As New OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim trans As OleDbTransaction = objCon.BeginTransaction()
        Dim cmd As New OleDbCommand


        Try


            cmd.CommandType = CommandType.Text
            cmd.Connection = objCon
            cmd.Transaction = trans
            cmd.CommandTimeout = 300
            '            FatherName	nvarchar(50)	Checked
            'Cast	nvarchar(50)	Checked
            'CNIC	nvarchar(50)	Checked
            'EngineNo	nvarchar(100)	Checked
            'RegistrationNo	nvarchar(100)	Checked
            'AC	nvarchar(100)	Checked

            cmd.CommandText = ""
            cmd.CommandText = "INSERT INTO InstallmentMasterTable(Doc_No,Doc_Date,CustomerCode,Remarks,InvoiceId,Period_Start_Date,Period_End_Date,Product_Name, ChassisNo,Model,Color,InvoiceAmount,DueDays,AdvanceAmount,RemainigAmount,InstallmentPerMonth,UserName,EntryDate,SalesId, SalesDetailID, ArticleDefID, FatherName, Cast, CNIC, EngineNo, RegistrationNo, AC) " _
                & " VALUES('" & Me.txtPONo.Text.Replace("'", "''") & "',Convert(DateTime,'" & dtpPODate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102)," & Me.cmbVendor.Value & ",N'" & Me.txtRemarks.Text.Replace("'", "''") & "',0, " _
                & " Convert(DateTime,'" & dtpPeriodStartDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102),Convert(DateTime,'" & dtpPeriodEndDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102),N'" & Me.txtProductName.Text.Replace("'", "''") & "', N'" & Me.txtChassisNo.Text.Replace("'", "''") & "', N'" & Me.txtEngineNo.Text.Replace("'", "''") & "',N'" & Me.txtColor.Text.Replace("'", "''") & "'," & Val(Me.txtInvoiceAmount.Text) & "," & Val(Me.txtInstallmentDueDay.Text) & ", " _
                & " " & Val(Me.txtAdvance.Text) & "," & Val(Me.txtRemainingAmount.Text) & "," & Val(Me.txtMonthlyInstallmentPerMonth.Text) & ", N'" & LoginUserName & "',Convert(DateTime,GetDate(),102)," & Val(Me.cmbInvoice.ActiveRow.Cells("SalesID").Value.ToString) & "," & Val(Me.cmbInvoice.ActiveRow.Cells("SaleDetailID").Value.ToString) & "," & Val(Me.cmbInvoice.ActiveRow.Cells("ArticleDefID").Value.ToString) & ", N'" & Me.txtFather.Text.Replace("'", "''") & "', N'" & Me.txtCast.Text.Replace("'", "''") & "', " _
                & " N'" & Me.txtCNIC.Text.Replace("'", "''") & "', N'" & Me.txtEngineNo.Text.Replace("'", "''") & "', N'" & Me.txtRegistrationNo.Text.Replace("'", "''") & "', N'" & Me.txtAC.Text.Replace("'", "''") & "' ) Select @@Identity"

            Dim InstallmentID As Integer = cmd.ExecuteScalar

            cmd.CommandText = ""
            cmd.CommandText = "Delete From InstallmentScheduleSMSTable WHERE InstallmentID=" & InstallmentID & ""
            cmd.ExecuteNonQuery()

            AddWitnessOne(InstallmentID, trans)
            AddWitnessTwo(InstallmentID, trans)
            InstallmentDetail(InstallmentID, trans)

            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            objCon.Close()

        End Try
    End Function
    Public Sub InstallmentDetail(InstallmentId As Integer, trans As OleDbTransaction)
        Dim cmd As New OleDbCommand
        cmd.Connection = trans.Connection
        cmd.Transaction = trans
        cmd.CommandType = CommandType.Text
        cmd.CommandTimeout = 300
        Try
            Dim strSQL As String = String.Empty




            For i As Integer = 0 To Me.grd.RowCount - 1

                strSQL = ""
                strSQL = "INSERT INTO InstallmentDetailTable(InstallmentId,Month_Start_Date,Month_End_Date,Due_Date,Installments) Values(" & InstallmentId & ",Convert(DateTime, '" & CDate(Me.grd.GetRows(i).Cells(grdDetail.Month_Start_Date).Value).ToString("yyyy-M-d hh:mm:ss tt") & "',102),Convert(DateTime, '" & CDate(Me.grd.GetRows(i).Cells(grdDetail.Month_End_Date).Value).ToString("yyyy-M-d hh:mm:ss tt") & "',102),Convert(DateTime, '" & CDate(Me.grd.GetRows(i).Cells(grdDetail.Due_Date).Value).ToString("yyyy-M-d hh:mm:ss tt") & "',102)," & Val(grd.GetRows(i).Cells(grdDetail.Installments).Value.ToString) & ")"
                cmd.CommandText = ""
                cmd.CommandText = strSQL
                cmd.ExecuteNonQuery()

                SMSScheduleSave(InstallmentId, Me.grd.GetRows(i), trans)


            Next

        Catch ex As Exception

            Throw ex
        End Try
    End Sub
    Public Sub AddWitnessOne(InstallmentID As Integer, trans As OleDbTransaction)
        Dim cmd As New OleDbCommand
        cmd.Connection = trans.Connection
        cmd.Transaction = trans
        cmd.CommandType = CommandType.Text
        cmd.CommandTimeout = 300
        Try
            Dim strSQL As String = String.Empty
            '            WitnessID	int	Unchecked
            'Name	nvarchar(50)	Unchecked
            'FatherName	nvarchar(50)	Checked
            'Cast	nvarchar(50)	Checked
            'CNIC	nvarchar(50)	Checked
            'Address	nvarchar(100)	Checked
            'CustomerCode	int	Checked
            '            Unchecked()




            strSQL = ""
            strSQL = "INSERT INTO InstallmentWitnessOne(Name, FatherName, Cast, CNIC, Address, Phone, InstallmentID ) Values(N'" & Me.txtWitnessOneName.Text.Replace("'", "''") & "', N'" & Me.txtWitnessOneFather.Text.Replace("'", "''") & "', N'" & Me.txtWitnessOneCast.Text.Replace("'", "''") & "', N'" & Me.txtWitnessOneCNIC.Text.Replace("'", "''") & "', N'" & Me.txtWitnessOneAddress.Text.Replace("'", "''") & "', N'" & Me.txtWitnessOnePhone.Text.Replace("'", "''") & "', " & InstallmentID & " )"
            cmd.CommandText = ""
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()


        Catch ex As Exception

            Throw ex
        End Try
    End Sub
    Public Sub AddWitnessTwo(InstallmentID As Integer, trans As OleDbTransaction)
        Dim cmd As New OleDbCommand
        cmd.Connection = trans.Connection
        cmd.Transaction = trans
        cmd.CommandType = CommandType.Text
        cmd.CommandTimeout = 300
        Try
            Dim strSQL As String = String.Empty
            '            WitnessID	int	Unchecked
            'Name	nvarchar(50)	Unchecked
            'FatherName	nvarchar(50)	Checked
            'Cast	nvarchar(50)	Checked
            'CNIC	nvarchar(50)	Checked
            'Address	nvarchar(100)	Checked
            'Phone     nvarchar (50) Checked
            'CustomerCode	int	Checked
            '            Unchecked()




            strSQL = ""
            strSQL = "INSERT INTO InstallmentWitnessTwo(Name, FatherName, Cast, CNIC, Address, Phone, InstallmentID) Values(N'" & Me.txtWitnessTwoName.Text.Replace("'", "''") & "', N'" & Me.txtWitnessTwoFather.Text.Replace("'", "''") & "', N'" & Me.txtWitnessTwoCast.Text.Replace("'", "''") & "', N'" & Me.txtWitnessTwoCNIC.Text.Replace("'", "''") & "', N'" & Me.txtWitnessTwoAddress.Text.Replace("'", "''") & "', N'" & Me.txtWitnessTwoPhone.Text.Replace("'", "''") & "', " & InstallmentID & " )"
            cmd.CommandText = ""
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()


        Catch ex As Exception

            Throw ex
        End Try
    End Sub
    Public Sub UpdateWitnessOne(InstallmentID As Integer, trans As OleDbTransaction)
        Dim cmd As New OleDbCommand
        cmd.Connection = trans.Connection
        cmd.Transaction = trans
        cmd.CommandType = CommandType.Text
        cmd.CommandTimeout = 300
        Try
            Dim strSQL As String = String.Empty
            '            WitnessID	int	Unchecked
            'Name	nvarchar(50)	Unchecked
            'FatherName	nvarchar(50)	Checked
            'Cast	nvarchar(50)	Checked
            'CNIC	nvarchar(50)	Checked
            'Address	nvarchar(100)	Checked
            'CustomerCode	int	Checked
            '            Unchecked()




            strSQL = ""
            strSQL = "Update InstallmentWitnessOne Set Name = N'" & Me.txtWitnessOneName.Text.Replace("'", "''") & "' , FatherName =  N'" & Me.txtWitnessOneFather.Text.Replace("'", "''") & "' , Cast = N'" & Me.txtWitnessOneCast.Text.Replace("'", "''") & "' , CNIC = N'" & Me.txtWitnessOneCNIC.Text.Replace("'", "''") & "' , Address = N'" & Me.txtWitnessOneAddress.Text.Replace("'", "''") & "' , Phone = N'" & Me.txtWitnessOnePhone.Text.Replace("'", "''") & "' Where InstallmentID = " & InstallmentID & " "
            cmd.CommandText = ""
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()


        Catch ex As Exception

            Throw ex
        End Try
    End Sub
    Public Sub UpdateWitnessTwo(InstallmentID As Integer, trans As OleDbTransaction)
        Dim cmd As New OleDbCommand
        cmd.Connection = trans.Connection
        cmd.Transaction = trans
        cmd.CommandType = CommandType.Text
        cmd.CommandTimeout = 300
        Try
            Dim strSQL As String = String.Empty
            '            WitnessID	int	Unchecked
            'Name	nvarchar(50)	Unchecked
            'FatherName	nvarchar(50)	Checked
            'Cast	nvarchar(50)	Checked
            'CNIC	nvarchar(50)	Checked
            'Address	nvarchar(100)	Checked
            'CustomerCode	int	Checked
            '            Unchecked()




            strSQL = ""
            strSQL = "Update InstallmentWitnessTwo Set Name = N'" & Me.txtWitnessTwoName.Text.Replace("'", "''") & "' , FatherName =  N'" & Me.txtWitnessTwoFather.Text.Replace("'", "''") & "' , Cast = N'" & Me.txtWitnessTwoCast.Text.Replace("'", "''") & "' , CNIC = N'" & Me.txtWitnessTwoCNIC.Text.Replace("'", "''") & "' , Address = N'" & Me.txtWitnessTwoAddress.Text.Replace("'", "''") & "' , Phone = N'" & Me.txtWitnessTwoPhone.Text.Replace("'", "''") & "' Where InstallmentID = " & InstallmentID & " "
            cmd.CommandText = ""
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()


        Catch ex As Exception

            Throw ex
        End Try
    End Sub


    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Dim objCon As New OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()

        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Dim cmd As New OleDbCommand

        Try


            cmd.CommandType = CommandType.Text
            cmd.Connection = objCon
            cmd.Transaction = trans
            cmd.CommandTimeout = 300


            cmd.CommandText = ""
            cmd.CommandText = "Update InstallmentMasterTable SET Doc_No='" & Me.txtPONo.Text.Replace("'", "''") & "',Doc_Date=Convert(DateTime,'" & dtpPODate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102),CustomerCode=" & Me.cmbVendor.Value & ",Remarks=N'" & Me.txtRemarks.Text.Replace("'", "''") & "',InvoiceId=0, " _
                & " Period_Start_Date=Convert(DateTime,'" & dtpPeriodStartDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102),Period_End_Date=Convert(DateTime,'" & dtpPeriodEndDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102),Product_Name=N'" & Me.txtProductName.Text.Replace("'", "''") & "',ChassisNo=N'" & Me.txtChassisNo.Text.Replace("'", "''") & "', Model=N'" & Me.txtEngineNo.Text.Replace("'", "''") & "',Color=N'" & Me.txtColor.Text.Replace("'", "''") & "',InvoiceAmount=" & Val(Me.txtInvoiceAmount.Text) & ",DueDays=" & Val(Me.txtInstallmentDueDay.Text) & ",AdvanceAmount= " & Val(Me.txtAdvance.Text) & ", " _
                & " RemainigAmount=" & Val(Me.txtRemainingAmount.Text) & ",InstallmentPerMonth=" & Val(Me.txtMonthlyInstallmentPerMonth.Text) & ",SalesId=" & Val(Me.cmbInvoice.ActiveRow.Cells("SalesID").Value.ToString) & ", SalesDetailID=" & Val(Me.cmbInvoice.ActiveRow.Cells("SaleDetailID").Value.ToString) & ", ArticleDefID=" & Val(Me.cmbInvoice.ActiveRow.Cells("ArticleDefID").Value.ToString) & " , FatherName= N'" & Me.txtFather.Text.Replace("'", "''") & "', " _
                & " Cast = N'" & Me.txtCast.Text.Replace("'", "''") & "', CNIC = N'" & Me.txtCNIC.Text.Replace("'", "''") & "', EngineNo = N'" & Me.txtEngineNo.Text.Replace("'", "''") & "', RegistrationNo = N'" & Me.txtRegistrationNo.Text.Replace("'", "''") & "', AC = N'" & Me.txtAC.Text.Replace("'", "''") & "' WHERE InstallmentID=" & intInstallmentId & ""

            cmd.ExecuteNonQuery()

            cmd.CommandText = ""
            cmd.CommandText = "Delete From InstallmentDetailTable WHERE InstallmentId=" & intInstallmentId & ""
            cmd.ExecuteNonQuery()

            cmd.CommandText = ""
            cmd.CommandText = "Delete From InstallmentScheduleSMSTable WHERE InstallmentID=" & intInstallmentId & ""
            cmd.ExecuteNonQuery()

            InstallmentDetail(intInstallmentId, trans)
            UpdateWitnessOne(intInstallmentId, trans)
            UpdateWitnessTwo(intInstallmentId, trans)

            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            objCon.Close()

        End Try
    End Function

    Private Sub frmInstallment_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            FillCombos()
            ReSetControls()
            IsOpenForm = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtMonthlyInstallmentPerMonth_LostFocus(sender As Object, e As EventArgs) Handles txtMonthlyInstallmentPerMonth.LostFocus
        Try
            Me.txtMonthlyNoOfInstallment.Text = Val(Me.txtRemainingAmount.Text) / Val(Me.txtMonthlyInstallmentPerMonth.Text)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtMonthlyNoOfInstallment_LostFocus(sender As Object, e As EventArgs) Handles txtMonthlyNoOfInstallment.LostFocus
        Try
            Me.txtMonthlyInstallmentPerMonth.Text = Val(Me.txtRemainingAmount.Text) / Val(Me.txtMonthlyNoOfInstallment.Text)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtAdvance_LostFocus(sender As Object, e As EventArgs) Handles txtAdvance.LostFocus
        Try

            If Val(Me.txtAdvance.Text) > Val(Me.txtInvoiceAmount.Text) Then
                ShowErrorMessage("Your Advance Amount Exceeded From Invoice Amount.")
                Me.txtAdvance.Focus()
                Exit Sub
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtInvoiceAmount_LostFocus(sender As Object, e As EventArgs) Handles txtInvoiceAmount.LostFocus
        Try
            If Val(Me.txtInvoiceAmount.Text) < Val(Me.txtAdvance.Text) Then
                ShowErrorMessage("Your Advance Amount Exceeded From Invoice Amount.")
                Me.txtAdvance.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtAmount_TextChanged(sender As Object, e As EventArgs) Handles txtInvoiceAmount.TextChanged, txtAdvance.TextChanged
        Try

            Me.txtRemainingAmount.Text = Val(Me.txtInvoiceAmount.Text) - Val(Me.txtAdvance.Text)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try

            SetMonthlyInstallments()


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub SetMonthlyInstallments()
        Try

            Dim dtData As DataTable = CType(Me.grd.DataSource, DataTable)
            dtData.AcceptChanges()
            dtData.Clear()
            dtData.AcceptChanges()

            Dim intFixMonth As Integer = 0I
            Dim intoneday As Integer = 0I
            Dim intTotalMonth As Integer = 0I
            Dim dblTotalInstallments As Double = 0D
            Dim dblInstallment As Double = 0D

            intFixMonth = Fix(Val(Me.txtMonthlyNoOfInstallment.Text))
            intoneday = (1 - (Val(Me.txtMonthlyNoOfInstallment.Text) - Fix(Val(Me.txtMonthlyNoOfInstallment.Text)))) + (Val(Me.txtMonthlyNoOfInstallment.Text) - Fix(Val(Me.txtMonthlyNoOfInstallment.Text)))
            intTotalMonth = intFixMonth + intoneday

            Dim monthStartDate As DateTime
            Dim monthEndDate As DateTime
            Dim installmentDueDate As DateTime


            For i As Integer = 0 To intTotalMonth - 1

                monthStartDate = Me.dtpPeriodStartDate.Value.AddMonths(i)
                monthEndDate = CDate(monthStartDate.Year & "-" & monthStartDate.Month & "-" & Date.DaysInMonth(monthStartDate.Year, monthStartDate.Month))
                installmentDueDate = monthStartDate.AddDays(Val(Me.txtInstallmentDueDay.Text))

                If i > 0 Then
                    monthStartDate = CDate(monthStartDate.Year & "-" & monthStartDate.Month & "-" & "1")
                    installmentDueDate = monthStartDate.AddDays(Val(Me.txtInstallmentDueDay.Text))
                End If
                dblInstallment = Val(Me.txtMonthlyInstallmentPerMonth.Text)
                If i = (intTotalMonth - 1) Then
                    dblInstallment = Val(Me.txtRemainingAmount.Text) - dblTotalInstallments

                    If Not dblInstallment > 1 Then
                        Exit For
                    End If

                End If

                Dim dr As DataRow = dtData.NewRow
                dr(grdDetail.InstallmentId) = intInstallmentId
                dr(grdDetail.Month_Start_Date) = monthStartDate
                dr(grdDetail.Month_End_Date) = monthEndDate
                dr(grdDetail.Due_Date) = installmentDueDate
                dr(grdDetail.Installments) = dblInstallment
                dtData.Rows.Add(dr)
                dtData.AcceptChanges()
                dblTotalInstallments += dblInstallment

            Next
            dblTotalInstallments = 0D
            dblInstallment = 0D

            Me.dtpPeriodEndDate.Value = monthEndDate

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Try
            If IsValidate() = False Then Exit Sub
            If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "Save" Then
                If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                ReSetControls()
            Else
                If msg_Confirm(str_ConfirmUpdate) = False Then Exit Sub
                If Update1() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles BtnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BtnEdit_Click(sender As Object, e As EventArgs) Handles BtnEdit.Click
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub EditRecords(Optional Condition As String = "")
        Try

            If Me.grdSaved.RowCount = 0 Then Exit Sub
            Me.BtnSave.Text = "&Update"
            intInstallmentId = Val(Me.grdSaved.GetRow.Cells("InstallmentId").Value.ToString)
            Me.txtPONo.Text = Me.grdSaved.GetRow.Cells("Doc_No").Value.ToString
            Me.dtpPODate.Value = Me.grdSaved.GetRow.Cells("Doc_Date").Value
            Me.dtpPeriodStartDate.Value = Me.grdSaved.GetRow.Cells("Period_Start_Date").Value
            Me.dtpPeriodEndDate.Value = Me.grdSaved.GetRow.Cells("Period_End_Date").Value
            Me.txtInstallmentDueDay.Text = Val(Me.grdSaved.GetRow.Cells("DueDays").Value.ToString)
            RemoveHandler Me.cmbVendor.Leave, AddressOf Me.cmbVendor_Leave
            RemoveHandler Me.cmbInvoice.RowSelected, AddressOf Me.cmbInvoice_RowSelected
            Me.cmbVendor.Value = Val(Me.grdSaved.GetRow.Cells("CustomerCode").Value.ToString)
            Me.txtProductName.Text = Me.grdSaved.GetRow.Cells("Product_Name").Value.ToString
            Me.txtChassisNo.Text = Me.grdSaved.GetRow.Cells("ChassisNo").Value.ToString
            Me.txtEngineNo.Text = Me.grdSaved.GetRow.Cells("Engine No").Value.ToString
            Me.txtColor.Text = Me.grdSaved.GetRow.Cells("Color").Value.ToString
            Me.txtInvoiceAmount.Text = Val(Me.grdSaved.GetRow.Cells("InvoiceAmount").Value.ToString)
            Me.txtAdvance.Text = Val(Me.grdSaved.GetRow.Cells("AdvanceAmount").Value.ToString)
            Me.txtRemainingAmount.Text = Val(Me.grdSaved.GetRow.Cells("RemainigAmount").Value.ToString)
            Me.txtRemarks.Text = Me.grdSaved.GetRow.Cells("Remarks").Value.ToString
            Me.txtMonthlyInstallmentPerMonth.Text = Val(Me.grdSaved.GetRow.Cells("InstallmentPerMonth").Value.ToString)
            Me.txtFather.Text = Me.grdSaved.GetRow.Cells("FatherName").Value.ToString
            Me.txtCast.Text = Me.grdSaved.GetRow.Cells("Cast").Value.ToString
            Me.txtCNIC.Text = Me.grdSaved.GetRow.Cells("CNIC").Value.ToString
            Me.txtEngineNo.Text = Me.grdSaved.GetRow.Cells("EngineNo").Value.ToString
            Me.txtRegistrationNo.Text = Me.grdSaved.GetRow.Cells("RegistrationNo").Value.ToString
            Me.txtAC.Text = Me.grdSaved.GetRow.Cells("AC").Value.ToString

            If Val(Me.grdSaved.GetRow.Cells("SalesDetailID").Value.ToString) > 0 Then
                Dim dt As DataTable = CType(Me.cmbInvoice.DataSource, DataTable)
                dt.AcceptChanges()
                Dim dr As DataRow = dt.NewRow
                dr(0) = Val(Me.grdSaved.GetRow.Cells("SalesDetailID").Value.ToString)
                dr(1) = Me.grdSaved.GetRow.Cells("Invoice No").Value.ToString
                dr(2) = Me.grdSaved.GetRow.Cells("Invoice Date").Value.ToString
                dr(3) = Me.grdSaved.GetRow.Cells("Item").Value.ToString
                dr(4) = Me.grdSaved.GetRow.Cells("ChassisNo").Value.ToString
                dr(9) = Me.grdSaved.GetRow.Cells("SalesID").Value.ToString
                dr(10) = Me.grdSaved.GetRow.Cells("ArticleDefID").Value.ToString
                dt.Rows.Add(dr)
                dt.AcceptChanges()
            End If
            Me.cmbInvoice.Value = Val(Me.grdSaved.GetRow.Cells("SalesDetailID").Value.ToString)
            Me.cmbInvoice.Enabled = False
            AddHandler Me.cmbInvoice.RowSelected, AddressOf Me.cmbInvoice_RowSelected
            AddHandler Me.cmbVendor.Leave, AddressOf Me.cmbVendor_Leave
            Me.txtMonthlyInstallmentPerMonth_LostFocus(Nothing, Nothing)
            GetAllRecords("Detail")
            GetAllRecords("WitnessOne")
            GetAllRecords("WitnessTwo")
            ApplySecurity(SBUtility.Utility.EnumDataMode.Edit)
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub grdSaved_DoubleClick(sender As Object, e As EventArgs) Handles grdSaved.DoubleClick
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Function GetDocumentNo() As String
        Try
            'If Me.txtPONo.Text = "" Then
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("INS" & "-" & Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "InstallmentMasterTable", "Doc_No")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("INS" & "-" & Format(Me.dtpPODate.Value, "yy") & Me.dtpPODate.Value.Month.ToString("00"), 4, "InstallmentMasterTable", "Doc_No")
            Else
                Return GetNextDocNo("INS", 6, "InstallmentMasterTable", "Doc_No")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            If e.Column.Index = grdDetail.DeleteButton Then
                Me.grd.GetRow.Delete()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        Try

            If Me.cmbVendor.ActiveRow Is Nothing Then
                Me.cmbVendor.Rows(0).Activate()
            End If

            Dim id As Integer = 0I
            id = Me.cmbVendor.ActiveRow.Cells(0).Value
            FillCombos()
            Me.cmbVendor.Value = id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@InstallmentId", grdSaved.GetRow.Cells("InstallmentId").Value)
            ShowReport("rptInstallment")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        Try

            If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
            If Delete() = True Then DialogResult = Windows.Forms.DialogResult.Yes
            ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbVendor_Leave(sender As Object, e As EventArgs) Handles cmbVendor.Leave
        Try
            If Me.cmbVendor.IsItemInList = False Then Exit Sub
            If Me.cmbVendor.ActiveRow Is Nothing Then Exit Sub
            '    Me.cmbVendor.Rows(0).Activate()
            'End If
            FillCombos("Invoice")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbInvoice_RowSelected(sender As Object, e As Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbInvoice.RowSelected
        Try

            If Me.cmbInvoice.IsItemInList = False Then Exit Sub
            If Me.cmbInvoice.ActiveRow Is Nothing Then
                Me.cmbInvoice.Rows(0).Activate()
            End If

            Me.txtInvoiceAmount.Text = Val(Me.cmbInvoice.ActiveRow.Cells("Invoice Amount").Value.ToString())
            Me.txtAdvance.Text = Val(Me.cmbInvoice.ActiveRow.Cells("Receipt").Value.ToString)
            Me.txtProductName.Text = Me.cmbInvoice.ActiveRow.Cells("Item").Value.ToString
            Me.txtChassisNo.Text = Me.cmbInvoice.ActiveRow.Cells("Chassis No").Value.ToString
            Me.txtEngineNo.Text = Me.cmbInvoice.ActiveRow.Cells("Engine No").Value.ToString
            Me.txtColor.Text = Me.cmbInvoice.ActiveRow.Cells("Color").Value.ToString


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
#Region "SMS Template Setting"
    Public Function GetSMSParamters() As List(Of String)
        Try
            Dim str As New List(Of String)
            str.Add("@AccountCode")
            str.Add("@AccountTitle")
            str.Add("@InvoiceNo")
            str.Add("@InvoiceDate")
            str.Add("@OtherDocNo")
            str.Add("@Remarks")
            str.Add("@InvoiceAmount")
            str.Add("@AdvanceAmount")
            str.Add("@InstallmentAmount")
            str.Add("@PreviousBalance")
            str.Add("@CompanyName")
            str.Add("@SIRIUS")
            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetSMSKey() As List(Of String)
        Try
            Dim str As New List(Of String)
            str.Add("Due Payment")
            str.Add("Over Due Payment")
            str.Add("Upcoming Payment")
            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnSMSTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSMSTemplate.Click
        Try
            Dim frmSMS As New frmSMSTemplate
            ApplyStyleSheet(frmSMS)
            frmSMS.cmbKey.DataSource = GetSMSKey()
            frmSMS.lstParameters.DataSource = GetSMSParamters()
            frmSMS.Show()
            frmSMS.BringToFront()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub



    Public Sub SMSScheduleSave(InstallmentId As Integer, jsRow As Janus.Windows.GridEX.GridEXRow, trans As OleDb.OleDbTransaction)
        Dim cmd As New OleDbCommand
        cmd.Connection = trans.Connection
        cmd.Transaction = trans

        Try

            cmd.CommandTimeout = 300
            cmd.CommandType = CommandType.Text


            Dim dblPreviouseAmount As Double = GetCurrentBalance(Me.cmbVendor.Value, trans)

            Dim objTemp As New SMSTemplateParameter
            Dim obj As Object = GetSMSTemplate("Upcoming Payment")
            If obj IsNot Nothing Then
                objTemp.SMSTemplate = CType(obj, SMSTemplateParameter).SMSTemplate
                Dim strMessage As String = objTemp.SMSTemplate
                strMessage = strMessage.Replace("@AccountCode", "" & Me.cmbVendor.ActiveRow.Cells("Code").Value.ToString.Replace("'", "''") & "").Replace("@AccountTitle", "" & Me.cmbVendor.ActiveRow.Cells("Name").Value.ToString.Replace("'", "''") & "").Replace("@InvoiceNo", "" & Me.cmbInvoice.ActiveRow.Cells("Invoice No").Value.ToString.Replace("'", "''") & "").Replace("@InvoiceDate", "" & Me.cmbInvoice.ActiveRow.Cells("Invoice Date").Value.ToString.Replace("'", "''") & "").Replace("@OtherDocNo", "" & Me.txtPONo.Text.Replace("'", "''") & "").Replace("@Remarks", "" & Me.txtRemarks.Text.Replace("'", "''") & "").Replace("@InvoiceAmount", "" & Val(Me.cmbInvoice.ActiveRow.Cells("Invoice Amount").Value.ToString) & "").Replace("@AdvanceAmount", "" & Val(Me.cmbInvoice.ActiveRow.Cells("Receipt").Value.ToString) & "").Replace("@InstallmentAmount", "" & Val(jsRow.Cells(grdDetail.Installments).Value.ToString) & "").Replace("@PreviousBalance", "" & dblPreviouseAmount & "").Replace("@CompanyName", "" & CompanyTitle & "").Replace("@SIRIUS", "Automated by http://www.SIRIUS.net")

                cmd.CommandText = ""
                cmd.CommandText = "INSERT INTO InstallmentScheduleSMSTable(InstallmentId,SMSDate,CustomerID, CurstomerName,MobileNo,Email,SMSBody,SMSType) " _
                    & " VALUES(" & InstallmentId & ",Convert(DateTime,'" & Convert.ToDateTime(jsRow.Cells(grdDetail.Due_Date).Value).AddDays(-5).ToString("yyyy-M-d hh:mm:ss tt") & "',102)," & Me.cmbVendor.Value & ", N'" & Me.cmbVendor.ActiveRow.Cells("Name").Value.ToString.Replace("'", "''") & "','" & Me.cmbVendor.ActiveRow.Cells("Mobile").Value.ToString.Replace("'", "''") & "','" & Me.cmbVendor.ActiveRow.Cells("Email").Value.ToString.Replace("'", "''") & "',N'" & strMessage.Replace("'", "''") & "','Upcoming Payment')"
                cmd.ExecuteNonQuery()

            End If

            Dim objTemp1 As New SMSTemplateParameter
            Dim obj1 As Object = GetSMSTemplate("Due Payment")
            If obj1 IsNot Nothing Then
                objTemp1.SMSTemplate = CType(obj1, SMSTemplateParameter).SMSTemplate
                Dim strMessage As String = objTemp1.SMSTemplate
                strMessage = strMessage.Replace("@AccountCode", "" & Me.cmbVendor.ActiveRow.Cells("Code").Value.ToString.Replace("'", "''") & "").Replace("@AccountTitle", "" & Me.cmbVendor.ActiveRow.Cells("Name").Value.ToString.Replace("'", "''") & "").Replace("@InvoiceNo", "" & Me.cmbInvoice.ActiveRow.Cells("Invoice No").Value.ToString.Replace("'", "''") & "").Replace("@InvoiceDate", "" & Me.cmbInvoice.ActiveRow.Cells("Invoice Date").Value.ToString.Replace("'", "''") & "").Replace("@OtherDocNo", "" & Me.txtPONo.Text.Replace("'", "''") & "").Replace("@Remarks", "" & Me.txtRemarks.Text.Replace("'", "''") & "").Replace("@InvoiceAmount", "" & Val(Me.cmbInvoice.ActiveRow.Cells("Invoice Amount").Value.ToString) & "").Replace("@AdvanceAmount", "" & Val(Me.cmbInvoice.ActiveRow.Cells("Receipt").Value.ToString) & "").Replace("@InstallmentAmount", "" & Val(jsRow.Cells(grdDetail.Installments).Value.ToString) & "").Replace("@PreviousBalance", "" & dblPreviouseAmount & "").Replace("@CompanyName", "" & CompanyTitle & "").Replace("@SIRIUS", "Automated by http://www.SIRIUS.net")
                cmd.CommandText = ""
                cmd.CommandText = "INSERT INTO InstallmentScheduleSMSTable(InstallmentId,SMSDate,CustomerID,CurstomerName,MobileNo,Email,SMSBody,SMSType) " _
                    & " VALUES(" & InstallmentId & ",Convert(DateTime,'" & Convert.ToDateTime(jsRow.Cells(grdDetail.Due_Date).Value).ToString("yyyy-M-d hh:mm:ss tt") & "',102)," & Me.cmbVendor.Value & ", N'" & Me.cmbVendor.ActiveRow.Cells("Name").Value.ToString.Replace("'", "''") & "','" & Me.cmbVendor.ActiveRow.Cells("Mobile").Value.ToString.Replace("'", "''") & "','" & Me.cmbVendor.ActiveRow.Cells("Email").Value.ToString.Replace("'", "''") & "',N'" & strMessage.Replace("'", "''") & "','Due Payment')"
                cmd.ExecuteNonQuery()

            End If

            Dim objTemp2 As New SMSTemplateParameter
            Dim obj2 As Object = GetSMSTemplate("Over Due Payment")
            If obj2 IsNot Nothing Then
                objTemp2.SMSTemplate = CType(obj2, SMSTemplateParameter).SMSTemplate
                Dim strMessage As String = objTemp2.SMSTemplate
                strMessage = strMessage.Replace("@AccountCode", "" & Me.cmbVendor.ActiveRow.Cells("Code").Value.ToString.Replace("'", "''") & "").Replace("@AccountTitle", "" & Me.cmbVendor.ActiveRow.Cells("Name").Value.ToString.Replace("'", "''") & "").Replace("@InvoiceNo", "" & Me.cmbInvoice.ActiveRow.Cells("Invoice No").Value.ToString.Replace("'", "''") & "").Replace("@InvoiceDate", "" & Me.cmbInvoice.ActiveRow.Cells("Invoice Date").Value.ToString.Replace("'", "''") & "").Replace("@OtherDocNo", "" & Me.txtPONo.Text.Replace("'", "''") & "").Replace("@Remarks", "" & Me.txtRemarks.Text.Replace("'", "''") & "").Replace("@InvoiceAmount", "" & Val(Me.cmbInvoice.ActiveRow.Cells("Invoice Amount").Value.ToString) & "").Replace("@AdvanceAmount", "" & Val(Me.cmbInvoice.ActiveRow.Cells("Receipt").Value.ToString) & "").Replace("@InstallmentAmount", "" & Val(jsRow.Cells(grdDetail.Installments).Value.ToString) & "").Replace("@PreviousBalance", "" & dblPreviouseAmount & "").Replace("@CompanyName", "" & CompanyTitle & "").Replace("@SIRIUS", "Automated by http://www.SIRIUS.net")
                cmd.CommandText = ""
                cmd.CommandText = "INSERT INTO InstallmentScheduleSMSTable(InstallmentId,SMSDate,CustomerID,CurstomerName,MobileNo,Email,SMSBody,SMSType) " _
                    & " VALUES(" & InstallmentId & ",Convert(DateTime,'" & Convert.ToDateTime(jsRow.Cells(grdDetail.Due_Date).Value).AddDays(1).ToString("yyyy-M-d hh:mm:ss tt") & "',102)," & Me.cmbVendor.Value & ",N'" & Me.cmbVendor.ActiveRow.Cells("Name").Value.ToString.Replace("'", "''") & "','" & Me.cmbVendor.ActiveRow.Cells("Mobile").Value.ToString.Replace("'", "''") & "','" & Me.cmbVendor.ActiveRow.Cells("Email").Value.ToString.Replace("'", "''") & "',N'" & strMessage.Replace("'", "''") & "','Overdue Payment')"
                cmd.ExecuteNonQuery()

            End If

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Sub

#End Region

    Private Sub btnAgreementLetter_Click(sender As Object, e As EventArgs) Handles btnAgreementLetter.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@InstallmentId", Val(Me.grdSaved.GetRow.Cells("InstallmentId").Value.ToString))
            ShowReport("rptAutoAgreementMemo", , , , False)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lnkWitnessTwo_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkWitnessTwo.LinkClicked
        Try
            If Me.gbWitnessTwo.Visible = False Then
                Me.gbWitnessTwo.Visible = True
                'Me.gbWitnessOne.SendToBack()
            End If
            Me.gbWitnessOne.Visible = False
            'Me.gbWitnessTwo.BringToFront()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lnkWitnessOne_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkWitnessOne.LinkClicked
        Try
            If Me.gbWitnessOne.Visible = False Then
                Me.gbWitnessOne.Visible = True
                'Me.gbWitnessTwo.SendToBack()
            End If
            Me.gbWitnessTwo.Visible = False
            'Me.gbWitnessOne.BringToFront()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub tsbTask_Click(sender As Object, e As EventArgs) Handles tsbTask.Click
        Try
            If Not grdSaved.GetRow Is Nothing AndAlso grdSaved.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Dim Lcontrol As String = String.Empty
                Dim control As String = String.Empty
                'Dim VNo = v
                Lcontrol = frmModProperty.fname.Name
                control = "frminstallment"
                'frmMain.LoadControl("Tasks")
                Dim frmtask As New frmTasks
                frmtask.Ref_No = grdSaved.CurrentRow.Cells(1).Value.ToString
                frmtask.ReferenceForm = control
                'frmtask.GetReferenceTasks(frmtask.Ref_No)
                'tsbAssign.Text = frmtask.CountReferenceTasks(frmtask.Ref_No).ToString()
                frmtask.StartPosition = FormStartPosition.CenterScreen
                frmtask.Text = "Installment (" & frmtask.Ref_No & ") "
                frmtask.Width = 950
                frmtask.ShowDialog()
                frmtask.UltraTabControl1.SelectedTab = frmTasks.UltraTabControl1.Tabs(1).TabPage.Tab
                'frmtask.ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub tsbConfig_Click(sender As Object, e As EventArgs) Handles tsbConfig.Click
        Try
            If Not frmMain.Panel2.Controls.Contains(frmSystemConfigurationNew) Then
                frmMain.LoadControl("frmSystemConfiguration")
            End If
            frmSystemConfigurationNew.ScreenName = frmSystemConfigurationNew.enmScreen.Sales
            frmMain.LoadControl("frmSystemConfiguration")
            frmSystemConfigurationNew.SelectTab()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


End Class
