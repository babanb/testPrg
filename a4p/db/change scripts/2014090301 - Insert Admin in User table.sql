use ADOPets_Rev
update [User]
set UserTypeId = 6

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
 Values(50,1,'Admin','Admin','07/19/1995 00:00:00',1,'09/03/2014 00:00:00',1)
 
 Go

SET IDENTITY_INSERT [dbo].[User] OFF

INSERT INTO [dbo].[Login] values('voHTIMkI2HOZpYM5iTtOkg==','M0Dqp8Wu33ZCSn5i1bVHJqYrEamvqgUM5h8bMYbtn9G5jPn+IZMysTnXnrXdwWFeAW2JGLbOweOb5Td6ggiYIg==',50, '50B&(', 0)