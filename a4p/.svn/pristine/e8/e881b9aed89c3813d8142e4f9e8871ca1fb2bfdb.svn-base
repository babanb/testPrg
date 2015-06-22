use ADOPets_Rev

SET IDENTITY_INSERT [dbo].[User] ON

INSERT INTO [dbo].[User](
	   [Id]
	  ,[UserTypeId] 
	  ,[FirstName]
      ,[LastName]
      ,[BirthDate]               
      ,[GeneralConditions]      
      ,[CreationDate]
      ,[UserStatusId])
 Values(1,6,'Robert','Smith','1984/02/03',1,'2014/01/03',1)
 
 INSERT INTO [dbo].[User](
	   [Id]
	  ,[UserTypeId] 
	  ,[FirstName]
      ,[LastName]
      ,[BirthDate]               
      ,[GeneralConditions]      
      ,[CreationDate]
      ,[UserStatusId])
 Values(2,6,'David','Miller','1984/02/03',1,'2014/01/03',1)
 
 SET IDENTITY_INSERT [dbo].[User] OFF
 
 INSERT INTO [dbo].[Login]([UserName],[Password],[UserId]) values('robert','robert',1)
 INSERT INTO [dbo].[Login]([UserName],[Password],[UserId]) values('david','david',2)