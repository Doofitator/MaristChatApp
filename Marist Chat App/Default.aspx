﻿<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Sidebar" Runat="Server">
     
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="topBar" runat="Server">
    <span class="title">Please Log In</span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">

<asp:Image ID="imgBanner" runat="server" ImageUrl="~/banner.png" CssClass="banner" />

    <asp:Panel runat="server" CssClass="loginWrapper">
        <asp:Login ID="frmLogin" runat="server" CreateUserText="Register" CssClass="loginForm" CreateUserUrl="~/Register.aspx" DestinationPageUrl="~/web.aspx" UserNameLabelText="Email:" UserNameRequiredErrorMessage="Email is required." TitleText="" >
        </asp:Login>
    </asp:Panel>

</asp:Content>

