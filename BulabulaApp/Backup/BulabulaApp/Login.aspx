<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BulabulaApp.Login" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>login..</title>
    <link rel="stylesheet" href="Styles/Site.css" />
      <link rel="shortcut icon" href="~/images/logo16x16.ico" >
   <link rel="icon" type="image/gif" href="~/images/animated_logo.gif" >
   <script type="text/javascript">
       function setFocus() {
           document.getElementById("LoginUser_UserName").focus();
       }
</script>
 
</head>
<body id="loginBody" onload="setFocus()">

    
    <form id="login" runat="server">

   <div id="mainLogin">
   <div id="logo">
     <img src="images/LoginLogo.gif" alt="nmmu Logo"/>
   </div>
   
       

    <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server"
                 ValidationGroup="LoginUserValidationGroup" class="BorberRad3 LoginUserValidationSummary"  />
     <asp:Panel ID="ErrorInfo" runat="server" CssClass="BorberRad3 LoginUserValidationSummary">  <%=failureText%></asp:Panel>
    <div id="mainBox" class="BorberRad5">
     <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false">
        <LayoutTemplate>
           <%-- <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server" ></asp:Literal>
            </span>--%>
            
            <div class="accountInfo">
                <fieldset class="login">
                    <legend>Login</legend>
                    <p><%--BEGIN USERNAME SECTION--%>
                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username:</asp:Label><br />
                        <asp:TextBox ID="UserName" runat="server" placeholder="Username here..." style="width: 85%;" CssClass=" 80PercWidth blueFontTextColor  inputTextBox BorberRad3"  />


                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                             CssClass="failureNotification" ErrorMessage="Username is required." ToolTip="Username is required." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p><%--END USERNAME SECTION--%>

                    <p><%--BEGIN PASSWORD SECTION--%>
                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" >Password:</asp:Label><br />

                        <asp:TextBox ID="Password" runat="server" style="width: 85%;" placeholder="Password" CssClass=" 80PercWidth blueFontTextColor inputTextBox BorberRad3" TextMode="Password"  />


                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                             CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p><%--END PASSWORD SECTION--%>
                    <p>

                        <asp:CheckBox ID="RememberMe" runat="server"/>
                        <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Keep me logged in</asp:Label>
                    </p>
                </fieldset>
                <div>
                <div class="LoginButton" >
                
                    <asp:Button ID="LoginButton" runat="server" style="margin:0;" Text="Login" CssClass="btnSendMsg BorberRad3" 
                        CommandName="Login" ValidationGroup="LoginUserValidationGroup" 
                        onclick="LoginButton_Click"/>
                                   
                </div> <%--END LOGIN BUTTON--%>
                </div>
       
        </LayoutTemplate>
         
    </asp:Login>
  
        
    
  
    </div> <%--END MAINBOX--%>
  </div><%--END MAIN LOGIN WRAPPER--%>
  </form>




     <%--Scripts--%>
<script type="text/javascript" src="Scripts/jquery-1.8.2.js"></script>
<%--  <script type="text/javascript" src="Scripts/jquery-ui-1.8.18.custom.min.js"></script>--%>
    <script src="Scripts/jquery-ui-1.8.23.js" type="text/javascript"></script>
    <script src="Scripts/jquery.signalR-0.5.3.js" type="text/javascript"></script>
     <script src="signalr/hubs" type="text/javascript"></script>
<%--    <script src="/signalr/hubs"></script>--%>
   <script type="text/javascript" src="Scripts/jquery.BetterGrow.min.js"></script>
     <script  charset='utf-8' type="text/javascript" src="Scripts/popbox.min.js"></script>
<%--  <script  type="text/javascript"  src="Scripts/bulabula.js"></script>--%>
  <%--  <script src="/Scripts/bulabula.js" type="text/javascript"></script>--%>
  </body>
</html>
