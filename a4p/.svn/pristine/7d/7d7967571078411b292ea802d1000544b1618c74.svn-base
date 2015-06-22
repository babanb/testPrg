
-- Alter Econsultation Table
Alter table Econsultation add CalenderId int null

ALTER TABLE [dbo].[Econsultation]  WITH CHECK ADD  CONSTRAINT [FK_ECalender_CalenderId] FOREIGN KEY([CalenderId])
REFERENCES [dbo].[Calendar] ([Id])
GO