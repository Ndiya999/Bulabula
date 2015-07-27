<%@ Page Title="View a message" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ViewMessages.aspx.cs" Inherits="BulabulaApp.WebForm4" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

 



<div id="leftColumn" class="floatLeft ">
 <div class="buttons" >
 
<%--<asp:HyperLink ID="lnkCompose" CssClass="button big floatLeft" runat="server" NavigateUrl="~/ComposeMessage.aspx" Text="Compose" />
<a class="button left big floatLeft" id="reply" >Reply</a>
<a class="button right big floatLeft "  id="filter">More</a>--%>
<a class="button big floatLeft"  id="btnDeleteMsge">Delete</a>
<%--<a class="button right big floatright tip"  style="font-size: 1.5em; line-height:15px; margin-right: 0;" id="nextMsge" title="Next message" >&gt;</ a>
<a class="button left big floatright tip"  style="font-size: 1.5em; line-height:15px;" id="prevMsge" title="Previous message">&lt;</ a>--%>
<a class="button right big floatright tip" href="#" style="font-size: 1.5em; line-height:15px; margin-right: 0;" id="nextMsge" title="Next message" >></a>
<a class="button left big floatright tip" href="#" style="font-size: 1.5em; line-height:15px;" id="prevMsge" title="Previous message"><</a>


</div>

    <%=MessageString %>
   
  
   
    <textarea id="replyMsge" cols="20" rows="2" class="inputTextBox BorberRad3" style="margin-bottom: 0;"></textarea>
    <input id="btnReply" type="button" value="Reply" class=" floatright BorberRad3 btnSendMsg " />

    <asp:HiddenField ID="profileFriendID" runat="server" />

    </div>
    <%--RIGHT PROFILE DIV--%>

 

    


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="RightColumnContentPlaceHolder" runat="server">
</asp:Content>

