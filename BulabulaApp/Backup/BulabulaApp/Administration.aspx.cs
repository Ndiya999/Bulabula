using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;

namespace BulabulaApp
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        DateTime today = DateTime.Now;
        ReportsDAL reportsDAL = new ReportsDAL();
        Reusable_Methods reusable_Methods = new Reusable_Methods();

    

        public string numberOfMembers = null, GroupOfNames = null, finalPie = null,
            startDate = "31/01/2012", startTime = "12:00 AM", endDate = "", endTime = null;

     
        public int textPostCount = 0, filePostCount = 0, photoPostCount = 0,
            videoPostCount = 0, articlePostCount = 0, eventPostCount = 0,
            messages = 0, reports = 0, members = 0, posts = 0,
            blockedMembers = 0, blocedMembers = 0;
        string[] result = new string[2];

    

   

        
        protected void Page_Load(object sender, EventArgs e)
        {
            Literal title = (Literal)Master.FindControl("pageTitle");
            title.Text = "Administration";


            endDate = today.ToShortDateString();


           string[] temp = endDate.Split('/');
       
               
               endTime = today.ToString("t");
            //  endDate = temp[0] + "/" + temp[1] + "/" + temp[2];
               endDate = temp[1] + "/" + temp[0] + "/" + temp[2];
               DateTime startDateD = reusable_Methods.CreateDateTime(startDate + " " + startTime);
                DateTime endDateD = reusable_Methods.CreateDateTime(endDate + " " + endTime);
           

          



           


            

            int total = GetTotalOfPosts(startDateD, endDateD);

            finalPie = GetDataForPie(total);




            GenerateSatistics(startDateD, endDateD);

            GetNmbersInGroupsValues(startDateD, endDateD);
      
           
        }
        
        [WebMethod]
        public static string[] RefreshNmbersInGroups(string startDate, string endDate)
        {

            Reusable_Methods reusable_Methods = new Reusable_Methods();

            DateTime startDateD = reusable_Methods.CreateDateTime(startDate);
            DateTime endDateD = reusable_Methods.CreateDateTime(endDate);


            WebForm6 thisWebForm = new WebForm6();
            List<Group> groups = thisWebForm.GetNmbersInGroupsValues(startDateD, endDateD);
            string[] result = new string[2];
            result[0] = thisWebForm.numberOfMembers;
            result[1] = thisWebForm.GroupOfNames;
           
            return result;
        }

        [WebMethod]
        public static string RefreshPie(string startDate, string endDate)
        {

            Reusable_Methods reusable_Methods = new Reusable_Methods();
            DateTime startDateD = reusable_Methods.CreateDateTime(startDate);
            DateTime endDateD = reusable_Methods.CreateDateTime(endDate);

            
            WebForm6 thisWebForm = new WebForm6();
            int total = thisWebForm.GetTotalOfPosts(startDateD, endDateD);

            string result = thisWebForm.GetDataForPie(total);


           
            return result;
        }
        [WebMethod]
        public static string[] UpdateStats(string startDate, string endDate)
        {

            Reusable_Methods reusable_Methods = new Reusable_Methods();
            DateTime startDateD = reusable_Methods.CreateDateTime(startDate);
            DateTime endDateD = reusable_Methods.CreateDateTime(endDate);


            WebForm6 thisWebForm = new WebForm6();
            int total = thisWebForm.GetTotalOfPosts(startDateD, endDateD);

           // string result = thisWebForm.GetDataForPie(total);
            thisWebForm.GenerateSatistics(startDateD, endDateD);
            string[] result = new string[] {thisWebForm.members.ToString(), thisWebForm.posts.ToString(), thisWebForm.messages.ToString(), thisWebForm.reports.ToString(), 
                 thisWebForm.blocedMembers.ToString()};
  
            return result;
        }
        #region Building methods

        private void GenerateSatistics(DateTime startDateD, DateTime endDateD)
        {

            messages = reportsDAL.GetTotalNumberOfMessages(startDateD, endDateD);
            reports = reportsDAL.GetTotalReports(startDateD, endDateD);
            members = reportsDAL.GetTotalNumberOfMembersByDate(startDateD, endDateD);
            blocedMembers = reportsDAL.GetTotalNumberOfBlockedMembersByDate(startDateD, endDateD);
            posts = GetTotalOfPosts(startDateD, endDateD);
            blockedMembers = reportsDAL.GetBlockedMembers(startDateD, endDateD);
        
        }
        //private void GetNmbersInGroupsValues(DateTime sd, DateTime ed)
        //{
        //    DataTable membersInGroups = reportsDAL.GetNumberOfMembersInGroup(sd, ed);
        //    foreach (DataRow row in membersInGroups.Rows)
        //    {
        //        GroupOfNames += "'" + row["GroupDescription"].ToString() + "',";
        //        numberOfMembers += row["Total Members"].ToString() + ",";
        //    }
           
             
        //   GroupOfNames.TrimEnd(',');
        //   numberOfMembers.TrimEnd(',');
           
        //}

        private List<Group> GetNmbersInGroupsValues(DateTime sd, DateTime ed)
        {
           // string[,] array = new string[2,];
            List<Group> membersInGroups = reportsDAL.GetNumberOfMembersInGroup(sd, ed);
            foreach (var element in membersInGroups)
            {
                GroupOfNames += "'" + element.GroupDescription + "',";
                numberOfMembers += element.GroupId + ",";

           }


            GroupOfNames.TrimEnd(',');
            numberOfMembers.TrimEnd(',');

            return membersInGroups;
        }
        public double calPercentage(int current, int maximum)
        {
            return Math.Round(((double)current / (double)maximum) * 100, 2);

        }

        private int GetTotalOfPosts(DateTime sd, DateTime ed)
        {

            textPostCount = reportsDAL.GetTotalTextPost(sd, ed);
            filePostCount = reportsDAL.GetTotalFilePost(sd, ed);
            photoPostCount = reportsDAL.GetTotalPhotoPost(sd, ed);
            eventPostCount = reportsDAL.GetTotalEventPost(sd, ed);
            videoPostCount = reportsDAL.GetTotalVideoPost(sd, ed);
            articlePostCount = reportsDAL.GetTotalArticlePost(sd, ed);

            int total = (textPostCount + filePostCount + filePostCount + eventPostCount + videoPostCount + articlePostCount);
            return total;

        }

        private string GetDataForPie(int total)
        {
            string result = +calPercentage(textPostCount, total)
                 + ", " + calPercentage(articlePostCount, total)
            + ", " + calPercentage(eventPostCount, total)
            + ", " + calPercentage(videoPostCount, total)
            + ", " + calPercentage(photoPostCount, total)
            + ", " + calPercentage(filePostCount, total);
            return result;
        }
        #endregion

    }
}