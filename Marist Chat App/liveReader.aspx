<%@ Page Language="VB" AutoEventWireup="false" CodeFile="liveReader.aspx.vb" Inherits="liveReader" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Read Messages</title>
    <script>
        alert("Please wait - messages will load soon...")
    </script>
</head>
<body>
    <form id="frmMain" runat="server">
        <div>
            <asp:ScriptManager ID="smgrRead" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="pnlUpdate" runat="server">
                <ContentTemplate>                    
                    <asp:Timer ID="tmrRead" runat="server" Interval="1000"></asp:Timer>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
