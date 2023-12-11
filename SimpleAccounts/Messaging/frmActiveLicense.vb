Imports System.Net.NetworkInformation
Imports System.Management
Public Class frmActiveLicense
    Dim dtRegistrationData As New DataTable
    Private _LicenseKey As String
    Private _VerifyRegistrationKey As Boolean = False
    Private _VarifySystemId As Boolean = False
    Private _RegistrationId As Integer = 0
    Dim ConfigList As List(Of SBModel.ConfigSystem)
    Dim ConfigTerminals As SBModel.ConfigSystem
    Dim ConfigLocations As SBModel.ConfigSystem
    Dim ConfigProductId As SBModel.ConfigSystem
    Dim ConfigLicenseKey As SBModel.ConfigSystem
    Dim SystemIds As String = String.Empty
    Dim CustomerId As Integer = 0
    Dim CustomerCode As String = String.Empty
    Dim CustomerName As String = String.Empty
    Dim Company_Name As String = String.Empty
    Dim Product_Name As String = String.Empty
    Public LicenseKey As String = String.Empty
    Public IsFormOpenedFromTrial As Boolean = False
    Public LicenseValidate As Boolean = True
    Public IsbtnActiveClicked As Boolean = False
    Public Sub btnActive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActive.Click
        LicenseProgressbar.Visible = True
        Try
            'If LicenseKey.Length > 0 Then
            '    Me.txtLicenseKey.Text = LicenseKey
            '    btnActive_Click(Nothing, Nothing)
            'End If
            IsbtnActiveClicked = True
            If Me.txtLicenseKey.Text = String.Empty Then
                ShowErrorMessage("Please enter license key.")
                Me.txtLicenseKey.Focus()
                Exit Sub
            End If
            LicenseProgressbar.Value = 0
            If CheckInterNet() = True Then
                If Me.BackgroundWorker1.IsBusy Then Exit Sub
                BackgroundWorker1.RunWorkerAsync()
                Do While BackgroundWorker1.IsBusy
                    Application.DoEvents()
                Loop
            End If
            Me.btnClose.Visible = False
            GroupBox1.Visible = False
            Me.lblCustomerName.Text = String.Empty
            Me.lblCustomerCode.Text = String.Empty
            Me.lblCompany.Text = String.Empty
            Me.lblTerminals.Text = String.Empty
            Me.lblLocations.Text = String.Empty
            Me.lblProduct.Text = String.Empty
            Dim Id As Integer = 0
            Dim dr() As DataRow
            pbRegistration.Image = Nothing
            Me.lblProcess.Text = "Check Internet Connection"
            Do Until Me.LicenseProgressbar.Value >= 15
                Me.LicenseProgressbar.Value = Me.LicenseProgressbar.Value + 1
                Application.DoEvents()
                'System.Threading.Thread.Sleep(50)
            Loop
            If Not CheckInterNet() = True Then
                ShowErrorMessage("Error occured while checking internet connection")
                LicenseValidate = False
                Me.txtLicenseKey.Focus()
                Exit Sub
            End If
            Me.lblProcess.Text = "Validate License Key"
            Do Until Me.LicenseProgressbar.Value >= 30
                Me.LicenseProgressbar.Value = Me.LicenseProgressbar.Value + 1
                Application.DoEvents()
                'System.Threading.Thread.Sleep(50)
            Loop

            If dtRegistrationData IsNot Nothing Then
                dr = dtRegistrationData.Select("RegistrationKey='" & Me.txtLicenseKey.Text & "'")
                If dr.Length > 0 Then
                    _VerifyRegistrationKey = True

                    CustomerId = Val(dr(0).ItemArray(8).ToString)
                    CustomerCode = dr(0).ItemArray(2).ToString
                    CustomerName = dr(0).ItemArray(3).ToString
                    Company_Name = dr(0).ItemArray(9).ToString
                    Product_Name = dr(0).ItemArray(7).ToString


                    _RegistrationId = Val(dr(0).ItemArray(0).ToString)
                    Me.txtTerminalsAllowed.Text = (dr(0).ItemArray(4).ToString)
                    Me.txtLocationsAllowed.Text = (dr(0).ItemArray(5).ToString)
                    Me.txtProduct.Text = dr(0).ItemArray(6).ToString

                    Dim macId As String = GetMACAddress()
                    If Not VerifySystemIds(dr(0).ItemArray(0), Convert.ToInt32(dr(0).ItemArray(4).ToString.Replace("terminal", "")), macId) = True Then
                        LicenseValidate = False
                        ShowErrorMessage("Authentication faild")

                        Me.txtLicenseKey.Focus()
                        _VerifyRegistrationKey = False
                        _VarifySystemId = False
                        LicenseValidate = False
                        Exit Sub
                    End If
                Else
                    _VerifyRegistrationKey = False
                    _VarifySystemId = False
                    LicenseValidate = False
                End If
            Else
                _VerifyRegistrationKey = False
                _VarifySystemId = False
                LicenseValidate = False
            End If

            If Me.txtLicenseKey.Text <> "" Then
                If _VerifyRegistrationKey = True Then
                    pbRegistration.Image = My.Resources._20604_24_button_ok_icon
                ElseIf _VerifyRegistrationKey = False Then
                    pbRegistration.Image = My.Resources.cross_icon
                Else
                    pbRegistration.Image = Nothing
                End If
            Else
                pbRegistration.Image = Nothing
            End If

            Me.lblProcess.Text = "Update License Key"
            Do Until Me.LicenseProgressbar.Value >= 90
                Me.LicenseProgressbar.Value = Me.LicenseProgressbar.Value + 1
                Application.DoEvents()
                'System.Threading.Thread.Sleep(50)
            Loop

            If IsValidate() = True Then
                If New SBDal.ConfigSystemDAL().SaveConfigSys(ConfigList) = True Then
                    Me.lblProcess.Text = "Software Registration Complete Successfully"
                    Do Until Me.LicenseProgressbar.Value >= 100
                        Me.LicenseProgressbar.Value = Me.LicenseProgressbar.Value + 1
                        Application.DoEvents()
                        'System.Threading.Thread.Sleep(50)
                    Loop
                    msg_Information("Software Registration Complete Successfully")
                    '------------------------------------ Show License information --------------------------------------------
                    If Con.State = ConnectionState.Closed Then Con.Open()
                    Dim trans As OleDb.OleDbTransaction = Con.BeginTransaction
                    Dim cmd As New OleDb.OleDbCommand
                    cmd.Connection = trans.Connection
                    cmd.Transaction = trans
                    Try

                        If GetRegistrationDataLocal.Rows.Count > 0 Then
                            cmd.CommandText = "UPDATE tblRegistration_Local  SET RegistrationKey='" & EncryptLicense(Me.txtLicenseKey.Text) & "', " _
                                           & " CustomerId=" & CustomerId & "," _
                                           & " CustomerCode='" & CustomerCode & "', " _
                                           & " CustomerName=N'" & CustomerName.Replace("'", "''") & "', " _
                                           & " Company=N'" & Company_Name.Replace("'", "''") & "', " _
                                           & " Terminals_Allowed='" & EncryptLicense(Me.txtTerminalsAllowed.Text) & "',  " _
                                           & " Locations_Allowed='" & EncryptLicense(Me.txtLocationsAllowed.Text) & "',  " _
                                           & " Product_Name ='" & Product_Name.Replace("'", "''") & "' WHERE RegistrationId=" & GetRegistrationDataLocal.Rows(0).Item("RegistrationId")
                            cmd.ExecuteNonQuery()
                        Else
                            cmd.CommandText = "INSERT INTO tblRegistration_Local (RegistrationKey, CustomerId, CustomerCode, CustomerName, Company, Terminals_Allowed, Locations_Allowed, Product_Name) Values ('" & EncryptLicense(Me.txtLicenseKey.Text) & "', " & CustomerId & ",  '" & CustomerCode & "', '" & CustomerName & "', '" & Company_Name & "', '" & EncryptLicense(Me.txtTerminalsAllowed.Text) & "', '" & EncryptLicense(Me.txtLocationsAllowed.Text) & "', '" & Product_Name & "')"
                            cmd.ExecuteNonQuery()
                        End If
                        trans.Commit()
                    Catch ex As Exception
                        trans.Rollback()
                        Throw ex
                    Finally
                        Con.Close()
                    End Try
                    Me.GroupBox1.Visible = True
                    Dim dtData As DataTable = GetDataTable("Select * from tblRegistration_Local")
                    Me.lblCustomerName.Text = " : " & dtData.Rows(0).Item("CustomerName").ToString
                    Me.lblCustomerCode.Text = " : " & dtData.Rows(0).Item("CustomerCode").ToString
                    Me.lblCompany.Text = " : " & dtData.Rows(0).Item("Company").ToString
                    Me.lblTerminals.Text = " : " & DecryptLicense(dtData.Rows(0).Item("Terminals_Allowed").ToString).ToString.Replace("terminal", "")
                    Me.lblLocations.Text = " : " & DecryptLicense(dtData.Rows(0).Item("Locations_Allowed").ToString).ToString.Replace("location", "")
                    Me.lblProduct.Text = " : " & dtData.Rows(0).Item("Product_Name").ToString
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage("Error occurred while saveing record: " & ex.Message)
            LicenseValidate = False
            SystemIds = String.Empty
        End Try
    End Sub

    Private Sub frmActiveLicense_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If LicenseValidate = False AndAlso IsFormOpenedFromTrial = True Then
            Environment.Exit(0)
        ElseIf IsbtnActiveClicked = False AndAlso IsFormOpenedFromTrial = True Then
            Environment.Exit(0)
        End If
    End Sub

    Private Sub frmActiveLicense_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        Try
            'Dim MacAddress As String = GetMACAddress()
            If LicenseKey.Length > 0 Then
                Me.txtLicenseKey.Text = LicenseKey
                LicenseKey = String.Empty
                'btnActive_Click(Nothing, Nothing)
            Else
                Dim dtData As DataTable = GetDataTable("Select * from tblRegistration_Local")
                If Not dtData.Rows.Count = 0 Then
                    Me.txtLicenseKey.Text = DecryptLicense(dtData.Rows(0).Item("RegistrationKey").ToString)
                    Me.lblCustomerName.Text = " : " & dtData.Rows(0).Item("CustomerName").ToString
                    Me.lblCustomerCode.Text = " : " & dtData.Rows(0).Item("CustomerCode").ToString
                    Me.lblCompany.Text = " : " & dtData.Rows(0).Item("Company").ToString
                    Me.lblTerminals.Text = " : " & DecryptLicense(dtData.Rows(0).Item("Terminals_Allowed").ToString).ToString.Replace("terminal", "")
                    Me.lblLocations.Text = " : " & DecryptLicense(dtData.Rows(0).Item("Locations_Allowed").ToString).ToString.Replace("location", "")
                    Me.lblProduct.Text = " : " & dtData.Rows(0).Item("Product_Name").ToString
                    Me.pbRegistration.Image = My.Resources._20604_24_button_ok_icon
                Else
                    Me.GroupBox1.Visible = False
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Function GetRegistrationData() As DataTable
        Try

            Dim str As String = "SIRIUS_sa.SP_Registration_Info"
            If SIRIUS_Con.State = ConnectionState.Closed Then SIRIUS_Con.Open()
            Dim da As New SqlClient.SqlDataAdapter(str, SIRIUS_Con)
            da.Fill(dtRegistrationData)
            dtRegistrationData.AcceptChanges()
            Return dtRegistrationData

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SIRIUS_Con() As SqlClient.SqlConnection
        Try
            Dim cn As New SqlClient.SqlConnection("Password=Sa_2012_123*1023;Persist Security Info=True;User ID=SIRIUS_net_sa;Initial Catalog=112_softb;Data Source=SIRIUS.net")
            cn.Open()
            Return cn
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            GetRegistrationData()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "")
        Try
            ConfigList = New List(Of SBModel.ConfigSystem)

            ConfigTerminals = New SBModel.ConfigSystem
            ConfigTerminals.Config_Type = "LID1"
            ConfigTerminals.Config_Value = EncryptLicense(Me.txtTerminalsAllowed.Text)
            ConfigList.Add(ConfigTerminals)

            ConfigLocations = New SBModel.ConfigSystem
            ConfigLocations.Config_Type = "LID2"
            ConfigLocations.Config_Value = EncryptLicense(Me.txtLocationsAllowed.Text)
            ConfigList.Add(ConfigLocations)

            ConfigProductId = New SBModel.ConfigSystem
            ConfigProductId.Config_Type = "VersionId"
            ConfigProductId.Config_Value = Me.txtProduct.Text
            ConfigList.Add(ConfigProductId)

            ConfigLicenseKey = New SBModel.ConfigSystem
            ConfigLicenseKey.Config_Type = "LicenseKey"
            ConfigLicenseKey.Config_Value = EncryptLicense(Me.txtLicenseKey.Text)
            ConfigList.Add(ConfigLicenseKey)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate() As Boolean
        Try
            If Not _VerifyRegistrationKey = True Then
                ShowErrorMessage("Please enter valid license key")
                Me.txtLicenseKey.Focus()
                Return False
            End If
            If Not _VarifySystemId = True Then
                ShowErrorMessage("Cannot license active because of failed authentication")
                Me.txtLicenseKey.Focus()
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function CheckInterNet() As Boolean
        Try
            'If My.Computer.Network.IsAvailable = True Then
            '    Return My.Computer.Network.Ping("8.8.8.8")
            'Else
            '    Return False
            'End If
            Dim Req As Net.HttpWebRequest = Net.HttpWebRequest.Create("http://www.siriussolution.com")
            Dim Resp As Net.HttpWebResponse = Req.GetResponse
            If (Resp.StatusCode = Net.HttpStatusCode.OK) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function GetRegistrationDataLocal() As DataTable
        Try
            Dim str As String = "select * From tblRegistration_Local"
            Dim dt As DataTable = GetDataTable(str)
            If dt IsNot Nothing Then
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Public Function VerifySystemIds(ByVal RegistrationId As Integer, ByVal terminals As Integer, Optional macId As String = "") As Boolean
        Try
            Dim strSQL = String.Empty
            Dim str As String = "Select * From SIRIUS_sa.tblRegistrationDetail WHERE RegistrationId=" & RegistrationId
            If SIRIUS_Con.State = ConnectionState.Closed Then SIRIUS_Con.Open()
            Dim da As New SqlClient.SqlDataAdapter(str, SIRIUS_Con)
            Dim dtData As New DataTable
            da.Fill(dtData)
            dtData.AcceptChanges()

            Dim dt As New DataTable
            dt = GetDataTable("Select * From tblSystemList")
            dt.AcceptChanges()

            Dim intSysCount As Integer = dt.Rows.Count
            If intSysCount < terminals Then
                Dim drFoundOnline() As DataRow = dtData.Select("System_Id='" & macId & "'")
                Dim drFoundOfLine() As DataRow = dt.Select("SystemId='" & EncryptLicense(macId) & "' Or SystemName='" & System.Environment.MachineName.ToString & "'")
                If drFoundOnline.Length = 0 Then
                    If SIRIUS_Con.State = ConnectionState.Closed Then SIRIUS_Con.Open()
                    strSQL = String.Empty
                    strSQL = "INSERT INTO SIRIUS_sa.tblRegistrationDetail(RegistrationId, System_Name, System_Id, Active) Values(" & RegistrationId & ", '" & System.Environment.MachineName.ToString & "', '" & macId & "', 1)"
                    Dim cmd As New SqlClient.SqlCommand(strSQL, SIRIUS_Con)
                    cmd.ExecuteNonQuery()
                End If
                If drFoundOfLine.Length = 0 Then
                    Dim cmd1 As New OleDb.OleDbCommand
                    strSQL = String.Empty
                    strSQL = "Insert INTO tblSystemList(SystemName,SystemCode, SystemId) Values('" & System.Environment.MachineName.ToString & "','" & System.Environment.MachineName.ToString & "', '" & EncryptLicense(macId) & "') "
                    If Con.State = ConnectionState.Closed Then Con.Open()
                    cmd1 = New OleDb.OleDbCommand(strSQL, Con)
                    cmd1.ExecuteNonQuery()
                Else
                    Dim cmd1 As New OleDb.OleDbCommand
                    strSQL = String.Empty
                    strSQL = "Update tblSystemList SET SystemName='" & System.Environment.MachineName.ToString & "',SystemCode='" & System.Environment.MachineName.ToString & "', SystemId='" & EncryptLicense(macId) & "' WHERE Id=" & Val(drFoundOfLine(0)(0).ToString) & ""
                    If Con.State = ConnectionState.Closed Then Con.Open()
                    cmd1 = New OleDb.OleDbCommand(strSQL, Con)
                    cmd1.ExecuteNonQuery()
                End If
                _VarifySystemId = True
                Return _VarifySystemId
            Else
                _VarifySystemId = False
                Return _VarifySystemId
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'Public Function VerifySystemIds(ByVal RegistrationId As Integer, ByVal terminals As Integer) As Boolean
    '    Try
    '        _VarifySystemId = False
    '        Dim _VerifyLocalSystemId As Boolean = False
    '        Dim dtSystemData As DataTable = GetDataTable("Select * From tblSystemList")
    '        dtSystemData.AcceptChanges()
    '        Dim systemCount As Integer = dtSystemData.Rows.Count
    '        Dim cmd1 As OleDb.OleDbCommand
    '        Dim nif() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces
    '        If nif(0).GetPhysicalAddress Is Nothing Then
    '            ShowErrorMessage("Your system is not compatible " & vbCrLf & "Please contact SIRIUS Business Solutions " & vbCrLf & "For Help: +92 03-444-114-000")
    '            _VarifySystemId = False
    '            Exit Function
    '        End If
    '        SystemIds = String.Empty
    '        For Each networks As NetworkInterface In nif
    '            If networks.NetworkInterfaceType = NetworkInterfaceType.Ethernet Then
    '                If SystemIds.Length > 1 Then
    '                    SystemIds = SystemIds & ";" & networks.GetPhysicalAddress.ToString
    '                Else
    '                    SystemIds = networks.GetPhysicalAddress.ToString
    '                End If
    '            End If
    '        Next
    '        SystemIds = SystemIds.Replace(";;", ";")
    '        Dim strSQL As String = String.Empty
    '        Dim strSystemIdLoc() As String = {}
    '        For Each dr As DataRow In dtSystemData.Rows
    '            If _VerifyLocalSystemId = True Then Exit For
    '            strSystemIdLoc = dr.Item("SystemId").ToString.Split(";")
    '            If strSystemIdLoc.Length > 0 Then
    '                For Each strSysId As String In strSystemIdLoc
    '                    If strSysId.Length > 1 Then
    '                        If strSysId = nif(0).GetPhysicalAddress.ToString Then
    '                            _VerifyLocalSystemId = True
    '                            Exit For
    '                        End If
    '                    Else
    '                        If dr("SystemName").ToString = System.Environment.MachineName.ToString Then
    '                            If dr("SystemId").ToString.Length = 0 Then
    '                                strSQL = String.Empty
    '                                strSQL = "Update tblSystemList SET SystemId='" & SystemIds & "' WHERE SystemName='" & dr("SystemName") & "'"
    '                                If Con.State = ConnectionState.Closed Then Con.Open()
    '                                cmd1 = New OleDb.OleDbCommand(strSQL, Con)
    '                                cmd1.ExecuteNonQuery()
    '                                _VerifyLocalSystemId = True
    '                                Exit For
    '                            End If
    '                        End If
    '                    End If
    '                Next
    '            End If
    '        Next
    '        If _VerifyLocalSystemId = True Then
    '            Dim str As String = "Select * From SIRIUS_sa.tblRegistrationDetail WHERE RegistrationId=" & RegistrationId
    '            If SIRIUS_Con.State = ConnectionState.Closed Then SIRIUS_Con.Open()
    '            Dim da As New SqlClient.SqlDataAdapter(str, SIRIUS_Con)
    '            Dim dt As New DataTable
    '            da.Fill(dt)
    '            dt.AcceptChanges()
    '            Dim strOnlineSystemId() As String
    '            If dt IsNot Nothing Then
    '                If dt.Rows.Count > 0 Then
    '                    For Each row As DataRow In dt.Rows
    '                        If _VarifySystemId = True Then Exit For
    '                        strOnlineSystemId = row.Item("System_Id").ToString.Split(";")
    '                        If strOnlineSystemId.Length > 0 Then
    '                            For Each strOnlineSystem_Id As String In strOnlineSystemId
    '                                If strOnlineSystem_Id.Length > 1 Then
    '                                    If strOnlineSystem_Id = nif(0).GetPhysicalAddress.ToString Then
    '                                        _VarifySystemId = True
    '                                        Exit For
    '                                    End If
    '                                End If
    '                            Next
    '                        End If
    '                    Next
    '                Else
    '                    If SIRIUS_Con.State = ConnectionState.Closed Then SIRIUS_Con.Open()
    '                    strSQL = String.Empty
    '                    strSQL = "Delete From SIRIUS_sa.tblRegistrationDetail WHERE System_Name=N'" & System.Environment.MachineName.ToString & "' AND RegistrationId=" & RegistrationId & ""
    '                    Dim cmd2 As New SqlClient.SqlCommand(strSQL, SIRIUS_Con)
    '                    cmd2.ExecuteNonQuery()

    '                    If SIRIUS_Con.State = ConnectionState.Closed Then SIRIUS_Con.Open()
    '                    strSQL = "INSERT INTO SIRIUS_sa.tblRegistrationDetail(RegistrationId, System_Name, System_Id, Active) Values(" & RegistrationId & ", '" & System.Environment.MachineName.ToString & "', '" & SystemIds & "', 1)"
    '                    Dim cmd As New SqlClient.SqlCommand(strSQL, SIRIUS_Con)
    '                    cmd.ExecuteNonQuery()

    '                    strSQL = String.Empty
    '                    strSQL = "Delete From tblSystemList where SystemName='" & System.Environment.MachineName.ToString & "'"
    '                    If Con.State = ConnectionState.Closed Then Con.Open()
    '                    cmd1 = New OleDb.OleDbCommand(strSQL, Con)
    '                    cmd1.ExecuteNonQuery()

    '                    strSQL = String.Empty
    '                    strSQL = "Insert INTO tblSystemList(SystemName,SystemCode, SystemId) Values('" & System.Environment.MachineName.ToString & "','" & System.Environment.MachineName.ToString & "', '" & SystemIds & "') "
    '                    If Con.State = ConnectionState.Closed Then Con.Open()
    '                    cmd1 = New OleDb.OleDbCommand(strSQL, Con)
    '                    cmd1.ExecuteNonQuery()
    '                    _VarifySystemId = True

    '                End If
    '            End If
    '        Else
    '            If systemCount < terminals Then

    '                If SIRIUS_Con.State = ConnectionState.Closed Then SIRIUS_Con.Open()
    '                strSQL = String.Empty
    '                strSQL = "Delete From SIRIUS_sa.tblRegistrationDetail WHERE System_Name=N'" & System.Environment.MachineName.ToString & "' AND RegistrationId=" & RegistrationId & ""
    '                Dim cmd2 As New SqlClient.SqlCommand(strSQL, SIRIUS_Con)
    '                cmd2.ExecuteNonQuery()

    '                If SIRIUS_Con.State = ConnectionState.Closed Then SIRIUS_Con.Open()
    '                strSQL = String.Empty
    '                strSQL = "INSERT INTO SIRIUS_sa.tblRegistrationDetail(RegistrationId, System_Name, System_Id, Active) Values(" & RegistrationId & ", '" & System.Environment.MachineName.ToString & "', '" & SystemIds & "', 1)"
    '                Dim cmd As New SqlClient.SqlCommand(strSQL, SIRIUS_Con)
    '                cmd.ExecuteNonQuery()

    '                strSQL = String.Empty
    '                strSQL = "Delete From tblSystemList where SystemName=N'" & System.Environment.MachineName.ToString & "'"
    '                If Con.State = ConnectionState.Closed Then Con.Open()
    '                cmd1 = New OleDb.OleDbCommand(strSQL, Con)
    '                cmd1.ExecuteNonQuery()

    '                strSQL = String.Empty
    '                strSQL = "Insert INTO tblSystemList(SystemName,SystemCode, SystemId) Values('" & System.Environment.MachineName.ToString & "','" & System.Environment.MachineName.ToString & "', '" & SystemIds & "') "
    '                If Con.State = ConnectionState.Closed Then Con.Open()
    '                cmd1 = New OleDb.OleDbCommand(strSQL, Con)
    '                cmd1.ExecuteNonQuery()
    '                _VarifySystemId = True
    '            Else
    '                _VarifySystemId = False
    '            End If
    '        End If
    '        Return _VarifySystemId
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        Con.Close()
    '        SIRIUS_Con.Close()
    '    End Try
    'End Function


    'Public Function getMacAddress() As String
    '    Dim cpuID As String = String.Empty
    '    Dim mc As ManagementClass = New ManagementClass("Win32_NetworkAdapterConfiguration")
    '    Dim moc As ManagementObjectCollection = mc.GetInstances()
    '    For Each mo As ManagementObject In moc
    '        If (cpuID = String.Empty And CBool(mo.Properties("IPEnabled").Value) = True) Then
    '            cpuID = mo.Properties("MacAddress").Value.ToString()
    '        End If
    '    Next
    '    Return cpuID
    'End Function
End Class