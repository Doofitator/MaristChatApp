<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="web.aspx.vb" Inherits="_Default" %>

<%-- Add content controls here --%>

<asp:Content ID="Content1" ContentPlaceHolderID="Sidebar" Runat="Server">
    <li><a href="/">Sidebar Content</a></li>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    Testing body content
</asp:Content>