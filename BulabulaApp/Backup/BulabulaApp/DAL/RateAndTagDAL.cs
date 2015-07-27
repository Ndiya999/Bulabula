using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Collections;


namespace BulabulaApp
{
    public class RateAndTagDAL
    {

        private string connectionString;

        public RateAndTagDAL()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        }

        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }
        public bool TagFriend(Post post, Member friend)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspTagFriend", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PostID ", post.PostId);
                cmd.Parameters.AddWithValue("@FriendID", friend.MemberId);

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

        
        #region Search for an untagged Member
        public List<Member> GetTagedFriends(Post aPost)
        {
            Member m;
            List<Member> members = new List<Member>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {


                SqlCommand cmd = new SqlCommand("uspGetTagedFriends", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PostID", aPost.PostId);
           
           
                try
                {
                    con.Open();


                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        m = new Member(Convert.ToString(reader["MemberID"]), Convert.ToString(reader["DisplayName"]));
                        members.Add(m);


                    }//End while
                    reader.Close();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    // MessageBox.Show(ex.Message);
                }

                return members;

            }
        }
        #endregion

        public bool DeleteTag(Post post, Member friend)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspDeleteTag", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PostID ", post.PostId);
                cmd.Parameters.AddWithValue("@FriendID", friend.MemberId);

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

        public int PostRatingExists(Member member, Post post)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspPostRatingExists", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PostID", post.PostId);
                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);

                SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
                returnValue.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(returnValue);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    int value = Int32.Parse(cmd.Parameters["@Return_Value"].Value.ToString());

                    return value;
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                    return -1;
                }
            }
        }

        public bool RatePost(Member member, Post post)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspRatePost", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PostID", post.PostId);
                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);

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

        public int CommentRatingExists(Member member, Comments comments)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspCommentRatingExists", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);
                cmd.Parameters.AddWithValue("@CommentID", comments.Commentid);

                SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
                returnValue.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(returnValue);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    int value = Int32.Parse(cmd.Parameters["@Return_Value"].Value.ToString());

                    return value;
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                    return -1;
                }
            }
        }

        public bool RateComment(Member member, Comments comments)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspRateComment", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);
                cmd.Parameters.AddWithValue("@CommentID", comments.Commentid);

                try
                {
                    con.Open();
                    int x = cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                    return false;
                }
            }
        }

        public bool DeletePostLike(Member member, Post post)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspDeletePostLike", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PostID", post.PostId);
                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);

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

        public bool DeleteCommentLike(Member member, Comments comments)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspDeleteCommentLike", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);
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
        public List<Member> GetPostMemberID(Post post)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetPostMemberID", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PostID", post.PostId);

                List<Member> memeberList = new List<Member>();

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Member member = new Member(Convert.ToString(reader["MemberID"]));
                        memeberList.Add(member);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    throw new Exception (e.ToString());
                }
                return memeberList;
            }
        }


        #region Get people who liked a post
        public List<Member> GetPeopleLikedPost(Post aPost)
        {
            Member m;
            List<Member> members = new List<Member>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {


                SqlCommand cmd = new SqlCommand("uspGetPeopleLikedPost", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PostID", aPost.PostId);


                try
                {
                    con.Open();


                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        m = new Member(Convert.ToString(reader["MemberID"]), Convert.ToString(reader["DisplayName"]));
                        members.Add(m);


                    }//End while
                    reader.Close();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    // MessageBox.Show(ex.Message);
                }

                return members;

            }
        }
        #endregion
    }
}