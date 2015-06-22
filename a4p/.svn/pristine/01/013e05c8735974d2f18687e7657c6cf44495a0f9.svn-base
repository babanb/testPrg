use [ADOPets_Rev]

ALTER TABLE [Contact]
ADD OwnerHistoryId int
GO

ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_Contact_OwnerHistory] FOREIGN KEY([OwnerHistoryId])
REFERENCES [dbo].[OwnerHistory] ([Id])
GO

ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_OwnerHistory]
GO