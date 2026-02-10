using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ITI_DB.Data;
using ITI_DB.Models;

// Alias to avoid confusion with Forms.Exam if it exists
using ExamModel = ITI_DB.Models.Exam;

namespace ITI_DB.Forms
{
    public partial class DashboardForm : Form
    {
        private Student currentStudent;
        private List<Course> studentCourses;

        public DashboardForm(Student student)
        {
            InitializeComponent();
            currentStudent = student;
            SetupUI();
            LoadStudentCourses();
        }

        private void SetupUI()
        {
            this.Text = "Student Dashboard";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ColorTranslator.FromHtml("#F5F5DC"); // Beige

            // Header Panel
            Panel headerPanel = new Panel
            {
                Size = new Size(900, 80),
                Location = new Point(0, 0),
                BackColor = ColorTranslator.FromHtml("#8B4513")
            };
            this.Controls.Add(headerPanel);

            // Welcome Label
            Label lblWelcome = new Label
            {
                Text = $"Welcome, {currentStudent.FullName}!",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 25)
            };
            headerPanel.Controls.Add(lblWelcome);

            // Logout Button
            Button btnLogout = new Button
            {
                Text = "Logout",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(100, 35),
                Location = new Point(760, 22),
                BackColor = ColorTranslator.FromHtml("#DC143C"),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += (s, e) => this.Close();
            headerPanel.Controls.Add(btnLogout);

            // Title Label
            Label lblTitle = new Label
            {
                Text = "Select a Course to Take Exam",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#8B4513"),
                AutoSize = true,
                Location = new Point(30, 100)
            };
            this.Controls.Add(lblTitle);

            // Courses Panel
            Panel coursesPanel = new Panel
            {
                Location = new Point(30, 140),
                Size = new Size(830, 400),
                AutoScroll = true,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Name = "coursesPanel"
            };
            this.Controls.Add(coursesPanel);
        }

        private void LoadStudentCourses()
        {
            try
            {
                studentCourses = StudentRepository.GetStudentCourses(currentStudent.StudentID);

                Panel coursesPanel = this.Controls["coursesPanel"] as Panel;
                coursesPanel.Controls.Clear();

                if (studentCourses.Count == 0)
                {
                    Label lblNoData = new Label
                    {
                        Text = "No courses found for your track.",
                        Font = new Font("Segoe UI", 12),
                        ForeColor = Color.Gray,
                        AutoSize = true,
                        Location = new Point(20, 20)
                    };
                    coursesPanel.Controls.Add(lblNoData);
                    return;
                }

                int yPosition = 20;

                foreach (var course in studentCourses)
                {
                    Panel courseCard = CreateCourseCard(course, yPosition);
                    coursesPanel.Controls.Add(courseCard);
                    yPosition += 110;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading courses: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel CreateCourseCard(Course course, int yPosition)
        {
            Panel card = new Panel
            {
                Size = new Size(780, 90),
                Location = new Point(20, yPosition),
                BackColor = ColorTranslator.FromHtml("#FFF8DC"),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Course Name
            Label lblCourseName = new Label
            {
                Text = course.CourseName,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#8B4513"),
                AutoSize = false,
                Size = new Size(500, 30),
                Location = new Point(15, 15)
            };
            card.Controls.Add(lblCourseName);

            // Course Description
            Label lblDescription = new Label
            {
                Text = string.IsNullOrEmpty(course.Description) ? "No description available" : course.Description,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                AutoSize = false,
                Size = new Size(500, 35),
                Location = new Point(15, 45)
            };
            card.Controls.Add(lblDescription);

            // Take Exam Button
            Button btnTakeExam = new Button
            {
                Text = "Take Exam",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(130, 40),
                Location = new Point(630, 25),
                BackColor = ColorTranslator.FromHtml("#DC143C"),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnTakeExam.FlatAppearance.BorderSize = 0;
            btnTakeExam.Click += (s, e) => ShowExamSelection(course);
            card.Controls.Add(btnTakeExam);

            return card;
        }

        private void ShowExamSelection(Course course)
        {
            try
            {
                List<ExamModel> exams = ExamRepository.GetCourseExams(course.CourseID, currentStudent.StudentID);

                if (exams.Count == 0)
                {
                    MessageBox.Show("No available exams for this course or you have already completed all exams.",
                        "No Exams", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                ExamSelectionForm examForm = new ExamSelectionForm(exams, currentStudent, course);
                examForm.ShowDialog();

                // Refresh courses
                LoadStudentCourses();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading exams: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new Size(900, 600);
            this.Name = "DashboardForm";
            this.ResumeLayout(false);
        }
    }
}
