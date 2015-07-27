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
using System.IO;
using AjaxControlToolkit;

namespace BulabulaApp
{
    public class ChatDAL
    {
         private string connectionString;

         public ChatDAL()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        }

        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }



        public int InsertChatText(Group group, string ChatText, Member member)
        {
            using (SqlConnection dbcon = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspInsertChatText", dbcon);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@GroupID", group.GroupId));
                cmd.Parameters.Add(new SqlParameter("@MemberID", member.MemberId));
                cmd.Parameters.Add(new SqlParameter("@ChatText", ChatText));

                SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
                returnValue.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(returnValue);
                int postId = 0;
                try
                {
                    dbcon.Open();
                    cmd.ExecuteNonQuery();
                    postId = Int32.Parse(cmd.Parameters["@Return_Value"].Value.ToString());
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.ToString());
                }
                return postId;
               
            }
        }


        public Article_Post getChatText(ref Post aPost)
        {
            Article_Post aArticle = new Article_Post();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspGetChatText", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PostID", aPost.PostId));

                con.Open();
                SqlDataReader dbReader = cmd.ExecuteReader();
               

                while (dbReader.Read())
                {

                    aPost.CreateDate = DateTime.Parse(dbReader["CreateDate"].ToString());
                    aArticle.ArticleText = dbReader["ArticleText"].ToString();
                }
                con.Close();
            }
         return aArticle;
           
        }

        public List<Article_Post> getAllChats(int groupID)
        {
            Article_Post aArticle;
            List<Article_Post> arcticlePosts = new List<Article_Post>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspGetAllChatPosts", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@GroupID", groupID));

                con.Open(); 
                SqlDataReader dbReader = cmd.ExecuteReader();


                while (dbReader.Read())
                {

                    aArticle = new Article_Post(int.Parse(dbReader["PostID"].ToString()), dbReader["ArticleText"].ToString(), DateTime.Parse(dbReader["CreateDate"].ToString()), dbReader["MemberID"].ToString());
                    arcticlePosts.Add(aArticle);
                  //  aArticle.ArticleText = dbReader["ArticleText"].ToString();
                }
                con.Close();
            }
            return arcticlePosts;

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



    }
}