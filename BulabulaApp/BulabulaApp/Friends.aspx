<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Friends.aspx.cs" Inherits="BulabulaApp.WebForm2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

      
       <%-- <div id="innerWrapper" class="BorberRad5" >--%>
 
         
          

   <%-- LEFT COLLUMN--%>
 <div id="leftColumn" class="floatLeft " > 

 <%-- BLOCKED FRIENDS--%>
 <div id="blockedFriendsContainer" class="FriendsMiniWraper ui-corner-all ">
<%-- <h4 class="shinyRed ui-corner-top" >Blocked People (<span>12</span>)</h4>--%>
 <%= CountBlockedFriendsString%>
 <ul class="friendContentArea ui-corner-bottom">
 <%= AllBlockedFriendsString %>
 </ul>
 </div>
 <%-- END BLOCKED FRIENDS--%>

 <%-- FRIEND INVITES--%>
  <div id="FriendInvitesContainer"  class="FriendsMiniWraper ui-corner-all">
 <%--<h4  class="shinyLightBlue ui-corner-top" >Invites</h4>--%>
 <%= CountFriendsInvitesString%>
    <ul class="friendContentArea ui-corner-bottom" >
 <%= AllFriendsInvitesString%>
 </ul>
 </div>
  <%--END  FRIEND INVITES--%>

   <%--ALL CURRENT FRIENDS--%>
 <div  id="allFriendsContainer" class="FriendsMiniWraper ui-corner-all">
  <%--<h4  class="shinyGreen ui-corner-top" >All Friends</h4>--%>
  <%= CountFriendsString%>
   <ul class="friendContentArea ui-corner-bottom">
 <%= AllFriendsString%>
 </ul>
 </div>
 
  





   <%--END ALL CURRENT FRIENDS--%>
 </div>

<%-- <asp:HiddenField ID="profileFriendID" runat="server" />--%>

<%--
        </div>--%>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="RightColumnContentPlaceHolder" runat="server">
 
</asp:Content>
