using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_DB.Models
{
    public class Question
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; } 
        public decimal Degree { get; set; }
        public int CourseID { get; set; }
        public List<Choice> Choices { get; set; }
        public bool IsFlagged { get; set; }
        public List<string> SelectedChoices { get; set; }

        public Question()
        {
            Choices = new List<Choice>();
            SelectedChoices = new List<string>();
        }
    }
}
