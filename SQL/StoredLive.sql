
-- ========== CRUD (student) ===================

-- Create 
create proc sp_createStudent
@FName nvarchar(100) = NULL,
@LName nvarchar(100) = NULL,
@Gender varchar(1) = NULL,
@Email varchar(100) = NULL,
@Address varchar(100) = NULL , 
@BDate date = NULL,
@IntakeID int = NULL, 
@DepID int = NULL, 
@TrackID int = NULL
as 
BEGIN 
 INSERT INTO student(Fname, LName, Gender, Email, Address, BDate, IntakeID, DepID, TrackID) VALUES(@FName, @LName, @Gender, @Email, @Address, @BDate, @IntakeID, @DepID, @TrackID)
END

-- Execute
EXEC sp_createStudent
    @FName = 'Ali',
    @BDate = '2002-05-10',
    @Email = 'ali@mail.com',
    @Address = 'Giza',
    @Gender = 'M'

-- Read 
alter proc sp_readStudents 
@StudentId int = NULL
as 
begin 
if @StudentId is null 
    begin 
    select * from student 
    end 
else 
    begin 
    select * from student where StudentID = @StudentId
    end 
END 


-- exec 
exec sp_readStudents @StudentId = 2


-- Update
CREATE PROC sp_updateStudent 
    @StudentId INT,
    @FName NVARCHAR(100) = NULL, 
    @LName NVARCHAR(100) = NULL, 
    @Gender VARCHAR(1) = NULL, 
    @Email VARCHAR(100) = NULL, 
    @Address VARCHAR(100) = NULL,  
    @BDate DATE = NULL, 
    @IntakeID INT = NULL, 
    @DepID INT = NULL,  
    @TrackID INT = NULL  
AS  
BEGIN  

    IF NOT EXISTS (SELECT 1 FROM Student WHERE StudentId = @StudentId)
    BEGIN
        RAISERROR('Student not found', 16, 1);
        RETURN;
    END

    
    IF @BDate IS NOT NULL AND @BDate > GETDATE() 
    BEGIN  
        RAISERROR('BirthDate cannot be in the future', 16, 1); 
        RETURN; 
    END  

    -- Update
    UPDATE Student  
    SET  
        FName    = ISNULL(@FName, FName), 
        LName    = ISNULL(@LName, LName), 
        Gender   = ISNULL(@Gender, Gender),
        Email    = ISNULL(@Email, Email),
        Address  = ISNULL(@Address, Address),
        BDate    = ISNULL(@BDate, BDate),
        IntakeID = ISNULL(@IntakeID, IntakeID),
        DepID    = ISNULL(@DepID, DepID),
        TrackID  = ISNULL(@TrackID, TrackID)
    WHERE StudentId = @StudentId;
END


-- Execute 
exec sp_updateStudent
@FName = 'Mohamed',
@LName  = 'Fathi',
@StudentId = 2


-- Delete
create proc sp_deleteStudent  @StudentId int
as 
begin 

delete from student 
    where StudentID = @StudentId
end 

exec sp_deleteStudent @StudentId = 2 





-- ================== CRUD (Exam) ==================

-- Create 
create proc sp_createExam
    @ExamDate date = NULL, 
    @duration int = NULL, 
    @CourseID int = NULL 
as 
BEGIN 

    Insert into Exam(ExamDate, duration, CourseID) values(@ExamDate, @duration, @CourseID)
END 


-- Read 
create proc sp_readExams 
@ExamID int = NULL
as 
begin 
if @ExamID is null 
    begin 
    select * from Exam 
    end 
else 
    begin 
    select * from Exam where ExamID = @ExamID
    end 
END 


-- exec 
exec sp_readExams @ExamID = 2


-- Update

CREATE proc sp_updateExam 
    @ExamDate date = NULL, 
    @duration int = NULL, 
    @CourseID int = NULL 
as 
BEGIN 
    UPDATE Exam
        SET 
            ExamDate = ISNULL(@ExamDate, ExamDate),
            duration = ISNULL(@duration, duration),
            CourseID = ISNULL(@CourseID, CourseID)

END 

-- Delete

create proc sp_deleteExam 
@ExamID int
as 
BEGIN 
    IF NOT EXISTS (SELECT 1 FROM Exam where ExamID = @ExamID)
    begin 
       RAISERROR('Exam not found', 16, 1);
        RETURN;
    end 
END 



-- =============== CRUD (Department) ============
-- Create
CREATE PROC sp_createDepartment @DepName NVARCHAR(100)
AS 
BEGIN 
    INSERT INTO Department(DepName) VALUES (@DepName)
END

GO 

-- Read
CREATE PROC sp_readDepartments @DepID INT = NULL
AS
BEGIN
    IF @DepID IS NULL
        SELECT * FROM Department
    ELSE
        SELECT * FROM Department WHERE DepID = @DepID
END

GO

-- Update
CREATE PROC sp_updateDepartment @DepID INT, @DepName NVARCHAR(100)
AS
BEGIN
    UPDATE Department SET DepName = @DepName WHERE DepID = @DepID
END

GO

-- Delete
CREATE PROC sp_deleteDepartment @DepID INT
AS
BEGIN
    DELETE FROM Department WHERE DepID = @DepID
END

exec sp_updateDepartment @depID = 2, @DepName = 'IS'




-- =============== CRUD (Course) ============
-- Create
CREATE PROC sp_createCourse @CourseName NVARCHAR(100), @Description NVARCHAR(255) = NULL
AS
BEGIN
    INSERT INTO Course(CourseName, Description) VALUES (@CourseName, @Description)
END

-- Read
CREATE PROC sp_readCourses @CourseID INT = NULL
AS
BEGIN
    IF @CourseID IS NULL
        SELECT * FROM Course
    ELSE
        SELECT * FROM Course WHERE CourseID = @CourseID
END

-- Update
CREATE PROC sp_updateCourse @CourseID INT, @CourseName NVARCHAR(100) = NULL, @Description NVARCHAR(255) = NULL
AS
BEGIN
    UPDATE Course 
    SET CourseName = ISNULL(@CourseName, CourseName),
        Description = ISNULL(@Description, Description)
    WHERE CourseID = @CourseID
END

-- Delete
CREATE PROC sp_deleteCourse @CourseID INT
AS
BEGIN
    DELETE FROM Course WHERE CourseID = @CourseID
END




-- =============== CRUD (Question) ============
-- Create
CREATE PROC sp_createQuestion 
    @Text NVARCHAR(MAX), @Type NVARCHAR(50), @Degree INT, @CourseID INT
AS
BEGIN
    INSERT INTO Question(QuestionText, QuestionType, Degree, CourseID)
    VALUES (@Text, @Type, @Degree, @CourseID)
END

-- Read
CREATE PROC sp_readQuestions @QuestionID INT = NULL
AS
BEGIN
    IF @QuestionID IS NULL
        SELECT * FROM Question
    ELSE
        SELECT * FROM Question WHERE QuestionID = @QuestionID
END

-- Update
CREATE PROC sp_updateQuestion 
    @QuestionID INT, @Text NVARCHAR(MAX) = NULL, @Type NVARCHAR(50) = NULL, @Degree INT = NULL
AS
BEGIN
    UPDATE Question 
    SET QuestionText = ISNULL(@Text, QuestionText),
        QuestionType = ISNULL(@Type, QuestionType),
        Degree = ISNULL(@Degree, Degree)
    WHERE QuestionID = @QuestionID
END

-- Delete
CREATE PROC sp_deleteQuestion @QuestionID INT
AS
BEGIN
    DELETE FROM Question WHERE QuestionID = @QuestionID
END





-- =============== CRUD (Choice) ============
-- Create
CREATE PROC sp_createChoice 
    @QuestionID INT, @ChoiceNo CHAR(1), @Text NVARCHAR(100), @IsCorrect BIT = 0
AS
BEGIN
    INSERT INTO Choice(QuestionID, ChoiceNo, ChoiceText, IsCorrect)
    VALUES (@QuestionID, @ChoiceNo, @Text, @IsCorrect)
END

-- Read (Fetch choices for a specific question)
CREATE PROC sp_readChoices @QuestionID INT
AS
BEGIN
    SELECT * FROM Choice WHERE QuestionID = @QuestionID
END

-- Update
CREATE PROC sp_updateChoice 
    @QuestionID INT, @ChoiceNo CHAR(1), @Text NVARCHAR(100) = NULL, @IsCorrect BIT = NULL
AS
BEGIN
    UPDATE Choice 
    SET ChoiceText = ISNULL(@Text, ChoiceText),
        IsCorrect = ISNULL(@IsCorrect, IsCorrect)
    WHERE QuestionID = @QuestionID AND ChoiceNo = @ChoiceNo
END

-- Delete
CREATE PROC sp_deleteChoice @QuestionID INT, @ChoiceNo CHAR(1)
AS
BEGIN
    DELETE FROM Choice WHERE QuestionID = @QuestionID AND ChoiceNo = @ChoiceNo
END




-- =============== CRUD (EXAMQUESTIONs) ============
-- Create 
create proc sp_addQuestionToExam @ExamID int, @QuestionID int
as 
begin 
    if @ExamID is null or @QuestionID is null
    begin 
       RAISERROR('ExamID OR QUESTOINID can not be null', 16, 1);
       return;
    end 
    else
    begin
        insert into ExamQuestion(ExamID,QuestionID) values(@ExamID, @QuestionID)
    end
end 
-- Read (get Exam Questions )
create proc sp_readExamQuestions @ExamID int
as
begin 
    Select q.QuestionID, q.QuestionText, q.QuestionType, q.Degree  from Exam e
    join ExamQuestion eq
    on eq.ExamID = e.ExamID
    join Question q
    on q.QuestionID = eq.QuestionID
    where eq.ExamID = @ExamID
end 

exec sp_readExamQuestions @ExamID = 1
-- Update (UPDATE A QUESTIONID RELATED TO AN EXAM)
create proc sp_updateQuestionExam @ExamID Int, @oldQuestionID int , @newQuestionID int
as 
begin 
    update ExamQuestion 
        Set QuestionID = @newQuestionID
        where ExamID = @ExamID and QuestionID = @oldQuestionID
end 

-- Delete (Delelte a questionID from Exam)
CREATE PROC sp_removeQuestionFromExam @ExamID INT, @QuestionID INT
AS
BEGIN
    DELETE FROM ExamQuestion WHERE ExamID = @ExamID AND QuestionID = @QuestionID
END






-- =============== CRUD (Instructor) ============
-- Create
CREATE PROC sp_createInstructor 
    @SSN CHAR(11), @FName NVARCHAR(50), @LName NVARCHAR(50), @Gender VARCHAR(1), 
    @Email VARCHAR(50), @Address VARCHAR(100), @BDate DATE, @Salary MONEY, @DepID INT
AS
BEGIN
    INSERT INTO Instructor(SSN, FName, LName, Gender, Email, Address, BDate, HireDate, salary, DepID)
    VALUES (@SSN, @FName, @LName, @Gender, @Email, @Address, @BDate, GETDATE(), @Salary, @DepID)
END

-- Read
CREATE PROC sp_readInstructor @InstructorID INT = NULL
AS
BEGIN
    IF @InstructorID IS NULL
        SELECT * FROM Instructor
    ELSE
        SELECT * FROM Instructor WHERE InstructorID = @InstructorID
END

-- Update
CREATE PROC sp_updateInstructor 
    @InstructorID INT, @Email VARCHAR(50) = NULL, @Address VARCHAR(100) = NULL, @Salary MONEY = NULL, @DepID INT = NULL
AS
BEGIN
    UPDATE Instructor
    SET Email = ISNULL(@Email, Email),
        Address = ISNULL(@Address, Address),
        salary = ISNULL(@Salary, salary),
        DepID = ISNULL(@DepID, DepID)
    WHERE InstructorID = @InstructorID
END

-- Delete
CREATE PROC sp_deleteInstructor @InstructorID INT
AS
BEGIN
    DELETE FROM Instructor WHERE InstructorID = @InstructorID
END

--============================================= InstructorPhone =======================================
CREATE PROC sp_addInstructorPhone 
    @InstructorID INT, 
    @Phone CHAR(12)
AS
BEGIN
    INSERT INTO InstructorPhone(InstructorID, Phone) 
    VALUES (@InstructorID, @Phone)
END

CREATE PROC sp_readInstructorPhones @InstructorID INT = NULL
AS
BEGIN
    IF @InstructorID IS NULL
        SELECT I.FName + ' ' + I.LName AS Instructor, P.Phone 
        FROM InstructorPhone P 
        JOIN Instructor I ON P.InstructorID = I.InstructorID
    ELSE
        SELECT Phone FROM InstructorPhone WHERE InstructorID = @InstructorID
END

CREATE PROC sp_updateInstructorPhone 
    @InstructorID INT, 
    @OldPhone CHAR(12), 
    @NewPhone CHAR(12)
AS
BEGIN
    UPDATE InstructorPhone 
    SET Phone = @NewPhone 
    WHERE InstructorID = @InstructorID AND Phone = @OldPhone
END


CREATE PROC sp_deleteInstructorPhone @InstructorID INT, @Phone CHAR(12)
AS
BEGIN
    DELETE FROM InstructorPhone 
    WHERE InstructorID = @InstructorID AND Phone = @Phone
END


-- =============== CRUD (Intake) ============

-- Create
CREATE PROC sp_createIntake @Name NVARCHAR(100), @Start DATE, @End DATE
AS
BEGIN
    INSERT INTO Intake(IntakeName, StartDate, EndDate) VALUES (@Name, @Start, @End)
END

-- Read
CREATE PROC sp_readIntakes @IntakeID INT = NULL
AS
BEGIN
    IF @IntakeID IS NULL SELECT * FROM Intake
    ELSE SELECT * FROM Intake WHERE IntakeID = @IntakeID
END

-- Update
CREATE PROC sp_updateIntake @IntakeID INT, @Name NVARCHAR(100), @End DATE
AS
BEGIN
    UPDATE Intake SET IntakeName = @Name, EndDate = @End WHERE IntakeID = @IntakeID
END

-- Delete
CREATE PROC sp_deleteIntake @IntakeID INT
AS
BEGIN
    DELETE FROM Intake WHERE IntakeID = @IntakeID
END



-- =============== CRUD (StudentPhone) ============

-- Create
CREATE PROC sp_addStudentPhone @StudentID INT, @Phone CHAR(12)
AS
BEGIN
    INSERT INTO StudentPhone(StudentID, Phone) VALUES (@StudentID, @Phone)
END

-- Read
CREATE PROC sp_readStudentPhones @StudentID INT
AS
BEGIN
    SELECT Phone FROM StudentPhone WHERE StudentID = @StudentID
END

-- Delete (Specific Phone)
CREATE PROC sp_deleteStudentPhone @StudentID INT, @Phone CHAR(12)
AS
BEGIN
    DELETE FROM StudentPhone WHERE StudentID = @StudentID AND Phone = @Phone
END



-- =============== CRUD (Topic) ============
-- Create
CREATE PROC sp_createTopic @TopicName NVARCHAR(75), @CourseID INT
AS
BEGIN
    INSERT INTO Topic(TopicName, CourseID) VALUES (@TopicName, @CourseID)
END

-- Read
CREATE PROC sp_readTopics @CourseID INT = NULL
AS
BEGIN
    IF @CourseID IS NULL SELECT * FROM Topic
    ELSE SELECT * FROM Topic WHERE CourseID = @CourseID
END

-- Update
CREATE PROC sp_updateTopic @TopicID INT, @TopicName NVARCHAR(75)
AS
BEGIN
    UPDATE Topic SET TopicName = @TopicName WHERE TopicID = @TopicID
END

-- Delete
CREATE PROC sp_deleteTopic @TopicID INT
AS
BEGIN
    DELETE FROM Topic WHERE TopicID = @TopicID
END


-- =============== CRUD (Track) ============
-- Create
CREATE PROC sp_createTrack @Name NVARCHAR(100), @DepID INT, @SuperID INT, @LeaderID INT
AS
BEGIN
    INSERT INTO Track(TrackName, DepID, TrackSuperID, TrackLeaderID) 
    VALUES (@Name, @DepID, @SuperID, @LeaderID)
END

-- Read
CREATE PROC sp_readTracks @TrackID INT = NULL
AS
BEGIN
    IF @TrackID IS NULL SELECT * FROM Track
    ELSE SELECT * FROM Track WHERE TrackID = @TrackID
END

-- Update
CREATE PROC sp_updateTrack 
    @TrackID INT, @Name NVARCHAR(100) = NULL, @SuperID INT = NULL, @LeaderID INT = NULL
AS
BEGIN
    UPDATE Track
    SET TrackName = ISNULL(@Name, TrackName),
        TrackSuperID = ISNULL(@SuperID, TrackSuperID),
        TrackLeaderID = ISNULL(@LeaderID, TrackLeaderID)
    WHERE TrackID = @TrackID
END

-- Delete
CREATE PROC sp_deleteTrack @TrackID INT
AS
BEGIN
    DELETE FROM Track WHERE TrackID = @TrackID
END



-- =============== CRUD (Teaching) =========================================================

CREATE PROC sp_createTeaching 
    @InstructorID INT, 
    @CourseID INT, 
    @TrackID INT, 
    @IntakeID INT
AS 
BEGIN 
    -- Check if assignment already exists to avoid PK violation
    IF EXISTS (SELECT 1 FROM Teaching 
               WHERE InstructorID = @InstructorID AND CourseID = @CourseID 
               AND TrackID = @TrackID AND IntakeID = @IntakeID)
    BEGIN
        RAISERROR('This teaching assignment already exists.', 16, 1);
        RETURN;
    END

    INSERT INTO Teaching(InstructorID, CourseID, TrackID, IntakeID) 
    VALUES (@InstructorID, @CourseID, @TrackID, @IntakeID)
END


--=======READ
CREATE PROC sp_readTeaching 
    @InstructorID INT = NULL,
    @CourseID INT = NULL
AS 
BEGIN 
    SELECT 
        I.FName + ' ' + I.LName AS InstructorName,
        C.CourseName,
        T.TrackName,
        Intk.IntakeName
    FROM Teaching Tech
    JOIN Instructor I ON Tech.InstructorID = I.InstructorID
    JOIN Course C     ON Tech.CourseID = C.CourseID
    JOIN Track T      ON Tech.TrackID = T.TrackID
    JOIN Intake Intk  ON Tech.IntakeID = Intk.IntakeID
    WHERE (@InstructorID IS NULL OR Tech.InstructorID = @InstructorID)
      AND (@CourseID IS NULL OR Tech.CourseID = @CourseID)
END


--========UPDATE
CREATE PROC sp_updateTeachingInstructor
    @OldInstructorID INT,
    @NewInstructorID INT,
    @CourseID INT,
    @TrackID INT,
    @IntakeID INT
AS
BEGIN
    UPDATE Teaching
    SET InstructorID = @NewInstructorID
    WHERE InstructorID = @OldInstructorID 
      AND CourseID = @CourseID 
      AND TrackID = @TrackID 
      AND IntakeID = @IntakeID
END

--======DELETE (UNLINK)
CREATE PROC sp_deleteTeaching 
    @InstructorID INT, 
    @CourseID INT, 
    @TrackID INT, 
    @IntakeID INT
AS 
BEGIN 
    DELETE FROM Teaching 
    WHERE InstructorID = @InstructorID 
      AND CourseID = @CourseID 
      AND TrackID = @TrackID 
      AND IntakeID = @IntakeID
END


---=================================== TRACK COURSE ==============
CREATE PROC sp_addCourseToTrack @TrackID INT, @CourseID INT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM TrackCourse WHERE TrackID = @TrackID AND CourseID = @CourseID)
    BEGIN
        RAISERROR('This course is already assigned to this track.', 16, 1);
        RETURN;
    END
    INSERT INTO TrackCourse(TrackID, CourseID) VALUES (@TrackID, @CourseID)
END



CREATE PROC sp_readTrackCourses @TrackID INT = NULL
AS
BEGIN
    SELECT T.TrackName, C.CourseName
    FROM TrackCourse TC
    JOIN Track T ON TC.TrackID = T.TrackID
    JOIN Course C ON TC.CourseID = C.CourseID
    WHERE (@TrackID IS NULL OR TC.TrackID = @TrackID)
END



CREATE PROC sp_updateTrackCourse 
    @OldTrackID INT, @CourseID INT, @NewTrackID INT
AS
BEGIN
    UPDATE TrackCourse 
    SET TrackID = @NewTrackID 
    WHERE TrackID = @OldTrackID AND CourseID = @CourseID
END



CREATE PROC sp_deleteTrackCourse @TrackID INT, @CourseID INT
AS
BEGIN
    DELETE FROM TrackCourse WHERE TrackID = @TrackID AND CourseID = @CourseID
END




-- ================================= ExamAnswer =====================
create  proc sp_getExamAnswer @ExamID int
as 
begin 
    select q.QuestionText, q.QuestionType, q.Degree , c.ChoiceNo, c.ChoiceText from ExamQuestion eq
    join Question q  on eq.QuestionID = q.QuestionID
    join Choice c on c.QuestionID = q.QuestionID
    where c.IsCorrect = 1 and 
    eq.ExamID = @ExamID
end

--=============================================================================== END OF 3 Procedures =================================================================



---================================================ REPORT PROCEDURES =======================================================================


