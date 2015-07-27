<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Administration.aspx.cs" Inherits="BulabulaApp.WebForm6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 <link rel="stylesheet" href="Styles/demos.css" type="text/css"  />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <div id="leftColumn" class="floatLeft " >
   <div id='datPicker'>
   <div id="eventsErrorInvalidDates" class=" eventsErrors LoginUserValidationSummary ui-corner-all">
        <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>Please make sure that the end date and time are not before the start date and time.</div>
    <table id="dateTable" style="width: 80%">
 <tbody>
   <tr>
 <td><span>Start Date </span> <input id="startDateR" type="text" value="<%=startDate %>" class=" DateTime blueFontTextColor  tagSearchBox ui-corner-all  datepicker" /> </td>
 <td><span>Start Time</span> <input id="startTime" type="text" value="<%=startTime %>"  class=" DateTime blueFontTextColor  tagSearchBox ui-corner-all time hasTimeEntry"   /> </td>
  <td><span>End Date </span> <input id="endDateR"  type="text" value="<%=endDate %>" class=" DateTime blueFontTextColor  tagSearchBox ui-corner-all datepicker" /> </td>
 <td><span>End Time</span> <input id="endTime" type="text" value="<%=endTime %>" class=" DateTime blueFontTextColor  tagSearchBox ui-corner-all time hasTimeEntry"  /> </td>
 </tr>




  </tbody>
 </table>

<a id="generateReports" class="button left big"  style="padding:4px 7px; font-size: 0.7em; width: 15%; margin-top:2em;">Generate content</a>
<div class='clr'></div>
</div>
   <div id="ReportsMain" class="ui-tabs ui-widget ui-widget-content ui-corner-all" style="width: 102.5%;">
			<ul class="ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">
				<li class="ui-state-default ui-corner-top ui-tabs-selected ui-state-active"><a id='MembersInG_Tab' href="#MembersInG">Members in groups</a></li>
                	<li class="ui-state-default ui-corner-top "><a id ='PostCompare_Tab' href="#PostCompare">Post Comparison</a></li>
				<li class="ui-state-default ui-corner-top "><a id ='Statistics_Tab' href="#Statistics">Statistics</a></li>
			
       
                </ul>
              
			<div id="MembersInG"    class="ui-tabs-panel ui-widget-content ui-corner-bottom ui-tabs-hide"> 
                <asp:GridView ID="GridView1" runat="server">
                </asp:GridView>
             <canvas id="cvs1" width="610" height="700" style="border: 1px solid gray; border-top-left-radius: 15px; border-top-right-radius: 15px; border-bottom-right-radius: 15px; border-bottom-left-radius: 15px; ">[No canvas support]</canvas></div>
            <div id="PostCompare" class="ui-tabs-panel ui-widget-content ui-corner-bottom ui-tabs-hide">
            <canvas id="pie4" width="600" height="400" style="margin-top:15px;">[No canvas support]</canvas>
           </div>
			<div id="Statistics" class="ui-tabs-panel ui-widget-content ui-corner-bottom ui-tabs-hide">
            <table id="profilePersonalInfo">
            <tbody>
             <tr><td style="font-weight: bold;">Members:</td><td id="memberStats"><%=members %></td></tr>
             <tr><td style="font-weight: bold;">Posts:</td><td id="postsStats"><%=posts %></td></tr>
             <tr><td style="font-weight: bold;">Messages:</td><td id="messagesStats"><%=messages %></td></tr> 
             <tr><td style="font-weight: bold;">Reports:</td><td id="reportsStats"><%=reports %></td></tr>
             <tr><td style="font-weight: bold;">Blocked Members:</td><td id="BmembersStats"><%=blockedMembers%></td></tr> 

          
            </tbody>
            </table>
            </div>
		
      
         
		</div><%--END TABS--%>
    
 
  </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="RightColumnContentPlaceHolder" runat="server">

    
    <script type="text/javascript" src="Scripts/Charts/RGraph.common.core.js" ></script>
    <script type="text/javascript" src="Scripts/Charts/RGraph.common.dynamic.js" ></script>
      <script type="text/javascript" src="Scripts/Charts/RGraph.common.effects.js" ></script>
    <script type="text/javascript" src="Scripts/Charts/RGraph.common.tooltips.js" ></script>
      <script type="text/javascript" src="Scripts/Charts/RGraph.hbar.js" ></script>
    <script type="text/javascript" src="Scripts/Charts/RGraph.pie.js" ></script>

<script type="text/javascript" >
//$(function () {

var sDate = "<%=startDate %>";
var eDate = "<%=endDate %>";
var numberOfMembers = [<%=numberOfMembers %>];
var GroupOfNames = [<%=GroupOfNames %>];
var data4 = [<%=finalPie %>];


generateMembersInGroups(numberOfMembers, GroupOfNames);
generatePostPie(data4);

 function generateMembersInGroups(numberOfMembers, GroupOfNames)
 {
    
        var hbar = new RGraph.HBar('cvs1', [numberOfMembers]);
        hbar.Set('chart.key.linewidth', 7);
        hbar.Set('chart.background.grid', false);
       // hbar.Set('chart.title', 'Total number of members per group from: to: ');154166
        hbar.Set('chart.colors', ['#E24547']);
        hbar.Set('chart.strokestyle', 'rgba(0,0,0,0)');
        hbar.Set('chart.labels.above', true);
      //  hbar.Set('chart.linewidth', 10);
       // hbar.Set('chart.labels.above', true);
     //   hbar.Set('chart.vmargin', 15);

        hbar.Set('chart.labels', GroupOfNames);

        if (!RGraph.isOld()) {
            hbar.Set('chart.tooltips', [GroupOfNames]);
            hbar.Set('chart.tooltips.event', 'onmousemove');
        }

       // hbar.Set('chart.labels.above.decimals', 1);
        hbar.Set('chart.xlabels', false);
        hbar.Set('chart.gutter.left', 250);
        hbar.Set('chart.gutter.right', 50);
        hbar.Set('chart.gutter.top', 25);
        hbar.Set('chart.noxaxis', true);
        hbar.Set('chart.noxtickmarks', true);
        hbar.Set('chart.noytickmarks', true);
        RGraph.isOld() ? hbar.Draw() : RGraph.Effects.HBar.Grow(hbar);
    } 

/*
*
*PIE SERIES
*
*/
 function generatePostPie(postsPercentages)
 {

   

            var pie4 = new RGraph.Pie('pie4', postsPercentages);
         //   pie4.Set('chart.title', 'Total number of members per group from: to: ');
       //   RGraph.Effects.Pie.RoundRobin('pie4', null, function () { pie.Explode(0, 20); })
            pie4.Set('chart.labels', ['Text','Articles','Events','Videos', 'Photos','Files']);
            pie4.Set('chart.tooltips', ['Text','Articles','Events','Videos', 'Photos','Files']);
            pie4.Set('chart.tooltips.event', 'onmousemove');
            pie4.Set('chart.colors', ['#EC0033','#A0D300','#FFCD00','#00B869','#999999','#FF7300','#004CB0']);
            pie4.Set('chart.strokestyle', 'white');
            pie4.Set('chart.linewidth', 3);
            pie4.Set('chart.shadow', true);
            pie4.Set('chart.shadow.offsetx', 2);
            pie4.Set('chart.shadow.offsety', 2);
            pie4.Set('chart.radius', 170);
            pie4.Set('chart.shadow.blur', 3);
            pie4.Set('chart.exploded', 0);
            // RGraph.Effects.Pie.RoundRobin(pie4);
            for (var i=0; i<postsPercentages.length; ++i) {
        
                pie4.Get('chart.labels')[i] = pie4.Get('chart.labels')[i] + ', ' + postsPercentages[i] + '%';
            }



            
         var explode = 20;
        function myExplode (obj)
        {
            window.__pie__ = pie4;

            for (var i=5; i<obj.data.length; ++i) {
                  setTimeout('window.__pie__.Explode('+i+',10)', 0);
            }
        }
            if (RGraph.isOld()) {
            pie4.Draw();
            } 
        else {
        /**
        * The RoundRobin callback initiates the exploding
        */
        RGraph.Effects.Pie.RoundRobin(pie4, null, myExplode);
        RGraph.Effects.Pie.Implode(pie4);
        }




        
   }

          //  });
</script>
  
</asp:Content>
