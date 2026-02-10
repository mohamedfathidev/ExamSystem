using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_DB.Models
{
    public class ExamResult
    {
        public int StudentID { get; set; }
        public int ExamID { get; set; }
        public decimal GradePercent { get; set; }
        public decimal ObtainedMarks { get; set; }
        public decimal TotalMarks { get; set; }
        public DateTime CompletedDate { get; set; }
        public List<QuestionResult> QuestionResults { get; set; }

        public ExamResult()
        {
            QuestionResults = new List<QuestionResult>();
        }
    }

    public class QuestionResult
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public decimal Marks { get; set; }
        public List<Choice> Choices { get; set; }
        public List<string> StudentAnswers { get; set; }
        public List<string> CorrectAnswers { get; set; }
        public bool IsCorrect { get; set; }

        public QuestionResult()
        {
            Choices = new List<Choice>();
            StudentAnswers = new List<string>();
            CorrectAnswers = new List<string>();
        }
    }
}
