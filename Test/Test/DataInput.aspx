<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataInput.aspx.cs" Inherits="Test.DataInput" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .txtStrat {
            width: 200px;
            margin-left: 0px;
        }

        .txtBus {
            width: 180px;
            margin-left: 20px;
        }

        .txtProj {
            width: 160px;
            margin-left: 40px;
        }

   
    </style>
    <script type="text/javascript">
        
        function addStrat(e) {
            if (e.keyCode === 13) {
                
                var mainDiv = document.getElementById('mainDiv');

                var mainroot = document.createElement('div');

                mainroot.innerHTML = "<div class='mainroot'>" +
                                    "<div id='busroot'>" +
                                    "<input class='txtBus' type='text' placeholder='Add Business Value' onkeypress='addBus(event, this)' /><br />" +
                                    "<div id='projroot'>" +
                                    "<input class='txtProj' type='text' placeholder='Add Project' onkeypress='addProj(event, this)' /><br />" +
                                    "</div>" +
                                    "</div>" +
                                    "<input class='txtStrat' type='text' placeholder='Add Strategy Point' onkeypress='addStrat(event)'/><br />" +
                                    "</div>";

                mainDiv.appendChild(mainroot);                
            }

            return false;
        }

        function addBus(e, obj) {
            if (e.keyCode === 13) {
             
                var busroot = document.createElement('div');

                busroot.innerHTML = "<div id='busroot'>" +
                                    "<input class='txtBus' type='text' placeholder='Add Business Value' onkeypress='addBus(event, this)' /><br />" +
                                    "<div id='projroot'>" +
                                    "<input class='txtProj' type='text' placeholder='Add Project' onkeypress='addProj(event, this)' /><br />" +
                                    "</div>" +
                                    "</div>";
                                    
                var projroot = obj.parentElement;
                projroot.appendChild(busroot);
            }

            return false;
        }

        function addProj(e, obj) {
            if (e.keyCode === 13) {

                var newInput = document.createElement('input');
                var br = document.createElement('br');

                newInput.type = 'text';
                newInput.className = 'txtProj';
                newInput.placeholder = 'Add Project';
                newInput.setAttribute('onkeypress', 'addProj(event, this)');

                var projroot = obj.parentElement;
                projroot.appendChild(newInput);
                projroot.appendChild(br);
            }

            return false;
        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="mainDiv">
                <input class="txtStrat" type="text" placeholder="Add Strategy Point" onkeypress="addStrat(event)" />               
            </div>
        </div>
    </form>
</body>
</html>
