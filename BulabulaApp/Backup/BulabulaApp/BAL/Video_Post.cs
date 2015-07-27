using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulabulaApp
{
    public class Video_Post
    {
        private int postId;
        private byte[] video;
        private string videoCaption;
        private long videoSize;
        private string videoName;


        public Video_Post(int postId, byte[] video, string videoCaption, long videoSize, string videoName)
        {
            PostId = postId;
            Video = video;
            VideoCaption = videoCaption;
            VideoSize = videoSize;
            VideoName = videoName;
        }
        public Video_Post(int postId, string videoCaption, long videoSize, string videoName)
        {
            PostId = postId;
            VideoCaption = videoCaption;
            VideoSize = videoSize;
            VideoName = videoName;
        }
        public Video_Post(string videoCaption)
        {
            VideoCaption = videoCaption;
        }
        public Video_Post()
        { }
        #region PROPERTIES

        public int PostId
        {
            get { return postId; }
            set { postId = value; }
        }

        public byte[] Video
        {
            get { return video; }
            set { video = value; }
        }

        public string VideoCaption
        {
            get { return videoCaption; }
            set { videoCaption = value; }
        }

        public long VideoSize
        {
            get { return videoSize; }
            set { videoSize = value; }
        }

        public string VideoName
        {
            get { return videoName; }
            set { videoName = value; }
        }

        #endregion

    }
}
