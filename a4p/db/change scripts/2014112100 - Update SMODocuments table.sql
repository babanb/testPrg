use	[ADOPets_rev]
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
ALTER TABLE dbo.SMODocuments
	DROP CONSTRAINT FK_SMODocuments_DocumentSubType
GO
ALTER TABLE dbo.DocumentSubType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SMODocuments
	DROP CONSTRAINT FK_SMODocuments_SMORequest
GO
ALTER TABLE dbo.SMORequest SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_SMODocuments
	(
	Id int NOT NULL IDENTITY (1, 1),
	SMOId bigint NULL,
	DocumentSubTypeId int NULL,
	DocumentName nvarchar(MAX) NULL,
	DocumentPath nvarchar(MAX) NULL,
	UploadDate datetime NULL,
	IsDeleted bit NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_SMODocuments SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_SMODocuments ON
GO
IF EXISTS(SELECT * FROM dbo.SMODocuments)
	 EXEC('INSERT INTO dbo.Tmp_SMODocuments (Id, SMOId, DocumentSubTypeId, DocumentName, DocumentPath, UploadDate, IsDeleted)
		SELECT Id, SMOId, DocumentSubTypeId, DocumentName, DocumentPath, UploadDate, IsDeleted FROM dbo.SMODocuments WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_SMODocuments OFF
GO
DROP TABLE dbo.SMODocuments
GO
EXECUTE sp_rename N'dbo.Tmp_SMODocuments', N'SMODocuments', 'OBJECT' 
GO
ALTER TABLE dbo.SMODocuments ADD CONSTRAINT
	PK_SMODocuments PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.SMODocuments ADD CONSTRAINT
	FK_SMODocuments_SMORequest FOREIGN KEY
	(
	SMOId
	) REFERENCES dbo.SMORequest
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SMODocuments ADD CONSTRAINT
	FK_SMODocuments_DocumentSubType FOREIGN KEY
	(
	DocumentSubTypeId
	) REFERENCES dbo.DocumentSubType
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
