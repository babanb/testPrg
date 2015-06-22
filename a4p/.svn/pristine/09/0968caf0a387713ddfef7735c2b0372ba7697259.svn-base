
ALTER TABLE [dbo].[UserSubscription] add [TempUserSubscriptionId] int null

ALTER TABLE [dbo].[UserSubscription]  WITH CHECK ADD  CONSTRAINT [FK_UserSubscription_TempUserSubscription] FOREIGN KEY([TempUserSubscriptionId])
REFERENCES [dbo].[TempUserSubscription] ([Id])
GO