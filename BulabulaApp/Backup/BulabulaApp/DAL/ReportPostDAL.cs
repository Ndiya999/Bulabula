using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Collections;
using System.Drawing;

namespace BulabulaApp
{
    class ReportPostDAL
    {
        private string connectionString;


        public ReportPostDAL()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        }

        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }
        public bool ReportPost(Member member, Post post, string option, string administratorId)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspReportPost", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);
                cmd.Parameters.AddWithValue("@PostID ", post.PostId);
                cmd.Parameters.AddWithValue("@Option", option);
                cmd.Parameters.AddWithValue("@AdministratorID", administratorId);

                try
                {
                    con.Open();
                    int x = cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                    return false;
                }
            }
        }
        public bool ReportComment(Member member, Comments comments, string option, string administratorId)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspReportComment", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);
                cmd.Parameters.AddWithValue("@CommentID", comments.Commentid);
                cmd.Parameters.AddWithValue("@Option", option);
                cmd.Parameters.AddWithValue("@AdministratorID", administratorId);

                try
                {
                    con.Open();
                    int x = cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                    return false;
                }
            }
        }
        public bool ReportMessage(Member member, Messages message, string option, string administratorId)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspReportMessage", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);
                cmd.Parameters.AddWithValue("@MessageID", message.MessageId);
                cmd.Parameters.AddWithValue("@Option", option);
                cmd.Parameters.AddWithValue("@AdministratorID", administratorId);

                try
                {
                    con.Open();
                    int x = cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                    return false;
                }
            }
        }
        public List<Member> GetAllActiveAdministrators()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetAdminNameAndEmail", con);
                cmd.CommandType = CommandType.StoredProcedure;

                List<Member> AdministratorList = new List<Member>();

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Member Administrator = new Member(Convert.ToString(reader["MemberID"]), Convert.ToString(reader["DisplayName"]), Convert.ToString(reader["Email"]));
                        AdministratorList.Add(Administrator);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return AdministratorList;
            }
        }
        public List<Member> GetMemberNameAndEmail(Member member)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetMemberNameAndEmail", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);

                List<Member> MemberNameAndEmailList = new List<Member>();

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Member MemberNameAndEmail = new Member(Convert.ToString(reader["DisplayName"]), Convert.ToString(reader["Email"]));
                        MemberNameAndEmailList.Add(MemberNameAndEmail);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return MemberNameAndEmailList;
            }
        }
        public List<Member> Gers()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetAdminNameAndEmail", con);
                cmd.CommandType = CommandType.StoredProcedure;

                List<Member> AdministratorList = new List<Member>();

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Member Administrator = new Member(Convert.ToString(reader["MemberID"]), Convert.ToString(reader["DisplayName"]), Convert.ToString(reader["Email"]));
                        AdministratorList.Add(Administrator);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return AdministratorList;
            }
        }
    }
}
