<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="default.aspx.vb" Inherits="NetbooksYAlumnos._default" %>

<!DOCTYPE html>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <title>Alumno y Netbooks</title>

    <!-- Bootstrap -->
    <link href="css/bootstrap.min.css" rel="stylesheet">
    <link rel="shortcut icon" type="image/x-icon" href="../img/favicon.ico" />

    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="js/html5shiv.min.js"></script>
        <script src="js/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form action="/" method="post" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container marketing">
            <div class="jumbotron" style="margin-top: 15px;">
                <img src="img/netbook.png" style="margin-bottom: -25px; margin-top: -25px;" />
                <h1>Informática</h1>
                <p class="lead">Estado de netbooks por curso.</p>
                <asp:DropDownList ID="DDL_Cursos" runat="server" CssClass="btn btn-success dropdown-toggle" AutoPostBack="True">
                </asp:DropDownList>
            </div>
            <div class="row">
                <div class="col-lg-12">
                      <div id="Alerta" runat="server" Class="alert alert-info"><asp:Label ID="lblMensaje" runat="server" Text="Label" ></asp:Label>
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                      </div>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="DDL_Cursos" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:GridView ID="gridview_Alumnos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover table-condensed table-striped">
                                <Columns>
                                    <asp:TemplateField HeaderText="DOCUMENTO" ItemStyle-CssClass="col-sm-4 col-lg-1">
                                        <ItemTemplate>
                                            <%# Eval("DOCUMENTO") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="APELLIDO" ItemStyle-CssClass="col-sm-4 col-lg-2">
                                        <ItemTemplate>
                                            <%# Eval("APELLIDO") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="NOMBRE" ItemStyle-CssClass="col-sm-6 col-lg-2">
                                        <ItemTemplate>
                                            <%# Eval("NOMBRE") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ESTADO ADMINISTRATIVO" ItemStyle-CssClass="col-sm-4 col-lg-4">
                                        <ItemTemplate>
                                            <%# Eval("ESTADO_ADMINISTRATIVO_EQUIPAMIENTO") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ESTADO OPERATIVO" ItemStyle-CssClass="col-sm-4 col-lg-3">
                                        <ItemTemplate>
                                            <%# Eval("ESTADO_OPERATIVO_EQUIPAMIENTO") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <p>
                                <asp:Label ID="lblCantidad" runat="server"></asp:Label>
                            </p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                 
                </div>
                       
            </div>
            <footer>
                <p>
                    Última actualización:
                    <asp:Label ID="lblUltimaVersion" runat="server" Text=""></asp:Label>
                </p>
                <p>#N/A: Significa que el equipo se debe llevar para revisión a la oficina del Administrador de la Red.</p>
                <p>Ante cualquier irregularidad por favor contactar al Administrador de la Red.</p>
                <hr />
                <p>Gabinete de Informática - 2017</p>
            </footer>
        </div>
    </form>

    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="js/jquery.min.js"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="js/bootstrap.min.js"></script>
</body>
</html>
