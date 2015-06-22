USE [ADOPets_Rev]
GO

ALTER TABLE [Contact]
drop Constraint FK_Contact_OwnerHistory


ALTER TABLE [Contact]
drop column OwnerHistoryId

ALTER TABLE [Contact]
ADD PetContactId int
GO

ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_Contact_PetContact] FOREIGN KEY([PetContactId])
REFERENCES [dbo].[PetContact] ([Id])
GO

ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_PetContact]
GO


