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
ALTER TABLE dbo.PaymentType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.PaymentType', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.PaymentType', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.PaymentType', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CreditCardType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CreditCardType', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CreditCardType', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CreditCardType', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.BillingInformation ADD CONSTRAINT
	FK_BillingInformation_PaymentType FOREIGN KEY
	(
	PaymentTypeId
	) REFERENCES dbo.PaymentType
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.BillingInformation ADD CONSTRAINT
	FK_BillingInformation_CreditCardType FOREIGN KEY
	(
	CreditCardTypeId
	) REFERENCES dbo.CreditCardType
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.BillingInformation SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.BillingInformation', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.BillingInformation', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.BillingInformation', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.PaymentHistory ADD CONSTRAINT
	FK_PaymentHistory_BillingInformation FOREIGN KEY
	(
	BillingInformationId
	) REFERENCES dbo.BillingInformation
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.PaymentHistory SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.PaymentHistory', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.PaymentHistory', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.PaymentHistory', 'Object', 'CONTROL') as Contr_Per 