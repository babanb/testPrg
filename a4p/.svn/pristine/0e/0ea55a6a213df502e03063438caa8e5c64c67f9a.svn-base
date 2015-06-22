use ADOPets_Rev

ALTER TABLE [PetDocument] ALTER COLUMN [Comment] nvarchar(Max)

ALTER TABLE [Insurance] ALTER COLUMN [AccountNumber] nvarchar(Max)
ALTER TABLE [Insurance] ALTER COLUMN [PlanName] nvarchar(Max)
ALTER TABLE [Insurance] ALTER COLUMN [GroupNumber] nvarchar(Max)

ALTER TABLE [Veterinarian] ALTER COLUMN [FirstName] nvarchar(Max)
ALTER TABLE [Veterinarian] ALTER COLUMN [LastName] nvarchar(Max)
ALTER TABLE [Veterinarian] ALTER COLUMN [MiddleName] nvarchar(Max)
ALTER TABLE [Veterinarian] ALTER COLUMN [Email] nvarchar(Max)
ALTER TABLE [Veterinarian] ALTER COLUMN [NPI] nvarchar(Max)
ALTER TABLE [Veterinarian] ALTER COLUMN [HospitalName] nvarchar(Max)
ALTER TABLE [Veterinarian] ALTER COLUMN [Address1] nvarchar(Max)
ALTER TABLE [Veterinarian] ALTER COLUMN [Address2] nvarchar(Max)
ALTER TABLE [Veterinarian] ALTER COLUMN [City] nvarchar(Max)
ALTER TABLE [Veterinarian] ALTER COLUMN [Zip] nvarchar(Max)
ALTER TABLE [Veterinarian] ALTER COLUMN [PhoneHome] nvarchar(Max)
ALTER TABLE [Veterinarian] ALTER COLUMN [PhoneCell] nvarchar(Max)
ALTER TABLE [Veterinarian] ALTER COLUMN [PhoneOffice] nvarchar(Max)
ALTER TABLE [Veterinarian] ALTER COLUMN [Fax] nvarchar(Max)

EXEC sp_RENAME 'Veterinarian.Comments' , 'Comment', 'COLUMN'
ALTER TABLE [Veterinarian] ALTER COLUMN [Comment] nvarchar(Max)


