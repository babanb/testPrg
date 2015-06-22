use ADOPets_Rev

alter table [UserSubscription]
add SubscriptionMailSent bit not null default (0)

alter table [UserSubscription]
add SubscriptionExpirationAlertId int not null default (1)

ALTER TABLE dbo.[UserSubscription] 
ADD CONSTRAINT FK_UserSubscription_SubscriptionExpirationAlert 
FOREIGN KEY (SubscriptionExpirationAlertId) 
REFERENCES dbo.SubscriptionExpirationAlert (Id)