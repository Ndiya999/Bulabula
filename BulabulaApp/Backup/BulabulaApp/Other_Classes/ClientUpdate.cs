using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Collections;
using SignalR.Hubs;
using System.Web;
using System.Data;
using System.Web.UI;


namespace BulabulaApp
{
    [HubName("moveShape")]
    public class ClientUpdate : Hub
    {
        int GroupID;
        Post aPost = new Post();
        ArrayList ArrayPosts;
        ArrayList ArrayMembers;
        int countMembers;
        string newsfeed = null;
        int? latestPostID = null;
        Member aMember = new Member();
        LoginDAL loginDAL = new LoginDAL();
        NewsfeedDAL bl = new NewsfeedDAL();
        MessagesDAL messageDAL = new MessagesDAL();
        string[] postReturn = new string[2];
        public string FriendsOnlineString = null;
        public string NotificationsString = null;
        RateAndTagDAL tagDAL = new RateAndTagDAL();
        StringBuilder concatinater = new StringBuilder();
        Reusable_Methods reusable_Methods = new Reusable_Methods();
       NotificationDAL notificationDAL = new NotificationDAL();
        NewsfeedDAL newsFeedDAL = new NewsfeedDAL();
       

        string memberID = null, date = null;
        int i = 0;

        PostDAL postDAL = new PostDAL();

     public void getCaption(string PostToClients, int groupID)
        {


            WebForm5 groups = new WebForm5();



            Clients.updatePictures(Context.ConnectionId, PostToClients, groupID);



        }

     public void geCaption(string PostToClients, int groupID)
        {


            WebForm5 groups = new WebForm5();

            //while (i == 0)
            // {


            Clients.shapeMoved(Context.ConnectionId, PostToClients, groupID);


            //   }

        }

     public void GetLatestPost(string thisMemberID)
        {


            WebForm5 groups = new WebForm5();


            BulabulaApp.WebServices.postsWebservice posts = new WebServices.postsWebservice();
            posts.ClientUpdateMemberID = thisMemberID;
            string PostToClients = posts.ClientUpdateSignalR();
            string  groupID = posts.ClientUpdateGroupID.ToString();
            string postType = posts.ClientUpdateType;

            Clients.InsertLatestPost(Context.ConnectionId, PostToClients, groupID, postType);


         

        }

     public void AddEventPost(string sessionMemberID, string startDate, string endDate, string name, string host, string venue, int groupID, string details, string type)
        {
            
            memberID = sessionMemberID;
            StringBuilder htmlText = new StringBuilder();
            Member aMember = new Member(memberID);
       
            Group aGroup = new Group(groupID);

            PostDAL postDAL = new PostDAL();
         
            Reusable_Methods reusable_Methods = new Reusable_Methods();

         /*
          * NEEDS ATTENTION !!!!!!!!!
          * 
          * 
          */

            DateTime startDateD = reusable_Methods.CreateDateTime(startDate);
            DateTime endDateD = reusable_Methods.CreateDateTime(endDate);


          /*
            * END NEEDS ATTENTION !!!!!!!!!
            */
        
            //DateTime startDateD = reusable_Methods.FormatDateFromDateTimePicker(startDate);
          //  DateTime endDateD = reusable_Methods.FormatDateFromDateTimePicker(endDate);

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
          
            #endregion

         string postToClients = concatinater.ToString();

        //Updating all Clients
            Clients.insertEvent(Context.ConnectionId, postToClients, groupID);

           

        }

     public void MoveShape(string postText, string sessionMemberID, int groupID)
        {
            
            memberID = sessionMemberID;
            StringBuilder htmlText = new StringBuilder();
            Member aMember = new Member(memberID);
            htmlText.Append(postText);
           
            //htmlText.Replace("cat", "dog",); 
            //htmlText.S
            //string[] parts = postText.Split('');
            Text_Post aText_Post = new Text_Post(postText);
            Group aGroup = new Group();
            aGroup.GroupId = groupID;



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
            string postToClients = concatinater.ToString();




            #endregion



            //Updating all Clients
            Clients.shapeMoved(Context.ConnectionId, postToClients, groupID);

        }

     public void DeleteAPost(int postID)
     {

         NewsfeedDAL newsfeedDAL = new NewsfeedDAL();
         Post aPost = new Post(postID);
         newsfeedDAL.FlagPost(aPost);


         //Updating all Clients
         Clients.removeDeletedPost(Context.ConnectionId, postID);
     }

     public void DeleteAComment(string sessionMemberID, int postID, int commentID)
     {

         NewsfeedDAL dal = new NewsfeedDAL();
         Comments aComment = new Comments(commentID);
         dal.FlagComment(aComment);
         Post aPost = new Post(postID);
       string commentCount =  messageDAL.CountComments(aPost).ToString();


           //Updating all Clients
       Clients.removeDeletedComment(Context.ConnectionId, commentCount, commentID, postID);
         

     }

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

     
     #region like a  post


     public void LikeAPost( string sessionMemberID, int postID)
     {

         NotificationDAL notificationDAL = new NotificationDAL();

         RateAndTagDAL dal = new RateAndTagDAL();

         Post aPost = new Post(postID);

         List<Member> MemberList = new List<Member>();
         MemberList = notificationDAL.GetPostOwner(aPost);
         Member aFriend = new Member(MemberList[0].MemberId);

         string memberId = sessionMemberID;
         Member aMember = new Member(memberId);

         //Check if like exists
         if (dal.PostRatingExists(aMember, aPost) == 0)
         {
             dal.RatePost(aMember, aPost);


             List<Rating> RatingList = new List<Rating>();
             RatingList = notificationDAL.GetPostRatingID(aPost, aMember);

             Rating aRating = new Rating(RatingList[0].RatingId);

             if (RatingList.Count > 0)
             {
                 notificationDAL.InsertPostRatingNotification(aRating, aMember, aFriend, aPost);
             }
         }
         else
         {
             //MessageBox.Show("already rated by u");
         }


         aPost.PostId = postID;

         int numberofPostLikes = newsFeedDAL.CountPostLikes(aPost);
         //Updating all Clients
         Clients.updatePostLikeCount(numberofPostLikes, postID);
        
     }
     #endregion 

     #region Unlike a  post


     public void UnlikeAPost(string sessionMemberID, int postID)
     {

         RateAndTagDAL dal = new RateAndTagDAL();

        

         string memberId = sessionMemberID;
         Member aMember = new Member(memberId);
         aPost.PostId = postID;
         dal.DeletePostLike(aMember, aPost);

        

         int numberofPostLikes = newsFeedDAL.CountPostLikes(aPost);
     

     
         //Updating all Clients
         Clients.updatePostLikeCount(numberofPostLikes, postID);

     }
     #endregion 

     #region Tag a  post


     public void TagAPost(string sessionMemberID, int postID, string friendID)
     {
         NotificationDAL notificationDAL = new NotificationDAL();

         //Tag method here 
         Member m;
         Post p;

         bool isTagged = tagDAL.TagFriend(p = new Post(postID), m = new Member(friendID));


         // INSERT NOTIFICATION
         string friendId = friendID;
         Member aFriend = new Member(friendId);

         string memberId = sessionMemberID;
         Member aMember = new Member(memberId);

         aPost.PostId = postID;

         notificationDAL.InsertTagNotification(aMember, aFriend, aPost);
         //return number of tags
         int numberoftags = newsFeedDAL.CountPostTags(aPost);
         Clients.updateTagCount(numberoftags, postID);
     }
     #endregion

     #region UnTagg a  post


     public void UntagAPost(string sessionMemberID, int postID, string friendID)
     {
         Member m;
         Post p;

         bool isTagged = tagDAL.DeleteTag(p = new Post(postID), m = new Member(friendID));


         aPost.PostId = postID;
         //returns number of tags

         int numberoftags = newsFeedDAL.CountPostTags(aPost);
         //Updating all Clients
         Clients.updateTagCount(numberoftags, postID);
       
     }
     #endregion 

     #region  Comment of a post Comments 


     public void AddComment(string sessionMemberID, string postID, string commentTxt)
        {
            Comments comment = new Comments();
            comment.MemberId = sessionMemberID;
            comment.CommentText = commentTxt;
            Post post = new Post();
            post.PostId = int.Parse(postID);
            comment.PostId = post.PostId;
            messageDAL.InsertComment(comment);
            StringBuilder commentBuilder = new StringBuilder();


            //###################################
            //refreshing Comments
            //###################################
            DataTable comments = messageDAL.GetAllComments(post);

       

            foreach (DataRow row in comments.Rows)
            {
                date = Convert.ToDateTime(row["CreateDate"]).ToString("M");

                //Building single message view box
                commentBuilder.Append("<div id='");
                commentBuilder.Append((row["CommentID"]).ToString());

                commentBuilder.Append("' class='aComment'><div class='deleteComment floatright'>");





                if ((row["MemberID"]).ToString() == sessionMemberID)
                {

                    commentBuilder.Append("<a class='btnDleteComment ui-icon ui-icon-close tip' title='Delete this comment'></a>");
                }
                else
                {
                    commentBuilder.Append("<a class='btnOpenMenuComment ui-icon ui-icon-circle-triangle-s tip' title='More options..'></a>");
                }

                commentBuilder.Append("</div><h5><a style='font-weight:normal;' ");
                commentBuilder.Append("id='" + (row["MemberID"]).ToString() + "'> ");



                commentBuilder.Append(row["Friend"].ToString());
                commentBuilder.Append("</a><span class='DateWithTime ui-corner-all floatright'>");

                Reusable_Methods reusable_Methods = new Reusable_Methods();
                date = reusable_Methods.FormatDateTime(Convert.ToDateTime(row["CreateDate"]));

                commentBuilder.Append(date);
                commentBuilder.Append("</span></h5><p>");
                commentBuilder.Append((row["CommentText"]).ToString());
                commentBuilder.Append("</p></div>");
                //End Building 


            }

         
         string commentCount = messageDAL.CountComments(post).ToString();
         string commentText = commentBuilder.ToString();

            //Updating all Clients
         Clients.updatingComments(Context.ConnectionId, commentCount, commentText, postID);
        

        
        
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

                concatinater.Append("<div class='aPost typeOfText' id='");
                concatinater.Append(aPost.PostId);
                concatinater.Append("'><div class='msgeHeading ui-corner-all' ><h2 class='ui-corner-all'> Posted by: <a href='#' id='");
                concatinater.Append(aPost.MemberId);
                concatinater.Append("' class='btnMemberProfile' style='font-weight:normal; '>");
                concatinater.Append(aMember.DisplayName);

                bool isForMember = false;

                string date = "";



                date = reusable_Methods.FormatDateTime(aPost.CreateDate);

                if (aPost.MemberId == memberID)
                { isForMember = true; }

                if (GroupID == aPost.GroupId)
                {


                }
                else
                {
                    concatinater.Append("</a> <span class='space'></span> in group: <a id='");
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

            #endregion
        }

        private void CloseWrapper()
        {
            #region CLOSING WRAPPER

            int numberofPostLikes = bl.CountPostLikes(aPost);
            int numberofPostTags = bl.CountPostTags(aPost);

            //ContentArea Closing AND Wrapper oppening+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            concatinater.Append("<div class='clr' ></div> </div> </div><div class='commentWrapper'> ");

            //buttons  Right
            concatinater.Append("<div class='btnHomePost width50% floatright '><a class='button left big floatLeft homeLikeBtn");

            RateAndTagDAL dal = new RateAndTagDAL();

            //aMember = new Member(memberId);

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

        ChatDAL chatDAL = new ChatDAL();

        #region CHATTING
        public void InsertChatText(string sessionMemberID, int groupID, string ChatText)
        {

            //Article_Post article_Post = new Article_Post(ChatText);
            Group group = new Group(groupID);
            Member thisMember = new Member(sessionMemberID);


           int postID = chatDAL.InsertChatText(group, ChatText, thisMember);
           Post post = new Post(postID);
           Article_Post thisArticle = chatDAL.getChatText(ref post);

           //BulabulaApp.Other_Classes. date new = reusable_Methods.FormatDateTime(aPost.CreateDate);
           Reusable_Methods reusable_Methods = new Reusable_Methods();
           string date = reusable_Methods.FormatDateTimeForChat(post.CreateDate);
           thisMember =  chatDAL.GetMemberDisplayName(thisMember);

               concatinater.Append(" <div class='chatMemberWrapper'>");
                concatinater.Append("<div class='memberNameSection  '> <a id=");
                concatinater.Append(thisMember.MemberId);
                concatinater.Append(" class='btnMemberProfile pointer' ><em>");
                concatinater.Append(thisMember.DisplayName);
                concatinater.Append("</em></a></div>");
                concatinater.Append("</a></div>");
               concatinater.Append("<div class='chatContentSection '>");
               concatinater.Append(thisArticle.ArticleText);

             

               concatinater.Append("</div> <div class='clr'></div>");
      
               concatinater.Append(" </div>");
               string chatString = concatinater.ToString();

               Clients.updateChat(Context.ConnectionId, chatString, groupID.ToString(), sessionMemberID);

        }
        #endregion

    }
}