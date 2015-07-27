<%@ Page Title="Home" Language="C#"  MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="BulabulaApp.Home" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">


    <%-- LEFT COLLUMN--%>
 <div id="leftColumn" class="floatLeft " >



   <div id="tabs" class="ui-tabs ui-widget ui-widget-content ui-corner-all" style="width: 102.5%;">
			<ul class="ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">
				<li class="ui-state-default ui-corner-top ui-tabs-selected ui-state-active"><a id='Posts_Tab' href="#Posts">News Feed</a></li>
                	<li class="ui-state-default ui-corner-top "><a id ='Text_Tab' href="#Text">Text</a></li>
				<li class="ui-state-default ui-corner-top "><a id ='Pictures_Tab' href="#Pictures">Pictures</a></li>
				<li class="ui-state-default ui-corner-top"><a id ='Videos_Tab' href="#Videos">Videos</a></li>
               <%-- <li class="ui-state-default ui-corner-top "><a id ='Articles_Tab' href="#Articles">Articles</a></li>--%>
				<li class="ui-state-default ui-corner-top"><a id ='Files_Tab' href="#Files">Files</a></li>
                <li class="ui-state-default ui-corner-top"><a id ='Events_Tab' href="#Events">Events</a></li>
                </ul>

			<div id="Posts"    class="ui-tabs-panel ui-widget-content ui-corner-bottom ui-tabs-hide">  <%= newsfeed%></div>
            <div id="Text" class="ui-tabs-panel ui-widget-content ui-corner-bottom ui-tabs-hide">
           </div>
			<div id="Pictures" class="ui-tabs-panel ui-widget-content ui-corner-bottom ui-tabs-hide"></div>
			<div id="Videos"   class="ui-tabs-panel ui-widget-content ui-corner-bottom ui-tabs-hide"></div>
           <%-- <div id="Articles" class="ui-tabs-panel ui-widget-content ui-corner-bottom ui-tabs-hide"></div>--%>
			<div id="Files"    class="ui-tabs-panel ui-widget-content ui-corner-bottom ui-tabs-hide"></div>
            <div id="Events"    class="ui-tabs-panel ui-widget-content ui-corner-bottom ui-tabs-hide"></div>
         <%--  <div class="postsHeadingSml "><a id="loadMore" class="button left big" >Load more posts</a></div>--%>
		</div>



   

</div>
    <asp:HiddenField ID="ThisFileToDownload" runat="server" value=""/>





  


  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="RightColumnContentPlaceHolder" runat="server">
  
</asp:Content>
