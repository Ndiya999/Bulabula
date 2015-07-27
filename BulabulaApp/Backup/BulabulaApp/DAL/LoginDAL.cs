using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration; //For Connectionstring configuration
using System.Data; // For non-provider specific data objects
using System.Data.SqlClient; // For Sql Server specific data objects
using System.Data.SqlTypes; // For Sql types required for command parameters
using System.Collections;
using System.IO;

namespace BulabulaApp
{
    public class LoginDAL
    {
       // DataSet ds = new DataSet();

        private string connectionString;


        public LoginDAL()
        {
            connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        }

        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        public bool InsertMember(Member member)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                MemoryStream ms = new MemoryStream();
                member.ProfilePicture.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                SqlCommand cmd = new SqlCommand("uspInsertMember", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);
                cmd.Parameters.AddWithValue("@FirstName", member.FirstName);
                cmd.Parameters.AddWithValue("@LastName", member.LastName);
                cmd.Parameters.AddWithValue("@DisplayName", member.DisplayName);
                cmd.Parameters.AddWithValue("@Email", member.Email);
                cmd.Parameters.AddWithValue("@Profilepicture", SqlDbType.Image).Value = ms.ToArray();
                cmd.Parameters.AddWithValue("@Description", member.Description);
                cmd.Parameters.AddWithValue("@Campus", member.Campus);
                cmd.Parameters.AddWithValue("@MemberType", member.MemberType);

                try
                {
                    con.Open();
                    int x = cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                    HttpContext.Current.Response.Redirect("~/Error_Pages/503.htm");
                    return false;
                }
            }
        }
        public bool MemberExists(Member member)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspMemberExists", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@MemberID", member.MemberId));

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
                    e.Message.ToString();
                    HttpContext.Current.Response.Redirect("~/Error_Pages/503.htm");
                    return false;
                }
            }
        }
        public bool MembershipExists(Membership membership)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspMembershipsExists", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Department", membership.Department));
                cmd.Parameters.Add(new SqlParameter("@Description", membership.Description));

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
                    e.Message.ToString();
                    HttpContext.Current.Response.Redirect("~/Error_Pages/503.htm");
                    return false;
                }

            }
        }
        public bool GroupExists(Group group)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGroupExists", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@groupName", group.GroupName));

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
                    e.Message.ToString();
                    HttpContext.Current.Response.Redirect("~/Error_Pages/503.htm");
                    return false;
                }
            }
        }
        public bool InsertGroup(Group group)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspInsertGroup", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@groupName", group.GroupName);
                cmd.Parameters.AddWithValue("@groupDescription", group.GroupDescription);

                try
                {
                    con.Open();
                    int x = cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                    HttpContext.Current.Response.Redirect("~/Error_Pages/503.htm");
                    return false;
                }
            }
        }
        public bool AssignMemberTogroup(Group_Member group_Member)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspAssignMemberTogroup", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", group_Member.MemberId);
                cmd.Parameters.AddWithValue("@GroupID", group_Member.GroupId);

                try
                {
                    con.Open();
                    int x = cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                    HttpContext.Current.Response.Redirect("~/Error_Pages/503.htm");
                    return false;
                }
            }
        }
        public int GetGroupID(Group group)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetGroupID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@groupName", group.GroupName);

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
                    HttpContext.Current.Response.Redirect("~/Error_Pages/503.htm");
                    return -1;
                }
            }
        }
        public bool GetMemberStatus(Member_Status member_Status)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetMemberStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member_Status.MemberId);

                try
                {
                    con.Open();
                    SqlDataReader dbReader = cmd.ExecuteReader();

                    bool isBlocked = false;
                    while (dbReader.Read())
                    {
                        if (int.Parse(dbReader["StatusID"].ToString()) == 1)
                        {
                            isBlocked = false;
                        }
                        else
                        {
                            isBlocked = true;
                        }
                    }
                    return isBlocked;
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                    HttpContext.Current.Response.Redirect("~/Error_Pages/503.htm");
                    return false;
                }
            }
        }
    }
}