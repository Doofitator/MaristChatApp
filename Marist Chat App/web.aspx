﻿<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="web.aspx.vb" Inherits="_Default" EnableEventValidation="false" validateRequest="false" %>

<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>

<%-- Add content controls here --%>

<asp:Content ID="Content1" ContentPlaceHolderID="Sidebar" Runat="Server">
    <input type="text" placeholder="Search" id="sbox" disabled/>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="topBar" Runat="Server">
    <asp:LoginStatus id="LoginStatus" runat="server" CssClass="loginStatus" />
    <asp:HyperLink ID="aChangePW" runat="server" CssClass="loginStatus" NavigateUrl="~/Forgot.aspx">Change Password</asp:HyperLink>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BodyContent" Runat="Server">
    <script>
        setTimeout(function () {
            ScrollDown();
        }, 500);
    </script>
    <script>
        $(document).ready(function () {
            if ($(window).width() < 450)
                $('#topBar_lblStreamName').css('display', 'none');
        });
        function collapse(label) {

            var elems = $(label).nextUntil('li, hr');
            var i;
            for (i = 0; i < elems.length; i++) {
                if (elems[i].style.display == "none") {
                    elems[i].style.display = "block";
                } else {
                    elems[i].style.display = "none";
                }
            }
        }
        function revealNext(divelmnt) {
            var ancestor = document.getElementById(divelmnt);
            var descendents = ancestor.getElementsByTagName('select');
            var i;
            for (i = 0; i < descendents.length; ++i) {
                if (getComputedStyle(descendents[i], null).display == "none") {
                    descendents[i].style.display = "block";
                    break;
                }
            }
        }
    </script>

    <asp:ScriptManager ID="smgrTimer" runat="server" EnableCdn="true"></asp:ScriptManager>
    <asp:UpdatePanel ID="pnlWizards" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="messagesContainer">
            <asp:UpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
                <ContentTemplate>                    
                    <asp:Timer ID="tmrUpdate" runat="server" Interval="1000"></asp:Timer>
                </ContentTemplate>
            </asp:UpdatePanel>
    </div>
        <asp:UpdatePanel ID="pnlControls" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

            </ContentTemplate>
             <Triggers>
                   <asp:AsyncPostBackTrigger ControlID="btnSend" />
             </Triggers>
        </asp:UpdatePanel>
</asp:Content>