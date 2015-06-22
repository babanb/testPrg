ALTER TABLE Subscription
ADD [CenterId] int Default Null

ALTER TABLE [dbo].[Subscription]  WITH CHECK ADD  CONSTRAINT [FK_Subscription_Id] FOREIGN KEY([CenterId])
REFERENCES [dbo].[Centers] ([Id])
GO


ALTER TABLE Centers
ADD [IsDeleted] bit not null Default 0