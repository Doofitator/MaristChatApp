<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="testPages_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Sidebar" Runat="Server">
            <li><a href="/testPages/">Ash's Test Page</a></li>
            <li><a href="/testPages/Another.aspx">Another page</a></li>
            <li><a href="/testPages/Database.aspx">Database test page</a></li>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
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
</asp:Content>

