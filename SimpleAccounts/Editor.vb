Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports mshtml
Imports System.Diagnostics
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Web
Imports System.Threading
Imports System.Net.Mail
Imports System.Net.Mime

Public Class Editor
    Inherits UserControl
    'implement SearchableBrowser

    Private doc As IHTMLDocument2
    Private updatingFontName As Boolean = False
    Private updatingFontSize As Boolean = False
    Private setup As Boolean = False
    Private init_timer As Boolean = False
    Public Delegate Sub TickDelegate()
    Public Const _InitializedFontName As String = "Times New Roman"
    Public Const _InitializedFontSize As Integer = 1

    Public Class EnterKeyEventArgs
        Inherits EventArgs

        Private _cancel As Boolean = False

        Public Property Cancel As Boolean
            Get
                Return _cancel
            End Get
            Set(ByVal value As Boolean)
                _cancel = value
            End Set
        End Property
    End Class

    Public Event Tick As TickDelegate
    Public Event Navigated As WebBrowserNavigatedEventHandler
    Public Event EnterKeyEvent As EventHandler(Of EnterKeyEventArgs)
    Public Sub New()
        'Load += New EventHandler(AddressOf Editor_Load)

        AddHandler Load, New EventHandler(AddressOf Editor_Load)

        InitializeComponent()
        SetupEvents()
        'SetupTimer()
        SetupBrowser()
        SetupFontComboBox()
        SetupFontSizeComboBox()
        AddHandler boldButton.CheckedChanged, Function()
                                                  RaiseEvent BoldChanged()
                                              End Function

        AddHandler italicButton.CheckedChanged, Function()
                                                    RaiseEvent ItalicChanged()
                                                End Function

        AddHandler underlineButton.CheckedChanged, Function()
                                                       RaiseEvent UnderlineChanged()
                                                   End Function

        AddHandler orderedListButton.CheckedChanged, Function()
                                                         RaiseEvent OrderedListChanged()
                                                     End Function

        AddHandler unorderedListButton.CheckedChanged, Function()
                                                           RaiseEvent UnorderedListChanged()
                                                       End Function

        AddHandler justifyLeftButton.CheckedChanged, Function()
                                                         RaiseEvent JustifyLeftChanged()
                                                     End Function

        AddHandler justifyCenterButton.CheckedChanged, Function()
                                                           RaiseEvent JustifyCenterChanged()
                                                       End Function

        AddHandler justifyRightButton.CheckedChanged, Function()
                                                          RaiseEvent JustifyRightChanged()
                                                      End Function

        AddHandler justifyFullButton.CheckedChanged, Function()
                                                         RaiseEvent JustifyFullChanged()
                                                     End Function

        AddHandler linkButton.CheckedChanged, Function()
                                                  RaiseEvent IsLinkChanged()
                                              End Function
    End Sub
    Private Sub SetupFontComboBox()
        Dim ac As AutoCompleteStringCollection = New AutoCompleteStringCollection()

        For Each fam As FontFamily In FontFamily.Families
            fontComboBox.Items.Add(fam.Name)
            ac.Add(fam.Name)
        Next
        fontComboBox.Text = _InitializedFontName
        AddHandler fontComboBox.Leave, New EventHandler(AddressOf fontComboBox_TextChanged)
        fontComboBox.AutoCompleteMode = AutoCompleteMode.Suggest
        fontComboBox.AutoCompleteSource = AutoCompleteSource.CustomSource
        fontComboBox.AutoCompleteCustomSource = ac
    End Sub
    Private Sub fontComboBox_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles fontComboBox.TextChanged
        If updatingFontName Then Return
        Dim ff As FontFamily

        Try
            ff = New FontFamily(fontComboBox.Text)
        Catch __unusedException1__ As Exception
            updatingFontName = True
            fontComboBox.Text = FontName.GetName(0)
            updatingFontName = False
            Return
        End Try

        FontName = ff
    End Sub
    <Browsable(False)>
    Public Property FontName As FontFamily
        Get
            If _ReadyState <> ReadyState.Complete Then Return Nothing
            If doc.queryCommandValue("FontName") Is Nothing Then Return Nothing
            'Dim name As String = TryCast(doc.queryCommandValue("FontName"), String)
            Dim name As String = doc.queryCommandValue("FontName").ToString
            If name = "" Then Return Nothing
            Return New FontFamily(name)
        End Get
        Set(ByVal value As FontFamily)
            If value IsNot Nothing Then webBrowser1.Document.ExecCommand("FontName", False, value.Name)
        End Set
    End Property

    Private Sub UpdateFontComboBox()
        Try
            If Not fontComboBox.Focused Then
                If FontName.Name = "Editor1" Then
                    Exit Sub
                End If
                If FontName IsNot Nothing Then
                    Dim fam As FontFamily = FontName

                    If fam IsNot Nothing Then
                        Dim fontname As String = fam.Name

                        If fontname <> fontComboBox.Text Then
                            updatingFontName = True
                            fontComboBox.Text = fontname
                            RaiseEvent HtmlFontChanged()
                            updatingFontName = False
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public ReadOnly Property _ReadyState As ReadyState
        Get

            Select Case doc.readyState.ToLower()
                Case "uninitialized"
                    Return ReadyState.Uninitialized
                Case "loading"
                    Return ReadyState.Loading
                Case "loaded"
                    Return ReadyState.Loaded
                Case "interactive"
                    Return ReadyState.Interactive
                Case "complete"
                    Return ReadyState.Complete
                Case Else
                    Return ReadyState.Uninitialized
            End Select
        End Get
    End Property
    Public Enum ReadyState
        Uninitialized
        Loading
        Loaded
        Interactive
        Complete
    End Enum
    Public Event BoldChanged As MethodInvoker
    Public Event ItalicChanged As MethodInvoker
    Public Event UnderlineChanged As MethodInvoker
    Public Event OrderedListChanged As MethodInvoker
    Public Event UnorderedListChanged As MethodInvoker
    Public Event JustifyLeftChanged As MethodInvoker
    Public Event JustifyCenterChanged As MethodInvoker
    Public Event JustifyRightChanged As MethodInvoker
    Public Event JustifyFullChanged As MethodInvoker
    Public Event IsLinkChanged As MethodInvoker
    Public Event HtmlFontChanged As MethodInvoker
    Public Event HtmlFontSizeChanged As MethodInvoker
    Private lastSplash As DateTime = DateTime.Now
    Private Sub Editor_Load(ByVal sender As Object, ByVal e As EventArgs)
        timer.Start()
    End Sub
    Private Sub SetupEvents()
        AddHandler webBrowser1.Navigated, New WebBrowserNavigatedEventHandler(AddressOf webBrowser1_Navigated)
        AddHandler webBrowser1.GotFocus, New EventHandler(AddressOf webBrowser1_GotFocus)
        If webBrowser1.Version.Major >= 9 Then AddHandler webBrowser1.DocumentCompleted, New WebBrowserDocumentCompletedEventHandler(AddressOf webBrowser1_DocumentCompleted)
    End Sub
    Private Sub webBrowser1_Navigated(ByVal sender As Object, ByVal e As WebBrowserNavigatedEventArgs) Handles webBrowser1.Navigated
        Try

            SetBackgroundColor(BackColor)
            RaiseEvent Navigated(Me, e)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub webBrowser1_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles webBrowser1.GotFocus
        SuperFocus()
    End Sub
    Private Sub SetBackgroundColor(ByVal value As Color)
        If webBrowser1.Document IsNot Nothing AndAlso webBrowser1.Document.Body IsNot Nothing Then webBrowser1.Document.Body.Style = String.Format("background-color: {0}", value.Name)
    End Sub
    Private Sub SuperFocus()
        If webBrowser1.Document IsNot Nothing AndAlso webBrowser1.Document.Body IsNot Nothing Then webBrowser1.Document.Body.Focus()
    End Sub
    Private Sub webBrowser1_DocumentCompleted(ByVal sender As Object, ByVal e As WebBrowserDocumentCompletedEventArgs) Handles webBrowser1.DocumentCompleted
        If webBrowser1.Document.Body IsNot Nothing Then webBrowser1.Document.Body.SetAttribute("contentEditable", "true")
    End Sub
    'Private Sub SetupTimer()
    '    timer.Interval = 200
    '    AddHandler timer.Tick, New EventHandler(AddressOf timer_Tick)
    'End Sub

    'Private Sub timer_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles timer.Tick
    '    If Not init_timer Then
    '        AddHandler ParentForm.FormClosed, New FormClosedEventHandler(AddressOf ParentForm_FormClosed)
    '        init_timer = True
    '        lastSplash = DateTime.Now
    '    End If

    '    If _ReadyState <> ReadyState.Complete Then Return
    '    SetupKeyListener()
    '    boldButton.Checked = IsBold()
    '    italicButton.Checked = IsItalic()
    '    underlineButton.Checked = IsUnderline()
    '    orderedListButton.Checked = IsOrderedList()
    '    unorderedListButton.Checked = IsUnorderedList()
    '    justifyLeftButton.Checked = IsJustifyLeft()
    '    justifyCenterButton.Checked = IsJustifyCenter()
    '    justifyRightButton.Checked = IsJustifyRight()
    '    justifyFullButton.Checked = IsJustifyFull()
    '    'linkButton.Enabled = CanInsertLink()
    '    UpdateFontComboBox()
    '    UpdateFontSizeComboBox()
    '    UpdateImageSizes()
    '    RaiseEvent Tick()
    'End Sub
    Private Sub ParentForm_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs)
        timer.[Stop]()
        updatingFontName = False
        updatingFontSize = False
        setup = False
        init_timer = False
        BodyHtml = String.Empty
        RemoveHandler ParentForm.FormClosed, New FormClosedEventHandler(AddressOf ParentForm_FormClosed)
    End Sub
    Private Sub SetupKeyListener()
        If Not setup Then
            AddHandler webBrowser1.Document.Body.KeyDown, New HtmlElementEventHandler(AddressOf Body_KeyDown)
            setup = True
        End If
    End Sub
    Private Sub Body_KeyDown(ByVal sender As Object, ByVal e As HtmlElementEventArgs)
        If e.KeyPressedCode = 13 AndAlso Not e.ShiftKeyPressed Then
            Dim cancel As Boolean = False

            'If EnterKeyEvent IsNot Nothing Then
            '    Dim args As EnterKeyEventArgs = New EnterKeyEventArgs()
            '    EnterKeyEvent(Me, args)
            '    cancel = args.Cancel
            'End If

            e.ReturnValue = Not cancel
        End If
    End Sub
    Private Sub SetupBrowser()
        webBrowser1.DocumentText = "<html><body></body></html>"
        doc = TryCast(webBrowser1.Document.DomDocument, IHTMLDocument2)
        doc.designMode = "On"
        AddHandler webBrowser1.Document.ContextMenuShowing, New HtmlElementEventHandler(AddressOf Document_ContextMenuShowing)
    End Sub
    Private Sub Document_ContextMenuShowing(ByVal sender As Object, ByVal e As HtmlElementEventArgs)
        e.ReturnValue = False
        'cutToolStripMenuItem1.Enabled = CanCut()
        'copyToolStripMenuItem2.Enabled = CanCopy()
        'pasteToolStripMenuItem3.Enabled = CanPaste()
        'deleteToolStripMenuItem.Enabled = CanDelete()
        'cSSToolStripMenuItem.Enabled = SelectionType <> TextControl.SelectionType.None
        'contextMenuStrip1.Show(Me, e.ClientMousePosition)
    End Sub
    Private Sub UpdateFontSizeComboBox()
        If Not fontSizeComboBox.Focused Then
            Dim foo As Integer
            Select Case _Fontsize
                Case FontSize.One
                    foo = 1
                Case FontSize.Two
                    foo = 2
                Case FontSize.Three
                    foo = 3
                Case FontSize.Four
                    foo = 4
                Case FontSize.Five
                    foo = 5
                Case FontSize.Six
                    foo = 6
                Case FontSize.Seven
                    foo = 7
                Case FontSize.NA
                    foo = 0
                Case Else
                    foo = 7
            End Select
            Dim fontsize1 As String = Convert.ToString(foo)
            If fontsize1 <> fontSizeComboBox.Text Then
                updatingFontSize = True
                fontSizeComboBox.Text = fontsize1
                RaiseEvent HtmlFontSizeChanged()
                updatingFontSize = False
            End If
        End If
    End Sub
    Public Enum FontSize
        One
        Two
        Three
        Four
        Five
        Six
        Seven
        NA
    End Enum

    <Browsable(False)>
    Public Property _FontSize As FontSize
        Get
            If _ReadyState <> ReadyState.Complete Then Return FontSize.NA
            Dim fs As String = doc.queryCommandValue("FontSize").ToString()

            Select Case fs
                Case "1"
                    Return FontSize.One
                Case "2"
                    Return FontSize.Two
                Case "3"
                    Return FontSize.Three
                Case "4"
                    Return FontSize.Four
                Case "5"
                    Return FontSize.Five
                Case "6"
                    Return FontSize.Six
                Case "7"
                    Return FontSize.Seven
                Case Else
                    Return FontSize.NA
            End Select
        End Get
        Set(ByVal value As FontSize)
            Dim sz As Integer

            Select Case value
                Case FontSize.One
                    sz = 1
                Case FontSize.Two
                    sz = 2
                Case FontSize.Three
                    sz = 3
                Case FontSize.Four
                    sz = 4
                Case FontSize.Five
                    sz = 5
                Case FontSize.Six
                    sz = 6
                Case FontSize.Seven
                    sz = 7
                Case Else
                    sz = 7
            End Select

            webBrowser1.Document.ExecCommand("FontSize", False, sz.ToString())
        End Set
    End Property
    Private Sub SetupFontSizeComboBox()
        For x As Integer = 1 To 10
            fontSizeComboBox.Items.Add(x.ToString())
        Next
        fontSizeComboBox.Text = 5
        AddHandler fontSizeComboBox.TextChanged, New EventHandler(AddressOf fontSizeComboBox_TextChanged)
        AddHandler fontSizeComboBox.KeyPress, New KeyPressEventHandler(AddressOf fontSizeComboBox_KeyPress)
    End Sub
    Private Sub fontSizeComboBox_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        If updatingFontSize Then Return

        Select Case fontSizeComboBox.Text.Trim()
            Case "1"
                _FontSize = FontSize.One
            Case "2"
                _FontSize = FontSize.Two
            Case "3"
                _FontSize = FontSize.Three
            Case "4"
                _FontSize = FontSize.Four
            Case "5"
                _FontSize = FontSize.Five
            Case "6"
                _FontSize = FontSize.Six
            Case "7"
                _FontSize = FontSize.Seven
            Case Else
                _FontSize = FontSize.Seven
        End Select
    End Sub
    Private Sub fontSizeComboBox_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles fontSizeComboBox.KeyPress
        If Char.IsNumber(e.KeyChar) Then
            e.Handled = True
            If e.KeyChar <= "7"c AndAlso e.KeyChar > "0"c Then fontSizeComboBox.Text = e.KeyChar.ToString()
        ElseIf Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub UpdateImageSizes()
        For Each image As HTMLImg In doc.images

            If image IsNot Nothing Then
                If image.height <> image.style.pixelHeight AndAlso image.style.pixelHeight <> 0 Then image.height = image.style.pixelHeight
                If image.width <> image.style.pixelWidth AndAlso image.style.pixelWidth <> 0 Then image.width = image.style.pixelWidth
            End If
        Next
    End Sub
    'Public Sub Cut()
    '    webBrowser1.Document.ExecCommand("Cut", False, Nothing)
    'End Sub

    Private Sub CutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CutToolStripMenuItem.Click
        Cut()
    End Sub
    'Public Sub Copy()
    '    webBrowser1.Document.ExecCommand("Copy", False, Nothing)
    'End Sub

    'Public Sub Paste()
    '    webBrowser1.Document.ExecCommand("Paste", False, Nothing)
    'End Sub

    Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        Paste()
    End Sub
    'Public Sub Delete()
    '    webBrowser1.Document.ExecCommand("Delete", False, Nothing)
    'End Sub

    'Public Sub SelectBodyColor()

    '        Color color = BodyBackgroundColor;
    '        if (ShowColorDialog(ref color))
    '            BodyBackgroundColor = color;

    'Public Sub SelectBodyColor()
    '    Dim color As Color = BodyBackgroundColor
    '    If ShowColorDialog(color) Then BodyBackgroundColor = color
    'End Sub
    Public Property BodyBackgroundColor As Color
        Get
            If doc.body IsNot Nothing AndAlso doc.body.style IsNot Nothing AndAlso doc.body.style.backgroundColor IsNot Nothing Then Return ConvertToColor(doc.body.style.backgroundColor.ToString())
            Return Color.White
        End Get
        Set(ByVal value As Color)

            If _ReadyState = ReadyState.Complete Then
                If doc.body IsNot Nothing AndAlso doc.body.style IsNot Nothing Then
                    Dim colorstr As String = String.Format("#{0:X2}{1:X2}{2:X2}", value.R, value.G, value.B)
                    doc.body.style.backgroundColor = colorstr
                End If
            End If
        End Set
    End Property
    Private Function ShowColorDialog(ByRef color As Color) As Boolean
        Dim selected As Boolean
        Using dlg As ColorDialog = New ColorDialog()
            dlg.SolidColorOnly = True
            dlg.AllowFullOpen = False
            dlg.AnyColor = False
            dlg.FullOpen = False
            dlg.CustomColors = Nothing
            dlg.Color = color
            If dlg.ShowDialog(Me) = DialogResult.OK Then
                selected = True
                color = dlg.Color
            Else
                selected = False
            End If
        End Using
        Return selected
    End Function

    Private Sub italicButton_Click(sender As Object, e As EventArgs) Handles italicButton.Click
        Italic()
    End Sub

    Private Sub boldButton_Click(sender As Object, e As EventArgs) Handles boldButton.Click
        Bold()
    End Sub
    Public Sub Print()
        webBrowser1.Document.ExecCommand("Print", True, Nothing)
    End Sub

    Public Sub InsertParagraph()
        webBrowser1.Document.ExecCommand("InsertParagraph", False, Nothing)
    End Sub

    Public Sub InsertBreak()
        webBrowser1.Document.ExecCommand("InsertHorizontalRule", False, Nothing)
    End Sub

    Public Sub SelectAll()
        webBrowser1.Document.ExecCommand("SelectAll", False, Nothing)
    End Sub

    Public Sub Undo()
        webBrowser1.Document.ExecCommand("Undo", False, Nothing)
    End Sub

    Public Sub Redo()
        webBrowser1.Document.ExecCommand("Redo", False, Nothing)
    End Sub

    Public Sub Cut()
        webBrowser1.Document.ExecCommand("Cut", False, Nothing)
    End Sub

    Public Sub Paste()
        webBrowser1.Document.ExecCommand("Paste", False, Nothing)
    End Sub

    'Public Sub Copy()
    '    webBrowser1.Document.ExecCommand("Copy", False, Nothing)
    'End Sub

    'Public Sub OrderedList()
    '    webBrowser1.Document.ExecCommand("InsertOrderedList", False, Nothing)
    'End Sub

    'Public Sub UnorderedList()
    '    webBrowser1.Document.ExecCommand("InsertUnorderedList", False, Nothing)
    'End Sub

    'Public Sub JustifyLeft()
    '    webBrowser1.Document.ExecCommand("JustifyLeft", False, Nothing)
    'End Sub

    'Public Sub JustifyRight()
    '    webBrowser1.Document.ExecCommand("JustifyRight", False, Nothing)
    'End Sub

    'Public Sub JustifyCenter()
    '    webBrowser1.Document.ExecCommand("JustifyCenter", False, Nothing)
    'End Sub

    'Public Sub JustifyFull()
    '    webBrowser1.Document.ExecCommand("JustifyFull", False, Nothing)
    'End Sub

    Public Sub Bold()
        webBrowser1.Document.ExecCommand("Bold", False, Nothing)
    End Sub

    Public Sub Italic()
        webBrowser1.Document.ExecCommand("Italic", False, Nothing)
    End Sub

    Public Sub Underline()
        webBrowser1.Document.ExecCommand("Underline", False, Nothing)
    End Sub

    Public Sub Delete()
        webBrowser1.Document.ExecCommand("Delete", False, Nothing)
    End Sub

    'Public Sub InsertImage()
    '    webBrowser1.Document.ExecCommand("InsertImage", True, Nothing)
    'End Sub

    'Public Sub Indent()
    '    webBrowser1.Document.ExecCommand("Indent", False, Nothing)
    'End Sub

    'Public Sub Outdent()
    '    webBrowser1.Document.ExecCommand("Outdent", False, Nothing)
    'End Sub

    Public Sub InsertLink(ByVal url As String)
        webBrowser1.Document.ExecCommand("CreateLink", False, url)
    End Sub

    Private Sub underlineButton_Click(sender As Object, e As EventArgs) Handles underlineButton.Click
        Underline()
    End Sub

    Private Sub colorButton_Click(sender As Object, e As EventArgs) Handles colorButton.Click
        SelectForeColor()
    End Sub
    Public Sub SelectForeColor()
        Dim color As Color = EditorForeColor
        If ShowColorDialog(color) Then EditorForeColor = color
    End Sub
    <Browsable(False)>
    Public Property EditorForeColor As Color
        Get
            If _ReadyState <> ReadyState.Complete Then Return Color.Black
            Return ConvertToColor(doc.queryCommandValue("ForeColor").ToString())
        End Get
        Set(ByVal value As Color)
            Dim colorstr As String = String.Format("#{0:X2}{1:X2}{2:X2}", value.R, value.G, value.B)
            webBrowser1.Document.ExecCommand("ForeColor", False, colorstr)
        End Set
    End Property

    Private Shared Function ConvertToColor(ByVal clrs As String) As Color
        Try
            Dim red, green, blue As Integer
            '' sometimes clrs is HEX organized as (RED)(GREEN)(BLUE)
            If clrs.StartsWith("#") Then
                Dim clrn As Integer = Convert.ToInt32(clrs.Substring(1), 16)
                red = (clrn > 16) And 255
                green = (clrn > 8) And 255
                blue = clrn And 255
            Else ''otherwise clrs is DECIMAL organized as (BlUE)(GREEN)(RED)
                Dim clrn As Integer = Convert.ToInt32(clrs)
                red = clrn And 255
                green = (clrn > 8) And 255
                blue = (clrn > 16) And 255
                Dim incolor As Color = Color.FromArgb(red, green, blue)
                Return incolor
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function

    Private Sub backColorButton_Click(sender As Object, e As EventArgs) Handles backColorButton.Click
        SelectBackColor()
    End Sub

    Public Sub SelectBackColor()
        Try
            Dim color As Color = EditorBackColor
            If ShowColorDialog(color) Then EditorBackColor = color
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    <Browsable(False)>
    Public Property EditorBackColor As Color
        Get
            If _ReadyState <> ReadyState.Complete Then Return Color.White
            Return ConvertToColor(doc.queryCommandValue("BackColor").ToString())
        End Get
        Set(ByVal value As Color)
            Dim colorstr As String = String.Format("#{0:X2}{1:X2}{2:X2}", value.R, value.G, value.B)
            webBrowser1.Document.ExecCommand("BackColor", False, colorstr)
        End Set
    End Property

    Public Sub SelectBodyColor()
        Dim color As Color = BodyBackgroundColor
        If ShowColorDialog(color) Then BodyBackgroundColor = color
    End Sub

    Private Sub linkButton_Click(sender As Object, e As EventArgs) Handles linkButton.Click

    End Sub
    'Public Sub SelectLink()
    '    Dim url As String = String.Empty

    '    Select Case _SelectionType
    '        Case SelectionType.Control
    '            Dim range As IHTMLControlRange = TryCast(doc.selection.createRange(), IHTMLControlRange)

    '            If range IsNot Nothing AndAlso range.length > 0 Then
    '                Dim elem = range.item(0)

    '                If elem IsNot Nothing AndAlso String.Compare(elem.tagName, "img", True) = 0 Then
    '                    elem = elem.parentElement

    '                    If elem IsNot Nothing AndAlso String.Compare(elem.tagName, "a", True) = 0 Then
    '                        url = TryCast(elem.getAttribute("href"), String)
    '                    End If
    '                End If
    '            End If

    '        Case SelectionType.Text
    '            Dim txtRange As IHTMLTxtRange = CType(doc.selection.createRange(), IHTMLTxtRange)

    '            If txtRange IsNot Nothing AndAlso Not String.IsNullOrEmpty(txtRange.htmlText) Then
    '                Dim regex As Regex = New Regex("^\s*<a href=""([^""]+)"">[^<]+</a>\s*$", RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant)
    '                Dim match As Match = regex.Match(txtRange.htmlText)
    '                If match.Success Then url = match.Groups(1).Value
    '            End If
    '    End Select

    '    Using dlg As LinkDialog = New LinkDialog()
    '        Dim uri As Uri

    '        If uri.TryCreate(url, UriKind.Absolute, uri) Then
    '            dlg.URL = String.Format("{0}{1}", uri.Host, If(uri.PathAndQuery Is Nothing, Nothing, uri.PathAndQuery.TrimEnd("/"c)))
    '            dlg.Scheme = String.Format("{0}://", uri.Scheme)
    '        End If

    '        dlg.ShowDialog(Me.ParentForm)
    '        If Not dlg.Accepted Then Return
    '        Dim link As String = String.Format("{0}{1}", dlg.Scheme, dlg.URL)

    '        If link Is Nothing OrElse link.Length = 0 Then
    '            MessageBox.Show(Me.ParentForm, "Invalid URL")
    '            Return
    '        End If

    '        InsertLink(link)
    '    End Using
    'End Sub
    Public ReadOnly Property _SelectionType As SelectionType
        Get
            Dim type = doc.selection.type.ToLower()

            Select Case type
                Case "text"
                    Return SelectionType.Text
                Case "control"
                    Return SelectionType.Control
                Case "none"
                    Return SelectionType.None
                Case Else
                    Return SelectionType.None
            End Select
        End Get
    End Property
    Public Enum SelectionType
        Text
        Control
        None
    End Enum

    Private Sub justifyLeftButton_Click(sender As Object, e As EventArgs) Handles justifyLeftButton.Click
        JustifyLeft()
    End Sub

    Private Sub justifyCenterButton_Click(sender As Object, e As EventArgs) Handles justifyCenterButton.Click
        JustifyCenter()
    End Sub

    Private Sub imageButton_Click(sender As Object, e As EventArgs) Handles imageButton.Click
        InsertImage()
    End Sub
    Public Sub InsertImage()
        webBrowser1.Document.ExecCommand("InsertImage", True, Nothing)
    End Sub

    Public Sub JustifyLeft()
        webBrowser1.Document.ExecCommand("JustifyLeft", False, Nothing)
    End Sub

    Private Sub justifyRightButton_Click(sender As Object, e As EventArgs) Handles justifyRightButton.Click
        JustifyRight()
    End Sub
    Public Sub JustifyCenter()
        webBrowser1.Document.ExecCommand("JustifyCenter", False, Nothing)
    End Sub
    Public Sub JustifyRight()
        webBrowser1.Document.ExecCommand("JustifyRight", False, Nothing)
    End Sub
    Public Sub JustifyFull()
        webBrowser1.Document.ExecCommand("JustifyFull", False, Nothing)
    End Sub

    Private Sub justifyFullButton_Click(sender As Object, e As EventArgs) Handles justifyFullButton.Click
        JustifyFull()
    End Sub

    Private Sub orderedListButton_Click(sender As Object, e As EventArgs) Handles orderedListButton.Click
        OrderedList()
    End Sub
    Public Sub OrderedList()
        webBrowser1.Document.ExecCommand("InsertOrderedList", False, Nothing)
    End Sub

    Public Sub UnorderedList()
        webBrowser1.Document.ExecCommand("InsertUnorderedList", False, Nothing)
    End Sub

    Private Sub unorderedListButton_Click(sender As Object, e As EventArgs) Handles unorderedListButton.Click
        UnorderedList()
    End Sub
    Public Sub Outdent()
        webBrowser1.Document.ExecCommand("Outdent", False, Nothing)
    End Sub

    Private Sub outdentButton_Click(sender As Object, e As EventArgs) Handles outdentButton.Click
        Outdent()
    End Sub

    Private Sub indentButton_Click(sender As Object, e As EventArgs) Handles indentButton.Click
        Indent()
    End Sub

    Public Sub Indent()
        webBrowser1.Document.ExecCommand("Indent", False, Nothing)
    End Sub
    Public Function IsBold() As Boolean
        Return doc.queryCommandState("Bold")
    End Function
    Public Function IsItalic() As Boolean
        Return doc.queryCommandState("Italic")
    End Function
    Public Function IsUnderline() As Boolean
        Return doc.queryCommandState("Underline")
    End Function
    Public Function IsOrderedList() As Boolean
        Return doc.queryCommandState("InsertOrderedList")
    End Function
    Public Function IsUnorderedList() As Boolean
        Return doc.queryCommandState("InsertUnorderedList")
    End Function
    Public Function IsJustifyLeft() As Boolean
        Return doc.queryCommandState("JustifyLeft")
    End Function
    Public Function IsJustifyRight() As Boolean
        Return doc.queryCommandState("JustifyRight")
    End Function
    Public Function IsJustifyCenter() As Boolean
        Return doc.queryCommandState("JustifyCenter")
    End Function
    Public Function IsJustifyFull() As Boolean
        Return doc.queryCommandState("JustifyFull")
    End Function
    'Public Function CanInsertLink() As Boolean
    '    'return (SelectionType == SelectionType.Text && !LinksInSelection());
    '    Return (!LinksInSelection())
    'End Function



    <Browsable(False)>
    Public Property BodyHtml As String
        Get

            If webBrowser1.Document IsNot Nothing AndAlso webBrowser1.Document.Body IsNot Nothing Then
                Dim html As String = webBrowser1.Document.Body.InnerHtml

                If html IsNot Nothing Then
                    html = ReplaceFileSystemImages(html)
                End If

                Return html
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            If webBrowser1.Document.Body IsNot Nothing Then webBrowser1.Document.Body.InnerHtml = value
        End Set
    End Property
    Private Function ReplaceFileSystemImages(ByVal html As String) As String
        Dim matches = Regex.Matches(html, "<img[^>]*?src\s*=\s*([""']?[^'"">]+?['""])[^>]*?>", RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace Or RegexOptions.Multiline)

        For Each match As Match In matches
            Dim src As String = match.Groups(1).Value
            src = src.Trim(""""c)

            If File.Exists(src) Then
                Dim ext = Path.GetExtension(src)

                If ext.Length > 0 Then
                    ext = ext.Substring(1)
                    src = String.Format("'data:image/{0};base64,{1}'", ext, Convert.ToBase64String(File.ReadAllBytes(src)))
                    html = html.Replace(match.Groups(1).Value, src)
                End If
            End If
        Next

        Return html
    End Function
End Class


