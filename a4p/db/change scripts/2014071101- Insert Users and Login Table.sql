
use [ADOPets_Rev]

INSERT INTO [dbo].[User]([UserTypeId] 
	  ,[FirstName]
      ,[LastName]
      ,[BirthDate]               
      ,[GeneralConditions]      
      ,[CreationDate]
      ,[UserStatusId])
 Values(1,'Admin','admin','1984/02/03',1,'2014/01/03',1)
 
 INSERT INTO [dbo].[Login]([UserName],[Password],[UserId]) values('admin','admin',1)
 