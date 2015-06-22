


ALTER TABLE EConsultation
ADD [CountryId] INT NULL

ALTER TABLE [dbo].[EConsultation]  WITH CHECK ADD  CONSTRAINT [FK_EConsultation_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([Id])
GO

