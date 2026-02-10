using System;
using System.Drawing;
using System.Windows.Forms;
using ITI_DB.Data;
using ITI_DB.Models;

namespace ITI_DB.Forms
{
    public partial class LoginForm : Form
    {
        private void SetupUI()
        {
            this.Text = "Student Exam System - Login";
            this.Size = new Size(500, 350);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ColorTranslator.FromHtml("#F5F5DC"); // Beige
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Title Label
            Label lblTitle = new Label();
            lblTitle.Text = "Student Exam System";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.ForeColor = ColorTranslator.FromHtml("#8B4513"); // Brown
            lblTitle.AutoSize = false;
            lblTitle.Size = new Size(400, 50);
            lblTitle.Location = new Point(50, 30);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblTitle);

            // Subtitle
            Label lblSubtitle = new Label();
            lblSubtitle.Text = "Please enter your SSN to login";
            lblSubtitle.Font = new Font("Segoe UI", 10);
            lblSubtitle.ForeColor = ColorTranslator.FromHtml("#696969");
            lblSubtitle.AutoSize = false;
            lblSubtitle.Size = new Size(400, 30);
            lblSubtitle.Location = new Point(50, 80);
            lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblSubtitle);

            // SSN Label
            Label lblSSN = new Label();
            lblSSN.Text = "SSN:";
            lblSSN.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblSSN.ForeColor = ColorTranslator.FromHtml("#8B4513");
            lblSSN.AutoSize = true;
            lblSSN.Location = new Point(80, 140);
            this.Controls.Add(lblSSN);

            // SSN TextBox
            TextBox txtSSN = new TextBox();
            txtSSN.Name = "txtSSN";
            txtSSN.Font = new Font("Segoe UI", 12);
            txtSSN.Size = new Size(300, 30);
            txtSSN.Location = new Point(80, 170);
            txtSSN.MaxLength = 14;
            this.Controls.Add(txtSSN);

            // Login Button
            Button btnLogin = new Button();
            btnLogin.Text = "Login";
            btnLogin.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnLogin.Size = new Size(140, 45);
            btnLogin.Location = new Point(180, 230);
            btnLogin.BackColor = ColorTranslator.FromHtml("#DC143C"); // Red
            btnLogin.ForeColor = Color.White;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.Click += (s, e) => LoginButton_Click(txtSSN.Text);
            this.Controls.Add(btnLogin);

            // Enter key press
            txtSSN.KeyPress += (s, e) => {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    LoginButton_Click(txtSSN.Text);
                    e.Handled = true;
                }
            };

            // Set focus on textbox
            this.Load += (s, e) => txtSSN.Focus();
        }

        private void LoginButton_Click(string ssn)
        {
            if (string.IsNullOrWhiteSpace(ssn))
            {
                MessageBox.Show("Please enter your SSN", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Student student = StudentRepository.AuthenticateStudent(ssn);

                if (student != null)
                {
                    this.Hide();
                    DashboardForm dashboard = new DashboardForm(student);
                    dashboard.FormClosed += (s, e) => this.Close();
                    dashboard.Show();
                }
                else
                {
                    MessageBox.Show("Invalid SSN. Please try again.", "Login Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new Size(284, 261);
            this.Name = "LoginForm";
            this.ResumeLayout(false);
        }
    }
}