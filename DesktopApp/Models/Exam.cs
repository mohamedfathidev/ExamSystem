using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_DB.Models
{
    public class Exam
    {
        public int ExamID { get; set; }
        public DateTime ExamDate { get; set; }
        public decimal TotalDegree { get; set; }
        public int Duration { get; set; } 
        public int CourseID { get; set; }
        public string CourseName { get; set; }
    }
}
