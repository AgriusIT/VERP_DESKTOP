Imports SBDal
Imports SBModel
Imports System.Data
Imports System.Data.SqlClient
Public Class frmActivityRemarks
    Private Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(ByVal LeadProfileId As Integer)

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        GetActivityRemarks(LeadProfileId)

    End Sub

    Private Sub GetActivityRemarks(ByVal LeadProfileId As Integer)
        Try
            Dim dtActivityRemarks As DataTable = New LeadProfileDAL2().GetActivityRemarks(LeadProfileId)
            Me.grd.DataSource = dtActivityRemarks
            Me.grd.RetrieveStructure()
            Me.grd.RootTable.Columns("RemarksId").Visible = False
            Me.grd.RootTable.Columns("LeadProfileId").Visible = False
            Me.grd.RootTable.Columns("ModifiedDate").FormatString = str_DisplayDateFormat
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class