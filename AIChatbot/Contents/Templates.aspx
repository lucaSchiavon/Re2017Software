<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/ChatBotGskAdm.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Templates.aspx.cs" Inherits="Ls.Re2017.Contents.Templates" %>
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
                    <h1 class="page-header">Templates</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
               
   
            <!-- /.row -->
            <div class="row">
                
                <div class="col-lg-12">
                     
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            List of Templates...
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
           <%--                  <div class="row">
                <div class="col-lg-12 text-right">
                 <a class="various btn btn-success" data-fancybox-type="iframe" href="/Contents/ImportTags.aspx"><i class="fa fa-pencil"></i> Import tags</a>&nbsp&nbsp<a class="btn btn-success" href="TagDetail.aspx"><i class="fa fa-pencil"></i> Insert</a>
                 </div></div>--%>
                              <div class="row">
                <div class="col-lg-12 text-right">
                   <a class="btn btn-success" href="TemplateDetail.aspx"><i class="fa fa-pencil"></i> Insert</a>
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
                                        <th>Description</th>       
                                        <th style="width: 30px">Enabled</th>
                                        <th data-sortable="false" style="width:200px"></th>
                                    </tr>
                                  </thead>
                                <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <tr class="gradeA odd" role="row">
                                       <td><%#Eval("description") %></td>
                                       <td><%#Eval("enabled") %></td> 
                                       <td class='center'><a class='btn btn-primary' href='TemplateDetail.aspx?Id=<%#Eval("Id") %>'><i class='fa fa-edit'></i> Update</a></td>

                                   </tr>     
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                     <tr class="gradeA even" role="row">
                                         <td><%#Eval("description") %></td>
                                         <td><%#Eval("enabled") %></td>
                                         <td class='center'><a class='btn btn-primary' href='TemplateDetail.aspx?Id=<%#Eval("Id") %>'><i class='fa fa-edit'></i> Update</a></td>
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
