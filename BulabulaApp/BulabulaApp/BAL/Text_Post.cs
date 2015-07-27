using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulabulaApp
{
    public class Text_Post
    {
        private int postId;
        private string postText;

        public Text_Post(int postId, string postText)
        {
            PostId = postId;
            PostText = postText;
        }
        public Text_Post(string postText)
        {
            PostText = postText;
        }
        public Text_Post(int postId)
        {
            PostId = postId;
        }
        #region PROPERTIES

        public int PostId
        {
            get { return postId; }
            set { postId = value; }
        }

        public string PostText
        {
            get { return postText; }
            set { postText = value; }
        }

        #endregion
    }
}
