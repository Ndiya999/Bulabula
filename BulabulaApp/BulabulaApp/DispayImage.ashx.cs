using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Specialized;
using System.Configuration;

namespace BulabulaApp
{
    /// <summary>
    /// Summary description for DispayImage
    /// </summary>
    public class DispayImage : IHttpHandler
    {

        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        }

        public void ProcessRequest(HttpContext context)
        {
            string id = context.Request.QueryString["id"]; //get the querystring value that was pass on the ImageURL (see GridView MarkUp in Page1.aspx)

            if (id != null)
            {
                
                MemoryStream memoryStream = new MemoryStream();
                SqlConnection connection = new SqlConnection(GetConnectionString());
                string sql = "SELECT * FROM Photo_Post WHERE PostID = @PostID";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@PostID", id);
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                //Get Image Data
                byte[] file = (byte[])reader["Photo"];

                reader.Close();
                connection.Close();
                memoryStream.Write(file, 0, file.Length);
                context.Response.Buffer = true;
                context.Response.BinaryWrite(file);
                memoryStream.Dispose();
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}