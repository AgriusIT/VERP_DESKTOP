Imports System.Data
Imports System.Data.OleDb
Public Class frmServicesDispatch
    Implements IGeneral
    Enum enmDetail
        LocationId
        ArticleDefId
        ArticleDescription
        ArticleCode
        PackQty
        Qty
        TotalQty
        CurrentPrice
        PackPrice
        Price
        TotalAmount
        Comments
        IssuedQty
        PrdNo
        RefDocId
        RefDocDetailId
    End Enum
    Dim DocId As Integer = 0I
    Dim IsOpenForm As Boolean = False
    Dim Comments As String = String.Empty

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdGatePass.RootTable.Columns
                If col.Index <> enmDetail.PackQty AndAlso col.Index <> enmDetail.Qty AndAlso col.Index <> enmDetail.TotalQty AndAlso col.Index <> enmDetail.Price AndAlso col.Index <> enmDetail.PackPrice AndAlso col.Index <> enmDetail.Comments Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmCustomerPlanning)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                For Each RightsDt As SBModel.GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                    End If
                Next
            End If

            If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                Me.btnDelete.Visible = False
            Else
                Me.btnDelete.Visible = True
            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

        Dim objCon As New OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Dim cmd As New OleDbCommand

        Try


            cmd.CommandType = CommandType.Text
            cmd.Connection = objCon
            cmd.Transaction = trans
            cmd.CommandTimeout = 120


            SaveDocDetail(Val(Me.grdSaved.GetRow.Cells("DocId").Value.ToString), "0", trans)

            cmd.CommandText = ""
            cmd.CommandText = "Delete From ServicesDispatchDetailTable WHERE DocId=" & Val(Me.grdSaved.GetRow.Cells("DocId").Value.ToString) & ""
            cmd.ExecuteNonQuery()

            cmd.CommandText = ""
            cmd.CommandText = "Delete From ServicesDispatchMasterTable WHERE DocId=" & Val(Me.grdSaved.GetRow.Cells("DocId").Value.ToString) & ""
            cmd.ExecuteNonQuery()

            trans.Commit()

            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try

            If Condition = "Location" Then
                FillDropDown(Me.cmbLocation, "Select Location_Id, Location_Name From tblDefLocation ORDER BY Sort_Order ASC", False)
            ElseIf Condition = String.Empty Then
                FillUltraDropDown(Me.cmbVendor, "Select coa_detail_id, detail_title as [Customer], detail_code as [Account Code], Sub_Sub_Title as [Account Head], Account_Type as [Account Type], Contact_Mobile as [Mobile], Contact_Email as [Email], CityName as City From vwCOADetail WHERE Detail_Title <> '' and Account_Type in('Customer','Vendor') Order By Detail_Title")
                Me.cmbVendor.Rows(0).Activate()
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
            ElseIf Condition = "Item" Then
                'Marked Against Task#201508010 By Ali Ansari to add Sales AccountID in Combo 
                'FillUltraDropDown(Me.cmbItem, "Select ArticleId, ArticleDescription as [Item], ArticleCode as [Code], ArticleGroupName as [Department],ArticleTypeName as [Type/Brand],ArticleCompanyName as [Category], ArticleSizeName as [Size], ArticleColorName as Combination, PurchasePrice as Price, MasterID From ArticleDefView WHERE ArticleDescription <> '' ORDER BY ArticleDescription ASC")
                FillUltraDropDown(Me.cmbItem, "Select ArticleId, ArticleDescription as [Item], ArticleCode as [Code], ArticleGroupName as [Department],ArticleTypeName as [Type/Brand],ArticleCompanyName as [Category], ArticleSizeName as [Size], ArticleColorName as Combination, PurchasePrice as Price, MasterID,SalesAccountId From ArticleDefView WHERE ArticleDescription <> '' ORDER BY ArticleDescription ASC")
                'Marked Against Task#201508010 By Ali Ansari to add Sales AccountID in Combo 
                Me.cmbItem.Rows(0).Activate()
                Me.cmbItem.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("MasterID").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("SalesAccountId").Hidden = True
            ElseIf Condition = "UnitPack" Then
                FillDropDown(Me.cmbUnit, "Select ArticlePackId, PackName, PackQty from ArticleDefPackTable WHERE ArticleMasterID=" & Val(Me.cmbItem.ActiveRow.Cells("MasterID").Value.ToString) & "", False)
            ElseIf Condition = "Documents" Then
                'FillDropDown(Me.cmbInvoice, "select ServicesProductionMasterTable.DocId,DocNo from ServicesProductionMasterTable left outer join (select DocId,Sum(IsNull(Qty,0)-IsNull(IssuedQty,0)) as Qty from ServicesProductionDetailTable group by DocId) a on a.DocId=ServicesProductionMasterTable.DocId  where DocNo <> '' and a.Qty > 0")
                'FillDropDown(Me.cmbInvoice, "select ServicesProductionMasterTable.DocId,DocNo,Job_No from ServicesProductionMasterTable left outer join (select DocId,Sum(IsNull(Qty,0)-IsNull(IssuedQty,0)) as Qty from ServicesInvoiceDetailTable group by DocId) a on a.DocId=ServicesProductionMasterTable.DocId  where DocNo <> '' and IsNull(a.Qty,0) > 0 and ServicesProductionMasterTable.customercode = " & Me.cmbVendor.Value & "")
                Dim strSQL As String = String.Empty
                strSQL = "select ServicesProductionMasterTable.DocId,DocNo, Job_No from ServicesProductionMasterTable left outer join (select DocId,Sum(IsNull(Qty,0)-IsNull(IssuedQty,0)) as Qty " _
                                          & " from ServicesProductionDetailTable group by DocId) a on a.DocId=ServicesProductionMasterTable.DocId  left outer join vwCOADetail on ServicesProductionMasterTable.CustomerCode = vwCOADetail.coa_detail_id " _
                                          & " where DocNo <> '' And IsNull(a.Qty,0) > 0 And vwCOADetail.coa_detail_id = " & Val(Me.cmbVendor.ActiveRow.Cells("coa_detail_id").Value.ToString)
                FillDropDown(Me.cmbInvoice, strSQL)

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim strSQL As String = String.Empty
            If Condition = "Master" Then
                strSQL = String.Empty
                'strSQL = "SELECT WIP.DocId, WIP.DocNo, WIP.DocDate, WIP.LocationId, WIP.CustomerCode, COA.detail_title as [Customer], COA.detail_code as [A/c Code], COA.account_type as [Type], COA.StateName as [Region], COA.CityName as [City], " _
                '        & " COA.Contact_Phone as [Phone],WIP.IGP_NO, IGPMasterTable.DocNo as IGPDocNo, WIP.Job_No, WIP.Remarks, WIP.TotalQty, WIP.TotalAmount, WIP.EntryDate, WIP.UserName " _
                '        & " FROM dbo.WIPMasterTable AS WIP INNER JOIN dbo.vwCOADetail AS COA ON WIP.CustomerCode = COA.coa_detail_id left outer join IGPMasterTable on WIP.DocId=IGPMasterTable.DocId  ORDER BY WIP.DocId DESC "


                'strSQL = "SELECT DIS.DocId, PRD.DocNo, DIS.DocDate, DIS.LocationId, DIS.CustomerCode, COA.detail_title as [Customer], COA.detail_code as [A/c Code], COA.account_type as [Type], COA.StateName as [Region], COA.CityName as [City], " _
                '       & " COA.Contact_Phone as [Phone],DIS.IGP_NO, IGPMasterTable.DocNo as IGPDocNo, DIS.Job_No, DIS.Remarks, DIS.TotalQty, DIS.TotalAmount, DIS.EntryDate, DIS.UserName " _
                '       & " FROM dbo.ServicesDispatchMasterTable AS DIS INNER JOIN dbo.vwCOADetail AS COA ON DIS.CustomerCode = COA.coa_detail_id left outer join ServicesProductionMasterTable on DIS.DocId=ServicesProductionMasterTable.DocId  ORDER BY DIS.DocId DESC "



                strSQL = "SELECT DIS.DocId, DIS.DocNo, DIS.DocDate, DIS.LocationId, DIS.CustomerCode, COA.detail_title as [Customer], COA.detail_code as [A/c Code], COA.account_type as [Type], COA.StateName as [Region], COA.CityName as [City], " _
                & " COA.Contact_Phone as [Phone],DIS.InvNo, ServicesProductionMasterTable.DocNo as ProDocNo, DIS.Job_No, DIS.Remarks, DIS.TotalQty, DIS.TotalAmount, DIS.EntryDate, DIS.UserName " _
                & " FROM dbo.ServicesDispatchMasterTable AS DIS INNER JOIN dbo.vwCOADetail AS COA ON DIS.CustomerCode = COA.coa_detail_id left outer join ServicesProductionMasterTable on DIS.DocId=ServicesProductionMasterTable.DocId  ORDER BY DIS.DocId DESC "

                Dim dt As New DataTable
                dt = GetDataTable(strSQL)
                dt.AcceptChanges()

                Me.grdSaved.DataSource = dt
                Me.grdSaved.RetrieveStructure()
                grdSaved.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
                grdSaved.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed


                Me.grdSaved.RootTable.Columns("DocId").Visible = False
                Me.grdSaved.RootTable.Columns("CustomerCode").Visible = False
                Me.grdSaved.RootTable.Columns("LocationId").Visible = False
                Me.grdSaved.RootTable.Columns("InvNo").Visible = False
                Me.grdSaved.RootTable.Columns("ProDocNo").Visible = False
                '

                Me.grdSaved.RootTable.Columns("DocDate").FormatString = "dd/MMM/yyyy"
                Me.grdSaved.RootTable.Columns("EntryDate").FormatString = "dd/MMM/yyyy"

                Me.grdSaved.RootTable.Columns("TotalQty").FormatString = "N" & DecimalPointInQty
                Me.grdSaved.RootTable.Columns("TotalAmount").FormatString = "N" & DecimalPointInValue
                Me.grdSaved.RootTable.Columns("TotalQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grdSaved.RootTable.Columns("TotalAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

                Me.grdSaved.RootTable.Columns("TotalQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns("TotalAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns("TotalQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns("TotalAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far



                Me.grdSaved.AutoSizeColumns()


            ElseIf Condition = "Detail" Then

                strSQL = String.Empty
                'strSQL = "SELECT WIP_D.LocationId, WIP_D.ArticleDefId, Art.ArticleDescription, Art.ArticleCode, WIP_D.PackQty, WIP_D.Qty, WIP_D.TotalQty, WIP_D.CurrentPrice, " _
                '& "  WIP_D.Price, WIP_D.TotalAmount, WIP_D.Comments FROM dbo.WIPDetailTable AS WIP_D INNER JOIN dbo.ArticleDefView AS Art ON WIP_D.ArticleDefId = Art.ArticleId WHERE WIP_D.DocId=" & DocId & " ORDER BY WIP_D.DocId "

                strSQL = "SELECT DDT.LocationId, DDT.ArticleDefId, Art.ArticleDescription, Art.ArticleCode, DDT.PackQty, DDT.Qty, DDT.TotalQty, DDT.CurrentPrice, IsNull(DDT.PackPrice,0) as PackPrice, " _
                & " DDT.Price, DDT.TotalAmount, DDT.Comments,0 issuedqty,isnull(PrdNo,0) as PrdNo , IsNull(DDT.Ref_DocId,0) as Ref_DocId, IsNull(DDT.Ref_DocDetailId,0) as Ref_DocDetailId FROM dbo.ServicesDispatchDetailTable AS DDT INNER JOIN dbo.ArticleDefView AS Art ON DDT.ArticleDefId = Art.ArticleId " _
                & " WHERE DDT.DocId=" & DocId & " ORDER BY DDT.DocId "

                Dim dt As New DataTable
                dt = GetDataTable(strSQL)
                dt.AcceptChanges()

                dt.Columns("TotalAmount").Expression = "IsNull(TotalQty,0) * IsNull(Price,0)"
                Me.grdGatePass.DataSource = dt

                Dim dtLocation As New DataTable
                dtLocation = GetDataTable("Select Location_Id, Location_Name From tblDefLocation Order By Sort_Order ASC")
                dtLocation.AcceptChanges()
                Me.grdGatePass.RootTable.Columns("LocationId").ValueList.PopulateValueList(dtLocation.DefaultView, "Location_Id", "Location_Name")
                ApplyGridSettings()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try

            If Me.cmbVendor.IsItemInList = False Then Return False
            If Me.cmbVendor.ActiveRow Is Nothing Then Return False

            If Me.cmbVendor.Value = 0 Then
                ShowErrorMessage("Please select Customer.")
                Me.cmbVendor.Focus()
                Return False
            End If

            If Me.grdGatePass.RowCount = 0 Then
                ShowErrorMessage("Record not in grid.")
                Return False
            End If

            'Task#13082015 Validate Job No if is nothing then ask for enter job no
            'If Me.txtJobNo.Text = String.Empty Then
            '    ShowErrorMessage("Please enter the Job no.")
            '    Me.txtJobNo.Focus()
            '    Return False
            'End If
            'End Task#13082015

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            DocId = 0
            Me.btnSave.Text = "&Save"
            Me.dtpDocDate.Value = Date.Now
            Me.txtDocNo.Text = GetDocumentNo()
            Me.cmbVendor.Rows(0).Activate()
            Me.cmbVendor.PerformAction(Win.UltraWinGrid.UltraComboAction.Dropdown)
            Me.txtJobNo.Text = String.Empty
            Me.cmbInvoice.SelectedValue = 0
            Me.txtRemarks.Text = String.Empty
            GetAllRecords("Master")
            GetAllRecords("Detail")
            FillCombos("Documents")
            Me.cmbInvoice.SelectedValue = 0
            Me.cmbVendor.Enabled = True
            Me.cmbVendor.Focus()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            Me.UltraTabControl1_SelectedTabChanged(Nothing, Nothing)
            ApplySecurity(SBUtility.Utility.EnumDataMode.[New])
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save


        Me.txtDocNo.Text = GetDocumentNo()

        Dim objCon As New OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Dim cmd As New OleDbCommand

        Try
            cmd.CommandType = CommandType.Text
            cmd.Connection = objCon
            cmd.Transaction = trans
            cmd.CommandTimeout = 120

            cmd.CommandText = ""
            cmd.CommandText = "INSERT INTO ServicesDispatchMasterTable(DocNo,DocDate,LocationId,CustomerCode,InvNo,Job_No,Remarks,TotalQty,TotalAmount,EntryDate,UserName) " _
            & " VALUES('" & Me.txtDocNo.Text & "',Convert(DateTime,'" & Me.dtpDocDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102),1," & Me.cmbVendor.Value & "," & Val(Me.cmbInvoice.SelectedValue) & ",N'" & Me.txtJobNo.Text.Replace("'", "''") & "',N'" & Me.txtRemarks.Text.Replace("'", "''") & "'," & Me.grdGatePass.GetTotal(Me.grdGatePass.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum) & "," & Me.grdGatePass.GetTotal(Me.grdGatePass.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum) & ",Convert(DateTime,'" & Date.Now.ToString("yyyy-M-d hh:mm:ss tt") & "',102),'" & LoginUserName.Replace("'", "''") & "') Select @@Identity"
            Dim objID As Object = cmd.ExecuteScalar

            SaveDetail(objID, trans)
            SaveDocDetail(objID, "1", trans)

            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Function

    Public Sub SaveDetail(ByVal DocId As Integer, ByVal trans As OleDbTransaction)
        Dim cmd As New OleDbCommand
        cmd.Connection = trans.Connection
        cmd.Transaction = trans
        cmd.CommandType = CommandType.Text
        Try

            For Each jsRow As Janus.Windows.GridEX.GridEXRow In Me.grdGatePass.GetRows
                cmd.CommandText = ""
                cmd.CommandText = "INSERT INTO ServicesDispatchDetailTable(DocId,LocationId,ArticleDefId,PackQty,Qty,TotalQty,CurrentPrice,PackPrice,Price,TotalAmount,Comments,PRdNo,Ref_DocId, Ref_DocDetailId) " _
                & " VALUES(" & DocId & "," & Val(jsRow.Cells("LocationId").Value.ToString) & "," & Val(jsRow.Cells("ArticleDefId").Value.ToString) & "," & Val(jsRow.Cells("PackQty").Value.ToString) & "," & Val(jsRow.Cells("Qty").Value.ToString) & "," & Val(jsRow.Cells("TotalQty").Value.ToString) & ",   " _
                & " " & Val(jsRow.Cells("CurrentPrice").Value.ToString) & "," & Val(jsRow.Cells("PackPrice").Value.ToString) & "," & Val(jsRow.Cells("Price").Value.ToString) & "," & Val(jsRow.Cells("TotalAmount").Value.ToString) & ",'" & jsRow.Cells("Comments").Value.ToString & "'," & Val(jsRow.Cells("PrdNo").Value.ToString) & "," & Val(jsRow.Cells("Ref_DocId").Value.ToString) & ", " & Val(jsRow.Cells("Ref_DocDetailId").Value.ToString) & ") "

                cmd.ExecuteNonQuery()
            Next

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Sub


    Private Sub SaveDocDetail(ByVal DocId As Integer, ByVal Mode As String, ByVal trans As OleDbTransaction)

        Try
            Dim strSQL As String = String.Empty
            Dim cmd As New OleDbCommand
            cmd.Connection = trans.Connection
            cmd.Transaction = trans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text

            strSQL = String.Empty
            'strSQL = "Update ServicesInvoiceDetailTable Set IssuedQty=" & IIf(Mode = "1", "IsNull(IssuedQty,0) + IsNull(a.Qty,0) ", "IsNull(IssuedQty,0) - IsNull(a.Qty,0) ") & ", IssuedTotalQty=" & IIf(Mode = "1", "IsNull(IssuedTotalQty,0) + IsNull(a.TotalQty,0) ", "IsNull(IssuedTotalQty,0) - IsNull(a.TotalQty,0) ") & "  From ServicesInvoiceDetailTable," _
            '& " (Select DocId as InvNo, DocDetailId, ArticleDefId,PackQty,ServicesProductionDetailTable.LocationId, SUM(IsNull(Qty,0))  as Qty, SUM(IsNull(TotalQty,0) as TotalQty  From ServicesProductionDetailTable " _
            '& " inner join ServicesProductionMasterTable on ServicesProductionDetailTable.DocId = ServicesProductionMasterTable.DocId WHERE ServicesProductionDetailTable.DocId= " & DocId & "  Group By  DocId,DocDetailId, ArticleDefId,PackQty,ServicesProductionDetailTable.LocationId) a" _
            '& " WHERE a.InvNo = ServicesInvoiceDetailTable.DocId AND a.ArticleDefId = ServicesInvoiceDetailTable.ArticleDefId And a.PackQty = ServicesInvoiceDetailTable.PackQty And a.LocationId = ServicesInvoiceDetailTable.LocationId a.Ref_DocDetailId = ServicesInvoiceDetailTable.DocDetailId "

            'strSQL = "Update ServicesInvoiceDetailTable Set IssuedQty=" & IIf(Mode = "1", "IsNull(IssuedQty,0) + IsNull(a.Qty,0) ", "IsNull(IssuedQty,0) - IsNull(a.Qty,0) ") & ", IssuedTotalQty=" & IIf(Mode = "1", "IsNull(IssuedTotalQty,0) + IsNull(a.TotalQty,0) ", "IsNull(IssuedTotalQty,0) - IsNull(a.TotalQty,0) ") & "  From ServicesInvoiceDetailTable," _
            '& "  (Select ServicesProductionDetailTable.DocId as InvNo, DocDetailId, ArticleDefId,PackQty,ServicesProductionDetailTable.LocationId, SUM(IsNull(Qty,0))  as Qty, " _
            '& " SUM(IsNull(ServicesProductionDetailTable.TotalQty,0)) as TotalQty  From ServicesProductionDetailTable  " _
            '& " inner join ServicesProductionMasterTable on ServicesProductionDetailTable.DocId = ServicesProductionMasterTable.DocId " _
            '& " WHERE ServicesProductionDetailTable.DocId= " & DocId & "  Group By  ServicesProductionDetailTable.DocId,DocDetailId, ArticleDefId,PackQty,ServicesProductionDetailTable.LocationId) a " _
            '& " WHERE a.InvNo = ServicesInvoiceDetailTable.DocId AND a.ArticleDefId = ServicesInvoiceDetailTable.ArticleDefId  " _
            '& " And a.PackQty = ServicesInvoiceDetailTable.PackQty And a.LocationId = ServicesInvoiceDetailTable.LocationId and ServicesInvoiceDetailTable.Ref_DocDetailId = a.DocDetailId "

            strSQL = "Update ServicesProductionDetailTable Set IssuedQty=" & IIf(Mode = "1", "IsNull(IssuedQty,0) + IsNull(a.Qty,0) ", "IsNull(IssuedQty,0) - IsNull(a.Qty,0) ") & ", IssuedTotalQty=" & IIf(Mode = "1", "IsNull(IssuedTotalQty,0) + IsNull(a.TotalQty,0) ", "IsNull(IssuedTotalQty,0) - IsNull(a.TotalQty,0) ") & "  From ServicesProductionDetailTable," _
            & " (Select Ref_DocId as IGP_No, Ref_DocDetailId, ArticleDefId,PackQty,ServicesDispatchDetailTable.LocationId, SUM(IsNull(ServicesDispatchDetailTable.Qty,0))  as Qty, SUM(ISNull(ServicesDispatchDetailTable.TotalQty,0)) as TotalQty  From ServicesDispatchDetailTable " _
            & " inner join ServicesDispatchMasterTable on ServicesDispatchDetailTable.DocId = ServicesDispatchMasterTable.DocId WHERE ServicesDispatchDetailTable.DocId= " & DocId & "  Group By  Ref_DocId, ArticleDefId,PackQty,ServicesDispatchDetailTable.LocationId,Ref_DocDetailId ) a" _
            & " WHERE a.IGP_No = ServicesProductionDetailTable.DocId AND a.ArticleDefId = ServicesProductionDetailTable.ArticleDefId And a.PackQty = ServicesProductionDetailTable.PackQty And a.LocationId = ServicesProductionDetailTable.LocationId And a.Ref_DocDetailId = ServicesProductionDetailTable.DocDetailId"


            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Sub

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

        'Me.txtDocNo.Text = GetDocumentNo()

        Dim objCon As New OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Dim cmd As New OleDbCommand

        Try
            cmd.CommandType = CommandType.Text
            cmd.Connection = objCon
            cmd.Transaction = trans
            cmd.CommandTimeout = 120

            cmd.CommandText = ""
            cmd.CommandText = "UPDATE ServicesDispatchMasterTable  SET  DocNo='" & Me.txtDocNo.Text & "',DocDate=Convert(DateTime,'" & Me.dtpDocDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102),LocationId=1,CustomerCode=" & Me.cmbVendor.Value & ",InvNo=" & Val(Me.cmbInvoice.SelectedValue) & ",Job_No=N'" & Me.txtJobNo.Text.Replace("'", "''") & "',Remarks=N'" & Me.txtRemarks.Text.Replace("'", "''") & "',TotalQty=" & Me.grdGatePass.GetTotal(Me.grdGatePass.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum) & ",TotalAmount=" & Me.grdGatePass.GetTotal(Me.grdGatePass.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum) & ",EntryDate=Convert(DateTime,'" & Date.Now.ToString("yyyy-M-d hh:mm:ss tt") & "',102),UserName='" & LoginUserName.Replace("'", "''") & "' WHERE DocId=" & Val(Me.grdSaved.GetRow.Cells("DocId").Value.ToString) & ""
            cmd.ExecuteNonQuery()

            SaveDocDetail(Val(Me.grdSaved.GetRow.Cells("DocId").Value.ToString), "0", trans)

            cmd.CommandText = ""
            cmd.CommandText = "Delete From ServicesDispatchDetailTable WHERE DocId=" & Val(Me.grdSaved.GetRow.Cells("DocId").Value.ToString) & ""
            cmd.ExecuteNonQuery()

            SaveDetail(Val(Me.grdSaved.GetRow.Cells("DocId").Value.ToString), trans)
            SaveDocDetail(Val(Me.grdSaved.GetRow.Cells("DocId").Value.ToString), "1", trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Function

    Function GetDocumentNo() As String
        Try
            'If Me.txtPONo.Text = "" Then
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("SD" + "-" + Microsoft.VisualBasic.Right(Me.dtpDocDate.Value.Year, 2) + "-", "ServicesDispatchMasterTable", "DocNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("SD" & "-" & Format(Me.dtpDocDate.Value, "yy") & Me.dtpDocDate.Value.Month.ToString("00"), 4, "ServicesDispatchMasterTable", "DocNo")
            Else
                Return GetNextDocNo("SD", 6, "ServicesDispatchMasterTable", "DocNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub frmIGP_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            FillCombos()
            FillCombos("Location")
            FillCombos("Item")
            FillCombos("UnitPack")
            FillCombos("Documents")
            ReSetControls()
            IsOpenForm = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub EditRecords()
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            Me.btnSave.Text = "&Update"
            DocId = Val(Me.grdSaved.GetRow.Cells("DocId").Value.ToString)
            Me.txtDocNo.Text = Me.grdSaved.GetRow.Cells("DocNo").Value.ToString
            Me.dtpDocDate.Value = Me.grdSaved.GetRow.Cells("DocDate").Value
            Me.cmbVendor.Value = Val(Me.grdSaved.GetRow.Cells("CustomerCode").Value.ToString)

            Me.cmbInvoice.SelectedValue = Val(Me.grdSaved.GetRow.Cells("InvNo").Value.ToString)
            If Me.cmbInvoice.SelectedValue Is Nothing Then
                Dim dt As DataTable
                dt = CType(Me.cmbInvoice.DataSource, DataTable)
                Dim row As DataRow
                row = dt.NewRow
                row(0) = Val(Me.grdSaved.GetRow.Cells("InvNo").Value.ToString)
                row(1) = Me.grdSaved.GetRow.Cells("ProDocNo").Value.ToString

                dt.Rows.Add(row)
                dt.AcceptChanges()
            End If

            Me.cmbInvoice.SelectedValue = Val(Me.grdSaved.GetRow.Cells("InvNo").Value.ToString)
            Me.txtJobNo.Text = Me.grdSaved.GetRow.Cells("Job_No").Value.ToString
            Me.txtRemarks.Text = Me.grdSaved.GetRow.Cells("Remarks").Value.ToString
            GetAllRecords("Detail")
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            ApplySecurity(SBUtility.Utility.EnumDataMode.Edit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try

            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdGatePass_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                    If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    ReSetControls()
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update1() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbItem_RowSelected(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbItem.RowSelected
        Try
            If IsOpenForm = False Then Exit Sub
            If Me.cmbItem.IsItemInList = False Then Exit Sub
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            FillCombos("UnitPack")
            If Me.cmbUnit.SelectedIndex = -1 Then
                Dim dt As New DataTable
                dt = CType(Me.cmbUnit.DataSource, DataTable)
                If dt IsNot Nothing Then
                    dt.AcceptChanges()
                    Dim dr As DataRow
                    dr = dt.NewRow
                    dr(0) = 0
                    dr(1) = "Loose"
                    dr(2) = 1
                    dt.Rows.Add(dr)
                    dr = dt.NewRow
                    dr(0) = 0
                    dr(1) = "Pack"
                    dr(2) = 1
                    dt.Rows.Add(dr)
                    dt.AcceptChanges()
                End If
            End If
            Me.txtPrice.Text = Val(Me.cmbItem.ActiveRow.Cells("Price").Value.ToString)
            'GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbUnit_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbUnit.LostFocus
        Try
            GetTotalAmount()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbUnit_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbUnit.SelectedIndexChanged
        Try
            If IsOpenForm = False Then Exit Sub
            If Me.cmbItem.IsItemInList = False Then Exit Sub
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            If Me.cmbUnit.SelectedIndex = -1 Then Exit Sub

            If Me.cmbUnit.Text <> "Loose" Then
                Me.txtPackQty.Text = CType(Me.cmbUnit.SelectedItem, DataRowView).Row("PackQty").ToString
            Else
                Me.txtPackQty.Text = 1
            End If
            GetTotalQty()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub GetTotalAmount()
        Try
            Dim dblTotalAmount As Double = (Val(Me.txtTotalQty.Text) * Val(Me.txtPrice.Text))
            Me.txtTotalAmount.Text = dblTotalAmount
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetTotalQty()
        Try
            Dim dblTotalQty As Double = IIf(Val(Me.txtPackQty.Text) > 1, (Val(Me.txtQty.Text) * Val(Me.txtPackQty.Text)), Val(Me.txtQty.Text))
            Me.txtTotalQty.Text = dblTotalQty
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValide_Add() As Boolean
        Try
            If Me.cmbItem.IsItemInList = False Then Return False
            If Me.cmbItem.ActiveRow Is Nothing Then Return False

            If Me.cmbItem.Value <= 0 Then
                ShowErrorMessage("Please select item.")
                Me.cmbItem.Focus()
                Return False
            End If
            If Val(Me.txtQty.Text) <= 0 Then
                ShowErrorMessage("Qty should be greater than zero.")
                Me.txtQty.Focus()
                Return False
            End If
            'If Val(Me.txtPrice.Text) = 0 Then
            '    ShowErrorMessage("Please enter the price.")
            '    Me.txtQty.Focus()
            '    Return False
            'End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            If IsValide_Add() = False Then Exit Sub
            If Not FindExistsItem(Val(Me.cmbItem.Value), Val(Me.txtPackQty.Text), Val(Me.cmbInvoice.SelectedValue)) = True Then
                AddItemToGrid()
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPackQty_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPackQty.LostFocus
        Try
            GetTotalAmount()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQty_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.LostFocus
        Try
            GetTotalAmount()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtQty_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.TextChanged
        Try
            GetTotalQty()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub DetailClear()
        Try
            Me.cmbItem.PerformAction(Win.UltraWinGrid.UltraComboAction.Dropdown)
            'me.cmbItem.
            Me.txtQty.Text = String.Empty
            Me.txtTotalQty.Text = String.Empty
            Me.txtPackPrice.Text = String.Empty
            Me.txtPrice.Text = String.Empty
            Me.txtTotalAmount.Text = String.Empty
            Me.cmbItem.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdSaved_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        Try
            If Me.grdSaved.Row < 0 Then
                Exit Sub
            Else
                EditRecords()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If Delete() = True Then DialogResult = Windows.Forms.DialogResult.Yes
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Task#13082015 gate pass selection changed event  (Ahmad Sharif)
    Private Sub cmbInvoice_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbInvoice.SelectedIndexChanged
        Try
            'If IsOpenForm = False Then Exit Sub
            'If Me.cmbInvoice.SelectedValue = 0 Then Exit Sub
            'LoadGatePass(Me.cmbInvoice.SelectedValue)
            If Me.cmbInvoice.SelectedIndex <= 0 Then Exit Sub
            '' Ali Ansari
            If IsOpenForm = False Then
                Exit Sub
            Else
                Me.LoadGatePass(Me.cmbInvoice.SelectedValue)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Task#13082015 select gate pass from combo and then  load gate pass data in grid
    Private Sub LoadGatePass(ByVal DocId As Integer)
        Try

            Dim i As Integer = 0I
            'Dim str As String = String.Empty
            Dim dtGrd As DataTable = CType(Me.grdGatePass.DataSource, DataTable)
            dtGrd.AcceptChanges()
            Dim dt As New DataTable
            Dim strSQL As String = String.Empty
            strSQL = "SELECT DDT.LocationId, DDT.ArticleDefId, Art.ArticleDescription, Art.ArticleCode, DDT.PackQty, IsNull(DDT.Qty,0)  - IsNull(DDT.IssuedQty,0) as Qty , IsNull(DDT.TotalQty,0)-IsNull(DDT.IssuedTotalQty,0) as TotalQty, DDT.CurrentPrice, IsNull(DDT.PackPrice,0) as PackPrice, " _
                & "  DDT.Price, DDT.TotalAmount, DDT.Comments,IsNull(DDT.IssuedQty,0) as IssuedQty,isnull(DDT.docid,0) prdNo, IsNull(DDT.DocId,0) as Ref_DocId, IsNull(DDT.DocDetailId,0) as Ref_DocDetailId FROM dbo.ServicesProductionDetailTable AS DDT INNER JOIN dbo.ArticleDefView AS Art ON DDT.ArticleDefId = Art.ArticleId WHERE DDT.DocId=" & DocId & " And  (IsNull(DDT.Qty,0)  - IsNull(DDT.IssuedQty,0)) > 0 ORDER BY DDT.DocId "
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()
            Dim dr As DataRow
            For Each row As DataRow In dt.Rows
                dr = dtGrd.NewRow
                dr(enmDetail.LocationId) = row("LocationId").ToString
                dr(enmDetail.ArticleDefId) = row("ArticleDefId").ToString
                dr(enmDetail.ArticleDescription) = row("ArticleDescription").ToString
                dr(enmDetail.ArticleCode) = row("ArticleCode").ToString
                dr(enmDetail.PackQty) = Val(row("PackQty").ToString)
                dr(enmDetail.Qty) = Val(row("Qty").ToString)
                dr(enmDetail.TotalQty) = Val(row("TotalQty").ToString)
                dr(enmDetail.CurrentPrice) = Val(row("CurrentPrice").ToString)
                dr(enmDetail.PackPrice) = Val(row("PackPrice").ToString)
                dr(enmDetail.Price) = Val(row("Price").ToString)
                dr(enmDetail.Comments) = row("Comments").ToString
                dr(enmDetail.PrdNo) = Val(row("prdNo").ToString)
                dr(enmDetail.RefDocId) = Val(row("Ref_DocId").ToString)
                dr(enmDetail.RefDocDetailId) = Val(row("Ref_DocDetailId").ToString)
                dtGrd.Rows.Add(dr)
                dtGrd.AcceptChanges()
            Next


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Task#13082015 add Refresh button on screen (Ahmad Sharif)
    'Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRe.Click
    '    Try
    '        Dim id As Integer = 0I

    '        id = Me.cmbInvoice.SelectedValue
    '        FillCombos("Documents")
    '        Me.cmbInvoice.SelectedValue = id

    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    'Delete item row from grdGatepass (Ahmad Sharif)
    Private Sub grdGatePass_ColumnButtonClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdGatePass.ColumnButtonClick
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If e.Column.Key = "btnDelete1" Then
                Me.grdGatePass.GetRow.Delete()
                grdGatePass.UpdateData()
                'GetTotal()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'on grid changing the pack qty or qty then calculate the total qty (Ahmad Sharif)
    Private Sub grdGatePass_CellUpdated(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdGatePass.CellUpdated
        Try

            Me.grdGatePass.UpdateData()
            Me.grdGatePass.GetRow.BeginEdit()
            If Val(Me.grdGatePass.GetRow.Cells("Qty").Value.ToString) <> 0 AndAlso Val(Me.grdGatePass.GetRow.Cells("PackQty").Value.ToString) <> 0 Then
                Me.grdGatePass.GetRow.Cells("TotalQty").Value = (Val(Me.grdGatePass.GetRow.Cells("Qty").Value.ToString) * Val(Me.grdGatePass.GetRow.Cells("PackQty").Value.ToString))
            ElseIf e.Column.DataMember = "TotalQty" Then
                If Not Val(Me.grdGatePass.GetRow.Cells("PackQty").Value.ToString) > 1 Then
                    Me.grdGatePass.GetRow.Cells("Qty").Value = Val(Me.grdGatePass.GetRow.Cells("TotalQty").Value.ToString)
                End If
            ElseIf e.Column.DataMember = "PackPrice" Then
                If getConfigValueByType("Apply40KgRate").ToString = "True" Then
                    Me.grdGatePass.GetRow.Cells(enmDetail.Price).Value = (Val(Me.grdGatePass.GetRow.Cells(enmDetail.PackPrice).Value.ToString) / 40)
                Else
                    If Val(Me.grdGatePass.GetRow.Cells(enmDetail.PackQty).Value.ToString) > 1 Then
                        Me.grdGatePass.GetRow.Cells(enmDetail.Price).Value = (Val(Me.grdGatePass.GetRow.Cells(enmDetail.PackPrice).Value.ToString) / Val(Me.grdGatePass.GetRow.Cells(enmDetail.PackQty).Value.ToString))
                    End If
                End If
            End If
            Me.grdGatePass.GetRow.EndEdit()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task#13082015

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0I

            id = Me.cmbInvoice.SelectedValue
            FillCombos("Documents")
            Me.cmbInvoice.SelectedValue = id
            id = Me.cmbVendor.ActiveRow.Cells(0).Value
            FillCombos()
            Me.cmbVendor.Value = id
            id = Me.cmbLocation.SelectedIndex
            FillCombos("Location")
            Me.cmbLocation.SelectedIndex = id
            If cmbItem.ActiveRow Is Nothing Then
                Me.cmbItem.Rows(0).Activate()
            End If
            id = Me.cmbItem.ActiveRow.Cells(0).Value
            FillCombos("Item")
            Me.cmbItem.Value = id
            'id = Me.cmbUnit.SelectedIndex
            'FillCombos("UnitPack")
            'Me.cmbUnit.SelectedIndex = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbVendor_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbVendor.ValueChanged
        Try
            Call FillCombos("Documents")
        Catch ex As Exception

        End Try
    End Sub
    Private Function FindExistsItem(ByVal ArticleDefID As String, ByVal PackQty As Double, ByVal PO_Id As Double) As Boolean
        Try
            'Task:2432 Added flg Marge Item.

            Dim dt As DataTable = CType(Me.grdGatePass.DataSource, DataTable)
            Dim dr() As DataRow
            dr = dt.Select("ArticleDefId='" & ArticleDefID & "' AND PackQty=" & Val(PackQty) & " And PrdNo = " & Val(cmbInvoice.SelectedValue))

            If dr.Length > 0 Then

                Return True
            Else
                Return False
            End If
            'End Task:2432
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub AddItemToGrid()
        Try

            Dim dtGrd As DataTable
            dtGrd = CType(Me.grdGatePass.DataSource, DataTable)
            dtGrd.AcceptChanges()
            Dim drGrd As DataRow
            drGrd = dtGrd.NewRow
            drGrd.Item(enmDetail.LocationId) = Me.cmbLocation.SelectedValue
            drGrd.Item(enmDetail.ArticleDefId) = Me.cmbItem.ActiveRow.Cells("ArticleId").Text.ToString
            drGrd.Item(enmDetail.ArticleDescription) = Me.cmbItem.ActiveRow.Cells("Item").Text.ToString
            drGrd.Item(enmDetail.ArticleCode) = Me.cmbItem.ActiveRow.Cells("Code").Text.ToString
            drGrd.Item(enmDetail.PackQty) = Val(Me.txtPackQty.Text)
            drGrd.Item(enmDetail.Qty) = Val(Me.txtQty.Text)
            drGrd.Item(enmDetail.TotalQty) = Val(Me.txtTotalQty.Text)
            drGrd.Item(enmDetail.CurrentPrice) = Val(Me.cmbItem.ActiveRow.Cells("Price").Text.ToString)
            drGrd(enmDetail.PackPrice) = Val(Me.txtPackPrice.Text)
            drGrd.Item(enmDetail.Price) = Val(Me.txtPrice.Text)
            drGrd.Item(enmDetail.TotalAmount) = Val(Me.txtTotalAmount.Text)
            drGrd.Item(enmDetail.Comments) = Comments
            drGrd.Item(enmDetail.IssuedQty) = 0
            drGrd.Item(enmDetail.PrdNo) = Me.cmbInvoice.SelectedValue
            dtGrd.AcceptChanges()
            dtGrd.Rows.InsertAt(drGrd, 0)

            dtGrd.AcceptChanges()
            DetailClear()
            cmbVendor.Enabled = False
        Catch ex As Exception
            Throw ex
        End Try


    End Sub

    Private Sub txtPackPrice_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPackPrice.TextChanged
        Try
            If getConfigValueByType("Apply40KgRate").ToString = "False" Then
                If Val(Me.txtPackQty.Text) > 1 Then
                    txtPrice.Text = Val(Me.txtPackPrice.Text) / (Me.txtPackQty.Text)
                Else
                    txtPrice.Text = Val(txtPrice.Text)
                End If
            Else
                txtPrice.Text = (Val(Me.txtPackPrice.Text) / 40)
            End If
            GetTotalAmount()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtPackQty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTotalQty.KeyPress, txtQty.KeyPress, txtPrice.KeyPress, txtPackQty.KeyPress, txtPackPrice.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If Me.UltraTabControl1.SelectedTab.Index = 0 Then
                Me.btnSave.Visible = True
                If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                    Me.btnDelete.Visible = False
                Else
                    Me.btnDelete.Visible = True
                End If
            Else
                Me.btnDelete.Visible = True
                Me.btnSave.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@DocId", Val(Me.grdSaved.GetRow.Cells("DocId").Value.ToString))
            ShowReport("rptServiceDispatch")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtPackQty_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPackQty.TextChanged
        Try
            GetTotalQty()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPrice_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice.TextChanged
        Try
            GetTotalAmount()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
