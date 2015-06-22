

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
 

 
 
 
