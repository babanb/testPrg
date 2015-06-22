USE [ADOPets_rev]
GO

CREATE TABLE [dbo].[SMOExpertRelation](
	[ID] [bigint] NOT NULL,
	[SMORequestID] [bigint] NULL,
	[VetExpertID] [int] NULL,
	[AssingedDate] [datetime] NULL,
	[ExpertResponse] [nvarchar](max) NULL,
 CONSTRAINT [PK_SMOExpertRelation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SMOExpertRelation]  WITH CHECK ADD  CONSTRAINT [FK_SMOExpertRelation_SMORequest] FOREIGN KEY([SMORequestID])
REFERENCES [dbo].[SMORequest] ([ID])
GO

ALTER TABLE [dbo].[SMOExpertRelation] CHECK CONSTRAINT [FK_SMOExpertRelation_SMORequest]
GO

ALTER TABLE [dbo].[SMOExpertRelation]  WITH CHECK ADD  CONSTRAINT [FK_SMOExpertRelation_Veterinarian] FOREIGN KEY([VetExpertID])
REFERENCES [dbo].[Veterinarian] ([Id])
GO

ALTER TABLE [dbo].[SMOExpertRelation] CHECK CONSTRAINT [FK_SMOExpertRelation_Veterinarian]
GO

