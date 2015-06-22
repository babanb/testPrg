use ADOPets_Rev

update Subscription
set Duration = 365, IsTrial = 1, SubscriptionBaseId = 1
where PromotionCode = 'GRPN-111914'