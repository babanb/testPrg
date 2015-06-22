USE [ADOPets_Rev]


  
  ALTER TABLE [ADOPets_Rev].[dbo].[Subscription]
  ADD IsBase bit  not null default 0
  GO
  
  
SET IDENTITY_INSERT [dbo].[Subscription] ON
 
INSERT [dbo].[Subscription] ([Id], [Name], [Description], [IsPromotionCode], [PromotionCode], [IsTrial], [TrialStartDate], [TrialEndDate], [TrialDuration], [PaymentTypeId], [MaxOwnerCount], [MaxContactCount], [MaxPetCount], [Amount],[IsBase])
 VALUES (7, N'$30 dollars per year for 1 pet, which includes 1 MRA', N'$20 per additional pet, with 1 MRA', 1, 'ABC01', 0, NULL, NULL, 0, NULL, 1, 4, 6, CAST(30 AS Decimal(18, 0)),1)
INSERT [dbo].[Subscription] ([Id], [Name], [Description], [IsPromotionCode], [PromotionCode], [IsTrial], [TrialStartDate], [TrialEndDate], [TrialDuration], [PaymentTypeId], [MaxOwnerCount], [MaxContactCount], [MaxPetCount], [Amount],[IsBase])
 VALUES (8, N'$50 dollars per year for 2 pet, which includes 2 MRA', N'$20 per additional pet, with 1 MRA', 1, 'ABC01', 0, NULL, NULL, 0, NULL, 1, 4, 6, CAST(50 AS Decimal(18, 0)),0)
INSERT [dbo].[Subscription] ([Id], [Name], [Description], [IsPromotionCode], [PromotionCode], [IsTrial], [TrialStartDate], [TrialEndDate], [TrialDuration], [PaymentTypeId], [MaxOwnerCount], [MaxContactCount], [MaxPetCount], [Amount],[IsBase])
 VALUES (9, N'$70 dollars per year for 3 pet, which includes 3 MRA', N'$20 per additional pet, with 1 MRA', 1, 'ABC01', 0, NULL, NULL, 0, NULL, 1, 4, 6, CAST(70 AS Decimal(18, 0)),0)
 SET IDENTITY_INSERT [dbo].[Subscription] OFF
 
 Update Subscription Set IsBase = 1 where Id = 1
 
