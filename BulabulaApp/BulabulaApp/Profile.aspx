<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="BulabulaApp.Profile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta charset="utf-8" /> 
     <link href="~/Styles/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
     <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
     <link href="~/Styles/Site2.css" rel="stylesheet" type="text/css" />

     
     <style type="text/css" >
      #NavigationMenu ul{ margin-top: 15.5px !important;}
      #ptTimeSelectCntr{display:none !important;}
     </style>


    <link rel="shortcut icon" href="Images/logo16x16.ico" >
   <link rel="icon" type="image/gif" href="Images/animated_logo.gif" >

</head>
 <body style="background: #6c6c6c;">
    <form id="form1" runat="server">
 

     <div class="Menu">
   <div >
  
   <div class="floatLeft" id="logoDiv" >
       <asp:HyperLink ID="Logo" runat="server" NavigateUrl="~/Home.aspx" tooltip="Home"> <img src="images/Logo.png" alt="Home" /></asp:HyperLink>
       </div>
       <div class="floatLeft" id="menuDiv">
    <asp:Menu ID="NavigationMenu" runat="server" CssClass="menu floatLeft" EnableViewState="false" IncludeStyleBlock="false" Orientation="Horizontal">
                    <Items>
                     
                        <asp:MenuItem NavigateUrl="~/Profile.aspx" Text="Profile"/>
                        <asp:MenuItem  Text="Friends" NavigateUrl="~/friends.aspx"/>
                        
                       <asp:MenuItem  Text="Messages">
                           <asp:MenuItem Text="View Inbox" NavigateUrl="~/Inbox.aspx"> </asp:MenuItem>
                           <asp:MenuItem Text="Compose Message" NavigateUrl="~/ComposeMessage.aspx"> </asp:MenuItem>
                       </asp:MenuItem>

                       <asp:MenuItem  Text="More">
                      
                           <asp:MenuItem Text="Help" > </asp:MenuItem>
                       </asp:MenuItem>
                    </Items>

                 
                </asp:Menu> 
                </div>
          
                
                <div id="rightMenuControls" class="floatright ui-widget">
               
                   <asp:Button ID="btnLogout" runat="server" Text="Logout" 
                        CssClass="textShadow LogoutBtn floatright BorberRad3" 
                        onclick="btnLogout_Click"/>
                   <div id="ben" class="floatright"><asp:Button ID="searchBtn"  runat="server" Text="" CssClass=" tip floatright ui-corner-br ui-corner-tr" />
             
                    <input id="searchTxtBox" class=" searchTxtBox floatright blueFontTextColor ui-corner-bl ui-corner-tl" placeholder="Search here"/></div>
            

                    <asp:HiddenField ID="friendSearchID" runat="server" />






                </div>
       
                </div>
                
   </div>
   

<%--    BEGIN PAGE--%>
    <div class="page BorberRad5">
    <div class='headingMain'> 
   <h2 id="titlePage" class="floatLeft"><asp:Literal ID="pageTitle" runat="server" Text=""></asp:Literal></h2>
    <span id="displayNameTop" class="floatright">  <%= actualMemberDisplayName %></span>
    <div class='clr'></div> 
   
   </div>
   <div id="WrapperMain">
      
    <div id="innerWrapper" class="BorberRad5">
    <div  id="profileHeader" class="ui-corner-all"> 
    <%--Profile picture--%> 
        <asp:HiddenField ID="profilesMemnerID" runat="server"  />
        <asp:HiddenField ID="profileName" runat="server" />
       <div>
      <%-- <img src="Images/ProfilePic.png"  alt="Profile picture"  class="BorberRad5 floatLeft" style=" width: 108px; height:144px; padding: 2px; border: 1px solid #A3ADB5;"/>--%>
       <asp:Image ID="ProfileImage" runat="server" alt="Profile picture"  class="BorberRad5 floatLeft" style=" width: 108px; height:144px; padding: 2px; border: 1px solid #A3ADB5;"/>
       </div>
       <%--Mid-Section--%>
       <div style="padding: 5px 15px 0 10px; width: 56%;" class="floatLeft">
       <h4 style="font-size: 1.7em; color: #555555; padding-bottom: 15px;"><span id="ProfileName"> <%= heading%></span>'s Profile</h4>
       <%= mostRecentPostDate%>
       <%--<p style="font-size: 1.2em; color: #A3ADB5;">This is where the latest post <span style="color: #0e93be;">-3hour(s) ago</span></p>--%>
       </div>


       <%--Buttons--%>
       <div class="floatLeft" id="profileButtons" style="width:25%;">
      <%=profileButtons %>
       
       </div>
       <div style="clear:both;"></div>
    </div>
    <br />
    <div id="leftColumn" class="floatLeft " >
    <div id="tabs" class="ui-tabs ui-widget ui-widget-content ui-corner-all">
			<ul class="ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">
				<li class="ui-state-default ui-corner-top ui-tabs-selected ui-state-active"><a href="#tabs-1">Personal Information</a></li>

			</ul>
            <div id="tabs-1" class="ui-tabs-panel ui-widget-content ui-corner-bottom ui-tabs-hide">
            <table id="profilePersonalInfo">
            <tbody>
            
             <%= memberDataMain%>
            </tbody>
            </table>
           
            </div>
		
			
		</div>
         </div>

<div class="floatright rightMenu" id="rightColumn"  >
 
      <ul class="ui-corner-top">    
            <li class="ui-corner-top"><h4 class="ui-corner-top bluePc">Groups</h4></li>
         <%= allgroups %>
         
     </ul>
</div>
 
</div>
        </div>
       
 
    </div>
    <asp:HiddenField ID="profileFriendID" runat="server" />

     <div id="ProfileMsge" title="Send Message" style="display:none;">
  

    
        <asp:Label ID="LblRecipient" runat="server" Text="Recipient:" CssClass="messageLbls"></asp:Label>
          <br />
        <div id="recipient" class = "MContent_TxtRecipient BorberRad3 grayFontTextColor" > Ndiyakholwa Ndzawumbi</div>
 
        
       
  
        <asp:Label ID="LblMessage" runat="server" Text="Message:" CssClass="messageLbls"></asp:Label>
        <br />

        <asp:TextBox ID="messageTextarea" runat="server" TextMode="MultiLine" CssClass="input BorberRad3 commentBox" style="min-height:180px; width:98%;"></asp:TextBox>


       

  
    </div><%--END COMPOSER--%>

   <%-- <asp:HiddenField ID="GoToGroupPageID" runat="server" />--%>

        <div id="Help" title="Help" style="display: none;">
	<p id="Helploader">
    </p>
</div>
 <div id="reportFriend" style="display:none;"> <%--BEGIN REPORT--%>
     <input id="elementID" type="hidden" />
     <label >Options:</label>
     <ul>
            <li><input name="report" id="1" type="radio" checked="checked" value="I don't like this post"/> <label for="1">I don't like this <span>post</span></label></li>
            <li><input name="report" id="2" type="radio" value="It's harassing me"/><label for="2">It's harassing me</label></li>
            <li><input name="report" id="3" type="radio" value="It's harassing a friend"/><label for="3">It's harassing a friend</label></li>
            <li><input name="report" id="4" type="radio" value="Spam or scam"/><label for="4">Spam or scam</label></li>
            <li><input name="report" id="5" type="radio" value="Sexually explicit content"/><label for="5">Sexually explicit content</label></li>
            <li><input name="report" id="6" type="radio" value="Violence or harmful behavior"/><label for="6">Violence or harmful behavior</label></li> 
            <li><input name="report" id="7" type="radio" value="Hate speech or symbol"/><label for="7">Hate speech or symbol</label></li>
            <li><input name="report" id="8" type="radio" value="Illegal drug use"/><label for="8">Illegal drug use</label></li>
            <li><input name="report" id="9" type="radio" value="My friend's account might be compromised or hacked"/><label for="9">My friend's account might be compromised or hacked</label></li>
     </ul>
  <label>Email Address:</label><label style="float:right; color: red; font-size:0.8em;" class="noShow">Please enter a valid email address.</label>
   <input id="emailAddress" type="text"  class=" blueFontTextColor  tagSearchBox ui-corner-all " style="width: 97.7%;"/><span class="noShow" style="width: 3px; color:Red;">*</span>

  </div> <%--END REPORT--%>
  
   <%--Begin--%>
   <span style='width:24px; height:24px;' class="blueCircleLoader"></span>

    <div id="dialog-confirm" title="" style="display: none;">
	<p><span class="ui-icon ui-icon-circle-check" style="float:left; margin:0 7px 50px 0;">
    </span>Are you sure you want to delete this <span id="itemToDelete"></span> ?</p>
    </div>

   <footer id="footer" class="blueFontTextColor" >
   <span class=" floatLeft" style="padding: 12px 5px 12px 12px; color:White;">Bulabula &copy; 2012</span>
<asp:HiddenField ID="GoToGroupPageID" runat="server" Value="" ></asp:HiddenField>
   </footer>      
   
   
    
   
    </form>

      <%--Scripts--%>
<script type="text/javascript" src="Scripts/jquery-1.8.2.js"></script>
<%--  <script type="text/javascript" src="Scripts/jquery-ui-1.8.18.custom.min.js"></script>--%>
    <script src="Scripts/jquery-ui-1.8.23.js" type="text/javascript"></script>
   <script type="text/javascript" src="Scripts/jquery.tipTip.js"></script>
    <script type="text/javascript" src="Scripts/jquery.ptTimeSelect.js"></script>

          <script src="Scripts/jquery-ui-1.8.23.js" type="text/javascript"></script>
    <script src="Scripts/jquery.signalR-0.5.3.js" type="text/javascript"></script>
     <script src="signalr/hubs" type="text/javascript"></script>
   <script type="text/javascript" src="Scripts/jquery.BetterGrow.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.placeholder.min.js"></script>
   <script type="text/javascript" src="Scripts/jquery.address-1.4.min.js"></script>
     <script  charset='utf-8' type="text/javascript" src="Scripts/popbox.min.js"></script>
  <script  type="text/javascript"  src="Scripts/bulabula.js"></script>
    

</body>
</html>

