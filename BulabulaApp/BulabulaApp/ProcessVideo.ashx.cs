using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Specialized;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.SessionState;

namespace BulabulaApp
{
    /// <summary>
    /// Summary description for ProcessVideo
    /// </summary>
    public class ProcessVideo : IHttpHandler
    {
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        }

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                // Check if id was given 
                if (context.Request.QueryString["id"] != null)
                {
                    string movId = context.Request.QueryString["id"];


                    SqlConnection connection = new SqlConnection(GetConnectionString());
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("uspGetVideo", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter sqlParam = cmd.Parameters.Add("@PostID", SqlDbType.Int);
                        sqlParam.Value = movId;


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();

                                context.Response.Cache.SetCacheability(HttpCacheability.Public);
                                context.Response.Cache.SetLastModified(DateTime.Now);
                                //context.Response.AppendHeader("Content-Type", "video/mp4");
                                context.Response.AppendHeader("Content-Type", "video/ogg");
                                context.Response.AppendHeader("Content-Length", ((byte[])dr["Video"]).Length.ToString());
                                context.Response.BinaryWrite((byte[])dr["Video"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
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