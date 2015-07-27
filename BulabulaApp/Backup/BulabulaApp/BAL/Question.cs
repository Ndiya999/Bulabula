using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulabulaApp
{
    class Question
    {
        private int questionId;
        private string questionText;
        private string option1;
        private string option2;
        private string option3;
        private int surveyId;

        private string question1Text; private string[] question1option;
        private string question2Text; private string[] question2option;
        private string question3Text; private string[] question3option;
        private string question4Text; private string[] question4option;
        private string question5Text; private string[] question5option;

        public Question(string question1Text, string[] question1option,
                        string question2Text, string[] question2option,
                        string question3Text, string[] question3option,
                        string question4Text, string[] question4option,
                        string question5Text, string[] question5option)
        {
            Question1Text = question1Text;
            Question2Text = question2Text;
            Question3Text = question3Text;
            Question4Text = question4Text;
            Question5Text = question5Text;

            Question1option = question1option;
            Question2option = question2option;
            Question3option = question3option;
            Question4option = question4option;
            Question5option = question5option;
        }
        public Question(int questionId, string questionText, string option1, string option2, string option3, int surveyId)
        {
            QuestionId = questionId;
            QuestionText = questionText;
            Option1 = option1;
            Option2 = option2;
            Option3 = option3;
            SurveyId = surveyId;
        }
        public Question(int questionId, string questionText, string option1, string option2, string option3)
        {
            QuestionId = questionId;
            QuestionText = questionText;
            Option1 = option1;
            Option2 = option2;
            Option3 = option3;
        }

        #region PROPERTIES

        public int QuestionId
        {
            get { return questionId; }
            set { questionId = value; }
        }

        public int SurveyId
        {
            get { return surveyId; }
            set { surveyId = value; }
        }
        public string QuestionText
        {
            get { return questionText; }
            set { questionText = value; }
        }
        public string Option1
        {
            get { return option1; }
            set { option1 = value; }
        }
        public string Option2
        {
            get { return option2; }
            set { option2 = value; }
        }
        public string Option3
        {
            get { return option3; }
            set { option3 = value; }
        }
        public string Question1Text
        {
            get { return question1Text; }
            set { question1Text = value; }
        }

        public string[] Question1option
        {
            get { return question1option; }
            set { question1option = value; }
        }


        public string Question2Text
        {
            get { return question2Text; }
            set { question2Text = value; }
        }

        public string[] Question2option
        {
            get { return question2option; }
            set { question2option = value; }
        }


        public string Question3Text
        {
            get { return question3Text; }
            set { question3Text = value; }
        }

        public string[] Question3option
        {
            get { return question3option; }
            set { question3option = value; }
        }


        public string Question4Text
        {
            get { return question4Text; }
            set { question4Text = value; }
        }

        public string[] Question4option
        {
            get { return question4option; }
            set { question4option = value; }
        }


        public string Question5Text
        {
            get { return question5Text; }
            set { question5Text = value; }
        }

        public string[] Question5option
        {
            get { return question5option; }
            set { question5option = value; }
        }

        #endregion
    }
}