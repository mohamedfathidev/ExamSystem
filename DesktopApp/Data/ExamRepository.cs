using ITI_DB.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ITI_DB.Data
{
    public class ExamRepository
    {
        public static List<Exam> GetCourseExams(int courseId, int studentId)
        {
            // Get exams that student hasn't taken yet
            string query = @"
                SELECT e.ExamID, e.ExamDate, e.TotalDegree, e.Duration, e.CourseID, c.CourseName
                FROM Exam e
                INNER JOIN Course c ON e.CourseID = c.CourseID
                WHERE e.CourseID = @CourseID
                AND e.ExamID NOT IN (
                    SELECT ExamID FROM StudentResult WHERE StudentID = @StudentID
                )";

            SqlParameter[] parameters = {
                new SqlParameter("@CourseID", courseId),
                new SqlParameter("@StudentID", studentId)
            };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
            List<Exam> exams = new List<Exam>();

            foreach (DataRow row in dt.Rows)
            {
                exams.Add(new Exam
                {
                    ExamID = Convert.ToInt32(row["ExamID"]),
                    ExamDate = Convert.ToDateTime(row["ExamDate"]),
                    TotalDegree = Convert.ToDecimal(row["TotalDegree"]),
                    Duration = Convert.ToInt32(row["Duration"]),
                    CourseID = Convert.ToInt32(row["CourseID"]),
                    CourseName = row["CourseName"].ToString()
                });
            }

            return exams;
        }

        public static List<Question> GetExamQuestions(int examId)
        {
            string query = @"
                SELECT q.QuestionID, q.QuestionText, q.QuestionType, q.Degree, q.CourseID
                FROM Question q
                INNER JOIN ExamQuestion eq ON q.QuestionID = eq.QuestionID
                WHERE eq.ExamID = @ExamID
                ORDER BY NEWID()"; 

            SqlParameter[] parameters = {
                new SqlParameter("@ExamID", examId)
            };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
            List<Question> questions = new List<Question>();

            foreach (DataRow row in dt.Rows)
            {
                int questionId = Convert.ToInt32(row["QuestionID"]);
                Question question = new Question
                {
                    QuestionID = questionId,
                    QuestionText = row["QuestionText"].ToString(),
                    QuestionType = row["QuestionType"].ToString(),
                    Degree = Convert.ToDecimal(row["Degree"]),
                    CourseID = Convert.ToInt32(row["CourseID"]),
                    Choices = GetQuestionChoices(questionId)
                };

                questions.Add(question);
            }

            return questions;
        }

        public static List<Choice> GetQuestionChoices(int questionId)
        {
            string query = @"
                SELECT QuestionID, ChoiceNo, ChoiceText, IsCorrect
                FROM Choice
                WHERE QuestionID = @QuestionID
                ORDER BY ChoiceNo";

            SqlParameter[] parameters = {
                new SqlParameter("@QuestionID", questionId)
            };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
            List<Choice> choices = new List<Choice>();

            foreach (DataRow row in dt.Rows)
            {
                choices.Add(new Choice
                {
                    QuestionID = Convert.ToInt32(row["QuestionID"]),
                    ChoiceNo = row["ChoiceNo"].ToString(),
                    ChoiceText = row["ChoiceText"].ToString(),
                    IsCorrect = Convert.ToBoolean(row["IsCorrect"])
                });
            }

            return choices;
        }

        public static void SaveExamResult(int studentId, int examId, List<Question> questions, decimal gradePercent)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    string resultQuery = @"
                        INSERT INTO StudentResult (StudentID, ExamID, GradePercent)
                        VALUES (@StudentID, @ExamID, @GradePercent)";

                    using (SqlCommand cmd = new SqlCommand(resultQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@StudentID", studentId);
                        cmd.Parameters.AddWithValue("@ExamID", examId);
                        cmd.Parameters.AddWithValue("@GradePercent", gradePercent);
                        cmd.ExecuteNonQuery();
                    }

                    foreach (var question in questions)
                    {
                        foreach (var choiceNo in question.SelectedChoices)
                        {
                            string answerQuery = @"
                                INSERT INTO StudentAnswer (StudentID, ExamID, QuestionID, Answer)
                                VALUES (@StudentID, @ExamID, @QuestionID, @Answer)";

                            using (SqlCommand cmd = new SqlCommand(answerQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@StudentID", studentId);
                                cmd.Parameters.AddWithValue("@ExamID", examId);
                                cmd.Parameters.AddWithValue("@QuestionID", question.QuestionID);
                                cmd.Parameters.AddWithValue("@Answer", choiceNo.ToString());
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public static ExamResult CalculateExamResult(int studentId, int examId, List<Question> questions)
        {
            decimal totalMarks = 0;
            decimal obtainedMarks = 0;

            ExamResult result = new ExamResult
            {
                StudentID = studentId,
                ExamID = examId,
                CompletedDate = DateTime.Now
            };

            foreach (var question in questions)
            {
                totalMarks += question.Degree;

                var correctChoices = question.Choices.Where(c => c.IsCorrect).Select(c => c.ChoiceNo).ToList();
                var studentAnswers = question.SelectedChoices;

                bool isCorrect = correctChoices.Count == studentAnswers.Count &&
                                correctChoices.All(c => studentAnswers.Contains(c));

                QuestionResult qResult = new QuestionResult
                {
                    QuestionID = question.QuestionID,
                    QuestionText = question.QuestionText,
                    Marks = question.Degree,
                    Choices = question.Choices,
                    StudentAnswers = studentAnswers,
                    CorrectAnswers = correctChoices,
                    IsCorrect = isCorrect
                };

                if (isCorrect)
                {
                    obtainedMarks += question.Degree;
                }

                result.QuestionResults.Add(qResult);
            }

            result.TotalMarks = totalMarks;
            result.ObtainedMarks = obtainedMarks;
            result.GradePercent = totalMarks > 0 ? (obtainedMarks / totalMarks) * 100 : 0;

            return result;
        }
    }
}
