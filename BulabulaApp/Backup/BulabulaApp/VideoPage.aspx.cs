using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace BulabulaApp
{
    public partial class VideoPage : System.Web.UI.Page
    {
        public string video = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            

            StringBuilder concatinater = new StringBuilder();

            string videoUrl = "ProcessVideo.ashx?id=" + 150;
            concatinater.Append("<video  controls='controls'>");
            concatinater.Append("<source src='");
            concatinater.Append(videoUrl);
            //concatinater.Append("' type='video/mp4' />");
            concatinater.Append("' type='video/ogg' />");
            concatinater.Append("Your browser does not support the video tag.");
            concatinater.Append("</video>");

           video =  concatinater.ToString();

        }
    }
}