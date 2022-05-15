USE [master]
GO

CREATE DATABASE [LaborExchangeDb]
GO

USE [LaborExchangeDb]
GO

CREATE TABLE [City]
(
	[Id] INT IDENTITY PRIMARY KEY,
	[Value] NVARCHAR(50) NOT NULL,
	[IsDeleted] BIT DEFAULT 0 NOT NULL
)

INSERT INTO [City] ([Value]) VALUES ('Не указан')

CREATE TABLE [Role]
(
	[Id] INT IDENTITY PRIMARY KEY,
	[Value] NVARCHAR(50) NOT NULL,
	[IsDeleted] BIT DEFAULT 0 NOT NULL
)

INSERT INTO [Role] ([Value]) VALUES ('Гость'), ('Претендент'), ('Работодатель'), ('Администратор')

CREATE TABLE [Profession]
(
	[Id] INT IDENTITY PRIMARY KEY,
	[Value] NVARCHAR(50) NOT NULL,
	[IsDeleted] BIT DEFAULT 0 NOT NULL
)

INSERT INTO [Profession] ([Value]) VALUES ('Неизвестна')

CREATE TABLE [Rank]
(
	[Id] INT IDENTITY PRIMARY KEY,
	[Value] NVARCHAR(50) NOT NULL,
	[IsDeleted] BIT DEFAULT 0 NOT NULL
)

INSERT INTO [Rank] ([Value]) VALUES
	('Начальное общее'),
	('Основное общее'),
	('Среднее общее'),
	('Среднее профессиональное'),
	('Высшее - бакалавриат'),
	('Высшее - специалитет, магистратура'),
	('Высшее - подготовка кадров высшей квалификации')



CREATE TABLE [Qualification]
(
	[Id] INT IDENTITY PRIMARY KEY,
	[Value] NVARCHAR(50) NOT NULL,
	[IsDeleted] BIT DEFAULT 0 NOT NULL
)

INSERT INTO [Qualification] ([Value]) VALUES ('Без квалификации')

CREATE TABLE [Education]
(
	[Id] INT IDENTITY PRIMARY KEY,
	[RankId] INT REFERENCES [Rank](Id) DEFAULT 1,
	[QualificationId] INT REFERENCES [Qualification](Id) DEFAULT 1,
	[ProfessionId] INT REFERENCES [Profession](Id) DEFAULT 1,
	[IsDeleted] BIT DEFAULT 0 NOT NULL
)

CREATE TABLE [FamilyStatus]
(
	[Id] INT IDENTITY PRIMARY KEY,
	[Value] NVARCHAR(50) NOT NULL,
	[IsDeleted] BIT DEFAULT 0 NOT NULL
)

INSERT INTO [FamilyStatus] ([Value]) VALUES ('Неизвестно')

CREATE TABLE [Activity]
(
	[Id] INT IDENTITY PRIMARY KEY,
	[Value] NVARCHAR(50) NOT NULL,
	[IsDeleted] BIT DEFAULT 0 NOT NULL
)

INSERT INTO [Activity] ([Value]) VALUES ('Неизвестно')

CREATE TABLE [Company]
(
	[Id] INT IDENTITY PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL,
	[ActivityId] INT REFERENCES [Activity](Id) DEFAULT 1,
	[Logotype] IMAGE,
	[IsDeleted] BIT DEFAULT 0 NOT NULL
)

CREATE TABLE [WorkDayRequirements]
(
	[Id] INT IDENTITY PRIMARY KEY,
	[Value] NVARCHAR(50) NOT NULL,
	[IsDeleted] BIT DEFAULT 0 NOT NULL
)

INSERT INTO [WorkDayRequirements] ([Value]) VALUES ('Не указано')

CREATE TABLE [Vacancy]
(
	[Id] INT IDENTITY PRIMARY KEY,
	[ProfessionId] INT REFERENCES [Profession](Id) NOT NULL,
	[EducationId] INT REFERENCES [Education](Id),
	[WorkDayRequirementsId] INT REFERENCES [WorkDayRequirements](Id) DEFAULT 1,
	[Payment] MONEY NOT NULL,
	[SpecialistRequirements] NVARCHAR(200),
	[Info] NVARCHAR(500),
	[IsDeleted] BIT DEFAULT 0 NOT NULL
)

CREATE TABLE [CompanyHasVacancy]
(
	[CompanyId] INT REFERENCES [Company](Id),
	[VacancyId] INT REFERENCES [Vacancy](Id),
	[IsDeleted] BIT DEFAULT 0 NOT NULL
	PRIMARY KEY ([CompanyId], [VacancyId])
)

CREATE TABLE [Gender]
(
	[Id] INT IDENTITY PRIMARY KEY,
	[Value] NVARCHAR(50) NOT NULL,
	[IsDeleted] BIT DEFAULT 0 NOT NULL
)

INSERT INTO [Gender] ([Value]) VALUES ('Неизвестен'), ('Мужской'), ('Женский')

CREATE TABLE [User]
(
	[Id] INT IDENTITY PRIMARY KEY,
	[Firstname] NVARCHAR(50) NOT NULL,
	[Middlename] NVARCHAR(50) NOT NULL,
	[Lastname] NVARCHAR(50) NOT NULL,
	[Login] NVARCHAR(50) NOT NULL,
	[Password] NVARCHAR(50) NOT NULL,
	[BornDate] DATE NOT NULL,
	[GenderId] INT REFERENCES [Gender](Id) DEFAULT 1 NOT NULL,
	[Email] NVARCHAR(50) NOT NULL,
	[Phone] NVARCHAR(20) NOT NULL,
	[RoleId] INT REFERENCES [ROLE](Id) DEFAULT 1 NOT NULL,
	[CityId] INT REFERENCES [City](Id) NOT NULL,
	[FamilyStatusId] INT REFERENCES [FamilyStatus](Id) NOT NULL,
	[HousingConditions] NVARCHAR(100),
	[Info] NVARCHAR (400),
	[Avatar] IMAGE,
	[IsDeleted] BIT DEFAULT 0 NOT NULL
)

CREATE TABLE [UserHasCompany]
(
	[UserId] INT REFERENCES [User](Id),
	[CompanyId] INT REFERENCES [Company](Id),
	[IsDeleted] BIT DEFAULT 0 NOT NULL,
	PRIMARY KEY ([UserId], [CompanyId])
)

CREATE TABLE [UserHasEducation]
(
	[UserId] INT REFERENCES [User](Id),
	[EducationId] INT REFERENCES [Education](Id) DEFAULT 1 NOT NULL,
	[IsDeleted] BIT DEFAULT 0 NOT NULL,
	PRIMARY KEY ([UserId], [EducationId])
)

CREATE TABLE [ReasonOfDismissal]
(
	[Id] INT IDENTITY PRIMARY KEY,
	[Value] NVARCHAR(50) NOT NULL,
	[IsDeleted] BIT DEFAULT 0 NOT NULL
)

INSERT INTO [ReasonOfDismissal] ([Value]) VALUES ('Не указана')

CREATE TABLE [JobRequest]
(
	[Id] INT IDENTITY PRIMARY KEY,
	[ProfessionId] INT REFERENCES [Profession](Id) NOT NULL,
	[SalaryRequirements] MONEY,
	[WorkDayRequirementsId] INT REFERENCES [WorkDayRequirements](Id) DEFAULT 1,
	[Info] NVARCHAR(500),
	[IsDeleted] BIT DEFAULT 0 NOT NULL
)

CREATE TABLE [UserHasJobRequest]
(
	[UserId] INT REFERENCES [User](Id),
	[JobRequestId] INT REFERENCES [JobRequest](Id),
	[IsDeleted] BIT DEFAULT 0 NOT NULL,
	PRIMARY KEY ([UserId], [JobRequestId])
)

CREATE TABLE [UserHasJob]
(
	[UserId] INT REFERENCES [User](Id),
	[CompanyId] INT REFERENCES [Company](Id),
	[ProfessionId] INT REFERENCES [Profession](Id),
	[ReasonOfDismissalId] INT REFERENCES [ReasonOfDismissal](Id),
	[IsDeleted] BIT DEFAULT 0 NOT NULL,
	PRIMARY KEY([UserId], [CompanyId], [ProfessionId])
)
GO

CREATE PROCEDURE [GetVacancy]
	@professionId INT
AS
BEGIN
	SELECT *
	FROM [Vacancy]
	WHERE [Vacancy].[ProfessionId] = @professionId
END
GO

CREATE PROCEDURE [GetSearchersAmount]
	@educationId INT
AS
BEGIN
	SELECT * 
	FROM [User], [JobRequest], [UserHasJobRequest], [UserHasEducation], [Education]
	WHERE [User].[Id] = [UserHasJobRequest].[UserId]
		AND [JobRequest].[Id] = [UserHasJobRequest].[JobRequestId]
		AND [User].[Id] = [UserHasEducation].[UserId]
		AND [UserHasEducation].[EducationId] = @educationId
END
GO

CREATE TRIGGER [CitySoftDelete]
ON [City]
INSTEAD OF DELETE
AS
BEGIN
	UPDATE [City] SET [IsDeleted] = 1
	WHERE [Id] = (SELECT [Id] FROM [deleted])
END
GO

CREATE TRIGGER [RoleSoftDelete]
ON [Role]
INSTEAD OF DELETE
AS
BEGIN
	UPDATE [Role] SET [IsDeleted] = 1
	WHERE [Id] = (SELECT [Id] FROM [deleted])
END
GO

CREATE TRIGGER [ProfessionSoftDelete]
ON [Profession]
INSTEAD OF DELETE
AS
BEGIN
	UPDATE [Profession] SET [IsDeleted] = 1
	WHERE [Id] = (SELECT [Id] FROM [deleted])
END
GO

CREATE TRIGGER [RankSoftDelete]
ON [Rank]
INSTEAD OF DELETE
AS
BEGIN
	UPDATE [Rank] SET [IsDeleted] = 1
	WHERE [Id] = (SELECT [Id] FROM [deleted])
END
GO

CREATE TRIGGER [EducationSoftDelete]
ON [Education]
INSTEAD OF DELETE
AS
BEGIN
	UPDATE [Education] SET [IsDeleted] = 1
	WHERE [Id] = (SELECT [Id] FROM [deleted])
END
GO

CREATE TRIGGER [QualificationSoftDelete]
ON [Qualification]
INSTEAD OF DELETE
AS
BEGIN
	UPDATE [Qualification] SET [IsDeleted] = 1
	WHERE [Id] = (SELECT [Id] FROM [deleted])
END
GO

CREATE TRIGGER [FamilyStatusSoftDelete]
ON [FamilyStatus]
INSTEAD OF DELETE
AS
BEGIN
	UPDATE [FamilyStatus] SET [IsDeleted] = 1
	WHERE [Id] = (SELECT [Id] FROM [deleted])
END
GO

CREATE TRIGGER [ActivitySoftDelete]
ON [Activity]
INSTEAD OF DELETE
AS
BEGIN
	UPDATE [Activity] SET [IsDeleted] = 1
	WHERE [Id] = (SELECT [Id] FROM [deleted])
END
GO

CREATE TRIGGER [CompanySoftDelete]
ON [Company]
INSTEAD OF DELETE
AS
BEGIN
	UPDATE [Company] SET [IsDeleted] = 1
	WHERE [Id] = (SELECT [Id] FROM [deleted])
END
GO

CREATE TRIGGER [WorkDayRequirementsSoftDelete]
ON [WorkDayRequirements]
INSTEAD OF DELETE
AS
BEGIN
	UPDATE [WorkDayRequirements] SET [IsDeleted] = 1
	WHERE [Id] = (SELECT [Id] FROM [deleted])
END
GO

CREATE TRIGGER [VacancySoftDelete]
ON [Vacancy]
INSTEAD OF DELETE
AS
BEGIN
	UPDATE [Vacancy] SET [IsDeleted] = 1
	WHERE [Id] = (SELECT [Id] FROM [deleted])
END
GO

CREATE TRIGGER [CompanyHasVacancySoftDelete]
ON [CompanyHasVacancy]
INSTEAD OF DELETE
AS
BEGIN
	UPDATE [CompanyHasVacancy] SET [IsDeleted] = 1
	WHERE [CompanyId] = (SELECT [CompanyId] FROM [deleted])
	AND [VacancyId] = (SELECT [VacancyId] FROM [deleted])
END
GO

CREATE TRIGGER [UserHasEducationSoftDelete]
ON [UserHasEducation]
INSTEAD OF DELETE
AS
BEGIN
	UPDATE [UserHasEducation] SET [IsDeleted] = 1
	WHERE [UserId] = (SELECT [UserId] FROM [deleted])
	AND [EducationId] = (SELECT [EducationId] FROM [deleted])
END
GO

CREATE TRIGGER [ReasonOfDismissalSoftDelete]
ON [ReasonOfDismissal]
INSTEAD OF DELETE
AS
BEGIN
	UPDATE [ReasonOfDismissal] SET [IsDeleted] = 1
	WHERE [Id] = (SELECT [Id] FROM [deleted])
END
GO

CREATE TRIGGER [JobRequestSoftDelete]
ON [JobRequest]
INSTEAD OF DELETE
AS
BEGIN
	UPDATE [JobRequest] SET [IsDeleted] = 1
	WHERE [Id] = (SELECT [Id] FROM [deleted])
END
GO

CREATE TRIGGER [UserHasJobRequestSoftDelete]
ON [UserHasJobRequest]
INSTEAD OF DELETE
AS
BEGIN
	UPDATE [UserHasJobRequest] SET [IsDeleted] = 1
	WHERE [UserId] = (SELECT [UserId] FROM [deleted])
	AND [JobRequestId] = (SELECT [JobRequestId] FROM [deleted])
END
GO

CREATE TRIGGER [UserHasJobSoftDelete]
ON [UserHasJob]
INSTEAD OF DELETE
AS
BEGIN
	UPDATE [UserHasJob] SET [IsDeleted] = 1
	WHERE [UserId] = (SELECT [UserId] FROM [deleted])
	AND [CompanyId] = (SELECT [CompanyId] FROM [deleted])
	AND [ProfessionId] = (SELECT [ProfessionId] FROM [deleted])
END
GO

CREATE TRIGGER [GenderSoftDelete]
ON [Gender]
INSTEAD OF DELETE
AS
BEGIN
	UPDATE [Gender] SET [IsDeleted] = 1
	WHERE [Id] = (SELECT [Id] FROM [deleted])
END
GO

CREATE TRIGGER [UserSoftDelete]
ON [User]
INSTEAD OF DELETE
AS
BEGIN
	UPDATE [User] SET [IsDeleted] = 1
	WHERE [Id] = (SELECT [Id] FROM [deleted])
END
GO

CREATE TRIGGER [UserHasCompanySoftDelete]
ON [UserHasCompany]
INSTEAD OF DELETE
AS
BEGIN
	UPDATE [UserHasCompany] SET [IsDeleted] = 1
	WHERE [UserId] = (SELECT [UserId] FROM [deleted])
	AND [CompanyId] = (SELECT [CompanyId] FROM [deleted])
END
GO