CREATE TABLE [dbo].[PlanType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PlanType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


INSERT INTO [dbo].[PlanType]([Name])
     VALUES ('Basic Free'),
	 ('Premium'),
	 ('Premium Plus')
GO



Alter Table Subscription 
Add [PlanTypeId] int null


ALTER TABLE [dbo].[Subscription]  WITH CHECK ADD  CONSTRAINT [FK_Subscription_PlanType] FOREIGN KEY([PlanTypeId])
REFERENCES [dbo].[PlanType] ([Id])
GO
ALTER TABLE [dbo].[Subscription] CHECK CONSTRAINT [FK_Subscription_PlanType]
GO

update [dbo].[Subscription] set
          [Name]='$50 per year for 4 pets with 4 MRA',
		  [Amount]=50.0, [MaxPetCount]=4,[MRACount]=4,
		  [IsBase]='true', [AmmountPerAddionalPet]=0,
		  [IsVisibleToOwner]='true', [AditionalInfo]='',
		  [PlanTypeId]=2 , [Description]='Includes the Basic Plan, plus advanced features: 
		  Pet Medical Records (Health History, Health Measure Tracker, Medical Documents), 
		  Medical Records Assistance (we collect all your pets'' health information on your behalf),
		   plus edit and upload existing records whenever you need to. Coverage for up to 4 pets.'
     Where Id= 1
	 GO


CREATE TABLE [dbo].[PlanFeature](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SubscriptionId] int null,
	[IsPetIDInformation] bit Not NULL Default 1,
	[IsEmergencyContact] bit Not NULL Default 1,
	[IsVetInformation] bit Not NULL Default 1,
	[IsMessage] bit Not NULL Default 1,
	[IsCalendar] bit Not NULL Default 1,
	[IsReminder] bit Not NULL Default 1,
	[IsPhotoGallery] bit Not NULL Default 1,
	[IsPetShare] bit Not NULL Default 1,
	[IsHealthHistory] bit Not NULL Default 1,
	[IsHealthMeasureTracker] bit Not NULL Default 1,
	[IsMedicalDocument] bit Not NULL Default 1,
	[IsMRA] bit Not NULL Default 1,
	[IsSMO] bit Not NULL Default 1,
	[IsEC] bit Not NULL Default 1
 CONSTRAINT [PK_PlanFeature] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


SET IDENTITY_INSERT [dbo].[PlanFeature] ON 

GO
INSERT [dbo].[PlanFeature] ([Id], [SubscriptionId], [IsPetIDInformation], [IsEmergencyContact], [IsVetInformation], [IsMessage], [IsCalendar], [IsReminder], [IsPhotoGallery], [IsPetShare], [IsHealthHistory], [IsHealthMeasureTracker], [IsMedicalDocument], [IsMRA], [IsSMO], [IsEC])
 VALUES (1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1)
GO
--check the plan id
--INSERT [dbo].[PlanFeature] ([Id], [SubscriptionId], [IsPetIDInformation], [IsEmergencyContact], [IsVetInformation], [IsMessage], [IsCalendar], [IsReminder], [IsPhotoGallery], [IsPetShare], [IsHealthHistory], [IsHealthMeasureTracker], [IsMedicalDocument], [IsMRA], [IsSMO], [IsEC])
-- VALUES (2, 1039, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0)
--GO
--INSERT [dbo].[PlanFeature] ([Id], [SubscriptionId], [IsPetIDInformation], [IsEmergencyContact], [IsVetInformation], [IsMessage], [IsCalendar], [IsReminder], [IsPhotoGallery], [IsPetShare], [IsHealthHistory], [IsHealthMeasureTracker], [IsMedicalDocument], [IsMRA], [IsSMO], [IsEC]) 
--VALUES (3, 1040, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1)
--GO
SET IDENTITY_INSERT [dbo].[PlanFeature] OFF
GO



ALTER TABLE [dbo].[PlanFeature]  WITH CHECK ADD  CONSTRAINT [FK_PlanFeature_Subscription] FOREIGN KEY([SubscriptionId])
REFERENCES [dbo].[Subscription] ([Id])
GO
ALTER TABLE [dbo].[PlanFeature] CHECK CONSTRAINT [FK_PlanFeature_Subscription]
GO
	 
	 --for demo and live
	
	 --INSERT INTO [dbo].[Subscription]
  --         ([Name], [Description],
		--   [IsPromotionCode],[PromotionCode],
  --         [IsTrial], [PaymentTypeId],
		--   [Amount], [MaxPetCount],
		--   [MaxOwnerCount],[MaxContactCount],
		--   [HasMRA],[MRACount],
		--   [MRAAdditionalPrice],[HasSMO],
		--   [SMOCount],[SMOAdditionalPrice],
		--   [HasEconsultation],[EconsultationCount],
		--   [EconsultationAdditionalPrice],[EconsultationPaymentTypeId],
		--   [IsBase], [AmmountPerAddionalPet],
		--   [Duration], [SubscriptionBaseId],
		--   [LanguageId], [IsVisibleToOwner],
		--   [AditionalInfo],[PlanTypeId])
  --   VALUES
  --         (N'Free account with limited access', N'Get started with a free account. No credit card required. 
		--   No time limitations. Unlimited access to: Pet''s ID information, Emergency Contacts, Veterinarian/s Contact Info, Messages,
		--    Calendars, Reminders, Photo Gallery, Sharing Feature (giving you the ability to share your pet''s information with Vets, 
		--	Boarders, Friends and Family Members).',
  --          0,null,
		--	0,null,
		--	null,5,
		--	1,0,
		--	1,1,
		--	25.0,0,
		--	0,95.0,
		--	0,0,
		--	25.0,NULL,
		--	1,null,
		--	20,null,
		--    1,1,
		--    N'',1);
						
--INSERT INTO [dbo].[Subscription]
--           ([Name], [Description],
--		   [IsPromotionCode],[PromotionCode],
--           [IsTrial], [PaymentTypeId],
--		   [Amount], [MaxPetCount],
--		   [MaxOwnerCount],[MaxContactCount],
--		   [HasMRA],[MRACount],
--		   [MRAAdditionalPrice],[HasSMO],
--		   [SMOCount],[SMOAdditionalPrice],
--		   [HasEconsultation],[EconsultationCount],
--		   [EconsultationAdditionalPrice],[EconsultationPaymentTypeId],
--		   [IsBase], [AmmountPerAddionalPet],
--		   [Duration], [SubscriptionBaseId],
--		   [LanguageId], [IsVisibleToOwner],
--		   [AditionalInfo],[PlanTypeId])
--     VALUES
--           (N'$70 per year for 8 pets with 8 MRA', N'Includes the Basic Plan, plus advanced features: 
--		   Pet Medical Records (Health History, Health Measure Tracker, Medical Documents), 
--		   Medical Records Assistance (we collect all your pets'' health information on your behalf),
--		   plus edit and upload existing records whenever you need to. Coverage for up to 8 pets.',
--            0,null,
--			0,1,
--			70.0,10,
--			1,0,
--			1,10,
--			25.0,0,
--			0,95.0,
--			0,0,
--			25.0,NULL,
--			1,0,
--			0,1,
--		    1,1,
--		    N'',3);



