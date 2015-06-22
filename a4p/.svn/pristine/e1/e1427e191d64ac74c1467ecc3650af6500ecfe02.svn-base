use ADOPets_Rev

alter table Subscription
add HasEConsultation bit 

alter table Subscription
add EConsultationCount int 

alter table Subscription
add EConsultationAdditionalPrice decimal(18,0) 

alter table Subscription
add EConsultationPaymentTypeId int 
GO

ALTER TABLE [dbo].[Subscription]  WITH CHECK ADD  CONSTRAINT [FK_Subscription_EConsultationPaymentType] FOREIGN KEY([EConsultationPaymentTypeId])
REFERENCES [dbo].[EConsultationPaymentType] ([Id])
GO