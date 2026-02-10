using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_DB.Models
{
    public class Choice
    {
        public int QuestionID { get; set; }
        public string ChoiceNo { get; set; }
        public string ChoiceText { get; set; }
        public bool IsCorrect { get; set; }
    }
}
