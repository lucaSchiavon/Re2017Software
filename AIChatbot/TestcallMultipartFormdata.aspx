<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestcallMultipartFormdata.aspx.cs" Inherits="Re2017.TestcallMultipartFormdata" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
   <form enctype="multipart/form-data" runat="server">
   File: <input id="myFile" type="file" runat="server" />    <input type="button" value="Upload" OnServerClick="Upload" runat="server" />
   <asp:label id="lblMsg" runat="server" />
   <asp:label id="lblFileContentType" runat="server" />
   <asp:label id="lblFileSize" runat="server" />
</form>
</body>
</html>
