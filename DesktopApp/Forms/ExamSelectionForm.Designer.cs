using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ITI_DB.Models;

using ExamModel = ITI_DB.Models.Exam;

namespace ITI_DB.Forms
{
    public partial class ExamSelectionForm : Form
    {
        private List<ExamModel> exams;
        private Student student;
        private Course course;

        public ExamSelectionForm(List<ExamModel> examList, Student currentStudent, Course selectedCourse)
        {
            InitializeComponent();
            exams = examList;
            student = currentStudent;
            course = selectedCourse;
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "Select Exam - " + course.CourseName;
            this.Size = new Size(700, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = ColorTranslator.FromHtml("#F5F5DC");
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Title
            Label lblTitle = new Label
            {
                Text = $"Available Exams for {course.CourseName}",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#8B4513"),
                AutoSize = true,
                Location = new Point(30, 20)
            };
            this.Controls.Add(lblTitle);

            // Exams Panel
            Panel examsPanel = new Panel
            {
                Location = new Point(30, 60),
                Size = new Size(630, 330),
                AutoScroll = true,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(examsPanel);

            int yPosition = 10;
            foreach (var exam in exams)
            {
                Panel examCard = CreateExamCard(exam, yPosition);
                examsPanel.Controls.Add(examCard);
                yPosition += 85;
            }

            // Close Button
            Button btnClose = new Button
            {
                Text = "Close",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(120, 40),
                Location = new Point(540, 410),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => this.Close();
            this.Controls.Add(btnClose);
        }

        private Panel CreateExamCard(ExamModel exam, int yPosition)
        {
            Panel card = new Panel
            {
                Size = new Size(600, 70),
                Location = new Point(10, yPosition),
                BackColor = ColorTranslator.FromHtml("#FFF8DC"),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Exam Info
            Label lblInfo = new Label
            {
                Text = $"Exam Date: {exam.ExamDate:dd/MM/yyyy}",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#8B4513"),
                AutoSize = true,
                Location = new Point(15, 10)
            };
            card.Controls.Add(lblInfo);

            Label lblDetails = new Label
            {
                Text = $"Duration: {exam.Duration} min  |  Total Marks: {exam.TotalDegree}",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(15, 35)
            };
            card.Controls.Add(lblDetails);

            // Start Button
            Button btnStart = new Button
            {
                Text = "Start Exam",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Size = new Size(110, 35),
                Location = new Point(470, 17),
                BackColor = ColorTranslator.FromHtml("#DC143C"),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.Click += (s, e) => StartExam(exam);
            card.Controls.Add(btnStart);

            return card;
        }

        private void StartExam(ExamModel exam)
        {
            DialogResult result = MessageBox.Show(
                $"You are about to start the exam.\n\n" +
                $"Duration: {exam.Duration} minutes\n" +
                $"Total Marks: {exam.TotalDegree}\n\n" +
                "Once started, the timer cannot be paused.\n" +
                "Are you ready to begin?",
                "Confirm Exam Start",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                ExamForm examForm = new ExamForm(exam, student);
                examForm.FormClosed += (s, e) => this.Close();
                examForm.ShowDialog();
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new Size(700, 500);
            this.Name = "ExamSelectionForm";
            this.ResumeLayout(false);
        }
    }
}
