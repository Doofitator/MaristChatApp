<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="web.aspx.vb" Inherits="_Default" %>

<%-- Add content controls here --%>

<asp:Content ID="Content1" ContentPlaceHolderID="Sidebar" Runat="Server">
    <input type="text" placeholder="Search" id="sbox" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="topBar" Runat="Server">
        <asp:LoginStatus id="LoginStatus" runat="server" CssClass="loginStatus" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BodyContent" Runat="Server">
    <script>
        function HideShow(div) {
            var element = document.getElementById(div);
            if (element.style.display == "block") {
                element.style.display = "none";
            } else {
                element.style.display = "block";
            }
        }
    </script>
    <div id="divNewClass" class="wizard">
        <h3>New Class Wizard</h3>
        <asp:Label ID="lblClassID" runat="server" Text="Class Identifier: "></asp:Label>
        <asp:TextBox ID="txtClassID" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblUserList" runat="server" Text="CSV User list: "></asp:Label>
        <asp:TextBox ID="txtUserLst" runat="server" TextMode="MultiLine"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnWriteClass" runat="server" Text="Write Class" />
        <input type="button" onclick="HideShow('divNewClass')" value="Cancel" />
        <br />
    </div>
</asp:Content>