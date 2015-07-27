using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BulabulaApp
{
	public class Comments
	{
        private int commentid;
        private string commentText;
        private DateTime createDate;
        private DateTime deleteDate;
        private int postId;
        private string memberId;

        public Comments()
        {

        }
        public Comments(int commentid, string commentText, DateTime createDate, int postId, string memberId)
        {
            Commentid = commentid;
            CommentText = commentText;
            CreateDate = createDate;
            DeleteDate = deleteDate;
            PostId = postId;
            MemberId = memberId;
        }
        public Comments(string commentText, DateTime createDate, int postId)
        {
            CommentText = commentText;
            CreateDate = createDate;
            PostId = postId;
        }
        public Comments(int commentid)
        {
            Commentid = commentid;
        }

        #region PROPERTIES

        public int Commentid
        {
            get { return commentid; }
            set { commentid = value; }
        }

        public string CommentText
        {
            get { return commentText; }
            set { commentText = value; }
        }

        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        public DateTime DeleteDate
        {
            get { return deleteDate; }
            set { deleteDate = value; }
        }

        public int PostId
        {
            get { return postId; }
            set { postId = value; }
        }

        public string MemberId
        {
            get { return memberId; }
            set { memberId = value; }
        }

        #endregion
	}
}