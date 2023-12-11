Imports System.Windows.Forms.DataVisualization.Charting
Imports SBDal
Imports SBModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Data.OleDb
Public Class OpportunityQuartelyChart
    Dim UserId As Integer = 0
    Private Sub OpportunityQuartelyChart_Load(sender As Object, e As EventArgs) Handles Me.Load
        ComboBox1.Text = "2019"
        LoadData()
        'GetSecurityRights()
    End Sub

    Private Sub LoadData()
        Chart1.DataSource = GetData()
        Chart1.Series("Stages").Points.Clear()
        Chart1.Series("Stages").XValueMember = "Total"
        Chart1.Series("Stages").YValueMembers = "Year"
        '  Chart1.Series("Stages").LabelFormat = "{#VALX,##0}"
        ' Chart1.Series("Stages").LegendText = "#VALX  #TotalAmount"
        'Chart1.Series("Stages").Label = "#VALY  #AXISLABEL"
        'Chart1.Series("Stages")("PieLabelStyle") = "outside"
    End Sub
    Private Function GetData() As DataTable
        Dim objCon As New OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Open Then objCon.Close()
        objCon.Open()
        Try
            Dim dtchart As New DataTable
            GetUser(LoginUserId)
            Dim LoginGroupId As Integer = 10
            Dim year As String = ComboBox1.Text
            ' Dim cmd As New OleDbCommand("OpportunityChart" & UserId & "", objCon)
            Dim cmd As New OleDbCommand("OpportunityquarterlyChart", objCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@Year", ComboBox1.Text)
            cmd.Parameters.Add("@UserId", OleDbType.Integer).Value = LoginUserId
            cmd.Parameters.Add("@LoginGroupId", OleDbType.Integer).Value = LoginGroupId
            Dim dr As OleDbDataReader = cmd.ExecuteReader
            dtchart.Load(dr)
            Return dtchart
        Catch ex As Exception
            Throw ex

        Finally
            objCon.Close()
        End Try

    End Function

    'Try
    '    'Rafay:Modified query to add country name
    '    Dim strSQL As String = String.Empty
    '    Dim year As String = ComboBox1.SelectedItem
    '    If LoginUser.LoginUserGroup = "Administrator" Then
    '        strSQL = "SELECT DATEPART(YEAR,CloseDate)  [Year], " _
    '        & " concat('Q',DATEPART(QQ,CloseDate), '=' , CAST(sum(tblDefOpportunity.TotalAmount) as nvarchar(100))) as Total " _
    '        & "  FROM tblDefOpportunity " _
    '        & " WHERE YEAR(CloseDate) = " & year & " AND tblDefOpportunity.UserId = " & LoginUser.LoginUserId & " GROUP BY DATEPART(YEAR,CloseDate),DATEPART(QQ,CloseDate) "
    '    Else
    '        strSQL = "SELECT DATEPART(YEAR,CloseDate)  [Year]," _
    '        & " concat('Q',DATEPART(QQ,CloseDate), '=' , CAST(sum(tblDefOpportunity.TotalAmount) as nvarchar(100))) as Total " _
    '        & "  FROM tblDefOpportunity " _
    '        & " WHERE YEAR(CloseDate) = " & year & " and tblDefOpportunity.UserId = " & LoginUser.LoginUserId & " GROUP BY DATEPART(YEAR,CloseDate),DATEPART(QQ,CloseDate) "
    '    End If
    '    Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
    '    Return dt
    'Catch ex As Exception
    '    Throw ex
    'End Try



    'Dim objCon As New OleDbConnection(Con.ConnectionString)
    'If objCon.State = ConnectionState.Open Then objCon.Close()
    'objCon.Open()
    'Try
    '    Dim dtchart As New DataTable()            
    '    GetUser(LoginUserId)
    '    'Dim cmd As New OleDbCommand("OpportunityChart" & UserId & "", objCon)
    '    Dim cmd As New OleDbCommand("OpportunityChart ", objCon)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@UserId", OleDbType.Integer).Value = LoginUserId
    '    Dim dr As OleDbDataReader = cmd.ExecuteReader
    '    dtchart.Load(dr)
    '    Return dtchart
    'Catch ex As Exception
    '    Throw ex

    'Finally
    '    objCon.Close()
    'End Try
    Private Sub Chart1_Click(sender As Object, e As EventArgs) Handles Chart1.Click
        'Try
        '    Dim pointindex As Integer
        '    Dim result As HitTestResult
        '    result = Chart1.HitTest(Cursor.Position.X, Cursor.Position.Y)
        '    If result.ChartElementType = ChartElementType.DataPoint Then
        '        pointindex = result.PointIndex
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
    End Sub


    Public Function GetUser(ByVal UserId As Integer) As DataTable
        Dim Str As String = String.Empty
        Try
            Str = "SELECT User_ID FROM tblUser WHERE tblUser.User_ID =" & UserId & ""
            Dim dt As DataTable = UtilityDAL.GetDataTable(Str)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Exit Sub
            End If
            Me.Visible = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        'If ComboBox1.Text = "2019" Then
        '    LoadData()
        'ElseIf ComboBox1.Text = "2020" Then
        '    LoadData()
        'ElseIf ComboBox1.Text = "2021" Then
        '    LoadData()
        'ElseIf ComboBox1.Text = "2022" Then
        '    LoadData()
        'ElseIf ComboBox1.Text = "2023" Then
        '    LoadData()
        'ElseIf ComboBox1.Text = "2024" Then
        '    LoadData()
        'ElseIf ComboBox1.Text = "2025" Then
        '    LoadData()
        'ElseIf ComboBox1.Text = "2026" Then
        '    LoadData()
        'ElseIf ComboBox1.Text = "2027" Then
        '    LoadData()
        'ElseIf ComboBox1.Text = "2028" Then
        '    LoadData()
        'ElseIf ComboBox1.Text = "2029" Then
        '    LoadData()
        'ElseIf ComboBox1.Text = "2030" Then
        '    LoadData()
        'End If
        LoadData()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        LoadData()
    End Sub
End Class