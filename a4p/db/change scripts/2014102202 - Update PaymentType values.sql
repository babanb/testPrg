use ADOPets_Rev

Delete From PaymentType
where Id in (3,4)

SET IDENTITY_INSERT [dbo].[PaymentType] ON
Insert into PaymentType ([Id], [Name])values(3, 'ThirdPartyCompany')
SET IDENTITY_INSERT [dbo].[PaymentType] OFF

