<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/ChatBotGskAdm.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AQuest.ChatBotGsk.PigeonCms.pgn_content.Contents.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div id="DivDelete" runat="server" class="ParentDivDeleting Disattivato"><div class="InnerDivDeleting">
       <div class="panel panel-red" style="width:100%;height:100%">
                        <div class="panel-heading">
                            Confirm deleting
                        </div>
                        <div class="panel-body text-center" >
                            <p><asp:Literal ID="LitDeleteMsg" runat="server" Text="Do you really want to delete this row?"></asp:Literal></p>
                        </div>
                       <div  class="text-center">
                           <asp:Button ID="BtnCancelDeleting"  Text="CANCEL" class="btn btn-primary" runat="server" OnClick="BtnCancelDeleting_Click" />&nbsp&nbsp&nbsp&nbsp&nbsp
                      <asp:Button ID="BtnConfirmDeleting" Text="YES" class="btn btn-danger" runat="server" OnClick="BtnConfirmDeleting_Click" />
                           
                       </div>
                      <%--  <div class="panel-footer">
                            Panel Footer
                        </div>--%>
                    </div>
      </div></div>
      <div id="DivError" runat="server" class="ParentDivDeleting Disattivato"><div class="InnerDivDeleting">
       <div class="panel panel-red" style="width:100%;height:100%">
                        <div class="panel-heading">
                            Error
                        </div>
                        <div class="panel-body text-center" >
                            <p><asp:Literal ID="LitError" runat="server" Text="xxx"></asp:Literal></p>
                        </div>
                       <div  class="text-center">
                           <asp:Button ID="BtnClose"  Text="CLOSE" class="btn btn-primary" runat="server" OnClick="BtnClose_Click" />
                       </div>
                      <%--  <div class="panel-footer">
                            Panel Footer
                        </div>--%>
                    </div>
      </div></div>
    
    
                <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Dashboard</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
               
    <%--<div class="fancybox">
        aaaa
    </div>
  <a class="various" data-fancybox-type="iframe" href="/Contents/DeleteConfirm.aspx">Iframe</a>--%>
   
            <!-- /.row -->
            <div class="row">
                
                <div class="col-lg-12">
                     
                    <div class="panel panel-default">
                        <div class="panel-heading">
                             Dashboard data:
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                           <h3> Qui eventualmente andranno delle statistiche generali di re2017 (profitti, fondo cassa, andamento negli anni ecc...)</h3>
                             <div id="morris-area-chart"></div>
         <%--                    <div class="row">
                <div class="col-lg-12 text-right">
                   <a class="btn btn-success" href="UserDetail.aspx"><i class="fa fa-pencil"></i> Insert</a>
                 </div></div>
                            <br />
                             
                            <table width="100%" class="table table-striped table-bordered table-hover" id="dataTables-example">
                              
                                    <thead>
                                    <tr>
                                        <th>Name</th>
                                        <th>UserId</th>
                                        <th>Role</th>
                                        <th style="width: 30px">Enabled</th>
                                        <th data-sortable="false" style="width:200px"></th>
                                    </tr>
                                </thead>
                                <tbody>
                                      <asp:Literal ID="LitContentTable" runat="server"></asp:Literal>
                       
                                 
                                </tbody>
                            </table>--%>

                     
                        </div>
                        <!-- /.panel-body -->
                    </div>
                    <!-- /.panel -->
                </div>
                <!-- /.col-lg-12 -->
            </div>
                <div class="row">
              
                <!-- /.col-lg-6 -->
                <div class="col-lg-6">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Bar Chart Example
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <div id="morris-bar-chart"></div>
                        </div>
                        <!-- /.panel-body -->
                    </div>
                    <!-- /.panel -->
                </div>
                <!-- /.col-lg-6 -->
                
                <!-- /.col-lg-6 -->
                <div class="col-lg-6">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Donut Chart Example
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <div id="morris-donut-chart"></div>
                        </div>
                        <!-- /.panel-body -->
                    </div>
                    <!-- /.panel -->
                </div>
                <!-- /.col-lg-6 -->
               
                <!-- /.col-lg-6 -->
            </div>
           <input id="HydIdToDelete" runat="server" type="hidden"/>
      <script>

          function ShowDelForm(IdUser)
          {
             
              document.getElementById('<%= HydIdToDelete.ClientID %>').value = IdUser;
              var DivDelete = document.getElementById('<%= DivDelete.ClientID %>');
              DivDelete.classList.remove("Disattivato");
              DivDelete.className += " Attivo";
           
        }
      
    </script>
</asp:Content>
