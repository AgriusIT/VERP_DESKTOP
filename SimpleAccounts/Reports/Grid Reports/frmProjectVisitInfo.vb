'25-Jun-2015 Task#125062015 Ahmad Sharif Design the form and written all the logics of form
'14-Jul-2015 Task# 201507020 Ali Ansari Adding Contact Numbers of concerned persons,Customer Link and general lay out improvement
Imports System.Data.OleDb

Public Class frmProjectVisitInfo

    'Property for setting and getting  ProjectCode
    Private _ProjectCode As Integer
    Public Property ProjectCode() As Integer
        Get
            Return _ProjectCode
        End Get
        Set(ByVal value As Integer)
            _ProjectCode = value
        End Set
    End Property

    'KeyDown event for form
    Private Sub frmProjectVisitInfo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then     'on Escape key press, dialog box will close
                Me.Close()      'closing dialog box
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Public Sub New(ByVal code As String)
    '    Try

    '        ProjectCode = code

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub


    Private Sub frmProjectVisitInfo_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        Try
            'calling the function according to the Tag value
            If Me.Tag = "ConName" Then
                FillConInformation(Me.Tag.ToString)
            ElseIf Me.Tag = "BulName" Then
                FillBulInformation(Me.Tag.ToString)
            ElseIf Me.Tag = "ArcName" Then
                FillArcInformation(Me.Tag.ToString)

                'Altered Against  Task# 201507020 call function according to tag for customer name
            ElseIf Me.Tag = "CustName" Then
                FillCustInformation(Me.Tag.ToString)
                'Altered Against  Task# 201507020 call function according to tag for customer name
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub

    'Sub for Displaying Consultant information
    Private Sub FillConInformation(ByVal str As String)

        Try
            Dim objCommand As New OleDbCommand
            Dim objCon As OleDbConnection
            objCon = Con        'passing the global connection object to our local connection object

            'checking if connection is already opened, then close the connection 
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If

            objCon.Open()           'open the connection
            objCommand.Connection = objCon

            Dim reader As OleDbDataReader       'reader object for reading data row by row
            'Marked Against Task# 201507020 getting Contact Numbers in query
            'objCommand.CommandText = "select ConOwner,ConName,ConMainName,ConConName from tblProjectPortfolio Where ProjectCode=" & ProjectCode
            'Marked Against Task# 201507020 getting Contact Numbers in query

            'Altered Against Task# 201507020 getting Contact Numbers in query
            objCommand.CommandText = "select ConName,ConPhNo,ConOwner,ConMBNo,ConMainName,ConMainMbNo,ConConName,ConConMbNo from tblProjectPortfolio Where ProjectCode=" & ProjectCode
            'Altered Against Task# 201507020 getting Contact Numbers in query
            reader = objCommand.ExecuteReader()

            gboxProjectVisitInfo.Text = "Consultant Information"        'setting the group box title
            'Marked Against Task# 201507020 Seting Labels Caption
            'setting the labels titles
            'Me.labelCompanyName.Text = "Company Name :"
            'Me.labelName.Text = "Owner Name :"
            'Me.labelMainConName.Text = "Main Consultant Name :"
            'Me.labelConsConName.Text = "Concerned Consultant Name :"
            ''Marked Against Task# 201507020 Seting Labels Caption


            'Altered Against Task# 201507020 Seting Labels Caption
            Me.labelCompanyName.Text = "Company Name: "
            Me.LblPhNo.Text = "Phone No: "
            Me.labelName.Text = "Owner Name: "
            Me.LblOwnerNo.Text = "Owner Phone:"
            Me.labelMainConName.Text = "Main Consultant Name :"
            Me.LblMainConNo.Text = "Main Consultant Phone No: "
            Me.labelConsConName.Text = "Concerned Consultant Name :"
            Me.LblConConNo.Text = "Concerned Consultant Phone No"
            'Altered Against Task# 201507020 Seting Labels Caption

            'Altered Against  Task# 201507020 to hide irrevalant information
            Me.labelMainConName.Visible = True
            Me.LblMainConNo.Visible = True
            Me.labelConsConName.Visible = True
            Me.LblConConNo.Visible = True
            Me.txtMainConName.Visible = True
            Me.TxtMainConNo.Visible = True
            Me.txtConsConName.Visible = True
            Me.TxtConConNo.Visible = True
            'Altered Against  Task# 201507020 to hide irrevalant information


            


            If reader.HasRows Then      'checking reader object has rows , if has then reading will start
                'reading data row by row using while loop
                While reader.Read()
                    'Marked Against Task# 201507020 Forwading information to Text Boxes
                    'Me.txtName.Text = reader.GetValue(0).ToString
                    'Me.txtCompanyName.Text = reader.GetValue(1).ToString
                    'Me.txtMainConName.Text = reader.GetValue(2).ToString
                    'Me.txtConsConName.Text = reader.GetValue(3).ToString
                    'Marked Against Task# 201507020 Forwading information to Text Boxes
                    'Altered Against Task# 201507020 Forwading information to Text Boxes
                    Me.txtCompanyName.Text = reader.GetValue(0).ToString
                    Me.TxtPhoneNo.Text = reader.GetValue(1).ToString
                    Me.txtName.Text = reader.GetValue(2).ToString
                    Me.TxtOwnerNo.Text = reader.GetValue(3).ToString
                    Me.txtMainConName.Text = reader.GetValue(4).ToString
                    Me.TxtMainConNo.Text = reader.GetValue(5).ToString
                    Me.txtConsConName.Text = reader.GetValue(6).ToString
                    Me.TxtConConNo.Text = reader.GetValue(7).ToString
                    'Altered Against Task# 201507020 Forwading information to Text Boxes

                End While
            End If

            objCon.Close()      'closing the opened connection


        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    'Sub for Displaying Builder information
    Private Sub FillBulInformation(ByVal str As String)

        Try

            Dim objCommand As New OleDbCommand
            Dim objCon As OleDbConnection
            objCon = Con        'passing the global connection object to our local connection object

            'checking if connection is already opened, then close the connection 
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If

            objCon.Open()           'open the connection
            objCommand.Connection = objCon

            Dim reader As OleDbDataReader     'reader object for reading data row by row
            'Marked Against Task# 201507020 getting Contact Numbers in query
            objCommand.CommandText = "select BulName,BulOwner,BulGManager,BulPManager from tblProjectPortfolio Where ProjectCode=" & ProjectCode
            'Marked Against Task# 201507020 getting Contact Numbers in query

            'Altered Against Task# 201507020 getting Contact Numbers in query
            objCommand.CommandText = "select BulName,BulPhNo,BulOwner,BulMbNo,BulGManager,BulGMMbNo,BulPManager,BulPMMbNo from tblProjectPortfolio Where ProjectCode=" & ProjectCode
            'Altered Against Task# 201507020 getting Contact Numbers in query
            reader = objCommand.ExecuteReader()

            gboxProjectVisitInfo.Text = "Builder Information"       'setting the group box title
            'Marked Against Task# 201507020 Seting Labels Caption
            'setting the labels titles
            'Me.labelCompanyName.Text = "Company Name :"
            'Me.labelName.Text = "Owner Name :"
            'Me.labelName.Text = "Owner Name :"
            'Me.labelMainConName.Text = "Project General Manager :"
            'Me.labelConsConName.Text = "Project Manager Name :"
            'Marked Against Task# 201507020 Seting Labels Caption

            'Altered Against Task# 201507020 Seting Labels Caption
            Me.labelCompanyName.Text = "Company Name: "
            Me.LblPhNo.Text = "Phone No: "
            Me.labelName.Text = "Owner Name: "
            Me.LblOwnerNo.Text = "Owner Phone:"
            Me.labelMainConName.Text = "General Manager :"
            Me.LblMainConNo.Text = "General Manager Phone No: "
            Me.labelConsConName.Text = "Project Manager Name :"
            Me.LblConConNo.Text = "Project Manager Phone No"
            'Altered Against Task# 201507020 Seting Labels Caption

            'Altered Against  Task# 201507020 to hide irrevalant information
            Me.labelMainConName.Visible = True
            Me.LblMainConNo.Visible = True
            Me.labelConsConName.Visible = True
            Me.LblConConNo.Visible = True
            Me.txtMainConName.Visible = True
            Me.TxtMainConNo.Visible = True
            Me.txtConsConName.Visible = True
            Me.TxtConConNo.Visible = True
            'Altered Against  Task# 201507020 to hide irrevalant information

            If reader.HasRows Then       'checking reader object has rows , if has then reading will start
                'reading data row by row using while loop
                While reader.Read()
                    'Marked Against Task# 201507020 Forwading information to Text Boxes
                    'Me.txtName.Text = reader.GetValue(0).ToString
                    'Me.txtCompanyName.Text = reader.GetValue(1).ToString
                    'Me.txtMainConName.Text = reader.GetValue(2).ToString
                    'Me.txtConsConName.Text = reader.GetValue(3).ToString
                    'Marked Against Task# 201507020 Forwading information to Text Boxes

                    'Altered Against Task# 201507020 Forwading information to Text Boxes
                    Me.txtCompanyName.Text = reader.GetValue(0).ToString
                    Me.TxtPhoneNo.Text = reader.GetValue(1).ToString
                    Me.txtName.Text = reader.GetValue(2).ToString
                    Me.TxtOwnerNo.Text = reader.GetValue(3).ToString
                    Me.txtMainConName.Text = reader.GetValue(4).ToString
                    Me.TxtMainConNo.Text = reader.GetValue(5).ToString
                    Me.txtConsConName.Text = reader.GetValue(6).ToString
                    Me.TxtConConNo.Text = reader.GetValue(7).ToString
                    'Altered Against Task# 201507020 Forwading information to Text Boxes
                End While
            End If

            objCon.Close()      'closing the opened connection

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    'Sub for Displaying Architect information
    Private Sub FillArcInformation(ByVal str As String)

        Try

            Dim objCommand As New OleDbCommand
            Dim objCon As OleDbConnection
            objCon = Con        'passing the global connection object to our local connection object

            'checking if connection is already opened, then close the connection 
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If

            objCon.Open()           'open the connection
            objCommand.Connection = objCon

            Dim reader As OleDbDataReader    'reader object for reading data row by row
            'Marked Against Task# 201507020 getting Contact Numbers in query
            '            objCommand.CommandText = "select ArcOwner,ArcName,ArcGManager,ArcPManager from tblProjectPortfolio Where ProjectCode=" & ProjectCode
            'Marked Against Task# 201507020 getting Contact Numbers in query

            'Altered Against Task# 201507020 getting Contact Numbers in query
            objCommand.CommandText = "select ArcName,ArcPhNo,ArcOwner,ArcMbNo,ArcGManager,ArcGMMbno,ArcPManager,ArcPMMbNo from tblProjectPortfolio Where ProjectCode=" & ProjectCode
            'Altered Against Task# 201507020 getting Contact Numbers in query
            reader = objCommand.ExecuteReader()

            gboxProjectVisitInfo.Text = "Architect Information"         'setting the group box title

            'Marked Against Task# 201507020 Forwading information to Text Boxes
            'setting the labels titles
            'Me.labelCompanyName.Text = "Company Name :"
            'Me.labelName.Text = "Owner Name :"
            'Me.labelMainConName.Text = "Main Architect Name :"
            'Me.labelConsConName.Text = "Concerned Architect Name :"
            ''Marked Against Task# 201507020 Forwading information to Text Boxes

            'Altered Against Task# 201507020 Seting Labels Caption
            Me.labelCompanyName.Text = "Company Name :"
            Me.LblPhNo.Text = "Phone No: "
            Me.labelName.Text = "Owner Name :"
            Me.LblOwnerNo.Text = "Owner Phone:"
            Me.labelMainConName.Text = "Main Architect Name :"
            Me.LblMainConNo.Text = "Main Architect Phone No: "
            Me.labelConsConName.Text = "Concerned Architect Name :"
            Me.LblConConNo.Text = "Concerned Architect Phone No"
            'Altered Against Task# 201507020 Seting Labels Caption

            'Altered Against  Task# 201507020 to hide irrevalant information
            Me.labelMainConName.Visible = True
            Me.LblMainConNo.Visible = True
            Me.labelConsConName.Visible = True
            Me.LblConConNo.Visible = True
            Me.txtMainConName.Visible = True
            Me.TxtMainConNo.Visible = True
            Me.txtConsConName.Visible = True
            Me.TxtConConNo.Visible = True
            'Altered Against  Task# 201507020 to hide irrevalant information

            
            If reader.HasRows Then       'checking reader object has rows , if has then reading will start
                'reading data row by row using while loop
                While reader.Read()
                    'Marked Against Task# 201507020 Forwading information to Text Boxes
                    'Me.txtName.Text = reader.GetValue(0).ToString
                    'Me.txtCompanyName.Text = reader.GetValue(1).ToString
                    'Me.txtMainConName.Text = reader.GetValue(2).ToString
                    'Me.txtConsConName.Text = reader.GetValue(3).ToString
                    'Marked Against Task# 201507020 Forwading information to Text Boxes

                    'Altered Against Task# 201507020 Forwading information to Text Boxes
                    Me.txtCompanyName.Text = reader.GetValue(0).ToString
                    Me.TxtPhoneNo.Text = reader.GetValue(1).ToString
                    Me.txtName.Text = reader.GetValue(2).ToString
                    Me.TxtOwnerNo.Text = reader.GetValue(3).ToString
                    Me.txtMainConName.Text = reader.GetValue(4).ToString
                    Me.TxtMainConNo.Text = reader.GetValue(5).ToString
                    Me.txtConsConName.Text = reader.GetValue(6).ToString
                    Me.TxtConConNo.Text = reader.GetValue(7).ToString
                    'Altered Against Task# 201507020 Forwading information to Text Boxes
                End While
            End If

            objCon.Close()      'closing the opened connection

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub FillCustInformation(ByVal str As String)

        Try
            Dim objCommand As New OleDbCommand
            Dim objCon As OleDbConnection
            objCon = Con


            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If

            objCon.Open()           'open the connection
            objCommand.Connection = objCon

            Dim reader As OleDbDataReader       'reader object for reading data row by row

            

            objCommand.CommandText = "select CustName,CustoffAdd,CustMob,CustEmail from tblProjectPortfolio Where ProjectCode=" & ProjectCode
            reader = objCommand.ExecuteReader()

            gboxProjectVisitInfo.Text = "Customer Information"        'setting the group box title

            Me.labelCompanyName.Text = "Customer Name: "
            Me.LblPhNo.Text = "Customer Address: "
            Me.labelName.Text = "Customer Phone No: "
            Me.LblOwnerNo.Text = "Customer Email:"

            'Altered Against  Task# 201507020 to hide irrevalant information
            Me.labelMainConName.Visible = False
            Me.LblMainConNo.Visible = False
            Me.labelConsConName.Visible = False
            Me.LblConConNo.Visible = False
            Me.txtMainConName.Visible = False
            Me.TxtMainConNo.Visible = False
            Me.txtConsConName.Visible = False
            Me.TxtConConNo.Visible = False
            'Altered Against  Task# 201507020 to hide irrevalant information
            






            If reader.HasRows Then      'checking reader object has rows , if has then reading will start
                'reading data row by row using while loop
                While reader.Read()
                    'Marked Against Task# 201507020 Forwading information to Text Boxes
                    'Me.txtName.Text = reader.GetValue(0).ToString
                    'Me.txtCompanyName.Text = reader.GetValue(1).ToString
                    'Me.txtMainConName.Text = reader.GetValue(2).ToString
                    'Me.txtConsConName.Text = reader.GetValue(3).ToString
                    'Marked Against Task# 201507020 Forwading information to Text Boxes
                    'Altered Against Task# 201507020 Forwading information to Text Boxes
                    Me.txtCompanyName.Text = reader.GetValue(0).ToString
                    Me.TxtPhoneNo.Text = reader.GetValue(1).ToString
                    Me.txtName.Text = reader.GetValue(2).ToString
                    Me.TxtOwnerNo.Text = reader.GetValue(3).ToString
                    'Altered Against Task# 201507020 Forwading information to Text Boxes

                End While
            End If

            objCon.Close()      'closing the opened connection


        Catch ex As Exception
            Throw ex
        End Try

    End Sub

End Class