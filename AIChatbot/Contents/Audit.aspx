<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/ChatBotGskAdm.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Audit.aspx.cs" Inherits="Ls.Re2017.PigeonCms.pgn_content.Contents.Audit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript">
    function fileinfo()
    {
        document.getElementById('<%=TextBox1.ClientID%>').value = document.getElementById('<%=FileUpload1.ClientID%>').value;
    }
</script>
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
                    <h1 class="page-header">Tracks import</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
               
    <asp:FileUpload ID="FileUpload1" runat="server" onchange="fileinfo()" Style="display:none;" />
            <!-- /.row -->
            <div class="row">
                                 <div class="col-lg-12">
                <div class="panel panel-default">
                        <div class="panel-heading">
                            import bank transactions (.csv file)
                            
                        </div>
                        <div class="panel-body">
                      <div class="col-lg-1">
                                  <div class="form-group">
                                            <label></label>
                                           <br /> 
                                     
                                         <asp:linkbutton id="LnkBtnBrowse" runat="server" class="btn btn-primary" OnClick="BtnFilter_Click"><%--<i class='fa fa-filter'></i>--%>Browse</asp:linkbutton>         
                                                                  
                                        </div>
                                       </div>
                               <div class="col-lg-5">
                                        <div class="form-group">
                                            <label></label>
                                       <asp:TextBox ID="TextBox1" runat="server"  class="form-control"></asp:TextBox>      
                                        </div>
                                      </div>
                                   <div class="col-lg-5">
                                        <div class="form-group">
                                            <label></label>
                                           <asp:DropDownList ID="CboBank" runat="Server" class="form-control"></asp:DropDownList>
                                        </div>
                                      </div>
                             <div class="col-lg-1">
                                  <div class="form-group">
                                            <label></label>   
                                        <br />  
                                           <asp:linkbutton id="LnkBtnUploadTrack" runat="server" class="btn btn-primary" ><i class='fa fa-upload'></i> Upload</asp:linkbutton>                                                                           
                                        </div>
                                       </div>
                      
                            

                               
                        </div>
                      
                    </div>
                     </div>
                 <div class="col-lg-12">
                <div class="panel panel-default">
                        <div class="panel-heading">
                            Filters
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-3">
                                  
                                        <div class="form-group">
                                            <label>Da</label>
                                            <asp:TextBox ID="TxtDa" runat="server" type="date" class="form-control"></asp:TextBox>                                        
                                        </div>
                                 </div>
                             <div class="col-lg-3">
                                  <div class="form-group">
                                            <label>A</label>                                          
                                       <asp:TextBox ID="TxtA" runat="server" type="date" class="form-control"></asp:TextBox>                                        
                                        </div>
                                       </div>
                             <div class="col-lg-3">
                                        <div class="form-group">
                                            <label>Users</label>
                                           <asp:DropDownList ID="CboUsers" runat="Server" class="form-control"></asp:DropDownList>
                                        </div>
                                      </div>
                             <div class="col-lg-3" style="text-align:center;Padding-top:3px">
                                  <div class="form-group">
                                            <label></label>
                                           <br />     
                                         <asp:linkbutton id="LnkBtnFilter" runat="server" class="btn btn-primary" OnClick="BtnFilter_Click"><i class='fa fa-filter'></i> Filter</asp:linkbutton>                                         
                                        </div>
                                       </div>

                               
                        </div>
                      
                    </div>
                     </div>
                <div class="col-lg-12">
                     
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            User's operations recorded...
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
         
                            <div class="row"><div class="col-sm-6"><div class="dataTables_length" id="dataTables-example_length"><label>Show <asp:DropDownList ID="CboRowsInPages" runat="server" AutoPostBack="true" class="form-control input-sm" OnSelectedIndexChanged="CboRowsInPages_SelectedIndexChanged">
                                   <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                <asp:ListItem Enabled="true" Text="100" Value="100"></asp:ListItem>
                                                 <asp:ListItem Text="200" Value="200"></asp:ListItem>
                                                 <asp:ListItem Text="300" Value="300"></asp:ListItem>
                                                 <asp:ListItem Text="500" Value="500"></asp:ListItem>
                                  </asp:DropDownList> entries</label></div></div></div>
                           <asp:Repeater ID="Repeater1" runat="server">
                                    <HeaderTemplate>
                                     <table width="100%" class="table table-striped table-bordered table-hover" id="dataTables-example">
                                    <thead>
                                    <tr>
                                        <th  style="width: 150px">User</th>
                                        <th>Role</th>
                                        <th style="width: 150px">Date/Time</th>
                                        <th>Description</th>
                                    </tr>
                                  </thead>
                                <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <tr class="gradeA odd" role="row"><td><%#Eval("AuditUser") %></td><td><%#Eval("Role") %></td><td><%#Eval("ModTime") %></td><td><%#Eval("Description") %></td></tr>     
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                     <tr class="gradeA even" role="row"><td><%#Eval("AuditUser") %></td><td><%#Eval("Role") %></td><td><%#Eval("ModTime") %></td><td><%#Eval("Description") %></td></tr>
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

</asp:Content>
