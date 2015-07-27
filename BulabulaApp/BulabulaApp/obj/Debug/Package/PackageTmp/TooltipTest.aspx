<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TooltipTest.aspx.cs" Inherits="BulabulaApp.TooltipTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="Scripts/jquery-1.7.1-vsdoc.js"></script>

    <style> type="text/css" >
                #personPopupContainer
                {
                position:absolute;
                left:0;
                top:0;
                display:none;
                z-index: 20000;
                }

                .personPopupPopup
                {
                    border: 2px solid #000;
                }

                #personPopupContent
                {
                background-color: #fff;
                min-width: 175px;
                min-height: 50px;
                }

                .personPopupPopup .personPopupImage
                {
                     margin: 5px;
                      margin-right: 15px;
                }

                .personPopupPopup .corner 
                {
                     width: 19px;
                     height: 15px;
                }
    
                .personPopupPopup .topLeft 
                {
                background: url(../images/personpopup/balloon_topLeft.png) no-repeat;
                }
    
                .personPopupPopup .bottomLeft 
                {
                background: url(../images/personpopup/balloon_bottomLeft.png) no-repeat;
                }
    
                .personPopupPopup .left 
                {
                background: url(../images/personpopup/balloon_left.png) repeat-y;
                }
    
                .personPopupPopup .right 
                {
                background: url(../images/personpopup/balloon_right.png) repeat-y;
                }
    
                .personPopupPopup .topRight 
                {
                background: url(../images/personpopup/balloon_topRight.png) no-repeat;
                }
    
                .personPopupPopup .bottomRight 
                {
                background: url(../images/personpopup/balloon_bottomRight.png) no-repeat;
                }
    
                .personPopupPopup .top 
                {
                background: url(../images/personpopup/balloon_top.png) repeat-x;
                }
    
                .personPopupPopup .bottom 
                {
                background: url(../images/personpopup/balloon_bottom.png) repeat-x;
                text-align: center;
                }
    
    </style>

    <script  type="text/jscript">
        $(document).ready(function () {

            $(function () {
                var hideDelay = 500;
                var currentID;
                var hideTimer = null;

                // One instance that's reused to show info for the current person
                        var container = $('<div id="personPopupContainer">'
              + '<table width="" border="0" cellspacing="0" cellpadding="0" align="center" class="personPopupPopup">'
              + '<tr>'
              + '   <td class="corner topLeft"></td>'
              + '   <td class="top"></td>'
              + '   <td class="corner topRight"></td>'
              + '</tr>'
              + '<tr>'
              + '   <td class="left">&nbsp;</td>'
              + '   <td><div id="personPopupContent"></div></td>'
              + '   <td class="right">&nbsp;</td>'
              + '</tr>'
              + '<tr>'
              + '   <td class="corner bottomLeft">&nbsp;</td>'
              + '   <td class="bottom">&nbsp;</td>'
              + '   <td class="corner bottomRight"></td>'
              + '</tr>'
              + '</table>'
              + '</div>');

                $('body').append(container);

                $('.personPopupTrigger').live('mouseover', function () {
                    // format of 'rel' tag: pageid,personguid
                    var settings = $(this).attr('rel').split(',');
                    var pageID = settings[0];
                    currentID = settings[1];

                    // If no guid in url rel tag, don't popup blank
                    if (currentID == '')
                        return;

                    if (hideTimer)
                        clearTimeout(hideTimer);

                    var pos = $(this).offset();
                    var width = $(this).width();
                    container.css({
                        left: (pos.left + width) + 'px',
                        top: pos.top - 5 + 'px'
                    });

                    $('#personPopupContent').html('&nbsp;');

                    $.ajax({
                        type: 'GET',
                        url: 'ProcessData.aspx',
                        data: 'page=' + pageID + '&guid=' + currentID,
                        success: function (data) {
                            // Verify that we're pointed to a page that returned the expected results.
                            if (data.indexOf('personPopupResult') < 0) {
                                $('#personPopupContent').html('<span >Page ' + pageID + ' did not return a valid result for person ' + currentID + '.<br />Please have your administrator check the error log.</span>');
                            }

                            // Verify requested person is this person since we could have multiple ajax
                            // requests out if the server is taking a while.
                            if (data.indexOf(currentID) > 0) {
                                var text = $(data).find('.personPopupResult').html();
                                $('#personPopupContent').html(text);
                            }
                        }
                    });

                    container.css('display', 'block');
                });

                $('.personPopupTrigger').live('mouseout', function () {
                    if (hideTimer)
                        clearTimeout(hideTimer);
                    hideTimer = setTimeout(function () {
                        container.css('display', 'none');
                    }, hideDelay);
                });

                // Allow mouse over of details without hiding details
                $('#personPopupContainer').mouseover(function () {
                    if (hideTimer)
                        clearTimeout(hideTimer);
                });

                // Hide after mouseout
                $('#personPopupContainer').mouseout(function () {
                    if (hideTimer)
                        clearTimeout(hideTimer);
                    hideTimer = setTimeout(function () {
                        container.css('display', 'none');
                    }, hideDelay);
                });
            });
        
        
        
        
        });
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>


    <a class="personPopupTrigger" href="<link to person>" rel="4218,a17bee64-8593-436e-a2f8-599a626370df">House, Devon</a>
    <a class="personPopupTrigger" href="<link to person>" rel="4218,f6434101-15bf-4c06-bbb2-fbe8c111b948">House, Gregory</a>
    
    </div>
    </form>
</body>
</html>
