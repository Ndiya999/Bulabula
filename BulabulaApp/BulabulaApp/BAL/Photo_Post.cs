using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BulabulaApp
{
    public  class Photo_Post
    {
        private int postId;
        private Image photo;
        private string photoCaption;
        private string photoName;

        public Photo_Post(int postId, Image photo, string photoCaption, string photoName)
        {
            PostId = postId;
            Photo = photo;
            PhotoCaption = photoCaption;
            PhotoName = photoName;
        }
        public Photo_Post(int postId, string photoCaption, string photoName)
        {
            PostId = postId;
            PhotoCaption = photoCaption;
            PhotoName = photoName;
        }
        public Photo_Post(string photoCaption)
        {
            PhotoCaption = photoCaption;
        }
        public Photo_Post()
        { }

        #region Properties

        public int PostId
        {
            get { return postId; }
            set { postId = value; }
        }

        public Image Photo
        {
            get { return photo; }
            set { photo = value; }
        }

        public string PhotoCaption
        {
            get { return photoCaption; }
            set { photoCaption = value; }
        }
        public string PhotoName
        {
            get { return photoName; }
            set { photoName = value; }
        }
        #endregion
    }
}
