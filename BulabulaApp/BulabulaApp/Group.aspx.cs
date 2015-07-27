using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Collections;
using System.Drawing;
using System.Text;
using AjaxControlToolkit;
using System.IO;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;



namespace BulabulaApp
{

    public partial class WebForm5 : System.Web.UI.Page
    {
        private Post aPost;
        ArrayList ArrayPosts;
        ArrayList ArrayMembers;
        int countMembers;
        public string newsfeed = null;
        public string allgroups = null;
        public StringBuilder concatinater = new StringBuilder();
        public Member aMember = new Member();
        LoginDAL loginDAL = new LoginDAL();
        Member_Status member_Status;
        public string FriendsOnlineString = null;
        public int groupID;
        // string groupDesc;
        PostDAL postDAL = new PostDAL();
        Video_Post video = new Video_Post();
        Group group = new Group();
        Member member = new Member();
        string uploadType = null;
        public HiddenField caption = null;
        public static string captionP = "";

        Reusable_Methods reusable_Methods = new Reusable_Methods();
        NewsfeedDAL bl = new NewsfeedDAL();
        int GroupID;

        protected void Page_Load(object sender, EventArgs e)
        {
            Literal title = (Literal)Master.FindControl("pageTitle");

            caption = (HiddenField)Master.FindControl("caption");

            #region NEWSFEED
            MessagesDAL member = new MessagesDAL();
            if (!IsPostBack)
            {
                if (Session["memberID"] == null)
                {

                    Response.Redirect("~/Login.aspx");

                }
                else if (loginDAL.GetMemberStatus(member_Status = new Member_Status(Session["memberID"].ToString())))
                {
                    if (Request.Cookies["login"] != null)
                    {
                        var c = new HttpCookie("login");
                        c.Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies.Add(c);
                        Session.Abandon();
                    }

                    Response.Redirect("~/Login.aspx");
                }
                else if (Session["GroupInfo"] != null)
                {
                    string m = Session["GroupInfo"].ToString();

                    string[] data = m.Split('&');

                    groupID = int.Parse(data[0]);
                    ThisGroupsID.Value = "" + groupID;
                    title.Text = data[1];

                    #region RIGHT COLUMN GET 5 JOINEDGROUPS
                    //JOINED GROUPS==============================================================================


                    string memberId = Context.Session["memberID"].ToString();
                    aMember.MemberId = memberId;

                    Reusable_Methods reusable_Methods = new Reusable_Methods(aMember);
                    allgroups = reusable_Methods.RightColumnGet5JoinedGroups();
                    //===========================================================================================
                    #endregion

                    #region NEWS FEED

                    string TabName = "Posts";

                    BulabulaApp.WebServices.postsWebservice posts = new WebServices.postsWebservice();

                    newsfeed = posts.GetTabContents(TabName, groupID, -1);
                    #endregion

                    #region RIGHT COLUMN FRIENDS ONLINE
                    //FRIENDS ONLINE==============================================================================

                    memberId = Context.Session["memberID"].ToString();
                    aMember.MemberId = memberId;

                    reusable_Methods = new Reusable_Methods(aMember);
                    FriendsOnlineString = reusable_Methods.RightColumnGetFriendsOnline();

                    //===========================================================================================
                    #endregion

                }

            }//END MAIN if loading for first time
            #endregion

            #region Picture Upload
            if (Request.QueryString["preview"] == "1" && !string.IsNullOrEmpty(Request.QueryString["fileId"]) && uploadType == "Picture")
            {
                var fileId = Request.QueryString["fileId"];
                var fileContentType = (string)Session["fileContentType_" + fileId];
                var fileName = (string)Session["fileName_" + fileId];
                byte[] imageBytes = File.ReadAllBytes(System.Web.HttpContext.Current.Server.MapPath("~") + "file.png");
                var fileContents = imageBytes;

                string ct = (string)Session["fileContentType_" + fileId];
                if (ct.Contains("jpg") || ct.Contains("gif") || ct.Contains("png") || ct.Contains("jpeg"))
                    fileContents = (byte[])Session["fileContents_" + fileId];




                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                // Convert byte[] to Image
                ms.Write(imageBytes, 0, imageBytes.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);

                Photo_Post photoPost = new Photo_Post();
                Group g = new Group();
                PostDAL postDAl = new PostDAL();

                aMember.MemberId = "5ZMjMHEwVN7GyJ2miPDjeQ==";
                g.GroupId = 1;

                photoPost.Photo = image;
                photoPost.PhotoName = fileName;
                photoPost.PhotoCaption = "STeVO";

                postDAl.InsertPhoto(photoPost, g, aMember);
                Response.Clear();
                Response.ContentType = fileContentType;
                Response.BinaryWrite(fileContents);
                Response.End();
            }

            #endregion

            #region Video Upload

            if (Request.QueryString["preview"] == "1" && !string.IsNullOrEmpty(Request.QueryString["fileId"]) && uploadType == "Video")
            {
                var videoFileId = Request.QueryString["fileId"];
                var videofileContentType = (string)Session["fileContentType_" + videoFileId];
                var videofileName = (string)Session["fileName_" + videoFileId];
                byte[] videoBytes = File.ReadAllBytes(System.Web.HttpContext.Current.Server.MapPath("~") + "file.png");
                var videofileContents = videoBytes;

                string ct = (string)Session["fileContentType_" + videoFileId];
                if (ct.Contains("mp4") || ct.Contains("mov") || ct.Contains("vlc") || ct.Contains("avi") || ct.Contains("jpeg"))
                    videofileContents = (byte[])Session["fileContents_" + videoFileId];




                //  MemoryStream vs = new MemoryStream(videoBytes, 0, videoBytes.Length);
                // Convert byte[] to Image
                //vs.Write(videoBytes, 0, videoBytes.Length);
                //System.Drawing.Image image = System.Drawing.Image.FromStream(vs, true);

                Video_Post videoPost = new Video_Post();
                Group g = new Group();
                PostDAL postDAl = new PostDAL();

                aMember.MemberId = "5ZMjMHEwVN7GyJ2miPDjeQ==";
                g.GroupId = 1;

                videoPost.Video = videoBytes;
                videoPost.VideoName = videofileName;
                videoPost.VideoCaption = "STeVO";
                //videoPost.VideoSize = 


                //postDAl.InsertPhoto1(photoPost, g, aMember);
                postDAl.InsertVideo(videoPost, g, aMember);





                //Response.Clear();
                //Response.ContentType = fileContentType;
                //Response.BinaryWrite(fileContents);
                //Response.End();
            }


            #endregion

        }
        //END PAGE_LOAD



        #region GET POSTS
        private void GetTextPosts()
        {
            Text_Post aText;
            ArrayList ArrayTexts;


            ArrayTexts = bl.GetPost(aPost);
            int c = ArrayTexts.Count;

            for (int j = 0; j < c; ++j)
            {
                aText = (Text_Post)ArrayTexts[j];



                concatinater.Append(aText.PostText);
            }

        }

        private void GetEventPosts()
        {
            Event_Post aEvent;
            ArrayList ArrayEvents;

            ArrayEvents = bl.GetPost(aPost);
            int c = ArrayEvents.Count;

            for (int j = 0; j < c; ++j)
            {
                aEvent = (Event_Post)ArrayEvents[j];

                concatinater.Append("Name: ");
                concatinater.Append(aEvent.EventName);
                concatinater.Append("<br/>");

                concatinater.Append("Details: ");
                concatinater.Append(aEvent.EventDetails);
                concatinater.Append("<br/>");

                concatinater.Append("Type: ");
                concatinater.Append(aEvent.EventType);
                concatinater.Append("<br/>");

                concatinater.Append("Venue: ");
                concatinater.Append(aEvent.EventVenue);
                concatinater.Append("<br/>");

                concatinater.Append("Host: ");
                concatinater.Append(aEvent.Host);
                concatinater.Append("<br/>");

                concatinater.Append("Start Date: ");
                concatinater.Append(reusable_Methods.FormatDateTime(aEvent.StartDate));
                concatinater.Append("<br/>");

                concatinater.Append("End Date: ");
                concatinater.Append(reusable_Methods.FormatDateTime(aEvent.EndDate));
                concatinater.Append("<br/>");


            }

        }

        private void GetPhotoPosts()
        {
            Photo_Post aPhoto;
            ArrayList ArrayPhoto;

            ArrayPhoto = bl.GetPost(aPost);
            int c = ArrayPhoto.Count;

            for (int j = 0; j < c; ++j)
            {
                aPhoto = (Photo_Post)ArrayPhoto[j];




                String img = "DispayImage.ashx?id=" + aPhoto.PostId;

                concatinater.Append("<a class='pointer pictureLaunch' ><img src='");
                concatinater.Append(img);

                concatinater.Append("'alt='Profile picture'  class='BorberRad5 floatLeft photoPostImage thumbnails'  style=' padding: 2px; border: 1px solid #A3ADB5;'/></a> <div class='clr' ></div> <p class='caption'> ");


                concatinater.Append(aPhoto.PhotoCaption);
                concatinater.Append("</p>");

            }

        }

        private void GetArticlePosts()
        {
            Article_Post aArticle;
            ArrayList ArrayArticle;

            ArrayArticle = bl.GetPost(aPost);
            int c = ArrayArticle.Count;

            for (int j = 0; j < c; ++j)
            {
                aArticle = (Article_Post)ArrayArticle[j];


                concatinater.Append(aArticle.PostId + "<br/>");
                concatinater.Append(aArticle.Title + "<br/>");
                concatinater.Append(aArticle.ArticleText + "<br/>");
            }

        }

        private void GetVideoPosts()
        {
            Video_Post aVideo;
            ArrayList ArrayVideo;

            ArrayVideo = bl.GetPost(aPost);
            int c = ArrayVideo.Count;

            for (int j = 0; j < c; ++j)
            {
                aVideo = (Video_Post)ArrayVideo[j];


                ////concatinater.Append(aVideo.PostId + "<br/>");
                ////concatinater.Append(aVideo.Video + "<br/>");
                concatinater.Append(aVideo.VideoCaption + "<br/>");
                //concatinater.Append(aVideo.VideoSize + "<br/>");
                // aVideo.VideoName
                concatinater.Append("<a href='#' class='videoLaunch'>");
                concatinater.Append(aVideo.VideoName);
                concatinater.Append("</a>");

                //string videoUrl = "ProcessVideo.ashx?id=" + aVideo.PostId;
                //concatinater.Append("<video  controls='controls'>");
                //concatinater.Append("<source src='");
                //concatinater.Append(videoUrl);
                //concatinater.Append("' type='video/mp4' />");
                //concatinater.Append("Your browser does not support the video tag.");
                //concatinater.Append("</video>");

            }

        }

        private void GetFilePosts()
        {
            File_Post aFile;
            ArrayList ArrayFile;

            ArrayFile = bl.GetPost(aPost);
            int c = ArrayFile.Count;

            for (int j = 0; j < c; ++j)
            {
                aFile = (File_Post)ArrayFile[j];

                concatinater.Append(aFile.FileCaption + "<br/><br/>");
                concatinater.Append("File size: ");
                concatinater.Append(reusable_Methods.FormatFileSize(aFile.FileSize) + "<br/>");
                concatinater.Append("Download file <a class='downloadFile' ");
                //concatinater.Append(fileUrl);
                concatinater.Append(" ><u>");
                concatinater.Append(aFile.FileName);
                concatinater.Append("</u></a>");
                //concatinater.Append(aFile.File + "<br/>");
            }

        }
        #endregion

        #region WRAPPERS
        private void OpenWrapper(int i)
        {
            #region OPENING WRAPPER
            aPost = (Post)ArrayPosts[i];

            ArrayMembers = bl.GetMemberDisplayName(aPost.MemberId);
            countMembers = ArrayMembers.Count;

            for (int a = 0; a < countMembers; ++a)
            {
                aMember = (Member)ArrayMembers[a];

                NotificationDAL notificationDAL = new NotificationDAL();
                Group group = new Group(aPost.GroupId);

                string groupDescription = notificationDAL.GetGroupDescription2(group);

                concatinater.Append("<div class='aPost typeOfText' id='");
                concatinater.Append(aPost.PostId);
                concatinater.Append("'><div class='msgeHeading ui-corner-all' ><h2 class='ui-corner-all'> Posted by: <a href='#' id='");
                concatinater.Append(aPost.MemberId);
                concatinater.Append("' class='btnMemberProfile' style='font-weight:normal; '>");
                concatinater.Append(aMember.DisplayName);

                bool isForMember = false;

                string date = "";



                date = reusable_Methods.FormatDateTime(aPost.CreateDate);

                if (aPost.MemberId == HttpContext.Current.Session["memberID"].ToString())
                { isForMember = true; }

                if (GroupID == aPost.GroupId)
                {


                }
                else
                {
                    concatinater.Append("</a> <span class='space'></span> in group: <a id='");
                    concatinater.Append(aPost.GroupId);

                    concatinater.Append("' class='btnGoToGroupPage groupDesc'>");
                    concatinater.Append(groupDescription);

                }

                concatinater.Append("</a>");
                if (isForMember)
                {
                    concatinater.Append("<span class='ui-icon ui-icon-closethick floatright deletePost pointer ' title ='Delete this post' style='display:none;'></span>");
                }

                concatinater.Append("<span class='DateWithTime ui-corner-all floatright'>");
                concatinater.Append(date);
                //concatinater.Append(Convert.ToDateTime(aPost.CreateDate).ToString("D"));
                concatinater.Append("</span></h2></div> <div class='postContentArea'> <div> ");

            }

            #endregion
        }

        private void CloseWrapper()
        {
            #region CLOSING WRAPPER

            int numberofPostLikes = bl.CountPostLikes(aPost);
            int numberofPostTags = bl.CountPostTags(aPost);

            //ContentArea Closing AND Wrapper oppening+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            concatinater.Append("<div class='clr' ></div> </div> </div><div class='commentWrapper'> ");

            //buttons  Right
            concatinater.Append("<div class='btnHomePost width50% floatright '><a class='button left big floatLeft homeLikeBtn");

            RateAndTagDAL dal = new RateAndTagDAL();

            //aMember = new Member(memberId);

            //Check if member not liked
            if (dal.PostRatingExists(aMember, aPost) == 0)
            {
                //if member not liked 
                //concatinater.Append(" homeLikeBtn");
                concatinater.Append("' title='");
                concatinater.Append("Like this post");
                concatinater.Append("'  >");
                concatinater.Append("Like");
            }
            else
            {
                //if member is liked
                concatinater.Append("' title='");
                concatinater.Append("Unlike this post");
                concatinater.Append("'  >");
                concatinater.Append("Unlike");
            }

            //Post button
            concatinater.Append("</a><a class='button middle big floatLeft homeCommentBtn' title='Comment on this post' >Comment</a>");
            //report Button
            concatinater.Append("<a class='button middle big floatLeft reportPost' title='Report this post' >Report</a>");
            //tag button
            concatinater.Append("<a class='button right big floatLeft homeTagBtn open tag'  title='Tag a friend'  >Tag </a>");

            concatinater.Append("</div>");

            List<Member> AllLikedMembers = dal.GetPeopleLikedPost(aPost);
            //Details Left
            concatinater.Append("<div class='postinfo width50% floatLeft tip'> <a title='");

            foreach (var Member in AllLikedMembers)
            {

                concatinater.Append(Member.DisplayName);
                concatinater.Append("<br />");
            }

            concatinater.Append("' href='#'>Likes</a>: <span class='likesCount'> " + numberofPostLikes);
            concatinater.Append("</span>, <a href='#'>Tags</a>: <span class='tagsCount'>" + numberofPostTags);

            concatinater.Append("</span>,   <a  class='btnViewComments' title='View all comments'>Comments</a>: <span class='commentCount'>" + bl.CountComments(aPost) + "</span> </div>");


            concatinater.Append("</div>" // Wrapper End

                                  //Buiding the  Comments section
                                 + "<div class='allComments'><h4>All Comments</h4></div>"
                //Buiding the  Comments section
                                 + "<div class='commentSection' style='display:none; padding: 7px;'><div>"
                                 + "<textarea title='Write a comment...'  placeholder='Write a comment...' class=' commentBox floatLeft input BorberRad3'"
                                 + " style='width: 100%; height: 30px; font-size: 0.85em; margin: 5px 0pt 7px;' rows='2' cols='20'></textarea>"
                                 + " <a class='button big floatLeft btnSendComment floatright '  style='font-size: 0.9em; margin-right: -10px; '  >Comment</a></div></div></div>");

            #endregion
        }
        #endregion

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
        protected void PictureUploader_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
        {



            Session["fileContentType_" + file.FileId] = file.ContentType;
            Session["fileContents_" + file.FileId] = file.GetContents();

            Session["fileName_" + file.FileId] = file.FileName.Split('\\').Last();
            string fileName = file.FileName.Split('\\').Last();
            string[] fileExtensionToSplit = fileName.Split('.');

            string fileExtension = fileExtensionToSplit[fileExtensionToSplit.Length - 1];
            fileExtension = fileExtension.ToLower();

            if (fileExtension.Contains("mp4") || fileExtension.Contains("mov") || fileExtension.Contains("ogv") || fileExtension.Contains("wmv") || fileExtension.Contains("vlc") || fileExtension.Contains("avi") || fileExtension.Contains("mkv"))
            {
                SaveVideo(file);
            }
            else if (fileExtension.Contains("jpg") || fileExtension.Contains("gif") || fileExtension.Contains("png") || fileExtension.Contains("jpeg"))
            {

                SavePicture(file);
            }
            else
            {


                SaveFile(file);

            }

         
        }

        [WebMethod]
        public static void GetTCaption(string caption)
        {
            captionP = caption;
          
        }
        protected void SavePicture(AjaxFileUploadEventArgs file)
        {
           

            byte[] imageBytes = file.GetContents();
            string fileName =  file.FileName.Split('\\').Last();

            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);

            Photo_Post photoPost = new Photo_Post();
            Group g = new Group();
            PostDAL postDAl = new PostDAL();

            aMember.MemberId = (string)Session["memberID"];
            g.GroupId = int.Parse(ThisGroupsID.Value);

            photoPost.Photo = image;
            photoPost.PhotoName = fileName;
            photoPost.PhotoCaption = captionP;
            postDAl.InsertPhoto(photoPost, g, aMember);
  
          
         

        }
        public string PostToClients { get; set; }
        protected void SaveVideo(AjaxFileUploadEventArgs file)
        {

            byte[] videoBytes = file.GetContents();
            string fileName = file.FileName.Split('\\').Last();

            Video_Post videoPost = new Video_Post();
            Group g = new Group();
            PostDAL postDAl = new PostDAL();

            aMember.MemberId = (string)Session["memberID"];
            g.GroupId = int.Parse(ThisGroupsID.Value);

            videoPost.Video = videoBytes;
            videoPost.VideoName = fileName;
            videoPost.VideoSize = file.FileSize;
            videoPost.VideoCaption = captionP;

            postDAl.InsertVideo(videoPost, g, aMember);
        }
        protected void SaveFile(AjaxFileUploadEventArgs file)
        {

            byte[] fileBytes = file.GetContents();
            string fileName = file.FileName.Split('\\').Last();

            File_Post filePost = new File_Post();
            Group g = new Group();
            PostDAL postDAl = new PostDAL();

            aMember.MemberId = (string)Session["memberID"];
            g.GroupId = int.Parse(ThisGroupsID.Value);


            filePost.File = fileBytes;
            filePost.FileName = fileName;
            filePost.FileSize = file.FileSize;

            filePost.FileCaption = captionP;

            postDAl.InsertFile(filePost, g, aMember);


        }
        protected void FileUploader_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
        {
            Session["fileContentType_" + file.FileId] = file.ContentType;
            Session["fileContents_" + file.FileId] = file.GetContents();

            Session["fileName_" + file.FileId] = file.FileName.Split('\\').Last();

            file.PostedUrl = string.Format("?preview=1&fileId={0}", file.FileId);
            uploadType = captionP;
        }
        protected void VideoUploader_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
        {
            Session["fileContentType_" + file.FileId] = file.ContentType;
            Session["fileContents_" + file.FileId] = file.GetContents();

            Session["fileName_" + file.FileId] = file.FileName.Split('\\').Last();

            file.PostedUrl = string.Format("?preview=1&fileId={0}", file.FileId);
            uploadType = captionP;
        }



    }
}