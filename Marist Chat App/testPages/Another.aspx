<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Another.aspx.vb" Inherits="testPages_Another" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Sidebar" Runat="Server">
            <li><a href="/testPages/">Ash's Test Page</a></li>
            <li><a href="/testPages/Another.aspx">Another page</a></li>
            <li><a href="/testPages/Database.aspx">Database test page</a></li>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <asp:Label ID="Label1" runat="server" Text="ANOTHER PAGE"></asp:Label>
    <hr />
    <asp:Image ID="Image1" runat="server" Height="300px" ImageUrl="https://previews.123rf.com/images/nyul/nyul1102/nyul110200306/8748105-portrait-of-senior-man-at-home-having-laptop-computer-smiling-at-camera-.jpg" />
</asp:Content>

