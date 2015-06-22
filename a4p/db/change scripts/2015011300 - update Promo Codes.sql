
/* Update several Promo Code values */


/*  Activ-GD-01 : only change name     */
update Subscription set PromotionCode = 'ACTIVGD' where PromotionCode = 'Activ-GD-01'

/*  ALLSTAR... same as PETPLACE     */
 Update [dbo].[Subscription] set [Name]=N'$50 per year for 1 pet, which includes 1 MRA',
		   [Description]='',
		   [IsPromotionCode]=1,           
		   [PaymentTypeId]=1,
		   [Amount]=50,		   		   
		   [MRAAdditionalPrice]=25,		   
		   [EconsultationAdditionalPrice]=25,	
		   [SMOAdditionalPrice]=95,	   
		   [AmmountPerAddionalPet]=20,
		   duration = 0
		    where  PromotionCode = 'ALLSTARCOCO'