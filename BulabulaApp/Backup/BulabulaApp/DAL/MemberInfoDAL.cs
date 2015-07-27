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

namespace BulabulaApp
{
    class MemberInfoDAL
    {
        private string connectionString;

        public MemberInfoDAL()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        }

        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }
        public Member GetAllMemberInfo(Member member)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetAllMemberInfo", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);

               // List<Member> memberList = new List<Member>();

               
                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        byte[] imageBytes = reader["ProfilePicture"] as byte[];
                        MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                        // Convert byte[] to Image
                        ms.Write(imageBytes, 0, imageBytes.Length);
                        Image image = Image.FromStream(ms, true);
                       
                                member.FirstName = Convert.ToString(reader["FirstName"]);
                                member.LastName = Convert.ToString(reader["LastName"]);
                                member.DisplayName = Convert.ToString(reader["DisplayName"]);
                                member.Email =  Convert.ToString(reader["Email"]);
                                member.RegistrationDate = Convert.ToDateTime(reader["RegistrationDate"]);
                                member.Description = Convert.ToString(reader["Description"]);
                                member.Campus =  Convert.ToString(reader["Campus"]);
                                member.MemberType = Convert.ToString(reader["MemberType"]);
                                member.IsOnline = Convert.ToBoolean(reader["isOnline"]);
                                member.ProfilePicture = image;
                        
                        //memberList.Add(memberInfo);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString());
                }
                return member;
            }
        }

        public Member GetActualDisplayName(Member member)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetActualDisplayName", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@MemberID", member.MemberId));

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        member.DisplayName = reader["DisplayName"].ToString();
                   
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return member;
            }
        }
        public bool UpdateToOffline(Member member)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspUpdateToOffline", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);

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
        public bool UpdateToOnline(Member member)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspUpdateToOnline", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);

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
