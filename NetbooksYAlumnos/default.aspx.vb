Public Class _default
    Inherits System.Web.UI.Page

    Dim dv As New DataView
    Dim lista_alumnos As New List(Of Alumnos)
    Dim path As String = "D:\netbooks.xlsx"
    Dim pathMensaje As String = "D:\mensaje.txt"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then

                If IO.File.Exists(path) = False Then
                    path = "D:\Carpetas de Sistema\Dropbox\Administrador de Redes\Establecimientos\Nacional\Base de datos\NACIONAL 2015.xlsx"
                End If
                If IO.File.Exists(pathMensaje) = True Then
                    Alerta.Visible = True
                    lblMensaje.Text = IO.File.ReadAllText(pathMensaje, Encoding.Default)
                Else
                    Alerta.Visible = False
                End If

                Cargar_datos()
                Cargar_combo()
                Cargar_grilla()
                lblUltimaVersion.Text = Format(IO.File.GetLastWriteTime(path), "dd/MM/yyyy HH:mm")

                If IsNothing(Request.Cookies("UltimoCursoSeleccionado")) = False Then
                    DDL_Cursos.SelectedValue = Request.Cookies("UltimoCursoSeleccionado").Value
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub Cargar_datos()
        Try
            dv = Leer_Planilla(path, "Hoja1")

            If IsNothing(dv) Then
                Exit Sub
            End If

            Dim query =
              From a As DataRowView In dv
              Where Not a.Row.Field(Of String)("CURSO") Is Nothing AndAlso a.Row.Field(Of String)("CURSO").Trim <> "" AndAlso
                  a.Row.Field(Of String)("CURSO").Contains("INDE") = False AndAlso a.Row.Field(Of String)("CURSO").Contains("2017")
              Select New With {
                  .DNI = a.Row.Field(Of String)("DOCUMENTO"),
                  .APELLIDO = a.Row.Field(Of String)("APELLIDOS"),
                  .NOMBRE = a.Row.Field(Of String)("NOMBRES"),
                  .ESTADO_ADMINISTRATIVO_EQUIPAMIENTO = a.Row.Field(Of String)("ESTADO_ADMINISTRATIVO_EQUIPAMIENTO"),
                  .ESTADO_OPERATIVO_EQUIPAMIENTO = a.Row.Field(Of String)("ESTADO_OPERATIVO_EQUIPAMIENTO"),
                  .CURSO = a.Row.Field(Of String)("CURSO")
                  }

            For Each a In query
                If IsNothing(a) = False Then
                    lista_alumnos.Add(New Alumnos With {
                    .DOCUMENTO = a.DNI,
                    .APELLIDOS = a.APELLIDO,
                    .NOMBRES = a.NOMBRE,
                    .ESTADO_ADMINISTRATIVO_EQUIPAMIENTO = a.ESTADO_ADMINISTRATIVO_EQUIPAMIENTO,
                    .ESTADO_OPERATIVO_EQUIPAMIENTO = a.ESTADO_OPERATIVO_EQUIPAMIENTO,
                    .CURSO = a.CURSO})
                End If
            Next
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Sub Cargar_grilla()
        Try
            Dim query =
              From a As Alumnos In lista_alumnos
              Where a.CURSO = DDL_Cursos.SelectedValue
              Order By a.APELLIDOS Ascending
              Select New With {
                  .DOCUMENTO = a.DOCUMENTO,
                  .APELLIDO = a.APELLIDOS,
                  .NOMBRE = a.NOMBRES,
                  .ESTADO_ADMINISTRATIVO_EQUIPAMIENTO = a.ESTADO_ADMINISTRATIVO_EQUIPAMIENTO,
                  .ESTADO_OPERATIVO_EQUIPAMIENTO = a.ESTADO_OPERATIVO_EQUIPAMIENTO
                  }

            If query.Count > 0 Then
                gridview_Alumnos.DataSource = query.ToList
            Else
                gridview_Alumnos.DataSource = Nothing

            End If

            gridview_Alumnos.DataBind()
            lblCantidad.Text = "Cantidad: " & gridview_Alumnos.Rows.Count

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Sub Cargar_combo()
        Try

            DDL_Cursos.DataSource = Nothing
            Dim lista As New List(Of String)

            Dim query =
               From a As Alumnos In lista_alumnos
               Where Not a.CURSO Is Nothing AndAlso a.CURSO.Trim <> "" AndAlso a.CURSO.Contains("INDE") = False AndAlso a.CURSO.Contains("2017")
               Order By a.CURSO Ascending
               Select New With {.CURSO = a.CURSO.Trim}

            For Each c In query
                Try
                    If c.CURSO <> "" Then
                        If lista.Contains(c.CURSO.ToUpper) = False Then
                            lista.Add(c.CURSO.ToUpper)
                        End If
                    End If
                Catch ex As Exception

                End Try
            Next

            DDL_Cursos.DataSource = lista
            DDL_Cursos.DataBind()

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub DDL_Cursos_TextChanged(sender As Object, e As EventArgs) Handles DDL_Cursos.TextChanged
        Try

            Cargar_datos()
            Cargar_grilla()

            Dim cookie As New HttpCookie("UltimoCursoSeleccionado") With {
                .Value = DDL_Cursos.SelectedValue,
                .Expires = DateTime.Now.AddDays(30)
            }
            Response.SetCookie(cookie)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class

Public Class Alumnos

    Dim _DOCUMENTO As String
    Dim _APELLIDOS As String
    Dim _NOMBRES As String
    Dim _ESTADO_ADMINISTRATIVO_EQUIPAMIENTO As String
    Dim _ESTADO_OPERATIVO_EQUIPAMIENTO As String
    Dim _CURSO As String

    Public Property DOCUMENTO As String
        Get
            Return _DOCUMENTO
        End Get
        Set(value As String)
            _DOCUMENTO = value
        End Set
    End Property

    Public Property APELLIDOS As String
        Get
            Return _APELLIDOS
        End Get
        Set(value As String)
            _APELLIDOS = value
        End Set
    End Property

    Public Property NOMBRES As String
        Get
            Return _NOMBRES
        End Get
        Set(value As String)
            _NOMBRES = value
        End Set
    End Property

    Public Property ESTADO_ADMINISTRATIVO_EQUIPAMIENTO As String
        Get
            Return _ESTADO_ADMINISTRATIVO_EQUIPAMIENTO
        End Get
        Set(value As String)
            _ESTADO_ADMINISTRATIVO_EQUIPAMIENTO = value
        End Set
    End Property

    Public Property ESTADO_OPERATIVO_EQUIPAMIENTO As String
        Get
            Return _ESTADO_OPERATIVO_EQUIPAMIENTO
        End Get
        Set(value As String)
            _ESTADO_OPERATIVO_EQUIPAMIENTO = value
        End Set
    End Property

    Public Property CURSO As String
        Get
            Return _CURSO
        End Get
        Set(value As String)
            _CURSO = value
        End Set
    End Property
End Class