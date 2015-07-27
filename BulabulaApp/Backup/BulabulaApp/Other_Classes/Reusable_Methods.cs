using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Globalization;

namespace BulabulaApp
{
    public class Reusable_Methods
    {

        Member aMember = new Member();

        public Reusable_Methods(Member aMember)
        {
            this.aMember = aMember;
        }
        public Reusable_Methods()
        {

        }
        public string FormatDateTime(DateTime date)
        {
            string dateToDisplay = "";

            var ts = new TimeSpan(DateTime.Now.Ticks - date.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            const int SECOND = 1; const int MINUTE = 60 * SECOND; const int HOUR = 60 * MINUTE; //const int DAY = 24 * HOUR; //const int MONTH = 30 * DAY;

            if (delta < 1 * MINUTE) { dateToDisplay = "Just now"; }
            else if (delta < 2 * MINUTE) { dateToDisplay = "A Minute ago"; }
            else if (delta < 45 * MINUTE) { dateToDisplay = ts.Minutes + " Minutes ago"; }
            else if (delta < 90 * MINUTE) { dateToDisplay = "About an hour ago"; }
            else if (delta < 24 * HOUR)
            {
                if (ts.Hours <= 2)
                {
                    dateToDisplay = "About 2 hours ago";
                }
                else
                {
                    //string[] dateArray = date.ToString("D").ToString().Split(',');
                    //dateToDisplay = dateArray[1] + ", " + dateArray[2] + " at " + date.ToString("HH:mm");

                    dateToDisplay = date.ToString("dd MMMM yyyy") + " at " + date.ToString("HH:mm");
                }
            }
            else
            {

                //string[] dateArray = date.ToString("D").ToString().Split(',');
                //dateToDisplay = dateArray[1] + ", " + dateArray[2] + " at " + date.ToString("HH:mm");

                dateToDisplay = date.ToString("dd MMMM yyyy") + " at " + date.ToString("HH:mm");
            }
            return dateToDisplay;
        }
        public string FormatDateTimeForChat(DateTime date)
        {
            string dateToDisplay = "";

            var ts = new TimeSpan(DateTime.Now.Ticks - date.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            const int SECOND = 1; const int MINUTE = 60 * SECOND; const int HOUR = 60 * MINUTE; //const int DAY = 24 * HOUR; //const int MONTH = 30 * DAY;

            if (delta < 1 * MINUTE) { dateToDisplay = "Just now"; }
            else if (delta < 2 * MINUTE) { dateToDisplay = "A Minute ago"; }
            else if (delta < 45 * MINUTE) { dateToDisplay = ts.Minutes + " Minutes ago"; }
            else if (delta < 90 * MINUTE) { dateToDisplay = "About an hour ago"; }
            else if (delta < 24 * HOUR)
            {
                if (ts.Hours <= 2)
                {
                    dateToDisplay = "About 2 hours ago";
                }
                else
                {
                    //string[] dateArray = date.ToString("D").ToString().Split(',');
                    //dateToDisplay = dateArray[1] + ", " + dateArray[2] + " at " + date.ToString("HH:mm");



                    dateToDisplay = date.ToString("ddd d MMM", CultureInfo.CreateSpecificCulture("en-US"));
                }
            }
            else
            {

                //string[] dateArray = date.ToString("D").ToString().Split(',');
                //dateToDisplay = dateArray[1] + ", " + dateArray[2] + " at " + date.ToString("HH:mm");

                //dateToDisplay = date.ToString("dd MMMM yyyy") + " at " + date.ToString("HH:mm");

                dateToDisplay = date.ToString("ddd d MMM", CultureInfo.CreateSpecificCulture("en-US"));
            }
            return dateToDisplay;
        }
        
        public DateTime CreateDateTime(string date)
        { 
       
         var dateArr = date.Split('/');
 
           int days = int.Parse(dateArr[0]);
           int months = int.Parse(dateArr[1]);

            string[] temp = dateArr[2].Split(' ');

            int year = int.Parse(temp[0]);

         string[] timeArr = temp[1].Split(':');

         int hrs = 0;
        int mins = int.Parse(timeArr[1]);


        DateTime dateTime;
    

        if (temp[2] == "AM") {
            hrs = int.Parse(timeArr[0]);
        }
        else {

            hrs = (int.Parse(timeArr[0]) + 3);
        }
        try
        {
            dateTime = new DateTime(year, months, days, hrs, mins, 00);
        }
        catch (ArgumentOutOfRangeException ex)
        {

            dateTime = new DateTime(days, months, year, hrs, mins, 00);

        }
        catch {
            dateTime = new DateTime(months, days, year, hrs, mins, 00);
        
        }

            return dateTime;
        
        }


        #region RIGHT COLUMN

        public string RightColumnNotifications()
        {

            StringBuilder notifications = new StringBuilder();

            notifications.Append("<li><a  href='#' >Messages (<span id='messagesNotifications'>");
            notifications.Append(CountMessageNotifications());
            notifications.Append("</span>)</a></li>");

            notifications.Append("<li><a  href='#' >Friend Requests (<span id='friendReqNotifications'>");
            notifications.Append(CountFriendRequestSentNotifications());
            notifications.Append("</span>)</a></li>");

            notifications.Append("<li><a  href='#' >Tags (<span id='tagNotifications'>");
            notifications.Append(CountTagNotifications());
            notifications.Append("</span>)</a></li>");

            notifications.Append("<li><a  href='#' >Events (<span id='eventNotifications'>");
            notifications.Append(CountEventNotifications());
            notifications.Append("</span>)</a></li>");

          
            return notifications.ToString();
        }
        public string RightColumnGetAllJoinedGroups()
        {
            GroupsDAL groupsDal = new GroupsDAL();

            List<Group> groupList = new List<Group>();

            groupList = groupsDal.GetAllMembersGroups(aMember);
            int groupCount = groupList.Count;
            StringBuilder groups = new StringBuilder();
            for (int i = 0; i < groupCount; ++i)
            {
                groups.Append("<li><a class = 'btnGoToGroupPage' id='");
                groups.Append(groupList[i].GroupId);
                groups.Append("' >");
                groups.Append(groupList[i].GroupDescription);
                groups.Append("</a></li>");

            }
            return groups.ToString();
        }
        public string RightColumnGet5JoinedGroups()
        {
            GroupsDAL groupsDal = new GroupsDAL();

            List<Group> groupList = new List<Group>();

            groupList = groupsDal.GetAllMembersGroups(aMember);
            int groupCount = groupList.Count;
            StringBuilder groups = new StringBuilder();
            if (groupCount > 5)
            {

                for (int i = 0; i < 5; ++i)
                {
                    groups.Append("<li><a class = 'btnGoToGroupPage' id='");
                    groups.Append(groupList[i].GroupId);
                    groups.Append("' >");
                    groups.Append(groupList[i].GroupDescription);
                    groups.Append("</a></li>");

                  
                }

                MasterPage master = new MasterPage();
                HiddenField h = (HiddenField)master.FindControl("StayExpanded"); //
                try
                {
                    if (h.Value != null)
                    {
                        if (h.Value == "expanded")
                        {
                            for (int i = 5; i < groupList.Count; ++i)
                            {
                                groups.Append("<li class='groupsToHihe'><a class = 'btnGoToGroupPage' id='");
                                groups.Append(groupList[i].GroupId);
                                groups.Append("' >");
                                groups.Append(groupList[i].GroupDescription);
                                groups.Append("</a></li>");

                            }
                        }
                    }
                    else
                    {



                        for (int i = 5; i < groupList.Count; ++i)
                        {
                            groups.Append("<li style='display: none;' class='groupsToHihe'><a class = 'btnGoToGroupPage' id='");
                            groups.Append(groupList[i].GroupId);
                            groups.Append("' >");
                            groups.Append(groupList[i].GroupDescription);
                            groups.Append("</a></li>");

                        }
                    }
                }
                catch {

                    for (int i = 5; i < groupList.Count; ++i)
                    {
                        groups.Append("<li style='display: none;' class='groupsToHihe'><a class = 'btnGoToGroupPage' id='");
                        groups.Append(groupList[i].GroupId);
                        groups.Append("' >");
                        groups.Append(groupList[i].GroupDescription);
                        groups.Append("</a></li>");

                    }
                
                
                
                }

            }
            else
            {

                for (int i = 0; i < groupCount; ++i)
                {
                    groups.Append("<li><a class = 'btnGoToGroupPage' id='");
                    groups.Append(groupList[i].GroupId);
                    groups.Append("' >");
                    groups.Append(groupList[i].GroupDescription);
                    groups.Append("</a></li>");

                    //richTextBox6.Text += groupList[i].GroupId + "\t";
                    //richTextBox6.Text += groupList[i].GroupDescription + "\n";
                }
            }
            return groups.ToString();
        }
        public string RightColumnGetFriendsOnline()
        {
            StringBuilder FriendsOnline = new StringBuilder();

            FriendshipDAL friendsOnlineDAL = new FriendshipDAL();

            List<Member> FriendsOnlineList = new List<Member>();

            FriendsOnlineList = friendsOnlineDAL.GetAllFriendsOnline(aMember);
            int countFriendsOnline = FriendsOnlineList.Count;

            //listBox3.DataSource = FriendsOnlineList;
            //listBox3.DisplayMember = "Displayname";
            //listBox3.ValueMember = "MemberID";

            if (countFriendsOnline == 0)
            {
                FriendsOnline.Append("<li><a ");
                FriendsOnline.Append(" >");
                FriendsOnline.Append("There are no friends online");
                FriendsOnline.Append("</a></li>");
            }
            else
            {
                for (int i = 0; i < countFriendsOnline; ++i)
                {
                    FriendsOnline.Append("<li><a class = 'btnMemberProfile' id='");
                    FriendsOnline.Append(FriendsOnlineList[i].MemberId);
                    FriendsOnline.Append("' >");
                    FriendsOnline.Append(FriendsOnlineList[i].DisplayName);
                    FriendsOnline.Append("</a></li>");

                }
            }

            return FriendsOnline.ToString();
        }
        #endregion

        #region NOTIFICATION TEXT

        public string PostRatingNotifications()
        {
            string notification = "";

            NewsfeedDAL newsFeedDAL = new NewsfeedDAL();

            NotificationDAL notificationDAL = new NotificationDAL();

            List<Notification> NotificationsList = new List<Notification>();
            NotificationsList = notificationDAL.GetPostRatingNotificationsForAMember(aMember);


            ArrayList ArrayPosts;
            for (int i = 0; i < NotificationsList.Count; ++i)
            {
                #region PostRating

                List<Member> MemberList = new List<Member>();
                Member aFriend = new Member(NotificationsList[i].MemberId);
                MemberList = notificationDAL.GetMemberDisplayName(aFriend);

                List<Post> PostList = new List<Post>();
                Post aPost = new Post(NotificationsList[i].PostId);
                PostList = notificationDAL.GetPostType(aPost);

                if (PostList[0].PostType == "Event")
                {
                    aPost = new Post(NotificationsList[i].PostId, PostList[0].PostType);
                    ArrayPosts = notificationDAL.GetPostCaption(aPost);

                    Event_Post aEvent_Post;
                    aEvent_Post = (Event_Post)ArrayPosts[i];

                    PostList = new List<Post>();
                    aPost = new Post(NotificationsList[i].PostId);

                    PostList = new List<Post>();
                    PostList = notificationDAL.GetPostCreateDate(aPost);

                    string postDisplayText = "";
                    if (aEvent_Post.EventName.Length > 30)
                    {
                        postDisplayText = aEvent_Post.EventName.Substring(0, 30) + "...";
                    }
                    else
                    {
                        postDisplayText = aEvent_Post.EventName;
                    }

                    notification += MemberList[0].DisplayName + " liked your event '" + postDisplayText + "' which you posted " + FormatDateTime(PostList[0].CreateDate) + "\n\n";
                }
                else if (PostList[0].PostType == "Text")
                {
                    aPost = new Post(NotificationsList[i].PostId, PostList[0].PostType);
                    ArrayPosts = notificationDAL.GetPostCaption(aPost);

                    Text_Post aText_Post;
                    aText_Post = (Text_Post)ArrayPosts[0];

                    PostList = new List<Post>();
                    aPost = new Post(NotificationsList[i].PostId);

                    PostList = new List<Post>();
                    PostList = notificationDAL.GetPostCreateDate(aPost);

                    string postDisplayText = "";
                    if (aText_Post.PostText.Length > 30)
                    {
                        postDisplayText = aText_Post.PostText.Substring(0, 30) + "...";
                    }
                    else
                    {
                        postDisplayText = aText_Post.PostText;
                    }

                    notification += MemberList[0].DisplayName + " liked your post '" + postDisplayText + "' which you posted " + FormatDateTime(PostList[0].CreateDate) + "\n\n";
                }
                else if (PostList[0].PostType == "Photo")
                {
                    aPost = new Post(NotificationsList[i].PostId, PostList[0].PostType);
                    ArrayPosts = notificationDAL.GetPostCaption(aPost);

                    Photo_Post aPhoto_Post;
                    aPhoto_Post = (Photo_Post)ArrayPosts[0];

                    PostList = new List<Post>();
                    aPost = new Post(NotificationsList[i].PostId);

                    PostList = new List<Post>();
                    PostList = notificationDAL.GetPostCreateDate(aPost);


                    string postDisplayText = "";
                    if (aPhoto_Post.PhotoCaption.Length > 30)
                    {
                        postDisplayText = aPhoto_Post.PhotoCaption.Substring(0, 30) + "...";
                    }
                    else
                    {
                        postDisplayText = aPhoto_Post.PhotoCaption;
                    }

                    notification += MemberList[0].DisplayName + " liked your photo '" + postDisplayText + "' which you posted " + FormatDateTime(PostList[0].CreateDate) + "\n\n";
                }
                else if (PostList[0].PostType == "Article")
                {
                    aPost = new Post(NotificationsList[i].PostId, PostList[0].PostType);
                    ArrayPosts = notificationDAL.GetPostCaption(aPost);

                    Article_Post aArticle_Post;
                    aArticle_Post = (Article_Post)ArrayPosts[0];

                    PostList = new List<Post>();
                    aPost = new Post(NotificationsList[i].PostId);

                    PostList = new List<Post>();
                    PostList = notificationDAL.GetPostCreateDate(aPost);

                    string postDisplayText = "";
                    if (aArticle_Post.Title.Length > 30)
                    {
                        postDisplayText = aArticle_Post.Title.Substring(0, 30) + "...";
                    }
                    else
                    {
                        postDisplayText = aArticle_Post.Title;
                    }

                    notification += MemberList[0].DisplayName + " liked your article '" + postDisplayText + "' which you posted " + FormatDateTime(PostList[0].CreateDate) + "\n\n";
                }
                else if (PostList[0].PostType == "Video")
                {
                    aPost = new Post(NotificationsList[i].PostId, PostList[0].PostType);
                    ArrayPosts = notificationDAL.GetPostCaption(aPost);

                    Video_Post aVideo_Post;
                    aVideo_Post = (Video_Post)ArrayPosts[0];

                    PostList = new List<Post>();
                    aPost = new Post(NotificationsList[i].PostId);

                    PostList = new List<Post>();
                    PostList = notificationDAL.GetPostCreateDate(aPost);


                    string postDisplayText = "";
                    if (aVideo_Post.VideoCaption.Length > 30)
                    {
                        postDisplayText = aVideo_Post.VideoCaption.Substring(0, 30) + "...";
                    }
                    else
                    {
                        postDisplayText = aVideo_Post.VideoCaption;
                    }

                    notification += MemberList[0].DisplayName + " liked your video '" + postDisplayText + "' which you posted " + FormatDateTime(PostList[0].CreateDate) + "\n\n";
                }
                else if (PostList[0].PostType == "File")
                {
                    aPost = new Post(NotificationsList[i].PostId, PostList[0].PostType);
                    ArrayPosts = notificationDAL.GetPostCaption(aPost);

                    File_Post aFile_Post;
                    aFile_Post = (File_Post)ArrayPosts[0];

                    PostList = new List<Post>();
                    aPost = new Post(NotificationsList[i].PostId);

                    PostList = new List<Post>();
                    PostList = notificationDAL.GetPostCreateDate(aPost);


                    string postDisplayText = "";
                    if (aFile_Post.FileCaption.Length > 30)
                    {
                        postDisplayText = aFile_Post.FileCaption.Substring(0, 30) + "...";
                    }
                    else
                    {
                        postDisplayText = aFile_Post.FileCaption;
                    }

                    notification += MemberList[0].DisplayName + " liked your file post '" + postDisplayText + "' which you posted " + FormatDateTime(PostList[0].CreateDate) + "\n\n";
                }
                #endregion
            }
            return notification;

        }
        public string CommentRatingNotifications()
        {
            string notification = "";

            NewsfeedDAL newsFeedDAL = new NewsfeedDAL();

            NotificationDAL notificationDAL = new NotificationDAL();

            List<Notification> NotificationsList = new List<Notification>();
            NotificationsList = notificationDAL.GetCommentRatingNotificationsForAMember(aMember);


            for (int i = 0; i < NotificationsList.Count; ++i)
            {
                #region CommentRating
                //memberlist
                List<Member> MemberList = new List<Member>();
                Member aFriend = new Member(NotificationsList[i].MemberId);
                MemberList = notificationDAL.GetMemberDisplayName(aFriend);

                string FriendDisplayName = MemberList[0].DisplayName;

                List<Comments> CommentList = new List<Comments>();
                Comments aComment = new Comments(NotificationsList[i].CommentId);
                CommentList = notificationDAL.GetCommentTextAndCreateDate(aComment);

                //memberslist
                MemberList = new List<Member>();
                Post aPost = new Post(CommentList[0].PostId);
                MemberList = notificationDAL.GetPostOwner(aPost);

                string postDisplayText = "";
                if (CommentList[0].CommentText.Length > 30)
                {
                    postDisplayText = CommentList[0].CommentText.Substring(0, 30) + "...";
                }
                else
                {
                    postDisplayText = CommentList[0].CommentText;
                }

                notification += FriendDisplayName + " liked your comment '" + postDisplayText + "' which you posted " + FormatDateTime(CommentList[0].CreateDate) + " on " + MemberList[0].DisplayName + " post\n\n";
                #endregion
            }
            return notification;
        }
        public string TagNotifications()
        {
            string notification = "";

            NewsfeedDAL newsFeedDAL = new NewsfeedDAL();

            NotificationDAL notificationDAL = new NotificationDAL();

            List<Notification> NotificationsList = new List<Notification>();
            NotificationsList = notificationDAL.GetTagNotificationsForAMember(aMember);


            for (int i = 0; i < NotificationsList.Count; ++i)
            {
                #region TagRating
                //memberlist
                List<Member> MemberList = new List<Member>();
                Member aFriend = new Member(NotificationsList[i].MemberId);
                MemberList = notificationDAL.GetMemberDisplayName(aFriend);

                string FriendDisplayName = MemberList[0].DisplayName;

                //memberslist
                List<Member> PostOwnerMemberList = new List<Member>();
                Post aPost = new Post(NotificationsList[i].PostId);
                PostOwnerMemberList = notificationDAL.GetPostOwner(aPost);


                List<Post> PostList = new List<Post>();
                PostList = notificationDAL.GetPostCreateDate(aPost);

                string memberLoggedOn = aMember.MemberId;

                if (memberLoggedOn == PostOwnerMemberList[0].MemberId)
                {
                    notification += FriendDisplayName + " tagged you on your post which you posted " + FormatDateTime(PostList[0].CreateDate) + " \n\n";
                }
                else if (FriendDisplayName == PostOwnerMemberList[0].DisplayName)
                {
                    notification += "You were tagged on " + PostOwnerMemberList[0].DisplayName + "'s post by " + FriendDisplayName + " " + FormatDateTime(PostList[0].CreateDate) + "\n\n";
                }
                else
                {
                    notification += FriendDisplayName + " tagged you on " + PostOwnerMemberList[0].DisplayName + "'s post which was posted " + FormatDateTime(PostList[0].CreateDate) + " \n\n";
                }


                #endregion
            }
            return notification;
        }
        public string FriendRequestSentNotifications()
        {
            string notification = "";

            NewsfeedDAL newsFeedDAL = new NewsfeedDAL();

            NotificationDAL notificationDAL = new NotificationDAL();

            List<Notification> NotificationsList = new List<Notification>();
            NotificationsList = notificationDAL.GetFriendRequestSentNotificationsForAMember(aMember);


            for (int i = 0; i < NotificationsList.Count; ++i)
            {
                #region FriendRequestSent
                //memberlist
                List<Member> MemberList = new List<Member>();
                Member aFriend = new Member(NotificationsList[i].MemberId);
                MemberList = notificationDAL.GetMemberDisplayName(aFriend);

                string FriendDisplayName = MemberList[0].DisplayName;


                notification += FriendDisplayName + " sent you a friend request\n\n";
                #endregion
            }
            return notification;
        }
        public string FriendRequestAcceptedNotifications()
        {
            string notification = "";

            NewsfeedDAL newsFeedDAL = new NewsfeedDAL();

            NotificationDAL notificationDAL = new NotificationDAL();

            List<Notification> NotificationsList = new List<Notification>();
            NotificationsList = notificationDAL.GetFriendRequestAcceptedNotificationsForAMember(aMember);


            for (int i = 0; i < NotificationsList.Count; ++i)
            {
                #region FriendRequestAccepted
                //memberlist
                List<Member> MemberList = new List<Member>();
                Member aFriend = new Member(NotificationsList[i].MemberId);
                MemberList = notificationDAL.GetMemberDisplayName(aFriend);

                string FriendDisplayName = MemberList[0].DisplayName;


                notification += FriendDisplayName + " accepted your friend request\n\n";
                #endregion
            }
            return notification;
        }
        public string CommentNotifications()
        {
            string notification = "";

            NewsfeedDAL newsFeedDAL = new NewsfeedDAL();

            NotificationDAL notificationDAL = new NotificationDAL();

            List<Notification> NotificationsList = new List<Notification>();
            NotificationsList = notificationDAL.GetCommentedOnPostNotification(aMember);


            for (int i = 0; i < NotificationsList.Count; ++i)
            {
                #region Comment notification
                //memberlist
                List<Member> MemberList = new List<Member>();
                Member aFriend = new Member(NotificationsList[i].MemberId);
                MemberList = notificationDAL.GetMemberDisplayName(aFriend);

                string FriendDisplayName = MemberList[0].DisplayName;

                //memberslist
                MemberList = new List<Member>();
                Post aPost = new Post(NotificationsList[i].PostId);
                MemberList = notificationDAL.GetPostOwner(aPost);

                List<Post> PostList = new List<Post>();
                PostList = notificationDAL.GetPostCreateDate(aPost);

                List<Post> PostTypeList = new List<Post>();
                PostTypeList = notificationDAL.GetPostType(aPost);

                string postType = "";
                if (PostTypeList[0].PostType == "Event")
                {
                    postType = "event";
                }
                else if (PostTypeList[0].PostType == "Text")
                {
                    postType = "post";
                }
                else if (PostTypeList[0].PostType == "Photo")
                {
                    postType = "photo";
                }
                else if (PostTypeList[0].PostType == "Article")
                {
                    postType = "article";
                }
                else if (PostTypeList[0].PostType == "Video")
                {
                    postType = "video";
                }
                else if (PostTypeList[0].PostType == "File")
                {
                    postType = "file post";
                }
                notification += FriendDisplayName + " commented on your " + postType + " which you posted " + FormatDateTime(PostList[0].CreateDate) + " \n\n";
                #endregion
            }
            return notification;
        }
        public string EventNotifications()
        {
            string notification = "";

            NotificationDAL notificationDAL = new NotificationDAL();

            List<Notification> NotificationsList = new List<Notification>();
            NotificationsList = notificationDAL.GetEventNotificationsForGroupMembers();

            for (int i = 0; i < NotificationsList.Count; ++i)
            {
                int postId = NotificationsList[i].PostId;
                Post aPost = new Post(postId);

                List<Event_Post> EventNameList = new List<Event_Post>();
                EventNameList = notificationDAL.GetEventName(aPost);

                string eventDisplayText = "";
                if (EventNameList[0].EventName.Length > 30)
                {
                    eventDisplayText = EventNameList[0].EventName.Substring(0, 30) + "...";
                }
                else
                {
                    eventDisplayText = EventNameList[0].EventName;
                }

                List<Member> GroupMembersList = new List<Member>();

                Group aGroup = new Group(NotificationsList[i].GroupId);
                GroupMembersList = notificationDAL.GetGroupMembers(aGroup);

                for (int j = 0; j < GroupMembersList.Count; ++j)
                {
                    string friendId = aMember.MemberId; //PERSON WHO IS LOGGED IN

                    //CHECK TO SEE IF PE
                    if (friendId == GroupMembersList[j].MemberId)
                    {
                        List<Member> MemberList = new List<Member>();
                        Member aFriend = new Member(NotificationsList[i].MemberId);
                        MemberList = notificationDAL.GetMemberDisplayName(aFriend);

                        List<Group> GroupList = new List<Group>();
                        Group Group = new Group(NotificationsList[i].GroupId);
                        GroupList = notificationDAL.GetGroupDescription(Group);

                        notification += MemberList[0].DisplayName + " created the event '" + eventDisplayText + "' in the group '" + GroupList[0].GroupDescription + "'\n\n";
                    }
                }
            }
            return notification;
        }
        public string MessageNotifications()
        {
            string notification = "";

            NotificationDAL notificationDAL = new NotificationDAL();

            List<Notification> NotificationsList = new List<Notification>();
            NotificationsList = notificationDAL.GetMessageNotificationForAMember(aMember);

            for (int i = 0; i < NotificationsList.Count; ++i)
            {
                List<Member> MemberList = new List<Member>();
                Member aFriend = new Member(NotificationsList[i].MemberId);
                MemberList = notificationDAL.GetMemberDisplayName(aFriend);

                Message aMessage = new Message(NotificationsList[i].MessageId);


                aMessage = notificationDAL.GetMessageDateTime(aMessage);

                notification += MemberList[0].DisplayName + " sent you a message at " + FormatDateTime(aMessage.DateTime) + "\n\n";
            }
            return notification;
        }

        #endregion

        #region NOTIFICATIONS COUNT

        public int CountPostRatingNotifications()
        {

            NewsfeedDAL newsFeedDAL = new NewsfeedDAL();

            NotificationDAL notificationDAL = new NotificationDAL();

            List<Notification> NotificationsList = new List<Notification>();
            NotificationsList = notificationDAL.GetPostRatingNotificationsForAMember(aMember);


            return NotificationsList.Count;

        }
        public int CountCommentRatingNotifications()
        {

            NewsfeedDAL newsFeedDAL = new NewsfeedDAL();

            NotificationDAL notificationDAL = new NotificationDAL();

            List<Notification> NotificationsList = new List<Notification>();
            NotificationsList = notificationDAL.GetCommentRatingNotificationsForAMember(aMember);

            return NotificationsList.Count;
        }
        public int CountTagNotifications()
        {

            NewsfeedDAL newsFeedDAL = new NewsfeedDAL();

            NotificationDAL notificationDAL = new NotificationDAL();

            List<Notification> NotificationsList = new List<Notification>();
            NotificationsList = notificationDAL.GetTagNotificationsForAMember(aMember);

            return NotificationsList.Count;
        }
        public int CountFriendRequestSentNotifications()
        {

            NewsfeedDAL newsFeedDAL = new NewsfeedDAL();

            NotificationDAL notificationDAL = new NotificationDAL();

            List<Notification> NotificationsList = new List<Notification>();
            NotificationsList = notificationDAL.GetFriendRequestSentNotificationsForAMember(aMember);

            return NotificationsList.Count;
        }
        public int CountFriendRequestAcceptedNotifications()
        {

            NewsfeedDAL newsFeedDAL = new NewsfeedDAL();

            NotificationDAL notificationDAL = new NotificationDAL();

            List<Notification> NotificationsList = new List<Notification>();
            NotificationsList = notificationDAL.GetFriendRequestAcceptedNotificationsForAMember(aMember);

            return NotificationsList.Count;
        }
        public int CountCommentNotifications()
        {

            NewsfeedDAL newsFeedDAL = new NewsfeedDAL();

            NotificationDAL notificationDAL = new NotificationDAL();

            List<Notification> NotificationsList = new List<Notification>();
            NotificationsList = notificationDAL.GetCommentedOnPostNotification(aMember);

            return NotificationsList.Count;
        }
        public int CountEventNotifications()
        {

            NotificationDAL notificationDAL = new NotificationDAL();

            List<Notification> NotificationsList = new List<Notification>();
            NotificationsList = notificationDAL.GetEventNotificationsForGroupMembers();

            return NotificationsList.Count;
        }
        public int CountMessageNotifications()
        {

            NotificationDAL notificationDAL = new NotificationDAL();

            List<Notification> NotificationsList = new List<Notification>();
            NotificationsList = notificationDAL.GetMessageNotificationForAMember(aMember);

            return NotificationsList.Count;
        }
        #endregion

        #region FORMAT FILE SIZE

        public string FormatFileSize(long len)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            //double len = new FileInfo(filename).Length;
            int order = 0;
            while (len >= 1024 && order + 1 < sizes.Length)
            {
                order++;
                len = len / 1024;
            }

            // Adjust the format string to your preferences. For example "{0:0.#}{1}" would 
            // show a single decimal place, and no space. 
            return String.Format("{0:0.##} {1}", len, sizes[order]);

        }

        #endregion

        public DateTime FormatDateFromDateTimePicker(string date)
        {
            string[] dateData = date.Split(' ');


            string[] MonthNames = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames;
            //"Tue Sep 11 02:00:00 UTC+0200 2012"
            return DateTime.Parse(dateData[3] + "-" + (Array.IndexOf(MonthNames, dateData[1]) + 1) + "-" + dateData[2] + " " + dateData[4]);

        }
    }
}