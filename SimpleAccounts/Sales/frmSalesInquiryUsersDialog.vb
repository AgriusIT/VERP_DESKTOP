Imports SBDal
Imports SBModel
Public Class frmSalesInquiryUsersDialog

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Public Sub New(ByVal ID As Integer, ByVal IsSalesInquiryApproval As Boolean)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        If IsSalesInquiryApproval = True Then
            Me.grd.DataSource = SalesInquiryRightsDAL.GetInquiryUsers(ID)
        Else
            Me.grd.DataSource = SalesInquiryRightsDAL.GetInquiryUsers2(ID)
        End If
    End Sub
    'Public Sub New(ByVal SalesInquiryRightsId As Integer)
    '    ' This call is required by the designer.
    '    InitializeComponent()
    '    ' Add any initialization after the InitializeComponent() call.
    '    Me.grd.DataSource = SalesInquiryRightsDAL.GetInquiryUsers(SalesInquiryRightsId)
    'End Sub
End Class