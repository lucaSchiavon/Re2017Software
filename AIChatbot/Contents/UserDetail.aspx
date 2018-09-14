<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/ChatBotGskAdm.Master" AutoEventWireup="true" CodeBehind="UserDetail.aspx.cs" Inherits="AQuest.ChatBotGsk.PigeonCms.pgn_content.Contents.UserDetail" %>
<%--<%@ MasterType virtualpath="~/masterpages/ChatBotGskAdm.Master" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div id="DivError" runat="server" class="ParentDivDeleting Disattivato"><div class="InnerDivDeleting">
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
      </div></div>
    
                 <div class="row">
                <div class="col-lg-6">
                    <h1 class="page-header">User's Form</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>

                <div class="row">
                <div class="col-lg-6">
                         <div class="panel panel-default">
                        <div class="panel-heading">
                         <asp:Literal ID="LitUser" runat="server"></asp:Literal>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-6">
                                 
                                        <div class="form-group" id="Fg_TxtNomeUtente">
                                            <label>Name</label>
                                            <asp:TextBox ID="TxtNomeUtente" runat="server" class="form-control"></asp:TextBox>
                                            <%--<p class="help-block"></p>--%>
                                        </div>
                                       </div>
                                    <div class="col-lg-6">
                                     
                                         <div class="form-group">
                                            <label>Enabled</label>
                                             <asp:DropDownList ID="CboEnable" runat="server" class="form-control">                                                 
                                                 <asp:ListItem Enabled="true" Text="YES" Value="True"></asp:ListItem>
                                                 <asp:ListItem Text="NO" Value="False"></asp:ListItem>
                                             </asp:DropDownList>
                                          <%--  <select class="form-control">
                                                <option>--Select--</option>
                                                <option>YES</option>
                                                <option>NO</option>
                                            </select>--%>
                                        </div>
                                        </div>
                                   
                                  <div class="col-lg-6">
                                           <div class="form-group" id="Fg_CboRoles">
                                            <label>Role</label>
                                               <asp:DropDownList ID="CboRoles" runat="server" class="form-control"></asp:DropDownList>
                                         
                                        </div>
                                         </div>
                                      <div class="col-lg-6">
                                         <div class="form-group" id="Fg_TxtUsername">
                                            <label for="inputError">Username</label>
                                              <asp:TextBox ID="TxtUsername" runat="server" class="form-control"></asp:TextBox>
                                           <%-- <input class="form-control">--%>
                                          <%--  <p class="help-block">Example block-level help text here.</p>--%>
                                        </div>
                                          </div>
                                 <div class="col-lg-6">
                                         <div class="form-group" id="Fg_TxtPassword">
                                            <label>Password</label>
                                              <asp:TextBox ID="TxtPassword"  runat="server" class="form-control"></asp:TextBox>
                                           <%-- <input class="form-control">--%>
                                          <%--  <p class="help-block">Example block-level help text here.</p>--%>
                                        </div>
                                        </div>
                      <%--           <div class="col-lg-6">
                                  
                                     <div class="form-group">
                                            <label>Confirm password</label>
                                           <asp:TextBox ID="TxtConfirmPassword" runat="server" class="form-control"></asp:TextBox>
                                         
                                        
                                        </div>
                                </div>--%>
                             
                                <div class="col-lg-12" style="text-align:right">
                                    <%-- <asp:Button Id="BtnBack" runat="server"  Text="Back" class="btn btn-primary" OnClick="BtnBack_Click" />--%>
                                    <asp:HyperLink ID="HypBack" runat="server" NavigateUrl="Users.aspx" class="btn btn-primary"><i class='fa fa-arrow-left'></i> Back</asp:HyperLink>&nbsp&nbsp&nbsp&nbsp&nbsp
                                 <%--   <asp:Button Id="BtnSalva" runat="server"  Text="Save" class="btn btn-primary" OnClick="BtnSalva_Click"/>--%>
                                    <asp:linkbutton id="LnkBtnSalva" runat="server" class="btn btn-primary" OnClick="BtnSalva_Click"><i class='fa fa-arrow-down'></i> Save</asp:linkbutton>
                                   <%-- <button type="submit" class="btn btn-default">Submit Button</button>
                                        <button type="reset" class="btn btn-default">Reset Button</button>--%>
                                    </div>
                                        
                                 
                               
                                <!-- /.col-lg-6 (nested) -->
                              
                                <!-- /.col-lg-6 (nested) -->
                            </div>
                            <!-- /.row (nested) -->
                        </div>
                        <!-- /.panel-body -->
                    </div>
                </div></div>

     
            <!-- /.row -->
          <%-- <asp:Literal Id="LitScriptLightBox" runat="server"></asp:Literal>
    <a id="lightbox" style="display:none" href="/Contents/UserDetail.aspx?Id=3">&nbsp;</a>--%>
</asp:Content>
