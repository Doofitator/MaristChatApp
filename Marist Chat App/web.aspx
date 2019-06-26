<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="web.aspx.vb" Inherits="_Default" %>

<%-- Add content controls here --%>

<asp:Content ID="Content1" ContentPlaceHolderID="Sidebar" Runat="Server">
    <input type="text" placeholder="Search" id="sbox" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="topBar" Runat="Server">
        <asp:LoginStatus id="LoginStatus" runat="server" CssClass="loginStatus" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BodyContent" Runat="Server">
    <div id="divNewClass" class="wizard">
        <div id="divNewClassTitleBar" class="titleBar">
            <h3>New Class Wizard
                <input style="float: right;" type="button" onclick="HideShow('divNewClass')" value="X" />
            </h3>
        </div>
        <asp:Label ID="lblClassID" runat="server" Text="Class Identifier: "></asp:Label>
        <asp:TextBox ID="txtClassID" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblUserList" runat="server" Text="CSV User list: "></asp:Label>
        <asp:TextBox ID="txtUserLst" runat="server" TextMode="MultiLine"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnWriteClass" runat="server" Text="Write Class" />
        <br />
    </div>
    
    <div id="divNewAlert" class="wizard">
        <div id="divNewAlertTitleBar" class="titleBar">
            <h3>New Alert Wizard
                <input style="float: right;" type="button" onclick="HideShow('divNewAlert')" value="X" />
            </h3>
            </div>
        <asp:Label ID="lblMessage" runat="server" Text="Message:"></asp:Label>
        <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine"></asp:TextBox>
        <br />
        <asp:CheckBox ID="cbxUrgent" runat="server" Text="Urgent?" TextAlign="Left" />
        <br />
        <asp:Label ID="lblRoles" runat="server" Text="Label"></asp:Label>
        <asp:DropDownList ID="ddlRoles" runat="server" AppendDataBoundItems="False">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="btnWriteAlert" runat="server" Text="Write Alert" />
        <br />
    </div>
</asp:Content>