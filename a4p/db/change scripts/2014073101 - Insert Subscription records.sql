USE [ADOPets_Rev]

SET IDENTITY_INSERT [dbo].[Subscription] ON
INSERT [dbo].[Subscription] ([Id], [Name], [Description], [IsPromotionCode], [PromotionCode], [IsTrial], [TrialStartDate], [TrialEndDate], [TrialDuration], [PaymentTypeId], [MaxOwnerCount], [MaxContactCount], [MaxPetCount], [Amount]) VALUES (1, N'$40 dollars per year for 1 pet, which includes 1 MRA', N'$20 per additional pet, with 1 MRA', 0, NULL, 0, NULL, NULL, 0, NULL, 1, 4, 6, CAST(40 AS Decimal(18, 0)))
INSERT [dbo].[Subscription] ([Id], [Name], [Description], [IsPromotionCode], [PromotionCode], [IsTrial], [TrialStartDate], [TrialEndDate], [TrialDuration], [PaymentTypeId], [MaxOwnerCount], [MaxContactCount], [MaxPetCount], [Amount]) VALUES (2, N'$60 dollars per year for 2 pet, which includes 2 MRA', N'$20 per additional pet, with 1 MRA', 0, NULL, 0, NULL, NULL, 0, NULL, 1, 4, 6, CAST(60 AS Decimal(18, 0)))
INSERT [dbo].[Subscription] ([Id], [Name], [Description], [IsPromotionCode], [PromotionCode], [IsTrial], [TrialStartDate], [TrialEndDate], [TrialDuration], [PaymentTypeId], [MaxOwnerCount], [MaxContactCount], [MaxPetCount], [Amount]) VALUES (3, N'$80 dollars per year for 3 pet, which includes 3 MRA', N'$20 per additional pet, with 1 MRA', 0, NULL, 0, NULL, NULL, 0, NULL, 1, 4, 6, CAST(80 AS Decimal(18, 0)))
INSERT [dbo].[Subscription] ([Id], [Name], [Description], [IsPromotionCode], [PromotionCode], [IsTrial], [TrialStartDate], [TrialEndDate], [TrialDuration], [PaymentTypeId], [MaxOwnerCount], [MaxContactCount], [MaxPetCount], [Amount]) VALUES (4, N'$100 dollars per year for 4 pet, which includes 4 MRA', N'$20 per additional pet, with 1 MRA', 0, NULL, 0, NULL, NULL, 0, NULL, 1, 4, 6, CAST(100 AS Decimal(18, 0)))
INSERT [dbo].[Subscription] ([Id], [Name], [Description], [IsPromotionCode], [PromotionCode], [IsTrial], [TrialStartDate], [TrialEndDate], [TrialDuration], [PaymentTypeId], [MaxOwnerCount], [MaxContactCount], [MaxPetCount], [Amount]) VALUES (5, N'$120 dollars per year for 5 pet, which includes 5 MRA', N'$20 per additional pet, with 1 MRA', 0, NULL, 0, NULL, NULL, 0, NULL, 1, 4, 6, CAST(120 AS Decimal(18, 0)))
INSERT [dbo].[Subscription] ([Id], [Name], [Description], [IsPromotionCode], [PromotionCode], [IsTrial], [TrialStartDate], [TrialEndDate], [TrialDuration], [PaymentTypeId], [MaxOwnerCount], [MaxContactCount], [MaxPetCount], [Amount]) VALUES (6, N'$140 dollars per year for 6 pet, which includes 6 MRA', N'$20 per additional pet, with 1 MRA', 0, NULL, 0, NULL, NULL, 0, NULL, 1, 4, 6, CAST(140 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[Subscription] OFF
/****** Object:  ForeignKey [FK_SubscriptionBase_PaymentType]    Script Date: 07/31/2014 16:16:18 ******/
ALTER TABLE [dbo].[Subscription]  WITH CHECK ADD  CONSTRAINT [FK_SubscriptionBase_PaymentType] FOREIGN KEY([PaymentTypeId])
REFERENCES [dbo].[PaymentType] ([Id])
GO
ALTER TABLE [dbo].[Subscription] CHECK CONSTRAINT [FK_SubscriptionBase_PaymentType]
GO
