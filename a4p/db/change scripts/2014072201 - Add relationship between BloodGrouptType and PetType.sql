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
ALTER TABLE dbo.PetType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.PetType', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.PetType', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.PetType', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.BloodGroupType ADD CONSTRAINT
	FK_BloodGroupType_PetType FOREIGN KEY
	(
	PetTypeId
	) REFERENCES dbo.PetType
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.BloodGroupType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.BloodGroupType', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.BloodGroupType', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.BloodGroupType', 'Object', 'CONTROL') as Contr_Per 