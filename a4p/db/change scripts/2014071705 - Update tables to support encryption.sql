use ADOPets_Rev


Delete From Farmer

ALTER TABLE [Farmer] ALTER COLUMN [Name] nvarchar(Max) Not Null
ALTER TABLE [Farmer] ALTER COLUMN [City] nvarchar(Max) Not Null
ALTER TABLE [Farmer] ALTER COLUMN [Zip] nvarchar(Max)
ALTER TABLE [Farmer] ALTER COLUMN [Email] nvarchar(Max)
ALTER TABLE [Farmer] ALTER COLUMN [Address1] nvarchar(Max) Not Null
ALTER TABLE [Farmer] ALTER COLUMN [Address2] nvarchar(Max)
ALTER TABLE [Farmer] ALTER COLUMN [PhoneHome] nvarchar(Max)
ALTER TABLE [Farmer] ALTER COLUMN [PhoneCell] nvarchar(Max)
ALTER TABLE [Farmer] ALTER COLUMN [PhoneOffice] nvarchar(Max)
ALTER TABLE [Farmer] ALTER COLUMN [Fax] nvarchar(Max)