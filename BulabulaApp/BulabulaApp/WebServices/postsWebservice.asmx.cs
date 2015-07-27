using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using System.Text;
using System.Data;
using System.Collections;
using System.Drawing;



using System.Web.UI.WebControls;
using System.Configuration;
using AjaxControlToolkit;
using System.IO;
using System.Data.SqlClient;

namespace BulabulaApp.WebServices
{
    /// <summary>
    /// Summary description for processComments
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    [System.ComponentModel.ToolboxItem(false)]
    public class postsWebservice : System.Web.Services.WebService
    {

        private Post aPost;
        ArrayList ArrayPosts;
        ArrayList ArrayMembers;
        int countMembers;
        string newsfeed = null;
        // string allgroups = null;
        string[] postReturn = new string[2];
        int? latestPostID = null;
        StringBuilder concatinater = new StringBuilder();
        StringBuilder ChatText = new StringBuilder();
        Member aMember = new Member();
        LoginDAL loginDAL = new LoginDAL();
        // Member_Status member_Status;
        public string FriendsOnlineString = null;
        public string NotificationsString = null;
        Reusable_Methods reusable_Methods = new Reusable_Methods();
        NewsfeedDAL bl = new NewsfeedDAL();
        int GroupID;
        string tempMemberID = null;
        public int ClientUpdateGroupID { get; set; }
        public string ClientUpdateType  { get; set; }
        public string ClientUpdateMemberID { get; set; }


        #region Update posts
        [WebMethod(EnableSession = true)]
        public string ClientUpdateSignalR()
        {
            //-2 for get GetTop1Post
            return GetTabContents("Posts", -2, -1);

        }
        #endregion

        [WebMethod(EnableSession = true)]
        public void DeleteAPost(int postID)
        {

            NewsfeedDAL newsfeedDAL = new NewsfeedDAL();
            Post aPost = new Post(postID);
            newsfeedDAL.FlagPost(aPost);
        }

        [WebMethod(EnableSession = true)]
        public string SendTextPost(string postText, int groupID)
        {
            Member aMember = new Member(Context.Session["memberID"].ToString());

            Text_Post aText_Post = new Text_Post(postText);
            Group aGroup = new Group(groupID);

            PostDAL postDAL = new PostDAL();

            int postId = postDAL.InsertText(aGroup, aText_Post, aMember);

            #region GET POST
            ArrayPosts = bl.GetASinglePost(postId);
            int count = ArrayPosts.Count;

            for (int i = 0; i < count; ++i)
            {

                OpenWrapper(i);

                #region TEXT POST
                GetTextPosts();
                #endregion

                CloseWrapper();

            }
            return concatinater.ToString();
            #endregion


        }

        [WebMethod(EnableSession = true)]
        public string SendEventPost(string startDate, string endDate, string name, string host, string venue, int groupID, string details, string type)
        {
            Member aMember = new Member(Context.Session["memberID"].ToString());

            Group aGroup = new Group(groupID);

            PostDAL postDAL = new PostDAL();

            Reusable_Methods reusable_Methods = new Reusable_Methods();
            DateTime startDateD = reusable_Methods.FormatDateFromDateTimePicker(startDate);
            DateTime endDateD = reusable_Methods.FormatDateFromDateTimePicker(endDate);

            Event_Post aEvent_Post = new Event_Post(name, details, venue, startDateD, endDateD, host, type);
            int postId = postDAL.InsertEvent(aEvent_Post, aGroup, aMember);

            #region GET POST
            ArrayPosts = bl.GetASinglePost(postId);
            int count = ArrayPosts.Count;

            for (int i = 0; i < count; ++i)
            {

                OpenWrapper(i);

                #region EVENT POST
                GetEventPosts();
                #endregion

                CloseWrapper();

            }
            return concatinater.ToString();
            #endregion



        }

        [WebMethod(EnableSession = true)]
        public string GetPeopleLikedPost(int postID)
        {
            StringBuilder concatinater = new StringBuilder();
            if (Session["memberID"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                Member aMember = new Member(Context.Session["memberID"].ToString());

                Post aPost = new Post(postID);
                aPost.PostId = postID;

                RateAndTagDAL rateAndTagDAL = new RateAndTagDAL();

                List<Member> GetPeopleLikedPost = new List<Member>();
                GetPeopleLikedPost = rateAndTagDAL.GetPeopleLikedPost(aPost);

                concatinater.Append("<div class='postinfo width50% floatLeft tip'> <a title='");
                for (int i = 0; i < GetPeopleLikedPost.Count; ++i)
                {
                    concatinater.Append(GetPeopleLikedPost[i].MemberId);
                    concatinater.Append("<br />");
                }
            }
            return concatinater.ToString();

            //Details Left


            //foreach (var Member in AllLikedMembers)
            //{

            //    concatinater.Append(Member.DisplayName);
            //    concatinater.Append("<br />");
            //}

        }

        [WebMethod(EnableSession = true)]
        public string GetVideo(int postID)
        {
            StringBuilder concatinater = new StringBuilder();

            string videoUrl = "ProcessVideo.ashx?id=" + postID;
            concatinater.Append("<video id='video' controls='controls' autoplay='autoplay'>");
            concatinater.Append("<source src='");
            concatinater.Append(videoUrl);
            concatinater.Append("' type='video/ogg' />");
            concatinater.Append("Your browser does not support the video tag.");
            concatinater.Append("</video>");

            return concatinater.ToString();
        }

       [WebMethod(EnableSession = true)]
        public string RefreshChats(int groupID)
        {
            ChatDAL chatDAL = new ChatDAL();
        List<Article_Post> at = chatDAL.getAllChats(groupID);
        string chatString = null;
        foreach (var ChatMessage in at)
        {
            Group group = new Group(groupID);
            Member thisMember = new Member(ChatMessage.MemberID);


        //    Post post = new Post(postID);
          //  Article_Post thisArticle = chatDAL.getChatText(ref post);

            //BulabulaApp.Other_Classes. date new = reusable_Methods.FormatDateTime(aPost.CreateDate);
            Reusable_Methods reusable_Methods = new Reusable_Methods();
            string date = reusable_Methods.FormatDateTimeForChat(ChatMessage.CreateDate);
            thisMember = chatDAL.GetMemberDisplayName(thisMember);

            ChatText.Append(" <div class='chatMemberWrapper'>");
            ChatText.Append("<div class='memberNameSection  '> <a id=");
            ChatText.Append(thisMember.MemberId);
            ChatText.Append(" class='btnMemberProfile pointer' ><em>");
            ChatText.Append(thisMember.DisplayName);
            ChatText.Append("</em></a></div>");
            ChatText.Append("</a></div>");
            ChatText.Append("<div class='chatContentSection '>");
            ChatText.Append(ChatMessage.ArticleText);
            ChatText.Append("</div> <div class='clr'></div>");
            ChatText.Append("</div>");
            chatString = ChatText.ToString();
        
        }//end foreach

        return chatString;
        }
        

        [WebMethod(EnableSession = true)]
        public string GetTabContents(string tabName, int GroupId, int lastPostID)
        {

            GroupID = GroupId;
            #region NEWS FEED TAB
            if (tabName == "Posts")
            {

                if (lastPostID > -1)
                {
                    ArrayPosts = bl.GetNextTop20Posts(lastPostID);
                }
                else if (lastPostID > -1 && GroupId > -1)
                {
                    ArrayPosts = bl.GetNextTop20PostsForAGroup(lastPostID, GroupId);
                }
                else if (GroupId == -1)
                {
                    ArrayPosts = bl.GetTop20NewsFeeds();
                }
                else if (GroupId > -1)
                {
                    ArrayPosts = bl.GetTop20NewsFeedsForAGroup(GroupId);
                }
                else if (GroupId == -2)
                {
                    ArrayPosts = bl.GetTop1Post();
                }



                int count = ArrayPosts.Count;

                if (count > 0)
                {
                    for (int i = 0; i < count; ++i)
                    {

                        OpenWrapper(i);

                        #region TEXT POST
                        if (aPost.PostType == "Text")
                        {
                            GetTextPosts();
                        }
                        #endregion

                        #region EVENT POST
                        else if (aPost.PostType == "Event")
                        {
                            GetEventPosts();
                        }
                        #endregion

                        #region PHOTO POST
                        else if (aPost.PostType == "Photo")
                        {
                            GetPhotoPosts();
                        }
                        #endregion

                        #region ARTICLE POST
                        else if (aPost.PostType == "Article")
                        {
                            //GetArticlePosts();

                        }
                        #endregion

                        #region VIDEO POST
                        else if (aPost.PostType == "Video")
                        {
                            GetVideoPosts();
                        }
                        #endregion

                        #region FILE POST
                        else if (aPost.PostType == "File")
                        {
                            GetFilePosts();

                        }
                        #endregion

                        CloseWrapper();

                    }
                }
                else
                {
                    if (lastPostID == -1)
                    {
                        concatinater.Append("<div class='allComments noMorePosts' style='display: block;'><h4>There are no more post here</h4></div>");
                    }
                    else
                    {
                        concatinater.Append("<div class='allComments noMorePosts' style='display: block;'><h4>There are no more post here</h4></div>");
                    }
                }
                newsfeed = concatinater.ToString();

            }
            #endregion

            #region TEXT TAB
            else if (tabName == "Text")
            {

                if (lastPostID > -1)
                {
                    ArrayPosts = bl.GetNextTop20TextPosts(lastPostID);
                }
                else if (lastPostID > -1 && GroupId > -1)
                {
                    ArrayPosts = bl.GetNextTop20TextPostsForAGroup(lastPostID, GroupId);
                }
                else if (GroupId == -1)
                {
                    ArrayPosts = bl.GetTop20TextPosts();
                }
                else
                {
                    ArrayPosts = bl.GetTop20TextPostsForAGroup(GroupId);
                }

                int count = ArrayPosts.Count;

                if (count > 0)
                {
                    for (int i = 0; i < count; ++i)
                    {

                        OpenWrapper(i);

                        GetTextPosts();

                        CloseWrapper();

                    }
                }
                else
                {
                    concatinater.Append("<div class='allComments noMorePosts' style='display: block;'><h4>There are no more post here</h4></div>");
                }
                newsfeed = concatinater.ToString();
            }
            #endregion

            #region EVENT TAB
            else if (tabName == "Events")
            {

                if (lastPostID > -1)
                {
                    ArrayPosts = bl.GetNextTop20EventPosts(lastPostID);
                }
                else if (lastPostID > -1 && GroupId > -1)
                {
                    ArrayPosts = bl.GetNextTop20EventPostsForAGroup(lastPostID, GroupId);
                }
                else if (GroupId == -1)
                {
                    ArrayPosts = bl.GetTop20EventPosts();
                }
                else
                {
                    ArrayPosts = bl.GetTop20EventPostsForAGroup(GroupId);
                }
                int count = ArrayPosts.Count;

                if (count > 0)
                {
                    for (int i = 0; i < count; ++i)
                    {

                        OpenWrapper(i);

                        GetEventPosts();

                        CloseWrapper();
                    }
                }
                else
                {
                    concatinater.Append("<div class='allComments noMorePosts noTabContent' style='display: block;'><h4>There are no more post here</h4></div>");
                }

                newsfeed = concatinater.ToString();
            }
            #endregion

            #region PHOTO TAB
            else if (tabName == "Pictures")
            {
                if (lastPostID > -1)
                {
                    ArrayPosts = bl.GetNextTop20PhotoPosts(lastPostID);
                }
                else if (lastPostID > -1 && GroupId > -1)
                {
                    ArrayPosts = bl.GetNextTop20PhotoPostsForAGroup(lastPostID, GroupId);
                }
                else if (GroupId == -1)
                {
                    ArrayPosts = bl.GetTop20PhotoPosts();
                }
                else
                {
                    ArrayPosts = bl.GetTop20PhotoPostsForAGroup(GroupId);
                }

                int count = ArrayPosts.Count;

                if (count > 0)
                {
                    for (int i = 0; i < count; ++i)
                    {

                        OpenWrapper(i);

                        GetPhotoPosts();

                        CloseWrapper();


                    }
                }
                else
                {
                    concatinater.Append("<div class='allComments noTabContent noMorePosts' style='display: block;'><h4>There are no more post here</h4></div>");
                }
                newsfeed = concatinater.ToString();
            }
            #endregion

            #region ARTICLE TAB
            else if (tabName == "Articles")
            {
                if (lastPostID > -1)
                {
                    ArrayPosts = bl.GetNextTop20ArticlePosts(lastPostID);
                }
                else if (lastPostID > -1 && GroupId > -1)
                {
                    ArrayPosts = bl.GetNextTop20ArticlePostsForAGroup(lastPostID, GroupId);
                }
                else if (GroupId == -1)
                {
                    ArrayPosts = bl.GetTop20ArticlePosts();
                }
                else
                {
                    ArrayPosts = bl.GetTop20ArticlePostsForAGroup(GroupId);
                }

                int count = ArrayPosts.Count;

                if (count > 0)
                {
                    for (int i = 0; i < count; ++i)
                    {

                        OpenWrapper(i);

                        GetArticlePosts();

                        //CloseWrapper();

                        


                    }
                }
                else
                {
                    concatinater.Append("<div class='allComments noTabContent noMorePosts' style='display: block;'><h4>There are no post here</h4></div>");
                }
                newsfeed = concatinater.ToString();
            }
            #endregion

            #region VIDEO TAB
            else if (tabName == "Videos")
            {
                if (lastPostID > -1)
                {
                    ArrayPosts = bl.GetNextTop20VideoPosts(lastPostID);
                }
                else if (lastPostID > -1 && GroupId > -1)
                {
                    ArrayPosts = bl.GetNextTop20VideoPostsForAGroup(lastPostID, GroupId);
                }
                else if (GroupId == -1)
                {
                    ArrayPosts = bl.GetTop20VideoPosts();
                }
                else
                {
                    ArrayPosts = bl.GetTop20VideoPostsForAGroup(GroupId);
                }

                int count = ArrayPosts.Count;
                if (count > 0)
                {
                    for (int i = 0; i < count; ++i)
                    {

                        OpenWrapper(i);

                        GetVideoPosts();

                        CloseWrapper();


                    }
                }
                else
                {
                    concatinater.Append("<div class='allComments noTabContent noMorePosts' style='display: block;'><h4>There are no more post here</h4></div>");
                }
                newsfeed = concatinater.ToString();
            }
            #endregion

            #region FILE TAB
            else if (tabName == "Files")
            {
                if (lastPostID > -1)
                {
                    ArrayPosts = bl.GetNextTop20FilePosts(lastPostID);
                }
                else if (lastPostID > -1 && GroupId > -1)
                {
                    ArrayPosts = bl.GetNextTop20FilePostsForAGroup(lastPostID, GroupId);
                }
                else if (GroupId == -1)
                {
                    ArrayPosts = bl.GetTop20FilePosts();
                }
                else
                {
                    ArrayPosts = bl.GetTop20FilePostsForAGroup(GroupId);
                }
                int count = ArrayPosts.Count;
                if (count > 0)
                {
                    for (int i = 0; i < count; ++i)
                    {

                        OpenWrapper(i);

                        GetFilePosts();

                        CloseWrapper();


                    }
                }
                else
                {
                    concatinater.Append("<div class='allComments noTabContent noMorePosts' style='display: block;'><h4>There are no more post here</h4></div>");
                }
                newsfeed = concatinater.ToString();
            }
            #endregion

            return concatinater.ToString();

        }

        #region GET POSTS
        private void GetTextPosts()
        {
            Text_Post aText;
            ArrayList ArrayTexts;


            ArrayTexts = bl.GetPost(aPost);
            int c = ArrayTexts.Count;

            for (int j = 0; j < c; ++j)
            {
                aText = (Text_Post)ArrayTexts[j];



                concatinater.Append(aText.PostText);
            }

        }

        private void GetEventPosts()
        {
            Event_Post aEvent;
            ArrayList ArrayEvents;

            ArrayEvents = bl.GetPost(aPost);
            int c = ArrayEvents.Count;

            for (int j = 0; j < c; ++j)
            {
                aEvent = (Event_Post)ArrayEvents[j];



                concatinater.Append("<table class='eventWrapper' style=' width:100%;'> <tbody> <tr ><td style='font-weight: bold; width:30%;'>"); //Opening table
                concatinater.Append("Name: ");
                concatinater.Append("</td><td style=''>");
                concatinater.Append(aEvent.EventName);

                concatinater.Append("</td></tr> <tr><td style='font-weight: bold;'>");
                concatinater.Append("Type: ");
                concatinater.Append("</td><td>");
                concatinater.Append(aEvent.EventType);

                concatinater.Append("</td></tr> <tr><td style='font-weight: bold;'>");
                concatinater.Append("Venue: ");
                concatinater.Append("</td><td>");
                concatinater.Append(aEvent.EventVenue);

                concatinater.Append("</td></tr> <tr><td style='font-weight: bold;'>");
                concatinater.Append("Host: ");
                concatinater.Append("</td><td>");
                concatinater.Append(aEvent.Host);
                
                concatinater.Append("</td></tr> <tr><td style='font-weight: bold;'>");
                concatinater.Append("Start Date: ");
                concatinater.Append("</td><td>");
                concatinater.Append(reusable_Methods.FormatDateTime(aEvent.StartDate));

                concatinater.Append("</td></tr> <tr><td style='font-weight: bold;'>");
                concatinater.Append("End Date: ");
                concatinater.Append("</td><td>");
                concatinater.Append(reusable_Methods.FormatDateTime(aEvent.EndDate));

                concatinater.Append("</td></tr></tbody></table>");//Closing table



                concatinater.Append("<div class='eventDetailsWrapper'> <div ><a title='click to view details'  class='btnViewDetails pointer'>View details</a></div><p class='eventDetails'>");
                concatinater.Append(aEvent.EventDetails);
                concatinater.Append("</p></div>");
                
            }

        }

        private void GetPhotoPosts()
        {
            Photo_Post aPhoto;
            ArrayList ArrayPhoto;

            ArrayPhoto = bl.GetPost(aPost);
            int c = ArrayPhoto.Count;

            for (int j = 0; j < c; ++j)
            {
                aPhoto = (Photo_Post)ArrayPhoto[j];




                String img = "DispayImage.ashx?id=" + aPhoto.PostId;

                concatinater.Append("<a class='pointer pictureLaunch' ><img src='");
                concatinater.Append(img);

                concatinater.Append("'alt='Profile picture'  class='BorberRad5 floatLeft photoPostImage thumbnails'  style=' padding: 2px; border: 1px solid #A3ADB5;'/></a> <div class='clr' ></div> <p class='caption'> ");


                concatinater.Append(aPhoto.PhotoCaption);
                concatinater.Append("</p>");

            }

        }

        private void GetArticlePosts()
        {
            Article_Post aArticle;
            ArrayList ArrayArticle;

            ArrayArticle = bl.GetPost(aPost);
            int c = ArrayArticle.Count;

            for (int j = 0; j < c; ++j)
            {
                aArticle = (Article_Post)ArrayArticle[j];


                concatinater.Append(aArticle.PostId + "<br/>");
                concatinater.Append(aArticle.Title + "<br/>");
                concatinater.Append(aArticle.ArticleText + "<br/>");
            }

        }

        private void GetVideoPosts()
        {
            Video_Post aVideo;
            ArrayList ArrayVideo;

            ArrayVideo = bl.GetPost(aPost);
            int c = ArrayVideo.Count;

            for (int j = 0; j < c; ++j)
            {
                aVideo = (Video_Post)ArrayVideo[j];


             
                concatinater.Append(aVideo.VideoCaption + "<br/>");
                concatinater.Append("<a href='#' class='videoLaunch'>");
                concatinater.Append(aVideo.VideoName);
                concatinater.Append("</a>");

     

            }

        }

        private void GetFilePosts()
        {
            File_Post aFile;
            ArrayList ArrayFile;

            ArrayFile = bl.GetPost(aPost);
            int c = ArrayFile.Count;

            for (int j = 0; j < c; ++j)
            {
                aFile = (File_Post)ArrayFile[j];

                concatinater.Append(aFile.FileCaption + "<br/><br/>");
                concatinater.Append("File size: ");
                concatinater.Append(reusable_Methods.FormatFileSize(aFile.FileSize) + "<br/>");
                concatinater.Append("Download file <a class='downloadFile' ");
                concatinater.Append(" ><u>");
                concatinater.Append(aFile.FileName);
                concatinater.Append("</u></a>");

            }

        }
        #endregion

        #region WRAPPERS
        private void OpenWrapper(int i)
        {
            #region OPENING WRAPPER
            aPost = (Post)ArrayPosts[i];

            ArrayMembers = bl.GetMemberDisplayName(aPost.MemberId);
            countMembers = ArrayMembers.Count;

            for (int a = 0; a < countMembers; ++a)
            {
                aMember = (Member)ArrayMembers[a];

                NotificationDAL notificationDAL = new NotificationDAL();
                Group group = new Group(aPost.GroupId);

                string groupDescription = notificationDAL.GetGroupDescription2(group);
                if (aPost.PostType != "Article")
                {

                    concatinater.Append("<div class='aPost ");

                    switch (aPost.PostType)
                    {
                        case "Text":
                            concatinater.Append("typeOfText");
                            ClientUpdateType = "Text";
                            break;
                        case "Event":
                            concatinater.Append("typeOfEvent");
                            ClientUpdateType = "Event";
                            break;
                        case "Photo":
                            concatinater.Append("typeOfPhoto");
                            ClientUpdateType = "Photo";
                            break;
                        case "Article":
                            concatinater.Append("typeOfArticle");
                            ClientUpdateType = "Article";
                            break;
                        case "Video":
                            concatinater.Append("typeOfVideo");
                            ClientUpdateType = "Video";
                            break;
                        case "File":
                            concatinater.Append("typeOfFile");
                            ClientUpdateType = "File";
                            break;
                        default:

                            break;


                    }


                    concatinater.Append("' id='");
                    concatinater.Append(aPost.PostId);
                    concatinater.Append("'><div class='msgeHeading ui-corner-all' ><h2 class='ui-corner-all'> Posted by: <a href='#' id='");
                    concatinater.Append(aPost.MemberId);
                    concatinater.Append("' class='btnMemberProfile' style='font-weight:normal; '>");
                    concatinater.Append(aMember.DisplayName);

                    bool isForMember = false;

                    string date = "";



                    date = reusable_Methods.FormatDateTime(aPost.CreateDate);



                    try
                    {
                        tempMemberID = Context.Session["memberID"].ToString();

                    }
                    catch
                    {
                        tempMemberID = ClientUpdateMemberID;

                    }

                    if (aPost.MemberId == tempMemberID)
                    { isForMember = true; }

                    if (GroupID == aPost.GroupId)
                    {


                    }
                    else
                    {
                        concatinater.Append("</a> <span class='space'></span> in group: <a id='");
                        ClientUpdateGroupID = aPost.GroupId;
                        concatinater.Append(aPost.GroupId);

                        concatinater.Append("' class='btnGoToGroupPage groupDesc'>");
                        concatinater.Append(groupDescription);

                    }

                    concatinater.Append("</a>");
                    if (isForMember)
                    {
                        concatinater.Append("<span class='ui-icon ui-icon-closethick floatright deletePost pointer ' title ='Delete this post' style='display:none;'></span>");
                    }

                    concatinater.Append("<span class='DateWithTime ui-corner-all floatright'>");
                    concatinater.Append(date);
                    //concatinater.Append(Convert.ToDateTime(aPost.CreateDate).ToString("D"));
                    concatinater.Append("</span></h2></div> <div class='postContentArea'> <div> ");


                }
                else
                {
                    string sessionMemberID = "";
                    ChatDAL chatDAL = new ChatDAL();
                    try
                    {
                        sessionMemberID = Context.Session["memberID"].ToString();

                    }
                    catch
                    {
                        sessionMemberID = ClientUpdateMemberID;

                    }

                    Member thisMember = new Member(sessionMemberID);


                    Post post = new Post(aPost.PostId);
                    Article_Post thisArticle = chatDAL.getChatText(ref post);

                    //BulabulaApp.Other_Classes. date new = reusable_Methods.FormatDateTime(aPost.CreateDate);
                    Reusable_Methods reusable_Methods = new Reusable_Methods();
                    string date = reusable_Methods.FormatDateTimeForChat(post.CreateDate);
                    thisMember = chatDAL.GetMemberDisplayName(thisMember);

                    ChatText.Append(" <div id='");
                    ChatText.Append(aPost.PostId);
                    ChatText.Append("' class='chatMemberWrapper'>");
                    ChatText.Append("<div class='memberNameSection  '> <a id=");
                    ChatText.Append(thisMember.MemberId);
                    ChatText.Append(" class='btnMemberProfile pointer' ><em>");
                    ChatText.Append(thisMember.DisplayName);
                    ChatText.Append("</em></a></div>");
                    ChatText.Append("</a></div>");
                    ChatText.Append("<div class='chatContentSection '>");
                    ChatText.Append(thisArticle.ArticleText);



                    ChatText.Append("</div> <div class='clr'></div><span class='time'>");
                    ChatText.Append(date);
                    ChatText.Append(" <span> </div>");
                    string chatString = ChatText.ToString();
                }
            }
            #endregion
        }

        private void CloseWrapper()
        {
            #region CLOSING WRAPPER

            int numberofPostLikes = bl.CountPostLikes(aPost);
            int numberofPostTags = bl.CountPostTags(aPost);

            //ContentArea Closing AND Wrapper oppening+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            if (aPost.PostType != "Article")
            {
                concatinater.Append("<div class='clr' ></div> </div> </div><div class='commentWrapper'> ");

                //buttons  Right
                concatinater.Append("<div class='btnHomePost width50% floatright '><a class='button left big floatLeft homeLikeBtn");

                RateAndTagDAL dal = new RateAndTagDAL();



                try
                {
                    tempMemberID = Context.Session["memberID"].ToString();

                }
                catch
                {
                    tempMemberID = ClientUpdateMemberID;

                }

                aMember = new Member(tempMemberID);

                //Check if member not liked
                if (dal.PostRatingExists(aMember, aPost) == 0)
                {
                    //if member not liked 
                    //concatinater.Append(" homeLikeBtn");
                    concatinater.Append("' title='");
                    concatinater.Append("Like this post");
                    concatinater.Append("'  >");
                    concatinater.Append("Like");
                }
                else
                {
                    //if member is liked
                    concatinater.Append("' title='");
                    concatinater.Append("Unlike this post");
                    concatinater.Append("'  >");
                    concatinater.Append("Unlike");
                }

                //Post button
                concatinater.Append("</a><a class='button middle big floatLeft homeCommentBtn' title='Comment on this post' >Comment</a>");
                //report Button
                concatinater.Append("<a class='button middle big floatLeft reportPost' title='Report this post' >Report</a>");
                //tag button
                concatinater.Append("<a class='button right big floatLeft homeTagBtn open tag'  title='Tag a friend'  >Tag </a>");

                concatinater.Append("</div>");

                List<Member> AllLikedMembers = dal.GetPeopleLikedPost(aPost);
                //Details Left
                concatinater.Append("<div class='postinfo width50% floatLeft tip'> <a title='");

                foreach (var Member in AllLikedMembers)
                {

                    concatinater.Append(Member.DisplayName);
                    concatinater.Append("<br />");
                }

                concatinater.Append("' href='#'>Likes</a>: <span class='likesCount'> " + numberofPostLikes);
                concatinater.Append("</span>, <a href='#'>Tags</a>: <span class='tagsCount'>" + numberofPostTags);

                concatinater.Append("</span>,   <a  class='btnViewComments' title='View all comments'>Comments</a>: <span class='commentCount'>" + bl.CountComments(aPost) + "</span> </div>");


                concatinater.Append("</div>" // Wrapper End

                                      //Buiding the  Comments section
                                     + "<div class='allComments'><h4>All Comments</h4></div>"
                    //Buiding the  Comments section
                                     + "<div class='commentSection' style='display:none; padding: 7px;'><div>"
                                     + "<textarea title='Write a comment...'  placeholder='Write a comment...' class=' commentBox floatLeft input BorberRad3'"
                                     + " style='width: 100%; height: 30px; font-size: 0.85em; margin: 5px 0pt 7px;' rows='2' cols='20'></textarea>"
                                     + " <a class='button big floatLeft btnSendComment floatright '  style='font-size: 0.9em; margin-right: -10px; '  >Comment</a></div></div></div>");

            #endregion
            }
        #endregion
        }
    }
}