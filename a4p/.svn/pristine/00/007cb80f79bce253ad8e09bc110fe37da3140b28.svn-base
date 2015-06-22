use ADOPets_Rev

alter table [EConsultation]
alter column EconsultationStatusId int
Go

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
ALTER TABLE dbo.EConsultationStatus SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.EConsultationStatus', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.EConsultationStatus', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.EConsultationStatus', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.EConsultation ADD CONSTRAINT
	FK_EConsultation_EConsultationStatus FOREIGN KEY
	(
	EconsultationStatusId
	) REFERENCES dbo.EConsultationStatus
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.EConsultation SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.EConsultation', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.EConsultation', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.EConsultation', 'Object', 'CONTROL') as Contr_Per 