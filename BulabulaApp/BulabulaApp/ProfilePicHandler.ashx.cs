using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;

namespace BulabulaApp
{
    /// <summary>
    /// Summary description for ProfilePicHandler
    /// </summary>
    public class ProfilePicHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext Context)
        {
         
            if (Context.Session["profileFriendID"] != null)
            {

                string memberId = Context.Request.QueryString["id"]; //get the querystring value that was pass on the ImageURL (see GridView MarkUp in Page1.aspx)
                memberId = SSTCryptographer.Encrypt(memberId);
     
                
                Member aMember = new Member(memberId);

                if (memberId != null)
                {
                    MemberInfoDAL dal = new MemberInfoDAL();
                    aMember = dal.GetAllMemberInfo(aMember);

                    MemoryStream ms = new MemoryStream();
                    aMember.ProfilePicture.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                    byte[] file = ms.ToArray();
                    ms.Write(file, 0, file.Length);
                    Context.Response.Buffer = true;
                    Context.Response.BinaryWrite(file);
                    ms.Dispose();
                }
                Context.Session["profileFriendID"] = Context.Session["memberID"].ToString();

            }
            else
            {
                string memberId = Context.Request.QueryString["id"]; //get the querystring value that was pass on the ImageURL (see GridView MarkUp in Page1.aspx)
                memberId = SSTCryptographer.Encrypt(memberId);
                

                Member aMember = new Member(memberId);

                if (memberId != null)
                {
                    MemberInfoDAL dal = new MemberInfoDAL();
                    aMember = dal.GetAllMemberInfo(aMember);

                    MemoryStream ms = new MemoryStream();
                    aMember.ProfilePicture.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                    byte[] file = ms.ToArray();
                    ms.Write(file, 0, file.Length);
                    Context.Response.Buffer = true;
                    Context.Response.BinaryWrite(file);
                    ms.Dispose();
                }


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