USE [ADOPets_rev]
GO

/****** Object:  Table [dbo].[ExpertBioData]    Script Date: 11/17/2014 16:22:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ExpertBioData](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VeterinaryExpertId] [int] NULL,
	[VeterinaryId] [int] NULL,
	[Information] [nvarchar](max) NULL,
 CONSTRAINT [PK_ExpertBioData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ExpertBioData]  WITH CHECK ADD  CONSTRAINT [FK_ExpertBioData_Veterinarian] FOREIGN KEY([VeterinaryId])
REFERENCES [dbo].[Veterinarian] ([Id])
GO

ALTER TABLE [dbo].[ExpertBioData] CHECK CONSTRAINT [FK_ExpertBioData_Veterinarian]
GO

ALTER TABLE [dbo].[ExpertBioData]  WITH CHECK ADD  CONSTRAINT [FK_ExpertBioData_Veterinarian1] FOREIGN KEY([VeterinaryExpertId])
REFERENCES [dbo].[Veterinarian] ([Id])
GO

ALTER TABLE [dbo].[ExpertBioData] CHECK CONSTRAINT [FK_ExpertBioData_Veterinarian1]
GO


