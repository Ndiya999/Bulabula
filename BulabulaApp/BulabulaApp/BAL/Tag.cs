using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulabulaApp
{
    class Tag
    {
        private int tagId;
        private int postId;
        private int friendId;

        public Tag(int tagId, int postId, int friendId)
        {
            TagId = tagId;
            PostId = postId;
            FriendId = friendId;
        }

        #region PROPERTIES

        public int TagId
        {
            get { return tagId; }
            set { tagId = value; }
        }

        public int PostId
        {
            get { return postId; }
            set { postId = value; }
        }

        public int FriendId
        {
            get { return friendId; }
            set { friendId = value; }
        }

        #endregion

    }
}
