<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/ChatBotGskAdm.Master" AutoEventWireup="true" CodeBehind="Images.aspx.cs" Inherits="AQuest.ChatBotGsk.PigeonCms.pgn_content.Contents.Images" %>
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
                    
                    </div>
      </div></div>
    
    
                <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Images</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
               
   
            <!-- /.row -->
            <div class="row">
                
                <div class="col-lg-12">
                     
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            List of Images...
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                             <div class="row">
                <div class="col-lg-12 text-right">
                   <a class="btn btn-success" href="ImageDetail.aspx"><i class="fa fa-pencil"></i> Insert</a>
                 </div></div>
                 <div class="row"><div class="col-sm-6"><div class="dataTables_length" id="dataTables-example_length"><label>Show <asp:DropDownList ID="CboRowsInPages" runat="server" AutoPostBack="true" class="form-control input-sm" OnSelectedIndexChanged="CboRowsInPages_SelectedIndexChanged">
                 <asp:ListItem Text="50" Value="50"></asp:ListItem>
                <asp:ListItem Enabled="true" Text="100" Value="100"></asp:ListItem>
                                    <asp:ListItem Text="200" Value="200"></asp:ListItem>
                                    <asp:ListItem Text="300" Value="300"></asp:ListItem>
                                    <asp:ListItem Text="500" Value="500"></asp:ListItem>
                    </asp:DropDownList> entries</label></div></div>

                    </div>


                            <asp:Repeater ID="Repeater1" runat="server">
                                    <HeaderTemplate>
                                     <table width="100%" class="table table-striped table-bordered table-hover" id="dataTables-example">
                                    <thead>
                                    <tr>
                                         <th  style="width:80px"></th>
                                        <th>Name</th>
                                        <th>Argument</th>
                                        <th>Device</th>
                                         <th>Description</th>
                                        <th style="width: 30px">Enabled</th>
                                        <th data-sortable="false" style="width:200px"></th>
                                    </tr>
                                  </thead>
                                <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <tr class="gradeA odd" role="row">
                                       <td><a id="single_image" class="fancybox" href='<%#Eval("UrlImage") %>'><img src='<%#Eval("UrlImageSmall") %>' /></a></td>
                                       <td><%#Eval("ImageName") %></td>
                                       <td><%#Eval("Argument") %></td>
                                       <td><%#Eval("Device") %></td>
                                        <td><%#Eval("Description") %></td>
                                       <td><%#Eval("Enabled") %></td>
                                       <td class='center'><a class='btn btn-primary' href='ImageDetail.aspx?Id=<%#Eval("IdImage") %>'><i class='fa fa-edit'></i> Update</a>&nbsp&nbsp&nbsp<a class='btn btn-danger' href='javascript:ShowDelForm(<%#Eval("IdImage") %>);'><i class='fa fa-times'></i> Delete</a></td>

                                   </tr>     
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                     <tr class="gradeA even" role="row">
                                         <td><a id="single_image" class="fancybox" href='<%#Eval("UrlImage") %>'><img src='<%#Eval("UrlImageSmall") %>' /></a></td>
                                       <td><%#Eval("ImageName") %></td>
                                       <td><%#Eval("Argument") %></td>
                                       <td><%#Eval("Device") %></td>
                                        <td><%#Eval("Description") %></td>
                                       <td><%#Eval("Enabled") %></td>
                                         <td class='center'><a class='btn btn-primary' href='ImageDetail.aspx?Id=<%#Eval("IdImage") %>'><i class='fa fa-edit'></i> Update</a>&nbsp&nbsp&nbsp<a class='btn btn-danger' href='javascript:ShowDelForm(<%#Eval("IdImage") %>);'><i class='fa fa-times'></i> Delete</a></td>
                                     </tr>
                                </AlternatingItemTemplate>
                                <FooterTemplate>
                                   </tbody>
                            </table>
                                </FooterTemplate>
                             </asp:Repeater>
                            
                             <div class="row">
                            <div class="col-sm-6"><div class="dataTables_info" id="dataTables-example_info" role="status" aria-live="polite"><asp:Literal id="LitShowOneOf" runat="server"></asp:Literal>
           </div></div>

                                  <div class="col-sm-6">
                                      <div class="dataTables_paginate paging_simple_numbers" id="dataTables-example_paginate">
                                        <ul class="pagination">
                                            <li class="paginate_button previous <%=HidePreviousClass%>" aria-controls="dataTables-example" tabindex="0" id="dataTables-example_previous">
                                                <asp:LinkButton ID="BtnPrevious" OnClick="BtnPrevious_Click" runat="server">Previous</asp:LinkButton>
                                            </li>
                                             <li id="Li" runat="server" class="paginate_button active" aria-controls="dataTables - example" tabindex="0">
                                                <asp:LinkButton ID="btnPage"  CommandName="Page" Text="pag. 1"  runat="server"></asp:LinkButton>
                                            </li>                                                   
                                            <li class="paginate_button next <%=HideNextClass%>" aria-controls="dataTables-example" tabindex="0" id="dataTables-example_next">
                                                  <asp:LinkButton ID="BtnNext" OnClick="BtnNext_Click" runat="server">Next</asp:LinkButton>
                                            </li>
                                     </ul>
                                         
                                           
                            <asp:Repeater ID="rptPaging" runat="server" >
                                 <HeaderTemplate>
                                  </HeaderTemplate>
                                <ItemTemplate>      
                                 </ItemTemplate>
                                <FooterTemplate>  
                                </FooterTemplate>
                                </asp:Repeater>

                                      </div></div>
                           
                                 </div>


                            <!-- /.table-responsive -->
                           <%-- <div class="well">
                                <h4>DataTables Usage Information</h4>
                                <p>DataTables is a very flexible, advanced tables plugin for jQuery. In SB Admin, we are using a specialized version of DataTables built for Bootstrap 3. We have also customized the table headings to use Font Awesome icons in place of images. For complete documentation on DataTables, visit their website at <a target="_blank" href="https://datatables.net/">https://datatables.net/</a>.</p>
                                <a class="btn btn-default btn-lg btn-block" target="_blank" href="https://datatables.net/">View DataTables Documentation</a>
                            </div>--%>
                        </div>
                        <!-- /.panel-body -->
                    </div>
                    <!-- /.panel -->
                </div>
                <!-- /.col-lg-12 -->
            </div>
           <input id="HydIdToDelete" runat="server" type="hidden"/>
      <script>

          function ShowDelForm(IdEntity)
          {
             
              document.getElementById('<%= HydIdToDelete.ClientID %>').value = IdEntity;
              var DivDelete = document.getElementById('<%= DivDelete.ClientID %>');
              DivDelete.classList.remove("Disattivato");
              DivDelete.className += " Attivo";
           
        }
      
    </script>
</asp:Content>
