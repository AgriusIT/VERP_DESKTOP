Imports SBUtility
Imports SBModel
Imports SBDAL
Public Class Add
    Implements IGeneral
    Public Enum WhichCombo
        Category
        Type
        Gender
        LPO
        Company
        Unit
        Color
        Size
        Brand
    End Enum

    Private _Combo As WhichCombo
    Public WriteOnly Property Combo() As WhichCombo
        Set(ByVal value As WhichCombo)
            Me._Combo = value
        End Set
    End Property

    Private _CompanyID As Integer
    Public WriteOnly Property CompanyID() As Integer
        Set(ByVal value As Integer)
            Me._CompanyID = value
        End Set
    End Property

    Private mobjLPO As LPO
    Private mobjCategory As ProductGroup
    Private mobjType As ProductType
    Private mobjGender As ProductGender
    Private mobjCompany As Company
    Private mobjUnit As Unit
    Private mobjcolor As ColorBE
    Private mobjSize As SizeBE
    Private mobjBrand As BrandBE
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try

            If Me._Combo = WhichCombo.Category Then

                Me.mobjCategory = New ProductGroup
                Me.mobjCategory.Name = Me.txtName.Text
                Me.mobjCategory.Comments = Me.txtDescription.Text

                Me.mobjCategory.ActivityLog = New ActivityLog

                ''filling activity log
                Me.mobjCategory.ActivityLog.ApplicationName = "Config"
                Me.mobjCategory.ActivityLog.FormCaption = Me.Text

                'TODO : Define Loging User ID
                Me.mobjCategory.ActivityLog.UserID = LoginUserId
                Me.mobjCategory.ActivityLog.LogDateTime = Date.Now
                Me.mobjCategory.GroupCode = Me.txtCode.Text


            ElseIf Me._Combo = WhichCombo.LPO Then

                Me.mobjLPO = New LPO
                Me.mobjLPO.Name = Me.txtName.Text
                Me.mobjLPO.Comments = Me.txtDescription.Text
                Me.mobjLPO.CompanyID = Me._CompanyID

                Me.mobjLPO.ActivityLog = New ActivityLog

                ''filling activity log
                Me.mobjLPO.ActivityLog.ApplicationName = "Config"
                Me.mobjLPO.ActivityLog.FormCaption = Me.Text

                'TODO : Define Loging User ID
                Me.mobjLPO.ActivityLog.UserID = LoginUserId
                Me.mobjLPO.ActivityLog.LogDateTime = Date.Now
                Me.mobjLPO.LPOCode = Me.txtCode.Text


            ElseIf Me._Combo = WhichCombo.Type Then

                Me.mobjType = New ProductType
                Me.mobjType.Name = Me.txtName.Text
                Me.mobjType.Comments = Me.txtDescription.Text

                Me.mobjType.ActivityLog = New ActivityLog

                ''filling activity log
                Me.mobjType.ActivityLog.ApplicationName = "Config"
                Me.mobjType.ActivityLog.FormCaption = Me.Text

                'TODO : Define Loging User ID
                Me.mobjType.ActivityLog.UserID = LoginUserId
                Me.mobjType.ActivityLog.LogDateTime = Date.Now
                Me.mobjType.TypeCode = Me.txtCode.Text


            ElseIf Me._Combo = WhichCombo.Gender Then

                Me.mobjGender = New ProductGender
                Me.mobjGender.Name = Me.txtName.Text
                Me.mobjGender.Comments = Me.txtDescription.Text

                Me.mobjGender.ActivityLog = New ActivityLog

                ''filling activity log
                Me.mobjGender.ActivityLog.ApplicationName = "Config"
                Me.mobjGender.ActivityLog.FormCaption = Me.Text

                'TODO : Define Loging User ID
                Me.mobjGender.ActivityLog.UserID = LoginUserId
                Me.mobjGender.ActivityLog.LogDateTime = Date.Now

            ElseIf Me._Combo = WhichCombo.Unit Then

                Me.mobjUnit = New Unit
                Me.mobjUnit.Name = Me.txtName.Text
                Me.mobjUnit.Comments = Me.txtDescription.Text
                Me.mobjUnit.Active = True

                Me.mobjUnit.ActivityLog = New ActivityLog

                ''filling activity log
                Me.mobjUnit.ActivityLog.ApplicationName = "Config"
                Me.mobjUnit.ActivityLog.FormCaption = Me.Text

                'TODO : Define Loging User ID
                Me.mobjUnit.ActivityLog.UserID = LoginUserId
                Me.mobjUnit.ActivityLog.LogDateTime = Date.Now

            ElseIf Me._Combo = WhichCombo.Company Then

                Me.mobjCompany = New Company
                Me.mobjCompany.Name = Me.txtName.Text
                Me.mobjCompany.Comments = Me.txtDescription.Text

                Me.mobjCompany.ActivityLog = New ActivityLog

                ''filling activity log
                Me.mobjCompany.ActivityLog.ApplicationName = "Config"
                Me.mobjCompany.ActivityLog.FormCaption = Me.Text

                'TODO : Define Loging User ID
                Me.mobjCompany.ActivityLog.UserID = LoginUserId
                Me.mobjCompany.ActivityLog.LogDateTime = Date.Now
                Me.mobjCompany.CategoryCode = Me.txtCode.Text
            ElseIf _Combo = WhichCombo.Color Then
                ' for Add Color
                Me.mobjcolor = New ColorBE
                Me.mobjcolor.ArticleColorName = Me.txtName.Text
                Me.mobjcolor.ColorCode = Me.txtDescription.Text
                Me.mobjcolor.Comments = String.Empty
                Me.mobjcolor.SortOrder = 1
                Me.mobjcolor.Active = True
                Me.mobjcolor.IsDate = Date.Now


                ''filling activity log
                mobjcolor.ActivityLog = New ActivityLog
                Me.mobjcolor.ActivityLog.ApplicationName = "Config"
                Me.mobjcolor.ActivityLog.FormCaption = Me.Text

                'TODO : Define Loging User ID
                Me.mobjcolor.ActivityLog.UserID = LoginUserId
                Me.mobjcolor.ActivityLog.LogDateTime = Date.Now
            ElseIf _Combo = WhichCombo.Size Then
                Me.mobjSize = New SizeBE
                Me.mobjSize.SizeId = 0
                Me.mobjSize.SizeName = Me.txtName.Text
                Me.mobjSize.SizeCode = Me.txtDescription.Text

                mobjSize.ActivityLog = New ActivityLog
                Me.mobjSize.ActivityLog.ApplicationName = "Config"
                Me.mobjSize.ActivityLog.FormCaption = Me.Text

                Me.mobjSize.ActivityLog.UserID = LoginUserId
                Me.mobjSize.ActivityLog.LogDateTime = Date.Now
            ElseIf _Combo = WhichCombo.Brand Then

                Me.mobjBrand = New BrandBE
                Me.mobjBrand.ArticleBrandId = 0I
                Me.mobjBrand.ArticleBrandName = Me.txtName.Text
                Me.mobjBrand.Description = Me.txtDescription.Text
                Me.mobjBrand.Active = True
                Me.mobjBrand.SortOrder = 1
                mobjBrand.ActivityLog = New ActivityLog
                Me.mobjBrand.ActivityLog.ApplicationName = "Config"
                Me.mobjBrand.ActivityLog.FormCaption = Me.Text
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional ByVal Mode As Utility.EnumDataMode = Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtName.Text.Trim = String.Empty Then
                ShowInformationMessage("Enter Name")
                Me.txtName.Focus()
                Return False
            End If
            Me.FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If Me._Combo = WhichCombo.Category Then
                If New ProductGroupDAL().Add(Me.mobjCategory) Then Return True
            ElseIf Me._Combo = WhichCombo.Gender Then
                If New ProductGenderDAL().Add(Me.mobjGender) Then Return True
            ElseIf Me._Combo = WhichCombo.Unit Then
                If New UnitDAL().Add(Me.mobjUnit) Then Return True
            ElseIf Me._Combo = WhichCombo.LPO Then
                If New LPODAL().Add(Me.mobjLPO) Then Return True
            ElseIf Me._Combo = WhichCombo.Type Then
                If New ProductTypeDAL().Add(Me.mobjType) Then Return True
            ElseIf Me._Combo = WhichCombo.Company Then
                If New CompanyDAL().Add(Me.mobjCompany) Then Return True
            ElseIf Me._Combo = WhichCombo.Color Then
                If New ColorDAL().Add(Me.mobjcolor) = True Then Return True
            ElseIf Me._Combo = WhichCombo.Size Then
                If New SizeDAL().Add(mobjSize) = True Then Return True
            ElseIf Me._Combo = WhichCombo.Brand Then
                If New BrandDAL().Add(mobjBrand) = True Then Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub Add_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 15 -1-14
            If e.KeyCode = Keys.F4 Then
                btnSave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then

                btnCancel_Click(Nothing, Nothing)
                Exit Sub
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub

    Private Sub AddItems_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.txtName.Size = New Size(268, 20)
            txtName.BringToFront()
            Me.Label1.Text = "Name"
            Me.Label2.Text = "Description"
            If Me._Combo = WhichCombo.Category Then
                Me.Text = "Define Category"
                Me.txtName.Size = New Size(165, 20)
                txtName.BringToFront()
            ElseIf Me._Combo = WhichCombo.Gender Then
                Me.Text = "Define Gender"
            ElseIf Me._Combo = WhichCombo.Unit Then
                Me.Text = "Define Unit"
            ElseIf Me._Combo = WhichCombo.LPO Then
                Me.Text = "Define LPO"
                Me.txtName.Size = New Size(165, 20)
                txtName.BringToFront()
            ElseIf Me._Combo = WhichCombo.Type Then
                Me.Text = "Define Type"
                Me.txtName.Size = New Size(165, 20)
                txtName.BringToFront()
            ElseIf Me._Combo = WhichCombo.Company Then
                Me.Text = "Define Company"
                'ElseIf Me._Combo = WhichCombo.Size Then
                '    Me.Text = "Add Size"
                Me.txtName.Size = New Size(165, 20)
                txtName.BringToFront()
            ElseIf Me._Combo = WhichCombo.Color Then
                Me.Text = "Add Color"
                Me.Label2.Text = "Code"
            ElseIf Me._Combo = WhichCombo.Color Then
                Me.Text = "Define Combination"
                Me.Label1.Text = "Code"
                Me.Label2.Text = "Name"
            ElseIf Me._Combo = WhichCombo.Size Then
                Me.Text = "Define Size"
                Me.Label1.Text = "Code"
                Me.Label2.Text = "Name"
            ElseIf Me._Combo = WhichCombo.Brand Then
                Me.Text = "Define Article Grade"
                Me.Label1.Text = "Grade Name"
                Me.Label2.Text = "Description"
            End If
            Me.txtDescription.Text = String.Empty
            Me.txtName.Text = String.Empty
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Try
            If Not Me.IsValidate Then Exit Sub
            'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Application.DoEvents()
            If Me.Save Then Me.DialogResult = Windows.Forms.DialogResult.Yes
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class