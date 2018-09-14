<%@ Page Title="" Language="C#" MasterPageFile="~/masterpages/ChatBotGskAdm.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="ImageDetail.aspx.cs" Inherits="AQuest.ChatBotGsk.PigeonCms.pgn_content.Contents.ImageDetail" %>
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
                    <h1 class="page-header">Image's Form</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>

                <div class="row">
                <div class="col-lg-6">
                   
                         <div class="panel panel-primary">
                        <div class="panel-heading">
                            
                         <asp:Literal ID="LitEntity" runat="server"></asp:Literal>
                        </div>
                        <div class="panel-body">
                           
                 <%--  <h3>Document's heading info:</h3>--%>
               
                             
                            <div class="row">
                                <div class="col-lg-9">
                                 
                                        <div class="form-group" id="Fg_TxtNomeImmagine">
                                            <label>Image name</label>
                                            <asp:TextBox ID="TxtNomeImmagine" runat="server" class="form-control"></asp:TextBox>
                                            <%--<p class="help-block"></p>--%>
                                        </div>
                                       </div>
                                    <div class="col-lg-3">
                                     
                                         <div class="form-group">
                                            <label>Enabled</label>
                                             <asp:DropDownList ID="CboEnable" runat="server" class="form-control">                                                 
                                                 <asp:ListItem Enabled="true" Text="YES" Value="True"></asp:ListItem>
                                                 <asp:ListItem Text="NO" Value="False"></asp:ListItem>
                                             </asp:DropDownList>
                                         
                                        </div>
                                        </div>
                                   
                                  <div class="col-lg-6">
                                           <div class="form-group" id="Fg_TxtArgument">
                                            <label>Argument</label>
                                              <asp:TextBox ID="TxtArgument" runat="server" class="form-control"></asp:TextBox>
                                         
                                        </div>
                                         </div>
                                      <div class="col-lg-6">
                                         <div class="form-group" id="Fg_TxtDevice">
                                            <label>Device</label>
                                              <asp:TextBox ID="TxtDevice" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                          </div>
                                  <div class="col-lg-12">
                                         <div class="form-group" id="Fg_TxtAlias">
                                            <label>Alias (insert comma-separated aliases if they exist)</label>
                                              <asp:TextBox ID="TxtAlias" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                          </div>
                                <div class="col-lg-12">
                                         <div class="form-group" id="Fg_TxtTags">
                                            <label>Tags</label><br />
                                              <asp:TextBox ID="TxtTags" data-role="tagsinput" Style="width:100%;font-size:95%" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                          </div>
                                  <div class="col-lg-12">
                                         <div class="form-group" id="Fg_TxtDescription">
                                            <label>Description</label>
                                              <asp:TextBox ID="TxtDescription" TextMode="MultiLine" Rows="5" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                          </div>
                                  
                           <input type="hidden" id="HidTempFileName" visible="true" runat="server" />
                                  <input type="hidden" id="HidTempFileNamePlusExt" visible="true" runat="server" />
                                <asp:placeholder id="PhdDropZone" runat="server" Visible="false">
              <div class="col-lg-12" style="Padding-bottom:20px">
           <%-- <asp:LinkButton ID="lbtUsername" runat="server" Font-Names="Verdana" Text="Dotnetdrizzles@gmail.com" Font-Size="Large" Font-Underline="True" ForeColor="#FF0066"></asp:LinkButton>--%>
              <div id="dZUpload" style="width:90%; margin:0 auto;" class="dropzone" >
        <div  class="dz-default dz-message">
           Drag & Drop PDF file here. 
            <br />
        </div>
    </div> 
        </div>
                                    </asp:placeholder>
                                  <asp:placeholder id="PhdLnkPdfDoc" runat="server" Visible="false">
                                 <div class="col-lg-12" style="Padding-bottom:20px">
                                      <div class="form-group">
                                            <label>Image preview</label><br />
                                          <asp:Image ID="ImgPreview" runat="server" />
                                   
                                         </div>
                                     </div>
                             </asp:placeholder>
                                <div class="col-lg-12" style="text-align:right">
                                    <%-- <asp:Button Id="BtnBack" runat="server"  Text="Back" class="btn btn-primary" OnClick="BtnBack_Click" />--%>
                                    <asp:HyperLink ID="HypBack" runat="server" NavigateUrl="Images.aspx" class="btn btn-primary"><i class='fa fa-arrow-left'></i> Back</asp:HyperLink>&nbsp&nbsp&nbsp&nbsp&nbsp
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
