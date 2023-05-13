Public Class UserControl1

    Dim dtFiles As DataTable

    Private _ReleaseFolder As String

    Public Property ReleaseFolder() As String
        Get
            Return _ReleaseFolder
        End Get
        Set(ByVal value As String)
            _ReleaseFolder = value
        End Set
    End Property

    Private _VersionStart As Integer
    Public Property VersionStart() As Integer
        Get
            Return _VersionStart
        End Get
        Set(ByVal value As Integer)
            _VersionStart = value
        End Set
    End Property

    Private _VersionEnd As Integer
    Public Property VersionEnd() As Integer
        Get
            Return _VersionEnd
        End Get
        Set(ByVal value As Integer)
            _VersionEnd = value
        End Set
    End Property

    Private _ConnectionString As String
    Public Property ConnectionString() As String
        Get
            Return _ConnectionString
        End Get
        Set(ByVal value As String)
            _ConnectionString = value
        End Set
    End Property

    Private _TitleLable As String
    Public Property TitleLable() As String
        Get
            Return _TitleLable
        End Get
        Set(ByVal value As String)
            _TitleLable = value
        End Set
    End Property


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.ParentForm.Close()
    End Sub

    Public Sub ConfigureControl()
        Me.lblTitle.Text = _TitleLable
        Me.lblSubTitle.Text = "Welcome to version update process"
        Dim dInfo As IO.DirectoryInfo
        If _ReleaseFolder Is Nothing Or ReleaseFolder = String.Empty Then
            dInfo = New IO.DirectoryInfo(Application.StartupPath)
        Else
            dInfo = New IO.DirectoryInfo(_ReleaseFolder)

        End If

        Dim Dir As IO.DirectoryInfo

        For Each Dir In dInfo.GetDirectories
            If _VersionStart > 0 Then
                If Val(Dir.Name.ToString.Replace(".", "")) >= _VersionStart AndAlso Val(Dir.Name.ToString.Replace(".", "")) <= _VersionEnd Then
                    Me.treeVersions.Nodes.Add(Dir.Name.ToString)
                End If
            Else
                Me.treeVersions.Nodes.Add(Dir.Name.ToString)
            End If
            Me.treeVersions.Nodes(Me.treeVersions.Nodes.Count - 1).Checked = True

        Next
        Me.ProgTotal.Maximum = Me.treeVersions.Nodes.Count

    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click

        Me.ProgTotal.Value = 0

        Me.dtFiles = Nothing
        Me.dtFiles = New DataTable("Release")
        Dim dt As DataTable = New DataTable("history")
        For iC As Integer = 1 To Me.grdDetails.Columns.Count - 1
            Me.dtFiles.Columns.Add(grdDetails.Columns(iC).Name.ToString, GetType(String))
            dt.Columns.Add(grdDetails.Columns(iC).Name.ToString)

        Next

        For i As Integer = 0 To Me.treeVersions.Nodes.Count - 1
            If treeVersions.Nodes(i).Checked = True Then

                Dim Directory As IO.DirectoryInfo
                If _ReleaseFolder Is Nothing Or ReleaseFolder = String.Empty Then
                    Directory = New IO.DirectoryInfo(Application.StartupPath & "\" & Me.treeVersions.Nodes(i).Text & "\")
                Else
                    Directory = New IO.DirectoryInfo(_ReleaseFolder & "\" & Me.treeVersions.Nodes(i).Text & "\")
                End If

                Dim allFiles As IO.FileInfo() = Directory.GetFiles("*.sql*")
                Dim singleFile As IO.FileInfo



                For Each singleFile In allFiles
                    Me.dtFiles.Rows.Add(Me.treeVersions.Nodes(i).Text, singleFile.Name.ToString, "", "Test")
                Next

                Me.dtFiles.AcceptChanges()

            End If

        Next

        ''Me.ProgTotal.Maximum = dtFiles.Rows.Count

        ''For i As Integer = 0 To dtFiles.Rows.Count - 1

        ''Next

        'Dim path As String = _ReleaseFolder & "\" & Date.Now.ToString("ddyyyyMMhhmmss") & ".xml"
        'dtFiles.WriteXml(path)

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Me.grdDetails.Visible = True Then
            Me.grdDetails.Visible = False
            Me.Button1.Text = "Show Details"
        Else
            Me.grdDetails.Visible = True
            Me.Button1.Text = "Hide Details"
        End If

    End Sub

    Private Sub treeVersions_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles treeVersions.AfterSelect

        Me.grdDetails.DataSource = dtFiles 'Filter(dtFiles, "", False)

        'Dim Directory As IO.DirectoryInfo
        'If _ReleaseFolder Is Nothing Or ReleaseFolder = String.Empty Then
        '    Directory = New IO.DirectoryInfo(Application.StartupPath & "\" & Me.treeVersions.SelectedNode.Text & "\")
        'Else
        '    Directory = New IO.DirectoryInfo(_ReleaseFolder & "\" & Me.treeVersions.SelectedNode.Text & "\")
        'End If

        'Dim allFiles As IO.FileInfo() = Directory.GetFiles("*.*")
        'Dim singleFile As IO.FileInfo

        'Me.grdDetails.Rows.Clear()
        'Me.grdDetails.Update()
        'For Each singleFile In allFiles

        '    Me.grdDetails.Rows.Add(Nothing, singleFile.Name.ToString, "", "Test")
        'Next
        'Me.grdDetails.Update()
        'Application.DoEvents()
        'If Not Me.grdDetails.Rows.Count > 0 Then Exit Sub
        'Me.progTask.Value = 0
        'Me.progTask.Maximum = Me.grdDetails.Rows.Count
        'For i As Integer = 0 To grdDetails.Rows.Count - 1
        '    Me.progTask.Value += 1
        '    grdDetails.ClearSelection()
        '    grdDetails.Rows(i).Selected = True
        '    Me.lblProgress.Text = "Executing: " & grdDetails.Rows(i).Cells(1).Value

        '    grdDetails.Rows(i).Cells(2).Value = "In Progress"
        '    Application.DoEvents()
        '    System.Threading.Thread.Sleep(100)
        '    grdDetails.Rows(i).Cells(2).Value = "Success"
        '    Application.DoEvents()
        'Next
        'Me.ProgTotal.Value = Me.treeVersions.SelectedNode.Index + 1
    End Sub
End Class
