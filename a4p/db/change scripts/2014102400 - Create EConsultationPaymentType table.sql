use AdoPets_Rev

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
CREATE TABLE dbo.EConsultationPaymentType
	(
	Id int NOT NULL,
	Name nvarchar(100) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.EConsultationPaymentType ADD CONSTRAINT
	PK_EConsultationPaymentType PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.EConsultationPaymentType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.EConsultationPaymentType', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.EConsultationPaymentType', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.EConsultationPaymentType', 'Object', 'CONTROL') as Contr_Per 


