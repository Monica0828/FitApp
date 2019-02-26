CREATE TABLE GymClass (
	[GymClassId] int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [Name] nvarchar(255) NOT NULL,
    [MaxClients] int
);
CREATE TABLE GymClassSchedule (
	[ScheduleId] int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [Date] DateTime  NOT NULL,
	[UserId] nvarchar(128) NOT NULL,
	[GymClassId] int NOT NULL
);
CREATE TABLE Appointment (
	[UserId] nvarchar(128) NOT NULL,
    [ScheduleId] int  NOT NULL
);
