begin tran

-- Update reference Plans 
update Subscription set Name = '$50 per year for 1 pet, which includes 1 MRA', Description = '', Amount= 50, LanguageId = 1, AditionalInfo = N'$20 per additional pet, with 1 MRA', IsVisibleToOwner = 1 where PromotionCode is NULL


-- Update Test plans & Promo Code (ABC01)
update Subscription set LanguageId = 1, IsVisibleToOwner = 0 where id = 7
update Subscription set LanguageId = 1, IsVisibleToOwner = 0 where id = 8
update Subscription set LanguageId = 1, IsVisibleToOwner = 0 where id = 9

-- Update HSBC Plan & Promo Code
update Subscription set LanguageId = 1, IsVisibleToOwner = 0, AditionalInfo = N'$20 per additional pet, with 1 MRA' where PromotionCode = 'HSBC' 

	
-- Promo Code Groupon
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
            N'This plan includes a 45% discount on the total retail price. Groupon Deal.',
            1,'GRPN-111914',
			0,
			1,
			28.0,
			1,
			1,0,
			0,0,25.0,
			0,0,95.0,
			0,0,25.0,NULL,
			0,
			20.0,
			0,
			NULL,
		    1,0,
		    N'$20 per additional pet, with 1 MRA');
		    
commit tran