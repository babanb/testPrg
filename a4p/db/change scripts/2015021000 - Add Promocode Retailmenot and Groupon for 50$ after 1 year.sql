

-- Promo Code RETAILMENOT
INSERT INTO [dbo].[Subscription]
           ([Name],
		   [Description],
		   [IsPromotionCode],[PromotionCode],
           [IsTrial],
		   [PaymentTypeId],
		   [Amount],
		   [MaxPetCount],
		   [MaxOwnerCount],[MaxContactCount],
		   [HasMRA],[MRACount],[MRAAdditionalPrice],
		   [HasSMO],[SMOCount],[SMOAdditionalPrice],
		   [HasEconsultation],[EconsultationCount],[EconsultationAdditionalPrice],[EconsultationPaymentTypeId],
		   [IsBase],
		   [AmmountPerAddionalPet],
		   [Duration],
		   [SubscriptionBaseId],
		   [LanguageId], [IsVisibleToOwner],
		   [AditionalInfo])
     VALUES
           (N'$50 per year for 1 pet, which includes 1 MRA',
            N'',
            1,'RETAILMENOT',
			0,
			1,
			50.0,
			1,
			1,0,
			1,1,25.0,
			0,0,95.0,
			0,0,25.0,NULL,
			0,
			20.0,
			0,
			NULL,
		    1,0,
		    N'$20 per additional pet, with 1 MRA');
		    

			INSERT INTO [dbo].[Subscription]
           ([Name],
		   [Description],
		   [IsPromotionCode],[PromotionCode],
           [IsTrial],
		   [PaymentTypeId],
		   [Amount],
		   [MaxPetCount],
		   [MaxOwnerCount],[MaxContactCount],
		   [HasMRA],[MRACount],[MRAAdditionalPrice],
		   [HasSMO],[SMOCount],[SMOAdditionalPrice],
		   [HasEconsultation],[EconsultationCount],[EconsultationAdditionalPrice],[EconsultationPaymentTypeId],
		   [IsBase],
		   [AmmountPerAddionalPet],
		   [Duration],
		   [SubscriptionBaseId],
		   [LanguageId], [IsVisibleToOwner],
		   [AditionalInfo])
     VALUES
           (N'$50 per year for 1 pet, which includes 1 MRA',
            N'',
            1,'GRPN-111914',
			0,
			1,
			50.0,
			1,
			1,0,
			1,1,25.0,
			0,0,95.0,
			0,0,25.0,NULL,
			0,
			20.0,
			0,
			NULL,
		    1,0,
		    N'$20 per additional pet, with 1 MRA');

 select * from Subscription
 
 update Subscription set IsVisibleToOwner = 0,SubscriptionBaseId=28 where id = 18
 update Subscription set IsVisibleToOwner = 0,SubscriptionBaseId=29 where id = 13
  update Subscription set IsVisibleToOwner = 0,SubscriptionBaseId=28 where id = 28
 update Subscription set IsVisibleToOwner = 0,SubscriptionBaseId=29 where id = 29
 

