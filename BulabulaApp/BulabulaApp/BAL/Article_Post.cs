using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulabulaApp
{
    public class Article_Post
    {
        private int postId;
        private string title;
        private string articleText;
        public DateTime CreateDate {get; set;}
        public string MemberID { get; set; }

        public Article_Post(int postId, string title, string articleText)
        {
            PostId = postId;
            Title = title;
            ArticleText = articleText;
        }

         public Article_Post(int postId, string articleText, DateTime d, string m)
        {
            PostId = postId;
           
            ArticleText = articleText;
             CreateDate = d;
            MemberID =  m;
        }

       // Post.PostID ,[CreateDate] ,[GroupID] ,[MemberID] ,[IsFlaged]  ,[PostType], [ArticleText]


        public Article_Post(int postId,  string articleText)
        {
            PostId = postId;
            ArticleText = articleText;
        }
        public Article_Post(string articleText)
        {
            ArticleText = articleText;
        }
        public Article_Post()
        {
            
        }

        #region PROPERTIES

        public int PostId
        {
            get { return postId; }
            set { postId = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string ArticleText
        {
            get { return articleText; }
            set { articleText = value; }
        }

        #endregion
    }
}
