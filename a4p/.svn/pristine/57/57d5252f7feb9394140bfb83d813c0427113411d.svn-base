use ADOPets_Rev

alter table [User]
add SubscriptionExpirationAlertId int not null default (1)
GO

ALTER TABLE dbo.[User] 
ADD CONSTRAINT FK_User_SubscriptionExpirationAlert 
FOREIGN KEY (SubscriptionExpirationAlertId) 
REFERENCES dbo.SubscriptionExpirationAlert (Id) 