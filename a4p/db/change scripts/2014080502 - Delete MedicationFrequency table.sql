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
ALTER TABLE dbo.PetMedication
	DROP CONSTRAINT FK_PetMedication_MedicationFrequency
GO
ALTER TABLE dbo.MedicationFrequency SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.MedicationFrequency', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.MedicationFrequency', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.MedicationFrequency', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.PetMedication SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.PetMedication', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.PetMedication', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.PetMedication', 'Object', 'CONTROL') as Contr_Per 

Go

ALTER TABLE PetMedication
DROP COLUMN MedicationFrequencyId
Go

Drop Table MedicationFrequency
GO

