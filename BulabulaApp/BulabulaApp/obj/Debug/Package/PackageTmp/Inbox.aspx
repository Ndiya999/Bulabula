<%@ Page Title="Inbox" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Inbox.aspx.cs" Inherits="BulabulaApp.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">







<div id="leftColumn" class="floatLeft ">
<div class="buttons" >
 
<%--<asp:HyperLink ID="lnkCompose" class="button big floatLeft" runat="server" NavigateUrl="~/ComposeMessage.aspx" Text="Compose" />--%>
<%--<a class="button big floatLeft" href="../Messages/ComposeMessage.aspx">Compose</a>--%>
<a class="button left big floatLeft" href="#">Refresh</a>
<a id = 'deleteAllMessages' class="button middle big floatLeft" href="#">Delete All</a>
<a class="button right big floatright tip" href="#" style="font-size: 1.5em; line-height:15px; margin-right: 0;" id="nextMsge" title="View older messages" >></a>
<a class="button left big floatright tip" href="#" style="font-size: 1.5em; line-height:15px;" id="prevMsge" title="View newer messages"><</a>


</div>

  <%= items %>
   

        <input type =hidden name ="__EVENTTARGET" value ="">
        <input type =hidden name ="__EVENTARGUMENT" value ="">

</div>


<div id ="loading">



</div>

<div id="filterMenu" class="ui-corner-bl ui-corner-br" >
<ul>
<li><a class="me" href="#" title="List messages by date">Date</a></li>
<li><a class="" href="#" title="List only messages sent to you">Sent to me</a></li>
<li><a class="" href="#" title="List uread messages">Unread</a></li>
<li></li>

</ul>

</div>
<input type="text" id="focusHack" style="display: none"/>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="RightColumnContentPlaceHolder" runat="server">
</asp:Content>
