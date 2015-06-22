use ADOPets_Rev

ALTER TABLE Subscription DROP CONSTRAINT FK_Subscription_EConsultationPaymentType

drop table EConsultationPaymentType

ALTER TABLE EConsultation ADD CONSTRAINT FK_EConsultation_PaymentType FOREIGN KEY (PaymentType) references PaymentType(ID)


ALTER TABLE EConsultation ADD Symptoms1 nvarchar(max),Symptoms2 nvarchar(max), Symptoms3 nvarchar(max)

select * from Subscription

update Subscription set HasEConsultation = 0, EConsultationCount = 0

ALTER TABLE EConsultation ADD EConsultationContactValue nvarchar(max)