/*
   Wednesday, November 05, 201412:11:30 PM
   User: sa
   Server: ARYANSVR\SQL2008
   Database: ADOPets_rev
   Application: 
*/
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
ALTER TABLE dbo.SMOExpertRelation
	DROP CONSTRAINT FK_SMOExpertRelation_User
GO
ALTER TABLE dbo.[User] SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
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
ALTER TABLE dbo.SMOExpertRelation SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
