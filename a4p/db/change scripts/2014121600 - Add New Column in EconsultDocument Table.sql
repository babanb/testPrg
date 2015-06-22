
alter table EconsultDocument add DocumentSubTypeId int NULL

ALTER TABLE [dbo].[EconsultDocument]  WITH CHECK ADD  CONSTRAINT [FK_ECDocuments_DocumentSubType] FOREIGN KEY([DocumentSubTypeId])
REFERENCES [dbo].[DocumentSubType] ([Id])
GO