using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Collections;
using System.Drawing;
using System.IO;

namespace BulabulaApp
{
    public class NewsfeedDAL
    {

        DataSet ds = new DataSet();
        private SqlDataAdapter da;
        private string connectionString;




        public NewsfeedDAL()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        }

        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }
        public ArrayList GetTop20NewsFeeds()
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetTop20NewsFeeds", dbConnection);
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
                    custArray.Add(aCountry);
                }
                dbConnection.Close();
            }
            return custArray;
        }
        public ArrayList GetPost(Post post)
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetPost", dbConnection);
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
                        Event_Post aCountry = new Event_Post(int.Parse(dbReader["PostID"].ToString()), dbReader["EventName"].ToString(), dbReader["EventDetails"].ToString(), dbReader["EventVenue"].ToString(), DateTime.Parse(dbReader["StartDate"].ToString()), DateTime.Parse(dbReader["EndDate"].ToString()), dbReader["Host"].ToString(), dbReader["EventType"].ToString());
                        custArray.Add(aCountry);
                    }
                }
                else if (post.PostType == "Text")
                {
                    while (dbReader.Read())
                    {
                        Text_Post aCountry = new Text_Post(int.Parse(dbReader["PostID"].ToString()), dbReader["PostText"].ToString());
                        custArray.Add(aCountry);
                    }
                }
                else if (post.PostType == "Photo")
                {
                    while (dbReader.Read())
                    {

                        Photo_Post aCountry = new Photo_Post(int.Parse(dbReader["PostID"].ToString()), dbReader["PhotoCaption"].ToString(), dbReader["PhotoName"].ToString());
                        custArray.Add(aCountry);
                    }
                }
                //else if (post.PostType == "Article")
                //{
                //    while (dbReader.Read())
                //    {
                //        Article_Post aCountry = new Article_Post(int.Parse(dbReader["PostID"].ToString()), dbReader["Title"].ToString(), dbReader["ArticleText"].ToString());
                //        custArray.Add(aCountry);
                //    }
                //}
                else if (post.PostType == "Video")
                {
                    while (dbReader.Read())
                    {
                        Video_Post aCountry = new Video_Post(int.Parse(dbReader["PostID"].ToString()), dbReader["VideoCaption"].ToString(), long.Parse(dbReader["VideoSize"].ToString()), dbReader["VideoName"].ToString());
                        custArray.Add(aCountry);
                    }
                }
                else if (post.PostType == "File")
                {
                    while (dbReader.Read())
                    {
                        File_Post aCountry = new File_Post(int.Parse(dbReader["PostID"].ToString()), dbReader["FileCaption"].ToString(), long.Parse(dbReader["FileSize"].ToString()), dbReader["FileName"].ToString());
                        custArray.Add(aCountry);
                    }
                }

                dbConnection.Close();
            }
            return custArray;
        }
        public int CountComments(Post post)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspCountComments", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PostID", post.PostId));


                SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
                returnValue.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(returnValue);

                con.Open();
                cmd.ExecuteNonQuery();
                int value = Int32.Parse(cmd.Parameters["@Return_Value"].Value.ToString());

                return value;

            }
        }
        public ArrayList GetTop2Comments(Post post)
        {
            ArrayList custArray;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspGetTop2Comments", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PostID", post.PostId));

                con.Open();
                SqlDataReader dbReader = cmd.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Comments aCountry = new Comments(int.Parse(dbReader["CommentID"].ToString()), dbReader["CommentText"].ToString(), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["PostID"].ToString()), dbReader["MemberID"].ToString());
                    custArray.Add(aCountry);
                }
                con.Close();
            }
            return custArray;
        }
        public ArrayList GetMemberDisplayName(string MemberId)
        {
            ArrayList custArray;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspGetMemberDisplayName", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@MemberID", MemberId));

                con.Open();
                SqlDataReader dbReader = cmd.ExecuteReader();
                custArray = new ArrayList();

                while (dbReader.Read())
                {
                    Member aCountry = new Member(dbReader["DisplayName"].ToString(), 0);
                    custArray.Add(aCountry);
                }
                con.Close();
            }
            return custArray;
        }

        public Member GetMemberDisplayName(Member member)
        {
           
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspGetMemberDisplayName", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@MemberID", member.MemberId));

                con.Open();
                SqlDataReader dbReader = cmd.ExecuteReader();
             

                while (dbReader.Read())
                {
                    member.DisplayName = dbReader["DisplayName"].ToString();
                   
                }
                con.Close();
            }
            return member;
        }
        public int CountPostLikes(Post post)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspCountPostLikes", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PostID", post.PostId));


                SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
                returnValue.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(returnValue);

                con.Open();
                cmd.ExecuteNonQuery();
                int value = Int32.Parse(cmd.Parameters["@Return_Value"].Value.ToString());

                return value;

            }
        }
        public int CountCommentsLikes(Comments comment)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspCountCommentsLikes", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CommentID", comment.Commentid));


                SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
                returnValue.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(returnValue);

                con.Open();
                cmd.ExecuteNonQuery();
                int value = Int32.Parse(cmd.Parameters["@Return_Value"].Value.ToString());

                return value;

            }
        }
        public int CountPostTags(Post post)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspCountPostTags", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PostID", post.PostId));


                SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
                returnValue.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(returnValue);

                con.Open();
                cmd.ExecuteNonQuery();
                int value = Int32.Parse(cmd.Parameters["@Return_Value"].Value.ToString());

                return value;

            }
        }
        public int uspCountComments(Post post)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspCountComments", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PostID", post.PostId));


                SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
                returnValue.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(returnValue);

                con.Open();
                cmd.ExecuteNonQuery();
                int value = Int32.Parse(cmd.Parameters["@Return_Value"].Value.ToString());

                return value;

            }
        }
        public DataTable GetRefreshComments(Post post, Comments comment)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {


                SqlCommand cmd = new SqlCommand("uspRefreshComments", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PostID", post.PostId);
                cmd.Parameters.AddWithValue("@LastCommentID", comment.Commentid);

                DataTable Comments = new DataTable();


                try
                {
                    con.Open();

                    da = new SqlDataAdapter(cmd);
                    da.Fill(Comments);


                }
                catch (SqlException)
                {
                    throw new ApplicationException("Error connection to database");

                }
                return Comments;

            }
        }
        public bool FlagComment(Comments comments)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspFlagComment", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CommentID", comments.Commentid);

                try
                {
                    con.Open();
                    int x = cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    e.ToString();
                    return false;
                }
            }
        }
        public bool FlagPost(Post post)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspDeletePost", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PostID", post.PostId);

                try
                {
                    con.Open();
                    int x = cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    e.ToString();
                    return false;
                }
            }
        }
        public ArrayList GetLatestPost(Post post)
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetLatestPost", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddWithValue("@PostID", post.PostId);


                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
                    custArray.Add(aCountry);
                }
                dbConnection.Close();
            }
            return custArray;
        }
        public ArrayList GetTop20TextPosts()
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetTop20TextPosts", dbConnection);
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
                    custArray.Add(aCountry);
                }
                dbConnection.Close();
            }
            return custArray;
        }
        public ArrayList GetTop20EventPosts()
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetTop20EventPosts", dbConnection);
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
                    custArray.Add(aCountry);
                }
                dbConnection.Close();
            }
            return custArray;
        }
        public ArrayList GetTop20PhotoPosts()
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetTop20PhotoPosts", dbConnection);
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
                    custArray.Add(aCountry);
                }
                dbConnection.Close();
            }
            return custArray;
        }
        public ArrayList GetTop20ArticlePosts()
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetTop20ArticlePosts", dbConnection);
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
                    custArray.Add(aCountry);
                }
                dbConnection.Close();
            }
            return custArray;
        }
        public ArrayList GetTop20VideoPosts()
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetTop20VideoPosts", dbConnection);
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
                    custArray.Add(aCountry);
                }
                dbConnection.Close();
            }
            return custArray;
        }
        public ArrayList GetTop20FilePosts()
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetTop20FilePosts", dbConnection);
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
                    custArray.Add(aCountry);
                }
                dbConnection.Close();
            }
            return custArray;
        }
        public ArrayList GetTop20NewsFeedsForAGroup(int groupID)
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetTop20NewsFeedsForAGroup", dbConnection);
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddWithValue("@GroupID", groupID);
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
                    custArray.Add(aCountry);
                }
                dbConnection.Close();
            }
            return custArray;
        }
        public ArrayList GetTop20TextPostsForAGroup(int groupID)
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetTop20TextPostsForAGroup", dbConnection);
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddWithValue("@GroupID", groupID);
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
                    custArray.Add(aCountry);
                }
                dbConnection.Close();
            }
            return custArray;
        }
        public ArrayList GetTop20EventPostsForAGroup(int groupID)
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetTop20EventPostsForAGroup", dbConnection);
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddWithValue("@GroupID", groupID);
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
                    custArray.Add(aCountry);
                }
                dbConnection.Close();
            }
            return custArray;
        }
        public ArrayList GetTop20PhotoPostsForAGroup(int groupID)
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetTop20PhotoPostsForAGroup", dbConnection);
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddWithValue("@GroupID", groupID);
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
                    custArray.Add(aCountry);
                }
                dbConnection.Close();
            }
            return custArray;
        }
        public ArrayList GetTop20ArticlePostsForAGroup(int groupID)
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetTop20ArticlePostsForAGroup", dbConnection);
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddWithValue("@GroupID", groupID);
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
                    custArray.Add(aCountry);
                }
                dbConnection.Close();
            }
            return custArray;
        }
        public ArrayList GetTop20VideoPostsForAGroup(int groupID)
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetTop20VideoPostsForAGroup", dbConnection);
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddWithValue("@GroupID", groupID);
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
                    custArray.Add(aCountry);
                }
                dbConnection.Close();
            }
            return custArray;
        }
        public ArrayList GetTop20FilePostsForAGroup(int groupID)
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetTop20FilePostsForAGroup", dbConnection);
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddWithValue("@GroupID", groupID);
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
                    custArray.Add(aCountry);
                }
                dbConnection.Close();
            }
            return custArray;
        }
        public ArrayList GetASinglePost(int postID)
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetASinglePost", dbConnection);
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddWithValue("@PostID", postID);
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
                    custArray.Add(aCountry);
                }
                dbConnection.Close();
            }
            return custArray;
        }

        public ArrayList GetTop1Post()
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetTop1Post", dbConnection);

                dbCommand.CommandType = CommandType.StoredProcedure;


                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
                    custArray.Add(aCountry);
                }
                dbConnection.Close();
            }
            return custArray;
        }

        public ArrayList GetNextTop20Posts(int lastPostID)
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetNextTop20Posts", dbConnection);
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddWithValue("@lastPostID", lastPostID);
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
                    custArray.Add(aCountry);
                }
                dbConnection.Close();
            }
            return custArray;
        }
        public ArrayList GetNextTop20PostsForAGroup(int lastPostID, int GroupID)
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("GetNextTop20PostsForAGroup", dbConnection);
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddWithValue("@lastPostID", lastPostID);
                dbCommand.Parameters.AddWithValue("@GroupID", GroupID);
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
                    custArray.Add(aCountry);
                }
                dbConnection.Close();
            }
            return custArray;
        }

        public ArrayList GetNextTop20TextPosts(int lastPostID)
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetNextTop20TextPosts", dbConnection);
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddWithValue("@lastPostID", lastPostID);
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
                    custArray.Add(aCountry);
                }
                dbConnection.Close();
            }
            return custArray;
        }
        public ArrayList GetNextTop20TextPostsForAGroup(int lastPostID, int GroupID)
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetNextTop20TextPostsForAGroup", dbConnection);
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddWithValue("@lastPostID", lastPostID);
                dbCommand.Parameters.AddWithValue("@GroupID", GroupID);
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
                    custArray.Add(aCountry);
                }
                dbConnection.Close();
            }
            return custArray;
        }

        public ArrayList GetNextTop20EventPosts(int lastPostID)
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetNextTop20EventPosts", dbConnection);
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddWithValue("@lastPostID", lastPostID);
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
                    custArray.Add(aCountry);
                }
                dbConnection.Close();
            }
            return custArray;
        }
        public ArrayList GetNextTop20EventPostsForAGroup(int lastPostID, int GroupID)
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetNextTop20EventPostsForAGroup", dbConnection);
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddWithValue("@lastPostID", lastPostID);
                dbCommand.Parameters.AddWithValue("@GroupID", GroupID);
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
                    custArray.Add(aCountry);
                }
                dbConnection.Close();
            }
            return custArray;
        }

        public ArrayList GetNextTop20PhotoPosts(int lastPostID)
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetNextTop20PhotoPosts", dbConnection);
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddWithValue("@lastPostID", lastPostID);
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
                    custArray.Add(aCountry);
                }
                dbConnection.Close();
            }
            return custArray;
        }
        public ArrayList GetNextTop20PhotoPostsForAGroup(int lastPostID, int GroupID)
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetNextTop20PhotoPostsForAGroup", dbConnection);
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddWithValue("@lastPostID", lastPostID);
                dbCommand.Parameters.AddWithValue("@GroupID", GroupID);
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
                    custArray.Add(aCountry);
                }
                dbConnection.Close();
            }
            return custArray;
        }

        public ArrayList GetNextTop20ArticlePosts(int lastPostID)
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetNextTop20ArticlePosts", dbConnection);
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddWithValue("@lastPostID", lastPostID);
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
                    custArray.Add(aCountry);
                }
                dbConnection.Close();
            }
            return custArray;
        }
        public ArrayList GetNextTop20ArticlePostsForAGroup(int lastPostID, int GroupID)
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetNextTop20ArticlePostsForAGroup", dbConnection);
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddWithValue("@lastPostID", lastPostID);
                dbCommand.Parameters.AddWithValue("@GroupID", GroupID);
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
                    custArray.Add(aCountry);
                }
                dbConnection.Close();
            }
            return custArray;
        }

        public ArrayList GetNextTop20VideoPosts(int lastPostID)
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetNextTop20VideoPosts", dbConnection);
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddWithValue("@lastPostID", lastPostID);
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
                    custArray.Add(aCountry);
                }
                dbConnection.Close();
            }
            return custArray;
        }
        public ArrayList GetNextTop20VideoPostsForAGroup(int lastPostID, int GroupID)
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetNextTop20VideoPostsForAGroup", dbConnection);
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddWithValue("@lastPostID", lastPostID);
                dbCommand.Parameters.AddWithValue("@GroupID", GroupID);
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
                    custArray.Add(aCountry);
                }
                dbConnection.Close();
            }
            return custArray;
        }

        public ArrayList GetNextTop20FilePosts(int lastPostID)
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetNextTop20FilePosts", dbConnection);
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddWithValue("@lastPostID", lastPostID);
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
                    custArray.Add(aCountry);
                }
                dbConnection.Close();
            }
            return custArray;
        }
        public ArrayList GetNextTop20FilePostsForAGroup(int lastPostID, int GroupID)
        {
            ArrayList custArray;
            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand dbCommand = new SqlCommand("uspGetNextTop20FilePostsForAGroup", dbConnection);
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddWithValue("@lastPostID", lastPostID);
                dbCommand.Parameters.AddWithValue("@GroupID", GroupID);
                dbConnection.Open();
                SqlDataReader dbReader = dbCommand.ExecuteReader();
                custArray = new ArrayList();
                while (dbReader.Read())
                {
                    Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
                    custArray.Add(aCountry);
                }
                dbConnection.Close();
            }
            return custArray;
        }
    }
}