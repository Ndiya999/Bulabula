using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BulabulaApp
{
    public class Post
    {

        private int postId;
        private DateTime createDate;
        private DateTime deleteDate;
        private int groupId;
        private string memberId;
        private bool isFlaged;
        private string postType;


        public Post(int postId, DateTime createDate, int groupId, string memberId, string postType)
        {
            PostId = postId;
            CreateDate = createDate;
            DeleteDate = deleteDate;
            GroupId = groupId;
            MemberId = memberId;
            IsFlaged = isFlaged;
            PostType = postType;
        }
        public Post()
        {

        }
        public Post(int postId)
        {
            PostId = postId;
        }
        public Post(string postType)
        {
            PostType = postType;
        }
        public Post(int postId, string postType)
        {
            PostId = postId;
            PostType = postType;
        }
        public Post(DateTime createDate)
        {
            CreateDate = createDate;
        }

        #region PROPERTIES
        public int PostId
        {
            get { return postId; }
            set { postId = value; }
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

        public int GroupId
        {
            get { return groupId; }
            set { groupId = value; }
        }

        public string MemberId
        {
            get { return memberId; }
            set { memberId = value; }
        }

        public bool IsFlaged
        {
            get { return isFlaged; }
            set { isFlaged = value; }
        }

        public string PostType
        {
            get { return postType; }
            set { postType = value; }
        }
        #endregion
    }
}