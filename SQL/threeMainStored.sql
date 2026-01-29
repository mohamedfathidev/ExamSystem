-- ======================================================================================= 3 PROCEDURES ==============================================

-- =============== (ExamGeneration)  [Generate Exam] ============ -- with updating degree in Exam Table
create proc sp_generateExam  
    @CourseID int,  
    @NoOfTF int,  
    @NoOfMcq int,  
    @ExamID int = null,  
    @ExamDuration int = 90 
as 
begin 
    set nocount on; 
    begin transaction; 
 
    declare @totalDegree int = 0; 
 
    -- create exam if not exists 
    if @ExamID is null or not exists (select 1 from Exam where ExamID = @ExamID) 
    begin 
        insert into Exam(ExamDate, Duration, CourseID) 
        values (getdate(), @ExamDuration, @CourseID); 
 
        set @ExamID = scope_identity(); 
    end 
 
    -- insert tf questions 
    insert into ExamQuestion(ExamID, QuestionID) 
    select top (@NoOfTF) @ExamID, QuestionID 
    from Question 
    where QuestionType = 'T/F' and CourseID = @CourseID 
    order by newid(); 
 
    -- insert mcq questions 
    insert into ExamQuestion(ExamID, QuestionID) 
    select top (@NoOfMcq) @ExamID, QuestionID 
    from Question 
    where QuestionType = 'MCQ' and CourseID = @CourseID 
    order by newid(); 
 
    -- calculate total degree 
    select @totalDegree = sum(q.Degree) 
    from Question q 
    join ExamQuestion eq on q.QuestionID = eq.QuestionID 
    where eq.ExamID = @ExamID; 
 
    -- update exam 
    update Exam 
    set TotalDegree = @totalDegree 
    where ExamID = @ExamID; 
 
    commit transaction; 
end


-- =================== (ExamCorrection) [Correct Exam] --===========

-- Backend desktop => if no radioButton checked text answer will be (n)

create proc sp_CorrectExam
    @ExamID int,
    @StudentID int
as
begin
    select sum(q.Degree) as Score
    from Question q
    join Choice c on q.QuestionID = c.QuestionID and c.IsCorrect = 1
    join StudentAnswer sa 
        on sa.QuestionID = q.QuestionID
    where sa.ExamID = @ExamID
      and sa.StudentID = @StudentID
      and sa.Answer = c.ChoiceNo
end




--===================== store STUDENT ANSWER when submitting  ===================
create proc sp_StudentAnswer @ExamID int, @StudentID int, @QuestionID int, @Answer char(1)
as 
begin
    SET NOCOUNT ON; -- avoid rows affected messages 

    if not exists(SELECT 1 FROM StudentAnswer  WHERE StudentID=@StudentID AND ExamID=@ExamID AND QuestionID=@QuestionID )
    begin
        Insert into StudentAnswer(StudentID, ExamID, QuestionID, Answer) values(@StudentID, @ExamID, @QuestionID, @Answer)
    end
    else
    begin 
        update studentAnswer
            set Answer = @Answer
            where StudentID=@StudentID AND ExamID=@ExamID AND QuestionID=@QuestionID
    end 
end 