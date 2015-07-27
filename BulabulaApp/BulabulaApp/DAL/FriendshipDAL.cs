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
    class FriendshipDAL
    {
        private string connectionString;


        public FriendshipDAL()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        }

        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }
        public bool InviteFriend(Member member, Member friend)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspInviteFriend", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);
                cmd.Parameters.AddWithValue("@FriendID", friend.MemberId);

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
        public bool UpdateInvitationStatus(Member member, Member friend, string invitationStatus)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspUpdateInvitationStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);
                cmd.Parameters.AddWithValue("@FriendID", friend.MemberId);
                cmd.Parameters.AddWithValue("@InvitationStatus", invitationStatus);

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
        public List<Member_Friend> GetInvitationStatus(Member member, Member friend)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetInvitationStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);
                cmd.Parameters.AddWithValue("@FriendID", friend.MemberId);

                List<Member_Friend> statusList = new List<Member_Friend>();

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Member_Friend status = new Member_Friend(Convert.ToString(reader["InvitationStatus"]));
                        statusList.Add(status);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString());
                }
                return statusList;
            }
        }

        public int InviteExists(Member member, Member friend)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspInviteExists", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);
                cmd.Parameters.AddWithValue("@FriendID", friend.MemberId);

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
        public bool BlockFriend(Member member, Member friend)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspBlockFriend", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);
                cmd.Parameters.AddWithValue("@FriendID", friend.MemberId);

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
        public bool UnblockFriend(Member member, Member friend)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspUnblockFriend", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);
                cmd.Parameters.AddWithValue("@FriendID", friend.MemberId);

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
        public bool BlockMember(Member member, Member friend)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspBlockMember", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);
                cmd.Parameters.AddWithValue("@FriendID", friend.MemberId);

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
        public bool UnblockMember(Member member, Member friend)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspUnblockMember", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);
                cmd.Parameters.AddWithValue("@FriendID", friend.MemberId);

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
        public List<Member> GetAllBlockedFriends(Member member)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetAllBlockedFriends", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);

                List<Member> BlockedMembersList = new List<Member>();

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

                        Member BlockedMembers = new Member(Convert.ToString(reader["MemberID"]), Convert.ToString(reader["DisplayName"]), Convert.ToString(reader["Email"]), Convert.ToString(reader["Description"]), Convert.ToString(reader["Campus"]), bool.Parse(reader["isOnline"].ToString()), image);
                        BlockedMembersList.Add(BlockedMembers);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString());
                }
                return BlockedMembersList;
            }
        }
        public int MemberIsActaullyTheMember(Member member, Member friend)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspMemberIsActaullyTheMember", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);
                cmd.Parameters.AddWithValue("@FriendID", friend.MemberId);

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
        public int FriendIsActaullyTheMember(Member member, Member friend)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspFriendIsActaullyTheMember", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);
                cmd.Parameters.AddWithValue("@FriendID", friend.MemberId);

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
        public List<Member> GetAllFriends(Member member)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetAllFriends", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);

                List<Member> AllFriendsList = new List<Member>();

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

                        Member AllFriends = new Member(Convert.ToString(reader["MemberID"]), Convert.ToString(reader["DisplayName"]), Convert.ToString(reader["Email"]), Convert.ToString(reader["Description"]), Convert.ToString(reader["Campus"]), bool.Parse(reader["isOnline"].ToString()), image);
                        AllFriendsList.Add(AllFriends);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString());
                }
                return AllFriendsList;
            }
        }
        public List<Member> GetAllFriendInvites(Member member)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetAllFriendInvites", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);

                List<Member> AllFriendInvitesList = new List<Member>();

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

                        Member AllFriendInvites = new Member(Convert.ToString(reader["MemberID"]), Convert.ToString(reader["DisplayName"]), Convert.ToString(reader["Email"]), Convert.ToString(reader["Description"]), Convert.ToString(reader["Campus"]), bool.Parse(reader["isOnline"].ToString()), image);
                        AllFriendInvitesList.Add(AllFriendInvites);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString());
                }
                return AllFriendInvitesList;
            }
        }
        public List<Member> GetAllFriendsOnline(Member member)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetAllFriendsOnline", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);

                List<Member> AllFriendOnlineList = new List<Member>();

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Member AllFriendOnline = new Member(Convert.ToString(reader["MemberID"]), Convert.ToString(reader["DisplayName"]));
                        AllFriendOnlineList.Add(AllFriendOnline);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString());
                }
                return AllFriendOnlineList;
            }
        }
        public List<Member_Friend> GetMemberBlockedFriendStatus(Member member, Member friend)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetMemberBlockedFriendStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);
                cmd.Parameters.AddWithValue("@FriendID", friend.MemberId);

                List<Member_Friend> statusList = new List<Member_Friend>();

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Member_Friend status = new Member_Friend(bool.Parse(reader["MemberBlockedFriend"].ToString()));
                        statusList.Add(status);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString());
                }
                return statusList;
            }
        }
        public List<Member_Friend> GetFriendBlockedMemberStatus(Member member, Member friend)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetFriendBlockedMemberStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);
                cmd.Parameters.AddWithValue("@FriendID", friend.MemberId);

                List<Member_Friend> statusList = new List<Member_Friend>();

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Member_Friend status = new Member_Friend(bool.Parse(reader["FriendBlockedMember"].ToString()), 1);
                        statusList.Add(status);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString());
                }
                return statusList;
            }
        }
        public bool RemoveFriend(Member member, Member friend)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspRemoveFriend", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MemberID", member.MemberId);
                cmd.Parameters.AddWithValue("@FriendID", friend.MemberId);

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
