use ADOPets_Rev


Delete From OwnerHistory

ALTER TABLE [OwnerHistory] ALTER COLUMN [FullName] nvarchar(Max) Not Null
ALTER TABLE [OwnerHistory] ALTER COLUMN [City] nvarchar(Max)
ALTER TABLE [OwnerHistory] ALTER COLUMN [Zip] nvarchar(Max)
ALTER TABLE [OwnerHistory] ALTER COLUMN [Email] nvarchar(Max)
ALTER TABLE [OwnerHistory] ALTER COLUMN [Address1] nvarchar(Max)
ALTER TABLE [OwnerHistory] ALTER COLUMN [Address2] nvarchar(Max)
ALTER TABLE [OwnerHistory] ALTER COLUMN [PhoneHome] nvarchar(Max)
ALTER TABLE [OwnerHistory] ALTER COLUMN [PhoneCell] nvarchar(Max)
ALTER TABLE [OwnerHistory] ALTER COLUMN [PhoneOffice] nvarchar(Max)
ALTER TABLE [OwnerHistory] ALTER COLUMN [Fax] nvarchar(Max)
ALTER TABLE [OwnerHistory] ALTER COLUMN [Comments] nvarchar(Max)