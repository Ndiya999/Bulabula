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
    class AdministratorTasksDAL
    {

        private string connectionString;

        public AdministratorTasksDAL()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        }

        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }
        public bool DisableAccount(Administrator administrator, Member member, string reasonBlocked)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspDisableAccount", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AdministratorID", administrator.AdministratorId);
                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);
                cmd.Parameters.AddWithValue("@ReasonBlocked", reasonBlocked);

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
        public bool UnblockAccount(Administrator administrator, Member member, string reasonUnblocked)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspUnblockAccount", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AdministratorID", administrator.AdministratorId);
                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);
                cmd.Parameters.AddWithValue("@ReasonUnblocked", reasonUnblocked);

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
