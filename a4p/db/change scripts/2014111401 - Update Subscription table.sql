use ADOPets_Rev

ALTER TABLE Subscription
ADD LanguageId int not null default(1)

ALTER TABLE Subscription
ADD FOREIGN KEY (LanguageId) REFERENCES [Language](Id)

ALTER TABLE Subscription
ADD AditionalInfo nvarchar(max)

ALTER TABLE Subscription
ADD IsVisibleToOwner bit not null default(1)

