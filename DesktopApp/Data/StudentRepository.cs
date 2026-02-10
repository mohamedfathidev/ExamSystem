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
    public class StudentRepository
    {
        public static Student AuthenticateStudent(string ssn)
        {
            string query = @"SELECT StudentID, SSN, FName, LName, Gender, Email, Address, 
                            BDate, TrackID, DepID, IntakeID 
                            FROM Student WHERE SSN = @SSN";

            SqlParameter[] parameters = {
                new SqlParameter("@SSN", ssn)
            };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new Student
                {
                    StudentID = Convert.ToInt32(row["StudentID"]),
                    SSN = row["SSN"].ToString(),
                    FName = row["FName"].ToString(),
                    LName = row["LName"].ToString(),
                    Gender = row["Gender"].ToString(),
                    Email = row["Email"].ToString(),
                    Address = row["Address"]?.ToString(),
                    BDate = row["BDate"] != DBNull.Value ? Convert.ToDateTime(row["BDate"]) : (DateTime?)null,
                    TrackID = row["TrackID"] != DBNull.Value ? Convert.ToInt32(row["TrackID"]) : (int?)null,
                    DeptID = row["DepID"] != DBNull.Value ? Convert.ToInt32(row["DepID"]) : (int?)null,
                    IntakeID = row["IntakeID"] != DBNull.Value ? Convert.ToInt32(row["IntakeID"]) : (int?)null
                };
            }

            return null;
        }

        public static List<Course> GetStudentCourses(int studentId)
        {
            string query = @"
                SELECT DISTINCT c.CourseID, c.CourseName, c.Description
                FROM Course c
                INNER JOIN TrackCourse tc ON c.CourseID = tc.CourseID
                INNER JOIN Student s ON s.TrackID = tc.TrackID
                WHERE s.StudentID = @StudentID";

            SqlParameter[] parameters = {
                new SqlParameter("@StudentID", studentId)
            };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
            List<Course> courses = new List<Course>();

            foreach (DataRow row in dt.Rows)
            {
                courses.Add(new Course
                {
                    CourseID = Convert.ToInt32(row["CourseID"]),
                    CourseName = row["CourseName"].ToString(),
                    Description = row["Description"]?.ToString()
                });
            }

            return courses;
        }
    }
}
