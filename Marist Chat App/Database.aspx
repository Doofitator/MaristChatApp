<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Database.aspx.vb" Inherits="Database" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="Stylesheet" href="/Styles.css" />
    <link href="https://fonts.googleapis.com/css?family=Raleway&display=swap" rel="stylesheet">
    <title></title>
</head>
<body>
    <div class="sidebar">
        <ul>
            <li><a href="/">Ash's Test Page</a></li>
            <li><a href="/Another.aspx">Another page</a></li>
            <li><a href="/Database.aspx">Database test page</a></li>
        </ul>
    </div>
    <div class="content">
        <form id="form1" runat="server">
            
            <asp:Label ID="lblDatabaseHint" runat="server" Text="Text currently in the database:"></asp:Label>
&nbsp;<asp:Label ID="lblDatabase" runat="server" Text="0"></asp:Label>
            <br />
            <asp:Label ID="lblAddHint" runat="server" Text="Text to write to the database: "></asp:Label>
&nbsp;<asp:TextBox ID="txtAdd" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btnWrite" runat="server" Text="Write new text" />
            
        </form>
     </div>
</body>
</html>
