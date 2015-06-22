USE [ADOPets_Rev]
GO

/****** Object:  Table [dbo].[Calendar]    Script Date: 07/28/2014 16:11:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Calendar](
	[Id] [int] NOT NULL,
	[Reason] [nvarchar](max) NOT NULL,
	[Physician] [nvarchar](max) NOT NULL,
	[Date] [datetime] NULL,
	[Comment] [nvarchar](max) NULL,
	[UserId] [int] NOT NULL,
	[PetId] [int] NULL,
	[SendNotificationMail] [bit] NOT NULL,
	[NotificationSent] [bit] NOT NULL,
 CONSTRAINT [PK_Calendar] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Calendar]  WITH CHECK ADD  CONSTRAINT [FK_Calendar_Pet] FOREIGN KEY([PetId])
REFERENCES [dbo].[Pet] ([Id])
GO

ALTER TABLE [dbo].[Calendar] CHECK CONSTRAINT [FK_Calendar_Pet]
GO

ALTER TABLE [dbo].[Calendar]  WITH CHECK ADD  CONSTRAINT [FK_Calendar_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[Calendar] CHECK CONSTRAINT [FK_Calendar_User]
GO


