Imports System.Net
Imports System.Net.Sockets
Imports System.Net.NetworkInformation
Imports System.Management
Imports System.IO

Public Class frmAboutUsdialog
    Private Sub button1_Click(sender As Object, e As EventArgs) Handles button1.Click

        Try
        Dim slen As String = frmaboutus.lblRegistrationKey.Text

        If Me.txtSerialKey.Text.Contains("License key is missing") Then
            msg_Error("Please enter valid SerialKey")
        ElseIf Me.txtSerialKey.Text.Length <> slen.Length Then
            msg_Error("Please Enter Valid SerialKey")
        Else
            'Dim request As WebRequest = WebRequest.Create(("http://110.36.220.226/api/License/GetInformation?fileType=null&&serialKey=" _
            '+ (txtSerialKey.Text + ("&&macAddress=" _
            '+ (txtSystemId.Text + ("&&motherBoardId=  " _
            '+ (txtMotherBoardId.Text + ("&&biosId= " _
            '+ (txtSystemId.Text + ("&&dbName=" _
            '+ (txtDbName.Text + ("&&systemId=" _
            '+ (txtSystemId.Text + (" &&ipAddress= " + txtIpAddress.Text))))))))))))))

                Dim request As WebRequest = WebRequest.Create(("http://110.36.220.226/api/License/GetInformation?fileType=null&&serialKey=" _
            + (txtSerialKey.Text + ("&&macAddress=" _
            + (txtMacAddress.Text + ("&&motherBoardId=  " _
            + (txtMotherBoardId.Text + ("&&biosId= " _
            + (txtBios.Text + ("&&dbName=" _
            + (txtDbName.Text + (" &&ipAddress= " + txtIpAddress.Text))))))))))))




            request.Credentials = CredentialCache.DefaultCredentials
            
            Dim response As WebResponse = request.GetResponse()
            'MessageBox.Show(CType(response, HttpWebResponse).StatusDescription)
            Dim dataStream As Stream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()



            If responseFromServer.Contains("Your License key is Invalid") Then
                msg_Information("Your License Key Is not valid")
            ElseIf responseFromServer.Contains("Invalid License Agreement") Then
                    msg_Information("Invalid License Agreement")
                ElseIf responseFromServer.Contains("License is Successfully Updated") Then
                    frmSystemConfigurationNew.SaveConfiguration("LicenseKey", Encrypt(responseFromServer))
                    'msg_Information("Finger print generted successfully. You can get file from " & Encrypt(responseFromServer))
                    msg_Information("Your License Successfully updated")
                    Me.Close()
                Else
                    msg_Information(responseFromServer)
                End If
            reader.Close()
            response.Close()
        End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Public Shared Function GetLocalIPAddress() As String
        Dim ip
        Dim host = Dns.GetHostEntry(Dns.GetHostName)
        For Each ip In host.AddressList
            If (ip.AddressFamily = AddressFamily.InterNetwork) Then
                Return ip.ToString
            End If
        Next
        Throw New Exception("No network adapters Connected with an IPv4 address in the system!")
    End Function

    Public Shared Function GetMacAddress() As PhysicalAddress
        'For Each nic As NetworkInterface In NetworkInterface.GetAllNetworkInterfaces
        '    ' Only consider Ethernet and wireless network interfaces
        '    If (((nic.NetworkInterfaceType = NetworkInterfaceType.Wireless80211) _
        '                OrElse (nic.NetworkInterfaceType = NetworkInterfaceType.Ethernet)) _
        '                AndAlso (nic.OperationalStatus = OperationalStatus.Up)) Then
        '        Return nic.GetPhysicalAddress
        '    End If
        'Next
        'Return Nothing
        GetMACAddressList().ToString()
    End Function

    Public Function getMotherBoardID() As String
        'wmic bios get serialnumber
        'Dim serial As String = ""
        ''Try
        'Dim mos As ManagementObjectSearcher = New ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard")
        'Dim moc As ManagementObjectCollection = mos.Get
        'For Each mo As ManagementObject In moc
        '    serial = mo("SerialNumber").ToString
        'Next
        'Return serial
        'Catch  As Exception
        '    Return serial
        'End Try

        GetBIOS.ToString()

    End Function

    Private Sub frmAboutUsdialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtMacAddress.Text = GetMACAddressList.ToString
        txtIpAddress.Text = GetLocalIPAddress.ToString
        txtMotherBoardId.Text = GetMotherboard.ToString
        txtBios.Text = GetBIOS.ToString
        txtSystemName.Text = Environment.MachineName
        txtSerialKey.Text = frmaboutus.lblRegistrationKey.Text
        txtDbName.Text = frmaboutus.lblDatabaseName.Text

        'ConfigurationManager.AppSettings.
    End Sub
End Class