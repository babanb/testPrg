USE [ADOPets_rev]
GO


CREATE TABLE [dbo].[SMORequest](
	[ID] [bigint] NOT NULL,
	[Title] [nvarchar](200) NULL,
	[PetId] [int] NULL,
	[Diagnosis] [nvarchar](max) NULL,
	[DateOfOnSet] [date] NULL,
	[Comments] [nvarchar](max) NULL,
	[Symptoms1] [nvarchar](max) NULL,
	[Symptoms2] [nvarchar](max) NULL,
	[Symptoms3] [nvarchar](max) NULL,
	[FirstOpinion] [nvarchar](max) NULL,
	[SMORequestStatusId] [int] NULL,
	[VetDirectorID] [int] NULL,
	[Question] [nvarchar](max) NULL,
	[VetComment] [nvarchar](max) NULL,
	[ValidatedOn] [datetime] NULL,
	[ClosedOn] [datetime] NULL,
	[InCompleteMedicalRecord] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_SMORequest] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SMORequest]  WITH CHECK ADD  CONSTRAINT [FK_SMORequest_Pet] FOREIGN KEY([PetId])
REFERENCES [dbo].[Pet] ([Id])
GO

ALTER TABLE [dbo].[SMORequest] CHECK CONSTRAINT [FK_SMORequest_Pet]
GO

ALTER TABLE [dbo].[SMORequest]  WITH CHECK ADD  CONSTRAINT [FK_SMORequest_SMORequestStatus] FOREIGN KEY([SMORequestStatusId])
REFERENCES [dbo].[SMORequestStatus] ([ID])
GO

ALTER TABLE [dbo].[SMORequest] CHECK CONSTRAINT [FK_SMORequest_SMORequestStatus]
GO

ALTER TABLE [dbo].[SMORequest]  WITH CHECK ADD  CONSTRAINT [FK_SMORequest_Veterinarian] FOREIGN KEY([VetDirectorID])
REFERENCES [dbo].[Veterinarian] ([Id])
GO

ALTER TABLE [dbo].[SMORequest] CHECK CONSTRAINT [FK_SMORequest_Veterinarian]
GO

