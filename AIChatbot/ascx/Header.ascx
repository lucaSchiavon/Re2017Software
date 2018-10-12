<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="AQuest.ChatBotGsk.PigeonCms.pgn_content.ascx.Header" %>

        <%@ Register src="LateralMenu.ascx" tagname="LateralMenu" tagprefix="uc1" %>

        <!-- Navigation -->
        <nav class="navbar navbar-default navbar-static-top" role="navigation" style="margin-bottom: 0;">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <div style="float:left;padding-left:10px"><a  href="Default.aspx"><img  src="../Public/Images/LogoREM.png"/ style="width:80px;float:left"></a></div><div style="padding-left:267px"><asp:Label class="navbar-brand"  id="LblUsername" runat="server"></asp:Label></div>
            </div>
            <!-- /.navbar-header -->

            <ul class="nav navbar-top-links navbar-right">
                <!-- /.dropdown -->
                <li class="dropdown">
                    <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                        <i class="fa fa-user fa-fw"></i> <i class="fa fa-caret-down"></i>
                    </a>
                    <ul class="dropdown-menu dropdown-user">
                       <%-- <li><a href="#"><i class="fa fa-user fa-fw"></i> User Profile</a>
                        </li>
                        <li><a href="#"><i class="fa fa-gear fa-fw"></i> Settings</a>
                        </li>
                        <li class="divider"></li>--%>
                        <li><a href="/Contents/Logout.aspx"><i class="fa fa-sign-out fa-fw"></i> Logout</a>
                        </li>
                    </ul>
                    <!-- /.dropdown-user -->
                </li>
                <!-- /.dropdown -->
            </ul>
            <!-- /.navbar-top-links -->

            <!-- menu laterale-->
          <uc1:LateralMenu ID="LateralMenu1" runat="server" />
              <!-- menu laterale-->
            <!-- /.navbar-static-side -->
           
        </nav>
