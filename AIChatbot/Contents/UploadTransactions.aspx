<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadTransactions.aspx.cs" Inherits="Re2017.Contents.UploadTransactions" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <!-- Bootstrap Core CSS -->
    <link href="../vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">

    <!-- MetisMenu CSS -->
    <link href="../vendor/metisMenu/metisMenu.min.css" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="../dist/css/sb-admin-2.css" rel="stylesheet">

    <!-- Custom Fonts -->
    <link href="../vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
</head>
<body>

       <div class="row" style="margin-right:0px;margin-left:0px">
                <div class="col-lg-3">
                    <h1 class="page-header">Upload transactions</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
  

    <form id="Form1" runat="server" name="form1" method="post" enctype="multipart/form-data" action="http://2.235.241.7:8080/bank-report-entries/2/upload">
                     <div class="row" style="margin-right:0px;margin-left:0px">
                 <div class="col-lg-3">
                <div class="panel panel-primary">
                        <div class="panel-heading">
                            Upload form:
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-3">
                                  
                                        <div class="form-group">
                                            <label>Bank</label>
                                           <asp:DropDownList ID="CboBank" onchange="MemorizeCallUrl(this)" name="bankId" runat="Server" class="form-control"></asp:DropDownList>
                                            <%--<asp:TextBox ID="TxtDa" runat="server" type="date" class="form-control" Text="2018-09-01"></asp:TextBox>    --%>
                                           
                                        </div>
                                 </div>
                             <div class="col-lg-3">
                                  <div class="form-group">
                                            <label>Upload file</label> 
                                        <input name="file" type="file" />
                                     <%--  <asp:TextBox ID="TxtA" runat="server" type="date" class="form-control"></asp:TextBox>    --%>                                    
                                        </div>
                                       </div>
                           <%--  <div class="col-lg-3">
                                        <div class="form-group">
                                            <label>Users</label>
                                           <asp:DropDownList ID="CboUsers" runat="Server" class="form-control"></asp:DropDownList>
                                        </div>
                                      </div>--%>
                             <div class="col-lg-3" style="text-align:center;Padding-top:3px">
                                  <div class="form-group">
                                            <label></label>
                                           <br />   
                                       <input type="submit" value="Submit" class="btn btn-primary"/>
                                         <%--<asp:linkbutton id="LnkBtnFilter" runat="server" class="btn btn-primary" ><i class='fa fa-filter'></i> Filter</asp:linkbutton>       --%>                                  
                                        </div>
                                       </div>

                               
                        </div>
                      
                    </div>
                     </div>
                    </div>

        <div>
           
          <%--  <input name="bankId" type="text" />--%>
        </div>
        <div>         
          
        </div>
        <div>
           
        </div>
         <script src="../vendor/DropzoneJs_scripts/jquery.min.js"></script>
            <script type="text/javascript">
                var MemorizeCallUrl = function (e) {
                    var SelBankIndex = e.options[e.selectedIndex].value;
                    var urlToCall = 'http://2.235.241.7:8080/bank-report-entries/' + SelBankIndex + '/upload';
                  
                    if (e.preventDefault) {
                        e.preventDefault();
                    }
                    else {
                        e.returnValue = false;
                    }
                   //set action property of form
                    $('#Form1').attr("action", urlToCall);

                };
            </script>
    </form>
</body>
</html>
