﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Test.index" %>

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

    <form method="get" action ="home.aspx" onsubmit="true">
        <input type="text" id="username_ID" placeholder="Username" data-icon="U"> <!-- to make required required title="Password required" -->
        <input type="password" id="password_ID"  placeholder="Password" data-icon="x">

        <input type="submit" class="submit">Submit</a>
    </form>
</section>


</body>
</html>
