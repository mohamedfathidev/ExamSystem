

create table Department
(
	DepID int identity(1,1) primary key,
	DepName nvarchar(100) not null
)

create table Course 
(
	CourseID INT IDENTITY(1,1) PRIMARY KEY,
	CourseName nvarchar(100) not null,
	Description nvarchar(255)
)

create table Instructor
(
	InstructorID int identity(1,1) primary key,

	SSN char(11) unique not null,
	FName nvarchar(50) not null,
	LName nvarchar(50),
	Gender varchar(1) not null,
	Email varchar(50),
	Address varchar(100),
	BDate Date,
	HireDate Date,
	salary money,
	DepID int,

	-- foreign key 
	constraint FK_Ins_Dep
	foreign key (DepID) references Department(DepID)
)

create table InstructorPhone 
(
	InstructorID int, 
	Phone char(12),
	Primary Key(InstructorID, Phone),

	-- FK
	constraint FK_Instructor_Phone
	foreign key (InstructorID) references Instructor(InstructorID) on delete cascade -- auto delete phone when instructor deleted 
)



create table Topic 
(
	TopicID INT IDENTITY(1,1) PRIMARY KEY, 
	TopicName nvarchar(75), 
	CourseID INT, 

	-- FK
	constraint FK_Topic_Course
	foreign key (CourseID) references Course(CourseID)

)

CREATE TABLE Question (
    QuestionID INT IDENTITY(1,1) PRIMARY KEY,

    QuestionText NVARCHAR(MAX) NOT NULL,
    QuestionType NVARCHAR(50), 
	Degree INT,

    CourseID INT,
    CONSTRAINT FK_Question_Course 
        FOREIGN KEY (CourseID) REFERENCES Course(CourseID)
)


create table Choice 
(
	QuestionID int, 
	ChoiceNo char(1), -- A, B , C 
	PRIMARY KEY(QuestionID, ChoiceNo),
	ChoiceText nvarchar(100) not null,
	IsCorrect BIT not null Default 0, -- default false (0)

	-- FK
	constraint FK_Question_Choice
	foreign key (QuestionID) references Question(QuestionID)

)

create table Exam 
(
	ExamID int identity(1,1) Primary key, 
	ExamDate date, 
	TotalDegree int, 
	duration int, 
	CourseID int,

	-- FK
	constraint FK_Exam_Course
	foreign key (CourseID) references Course(CourseID)
)


create table ExamQuestion
(
	ExamID INT, 
	QuestionID INT, 
	
	PRIMARY KEY(ExamID, QuestionID), 

	-- FK
	constraint FK_EQ_Exam
	foreign key (ExamID) references Exam(ExamID),

	constraint FK_EQ_Question 
	foreign key (QuestionID) references Question(QuestionID)
)


create table Track
(
	TrackID int identity(1,1) Primary key, 
	TrackName nvarchar(100),
	DepID int, 
	TrackSuperID int,
	TrackLeaderID int,

	-- FK
	constraint FK_Track_Department
	foreign key (DepID) references Department(DepID),

	constraint FK_Track_InsSuper
	foreign key (TrackSuperID) references Instructor(InstructorID)

	-- Student FK later after student creation 

)


create table student 
(
	StudentID int identity(1,1) Primary key, 
	FName nvarchar(50) not null,
	LName nvarchar(50),
	Gender varchar(1) not null,
	Email varchar(50),
	Address varchar(100),
	BDate Date,
	IntakeID int, 


	-- Columns 
	DepID int, 
	TrackID int, 
	-- FK

	constraint FK_Student_Department
	foreign key (DepID) references	Department(DepID),

	constraint FK_Student_Track
	foreign key (TrackID) references Track(TrackID)
)

alter table Track
add constraint FK_Track_StudentLeader
foreign key (TrackLeaderID) references Student(StudentID)

create table StudentPhone 
(
	StudentID int, 
	Phone char(12),
	Primary Key(StudentID, Phone),

	-- FK
	constraint FK_Student_Phone
	foreign key (StudentID) references Student(StudentID) on delete cascade 
)

create table StudentAnswer
(
	StudentAnswerID int identity(1,1) primary key,
	StudentID int,
	ExamID int,
	QuestionID int,
	Answer char(1), -- A, B, C

	-- FK
	constraint FK_SA_Student
	foreign key (StudentID) references Student(StudentID),

	constraint FK_SA_Exam
	foreign key (ExamID) references Exam(ExamID),

	constraint FK_SA_Question
	foreign key (QuestionID) references Question(QuestionID)
)


create table StudentResult
(
	StudentID int,
	ExamID int,
	GradePercent decimal(5,2),
	Primary Key(StudentID, ExamID),

	-- FK
	constraint FK_SR_Student
	foreign key (StudentID) references Student(StudentID),

	constraint FK_SR_Exam
	foreign key (ExamID) references Exam(ExamID)
)


create table TrackCourse
(
	TrackID int,
	CourseID int,
	Primary Key(TrackID, CourseID),

	-- FK
	constraint FK_TC_Track
	foreign key (TrackID) references Track(TrackID),

	constraint FK_TC_Course
	foreign key (CourseID) references Course(CourseID)
)


CREATE TABLE Intake
(
	IntakeID int identity(1,1) primary key,
	IntakeName nvarchar(100),
	StartDate date,
	EndDate date
)


alter table student
add constraint FK_Student_Intake
foreign key (IntakeID) references Intake(IntakeID)


create table Teaching
(
	InstructorID INT, 
	CourseID int,
	TrackID int, 
	IntakeID int, 

	Primary key(InstructorID, CourseID, TrackID, IntakeID),


	-- FK 
	constraint FK_Teaching_Instructor
	foreign key (InstructorID) references Instructor(InstructorID),

	constraint FK_Teaching_Course
	foreign key (CourseID) references Course(CourseID),

	constraint FK_Teaching_Track
	foreign key (TrackID) references Track(TrackID),

	constraint FK_Teaching_Intake
	foreign key (IntakeID) references Intake(IntakeID)


)