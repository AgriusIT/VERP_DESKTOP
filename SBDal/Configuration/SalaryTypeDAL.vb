''TaskM126121 12/6/2015 Imran Ali Config Based Deduction Against Salary 
'2015-08-06 Task# 201508004 Ali Ansari Add Income Tax Excemption and Income Tax Deduction
''21-9-2015 Task:219151 Imran Ali: Enhancement Apply Value And Existing Account.
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Public Class SalaryTypeDAL
    Public Function AddSalaryType(ByVal SalaryType As SalaryType) As Integer
        Dim con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        con.Open()
        Dim trans As SqlClient.SqlTransaction = con.BeginTransaction
        Try
            Dim str As String = String.Empty
            'str = "INSERT INTO SalaryExpenseType(SalaryExpType, SalaryDeduction, Active, SortOrder, AccountId, flgAdvance) Values(N'" & SalaryType.SalaryExpType & "', " & IIf(SalaryType.Deduction = True, 1, 0) & ", " & IIf(SalaryType.Active = True, 1, 0) & ", " & SalaryType.SortOrder & ", " & SalaryType.AccountId & ", " & IIf(SalaryType.Advance = True, 1, 0) & ") Select @@Identity"
            'SalaryType.SalaryExpTypeId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            'str = "INSERT INTO SalaryExpenseType(SalaryExpType, SalaryDeduction, Active, SortOrder, AccountId, flgAdvance,GrossSalaryType) Values(N'" & SalaryType.SalaryExpType & "', " & IIf(SalaryType.Deduction = True, 1, 0) & ", " & IIf(SalaryType.Active = True, 1, 0) & ", " & SalaryType.SortOrder & ", " & SalaryType.AccountId & ", " & IIf(SalaryType.Advance = True, 1, 0) & "," & IIf(SalaryType.GrossSalaryType = True, 1, 0) & ") Select @@Identity"
            'SalaryType.SalaryExpTypeId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)

            ''TaskM126121 Added Field Deduction Against Salary 

            If CheckDuplicateRecord(SalaryType.SalaryExpType, SalaryType.SalaryExpTypeId, trans) = False Then
                Throw New Exception("[" & SalaryType.SalaryExpType.Replace("'", "''") & "] duplication record exists")
            End If

            'DeductionAgainstLeaves
            '  Marked Against Task# 201508004 Ali Ansari to add Income Tax Deduction and Income Tax Exemption
            'Marked Against Task# 201507004 Ali Ansari to add Leave Against Deduction
            'str = "INSERT INTO SalaryExpenseType(SalaryExpType, SalaryDeduction, Active, SortOrder, AccountId, flgAdvance,GrossSalaryType,DeductionAgainstSalary) Values(N'" & SalaryType.SalaryExpType.Replace("'", "''") & "', " & IIf(SalaryType.Deduction = True, 1, 0) & ", " & IIf(SalaryType.Active = True, 1, 0) & ", " & SalaryType.SortOrder & ", " & SalaryType.AccountId & ", " & IIf(SalaryType.Advance = True, 1, 0) & "," & IIf(SalaryType.GrossSalaryType = True, 1, 0) & "," & IIf(SalaryType.DeductionAgainstSalary = True, 1, 0) & ") Select @@Identity"
            'Marked Against Task# 201507004 Ali Ansari to add Leave Against Deduction
            'Altered Against Task# 201507004 Ali Ansari to add Leave Against Deduction and Overtime Allowance
            '            str = "INSERT INTO SalaryExpenseType(SalaryExpType, SalaryDeduction, Active, SortOrder, AccountId, flgAdvance,GrossSalaryType,DeductionAgainstSalary,DeductionAgainstLeaves,AllowanceOverTime) Values(N'" & SalaryType.SalaryExpType.Replace("'", "''") & "', " & IIf(SalaryType.Deduction = True, 1, 0) & ", " & IIf(SalaryType.Active = True, 1, 0) & ", " & SalaryType.SortOrder & ", " & SalaryType.AccountId & ", " & IIf(SalaryType.Advance = True, 1, 0) & "," & IIf(SalaryType.GrossSalaryType = True, 1, 0) & "," & IIf(SalaryType.DeductionAgainstSalary = True, 1, 0) & "," & IIf(SalaryType.DeductionAgainstLeaves = True, 1, 0) & "," & IIf(SalaryType.AllowanceAgainstOT = True, 1, 0) & ") Select @@Identity"
            'Altered Against Task# 201507004 Ali Ansari to add Leave Against Deduction and Overtime Allowance
            'str = "INSERT INTO SalaryExpenseType(SalaryExpType, SalaryDeduction, Active, SortOrder, AccountId, flgAdvance,GrossSalaryType,DeductionAgainstSalary,DeductionAgainstLeaves,AllowanceOverTime,DeductionAgainsIncomeTax,IncomeTaxExempted) Values(N'" & SalaryType.SalaryExpType.Replace("'", "''") & "', " & IIf(SalaryType.Deduction = True, 1, 0) & ", " & IIf(SalaryType.Active = True, 1, 0) & ", " & SalaryType.SortOrder & ", " & SalaryType.AccountId & ", " & IIf(SalaryType.Advance = True, 1, 0) & "," & IIf(SalaryType.GrossSalaryType = True, 1, 0) & "," & IIf(SalaryType.DeductionAgainstSalary = True, 1, 0) & "," & IIf(SalaryType.DeductionAgainstLeaves = True, 1, 0) & "," & IIf(SalaryType.AllowanceAgainstOT = True, 1, 0) & "," & IIf(SalaryType.IncomeTaxDeduction = True, 1, 0) & "," & IIf(SalaryType.IncomeTaxExcemption = True, 1, 0) & ") Select @@Identity"
            'Altered Against Task# 201507004 Ali Ansari to add Leave Against Deduction and Overtime Allowance
            'TASKM219151 Added Field ApplyValue And ExistingAccount
            str = "INSERT INTO SalaryExpenseType(SalaryExpType, SalaryDeduction, Active, SortOrder, AccountId, flgAdvance,GrossSalaryType,DeductionAgainstSalary,DeductionAgainstLeaves,AllowanceOverTime,DeductionAgainsIncomeTax,IncomeTaxExempted,ApplyValue,ExistingAccount,SiteVisitAllowance) Values(N'" & SalaryType.SalaryExpType.Replace("'", "''") & "', " & IIf(SalaryType.Deduction = True, 1, 0) & ", " & IIf(SalaryType.Active = True, 1, 0) & ", " & SalaryType.SortOrder & ", " & SalaryType.AccountId & ", " & IIf(SalaryType.Advance = True, 1, 0) & "," & IIf(SalaryType.GrossSalaryType = True, 1, 0) & "," & IIf(SalaryType.DeductionAgainstSalary = True, 1, 0) & "," & IIf(SalaryType.DeductionAgainstLeaves = True, 1, 0) & "," & IIf(SalaryType.AllowanceAgainstOT = True, 1, 0) & "," & IIf(SalaryType.IncomeTaxDeduction = True, 1, 0) & "," & IIf(SalaryType.IncomeTaxExcemption = True, 1, 0) & ",N'" & SalaryType.ApplyValue.Replace("'", "''") & "'," & IIf(SalaryType.ExistingAccount = True, 1, 0) & "," & IIf(SalaryType.SiteVisitAllowance = True, 1, 0) & ") Select @@Identity"
            'END TASK219151
            SalaryType.SalaryExpTypeId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            'End Task126151
            'Altered Against Task# 201507004 Ali Ansari to add Leave Against Deduction
            '  Marked Against Task# 201508004 Ali Ansari to add Income Tax Deduction and Income Tax Exemption
            '  Altered Against Task# 201508004 Ali Ansari to add Income Tax Deduction and Income Tax Exemption

            If SalaryType.ExistingAccount = False Then
                If SalaryType.AccountId = 0 AndAlso SalaryType.sub_sub_code.Length > 0 Then

                    str = String.Empty
                    '                str = "INSERT INTO tblCOAMainSubSubDetail(main_sub_sub_id, detail_code, detail_title, Active)  " _
                    '               & " VALUES(" & SalaryType.main_sub_sub_id & ", '" & GetCOACode(SalaryType.main_sub_sub_id, SalaryType.sub_sub_code, trans) & "', '" & SalaryType.SalaryExpType.Replace("'", "''") & "',1) Select @@Identity"
                    str = "INSERT INTO tblCOAMainSubSubDetail(main_sub_sub_id, detail_code, detail_title, Active)  " _
                   & " VALUES(" & SalaryType.main_sub_sub_id & ", '" & GetCOACode(SalaryType.main_sub_sub_id, SalaryType.sub_sub_code, trans) & "', '" & SalaryType.SalaryExpType.Replace("'", "''") & "',1) Select @@Identity"

                    Dim intSubSubAcId As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)

                    str = String.Empty
                    str = "Update  SalaryExpenseType Set AccountId=" & intSubSubAcId & " WHERE  SalaryExpTypeId=" & SalaryType.SalaryExpTypeId & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                End If
            Else

                str = String.Empty
                str = "Update  SalaryExpenseType Set AccountId=" & SalaryType.AccountId & " WHERE  SalaryExpTypeId=" & SalaryType.SalaryExpTypeId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                'END TASK219151 

            End If

            trans.Commit()
            Return SalaryType.SalaryExpTypeId
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            con.Close()
        End Try
    End Function
    Public Function UpdateSalaryType(ByVal SalaryType As SalaryType) As Boolean
        Dim con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        con.Open()
        Dim trans As SqlClient.SqlTransaction = con.BeginTransaction
        Try
            Dim str As String = String.Empty
            'str = "Update SalaryExpenseType Set SalaryExpType=N'" & SalaryType.SalaryExpType & "', SalaryDeduction=" & IIf(SalaryType.Deduction = True, 1, 0) & ", Active=" & IIf(SalaryType.Active = True, 1, 0) & ", SortOrder=" & SalaryType.SortOrder & ", AccountId=" & SalaryType.AccountId & ", flgAdvance=" & IIf(SalaryType.Advance = True, 1, 0) & ",GrossSalaryType=" & IIf(SalaryType.GrossSalaryType = True, 1, 0) & " WHERE SalaryExpTypeId=" & SalaryType.SalaryExpTypeId & ""
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ''TaskM126121 Added Field Deduction Against Salary 

            If CheckDuplicateRecord(SalaryType.SalaryExpType, SalaryType.SalaryExpTypeId, trans) = False Then
                Throw New Exception("[" & SalaryType.SalaryExpType.Replace("'", "''") & "] duplication record exists")
            End If
            'Marked Against Task# 201508004 Ali Ansari to add Leave Against Deduction
            'Marked Against Task# 201507004 Ali Ansari to add Leave Against Deduction
            'str = "Update SalaryExpenseType Set SalaryExpType=N'" & SalaryType.SalaryExpType.Replace("'", "''") & "', SalaryDeduction=" & IIf(SalaryType.Deduction = True, 1, 0) & ", Active=" & IIf(SalaryType.Active = True, 1, 0) & ", SortOrder=" & SalaryType.SortOrder & ", AccountId=" & SalaryType.AccountId & ", flgAdvance=" & IIf(SalaryType.Advance = True, 1, 0) & ",GrossSalaryType=" & IIf(SalaryType.GrossSalaryType = True, 1, 0) & ",DeductionAgainstSalary=" & IIf(SalaryType.DeductionAgainstSalary = True, 1, 0) & " WHERE SalaryExpTypeId=" & SalaryType.SalaryExpTypeId & ""
            'Marked Against Task# 201507004 Ali Ansari to add Leave Against Deduction
            'Altered Against Task# 201507004 Ali Ansari to add Leave Against Deduction and OverTime Allowance
            '            str = "Update SalaryExpenseType Set SalaryExpType=N'" & SalaryType.SalaryExpType.Replace("'", "''") & "', SalaryDeduction=" & IIf(SalaryType.Deduction = True, 1, 0) & ", Active=" & IIf(SalaryType.Active = True, 1, 0) & ", SortOrder=" & SalaryType.SortOrder & ", AccountId=" & SalaryType.AccountId & ", flgAdvance=" & IIf(SalaryType.Advance = True, 1, 0) & ",GrossSalaryType=" & IIf(SalaryType.GrossSalaryType = True, 1, 0) & ",DeductionAgainstSalary=" & IIf(SalaryType.DeductionAgainstSalary = True, 1, 0) & ", DeductionAgainstLeaves=" & IIf(SalaryType.DeductionAgainstLeaves = True, 1, 0) & ", AllowanceOverTime=" & IIf(SalaryType.AllowanceAgainstOT = True, 1, 0) & " WHERE SalaryExpTypeId=" & SalaryType.SalaryExpTypeId & ""
            'Altered Against Task# 201507004 Ali Ansari to add Leave Against Deductin()
            'Marked Against Task# 201508004 Ali Ansari to add Leave Against Deduction
            'Altered Against Task# 201508004 Ali Ansari to add Leave Against Deduction
            'str = "Update SalaryExpenseType Set SalaryExpType=N'" & SalaryType.SalaryExpType.Replace("'", "''") & "', SalaryDeduction=" & IIf(SalaryType.Deduction = True, 1, 0) & ", Active=" & IIf(SalaryType.Active = True, 1, 0) & ", SortOrder=" & SalaryType.SortOrder & ", AccountId=" & SalaryType.AccountId & ", flgAdvance=" & IIf(SalaryType.Advance = True, 1, 0) & ",GrossSalaryType=" & IIf(SalaryType.GrossSalaryType = True, 1, 0) & ",DeductionAgainstSalary=" & IIf(SalaryType.DeductionAgainstSalary = True, 1, 0) & ", DeductionAgainstLeaves=" & IIf(SalaryType.DeductionAgainstLeaves = True, 1, 0) & ", AllowanceOverTime=" & IIf(SalaryType.AllowanceAgainstOT = True, 1, 0) & ", DeductionAgainsIncomeTax=" & IIf(SalaryType.IncomeTaxDeduction = True, 1, 0) & ", IncomeTaxExempted=" & IIf(SalaryType.IncomeTaxExcemption = True, 1, 0) & " WHERE SalaryExpTypeId=" & SalaryType.SalaryExpTypeId & ""
            'Altered Against Task# 201508004 Ali Ansari to add Leave Against Deduction
            'TASK219151 Added Field ApplyValue And ExistingAccount 
            str = "Update SalaryExpenseType Set SalaryExpType=N'" & SalaryType.SalaryExpType.Replace("'", "''") & "', SalaryDeduction=" & IIf(SalaryType.Deduction = True, 1, 0) & ", Active=" & IIf(SalaryType.Active = True, 1, 0) & ", SortOrder=" & SalaryType.SortOrder & ", AccountId=" & SalaryType.AccountId & ", flgAdvance=" & IIf(SalaryType.Advance = True, 1, 0) & ",GrossSalaryType=" & IIf(SalaryType.GrossSalaryType = True, 1, 0) & ",DeductionAgainstSalary=" & IIf(SalaryType.DeductionAgainstSalary = True, 1, 0) & ", DeductionAgainstLeaves=" & IIf(SalaryType.DeductionAgainstLeaves = True, 1, 0) & ", AllowanceOverTime=" & IIf(SalaryType.AllowanceAgainstOT = True, 1, 0) & ", DeductionAgainsIncomeTax=" & IIf(SalaryType.IncomeTaxDeduction = True, 1, 0) & ", IncomeTaxExempted=" & IIf(SalaryType.IncomeTaxExcemption = True, 1, 0) & ", ApplyValue=N'" & SalaryType.ApplyValue.Replace("'", "''") & "', ExistingAccount=" & IIf(SalaryType.ExistingAccount = True, 1, 0) & ", SiteVisitAllowance =" & IIf(SalaryType.SiteVisitAllowance = True, 1, 0) & " WHERE SalaryExpTypeId=" & SalaryType.SalaryExpTypeId & ""
            'END TASK219151
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'End TaskM126151



            If SalaryType.ExistingAccount = False Then
                If SalaryType.AccountId = 0 AndAlso SalaryType.sub_sub_code.Length > 0 Then

                    str = String.Empty
                    str = "INSERT INTO tblCOAMainSubSubDetail(main_sub_sub_id, detail_code, detail_title, Active)  " _
                    & " VALUES(" & SalaryType.main_sub_sub_id & ", '" & GetCOACode(SalaryType.main_sub_sub_id, SalaryType.sub_sub_code, trans) & "', '" & SalaryType.SalaryExpType.Replace("'", "''") & "',1) Select @@Identity"
                    Dim intSubSubAcId As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)

                    str = String.Empty
                    str = "Update  SalaryExpenseType Set AccountId=" & intSubSubAcId & " WHERE  SalaryExpTypeId=" & SalaryType.SalaryExpTypeId & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                Else

                    str = String.Empty
                    str = "Select * From vwCOADetail WHERE coa_detail_id=" & SalaryType.AccountId & " AND main_sub_sub_id=" & SalaryType.main_sub_sub_id & ""
                    Dim int As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)

                    If int > 0 Then

                        str = String.Empty
                        str = "Update tblCOAMainSubSubDetail Set Detail_title='" & SalaryType.SalaryExpType.Replace("'", "''") & "' WHERE coa_detail_id=" & SalaryType.AccountId & ""
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                    Else
                        str = String.Empty
                        str = "Select sub_sub_code From tblCOAMainSubSub WHERE main_sub_sub_Id=" & SalaryType.main_sub_sub_id & ""
                        Dim dt As New DataTable
                        dt = UtilityDAL.GetDataTable(str, trans)
                        dt.AcceptChanges()

                        Dim strCOAMainAcCode As String = String.Empty
                        If dt.Rows.Count > 0 Then
                            strCOAMainAcCode = dt.Rows(0).Item(0).ToString
                        End If

                        str = String.Empty
                        str = "Update tblCOAMainSubSubDetail Set Detail_title='" & SalaryType.SalaryExpType.Replace("'", "''") & "', Detail_Code='" & GetCOACode(SalaryType.main_sub_sub_id, strCOAMainAcCode) & "', main_sub_sub_id=" & SalaryType.main_sub_sub_id & " WHERE coa_detail_id=" & SalaryType.AccountId & ""
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                    End If
                End If
            Else

                str = String.Empty
                str = "Update  SalaryExpenseType Set AccountId=" & SalaryType.AccountId & " WHERE  SalaryExpTypeId=" & SalaryType.SalaryExpTypeId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            End If
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            con.Close()
        End Try
    End Function
    Public Function Delete(ByVal SalaryType As SalaryType) As Boolean
        Dim con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        con.Open()
        Dim trans As SqlClient.SqlTransaction = con.BeginTransaction
        Try
            Dim str As String = String.Empty

            'str = String.Empty
            'str = "Delete From tblCOAMainSubSubDetail where coa_detail_id=" & SalaryType.AccountId & ""
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = "Select Count(*) From SalariesExpenseDetailTable WHERE SalaryExpTypeId=" & SalaryType.SalaryExpTypeId & ""
            Dim intID As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)

            If intID > 0 Then
                Throw New Exception("Dependent Record Exist " & SalaryType.SalaryExpType.Replace("'", "''") & "")
            End If


            str = String.Empty
            str = "Delete From SalaryExpenseType WHERE SalaryExpTypeId=" & SalaryType.SalaryExpTypeId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            con.Close()
        End Try
    End Function
    Public Function CheckDuplicateRecord(ByVal SalaryExpType As String, ByVal SalaryExpTypeId As Integer, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Select Count(*) From SalaryExpenseType WHERE SalaryExpType=N'" & SalaryExpType.Replace("'", "''") & "' AND SalaryExpTypeId <> " & SalaryExpTypeId & ""
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strSQL, trans)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return True
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetAllRecord() As DataTable
        Try
            Dim str As String = String.Empty

            ''TaskM126121 Added Field Deduction Against Salary 
            'Marked Against Task# 201507004 Ali Ansari to add Leave Against Deduction
            'str = "Select SalaryExpTypeId, Convert(NVARCHAR,SalaryExpType) as SalaryExpType, SalaryDeduction, isnull(flgAdvance,0) as Advance, AccountId, detail_title, IsNull(GrossSalaryType,0) as GrossSalaryType, IsNull(DeductionAgainstSalary,0) as DeductionAgainstSalary, SalaryExpenseType.Active, SortOrder, main_sub_sub_id From SalaryExpenseType LEFT OUTER JOIN vwCoaDetail On vwCoaDetail.coa_detail_id = SalaryExpenseType.AccountId "
            'Marked Against Task# 201507004 Ali Ansari to add Leave Against Deduction
            '  Marked Against Task# 201508004 Ali Ansari to add Income Tax Deduction and Income Tax Exemption
            '  Altered Against Task# 201507004 Ali Ansari to add Leave Against Deduction
            'str = "Select SalaryExpTypeId, Convert(NVARCHAR,SalaryExpType) as SalaryExpType, SalaryDeduction, isnull(flgAdvance,0) as Advance, SalaryExpenseType.AccountId, detail_title, IsNull(GrossSalaryType,0) as GrossSalaryType, IsNull(DeductionAgainstLeaves,0) as DeductionAgainstLeaves, SalaryExpenseType.Active, SortOrder, main_sub_sub_id, IsNull(DeductionAgainstSalary,0) as DeductionAgainstSalary, IsNull(AllowanceOverTime,0) as AllowanceAgainstOT From SalaryExpenseType LEFT OUTER JOIN vwCoaDetail On vwCoaDetail.coa_detail_id = SalaryExpenseType.AccountId "
            'Altered Against Task# 201507004 Ali Ansari to add Leave Against Deduction
            '  Marked Against Task# 201508004 Ali Ansari to add Income Tax Deduction and Income Tax Exemption
            'end TaskM126151

            '  Altered Against Task# 201508004 Ali Ansari to add Income Tax Deduction and Income Tax Exemption
            'Task:219151 Added Field ApplyValue And Existing Account
            str = "Select SalaryExpTypeId, Convert(NVARCHAR,SalaryExpType) as SalaryExpType, SalaryDeduction, isnull(flgAdvance,0) as Advance, SalaryExpenseType.AccountId, detail_title, IsNull(GrossSalaryType,0) as GrossSalaryType, IsNull(DeductionAgainstLeaves,0) as DeductionAgainstLeaves, IsNull(ApplyValue,'Fixed') as ApplyValue, IsNull(ExistingAccount,0) as ExistingAccount, SalaryExpenseType.Active, SortOrder, main_sub_sub_id, IsNull(DeductionAgainstSalary,0) as DeductionAgainstSalary, IsNull(AllowanceOverTime,0) as AllowanceAgainstOT,IsNull(SiteVisitAllowance,0) as SiteVisitAllowance,IsNull(DeductionAgainsIncomeTax,0) as DeductionAgainstIncomeTax,IsNull(IncomeTaxExempted,0) as IncomeTaxExempted  From SalaryExpenseType LEFT OUTER JOIN vwCoaDetail On vwCoaDetail.coa_detail_id = SalaryExpenseType.AccountId "
            'End TASK:219151
            '  Altered Against Task# 201508004 Ali Ansari to add Income Tax Deduction and Income Tax Exemption
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetCOACode(ByVal intMainAcId As Integer, ByVal strMainAcCode As String, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As String
        Try

            Dim strSQL As String = String.Empty
            Dim serial As Integer = 0I
            strSQL = "Select IsNull(Max(Right(detail_code,5)),0)+1 as SerialNo From tblCOAMainSubSubDetail WHERE LEFT(Detail_Code," & strMainAcCode.Length & ")='" & strMainAcCode & "' AND main_sub_sub_Id=" & intMainAcId & ""
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strSQL, trans)
            dt.AcceptChanges()


            If dt.Rows.Count > 0 Then
                serial = Val(dt.Rows(0).Item(0).ToString)
            End If

            Dim strCOACode As String = String.Empty
            strCOACode = strMainAcCode & "-" & CStr(Right("00000" & CStr(serial), 5))

            Return strCOACode

        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
