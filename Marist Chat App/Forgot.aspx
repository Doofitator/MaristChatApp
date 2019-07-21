<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Forgot.aspx.vb" Inherits="Forgot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Sidebar" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="topBar" Runat="Server">
    <asp:Label ID="Label1" runat="server" Text="Password Reset"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContent" Runat="Server">
    <asp:Label ID="lblTextHint" runat="server"></asp:Label>
    <asp:TextBox ID="txtInput" runat="server"></asp:TextBox>
    <br />
    <asp:Button ID="btnSubmit" runat="server" />
</asp:Content>

