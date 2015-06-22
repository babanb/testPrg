USE [ADOPets_Rev]
GO

/****** Object:  Table [dbo].[OwnerHistory]    Script Date: 07/08/2014 09:54:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OwnerHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Email] [nvarchar](100) NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[Address1] [nvarchar](max) NULL,
	[Address2] [nvarchar](max) NULL,
	[City] [nvarchar](200) NULL,
	[StateId] [int] NULL,
	[CountryId] [int] NULL,
	[Zip] [nvarchar](100) NULL,
	[PhoneHome] [nvarchar](100) NULL,
	[PhoneOffice] [nvarchar](100) NULL,
	[PhoneCell] [nvarchar](100) NULL,
	[Fax] [nvarchar](100) NULL,
	[Comments] [nvarchar](max) NULL,
	[IsCurrentOwner] [bit] NOT NULL,
	[IsEmergencyContact] [bit] NOT NULL,
	[PetId] [int] NOT NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_OwnerHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[OwnerHistory]  WITH CHECK ADD  CONSTRAINT [FK_OwnerHistory_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([Id])
GO

ALTER TABLE [dbo].[OwnerHistory] CHECK CONSTRAINT [FK_OwnerHistory_Country]
GO

ALTER TABLE [dbo].[OwnerHistory]  WITH CHECK ADD  CONSTRAINT [FK_OwnerHistory_Pet] FOREIGN KEY([PetId])
REFERENCES [dbo].[Pet] ([Id])
GO

ALTER TABLE [dbo].[OwnerHistory] CHECK CONSTRAINT [FK_OwnerHistory_Pet]
GO

ALTER TABLE [dbo].[OwnerHistory]  WITH CHECK ADD  CONSTRAINT [FK_OwnerHistory_State] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([Id])
GO

ALTER TABLE [dbo].[OwnerHistory] CHECK CONSTRAINT [FK_OwnerHistory_State]
GO

ALTER TABLE [dbo].[OwnerHistory]  WITH CHECK ADD  CONSTRAINT [FK_OwnerHistory_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[OwnerHistory] CHECK CONSTRAINT [FK_OwnerHistory_User]
GO


