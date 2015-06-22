INSERT INTO [dbo].[Subscription]
           ([Name], [Description],
		   [IsPromotionCode],[PromotionCode],
           [IsTrial], [PaymentTypeId],
		   [Amount], [MaxPetCount],
		   [MaxOwnerCount],[MaxContactCount],
		   [HasMRA],[MRACount],
		   [MRAAdditionalPrice],[HasSMO],
		   [SMOCount],[SMOAdditionalPrice],
		   [HasEconsultation],[EconsultationCount],
		   [EconsultationAdditionalPrice],[EconsultationPaymentTypeId],
		   [IsBase], [AmmountPerAddionalPet],
		   [Duration], [SubscriptionBaseId],
		   [LanguageId], [IsVisibleToOwner],
		   [AditionalInfo])
     VALUES
           (N'Free account with limited access', N'Includes Pet’s profile, Gallery, Veterinarians, Calendar, Sharing, Messaging',
            0,null,
			0,null,
			null,5,
			1,0,
			1,1,
			25.0,0,
			0,95.0,
			0,0,
			25.0,NULL,
			1,null,
			20,null,
		    1,1,
		    N'');


update [dbo].[Subscription] set
          [Name]='$50 per year for 5 pets with 5 MRA',
		  [Amount]=50.0, [MaxPetCount]=5,[MRACount]=5,
		  [IsBase]='true', [AmmountPerAddionalPet]=20.0,
		  [IsVisibleToOwner]='true', [AditionalInfo]=N'$20 per additional 5 pets, with 5 MRA'
     Where Id= 1


			
INSERT INTO [dbo].[Subscription]
           ([Name], [Description],
		   [IsPromotionCode],[PromotionCode],
           [IsTrial], [PaymentTypeId],
		   [Amount], [MaxPetCount],
		   [MaxOwnerCount],[MaxContactCount],
		   [HasMRA],[MRACount],
		   [MRAAdditionalPrice],[HasSMO],
		   [SMOCount],[SMOAdditionalPrice],
		   [HasEconsultation],[EconsultationCount],
		   [EconsultationAdditionalPrice],[EconsultationPaymentTypeId],
		   [IsBase], [AmmountPerAddionalPet],
		   [Duration], [SubscriptionBaseId],
		   [LanguageId], [IsVisibleToOwner],
		   [AditionalInfo])
     VALUES
           (N'$70 per year for 10 pets with 10 MRA', N'',
            0,null,
			0,1,
			70.0,10,
			1,0,
			1,10,
			25.0,0,
			0,95.0,
			0,0,
			25.0,NULL,
			1,0,
			0,1,
		    1,1,
		    N'');