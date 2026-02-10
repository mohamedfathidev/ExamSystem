using ITI_DB.Data;
using ITI_DB.Models;
using System;
using System.Windows.Forms;

namespace ITI_DB.Forms
{
    public partial class ExamForm : Form
    {
        private void LoadExamQuestions()
        {
            try
            {
                examQuestions = ExamRepository.GetExamQuestions(currentExam.ExamID);

                if (examQuestions.Count == 0)
                {
                    MessageBox.Show("No questions found for this exam.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                CreateQuestionNavigationButtons();
                DisplayQuestion(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading questions: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void CreateQuestionNavigationButtons()
        {
            questionsNavPanel.Controls.Clear();

            int xPosition = 10;
            int yPosition = 10;
            int buttonWidth = 50;
            int buttonHeight = 50;
            int spacing = 10;
            int buttonsPerRow = 20;

            for (int i = 0; i < examQuestions.Count; i++)
            {
                if (i > 0 && i % buttonsPerRow == 0)
                {
                    xPosition = 10;
                    yPosition += buttonHeight + spacing;
                }

                Button btnQuestion = new Button();
                btnQuestion.Text = (i + 1).ToString();
                btnQuestion.Tag = i;
                btnQuestion.Size = new Size(buttonWidth, buttonHeight);
                btnQuestion.Location = new Point(xPosition, yPosition);
                btnQuestion.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                btnQuestion.FlatStyle = FlatStyle.Flat;
                btnQuestion.FlatAppearance.BorderSize = 1;
                btnQuestion.Cursor = Cursors.Hand;
                btnQuestion.Click += QuestionNavButton_Click;

                UpdateQuestionButtonAppearance(btnQuestion, i);

                questionsNavPanel.Controls.Add(btnQuestion);
                xPosition += buttonWidth + spacing;
            }
        }

        private void UpdateQuestionButtonAppearance(Button btn, int index)
        {
            Question question = examQuestions[index];

            if (index == currentQuestionIndex)
            {
                btn.BackColor = ColorTranslator.FromHtml("#8B4513");
                btn.ForeColor = Color.White;
            }
            else if (question.IsFlagged)
            {
                btn.BackColor = ColorTranslator.FromHtml("#FFA500");
                btn.ForeColor = Color.White;
            }
            else if (question.SelectedChoices.Count > 0)
            {
                btn.BackColor = ColorTranslator.FromHtml("#90EE90");
                btn.ForeColor = Color.Black;
            }
            else
            {
                btn.BackColor = Color.White;
                btn.ForeColor = Color.Black;
            }
        }

        private void QuestionNavButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int index = (int)btn.Tag;
            DisplayQuestion(index);
        }

        private void DisplayQuestion(int index)
        {
            if (index < 0 || index >= examQuestions.Count)
                return;

            currentQuestionIndex = index;
            Question question = examQuestions[index];

            // Update question number
            lblQuestionNumber.Text = $"Question {index + 1} of {examQuestions.Count}";

            // Update question text
            lblQuestion.Text = question.QuestionText;

            // Update flag button
            btnFlag.Text = question.IsFlagged ? "🚩 Unflag" : "🚩 Flag Question";

            // Update navigation buttons
            btnPrevious.Enabled = index > 0;
            btnNext.Enabled = index < examQuestions.Count - 1;

            // Display choices
            DisplayChoices(question);

            // Update all navigation buttons
            foreach (Control ctrl in questionsNavPanel.Controls)
            {
                if (ctrl is Button btn)
                {
                    int btnIndex = (int)btn.Tag;
                    UpdateQuestionButtonAppearance(btn, btnIndex);
                }
            }
        }

        private void DisplayChoices(Question question)
        {
            choicesPanel.Controls.Clear();

            int yPosition = 10;
            int correctAnswersCount = question.Choices.Count(c => c.IsCorrect);
            bool isMultipleChoice = correctAnswersCount > 1;

            if (isMultipleChoice)
            {
                Label lblInfo = new Label();
                lblInfo.Text = "* This question has multiple correct answers. Select all that apply.";
                lblInfo.Font = new Font("Segoe UI", 9, FontStyle.Italic);
                lblInfo.ForeColor = ColorTranslator.FromHtml("#DC143C");
                lblInfo.AutoSize = true;
                lblInfo.Location = new Point(10, yPosition);
                choicesPanel.Controls.Add(lblInfo);
                yPosition += 30;
            }

            foreach (var choice in question.Choices)
            {
                Panel choicePanel = new Panel();
                choicePanel.Size = new Size(1050, 50);
                choicePanel.Location = new Point(10, yPosition);
                choicePanel.BackColor = ColorTranslator.FromHtml("#FFF8DC");
                choicePanel.BorderStyle = BorderStyle.FixedSingle;
                choicePanel.Tag = choice;

                if (isMultipleChoice)
                {
                    CheckBox chk = new CheckBox();
                    chk.Text = choice.ChoiceText;
                    chk.Font = new Font("Segoe UI", 10);
                    chk.AutoSize = false;
                    chk.Size = new Size(1020, 40);
                    chk.Location = new Point(10, 5);
                    chk.Checked = question.SelectedChoices.Contains(choice.ChoiceNo);
                    chk.Tag = choice;
                    chk.CheckedChanged += (s, e) =>
                    {
                        CheckBox checkbox = s as CheckBox;
                        Choice selectedChoice = checkbox.Tag as Choice;

                        if (checkbox.Checked)
                        {
                            if (!question.SelectedChoices.Contains(selectedChoice.ChoiceNo))
                                question.SelectedChoices.Add(selectedChoice.ChoiceNo);
                        }
                        else
                        {
                            question.SelectedChoices.Remove(selectedChoice.ChoiceNo);
                        }

                        UpdateQuestionNavigationButtons();
                    };
                    choicePanel.Controls.Add(chk);
                }
                else
                {
                    RadioButton radio = new RadioButton();
                    radio.Text = choice.ChoiceText;
                    radio.Font = new Font("Segoe UI", 10);
                    radio.AutoSize = false;
                    radio.Size = new Size(1020, 40);
                    radio.Location = new Point(10, 5);
                    radio.Checked = question.SelectedChoices.Contains(choice.ChoiceNo);
                    radio.Tag = choice;
                    radio.CheckedChanged += (s, e) =>
                    {
                        RadioButton radioButton = s as RadioButton;
                        if (radioButton.Checked)
                        {
                            Choice selectedChoice = radioButton.Tag as Choice;
                            question.SelectedChoices.Clear();
                            question.SelectedChoices.Add(selectedChoice.ChoiceNo);
                            UpdateQuestionNavigationButtons();
                        }
                    };
                    choicePanel.Controls.Add(radio);
                }

                choicesPanel.Controls.Add(choicePanel);
                yPosition += 60;
            }
        }

        private void UpdateQuestionNavigationButtons()
        {
            foreach (Control ctrl in questionsNavPanel.Controls)
            {
                if (ctrl is Button btn)
                {
                    int index = (int)btn.Tag;
                    UpdateQuestionButtonAppearance(btn, index);
                }
            }
        }

        private void StartTimer()
        {
            remainingSeconds = currentExam.Duration * 60;

            examTimer = new System.Windows.Forms.Timer();
            examTimer.Interval = 1000; // 1 second
            examTimer.Tick += ExamTimer_Tick;
            examTimer.Start();

            UpdateTimerDisplay();
        }

        private void ExamTimer_Tick(object sender, EventArgs e)
        {
            remainingSeconds--;
            UpdateTimerDisplay();

            if (remainingSeconds <= 0)
            {
                examTimer.Stop();
                MessageBox.Show("Time's up! The exam will be submitted automatically.",
                    "Time Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SubmitExam();
            }
            else if (remainingSeconds <= 60)
            {
                lblTimer.ForeColor = ColorTranslator.FromHtml("#DC143C");
            }
            else if (remainingSeconds <= 300)
            {
                lblTimer.ForeColor = ColorTranslator.FromHtml("#FFA500");
            }
        }

        private void UpdateTimerDisplay()
        {
            int minutes = remainingSeconds / 60;
            int seconds = remainingSeconds % 60;
            lblTimer.Text = $"⏱ {minutes:D2}:{seconds:D2}";
        }
    }
}