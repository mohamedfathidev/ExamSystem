-- 1. Departments
INSERT INTO Department (DepName) VALUES ('Computer Science'), ('Information Systems'), ('Digital Media');

-- 2. Courses
INSERT INTO Course (CourseName, Description) VALUES 
('SQL Databases', 'Relational database management and SQL syntax'),
('Web Development', 'HTML, CSS, and JavaScript fundamentals'),
('Python Programming', 'Introduction to Python and data structures');

-- 3. Topics
INSERT INTO Topic (TopicName, CourseID) VALUES 
('Joins', 1), ('Indexing', 1), ('DOM Manipulation', 2), ('Lists & Dicts', 3);

-- 4. Instructors
INSERT INTO Instructor (SSN, FName, LName, Gender, Email, salary, DepID) VALUES 
('12345678901', 'Ahmed', 'Ali', 'M', 'ahmed@exam.com', 8000, 1),
('12345678902', 'Sara', 'Sami', 'F', 'sara@exam.com', 8500, 1);

-- 5. Tracks
INSERT INTO Track (TrackName, DepID, TrackSuperID) VALUES 
('Full Stack Development', 1, 1),
('Data Science', 1, 2);

-- 6. Students
INSERT INTO student (FName, LName, Gender, Email, DepID, TrackID, IntakeID) VALUES 
('John', 'Doe', 'M', 'john@mail.com', 1, 1, 2),
('Jane', 'Smith', 'F', 'jane@mail.com', 1, 1, 2),
('Mike', 'Ross', 'M', 'mike@mail.com', 1, 2, 2);


-- 7. Teaching 
INSERT INTO Intake values('Intake46', '2025-10-19', '2026-06-30')

-- ===========================================================================================
-- SQL Questions (CourseID 1)
INSERT INTO Question (QuestionText, QuestionType, Degree, CourseID) VALUES 
('What does SQL stand for?', 'MCQ', 10, 1),
('Which keyword is used to remove duplicates?', 'MCQ', 10, 1),
('Which join returns all rows from the left table?', 'MCQ', 10, 1);

-- Choices for Q1 (Correct: A)
INSERT INTO Choice (QuestionID, ChoiceNo, ChoiceText, IsCorrect) VALUES 
(1, 'A', 'Structured Query Language', 1),
(1, 'B', 'Strong Query Language', 0),
(1, 'C', 'Simple Query Language', 0),
(1, 'D', 'Standard Query Language', 0);

-- Choices for Q2 (Correct: B)
INSERT INTO Choice (QuestionID, ChoiceNo, ChoiceText, IsCorrect) VALUES 
(2, 'A', 'UNIQUE', 0),
(2, 'B', 'DISTINCT', 1),
(2, 'C', 'SINGLE', 0),
(2, 'D', 'REMOVE', 0);

-- Choices for Q3 (Correct: C)
INSERT INTO Choice (QuestionID, ChoiceNo, ChoiceText, IsCorrect) VALUES 
(3, 'A', 'INNER JOIN', 0),
(3, 'B', 'RIGHT JOIN', 0),
(3, 'C', 'LEFT JOIN', 1),
(3, 'D', 'FULL JOIN', 0);


-- ==================================================================================================
-- 1. Create an Exam for SQL
INSERT INTO Exam (ExamDate, TotalDegree, duration, CourseID) 
VALUES ('2024-05-20', 30, 60, 1); -- ExamID 1

-- 2. Link Questions to Exam 1
INSERT INTO ExamQuestion (ExamID, QuestionID) VALUES (1, 1), (1, 2), (1, 3);

-- 3. Student Answers (John Doe answers all correctly)
INSERT INTO StudentAnswer (StudentID, ExamID, QuestionID, Answer) VALUES 
(2, 1, 1, 'A'), 
(3, 1, 2, 'B'), 
(4, 1, 3, 'C');

-- 4. Student Answers (Jane Smith gets one wrong)
INSERT INTO StudentAnswer (StudentID, ExamID, QuestionID, Answer) VALUES 
(2, 1, 1, 'A'), 
(2, 1, 2, 'A'), -- Wrong (Should be B)
(2, 1, 3, 'C');


-- ===================================================================================================

