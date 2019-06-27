<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="web.aspx.vb" Inherits="_Default" %>

<%-- Add content controls here --%>

<asp:Content ID="Content1" ContentPlaceHolderID="Sidebar" Runat="Server">
    <input type="text" placeholder="Search" id="sbox" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="topBar" Runat="Server">
        <asp:LoginStatus id="LoginStatus" runat="server" CssClass="loginStatus" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BodyContent" Runat="Server">

</asp:Content>