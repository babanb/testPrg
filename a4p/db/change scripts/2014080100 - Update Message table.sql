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
ALTER TABLE dbo.Message
	DROP CONSTRAINT FK_Message_User
GO
ALTER TABLE dbo.Message
	DROP CONSTRAINT FK_Message_User1
GO
ALTER TABLE dbo.Message
	DROP CONSTRAINT FK_Message_User2
GO
ALTER TABLE dbo.[User] SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.[User]', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.[User]', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.[User]', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Message
	DROP CONSTRAINT FK_Message_MessageType
GO
ALTER TABLE dbo.MessageType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.MessageType', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.MessageType', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.MessageType', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Message
	(
	Id int NOT NULL IDENTITY (1, 1),
	UserId int NOT NULL,
	FromUserId int NOT NULL,
	ToUserId int NOT NULL,
	Date datetime NOT NULL,
	Subject nvarchar(MAX) NOT NULL,
	Body nvarchar(MAX) NOT NULL,
	MessageTypeId int NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Message SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Message ON
GO
IF EXISTS(SELECT * FROM dbo.Message)
	 EXEC('INSERT INTO dbo.Tmp_Message (Id, UserId, FromUserId, ToUserId, Date, Subject, Body, MessageTypeId)
		SELECT Id, UserId, FromUserId, ToUserId, Date, Subject, Body, MessageTypeId FROM dbo.Message WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Message OFF
GO
DROP TABLE dbo.Message
GO
EXECUTE sp_rename N'dbo.Tmp_Message', N'Message', 'OBJECT' 
GO
ALTER TABLE dbo.Message ADD CONSTRAINT
	PK_Message PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Message ADD CONSTRAINT
	FK_Message_MessageType FOREIGN KEY
	(
	MessageTypeId
	) REFERENCES dbo.MessageType
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Message ADD CONSTRAINT
	FK_Message_User FOREIGN KEY
	(
	UserId
	) REFERENCES dbo.[User]
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Message ADD CONSTRAINT
	FK_Message_User1 FOREIGN KEY
	(
	FromUserId
	) REFERENCES dbo.[User]
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Message ADD CONSTRAINT
	FK_Message_User2 FOREIGN KEY
	(
	ToUserId
	) REFERENCES dbo.[User]
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Message', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Message', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Message', 'Object', 'CONTROL') as Contr_Per 