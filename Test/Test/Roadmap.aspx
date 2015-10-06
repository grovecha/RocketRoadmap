<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Roadmap.aspx.cs" Inherits="Test.Roadmap" %>

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
        <ul class="sidebar-nav">

            <li class="sidebar-brand">
                <br />
            <asp:Button runat="server" id="testbutton" text="New Point" OnClick="Button_Click"  />
               
            </li>

        </ul>
    </div>
    <!-- /#sidebar-wrapper -->

    <!-- Page Content -->
    <div id="page-content-wrapper">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-12"><
                    <h1>Roadmap</h1>
                    <a href="#menu-toggle" class="btn btn-default" id="menu-toggle">Toggle Menu</a>
                    <button type="button" class="btn btn-info btn-lg" data-toggle="modal" data-target="#disModal">Open Modal</button>
                    <button type="button" class="btn btn-info btn-lg" data-toggle="modal" data-target="#inputModal">Input Modal</button>
                 
                </div>
                <br />
                <asp:Table ID="Table1" runat="server">

                </asp:Table>



            </div>
        </div>
    </div>
    <!-- /#page-content-wrapper -->

</div>
<!-- /#wrapper -->

    <!-- Modal -->
        <div class="modal fade" id="disModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="disLabel">Modal title</h4>
                    </div>
                    <div class="modal-body">
                        <h2><u>Description</u></h2>
                       <p>Modal data will be taken here</p> 
                        <p></p>
                        <h2><u>Dependencies</u></h2>
                        <p>Project ot Project dependencies</p>
                        <p></p>
                        <h2><u>Risks</u></h2>
                        <p>Risk textboxes</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
   
    
     <!-- Modal input -->
    <div id="inputModal" class="modal fade" role ="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal Content -->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h3 class="modal-title">Pull the project Title into here</h3>
                </div>
                <div class="modal-body">
                    <h2><u>Description</u></h2>
                        <textarea id="descText" rows="4" cols="75">
                        </textarea> 
                        <p></p>
                        <h2><u>Dependencies</u></h2>
                        <div id="dependencyText">
                        <input type="text" id="depenText1" size="60" /> <button type="button" class="btn btn-default" id="depButton" onclick="depClick">Add Dependecy</button>
                        </div>
                        <div id="dependencySelect">
                        <select id="depenSelect">
                            <option value="project1">Project1</option>
                            <option value="project2">Project2</option>
                            <option value="project3">Project3</option>
                            <option value="project4">Project4</option>
                        </select>
                        <button type="button" id="dep2Button" class="btn btn-default" onclick="depClick2()">Add Dependecy</button>
                       </div>
                        <p></p>
                        <h2><u>Risks</u></h2>
                        <textarea id="riskText" rows="4" cols="75">
                        </textarea> 

                        <h2><u>Links</u></h2>
                     <div id="link">
                        <input type="text" id="linkText" size="40"/> <button type="button" class="btn btn-default" id="linkButton" onclick="linkClick">New Link</button>
                     </div>
                <div class="modal-footer">
                     <button type="button" class="btn btn-default" onclick="saveClick()">Save</button>
                </div>
            </div>
        </div>
    </div>





<script type="text/javascript">
    function testclick(){
            //var count = 1;
            //var newinput = $(document.createElement('div')).attr("id", 'DepTextbox' + count);
            //newinput.after().html('<input type="text" id="depenText' + count + '" size = 60 /> <button type="button" id="depButton" onclick="depClick">Add Dependecy</button>');
            //newinput.appendTo("#dependencyText");
            //count++;
            alert("This hsit sucks");

     }


</script>








     <!-- jQuery -->
<script src="js/InputModal.js"></script>
<!-- jQuery -->
<script src="js/jquery.js"></script>

<!-- Bootstrap Core JavaScript -->
<script src="js/bootstrap.min.js"></script>

<!-- Modal Call Function -->
<script> type="text/javascript"
    function openModal() {
        $('#displayModal').modal('show');
    }
</script>

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
   
    </form>
</body>

</html>