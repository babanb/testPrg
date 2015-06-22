use ADOPets_Rev

alter table Subscription
drop column TrialStartDate, TrialEndDate, TrialDuration

alter table Subscription
add SubscriptionBaseId int 
GO

ALTER TABLE [dbo].[Subscription]  WITH CHECK ADD  CONSTRAINT [FK_Subscription_SubscriptionBaseId] FOREIGN KEY([SubscriptionBaseId])
REFERENCES [dbo].[Subscription] ([Id])
GO


