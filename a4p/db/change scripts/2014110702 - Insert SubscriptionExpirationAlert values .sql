use ADOPets_Rev

INSERT INTO [dbo].[SubscriptionExpirationAlert]
           ([Id]
           ,[Name]
           ,[DaysBefore])
     VALUES
           (1, 'Not Sent', 0), 
           (2, 'Ten Days Before', 10), 
           (3, 'Twenty Days Before', 20), 
           (4, 'Thirty Days Before', 30)
GO


