Imports SBDal
Imports SBModel
Imports SBDal.UtilityDAL
Public Class frmAdjustments
    Implements IGeneral
    Dim AdjId As Integer = 0
    Dim Adjustment As AdjustmentVoucher
    Dim MarketReturnsAcId As Integer = 0
    Dim IsEditMode As Boolean = False

    Enum Customer
        Id
        Name
        State
        City
        Territory
        ExpiryDate
        Discount
        Other_Exp
        Fuel
        TransitInsurance
        Credit_Limit
        Type
        Email
        PhoneNo
    End Enum
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grd.RootTable.Columns("AdjId").Visible = False
            Me.grd.RootTable.Columns("CustomerCode").Visible = False
            Me.grd.RootTable.Columns("AdjDate").FormatString = "dd/MMM/yyyy"
            Me.grd.RootTable.Columns("detail_code").Caption = "Account Code"
            Me.grd.RootTable.Columns("detail_title").Caption = "Account Description"
            Me.grd.RootTable.Columns("AdjDate").Caption = "Date"
            Me.grd.RootTable.Columns("AdjNo").Caption = "Document No"
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If New AdjustmentDAL().Delete(Adjustment) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String = String.Empty
            If GetConfigValue("Show Vendor On Sales") = "True" Then
                Str = "SELECT     vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name, tblListState.StateName as State, tblListCity.CityName as City,  " & _
                                    "tblListTerritory.TerritoryName as Territory , tblCustomer.ExpiryDate, tblCustomer.Discper as [Discount] ,tblCustomer.otherexpanses as [Other Expense], tblCustomer.Fuel as Fuel , tblCustomer.Cridtlimt as Limit, dbo.vwCOADetail.account_type as Type, isnull(customertypes,0) as typeid, tblCustomer.Email,tblCustomer.Phone " & _
                                    "FROM  tblCustomer LEFT OUTER JOIN " & _
                                    "tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                                    "tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                                    "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                                    "vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " & _
                                    "WHERE (vwCOADetail.account_type in( 'Customer','Vendor' )) and  vwCOADetail.coa_detail_id is not  null "
            Else
                Str = "SELECT     vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name, tblListState.StateName as State, tblListCity.CityName as City,  " & _
                                                   "tblListTerritory.TerritoryName as Territory , tblCustomer.ExpiryDate,tblCustomer.Discper as [Discount] ,tblCustomer.otherexpanses as [Other Expense], tblCustomer.Fuel as Fuel , tblCustomer.Cridtlimt as Limit, dbo.vwCOADetail.account_type as Type, isnull(customertypes,0) as typeid, tblCustomer.Email,tblCustomer.Phone " & _
                                                   "FROM         tblCustomer LEFT OUTER JOIN " & _
                                                   "tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                                                   "tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                                                   "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                                                   "vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " & _
                                                   "WHERE (vwCOADetail.account_type='Customer') and  vwCOADetail.coa_detail_id is not  null "
            End If
            If IsEditMode = False Then
                Str += " AND vwCOADetail.Active=1"
            Else
                Str += " AND vwCOADetail.Active in(0,1,NULL)"
            End If
            Str += " order by tblCustomer.Sortorder, vwCOADetail.detail_title "
            FillUltraDropDown(cmbVendor, Str)
            If cmbVendor.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Id).Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Territory).Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.State).Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.ExpiryDate).Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Fuel).Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Other_Exp).Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("typeid").Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Name).Width = 300
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Credit_Limit).Width = 80
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Discount).Width = 80
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            Adjustment = New AdjustmentVoucher
            Adjustment.AdjId = AdjId
            Adjustment.AdjDate = Me.dtpDate.Value
            Adjustment.AdjNo = Me.txtDocumentNo.Text.ToString.Replace("'", "''")
            Adjustment.CustomerCode = Me.cmbVendor.ActiveRow.Cells(0).Value
            Adjustment.CustomerName = Me.cmbVendor.Text
            Adjustment.EntryDate = Date.Now
            Adjustment.MarketReturnAcId = MarketReturnsAcId
            Adjustment.MarketReturns = Val(Me.txtMarketReturns.Text)
            Adjustment.Remarks = Me.txtRemarks.Text.ToString.Replace("'", "''")
            Adjustment.UserName = LoginUserName
            Adjustment.source = Me.Text
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim dt As DataTable = New AdjustmentDAL().GetAll(Condition)
            grd.DataSource = dt
            Me.grd.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbVendor.ActiveRow.Cells(0).Value <= 0 Then
                ShowErrorMessage("Please select a customer")
                Me.cmbVendor.Focus()
                Return False
            End If
            If Me.txtMarketReturns.Text = String.Empty Then
                ShowErrorMessage("Please enter market return value")
                Me.txtMarketReturns.Focus()
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            FillCombos()
            Me.btnSave.Text = "&Save"
            AdjId = 0
            IsEditMode = False
            Me.txtDocumentNo.Text = AdjustmentDAL.GetDocumentNo
            Me.txtMarketReturns.Text = String.Empty
            Me.txtRemarks.Text = String.Empty
            Me.dtpDate.Value = Date.Now
            Me.cmbVendor.Rows(0).Activate()
            Me.cmbVendor.Focus()
            Me.GetAllRecords()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14 
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            If New AdjustmentDAL().Add(Adjustment) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Me.lblProgress.Visible = False
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
            If New AdjustmentDAL().Update(Adjustment) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0
            id = Me.cmbVendor.Value
            FillCombos()
            Me.cmbVendor.Value = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmAdjustments_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        Try
            If e.KeyCode = Keys.F4 Then
                btnSave_Click(Nothing, Nothing)
            End If

            If e.KeyCode = Keys.Escape Then

                btnNew_Click(btnNew, Nothing)
                Exit Sub
            End If


        Catch ex As Exception

        End Try
    End Sub
    Private Sub frmAdjustments_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Application.DoEvents()
            MarketReturnsAcId = Convert.ToInt32(GetConfigValue("OtherExpAccount").ToString)
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub frmAdjustments_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
    End Sub
    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub EditRecords(Optional ByVal Condition As String = "")
        Try
            If Me.grd.RowCount = 0 Then Exit Sub
            IsEditMode = True
            AdjId = Me.grd.GetRow.Cells("AdjId").Value
            Me.txtDocumentNo.Text = Me.grd.GetRow.Cells("AdjNo").Value
            Me.dtpDate.Value = Me.grd.GetRow.Cells("AdjDate").Value
            Me.cmbVendor.Value = Me.grd.GetRow.Cells("CustomerCode").Value
            Me.txtMarketReturns.Text = Me.grd.GetRow.Cells("MarketReturns").Value
            Me.txtRemarks.Text = Me.grd.GetRow.Cells("Remarks").Value.ToString
            Me.btnSave.Text = "&Update"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If Me.btnSave.Text = "&Save" Then
                    'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14 
                    'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    If Save() = True Then
                        'msg_Information(str_informSave)
                        ReSetControls()
                    End If
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update1() = True Then
                        'msg_Information(str_informUpdate)
                        ReSetControls()
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If AdjId = 0 Then
                ShowErrorMessage("Can not delete this record")
                Me.cmbVendor.Focus()
                Exit Sub
            End If
            If IsValidate() = True Then
                If Delete() = True Then
                

                    'msg_Information(str_informDelete)
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grd_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grd.DoubleClick
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnLoadAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoadAll.Click
        Try
            GetAllRecords("All")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    
    Private Sub grd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 31-12-13
        If e.KeyCode = Keys.F2 Then
            btnEdit_Click(Me.btnEdit, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            btnDelete_Click(btnDelete, Nothing)
            Exit Sub
        End If
    End Sub

    Private Sub lblProgress_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblProgress.Click

    End Sub
End Class