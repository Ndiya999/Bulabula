using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace BulabulaApp
{
    class NotificationDAL
    {
        private string connectionString;

        public NotificationDAL()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        }

        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }
        public bool InsertPostRatingNotification(Rating rating, Member member, Member friend, Post post)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspInsertPostRatingNotification", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RatingID", rating.RatingId);
                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);
                cmd.Parameters.AddWithValue("@FriendID", friend.MemberId);
                cmd.Parameters.AddWithValue("@PostID", post.PostId);

                try
                {
                    con.Open();
                    int x = cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString());
                    return false;
                }
            }
        }   
        public List<Rating> GetPostRatingID(Post post, Member member)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetPostRatingID", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PostID", post.PostId);
                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);

                List<Rating> RatingList = new List<Rating>();

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Rating rating = new Rating(int.Parse(reader["RatingID"].ToString()));
                        RatingList.Add(rating);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return RatingList;
            }
        }
        public List<Notification> GetPostRatingNotificationsForAMember(Member member)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetPostRatingNotificationsForAMember", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);

                List<Notification> NotificationsList = new List<Notification>();

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                            Notification aNotification = new Notification(int.Parse(reader["NotificationID"].ToString()), reader["MemberID"].ToString(),int.Parse(reader["PostID"].ToString()), int.Parse(reader["RatingID"].ToString()), reader["NotificationType"].ToString());
                            NotificationsList.Add(aNotification);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return NotificationsList;
            }
        }
        public List<Member> GetMemberDisplayName(Member member)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetMemberDisplayName", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@MemberID", member.MemberId));

                List<Member> MemberList = new List<Member>();

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Member aMember = new Member(reader["DisplayName"].ToString(), 1);
                        MemberList.Add(aMember);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return MemberList;
            }
        }
        public List<Post> GetPostType(Post post)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetPostType", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@PostID", post.PostId));

                List<Post> PostList = new List<Post>();

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Post aMember = new Post(reader["PostType"].ToString());
                        PostList.Add(aMember);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return PostList;
            }
        }
        public ArrayList GetPostCaption(Post post)
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetPostCaption", dbConnection);
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.Add(new SqlParameter("@PostID", post.PostId));
                dbCommand.Parameters.Add(new SqlParameter("@PostType", post.PostType));

                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();

                if (post.PostType == "Event")
                {
                    while (dbReader.Read())
                    {
                        Event_Post aCountry = new Event_Post(dbReader["EventName"].ToString());
                        custArray.Add(aCountry);
                    }
                }
                else if (post.PostType == "Text")
                {
                    while (dbReader.Read())
                    {
                        Text_Post aCountry = new Text_Post(dbReader["PostText"].ToString());
                        custArray.Add(aCountry);
                    }
                }
                else if (post.PostType == "Photo")
                {
                    while (dbReader.Read())
                    {
                        Photo_Post aCountry = new Photo_Post(dbReader["PhotoCaption"].ToString());
                        custArray.Add(aCountry);
                    }
                }
                else if (post.PostType == "Article")
                {
                    while (dbReader.Read())
                    {
                        Article_Post aCountry = new Article_Post(dbReader["Title"].ToString());
                        custArray.Add(aCountry);
                    }
                }
                else if (post.PostType == "Video")
                {
                    while (dbReader.Read())
                    {
                        Video_Post aCountry = new Video_Post(dbReader["VideoCaption"].ToString());
                        custArray.Add(aCountry);
                    }
                }
                else if (post.PostType == "File")
                {
                    while (dbReader.Read())
                    {
                        File_Post aCountry = new File_Post(dbReader["FileCaption"].ToString());
                        custArray.Add(aCountry);
                    }
                }

                dbConnection.Close();
            }
            return custArray;
        }
        public List<Post> GetPostCreateDate(Post post)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetPostCreateDate", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@PostID", post.PostId));

                List<Post> PostList = new List<Post>();

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Post aMember = new Post(DateTime.Parse(reader["CreateDate"].ToString()));
                        PostList.Add(aMember);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return PostList;
            }
        }
        public bool InsertCommentRatingNotification(Rating rating, Member member, Member friend, Comments comments)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspInsertCommentRatingNotification", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RatingID", rating.RatingId);
                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);
                cmd.Parameters.AddWithValue("@FriendID", friend.MemberId);
                cmd.Parameters.AddWithValue("@CommentID", comments.Commentid);

                try
                {
                    con.Open();
                    int x = cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString());
                    return false;
                }
            }
        }
        public List<Rating> GetCommentRatingID(Comments comments, Member member)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetCommentRatingID", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CommentID", comments.Commentid);
                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);

                List<Rating> RatingList = new List<Rating>();

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Rating rating = new Rating(int.Parse(reader["RatingID"].ToString()));
                        RatingList.Add(rating);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return RatingList;
            }
        }
        public List<Notification> GetCommentRatingNotificationsForAMember(Member member)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetCommentRatingNotificationsForAMember", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);

                List<Notification> NotificationsList = new List<Notification>();

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        Notification aNotification = new Notification(int.Parse(reader["NotificationID"].ToString()), reader["MemberID"].ToString(), int.Parse(reader["RatingID"].ToString()), reader["NotificationType"].ToString(), int.Parse(reader["CommentID"].ToString()));
                        NotificationsList.Add(aNotification);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return NotificationsList;
            }
        }
        public List<Comments> GetCommentTextAndCreateDate(Comments comment)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetCommentTextAndCreateDate", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@CommentID", comment.Commentid));

                List<Comments> CommentsList = new List<Comments>();

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Comments aComment = new Comments(reader["CommentText"].ToString(), DateTime.Parse(reader["CreateDate"].ToString()), int.Parse(reader["PostID"].ToString()));
                        CommentsList.Add(aComment);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return CommentsList;
            }
        }
        public List<Member> GetPostOwner(Post post)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetPostOwner", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@PostID", post.PostId));

                List<Member> MemberList = new List<Member>();

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Member aMember = new Member(reader["MemberID"].ToString(), reader["DisplayName"].ToString(), 1);
                        MemberList.Add(aMember);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return MemberList;
            }
        }
        public bool InsertTagNotification(Member member, Member friend, Post post)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspInsertTagNotification", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);
                cmd.Parameters.AddWithValue("@FriendID", friend.MemberId);
                cmd.Parameters.AddWithValue("@PostID", post.PostId);

                try
                {
                    con.Open();
                    int x = cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString());
                    return false;
                }
            }
        }
        public List<Notification> GetTagNotificationsForAMember(Member member)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetTagNotificationsForAMember", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);

                List<Notification> NotificationsList = new List<Notification>();

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        Notification aNotification = new Notification(int.Parse(reader["NotificationID"].ToString()), reader["MemberID"].ToString(), int.Parse(reader["PostID"].ToString()), reader["NotificationType"].ToString());
                        NotificationsList.Add(aNotification);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return NotificationsList;
            }
        }
        public bool InsertFriendRequestSentNotification(Member member, Member friend)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspInsertFriendRequestSentNotification", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);
                cmd.Parameters.AddWithValue("@FriendID", friend.MemberId);

                try
                {
                    con.Open();
                    int x = cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString());
                    return false;
                }
            }
        }
        public List<Notification> GetFriendRequestSentNotificationsForAMember(Member member)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetFriendRequestSentNotificationsForAMember", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);

                List<Notification> NotificationsList = new List<Notification>();

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        Notification aNotification = new Notification(int.Parse(reader["NotificationID"].ToString()), reader["MemberID"].ToString(), reader["NotificationType"].ToString());
                        NotificationsList.Add(aNotification);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return NotificationsList;
            }
        }
        public bool InsertFriendRequestAcceptedNotification(Member member, Member friend)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspInsertFriendRequestAcceptedNotification", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);
                cmd.Parameters.AddWithValue("@FriendID", friend.MemberId);

                try
                {
                    con.Open();
                    int x = cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString());
                    return false;
                }
            }
        }
        public List<Notification> GetFriendRequestAcceptedNotificationsForAMember(Member member)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetFriendRequestAcceptedNotificationsForAMember", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);

                List<Notification> NotificationsList = new List<Notification>();

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        Notification aNotification = new Notification(int.Parse(reader["NotificationID"].ToString()), reader["FriendID"].ToString(), reader["NotificationType"].ToString(), 1);
                        NotificationsList.Add(aNotification);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return NotificationsList;
            }
        }
        public bool InsertCommentedOnPostNotification(Member member, Member friend, Post post)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspInsertCommentedOnPostNotification", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);
                cmd.Parameters.AddWithValue("@FriendID", friend.MemberId);
                cmd.Parameters.AddWithValue("@PostID", post.PostId);

                try
                {
                    con.Open();
                    int x = cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString());
                    return false;
                }
            }
        }
        public List<Notification> GetCommentedOnPostNotification(Member member)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetCommentedOnPostNotification", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);

                List<Notification> NotificationsList = new List<Notification>();

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        Notification aNotification = new Notification(int.Parse(reader["NotificationID"].ToString()), reader["MemberID"].ToString(), int.Parse(reader["PostID"].ToString()), reader["NotificationType"].ToString());
                        NotificationsList.Add(aNotification);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return NotificationsList;
            }
        }
        public List<Member> GetCommentOwner(Comments comments)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetCommentOwner", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@CommentID", comments.Commentid));

                List<Member> MemberList = new List<Member>();

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Member aMember = new Member(reader["MemberID"].ToString(), reader["DisplayName"].ToString(), 1);
                        MemberList.Add(aMember);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return MemberList;
            }
        }

        public List<Notification> GetEventNotificationsForGroupMembers()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetEventNotificationsForGroupMembers", con);
                cmd.CommandType = CommandType.StoredProcedure;

                List<Notification> EventList = new List<Notification>();

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        Notification aNotification = new Notification(int.Parse(reader["NotificationID"].ToString()), reader["MemberID"].ToString(), int.Parse(reader["GroupID"].ToString()), int.Parse(reader["PostID"].ToString()), reader["NotificationType"].ToString(), 1);
                        EventList.Add(aNotification);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return EventList;
            }
        }
        public bool InsertEventPostNotification(Member member, Post post, Group group)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspInsertEventPostNotification", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);
                cmd.Parameters.AddWithValue("@PostID", post.PostId);
                cmd.Parameters.AddWithValue("@GroupID", group.GroupId);

                try
                {
                    con.Open();
                    int x = cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString());
                    return false;
                }
            }
        }
        public List<Event_Post> GetEventName(Post post)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetEventName", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PostID", post.PostId);

                List<Event_Post> EventList = new List<Event_Post>();

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Event_Post aEvent = new Event_Post(reader["EventName"].ToString());
                        EventList.Add(aEvent);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return EventList;
            }
        }
        public List<Member> GetGroupMembers(Group group)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetGroupMembers", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@GroupID", group.GroupId));

                List<Member> MemberList = new List<Member>();

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Member aMember = new Member(reader["MemberID"].ToString(), reader["DisplayName"].ToString(), 1);
                        MemberList.Add(aMember);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return MemberList;
            }
        }
        public List<Group> GetGroupDescription(Group group)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetGroupDescription", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@GroupID", group.GroupId));

                List<Group> GroupList = new List<Group>();

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Group aGroup = new Group(reader["GroupDescription"].ToString(), 0);
                        GroupList.Add(aGroup);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return GroupList;
            }
        }

        public string GetGroupDescription2(Group group)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetGroupDescription", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@GroupID", group.GroupId));

                string GroupList = null;

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        GroupList = reader["GroupDescription"].ToString();
                       
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return GroupList;
            }
        }
        public List<Notification> GetMessageNotificationForAMember(Member member)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetMessageNotificationForAMember", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);

                List<Notification> NotificationsList = new List<Notification>();

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        Notification aNotification = new Notification(reader["NotificationType"].ToString(), reader["MemberID"].ToString(), int.Parse(reader["NotificationID"].ToString()), int.Parse(reader["MessageID"].ToString()));
                        NotificationsList.Add(aNotification);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return NotificationsList;
            }
        }
        public Message GetMessageDateTime(Message message)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetMessageDateTime", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@MessageID", message.MessageId));



                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        message.DateTime = DateTime.Parse(reader["DateTime"].ToString());

                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return message;
            }
        }
        public Post GetMostRecentPostDate(Member member)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetMostRecentPostDate", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@MemberID", member.MemberId));


                Post post = new Post();
                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        post.CreateDate = DateTime.Parse(reader["CreateDate"].ToString());

                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return post;
            }
        }
        public bool UpdateMessageNotificationIsRead(Message message)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspUpdateMessageNotificationIsRead", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MessageID", message.MessageId);

                try
                {
                    con.Open();
                    int x = cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString());
                    return false;
                }
            }
        }
    }
}
