using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using ITI_DB.Data;
using ITI_DB.Models;


namespace ITI_DB.Forms
{
    public partial class ExamForm : Form
    {
        private Exam currentExam;
        private Student currentStudent;
        private List<Question> examQuestions;
        private int currentQuestionIndex = 0;
        private System.Windows.Forms.Timer examTimer;
        private int remainingSeconds;

        // UI Controls
        private Label lblTimer;
        private Label lblQuestionNumber;
        private Panel questionsNavPanel;
        private Panel questionPanel;
        private Label lblQuestion;
        private Panel choicesPanel;
        private Button btnPrevious;
        private Button btnNext;
        private Button btnFlag;
        private Button btnSubmit;

        public ExamForm(Exam exam, Student student)
        {
            InitializeComponent();
            currentExam = exam;
            currentStudent = student;
            SetupUI();
            LoadExamQuestions();
            StartTimer();
        }

        private void SetupUI()
        {
            this.Text = "Exam - " + currentExam.CourseName;
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ColorTranslator.FromHtml("#F5F5DC");
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.FormClosing += ExamForm_FormClosing;

            // Header Panel
            Panel headerPanel = new Panel();
            headerPanel.Size = new Size(1200, 70);
            headerPanel.Location = new Point(0, 0);
            headerPanel.BackColor = ColorTranslator.FromHtml("#8B4513");
            this.Controls.Add(headerPanel);

            // Exam Title
            Label lblTitle = new Label();
            lblTitle.Text = currentExam.CourseName + " - Exam";
            lblTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(20, 20);
            headerPanel.Controls.Add(lblTitle);

            // Timer Label
            lblTimer = new Label();
            lblTimer.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTimer.ForeColor = Color.White;
            lblTimer.AutoSize = true;
            lblTimer.Location = new Point(1020, 18);
            headerPanel.Controls.Add(lblTimer);

            // Question Navigation Panel (Top)
            questionsNavPanel = new Panel();
            questionsNavPanel.Location = new Point(20, 90);
            questionsNavPanel.Size = new Size(1150, 80);
            questionsNavPanel.BackColor = Color.White;
            questionsNavPanel.BorderStyle = BorderStyle.FixedSingle;
            questionsNavPanel.AutoScroll = true;
            this.Controls.Add(questionsNavPanel);

            // Question Number Label
            lblQuestionNumber = new Label();
            lblQuestionNumber.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblQuestionNumber.ForeColor = ColorTranslator.FromHtml("#8B4513");
            lblQuestionNumber.AutoSize = true;
            lblQuestionNumber.Location = new Point(20, 190);
            this.Controls.Add(lblQuestionNumber);

            // Question Panel
            questionPanel = new Panel();
            questionPanel.Location = new Point(20, 220);
            questionPanel.Size = new Size(1150, 450);
            questionPanel.BackColor = Color.White;
            questionPanel.BorderStyle = BorderStyle.FixedSingle;
            questionPanel.AutoScroll = true;
            this.Controls.Add(questionPanel);

            // Question Text
            lblQuestion = new Label();
            lblQuestion.Font = new Font("Segoe UI", 11);
            lblQuestion.ForeColor = Color.Black;
            lblQuestion.AutoSize = false;
            lblQuestion.Size = new Size(1100, 80);
            lblQuestion.Location = new Point(20, 20);
            questionPanel.Controls.Add(lblQuestion);

            // Choices Panel
            choicesPanel = new Panel();
            choicesPanel.Location = new Point(20, 110);
            choicesPanel.Size = new Size(1100, 320);
            choicesPanel.AutoScroll = true;
            questionPanel.Controls.Add(choicesPanel);

            // Control Buttons Panel
            Panel buttonsPanel = new Panel();
            buttonsPanel.Location = new Point(20, 690);
            buttonsPanel.Size = new Size(1150, 60);
            this.Controls.Add(buttonsPanel);

            // Previous Button
            btnPrevious = new Button();
            btnPrevious.Text = "← Previous";
            btnPrevious.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnPrevious.Size = new Size(130, 45);
            btnPrevious.Location = new Point(0, 7);
            btnPrevious.BackColor = Color.Gray;
            btnPrevious.ForeColor = Color.White;
            btnPrevious.FlatStyle = FlatStyle.Flat;
            btnPrevious.FlatAppearance.BorderSize = 0;
            btnPrevious.Cursor = Cursors.Hand;
            btnPrevious.Click += BtnPrevious_Click;
            buttonsPanel.Controls.Add(btnPrevious);

            // Flag Button
            btnFlag = new Button();
            btnFlag.Text = "🚩 Flag Question";
            btnFlag.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnFlag.Size = new Size(150, 45);
            btnFlag.Location = new Point(380, 7);
            btnFlag.BackColor = ColorTranslator.FromHtml("#FFA500");
            btnFlag.ForeColor = Color.White;
            btnFlag.FlatStyle = FlatStyle.Flat;
            btnFlag.FlatAppearance.BorderSize = 0;
            btnFlag.Cursor = Cursors.Hand;
            btnFlag.Click += BtnFlag_Click;
            buttonsPanel.Controls.Add(btnFlag);

            // Next Button
            btnNext = new Button();
            btnNext.Text = "Next →";
            btnNext.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnNext.Size = new Size(130, 45);
            btnNext.Location = new Point(870, 7);
            btnNext.BackColor = ColorTranslator.FromHtml("#8B4513");
            btnNext.ForeColor = Color.White;
            btnNext.FlatStyle = FlatStyle.Flat;
            btnNext.FlatAppearance.BorderSize = 0;
            btnNext.Cursor = Cursors.Hand;
            btnNext.Click += BtnNext_Click;
            buttonsPanel.Controls.Add(btnNext);

            // Submit Button
            btnSubmit = new Button();
            btnSubmit.Text = "Submit Exam";
            btnSubmit.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnSubmit.Size = new Size(140, 45);
            btnSubmit.Location = new Point(1010, 7);
            btnSubmit.BackColor = ColorTranslator.FromHtml("#DC143C");
            btnSubmit.ForeColor = Color.White;
            btnSubmit.FlatStyle = FlatStyle.Flat;
            btnSubmit.FlatAppearance.BorderSize = 0;
            btnSubmit.Cursor = Cursors.Hand;
            btnSubmit.Click += BtnSubmit_Click;
            buttonsPanel.Controls.Add(btnSubmit);
        }
    }
}