# Exam Management System

A **Desktop-based Exam System** built to apply what we learned from **ERD, EERD, Database Design, and Advanced SQL Server**. Students can take exams, submit answers, and the system calculates their scores automatically.

---

## Features

- **Database**  
  - CRUD operations for students, instructors, courses, exams, questions, and choices.  
  - Stored procedures for exam generation, answer submission, and exam correction.  
  - Uses transactions for safe operations.

- **Desktop Application**  
  - Windows Forms app for students to take exams.  
  - Supports navigation (Next/Previous) and updating answers before submission.  
  - Submits answers directly to the database.

- **Reports**  
  - Generated using **SSRS**.  
  - Shows exam results, scores, and correct/wrong answers.

---

## Database

- **DBMS:** SQL Server  
- **Includes:**  
  - `.bak` database backup file.  
  - Tables for all entities with relationships and constraints.  
  - Stored procedures for exam generation, answer submission, and correction.

---

## Diagrams & Documentation

- **ERD & Schema Diagrams**  
  ![ERD Diagram](images/erd.png)  
  _Replace `images/erd.png` with your actual ERD diagram screenshot_

- **Mini Workflow Diagram**  
  ![Workflow](images/workflow.png)  
  _Shows exam creation → student answering → submission → correction → report_

---

## Getting Started

1. Restore the database from the `.bak` file.  
2. Open the desktop app in Visual Studio.  
3. Update the connection string if needed:

```csharp
SqlConnection con = new SqlConnection(
    "Data Source=.;Initial Catalog=ExamDB;Integrated Security=True"
);
```

4. Build and run the app.  
5. Students can take exams and submit answers.  
6. Generate reports from SSRS.

---

## Outcomes

- Practice designing databases with ERD/EERD.  
- Use advanced SQL Server features like stored procedures and transactions.  
- Build a desktop app connected to a database.  
- Generate professional reports with SSRS.

---

## Screenshots (Optional)

**Exam Form:**  
![Exam Form](images/examform.png)  

**Reports:**  
![Reports](images/reports.png)

---

## License

This project is for educational purposes and can be freely used for learning and reference.
