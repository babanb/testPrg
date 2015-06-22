use ADOPets_Rev

ALTER TABLE [PetHospitalization] ALTER COLUMN [HospitalName] nvarchar(Max)
ALTER TABLE [PetVaccination] ALTER COLUMN [HospitalName] nvarchar(Max)
ALTER TABLE [PetVaccination] ALTER COLUMN [Comment] nvarchar(Max)

ALTER TABLE [PetMedication] ALTER COLUMN [Physician] nvarchar(Max)
ALTER TABLE [PetSurgery] ALTER COLUMN [Physician] nvarchar(Max)
