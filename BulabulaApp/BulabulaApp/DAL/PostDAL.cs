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
    public class PostDAL
    {
        private string connectionString;

        public PostDAL()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        }

        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }
        public int InsertText(Group group, Text_Post text_Post, Member member)
        {
            using (SqlConnection dbcon = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspInsertText", dbcon);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@GroupID", group.GroupId));
                cmd.Parameters.Add(new SqlParameter("@MemberID", member.MemberId));
                cmd.Parameters.Add(new SqlParameter("@PostText", text_Post.PostText));

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
        public int InsertTextAndReturnPostID(Group group, Text_Post text_Post, Member member)
        {
            using (SqlConnection dbcon = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspInsertTextAndReturnPostID", dbcon);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@GroupID", group.GroupId));
                cmd.Parameters.Add(new SqlParameter("@MemberID", member.MemberId));
                cmd.Parameters.Add(new SqlParameter("@PostText", text_Post.PostText));

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
        public int InsertVideo(Video_Post video_Post, Group group, Member member)
        {
            using (SqlConnection dbcon = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspInsertVideo", dbcon);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@GroupID", group.GroupId));
                cmd.Parameters.Add(new SqlParameter("@MemberID", member.MemberId));
                cmd.Parameters.Add(new SqlParameter("@Video", video_Post.Video));
                cmd.Parameters.Add(new SqlParameter("@VideoName", video_Post.VideoName));
                cmd.Parameters.Add(new SqlParameter("@VideoCaption", video_Post.VideoCaption));
                cmd.Parameters.Add(new SqlParameter("@VideoSize", video_Post.VideoSize));

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
        public int InsertFile(File_Post file_Post, Group group, Member member)
        {
            using (SqlConnection dbcon = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspInsertFile", dbcon);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@GroupID", group.GroupId));
                cmd.Parameters.Add(new SqlParameter("@MemberID", member.MemberId));
                cmd.Parameters.Add(new SqlParameter("@File", file_Post.File));
                cmd.Parameters.Add(new SqlParameter("@FileName", file_Post.FileName));
                cmd.Parameters.Add(new SqlParameter("@FileCaption", file_Post.FileCaption));
                cmd.Parameters.Add(new SqlParameter("@FileSize", file_Post.FileSize));

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
        public int InsertPhoto(Photo_Post photo_Post, Group group, Member member)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                MemoryStream ms = new MemoryStream();
                photo_Post.Photo.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                SqlCommand cmd = new SqlCommand("uspInsertPhoto", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@GroupID", group.GroupId));
                cmd.Parameters.Add(new SqlParameter("@MemberID", member.MemberId));
                cmd.Parameters.Add(new SqlParameter("@Photo", ms.ToArray()));
                cmd.Parameters.Add(new SqlParameter("@PhotoName", photo_Post.PhotoName));
                cmd.Parameters.Add(new SqlParameter("@PhotoCaption", photo_Post.PhotoCaption));


                SqlParameter returnValue = new SqlParameter("@Return_Value", DbType.Int32);
                returnValue.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(returnValue);
                int postId = 0;
                try
                {
                    connection.Open();
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
        public int InsertEvent(Event_Post events, Group group, Member member)
        {
            using (SqlConnection dbcon = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspInsertEvent", dbcon);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@GroupID", group.GroupId));
                cmd.Parameters.Add(new SqlParameter("@MemberID", member.MemberId));
                cmd.Parameters.Add(new SqlParameter("@EventName", events.EventName));
                cmd.Parameters.Add(new SqlParameter("@EventDetails", events.EventDetails));
                cmd.Parameters.Add(new SqlParameter("@EventVenue", events.EventVenue));
                cmd.Parameters.Add(new SqlParameter("@StartDate", events.StartDate));
                cmd.Parameters.Add(new SqlParameter("@EndDate", events.EndDate));
                cmd.Parameters.Add(new SqlParameter("@Host", events.Host));
                cmd.Parameters.Add(new SqlParameter("@EventType", events.EventType));

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
        public void DeletePost(int postid)
        {
            using (SqlConnection dbcon = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspDeletePost", dbcon);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@PostID", postid));

                try
                {
                    dbcon.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    throw new ApplicationException("Error Inserting in: " + e);
                }
            }
        }
        public DataTable GetAImage(int id)
        {
            DataTable file = new DataTable();
            using (SqlConnection dbcon = new SqlConnection(ConnectionString))
            {
                dbcon.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dbcon;
                cmd.CommandTimeout = 0;
                cmd = new SqlCommand("uspGetImage", dbcon);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter();

                cmd.Parameters.Add(new SqlParameter("@PostID", id));

                adapter.SelectCommand = cmd;
                adapter.Fill(file);

                dbcon.Close();
            }
            return file;
        }
        public DataTable GetAVideo(int id)
        {
            DataTable file = new DataTable();
            using (SqlConnection dbcon = new SqlConnection(ConnectionString))
            {
                dbcon.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dbcon;
                cmd.CommandTimeout = 0;
                cmd = new SqlCommand("uspGetVideo", dbcon);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter();
                cmd.Parameters.Add(new SqlParameter("@PostID", id));
                adapter.SelectCommand = cmd;
                adapter.Fill(file);

                dbcon.Close();
            }
            return file;
        }
        public DataTable GetAFile(int id)
        {
            DataTable file = new DataTable();
            using (SqlConnection dbcon = new SqlConnection(ConnectionString))
            {
                dbcon.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dbcon;
                cmd.CommandTimeout = 0;
                cmd = new SqlCommand("uspGetAFile", dbcon);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter();
                cmd.Parameters.Add(new SqlParameter("@PostID", id));
                adapter.SelectCommand = cmd;
                adapter.Fill(file);

                dbcon.Close();
            }
            return file;
        }
    }
}