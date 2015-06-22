use AdoPets_Rev

alter table Contact
add PetId int 
GO

ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_Contact_PetId] FOREIGN KEY([PetId])
REFERENCES [dbo].[Pet] ([Id])
GO