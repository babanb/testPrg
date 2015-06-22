--USE [ADOPets_Rev]
GO

/****** Object:  Table [dbo].[UserSubscription]    Script Date: 12/03/2014 11:09:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TempUserSubscription](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SubscriptionId] [int] NOT NULL,
	[SubscriptionStatusId] [int] NULL,
	[StartDate] [datetime] NOT NULL,
	[RenewalDate] [datetime] NULL,
	[SubscriptionMailSent] [bit] NOT NULL,
	[SubscriptionExpirationAlertId] [int] NOT NULL,
	[AditionalPetCount][int] NULL,
    [AditionalSMOCount][int] NULL,
    [AditionalMRACount][int] NULL,
    [AditionalECCount][int] NULL
 CONSTRAINT [PK_TempSubcription] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

 

ALTER TABLE [dbo].[TempUserSubscription]  WITH CHECK ADD  CONSTRAINT [FK_TempUserSubscription_SubscriptionBase] FOREIGN KEY([SubscriptionId])
REFERENCES [dbo].[Subscription] ([Id])
GO

ALTER TABLE [dbo].[TempUserSubscription] CHECK CONSTRAINT [FK_TempUserSubscription_SubscriptionBase]
GO

ALTER TABLE [dbo].[TempUserSubscription]  WITH CHECK ADD  CONSTRAINT [FK_TempUserSubscription_SubscriptionExpirationAlert] FOREIGN KEY([SubscriptionExpirationAlertId])
REFERENCES [dbo].[SubscriptionExpirationAlert] ([Id])
GO

ALTER TABLE [dbo].[TempUserSubscription] CHECK CONSTRAINT [FK_TempUserSubscription_SubscriptionExpirationAlert]
GO

ALTER TABLE [dbo].[TempUserSubscription]  WITH CHECK ADD  CONSTRAINT [FK_TempUserSubscription_SubscriptionStatus] FOREIGN KEY([SubscriptionStatusId])
REFERENCES [dbo].[SubscriptionStatus] ([Id])
GO

ALTER TABLE [dbo].[TempUserSubscription] CHECK CONSTRAINT [FK_TempUserSubscription_SubscriptionStatus]
GO

ALTER TABLE [dbo].[TempUserSubscription] ADD  CONSTRAINT [Tempdefault_StartDate]  DEFAULT (getdate()) FOR [StartDate]
GO

ALTER TABLE [dbo].[TempUserSubscription] ADD  DEFAULT ((0)) FOR [SubscriptionMailSent]
GO

ALTER TABLE [dbo].[TempUserSubscription] ADD  DEFAULT ((1)) FOR [SubscriptionExpirationAlertId]
GO


Alter table TempUserSubscription
drop constraint [FK_TempUserSubscription_SubscriptionExpirationAlert]