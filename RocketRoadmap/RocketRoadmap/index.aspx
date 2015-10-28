<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WebApp.index" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>Test</title>
    <link rel="stylesheet" type="text/css" href="css/login.css">

</head>
<body class="align">

<div class="logo">
    <img src="images/quicken-loans-logo.png" alt="logo" width="200" height="100" align="middle">
</div>


<section class="login">

    <div class="title">Staff Login</div>

    <form method="post" action ="Home.aspx" onsubmit="true" runat="server">
        <input type="text" id="username_ID" placeholder="Username" runat="server" data-icon="U"> <!-- to make required required title="Password required" -->
        <input type="password" id="password_ID"  placeholder="Password" runat="server" data-icon="x">

        <input type="submit" class="submit" runat="server"></a>
    </form>
</section>


</body>
</html>
