'-----------------------------------------------------------------------------------------
'
'           EXCEL CONNECTION MODULE
'           by Marcelo L. Ponce F.
'           version: 2017.08.06 19:00
'
'-----------------------------------------------------------------------------------------

Module ModExcel

    Private Function Cadena_segun_tipo_archivo(ByVal path As String) As String

        Try

            Dim extension As String = IO.Path.GetExtension(path)
            Dim proveedor As String = ""
            Dim propiedad_extendida As String = ""

            Select Case extension

                Case ".xls"
                    proveedor = "Microsoft.Jet.OLEDB.4.0;"
                    propiedad_extendida = """Excel 8.0;HDR=YES"""

                Case ".xlsx"
                    proveedor = "Microsoft.ACE.OLEDB.12.0;"
                    propiedad_extendida = " ""Excel 12.0;IMEX=1;HDR=YES;"""

            End Select

            Return "Provider=" & proveedor & " Data Source=" & path & ";" & "Extended Properties=" & propiedad_extendida

        Catch ex As Exception
            Return -1
        End Try

    End Function

    Public Function conectar_excel(ByVal path As String) As Boolean

        Dim DT As DataTable
        Try
            path = "D:\Carpetas de Sistema\Desktop\combustibles.xlsx"
            Dim DBConnection2 As New OleDb.OleDbConnection
            DBConnection2.ConnectionString = Cadena_segun_tipo_archivo(path)
            DBConnection2.Open()
            DT = DBConnection2.GetSchema("TABLES")
            DBConnection2.Close()

        Catch ex As Exception
            Return False
        End Try

        Try
            Dim includeSheets As Boolean = False
            Dim includeRanges As Boolean = False
            Dim o As Object = Nothing
            Dim n As Int32 = 0

            If DT.Rows.Count > 0 Then

                For Each dr As DataRow In DT.Rows

                    o = dr.Item("TABLE_TYPE")

                    If (o.ToString.ToUpper = "TABLE") Then

                        o = dr.Item("TABLE_NAME")

                        Dim p As Int32 = o.ToString.IndexOf("$"c)

                        If p = -1 AndAlso Not includeRanges Then Continue For

                        If p <> -1 AndAlso Not includeSheets Then
                            o.ToString.TrimEnd("$")
                            n += 1
                        End If

                    End If

                Next dr

            End If

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function Leer_Planilla(
                                 ByVal path As String,
                                 Optional ByVal hoja As String = "Hoja1",
                                 Optional initialInterval As String = "A1",
                                 Optional ByVal finalInterval As String = "XFD1048576") As DataView
        Try

            If path <> "" Then

                Dim DBConnection2 As New OleDb.OleDbConnection
                Dim SqlSelect As String
                Dim dataset As New DataSet
                Dim DA As OleDb.OleDbDataAdapter

                hoja = hoja.Replace("$", "").Replace("'", "")

                DBConnection2.ConnectionString = Cadena_segun_tipo_archivo(path)
                SqlSelect = "SELECT * FROM " & "[" & hoja & "$" & initialInterval & ":" & finalInterval & "]"
                DBConnection2.Open()
                dataset.Clear()
                DA = New OleDb.OleDbDataAdapter(SqlSelect, DBConnection2)
                DA.Fill(dataset)
                DBConnection2.Close()

                Return dataset.Tables(0).AsDataView

            Else
                Return Nothing
            End If

        Catch ex As Exception
            Throw
        End Try

    End Function


End Module
