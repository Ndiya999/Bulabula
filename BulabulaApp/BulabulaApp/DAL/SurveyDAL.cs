using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration; //For Connectionstring configuration
using System.Data; // For non-provider specific data objects
using System.Data.SqlClient; // For Sql Server specific data objects
using System.Data.SqlTypes; // For Sql types required for command parameters
using System.Collections;
using System.IO;

namespace BulabulaApp
{
    class SurveyDAL
    {
        private string connectionString;

        public SurveyDAL()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        }

        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }
        public bool InsertSurvey(Survey survey, Question question)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspAddNewSurvey", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SurveyName", survey.SurveyName);
                cmd.Parameters.AddWithValue("@SurveyStartDate", survey.SurveyStartDate);
                cmd.Parameters.AddWithValue("@SurveyEndDate", survey.SurveyEndDate);
                cmd.Parameters.AddWithValue("@AdministratorId", survey.AdministratorId);


                cmd.Parameters.AddWithValue("@Question1Text", question.Question1Text);
                cmd.Parameters.AddWithValue("@Question2Text", question.Question2Text);
                cmd.Parameters.AddWithValue("@Question3Text", question.Question3Text);
                cmd.Parameters.AddWithValue("@Question4Text", question.Question4Text);
                cmd.Parameters.AddWithValue("@Question5Text", question.Question5Text);

                cmd.Parameters.AddWithValue("@Question1Option1", question.Question1option[0]);
                cmd.Parameters.AddWithValue("@Question1Option2", question.Question1option[1]);
                cmd.Parameters.AddWithValue("@Question1Option3", question.Question1option[2]);

                cmd.Parameters.AddWithValue("@Question2Option1", question.Question2option[0]);
                cmd.Parameters.AddWithValue("@Question2Option2", question.Question2option[1]);
                cmd.Parameters.AddWithValue("@Question2Option3", question.Question2option[2]);

                cmd.Parameters.AddWithValue("@Question3Option1", question.Question3option[0]);
                cmd.Parameters.AddWithValue("@Question3Option2", question.Question3option[1]);
                cmd.Parameters.AddWithValue("@Question3Option3", question.Question3option[2]);

                cmd.Parameters.AddWithValue("@Question4Option1", question.Question4option[0]);
                cmd.Parameters.AddWithValue("@Question4Option2", question.Question4option[1]);
                cmd.Parameters.AddWithValue("@Question4Option3", question.Question4option[2]);

                cmd.Parameters.AddWithValue("@Question5Option1", question.Question5option[0]);
                cmd.Parameters.AddWithValue("@Question5Option2", question.Question5option[1]);
                cmd.Parameters.AddWithValue("@Question5Option3", question.Question5option[2]);

                try
                {
                    con.Open();
                    int x = cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    
                    e.Message.ToString();
                    //return false;
                    throw new Exception(e.Message.ToString());
                }
            }
        }
        public List<Survey> GetAllSurveys()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetSurveysBetweenStartDateAndEndDate", con);
                cmd.CommandType = CommandType.StoredProcedure;

                List<Survey> SurveyList = new List<Survey>();

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Survey survey = new Survey(int.Parse(reader["SurveyID"].ToString()), Convert.ToString(reader["SurveyName"]), Convert.ToDateTime(reader["SurveyDateStart"]), Convert.ToDateTime(reader["SurveyDateEnd"]), Convert.ToString(reader["AdministratorID"]));
                        SurveyList.Add(survey);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return SurveyList;
            }
        }
        public List<Question> GetQuestionsBySurvey(Survey survey)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetQuestionsBySurvey", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SurveyID", survey.SurveyId);

                List<Question> QuestionList = new List<Question>();

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Question question = new Question(int.Parse(reader["QuestionID"].ToString()), Convert.ToString(reader["QuestionText"]), Convert.ToString(reader["Option1"]), Convert.ToString(reader["Option2"]), Convert.ToString(reader["Option3"]));
                        QuestionList.Add(question);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return QuestionList;
            }
        }
        public List<Survey> GetSelectedSurvey(Survey survey)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("uspGetSelectedSurvey", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SurveyID", survey.SurveyId);

                List<Survey> SurveyList = new List<Survey>();

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Survey aSurvey = new Survey(int.Parse(reader["SurveyID"].ToString()), Convert.ToString(reader["SurveyName"]), Convert.ToDateTime(reader["SurveyDateStart"]), Convert.ToDateTime(reader["SurveyDateEnd"]), Convert.ToString(reader["AdministratorID"]));
                        SurveyList.Add(aSurvey);
                    }//End while
                    reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                return SurveyList;
            }
        }
        public bool InsertSurveyResponse(Response response)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("uspInsertSurveyResponse", con);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@ResponseAnswer", response.ResponseAnswer);
                cmd.Parameters.AddWithValue("@MemberID", response.MemberId);
                cmd.Parameters.AddWithValue("@QuestionID", response.QuestionId);


                try
                {
                    con.Open();
                    int x = cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {

                    e.Message.ToString();
                    //return false;
                    throw new Exception(e.Message.ToString());
                }
            }
        }
    }
}
