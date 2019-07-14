<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Register.aspx.vb" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Sidebar" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="topBar" runat="Server">
    Sign Up for your New Account
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContent" runat="Server">
    <asp:Image ID="imgBanner" runat="server" ImageUrl="~/banner.png" CssClass="banner" />

    <asp:Panel ID="pnlRegisterForm" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Confirm Password:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="GraduationLabel" runat="server" AssociatedControlID="Graduation">Graduation Year (leave blank if not a student):</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Graduation" runat="server" TextMode="DateTime"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="color: Red;">
                    <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Button ID="btnSignUp" runat="server" Text="Sign Up" />
</asp:Content>

