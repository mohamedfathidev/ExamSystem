INSERT INTO Department (DepName) VALUES
('Computer Science'),
('Information Systems'),
('Electrical Engineering'),
('Mechanical Engineering'),
('Civil Engineering'),
('Business Administration'),
('Architecture'),
('Chemical Engineering');


INSERT INTO Intake (IntakeName, StartDate, EndDate) VALUES
('Fall 2025', '2025-09-01', '2025-12-31'),
('Spring 2026', '2026-01-15', '2026-05-15'),
('Summer 2026', '2026-06-01', '2026-08-31'),
('Winter 2026', '2026-12-01', '2027-02-28'),
('Fall 2026', '2026-09-01', '2026-12-31'),
('Spring 2027', '2027-01-15', '2027-05-15'),
('Summer 2027', '2027-06-01', '2027-08-31'),
('Winter 2027', '2027-12-01', '2028-02-28');


INSERT INTO Course (CourseName, Description) VALUES
('Database Systems', 'Learn about relational databases and SQL.'),
('Web Development', 'Frontend and Backend web technologies.'),
('Data Structures', 'Fundamental algorithms and data structures.'),
('Operating Systems', 'Introduction to OS concepts and processes.'),
('Networks', 'Computer networking concepts.'),
('Software Engineering', 'SDLC and project management.'),
('AI & ML', 'Introduction to Artificial Intelligence and Machine Learning.'),
('Cybersecurity', 'Security fundamentals and practices.');


INSERT INTO Instructor (SSN, FName, LName, Gender, Email, Address, BDate, HireDate, Salary, DepID) VALUES
('12345678901', 'Ahmed', 'Ali', 'M', 'ahmed.ali@example.com', 'Cairo', '1980-05-12', '2010-09-01', 10000, 1),
('23456789012', 'Mona', 'Hassan', 'F', 'mona.hassan@example.com', 'Alexandria', '1985-07-20', '2012-03-15', 12000, 2),
('34567890123', 'Omar', 'Youssef', 'M', 'omar.youssef@example.com', 'Giza', '1978-11-05', '2008-06-10', 15000, 1),
('45678901234', 'Sara', 'Mohamed', 'F', 'sara.mohamed@example.com', 'Cairo', '1990-02-28', '2015-01-20', 9000, 3),
('56789012345', 'Khaled', 'Ali', 'M', 'khaled.ali@example.com', 'Cairo', '1982-08-15', '2011-07-01', 11000, 4),
('67890123456', 'Laila', 'Ahmed', 'F', 'laila.ahmed@example.com', 'Alexandria', '1987-12-22', '2013-02-10', 9500, 2),
('78901234567', 'Hassan', 'Youssef', 'M', 'hassan.youssef@example.com', 'Giza', '1979-03-11', '2009-06-12', 14000, 1),
('89012345678', 'Nour', 'Mohamed', 'F', 'nour.mohamed@example.com', 'Cairo', '1991-09-05', '2016-04-18', 8800, 3);


INSERT INTO InstructorPhone (InstructorID, Phone) VALUES
(1, '01000000001'),
(1, '01000000002'),
(2, '01000000003'),
(2, '01000000004'),
(3, '01000000005'),
(4, '01000000006'),
(5, '01000000007'),
(6, '01000000008');


INSERT INTO Track (TrackName, DepID, TrackSuperID, TrackLeaderID) VALUES
('CS Advanced', 1, 1, NULL),
('IS Basics', 2, 2, NULL),
('EE Robotics', 3, 2, NULL),
('ME Design', 4, 2, NULL),
('Civil Structures', 5, 5, NULL),
('Business Analytics', 6, 6, NULL),
('Architecture Design', 7, 7, NULL),
('Cybersecurity Track', 8, 8, NULL);


INSERT INTO Student (FName, LName, Gender, Email, Address, BDate, DepID, TrackID, IntakeID) VALUES
('Ali', 'Mohamed', 'M', 'ali.mohamed@example.com', 'Cairo', '2000-01-15', 1, 1, 1),
('Fatma', 'Ahmed', 'F', 'fatma.ahmed@example.com', 'Alexandria', '2001-03-22', 2, 2, 2),
('Omar', 'Hassan', 'M', 'omar.hassan@example.com', 'Giza', '1999-07-10', 1, 1, 1),
('Sara', 'Youssef', 'F', 'sara.youssef@example.com', 'Cairo', '2002-05-18', 3, 3, 3),
('Khaled', 'Ali', 'M', 'khaled.ali@example.com', 'Cairo', '2000-09-12', 4, 4, 4),
('Laila', 'Ahmed', 'F', 'laila.ahmed@example.com', 'Alexandria', '2001-11-20', 2, 2, 2),
('Hassan', 'Youssef', 'M', 'hassan.youssef@example.com', 'Giza', '2000-06-25', 1, 1, 1),
('Nour', 'Mohamed', 'F', 'nour.mohamed@example.com', 'Cairo', '2002-02-28', 3, 3, 3);


INSERT INTO StudentPhone (StudentID, Phone) VALUES
(1, '01110000001'),
(2, '01110000002'),
(3, '01110000003'),
(4, '01110000004'),
(5, '01110000005'),
(6, '01110000006'),
(7, '01110000007'),
(8, '01110000008');


INSERT INTO Topic (TopicName, CourseID) VALUES
('SQL Basics', 1),
('Normalization', 1),
('HTML & CSS', 2),
('Algorithms', 3),
('OS Processes', 4),
('Networking Basics', 5),
('SDLC Phases', 6),
('AI Introduction', 7);


-- MCQ (1-10)
INSERT INTO Question (QuestionText, QuestionType, Degree, CourseID) VALUES
('What is a primary key?', 'MCQ', 5, 1),
('Which tag is used for a link?', 'MCQ', 5, 2),
('Time complexity of binary search?', 'MCQ', 10, 3),
('What is a process in OS?', 'MCQ', 10, 4),
('IP stands for?', 'MCQ', 5, 5),
('Which SDLC phase involves coding?', 'MCQ', 10, 6),
('AI stands for?', 'MCQ', 5, 7),
('What is a firewall used for?', 'MCQ', 5, 8),
('Which SQL command is used to remove data?', 'MCQ', 5, 1),
('Which HTML tag is used for a paragraph?', 'MCQ', 5, 2);

-- True/False (11-20)
INSERT INTO Question (QuestionText, QuestionType, Degree, CourseID) VALUES
('SQL is case-sensitive.', 'TF', 5, 1),
('CSS stands for Cascading Style Sheets.', 'TF', 5, 2),
('Stacks follow FIFO principle.', 'TF', 10, 3),
('OS manages hardware resources.', 'TF', 10, 4),
('TCP is connectionless.', 'TF', 5, 5),
('Testing is part of SDLC.', 'TF', 5, 6),
('Machine learning is part of AI.', 'TF', 5, 7),
('Firewalls protect networks.', 'TF', 5, 8),
('SELECT command is used to update data.', 'TF', 5, 1),
('HTML comments use <!-- -->.', 'TF', 5, 2);


-- MCQ 1-10
INSERT INTO Choice (QuestionID, ChoiceNo, ChoiceText, IsCorrect) VALUES
(1, 'A', 'Unique identifier', 1),
(1, 'B', 'Foreign key', 0),
(2, 'A', '<a>', 1),
(2, 'B', '<link>', 0),
(3, 'A', 'O(log n)', 1),
(3, 'B', 'O(n)', 0),
(4, 'A', 'A running program', 1),
(4, 'B', 'A thread', 0),
(5, 'A', 'Internet Protocol', 1),
(5, 'B', 'Internal Process', 0),
(6, 'A', 'Implementation', 1),
(6, 'B', 'Design', 0),
(7, 'A', 'Artificial Intelligence', 1),
(7, 'B', 'Automatic Input', 0),
(8, 'A', 'Security device', 1),
(8, 'B', 'Network cable', 0),
(9, 'A', 'DELETE', 1),
(9, 'B', 'UPDATE', 0),
(10, 'A', '<p>', 1),
(10, 'B', '<para>', 0);


INSERT INTO TrackCourse (TrackID, CourseID) VALUES
(1, 1),
(1, 3),
(2, 2),
(3, 4),
(4, 5),
(5, 6),
(6, 7),
(7, 8);

INSERT INTO Teaching (InstructorID, CourseID, TrackID, IntakeID) VALUES
(1, 1, 1, 1),
(2, 2, 2, 2),
(3, 3, 1, 1),
(4, 4, 3, 3),
(5, 5, 4, 4),
(6, 6, 5, 5),
(7, 7, 6, 6),
(8, 8, 7, 7);


INSERT INTO TrackCourse (TrackID, CourseID) VALUES
(1, 2),
(1, 4),
(2, 3),
(2, 5),
(3, 1),
(3, 6),
(4, 7),
(4, 8),
(5, 2),
(5, 3);


INSERT INTO Teaching (InstructorID, CourseID, TrackID, IntakeID) VALUES
(1, 2, 1, 1),
(1, 4, 1, 1),
(2, 3, 2, 2),
(2, 5, 2, 2),
(3, 1, 3, 1),
(3, 6, 3, 1),
(4, 7, 4, 3),
(4, 8, 4, 3),
(5, 2, 5, 4),
(5, 3, 5, 4);


-- =====================================================
-- SELECT ALL TABLES
-- =====================================================

-- 1. Department
SELECT * FROM Department;

-- 2. Intake
SELECT * FROM Intake;

-- 3. Course
SELECT * FROM Course;

-- 4. Instructor
SELECT * FROM Instructor;

-- 5. InstructorPhone
SELECT * FROM InstructorPhone;

-- 6. Track
SELECT * FROM Track;

-- 7. Student
SELECT * FROM Student;

-- 8. StudentPhone
SELECT * FROM StudentPhone;

-- 9. Topic
SELECT * FROM Topic;

-- 10. Question
SELECT * FROM Question;

-- 11. Choice
SELECT * FROM Choice;

-- 12. Exam
SELECT * FROM Exam;

-- 13. ExamQuestion
SELECT * FROM ExamQuestion;

-- 14. StudentAnswer
SELECT * FROM StudentAnswer;

-- 15. StudentResult
SELECT * FROM StudentResult;

-- 16. TrackCourse
SELECT * FROM TrackCourse;

-- 17. Teaching
SELECT * FROM Teaching;



