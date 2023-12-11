Imports SBUtility.Utility
Public Class uiListControl

    Enum enumWhichHelpForm
        _ProductSearchHelp
        _ShopSearchHelp
    End Enum


    Public Delegate Sub SelectionChange(ByVal sender As Object, ByVal e As IndexEventArgs)
    Public Event SelectedIndexChaned As SelectionChange
    Public Event Magnifire(ByVal sender As Object, ByVal e As EventArgs)
    Public Event OnRefresh(ByVal sender As Object, ByVal e As EventArgs)


    Private _DisableWhenChecked As Boolean = False
    Public Property disableWhenChecked() As Boolean
        Get
            Return Me._DisableWhenChecked
        End Get
        Set(ByVal value As Boolean)
            Me._DisableWhenChecked = value
            Me.uichkNot.Checked = value
        End Set
    End Property

    Private _strIDs As String = String.Empty
    Public ReadOnly Property SelectedIDs() As String
        Get
            Me._strIDs = Me.GetSelectedIDs()
            Return Me._strIDs
        End Get
    End Property

    Private _HeadingLabelName As String
    Public Property HeadingLabelName() As String
        Get
            Return Me._HeadingLabelName
        End Get
        Set(ByVal value As String)
            Me.lblHeading.Name = value
            Me._HeadingLabelName = value
        End Set
    End Property

    Private _SelectedItems As String
    Public ReadOnly Property SelectedItems() As String
        Get
            Me.GetSelectedItems()
            Return Me._SelectedItems
        End Get
    End Property
    Private _WhichHelp As enumWhichHelpForm = Nothing
    Public Property WhichHelp() As enumWhichHelpForm
        Get
            Return _WhichHelp
        End Get
        Set(ByVal value As enumWhichHelpForm)
            _WhichHelp = value
        End Set
    End Property

    Private _ShowNoCheck As Boolean = False
    Public Property ShowNoCheck() As Boolean
        Get
            Return _ShowNoCheck
        End Get
        Set(ByVal value As Boolean)
            _ShowNoCheck = value
            Me.uichkNot.Visible = value
        End Set
    End Property

    Private _ShowSelectAll As Boolean = True
    Public Property ShowSelectall() As Boolean
        Get
            Return Me._ShowSelectAll
        End Get
        Set(ByVal value As Boolean)
            Me._ShowSelectAll = value
            Me.btnSelectAll.Visible = value
        End Set
    End Property

    Private _ShowInverse As Boolean = True
    Public Property ShowInverse() As Boolean
        Get
            Return Me._ShowInverse
        End Get
        Set(ByVal value As Boolean)
            Me._ShowInverse = value
            Me.btnInverse.Visible = value
        End Set
    End Property


    Private _HeadingText As String
    Public Property HeadingText() As String
        Get
            Return _HeadingText
        End Get
        Set(ByVal value As String)
            _HeadingText = value
            Me.lblHeading.Text = value
        End Set
    End Property

    Private _ShowResetAllButton As Boolean = False
    Public Property ShowResetAllButton() As Boolean
        Get
            Return _ShowResetAllButton
        End Get
        Set(ByVal value As Boolean)
            _ShowResetAllButton = value
            Me.uibtnResetAll.Visible = value
        End Set
    End Property

    Private _ShowMagnifierButton As Boolean = False
    Public Property ShowMagnifierButton() As Boolean
        Get
            Return _ShowMagnifierButton
        End Get
        Set(ByVal value As Boolean)
            _ShowMagnifierButton = value
            Me.uibtnMagnifier.Visible = value
        End Set
    End Property

    Private _ShowAddNewButton As Boolean = False
    Public Property ShowAddNewButton() As Boolean
        Get
            Return _ShowAddNewButton
        End Get
        Set(ByVal value As Boolean)
            _ShowAddNewButton = value
            Me.uibtnAddNew.Visible = value
        End Set
    End Property

    Private _AddWhichConfiguration As EnumProjectForms = EnumProjectForms.ForAllForms
    Public Property AddWhichConfiguration() As EnumProjectForms
        Get
            Return _AddWhichConfiguration
        End Get
        Set(ByVal value As EnumProjectForms)
            _AddWhichConfiguration = value
        End Set
    End Property


    Sub SetProperties()
        Me.uibtnResetAll.Visible = Me._ShowResetAllButton
        Me.uibtnMagnifier.Visible = _ShowMagnifierButton
        Me.uichkNot.Visible = Me._ShowNoCheck
        Me.lblHeading.Text = _HeadingText
        Me.uibtnAddNew.Visible = _ShowAddNewButton
        Me.Refresh()
    End Sub

    Public ListItemText As String = ""

    Sub CountSelected()
        Me.lblCount.Text = "[" & Me.ListItem.SelectedItems.Count & "/" & Me.ListItem.Items.Count & "]"
    End Sub
    Private Sub uiListControl_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, MyBase.CausesValidationChanged, MyBase.BindingContextChanged
        Me.lblCount.Text = "[0/" & Me.ListItem.Items.Count & "]"
    End Sub

    Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        Try
            RemoveHandler ListItem.SelectedIndexChanged, AddressOf ListItem_SelectedIndexChanged
            For intI As Long = 0 To (Me.ListItem.Items.Count - 1)

                If intI = Me.ListItem.Items.Count - 1 Then
                    AddHandler ListItem.SelectedIndexChanged, AddressOf ListItem_SelectedIndexChanged
                End If
                Me.ListItem.SetSelected(intI, True)
            Next
            Me.CountSelected()
        Catch ex As Exception
            AddHandler ListItem.SelectedIndexChanged, AddressOf ListItem_SelectedIndexChanged
        End Try
    End Sub

    Private Sub btnInverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInverse.Click

        Try

            RemoveHandler ListItem.SelectedIndexChanged, AddressOf ListItem_SelectedIndexChanged


            For intI As Long = 0 To (Me.ListItem.Items.Count - 1)
                Me.ListItem.SetSelected(intI, Not Me.ListItem.GetSelected(intI))

                If intI = Me.ListItem.Items.Count - 1 Then
                    AddHandler ListItem.SelectedIndexChanged, AddressOf ListItem_SelectedIndexChanged
                End If
            Next
            Me.ListItem_SelectedIndexChanged(Me, Nothing)
            Me.CountSelected()

        Catch ex As Exception
            AddHandler ListItem.SelectedIndexChanged, AddressOf ListItem_SelectedIndexChanged
        End Try

    End Sub

    Private Sub Magnifier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uibtnMagnifier.Click
        Try
            If Not Me._WhichHelp = Nothing Then
                If Me._WhichHelp = enumWhichHelpForm._ProductSearchHelp Then

                ElseIf Me._WhichHelp = enumWhichHelpForm._ShopSearchHelp Then
                    'Dim frm As New frmShopSelectionHelp
                    Dim frm As New Form
                    frm.ShowInTaskbar = False
                    'frm.ShopList = Me
                    ' ApplyStyleSheet(frm, EnumProjectForms.ForAllForms.ToString)
                    frm.ShowDialog()
                End If
            End If
            RaiseEvent Magnifire(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ResetAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uibtnResetAll.Click

        Try
            If Not Me._WhichHelp = Nothing Then
                If Me._WhichHelp = enumWhichHelpForm._ProductSearchHelp Then

                ElseIf Me._WhichHelp = enumWhichHelpForm._ShopSearchHelp Then
                    Me.ConfigureControl()
                End If
            End If
            RaiseEvent OnRefresh(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub uiListControl_Layout(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LayoutEventArgs) Handles MyBase.Layout
        Me.SetProperties()
    End Sub

    Private Sub uiListControl_StyleChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.StyleChanged
        Me.SetProperties()
    End Sub

    Private Sub uiListControl_ImeModeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ImeModeChanged, MyBase.Validated
        Me.SetProperties()
    End Sub

    Private Sub uiListControl_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus, MyBase.Click
        Me.SetProperties()
    End Sub

    Private Sub uiListControl_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.SetProperties()
    End Sub

    Private Sub ListItem_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListItem.SelectedIndexChanged

        ' If Me.ListItem.SelectedIndex >= 0 Then
        Dim objClass As New IndexEventArgs
        objClass.IDs = Me.GetSelectedIDs()
        RaiseEvent SelectedIndexChaned(Me, objClass)
        Me.CountSelected()
        'End If

    End Sub

    Private Function GetSelectedIDs() As String
        Try
            Dim strIDs As String = String.Empty
            For Each obj As Object In Me.ListItem.SelectedItems
                If TypeOf obj Is DataRowView Then
                    Dim dr As DataRowView = CType(obj, DataRowView)
                    strIDs = strIDs & IIf(strIDs.Length > 0, ",", "") & dr.Row.Item(dr.Row.Table.Columns(Me.ListItem.ValueMember).ColumnName)
                ElseIf TypeOf obj Is System.String Then
                    Dim strItem As String = CType(obj, String)
                    strIDs = strIDs & IIf(strIDs.Length > 0, ",", "") & strItem
                End If
            Next
            Return strIDs
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub GetSelectedItems()
        Try

            Dim strItems As String = String.Empty
            For Each obj As Object In Me.ListItem.SelectedItems
                If TypeOf obj Is DataRowView Then
                    Dim dr As DataRowView = CType(obj, DataRowView)
                    strItems = strItems & IIf(strItems.Length > 0, ",  '", "'") & dr.Row.Item(dr.Row.Table.Columns(Me.ListItem.DisplayMember).ColumnName) & "'"
                ElseIf TypeOf obj Is System.String Then
                    Dim strItem As String = CType(obj, String)
                    strItems = strItems & IIf(strItems.Length > 0, ",  '", "'") & strItem & "'"
                End If
            Next

            Me._SelectedItems = strItems

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub uibtnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uibtnAddNew.Click

        'Dim frmAddConf As New frmAddConfigurationValue
        'frmAddConf.MyConfiguationForm = _AddWhichConfiguration
        'Call frmAddConf.ShowDialog()
        'Call frmAddConf.Dispose()
    End Sub


    Public Sub SelectAll()
        If Me.ListItem.Items.Count > 0 Then
            Me.btnSelectAll_Click(Nothing, Nothing)
        End If
    End Sub

    Public Sub DeSelect()
        If Me.ListItem.Items.Count > 0 Then
            Me.ListItem.SelectedIndex = -1
        End If
    End Sub

    Public Sub SelectItemsByIDs(ByVal IDs As String)
        Try
            If IDs.Length = 0 Then Exit Sub

            For Each ID As String In IDs.Split(",")
                Me.ListItem.SelectedValue = ID
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub uichkNot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uichkNot.CheckedChanged

        If Me._DisableWhenChecked = False Then Exit Sub

        ListItem.Enabled = True
        Me.uibtnResetAll.Enabled = True
        Me.uibtnMagnifier.Enabled = True
        Me.uibtnAddNew.Enabled = True
        Me.btnInverse.Enabled = True
        Me.btnSelectAll.Enabled = True

        If uichkNot.Visible = True Then
            If uichkNot.Checked = True Then
                ListItem.SelectedItems.Clear()
                ListItem.Enabled = False
                Me.uibtnResetAll.Enabled = False
                Me.uibtnMagnifier.Enabled = False
                Me.uibtnAddNew.Enabled = False
                Me.btnInverse.Enabled = False
                Me.btnSelectAll.Enabled = False
            Else

                ListItem.Enabled = True
                Me.uibtnResetAll.Enabled = True
                Me.uibtnMagnifier.Enabled = True
                Me.uibtnAddNew.Enabled = True
                Me.btnInverse.Enabled = True
                Me.btnSelectAll.Enabled = True
            End If
        Else

            ListItem.Enabled = True
            Me.uibtnResetAll.Enabled = True
            Me.uibtnMagnifier.Enabled = True
            Me.uibtnAddNew.Enabled = True
            Me.btnInverse.Enabled = True
            Me.btnSelectAll.Enabled = True
        End If
    End Sub

    Public Sub ConfigureControl()
        Try
            btnSelectAll.Visible = False
            btnInverse.Visible = False

            If Not Me._WhichHelp = Nothing Then
                If Me._WhichHelp = enumWhichHelpForm._ProductSearchHelp Then

                ElseIf Me._WhichHelp = enumWhichHelpForm._ShopSearchHelp Then
                    Me.ListItem.DataSource = Nothing
                    Me.ListItem.DisplayMember = "Shop"
                    Me.ListItem.ValueMember = "Shop ID"
                    Dim dt As DataTable = CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetUserShopList.ToString), DataTable).Copy
                    dt.Rows.RemoveAt(0)
                    Me.ListItem.DataSource = dt

                    ''checking the product version
                    'If DecryptWithALP(GetSystemConfigurationValue("Version")) = "1" Then
                    '    Me.Visible = False
                    '    Me.ListItem.SelectedIndex = 0
                    'Else
                    '    Me.Visible = True
                    '    Me.ListItem.SelectedIndex = 0
                    'End If

                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class

Public Class IndexEventArgs
    Inherits EventArgs

    Public Sub New()

    End Sub

    Public Sub New(ByVal Ids As String)
        Me._Ids = Ids
    End Sub

    Private _Ids As String
    Public Property IDs() As String
        Get
            Return Me._Ids
        End Get
        Set(ByVal value As String)
            Me._Ids = value
        End Set
    End Property
End Class