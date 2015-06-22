use ADOPets_Rev

ALTER TABLE UserSubscription
ADD SubscriptionServiceID int NULL

ALTER TABLE UserSubscription
ADD FOREIGN KEY (SubscriptionServiceID) REFERENCES SubscriptionService(Id)