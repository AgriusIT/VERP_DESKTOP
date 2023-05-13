Imports SBDAL
Imports SBModel
Imports System
Imports System.Collections.Specialized
Imports SBUtility.Utility
Imports System.IO


Module ModUtlGlobalFunctions
    ''=====================================================================================
    ''=====================================================================================

    Public Function AddZeroIndexString(ByVal dt As DataTable) As DataTable
        Try
            Dim dr As DataRow = dt.NewRow
            dr(0) = 0
            dr(1) = gstrComboZeroIndexString
            dt.Rows.InsertAt(dr, 0)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function IsValidToDelete1(ByVal TableName As String, ByVal FieldName As String, ByVal FieldValue As String) As Boolean

        Dim strSql As String = "SELECT    * from " & TableName & " where " & FieldName & "='" & FieldValue & "'"
        Dim dt As DataTable = UtilityDAL.GetDataTable(strSql)

        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    Public Sub FillApplicationHashTable()

        'frmLogin.prgBar1.Minimum = 0
        'frmLogin.prgBar1.Maximum = 1



        'frmLogin.prgBar1.Value = 1
        'frmLogin.lblStatusBarComment.Text = "Loading Security Controls"
        'Application.DoEvents()
        ' '' Add User Form Collection to Hashtable
        'AddUserFormListToHashTable()


        'frmLogin.prgBar1.Value = frmLogin.prgBar1.Value + 1
        'frmLogin.lblStatusBarComment.Text = "Loading Cities"
        'Application.DoEvents()
        ' ''Adds City Collection in hastable
        'AddCityListToHashTable()


        'frmLogin.lblStatusBarComment.Text = String.Empty
        'Application.DoEvents()
        'Application.DoEvents()
        'frmLogin.prgBar1.Value = 0



        'frmLogin.prgBar1.Value = frmLogin.prgBar1.Value + 1
        'frmLogin.lblStatusBarComment.Text = "Loading System Configurations"
        'Application.DoEvents()
        ' ''Add Language Based Control List in Hashtable
        'AddSystemConfigurationListToHashTable()


    End Sub


    Public Sub AddCityListToHashTable()

        ' ''Remove existing object from hashtable
        'If gObjMyAppHashTable.ContainsKey(EnumHashTableKeyConstants.GetCityList.ToString()) Then gObjMyAppHashTable.Remove(EnumHashTableKeyConstants.GetCityList.ToString())

        'Dim objDal As New CityDAL

        'Dim dt As DataTable = objDal.GetAll()
        'Dim dr As DataRow = dt.NewRow
        'dr.Item("City ID") = 0
        'dr.Item("City Name") = gstrComboZeroIndexString
        'dt.Rows.InsertAt(dr, 0)
        'gObjMyAppHashTable.Add(EnumHashTableKeyConstants.GetCityList.ToString(), dt)

    End Sub

    Public Function AddGeneralNotification(Title As String, Optional Description As String = "", Optional UserList As List(Of SecurityUser) = Nothing)
        Try
            Dim Notification As New AgriusNotifications
            Notification.NotificationTitle = Title
            Notification.NotificationDescription = Description

            If Not UserList Is Nothing Then
                Dim objDList As New List(Of NotificationDetail)

                For Each user As SecurityUser In UserList
                    objDList.Add(New NotificationDetail(user))
                Next
                Notification.NotificationDetils = objDList
            End If

            Dim Dal As New NotificationDAL
            Dim list As New List(Of AgriusNotifications)
            list.Add(Notification)

            Dal.AddNotification(list)

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Sub AddSystemConfigurationListToHashTable()
        ''Remove existing object from hashtable
        If gObjMyAppHashTable.ContainsKey(EnumHashTableKeyConstants.GetSystemConfigurationList.ToString()) Then gObjMyAppHashTable.Remove(EnumHashTableKeyConstants.GetSystemConfigurationList.ToString())
        'Dim objDal As New SystemConfigurationDAL
        Dim dt As DataTable = SystemConfigurationDAL.GetAll
        Dim dr As DataRow = dt.NewRow
        dr.Item("Configuration No") = 0
        dr.Item("Configuration Name") = gstrComboZeroIndexString
        dt.Rows.InsertAt(dr, 0)
        gObjMyAppHashTable.Add(EnumHashTableKeyConstants.GetSystemConfigurationList.ToString(), dt)

    End Sub

    ''=====================================================================================
    Public Sub SetVariablesBeforeLogin()
        Try

            ''Set DataDictionay Variables
            SetDataDictionaryVariables()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub SetVariablesAfterLogin()
        Try
            ''seting rounding variables

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub AddUserListToHashTable()

        ' ''Remove existing object from hashtable
        'If gObjMyAppHashTable.ContainsKey(EnumHashTableKeyConstants.GetUserList.ToString()) Then gObjMyAppHashTable.Remove(EnumHashTableKeyConstants.GetUserList.ToString())

        'Dim objDal As New SecurityUserDAL 'CBOCollectionDAL
        'Dim dt As DataTable = objDal.GetUserList ' .GetGLLocationList()
        'Dim dr As DataRow = dt.NewRow
        'dr.Item("User_id") = 0
        'dr.Item("User_name") = gstrComboZeroIndexString
        'dt.Rows.InsertAt(dr, 0)
        'gObjMyAppHashTable.Add(EnumHashTableKeyConstants.GetUserList.ToString(), dt)

    End Sub

    Public Sub AddUserFormListToHashTable()

        ' ''Remove existing object from hashtable
        'If gObjMyAppHashTable.ContainsKey(EnumHashTableKeyConstants.GetUserFormList.ToString()) Then gObjMyAppHashTable.Remove(EnumHashTableKeyConstants.GetUserFormList.ToString())

        'Dim objDal As New SecurityUserDAL
        'Dim dt As DataTable = objDal.GetUserFormRights(gObjUserInfo.UserID)
        'gObjMyAppHashTable.Add(EnumHashTableKeyConstants.GetUserFormList.ToString(), dt)

    End Sub



    ''=====================================================================================
    ''=====================================================================================


    Public Function CanUserViewThisForm(ByVal enumFromName As String, ByRef IsForm As Boolean) As Boolean
        'Dim dv As DataView = GetFilterDataFromDataTable(New SecurityUserDAL().GetUserFormRights(gObjUserInfo.UserID), "[Form Name] ='" & enumFromName & "' and [Control Name] ='View' ")
        'If dv.Count > 0 Then
        '    'If dv.Item(0)("Form Category").ToString.Split(">")(0).Trim = "Reports" Then
        '    '    IsForm = False
        '    'Else
        '    '    IsForm = True
        '    'End If
        '    Return True
        'Else
        '    Return False
        'End If
    End Function

    Public Function GetFormSecurityControls(ByVal strFromName As String) As NameValueCollection
        Try

            Dim objControlList As New NameValueCollection
            Dim dt As DataTable = New SecurityUserDAL().GetUserFormRights(LoginUserId)
            dt.TableName = "Rights"
            Dim objDV As DataView = GetFilterDataFromDataTable(dt, "[Form Name] ='" & strFromName & "'")
            If objDV.Count > 0 Then


                For intRowIndex As Integer = 0 To (objDV.Count - 1)
                    objControlList.Add(objDV.Item(intRowIndex).Item("Control Name").ToString, objDV.Item(intRowIndex).Item("Control Caption").ToString)
                Next
                Return objControlList
            Else
                Return objControlList
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ApplyStyleSheet2(ByVal objCtrl As Control, ByVal FormID As String)
        Try

            Dim dv As DataView
            Dim ctl As Control
            'ctl.RightToLeft = gblnIsRightToLeft

            If TypeOf objCtrl Is Form Then
                Dim frm As Form = CType(objCtrl, Form)

                If gEnumIsRightToLeft = RightToLeft.Yes Then
                    frm.RightToLeftLayout = True
                End If
                frm.RightToLeft = gEnumIsRightToLeft

                dv = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), "[Control Type] = 'Form' AND [Control Name] = 'Title' AND [Form Name] = '" & FormID.ToString() & "'")
                If Not dv Is Nothing Then
                    If Not dv.Count = 0 Then
                        frm.Text = dv.Item(0).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName)
                    End If
                End If

            End If

            For Each ctl In objCtrl.Controls

                If ctl.HasChildren Then

                    If TypeOf ctl Is System.Windows.Forms.TabControl Then
                        Dim tb As System.Windows.Forms.TabControl = CType(ctl, System.Windows.Forms.TabControl)
                        tb.Font = gobjDefaultFontSettingForTabs ''New Font(tb.Font.FontFamily, 10, FontStyle.Bold)

                        If gEnumIsRightToLeft = RightToLeft.Yes Then
                            tb.RightToLeft = RightToLeft.Yes
                            tb.RightToLeftLayout = True
                        End If
                        dv = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), "[Form Name] = '" & FormID.ToString & "' AND [Control Type] = 'Tab' AND [Control Name] like '" & tb.Name & "%'")
                        If Not dv Is Nothing Then
                            If dv.Count > 0 Then
                                For rowIndex As Integer = 0 To dv.Count - 1
                                    For Each pg As System.Windows.Forms.TabPage In tb.TabPages
                                        Dim strTabPageName() As String = dv.Item(rowIndex).Item(dv.Table.Columns("Control Name").ColumnName).Split(".")
                                        If pg.Name = strTabPageName(1) Then
                                            pg.Text = dv.Item(rowIndex).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName)
                                            Exit For
                                        Else
                                            ' pg.Text = "tab"
                                        End If
                                    Next
                                Next
                            End If
                        End If
                    ElseIf TypeOf ctl Is uiCtrlGridBar Then
                        Dim gbar As uiCtrlGridBar = CType(ctl, uiCtrlGridBar)

                        ''dv = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), "[Control Type] = 'DataDictionary' AND [Control Name] like 'lblGridTtile'")
                        ''If Not dv Is Nothing Then
                        ''    If Not dv.Count = 0 Then
                        ''        gbar.lblGridTitle.Text = dv.Item(0).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName)
                        ''    Else
                        ''        'gbar.lblGridTitle.Text = "gbar"
                        ''    End If
                        ''End If
                        ''If gEnumIsRightToLeft = RightToLeft.Yes Then
                        ''    gbar.Left = (gbar.Parent.Size.Width - (gbar.Left + gbar.Width))
                        ''    gbar.RightToLeft = RightToLeft.Yes
                        ''End If
                    ElseIf TypeOf ctl Is SplitContainer Then
                        Dim ctlAc As SplitContainer = CType(ctl, SplitContainer)
                        ctlAc.Font = gobjDefaultFontSettingForInput
                        If gEnumIsRightToLeft = RightToLeft.Yes Then
                            ctlAc.RightToLeft = RightToLeft.Yes
                            ctlAc.Left = (ctlAc.Parent.Size.Width - (ctlAc.Left + ctlAc.Width))
                        End If

                    ElseIf TypeOf ctl Is FlowLayoutPanel Then
                        If gEnumIsRightToLeft = RightToLeft.Yes Then
                            ctl.Left = (ctl.Parent.Size.Width - (ctl.Left + ctl.Width))
                            ctl.RightToLeft = RightToLeft.Yes
                        End If


                    ElseIf TypeOf ctl Is PictureBox Then

                        If gEnumIsRightToLeft = RightToLeft.Yes Then
                            ctl.RightToLeft = RightToLeft.Yes
                            ctl.Left = (ctl.Parent.Size.Width - (ctl.Left + ctl.Width))
                        End If

                    ElseIf TypeOf ctl Is GroupBox Then
                        ctl.Font = gobjDefaultFontSettingForInput
                        If gEnumIsRightToLeft = RightToLeft.Yes Then
                            ctl.RightToLeft = RightToLeft.Yes
                            ctl.Left = (ctl.Parent.Size.Width - (ctl.Left + ctl.Width))
                        End If

                        dv = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), "[Form Name] = '" & FormID.ToString & "' AND [Control Type] = 'GroupBox' AND [Control Name] = '" & ctl.Name & "'")
                        If Not dv Is Nothing Then
                            If dv.Count > 0 Then
                                ctl.Text = dv.Item(0).Item(dv.Item(0).Row.Table.Columns(gstrSystemLanguage).ToString())
                                'Else
                                '    ctl.Text = "grp"

                            End If

                        End If



                    ElseIf TypeOf ctl Is Panel Then
                        ctl.Font = gobjDefaultFontSettingForInput
                        If gEnumIsRightToLeft = RightToLeft.Yes Then
                            ctl.RightToLeft = RightToLeft.Yes
                            ctl.Left = (ctl.Parent.Size.Width - (ctl.Left + ctl.Width))
                        End If

                    End If

                    ApplyStyleSheet3(ctl, FormID)
                End If
                If TypeOf ctl Is System.Windows.Forms.TextBox Then
                    ctl.Font = gobjDefaultFontSettingForInput
                    If gEnumIsRightToLeft = RightToLeft.Yes Then
                        ctl.Left = (ctl.Parent.Size.Width - (ctl.Left + ctl.Width))
                        ctl.RightToLeft = RightToLeft.Yes

                    End If
                    If Not ctl.Tag Is Nothing Then
                        If ctl.Tag = "IsRequired" Then
                            ctl.BackColor = gobjRequiredFieldtBKColor
                        ElseIf ctl.Tag = "ReadOnly" Then
                            ctl.BackColor = gobjDisabledFieldtBKColor
                            'ctl.Enabled = False
                            CType(ctl, Windows.Forms.TextBox).ReadOnly = True
                            CType(ctl, Windows.Forms.TextBox).TabStop = False
                        End If

                    End If

                ElseIf TypeOf ctl Is System.Windows.Forms.MaskedTextBox Then
                    ctl.Font = gobjDefaultFontSettingForInput
                    If gEnumIsRightToLeft = RightToLeft.Yes Then
                        ctl.Left = (ctl.Parent.Size.Width - (ctl.Left + ctl.Width))
                        ctl.RightToLeft = RightToLeft.Yes

                    End If
                    If Not ctl.Tag Is Nothing Then
                        If ctl.Tag = "IsRequired" Then
                            ctl.BackColor = gobjRequiredFieldtBKColor
                        End If
                    End If

                ElseIf TypeOf ctl Is System.Windows.Forms.Label Then
                    Dim clbl As System.Windows.Forms.Label = CType(ctl, System.Windows.Forms.Label)
                    clbl.Font = gobjDefaultFontSettingForLables

                    If gEnumIsRightToLeft = RightToLeft.Yes Then
                        clbl.Left = (clbl.Parent.Size.Width - (clbl.Left + clbl.Width))
                        clbl.RightToLeft = RightToLeft.Yes
                    End If
                    dv = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), "[Form Name] = '" & FormID.ToString & "' AND [Control Type] = 'Label' AND [Control Name] = '" & clbl.Name & "'")
                    If Not dv Is Nothing Then
                        If dv.Count > 0 Then
                            clbl.Text = dv.Item(0).Item(dv.Item(0).Row.Table.Columns(gstrSystemLanguage).ToString())

                        Else
                            'clbl.Text = "lbl"
                        End If
                    Else
                        'clbl.Text = "lbl"

                    End If

                ElseIf TypeOf ctl Is System.Windows.Forms.ComboBox Then

                    Dim combo As System.Windows.Forms.ComboBox = CType(ctl, System.Windows.Forms.ComboBox)
                    combo.Font = gobjDefaultFontSettingForInput

                    combo.RightToLeft = gEnumIsRightToLeft
                    If gEnumIsRightToLeft = RightToLeft.Yes Then
                        combo.Left = (combo.Parent.Size.Width - (combo.Left + combo.Width))
                        combo.RightToLeft = RightToLeft.Yes
                    End If

                    If Not combo.Tag Is Nothing Then
                        If combo.Tag = "IsRequired" Then
                            combo.BackColor = gobjRequiredFieldtBKColor
                        End If
                    End If

                ElseIf TypeOf ctl Is Janus.Windows.GridEX.GridEX Then

                    Dim grd As Janus.Windows.GridEX.GridEX = CType(ctl, Janus.Windows.GridEX.GridEX)

                    grd.TotalRowFormatStyle.BackColor = gobjRequiredFieldtBKColor
                    grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
                    grd.AlternatingColors = True
                    grd.RecordNavigator = True
                    grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003

                    If gEnumIsRightToLeft = RightToLeft.Yes Then
                        grd.Left = (grd.Parent.Size.Width - (grd.Left + grd.Width))
                        grd.RightToLeft = RightToLeft.Yes
                    End If


                ElseIf TypeOf ctl Is System.Windows.Forms.Button Then

                    Dim btn As System.Windows.Forms.Button = CType(ctl, System.Windows.Forms.Button)
                    If Not TypeOf btn.Parent Is uiCtrlGridBar Then btn.Font = gobjDefaultFontSettingForLables

                    dv = GetFilterDataFromDataTable(CType(gObjMyAppHashTable(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), "([Form Name] IN ( '" & FormID.ToString & "') OR [Form ID] = " & EnumProjectForms.ForAllForms & ")  AND [Control Name] = '" & btn.Name & "'")
                    If Not dv Is Nothing Then
                        If dv.Count > 0 Then
                            If btn.Tag Is Nothing Or btn.Tag = String.Empty Then
                                btn.Text = dv.Item(0).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString
                                'Else
                                '    btn.Text = "btn"
                            End If
                            gObjToolTip.AutoPopDelay = 5000
                            gObjToolTip.InitialDelay = 1000
                            gObjToolTip.ReshowDelay = 500
                            gObjToolTip.ShowAlways = True
                            gObjToolTip.SetToolTip(btn, dv.Item(0).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString)
                        End If
                    End If
                    If gEnumIsRightToLeft = RightToLeft.Yes Then
                        btn.Left = (btn.Parent.Size.Width - (btn.Left + btn.Width))
                        btn.RightToLeft = RightToLeft.Yes
                    End If
                ElseIf TypeOf ctl Is System.Windows.Forms.DateTimePicker Then

                    Dim dtp As System.Windows.Forms.DateTimePicker = CType(ctl, System.Windows.Forms.DateTimePicker)
                    dtp.Font = gobjDefaultFontSettingForInput

                    If gEnumIsRightToLeft = RightToLeft.Yes Then
                        dtp.Left = (dtp.Parent.Size.Width - (dtp.Left + dtp.Width))
                        dtp.RightToLeft = RightToLeft.Yes
                        dtp.RightToLeftLayout = True
                    End If

                    If Not dtp.Tag Is Nothing Then
                        If dtp.Tag = "IsRequired" Then
                            dtp.CalendarMonthBackground = gobjRequiredFieldtBKColor
                        End If
                    End If

                ElseIf TypeOf ctl Is System.Windows.Forms.ListBox Then

                    Dim list As System.Windows.Forms.ListBox = CType(ctl, System.Windows.Forms.ListBox)
                    list.Font = gobjDefaultFontSettingForInput

                    If gEnumIsRightToLeft = RightToLeft.Yes Then
                        list.Left = (list.Parent.Size.Width - (list.Left + list.Width))
                        list.RightToLeft = RightToLeft.Yes
                    End If
                    If Not list.Tag Is Nothing Then
                        If list.Tag = "IsRequired" Then
                            list.BackColor = gobjRequiredFieldtBKColor
                        End If
                    End If

                ElseIf TypeOf ctl Is CheckBox Then
                    Dim chk As CheckBox = CType(ctl, CheckBox)
                    chk.Font = gobjDefaultFontSettingForLables
                    If gEnumIsRightToLeft = RightToLeft.Yes Then
                        chk.Left = (chk.Parent.Size.Width - (chk.Left + chk.Width))
                        chk.RightToLeft = RightToLeft.Yes

                    End If
                    If Not chk.Tag Is Nothing Then
                        If chk.Tag = "IsRequired" Then
                            chk.BackColor = gobjRequiredFieldtBKColor
                        End If
                    End If

                    dv = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), "[Form Name] = '" & FormID.ToString & "' AND [Control Type] = 'CheckBox' AND [Control Name] = '" & chk.Name & "'")
                    If Not dv Is Nothing Then
                        If dv.Count > 0 Then
                            chk.Text = dv.Item(0).Item(dv.Item(0).Row.Table.Columns(gstrSystemLanguage).ToString())
                        Else
                            'chk.Text = "chk"

                        End If

                    End If


                ElseIf TypeOf ctl Is PictureBox Then
                    Dim chk As PictureBox = CType(ctl, PictureBox)

                    If gEnumIsRightToLeft = RightToLeft.Yes Then
                        chk.Left = (chk.Parent.Size.Width - (chk.Left + chk.Width))
                        chk.RightToLeft = RightToLeft.Yes

                    End If

                ElseIf TypeOf ctl Is RadioButton Then
                    Dim opt As RadioButton = CType(ctl, RadioButton)
                    opt.Font = gobjDefaultFontSettingForLables
                    ''opt.Cursor = Cursors.Cross
                    If gEnumIsRightToLeft = RightToLeft.Yes Then
                        opt.Left = (opt.Parent.Size.Width - (opt.Left + opt.Width))
                        opt.RightToLeft = RightToLeft.Yes

                    End If

                    dv = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), "[Form Name] = '" & FormID.ToString & "' AND [Control Type] = 'RadioButton' AND [Control Name] = '" & opt.Name & "'")
                    If Not dv Is Nothing Then
                        If dv.Count > 0 Then
                            opt.Text = dv.Item(0).Item(dv.Item(0).Row.Table.Columns(gstrSystemLanguage).ToString())
                        Else
                            'opt.Text = "opt"

                        End If

                    End If

                ElseIf TypeOf ctl Is MenuStrip Then

                    Dim objMenuStrip As MenuStrip
                    objMenuStrip = ctl

                    'Get the Sub Menu Items   
                    For Each objMenuItem As ToolStripMenuItem In objMenuStrip.Items

                        objMenuItem.Font = gobjDefaultFontSettingForMenu ''gobjDefaultFontSettingForTabs
                        dv = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), "[Form ID] = " & EnumProjectForms.ForAllForms & " AND [Control Type] = 'Menu'  AND [Control Name] = '" & objMenuItem.Name & "' ")

                        If Not dv Is Nothing Then
                            If dv.Count > 0 Then
                                If objMenuItem.Name = dv.Item(0).Item(dv.Table.Columns("Control Name").ColumnName) Then
                                    objMenuItem.Text = dv.Item(0).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString
                                Else
                                    'objMenuItem.Text = "mnu"
                                End If
                            End If
                        End If
                        ' Call SetStripMenuDropdown(objMenuItem, FormID.ToString)
                    Next

                    ''following code is written to generate language script file for all menus. (Muahmmad Khalid)
                    ''a line of code will also be un commented from SetStripMenuDropdown function
                    'Dim objWriter As StreamWriter
                    'If Not System.IO.File.Exists("\LangScript\Menu.txt") Then

                    '    objWriter = New StreamWriter(Application.StartupPath & "\LangScript\Menu.txt")
                    'Else
                    '    objWriter.Write("")
                    'End If



                    'objWriter.Write(strMenuLangSQL.ToString)
                    'objWriter.AutoFlush = True
                    'strMenuLangSQL = Nothing
                    'objWriter.Close()
                    ''------------------------------------------------

                    ' ''ElseIf TypeOf ctl Is uiCtrlProductCodeInput Then

                    ' ''    If gEnumIsRightToLeft = RightToLeft.Yes Then
                    ' ''        ctl.Left = (ctl.Parent.Size.Width - (ctl.Left + ctl.Width))

                    ' ''    End If
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplyReportsStyleSheet(ByVal objCtrl As Control, ByVal FormID As String)
        Try

            Dim dv As DataView
            Dim ctl As Control
            'ctl.RightToLeft = gblnIsRightToLeft

            If TypeOf objCtrl Is Form Then
                Dim frm As Form = CType(objCtrl, Form)

                If gEnumIsRightToLeft = RightToLeft.Yes Then
                    frm.RightToLeftLayout = True
                End If
                frm.Icon = New Icon(frm.GetType(), Application.ExecutablePath.ToString & "\Images\Candela2".ToString)


                frm.RightToLeft = gEnumIsRightToLeft
                frm.BackColor = System.Drawing.Color.FromArgb(255, 201, 222, 250)
                dv = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), "[Control Type] = 'Form' AND [Control Name] = 'Title' AND [Form Name] = '" & FormID.ToString() & "'")
                If Not dv Is Nothing Then
                    If Not dv.Count = 0 Then
                        frm.Text = dv.Item(0).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName)
                    End If
                End If

            End If

            For Each ctl In objCtrl.Controls

                If ctl.HasChildren Then

                    If TypeOf ctl Is System.Windows.Forms.TabControl Then
                        Dim tb As System.Windows.Forms.TabControl = CType(ctl, System.Windows.Forms.TabControl)
                        tb.Font = gobjDefaultFontSettingForTabs ''New Font(tb.Font.FontFamily, 10, FontStyle.Bold)
                        tb.DrawMode = TabDrawMode.OwnerDrawFixed
                        AddHandler tb.DrawItem, AddressOf TabPage_DrawItem

                        tb.SizeMode = TabSizeMode.Normal
                        If gEnumIsRightToLeft = RightToLeft.Yes Then
                            tb.RightToLeft = RightToLeft.Yes
                            tb.RightToLeftLayout = True
                        End If
                        dv = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), "[Form Name] = '" & FormID.ToString & "' AND [Control Type] = 'Tab' AND [Control Name] like '" & tb.Name & "%'")
                        If Not dv Is Nothing Then
                            If dv.Count > 0 Then
                                For rowIndex As Integer = 0 To dv.Count - 1
                                    For Each pg As System.Windows.Forms.TabPage In tb.TabPages
                                        Dim strTabPageName() As String = dv.Item(rowIndex).Item(dv.Table.Columns("Control Name").ColumnName).Split(".")
                                        If pg.Name = strTabPageName(1) Then
                                            pg.BackColor = System.Drawing.Color.FromArgb(255, 201, 222, 250)
                                            pg.Text = dv.Item(rowIndex).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName)
                                            Exit For
                                        Else
                                            'pg.Text = "tab"
                                        End If
                                    Next
                                Next
                            End If
                        End If
                    ElseIf TypeOf ctl Is uiCtrlGridBar Then
                        Dim gbar As uiCtrlGridBar = CType(ctl, uiCtrlGridBar)

                        ''dv = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), "[Control Type] = 'DataDictionary' AND [Control Name] like 'lblGridTtile'")
                        ''If Not dv Is Nothing Then
                        ''    If Not dv.Count = 0 Then
                        ''        gbar.lblGridTitle.Text = dv.Item(0).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName)
                        ''    Else
                        ''        'gbar.lblGridTitle.Text = "gbar"
                        ''    End If
                        ''End If
                        ''If gEnumIsRightToLeft = RightToLeft.Yes Then
                        ''    gbar.Left = (gbar.Parent.Size.Width - (gbar.Left + gbar.Width))
                        ''    gbar.RightToLeft = RightToLeft.Yes
                        ''End If



                    ElseIf TypeOf ctl Is SplitContainer Then
                        Dim ctlAc As SplitContainer = CType(ctl, SplitContainer)
                        ctlAc.Font = gobjDefaultFontSettingForInput
                        If gEnumIsRightToLeft = RightToLeft.Yes Then
                            ctlAc.RightToLeft = RightToLeft.Yes
                            ctlAc.Left = (ctlAc.Parent.Size.Width - (ctlAc.Left + ctlAc.Width))
                        End If


                    ElseIf TypeOf ctl Is FlowLayoutPanel Then
                        If gEnumIsRightToLeft = RightToLeft.Yes Then
                            ctl.Left = (ctl.Parent.Size.Width - (ctl.Left + ctl.Width))
                            ctl.RightToLeft = RightToLeft.Yes
                        End If


                    ElseIf TypeOf ctl Is PictureBox Then

                        If gEnumIsRightToLeft = RightToLeft.Yes Then
                            ctl.RightToLeft = RightToLeft.Yes
                            ctl.Left = (ctl.Parent.Size.Width - (ctl.Left + ctl.Width))
                        End If

                    ElseIf TypeOf ctl Is GroupBox Then
                        ctl.Font = gobjDefaultFontSettingForInput
                        ctl.BackColor = System.Drawing.Color.FromArgb(255, 201, 222, 250)
                        If gEnumIsRightToLeft = RightToLeft.Yes Then
                            ctl.RightToLeft = RightToLeft.Yes
                            ctl.Left = (ctl.Parent.Size.Width - (ctl.Left + ctl.Width))
                        End If

                        dv = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), "[Form Name] = '" & FormID.ToString & "' AND [Control Type] = 'GroupBox' AND [Control Name] = '" & ctl.Name & "'")
                        If Not dv Is Nothing Then
                            If dv.Count > 0 Then
                                ctl.Text = dv.Item(0).Item(dv.Item(0).Row.Table.Columns(gstrSystemLanguage).ToString())
                                'Else
                                '    ctl.Text = "grp"
                            End If

                        End If



                    ElseIf TypeOf ctl Is Panel Then
                        ctl.Font = gobjDefaultFontSettingForInput
                        ctl.BackColor = System.Drawing.Color.FromArgb(255, 201, 222, 250)
                        If gEnumIsRightToLeft = RightToLeft.Yes Then
                            ctl.RightToLeft = RightToLeft.Yes
                            ctl.Left = (ctl.Parent.Size.Width - (ctl.Left + ctl.Width))
                        End If

                    End If

                    ApplyReportsStyleSheet(ctl, FormID)
                End If
                If TypeOf ctl Is System.Windows.Forms.TextBox Then
                    ctl.Font = gobjDefaultFontSettingForInput
                    If gEnumIsRightToLeft = RightToLeft.Yes Then
                        ctl.Left = (ctl.Parent.Size.Width - (ctl.Left + ctl.Width))
                        ctl.RightToLeft = RightToLeft.Yes

                    End If
                    If Not ctl.Tag Is Nothing Then
                        If ctl.Tag = "IsRequired" Then
                            ctl.BackColor = gobjRequiredFieldtBKColor
                        ElseIf ctl.Tag = "ReadOnly" Then
                            ctl.BackColor = gobjDisabledFieldtBKColor
                            'ctl.Enabled = False
                            CType(ctl, Windows.Forms.TextBox).ReadOnly = True
                            CType(ctl, Windows.Forms.TextBox).TabStop = False
                        End If

                    End If

                ElseIf TypeOf ctl Is System.Windows.Forms.MaskedTextBox Then
                    ctl.Font = gobjDefaultFontSettingForInput
                    If gEnumIsRightToLeft = RightToLeft.Yes Then
                        ctl.Left = (ctl.Parent.Size.Width - (ctl.Left + ctl.Width))
                        ctl.RightToLeft = RightToLeft.Yes

                    End If
                    If Not ctl.Tag Is Nothing Then
                        If ctl.Tag = "IsRequired" Then
                            ctl.BackColor = gobjRequiredFieldtBKColor
                        End If
                    End If

                ElseIf TypeOf ctl Is System.Windows.Forms.Label Then
                    Dim clbl As System.Windows.Forms.Label = CType(ctl, System.Windows.Forms.Label)
                    'clbl.Font = gobjDefaultFontSettingForLables

                    If gEnumIsRightToLeft = RightToLeft.Yes Then
                        clbl.Left = (clbl.Parent.Size.Width - (clbl.Left + clbl.Width))
                        clbl.RightToLeft = RightToLeft.Yes
                    End If
                    dv = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), "[Form Name] = '" & FormID.ToString & "' AND [Control Type] = 'Label' AND [Control Name] = '" & clbl.Name & "'")
                    If Not dv Is Nothing Then
                        If dv.Count > 0 Then
                            clbl.Text = dv.Item(0).Item(dv.Item(0).Row.Table.Columns(gstrSystemLanguage).ToString())

                        Else
                            ' clbl.Text = "lbl"
                        End If
                    Else
                        ' clbl.Text = "lbl"

                    End If

                ElseIf TypeOf ctl Is System.Windows.Forms.ComboBox Then

                    Dim combo As System.Windows.Forms.ComboBox = CType(ctl, System.Windows.Forms.ComboBox)
                    combo.Font = gobjDefaultFontSettingForInput

                    combo.RightToLeft = gEnumIsRightToLeft
                    If gEnumIsRightToLeft = RightToLeft.Yes Then
                        combo.Left = (combo.Parent.Size.Width - (combo.Left + combo.Width))
                        combo.RightToLeft = RightToLeft.Yes
                    End If

                    If Not combo.Tag Is Nothing Then
                        If combo.Tag = "IsRequired" Then
                            combo.BackColor = gobjRequiredFieldtBKColor
                        End If
                    End If

                ElseIf TypeOf ctl Is Janus.Windows.GridEX.GridEX Then

                    Dim grd As Janus.Windows.GridEX.GridEX = CType(ctl, Janus.Windows.GridEX.GridEX)

                    grd.TotalRowFormatStyle.BackColor = gobjRequiredFieldtBKColor
                    grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
                    grd.GroupByBoxVisible = True
                    grd.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
                    grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
                    grd.AlternatingColors = True
                    grd.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.True
                    grd.RecordNavigator = True

                    If gEnumIsRightToLeft = RightToLeft.Yes Then
                        grd.Left = (grd.Parent.Size.Width - (grd.Left + grd.Width))
                        grd.RightToLeft = RightToLeft.Yes
                    End If


                ElseIf TypeOf ctl Is System.Windows.Forms.Button Then

                    Dim btn As System.Windows.Forms.Button = CType(ctl, System.Windows.Forms.Button)
                    If Not TypeOf btn.Parent Is uiCtrlGridBar Then btn.Font = gobjDefaultFontSettingForLables

                    dv = GetFilterDataFromDataTable(CType(gObjMyAppHashTable(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), "([Form Name] IN ( '" & FormID.ToString & "') OR [Form ID] = " & EnumProjectForms.ForAllForms & ")  AND [Control Name] = '" & btn.Name & "'")
                    If Not dv Is Nothing Then
                        If dv.Count > 0 Then
                            If btn.Tag Is Nothing Or btn.Tag = String.Empty Then
                                btn.Text = dv.Item(0).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString
                            End If
                            gObjToolTip.AutoPopDelay = 5000
                            gObjToolTip.InitialDelay = 1000
                            gObjToolTip.ReshowDelay = 500
                            gObjToolTip.ShowAlways = True
                            gObjToolTip.SetToolTip(btn, dv.Item(0).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString)
                        End If
                    End If
                    If gEnumIsRightToLeft = RightToLeft.Yes Then
                        btn.Left = (btn.Parent.Size.Width - (btn.Left + btn.Width))
                        btn.RightToLeft = RightToLeft.Yes
                    End If
                ElseIf TypeOf ctl Is System.Windows.Forms.DateTimePicker Then

                    Dim dtp As System.Windows.Forms.DateTimePicker = CType(ctl, System.Windows.Forms.DateTimePicker)
                    dtp.Font = gobjDefaultFontSettingForInput

                    If gEnumIsRightToLeft = RightToLeft.Yes Then
                        dtp.Left = (dtp.Parent.Size.Width - (dtp.Left + dtp.Width))
                        dtp.RightToLeft = RightToLeft.Yes
                        dtp.RightToLeftLayout = True
                    End If

                    If Not dtp.Tag Is Nothing Then
                        If dtp.Tag = "IsRequired" Then
                            dtp.CalendarMonthBackground = gobjRequiredFieldtBKColor
                        End If
                    End If

                ElseIf TypeOf ctl Is System.Windows.Forms.ListBox Then

                    Dim list As System.Windows.Forms.ListBox = CType(ctl, System.Windows.Forms.ListBox)
                    list.Font = gobjDefaultFontSettingForInput

                    If gEnumIsRightToLeft = RightToLeft.Yes Then
                        list.Left = (list.Parent.Size.Width - (list.Left + list.Width))
                        list.RightToLeft = RightToLeft.Yes
                    End If
                    If Not list.Tag Is Nothing Then
                        If list.Tag = "IsRequired" Then
                            list.BackColor = gobjRequiredFieldtBKColor
                        End If
                    End If

                ElseIf TypeOf ctl Is CheckBox Then
                    Dim chk As CheckBox = CType(ctl, CheckBox)
                    chk.Font = gobjDefaultFontSettingForLables
                    If gEnumIsRightToLeft = RightToLeft.Yes Then
                        chk.Left = (chk.Parent.Size.Width - (chk.Left + chk.Width))
                        chk.RightToLeft = RightToLeft.Yes

                    End If
                    If Not chk.Tag Is Nothing Then
                        If chk.Tag = "IsRequired" Then
                            chk.BackColor = gobjRequiredFieldtBKColor
                        End If
                    End If

                    dv = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), "[Form Name] = '" & FormID.ToString & "' AND [Control Type] = 'CheckBox' AND [Control Name] = '" & chk.Name & "'")
                    If Not dv Is Nothing Then
                        If dv.Count > 0 Then
                            chk.Text = dv.Item(0).Item(dv.Item(0).Row.Table.Columns(gstrSystemLanguage).ToString())
                        Else
                            ' chk.Text = "chk"

                        End If

                    End If


                ElseIf TypeOf ctl Is PictureBox Then
                    Dim chk As PictureBox = CType(ctl, PictureBox)

                    If gEnumIsRightToLeft = RightToLeft.Yes Then
                        chk.Left = (chk.Parent.Size.Width - (chk.Left + chk.Width))
                        chk.RightToLeft = RightToLeft.Yes

                    End If

                ElseIf TypeOf ctl Is RadioButton Then
                    Dim opt As RadioButton = CType(ctl, RadioButton)
                    opt.Font = gobjDefaultFontSettingForLables
                    ''opt.Cursor = Cursors.Cross
                    If gEnumIsRightToLeft = RightToLeft.Yes Then
                        opt.Left = (opt.Parent.Size.Width - (opt.Left + opt.Width))
                        opt.RightToLeft = RightToLeft.Yes

                    End If

                    dv = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), "[Form Name] = '" & FormID.ToString & "' AND [Control Type] = 'RadioButton' AND [Control Name] = '" & opt.Name & "'")
                    If Not dv Is Nothing Then
                        If dv.Count > 0 Then
                            opt.Text = dv.Item(0).Item(dv.Item(0).Row.Table.Columns(gstrSystemLanguage).ToString())
                        Else
                            ' opt.Text = "opt"

                        End If

                    End If

                ElseIf TypeOf ctl Is MenuStrip Then

                    Dim objMenuStrip As MenuStrip
                    objMenuStrip = ctl


                    'Get the Sub Menu Items   
                    For Each objMenuItem As ToolStripMenuItem In objMenuStrip.Items

                        objMenuItem.Font = gobjDefaultFontSettingForMenu ''gobjDefaultFontSettingForTabs
                        dv = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), "[Form ID] = " & EnumProjectForms.ForAllForms & " AND [Control Type] = 'Menu'  AND [Control Name] = '" & objMenuItem.Name & "' ")

                        If Not dv Is Nothing Then
                            If dv.Count > 0 Then
                                If objMenuItem.Name = dv.Item(0).Item(dv.Table.Columns("Control Name").ColumnName) Then
                                    objMenuItem.Text = dv.Item(0).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString
                                Else
                                    ' objMenuItem.Text = "mnu"
                                End If
                            End If
                        End If
                        ' Call SetStripMenuDropdown(objMenuItem, FormID.ToString)
                    Next


                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Public Sub SetStripMenuDropdown(ByVal objMenuItem As ToolStripMenuItem, ByVal formID As String)
    '    Try

    '        Dim dv() As DataRow
    '        Dim dr As DataRow
    '        'For intX As Integer = 0 To objMenuItem.DropDownItems.Count - 1
    '        For Each dditm As Object In objMenuItem.DropDownItems

    '            If TypeOf dditm Is ToolStripDropDownItem Then

    '                dditm.Font = gobjDefaultFontSettingForMenu
    '                ''dditm.Font = gobjDefaultFontSettingForTabs

    '                dv = CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable).Select("[Form ID] = " & EnumProjectForms.ForAllForms & " AND [Control Type] = 'Menu'  AND [Control Name] = '" & dditm.Name & "' ")

    '                If Not dv Is Nothing Then
    '                    If dv.Length > 0 Then
    '                        For Each dr In dv
    '                            If dditm.Name = dr("Control Name") Then
    '                                dditm.Text = dr(gstrSystemLanguage)

    '                            End If
    '                        Next

    '                    End If
    '                End If
    '                ''following line of code is calling a function to wriet menu script file
    '                'Write_Lang_Script(dditm.name, dditm.text)
    '                ''--------------------------

    '                If dditm.HasDropDownItems Then Call SetStripMenuDropdown(dditm, formID)
    '            End If
    '        Next


    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
    'Public Sub SetStripMenuDropdown1(ByVal objMenuItem As ToolStripMenuItem, ByVal formID As String)
    '    Try

    '        Dim dv As DataView
    '        'For intX As Integer = 0 To objMenuItem.DropDownItems.Count - 1
    '        For Each dditm As Object In objMenuItem.DropDownItems

    '            If TypeOf dditm Is ToolStripDropDownItem Then

    '                dditm.Font = gobjDefaultFontSettingForTabs

    '                dv = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), "[Form ID] = " & EnumProjectForms.ForAllForms & " AND [Control Type] = 'Menu'  AND [Control Name] = '" & dditm.Name & "' ")

    '                If Not dv Is Nothing Then
    '                    If dv.Count > 0 Then
    '                        If dditm.Name = dv.Item(0).Item(dv.Table.Columns("Control Name").ColumnName) Then
    '                            dditm.Text = dv.Item(0).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString

    '                        End If
    '                    End If
    '                End If
    '                ''following line of code is calling a function to wriet menu script file
    '                'Write_Lang_Script(dditm.name, dditm.text)
    '                ''--------------------------

    '                If dditm.HasDropDownItems Then Call SetStripMenuDropdown(dditm, formID)
    '            End If
    '        Next


    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    'Private Sub Write_Lang_Script(ByVal ControlName As String, ByVal ControlValue As String)

    '    If strMenuLangSQL Is Nothing Then
    '        strMenuLangSQL = New System.Text.StringBuilder
    '    End If

    '    strMenuLangSQL.AppendLine("If not Exists(SELECT * FROM tblLanguageBasedControls WHERE ControlType='Menu' and ControlName='" & ControlName & "' ) " & vbCrLf & " " _
    '    & " Begin " & vbCrLf & " " _
    '    & " INSERT INTO [dbo].[tblLanguageBasedControls] ([FormID],[ControlType],[ControlName],[ENGL_US],[Urdu_PK],[Arabic_UAE]) " & vbCrLf & " " _
    '    & " VALUES ('9999','Menu','" & ControlName & "','" & ControlValue & "','', '')")

    '    strMenuLangSQL.AppendLine("End")

    '    strMenuLangSQL.AppendLine(vbCrLf)


    'End Sub
    ''=====================================================================================
    ''=====================================================================================

    ''Public Function GetSystemConfigurationValue(ByVal Key As String) As String
    ''    Try
    ''        Dim dv As DataView = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetSystemConfigurationList.ToString), DataTable), "[Configuration Name] = '" & Key & "'")
    ''        If Not dv Is Nothing Then
    ''            If dv.Count > 0 Then
    ''                Dim dr As DataRowView = dv(0)
    ''                Return dr.Item("Configuration Value").ToString()
    ''            End If
    ''        End If
    ''        Return String.Empty
    ''    Catch ex As Exception
    ''        Throw ex
    ''    End Try

    ''End Function

    Public Sub ApplyStyleSheet3(ByVal objCtrl As Control, ByVal FormID As String, Optional ByVal strCallingFrom As String = "Form")
        Try

            Dim ctl As Control
            Dim backBrush As Brush
            'ctl.RightToLeft = gblnIsRightToLeft

            If TypeOf objCtrl Is Form Then

                Dim frm As Form = CType(objCtrl, Form)
                'frm.Icon=
                frm.BackColor = System.Drawing.Color.FromArgb(255, 201, 222, 250)
                'backBrush = New System.Drawing.Drawing2D.LinearGradientBrush(frm.Bounds, Color.FromArgb(18, 95, 199), Color.FromArgb(175, 200, 245), System.Drawing.Drawing2D.LinearGradientMode.Vertical)
                'frm.CreateGraphics.FillRectangle(backBrush, frm.Bounds)



                'If gEnumIsRightToLeft = RightToLeft.Yes Then
                '    frm.RightToLeftLayout = True
                'End If
                'frm.RightToLeft = gEnumIsRightToLeft

                'dv = CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable).Select("[Control Type] = 'Form' AND [Control Name] = 'Title' AND [Form Name] = '" & FormID.ToString() & "'")



                'If Not dv Is Nothing Then
                '    If Not dv.Length = 0 Then
                '        For Each dr In dv
                '            frm.Text = IIf(IsDBNull(dr(gstrSystemLanguage)), "", dr(gstrSystemLanguage))
                '        Next

                '    End If
                'End If

            End If

            For Each ctl In objCtrl.Controls

                If ctl.HasChildren Then

                    If TypeOf ctl Is System.Windows.Forms.TabControl Then

                        Dim tb As System.Windows.Forms.TabControl = CType(ctl, System.Windows.Forms.TabControl)
                        tb.Font = gobjDefaultFontSettingForTabs ''New Font(tb.Font.FontFamily, 10, FontStyle.Bold)
                        ''tb.Appearance = TabAppearance.Buttons

                        'tb.DrawMode = TabDrawMode.OwnerDrawFixed
                        'tb.SizeMode = TabSizeMode.FillToRight
                        'AddHandler tb.DrawItem, AddressOf TabPage_DrawItem


                        If gEnumIsRightToLeft = RightToLeft.Yes Then
                            tb.RightToLeft = RightToLeft.Yes
                            tb.RightToLeftLayout = True
                        End If



                        ' tb.DrawMode = TabDrawMode.OwnerDrawFixed
                        tb.SizeMode = TabSizeMode.FillToRight

                    ElseIf TypeOf ctl Is uiCtrlGridBar Then

                        Dim gbar As uiCtrlGridBar = CType(ctl, uiCtrlGridBar)



                    ElseIf TypeOf ctl Is SplitContainer Then
                        Dim ctlAc As SplitContainer = CType(ctl, SplitContainer)
                        ctlAc.Font = gobjDefaultFontSettingForInput
                        If gEnumIsRightToLeft = RightToLeft.Yes Then
                            ctlAc.RightToLeft = RightToLeft.Yes
                            ctlAc.Left = (ctlAc.Parent.Size.Width - (ctlAc.Left + ctlAc.Width))
                        End If

                        backBrush = New System.Drawing.Drawing2D.LinearGradientBrush(ctl.Bounds, Color.FromArgb(80, 133, 187), Color.FromArgb(53, 106, 160), System.Drawing.Drawing2D.LinearGradientMode.Vertical)
                        ctl.CreateGraphics.FillRectangle(backBrush, ctl.Bounds)

                    ElseIf TypeOf ctl Is SplitterPanel Then
                        Dim ctlAc As SplitterPanel = CType(ctl, SplitterPanel)
                        'If gEnumIsRightToLeft = RightToLeft.Yes Then
                        '    ctlAc.Left = (ctlAc.Parent.Size.Width - (ctlAc.Left + ctl.Width))
                        '    ctlAc.RightToLeft = RightToLeft.Yes
                        'End If

                        backBrush = New System.Drawing.Drawing2D.LinearGradientBrush(ctlAc.Bounds, Color.FromArgb(80, 133, 187), Color.FromArgb(53, 106, 160), System.Drawing.Drawing2D.LinearGradientMode.Vertical)
                        ctlAc.CreateGraphics.FillRectangle(backBrush, ctlAc.Bounds)

                    ElseIf TypeOf ctl Is FlowLayoutPanel Then
                        If gEnumIsRightToLeft = RightToLeft.Yes Then
                            ctl.Left = (ctl.Parent.Size.Width - (ctl.Left + ctl.Width))
                            ctl.RightToLeft = RightToLeft.Yes
                        End If
                        backBrush = New System.Drawing.Drawing2D.LinearGradientBrush(ctl.Bounds, Color.FromArgb(80, 133, 187), Color.FromArgb(53, 106, 160), System.Drawing.Drawing2D.LinearGradientMode.Vertical)
                        ctl.CreateGraphics.FillRectangle(backBrush, ctl.Bounds)


                        'ctl.BackColor = System.Drawing.Color.FromArgb(255, 201, 222, 250)


                    ElseIf TypeOf ctl Is PictureBox Then

                        If gEnumIsRightToLeft = RightToLeft.Yes Then
                            ctl.RightToLeft = RightToLeft.Yes
                            ctl.Left = (ctl.Parent.Size.Width - (ctl.Left + ctl.Width))
                        End If

                    ElseIf TypeOf ctl Is Panel Then
                        ctl.BackColor = System.Drawing.Color.FromArgb(255, 201, 222, 250)

                        If gEnumIsRightToLeft = RightToLeft.Yes Then
                            ctl.RightToLeft = RightToLeft.Yes
                            ctl.Left = (ctl.Parent.Size.Width - (ctl.Left + ctl.Width))
                        End If

                        'CType(ctl, Panel).BorderStyle = BorderStyle.FixedSingle

                    ElseIf TypeOf ctl Is GroupBox Then
                        ctl.Font = gobjDefaultFontSettingForInput
                        ctl.BackColor = System.Drawing.Color.FromArgb(255, 201, 222, 250)
                        'backBrush = New System.Drawing.Drawing2D.LinearGradientBrush(ctl.Bounds, Color.FromArgb(18, 95, 199), Color.FromArgb(175, 200, 245), System.Drawing.Drawing2D.LinearGradientMode.Vertical)
                        'ctl.CreateGraphics.FillRectangle(backBrush, ctl.Bounds)

                        If gEnumIsRightToLeft = RightToLeft.Yes Then
                            ctl.RightToLeft = RightToLeft.Yes
                            ctl.Left = (ctl.Parent.Size.Width - (ctl.Left + ctl.Width))
                        End If

                        'dv = CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable).Select("[Form Name] = '" & FormID.ToString & "' AND [Control Type] = 'GroupBox' AND [Control Name] = '" & ctl.Name & "'")
                        'If Not dv Is Nothing Then
                        '    If dv.Length > 0 Then
                        '        For Each dr In dv
                        '            ctl.Text = IIf(IsDBNull(dr(gstrSystemLanguage)), "", dr(gstrSystemLanguage))
                        '        Next

                        '        'Else
                        '        '    ctl.Text = "grp"

                        '    End If

                        'End If

                        'dr = Nothing
                        'dv = Nothing


                    ElseIf TypeOf ctl Is Panel Then
                        ctl.Font = gobjDefaultFontSettingForInput
                        'backBrush = New System.Drawing.Drawing2D.LinearGradientBrush(ctl.Bounds, Color.FromArgb(18, 95, 199), Color.FromArgb(175, 200, 245), System.Drawing.Drawing2D.LinearGradientMode.Vertical)
                        'ctl.CreateGraphics.FillRectangle(backBrush, ctl.Bounds)
                        If gEnumIsRightToLeft = RightToLeft.Yes Then
                            ctl.RightToLeft = RightToLeft.Yes
                            ctl.Left = (ctl.Parent.Size.Width - (ctl.Left + ctl.Width))
                        End If

                    End If

                    ApplyStyleSheet3(ctl, FormID, IIf(strCallingFrom = "Form", "Form", "Report"))
                End If
                If TypeOf ctl Is System.Windows.Forms.TextBox Then
                    ctl.Font = gobjDefaultFontSettingForInput
                    If gEnumIsRightToLeft = RightToLeft.Yes Then
                        ctl.Left = (ctl.Parent.Size.Width - (ctl.Left + ctl.Width))
                        ctl.RightToLeft = RightToLeft.Yes

                    End If
                    If Not ctl.Tag Is Nothing Then
                        If ctl.Tag = "IsRequired" Then
                            ctl.BackColor = gobjRequiredFieldtBKColor
                        ElseIf ctl.Tag = "ReadOnly" Then
                            ctl.BackColor = gobjDisabledFieldtBKColor
                            'ctl.Enabled = False
                            CType(ctl, Windows.Forms.TextBox).ReadOnly = True
                            CType(ctl, Windows.Forms.TextBox).TabStop = False
                        End If

                    End If


                ElseIf TypeOf ctl Is System.Windows.Forms.MaskedTextBox Then
                    ctl.Font = gobjDefaultFontSettingForInput
                    If gEnumIsRightToLeft = RightToLeft.Yes Then
                        ctl.Left = (ctl.Parent.Size.Width - (ctl.Left + ctl.Width))
                        ctl.RightToLeft = RightToLeft.Yes

                    End If
                    If Not ctl.Tag Is Nothing Then
                        If ctl.Tag = "IsRequired" Then
                            ctl.BackColor = gobjRequiredFieldtBKColor
                        End If
                    End If

                ElseIf TypeOf ctl Is System.Windows.Forms.Label Then
                    Dim clbl As System.Windows.Forms.Label = CType(ctl, System.Windows.Forms.Label)
                    If clbl.AccessibleDescription = String.Empty Then
                        clbl.Font = gobjDefaultFontSettingForLables
                    End If
                    'If gEnumIsRightToLeft = RightToLeft.Yes Then
                    '    clbl.Left = (clbl.Parent.Size.Width - (clbl.Left + clbl.Width))
                    '    clbl.RightToLeft = RightToLeft.Yes
                    'End If
                    'dv = CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable).Select("[Form Name] = '" & FormID.ToString & "' AND [Control Type] = 'Label' AND [Control Name] = '" & clbl.Name & "'")
                    'If Not dv Is Nothing Then
                    '    If dv.Length > 0 Then
                    '        For Each dr In dv
                    '            clbl.Text = IIf(IsDBNull(dr(gstrSystemLanguage)), "", dr(gstrSystemLanguage))
                    '        Next


                    '    Else
                    '        'clbl.Text = "lbl"
                    '    End If
                    'Else
                    '    'clbl.Text = "lbl"

                    'End If
                    'dr = Nothing
                    'dv = Nothing

                ElseIf TypeOf ctl Is System.Windows.Forms.ComboBox Then

                    Dim combo As System.Windows.Forms.ComboBox = CType(ctl, System.Windows.Forms.ComboBox)
                    combo.Font = gobjDefaultFontSettingForInput

                    combo.RightToLeft = gEnumIsRightToLeft
                    If gEnumIsRightToLeft = RightToLeft.Yes Then
                        combo.Left = (combo.Parent.Size.Width - (combo.Left + combo.Width))
                        combo.RightToLeft = RightToLeft.Yes
                    End If

                    If Not combo.Tag Is Nothing Then
                        If combo.Tag = "IsRequired" Then
                            combo.BackColor = gobjRequiredFieldtBKColor
                        End If
                    End If

                ElseIf TypeOf ctl Is Janus.Windows.GridEX.GridEX Then

                    Dim grd As Janus.Windows.GridEX.GridEX = CType(ctl, Janus.Windows.GridEX.GridEX)

                    grd.TotalRowFormatStyle.BackColor = gobjRequiredFieldtBKColor
                    grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
                    grd.AlternatingColors = True
                    grd.RecordNavigator = True
                    grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
                    grd.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(225, 236, 252)

                    If strCallingFrom <> "Form" Then
                        grd.GroupByBoxVisible = True
                        grd.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
                        grd.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.True
                    End If



                    If gEnumIsRightToLeft = RightToLeft.Yes Then
                        grd.Left = (grd.Parent.Size.Width - (grd.Left + grd.Width))
                        grd.RightToLeft = RightToLeft.Yes
                    End If


                ElseIf TypeOf ctl Is System.Windows.Forms.Button Then

                    Dim btn As System.Windows.Forms.Button = CType(ctl, System.Windows.Forms.Button)
                    If Not TypeOf btn.Parent Is uiCtrlGridBar Then
                        btn.Font = gobjDefaultFontSettingForLables
                        backBrush = New System.Drawing.Drawing2D.LinearGradientBrush(btn.Bounds, Color.FromArgb(18, 95, 199), Color.FromArgb(175, 200, 245), System.Drawing.Drawing2D.LinearGradientMode.Vertical)
                        btn.CreateGraphics.FillRectangle(backBrush, btn.Bounds)
                        btn.BackColor = System.Drawing.Color.FromArgb(255, 201, 222, 250)
                        'btn.ForeColor = Color.AliceBlue
                    End If


                    'dv = CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable).Select("([Form Name] IN ( '" & FormID.ToString & "') OR [Form ID] = " & EnumProjectForms.ForAllForms & ")  AND [Control Name] = '" & btn.Name & "'")
                    'If Not dv Is Nothing Then
                    '    If dv.Length > 0 Then
                    '        If btn.Tag Is Nothing Or btn.Tag = String.Empty Then
                    '            For Each dr In dv
                    '                btn.Text = IIf(IsDBNull(dr(gstrSystemLanguage)), "", dr(gstrSystemLanguage))

                    '            Next

                    '            'Else
                    '            '    btn.Text = "btn"
                    '        End If
                    '        gObjToolTip.AutoPopDelay = 5000
                    '        gObjToolTip.InitialDelay = 1000
                    '        gObjToolTip.ReshowDelay = 500
                    '        gObjToolTip.ShowAlways = True
                    '        For Each dr In dv
                    '            gObjToolTip.SetToolTip(btn, IIf(IsDBNull(dr(gstrSystemLanguage)), "", dr(gstrSystemLanguage)))

                    '        Next


                    '    End If
                    'End If
                    'If gEnumIsRightToLeft = RightToLeft.Yes Then
                    '    btn.Left = (btn.Parent.Size.Width - (btn.Left + btn.Width))
                    '    btn.RightToLeft = RightToLeft.Yes


                ElseIf TypeOf ctl Is System.Windows.Forms.DateTimePicker Then

                    Dim dtp As System.Windows.Forms.DateTimePicker = CType(ctl, System.Windows.Forms.DateTimePicker)
                    dtp.Font = gobjDefaultFontSettingForInput

                    If gEnumIsRightToLeft = RightToLeft.Yes Then
                        dtp.Left = (dtp.Parent.Size.Width - (dtp.Left + dtp.Width))
                        dtp.RightToLeft = RightToLeft.Yes
                        dtp.RightToLeftLayout = True
                    End If

                    If Not dtp.Tag Is Nothing Then
                        If dtp.Tag = "IsRequired" Then
                            dtp.CalendarMonthBackground = gobjRequiredFieldtBKColor
                        End If
                    End If

                ElseIf TypeOf ctl Is System.Windows.Forms.ListBox Then

                    Dim list As System.Windows.Forms.ListBox = CType(ctl, System.Windows.Forms.ListBox)
                    list.Font = gobjDefaultFontSettingForInput

                    If gEnumIsRightToLeft = RightToLeft.Yes Then
                        list.Left = (list.Parent.Size.Width - (list.Left + list.Width))
                        list.RightToLeft = RightToLeft.Yes
                    End If
                    If Not list.Tag Is Nothing Then
                        If list.Tag = "IsRequired" Then
                            list.BackColor = gobjRequiredFieldtBKColor
                        End If
                    End If

                ElseIf TypeOf ctl Is CheckBox Then
                    Dim chk As CheckBox = CType(ctl, CheckBox)
                    chk.Font = gobjDefaultFontSettingForLables
                    If gEnumIsRightToLeft = RightToLeft.Yes Then
                        chk.Left = (chk.Parent.Size.Width - (chk.Left + chk.Width))
                        chk.RightToLeft = RightToLeft.Yes

                    End If
                    If Not chk.Tag Is Nothing Then
                        If chk.Tag = "IsRequired" Then
                            chk.BackColor = gobjRequiredFieldtBKColor
                        End If
                    End If

                    'dv = CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable).Select("[Form Name] = '" & FormID.ToString & "' AND [Control Type] = 'CheckBox' AND [Control Name] = '" & chk.Name & "'")
                    'If Not dv Is Nothing Then
                    '    If dv.Length > 0 Then
                    '        For Each dr In dv
                    '            chk.Text = IIf(IsDBNull(dr(gstrSystemLanguage)), "", dr(gstrSystemLanguage))
                    '        Next

                    '    Else
                    '        'chk.Text = "chk"

                    '    End If

                    'End If
                    'dr = Nothing
                    'dv = Nothing


                ElseIf TypeOf ctl Is PictureBox Then
                    Dim chk As PictureBox = CType(ctl, PictureBox)

                    If gEnumIsRightToLeft = RightToLeft.Yes Then
                        chk.Left = (chk.Parent.Size.Width - (chk.Left + chk.Width))
                        chk.RightToLeft = RightToLeft.Yes

                    End If

                ElseIf TypeOf ctl Is RadioButton Then
                    Dim opt As RadioButton = CType(ctl, RadioButton)
                    opt.Font = gobjDefaultFontSettingForLables
                    ''opt.Cursor = Cursors.Cross
                    If gEnumIsRightToLeft = RightToLeft.Yes Then
                        opt.Left = (opt.Parent.Size.Width - (opt.Left + opt.Width))
                        opt.RightToLeft = RightToLeft.Yes

                    End If


                ElseIf TypeOf ctl Is MenuStrip Then

                    Dim objMenuStrip As MenuStrip
                    objMenuStrip = ctl


                    'Get the Sub Menu Items   
                    '   For Each objMenuItem As ToolStripMenuItem In objMenuStrip.Items

                    '  objMenuItem.Font = gobjDefaultFontSettingForMenu ''gobjDefaultFontSettingForTabs

                    'dv = CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable).Select("[Form ID] = " & EnumProjectForms.ForAllForms & " AND [Control Type] = 'Menu'  AND [Control Name] = '" & objMenuItem.Name & "' ")


                    ''following code is written to generate language script file for all menus. (Muahmmad Khalid)
                    ''a line of code will also be un commented from SetStripMenuDropdown function
                    'Dim objWriter As StreamWriter
                    'If Not System.IO.File.Exists("\LangScript\Menu.txt") Then

                    '    objWriter = New StreamWriter(Application.StartupPath & "\LangScript\Menu.txt")
                    'Else
                    '    objWriter.Write("")
                    'End If



                    'objWriter.Write(strMenuLangSQL.ToString)
                    'objWriter.AutoFlush = True
                    'strMenuLangSQL = Nothing
                    'objWriter.Close()
                    ''------------------------------------------------

                    ' ''ElseIf TypeOf ctl Is uiCtrlProductCodeInput Then

                    ' ''    If gEnumIsRightToLeft = RightToLeft.Yes Then
                    ' ''        ctl.Left = (ctl.Parent.Size.Width - (ctl.Left + ctl.Width))

                    ' ''    End If
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Sub ShowInformationMessage(ByVal Message As String)
        Try
            'Dim dv As DataView = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), "[Form ID] = " & EnumProjectForms.ForAllForms & "   AND [Control Type] = 'DataDictionary'  AND [Control Name] = 'MsgBox_OK' ")
            'If Not dv Is Nothing Then
            '    If dv.Count > 0 Then
            '        MessageBoxManager.OK = dv.Item(0).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString
            '        MessageBoxManager.Register()
            '    End If
            'End If
            '    MessageBox.Show(Message, gstrMsgTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)

            frmMain.ShowErrorNotification(Message)

        Catch ex As Exception
            Throw ex
        Finally
            MessageBoxManager.Unregister()
        End Try
    End Sub

    Public Sub ShowErrorMessage(ByVal Message As String)
        Try
            'Dim dv As DataView = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), "[Form ID] = " & EnumProjectForms.ForAllForms & "   AND [Control Type] = 'DataDictionary'  AND [Control Name] = 'MsgBox_OK' ")
            'If Not dv Is Nothing Then
            '    If dv.Count > 0 Then
            '        MessageBoxManager.OK = dv.Item(0).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString
            '        MessageBoxManager.Register()
            '    End If
            'End If

            'MessageBox.Show(Message, gstrMsgTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            ' ''Dim frm As New frmErrorMessage
            ' ''frm.ShowMsg(Message)
            '' ''frm.BringToFront()

            frmMain.ShowErrorNotification(Message, MsgBoxStyle.Critical)

        Catch ex As Exception
            Throw ex
        Finally
            MessageBoxManager.Unregister()
        End Try

    End Sub

    Public Sub ShowValidationMessage(ByVal Message As String)
        Try
            'Dim dv As DataView = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), "[Form ID] = " & EnumProjectForms.ForAllForms & "   AND [Control Type] = 'DataDictionary'  AND [Control Name] = 'MsgBox_OK' ")
            'If Not dv Is Nothing Then
            '    If dv.Count > 0 Then
            '        MessageBoxManager.OK = dv.Item(0).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString
            '        MessageBoxManager.Register()
            '    End If
            'End If
            'MessageBox.Show(Message, gstrMsgTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)

            frmMain.ShowErrorNotification(Message, MsgBoxStyle.Critical)
        Catch ex As Exception
            Throw ex
        Finally
            MessageBoxManager.Unregister()
        End Try
    End Sub


    Public Function ShowConfirmationMessage(ByVal Message As String, ByVal DefaultButton As MessageBoxDefaultButton) As DialogResult
        Try

            'Dim dv As DataView = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), "[Form ID] = " & EnumProjectForms.ForAllForms & " AND  [Control Type] = 'DataDictionary'  AND [Control Name] = 'MsgBox_Yes' ")
            'If Not dv Is Nothing Then
            '    If dv.Count > 0 Then
            '        MessageBoxManager.Yes = dv.Item(0).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString
            '    End If
            'End If

            'dv = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), " [Form ID] = " & EnumProjectForms.ForAllForms & " AND  [Control Type] = 'DataDictionary'  AND [Control Name] = 'MsgBox_No' ")
            'If Not dv Is Nothing Then
            '    If dv.Count > 0 Then
            '        MessageBoxManager.No = dv.Item(0).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString
            '    End If
            'End If
            'MessageBoxManager.Register()
            Return MessageBox.Show(Message, gstrMsgTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, DefaultButton)
        Catch ex As Exception
            Throw ex
        Finally
            MessageBoxManager.Unregister()
        End Try
    End Function

    Public Sub SetMyImageList()

        gobjMyImageListForOperationBar = New System.Windows.Forms.ImageList

        gobjMyImageListForOperationBar.ImageSize = New System.Drawing.Size(32, 32)
        gobjMyImageListForOperationBar.Images.Add("Excel", New System.Drawing.Icon(My.Application.Info.DirectoryPath.ToString & "\Images\Excel.ico"))
        'gobjMyImageListForOperationBar.Images.Add("CSV", New System.Drawing.Icon(My.Application.Info.DirectoryPath.ToString & "\Images\Excel.ico"))
        gobjMyImageListForOperationBar.Images.Add("Print", New System.Drawing.Icon(My.Application.Info.DirectoryPath.ToString & "\Images\print.ico"))
        gobjMyImageListForOperationBar.Images.Add("Cancel", New System.Drawing.Icon(My.Application.Info.DirectoryPath.ToString & "\Images\Cancel.ICO"))
        gobjMyImageListForOperationBar.Images.Add("Save", New System.Drawing.Icon(My.Application.Info.DirectoryPath.ToString & "\Images\Save.ICO"))
        gobjMyImageListForOperationBar.Images.Add("Update", New System.Drawing.Icon(My.Application.Info.DirectoryPath.ToString & "\Images\Update.ico"))
        gobjMyImageListForOperationBar.Images.Add("Delete", New System.Drawing.Icon(My.Application.Info.DirectoryPath.ToString & "\Images\Delete.ico"))
        gobjMyImageListForOperationBar.Images.Add("New", New System.Drawing.Icon(My.Application.Info.DirectoryPath.ToString & "\Images\New.ico"))
        gobjMyImageListForOperationBar.Images.Add("Exit", New System.Drawing.Icon(My.Application.Info.DirectoryPath.ToString & "\Images\Exit.ico"))
        gobjMyImageListForOperationBar.Images.Add("First", New System.Drawing.Icon(My.Application.Info.DirectoryPath.ToString & "\Images\First.ico"))
        gobjMyImageListForOperationBar.Images.Add("Next", New System.Drawing.Icon(My.Application.Info.DirectoryPath.ToString & "\Images\Next.ico"))
        gobjMyImageListForOperationBar.Images.Add("Previous", New System.Drawing.Icon(My.Application.Info.DirectoryPath.ToString & "\Images\Previous.ico"))
        gobjMyImageListForOperationBar.Images.Add("Last", New System.Drawing.Icon(My.Application.Info.DirectoryPath.ToString & "\Images\Last.ico"))
        gobjMyImageListForOperationBar.Images.Add("Help", New System.Drawing.Icon(My.Application.Info.DirectoryPath.ToString & "\Images\Help.ico"))
        gobjMyImageListForOperationBar.Images.Add("Add", New System.Drawing.Icon(My.Application.Info.DirectoryPath.ToString & "\Images\Plus.ico"))
        gobjMyImageListForOperationBar.Images.Add("Eexecute", New System.Drawing.Icon(My.Application.Info.DirectoryPath.ToString & "\Images\execute.ico"))
        gobjMyImageListForOperationBar.Images.Add("Clear", New System.Drawing.Icon(My.Application.Info.DirectoryPath.ToString & "\Images\Clear.ico"))
        gobjMyImageListForOperationBar.Images.Add("OpenPopUP", New System.Drawing.Icon(My.Application.Info.DirectoryPath.ToString & "\Images\OpenPopUP.ico"))
        gobjMyImageListForOperationBar.Images.Add("Excel", New System.Drawing.Icon(My.Application.Info.DirectoryPath.ToString & "\Images\Candela2.ico"))

    End Sub

    Public Sub SetActiveLanguage(ByVal SelectedLang As EnumLanguagConstants)
        '===========================================================================
        '                   FIRST PART - LANGUAGE SWITCHING
        '===========================================================================
        'We'll use the case statement to inspect the sender objects .Tag property
        For Each Lng As InputLanguage In InputLanguage.InstalledInputLanguages

            Select Case SelectedLang

                Case EnumLanguagConstants.URDU_PK              'If the Tag contains the word "THAI"

                    'Loop through all the installed languages on this system

                    'If there exists a language whose DisplayName has got the 
                    'word "THAI" in it
                    If Lng.Culture.Name = ("ur-PK") Then
                        'If Lng.Culture.DisplayName.ToString = _PAK Then

                        'Change current input language to that 
                        InputLanguage.CurrentInputLanguage = Lng


                        InputLanguage.CurrentInputLanguage.Culture.DateTimeFormat = Lng.Culture.DateTimeFormat
                        InputLanguage.CurrentInputLanguage.Culture.NumberFormat = Lng.Culture.NumberFormat
                        InputLanguage.CurrentInputLanguage.Culture.NumberFormat.NativeDigits = Lng.Culture.NumberFormat.NativeDigits

                        gobjDefaultFontSettingForLables = New Font(New FontFamily("Microsoft Sans Serif"), 8, FontStyle.Regular)
                        gobjDefaultFontSettingForInput = New Font(New FontFamily("Microsoft Sans Serif"), 8, FontStyle.Regular)
                        gobjDefaultFontSettingForMenu = New Font(New FontFamily("Microsoft Sans Serif"), 9, FontStyle.Regular)
                        gobjDefaultFontSettingForTabs = New Font(New FontFamily("Microsoft Sans Serif"), 10, FontStyle.Bold)

                        Exit For



                    End If


                Case EnumLanguagConstants.ENGL_US           'If the Tag contains the word "ENGL"


                    'If there exists a language whose DisplayName has got the 
                    'word "ENGL" in it
                    If Lng.Culture.Name = "en-US" Then

                        'Change current input language to that 
                        InputLanguage.CurrentInputLanguage = Lng

                        InputLanguage.CurrentInputLanguage.Culture.DateTimeFormat = Lng.Culture.DateTimeFormat
                        InputLanguage.CurrentInputLanguage.Culture.NumberFormat = Lng.Culture.NumberFormat
                        InputLanguage.CurrentInputLanguage.Culture.NumberFormat.NativeDigits = Lng.Culture.NumberFormat.NativeDigits

                        gobjDefaultFontSettingForLables = New Font(New FontFamily("Microsoft Sans Serif"), 8, FontStyle.Regular)
                        gobjDefaultFontSettingForInput = New Font(New FontFamily("Microsoft Sans Serif"), 8, FontStyle.Regular)
                        gobjDefaultFontSettingForMenu = New Font(New FontFamily("Microsoft Sans Serif"), 9, FontStyle.Regular)
                        gobjDefaultFontSettingForTabs = New Font(New FontFamily("Microsoft Sans Serif"), 10, FontStyle.Bold)

                        Exit For

                    End If

                Case EnumLanguagConstants.ARABIC_UAE           'If the Tag contains the word "ENGL"


                    'If there exists a language whose DisplayName has got the 
                    'word "ENGL" in it
                    If Lng.Culture.Name = "ar-AE" Then

                        'Change current input language to that 
                        InputLanguage.CurrentInputLanguage = Lng

                        InputLanguage.CurrentInputLanguage.Culture.DateTimeFormat = Lng.Culture.DateTimeFormat
                        InputLanguage.CurrentInputLanguage.Culture.NumberFormat = Lng.Culture.NumberFormat
                        InputLanguage.CurrentInputLanguage.Culture.NumberFormat.NativeDigits = Lng.Culture.NumberFormat.NativeDigits

                        gobjDefaultFontSettingForLables = New Font(New FontFamily("Gill Sans MT Condensed"), 8, FontStyle.Regular)
                        gobjDefaultFontSettingForInput = New Font(New FontFamily("Gill Sans MT Condensed"), 8, FontStyle.Regular)
                        gobjDefaultFontSettingForMenu = New Font(New FontFamily("Microsoft Sans Serif"), 9, FontStyle.Regular)
                        gobjDefaultFontSettingForTabs = New Font(New FontFamily("Gill Sans MT Condensed"), 10, FontStyle.Bold)

                        Exit For

                    End If


            End Select
        Next



    End Sub



    Public Sub SetDataDictionaryVariables()
        'Try
        '    Dim strCriteriaDictionaryList As String = " 'ApplicationTitle' , 'MsgSaveConfirmation', 'MsgUpdateConfirmation', 'MsgAfterUpdate'" _
        '                                              & ", 'MsgNameRequired', 'MsgCodeRequired' , 'MsgComboSelectionRequired', 'MsgWrongInput' " _
        '                                              & ", 'MsgDependentRecordExist', 'MsgDelete', 'MsgExit', 'MsgExport', 'MsgRightToViewOption'" _
        '                                              & ", 'MsgValueRequired' ,'MsgDuplicateUserID', 'MsgPasswordMatch', 'MsgDeleteByEndDate', 'gstrComboZeroIndexString', " _
        '                                              & " 'gMsgAccountValidation','gMsgAlterationDetailValidation', 'gMsgAmountCantZero','gMsgAmountValidation','gMsgAssPrdGrdPopulate'"
        '    Dim dv As DataView
        '    dv = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetLanguageBasedControlList.ToString()), DataTable), "[Form ID] = " & EnumProjectForms.ForAllForms & "   AND [Control Name] in (" & strCriteriaDictionaryList & ")")

        '    If Not dv Is Nothing Then
        '        If dv.Count <> 0 Then
        '            For rowIndex As Integer = 0 To dv.Count - 1
        '                Select Case dv.Item(rowIndex).Item(dv.Table.Columns("Control Name").ColumnName)
        '                    Case "ApplicationTitle"
        '                        gstrMsgTitle = dv.Item(rowIndex).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString

        '                    Case "MsgSaveConfirmation"
        '                        gstrMsgSave = dv.Item(rowIndex).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString

        '                    Case "MsgAfterUpdate"
        '                        gstrMsgAfterUpdate = dv.Item(rowIndex).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString

        '                    Case "MsgUpdateConfirmation"
        '                        gstrMsgUpdate = dv.Item(rowIndex).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString

        '                    Case "MsgNameRequired"
        '                        gstrMsgNameRequired = dv.Item(rowIndex).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString

        '                    Case "MsgCodeRequired"
        '                        gstrMsgCodeRequired = dv.Item(rowIndex).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString

        '                    Case "MsgComboSelectionRequired"
        '                        gstrMsgComboSelectionRequired = dv.Item(rowIndex).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString

        '                    Case "MsgWrongInput"
        '                        gstrMsgWrongInput = dv.Item(rowIndex).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString

        '                    Case "MsgDependentRecordExist"
        '                        gstrMsgDependentRecordExist = dv.Item(rowIndex).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString

        '                    Case "MsgDelete"
        '                        gstrMsgDelete = dv.Item(rowIndex).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString

        '                    Case "MsgExit"
        '                        gstrMsgExit = dv.Item(rowIndex).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString

        '                    Case "MsgExport"
        '                        gstrMsgAfterExport = dv.Item(rowIndex).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString

        '                    Case "MsgRightToViewOption"
        '                        gstrMsgRightToViewOption = dv.Item(rowIndex).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString

        '                    Case "MsgValueRequired"
        '                        gstrValueRequired = dv.Item(rowIndex).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString

        '                    Case "MsgDuplicateUserID"
        '                        gstrDuplicateUserID = dv.Item(rowIndex).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString

        '                    Case "MsgDeleteByEndDate"
        '                        gstrMsgDeleteByEndDate = dv.Item(rowIndex).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString

        '                    Case "MsgPasswordMatch"
        '                        gstrMsgPasswordMatch = dv.Item(rowIndex).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString


        '                    Case "gstrComboZeroIndexString"
        '                        gstrComboZeroIndexString = dv.Item(rowIndex).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString

        '                    Case "gMsgAccountValidation"
        '                        gMsgAccountValidation = dv.Item(rowIndex).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString

        '                    Case "gMsgAlterationDetailValidation"

        '                        gMsgAlterationDetailValidation = dv.Item(rowIndex).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString

        '                    Case "gMsgAmountCantZero"
        '                        gMsgAmountCantZero = dv.Item(rowIndex).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString

        '                    Case "gMsgAmountValidation"
        '                        gMsgAmountValidation = dv.Item(rowIndex).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString

        '                    Case "gMsgAssPrdGrdPopulate"
        '                        gMsgAssPrdGrdPopulate = dv.Item(rowIndex).Item(dv.Table.Columns(gstrSystemLanguage).ColumnName).ToString

        '                    Case Else

        '                End Select
        '            Next
        '        End If
        '    End If

        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub



    Public Sub NumericInput(ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal txt As TextBox, Optional ByVal AllowDecimal As Boolean = True)

        Dim KeyAscii As Short
        KeyAscii = Asc(e.KeyChar)

        If AllowDecimal = True Then

            If KeyAscii = 46 And InStr(txt.Text, ".") > 0 Then
                e.Handled = True

                Exit Sub
                ' Now carry out numerals and period check
            ElseIf (KeyAscii < 48 Or KeyAscii > 57) And KeyAscii <> 46 And KeyAscii <> 8 Then
                e.Handled = True

            End If
        Else
            ' Now carry out numerals and period check
            If (KeyAscii < 48 Or KeyAscii > 57) And KeyAscii <> 8 Then
                e.Handled = True

            End If

        End If

    End Sub
    Public Sub Input_ForGridCell(ByVal e As Janus.Windows.GridEX.UpdatingCellEventArgs, ByVal IsNumeric As Boolean, Optional ByVal NoOfDigit As Integer = 5)

        Dim KeyAscii As Short
        KeyAscii = Asc(e.Value)


        If IsNumeric = True Then

            'If (KeyAscii < 48 Or KeyAscii > 57) And KeyAscii <> 8 Then

            '    e.Cancel = True
            '    Exit Sub
            'End If

            If Len(Mid(e.Value, 1, IIf(InStr(e.Value, ".") > 0, InStr(e.Value, "."), Len(e.Value)) - 1)) > NoOfDigit Then

                e.Value = e.InitialValue
            End If

        Else
            'If (KeyAscii < 48 Or KeyAscii > 57) And (KeyAscii < 65 Or KeyAscii > 90) And (KeyAscii < 98 Or KeyAscii > 122) And KeyAscii <> 8 Then
            '    e.Cancel = True
            '    Exit Sub
            'End If

            If Len(e.Value) > NoOfDigit Then
                e.Value = ""
                e.Cancel = True
            End If
        End If



    End Sub
    Public Sub AlphaNumericInput(ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal txt As TextBox)

        Dim KeyAscii As Short
        KeyAscii = Asc(e.KeyChar)


        If (KeyAscii < 48 Or KeyAscii > 57) And (KeyAscii < 65 Or KeyAscii > 90) And (KeyAscii < 98 Or KeyAscii > 122) And KeyAscii <> 8 Then
            e.Handled = True

        End If



    End Sub


    Public Sub TabPage_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs)
        'Dim f As Font
        'Dim backBrush As Brush
        'Dim foreBrush As Brush

        Dim TabPage As TabControl = CType(sender, TabControl)

        'f = New Font(e.Font, FontStyle.Bold)
        ''backBrush = New System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.FromArgb(175, 200, 245), Color.FromArgb(18, 95, 199), System.Drawing.Drawing2D.LinearGradientMode.Vertical)
        'backBrush = New System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.FromArgb(255, 201, 222, 250), Color.FromArgb(255, 201, 222, 250), System.Drawing.Drawing2D.LinearGradientMode.Vertical)
        'foreBrush = Brushes.Black ' Brushes.AliceBlue

        'Dim tabName As String = TabPage.TabPages(e.Index).Text
        'Dim sf As New StringFormat
        'sf.Alignment = StringAlignment.Center

        'Dim grp = TabPage.CreateGraphics


        ''e.Graphics.FillRectangle(backBrush, e.Bounds)
        'e.Graphics.FillRectangle(backBrush, New RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height))
        'Dim r As RectangleF = New RectangleF(e.Bounds.X, e.Bounds.Y + 4, e.Bounds.Width, e.Bounds.Height - 4)
        ' e.Graphics.DrawString( tabName, f, foreBrush, r, sf)


        'sf.Dispose()

        'f.Dispose()
        'backBrush.Dispose()

    End Sub




End Module

