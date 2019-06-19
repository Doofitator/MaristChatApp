<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Sidebar" Runat="Server">
     <li>Please login.</li>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">

    <asp:Login ID="frmLogin" runat="server" CreateUserText="Register" CreateUserUrl="~/Register.aspx" DestinationPageUrl="~/web.aspx" UserNameLabelText="Email:" UserNameRequiredErrorMessage="Email is required." >
    </asp:Login>

</asp:Content>

