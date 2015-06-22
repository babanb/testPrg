

-- Promo Code ROCKIN
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
           (N'Activ4Pets Membership for 1 pet',
            N'',
            1,'ROCKIN',
			0,
			1,
			40.0,
			1,
			1,0,
			1,1,25.0,
			0,0,95.0,
			0,0,25.0,NULL,
			0,
			20.0,
			0,
			NULL,
		    1,1,
		    N'$20 per additional pet, with 1 MRA');
		    
-- Promo Code GOODDOG
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
           (N'Activ4Pets Membership for 1 pet',
            N'',
            1,'GOODDOG',
			0,
			1,
			40.0,
			1,
			1,0,
			1,1,25.0,
			0,0,95.0,
			0,0,25.0,NULL,
			0,
			20.0,
			0,
			NULL,
		    1,1,
		    N'$20 per additional pet, with 1 MRA');
			
-- Promo Code SCOTTIEPAWS
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
           (N'Activ4Pets Membership for 1 pet',
            N'',
            1,'SCOTTIEPAWS',
			0,
			1,
			40.0,
			1,
			1,0,
			1,1,25.0,
			0,0,95.0,
			0,0,25.0,NULL,
			0,
			20.0,
			0,
			NULL,
		    1,1,
		    N'$20 per additional pet, with 1 MRA');			

-- Promo Code PEDIGREE
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
           (N'Activ4Pets Membership for 1 pet',
            N'',
            1,'PEDIGREE',
			0,
			1,
			40.0,
			1,
			1,0,
			1,1,25.0,
			0,0,95.0,
			0,0,25.0,NULL,
			0,
			20.0,
			0,
			NULL,
		    1,1,
		    N'$20 per additional pet, with 1 MRA');	


update subscription set Name =N'$40 per year for 1 pet, which includes 1 MRA' where Id > 18
			
 select * from Subscription where Id = 11 or id > 18
 

 

