using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulabulaApp
{
    class Rating
    {
        private int ratingId;
        private bool islike;
        private int postId;
        private int memberId;
        private int commentId;

        public Rating(int ratingId, bool islike, int postId, int memberId, int commentId)
        {
            RatingId = ratingId;
            Islike = islike;
            PostId = postId;
            MemberId = memberId;
            CommentId = commentId;
        }
        public Rating(int ratingId)
        {
            RatingId = ratingId;
        }
        public Rating()
        {

        }

        #region PROPERTIES

        public int RatingId
        {
            get { return ratingId; }
            set { ratingId = value; }
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

        #endregion
    }
}
