  ALTER TABLE [ADOPets_Rev].[dbo].[Subscription]
  ADD AmmountPerAddionalPet decimal(18,0)
  GO


Update [ADOPets_Rev].[dbo].[Subscription] set [HasMRA]=1,[MRACount]=1,[AmmountPerAddionalPet]=20 where [Id]=1
Update [ADOPets_Rev].[dbo].[Subscription] set [HasMRA]=1,[MRACount]=2,[AmmountPerAddionalPet]=20 where [Id]=2
Update [ADOPets_Rev].[dbo].[Subscription] set [HasMRA]=1,[MRACount]=3,[AmmountPerAddionalPet]=20 where [Id]=3
Update [ADOPets_Rev].[dbo].[Subscription] set [HasMRA]=1,[MRACount]=4,[AmmountPerAddionalPet]=20 where [Id]=4
Update [ADOPets_Rev].[dbo].[Subscription] set [HasMRA]=1,[MRACount]=5,[AmmountPerAddionalPet]=20 where [Id]=5
Update [ADOPets_Rev].[dbo].[Subscription] set [HasMRA]=1,[MRACount]=6,[AmmountPerAddionalPet]=20 where [Id]=6
Update [ADOPets_Rev].[dbo].[Subscription] set [HasMRA]=1,[MRACount]=1,[AmmountPerAddionalPet]=20 where [Id]=7
Update [ADOPets_Rev].[dbo].[Subscription] set [HasMRA]=1,[MRACount]=2,[AmmountPerAddionalPet]=20 where [Id]=8
Update [ADOPets_Rev].[dbo].[Subscription] set [HasMRA]=1,[MRACount]=3,[AmmountPerAddionalPet]=20 where [Id]=9