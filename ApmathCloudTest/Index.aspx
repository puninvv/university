<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Парсер функции</title>
</head>
<body>
    <form name="MainForm" runat="server" method="post">
        <asp:PlaceHolder ID="PlaceHolder" runat="server">
            <asp:TextBox ID="mfunctionTxt" runat="server" text="Введите функцию"/>
                  
            <asp:PlaceHolder ID="mVariables" runat="server"/>
            <asp:Button ID="mCountFuncValue" runat="server" />     
        </asp:PlaceHolder>
    </form>
</body>
</html>
