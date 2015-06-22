ALTER TABLE Subscription
ADD [IsDeleted] bit not null Default 0

Update Subscription SET IsDeleted=1 WHERE Name='Free 40 days trial for 1 pet, which includes 1 MRA' AND PromotionCode='HSBC' AND IsTrial=1

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
           (N'$3 per year for 1 pet, which includes 1 MRA',
            N'',
            1,'HSBC',
			0,
			1,
			3.0,
			1,
			1,0,
			1,1,25.0,
			0,0,95.0,
			0,0,25.0,NULL,
			0,
			20.0,
			0,
			11,
		    1,1,
		    N'$20 per additional pet, with 1 MRA');