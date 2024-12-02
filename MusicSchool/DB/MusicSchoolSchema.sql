USE MusicSchool

CREATE TABLE [Category] (
	[Id] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] varchar(255) NOT NULL,
)

CREATE TABLE [Instrument] (
	[Id] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] varchar(255) NOT NULL,
	[CategoryId] int NOT NULL
)

CREATE TABLE [Student] (
	[Id] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[FirstName] varchar(255) NOT NULL,
	[LastName] varchar(255) NOT NULL,
	[DateOfBirth] date NOT NULL,
)

CREATE TABLE [StudentInstrument] (
	[StudentId] int NOT NULL,
	[InstrumentId] int NOT NULL,
	CONSTRAINT PK_StudentInstrument PRIMARY KEY
	(
		StudentId,
		InstrumentId
	),
	CONSTRAINT FK_StudentInstrument_Student 
	FOREIGN KEY (StudentId) REFERENCES [Student](Id),
	CONSTRAINT FK_StudentInstrument_Instrument
	FOREIGN KEY (InstrumentId) REFERENCES [Instrument](Id)
)

ALTER TABLE [Instrument]
	ADD CONSTRAINT [FK_Instrument_Category]
	FOREIGN KEY (CategoryId) REFERENCES [Category](Id)

