using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;




namespace BulabulaApp
{
    class MessagesDAL
    {
        DataSet ds = new DataSet();
        private SqlDataAdapter da;
        private string connectionString;
        SqlCommand cmd;

        public MessagesDAL()
        {
            ConnectionString = WebConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

        }




        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        #region Search for a Member
        public List<Member> SearchMember(string member)
        {
            Member m;
            List<Member> members = new List<Member>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                
            
                SqlCommand cmd = new SqlCommand("uspSearchMemberName", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MemberName", member);

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

        #region Search for an untagged Member
        public List<Member> SearchUntaggedMember(Member aMember, Post aPost)
        {
            Member m;
            List<Member> members = new List<Member>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {


                SqlCommand cmd = new SqlCommand("uspSearchFriendsToTag", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PostID", aPost.PostId);
                cmd.Parameters.AddWithValue("@Search", aMember.DisplayName);
                cmd.Parameters.AddWithValue("@MemberID", aMember.MemberId);
           
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

        #region Send Message
        public bool AddMessage(Messages message, Member member, Member friend)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                int x = 0;
                SqlCommand cmd = new SqlCommand("AddMessage", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);
                cmd.Parameters.AddWithValue("@FriendID", friend.MemberId);
                cmd.Parameters.AddWithValue("@MessageText", message.MessageText);

                try
                {
                    con.Open();

                     x = cmd.ExecuteNonQuery();
                   
                    
                
                }
                catch (Exception e)
                {
                    e.ToString();
                }

                if (x < 0)
                { return true; }
                else { return false; }


                
               

            }


        }

        #endregion

        #region Get Inbox
        public DataTable getInboxList(Member member)
        {

            DataTable inboxList = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                cmd = new SqlCommand("uspGetInbox", con);
                cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@MemberID", member.MemberId);

                try
                {
                    con.Open();

                    da = new SqlDataAdapter(cmd);
                    da.Fill(inboxList);


                }
                catch (SqlException ex)
                {
                    throw new ApplicationException(ex.ToString());

                }
                return inboxList;

            }


        }
        #endregion


        #region Get a message
        public DataTable GetAMessage(Message message)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {


                SqlCommand cmd = new SqlCommand("uspGetMessage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MessageID", message.MessageId);

                DataTable aMessage = new DataTable();


                try
                {
                    con.Open();

                    da = new SqlDataAdapter(cmd);
                    da.Fill(aMessage);


                }
                catch (SqlException)
                {
                    throw new ApplicationException("Error connection to database");

                }
                return aMessage;

            }

        }
        #endregion


        #region Get the previous message
        public DataTable GetPreviousMessage(Messages message, Member member)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {


                SqlCommand cmd = new SqlCommand("uspGetPreviousMessage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MessageID", message.MessageId);
                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);
          

                DataTable aMessage = new DataTable();


                try
                {
                    con.Open();

                    da = new SqlDataAdapter(cmd);
                    da.Fill(aMessage);


                }
                catch (SqlException)
                {
                    throw new ApplicationException("Error connection to database");

                }
                return aMessage;

            }

        }
        #endregion

        #region Get the next message
        public DataTable GetNextMessage(Messages message, Member member)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {


                SqlCommand cmd = new SqlCommand("uspGetNextMessage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MessageID", message.MessageId);
                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);

                DataTable aMessage = new DataTable();


                try
                {
                    con.Open();

                    da = new SqlDataAdapter(cmd);
                    da.Fill(aMessage);


                }
                catch (SqlException)
                {
                    throw new ApplicationException("Error connection to database");

                }
                return aMessage;

            }

        }
        #endregion

        #region Get a message's index
        public int[] GetMessageIndex(Messages message, Member member)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                int[] index =  new int[2];
                SqlCommand cmd = new SqlCommand("uspGetMessageIndex", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MessageID", message.MessageId);
                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);

                DataTable aMessage = new DataTable();


                try
                {
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        index[0] = int.Parse(reader["IndexCount"].ToString());
                        index[1] = int.Parse(reader["InboxCount"].ToString());
                     
                    }//End while
                    reader.Close();


                }
                catch (SqlException)
                {
                    throw new ApplicationException("Error connection to database");

                }
                return index;

            }

     
          }
        #endregion

        #region Delete a message
        public bool DeleteMessage(Messages message)

        {
            int countRow = 0;
                 using (SqlConnection con = new SqlConnection(ConnectionString))
            {


                SqlCommand cmd = new SqlCommand("uspDeleteMessage", con);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@MessageID", message.MessageId);

                da = new SqlDataAdapter(cmd);
               
                   
                    try
                    {
                        con.Open();


                        countRow = cmd.ExecuteNonQuery();


                    }
                    catch (SqlException)
                    {
                        throw new ApplicationException("Error connection to database");

                    }
                    

                }
                 if (countRow > 0)
                 {

                     return true;
                 }
                 else { return false; }


        }
        #endregion

        #region Get Newsfeed

        //    public List<Post> GetTop20NewsFeeds()
    //    {
    //        List<Post> posts = new List<Post>();
    //        SqlCommand dbCommand = new SqlCommand("uspGetTop20NewsFeeds", ConnectionString);


    //        using (SqlConnection con = new SqlConnection(ConnectionString))
    //        {
    //            SqlCommand dbCommand = new SqlCommand("uspGetTop20NewsFeeds", con);

    //            con.Open();
    //            SqlDataReader dbReader = dbCommand.ExecuteReader();

    //            while (dbReader.Read())
    //            {
    //                Post aCountry = new Post(int.Parse(dbReader["PostID"].ToString()), DateTime.Parse(dbReader["CreateDate"].ToString()), int.Parse(dbReader["GroupID"].ToString()), dbReader["MemberID"].ToString(), dbReader["PostType"].ToString());
    //                posts.Add(aCountry);
    //            }

    //        }
    //        return posts;
    //    }
       #endregion




//##########################################################
                       //   COMMENTS
//##########################################################



        #region insert a Comment
        public void InsertComment(Comments comment)
        {
            using (SqlConnection dbcon = new SqlConnection(ConnectionString))
            {
                cmd = new SqlCommand("uspCommentOnPost", dbcon);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PostID", comment.PostId);
                cmd.Parameters.AddWithValue("@MemberID", comment.MemberId);
                cmd.Parameters.AddWithValue("@CommentText", comment.CommentText);

                try
                {
                    dbcon.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    throw new ApplicationException("Error inserting comment in:" + comment.PostId + ".");
                }
            }

        }
        #endregion

        #region Get the total number of comments for a particular post
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
        #endregion

        #region Get all the comments for a particular post
        public DataTable GetAllComments(Post post)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {


                SqlCommand cmd = new SqlCommand("uspGetALLComments", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PostID", post.PostId);

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
        #endregion

        public bool IsMessageRead(Message message)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspGetIsMessageRead", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@MessageID", message.MessageId));

                SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
                returnValue.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(returnValue);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    int count = Int32.Parse(cmd.Parameters["@Return_Value"].Value.ToString());

                    if (count > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message.ToString());
                    //return false;
                }
            }
        }

        #region Search for a Group
        public List<Group> SearchGroup(string group)
        {
            Group m;
            List<Group> groups = new List<Group>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {


                SqlCommand cmd = new SqlCommand("uspSearchGroupDescription", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GroupPrefix", group);

                try
                {
                    con.Open();


                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        m = new Group(int.Parse(reader["GroupID"].ToString()), Convert.ToString(reader["GroupDescription"]));
                        groups.Add(m);


                    }//End while
                    reader.Close();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    // MessageBox.Show(ex.Message);
                }

                return groups;

            }
        }
        #endregion
    }
}

        