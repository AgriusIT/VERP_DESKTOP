''Created by Muhammad Khalid on 29-Dec-08

Public Interface IReportsInterface
    Function FunAddReportCriteria() As String
    Sub FunAddReportPramaters()
    Sub FillCombos(Optional ByVal Condition As String = "")
    Sub ReportQuery(Optional ByVal Condition As String = "")
    Function RptDataTable() As DataTable





End Interface
