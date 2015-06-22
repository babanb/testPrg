use ADOPets_Rev

ALTER TABLE [User]
DROP CONSTRAINT FK_User_BillingInformation
GO

Alter table [User]
drop column BillingInformationId

ALTER TABLE [User]
DROP CONSTRAINT FK_UserProfile_ReferralSourceType
GO

Alter table [User]
drop column ReferralSourceTypeId
Go

Drop table ReferralSourceType



Alter table [User]
add ReferralSource nvarchar(200)

Alter table [User]
add Address1 nvarchar(200)

Alter table [User]
add Address2 nvarchar(200)


