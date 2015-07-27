using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using System.Text;
using System.Data;

namespace BulabulaApp.WebServices
{
    /// <summary>
    /// Summary description for processComments
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    public class Reporting : System.Web.Services.WebService
    {


        [System.ComponentModel.ToolboxItem(false)]
        public class processComments : System.Web.Services.WebService
        {
            MessagesDAL m = new MessagesDAL();
            NewsfeedDAL newsFeedDAL = new NewsfeedDAL();
            private Post aPost = new Post();


            //#region Insert A Comment
            //[WebMethod(EnableSession = true)]
            //public string[] AddCommentnRefresh(int postID, string commentTxt, int lastCommentID)
            //{
            //    return string[];
            //}
        }
    }
}
