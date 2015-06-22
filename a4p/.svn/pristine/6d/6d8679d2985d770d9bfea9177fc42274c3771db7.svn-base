USE [ADOPets_Rev]
GO

/****** Object:  Table [dbo].[Veterinarian]    Script Date: 07/08/2014 11:53:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Veterinarian](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](200) NOT NULL,
	[LastName] [nvarchar](200) NOT NULL,
	[MiddleName] [nvarchar](200) NULL,
	[Email] [nvarchar](100) NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[IsEmergencyContact] [bit] NOT NULL,
	[IsCurrentVeterinarian] [bit] NOT NULL,
	[NPI] [nvarchar](100) NULL,
	[HospitalName] [nvarchar](200) NULL,
	[Address1] [nvarchar](200) NULL,
	[Address2] [nvarchar](200) NULL,
	[City] [nvarchar](100) NULL,
	[StateId] [int] NULL,
	[CountryId] [int] NULL,
	[Zip] [nvarchar](100) NULL,
	[PhoneHome] [nvarchar](100) NULL,
	[PhoneOffice] [nvarchar](100) NULL,
	[PhoneCell] [nvarchar](100) NULL,
	[Fax] [nvarchar](100) NULL,
	[PetId] [int] NOT NULL,
	[UserId] [int] NULL,
	[Comments] [nvarchar](max) NULL,
 CONSTRAINT [PK_Veterinarian] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Veterinarian]  WITH CHECK ADD  CONSTRAINT [FK_Veterinarian_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([Id])
GO

ALTER TABLE [dbo].[Veterinarian] CHECK CONSTRAINT [FK_Veterinarian_Country]
GO

ALTER TABLE [dbo].[Veterinarian]  WITH CHECK ADD  CONSTRAINT [FK_Veterinarian_Pet] FOREIGN KEY([PetId])
REFERENCES [dbo].[Pet] ([Id])
GO

ALTER TABLE [dbo].[Veterinarian] CHECK CONSTRAINT [FK_Veterinarian_Pet]
GO

ALTER TABLE [dbo].[Veterinarian]  WITH CHECK ADD  CONSTRAINT [FK_Veterinarian_State] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([Id])
GO

ALTER TABLE [dbo].[Veterinarian] CHECK CONSTRAINT [FK_Veterinarian_State]
GO

ALTER TABLE [dbo].[Veterinarian]  WITH CHECK ADD  CONSTRAINT [FK_Veterinarian_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[Veterinarian] CHECK CONSTRAINT [FK_Veterinarian_User]
GO


