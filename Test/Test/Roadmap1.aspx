<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Roadmap1.aspx.cs" Inherits="Test.Roadmap1" %>

<!DOCTYPE html>
<html lang="en">

<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Roadmap</title>

    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="css/simple-sidebar.css" rel="stylesheet">

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
    <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->



</head>

<body>
<form id="form1" runat="server">




                                                
<div id="wrapper">

    <!-- Sidebar -->
    <div id="sidebar-wrapper">

        <!-- Data Input -->
        <div id="mainDiv">
            <input class="txtStrat" type="text" placeholder="Add Strategy Point" onkeypress="addStrat(event)" />               
        </div>
        <!--
            <ul class="sidebar-nav">

            <li class="sidebar-brand">
                <br />      
                <asp:Table runat="server" ID="SideBarTable">
                    <asp:TableRow ID="Strat1">
                        
                        <asp:TableCell>
                        <button type="button" id="button1" onclick="StratClick()"> Strategy Point</button>
                        </asp:TableCell>
                    </asp:TableRow>


                </asp:Table>

                
               
            </li>

        </ul>
            -->
    </div>
    <!-- /#sidebar-wrapper -->

    <!-- Page Content -->
    <div id="page-content-wrapper">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-12">
                    <h1>Roadmap</h1>
                    <a href="#menu-toggle" class="btn btn-default" id="menu-toggle">Toggle Menu</a>
                </div>
                <br />
                <table id="roadmap-table">
                    <tbody id="PlaceHolder1" runat="server">
                    </tbody>
                </table>



            </div>
        </div>
    </div>
    <!-- /#page-content-wrapper -->

</div>


<!-- /#wrapper -->

    <!-- Modal -->
        <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="myModalLabel">Modal title</h4>
                    </div>
                    <div class="modal-body">
                        <h2><u>Description</u></h2>
                       <p>It will be saying things about our object BLah BLah Blah</p> 
                        <p></p>
                        <h2><u>Dependencies</u></h2>
                        <p>Becky totally depends on Johnny</p>
                        <p></p>
                        <h2><u>Risks</u></h2>
                        <p>We are all risky</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

<!-- Boot strap modal asp button attempt -->





 

<!-- jQuery -->
<script src="js/jquery.js"></script>

<!-- Bootstrap Core JavaScript -->
<script src="js/bootstrap.min.js"></script>

    <script src="js/Roadmap.js"></script>
<!-- Menu Toggle Script -->
<script>
    $("#menu-toggle").click(function(e) {
        e.preventDefault();
        $("#wrapper").toggleClass("toggled");
    });
</script>
<script>
    $("#menu-modal").click(function (e) {
        e.preventDefault();
        $("#myModal").modal("show");
    });
</script>

<script>
    function showModal() {                        
        $("#myModal").modal("show");
    }                    
</script>


<!-- Data Input -->
        <style>
        .txtStrat {
            width: 250px;
            height: 30px;
            margin-left: 0px;
        }

        .txtBus {
            width: 230px;
            height: 30px;
            margin-left: 20px;
        }

        .txtProj {
            width: 210px;
            height: 30px;
            margin-left: 40px;
        }
        .btnStrat{
            height:200px;
            width:200px;
        }
        
        .busroot {
           
        }
        .mainroot {
           
        }
        .projroot {
         
        }
        .mainDiv {

        }
        
   
    </style>
    <script type="text/javascript">
        
        function addStrat(e) {
            if (e.keyCode === 13) {
                
                var mainDiv = document.getElementById('mainDiv');

                var mainroot = document.createElement('div');
                //add 1 to each 
                mainroot.innerHTML = "<div class='mainroot'>" +
                                    "<div id='busroot'>" +
                                    "<input name='p1' class='txtBus' type='text' placeholder='Add Business Value' onkeypress='addBus(event, this)' /><br />" +
                                    "<div id='projroot'>" +
                                    "<input name='DynamicTextBox' class='txtProj' type='text' placeholder='Add Project' onkeypress='addProj(event, this)' /><br />" +
                                    "</div>" +
                                    "</div>" +
                                    "<input name='s' class='txtStrat' type='text' placeholder='Add Strategy Point' onkeypress='addStrat(event)'/><br />" +
                                    "</div>";

                mainDiv.appendChild(mainroot);                
            }

            return false;
        }

        function addBus(e, obj) {
            if (e.keyCode === 13) {
             
                var busroot = document.createElement('div');
                //add 1 to current b value
                busroot.innerHTML = "<div id='busroot'>" +
                                    "<input class='txtBus' type='text' placeholder='Add Business Value' onkeypress='addBus(event, this)' /><br />" +
                                    "<div id='projroot'>" +
                                    "<input class='txtProj' id='projBox' type='text' placeholder='Add Project' onkeypress='addProj(event, this)' /><br />" +
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
                newInput.id = 'projBox';
                newInput.setAttribute('onkeypress', 'addProj(event, this)');
                

                var projroot = obj.parentElement;
                projroot.appendChild(newInput);
                projroot.appendChild(br);

                var newInput2 = document.createElement('input');
                newInput2.type = 'button';
                newInput2.className = 'btnStrat';
                //newInput2.setAttribute('value', "hello");
                
                newInput2.setAttribute('value', obj.value);
                newInput2.setAttribute('onclick', 'showModal()');
                

                                                
                var placeholder1 = document.getElementById("PlaceHolder1")
                placeholder1.appendChild(newInput2);
               

            }

            return false;
        }
        function on_click() {
            var newInput2 = document.createElement('input');
            newInput2.type = 'text';
            newInput2.setAttribute('onkeypress', 'addProj(event, this)');

            var placeholder1 = document.getElementById("PlaceHolder1")
            placeholder1.appendChild(newInput2);
        }


    </script>
    <!-- /#Data Input-->
   
    </form>
</body>

</html>