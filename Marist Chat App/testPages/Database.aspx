<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Database.aspx.vb" Inherits="testPages_Database" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Sidebar" Runat="Server">
            <li><a href="/testPages/">Ash's Test Page</a></li>
            <li><a href="/testPages/Another.aspx">Another page</a></li>
            <li><a href="/testPages/Database.aspx">Database test page</a></li>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <asp:Label ID="lblDatabaseHint" runat="server" Text="Text currently in the database:"></asp:Label>
&nbsp;<asp:Label ID="lblDatabase" runat="server" Text="0"></asp:Label>
            <br />
            <asp:Label ID="lblAddHint" runat="server" Text="Text to write to the database: "></asp:Label>
&nbsp;<asp:TextBox ID="txtAdd" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btnWrite" runat="server" Text="Write new text" />
</asp:Content>

