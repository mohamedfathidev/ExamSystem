using System;
using System.Drawing;
using System.Windows.Forms;
using ITI_DB.Models;


namespace ITI_DB.Forms
{
    public partial class ResultForm : Form
    {
        private ExamResult examResult;
        private Exam exam;

        public ResultForm(ExamResult result, Exam examInfo)
        {
            InitializeComponent();
            examResult = result;
            exam = examInfo;
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "Exam Results";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ColorTranslator.FromHtml("#F5F5DC");
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Header Panel
            Panel headerPanel = new Panel();
            headerPanel.Size = new Size(1000, 120);
            headerPanel.Location = new Point(0, 0);
            headerPanel.BackColor = ColorTranslator.FromHtml("#8B4513");
            this.Controls.Add(headerPanel);

            // Title
            Label lblTitle = new Label();
            lblTitle.Text = "Exam Results";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(30, 20);
            headerPanel.Controls.Add(lblTitle);

            // Score Panel
            Panel scorePanel = new Panel();
            scorePanel.Size = new Size(300, 80);
            scorePanel.Location = new Point(650, 20);
            scorePanel.BackColor = examResult.GradePercent >= 50 ?
                ColorTranslator.FromHtml("#28a745") :
                ColorTranslator.FromHtml("#DC143C");
            scorePanel.BorderStyle = BorderStyle.FixedSingle;
            headerPanel.Controls.Add(scorePanel);

            Label lblScore = new Label();
            lblScore.Text = $"{examResult.GradePercent:F2}%";
            lblScore.Font = new Font("Segoe UI", 32, FontStyle.Bold);
            lblScore.ForeColor = Color.White;
            lblScore.AutoSize = false;
            lblScore.Size = new Size(280, 60);
            lblScore.Location = new Point(10, 10);
            lblScore.TextAlign = ContentAlignment.MiddleCenter;
            scorePanel.Controls.Add(lblScore);

            // Summary Panel
            Panel summaryPanel = new Panel();
            summaryPanel.Location = new Point(30, 140);
            summaryPanel.Size = new Size(930, 100);
            summaryPanel.BackColor = Color.White;
            summaryPanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(summaryPanel);

            Label lblSummary = new Label();
            lblSummary.Text = $"Total Questions: {examResult.QuestionResults.Count}  |  " +
                             $"Obtained Marks: {examResult.ObtainedMarks}/{examResult.TotalMarks}  |  " +
                             $"Correct Answers: {examResult.QuestionResults.Count(q => q.IsCorrect)}";
            lblSummary.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblSummary.ForeColor = ColorTranslator.FromHtml("#8B4513");
            lblSummary.AutoSize = false;
            lblSummary.Size = new Size(900, 80);
            lblSummary.Location = new Point(15, 10);
            lblSummary.TextAlign = ContentAlignment.MiddleLeft;
            summaryPanel.Controls.Add(lblSummary);

            // Questions Review Label
            Label lblReview = new Label();
            lblReview.Text = "Detailed Review:";
            lblReview.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblReview.ForeColor = ColorTranslator.FromHtml("#8B4513");
            lblReview.AutoSize = true;
            lblReview.Location = new Point(30, 260);
            this.Controls.Add(lblReview);

            // Results Panel
            Panel resultsPanel = new Panel();
            resultsPanel.Location = new Point(30, 290);
            resultsPanel.Size = new Size(930, 320);
            resultsPanel.BackColor = Color.White;
            resultsPanel.BorderStyle = BorderStyle.FixedSingle;
            resultsPanel.AutoScroll = true;
            this.Controls.Add(resultsPanel);

            DisplayQuestionResults(resultsPanel);

            // Close Button
            Button btnClose = new Button();
            btnClose.Text = "Close";
            btnClose.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnClose.Size = new Size(150, 45);
            btnClose.Location = new Point(810, 625);
            btnClose.BackColor = ColorTranslator.FromHtml("#8B4513");
            btnClose.ForeColor = Color.White;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Cursor = Cursors.Hand;
            btnClose.Click += (s, e) => this.Close();
            this.Controls.Add(btnClose);
        }

        private void DisplayQuestionResults(Panel resultsPanel)
        {
            int yPosition = 10;
            int questionNumber = 1;

            foreach (var questionResult in examResult.QuestionResults)
            {
                Panel questionPanel = CreateQuestionResultPanel(questionResult, questionNumber, yPosition);
                resultsPanel.Controls.Add(questionPanel);

                yPosition += questionPanel.Height + 15;
                questionNumber++;
            }
        }

        private Panel CreateQuestionResultPanel(QuestionResult qResult, int questionNumber, int yPosition)
        {
            int panelHeight = 100 + (qResult.Choices.Count * 40);

            Panel panel = new Panel();
            panel.Location = new Point(10, yPosition);
            panel.Size = new Size(890, panelHeight);
            panel.BackColor = qResult.IsCorrect ?
                ColorTranslator.FromHtml("#E8F5E9") :
                ColorTranslator.FromHtml("#FFEBEE");
            panel.BorderStyle = BorderStyle.FixedSingle;

            // Status Indicator
            Label lblStatus = new Label();
            lblStatus.Text = qResult.IsCorrect ? "✓" : "✗";
            lblStatus.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblStatus.ForeColor = qResult.IsCorrect ?
                ColorTranslator.FromHtml("#28a745") :
                ColorTranslator.FromHtml("#DC143C");
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(15, 15);
            panel.Controls.Add(lblStatus);

            // Question Number and Text
            Label lblQuestion = new Label();
            lblQuestion.Text = $"Question {questionNumber}: {qResult.QuestionText}";
            lblQuestion.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblQuestion.ForeColor = Color.Black;
            lblQuestion.AutoSize = false;
            lblQuestion.Size = new Size(810, 50);
            lblQuestion.Location = new Point(60, 15);
            panel.Controls.Add(lblQuestion);

            // Marks
            Label lblMarks = new Label();
            lblMarks.Text = $"Marks: {(qResult.IsCorrect ? qResult.Marks : 0)}/{qResult.Marks}";
            lblMarks.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblMarks.ForeColor = ColorTranslator.FromHtml("#8B4513");
            lblMarks.AutoSize = true;
            lblMarks.Location = new Point(60, 65);
            panel.Controls.Add(lblMarks);

            // Choices
            int choiceY = 90;
            foreach (var choice in qResult.Choices)
            {
                bool isStudentAnswer = qResult.StudentAnswers.Contains(choice.ChoiceNo);
                bool isCorrectAnswer = qResult.CorrectAnswers.Contains(choice.ChoiceNo);

                Panel choicePanel = new Panel();
                choicePanel.Location = new Point(60, choiceY);
                choicePanel.Size = new Size(800, 35);

                // Determine background color
                if (isCorrectAnswer)
                {
                    choicePanel.BackColor = ColorTranslator.FromHtml("#C8E6C9"); // Light green
                }
                else if (isStudentAnswer && !isCorrectAnswer)
                {
                    choicePanel.BackColor = ColorTranslator.FromHtml("#FFCDD2"); // Light red
                }
                else
                {
                    choicePanel.BackColor = Color.White;
                }

                choicePanel.BorderStyle = BorderStyle.FixedSingle;

                // Choice text with indicators
                string prefix = "";
                if (isStudentAnswer && isCorrectAnswer)
                    prefix = "✓ Your Answer (Correct): ";
                else if (isStudentAnswer && !isCorrectAnswer)
                    prefix = "✗ Your Answer (Wrong): ";
                else if (isCorrectAnswer)
                    prefix = "✓ Correct Answer: ";

                Label lblChoice = new Label();
                lblChoice.Text = prefix + choice.ChoiceText;
                lblChoice.Font = new Font("Segoe UI", 9, isStudentAnswer || isCorrectAnswer ? FontStyle.Bold : FontStyle.Regular);
                lblChoice.ForeColor = Color.Black;
                lblChoice.AutoSize = false;
                lblChoice.Size = new Size(780, 30);
                lblChoice.Location = new Point(10, 2);
                lblChoice.TextAlign = ContentAlignment.MiddleLeft;
                choicePanel.Controls.Add(lblChoice);

                panel.Controls.Add(choicePanel);
                choiceY += 40;
            }

            return panel;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new Size(284, 261);
            this.Name = "ResultForm";
            this.ResumeLayout(false);
        }
    }
}