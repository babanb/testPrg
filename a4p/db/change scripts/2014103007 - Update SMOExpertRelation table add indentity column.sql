/*
   Tuesday, October 14, 20145:34:42 PM
   User: sa
   Server: ARYANSVR\SQL2008
   Database: ADOPets_rev
   Application: 
*/

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
ALTER TABLE dbo.SMOExpertRelation
	DROP CONSTRAINT FK_SMOExpertRelation_Veterinarian
GO
ALTER TABLE dbo.Veterinarian SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SMOExpertRelation
	DROP CONSTRAINT FK_SMOExpertRelation_SMORequest
GO
ALTER TABLE dbo.SMORequest SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_SMOExpertRelation
	(
	ID bigint NOT NULL IDENTITY (1, 1),
	SMORequestID bigint NULL,
	VetExpertID int NULL,
	AssingedDate datetime NULL,
	ExpertResponse nvarchar(MAX) NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_SMOExpertRelation SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_SMOExpertRelation ON
GO
IF EXISTS(SELECT * FROM dbo.SMOExpertRelation)
	 EXEC('INSERT INTO dbo.Tmp_SMOExpertRelation (ID, SMORequestID, VetExpertID, AssingedDate, ExpertResponse)
		SELECT ID, SMORequestID, VetExpertID, AssingedDate, ExpertResponse FROM dbo.SMOExpertRelation WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_SMOExpertRelation OFF
GO
DROP TABLE dbo.SMOExpertRelation
GO
EXECUTE sp_rename N'dbo.Tmp_SMOExpertRelation', N'SMOExpertRelation', 'OBJECT' 
GO
ALTER TABLE dbo.SMOExpertRelation ADD CONSTRAINT
	PK_SMOExpertRelation PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.SMOExpertRelation ADD CONSTRAINT
	FK_SMOExpertRelation_SMORequest FOREIGN KEY
	(
	SMORequestID
	) REFERENCES dbo.SMORequest
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SMOExpertRelation ADD CONSTRAINT
	FK_SMOExpertRelation_Veterinarian FOREIGN KEY
	(
	VetExpertID
	) REFERENCES dbo.Veterinarian
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
