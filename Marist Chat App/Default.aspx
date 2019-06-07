<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
<head runat="server">
    <link rel="Stylesheet" href="Styles.css" />
    <link href="https://fonts.googleapis.com/css?family=Raleway&display=swap" rel="stylesheet">
    <title></title>
</head>
<body>
    <div class='sidebar'>
        <ul>
            <li><a href="/">Ash's Test Page</a></li>
            <li><a href="/Another.aspx">Another page</a></li>
            <li><a href="/Database.aspx">Database test page</a></li>
        </ul>
    </div>
    <div class="content">
        <form id="form1" runat="server">
            <asp:Label ID="Label1" runat="server" Text="ASH'S TEST PAGE"></asp:Label>
            <hr />
            <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                <asp:ListItem>Test1</asp:ListItem>
                <asp:ListItem>Test2</asp:ListItem>
                <asp:ListItem>Test3</asp:ListItem>
            </asp:RadioButtonList>
            <asp:Button ID="Button1" runat="server" Text="Go" />
            <hr />
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </form>
     </div>
</body>
</html>
