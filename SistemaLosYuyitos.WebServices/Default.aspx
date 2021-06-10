<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SistemaLosYuyitos.WebServices.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p>Prueba Yuyitos</p>
            <a href="WSClientes.asmx?wsdl">WSClientes</a>
            <br />
            <a href="WSFiadosAbonos.asmx?wsdl">WSFiadosAbonos</a>
            <br />
            <a href="WSPruebaConexion.asmx?wsdl">WSPruebaConexion</a>
            <br />
            <a href="WSUsuarios.asmx?wsdl">WSUsuarios</a>
            <br />
            <a href="WSBoletas.asmx?wsdl">WSBoletas</a>
            <br />
            <a href="WSProductos.asmx?wsdl">WSProductos</a>
            <br />
            <a href="WSOrdenes.asmx?wsdl">WSOrdenes</a>
            <br />
            <a href="WSProveedores.asmx?wsdl">WSProveedores</a>
            <br />
            <a href="WSRegiones.asmx?wsdl">WSRegiones</a>
        </div>
    </form>
</body>
</html>
