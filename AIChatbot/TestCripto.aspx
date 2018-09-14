<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestCripto.aspx.cs" Inherits="AIChatbot.TestCeriptoaspx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="BtnCripta" runat="server" OnClick="BtnCripta_Click" Text="cripta" />
            <asp:Button ID="BtnDecripta" runat="server" OnClick="BtnDecripta_Click" Text="Decripta" />
            <asp:TextBox ID="TextBox1" runat="server" Width="550px"></asp:TextBox>
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            <br />
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            <br />
            <br />
        </div>
    </form>
</body>
</html>
