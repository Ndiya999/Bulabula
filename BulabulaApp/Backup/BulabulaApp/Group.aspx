<%@ Page Title="Group" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Group.aspx.cs" Inherits="BulabulaApp.WebForm5" %>

<%--<%@ Register Assembly="com.flajaxian.FileUploader" Namespace="com.flajaxian" TagPrefix="cc1" %>--%>

<%-- <ajaxToolkit:ToolkitScriptManager runat="Server" EnablePartialRendering="true" ID="ToolkitScriptManager1" />--%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 
           


       <ajaxToolkit:ToolkitScriptManager runat="Server" EnablePartialRendering="true" ID="ScriptManager1" />


    <%-- LEFT COLLUMN--%>
 <div id="leftColumn" class="floatLeft " >


     <%--<asp:HiddenField ID="tabState" runat="server" />--%>

   <div id="tabs" class="ui-tabs ui-widget ui-widget-content ui-corner-all" style="width: 102.5%;">
			<ul class="ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">

				<li class="ui-state-default ui-corner-top ui-tabs-selected ui-state-active"><a id='Posts_Tab' href="#Posts">News Feed</a></li>
                <li class="ui-state-default ui-corner-top "><a id ='Text_Tab' href="#Text">Text</a></li>
				<li class="ui-state-default ui-corner-top  "><a id ='Pictures_Tab' href="#Pictures">Pictures</a></li>
				<li class="ui-state-default ui-corner-top"><a id ='Videos_Tab' href="#Videos">Videos</a></li>
				<li class="ui-state-default ui-corner-top"><a id ='Files_Tab' href="#Files">Files</a></li>
                <li class="ui-state-default ui-corner-top"><a id ='Events_Tab' href="#Events">Events</a></li>
                  <li class="ui-state-default ui-corner-top "><a id ='Articles_Tab' href="#Articles">Chat</a></li>
                </ul>

			<div id="Posts"    class="ui-tabs-panel ui-widget-content ui-corner-bottom ui-tabs-hide"><%= newsfeed%></div>



           <%-- ++++++++++++++++++++++++++++++++++++++
            *Pictures PICTURE POST START
            * This only shows text posts
            
            --%>
			<div id="Pictures" class="ui-tabs-panel ui-widget-content ui-corner-bottom ui-tabs-hide">
                 <div class="PostWrapper ui-corner-all"> <%-- Post Wrapper Start --%>
                    <div class="postsHeadingSml ">
                        <span>These are all picture posts</span>
                        <a id="btnBringPostPicture" class="button left big "  >Post Picture</a>
              
            
                    </div>
                    <div id="picturePostUploadSection" >   <%--Post section start--%>
                    
               <asp:Label runat="server" ID="myThrobber" Style="display:none;"><img align="absmiddle" alt="" src="Images/uploaderProgress.gif"/></asp:Label>
               <ajaxToolkit:AjaxFileUpload ID="PictureUploader" 
               runat="server" padding-bottom="4"
               padding-left="2" padding-right="4"
               padding-top="4" ThrobberID="myThrobber"
                OnClientUploadComplete="onClientUploadComplete"
              OnUploadComplete="PictureUploader_OnUploadComplete"
              MaximumNumberOfFiles="1" />


                        <div style="padding: 7px; " class="commentSection">
                            <div style="height: auto; width:98%;">
                           <%-- <textarea id="picturePostCaption" cols="20" rows="2" style="width: 100%; height: 26px; font-size: 0.85em; margin: 5px 0pt 7px; overflow: hidden;" class=" commentBox floatLeft input BorberRad3" placeholder="Write a comment..." title="Write a caption..."></textarea>--%>
                             <asp:TextBox ID="picturePostCaption" Textmode="Multiline" runat="server" Rows="10" style="width: 100%; height: 26px; font-size: 0.85em; margin: 5px 0pt 7px; overflow: hidden;" CssClass=" commentBox floatLeft input BorberRad3" placeholder="Write a caption..."></asp:TextBox>
                             <a id ="btnSendPicturePost" style="font-size: 0.9em; margin-right: -10px; " class="button big floatLeft  btnSendComment floatright ">Post</a></div>
                        </div>
            
            
                    </div><%-- END Post section --%>
                </div><%-- END Wrapper --%>





      
            
            </div><%-- END Picture post section --%>

            <div id="Events"   class="ui-tabs-panel ui-widget-content ui-corner-bottom ui-tabs-hide">
                <div class="PostWrapper" >
                     <div class="postsHeadingSml ">
                        <span>These are all events</span>
                        <a class="button left big" id="bringEventsDialog">Create an event</a>
              
                    </div>
                  </div>
            </div>


                 <%-- ++++++++++++++++++++++++++++++++++++++
            *VIDEO POST START
            * This only shows text posts
                AllowedFileTypes="wmv,avi,mp4,3gp,flv,mpeg,mpg,m4v,mov"
            --%>
			<div id="Videos"   class="ui-tabs-panel ui-widget-content ui-corner-bottom ui-tabs-hide">
            
         
            

              <div class="PostWrapper ui-corner-all"> <%-- Post Wrapper Start --%>
                    <div class="postsHeadingSml ">
                        <span>These are all video posts</span>
                        <a id="btnBringPostVideo" class="button left big"  >Post Video</a>
              
            
                    </div>
                    <div id="videoPostUploadSection" >   <%--Post section start--%>
                    
               <asp:Label runat="server" ID="myThrobber2" Style="display: none;" BorderStyle="Outset"><img align="absmiddle" alt="" src="Images/uploaderProgress.gif"/></asp:Label>

              <ajaxToolkit:AjaxFileUpload ID="VideoUploader" 
              runat="server" padding-bottom="4"
              padding-left="2" padding-right="4"
               padding-top="4" ThrobberID="myThrobber2"
   
                OnClientUploadComplete="onClientUploadComplete"
            OnUploadComplete="PictureUploader_OnUploadComplete"
            MaximumNumberOfFiles="1"    />



                        <div style="padding: 7px; " class="commentSection">
                            <div style="height: auto; width:98%;">
                                    
                                   <%-- <textarea id="" cols="20" rows="2"   title="Write a post..."></textarea>--%>
                                <asp:TextBox ID="videoPostCaption" Textmode="Multiline" runat="server" Rows="10" style="width: 100%; height: 26px; font-size: 0.85em; margin: 5px 0pt 7px; overflow: hidden;" CssClass=" commentBox floatLeft input BorberRad3" placeholder="Write a caption..."></asp:TextBox>

                             <a id ="textSendVideoPostButton"  style="font-size: 0.9em; margin-right: -10px; " class="button big floatLeft btnSendComment floatright " >Post</a></div>
                        </div>
            
            
                    </div><%-- END Post section --%>
                </div><%-- END Wrapper --%>

            
                
           </div>

                <%-- ++++++++++++++++++++++++++++++++++++++
            *VIDEO POST END
            --%>


               <div id="Articles" class="ui-tabs-panel ui-widget-content ui-corner-bottom ui-tabs-hide">
               <div id="MessagesSection" >

      


               </div>
               <div id="chatInputSection">
               <textarea id="ChatTextArea" class=" commentBox floatLeft input BorberRad3" title="Write a message..." placeholder="say something..." style="width: 86.5%; height: 26px; font-size: 0.85em; margin: 5px 0pt 7px; overflow: hidden; " rows="2" cols="20"></textarea>
                <a id="sendChatText" style="font-size: 0.9em; margin-left: 10px; margin-right: 0; position:relative; right:0; bottom: -5px;" class="button big floatLeft btnSendComment ">Send</a>
                <div class="clr"></div>
                
               </div>
               </div>
        


                  <%-- ++++++++++++++++++++++++++++++++++++++
            *VIDEO POST END
            --%>
			<div id="Files"    class="ui-tabs-panel ui-widget-content ui-corner-bottom ui-tabs-hide">
            
              <div class="PostWrapper ui-corner-all"> <%-- Post Wrapper Start --%>
                    <div class="postsHeadingSml ">
                        <span>These are all file posts</span>
                        <a id="btnBringPostFile" class="button left big"  >Post File</a>
              
            
                    </div>
                    <div id="filePostUploadSection" >   <%--Post section start--%>
                    
               <asp:Label runat="server" ID="myThrobber3" Style="display:none;"><img align="absmiddle" alt="" src="Images/uploaderProgress.gif"/></asp:Label>

              <ajaxToolkit:AjaxFileUpload ID="FileUploader" 
              runat="server" padding-bottom="4"
              padding-left="2" padding-right="4"
               padding-top="4" ThrobberID="myThrobber3"
   
                OnClientUploadComplete="onClientUploadComplete"
            OnUploadComplete="FileUploader_OnUploadComplete"
            MaximumNumberOfFiles="1"    />

                        <div style="padding: 7px; " class="commentSection">
                            <div style="height: auto; width:98%;"><textarea id="filePostCaption" cols="20" rows="2" style="width: 100%; height: 26px; font-size: 0.85em; margin: 5px 0pt 7px; overflow: hidden;" class=" commentBox floatLeft input BorberRad3" placeholder="Write a caption..." title="Write a caption..."></textarea>
                             <a id ="btnSendFilePost" style="font-size: 0.9em; margin-right: -10px; " class="button big floatLeft btnSendComment floatright ">Post</a></div>
                        </div>
            
            
                    </div><%-- END Post section --%>
                </div><%-- END Wrapper --%>



           
            
            
            
            </div>
                  <%-- ++++++++++++++++++++++++++++++++++++++
            *FILE POST END
            --%>



            <%-- ++++++++++++++++++++++++++++++++++++++
            * TEXT POST START
            * This only shows text posts
            
            --%>
            <div id="Text"    class="ui-tabs-panel ui-widget-content ui-corner-bottom ui-tabs-hide">
                <div class="PostWrapper ui-corner-all"> <%-- Post Wrapper Start --%>
                    <div class="postsHeadingSml ">
                        <span>These are all text posts</span>
                        <a id="bringPostControl" class="button left big"  >Post something</a>
              
            
                    </div>
                    <div id="textPostControl" >   <%--Post section start--%>
                        <div style="padding: 7px; " class="commentSection">
                            <div style="height: auto; width:98%;"><textarea id="textPostTxtArea" cols="20" rows="2" style="width: 100%; height: 26px; font-size: 0.85em; margin: 5px 0pt 7px; overflow: hidden;" class=" commentBox floatLeft input BorberRad3" placeholder="Write a post..." title="Write a post..."></textarea>
                             <a id ="textPostButton" style="font-size: 0.9em; margin-right: -10px; " class="button big floatLeft btnSendComment floatright ">Post</a></div>
                        </div>
            
            
                    </div><%-- END Post section --%>
                </div><%-- END Wrapper --%>

            </div> <%-- END TEXT POST ###################################################################################################--%>
<%--
            <div class="postsHeadingSml "><a id="loadMore" class="button left big" >Load more posts</a> </div>--%>

		</div><%-- END Tabbs body--%>



   

</div>


 

  

                

  <asp:HiddenField ID="ThisGroupsID" runat="server" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="RightColumnContentPlaceHolder" runat="server">
 <%--
  *
  *EVENTS DIALOG
  *
  --%>
   <div id="eventsDialog" title="Create Event">
   <div id="eventsErrorEmpty" class=" eventsErrors LoginUserValidationSummary ui-corner-all">
        <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>Please fill in items highlighted in red.</div>
    <div id="eventsErrorInvalidDates" class=" eventsErrors LoginUserValidationSummary ui-corner-all">
        <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>Please make sure that the end date and time are not before the start date and time.</div>
        
   <span>Name</span><input id="eventName" type="text" class="blueFontTextColor  tagSearchBox ui-corner-all" />
 <table >
 <tbody>
   <tr>
 <td><span>Start Date </span> <input id="startDate" type="text" class=" DateTime blueFontTextColor  tagSearchBox ui-corner-all" /> </td>
 <td><span>Start Time</span> <input id="startTime" class=" DateTime blueFontTextColor  tagSearchBox ui-corner-all time hasTimeEntry"   /> </td>
  <td><span>End Date </span> <input id="endDate" type="text" class=" DateTime blueFontTextColor  tagSearchBox ui-corner-all " /> </td>
 <td><span>End Time</span> <input id="endTime" class=" DateTime blueFontTextColor  tagSearchBox ui-corner-all time hasTimeEntry"  /> </td>
 </tr>




  </tbody>
 </table>
 <div>

 <div class="floatLeft " style="width:42%;">
  <span>Host</span><input id="eventHost" type="text" class="blueFontTextColor tagSearchBox ui-corner-all" /> 
  </div>
   <div class="floatright" style="width:40%;margin-right:45px;">
  <span>Type</span><input id="eventType" type="text" class="blueFontTextColor  tagSearchBox ui-corner-all" />
  </div>

<div class="clr"></div>
</div>


   <span>Location</span><input id="eventLocation" type="text" class="blueFontTextColor  tagSearchBox ui-corner-all" />
   <span>Details</span>
   <textarea  id="eventDetails"cols="20"  rows="2" style="width: 97.7%; min-height:60px; font-size: 0.85em; margin: 5px 0pt 7px; overflow: hidden;" class="commentBox floatLeft input BorberRad3" placeholder="Write a details..." title="Write a details..."></textarea>

   
      

 </div>
<%--END EVENTS--%>
</asp:Content>
