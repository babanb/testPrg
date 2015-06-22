Use ADOPets_Rev

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
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
CREATE TABLE dbo.Tmp_ContactType
	(
	Id int NOT NULL,
	Name nvarchar(20) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_ContactType SET (LOCK_ESCALATION = TABLE)
GO
IF EXISTS(SELECT * FROM dbo.ContactType)
	 EXEC('INSERT INTO dbo.Tmp_ContactType (Id, Name)
		SELECT Id, Name FROM dbo.ContactType WITH (HOLDLOCK TABLOCKX)')
GO
ALTER TABLE dbo.Contact
	DROP CONSTRAINT FK_Contact_ContactType
GO
DROP TABLE dbo.ContactType
GO
EXECUTE sp_rename N'dbo.Tmp_ContactType', N'ContactType', 'OBJECT' 
GO
ALTER TABLE dbo.ContactType ADD CONSTRAINT
	PK_ContactType PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
select Has_Perms_By_Name(N'dbo.ContactType', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.ContactType', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.ContactType', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Contact ADD CONSTRAINT
	FK_Contact_ContactType FOREIGN KEY
	(
	ContactTypeId
	) REFERENCES dbo.ContactType
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Contact SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Contact', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Contact', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Contact', 'Object', 'CONTROL') as Contr_Per 