use ADOPets_Rev

 DECLARE @subId nvarchar(10);

INSERT INTO [dbo].[Subscription]
           ([Name],[Description],[IsPromotionCode],[PromotionCode]
           ,[IsTrial],[PaymentTypeId],[MaxOwnerCount]
           ,[MaxContactCount],[MaxPetCount],[HasMRA]
           ,[MRACount],[MRAAdditionalPrice],[HasSMO]
           ,[SMOCount],[SMOAdditionalPrice],[HasEconsultation]
           ,[EconsultationCount],[EconsultationAdditionalPrice]
		   ,[EconsultationPaymentTypeId],[Amount]
           ,[IsBase],[AmmountPerAddionalPet],[Duration],[SubscriptionBaseId])
     VALUES
           (N'HSBC Plan : free for 40 days – then $30/ year + $20/year per additional pet',
           N'HSBC Plan : free for 40 days – then $30/ year + $20/year per additional pet',
           0,'HSBC',0,1,1,0,1,0,0,25.0,0,0,95.0,0,0,25.0,NULL,30.0,1,20.0,0,NULL);
           
           
SELECT @subId=@@IDENTITY; 


INSERT INTO [dbo].[Subscription]
           ([Name],[Description],[IsPromotionCode],[PromotionCode],[IsTrial]
           ,[PaymentTypeId],[MaxOwnerCount],[MaxContactCount],[MaxPetCount]
           ,[HasMRA],[MRACount],[MRAAdditionalPrice],[HasSMO],[SMOCount]
           ,[SMOAdditionalPrice],[HasEconsultation],[EconsultationCount],[EconsultationAdditionalPrice]
		   ,[EconsultationPaymentTypeId],[Amount],[IsBase]
           ,[AmmountPerAddionalPet],[Duration],[SubscriptionBaseId])
     VALUES
           (N'HSBC Code : free for 40 days – then $30/ year + $20/year per additional pet',
           N'HSBC Code : free for 40 days – then $30/ year + $20/year per additional pet',
           1,'HSBC',1,1,1,0,1,0,0,25.0,0,0,95.0,0,0,25.0,NULL,30.0,1,20.0,40,@subId)