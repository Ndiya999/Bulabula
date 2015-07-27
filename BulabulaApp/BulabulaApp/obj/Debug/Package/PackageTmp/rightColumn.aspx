<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="rightColumn.aspx.cs" Inherits="BulabulaApp.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div class="page">
   <div id="WrapperMain">
<div id="innnerWrapper" class="BorberRad5">
<div id="leftColumn" class="floatLeft " >
<div class="inner">
							<h3 class="title">Mission</h3>
							<div class="content"> 
								<h6>What is Creative Commons?</h6>
                                                                Creative
 Commons helps you share your knowledge and creativity with the world.<br>
<br>
Creative Commons develops, supports, and stewards legal and technical<br>
infrastructure that maximizes digital creativity, sharing, and<br>
innovation.

<object classid=clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" 
codebase=http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,2,0 
Width = 500 Height = 500 > 
<param name=movie value="my.swf"> 
<param name=quality value=high> 
<param name=BGCOLOR value=#000000>
<param name=SCALE value=showall>
<embed src="my.swf"  
pluginspage=http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash 
type=application/x-shockwave-flash 
Width = 500 Height = 500
bgcolor=#000000 
scale= showall></embed>
</object>






                                                <div class="bucket-follow"><a class="btn" href="http://creativecommons.org/about">Learn about CC</a></div>
							</div> <!--! end of "content" --> 
</div> </div>

<div class="floatright " id="rightColumn">

       <%-- RIGHT-COLUMN ++NOTIFICATIONS+++++++++++++++++--%>
        <div id="notifications" class="gradientLigtBlueWhite BorberRad5">
         
        <h6 id="titleArea"  class="titleArea  radiustopLeftRight geadientDarkblue">Notifications</h6>
      
          

        <div id="content" class = "rightClmnContent radiustopLeftRight">
        <ul><li><a href="">friend Invites(<span>30</span>)</a>
        </li><li><a href="">Events(<span>5</span>)</a></li>
        <li><a href="">Suggested stuff(<span>3</span>)</a></li>
        </ul>
        </div>
        </div>

        <%-- RIGHT-COLUMN ++APLICATIONS+++++++++++++++++--%>
        <div id="applications" class="gradientLigtBlueWhite BorberRad5 ">
        <h6 class = "titleArea radiustopLeftRight geadientDarkblue" >
          Applicatications
        </h6>
        <div class = "rightClmnContent radiustopLeftRight" >

            <table style="width: 100%;" id="appTable">
            <tbody valign="middle">
                <tr>
                 <td class="icon">
                       <img src="images/icons/video.png" alt="my videos" />
                 </td>
                 <td class="text">
                        <a href="">Videos</a>
                    </td>
                      <td class="icon">
                        <img src="images/icons/settings.png" alt="Settings"/>
                 </td>
                 <td class="text">
                       <a href="">My settings</a>
                    </td>
                    
                </tr>
                 <tr>
                    <td class="icon" >
                        <img src="images/icons/gogreen.png" alt ="go green" />
                    </td>
                    <td class="text">
                        <a href="">Go green</a>
                    </td>
                      <td class="icon">
                       <img src="images/icons/documents.png" alt ="go green"/>
                 </td>
                 <td class="text">
                        <a href="">Documets</a>
                    </td>
                </tr>
                 <tr>
                    <td class="icon">
                        <img src="images/icons/help.png" alt ="go green"/>
                    </td>
                    <td class="text">
                        <a href="">Help</a>
                    </td>
                      <td class="icon">
                       <img src="images/icons/message.png"alt ="go green"/>
                 </td>
                 <td class="text">
                       <a href="">Mu messages</a>
                    </td>
                </tr>
                 <tr>
                    <td class="icon">
                        <img src="images/icons/mobile.png" alt ="go green"/>
                    </td>
                    <td class="text">
                        <a href="">Mobile</a>
                    </td>
                     <td class="icon">
                         <img src="images/icons/profile.png" alt ="go green"/>
                    </td>
                    <td class="text">
                        <a href="">Profile</a>
                    </td>
                </tr>
                 <tr>
                    <td class="icon">
                        <img src="images/icons/tag.png" alt ="go green"/>
                    </td>
                    <td class="text">
                        <a href="">Tags</a>
                    </td>
                      <td class="icon">
                       <img src="images/icons/license.png" alt ="go green"/>
                 </td>
                     
                 <td class="text">
                        <a href="">Driving</a>
                    </td>
                </tr>
                </tbody>
            </table>

      
      </div>
        
        </div>

        <%-- RIGHT-COLUMN ++BIRTHDAYS+++++++++++++++++--%>
        <div id="WrapperBirthDay" class="gradientLigtBlueWhite BorberRad5">
        <h6 class="titleArea  radiustopLeftRight geadientDarkblue">
        Birthdays
        </h6>

        <div id="Birthdays" class = "rightClmnContent radiusBottomLeftRight">
        <ul><li><span>Today</span><br /><a href="" id="langa">Vuyisanani Langa</a></li>
        <li><span>Tomrrow</span><br /><a href=""> Sedzani </a></li>
        
        </ul>
        </div>
        </div>


        </div>
        </div>
</div>
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="RightColumnContentPlaceHolder" Runat="Server">
  
       
 
</asp:Content>
