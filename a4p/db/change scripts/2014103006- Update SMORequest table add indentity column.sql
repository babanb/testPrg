
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
ALTER TABLE dbo.SMORequest
	DROP CONSTRAINT FK_SMORequest_User
GO
ALTER TABLE dbo.[User] SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SMORequest
	DROP CONSTRAINT FK_SMORequest_Veterinarian
GO
ALTER TABLE dbo.Veterinarian SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SMORequest
	DROP CONSTRAINT FK_SMORequest_SMORequestStatus
GO
ALTER TABLE dbo.SMORequestStatus SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SMORequest
	DROP CONSTRAINT FK_SMORequest_Pet
GO
ALTER TABLE dbo.Pet SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_SMORequest
	(
	ID bigint NOT NULL IDENTITY (1, 1),
	Title nvarchar(200) NULL,
	PetId int NULL,
	Diagnosis nvarchar(MAX) NULL,
	DateOfOnSet date NULL,
	Comments nvarchar(MAX) NULL,
	Symptoms1 nvarchar(MAX) NULL,
	Symptoms2 nvarchar(MAX) NULL,
	Symptoms3 nvarchar(MAX) NULL,
	FirstOpinion nvarchar(MAX) NULL,
	SMORequestStatusId int NULL,
	VetDirectorID int NULL,
	Question nvarchar(MAX) NULL,
	VetComment nvarchar(MAX) NULL,
	ValidatedOn datetime NULL,
	ClosedOn datetime NULL,
	InCompleteMedicalRecord bit NULL,
	IsDeleted bit NULL,
	UserId int NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_SMORequest SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_SMORequest ON
GO
IF EXISTS(SELECT * FROM dbo.SMORequest)
	 EXEC('INSERT INTO dbo.Tmp_SMORequest (ID, Title, PetId, Diagnosis, DateOfOnSet, Comments, Symptoms1, Symptoms2, Symptoms3, FirstOpinion, SMORequestStatusId, VetDirectorID, Question, VetComment, ValidatedOn, ClosedOn, InCompleteMedicalRecord, IsDeleted, UserId)
		SELECT ID, Title, PetId, Diagnosis, DateOfOnSet, Comments, Symptoms1, Symptoms2, Symptoms3, FirstOpinion, SMORequestStatusId, VetDirectorID, Question, VetComment, ValidatedOn, ClosedOn, InCompleteMedicalRecord, IsDeleted, UserId FROM dbo.SMORequest WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_SMORequest OFF
GO
ALTER TABLE dbo.SMOInvestigation
	DROP CONSTRAINT FK_SMOInvestigation_SMORequest
GO
ALTER TABLE dbo.SMOExpertRelation
	DROP CONSTRAINT FK_SMOExpertRelation_SMORequest
GO
DROP TABLE dbo.SMORequest
GO
EXECUTE sp_rename N'dbo.Tmp_SMORequest', N'SMORequest', 'OBJECT' 
GO
ALTER TABLE dbo.SMORequest ADD CONSTRAINT
	PK_SMORequest PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.SMORequest ADD CONSTRAINT
	FK_SMORequest_Pet FOREIGN KEY
	(
	PetId
	) REFERENCES dbo.Pet
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SMORequest ADD CONSTRAINT
	FK_SMORequest_SMORequestStatus FOREIGN KEY
	(
	SMORequestStatusId
	) REFERENCES dbo.SMORequestStatus
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SMORequest ADD CONSTRAINT
	FK_SMORequest_Veterinarian FOREIGN KEY
	(
	VetDirectorID
	) REFERENCES dbo.Veterinarian
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SMORequest ADD CONSTRAINT
	FK_SMORequest_User FOREIGN KEY
	(
	UserId
	) REFERENCES dbo.[User]
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
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
ALTER TABLE dbo.SMOExpertRelation SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SMOInvestigation ADD CONSTRAINT
	FK_SMOInvestigation_SMORequest FOREIGN KEY
	(
	SMORequestID
	) REFERENCES dbo.SMORequest
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SMOInvestigation SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
