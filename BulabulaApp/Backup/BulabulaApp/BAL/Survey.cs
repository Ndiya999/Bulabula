using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulabulaApp
{
    class Survey
    {
        private int surveyId;
        private string surveyName;
        private DateTime surveyStartDate;
        private DateTime surveyEndDate;
        private string administratorId;

        public Survey(int surveyId, string surveyName, DateTime surveyStartDate, DateTime surveyEndDate, string administratorId)
        {
            SurveyId = surveyId;
            SurveyName = surveyName;
            SurveyStartDate = surveyStartDate;
            SurveyEndDate = surveyEndDate;
            AdministratorId = administratorId;
        }
        public Survey(string surveyName, DateTime surveyStartDate, DateTime surveyEndDate, string administratorId)
        {
            SurveyName = surveyName;
            SurveyStartDate = surveyStartDate;
            SurveyEndDate = surveyEndDate;
            AdministratorId = administratorId;
        }
        public Survey(int surveyId)
        {
            SurveyId = surveyId;
        }
        #region PROPERTIES

        public int SurveyId
        {
            get { return surveyId; }
            set { surveyId = value; }
        }

        public string SurveyName
        {
            get { return surveyName; }
            set { surveyName = value; }
        }

        public DateTime SurveyStartDate
        {
            get { return surveyStartDate; }
            set { surveyStartDate = value; }
        }

        public DateTime SurveyEndDate
        {
            get { return surveyEndDate; }
            set { surveyEndDate = value; }
        }
        public string AdministratorId
        {
            get { return administratorId; }
            set { administratorId = value; }
        }
        #endregion
    }
}
