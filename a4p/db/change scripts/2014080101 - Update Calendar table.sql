Use ADOPets_Rev

BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Calendar
	DROP CONSTRAINT FK_Calendar_User
GO
ALTER TABLE dbo.[User] SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.[User]', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.[User]', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.[User]', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Calendar
	DROP CONSTRAINT FK_Calendar_Pet
GO
ALTER TABLE dbo.Pet SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Pet', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Pet', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Pet', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Calendar
	(
	Id int NOT NULL IDENTITY (1, 1),
	Reason nvarchar(MAX) NOT NULL,
	Physician nvarchar(MAX) NOT NULL,
	Date datetime NULL,
	Comment nvarchar(MAX) NULL,
	UserId int NOT NULL,
	PetId int NULL,
	SendNotificationMail bit NOT NULL,
	NotificationSent bit NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Calendar SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Calendar ON
GO
IF EXISTS(SELECT * FROM dbo.Calendar)
	 EXEC('INSERT INTO dbo.Tmp_Calendar (Id, Reason, Physician, Date, Comment, UserId, PetId, SendNotificationMail, NotificationSent)
		SELECT Id, Reason, Physician, Date, Comment, UserId, PetId, SendNotificationMail, NotificationSent FROM dbo.Calendar WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Calendar OFF
GO
DROP TABLE dbo.Calendar
GO
EXECUTE sp_rename N'dbo.Tmp_Calendar', N'Calendar', 'OBJECT' 
GO
ALTER TABLE dbo.Calendar ADD CONSTRAINT
	PK_Calendar PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Calendar ADD CONSTRAINT
	FK_Calendar_Pet FOREIGN KEY
	(
	PetId
	) REFERENCES dbo.Pet
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Calendar ADD CONSTRAINT
	FK_Calendar_User FOREIGN KEY
	(
	UserId
	) REFERENCES dbo.[User]
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Calendar', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Calendar', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Calendar', 'Object', 'CONTROL') as Contr_Per 