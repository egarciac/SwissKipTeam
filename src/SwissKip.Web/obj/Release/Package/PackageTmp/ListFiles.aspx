<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListFiles.aspx.cs" Inherits="SwissKip.Web.ListFiles" %>
<%@ Register Assembly="IZ.WebFileManager" Namespace="IZ.WebFileManager" TagPrefix="iz" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <iz:FileManager ID="FileManager1" runat="server" Height="400" Width="600">
    </iz:FileManager>
    </div>
    </form>
</body>
</html>
