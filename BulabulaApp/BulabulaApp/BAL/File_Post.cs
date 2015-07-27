using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulabulaApp
{
    public  class File_Post
    {
        private int postId;
        private byte[] file;
        private string fileCaption;
        private long fileSize;
        private string fileName;



        public File_Post(int postId, byte[] file, string fileCaption, long fileSize, string fileName)
        {
            PostId = postId;
            File = file;
            FileCaption = fileCaption;
            FileSize = fileSize;
            FileName = fileName;
        }
        public File_Post(int postId, string fileCaption, long fileSize, string fileName)
        {
            PostId = postId;
            FileCaption = fileCaption;
            FileSize = fileSize;
            FileName = fileName;
        }
        public File_Post()
        { }
        public File_Post(string fileCaption)
        {
            FileCaption = fileCaption;
        }

        #region Properties

        public int PostId
        {
            get { return postId; }
            set { postId = value; }
        }

        public byte[] File
        {
            get { return file; }
            set { file = value; }
        }

        public string FileCaption
        {
            get { return fileCaption; }
            set { fileCaption = value; }
        }

        public long FileSize
        {
            get { return fileSize; }
            set { fileSize = value; }
        }
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        #endregion
    }
}
