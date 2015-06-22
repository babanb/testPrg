USE [ADOPets_Rev]
GO

/****** Object:  Table [dbo].[ShareContact]    Script Date: 10/28/2014 10:36:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ShareContact](
	[Id] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[ContactId] [int] NOT NULL
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