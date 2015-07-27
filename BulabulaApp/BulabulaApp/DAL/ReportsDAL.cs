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
    public class ReportsDAL
    {
        SqlCommand command = new SqlCommand();
        SqlDataAdapter adapter;
        private string connectionString;


        public ReportsDAL()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        }

        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        public List<Group> GetNumberOfMembersInGroup(DateTime startdate, DateTime enddate)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                DataTable NumberOfMembersInGroup = new DataTable();
                List<Group> groups = new List<Group>();
                Group group;
                command = new SqlCommand("dbo.uspNumberOfMembersInGroup", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Startdate", startdate));
                command.Parameters.Add(new SqlParameter("@Enddate", enddate));

                try
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        group = new Group(int.Parse(reader["Total Members"].ToString()), reader["GroupDescription"].ToString());
                     //   group.NumberOfMenbers = int.Parse(reader["Total Members"].ToString());
                      //  group.GroupDescription = reader["GroupDescription"].ToString();
                        groups.Add(group);

                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }

              //  adapter = new SqlDataAdapter(command);
               // adapter.Fill(NumberOfMembersInGroup);
                return groups;
            }
        }
        public int GetTotalArticlePost(DateTime startdate, DateTime enddate)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                command = new SqlCommand("dbo.uspTotalArticlePosts", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Startdate", startdate));
                command.Parameters.Add(new SqlParameter("@Enddate", enddate));

                int value = 0;
                try
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        value = Int32.Parse(reader["ArticlePostCount"].ToString());

                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }

                return value;

            }
        }
        public int GetBlockedMembers(DateTime startdate, DateTime enddate)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                command = new SqlCommand("dbo.uspBlockedMembers", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Startdate", startdate));
                command.Parameters.Add(new SqlParameter("@Enddate", enddate));

                int value = 0;
                try
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        value = Int32.Parse(reader["BlockedMembers"].ToString());

                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }

                return value;
            }
        }
        public int GetTotalEventPost(DateTime startdate, DateTime enddate)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                command = new SqlCommand("dbo.uspTotalEventPost", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Startdate", startdate));
                command.Parameters.Add(new SqlParameter("@Enddate", enddate));


                int value = 0;
                try
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        value = Int32.Parse(reader["EventPostCount"].ToString());

                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }

                return value;
            }
        }
        public int GetTotalFilePost(DateTime startdate, DateTime enddate)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                command = new SqlCommand("dbo.uspTotalFilePost", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Startdate", startdate));
                command.Parameters.Add(new SqlParameter("@Enddate", enddate));


                int value = 0;
                try
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        value = Int32.Parse(reader["TotalFilePost"].ToString());

                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }

                return value;
            }
        }
        public int GetTotalNumberOfActiveUsers(string startdate, string enddate)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                command = new SqlCommand("dbo.uspTotalNumberOfActiveUsers", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Startdate", startdate));
                command.Parameters.Add(new SqlParameter("@Enddate", enddate));


                int value = 0;
                try
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        value = Int32.Parse(reader["ActiveUsers"].ToString());

                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return value;
            }
        }
        public int GetTotalGroups()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                command = new SqlCommand("dbo.uspTotalGroups", con);
                command.CommandType = CommandType.StoredProcedure;


                int value = 0;
                try
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        value = Int32.Parse(reader["TotalGroups"].ToString());

                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }

                return value;
            }
        }
        public int GetTotalNumberOfPost(DateTime startdate, DateTime enddate)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                command = new SqlCommand("dbo.uspTotalNumberOfPost", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Startdate", startdate));
                command.Parameters.Add(new SqlParameter("@Enddate", enddate));


                int value = 0;
                try
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        value = Int32.Parse(reader["TotalNumberOfPost"].ToString());

                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }

                return value;
            }
        }
        public int GetTotalPhotoPost(DateTime startdate, DateTime enddate)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                command = new SqlCommand("dbo.uspTotalPhotoPost", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Startdate", startdate));
                command.Parameters.Add(new SqlParameter("@Enddate", enddate));

                int value = 0;
                try
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        value = Int32.Parse(reader["TotalPhotoPost"].ToString());

                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }

                return value;
            }
        }
        public int GetTotalTextPost(DateTime startdate, DateTime enddate)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                command = new SqlCommand("uspTotalTextPost", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Startdate", startdate));
                command.Parameters.Add(new SqlParameter("@Enddate", enddate));

                int value = 0;
                try
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        value = Int32.Parse(reader["TotalTextPost"].ToString());

                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }

                return value;
            }
        }
        public int GetTotalVideoPost(DateTime startdate, DateTime enddate)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                command = new SqlCommand("dbo.uspTotalVideoPost", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Startdate", startdate));
                command.Parameters.Add(new SqlParameter("@Enddate", enddate));


                int value = 0;
                try
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        value = Int32.Parse(reader["vidPostCount"].ToString());

                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }

                return value;
            }
        }
        public int GetTotalNumberOfMessages(DateTime startdate, DateTime enddate)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                command = new SqlCommand("dbo.uspTotalNumberOfMessages", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Startdate", startdate));
                command.Parameters.Add(new SqlParameter("@Enddate", enddate));


                int value = 0;
                try
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        value = Int32.Parse(reader["NumberOfMessages"].ToString());

                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }

                return value;
            }
        }
        public int GetTotalReports(DateTime startdate, DateTime enddate)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                command = new SqlCommand("dbo.uspTotalReports", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@StartDate", startdate));
                command.Parameters.Add(new SqlParameter("@EndDate", enddate));

                int value = 0;
                try
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        value = Int32.Parse(reader["TotalReports"].ToString());

                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }

                return value;
            }
        }
        public int GetTotalNumberOfMembersByDate(DateTime startdate, DateTime enddate)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                command = new SqlCommand("uspTotalNumberOfMembersByDate", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@StartDate", startdate));
                command.Parameters.Add(new SqlParameter("@EndDate", enddate));

                int value = 0;
                try
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        value = Int32.Parse(reader["MembersByDate"].ToString());

                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    //  System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }

                return value;
            }
        }
        public int GetTotalNumberOfBlockedMembersByDate(DateTime startdate, DateTime enddate)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                command = new SqlCommand("uspBlockedMembers", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@StartDate", startdate));
                command.Parameters.Add(new SqlParameter("@EndDate", enddate));

                int value = 0;
                try
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        value = Int32.Parse(reader["BlockedMembers"].ToString());

                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    //  System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }

                return value;
            }
        }
    }


}