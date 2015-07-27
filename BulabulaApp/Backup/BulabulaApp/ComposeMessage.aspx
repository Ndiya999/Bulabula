<%@ Page Title="Compose a message" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="ComposeMessage.aspx.cs" Inherits="BulabulaApp.About" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<%--<div class="page  BorberRad5">
   <div id="WrapperMain">
<div id="innerWrapper" class="BorberRad5">--%>

<div id="leftColumn" class="floatLeft " >
  
    <div id="composer" class=" BorberRad5 blueBorder">
        <asp:Label ID="LblRecipient" runat="server" Text="Recipient:" CssClass="messageLbls"></asp:Label>
        <br />
        <asp:TextBox ID="TxtRecipient" runat="server" CssClass = "blueFontTextColor  MContent_TxtRecipient BorberRad3" ></asp:TextBox>
        <input type =hidden id ="memberID" value ="">
        <br />
        <asp:Label ID="LblMessage" runat="server" Text="Message:" CssClass="messageLbls"></asp:Label>
        <br />

        <asp:TextBox ID="messageTextarea" runat="server" TextMode="MultiLine" CssClass="input BorberRad3"></asp:TextBox>


        <input id="btnSendMsge" type="button" value="Send" class=" floatright BorberRad3 btnSendMsg " />

               <br />

    </div><%--END COMPOSER--%>
    
     <asp:HiddenField ID="profileFriendID" runat="server" />
  
    

</div><%--END LEFT COLUMN--%>


    



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="RightColumnContentPlaceHolder" Runat="Server">
</asp:Content>

