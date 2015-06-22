
use ADOPets_Rev


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
ALTER TABLE dbo.Insurance
	DROP CONSTRAINT FK_InsurancePet
GO
ALTER TABLE dbo.Pet SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Pet', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Pet', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Pet', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Insurance
	(
	Id int NOT NULL IDENTITY (1, 1),
	Name nvarchar(100) NOT NULL,
	AccountNumber nvarchar(100) NULL,
	PlanName nvarchar(100) NULL,
	GroupNumber nvarchar(100) NULL,
	StartDate date NOT NULL,
	EndDate date NOT NULL,
	Phone nvarchar(100) NULL,
	Comment nvarchar(MAX) NULL,
	SendNotificationMail bit NOT NULL,
	PetId int NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Insurance SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Insurance ON
GO
IF EXISTS(SELECT * FROM dbo.Insurance)
	 EXEC('INSERT INTO dbo.Tmp_Insurance (Id, Name, AccountNumber, PlanName, GroupNumber, StartDate, EndDate, Phone, Comment, SendNotificationMail, PetId)
		SELECT Id, Name, AccountNumber, PlanName, GroupNumber, StartDate, EndDate, Phone, Comment, SendNotificationMail, PetId FROM dbo.Insurance WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Insurance OFF
GO
DROP TABLE dbo.Insurance
GO
EXECUTE sp_rename N'dbo.Tmp_Insurance', N'Insurance', 'OBJECT' 
GO
ALTER TABLE dbo.Insurance ADD CONSTRAINT
	PK_Insurance PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Insurance ADD CONSTRAINT
	FK_InsurancePet FOREIGN KEY
	(
	PetId
	) REFERENCES dbo.Pet
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Insurance', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Insurance', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Insurance', 'Object', 'CONTROL') as Contr_Per 