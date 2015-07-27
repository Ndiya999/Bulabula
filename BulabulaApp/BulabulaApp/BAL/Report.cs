using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulabulaApp
{
    class Report
    {
        private int reportId;
        private bool islike;
        private int postId;
        private int messageId;
        private int memberId;
        private int commentId;
        private DateTime dateTime;
        private string option;

        public Report(int reportId, bool islike, int postId, int messageId, int memberId, int commentId, DateTime dateTime, string option)
        {
            ReportId = reportId;
            Islike = islike;
            PostId = postId;
            MessageId = messageId;
            MessageId = messageId;
            CommentId = commentId;
            DateTime = dateTime;
            Option = option;
        }

        #region PROPERTIES

        public int ReportId
        {
            get { return reportId; }
            set { reportId = value; }
        }

        public bool Islike
        {
            get { return islike; }
            set { islike = value; }
        }

        public int PostId
        {
            get { return postId; }
            set { postId = value; }
        }

        public int MessageId
        {
            get { return messageId; }
            set { messageId = value; }
        }

        public int MemberId
        {
            get { return memberId; }
            set { memberId = value; }
        }

        public int CommentId
        {
            get { return commentId; }
            set { commentId = value; }
        }

        public DateTime DateTime
        {
            get { return dateTime; }
            set { dateTime = value; }
        }

        public string Option
        {
            get { return option; }
            set { option = value; }
        }

        #endregion

    }
}
