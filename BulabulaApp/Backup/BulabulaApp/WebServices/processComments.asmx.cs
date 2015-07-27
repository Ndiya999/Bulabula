using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using System.Text;
using System.Data;

namespace BulabulaApp.WebServices
{
    /// <summary>
    /// Summary description for processComments
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    [System.ComponentModel.ToolboxItem(false)]
    public class processComments : System.Web.Services.WebService
    {
        MessagesDAL messageDAL = new MessagesDAL();
        NewsfeedDAL newsFeedDAL = new NewsfeedDAL();
        private Post aPost = new Post();
        RateAndTagDAL tagDAL = new RateAndTagDAL();
        Comments comment = new Comments();
        string date = "";


        #region Insert A Comment
        [WebMethod(EnableSession = true)]
        public string[] AddCommentnRefresh(int postID, string commentTxt, int lastCommentID)
        {
            NewsfeedDAL newsfeedDAL = new NewsfeedDAL();
            MessagesDAL messageDAL = new MessagesDAL();
            Comments comment = new Comments();
            StringBuilder commentBuilder = new StringBuilder();
            

            //Decrypting the member ID
            comment.MemberId = Context.Session["memberID"].ToString();

            comment.CommentText = commentTxt;
            comment.PostId = postID;
            comment.Commentid = lastCommentID;
            messageDAL.InsertComment(comment);

            //Insert notification
            NotificationDAL notificationDAL = new NotificationDAL();

            Post aPost = new Post(postID);

            List<Member> MemberList = new List<Member>();
            MemberList = notificationDAL.GetPostOwner(aPost);
            string friendId = MemberList[0].MemberId;
            Member aFriend = new Member(friendId);

            Member aMember = new Member(Context.Session["memberID"].ToString());

            if (aMember.MemberId != aFriend.MemberId)
            {
                notificationDAL.InsertCommentedOnPostNotification(aMember, aFriend, aPost);
            }

            //Refreshing the Comment count
            Post post = new Post();
            post.PostId = comment.PostId;


            //###################################
            //refreshing Comments
            //###################################

            DataTable comments = newsfeedDAL.GetRefreshComments(post, comment);

            foreach (DataRow row in comments.Rows)
            {
                date = Convert.ToDateTime(row["CreateDate"]).ToString("M");

                //Building single message view box
                commentBuilder.Append("<div id='");
                commentBuilder.Append((row["CommentID"]).ToString());

                commentBuilder.Append("' class='aComment'><div class='deleteComment floatright'>");





                if ((row["MemberID"]).ToString() == Session["memberID"].ToString())
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
                        //Array with the       //number of commeents         //new comments
          var comentArray =  new string[] { messageDAL.CountComments(post).ToString(), commentBuilder.ToString()};

            return comentArray;
      }

  
        [WebMethod(EnableSession = true)]
        public string AddComment(int postID, string commentTxt)
        {
            
  
            Comments comment = new Comments();
            comment.MemberId = Context.Session["memberID"].ToString();
            comment.CommentText = commentTxt;
            comment.PostId = postID;
            messageDAL.InsertComment(comment);

            //Insert notification
            NotificationDAL notificationDAL = new NotificationDAL();

            Post aPost = new Post(postID);

            List<Member> MemberList = new List<Member>();
            MemberList = notificationDAL.GetPostOwner(aPost);
            string friendId = MemberList[0].MemberId;
            Member aFriend = new Member(friendId);

            Member aMember = new Member(Context.Session["memberID"].ToString());

            if (aMember.MemberId != aFriend.MemberId)
            {
                notificationDAL.InsertCommentedOnPostNotification(aMember, aFriend, aPost);
            }
            //Refreshing the Comment count
            Post post = new Post();
            post.PostId = comment.PostId;


            return messageDAL.CountComments(post).ToString();
        }
        
        #endregion

        #region Insert the first Comment of a post Comments

        [WebMethod(EnableSession = true)]
        public string[] AddFirstComment(string postID, string commentTxt)
        {
            Comments comment = new Comments();
            comment.MemberId = Context.Session["memberID"].ToString();
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

           // DataTable comments = newsFeedDAL.GetRefreshComments(post, comment);

       
            foreach (DataRow row in comments.Rows)
            {
                date = Convert.ToDateTime(row["CreateDate"]).ToString("M");

                //Building single message view box
                commentBuilder.Append("<div id='");
                commentBuilder.Append((row["CommentID"]).ToString());

                commentBuilder.Append("' class='aComment'><div class='deleteComment floatright'>");





                if ((row["MemberID"]).ToString() == Session["memberID"].ToString())
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

            //Array with the       //number of commeents         //new comments
            var comentArray = new string[] { messageDAL.CountComments(post).ToString(), commentBuilder.ToString() };

            return comentArray;

        
        
        }

        #endregion

        #region get all Comments

        [WebMethod(EnableSession = true)]
        public string GetAllComments(int postID)
        {

            Post post = new Post();
            Comments comment = new Comments();
            MessagesDAL m = new MessagesDAL();
            string date = "";

            post.PostId = postID;
            StringBuilder comnt = new StringBuilder();
            DataTable comments = m.GetAllComments(post);

            foreach (DataRow row in comments.Rows)
            {

                Reusable_Methods reusable_Methods = new Reusable_Methods();

                date = reusable_Methods.FormatDateTime(Convert.ToDateTime(row["CreateDate"]));

                //Building single message view box
                comnt.Append("<div id='");
                comnt.Append((row["CommentID"]).ToString());
                comnt.Append("' class='aComment'><div class='deleteComment floatright'>");

                comment.Commentid = int.Parse(row["CommentID"].ToString());

                if ((row["MemberID"]).ToString() == Session["memberID"].ToString())
                {

                    comnt.Append("<a class='btnDleteComment ui-icon ui-icon-close tip floatLeft' title='Delete this comment' ></a>");
                }
                else
                {
                    //comnt.Append("<a class='btnOpenMenuComment ui-icon ui-icon-circle-triangle-s tip' title='More options..' ></a>");
                     comnt.Append("<a class='button left big floatLeft likeComment '");
                    
                      RateAndTagDAL dal = new RateAndTagDAL();

                            string memberId = Context.Session["memberID"].ToString();
                            Member amember = new Member(memberId);

                            //Check if member not liked
                            if (dal.CommentRatingExists(amember, comment) ==0)
                            {
                                //if member not liked 
                                //concatinater.Append(" homeLikeBtn");
                                comnt.Append("' title='");
                                comnt.Append("Like this comment");
                                comnt.Append("'  >");
                                comnt.Append("Like");
                            }
                            else
                            {
                                //if member is liked
                              //  concatinater.Append(" homeUnlikeBtn");
                                comnt.Append("' title='");
                                comnt.Append("Unlike this comment");
                                comnt.Append("'  >");
                                comnt.Append("Unlike");
                            }

                            comnt.Append("</a>");
                            
                     comnt.Append("<a class='button right big floatLeft reportComment' title='Report this comment'>Report </a>");
                }

                comnt.Append("</div><h5><a style='font-weight:normal;' ");
                comnt.Append("id='" + (row["MemberID"]).ToString() + "'> ");
                comnt.Append(row["Friend"].ToString());
                comnt.Append("</a><span class='DateWithTime ui-corner-all floatright'>");
                comnt.Append(date);
                comnt.Append("</span></h5><p>");
                comnt.Append((row["CommentText"]).ToString());
                comnt.Append("</p></div>");
                //End Building 

            }

            return comnt.ToString();
            //End Refresh



        }

        #endregion

        #region Deleting a Comment
        [WebMethod(EnableSession = true)]
        public string DeleteComment(int postID, int commentID)
        {

            Context.Session["memberID"].ToString();
            //like method here 

            NewsfeedDAL dal = new NewsfeedDAL();

            Comments aComment = new Comments(commentID);
            dal.FlagComment(aComment);

            Post aPost = new Post(postID);

            return messageDAL.CountComments(aPost).ToString();
            
        }
        #endregion

        #region like a  post

        [WebMethod(EnableSession = true)]
        public int LikeAPost(int postID)
        {

            NotificationDAL notificationDAL = new NotificationDAL();

            RateAndTagDAL dal = new RateAndTagDAL();

            Post aPost = new Post(postID);

            List<Member> MemberList = new List<Member>();
            MemberList = notificationDAL.GetPostOwner(aPost);
            Member aFriend = new Member(MemberList[0].MemberId);

            string memberId = Context.Session["memberID"].ToString();
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
            return numberofPostLikes;
        }
         #endregion 
         
        #region Unlike a  post

        [WebMethod(EnableSession = true)]
        public int UnlikeAPost(int postID)
        {


            //like method here 
            RateAndTagDAL dal = new RateAndTagDAL();

            Post aPost = new Post(postID);

            string memberId = Context.Session["memberID"].ToString();
            Member aMember = new Member(memberId);

            dal.DeletePostLike(aMember, aPost);

            aPost.PostId = postID;

            int numberofPostLikes = newsFeedDAL.CountPostLikes(aPost);
            return numberofPostLikes;
        }
        #endregion 
   
        #region Get All Taggs
        [WebMethod(EnableSession = true)]
        public string GetAllTags(int postID)
        {


         
            Post p;

            StringBuilder allTags = new StringBuilder();
            MessagesDAL member = new MessagesDAL();
            List<Member> members = tagDAL.GetTagedFriends(p = new Post(postID));

            List<Member> poster = tagDAL.GetPostMemberID(p = new Post(postID));

            foreach (var element in members)
            {
                string m = Session["memberID"].ToString();

                if (poster[0].MemberId == Session["memberID"].ToString()) 
                {
                    allTags.Append("<li ><a id='" + element.MemberId + "' class='btnMemberProfile pointer'>" + element.DisplayName + "</a><a class='button left big floatright btnUntag ' style='margin: 0;' >Untag friend</a></li>");
                }
                else if (element.MemberId == Session["memberID"].ToString())
                {
                    allTags.Append("<li ><a id='" + element.MemberId + "' class='btnMemberProfile pointer'>" + element.DisplayName + "</a><a class='button left big floatright btnUntag ' style='margin: 0;' >Untag yourself</a></li>");
                }
                else
                {
                    allTags.Append("<li ><a id='" + element.MemberId + "' class=' btnMemberProfile pointer'>" + element.DisplayName + "</a></li>");
                }


            }

            return allTags.ToString();

        }
      #endregion 

        #region Tag a  post

        [WebMethod(EnableSession = true)]
        public int TagAPost(int postID, string friendID)
        {
            NotificationDAL notificationDAL = new NotificationDAL();

            //Tag method here 
            Member m;
            Post p;

            bool isTagged = tagDAL.TagFriend(p = new Post(postID), m = new Member(friendID));


            // INSERT NOTIFICATION
            string friendId = friendID;
            Member aFriend = new Member(friendId);

            string memberId = Context.Session["memberID"].ToString();
            Member aMember = new Member(memberId);

            aPost.PostId = postID;

            notificationDAL.InsertTagNotification(aMember, aFriend, aPost);
            //return number of tags
            int numberoftags = newsFeedDAL.CountPostTags(aPost);
            return numberoftags;
        }
         #endregion 
         
        #region UnTagg a  post

        [WebMethod(EnableSession = true)]
        public int UntaggAPost(int postID, string friendID)
        {
             Member m;
            Post p;

            bool isTagged = tagDAL.DeleteTag(p = new Post(postID), m = new Member(friendID));


            aPost.PostId = postID;
            //returns number of tags

            int numberoftags = newsFeedDAL.CountPostTags(aPost);
            return numberoftags;
        }
        #endregion 

        #region Like a  comment
        [WebMethod(EnableSession = true)]
        public int LikeAComment(int commentID)
        {
            NotificationDAL notificationDAL = new NotificationDAL();
            
            //LIKE A COMMENT

            RateAndTagDAL dal = new RateAndTagDAL();

            Comments aComment = new Comments(commentID);

            List<Member> MemberList = new List<Member>();
            MemberList = notificationDAL.GetCommentOwner(aComment);
            Member aFriend = new Member(MemberList[0].MemberId);

            string memberId = Context.Session["memberID"].ToString();
            Member aMember = new Member(memberId);


            //checking if u have already rated the comment
            if (dal.CommentRatingExists(aMember, aComment) == 0)
            {
                //proceed and rate if u havnt commented
                dal.RateComment(aMember, aComment);

                List<Rating> RatingList = new List<Rating>();
                RatingList = notificationDAL.GetCommentRatingID(aComment, aMember);

                Rating aRating = new Rating(RatingList[0].RatingId);

                if (RatingList.Count > 0)
                {
                    notificationDAL.InsertCommentRatingNotification(aRating, aMember, aFriend, aComment);
                }
            }
            else
            {
                //you have already rated the comment
                //MessageBox.Show("already rated by u");
            }

            //like count
            int numberofCommentLikes = newsFeedDAL.CountCommentsLikes(aComment);
            return numberofCommentLikes;
        }
        #endregion

        #region Unlike a  comment
        [WebMethod(EnableSession = true)]
        public int UnlikeAComment(int commentID)
        {
            RateAndTagDAL dal = new RateAndTagDAL();

            Comments aComment = new Comments(commentID);

            string memberId = Context.Session["memberID"].ToString();
            Member aMember = new Member(memberId);

            dal.DeleteCommentLike(aMember, aComment);

            //like count
            int numberofCommentLikes = newsFeedDAL.CountCommentsLikes(aComment);
            return numberofCommentLikes;
        }
        #endregion

        #region Report 
        [WebMethod(EnableSession = true)]
        public bool SendReport(int itemID, string option, string emailAddress, string type)
        {

            ReportPostDAL dal = new ReportPostDAL();

            string memberId = Context.Session["memberID"].ToString();
            Member aMember = new Member(memberId);


            List<Member> AdministratorList = new List<Member>();

            AdministratorList = dal.GetAllActiveAdministrators();
            int countAllActiveAdministrators = dal.GetAllActiveAdministrators().Count;


            Random random = new Random();
            int randomNumber = random.Next(0, countAllActiveAdministrators);

            if (type == "post")
            {
                Post aPost = new Post(itemID);

                dal.ReportPost(aMember, aPost, option, AdministratorList[randomNumber].MemberId);
            }
            else if (type == "comment")
            {
                Comments aComment = new Comments(itemID);

                dal.ReportComment(aMember, aComment, option, AdministratorList[randomNumber].MemberId);
            }
            else if (type == "message")
            {

                Messages aMessage = new Messages(itemID);

                dal.ReportMessage(aMember, aMessage, option, AdministratorList[randomNumber].MemberId);
            }
            
            
            return true; //if report was successful
        }
        #endregion

        //13-09-2012
        #region Get Liked friends Post
        [WebMethod(EnableSession = true)]
        public int GetLikedfriendsPost(int postID)
        {
            RateAndTagDAL dal = new RateAndTagDAL();

            Comments aComment = new Comments(postID);

            string memberId = Context.Session["memberID"].ToString();
            Member aMember = new Member(memberId);

            dal.DeleteCommentLike(aMember, aComment);

            //like count
            int numberofCommentLikes = newsFeedDAL.CountCommentsLikes(aComment);
            return numberofCommentLikes;
        }
        #endregion

        #region Get Liked friends Comments
        [WebMethod(EnableSession = true)]
        public int GetLikedfriendsComment(int commentID)
        {
            RateAndTagDAL dal = new RateAndTagDAL();

            Comments aComment = new Comments(commentID);

            string memberId = Context.Session["memberID"].ToString();
            Member aMember = new Member(memberId);

            dal.DeleteCommentLike(aMember, aComment);

            //like count
            int numberofCommentLikes = newsFeedDAL.CountCommentsLikes(aComment);
            return numberofCommentLikes;
        }
        #endregion
        
    }
}
