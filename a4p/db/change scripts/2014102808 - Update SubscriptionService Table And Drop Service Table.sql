use ADOPets_Rev



ALTER TABLE SubscriptionService
DROP CONSTRAINT FK_SubscriptionService_Service

alter table SubscriptionService
drop column ServiceId, PromotionCode

alter table SubscriptionService
add  AditionalPetCount int null, AditionalSMOCount int null, AditionalMRACount int null
GO


drop  table [Service]
 
