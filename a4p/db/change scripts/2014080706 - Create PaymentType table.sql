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
CREATE TABLE dbo.PaymentType
	(
	Id int NOT NULL IDENTITY (1, 1),
	Name nvarchar(200) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.PaymentType ADD CONSTRAINT
	PK_PaymentType PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.PaymentType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.PaymentType', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.PaymentType', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.PaymentType', 'Object', 'CONTROL') as Contr_Per 
Go

Insert into PaymentType Values ('Yearly'),('Monthly'),('Trial'),('ThirdPartyCompany')