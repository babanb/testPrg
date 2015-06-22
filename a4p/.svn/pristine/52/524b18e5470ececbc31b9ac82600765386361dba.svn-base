
ALTER TABLE [dbo].[SMORequest] 
DROP CONSTRAINT [FK_SMORequest_Veterinarian] 

GO

ALTER TABLE [dbo].[SMORequest]  
DROP CONSTRAINT [FK_SMORequest_Veterinarian1] 

GO



ALTER TABLE [dbo].[SMORequest] 
DROP Column [VetDirectorID]

GO




ALTER TABLE [dbo].[SMORequest]  WITH CHECK ADD  CONSTRAINT [FK_SMORequest_User1] FOREIGN KEY([SMOSubmittedBy])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[SMORequest] CHECK CONSTRAINT [FK_SMORequest_User1]
GO


ALTER TABLE [dbo].[SMORequest] 
ADD  [IsSMOPaymentDone]  bit not null default (1)

GO