-- ======================================================================================= 3 PROCEDURES ==============================================

-- =============== (ExamGeneration)  [Generate Exam] ============ -- with updating degree in Exam Table
alter proc sp_generateExam    
    @TrackID int, 
    @CourseID int,    
    @NoOfTF int,    
    @NoOfMcq int,     
    @ExamDuration int = 90   
as   
begin   
    set nocount on; 

    begin try 
        begin transaction; 

        declare @ExamID int; 
        declare @TotalDegree int = 0; 
        declare @TFCount int, @MCQCount int; 

        
        --  check course belongs to track 
       
        if not exists ( 
            select 1  
            from TrackCourse  
            where TrackID = @TrackID  
              and CourseID = @CourseID 
        ) 
        begin 
            raiserror('Course is NOT assigned to this track',16,1); 
            rollback transaction; 
            return; 
        end 

        
        --  check if exam already exists 
       
        if exists ( 
            select 1  
            from Exam  
            where TrackID = @TrackID  
              and CourseID = @CourseID 
        ) 
        begin 
            raiserror('Exam already generated for this course in this track',16,1); 
            rollback transaction; 
            return; 
        end 

        
        --  check enough questions 
       
       /*
        select @TFCount = count(*)  
        from Question  
        where CourseID = @CourseID and QuestionType = 'T/F'; 


        select @MCQCount = count(*)  
        from Question  
        where CourseID = @CourseID and QuestionType = 'MCQ'; 

        if (@TFCount < @NoOfTF or @MCQCount < @NoOfMcq) 
        begin 
            raiserror('Not enough questions in Question Bank',16,1); 
            rollback transaction; 
            return; 
        end 

        */

      
        --  create exam 
       
        insert into Exam (TrackID, CourseID, ExamDate, Duration) 
        values (@TrackID, @CourseID, getdate(), @ExamDuration); 

        set @ExamID = scope_identity(); 

        
        -- insert tf questions randomly 
    
        insert into ExamQuestion (ExamID, QuestionID) 
        select top (@NoOfTF) @ExamID, QuestionID 
        from Question 
        where CourseID = @CourseID and QuestionType = 'TF' 
        order by newid(); 

        
        --  insert mcq questions randomly 
        
        insert into ExamQuestion (ExamID, QuestionID) 
        select top (@NoOfMcq) @ExamID, QuestionID 
        from Question 
        where CourseID = @CourseID and QuestionType = 'MCQ' 
        order by newid(); 

       
        --  calculate total degree 
       
        select @TotalDegree = sum(q.Degree) 
        from Question q 
        join ExamQuestion eq on q.QuestionID = eq.QuestionID 
        where eq.ExamID = @ExamID; 

       
        --  update exam total degree 
        
        update Exam 
        set TotalDegree = @TotalDegree 
        where ExamID = @ExamID; 

        commit transaction; 
    end try 
    begin catch 
        rollback transaction; 
        throw; 
    end catch 
end; 



--testing 
sp_generateExam 3, 1, 3, 3, 90

--- ==================================================================================


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

--- ================
select * from exam
select * from StudentAnswer

--- ================================================================================================



-- =================== (ExamCorrection) [Correct Exam] --===========
alter proc sp_correctexam 
    @examid int, 
    @studentid int 
as 
begin 
    declare @studegree int, 
            @totaldegree int, 
            @gradepercent decimal(5,2) 
 
    -- student degree 
    select @studegree = isnull(sum(q.degree), 0) 
    from question q 
    join choice c  
        on q.questionid = c.questionid  
       and c.iscorrect = 1 
    join studentanswer sa  
        on sa.questionid = q.questionid 
       and sa.answer = c.choiceno 
    where sa.examid = @examid 
      and sa.studentid = @studentid 
 
    -- total exam degree 
    select @totaldegree = totaldegree 
    from exam 
    where examid = @examid 
 
    -- calculate percentage 
    set @gradepercent =  
        case  
            when @totaldegree = 0 then 0 
            else (@studegree * 100.0) / @totaldegree 
        end 
 
    -- update result 
    update studentresult 
    set gradepercent = @gradepercent 
    where examid = @examid 
      and studentid = @studentid 
end





