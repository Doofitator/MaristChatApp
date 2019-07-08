﻿<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="web.aspx.vb" Inherits="_Default" %>

<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>

<%-- Add content controls here --%>

<asp:Content ID="Content1" ContentPlaceHolderID="Sidebar" Runat="Server">
    <input type="text" placeholder="Search" id="sbox" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="topBar" Runat="Server">
    <asp:LoginStatus id="LoginStatus" runat="server" CssClass="loginStatus" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BodyContent" Runat="Server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script>
        setTimeout(function () {
            ScrollDown();
        }, 500);
    </script>
    <script>
        $(document).ready(function () {
            if ($(window).width() < 388)
                $('#topBar_lblStreamName').css('display', 'none');
        });
    </script>
</asp:Content>