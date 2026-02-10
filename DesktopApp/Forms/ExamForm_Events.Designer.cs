using ITI_DB.Data;
using ITI_DB.Models;
using System;
using System.Windows.Forms;

namespace ITI_DB.Forms
{
    public partial class ExamForm : Form
    {
        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            if (currentQuestionIndex > 0)
            {
                DisplayQuestion(currentQuestionIndex - 1);
            }
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (currentQuestionIndex < examQuestions.Count - 1)
            {
                DisplayQuestion(currentQuestionIndex + 1);
            }
        }

        private void BtnFlag_Click(object sender, EventArgs e)
        {
            Question currentQuestion = examQuestions[currentQuestionIndex];
            currentQuestion.IsFlagged = !currentQuestion.IsFlagged;

            btnFlag.Text = currentQuestion.IsFlagged ? "🚩 Unflag" : "🚩 Flag Question";
            UpdateQuestionNavigationButtons();
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            int answeredCount = examQuestions.Count(q => q.SelectedChoices.Count > 0);
            int unansweredCount = examQuestions.Count - answeredCount;

            string message = $"You have answered {answeredCount} out of {examQuestions.Count} questions.";

            if (unansweredCount > 0)
            {
                message += $"\n\n{unansweredCount} question(s) are still unanswered.";
            }

            message += "\n\nAre you sure you want to submit the exam?";

            DialogResult result = MessageBox.Show(message, "Confirm Submission",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SubmitExam();
            }
        }

        private void SubmitExam()
        {
            if (examTimer != null)
            {
                examTimer.Stop();
                examTimer.Dispose();
            }

            try
            {
                // Calculate results
                ExamResult result = ExamRepository.CalculateExamResult(
                    currentStudent.StudentID,
                    currentExam.ExamID,
                    examQuestions);

                // Save to database
                ExamRepository.SaveExamResult(
                    currentStudent.StudentID,
                    currentExam.ExamID,
                    examQuestions,
                    result.GradePercent);

                // Show results
                this.Hide();
                ResultForm resultForm = new ResultForm(result, currentExam);
                resultForm.FormClosed += (s, e) => this.Close();
                resultForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error submitting exam: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExamForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (examTimer != null && examTimer.Enabled)
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to exit? Your exam will not be saved.",
                    "Confirm Exit",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    examTimer.Stop();
                    examTimer.Dispose();
                }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new Size(284, 261);
            this.Name = "ExamForm";
            this.ResumeLayout(false);
        }
    }
}
