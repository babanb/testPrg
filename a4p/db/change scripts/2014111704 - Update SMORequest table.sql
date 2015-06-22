Use [ADOPets_rev]

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
ALTER TABLE dbo.Veterinarian SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SMORequest ADD
	RequestReason nvarchar(MAX) NULL,
	MedicalHistoryComment nvarchar(MAX) NULL,
	AdditionalInformation nvarchar(MAX) NULL,
	SMOSubmittedBy int NULL
GO
ALTER TABLE dbo.SMORequest ADD CONSTRAINT
	FK_SMORequest_Veterinarian1 FOREIGN KEY
	(
	SMOSubmittedBy
	) REFERENCES dbo.Veterinarian
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SMORequest SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
