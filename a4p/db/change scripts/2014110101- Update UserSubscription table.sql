use ADOPets_Rev

ALTER TABLE UserSubscription
ADD CONSTRAINT default_StartDate DEFAULT GETDATE() FOR StartDate

ALTER TABLE UserSubscription
ADD RenewalDate datetime