use ADOPets_Rev

Delete FROM [dbo].[PetAllergy]
Delete FROM [dbo].[PetCondition]
Delete FROM [dbo].[PetConsultation]
Delete FROM [dbo].[PetDocument]
Delete FROM [dbo].[PetFoodPlan]
Delete FROM [dbo].[PetHealthMeasure]
Delete FROM [dbo].[PetHospitalization]
Delete FROM [dbo].[PetMedication]
Delete FROM [dbo].[PetSurgery]
Delete FROM [dbo].[PetUser]
Delete FROM [dbo].[PetVaccination]
Delete FROM [dbo].[Contact]
Delete FROM [dbo].[Insurance]
Delete FROM [dbo].[Login]
Delete FROM [dbo].[Veterinarian]
Delete FROM [dbo].[PetContact]
Delete FROM [dbo].[Notification]

Delete FROM [dbo].[Calendar]
Delete FROM [dbo].[Message]
Delete FROM [dbo].[PetUser]
Delete FROM [dbo].Gallery
Delete FROM [dbo].VideoGallery
Delete FROM [dbo].[Pet]
Delete FROM [dbo].[User] where UserTypeId !=1
Delete FROM [dbo].[PaymentHistory]
Delete FROM [dbo].[UserSubscription]

delete from Subscription where Id=2
delete from Subscription where Id=3
delete from Subscription where Id=4
delete from Subscription where Id=5
delete from Subscription where Id=6


