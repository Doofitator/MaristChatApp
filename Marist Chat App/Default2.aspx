<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Default2.aspx.vb" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Sidebar" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="topBar" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContent" Runat="Server">
    <script>$(document).ready(function () { HideShow('BodyContent_divNewStream') });</script>
    <script>
        function myFunction() {
            var ancestor = document.getElementById('BodyContent_divNewClass');
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
</asp:Content>

