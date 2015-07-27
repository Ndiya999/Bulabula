using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulabulaApp
{
    class Response
    {
        private int responseId;
        private string responseAnswer;
        private string memberId;
        private int questionId;

        public Response(int responseId, string responseAnswer, string memberId, int questionId)
        {
            ResponseId = responseId;
            ResponseAnswer = responseAnswer;
            MemberId = memberId;
            QuestionId = questionId;
        }
        public Response(string responseAnswer, string memberId, int questionId)
        {
            ResponseAnswer = responseAnswer;
            MemberId = memberId;
            QuestionId = questionId;
        }

        #region PROPERTIES

        public int ResponseId
        {
            get { return responseId; }
            set { responseId = value; }
        }

        public string ResponseAnswer
        {
            get { return responseAnswer; }
            set { responseAnswer = value; }
        }

        public string MemberId
        {
            get { return memberId; }
            set { memberId = value; }
        }

        public int QuestionId
        {
            get { return questionId; }
            set { questionId = value; }
        }

        #endregion

        internal static void Redirect(string p)
        {
            throw new NotImplementedException();
        }
    }
}
