﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TestWeb._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Js/jquery-1.8.2.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <%=XCLNetTools.StringHander.Common.RootUri%>

            <br />

            <%=XCLNetTools.StringHander.Common.RootURL%>
        </div>

        <a href="Default.aspx?a=1212&b=333&d=55&a=99">qqqqqqqqqqqq</a>


        <%=XCLNetTools.StringHander.FormHelper.GetQueryFromSerializeString()%>
    </form>
</body>
</html>