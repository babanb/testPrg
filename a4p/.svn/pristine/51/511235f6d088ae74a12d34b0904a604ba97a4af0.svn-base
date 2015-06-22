USE [ADOPets_Rev]
GO


--Drop table ShareCategoryTypeContact, ShareContact, ShareCategoryType
IF OBJECT_ID('dbo.[ShareCategoryTypeContact]', 'U') IS NOT NULL
  DROP TABLE dbo.[ShareCategoryTypeContact]

GO
IF OBJECT_ID('dbo.[ShareContact]', 'U') IS NOT NULL
  DROP TABLE dbo.[ShareContact]

GO


--Create table for the ShareContact 

/****** Object:  Table [dbo].[ShareContact]    Script Date: 10/30/2014 18:59:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ShareContact](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ContactId] [int] NOT NULL,
 CONSTRAINT [PK_ShareContact] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ShareContact]  WITH CHECK ADD  CONSTRAINT [FK_ShareContact_User1] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[ShareContact] CHECK CONSTRAINT [FK_ShareContact_User1]
GO

ALTER TABLE [dbo].[ShareContact]  WITH CHECK ADD  CONSTRAINT [FK_ShareContact_User2] FOREIGN KEY([ContactId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[ShareContact] CHECK CONSTRAINT [FK_ShareContact_User2]
GO



--Create table for the relationship between ShareContact and ShareContegoryType

GO

/****** Object:  Table [dbo].[ShareCategoryTypeContact]    Script Date: 10/30/2014 19:06:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ShareCategoryTypeContact](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ShareContactId] [int] NOT NULL,
	[ShareCategoryTypeId] [int] NOT NULL,
 CONSTRAINT [PK_ShareCategoryTypeContact] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ShareCategoryTypeContact]  WITH CHECK ADD  CONSTRAINT [FK_ShareCTypeContact_ShareCategoryType] FOREIGN KEY([ShareCategoryTypeId])
REFERENCES [dbo].[ShareCategoryType] ([Id])
GO

ALTER TABLE [dbo].[ShareCategoryTypeContact] CHECK CONSTRAINT [FK_ShareCTypeContact_ShareCategoryType]
GO

ALTER TABLE [dbo].[ShareCategoryTypeContact]  WITH CHECK ADD  CONSTRAINT [FK_ShareCTypeContact_ShareContact] FOREIGN KEY([ShareContactId])
REFERENCES [dbo].[ShareContact] ([Id])
GO

ALTER TABLE [dbo].[ShareCategoryTypeContact] CHECK CONSTRAINT [FK_ShareCTypeContact_ShareContact]
GO


