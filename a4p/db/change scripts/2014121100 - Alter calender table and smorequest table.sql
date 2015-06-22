
Alter table Calendar
Add IsRead bit NOT NULL Default 'false'

Alter table SMORequest
Add IsRead bit NOT NULL Default 'false',
IsOwnerRead bit NOT NULL Default 'false'

Alter table SMOExpertRelation
Add IsRead bit NOT NULL Default 'false'