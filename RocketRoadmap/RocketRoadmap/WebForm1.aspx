<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="RocketRoadmap.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>



    <button type="button" onclick="DoStuff();">Click Me!</button>
    
    
    
    </div>


        <!-- jQuery -->
<script src="js/jquery.js"></script>



        <script>
            function DoStuff()
            {
                
                console.log("got it");
                $.ajax({
                    type: "POST",
                    url: "WebService1.asmx/TestFunction",
                    contentType: "application/json; charset=utf-8",
                    datatype: "json",
                    data: JSON.stringify({ name: "test" }),
                    success: function(response)
                    {
                        console.log(response);
                    },
                    error:function (err)
                    {
                        console.log(err);
                    }

                });




            }
        </script>
    </form>
</body>
</html>
