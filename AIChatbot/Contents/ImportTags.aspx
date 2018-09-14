<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportTags.aspx.cs" Inherits="AQuest.ChatBotGsk.PigeonCms.pgn_content.Contents.ImportTags" %>

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
<%--                           <div id="DivError" runat="server" class="ParentDivDeleting Disattivato"><div class="InnerDivDeleting">
       <div class="panel panel-red" style="width:100%;height:100%">
                        <div class="panel-heading">
                            Error
                        </div>
                        <div class="panel-body text-center" >
                            <p><asp:Literal ID="LitError" runat="server" Text=""></asp:Literal></p>
                        </div>
                       <div  class="text-center">
                           <asp:Button ID="BtnClose"  Text="CLOSE" class="btn btn-primary" runat="server" OnClick="BtnClose_Click" />
                       </div>
                  
                    </div>
      </div></div>--%>
         <div class="container">

        <div class="row">
           
            <div class="panel panel-primary">
                 <asp:PlaceHolder ID="PhldPrimary" runat="server">
                        <div class="panel-heading">
                            Import tags
                        </div>
                </asp:PlaceHolder>
                 <asp:PlaceHolder ID="PhldDanger" runat="server">
                <div class="alert alert-danger">
                             <asp:Literal ID="LitError" runat="server"></asp:Literal>
                            </div>
                     </asp:PlaceHolder>
                        <div class="panel-body">
                            <h3>Upload csv with tag's informations</h3>
                            <p>remember that CSV or TXT file must have the following format:</p>
                        
                            <p>TagName[TAB]Node[TAB]Machine[TAB]Device[TAB]ValueType[TAB]IdTagValue[TAB]Alarm[TAB]Description</p>

                        </div>
                
                <div class="row">
                <div class="col-lg-6">
                         <div class="panel panel-default">
                       <%-- <div class="panel-heading">
                         <asp:Literal ID="LitUser" runat="server"></asp:Literal>
                        </div>--%>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-3">
                                 
                                        <div class="form-group">
                                            <label>Upload file:</label>
                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                        </div>
                                       </div>
                                    <div class="col-lg-3">
                                     
                                         <div class="form-group">
                                          <%--  <label>xx</label>--%>
                                            <asp:Literal ID="LitOk" runat="server"></asp:Literal>
                                        </div>
                                        </div>
                                   
                                  <div class="col-lg-6">
                                           <div class="form-group" style="height:30px">
                                            <label>Log:</label><br />
                                               <div style="height:20px">
                                               <asp:HyperLink ID="HypLnkLog" runat="Server" Text="   " Target="_blank"></asp:HyperLink>
                                         </div>
                                        </div>
                                         </div>
                                    
                                <div class="col-lg-6" style="text-align:right">
                                  <asp:linkbutton id="LnkBtnSalva" runat="server" class="btn btn-primary" OnClick="BtnSalva_Click"><i class='fa fa-arrow-down'></i> Save</asp:linkbutton>
                                    </div>
                                        
                                 
                               
                                <!-- /.col-lg-6 (nested) -->
                              
                                <!-- /.col-lg-6 (nested) -->
                            </div>
                            <!-- /.row (nested) -->
                        </div>
                        <!-- /.panel-body -->
                    </div>
                </div></div>
                     
                       
                
               
               
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
