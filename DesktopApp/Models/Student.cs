using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_DB.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public string SSN { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string FullName => $"{FName} {LName}";
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime? BDate { get; set; }
        public int? TrackID { get; set; }
        public int? DeptID { get; set; }
        public int? IntakeID { get; set; }
    }
}
