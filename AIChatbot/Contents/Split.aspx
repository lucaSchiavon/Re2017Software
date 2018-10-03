<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/ChatBotGskAdm.Master" MaintainScrollPositionOnPostback="true"   AutoEventWireup="true" CodeBehind="Split.aspx.cs" Inherits="Ls.Re2017.Contents.Split" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <asp:Literal ID="LitRe2017ScriptInject" runat="server"></asp:Literal>
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
                      <%--  <div class="panel-footer">
                            Panel Footer
                        </div>--%>
                    </div>
      </div></div>
    
    
                <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Split selected transaction</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
              
  
   
            <!-- /.row -->
            <div class="row">
                <div class="col-lg-12">
                     
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            Data summary of transaction to split:
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
         
                      
     
                           
                            <div style="text-align:right">
                                 <asp:linkbutton id="LnkBtnBack" runat="server" class="btn btn-primary" OnClick="BtnBack_Click"><i class='fa fa-long-arrow-left'></i> Back</asp:linkbutton>   
                            </div>
          
                            
                        </div>
                        <!-- /.panel-body -->
                         
                    </div>
                    <!-- /.panel -->
                </div>
                <!-- /.col-lg-12 -->
                       
                <div class="col-lg-12">
                     
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            splitted row section:
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
           <div class="col-lg-2">
                          
                                 <label>Select number of row:</label>
                                 <input type="number" id="CboSplitNumber" runat="server"  class="form-control" step="any" value="1"></input>
                              
                                </div>
                <div class="col-lg-1" style="padding-top:25px">
                          
                               
                                <asp:linkbutton id="LnkBtnSplit" runat="server" class="btn btn-primary" ><i class='fa fa-align-justify'></i> Create rows</asp:linkbutton>   
                              
                                </div>
                            <div class="col-lg-12">
                                    
                                  <asp:Repeater ID="RptSelEvt" runat="server" OnItemDataBound="RptSelEvt_ItemDataBound">
                                    <HeaderTemplate>
                                     <table width="100%" class="table table-striped table-bordered table-hover" id="dataTables-example">
                                    <thead>
                                    <tr>
                                        <th  style="width: 20px">Id</th>
                                        <th style="width: 150px">Date</th>
                                        <th>Bank</th>
                                         <th>Amount</th>
                                        <th>Description</th>
                                         <th>Event</th>
                                         <th>House</th>
                                        
                                    </tr>
                                  </thead>
                                <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <tr class="gradeA odd" role="row"><td><%#Eval("id") %></td><td><%#Eval("date") %></td><td><%#Eval("bankReportEntryId") %></td><td><span style="color:green"><asp:label ID="LblAmount" runat="server" Text='<%#Eval("amount") %>'></asp:label></span></td><td><%#Eval("description") %></td><td><asp:DropDownList disabled ID="CboEventi" MemId='<%#Eval("eventTypeId") %>' MemIdEvt='<%#Eval("id") %>' runat="server" class="form-control"></asp:DropDownList></td><td><asp:DropDownList disabled ID="CboCase" MemId='<%#Eval("houseId") %>' MemIdEvt='<%#Eval("id") %>' runat="server" class="form-control"></asp:DropDownList></td></tr>     
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                     <tr class="gradeA even" role="row"><td><%#Eval("id") %></td><td><%#Eval("date") %></td><td><%#Eval("bankReportEntryId") %></td><td><asp:label ID="LblAmount" runat="server" Text='<%#Eval("amount") %>'></asp:label></td><td><%#Eval("description") %></td><td><asp:DropDownList disabled ID="CboEventi" MemId='<%#Eval("eventTypeId") %>' MemIdEvt='<%#Eval("id") %>' runat="server" class="form-control"></asp:DropDownList></td><td><asp:DropDownList disabled ID="CboCase" MemId='<%#Eval("houseId") %>' MemIdEvt='<%#Eval("id") %>' runat="server" class="form-control"></asp:DropDownList></td></tr>
                                </AlternatingItemTemplate>
                                <FooterTemplate>
                                   </tbody>
                            </table>
                                </FooterTemplate>
                             </asp:Repeater>
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
