USE [ADOPets_Rev]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PetContact](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[Address1] [nvarchar](max) NULL,
	[Address2] [nvarchar](max) NULL,
	[City] [nvarchar](max) NULL,
	[StateId] [int] NULL,
	[CountryId] [int] NULL,
	[Zip] [nvarchar](max) NULL,
	[PhoneHome] [nvarchar](max) NULL,
	[PhoneOffice] [nvarchar](max) NULL,
	[PhoneCell] [nvarchar](max) NULL,
	[Fax] [nvarchar](max) NULL,
	[Comments] [nvarchar](max) NULL,
	[IsEmergencyContact] [bit] NOT NULL,
	[ContactTypeId] [int] NOT NULL,
	[PetId] [int] NOT NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_PetContact] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PetContact]  WITH CHECK ADD  CONSTRAINT [FK_PetContact_ContactType] FOREIGN KEY([ContactTypeId])
REFERENCES [dbo].[ContactType] ([Id])
GO

ALTER TABLE [dbo].[PetContact]  WITH CHECK ADD  CONSTRAINT [FK_PetContact_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([Id])
GO

ALTER TABLE [dbo].[PetContact] CHECK CONSTRAINT [FK_PetContact_Country]
GO

ALTER TABLE [dbo].[PetContact]  WITH CHECK ADD  CONSTRAINT [FK_PetContact_Pet] FOREIGN KEY([PetId])
REFERENCES [dbo].[Pet] ([Id])
GO

ALTER TABLE [dbo].[PetContact] CHECK CONSTRAINT [FK_PetContact_Pet]
GO

ALTER TABLE [dbo].[PetContact]  WITH CHECK ADD  CONSTRAINT [FK_PetContact_State] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([Id])
GO

ALTER TABLE [dbo].[PetContact] CHECK CONSTRAINT [FK_PetContact_State]
GO

ALTER TABLE [dbo].[PetContact]  WITH CHECK ADD  CONSTRAINT [FK_PetContact_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[PetContact] CHECK CONSTRAINT [FK_PetContact_User]
GO


