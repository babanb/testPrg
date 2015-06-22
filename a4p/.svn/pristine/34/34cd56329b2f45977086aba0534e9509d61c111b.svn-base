
 Update [dbo].[Subscription] set [Name]=N'$50 per year for 1 pet, which includes 1 MRA',
		   [Description]='',
		   [IsPromotionCode]=1,           
		   [PaymentTypeId]=1,
		   [Amount]=50,		   		   
		   [MRAAdditionalPrice]=25,		   
		   [EconsultationAdditionalPrice]=25,	
		   [SMOAdditionalPrice]=95,	   
		   [AmmountPerAddionalPet]=20,
		   duration = 0,
		   AditionalInfo=N'$20 per additional pet, with 1 MRA'
		    where PromotionCode='ALLSTARCOCO'
 

update Subscription set PromotionCode = 'ACTIVGD' where PromotionCode = 'Activ-GD-01'

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
           (N'$28 per year for 1 pet, which includes 1 MRA',
            N'This plan includes a 45% discount on the total retail price.',
            1,'RETAILMENOT',
			1,
			1,
			28.0,
			1,
			1,0,
			1,1,25.0,
			0,0,95.0,
			0,0,25.0,NULL,
			0,
			20.0,
			365,
			1,
		    1,0,
		    N'$20 per additional pet, with 1 MRA');
		     
 Set IDENTITY_INSERT subscription On

-- Promo Code Free plan
INSERT INTO [dbo].[Subscription]
           (ID,
		    [Name],
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
           (2,
		    N'$0 per year for 1 pet, which includes 1 MRA',
            N'',
            1,'FREE_PROMO',
			0,
			1,
			0.0,
			1,
			1,0,
			1,1,25.0,
			0,0,95.0,
			0,0,25.0,NULL,
			1,
			0.0,
			0,
			NULL,
		    1,0,
		    N'');
		    

 Set IDENTITY_INSERT subscription Off



