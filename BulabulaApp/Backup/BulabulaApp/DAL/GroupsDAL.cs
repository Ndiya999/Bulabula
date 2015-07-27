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
    class GroupsDAL
    {
        private string connectionString;

        public GroupsDAL()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        }

        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }
        public List<Group> GetAllMembersGroups(Member member)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetAllMembersGroups", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);

                List<Group> groupList = new List<Group>();
                
                
                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        Group groupInfo = new Group(int.Parse(reader["GroupID"].ToString()), Convert.ToString(reader["GroupDescription"]));
                        groupList.Add(groupInfo);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString());
                }
                return groupList;
            }
        }

    }
}
