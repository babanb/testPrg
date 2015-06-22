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
ALTER TABLE dbo.Subscription
	DROP CONSTRAINT FK_SubscriptionBase_PaymentType
GO
ALTER TABLE dbo.PaymentType SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.PaymentType', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.PaymentType', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.PaymentType', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Subscription
	(
	Id int NOT NULL IDENTITY (1, 1),
	Name varchar(200) NULL,
	Description nvarchar(MAX) NULL,
	IsPromotionCode bit NOT NULL,
	PromotionCode nvarchar(100) NULL,
	IsTrial bit NULL,
	TrialStartDate date NULL,
	TrialEndDate date NULL,
	TrialDuration int NOT NULL,
	PaymentTypeId int NULL,
	MaxOwnerCount int NOT NULL,
	MaxContactCount int NOT NULL,
	MaxPetCount int NOT NULL,
	HasMRA bit NOT NULL,
	MRACount int NULL,
	MRAAdditionalPrice decimal(18, 0) NULL,
	HasSMO bit NOT NULL,
	SMOCount int NULL,
	SMOAdditionalPrice decimal(18, 0) NULL,
	Amount decimal(18, 0) NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Subscription SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_Subscription ADD CONSTRAINT
	DF_Subscription_HasMRA DEFAULT 0 FOR HasMRA
GO
ALTER TABLE dbo.Tmp_Subscription ADD CONSTRAINT
	DF_Subscription_HasSMO DEFAULT 0 FOR HasSMO
GO
SET IDENTITY_INSERT dbo.Tmp_Subscription ON
GO
IF EXISTS(SELECT * FROM dbo.Subscription)
	 EXEC('INSERT INTO dbo.Tmp_Subscription (Id, Name, Description, IsPromotionCode, PromotionCode, IsTrial, TrialStartDate, TrialEndDate, TrialDuration, PaymentTypeId, MaxOwnerCount, MaxContactCount, MaxPetCount, Amount)
		SELECT Id, Name, Description, IsPromotionCode, PromotionCode, IsTrial, TrialStartDate, TrialEndDate, TrialDuration, PaymentTypeId, MaxOwnerCount, MaxContactCount, MaxPetCount, Amount FROM dbo.Subscription WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Subscription OFF
GO
ALTER TABLE dbo.PartnerSubscription
	DROP CONSTRAINT FK_PartnerSubscription_Subscription
GO
ALTER TABLE dbo.UserSubscription
	DROP CONSTRAINT FK_UserSubscription_SubscriptionBase
GO
ALTER TABLE dbo.SubscriptionService
	DROP CONSTRAINT FK_SubscriptionService_SubscriptionBase
GO
DROP TABLE dbo.Subscription
GO
EXECUTE sp_rename N'dbo.Tmp_Subscription', N'Subscription', 'OBJECT' 
GO
ALTER TABLE dbo.Subscription ADD CONSTRAINT
	PK_SubscriptionType PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Subscription ADD CONSTRAINT
	FK_SubscriptionBase_PaymentType FOREIGN KEY
	(
	PaymentTypeId
	) REFERENCES dbo.PaymentType
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Subscription', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Subscription', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Subscription', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.SubscriptionService ADD CONSTRAINT
	FK_SubscriptionService_SubscriptionBase FOREIGN KEY
	(
	SubscriptionId
	) REFERENCES dbo.Subscription
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SubscriptionService SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.SubscriptionService', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.SubscriptionService', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.SubscriptionService', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.UserSubscription ADD CONSTRAINT
	FK_UserSubscription_SubscriptionBase FOREIGN KEY
	(
	SubscriptionId
	) REFERENCES dbo.Subscription
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.UserSubscription SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.UserSubscription', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.UserSubscription', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.UserSubscription', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.PartnerSubscription ADD CONSTRAINT
	FK_PartnerSubscription_Subscription FOREIGN KEY
	(
	SubscriptionId
	) REFERENCES dbo.Subscription
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.PartnerSubscription SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.PartnerSubscription', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.PartnerSubscription', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.PartnerSubscription', 'Object', 'CONTROL') as Contr_Per 