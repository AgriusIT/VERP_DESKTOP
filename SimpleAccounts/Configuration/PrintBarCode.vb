Imports Neodynamic.SDK.Barcode
Imports System.Drawing.Rectangle
Imports System.Drawing.Printing
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports SBDal
Imports SBModel
Public Class PrintBarCode
    Public Articleid As Integer = 0
    Public NoOfCopies As Integer = 1
    Dim flgProName As Boolean = False
    Dim flgProPrice As Boolean = False
    Dim flgProCode As Boolean = False
    Dim flgVendorCode As Boolean = False
    Dim flgPackQty As Boolean = False
    Dim flgPrintDate As Boolean = False
    Dim flgCompanyName As Boolean = False
    Dim flgProSize As Boolean = False
    Dim PS As New System.Drawing.Printing.PrinterSettings
    Public Sub PrintBarCode()
        Try
            'PrintBarCodeInEditMode()
            Dim PrinterName As String = ""
            Dim PrintCount As Integer = 1
            PrinterName = getConfigValueByType("PrinterNameForBarCode").ToString()
            PrintCount = Val(getConfigValueByType("PrintCountForBarCode").ToString())
            If PrinterName = "" Then
                PrinterName = PS.PrinterName
            End If
            Dim pd As New PrintDocument()
            AddHandler pd.PrintPage, AddressOf Me.pd_PrintPage
            pd.PrinterSettings.PrinterName = PrinterName
            pd.PrinterSettings.Copies = Math.Round(NoOfCopies / 2, 0, MidpointRounding.AwayFromZero)
            pd.PrinterSettings.DefaultPageSettings.PaperSize = New Printing.PaperSize("Custom Paper size", 4, 1.1)
            pd.Print()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub pd_PrintPage(sender As Object, e As PrintPageEventArgs)

        Try
            Dim str As String = ""
            Dim pd As New PrintDocument()
            Dim cbProname As Boolean = False
            Dim cbProPrice As Boolean = False
            Dim cbProductCode As Boolean = False
            Dim cbVendorCode As Boolean = False
            Dim cbPackQuantity As Boolean = False
            'Dim cbValues As String = String.Empty
            Dim BarCodeDate As String = Date.Now.ToString("ddMMyy")

            '// Sticker design variables
            '// R@!Shahid 05-Aug-18

            Dim StickerCols As Int32 = 2
            Dim stickersGap As Int32 = 10
            Dim TopGap As Int32 = Val(getConfigValueByType("PrintBarcodeTopGap").ToString())
            Dim LeftGap As Int32 = Val(getConfigValueByType("PrintBarcodeLeftGap").ToString())
            Dim StickerWidth As Int32 = 200

            Dim row1 As Int32 = 0 + TopGap
            Dim row2 As Int32 = 10 + TopGap
            Dim row3 As Int32 = 25 + TopGap
            Dim row4 As Int32 = 40 + TopGap
            Dim row5 As Int32 = 60 + TopGap
            Dim row6 As Int32 = 80 + TopGap

            Dim col1 As Int32 = 0 + LeftGap
            Dim col2 As Int32 = 40 + LeftGap
            Dim Col3 As Int32 = StickerWidth - 15


            Dim fnt1 As New Font("Arial", 7.0F, FontStyle.Bold)
            Dim fnt2 As New Font("Arial", 7.0F, FontStyle.Regular)

            Dim sf As New Drawing.StringFormat
            Dim sf2 As New Drawing.StringFormat
            Dim sfdate As New Drawing.StringFormat
            sfdate.LineAlignment = StringAlignment.Center
            sfdate.Alignment = StringAlignment.Center
            sfdate.FormatFlags = StringFormatFlags.DirectionVertical
            sf2.Alignment = StringAlignment.Far
            sf.Alignment = StringAlignment.Center
            sf.FormatFlags = StringFormatFlags.DirectionVertical

            '// End Design Variables

            'cbValues = getConfigValueByType("BARCodeDisplayInformation").ToString()
            'If cbValues.Length > 0 Then
            '    Dim arday() As String = cbValues.Split("|")
            '    If arday.Length > 4 Then
            '        'PN&False|PP&True|PC&True|VC&True|PQ&False
            '        cbProname = Convert.ToBoolean(arday(0).Trim.Substring(3))
            '        cbProPrice = Convert.ToBoolean(arday(1).Trim.Substring(3))
            '        cbProductCode = Convert.ToBoolean(arday(2).Trim.Substring(3))
            '        cbVendorCode = Convert.ToBoolean(arday(3).Trim.Substring(3))
            '        cbPackQuantity = Convert.ToBoolean(arday(4).Trim.Substring(3))
            '    End If
            'End If

            Dim cbValues As String = String.Empty
            cbValues = getConfigValueByType("BARCodeDisplayInformation").ToString()
            If cbValues.Length > 0 Then
                Dim arday() As String = cbValues.Split("|")
                If arday.Length > 0 Then
                    flgProName = Convert.ToBoolean(arday(0).Trim.Substring(3))
                    flgProPrice = Convert.ToBoolean(arday(1).Trim.Substring(3))
                    flgProCode = Convert.ToBoolean(arday(2).Trim.Substring(3))
                    flgVendorCode = Convert.ToBoolean(arday(3).Trim.Substring(3))
                    flgPackQty = Convert.ToBoolean(arday(4).Trim.Substring(3))
                    flgPrintDate = Convert.ToBoolean(arday(5).Trim.Substring(3))
                    flgCompanyName = Convert.ToBoolean(arday(6).Trim.Substring(3))
                    flgProSize = Convert.ToBoolean(arday(7).Trim.Substring(3))
                End If
            End If

            Dim DT As New DataTable

            DT = GetDataTable("Select ArticleId, ArticleDescription ,ArticleGenderName, ArticleSizeName, ArticleCode,PackQty , ArticleBARCode ,SalePrice  from ArticleDefView where ArticleId = " & Articleid & "")
            DT.AcceptChanges()
            If DT.Rows.Count > 0 Then
                For Each DR As DataRow In DT.Rows
                    ' DR.BeginEdit()
                    Dim bcp As New BarcodeProfessional()

                    bcp.Symbology = Symbology.Code128
                    bcp.BarcodeUnit = BarcodeUnit.Inch
                    bcp.Extended = True
                    bcp.DisplayCode = False

                    bcp.AddChecksum = False
                    'Dim fnt As New Font("Courier New", 10.0F)
                    'bcp.Font = fnt
                    'fnt.Dispose()
                    bcp.Code = DR.Item("ArticleBARCode").ToString
                    Dim img As Image = bcp.GetBarcodeImage()


                    For i As Integer = 1 To StickerCols
                        Dim NextStickerAddition As Int32 = (i - 1) * (StickerWidth + stickersGap)

                        col1 = col1 + NextStickerAddition
                        col2 = col2 + NextStickerAddition
                        Col3 = Col3 + NextStickerAddition

                        '//Row 1 #Start

                        '//Row 1 > Col 1
                        If flgCompanyName = True Then
                            'e.Graphics.DrawString(DR.Item("ArticleDescription").ToString, fnt1, Brushes.Black, New PointF(col1, row1)) 'ArticleDescription
                            'e.Graphics.DrawString(CompanyTitle.ToString, fnt1, Brushes.Black, New PointF(col1, row1)) 'CompanyName
                            e.Graphics.DrawString(frmItemSearch.LocationComments, fnt1, Brushes.Black, New PointF(col1, row1)) 'CompanyName
                        End If

                        '//Row 1 #End

                        '//Row 2 #Start

                        '//Row 2 > Col1
                        If flgProName = True Then
                            'e.Graphics.DrawString(DR.Item("ArticleGenderName").ToString, fnt2, Brushes.Black, New PointF(col1, row2)) 'Vendor
                            e.Graphics.DrawString(DR.Item("ArticleDescription").ToString, fnt1, Brushes.Black, New PointF(col1, row2)) 'ArticleDescription
                        End If

                        '//Row 2 # End

                        'Row 3 Start

                        '//Row 3 > Col1
                        If flgVendorCode = True Then
                            e.Graphics.DrawString(DR.Item("ArticleGenderName").ToString, fnt2, Brushes.Black, New PointF(col1, row3)) 'Vendor
                        End If

                        '//Row 3 > Col3
                        If flgProSize = True Then
                            e.Graphics.DrawString(DR.Item("ArticleSizeName").ToString, fnt2, Brushes.Black, New PointF(Col3, row3), sf2) 'ArticleSize
                        End If

                        'Row 3 End

                        '//Row 4 #Start
                        '//Row 4 > Col2 #Barcode
                        e.Graphics.DrawImage(img, col2, row4)

                        '//Row 4 #End

                        '//Row 5 #Start
                        '//Row 5 > Col1
                        If flgPrintDate = True Then
                            e.Graphics.DrawString(BarCodeDate, fnt2, Brushes.Black, New PointF(col1, row5), sfdate) 'Date
                        End If

                        '//Row 5 > Col3
                        If flgPackQty = True Then
                            e.Graphics.DrawString(DR.Item("PackQty").ToString, fnt2, Brushes.Black, New PointF(Col3, row5), sf2) 'Pack Qty
                        End If
                        e.Graphics.DrawString(DR.Item("ArticleBARCode").ToString, fnt1, Brushes.Black, New PointF(col2, row5), sf2) 'ArticleBarCode

                        '//Row 5 #End

                        '//Row 6 #Start
                        '//Row 6 > Col1
                        If flgProCode = True Then
                            e.Graphics.DrawString(DR.Item("ArticleCode").ToString, fnt2, Brushes.Black, New PointF(col1, row6))
                        End If

                        '//Row 6 > Col3
                        If flgProPrice = True Then
                            e.Graphics.DrawString("Rs." & DR.Item("SalePrice").ToString, fnt1, Brushes.Black, New PointF(Col3, row6), sf2)
                        End If

                        '//Row 6 #End


                    Next
                Next

            End If

            fnt1.Dispose()
            fnt2.Dispose()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

End Class
