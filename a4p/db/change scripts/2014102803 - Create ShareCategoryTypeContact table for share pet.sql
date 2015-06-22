USE [ADOPets_Rev]
GO

/****** Object:  Table [dbo].[ShareCategoryTypeContact]   Script Date: 10/28/2014 10:44:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ShareCategoryTypeContact](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ShareContactId] [int] NOT NULL,
	[ShareCategoryTypeId] [int] NOT NULL
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

