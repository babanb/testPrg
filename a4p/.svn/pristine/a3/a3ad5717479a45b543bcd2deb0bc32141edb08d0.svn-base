use ADOPets_Rev

alter table EConsultation
add EConsultationContactTypeId int

alter table EConsultation
drop column UserTimeZone

alter table EConsultation
drop column VetTimeZone

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
ALTER TABLE dbo.EConsultationContactType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.EConsultationContactType', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.EConsultationContactType', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.EConsultationContactType', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.EConsultation ADD CONSTRAINT
	FK_EConsultation_EConsultationContactType FOREIGN KEY
	(
	EConsultationContactTypeId
	) REFERENCES dbo.EConsultationContactType
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.EConsultation SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

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
ALTER TABLE dbo.TimeZone SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.TimeZone', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.TimeZone', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.TimeZone', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.EConsultation ADD CONSTRAINT
	FK_EConsultation_UserTimeZone FOREIGN KEY
	(
	UserTimezoneID
	) REFERENCES dbo.TimeZone
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.EConsultation ADD CONSTRAINT
	FK_EConsultation_TimeZone FOREIGN KEY
	(
	VetTimezoneID
	) REFERENCES dbo.TimeZone
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.EConsultation SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.EConsultation', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.EConsultation', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.EConsultation', 'Object', 'CONTROL') as Contr_Per 