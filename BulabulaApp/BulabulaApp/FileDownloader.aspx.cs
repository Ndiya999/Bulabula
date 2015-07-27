using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
namespace BulabulaApp
{
    public partial class FileDownloader : System.Web.UI.Page
    {
        public string video = null;
        protected void Page_Load(object sender, EventArgs e)
        {

            PostDAL postDAL = new PostDAL();
            if (!String.IsNullOrEmpty(Request.Form["fileID"].ToString()))
            {
                int FileID = int.Parse(Request.Form["fileID"].ToString());
                DataTable file = postDAL.GetAFile(FileID);

                DataRow row = file.Rows[0];

                string name = (string)row["FileName"];
                string contentType = (string)row["FileCaption"];
                Byte[] data = (Byte[])row["File"];

                // Send the file to the browser
                Response.AddHeader("Content-type", contentType);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + name);
                Response.BinaryWrite(data);
                Response.Flush();
                Response.End();
            }

        }
    }
}