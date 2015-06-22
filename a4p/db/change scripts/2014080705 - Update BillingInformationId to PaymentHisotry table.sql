Use ADOPets_Rev
alter table PaymentHistory
add BillingInformationId int

alter table PaymentHistory
drop column PaymentTypeId