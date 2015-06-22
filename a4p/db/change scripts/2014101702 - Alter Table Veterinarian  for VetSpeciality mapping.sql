use ADOPets_Rev

alter table [Veterinarian]
add [VetSpecialtyID] int null

ALTER TABLE [dbo].[Veterinarian]  WITH CHECK ADD  CONSTRAINT [FK_Veterinarian_VetSpeciality] FOREIGN KEY([VetSpecialtyID])
REFERENCES [dbo].[VetSpeciality] ([Id])
GO