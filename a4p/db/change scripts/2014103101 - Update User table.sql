use ADOPets_Rev

alter table [User]
add SubscriptionMailSent bit not null default(0)