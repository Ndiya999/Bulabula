/// <reference path="jquery-1.8.2.js" />
/// <reference path="jquery-ui-1.8.23.js" />
/// <reference path="jquery.signalR-0.5.3.js" />

function IAmSelected(source, eventArgs) {
    document.getElementById("memberID").value = eventArgs.get_value();
}

function ClearCanvas(canvasID) {




    var canvas = document.getElementById(canvasID);

    var context = canvas.getContext("2d");
    context.clearRect(0, 0, canvas.width, canvas.height);

}

//CallServerEvent(n){debugger}
function onClientUploadComplete(sender, e) {
    var id = e.get_fileId();

    $Seven(e);


    //__doPostBack();
   
    // onImageValidated("TRUE", e);
}


$(function () {
    $("#video").bind("loadedmetadata", function () {
        var Vwidth = this.videoWidth;
        var Vheight = this.videoHeight;
        console.log(Vwidth);
        console.log(Vheight);

    });
});

//profileFriendID
$(document).ready(function () {





    //SyntaxHighlighter.all();
    $('.ajax__fileupload_progressBar').addClass('uploadProgress');
    $('#MainContent_VideoUploader_Footer').prepend($('#MainContent_myThrobber2'));
    $('#MainContent_PictureUploader_Footer').prepend($('#MainContent_myThrobber'));
    $('#MainContent_FileUploader_Footer').prepend($('#MainContent_myThrobber3'));


    //hover states on the static widgets
    $('.btnDleteComment').hover(
    function () { $(this).addClass('ui-state-hover'); },
    function () { $(this).removeClass('ui-state-hover'); }
    );

    //    var h = $('.headingMain').height();
    //    var hb = $('#displayNameTop').height();
    //    var margin = (h - hb) / 2;
    //    // alert(hb);
    //    $('#displayNameTop').css('margin-top', margin + 'px');


    /*
    *####################################################################################
    *
    *                        Popup CODE
    *
    *###################################################################################
    */




    //    var hideDelay = 500;
    //    var hideTimer = null;


    $('.btnViewComments').live('mouseover', function () {
        //        var hideDelay = 500;
        //        var hideTimer = null;
        var pos = $(this).offset();
        var width = $(this).width();
        var height = $(this).height();

        var container = $("<div id='hoverMenu'><div class='arrow' ></div>"
                    + "<div class='arrow-border' ></div>"
                    + "<div class='popupContentContainer'><table><tbody><tbody></table></div></div>");


        $(container).css({ 'width': '250px' });
        var containerW = $(container).outerWidth();

        //        if (hideTimer)
        //            clearTimeout(hideTimer);




        $('body').append(container);


        var thisPost = $(this).closest('.aPost');
        var postID = $(thisPost).attr('id');



        var arrow = $('.arrow');
        var arrowBorder = $('.arrow-border');
        var contentHolder = $('.popupContentContainer');

        arrow.css({ left: (containerW / 2 - 10) + 'px' });
        arrowBorder.css({ left: (containerW / 2 - 10) + 'px' });

        container.css({
            left: ((pos.left - (containerW / 2)) + (width / 2)) + 'px',
            top: pos.top + height + 10 + 'px'
        });

        // container.css({ 'display': 'block', 'top': 10, 'left': (($(this).width() / 2) - $(this).width() / 2) });

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "WebServices/postsWebservice.asmx/GetPeopleLikedPost",
            data: '{postID:"' + postID + '" }',

            //ON SUCCESS
            success: function (data) {



                $('tbody', contentHolder).html(data.d);




            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {

               
            }


        });

        //after ajax call

        container.css('display', 'block');

    })

    //    $(document).bind('click', function (event) {
    //        if (!$(event.target).closest('#hoverMenu').length) {
    //            $('#hoverMenu').css('display', 'none');
    //        }
    //    });

    //            $('.btnViewComments').live('mouseout', function () {
    //                if (hideTimer)
    //                    clearTimeout(hideTimer);
    //                hideTimer = setTimeout(function () {
    //                    $('#hoverMenu').css('display', 'none');
    //                }, hideDelay);
    //            });

    //            // Allow mouse over of details without hiding details
    //            $('#hoverMenu').mouseover(function () {
    //                if (hideTimer)
    //                    clearTimeout(hideTimer);
    //            });

    //            // Hide after mouseout
    //            $('#hoverMenu').mouseout(function () {
    //                if (hideTimer)
    //                    clearTimeout(hideTimer);
    //                hideTimer = setTimeout(function () {
    //                    $('#hoverMenu').css('display', 'none');
    //                }, hideDelay);
    //            });
    //        });





    //    $('.hoverMenu').live('mouseenter', function () {
    //       // $(this).css('background', 'red');

    //    })


    //    $('.btnViewComments').live('mouseleave', function () {

    //        $('.hoverMenu').delay(800).fadeOut('slow');



    //    })



    $('#acceptRequest').live('click', function () {

        $memberID = $('#profilesMemnerID').val();

        //AJAX POST
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "WebServices/FriendWebservice.asmx/Accept",
            data: '{friendID:"' + $memberID + '" }',      // data: '{param:"value"}',

            //ON SUCCESS
            success: function (data) {


                $('#profileButtons').html('<a class="button  big " id="reportfriend">Report this Person</a><br><a class="button  big " id="sendMessage">Send Message</a><br><a class="button  big " id="blockfriend">Block this Person</a><br><a class="button  big removeFriend" id="addfriend">Remove Friend</a><br>');

                $('.msgeSent').remove();
                $('.SingleViewmsgeWrapper').remove();
                $(data.d[2]).insertBefore('#replyMsge');




            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                /// debugger;

              
            }

            //END IF ELSE


        });


    });


    $('.downloadFile').live('click', function () {




        //selecting the current post
        var thisPost = $(this).closest('.aPost');
        var postID = $(thisPost).attr('id');
        $('#MainContent_ThisFileToDownload').val(postID);
        $("#form1").submit();
        $('#MainContent_ThisFileToDownload').val(null);


    });


    var o = $("#NavigationMenu:first-child").height();
    var ot = $('#NavigationMenu').height();
    var mb = (o - ot) / 2;
    $("#NavigationMenu:first-child").css('margin-top', '4px');


    $(".dropdown dt a").click(function () {
        $(".dropdown dd ul").toggle();
    });

    $(".dropdown dd ul li a").click(function () {
        var text = $(this).html();
        $(".dropdown dt a span").html(text);
        $(".dropdown dd ul").hide();
        $("#result").html("Selected value is: " + getSelectedValue("sample"));
    });

    function getSelectedValue(id) {
        return $("#" + id).find("dt a span.value").html();
    }

    $(document).bind('click', function (e) {
        var $clicked = $(e.target);
        if (!$clicked.parents().hasClass("dropdown"))
            $(".dropdown dd ul").hide();
    });


    var m = $('.ajax__html_editor_extender_buttoncontainer');

    /*
    *
    *PLACE TABS CODE HERE ###############################################################################################
    *
    *
    *
    *
    *
    *
    *
    */
    $('#tabs').bind('tabsselect', function (event, ui) {



        //this retuns id's with a underscore "_"
        var temp = $(ui.tab).attr('id');

        var tabName = temp.split('_');

        //Converting the tab id from e.g 'Pictures_Tab' to just 'Pictures'
        tabID = tabName[0];
        var thisTab = $('#' + tabID);

        $(thisTab).prepend('<div class="postsOverLay ui-corner-all"><div id="tabsProgress" ></div></div>')
       .hide().fadeIn('fast');

        GroupId = $('#MainContent_ThisGroupsID').val();


        if (!$('#MainContent_ThisGroupsID').length > 0) {

            GroupId = -1;

        }



        if (tabID == 'Articles') {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "WebServices/postsWebservice.asmx/RefreshChats",
                data: '{  groupID:"' + $('#MainContent_ThisGroupsID').val() + '" }',

                //ON SUCCESS
                success: function (data) {


                    $('#MessagesSection').html(data.d)


                    $('.postsOverLay').fadeOut('fast');


                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {

                    $('.postsOverLay').remove();

                }


            });
            // RefreshChats(int groupID)
        }
        else {

            //                    else if ($('.ui-tabs-selected #Articles_Tabs').length > 0) {

            //                        $('#MessagesSection').html(data.d);

            //                    }




            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "WebServices/postsWebservice.asmx/GetTabContents",
                data: '{tabName:"' + tabID + '",  GroupId:"' + GroupId + '",  lastPostID:"-1" }',

                //ON SUCCESS
                success: function (data) {


                    //If in home page
                    if (GroupId == -1) {
                        $(thisTab).html(data.d);


                    }

                    else {

                        $('.postInnerWrapper', thisTab).remove();

                        $('.PostWrapper', thisTab).after('<div class="postInnerWrapper"><div>');

                        $('.postInnerWrapper', thisTab).html(data.d);
                    }


                    $('.postsOverLay').fadeOut('fast');


                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {

                    $('.postsOverLay').remove();

                }


            });



            if ($('#MainContent_ThisGroupsID').length > 0) {
                GroupId = $('#MainContent_ThisGroupsID').val();


                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    url: "WebServices/postsWebservice.asmx/GetTabContents",
                    data: '{tabName:"' + tabID + '",  GroupId:"' + GroupId + '",  lastPostID:"-1" }',

                    //ON SUCCESS
                    success: function (data) {


                        //If in home page
                        if (GroupId == -1) {
                            $(thisTab).html(data.d);


                        }

                        else {

                            $('.postInnerWrapper', thisTab).remove();

                            $('.PostWrapper', thisTab).after('<div class="postInnerWrapper"><div>');

                            $('.postInnerWrapper', thisTab).html(data.d);
                        }


                        $('.postsOverLay').fadeOut('fast');


                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {

                        $('.postsOverLay').remove();

                    }


                });
            }

        }




    });





    //###############################################

    /*
    *SCROLL TO TOP
    *
    */
    // hide #back-top first
    $("#back-top").hide();
    var ajaxWait = 0;
    // fade in #back-top
    $(function () {
        $(window).scroll(function () {

            if ($(this).scrollTop() > 100) {
                $('#back-top').fadeIn();
            } else {
                $('#back-top').fadeOut();
            }



            if ((pathname == '/BulaBula/Group.aspx' || pathname == '/BulaBula/Home.aspx') && $('.ui-tabs-selected #Articles_Tab').length == 0) {



                // debugger





                var contentHeight = document.getElementById('pageID').offsetHeight;
                var yOffset = window.pageYOffset;
                var y = yOffset + window.innerHeight;

                if (y >= (contentHeight - 400) && ajaxWait == 0) {

                    //this retuns id's with a underscore "_"
                    var temp = $('.ui-tabs-selected a').attr('id');

                    var tabName = temp.split('_');

                    //Converting the tab id from e.g 'Pictures_Tab' to just 'Pictures'
                    tabID = tabName[0];

                    lastPostID = $('#' + tabID + " .aPost").last().attr('id');


                    ajaxWait = 1;




                    if (!$('#MainContent_ThisGroupsID').length > 0) {

                        GroupId = -1;

                    }
                    else {
                        GroupId = $('#MainContent_ThisGroupsID').val();

                    }

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        url: "WebServices/postsWebservice.asmx/GetTabContents",
                        data: '{tabName:"' + tabID + '",  GroupId:"' + GroupId + '",  lastPostID:"' + lastPostID + '" }',

                        //ON SUCCESS
                        success: function (data) {

                            ajaxWait = 0;

                            if ($('#' + tabID + ' .noMorePosts').length == 0) {
                                //If in home page
                                if (GroupId == -1) {

                                    $('#' + tabID).append(data.d); //Add at the end

                                }
                                else {

                                    if ($('.ui-tabs-selected #Posts_Tab').length > 0 && $('.ui-tabs-selected #Posts_Tab .noMorePosts').length == 0) {
                                        $('#Posts').append(data.d);
                                    }
                                    else {
                                        if ($('#' + tabID + '.postInnerWrapper .noMorePosts').length > 0) {


                                        }
                                        else {

                                            $('#' + tabID + '.postInnerWrapper').append(data.d);
                                        }

                                    }




                                }

                            }



                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {

                            $('.postsOverLay').remove();

                        }


                    });

                } //End if

            } //END IF 
        });


        // scroll body to 0px on click
        $('#back-top a').click(function () {
            $('body,html').animate({
                scrollTop: 0
            }, 800);
            return false;
        });
    });

    //END SCROLL TO TOP##########################################



    //   registering tabs
    $("#tabs").tabs();
    $("#ReportsMain").tabs();



    // Invoke Placeholder plugin


    $.widget("custom.catcomplete", $.ui.autocomplete, {
        _renderMenu: function (ul, items) {
            var self = this,
    currentCategory = "";
            $.each(items, function (index, item) {

                if (item.category != currentCategory) {
                    ul.append("<li class='ui-widget-autocomplete-category'  >" + item.category + "</li>");
                    currentCategory = item.category;
                }
                self._renderItem(ul, item);
            });
        }
    });



    $("#searchTxtBox").catcomplete({
        minLength: 1,


        source: function (request, response) {


            $('.ui-autocomplete .ui-menu').css('width', '231px');
            $("#searchTxtBox").addClass('textboxProgress');

            $.ajax({

                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                url: "WebServices/Searchfriend.asmx/GetAllMembers",
                data: '{prefixTxt:"' + request.term + '" }',

                //ON SUCCESS 
                success: function (data) {

                    $("#searchTxtBox").removeClass('textboxProgress');

                    response(data.d);

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    /// debugger;

                  
                }





            });



        },

        focus: function (event, ui) {

            $("#searchTxtBox").val(ui.item.label);
            return false;
        },

        select: function (event, ui) {

            // $("#friendSearchID").val(ui.item.id);

            if (ui.item.category == 'Groups') {
                $("#GoToGroupPageID").val(ui.item.id + "&" + ui.item.label);
            }
            else if (ui.item.category == 'People') {
                $("#profileFriendID").val(ui.item.id);
            }

            //            debugger
            $("#form1").submit();
            return false;

        }

    }); //END Autocomplete extender






    $('.popbox').popbox({
        'open': '.open',
        'box': '.box',
        'arrow': '.arrow',
        'arrow-border': '.arrow-border',
        'close': '.close'
    });





    $("#filter").click(function (event) {
        // event.stopPropagation();
        event.preventDefault();


        // .position() uses position relative to the offset parent, 
        var pos = $(this).position();

        // .outerWidth() takes into account border and padding.
        var width = $(this).outerWidth();
        console.log(pos);
        //show the menu directly over the placeholder
        // $("#filterMenu").slideToggle('slow');
        $("#filterMenu").slideDown('fast');


    });



    //  hide menu When cliked on document
    $(":not('#filterMenu')", document).mouseup(function (e) {

        $("#filterMenu").slideUp('fast');

    });

    //End Hide
    //    $(document).bind('keyup', function (event) {
    //        if (event.keyCode == 27) {
    //            $("#filterMenu").slideUp('fast');
    //        }
    //    });

    //    var d = $("#filterMenu");
    //    $(document).bind('click', function (event) {
    //        if (!$(event.target).closest(d).length) {
    //        debugger
    //            $("#filterMenu").slideUp('fast');
    //        }
    //    });



    $("#focusHack").blur(function () {

        $("#filterMenu").hide();

    });

    //###########################################################
    //POSTS SECTION
    //#############################################################




    //Showing Text Post Section
    $('#bringPostControl').live('click', function () {


        $('#textPostControl').slideDown('fast');


    });
    //END Showing Post Section





    //Showing  Pictre Post Section
    $('#btnBringPostPicture').live('click', function () {


        $('#picturePostUploadSection').slideDown('fast');


    });
    //END Showing Post Section


    //Showing Video Post Section
    $('#btnBringPostVideo').live('click', function () {


        $('#videoPostUploadSection').slideDown('fast');


    });
    //END Showing Post Section

    //Showing  File Post Section
    $('#btnBringPostFile').live('click', function () {


        $('#filePostUploadSection').slideDown('fast');


    });
    //END Showing Post Section


    //Uploading Videos
    $('#textSendVideoPostButton').live('click', function () {

        var $caption = $('#MainContent_videoPostCaption').val();
        $SendCaption($caption);

        $('#MainContent_VideoUploader_UploadOrCancelButton').click();

        $('#MainContent_videoPostCaption').val("");
    });

    //Uploading Picture
    $('#btnSendPicturePost').live('click', function () {

        var $caption = $('#MainContent_picturePostCaption').val();
        $SendCaption($caption);

        $('#MainContent_PictureUploader_UploadOrCancelButton').click();
        $('#MainContent_picturePostCaption').val("");

    });

    //Uploading File
    $('#btnSendFilePost').live('click', function () {



        var $caption = $('#filePostCaption').val();
        $SendCaption($caption);
        $('#MainContent_FileUploader_UploadOrCancelButton').click();
        $('#filePostCaption').val("");

    });




    //DELETING A POST
    var temp;
    $('.deletePost').live('click', function () {

        //selecting the current post
        var thisPost = $(this).closest('.aPost');

        $("#itemToDelete").html('this post');
        //Updating dialog title
        $("#dialog-confirm").attr('title', 'Delete Post ?');
        $("#dialog-confirm").dialog('option', 'title', 'Delete Post ?');

        $("#dialog-confirm").dialog({
            resizable: false,
            height: 160,
            width: 370,
            modal: true,
            buttons: {
                "Delete": function () {
                    //Actual deleting
                    $(this).dialog("close");

                    $(thisPost).animate({

                        visibility: 'none'
                    }, 600,
    'linear', function () {
        //easeOutElastic

        // Animation complete.
        $(thisPost).slideUp({ duration: 1500,
            easing: 'easeInElastic',
            complete: function () {


                // Animation complete.    
                $postID = $(thisPost).attr('id');



                $DeleteAPost($postID);
                $(this).dialog("close");




            }
        });



    });



                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });





    });



    //Refreshing posts every 10 minuets.

    //Uncomment this section to enable periodic posts refreshing
    // IMPORTANT**************************

    //    $PageRefresh = function () {

    //        var refreshId = setInterval(function () {

    //            var $latestPostID = $('#Posts div:first-child').attr('id');

    //            $.ajax({
    //                type: "POST",
    //                async: false,
    //                contentType: "application/json; charset=utf-8",
    //                dataType: "json",
    //                url: "../WebServices/postsWebservice.asmx/RefreshPostperiodically",
    //                data: '{postID:"' + $latestPostID + '" }',

    //                //ON SUCCESS
    //                success: function (data) {

    //                    var latestPID = data.d[0]
    //                    if (latestPID != $latestPostID) {

    //                        //   debugger
    //                        $('#Posts').prepend(data.d[1]).fadeIn('slow');
    //                        // $PageRefresh();

    //                    }

    //                },
    //                error: function (XMLHttpRequest, textStatus, errorThrown) {


    //                    alert("Please check your connection.");
    //                }


    //            });


    //            //        $.ajax({
    //            //            type: "POST",
    //            //            contentType: "application/json; charset=utf-8",
    //            //            dataType: "json",
    //            //            url: "../WebServices/postsWebservice.asmx/RefreshPostperiodically",
    //            //            data: '{postID:"' + $latestPostID + '" }',

    //            //            //ON SUCCESS
    //            //            success: function (data) {

    //            //                $('#Posts').prepend(data.d).fadeIn('slow');

    //            //            },
    //            //            error: function (XMLHttpRequest, textStatus, errorThrown) {


    //            //                alert("Please check your connection.");
    //            //            }


    //            //        });

    //            //REFRESHING NOTIFICATIONS============================================

    //            $.ajax({
    //                type: "POST",
    //                contentType: "application/json; charset=utf-8",
    //                dataType: "json",
    //                url: "../WebServices/Notification.asmx/GetAllNotifications",
    //                data: '{}',

    //                //ON SUCCESS
    //                success: function (data) {

    //                    $('#messagesNotifications').html(data.d[0]);
    //                    $('#friendReqNotifications').html(data.d[1]);
    //                    $('#tagNotifications').html(data.d[2]);
    //                    $('#eventNotifications').html(data.d[3]);

    //                },
    //                error: function (XMLHttpRequest, textStatus, errorThrown) {


    //                    alert("Please check your connection.");
    //                }


    //            });




    //        }, 5000);


    //    } //End Refreshing


    //    setTimeout($PageRefresh(), 6000);

    //###########################################################
    //END POSTS SECTION




    //###########################################################
    //COMMENTS SECTION
    //#############################################################

    //---Enable textArea grow
    $('.commentBox').BetterGrow();

    //A Post hover
    $('.aPost').live('mouseenter', function () {


        $('.btnHomePost a', this).fadeIn('fast');
        $('.deletePost', this).fadeIn('fast');


    });
    $('.aPost').live('mouseleave', function () {

        $('.btnHomePost a', this).hide();
        $('.deletePost', this).fadeOut('fast');

    });
    //END A Post Hover


    //Comment hover
    $(".aComment").hover(function () {


        $('.deleteComment', this).fadeIn('fast');


    }, function () {

        $('.deleteComment', this).hide();


    });
    //END Comment Hover

    //---Display Comment section
    $(".homeCommentBtn").live('click', function () {


        var thisPost = $(this).closest('.aPost');
        $('.commentSection', thisPost).fadeIn('slow');
        $('.commentBox', thisPost).focus();


    });


    //---Send Comment
    $('.btnSendComment').live('click', function () {

        //Get Current post ID
        var thisPost = $(this).closest('.aPost');
        $postID = $(thisPost).attr('id');

        //Get last comments ID
        $lastComment = $('.allComments', thisPost).children(':last');
        $lastCommentID = $($lastComment).attr('id');

        $comment = $('.commentBox', thisPost).val();
        $comment = $.trim($comment); //Remove leading/trailing spaces



        if ($comment != '') {

            $("<span style='width:24px; height:24px;' class='blueCircleLoader'></span>").insertAfter('div.postinfo');
            $('.blueCircleLoader').addClass('sendCommentProgress')
    .fadeIn('fast');





            $AddAComment($postID, $comment)






            $('.commentBox', thisPost).val('');
            $('.blueCircleLoader').remove();
        }
    });


    //List Comments
    $('.btnViewComments').live('click', function () {

        var thisPost = $(this).closest('.aPost');
        $postID = $(thisPost).attr('id');

        $('.homeCommentBtn', thisPost).click();

        //$("div.postinfo", thisPost).after("<span class='postProgress ui-corner-all floatLeft'><em >Pease wait</em></span>");

        //show loading progress
        $("<span style='width:24px; height:24px;' class='blueCircleLoader'></span>").insertAfter('div.postinfo');
        $('.blueCircleLoader', thisPost).addClass('viewCommentProgress')
    .fadeIn('fast');

        if ($('.allComments', thisPost).is(":visible")) {
            $("span.blueCircleLoader").remove();
        }
        else {


            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "WebServices/processComments.asmx/GetAllComments",
                data: '{postID:"' + $postID + '" }',

                //ON SUCCESS
                success: function (data) {
                    if (data.d == "") {
                        // $('.allComments', thisPost).empty();
                        $('.allComments', thisPost).html("<h4>There are no comments for this post</h4>")
    .hide()
    .slideDown('slow');


                    }
                    else {

                        $('.allComments', thisPost).append(data.d)
    .hide()
    .fadeIn('slow');
                        $('.homeCommentBtn', thisPost).click();
                    }

                    $("span.blueCircleLoader").remove();
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {


                 
                }






            });





            //            $(window).load(function () {
            //                $(".homeCommentBtn", thisPost).click();
            //            });
        }


    });

    //Bar Porgess Loader
    $likeCommentProgress = function ($thisElement) {

        var commentPos = $($thisElement).position();
        $("<span style='width:16px; height:11px;' class='blueBarLoader'></span>").insertAfter($thisElement);

        $('.blueBarLoader').css({ 'top': commentPos.top + 'px', 'left': (commentPos.left + 84) + 'px' });


        $('.blueBarLoader').addClass('reportCommentProgress')
    .fadeIn('fast');

    }
    //END Progress----->


    //Delete a Comment
    $('.btnDleteComment').live('click', function () {

        //get this Post's ID
        var thisPost = $(this).closest('.aPost');
        $postID = $(thisPost).attr('id');

        //get this Comment's ID
        var thisComment = $(this).closest('.aComment');
        $commentID = $(thisComment).attr('id');

        $("#itemToDelete").html('this comment');
        $("#dialog-confirm").attr('title', 'Delete Comment ?');
        $("#dialog-confirm").dialog('option', 'title', 'Delete Comment ?');


        //Showing progress
        //  $likeCommentProgress(this);

        $("#dialog-confirm").dialog({
            resizable: false,
            height: 160,
            width: 400,
            modal: true,
            buttons: {
                "Delete": function () {

                    $DeleteAComment($postID, $commentID, thisPost)

                    //remove comment
                    thisComment.slideUp('slow');





                    $(this).dialog("close");

                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });



    });
    //End Delete Comment



    //Open Comment Menu show
    $('.btnOpenMenuComment').live('click', function () {

        var thisPost = $(this).closest('.aPost');
        $postID = $(thisPost).attr('id');



        $("div#tag").dialog({
            resizable: false,
            height: 300,
            width: 350,
            modal: true,
            buttons: {

                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });




    });
    // End Comment menu Show



    //Liking/Unliking a Post
    $('.homeLikeBtn').live('click', function () {


        $elementContent = $(this).html();

        var thisPost = $(this).closest('.aPost');
        $postID = $(thisPost).attr('id');
        $button = $('.homeLikeBtn', thisPost);


        $("<span style='width:24px; height:24px;' class='blueCircleLoader'></span>").insertAfter('div.postinfo');
        $('.blueCircleLoader').addClass('viewCommentProgress')
    .fadeIn('fast');


        if ($elementContent == "Like") {

            $LikeAPost($postID);

        }
        else {

            $UnlikeAPost($postID);

        }

        $(".blueCircleLoader").remove();
    });
    //END LIKING






    //Liking/Unliking a Comment 
    $('.likeComment').live('click', function () {
        //Showing progress loader
        $likeCommentProgress(this);

        $elementContent = $(this).html();

        var thisComment = $(this).closest('.aComment');
        $commentID = $(thisComment).attr('id');

        $button = $('.likeComment', thisComment);

        if ($elementContent == "Like") {





            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "WebServices/processComments.asmx/LikeAComment",
                data: '{commentID:"' + $commentID + '" }',

                //ON SUCCESS
                success: function (data) {

                    //updating tag counts
                    if (data.d == true) {



                        $($button).html("Unlike");

                    }




                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {


                 
                }

            });
        }
        else {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "WebServices/processComments.asmx/UnlikeAComment",
                data: '{commentID:"' + $commentID + '" }',

                //ON SUCCESS
                success: function (data) {

                    //updating tag counts
                    if (data.d == true) {


                        // $($addRemoveFrnd).html("");

                        $($button).html("Like");

                    }




                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {


              
                }

            });



        }



        //remove Progress
        $('.blueBarLoader').remove();

    });




    //regex email validation
    function isValidEmailAddress(emailAddress) {
        var pattern = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
        return pattern.test(emailAddress);
    };


    var $postID = null;
    $('.tag').live('click',function () {

        var thisPost = $(this).closest('.aPost');
        $postID = $(thisPost).attr('id');
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "WebServices/processComments.asmx/GetAllTags",
            data: '{postID:"' + $postID + '" }',

            //ON SUCCESS
            success: function (data) {

                if (data.d == "") {

                    $('.friendList ul').append("<li id='noTags' ><a class='friend'>There are no friends tagged.</a></li>").fadeIn('slow');
                }
                else {


                    $('.friendList ul').append(data.d).fadeIn('slow');
                }

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {


             
            }


        });




        $("div#tag").dialog({
            resizable: false,
            width: 400,
            modal: true,
            buttons: {
                "Done": function () {

                    $(this).dialog("close");
                    $("div#tag").empty();
                },
                "Done": function () {
                    $(this).dialog("close");

                }
            }
        });

        $("div#tag").attr('class', $postID)
        $("div#tag .friendList ul").empty();
    });



    $('.ui-icon-closethick').live('click', function () {

        $("div#tag .friendList ul").empty();
    });

    $('.btnUntag').live('click', function () {


        var thisFriend = $(this).closest('li');
        $friendID = $('.btnMemberProfile', thisFriend).attr('id');




        $UntagAPost($postID, $friendID);

        $thisPost = $('#' + $postID);
        //Updating tags count
        $(thisFriend).slideUp();
        $(thisFriend).remove();

        if ($('.friendList ul').html() == "") {
            $('.friendList ul').append("<li id='noTags' ><a class='friend'>There are no friends tagged.</a></li>").fadeIn('slow');
        }





    });

    //Autocomplete for tagging
    $("#untaggedFriend").autocomplete({
        minLength: 1,


        source: function (request, response) {
            $("#untaggedFriend").addClass('textboxProgress');
            $.ajax({

                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                url: "WebServices/Searchfriend.asmx/GetUntaggedFriends",

                data: '{prefixTxt:"' + request.term + '", postID:"' + $postID + '" }',
                //ON SUCCESS 
                success: function (data) {


                    $members = data.d;

                    response($members);

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    /// debugger;

                  
                }





            });

            $("#untaggedFriend").removeClass('textboxProgress');

        },

        focus: function (event, ui) {

            $("#untaggedFriend").val(ui.item.label);
            return false;
        },

        //ACTUAL TAGGING
        select: function (event, ui) {




            $TagAPost($postID, ui.item.id)
            $('#noTags').remove();

            $thisPost = $('#' + $postID);

            //updating tagged friend list
            $('.friendList ul').append('<li ><a id="' + ui.item.id + '" class="btnMemberProfile pointer">' + ui.item.label + '</a><a class="button left big floatright btnUntag " style="margin: 0;" >Untag friend</a></li>').fadeIn('slow');
            $('#untaggedFriend').val('');


            return false;

        }

    });
    //END Autocomplete


    //Tagged hover
    $('.friendList ul li').live('mouseenter', function () {

        $('.big', this).show('fast');

    });

    $('.friendList ul li').live('mouseleave', function () {
        $('.big', this).hide();

    });

    //comment hover
    $('.aComment').live('mouseenter', function () {


        $('.deleteComment a', this).css('visibility', "visible");

    });

    $('.aComment').live('mouseleave', function () {
        $('.deleteComment a', this).css('visibility', "hidden");

    });

    //END TAG Hover




    //###########END COMMENTS#####################################


    /*####################################################################
    *                  <--GROUPS FUNCTIONS-->
    ####################################################################*/


    //REDIRECT TO GROUPS PAGE

    $('.btnGoToGroupPage').live("click", function () {
        $GroupID = $(this).attr('id');
        $GroupDisc = $(this).html();
        $('#GoToGroupPageID').val($GroupID + "&" + $GroupDisc);

        $("#form1").submit();


    });
    //END REDIRECTING TO GROUPS PAGE






    //###########END GROUPS FUNCTIONS#####################################


    /*####################################################################
    *                  <--REPORTING-->
    ####################################################################*/

    //report a post
    $('.reportPost').live('click', function () {

        var $thisPost = $(this).closest('.aPost');
        $postID = $($thisPost).attr('id');

        $report($postID, "post", "Report a post");

    });

    //report a message
    $('.reportMessage').click(function () {

        var $thisMessage = $(this).closest('.aMessage');
        $messageID = $($thisMessage).attr('id');

        $report($messageID, "message", "Report a message");

    });


    //report a comment
    $('.reportComment').live('click', function () {


        var $thisComment = $(this).closest('.aComment');
        $commentID = $($thisComment).attr('id');



        $report($commentID, "comment", "Report a comment");

    });


    //REPORT EVERYTHING FUNCTION
    $report = function ($itemID, $reportType, $title) {

        //Updating dialog title
        $("#reportFriend").attr('title', $title);
        $("#reportFriend").dialog('option', 'title', $title);

        //Updating first option radio button label
        $("#reportFriend label span").html($reportType);

        $("#reportFriend").dialog({
            resizable: false,
            width: 400,
            modal: true,
            buttons: {
                "Send Report": function () {

                    var $emailAdress = document.getElementById('emailAddress').value;

                    $emailAdress = $.trim($emailAdress);

                    //Check if user entered a valid email address
                    //  $emailAdress.indexOf("@") == -1
                    if (!isValidEmailAddress($emailAdress)) {

                        $('#emailAddress').css('width', '95%');



                        if ($emailAdress == "") {
                            $('label.noShow').html('Email address required.');
                        }
                        else {
                            $('label.noShow').html('Please enter a valid email address.');
                        }





                        $('.noShow').fadeIn('slow');



                    }
                    else {

                        //show loading progress
                        $('.blueCircleLoader').addClass('dialogProgress');
                        $('.blueCircleLoader').insertAfter('.ui-dialog-buttonset')
                        .fadeIn('fast');
                        // 
                        var $dialog = this;

                        //get value of checked radio button
                        var $radioValue = $("input[name=report]:checked").val();

                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            url: "WebServices/processComments.asmx/SendReport",
                            data: '{itemID:"' + $itemID + '", option:"' + $radioValue + '", emailAddress:"' + $emailAdress + '", type:"' + $reportType + '" }',

                            //ON SUCCESS
                            success: function (data) {


                                $($dialog).dialog("close");
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {


                            }


                        });
                        $('.blueCircleLoader').hide();
                    }



                },
                Cancel: function () {
                    $(this).dialog("close");

                }
            }
        }); // end report dialog





    }



    //#####################--END REPORTING--##################################
    $FormatDateTime = function (date, time) {

        var dateArr = date.split('/');
        var timeArr = time.split(':');
        timeArr[1].split(' ');

        return DateString;
    }

    $ConvertToSeconds = function (date, time) {

        var monthsOfYear = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31]



        var constant = (12 * (60 * 60));
        //a day
        var day = (24 * (60 * 60));

        var dateArr = date.split('/');

        //hours X days
        secs = dateArr[0] * day;

        //month
        month = 0;

        for (var i = 1; i < dateArr[1]; ++i) {

            secs += monthsOfYear[dateArr[1] - i] * day;

            month += monthsOfYear[dateArr[1] - i] * day;


        }


        secs += (dateArr[2] - 2011) * (day * 365);


        var timeArr = time.split(':');

        var amPm = timeArr[1].split(' ');
        secs += amPm[0] * 30;

        if (amPm[1] == 'AM') {

            secs += timeArr[0] * (60 * 60);
        }
        else {


            secs += timeArr[0] + constant;
        }
        return secs;


    }


    $getDifferenceE = function () {
        var sd = $("#startDate").val();
        var s = $("#startTime").val();
        startDate = sd + " " + s;


        var e = $("#endTime").val();
        var ed = $("#endDate").val();
        endDate = ed + " " + e;


        SDsecs = $ConvertToSeconds(sd, s);
        EDsecs = $ConvertToSeconds(ed, e);



        return dateDiff = EDsecs - SDsecs;
    }



    //#################################################################
    //            EVENTS FUNCTIONS
    //#################################################################
    $("#bringEventsDialog").click(function () {


        $("#eventsDialog").dialog({
            resizable: false,
            maxHeight: 700,
            width: 550,
            modal: true,
            buttons: {
                "Done": function () {




                    var isRequiredFilled = false;
                    $("#eventsDialog input").each(function () {

                        var temp = $(this).val();

                        if ($.trim(temp) == "") {

                            $(this).addClass('EventsWarning');
                            $("#eventsErrorEmpty").slideDown('fast');
                            isRequiredFilled = false;
                        }
                        else {
                            isRequiredFilled = true;
                            $(this).removeClass('EventsWarning');

                        }

                    });





                    if (isRequiredFilled) {

                        $("#eventsDialog input").each(function () {

                            $(this).removeClass('EventsWarning');
                            $("#eventsErrorEmpty").hide('fast');

                        });





                        dateDiff = $getDifferenceE();

                        if (dateDiff < 0) { //validating date and time


                            $("#eventsErrorInvalidDates").slideDown('fast');
                            $(".DateTime").addClass('EventsWarning');
                        }
                        else { //If all validation is passed

                            $("#eventsErrorInvalidDates").hide('fast');
                            $(".DateTime").removeClass('EventsWarning');

                            var groupID = $('#MainContent_ThisGroupsID').val();

                            var name = $('#eventName').val(), host = $('#eventHost').val(), venue = $('#eventLocation').val(), details = $('#eventDetails').val(), type = $('#eventType').val();

                            $(this).dialog("close");
                            $SendEventPost(startDate, endDate, name, host, venue, groupID, details, type);


                        }


                    }










                },
                Cancel: function () {
                    $(this).dialog("close");



                }
            }
        });


    });




    //date picker
    $("#startDateR").datepicker({ maxDate: 0, minDate: (new Date(2012, 1 - 1, 31)), showWeek: true, firstDay: 7 });
    $("#endDateR").datepicker({ maxDate: 0, showWeek: true, firstDay: 7 });



    $("#startDate").datepicker({ minDate: 0 });
    $("#endDate").datepicker({ minDate: 0 });

    $("#startDate").datepicker("option", "dateFormat", "dd/mm/yy");
    $("#endDate").datepicker("option", "dateFormat", "dd/mm/yy");

    //MASKING TIME
    $(".datepicker").each(function (index) {
        var datepicker_default_val = $(this).val();

        $(this).datepicker("option", "dateFormat", "dd/mm/yy");
        $(this).datepicker("setDate", datepicker_default_val);
    });


    // $("#startDateR").val(sDate);
    // $("#endDateR").val(eDate);

    $('.hasTimeEntry').ptTimeSelect();


    $('.time').css('width', '60%');



    //#####################--END EVENTS FUNCTIONS--##################################



    //#################################################################
    //            PROFILE functions
    //#################################################################



    $('.btnAllFriends').live('click', function () {

        $friendID = $(this).attr('id');
        //        alert($friendID);
        $('#profileFriendID').val($friendID);

        $("#form1").submit();


    });


    //REDIRECT TO PROFILE

    $('.btnMemberProfile').live("click", function () {

        $friendID = $(this).attr('id');

        $('#profileFriendID').val($friendID);

        $("#form1").submit();


    });
    //END REDIRECTING TO PROFILE
    var pathname = window.location.pathname;


    $('.dynamic').live('click', function () {
        $elementContent = $(this).html();

        if ($elementContent == "Help") {



            var page = "";
            $("#Help").dialog({
                resizable: false,
                height: 600,
                width: 900,
                modal: true,
                buttons: {

                    Cancel: function () {
                        $(this).dialog("close");
                    }
                }
            });

            //empties the help center dialog
            $('div#Help').empty();


            if (pathname == '/BulaBula/Home.aspx') {

                page = "homePageHelp.htm";

            } //END IF 

            if (pathname == '/BulaBula/ComposeMessage.aspx') {

                page = "composeMessage.htm";


            } //EDN IF

            if (pathname == '/BulaBula/Group.aspx') {

                page = "PageHelp.htm";


            } //EDN IF

            $.get("helpFiles/" + page,
        function (data) {


            $('#Help').html(data);
        });


        }


    });




    //Invite friend
    $('#addfriend').live('click', function () {

        $elementContent = $(this).html();
        $memberID = $('#profilesMemnerID').val();
        $addRemoveFrnd = $('#addfriend');

        if ($elementContent == "Add as friend") {





            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "WebServices/FriendWebservice.asmx/InviteFriend",
                data: '{friendID:"' + $memberID + '" }',

                //ON SUCCESS
                success: function (data) {

                    $('#profileButtons').html('<a id="requestPending" class="button  big disabledBtn">Request Pending..</a><br><a id="blockfriend" class="button  big ">Block this Person</a><br>');



                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {


                  
                }

            });


            $(this).css('background-image', "url('Images/delete.png')");

        }
        else {

            $(this).css('background-image', "url('Images/loader.gif')");

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "WebServices/FriendWebservice.asmx/Unfriend",
                data: '{friendID:"' + $memberID + '" }',

                //ON SUCCESS
                success: function (data) {

                    $($addRemoveFrnd).html("Add as friend");

                    //updating tag counts
                    if (data.d == true) {
                        // $($addRemoveFrnd).html("");


                    }




                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {


                   
                }






            });

            $(this).css('background-image', "url('Images/add.png')");
        }


    });

    //Block a Friend
    $('#blockfriend').live('click', function () {

        $elementContent = $(this).html();
        $memberID = $('#profilesMemnerID').val();
        $addRemoveFrnd = $('#blockfriend');

        if ($elementContent == "Block this Person") {



            $(this).css('background-image', "url('Images/loader.gif')");

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "WebServices/FriendWebservice.asmx/Block",
                data: '{friendID:"' + $memberID + '" }',

                //ON SUCCESS
                success: function (data) {

                    //updating tag counts
                    if (data.d == true) {


                        $($addRemoveFrnd).html("Unblock this Person");

                    }




                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {


              
                }

            });

            $(this).css('background-image', "url('Images/unblock.png')");

        }
        else {

            $(this).css('background-image', "url('Images/loader.gif')");

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "WebServices/FriendWebservice.asmx/Unblock",
                data: '{friendID:"' + $memberID + '" }',

                //ON SUCCESS
                success: function (data) {

                    //updating tag counts
                    if (data.d == true) {

                        $($addRemoveFrnd).html("Block this Person");

                    }




                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {


                
                }






            });

            $(this).css('background-image', "url('Images/block.png')");

        }


    });

    //###########END Profile#####################################




    //#################################################################
    //            POPUP CODER
    //#################################################################




    // $(this).css("box-shadow", "1px 1px 2px rgba(200, 200, 200, 0.2) inset");

    //    $('.popbox').popbox({
    //        'open': '.open',
    //        'box': '.box',
    //        'arrow': '.arrow',
    //        'arrow-border': '.arrow-border',
    //        'close': '.close'
    //    });

    //  $(".trigger").click(function () {

    //  var pos = $(this).position();

    // .outerWidth() takes into account border and padding.
    //        var width = $(this).outerWidth();
    //        var v = width/2;

    //        $(".popup").animate({
    //            top: pos.top()+'px',
    //            left: (pos.left() + width)+'px',
    //           display: 'block'
    //        }, 350, 'swing', function () {
    //            // Animation complete.




    //        });
    //        $(".popup").fadeIn('fast');

    // });








    //###############END POPUP########################################
    //enable tiptip tooltip plugin
    //    $(function () {
    //        $(".tip").tipTip();
    //    });







    //remove nav div
    $('#menuDiv div:nth-child(3)').remove();

    pathname = window.location.pathname;



    $("#helpLinkBtn").click(function () {

        var page = "";
        $("#Help").dialog({
            resizable: false,
            height: 600,
            width: 900,
            modal: true,
            buttons: {

                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        }); //END HELP DIALOG


        //empties the help center dialog
        $('div#Help').empty();

        if (pathname == '/BulaBula/Home.aspx') {

            page = "homePageHelp.htm";

        } //END IF 

        if (pathname == '/BulaBula/ComposeMessage.aspx') {

            page = "composeMessage.htm";


        } //EDN IF

        //GET HELP PAGE
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "helpFiles/" + page,
            data: '{}',

            //ON SUCCESS
            success: function (data) {


                $('#Help').html(data.d);




            }

        }); //END AJAX

    });


    $('#btnDeleteMsge').click(function () {

        $("#itemToDelete").html('this message');

        $("#dialog-confirm").attr('title', 'Delete Message ?');
        $("#dialog-confirm").dialog('option', 'title', "Delte Message ?");

        $("#dialog-confirm").dialog({
            resizable: false,
            height: 160,
            width: 400,
            modal: true,
            buttons: {
                "Delete Message": function () {


                    $('#dialog-confirm').empty();
                    $('<p id="dialogProgress"><span class="ui-icon ui-icon-circle-check" style="float:left; margin:0 7px 50px 0;"> </span></p>').appendTo('#dialog-confirm');
                    $(this).empty();


                    $(this).empty();
                    $(this).dialog("close");
                    $("<p><span class='ui-icon ui-icon-circle-check' style='float:left; margin:0 7px 50px 0;'></span>Are you sure you want to delete this message ?</p>").appendTo(this);

                    var $currentId = $(".SingleViewmsgeWrapper").attr('id');


                    $.post("Ajax_Processors/ProcessData.aspx",
        { msgeID: $currentId },
        function (data) {
            $("div#leftColumn").empty();

            $(data).appendTo("div#leftColumn").fadeIn('slow');



        });


                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });



    });






    $('#A1').click(function () {


        $("#dialog-message").dialog({
            modal: true,
            buttons: {
                Ok: function () {
                    $(this).dialog("close");
                }
            }
        });



    });





    $('#reply').click(function () {

        $('#replyMsge').focus();



    });





    //SearchBox Length animation
    $('#searchTxtBox').focus(function () {
        $(this).css("box-shadow", "1px 1px 2px rgba(200, 200, 200, 0.2) inset");
        $(this).animate({

            width: '320px'
        }, 350, 'swing', function () {
            // Animation complete.




        });
    });


    if ($('#StayExpanded').val() == 'expanded') {

        $('#expandContract').click();

    }

    $('#expandContract').click(function () {
        $expandContract = $(this);

        if ($expandContract.html() == '+') {

            $('.groupsToHihe').slideDown('slow');
            $expandContract.html('-')
            $('#StayExpanded').val('expanded');

        }
        else {
            $('.groupsToHihe').slideUp('slow');
            $expandContract.html('+')
            $('#StayExpanded').val('contracted');
        }


    })




    $('#replyMsge').focus(function () {
        $(this).animate({

            height: '190px'
        }, 1000, 'swing', function () {
            // Animation complete.
        });
    });

    $("#deleteAllMessages").live('click', function () {

        var MessageIDs = new Array();
        $test = "";
        $count = 0;

        $('.checkBox input').each(function () {
            var mcCbxCheck = $(this);

            if (mcCbxCheck.is(':checked')) {

                $thisPost = $(mcCbxCheck).closest('.msgeWrapper');

                $currentId = $($thisPost).attr('id');
                $test = $test + "_" + $currentId;

                MessageIDs[$count] = $currentId;
                $count = $count + 1;


            }
            else {

                return false;
            }
        });

        //Updating dialog title
        $("#dialog-confirm").attr('title', 'Delete all (' + $count + ')Messages ?');
        $("#dialog-confirm").dialog('option', 'title', 'Delete all (' + $count + ')Messages ?');
        $("#itemToDelete").html('all (' + $count + ')Messages');


        if ($count > 0) {
            $count = 0;
            $("#dialog-confirm").dialog({
                resizable: false,
                height: 160,
                width: 430,
                modal: true,
                buttons: {
                    "Delete": function () {
                        //Actual deleting
                        $(this).dialog("close");

                        //PostIDs
                        //AJAX POST
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            url: "WebServices/processMessages.asmx/DeleteMessages",
                            data: '{messageIDs:"' + $test + '" }',

                            //ON SUCCESS
                            success: function (data) {

                                $('.checkBox input').each(function () {

                                    var mcCbxChec = $(this);

                                    if (mcCbxChec.is(':checked')) {

                                        $thisPost = $(mcCbxChec).closest('.msgeWrapper');
                                        $thisPost.fadeOut('slow');



                                    }
                                    else {

                                        return false;
                                    }
                                });


                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                /// debugger;

                            
                            }

                            //END IF ELSE


                        }); //END AJAX



                    },

                    Cancel: function () {
                        $(this).dialog("close");
                    }

                }
            });

        }


    });


    $("div.msge").click(function () {

        $thisPost = $(this).closest('.msgeWrapper');

        var $currentId = $($thisPost).attr('id');



        $("#SelectedMessageID").val($currentId);
        $('#form1').submit();




    });

    $("#prevMsge").click(function () {

        var $currentId = $('.SingleViewmsgeWrapper').attr('id');
        $GetNxtPre($currentId, "pre");

    });

    $("#nextMsge").click(function () {

        var $currentId = $('.SingleViewmsgeWrapper').attr('id');
        $GetNxtPre($currentId, "nxt");

    });



    $GetNxtPre = function (i, x) {

        if (i != '') {

            //AJAX POST
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "WebServices/processMessages.asmx/GetNxtPre",
                data: '{messageID:"' + i + '", condition:"' + x + '" }',      // data: '{param:"value"}',

                //ON SUCCESS
                success: function (data) {

                    $('.msgeSent').remove();
                    $('.SingleViewmsgeWrapper').remove();
                    $(data.d[2]).insertBefore('#replyMsge');

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    /// debugger;

              


                }

                //END IF ELSE


            });
        }
        else { }

    };



    $("ul.onlineFriends a:not(ul.onlineFriends span a)").hover(function () {

        $(this).css("background-color", "#f1f1f1");

        $(this).delay(70).animate({

            'margin-left': '10px'
        }, 300, 'swing', function () {
            // Animation complete.
        });



    }, function () {


        $(this).animate({

            'margin-left': '0'
        }, 100, 'linear', function () {
            // Animation complete.
        });


    });



    $("div.msgeWrapper").hover(function () {

        if (!$(this).hasClass('isNotRead')) {

            $(this).css("background-color", "#EDF2FF").fadeIn();
        }

        $(".slideInMenu span", this).show();



    }, function () {

        if (!$(this).hasClass('isNotRead')) {

            $(this).css("background-color", "#FFFFFF");
        }

        $(".slideInMenu span", this).hide();
    });

    $('#btnReply').click(function () {

        $friendID = $('.replyMsge').attr('id');
        $msgeTxt = $('#replyMsge').val();
        $SendAllMessage($friendID, $msgeTxt);
        $('#replyMsge').val('');

    });

    var $SendAllMessage = function (friendID, msge) {


        if (msge == '') {
            $('#MainContent_messageTextarea').focus();

        }
        else {

            $("<div id='sendMesgprogress'></div>").insertAfter('#composer');

            $('msgeSentNotification').remove();
            //AJAX POST
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "WebServices/processMessages.asmx/SendMessage",
                data: '{messageTxt:"' + msge + '", friendID:"' + friendID + '" }',      // data: '{param:"value"}',

                //ON SUCCESS
                success: function (data) {


                    $("#sendMesgprogress").fadeOut(100);
                    $(".blueCircleLoader").remove();

                    $('#ProfileMsge').dialog('close');

                    $(data.d).insertBefore('#composer')
                              .hide()
                              .fadeIn('slow');



                    $('#MainContent_TxtRecipient').val("");
                    $('#MainContent_messageTextarea').val("");
                    $('memberID').val('');
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    /// debugger;

           
                }

                //END IF ELSE


            });

        }

    }

    //  $('#ProfileMsge').bind('dialogclose', function (event) {
    //        $.fx.speeds._default = 1000;

    //        $('#ProfileMsge').dialog('option', 'hide', 'fade');

    //        $.fx.speeds._default = 400;
    // });

    //    $("#closeSimple").click(function () {
    //        $("div#simpleModal").removeClass("show");
    //        
    //        return false;
    //    });

    $('#videoBox').bind('dialogclose', function (event) {
        $(this).empty();


    });


    $('#tag').bind('dialogclose', function (event) {

        $('#tag input').val('');


    });

    $('#eventsDialog').bind('dialogclose', function (event) {

        $('#eventsDialog input').val('');


    });


    $('#ProfileMsge').bind('dialogclose', function (event) {

        $('#ProfileMsge input').val('');


    });



    $.extend($.ui.dialog.prototype.options, {
        hide: { effect: 'fade', duration: 1000 },
        show: { effect: 'bounce', duration: 100 }
    });

    //Send Message
    $("#btnSendMsge").click(function () {



        var msge = $('#MainContent_messageTextarea').val();
        var friend = $('#MainContent_TxtRecipient').val();
        var friendID = $('#memberID').val();
        var replyMsge = $('#replyMsge').val();

        //Showing progress-->
        $("<span style='width:24px; height:24px; margin-top: 17px' class='blueCircleLoader'></span>").insertAfter('#btnSendMsge');
        $('.blueCircleLoader').addClass('sendCommentProgress')
            .fadeIn('fast');
        //<--End Showing progress

        //REPLACE CERTAIN CHARACTERS
        msge = msge.replace(/&/g, '&amp;').replace(/>/g, '&gt;').replace(/</g, '&lt;').replace(/"/g, '&quot;').replace(/'/g, '&#39;');

        // debugger

        if (friend == "Type in your friend's name here...") {

            $('#MainContent_TxtRecipient').focus();

        }
        else {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "WebServices/processMessages.asmx/SendMultiMessage",
                data: '{messageTxt:"' + msge + '", friendList:"' + myFriendList + '" }',      // data: '{param:"value"}',

                //ON SUCCESS
                success: function (data) {


                    $("#sendMesgprogress").fadeOut(100);
                    $(".blueCircleLoader").remove();

                    $('#ProfileMsge').dialog('close');

                    $(data.d).insertBefore('#composer')
                              .hide()
                              .fadeIn('slow');



                    $('#MainContent_TxtRecipient').val("");
                    $('#MainContent_messageTextarea').val("");
                    $('memberID').val('');
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    /// debugger;

                   
                }

                //END IF ELSE


            });

            //            for (var i = 0; i < friendCounter; ++i) {
            //                
            //            $SendAllMessage(;, msge);
            //            
            //            }



        }
    });

    $('.videoLaunch').live('click', function () {




        $videoTitle = $(this).html();

        $("#videoBox").attr('title', $videoTitle);
        $("#videoBox").dialog('option', 'title', $videoTitle);


        var $thisPost = $(this).closest('.aPost');
        $postID = $($thisPost).attr('id');


        //AJAX POST
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "WebServices/postsWebservice.asmx/GetVideo",
            data: '{postID:"' + $postID + '" }',

            //ON SUCCESS
            success: function (data) {

                $("#videoBox").html(data.d);
                //debugger

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                /// debugger;

            }

        });





        $("#videoBox").css({ 'width': ($(window).width() - 70) + 'px', 'height': ($(window).height() - 100) + 'px' });
        //$("#pictureBox .photoPostImage").css({ 'max-width': ($picturebox.width() - 20) + 'px', 'max-height': ($picturebox.height() - 20) + 'px' });

        $("#videoBox").dialog({
            resizable: false,
            height: 'auto',
            width: 'auto',
            position: 'center',
            modal: true,
            buttons: {
                "Close": function () {

                    $(this).dialog("close");

                }

            }
        });











    })


    $('.pictureLaunch').live('click', function () {




        $picturebox = $("#pictureBox");

        $picturebox.html($(this).html());
        $('#pictureBox .photoPostImage').removeClass('thumbnails');
        $('#pictureBox .photoPostImage').css({ 'margin-left': '0px' });
        $picturebox.css({ 'max-width': ($(window).width() - 70) + 'px', 'max-height': ($(window).height() - 100) + 'px' });
        $("#pictureBox .photoPostImage").css({ 'max-width': ($picturebox.width() - 20) + 'px', 'max-height': ($picturebox.height() - 20) + 'px' });

        $picturebox.dialog({
            resizable: false,
            height: 'auto',
            width: 'auto',
            position: 'center',
            modal: true,
            buttons: {
                "Close": function () {

                    $(this).dialog("close");

                }

            }
        });


    })



    $('#pictureBox').bind('dialogclose', function (event) {
        $('#pictureBox').empty();
    });


    //send message profile
    $("#sendMessage").click(function () {

        var m = $('#profileName').val();

        $('#recipient').html(m);


        $("#ProfileMsge").dialog({
            resizable: false,
            height: 400,
            width: 500,
            modal: true,
            buttons: {
                "Send": function () {

                    var msge = $('#messageTextarea').val();
                    var friendID = $('#profilesMemnerID').val();

                    //show loading progress
                    $('.blueCircleLoader').addClass('dialogProgress');
                    $('.blueCircleLoader').insertAfter('.ui-dialog-buttonset')
                    .fadeIn('fast');
                    // 


                    $SendAllMessage(friendID, msge);
                    $('#messageTextarea').val('');

                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });


    });  //End send message


    //DELETE A MESSAGE

    $(".deleteMsge").click(function () {



        var $currentId = $('div.SingleViewmsgeWrapper').attr('id');


        $("<div id='sendMesgprogress'></div>").insertAfter('#composer');


        if (msge != '' && friend != '') {

            $.post("Ajax_Processors/ProcessData.aspx",
        { msgex: msge, dlteMsgeID: $currentId, msgeID: '', memberIDx: '' },
        function (data) {


            if (test != '}{') {


                $('msge', this).fadeOut();
            }
            else {


                $("<div class='msgeNotSent'>Sorry your message was not sent</div>").insertBefore('#composer').slideDown('slow');
            }
        });




        }
        else { }
        return false;
    });
    //END DELETE

    //    var placeholder = 'placeholder' in document.createElement('input');
    //    if (!placeholder) {
    //        // $.getScript("../js/placeholder.js", function () {
    //        $(":input").each(function () {   // this will work for all input fields
    //            $(this).placeHolder();
    //            // });
    //        });
    //    }

    //Placeholder hack
    (function ($) {

        $.fn.placeHolder = function () {
            var input = this;
            var text = input.attr('placeholder');  // make sure you have your placeholder attributes completed for each input field
            if (text) input.val(text).css({ color: 'grey' });
            input.focus(function () {
                if (input.val() === text) input.css({ color: 'lightGrey' }).selectRange(0, 0).one('keydown', function () {
                    input.val("").css({ color: 'black' });
                });
            });
            input.blur(function () {
                if (input.val() == "" || input.val() === text) input.val(text).css({ color: 'grey' });
            });
            input.keyup(function () {
                if (input.val() == "") input.val(text).css({ color: 'lightGrey' }).selectRange(0, 0).one('keydown', function () {
                    input.val("").css({ color: 'black' });
                });
            });
            input.mouseup(function () {
                if (input.val() === text) input.selectRange(0, 0);
            });
        };

        $.fn.selectRange = function (start, end) {
            return this.each(function () {
                if (this.setSelectionRange) this.setSelectionRange(start, end);
                else if (this.createTextRange) this.createTextRange().moveStart('character', start);
            });
        };

    })(jQuery);

    /*##############################################################################*
    BROUSER History  
    #############################################################################*/





    var myFriendList = new Array(100)
    var friendCounter = 0;
    var tempC = 0;
    //   myFriendList[i] = new Array(2)

    //  


    //   myFriendList[1][1] = "Arrays"
    //   myFriendList[1][2] = "How to create arrays?"

    function extractLast(term) {
        return split(term).pop();
    }
    function split(val) {
        return val.split(/,\s*/);
    }


    $("#MainContent_TxtRecipient").bind("keydown", function (event) {
        if (event.keyCode === $.ui.keyCode.TAB &&
						$(this).data("autocomplete").menu.active) {
            event.preventDefault();
        }
    })
    .autocomplete({
        minLength: 1,


        source: function (request, response) {
            //


            $('.ui-autocomplete .ui-menu').css('width', '231px');


            if (tempC != 0) {
                var tempFriend = request.term.split(', ');
                $friend = tempFriend[(friendCounter)];
            }
            else { $friend = request.term; }

            tempC = tempC + 1;
            friendCounter = friendCounter + 1;

            console.log($friend);
            console.log(tempFriend);
            console.log(friendCounter);
            //            console.log($friend);

            $("#MainContent_TxtRecipient").addClass('textboxProgress');
            $.ajax({

                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                url: "WebServices/Searchfriend.asmx/GetAllFriends",
                data: '{prefixTxt:"' + $friend + '" }',      // data: '{param:"value"}',

                //ON SUCCESS 
                success: function (data) {
                    $("#MainContent_TxtRecipient").removeClass('textboxProgress');
                    $members = data.d;

                    response($.ui.autocomplete.filter(
						$members, extractLast(request.term)));

                    //response($members);

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    /// debugger;

                 
                }





            });



        },

        focus: function (event, ui) {


            return false;
        },

        select: function (event, ui) {
            var terms = split(this.value);
            // remove the current input
            terms.pop();
            // add the selected item
            terms.push(ui.item.value);
            // add placeholder to get the comma-and-space at the end
            terms.push("");
            this.value = terms.join(", ");

            myFriendList[friendCounter] = ui.item.id;
            //            myFriendList[0][1] = "Arrays"
            //            myFriendList[0][2] = "What is an array?"


            // $("#friendSearchID").val(ui.item.id);
            // $("#MainContent_profileFriendID").val(ui.item.id);
            $('#memberID').val(ui.item.id);
            return false;

        }

    }); //END Autocomplete extender





    $('.btnViewDetails').live('click', function () {

        var thisPost = $(this).closest('.aPost');
        $('.eventDetails', thisPost).slideDown('slow');


    });

    /************************************************************************************************
    *
    *                                   REPORTS - CODE
    *
    ************************************************************************************************/


    $("#generateReports").click(function () {


        s = $("#startTime").val();
        sd = $("#startDateR").val();
        startDate = sd + " " + s;


        e = $("#endTime").val();
        ed = $("#endDateR").val();
        endDate = ed + " " + e;


        SDsecs = $ConvertToSeconds(sd, s);
        EDsecs = $ConvertToSeconds(ed, e);

        dateDiff = EDsecs - SDsecs;


        var isRequiredFilled = false;
        $("#dateTable input").each(function () {

            var temp = $(this).val();

            if ($.trim(temp) == "") {

                $(this).addClass('EventsWarning');
                $("#eventsErrorEmpty").slideDown('fast');
                isRequiredFilled = false;
            }
            else {
                isRequiredFilled = true;
                $(this).removeClass('EventsWarning');

            }

        });





        if (isRequiredFilled) {

            $("#dateTable input").each(function () {

                $(this).removeClass('EventsWarning');
                $("#eventsErrorEmpty").hide('fast');

            });







            if (dateDiff < 0) { //validating date and time


                $("#eventsErrorInvalidDates").slideDown('fast');
                $(".DateTime").addClass('EventsWarning');
            }
            else { //If all validation is passed
                $("#eventsErrorInvalidDates").hide('fast');



                //Updating Bar Graph
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    url: "Administration.aspx/RefreshNmbersInGroups",
                    data: '{startDate:"' + startDate + '", endDate:"' + endDate + '"}',

                    //ON SUCCESS
                    success: function (data) {
                        var groups = data.d;
                        var b = groups[0].split(',');
                        for (var i = 0; i < b.length; i++) { b[i] = +b[i]; }
                        var a = groups[1].replace(/'/g, '').split(',');
                        b.pop();
                        a.pop();
                        $("#cvs1").remove();
                        $("#MembersInG").append('<canvas style="border: 1px solid gray; border-radius: 15px 15px 15px 15px;" height="700" width="610" id="cvs1">[No canvas support]</canvas>');
                        generateMembersInGroups(b, a);




                    }

                });

                //Updating Pie Graph
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    url: "Administration.aspx/RefreshPie",
                    data: '{startDate:"' + startDate + '", endDate:"' + endDate + '"}',

                    //ON SUCCESS
                    success: function (data) {
                        var groups = data.d.split(',');

                        for (var i = 0; i < groups.length; i++) { groups[i] = +groups[i]; }


                        $("#pie4").remove();
                        $("#PostCompare").append(' <canvas id="pie4" width="600" height="400" style="margin-top:15px;">[No canvas support]</canvas>');
                        generatePostPie(groups);




                    }

                });



                //Updating Statistics
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    url: "Administration.aspx/UpdateStats",
                    data: '{startDate:"' + startDate + '", endDate:"' + endDate + '"}',

                    //ON SUCCESS
                    success: function (data) {
                        var stats = data.d;


                        $('#memberStats').html(stats[0]);
                        $('#postsStats').html(stats[1]);
                        $('#messagesStats').html(stats[2]);
                        $('#reportsStats').html(stats[3]);
                        $('#BmembersStats').html(stats[4]);







                    }

                });



            }

        }

    });


    /************************************************************************************************
    *
    *                                   Chatting code
    *
    ************************************************************************************************/


    $('#sendChatText').click(function () {

        $chatText = $('#ChatTextArea').val();
        $chatText = $.trim($chatText);
        if ($chatText != '') {
            $SendChatText($chatText);
        }

    });


    /************************************************************************************************
    *
    *                                   SIGNAL-R CODE
    *
    ************************************************************************************************/

    $thisGroupsID = $('#MainContent_ThisGroupsID').val();

    $InsertPost = function (postHtml, groupID, type) {




        if ($('.ui-tabs-selected #Text_Tab').length > 0 && groupID == $thisGroupsID && type == 'Text') {//If in Text Tab Groups page

            $('#Text .PostWrapper').after(postHtml);
        }
        else if ($('.ui-tabs-selected #Events_Tab').length > 0 && groupID == $thisGroupsID && type == 'Event') {//If Events Tab Groups page

            $('#Events .postInnerWrapper').prepend(postHtml);
        }
        else if ($('.ui-tabs-selected #Posts_Tab').length > 0 && groupID == $thisGroupsID) {//If in newsfeed Groups page

            $('#Posts').prepend(postHtml);
        }
        else if ($('.ui-tabs-selected #Posts_Tab').length > 0 && $('#MainContent_ThisGroupsID').length == 0) { //If in newsfeed home page

            $('#Posts').prepend(postHtml);
        }
        else if ($('.ui-tabs-selected #Text_Tab').length > 0 && $('#MainContent_ThisGroupsID').length == 0 && type == 'Text') {//If in Text Tab home page

            $('#Text').prepend(postHtml);
        }
        else if ($('.ui-tabs-selected #Events_Tab').length > 0 && $('#MainContent_ThisGroupsID').length == 0 && type == 'Event') {//If in Events Tab home page

            $('#Events').prepend(postHtml);
        }

    }

    var hub = $.connection.moveShape;


    $.extend(hub, {

        shapeMoved: function (cid, postText, groupID) {



            type = 'Text'
            $InsertPost(postText, groupID, type);

        }, //END shapeMoved

        insertEvent: function (cid, postText, groupID) {
            $('#eventDetails').val('');

            type = 'Event'
            $InsertPost(postText, groupID, type);

            $("#Events .postInnerWrapper").prepend(data.d);
            $("#Events").prepend(data.d);

            $("#eventsDialog input").each(function () {

                $(this).val('');
                $("#eventsDialog textarea").val('');


            });



        }, //END Insert Event

        InsertLatestPost: function (ConID, postText, groupID, type) {





            if ($('.ui-tabs-selected #Posts_Tab').length > 0 && groupID == $thisGroupsID) {

                $('#Posts').prepend(postText);
            }
            else if ($('.ui-tabs-selected #Posts_Tab').length > 0 && $thisGroupsID.length == 0) {

                $('#Posts').prepend(postText);
            }
            else if (type == 'Photo' && groupID == $thisGroupsID) {
                $('#Pictures .postInnerWrapper').prepend(postText);
            }
            else if (type == 'Video' && groupID == $thisGroupsID) {
                $('#Videos .postInnerWrapper').prepend(postText);
            }
            else if (type == 'File' && groupID == $thisGroupsID) {
                $('#Files .postInnerWrapper').prepend(postText);
            }





        }, //END updatePictures

        removeDeletedPost: function (cid, postID) {


            $('#' + postID).fadeOut('fast');



        }, //END Removing a deleted post

        removeDeletedComment: function (cid, commentCount, commentID, postID) {


            $('#' + commentID).fadeOut('fast');
            if (commentCount == "0") {

                $('#' + postID + ' .allComments').html('<h4>There are no comments for this post</h4>');

            }

            $('#' + postID + ' .commentCount').html(commentCount);



        }, //END Removing a deleted comment

        updatingComments: function (cid, commentCount, commentText, postID) {


            if (!$('#' + postID + ' .allComments').is(":visible")) {
                $('#' + postID + ' .commentCount').html(commentCount);

            }
            else {


                $('#' + postID + ' .allComments').html("<h4>All Comments</h4>");




                //incrementing the comment count

                $('#' + postID + ' .commentCount').html(commentCount);

                //adding actual comment in the comment list
                $('#' + postID + ' .allComments').append(commentText);

                $('#' + postID + ' .allComments').fadeIn('slow');

            }
        },
        updateChat: function (cid, chatString, groupID, sessionMemberID) {


            if (groupID == $thisGroupsID) {

                // <span class="ui-icon ui-icon-closethick"></span>
                $('#MessagesSection').append(chatString);
                $('#ChatTextArea ').val('');

            }


        },

        updatePostLikeCount: function (numberofPostLikes, postID) {
      
            if ($('#' + postID + ' .homeLikeBtn').html() == "Like") {

                $('#' + postID + ' .homeLikeBtn').html("Unlike");
            }
            else {

                $('#' + postID + ' .homeLikeBtn').html("Like");
            }

            $('#' + postID + ' .likesCount').empty();
            $('#' + postID + ' .likesCount').append(numberofPostLikes).fadeIn('slow');

        },

        updateTagCount: function (numberoftags, postID) {


            $('#' + postID + ' .tagsCount').html(numberoftags);

        }


    }); //END HUB





    $.connection.hub.start().done(function () {


        sessionMemberID = $('#SessionMemberID').val();

        $('#textPostButton').live('click', function () {

            var text = $('#textPostTxtArea').val();
            var groupID = $('#MainContent_ThisGroupsID').val();
            var sessionMemberID = $('#SessionMemberID').val();
            $('#textPostTxtArea').val("");
            hub.moveShape(text, sessionMemberID, groupID || 0);

        }); //END Submitting A POST


        $SendEventPost = function (startDate, endDate, name, host, venue, groupID, details, type) {



            $.connection.hub.start().done(function () {

                sessionMemberID = $('#SessionMemberID').val();

                hub.addEventPost(sessionMemberID, startDate, endDate, name, host, venue, groupID, details, type);
            });

        } //End send Event post


        //Deleting a post
        $DeleteAPost = function (postID) {
            $.connection.hub.start().done(function () {
                hub.deleteAPost(postID);
            });
        }
        //Deleting a comment
        $DeleteAComment = function (postID, commentID) {

            sessionMemberID = $('#SessionMemberID').val();
            $.connection.hub.start().done(function () {


                hub.deleteAComment(sessionMemberID, postID, commentID);
            });
        }




        //Deleting a comment
        $DeleteAComment = function (postID, commentID) {
            sessionMemberID = $('#SessionMemberID').val();
            $.connection.hub.start().done(function () {


                hub.deleteAComment(sessionMemberID, postID, commentID);
            });
        }


        //Adding a comment
        $AddAComment = function (postID, commentTxt) {
            sessionMemberID = $('#SessionMemberID').val();
            $.connection.hub.start().done(function () {


                hub.addComment(sessionMemberID, postID, commentTxt);
            });
        }


        //Liking a post
        $LikeAPost = function (postID) {

            $.connection.hub.start().done(function () {

                hub.likeAPost(sessionMemberID, postID);
            });
        }
        //Unliking a post
        $UnlikeAPost = function (postID) {

            $.connection.hub.start().done(function () {

                hub.unlikeAPost(sessionMemberID, postID);
            });
        }

        //Tag a post
        $TagAPost = function (postID, friendID) {
            $.connection.hub.start().done(function () {
                hub.tagAPost(sessionMemberID, postID, friendID);
            });
        }

        //Untag a post
        $UntagAPost = function (postID, friendID) {
            $.connection.hub.start().done(function () {
                hub.untagAPost(sessionMemberID, postID, friendID);
            });
        }




        $SendCaption = function ($caption) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "Group.aspx/GetTCaption",
                data: '{caption:"' + $caption + '"}',

                //ON SUCCESS
                success: function (data) {

                }

            });


        }

        /*
        *
        *############## CHATTING ######################
        */

        //Untag a post
        $SendChatText = function (chatText) {
            groupID = $('#MainContent_ThisGroupsID').val();

            $.connection.hub.start().done(function () {
                hub.insertChatText(sessionMemberID, groupID, chatText);
            });

        }

    });





    $Seven = function (e) {



        $t = $('#thisMemberID').val(); ;
        $.connection.hub.start().done(function () {
            hub.getLatestPost($t);
        });

        $('.ui-tabs-selected').fadeIn("fast", function () {




        });




    }






    return false;


});

