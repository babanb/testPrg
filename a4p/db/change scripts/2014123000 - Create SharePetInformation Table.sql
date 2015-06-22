
/****** Object:  Table [dbo].[SharePetInformation]    Script Date: 12/30/2014 17:20:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SharePetInformation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ShareCategoryTypeId] [int] NOT NULL,
	[ContactId] [int] NOT NULL,
	[PetId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[IsRead] [bit] NOT NULL,
 CONSTRAINT [PK_SharePetInformation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SharePetInformation]  WITH CHECK ADD  CONSTRAINT [FK_SharePetInformation_Pet] FOREIGN KEY([PetId])
REFERENCES [dbo].[Pet] ([Id])
GO

ALTER TABLE [dbo].[SharePetInformation] CHECK CONSTRAINT [FK_SharePetInformation_Pet]
GO

ALTER TABLE [dbo].[SharePetInformation]  WITH CHECK ADD  CONSTRAINT [FK_SharePetInformation_ShareCategoryType] FOREIGN KEY([ShareCategoryTypeId])
REFERENCES [dbo].[ShareCategoryType] ([Id])
GO

ALTER TABLE [dbo].[SharePetInformation] CHECK CONSTRAINT [FK_SharePetInformation_ShareCategoryType]
GO

ALTER TABLE [dbo].[SharePetInformation]  WITH CHECK ADD  CONSTRAINT [FK_SharePetInformation_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[SharePetInformation] CHECK CONSTRAINT [FK_SharePetInformation_User]
GO

ALTER TABLE [dbo].[SharePetInformation]  WITH CHECK ADD  CONSTRAINT [FK_SharePetInformation_UserContact] FOREIGN KEY([ContactId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[SharePetInformation] CHECK CONSTRAINT [FK_SharePetInformation_UserContact]
GO


