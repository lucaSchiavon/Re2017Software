<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageNotFound.aspx.cs" Inherits="AQuest.ChatBotGsk.PigeonCms.pgn_content.Contents.PageNotFound" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>SB Admin 2 - Bootstrap Admin Theme</title>

    <!-- Bootstrap Core CSS -->
    <link href="../vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">

    <!-- MetisMenu CSS -->
    <link href="../vendor/metisMenu/metisMenu.min.css" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="../dist/css/sb-admin-2.css" rel="stylesheet">

    <!-- Custom Fonts -->
    <link href="../vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

</head>
<body>
    <form id="form1" runat="server">
         <div class="container">
        <div class="row">
            <div class="col-md-8 col-md-offset-2">
                               
                <div class="login-panel panel panel-default" >
                       <div class="panel panel-yellow" style="margin-bottom:0px">
                        <div class="panel-heading">
                           <h1>404 Not found</h1> 
                        </div>
                        <div class="panel-body">
                            <h4><p>Ops.. the page you have requested doesn't exist...</p></h4>
                        </div>
<%--                      <div style="text-align:center;background-color:none;margin-top:50px;margin-bottom:10px"> <label><asp:Button ID="BtnLogin" runat="server" Text="Back" class="btn btn-lg btn-warning btn-block" OnClientClick="javascript:history.back()" width="100px"/> </label></div>--%>
                    </div>
                     
                     
                </div>
            </div>
        </div>
    </div>

    <!-- jQuery -->
    <script src="../vendor/jquery/jquery.min.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="../vendor/bootstrap/js/bootstrap.min.js"></script>

    <!-- Metis Menu Plugin JavaScript -->
    <script src="../vendor/metisMenu/metisMenu.min.js"></script>

    <!-- Custom Theme JavaScript -->
    <script src="../dist/js/sb-admin-2.js"></script>
         <script src="../js/Login.js"></script>
    </form>
</body>
</html>
