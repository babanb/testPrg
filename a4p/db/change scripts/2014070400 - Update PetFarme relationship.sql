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
ALTER TABLE dbo.Pet
	DROP CONSTRAINT FK_Pet_Farmer
GO
ALTER TABLE dbo.Farmer SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Farmer', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Farmer', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Farmer', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Pet ADD CONSTRAINT
	FK_Pet_Farmer FOREIGN KEY
	(
	FarmerId
	) REFERENCES dbo.Farmer
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  SET NULL 
	
GO
ALTER TABLE dbo.Pet SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Pet', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Pet', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Pet', 'Object', 'CONTROL') as Contr_Per 