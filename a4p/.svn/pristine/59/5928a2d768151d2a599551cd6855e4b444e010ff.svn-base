USE [ADOPets_rev]
GO

/****** Object:  Table [dbo].[SMOExpertCommittee]    Script Date: 11/17/2014 16:17:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SMOExpertCommittee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VeterinaryExpertId] [int] NULL,
	[SMORequestId] [bigint] NULL,
	[Message] [nvarchar](max) NULL,
	[Date] [datetime] NULL,
 CONSTRAINT [PK_SMOExpertCommittee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SMOExpertCommittee]  WITH CHECK ADD  CONSTRAINT [FK_SMOExpertCommittee_SMORequest] FOREIGN KEY([SMORequestId])
REFERENCES [dbo].[SMORequest] ([ID])
GO

ALTER TABLE [dbo].[SMOExpertCommittee] CHECK CONSTRAINT [FK_SMOExpertCommittee_SMORequest]
GO

ALTER TABLE [dbo].[SMOExpertCommittee]  WITH CHECK ADD  CONSTRAINT [FK_SMOExpertCommittee_Veterinarian] FOREIGN KEY([VeterinaryExpertId])
REFERENCES [dbo].[Veterinarian] ([Id])
GO

ALTER TABLE [dbo].[SMOExpertCommittee] CHECK CONSTRAINT [FK_SMOExpertCommittee_Veterinarian]
GO


