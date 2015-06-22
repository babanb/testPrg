USE [ADOPets_Rev]
GO

/****** Object:  Table [dbo].[UserSubscriptionHistory]    Script Date: 11/12/2014 19:16:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserSubscriptionHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SubscriptionId] [int] NOT NULL,
	[UserId] [int] NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[RenewvalDate] [datetime] NOT NULL,
	[SubscriptionMailSent] [bit] NULL,
	[SubscriptionExpirationAlertId] [int] NULL,
	[AditionalPetCount] [int] NULL,
	[AditionalSMOCount] [int] NULL,
	[AditionalMRACount] [int] NULL,
	[AditionalECCount] [int] NULL,
	[PurchaseDate] [datetime] NOT NULL,
	[UsedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_UserSubscriptionHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UserSubscriptionHistory]  WITH CHECK ADD  CONSTRAINT [FK_UserSubscriptionHistory_Subscription] FOREIGN KEY([SubscriptionId])
REFERENCES [dbo].[Subscription] ([Id])
GO

ALTER TABLE [dbo].[UserSubscriptionHistory] CHECK CONSTRAINT [FK_UserSubscriptionHistory_Subscription]
GO

ALTER TABLE [dbo].[UserSubscriptionHistory]  WITH CHECK ADD  CONSTRAINT [FK_UserSubscriptionHistory_SubscriptionExpirationAlert] FOREIGN KEY([SubscriptionExpirationAlertId])
REFERENCES [dbo].[SubscriptionExpirationAlert] ([Id])
GO

ALTER TABLE [dbo].[UserSubscriptionHistory] CHECK CONSTRAINT [FK_UserSubscriptionHistory_SubscriptionExpirationAlert]
GO

ALTER TABLE [dbo].[UserSubscriptionHistory]  WITH CHECK ADD  CONSTRAINT [FK_UserSubscriptionHistory_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[UserSubscriptionHistory] CHECK CONSTRAINT [FK_UserSubscriptionHistory_User]
GO


