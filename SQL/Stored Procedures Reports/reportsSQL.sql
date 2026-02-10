-- schema 
create schema Report


--- 1) • Report that returns the students information according to Department No parameter.
create proc Report.StuInfo @DepID int
as 
begin 
	select * from student where DepID = @DepID
end 

--- 2) • Report that takes the student ID and returns the grades of the student in all courses. %
create proc Report.StuGrades @StudentID int
as
begin
	select c.CourseName, sr.GradePercent  from StudentResult sr
	join exam e 
	on e.ExamID = sr.ExamID
	join course c
	on e.CourseID = c.CourseID
end 


--- 3) •	Report that takes the instructor ID and returns the name of the courses that he teaches and the number of student per course.
alter proc Report.sp_InstructorCoursesWithStudents
    @InstructorID int
as
begin
    select 
    c.CourseName,
    count(distinct s.StudentID) as StudentNo
    from TrackCourse tc
    join Course c
        on c.CourseID = tc.CourseID
    join Teaching ta
        on tc.CourseID = ta.CourseID
    join student s
    on s.TrackID = ta.TrackID and s.IntakeID = ta.IntakeID
    where ta.InstructorID = @InstructorID
    group by c.CourseName
end



--- 4) •	Report that takes course ID and returns its topics  
create proc Report.sp_CourseTopics @CourseID int
as
begin 

    select TopicName from topic
    where courseID = @CourseID
end 


--- 5) •  Report that takes exam number and returns the Questions in it and choices [freeform report]





--- 6)  •  Report that takes exam number and the student ID then returns the Questions in this exam with the student answers. 
create proc Report.sp_QuestionsWithStudentAnswer @ExamID int, @StudentID int
as
begin
    select q.QuestionText, Q.Degree, c.ChoiceNo as CorrectChoice, c.ChoiceText, sa.Answer as StudentAnswer from 
    Question q
    join studentAnswer sa
    on sa.QuestionID = q.questionID
    join choice c 
    on c.QuestionID = q.questionID
    where c.isCorrect = 1 and sa.studentID = @StudentID and ExamID = @ExamID
end



/*
-- Testing for Exam 

exec sp_generateExam 1, 2, 2, 2, 90

select * from question

select * from Choice

SELECT * FROM ExamQuestion

select * from StudentAnswer

select * from student

insert into StudentAnswer values(1, 1, 2, 'A'), (1, 1, 4, 'C')

select q.QuestionText, Q.Degree, c.ChoiceNo as CorrectChoice, c.ChoiceText, sa.Answer as StudentAnswer from 
    Question q
    join studentAnswer sa
    on sa.QuestionID = q.questionID
    join choice c 
    on c.QuestionID = q.questionID
    where c.isCorrect = 1 and sa.studentID = 1 and ExamID = 1



*/


