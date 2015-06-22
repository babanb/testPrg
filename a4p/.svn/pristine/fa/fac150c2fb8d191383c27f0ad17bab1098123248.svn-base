use ADOPets_Redesign

update dbo.Subscription
set PaymentTypeId = 1
where IsTrial != 1

update dbo.Subscription
set PaymentTypeId = NULL
where IsTrial = 1