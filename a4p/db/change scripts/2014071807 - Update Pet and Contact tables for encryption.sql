use ADOPets_Rev



Delete FROM [ADOPets_Rev].[dbo].[PetAllergy]
Delete FROM [ADOPets_Rev].[dbo].[PetCondition]
Delete FROM [ADOPets_Rev].[dbo].[PetConsultation]
Delete FROM [ADOPets_Rev].[dbo].[PetDocument]
Delete FROM [ADOPets_Rev].[dbo].[PetFoodPlan]
Delete FROM [ADOPets_Rev].[dbo].[PetHealthMeasure]
Delete FROM [ADOPets_Rev].[dbo].[PetHospitalization]
Delete FROM [ADOPets_Rev].[dbo].[PetMedication]
Delete FROM [ADOPets_Rev].[dbo].[PetSurgery]
Delete FROM [ADOPets_Rev].[dbo].[PetVaccination]
Delete FROM [ADOPets_Rev].[dbo].[Contact]
Delete FROM [ADOPets_Rev].[dbo].[Insurance]
Delete FROM [ADOPets_Rev].[dbo].[OwnerHistory]
Delete FROM [ADOPets_Rev].[dbo].[Veterinarian]

Delete FROM [ADOPets_Rev].[dbo].[PetUser]

Delete FROM [ADOPets_Rev].[dbo].[Pet]

ALTER TABLE Pet DROP COLUMN Comment

ALTER TABLE [Pet] ALTER COLUMN [Name] nvarchar(Max) Not Null
ALTER TABLE [Pet] ALTER COLUMN [ChipNumber] nvarchar(Max)
ALTER TABLE [Pet] ALTER COLUMN [TattooNumber] nvarchar(Max)
ALTER TABLE [Pet] ALTER COLUMN [PlaceOfBirth] nvarchar(Max)
ALTER TABLE [Pet] ALTER COLUMN [Zip] nvarchar(Max)

ALTER TABLE [Contact] ALTER COLUMN [PhoneCell] nvarchar(Max)
ALTER TABLE [Contact] ALTER COLUMN [PhoneHome] nvarchar(Max)
ALTER TABLE [Contact] ALTER COLUMN [PhoneOffice] nvarchar(Max)
ALTER TABLE [Contact] ALTER COLUMN [Fax] nvarchar(Max)


